using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using SSOApp.Extensions;
using SSOApp.Service;
using WebMatrix.WebData;
using SSOApp.Filters;
using SSOApp.Models;

namespace SSOApp.Controllers
{
    [InitializeSimpleMembership]
    public class SSOController : Controller
    {
        [HttpGet]
        public ActionResult SaveAuth()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAuth()
        {
            return View();
        }
    }
}
