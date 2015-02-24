using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace AsNum.WPF.Controls {
    /// <summary>
    /// Exposes <see cref="Resizer"/> types to UI automation.
    /// </summary>
    public class ResizerAutomationPeer : FrameworkElementAutomationPeer, ITransformProvider {
        
        bool ITransformProvider.CanMove {
            get {
                return false;
            }
        }

        bool ITransformProvider.CanRotate {
            get {
                return false;
            }
        }

        bool ITransformProvider.CanResize {
            get {
                return true;
            }
        }

        public ResizerAutomationPeer(Resizer owner)
            : base(owner) {
        }

        protected override string GetClassNameCore() {
            return "Resizer";
        }

        protected override AutomationControlType GetAutomationControlTypeCore() {
            return AutomationControlType.Custom;
        }

        public override object GetPattern(PatternInterface patternInterface) {
            if (patternInterface == PatternInterface.Transform) {
                return this;
            }

            return base.GetPattern(patternInterface);
        }

        void ITransformProvider.Move(double x, double y) {
            throw new InvalidOperationException();
        }

        void ITransformProvider.Rotate(double degrees) {
            throw new InvalidOperationException();
        }

        void ITransformProvider.Resize(double width, double height) {
            Resizer resizer = Owner as Resizer;

            if (!resizer.IsEnabled || !resizer.IsGripEnabled) {
                throw new ElementNotEnabledException();
            }

            resizer.Width = width;
            resizer.Height = height;
        }
    }
}