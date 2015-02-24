using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class FreightTemplateListResult {

        [JsonProperty("aeopFreightTemplateDTOList")]
        public List<FreightTemplate> List { get; set; }

    }
}
