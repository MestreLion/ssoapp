using SSOApp.Filters;
using SSOApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SSOApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class XDMController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public void Login(string token)
        {
            const bool createPersistentCookie = false;

            if (!string.IsNullOrEmpty(token))
            {
                var user = SSOAuthenticationService.DecryptToken(token);

                if (WebSecurity.UserExists(user))
                {
                    SignInUser(createPersistentCookie, user);
                }
            }
        }

        private void SignInUser(bool createPersistentCookie, string user)
        {
            if (WebSecurity.UserExists(user))
            {
                int timeout = createPersistentCookie ? 43200 : 30;

                var cookie = SSOAuthenticationService.CreateFormsAuthenticationCookie(user, timeout, createPersistentCookie);

                HttpContext.Response.Cookies.Add(cookie);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SaveAuth()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAuth()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ClearAuth()
        {
            return View();
        }
    }
}
