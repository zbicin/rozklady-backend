using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class Departure
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public String Symbol { get; set; }
        public virtual Line Line { get; set; }
        public Boolean IsValidOnMonday { get; set; }
        public Boolean IsValidOnTueday { get; set; }
        public Boolean IsValidOnWednesday { get; set; }
        public Boolean IsValidOnThursday { get; set; }
        public Boolean IsValidOnFriday { get; set; }
        public Boolean IsValidOnSaturday { get; set; }
        public Boolean IsValidOnSunday { get; set; }
    }
}
