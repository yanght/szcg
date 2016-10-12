using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.CallAcceptance.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /CallAcceptance/Project/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reportlist()
        {
            return View();
        }
        public ActionResult Preview(string projectcode, string year, string isend, string nodeid)
        {
            return View();
        }

    }
}
