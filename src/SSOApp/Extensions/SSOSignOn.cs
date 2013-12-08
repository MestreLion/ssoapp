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
            //var cookie = filterContext.HttpContext.Request.Cookies["sso"];

            //if (cookie != null)
            //{
            //    var token = cookie.Value;
            //    var username = SSOAuthenticationService.DecryptToken(token);

            //    if (WebSecurity.UserExists(username) && !filterContext.HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        FormsAuthentication.SetAuthCookie(username, false);

            //        //expire cookie
            //        var c = new HttpCookie("sso") { Expires = DateTime.Now.AddDays(-1) };

            //        filterContext.HttpContext.Response.Cookies.Add(c);
            //    }

            //    //reload page after setting the auth cookie
            //    Uri referrer = filterContext.HttpContext.Request.UrlReferrer;
            //    if (referrer == null || string.IsNullOrEmpty(referrer.AbsoluteUri))
            //    {
            //        base.OnActionExecuting(filterContext);
            //    }

            //    filterContext.HttpContext.Response.Redirect(referrer.AbsoluteUri);
            //}

            //base.OnActionExecuting(filterContext);
        }
    }
}