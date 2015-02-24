using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.API.Entity {
    public class SuccinctProduct {

        [JsonProperty("productId")]
        public long ProductID {
            get;
            set;
        }

        [JsonProperty("subject")]
        public string ProductName {
            get;
            set;
        }

        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime CreateOn {
            get;
            set;
        }

        [JsonProperty("gmtModified"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime? ModifiedOn {
            get;
            set;
        }

        [JsonProperty("imageURLs")]
        internal string Imgs {
            get;
            set;
        }

        public List<string> ImageUrls {
            get {
                return this.Imgs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        [JsonProperty("ownerMemberID")]
        public string OwnerID {
            get;
            set;
        }

        /// <summary>
        /// 所属账户
        /// </summary>
        public string Account {
            get;
            set;
        }

        [JsonProperty("productMinPrice")]
        public decimal MinPrice {
            get;
            set;
        }

        [JsonProperty("productMaxPrice")]
        public decimal MaxPrice {
            get;
            set;
        }

        [JsonProperty("freightTemplateId")]
        public int FreightTemplateID {
            get;
            set;
        }
    }
}
