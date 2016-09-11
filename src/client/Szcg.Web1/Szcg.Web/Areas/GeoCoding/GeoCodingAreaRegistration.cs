using System.Web.Mvc;

namespace Szcg.Web.Areas.GeoCoding
{
    public class GeoCodingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GeoCoding";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "GeoCoding_default",
                "GeoCoding/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
