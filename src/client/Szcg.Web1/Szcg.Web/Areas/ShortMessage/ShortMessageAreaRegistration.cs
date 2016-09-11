using System.Web.Mvc;

namespace Szcg.Web.Areas.ShortMessage
{
    public class ShortMessageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ShortMessage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ShortMessage_default",
                "ShortMessage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
