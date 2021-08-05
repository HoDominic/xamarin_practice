using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juli06.Models
{
    public class TrelloList
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }


        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }


        [JsonProperty(PropertyName = "closed")]
        public bool IsClosed { get; set; }

    }
}
