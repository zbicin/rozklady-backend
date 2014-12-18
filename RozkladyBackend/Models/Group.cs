using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models
{
    public class Group
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<Line> Lines { get; set; }
        
    }
}