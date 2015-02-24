using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using VAB = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace AsNum.Common.WPF {
    public class RowDataInfoValidationRule : ValidationRule {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            BindingGroup group = (BindingGroup)value;
            List<string> errors = new List<string>();
            foreach (var item in group.Items) {
                IDataErrorInfo info = item as IDataErrorInfo;
                if (info != null) {
                    if (!string.IsNullOrEmpty(info.Error)) {
                        errors.Add(info.Error);
                    }
                } else {
                    var results = VAB.Validation.Validate(item);//, VAB.ValidationSpecificationSource.All);
                    if (!results.IsValid) {
                        errors.AddRange(results.Select(r => r.Message));
                    }
                }
            }

            if (errors.Count > 0)
                return new ValidationResult(false, string.Join("; ", errors));

            return ValidationResult.ValidResult;
        }
    }
}
