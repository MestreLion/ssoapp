using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSOApp.Infrastructure
{
    public class P3PHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader("P3P", "CP=\\\"OUR ONL\\\"");

            base.OnActionExecuted(filterContext);
        }
    }
}