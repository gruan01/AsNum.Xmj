using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CtripSZ.Frameworks.Mvc.Validation {
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequiredIfAttribute : ValidationAttribute , IClientValidatable {

        private RequiredAttribute required = new RequiredAttribute();

        /// <summary>
        /// 依赖于哪个属性
        /// <remarks>必须是本对象下的一个属性</remarks>
        /// </summary>
        public string DependentProperty { get; set; }

        private List<object> targetValues = new List<object>();
        /// <summary>
        /// 依赖属性为哪些值时，该属性为必填
        /// </summary>
        public List<object> TargetValues {
            get {
                return this.targetValues;
            }
            set {
                this.targetValues = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependentProperty"></param>
        /// <param name="targetValue"></param>
        public RequiredIfAttribute(string dependentProperty , object targetValue)
            : this(dependentProperty , targetValue , null) {
        }

        public RequiredIfAttribute(string dependentProperty , params object[] targetValues)
            : this(dependentProperty , targetValues , null) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependentProperty"></param>
        /// <param name="targetValue"></param>
        /// <param name="errorMessage"></param>
        public RequiredIfAttribute(string dependentProperty , object targetValue , string errorMessage)
            : base(errorMessage) {
            this.DependentProperty = dependentProperty;
            this.TargetValues.Add(targetValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependentProperty"></param>
        /// <param name="targetValues"></param>
        /// <param name="errorMessage"></param>
        public RequiredIfAttribute(string dependentProperty , object[] targetValues , string errorMessage)
            : base(errorMessage) {
            this.DependentProperty = dependentProperty;
            this.TargetValues.AddRange(targetValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata , ControllerContext context) {
            var rule = new ModelClientValidationRule() {
                ErrorMessage = this.FormatErrorMessage(metadata.GetDisplayName()) ,
                //只能是小写
                ValidationType = "requiredif" //要在 jquery.validate 里实现 requiredIf 规则
            };

            var tvs = this.TargetValues.Select(v => {
                if(v.GetType() == typeof(bool))
                    return v.ToString().ToLower();
                else
                    return v.ToString();
            });

            var ser = new JavaScriptSerializer();
            var values = ser.Serialize(tvs);
            //只能是小写
            rule.ValidationParameters.Add("dependencyvalue" , values);
            //不要试图获取该对象输出成 html 的表单前缘，我试了很多方法，都不能获取,特别是当 Model 是一个集合的时候
            rule.ValidationParameters.Add("dependency" , string.Format("*.{0}" , this.DependentProperty));

            yield return rule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name) {
            if(!String.IsNullOrEmpty(this.ErrorMessageString))
                required.ErrorMessage = this.ErrorMessageString;
            return required.FormatErrorMessage(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value , ValidationContext validationContext) {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if(field == null)
                throw new MissingMemberException(containerType.Name , this.DependentProperty);

            var dependentvalue = field.GetValue(validationContext.ObjectInstance , null);
            if((dependentvalue == null && (this.TargetValues == null || this.TargetValues.Count == 0)) || (dependentvalue != null && this.TargetValues.Any(t => t.Equals(dependentvalue)))) {
                if(!required.IsValid(value))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName) , new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
