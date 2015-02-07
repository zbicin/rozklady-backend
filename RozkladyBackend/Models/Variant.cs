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
        public virtual Line Line { get; set; }
        [JsonProperty(PropertyName = "firstLineStop")]
        public Stop FirstLineStop { get; set; }
        [JsonProperty(PropertyName = "lastLineStop")]
        public Stop LastLineStop { get; set; }
        [JsonProperty(PropertyName = "variantStops")]
        public List<VariantStop> VariantStops { get; set; }
        [JsonProperty(PropertyName = "departures")]
        public List<Departure> Departures { get; set; }

        public Variant()
        {
            this.Symbol = "";
        }
    }
}