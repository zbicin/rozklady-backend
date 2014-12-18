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
            var group = new Group()
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

            var line = new Line()
            {
                Group = group,
                Symbol = "A",
                FirstLineStop = stop1,
                LastLineStop = stop2
            };

            var lineStop1 = new LineStop()
            {
                Line = line,
                Stop = stop1,
                TimeOffset = 0
            };

            var lineStop2 = new LineStop()
            {
                Line = line,
                Stop = stop2,
                TimeOffset = 30
            };
            
            var departure1 = new Departure() {
                Hour = 9,
                Minute = 5,
                Symbol = "",
                IsValidOnFriday = true,
                IsValidOnMonday = true,
                IsValidOnSaturday = true,
                IsValidOnSunday = true,
                IsValidOnThursday = true,
                IsValidOnTueday = true,
                IsValidOnWednesday = true
            };
            var departure2 = new Departure() {
                Hour = 9,
                Minute = 20,
                Symbol = "",
                IsValidOnFriday = true,
                IsValidOnMonday = true,
                IsValidOnSaturday = true,
                IsValidOnSunday = true,
                IsValidOnThursday = true,
                IsValidOnTueday = true,
                IsValidOnWednesday = true
            };

            context.Stops.Add(stop1);
            context.Stops.Add(stop2);

            context.Lines.Add(line);

            context.Departures.Add(departure1);
            context.Departures.Add(departure2);

            context.LineStops.Add(lineStop1);
            context.LineStops.Add(lineStop2);

            context.SaveChanges();
        }
    }
}