using System.Web.Mvc;

namespace Szcg.Web.Areas.DataExchange
{
    public class DataExchangeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DataExchange";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DataExchange_default",
                "DataExchange/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
