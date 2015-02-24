using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AsNum.WPF.Controls {
    public class StyleDialogAdorner : Adorner {
        internal InnerStyleDialog Dialog;
        //internal StyleDialog PlaceHolder;

        public StyleDialogAdorner(StyleDialog ele)
            : base(ele) {

            this.Dialog = new InnerStyleDialog();

            this.AddVisualChild(this.Dialog);
            this.AddLogicalChild(this.Dialog);
        }

        protected override Visual GetVisualChild(int index) {
            return this.Dialog;
        }

        protected override int VisualChildrenCount {
            get {
                return 1;
            }
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform) {
            //变换Adorner 的起点
            var w = this.AdornedElement.GetParentWindow();
            var a = this.AdornedElement.TransformToAncestor(w);
            var b = a.Transform(new Point(0, 0));

            TransformGroup group = new TransformGroup();
            var t = new TranslateTransform(-b.X, -b.Y + ((SystemParameters.ResizeFrameHorizontalBorderHeight * 2) + SystemParameters.WindowCaptionHeight));
            group.Children.Add(t);

            group.Children.Add(transform as Transform);
            return group;
        }

        protected override Size MeasureOverride(Size constraint) {
            var w = this.AdornedElement.GetParentWindow();
            var height = w.ActualHeight - ((SystemParameters.ResizeFrameHorizontalBorderHeight * 2) + SystemParameters.WindowCaptionHeight);
            return new Size(w.ActualWidth, height);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            this.Dialog.Measure(finalSize);
            this.Dialog.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
