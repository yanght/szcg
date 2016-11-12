using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.Manager.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Manager/User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertUser()
        {
            return View();
        }

        public ActionResult RoleTree()
        {
            return View();
        }

    }
}
