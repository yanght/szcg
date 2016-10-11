using System.Web.Mvc;

namespace Szcg.Web.Areas.CallAcceptance
{
    public class CallAcceptanceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CallAcceptance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CallAcceptance_default",
                "CallAcceptance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Szcg.Web.Areas.CallAcceptance.Controllers" }
            );
        }
    }
}
