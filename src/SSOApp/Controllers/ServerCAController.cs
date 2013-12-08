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
using System.Net;

namespace SSOApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ServerCAController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //check if we are already logged in and if not then login
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                var model = new LoginModel() { ReturnUrl = returnUrl };
                return View(model);
            }
            else
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);

                var user = SSOAuthenticationService.EncryptToken(ticket.Name);

                return Redirect(returnUrl + "?token=" + user + "&createPersistentCookie=true");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe)) //simple login
            {
                var username = SSOAuthenticationService.EncryptToken(model.UserName);

                return Redirect(model.ReturnUrl + "?token=" + username + "&createPersistentCookie=" + (model.RememberMe ? "true" : "false"));
            }

            return View(model);
        }

        [HttpGet]
        public HttpStatusCode Logout(string returnUrl)
        {
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }

            return HttpStatusCode.OK;
        }
    }
}
