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
        public JsonCamelCaseResult ListExplanations()
        {
            using (BackendContext db = new BackendContext())
            {
                List<Explanation> explanations = db.Explanations.OrderBy(e => e.Abbreviation).ToList();
                return new JsonCamelCaseResult(explanations);
            }
        }
    }
}