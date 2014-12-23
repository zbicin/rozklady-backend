using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Line
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<Variant> Variants { get; set; }
        
    }
}