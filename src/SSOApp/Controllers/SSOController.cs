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
        public ActionResult Login(string encryptionToken, string returnUrl)
        {
            //check if we are already logged in and if not then login
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                var model = new LoginModel() { AuthToken = encryptionToken, ReturnUrl = returnUrl };
                return View(model);
            }
            else
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);

                var user = ticket.Name;

                return Redirect(returnUrl + "?token=" + user);
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe)) //simple login
            {
                return Redirect(model.ReturnUrl + "?token=" + model.UserName);
            }

            return View(model);
        }

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
