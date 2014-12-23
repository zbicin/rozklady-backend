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

    }
}