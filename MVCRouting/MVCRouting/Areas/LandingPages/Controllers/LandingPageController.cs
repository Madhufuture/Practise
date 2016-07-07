using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRouting.Areas.LandingPages.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPages/LandingPage
        public ActionResult Index()
        {
            return View();
        }
    }
}