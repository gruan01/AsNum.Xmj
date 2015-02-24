using System;

namespace AsNum.Common.Annoations {
    public class OrderAttribute : Attribute {

        public int Order { get; set; }


        public OrderAttribute(int order) {
            this.Order = order;
        }
    }
}
