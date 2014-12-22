using Newtonsoft.Json;
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
        public JsonCamelCaseResult GetStops()
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Stops.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}