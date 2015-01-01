using RozkladyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public class APITimetable
    {
        public APIStop Stop { get; set; }
        public List<APILine> Lines { get; set; }
        public List<List<APIDeparture>> Hours { get; set; }

        public List<Explanation> Explanations { get; set; }

    }
}