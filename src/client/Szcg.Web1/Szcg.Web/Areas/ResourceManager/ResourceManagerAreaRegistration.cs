using System.Web.Mvc;

namespace Szcg.Web.Areas.ResourceManager
{
    public class ResourceManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ResourceManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ResourceManager_default",
                "ResourceManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
