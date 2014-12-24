using Newtonsoft.Json;
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

        public JsonCamelCaseResult GetDeparturesByVariantId(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Departures.Where(d => d.Variant.Id == variantId).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public void UpdateDepartures(int variantId, List<Departure> variantDepartures, List<Departure> departuresToRemove)
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
        }
    }
}