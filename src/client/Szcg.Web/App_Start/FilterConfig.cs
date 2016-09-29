using System.Web;
using System.Web.Mvc;
using Szcg.Web.Filters;

namespace Szcg.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}