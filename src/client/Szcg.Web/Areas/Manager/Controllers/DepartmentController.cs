using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.Manager.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Manager/Department/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditDepart()
        {
            return View();
        }

    }
}
