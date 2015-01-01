using RozkladyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public class APILine
    {
        public APILine(Line line)
        {
            this.Id = line.Id;
            this.Name = line.Name;
        }
        public int Id { get; set; }
        public String Name { get; set; }
    }
}