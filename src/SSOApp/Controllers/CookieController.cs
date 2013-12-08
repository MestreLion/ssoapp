using SSOApp.Filters;
using SSOApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SSOApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class CookieController : Controller
    {
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

        public void Logoff(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var user = SSOAuthenticationService.DecryptToken(token);

                if (WebSecurity.UserExists(user))
                {
                    WebSecurity.Logout();
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
    }
}
