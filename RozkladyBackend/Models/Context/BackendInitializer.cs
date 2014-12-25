using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models.Context
{
    public class BackendInitializer : DropCreateDatabaseIfModelChanges<BackendContext>
    {
        protected override void Seed(BackendContext context)
        {
            var line = new Line()
            {
                Name = "TEST"
            };

            var stop1 = new Stop()
            {
                Description = "pole dodatkowe",
                Name = "Piłsudskiego/Śmigłego-Rydza",
                Latitude = 51.761679,
                Longitude = 19.486565
            };

            var stop2 = new Stop()
            {
                Description = "pole dodatkowe",
                Name = "Hetmańska/Rokicińska",
                Latitude = 51.752091,
                Longitude = 19.575073
            };

            var variant = new Variant()
            {
                Line = line,
                Symbol = "98",
                FirstLineStop = stop1,
                LastLineStop = stop2
            };

            var variantStop1 = new VariantStop()
            {
                Variant = variant,
                Stop = stop1,
                TimeOffset = 0
            };

            var variantStop2 = new VariantStop()
            {
                Variant = variant,
                Stop = stop2,
                TimeOffset = 30
            };

            var explanationA = new Explanation()
            {
                Abbreviation = "A",
                Definition = "A modifier"
            };
            
            var departure1 = new Departure() {
                Hour = 9,
                Minute = 5,
                IsValidOnFriday = true,
                IsValidOnMonday = true,
                IsValidOnSaturday = true,
                IsValidOnSunday = true,
                IsValidOnThursday = true,
                IsValidOnTueday = true,
                IsValidOnWednesday = true,
                Variant = variant
            };
            var departure2 = new Departure() {
                Hour = 9,
                Minute = 20,
                IsValidOnFriday = true,
                IsValidOnMonday = true,
                IsValidOnSaturday = true,
                IsValidOnSunday = true,
                IsValidOnThursday = true,
                IsValidOnTueday = true,
                IsValidOnWednesday = true,
                Variant = variant,
                Explanations = new List<Explanation>()
                {
                    explanationA
                }
            };

            context.Stops.Add(stop1);
            context.Stops.Add(stop2);

            context.Lines.Add(line);

            context.Explanations.Add(explanationA);

            context.Departures.Add(departure1);
            context.Departures.Add(departure2);

            context.VariantStops.Add(variantStop1);
            context.VariantStops.Add(variantStop2);

            context.SaveChanges();
        }
    }
}