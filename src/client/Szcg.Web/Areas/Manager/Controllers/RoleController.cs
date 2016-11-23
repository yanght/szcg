using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.Manager.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Manager/Role/

        public ActionResult Index()
        {
            return View();
        }

        //权限
        public ActionResult PermissionManager()
        {
            return View();
        }

        //角色
        public ActionResult RoleManager()
        {
            return View();
        }

        //授权
        public ActionResult PurviewManager()
        {
            return View();
        }

    }
}
