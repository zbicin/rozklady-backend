using RozkladyBackend.Lib;
using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public partial class AjaxController : Controller
    {
        public JsonCamelCaseResult GetVariantStopsForVariant(int variantId) 
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.VariantStops.Include("Stop").Where(vs => vs.Variant.Id == variantId).OrderBy(vs => vs.TimeOffset).ToList());
            }
        }
        public void UpdateVariantStops(int variantId, List<Stop> stops, List<VariantStop> variantStops, List<VariantStop> variantStopsToRemove)
        {
            using (BackendContext db = new BackendContext())
            {
                if (variantStopsToRemove != null)
                {
                    foreach (var singleVariantStop in variantStopsToRemove)
                    {
                        if (singleVariantStop.Id > 0)
                        {
                            db.Entry(singleVariantStop).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                    db.SaveChanges();
                }

                Variant variant = db.Variants.Include("VariantStops").Single(v => v.Id == variantId);
                if (variantStops != null)
                {
                    foreach (var singleVariantStop in variantStops)
                    {
                        // vs is new
                        if (singleVariantStop.Id < 1)
                        {
                            Stop stop;
                            // stop already exists
                            if (singleVariantStop.Stop.Id > 0)
                            {
                                stop = db.Stops.Single(s => s.Id == singleVariantStop.Stop.Id);
                            }
                            // stop is new
                            else
                            {
                                stop = db.Stops.Add(singleVariantStop.Stop);
                                db.SaveChanges();
                            }

                            singleVariantStop.Stop = stop;
                            singleVariantStop.Variant = variant;

                            db.Entry(singleVariantStop).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                    db.SaveChanges();

                    var orderedVariantStops = db.VariantStops.Include("Stop").Where(vs => vs.Variant.Id == variantId).OrderBy(vs => vs.TimeOffset).ToList();
                    if (orderedVariantStops.Count > 0)
                    {
                        int firstStopId = orderedVariantStops.First().Stop.Id;
                        int lastStopId = orderedVariantStops.Last().Stop.Id;
                        variant.FirstLineStop = db.Stops.Single(s => s.Id == firstStopId);
                        variant.LastLineStop = db.Stops.Single(s => s.Id == lastStopId);
                    }
                    else
                    {
                        variant.FirstLineStop = null;
                        variant.LastLineStop = null;
                    }
                    db.SaveChanges();
                }
            }
        }
    }

}