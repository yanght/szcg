using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Web.Controllers;

namespace Szcg.Web.Areas.Appraise.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Appraise/Home/

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult ProjectList(string labelname, string labeltype, string streetcode, string datafield, string datestart, string modeid, string dateend)
        {
            return View();
        }

    }
}
