using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RozkladyBackend.Models
{
    public class Departure
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public List<Explanation> Explanations { get; set; }
        public Variant Variant { get; set; }
        public int VariantId { get; set; }
        public Boolean IsValidOnMonday { get; set; }
        public Boolean IsValidOnTueday { get; set; }
        public Boolean IsValidOnWednesday { get; set; }
        public Boolean IsValidOnThursday { get; set; }
        public Boolean IsValidOnFriday { get; set; }
        public Boolean IsValidOnSaturday { get; set; }
        public Boolean IsValidOnSunday { get; set; }

        public Departure()
        {
            this.Explanations = new List<Explanation>();
        }

        public void AddTimeOffset(int delay)
        {
            this.Minute += delay;
            while (Minute > 59)
            {
                Minute -= 60;
                Hour++;
            }

            while (Hour > 23)
            {
                Hour -= 24;
                this.MoveForwardValidDaysByOne();
            }
        }

        private void MoveForwardValidDaysByOne()
        {
            bool tempIsValidOnSunday = IsValidOnSunday;
            this.IsValidOnSunday = this.IsValidOnSaturday;
            this.IsValidOnSaturday = this.IsValidOnFriday;
            this.IsValidOnFriday = this.IsValidOnThursday;
            this.IsValidOnThursday = this.IsValidOnWednesday;
            this.IsValidOnWednesday = this.IsValidOnTueday;
            this.IsValidOnTueday = this.IsValidOnMonday;
            this.IsValidOnMonday = tempIsValidOnSunday;
        }
    }
}
