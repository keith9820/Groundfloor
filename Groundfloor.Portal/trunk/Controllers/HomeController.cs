using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Groundfloor.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logs()
        {
            return View();
        }
        public ActionResult Configuration()
        {
            return View();
        }
        public ActionResult Insight()
        {
            return View();
        }
        public ActionResult Insignia()
        {
            return View();
        }
    }
}
