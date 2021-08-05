using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Juli06.Models
{
   public  class TrelloBoard
    {

        // name,id, starred

        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "starred")]

        public bool IsFavorite { get; set; }


        // extra property gaan toevoegen 1:08 vid2

        
        public String  ColorHex { get; set; }

        [JsonExtensionData]

        private Dictionary<String, JToken> _extrajsonData = new Dictionary<string, JToken>();


        [OnDeserialized]
        private void ProcessExtraJsonData(StreamingContext context)
        {
            JToken prefs = (JToken)_extrajsonData["prefs"];
            ColorHex =(String)prefs.SelectToken("backgroundColor");

        }


    }
}
