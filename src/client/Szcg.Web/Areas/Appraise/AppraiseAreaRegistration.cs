using System.Web.Mvc;

namespace Szcg.Web.Areas.Appraise
{
    public class AppraiseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Appraise";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
            name: "Appraise_main",
            url: "appraise/main.html",
            defaults: new { controller = "Home", action = "Main", @namespace = "Szcg.Web.Areas.Appraise.Controllers" }
        );

            context.MapRoute(
                "Appraise_default",
                "Appraise/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                   new[] { "Szcg.Web.Areas.Appraise.Controllers" }
            );
        }
    }
}
