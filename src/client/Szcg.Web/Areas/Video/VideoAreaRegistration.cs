using System.Web.Mvc;

namespace Szcg.Web.Areas.Video
{
    public class VideoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Video";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Video_default",
                "Video/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
