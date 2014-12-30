using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Lib.API
{
    public static class APITimetableBuilder
    {
        public static APITimetable Build(BackendContext db, int stopId)
        {
            Stop stop = db.Stops.Single(s => s.Id == stopId);
            List<Variant> variantsPresent = db.VariantStops.Include("Variant").Where(vs => vs.Stop.Id == stopId).Select(vs => vs.Variant).ToList();
            List<int> variantsIds = variantsPresent.Select(x => x.Id).ToList();
            List<Departure> allDeparturesThroughThatStop = db.Departures.Include("Variant").Include("Explanations").Where(d => variantsIds.Contains(d.Variant.Id)).ToList();
            List<Departure> timeAlteredDepartures = new List<Departure>();

            /* after this double-foreach loop we have all departures with updated minutes and hours */
            foreach (var singleVariant in variantsPresent)
            {
                int delay = db.VariantStops.Single(vs => vs.Stop.Id == stopId && vs.Variant.Id == singleVariant.Id).TimeOffset;
                List<Departure> variantDepartures = allDeparturesThroughThatStop.Where(d => d.Variant.Id == singleVariant.Id).ToList();

                singleVariant.Departures = null;

                foreach (var singleDeparture in variantDepartures)
                {
                    singleDeparture.AddTimeOffset(delay);
                    timeAlteredDepartures.Add(singleDeparture);
                }
            }

            // process all 24 hours
            Dictionary<int, List<Departure>> hours = new Dictionary<int, List<Departure>>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i, timeAlteredDepartures.Where(d => d.Hour == i).OrderBy(d => d.Minute).ToList());
            }

            return new APITimetable()
            {
                Hours = hours,
                Stop = stop,
                Variants = variantsPresent
            };
        }
    }
}