using RozkladyBackend.Lib;
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
                return new JsonCamelCaseResult(db.VariantStops.Include("Stop").Where(vs => vs.Variant.Id == variantId).OrderBy(vs => vs.TimeOffset).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}