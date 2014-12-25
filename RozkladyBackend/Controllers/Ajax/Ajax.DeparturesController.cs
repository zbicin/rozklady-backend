using Newtonsoft.Json;
using RozkladyBackend.Lib;
using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RozkladyBackend.Controllers
{
    public partial class AjaxController : Controller
    {

        public JsonCamelCaseResult GetDeparturesByVariantId(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Departures.Where(d => d.Variant.Id == variantId).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public void UpdateDepartures(Variant variant, List<Departure> departuresToRemove, List<Explanation> explanationsToRemove)
        {
            using (BackendContext db = new BackendContext())
            {
                var dbVariant = db.Variants.Single(v => v.Id == variant.Id);

                if (variant.Departures != null)
                {
                    dbVariant.Departures.ToList();
                    dbVariant.Departures.Clear();

                    foreach (var singleDeparture in variant.Departures)
                    {
                        List<Explanation> explanationsReplacement = new List<Explanation>();
                        foreach (var singleExplanation in singleDeparture.Explanations)
                        {
                            var dbExplanation = db.Explanations.SingleOrDefault(e => e.Abbreviation == singleExplanation.Abbreviation);
                            if (dbExplanation != null)
                            {
                                explanationsReplacement.Add(dbExplanation);
                            }
                            else
                            {
                                explanationsReplacement.Add(singleExplanation);
                            }
                        }

                        singleDeparture.Explanations = explanationsReplacement;
                        dbVariant.Departures.Add(singleDeparture);                        
                        db.SaveChanges();
                    }
                }

                if (explanationsToRemove != null)
                {
                    foreach (var singleExplanation in explanationsToRemove)
                    {
                        if(singleExplanation.Id > 0) {
                            db.Explanations.Remove(db.Explanations.Single(e => e.Id == singleExplanation.Id));
                        }
                    }
                }

                if(departuresToRemove != null) {
                    foreach (var singleDeparture in departuresToRemove)
                    {
                        if (singleDeparture.Id > 0)
                        {
                            db.Departures.Remove(db.Departures.Single(d => d.Id == singleDeparture.Id));    
                        }
                    }
                }

                db.SaveChanges();
            }
        }

        public void UpdateDepartures2(Variant variant, List<Departure> departuresToRemove, List<Explanation> explanationsToRemove)
        {
            using (BackendContext db = new BackendContext())
            {
                
                var variantEntry = db.Variants.Single(v => v.Id == variant.Id);
                if (variant.Departures != null)
                {
                    foreach (var singleDeparture in variant.Departures)
                    {
                        foreach (var singleExplanation in singleDeparture.Explanations)
                        {
                            if (singleExplanation.Id < 1)
                            {
                                db.Entry(singleExplanation).State = System.Data.Entity.EntityState.Added;
                            }
                        }

                        if (singleDeparture.Id < 1 && false)
                        {
                            singleDeparture.Variant = variantEntry;
                            db.Entry(singleDeparture).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                }
                
                if (explanationsToRemove != null)
                {
                    foreach (var singleExplanation in explanationsToRemove)
                    {
                        if (singleExplanation.Id > 0)
                        {
                            db.Entry(singleExplanation).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                }

                if (departuresToRemove != null)
                {
                    foreach (var singleDeparture in departuresToRemove)
                    {
                        if (singleDeparture.Id > 0)
                        {
                            db.Entry(singleDeparture).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                }

                db.SaveChanges();
            }
        }

        /*public void UpdateDepartures(int variantId, List<Departure> variantDepartures, List<Departure> departuresToRemove)
        {
            using (BackendContext db = new BackendContext())
            {
                if (variantDepartures != null)
                {
                    Variant editedVariant = db.Variants.Single(v => v.Id == variantId);
                    foreach (var singleDeparture in variantDepartures)
                    {
                        if (singleDeparture.Id < 1)
                        {
                            singleDeparture.Variant = editedVariant;
                            db.Departures.Add(singleDeparture);
                        }
                    }
                }
                if (departuresToRemove != null)
                {
                    foreach (var singleDeparture in departuresToRemove)
                    {
                        if(singleDeparture.Id > 0) {
                            db.Departures.Remove(db.Departures.Single(d => d.Id == singleDeparture.Id));
                        }
                    }
                }
                db.SaveChanges();
            }
        }*/
    }
}