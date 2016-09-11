using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.IBussiness;
using Szcg.Service.Bussiness;
using szcg.com.teamax.business.entity;

namespace Szcg.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            string[] ss = UserInfo.getSystemid();
            ChageRole("11");
            return View();
        }

        public void ChageRole(string systemId)
        {
            string strRoleId = string.Empty;
            string strAreacode = string.Empty;

            string[] strRoles = UserInfo.getRole();

            ((UserInfo)Session["UserInfo"]).CurrentSystemId = systemId;

            new PermissionService().GetRoleStepBySysCode(systemId, out strRoleId, out strAreacode, string.Join(",", strRoles));//多角色

            UserInfo.CurrentRole = int.Parse(strRoleId);

            UserInfo.ModelPowers = new PermissionService().GetUserModelPower(systemId, UserInfo.getUsercode());

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
