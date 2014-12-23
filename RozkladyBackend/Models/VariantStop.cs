using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class VariantStop
    {
        public int Id { get; set; }
        public virtual Stop Stop { get; set; }
        public virtual Variant Variant { get; set; }
        public int TimeOffset { get; set; }
    }
}
