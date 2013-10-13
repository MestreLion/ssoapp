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
    public class SSOSignOn : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["sso"];

            if (cookie != null)
            {
                var token = cookie.Value;
                var username = SSOAuthenticationService.DecryptToken(token);

                if (WebSecurity.UserExists(username))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                }

                filterContext.HttpContext.Request.Cookies.Remove("sso");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}