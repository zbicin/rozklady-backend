using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class Stop
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName="name")]
        public String Name { get; set; }
        [JsonProperty(PropertyName="description")]
        public String Description { get; set; }
        [JsonProperty(PropertyName="latitude")]
        public Double Latitude { get; set; }
        [JsonProperty(PropertyName="longitude")]
        public Double Longitude { get; set; }
    }
}
