using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public partial class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }
        
    }
}