using System.Web.Mvc;

namespace Szcg.Web.Areas.Manager
{
    public class ManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Manager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
              name: "Manager_main",
              url: "manager/main.html",
              defaults: new { controller = "Home", action = "Main", @namespace = "Szcg.Web.Areas.Manager.Controllers" }
          );

            context.MapRoute(
                "Manager_default",
                "Manager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Szcg.Web.Areas.Manager.Controllers" }
            );
        }
    }
}
