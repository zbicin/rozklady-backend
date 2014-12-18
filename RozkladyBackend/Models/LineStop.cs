using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class LineStop
    {
        public int Id { get; set; }
        public virtual Stop Stop { get; set; }
        public virtual Line Line { get; set; }
        public int TimeOffset { get; set; }
    }
}
