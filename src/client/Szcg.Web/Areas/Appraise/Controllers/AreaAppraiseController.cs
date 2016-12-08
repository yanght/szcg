using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Web.Controllers;

namespace Szcg.Web.Areas.Appraise.Controllers
{
    public class AreaAppraiseController : BaseController
    {
        //
        // GET: /Appraise/AreaAppraise/

        public ActionResult Index()
        {
            ViewBag.AreaCode = UserInfo.getAreacode();
            return View();
        }

    }
}
