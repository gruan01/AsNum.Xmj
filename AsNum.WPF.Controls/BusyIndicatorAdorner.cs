using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace AsNum.WPF.Controls {
    public class BusyIndicatorAdorner : Adorner {

        #region maskType
        private MaskTypes maskType;
        public MaskTypes MaskType {
            get {
                return this.maskType;
            }
            set {
                this.maskType = value;
                this.Indicator.MaskType = value;
            }
        }
        #endregion

        #region Text
        private string text;
        public string Text {
            get {
                return this.text;
            }
            set {
                this.text = value;
                this.Indicator.Text = value;
            }
        }
        #endregion

        #region contentControlTemplate
        private ControlTemplate contentControlTemplate;
        public ControlTemplate ContentControlTemplate {
            get {
                return this.contentControlTemplate;
            }
            set {
                this.contentControlTemplate = value;
                this.Indicator.ContentControlTemplate = value;
            }
        }
        #endregion

        public BusyIndicator Indicator {
            get;
            private set;
        }

        protected override int VisualChildrenCount {
            get {
                return 1;
            }
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index) {
            return this.Indicator;
        }

        public BusyIndicatorAdorner(FrameworkElement adornered)
            : base(adornered) {

            this.Indicator = new BusyIndicator();
            this.AddVisualChild(this.Indicator);
        }

        protected override Size MeasureOverride(Size constraint) {
            Size s = Size.Empty;
            switch (this.MaskType) {
                case MaskTypes.None:
                case MaskTypes.Adorned:
                    this.AdornedElement.Measure(constraint);
                    var ele = this.AdornedElement as FrameworkElement;
                    s = new Size(ele.ActualWidth, ele.ActualHeight);
                    //s = this.AdornedElement.DesiredSize;
                    break;
                case MaskTypes.Window:
                    var w = this.AdornedElement.GetParentWindow();
                    s = new Size(w.ActualWidth, w.ActualHeight - ((SystemParameters.ResizeFrameHorizontalBorderHeight * 2) + SystemParameters.WindowCaptionHeight));
                    break;
            }
            return s;
        }

        protected override Size ArrangeOverride(Size finalSize) {
            this.Indicator.Measure(finalSize);
            this.Indicator.Arrange(new Rect(finalSize));
            return finalSize;
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform) {
            if (this.MaskType == MaskTypes.Window) {
                //变换Adorner 的起点
                var w = this.AdornedElement.GetParentWindow();
                var a = this.AdornedElement.TransformToAncestor(w);
                var b = a.Transform(new Point(0, 0));

                TransformGroup group = new TransformGroup();
                var t = new TranslateTransform(-b.X, -b.Y + ((SystemParameters.ResizeFrameHorizontalBorderHeight * 2) + SystemParameters.WindowCaptionHeight));
                group.Children.Add(t);

                group.Children.Add(transform as Transform);
                return group;
            } else {
                return base.GetDesiredTransform(transform);
            }
        }
    }
}
