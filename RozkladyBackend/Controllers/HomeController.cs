using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var t = new BackendContext().Departures.First();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}