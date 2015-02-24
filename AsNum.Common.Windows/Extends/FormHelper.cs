using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsNum.Common.Extends {
    public static class FormHelper {

        public static void BeginInvokeIfNeed(this Control ctr, Action act) {
            if(ctr.InvokeRequired)
                ctr.BeginInvoke(act);
            else
                act();
        }

        public static void InvokeIfNeed(this Control ctr, Action act) {
            if(ctr.InvokeRequired)
                ctr.Invoke(act);
            else
                act();
        }

        public static void InvokeIfNeeded<T>(this Control ctr, Action<T> act, T args) {
            if(ctr.InvokeRequired)
                ctr.Invoke(act, args);
            else
                act(args);
        }

        /// <summary>
        /// 异步Invoke (BeginInvoke)
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="act"></param>
        public static void AsyncInvokeIfNeed(this Control ctr, Action act) {
            if(ctr.InvokeRequired)
                ctr.BeginInvoke(act);
            else
                act();
        }

        /// <summary>
        /// 异步Invoke (BeginInvoke)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctr"></param>
        /// <param name="act"></param>
        /// <param name="args"></param>
        public static void AsyncInvokeIfNeeded<T>(this Control ctr, Action<T> act, T args) {
            if(ctr.InvokeRequired)
                ctr.BeginInvoke(act, args);
            else
                act(args);
        }

        /// <summary>
        /// 获取以打开的 MDIChild Form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindMdiChild<T>(string tag = null) where T : Form {
            if(tag == null) {
                return Application.OpenForms.OfType<T>().FirstOrDefault();
            } else {
                return Application.OpenForms.OfType<T>().FirstOrDefault(t => t.Tag != null && t.Tag.ToString().Equals(tag));
            }
        }

        /// <summary>
        /// 创建或获取MDIChild
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrCreateMDIChild<T>(string tag = null) where T : Form, new() {
            var form = FindMdiChild<T>(tag);
            if(form == null) {
                form = new T();
                form.AddToMdiContainer();
            }
            return form;
        }

        public static T GetOrCreateMDIChild<T>(string tag = null, params object[] args) where T : Form {
            var form = FindMdiChild<T>(tag);
            if(form == null) {
                form = (T)Activator.CreateInstance(typeof(T), args);
                form.AddToMdiContainer();
            }
            return form;
        }

        /// <summary>
        /// 将窗体添到MDIParent 里
        /// </summary>
        /// <param name="form"></param>
        public static void AddToMdiContainer(this Form form) {
            var mdiContainer = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.IsMdiContainer);
            if(mdiContainer != null) {
                mdiContainer.InvokeIfNeed(() => {
                    form.MdiParent = mdiContainer;
                });
            }
        }

        public static Form GetMDI() {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.IsMdiContainer);
        }

        public static void DrawRowNumber(this DataGridView gd, DataGridViewRowPostPaintEventArgs e) {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                gd.RowHeadersWidth - 4,
                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                gd.RowHeadersDefaultCellStyle.Font, rectangle,
                gd.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        public static void MoveWindow(this Form form) {
            Win32.ReleaseCapture();
            Win32.SendMessage(form.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HTCAPTION, 0);
        }
    }
}
