using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Line
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName="name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "variants")]
        public virtual ICollection<Variant> Variants { get; set; }
        
    }
}