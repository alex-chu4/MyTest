using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Health.API.Models;

namespace Health.Web.Controllers
{
    public class RptController : Controller
    {
        public static int RefreshInterval
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["RefreshInterval"]))
                    return Convert.ToInt32(ConfigurationManager.AppSettings["RefreshInterval"]);
                else
                    return 60000;
            }
        }

        public ActionResult EMIC2_Current()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EMIC2_His(HealthHisModel healthCurrentView)
        {
            return View(healthCurrentView);
        }

        [HttpPost]
        public ActionResult EMIC2_His2(HealthHisModel healthCurrentView)
        {
            return View(healthCurrentView);
        }
    }
}