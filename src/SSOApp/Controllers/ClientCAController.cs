using SSOApp.Filters;
using SSOApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SSOApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ClientCAController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (WebSecurity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/Home/Index");
            }

            return Redirect(ConfigurationManager.AppSettings["AuthProviderSite"] + "/ServerCA/Login?returnUrl=" + Url.Action("LoginWithToken", "ClientCA", null, Request.Url.Scheme));
        }

        [AllowAnonymous]
        public ActionResult LoginWithToken(string token, bool createPersistentCookie)
        {
            var user = SSOAuthenticationService.DecryptToken(token);

            if (WebSecurity.UserExists(user))
            {
                int timeout = createPersistentCookie ? 43200 : 30;

                var cookie = SSOAuthenticationService.CreateFormsAuthenticationCookie(user, timeout, createPersistentCookie);

                HttpContext.Response.SetCookie(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
