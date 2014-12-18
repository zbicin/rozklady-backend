using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }
}
