using RozkladyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public class APIStop
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public String Name { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }

        public APIStop(Stop stop)
        {
            this.Id = stop.Id;
            this.Name = stop.Name;
            this.Latitude = stop.Latitude;
            this.Longitude = stop.Longitude;
            this.Description = stop.Description;
        }
    }
}