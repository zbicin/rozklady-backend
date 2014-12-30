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

        public JsonCamelCaseResult GetVariant(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Variants.Single(v => v.Id == variantId));
            }
        }

        public JsonCamelCaseResult GetVariantWithDeparturesAndExplanations(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var result = db.Variants.Include("Departures").Include("Departures.Explanations").Single(v => v.Id == variantId);
                return new JsonCamelCaseResult(result);
            }
        }

        public void RemoveVariant(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.VariantStops.RemoveRange(db.VariantStops.Where(vs => vs.Variant.Id == variantId).ToList());
                db.Variants.Remove(db.Variants.Single(v=>v.Id == variantId));
                db.SaveChanges();
            }
        }
    }
}