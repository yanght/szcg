using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using szcg.com.teamax.business.entity;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;
using Szcg.Web.Controllers;

namespace Szcg.Web.Areas.Manager.Controllers
{
    public class OrganizeController : BaseController
    {
        //
        // GET: /Manager/Organize/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

    }
}
