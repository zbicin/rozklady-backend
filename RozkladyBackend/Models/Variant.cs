using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Variant
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public String Symbol { get; set; }
        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }
        [JsonIgnore]
        public virtual Line Line { get; set; }
        [JsonProperty(PropertyName = "firstLineStop")]
        public virtual Stop FirstLineStop { get; set; }
        [JsonProperty(PropertyName = "lastLineStop")]
        public virtual Stop LastLineStop { get; set; }
        [JsonProperty(PropertyName = "variantStops")]
        public virtual List<VariantStop> VariantStops { get; set; }
        [JsonProperty(PropertyName = "departures")]
        public virtual List<Departure> Departures { get; set; }

        public Variant()
        {
            this.Symbol = "";
        }
    }
}