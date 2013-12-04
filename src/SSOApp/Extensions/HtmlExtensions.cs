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
            return ConfigurationManager.AppSettings["OtherSite"];
        }

        public static string AuthProviderSite(this HtmlHelper html)
        {
            return ConfigurationManager.AppSettings["AuthProviderSite"];
        }

        public static bool IsAuthProvider(this HtmlHelper html)
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["IsAuthProvider"] ?? "false");
        }
    }
}