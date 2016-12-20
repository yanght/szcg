using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Web.Controllers;

namespace Szcg.Web.Areas.Appraise.Controllers
{
    public class CollecterAppraiseController : BaseController
    {
        //
        // GET: /Appraise/CollecterAppraise/

        public ActionResult Index()
        {
            ViewBag.AreaCode = this.UserInfo.getAreacode();
            return View();
        }

    }
}
