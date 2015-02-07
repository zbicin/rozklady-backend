using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class VariantStop
    {
        public int Id { get; set; }
        public Stop Stop { get; set; }
        public int StopId { get; set; }
        public Variant Variant { get; set; }
        public int VariantId { get; set; }
        public int TimeOffset { get; set; }
    }
}
