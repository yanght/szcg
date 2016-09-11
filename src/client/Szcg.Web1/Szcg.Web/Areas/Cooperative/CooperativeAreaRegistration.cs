using System.Web.Mvc;

namespace Szcg.Web.Areas.Cooperative
{
    public class CooperativeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cooperative";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Cooperative_default",
                "Cooperative/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
