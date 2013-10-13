using System.Web.Security;
using SSOApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SSOApp.Extensions
{
    public class SSOSignOn : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["sso"];

            if (cookie != null)
            {
                var token = cookie.Value;
                var username = SSOAuthenticationService.DecryptToken(token);

                if (WebSecurity.UserExists(username))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                }
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}