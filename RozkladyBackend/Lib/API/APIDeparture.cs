using RozkladyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public class APIDeparture
    {
        public APIDeparture(int hour, Departure departure)
        {
            // TODO: Last/First stops
            this.Hour = hour;
            this.Minute = departure.Minute;
            this.VariantDescription = departure.Variant.Description;
            this.LineName = departure.Variant.Line.Name;
            this.LineId = departure.Variant.Line.Id;
            this.Explanations = departure.Explanations;
            
            this.IsValidOnMonday = departure.IsValidOnMonday;
            this.IsValidOnTueday = departure.IsValidOnTueday;
            this.IsValidOnWednesday = departure.IsValidOnWednesday;
            this.IsValidOnThursday = departure.IsValidOnThursday;
            this.IsValidOnFriday = departure.IsValidOnFriday;
            this.IsValidOnSaturday = departure.IsValidOnSaturday;
            this.IsValidOnSunday = departure.IsValidOnSunday;
        }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public String VariantDescription { get; set; }
        public String LineName { get; set;}
        public int LineId { get; set; }
        public Boolean IsValidOnMonday { get; set; }
        public Boolean IsValidOnTueday { get; set; }
        public Boolean IsValidOnWednesday { get; set; }
        public Boolean IsValidOnThursday { get; set; }
        public Boolean IsValidOnFriday { get; set; }
        public Boolean IsValidOnSaturday { get; set; }
        public Boolean IsValidOnSunday { get; set; }
        
        public List<Explanation> Explanations { get; set; }
    }
}