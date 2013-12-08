using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSOApp.Extensions;

namespace SSOApp.Controllers
{
    //[SSOSignOn]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cookies()
        {
            return View();
        }

        public ActionResult HTML5()
        {
            return View();
        }

        public ActionResult CAS()
        {
            return View();
        }
    }
}
