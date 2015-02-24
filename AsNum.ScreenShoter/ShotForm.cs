using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsNum.ScreenShoter {

    public class CompleteEventArgs : EventArgs {
        public Bitmap Img {
            get;
            set;
        }
    }

    [Flags]
    public enum ResizeTypes : byte {
        None = 0x00,
        Width = 0x01,
        Height = 0x02,
        X = 0x04,
        Y = 0x08
    }

    static class NativeMethods {
        [DllImport("user32")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);

        [DllImport("user32")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    public partial class ShotForm : Form {

        private Bitmap ScreenShortImg = null;

        #region 选取区域用到的
        public event EventHandler<CompleteEventArgs> SelectComplete;

        private bool IsMouseDown = false;
        private Point MouseDownPoint = Point.Empty;
        private Rectangle SelectedRect = Rectangle.Empty;
        private Pen SelectedRectPen = new Pen(Color.Red, 2L);
        private Brush SelectedRectBrush = new SolidBrush(Color.FromArgb(40, Color.Gray));

        private Rectangle[] TouchPoints = new Rectangle[8];
        private static readonly int TouchPointWH = 8;

        /// <summary>
        /// 触控点对应的鼠标样式
        /// </summary>
        private Cursor[] TouchPointCursors = new Cursor[8] {
            Cursors.SizeNWSE,
            Cursors.SizeNS,
            Cursors.SizeNESW,
            Cursors.SizeWE,
            Cursors.SizeWE,
            Cursors.SizeNESW,
            Cursors.SizeNS,
            Cursors.SizeNWSE        
        };

        /// <summary>
        /// 辅助线的点
        /// </summary>
        private Point[][] GuideLines = new Point[4][];
        #endregion

        #region 移动所选区域时用到的
        private bool IsMoveRegion = false;
        private Point MoveRegionStartPoint = Point.Empty;
        #endregion

        #region 改变大小
        private bool IsResize = false;
        private Point ResizeStartPoint = Point.Empty;
        private ResizeTypes ResizeType = ResizeTypes.None;

        private ResizeTypes[] CanResizeType = new ResizeTypes[8] {
            //左上角
            ResizeTypes.X | ResizeTypes.Y,
            ResizeTypes.Y,
            //右上角
            ResizeTypes.Width | ResizeTypes.Y,
            ResizeTypes.X,
            ResizeTypes.Width,
            //左下角
            ResizeTypes.Height | ResizeTypes.X,
            ResizeTypes.Height,
            //右下角
            ResizeTypes.Height | ResizeTypes.Width,
        };

        #endregion

        #region 辅助显示框
        private Rectangle ZoomRect = new Rectangle(0, 0, 100, 100);
        private Pen ZoomPen = new Pen(Color.Yellow, 3L);
        #endregion

        public ShotForm() {
            InitializeComponent();

            #region 窗体样式
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.Visible = false;
            this.TopLevel = true;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Cursor = Cursors.Cross;
            #endregion

            #region 注册热键
            if (!NativeMethods.RegisterHotKey(this.Handle, 100, 0, Keys.PrintScreen)) {
                MessageBox.Show("热键注册失败");
            }
            #endregion

            #region 事件
            this.Paint += ShotForm_Paint;
            this.MouseDown += ShotForm_MouseDown;
            this.MouseMove += ShotForm_MouseMove;
            this.MouseUp += ShotForm_MouseUp;

            this.DoubleClick += ShotForm_DoubleClick;
            #endregion
        }

        /// <summary>
        /// 重置标识
        /// </summary>
        private void ResetFlag() {
            this.MouseDownPoint = Point.Empty;
            this.SelectedRect = Rectangle.Empty;
            this.Cursor = Cursors.Arrow;
            this.IsMouseDown = false;

            this.IsMoveRegion = false;
            this.MoveRegionStartPoint = Point.Empty;
        }

        #region 事件

        void ShotForm_DoubleClick(object sender, EventArgs e) {
            var clickPoint = this.PointToClient(Control.MousePosition);
            if (this.SelectedRect != Rectangle.Empty
                && this.SelectedRect.Contains(clickPoint)) {

                if (this.SelectComplete != null) {
                    using (var img = new Bitmap(this.SelectedRect.Width, this.SelectedRect.Height))
                    using (Graphics g = Graphics.FromImage(img)) {
                        var destRect = new Rectangle(0, 0, img.Width, img.Height);
                        g.DrawImage(this.ScreenShortImg, destRect, this.SelectedRect, GraphicsUnit.Pixel);

                        this.Hide();
                        this.SelectComplete(this, new CompleteEventArgs() {
                            Img = img
                        });
                    }
                }
            }
            this.ResetFlag();
        }

        void ShotForm_MouseUp(object sender, MouseEventArgs e) {
            this.IsMouseDown = false;
            this.IsMoveRegion = false;
            this.IsResize = false;
        }

        void ShotForm_MouseMove(object sender, MouseEventArgs e) {
            var mp = this.PointToClient(Control.MousePosition);

            this.ZoomRect.Location = mp;

            if (this.IsMouseDown) {
                this.SetRects(mp);
                //this.Invalidate();
            } else if (this.SelectedRect != Rectangle.Empty) {
                this.SetCursor();

                if (this.IsMoveRegion && this.SelectedRect.Contains(mp)) {
                    //移动所选区域
                    var x = mp.X - this.MoveRegionStartPoint.X;
                    var y = mp.Y - this.MoveRegionStartPoint.Y;

                    this.SetRects(null, x, y);
                    this.MoveRegionStartPoint.Offset(x, y);
                    //this.Invalidate();
                } else if (IsResize && this.ResizeStartPoint != Point.Empty) {
                    // Resize
                    var x = mp.X - this.ResizeStartPoint.X;
                    var y = mp.Y - this.ResizeStartPoint.Y;
                    this.SetRects(null, x, y, this.ResizeType);
                    this.ResizeStartPoint.Offset(x, y);
                    //this.Invalidate();
                }
            }

            this.Invalidate();
        }

        void ShotForm_MouseDown(object sender, MouseEventArgs e) {
            var mp = this.PointToClient(Control.MousePosition);
            if (this.MouseDownPoint == Point.Empty && this.SelectedRect == Rectangle.Empty) {
                this.IsMouseDown = true;
                this.MouseDownPoint = mp;
                this.SelectedRect = Rectangle.Empty;

                this.GuideLines[0] = new Point[] { new Point(0, this.MouseDownPoint.Y), new Point(Screen.PrimaryScreen.Bounds.Width, this.MouseDownPoint.Y) };
                this.GuideLines[1] = new Point[] { new Point(this.MouseDownPoint.X, 0), new Point(this.MouseDownPoint.X, Screen.PrimaryScreen.Bounds.Height) };
            } else if (this.SelectedRect != Rectangle.Empty && this.SelectedRect.Contains(mp)) {
                //在选择区域内按下鼠标，代表想移动所选区域
                this.IsMoveRegion = true;
                this.IsResize = false;
                this.MoveRegionStartPoint = this.PointToClient(Control.MousePosition);
            } else if (this.SelectedRect != Rectangle.Empty && this.TouchPoints.FirstOrDefault(p => p.Contains(mp)) != Rectangle.Empty) {
                // Resize
                var point = this.TouchPoints.FirstOrDefault(p => p.Contains(mp));
                var idx = this.TouchPoints.ToList().FindIndex(p => p.Equals(point));
                this.IsResize = true;
                this.IsMoveRegion = false;
                this.ResizeStartPoint = mp;
                this.ResizeType = this.CanResizeType.ElementAt(idx);
            }
        }

        void ShotForm_Paint(object sender, PaintEventArgs e) {

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (this.SelectedRect != Rectangle.Empty) {
                g.DrawLines(Pens.ForestGreen, this.GuideLines[0]);
                g.DrawLines(Pens.ForestGreen, this.GuideLines[1]);
                g.DrawLines(Pens.ForestGreen, this.GuideLines[2]);
                g.DrawLines(Pens.ForestGreen, this.GuideLines[3]);

                g.DrawRectangle(this.SelectedRectPen, this.SelectedRect);
                g.FillRectangle(this.SelectedRectBrush, this.SelectedRect);

                g.DrawRectangles(Pens.Black, this.TouchPoints);
                g.FillRectangles(Brushes.Gray, this.TouchPoints);
            }

            #region 放大镜
            g.DrawEllipse(this.ZoomPen, this.ZoomRect);
            using (var img = new Bitmap(this.ZoomRect.Width, this.ZoomRect.Height))
            using (var imgG = Graphics.FromImage(img)) {

                var mp = this.PointToClient(Control.MousePosition);
                imgG.DrawImage(this.ScreenShortImg,
                    new Rectangle(Point.Empty, img.Size),
                    new Rectangle(mp.X - this.ZoomRect.Width / 2, mp.Y - this.ZoomRect.Height / 2, this.ZoomRect.Width, this.ZoomRect.Height),
                    GraphicsUnit.Pixel);

                #region 放大辅助图片
                using (var img2 = new Bitmap(img, img.Width * 5, img.Height * 5)) {

                    imgG.Clear(Color.Transparent);

                    #region 图片显示在圆形中
                    var imgRect = new Rectangle(0, 0, this.ZoomRect.Width, this.ZoomRect.Height);
                    using (var path = new GraphicsPath()) {
                        path.AddRectangle(imgRect);
                        path.AddEllipse(imgRect);
                        using (System.Drawing.Region reg = new System.Drawing.Region(path)) {
                            imgG.ExcludeClip(reg);
                        }
                    }
                    #endregion

                    imgG.DrawImage(img2,
                        new Rectangle(Point.Empty, img.Size),
                        new Rectangle((img2.Width - img.Width) / 2, (img2.Height - img.Height) / 2, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                #endregion

                g.DrawImage(img, this.ZoomRect.Location);

                //using(var fb = new TextureBrush(img , WrapMode.Tile)) {
                //    g.FillEllipse(fb , this.ZoomRect);
                //}
            }
            #endregion

            #region 放大镜辅助线
            g.DrawLine(Pens.Blue, this.ZoomRect.X, this.ZoomRect.Y + this.ZoomRect.Height / 2, this.ZoomRect.Right, this.ZoomRect.Y + this.ZoomRect.Height / 2);
            g.DrawLine(Pens.Blue, this.ZoomRect.X + this.ZoomRect.Width / 2, this.ZoomRect.Y, this.ZoomRect.X + this.ZoomRect.Width / 2, this.ZoomRect.Bottom);
            #endregion

            //using(var ellipsePath = new GraphicsPath()) {
            //    ellipsePath.AddEllipse(this.ZoomRect);
            //    using(var brush = new PathGradientBrush(ellipsePath)) {
            //        brush.CenterColor = Color.FromArgb(0, Color.White);
            //        brush.SurroundColors = new Color[] { Color.FromArgb(180, Color.Yellow) };
            //        g.FillEllipse(brush, this.ZoomRect);
            //    }
            //}
        }
        #endregion


        /// <summary>
        /// 设置鼠标样式
        /// </summary>
        private void SetCursor() {
            var mp = this.PointToClient(Control.MousePosition);
            var inTouchPoint = this.TouchPoints.FirstOrDefault(p => p.Contains(mp));
            if (this.SelectedRect.Contains(mp))
                this.Cursor = Cursors.SizeAll;
            else if (inTouchPoint != Rectangle.Empty) {
                var idx = this.TouchPoints.ToList().FindIndex(p => p.Equals(inTouchPoint));
                this.Cursor = this.TouchPointCursors.ElementAt(idx);
            } else {
                this.Cursor = Cursors.Arrow;
            }

        }

        private void SetResize(ref Rectangle rect, int offsetX, int offsetY, ResizeTypes type) {

            //this.Text = type.ToString();

            if ((type & ResizeTypes.Width) == ResizeTypes.Width)
                rect.Width += offsetX;

            if ((type & ResizeTypes.Height) == ResizeTypes.Height)
                rect.Height += offsetY;

            if ((type & ResizeTypes.X) == ResizeTypes.X) {
                rect.X += offsetX;
                rect.Width -= offsetX;
            }

            if ((type & ResizeTypes.Y) == ResizeTypes.Y) {
                rect.Y += offsetY;
                rect.Height -= offsetY;
            }
        }

        /// <summary>
        /// 设置要绘制的区域
        /// </summary>
        private void SetRects(Point? endPoint, int offsetX = 0, int offsetY = 0, ResizeTypes resizeType = ResizeTypes.None) {

            if (endPoint.HasValue) {
                var x = Math.Min(endPoint.Value.X, this.MouseDownPoint.X);
                var y = Math.Min(endPoint.Value.Y, this.MouseDownPoint.Y);
                var width = Math.Abs(endPoint.Value.X - this.MouseDownPoint.X);
                var height = Math.Abs(endPoint.Value.Y - this.MouseDownPoint.Y);
                this.SelectedRect = new Rectangle(x, y, width, height);
            }

            if (resizeType == ResizeTypes.None)
                this.SelectedRect.Offset(offsetX, offsetY);
            else
                this.SetResize(ref this.SelectedRect, offsetX, offsetY, resizeType);

            this.SetTouchPoints();
            this.SetGuideLines();
        }

        private void SetTouchPoints() {

            #region TouchPoints
            var t = TouchPointWH / 2;
            var baseSize = new Size(this.SelectedRect.Left, this.SelectedRect.Top);

            var points = new List<int[]>(){
                    new int[]{-t, - t},//上左
                    new int[]{this.SelectedRect.Width / 2 - t, -t},//上中
                    new int[]{this.SelectedRect.Width - t, -t},//上右

                    new int[]{-t, this.SelectedRect.Height / 2 -t}, //中左
                    new int[]{this.SelectedRect.Width - t, this.SelectedRect.Height / 2 -t},//中右

                    new int[]{-t, this.SelectedRect.Height - t},//下左
                    new int[]{this.SelectedRect.Width / 2 - t, this.SelectedRect.Height - t},//下中
                    new int[]{this.SelectedRect.Width - t, this.SelectedRect.Height - t}//下右
                };

            this.TouchPoints = points.Select(p => {
                var point = new Point(p[0], p[1]);
                point = Point.Add(point, baseSize);
                var rect = new Rectangle(point.X, point.Y, TouchPointWH, TouchPointWH);
                //rect.Offset(offsetX, offsetY);
                return rect;
            }).ToArray();
            #endregion

        }

        private void SetGuideLines() {
            //左上角的横线
            this.GuideLines[0] = new Point[2];
            this.GuideLines[0][0] = new Point(0, this.SelectedRect.Top);
            this.GuideLines[0][1] = new Point(Screen.PrimaryScreen.Bounds.Right, this.SelectedRect.Top);
            //左上角的竖线
            this.GuideLines[1] = new Point[2];
            this.GuideLines[1][0] = new Point(this.SelectedRect.Left, 0);
            this.GuideLines[1][1] = new Point(this.SelectedRect.Left, Screen.PrimaryScreen.Bounds.Bottom);
            //右下角的横线
            this.GuideLines[2] = new Point[2];
            this.GuideLines[2][0] = new Point(0, this.SelectedRect.Bottom);
            this.GuideLines[2][1] = new Point(Screen.PrimaryScreen.Bounds.Right, this.SelectedRect.Bottom);
            //右下角的竖线
            this.GuideLines[3] = new Point[2];
            this.GuideLines[3][0] = new Point(this.SelectedRect.Right, 0);
            this.GuideLines[3][1] = new Point(this.SelectedRect.Right, Screen.PrimaryScreen.Bounds.Bottom);
        }

        /// <summary>
        /// 抓取当前屏幕
        /// </summary>
        private void PrintScreen() {
            Screen sc = Screen.PrimaryScreen;
            this.ScreenShortImg = new Bitmap(sc.Bounds.Width, sc.Bounds.Height);
            Graphics g = Graphics.FromImage(this.ScreenShortImg);
            g.CopyFromScreen(Point.Empty, Point.Empty, sc.Bounds.Size);
            g.DrawImage(this.ScreenShortImg, Point.Empty);
            this.BackgroundImage = this.ScreenShortImg;
        }

        #region 热键处理
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape) {
                this.WindowState = FormWindowState.Minimized;
                this.ResetFlag();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosed(EventArgs e) {
            NativeMethods.UnregisterHotKey(this.Handle, 100);
            base.OnClosed(e);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0X312 && m.WParam.ToString() == "100") {
                this.Launch();
            }
            base.WndProc(ref m);
        }

        public void Launch() {
            this.ResetFlag();
            this.PrintScreen();
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            this.Show();
        }
        #endregion
    }
}
