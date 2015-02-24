using System.ComponentModel;

namespace AsNum.Common {
    public partial class ModelEditor : Component {
        public ModelEditor() {
            InitializeComponent();
        }

        public ModelEditor(IContainer container) {
            container.Add(this);

            InitializeComponent();
        }
    }
}
