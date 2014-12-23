using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public String Symbol { get; set; }
        public virtual Line Line { get; set; }
        public virtual Stop FirstLineStop { get; set; }
        public virtual Stop LastLineStop { get; set; }
        public virtual List<VariantStop> LineStops { get; set; }
        public virtual List<Departure> Departures { get; set; }
    }
}