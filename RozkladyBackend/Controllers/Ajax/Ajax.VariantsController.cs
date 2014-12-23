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
                return new JsonCamelCaseResult(db.Variants.Single(v => v.Id == variantId), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonCamelCaseResult GetVariantWithDepartures(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Variants.Include("Departures").Single(v => v.Id == variantId), JsonRequestBehavior.AllowGet);
            }
        }
    }
}