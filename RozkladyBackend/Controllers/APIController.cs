using RozkladyBackend.Lib;
using RozkladyBackend.Lib.API;
using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public class APIController : Controller
    {
        public JsonCamelCaseResult Timetable(int stopId, Boolean prettyPrint)
        {
            using (BackendContext db = new BackendContext())
            {
                // TODO: check if necessary
                db.Configuration.LazyLoadingEnabled = false;

                APITimetable result = APITimetableBuilder.Build(db, stopId);
                
                return new JsonCamelCaseResult(result) {
                    PrettyPrint = prettyPrint
                };
            }
        }
    }
}