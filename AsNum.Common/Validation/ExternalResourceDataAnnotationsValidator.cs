using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CtripSZ.Frameworks.Mvc.Validation {
    /// <summary>
    /// 
    /// </summary>
    public class ExternalResourceDataAnnotationsValidator : DataAnnotationsModelValidator<ValidationAttribute> {
        /// <summary>
        /// 
        /// </summary>
        public static Type ResourceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Func<ValidationAttribute , string> ResourceNameFunc {
            get { return _resourceNameFunc; }
            set { _resourceNameFunc = value; }
        }
        private static Func<ValidationAttribute , string> _resourceNameFunc = attr => attr.GetType().Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <param name="attribute"></param>
        public ExternalResourceDataAnnotationsValidator(ModelMetadata metadata , ControllerContext context , ValidationAttribute attribute)
            : base(metadata , context , attribute) {

            //ErrorMessage 和 ErrorMessageResourceName 不可同时存在
            if(attribute.ErrorMessage == null) {
                this.Attribute.ErrorMessageResourceType = ResourceType;
                this.Attribute.ErrorMessageResourceName = ResourceNameFunc(this.Attribute);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules() {
            IClientValidatable att;
            var rules = base.Metadata.GetValidators(base.ControllerContext).Select(v => v.GetClientValidationRules());
            if(null != (att = (IClientValidatable)this.Attribute)) {
                return att.GetClientValidationRules(base.Metadata , base.ControllerContext);
            } else
                return base.GetClientValidationRules();
        }
    }
}
