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
            List<Line> allLines = db.Lines.OrderBy(l => l.Name).ToList();
            List<Variant> variantsPresent = db.VariantStops.Include("Variant").Where(vs => vs.Stop.Id == stopId).Select(vs => vs.Variant).ToList();
            List<int> variantsIds = variantsPresent.Select(x => x.Id).ToList();
            List<Departure> allDeparturesThroughThatStop = db.Departures.Include("Variant").Include("Explanations").Where(d => variantsIds.Contains(d.Variant.Id)).ToList();
            List<Departure> timeAlteredDepartures = new List<Departure>();
            List<Explanation> explanations = db.Explanations.ToList();

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
            List<List<APIDeparture>> hours = new List<List<APIDeparture>>();
            for (int i = 0; i < 24; i++)
            {
                List<Departure> hourDepartures = timeAlteredDepartures.Where(d => d.Hour == i).OrderBy(d => d.Minute).ToList();
                List<APIDeparture> hourAPIDepartures = new List<APIDeparture>();
                foreach(var singleHourDeparture in hourDepartures) {
                    hourAPIDepartures.Add(new APIDeparture(i, singleHourDeparture));
                }
                hours.Add(hourAPIDepartures);
            }

            List<APILine> lines = new List<APILine>();
            foreach (var singleLine in allLines)
            {
                if (variantsPresent.Any(v => v.Line.Id == singleLine.Id))
                {
                    lines.Add(new APILine(singleLine));
                }
            }

            return new APITimetable()
            {
                Explanations = explanations,
                Hours = hours,
                Lines = lines,
                Stop = new APIStop(stop)
            };
        }
    }
}