using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Line
    {
        public int Id { get; set; }
        public String Symbol { get; set; }
        public virtual Group Group { get; set; }
        public virtual Stop FirstLineStop { get; set; }
        public virtual Stop LastLineStop { get; set; }
        public virtual List<LineStop> LineStops { get; set; }
        public virtual List<Departure> Departures { get; set; }
    }
}