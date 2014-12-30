using RozkladyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public class APITimetable
    {
        public Stop Stop { get; set; }
        public List<Variant> Variants { get; set; }
        public Dictionary<int, List<Departure>> Hours { get; set; }

    }
}