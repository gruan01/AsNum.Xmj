using AsNum.Xmj.API.Attributes;
using System;

namespace AsNum.Xmj.API.Methods {
    public class PhotoBankGroupList : MethodBase<Object> {
        
        protected override string APIName {
            get { return "api.listGroup"; }
        }

        [Param("groupId")]
        public string GroupID { get; set; }
    }
}
