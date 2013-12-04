using SSOApp.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace SSOApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new P3PHeaderAttribute());
        }
    }
}