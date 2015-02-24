using System;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace AsNum.Common.PropertyEditor {
    public class PropertyGridHtmlEditor : UITypeEditor {


        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value) {
            try {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if(edSvc != null) {
                    if(value is string) {
                        var form = new HtmlEditor((string)value);
                        edSvc.ShowDialog(form);
                        return form.Ctx;
                    }
                }
            } catch(Exception ex) {
                Console.Write(ex.Message);
                return value;
            }
            return value;
        }

    }
}
