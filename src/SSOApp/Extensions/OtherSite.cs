using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSOApp.Extensions
{
    public static class HtmlExtensions
    {
        public static string OtherSiteUrl(this HtmlHelper html)
        {
            var otherSite = ConfigurationManager.AppSettings["OtherSite"];

            return otherSite;
        }
    }
}