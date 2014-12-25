using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class Explanation
    {
        public int Id { get; set; }
        public String Abbreviation { get; set; }
        public String Definition { get; set; }
        [JsonIgnore]
        public virtual List<Departure> Departures { get; set; }
    }
}
