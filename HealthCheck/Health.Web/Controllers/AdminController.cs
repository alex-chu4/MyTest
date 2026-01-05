using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("SystemInfo");
        }

        public ActionResult SystemInfo()
        {
            return View();
        }

        public ActionResult HealthTarget()
        {
            return View();
        }

        public ActionResult NotificationInfo()
        {
            return View();
        }

        public ActionResult VMInfo()
        {
            return View();
        }
    }
}