﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Web.Controllers;

namespace Szcg.Web.Areas.Manager.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Manager/Home/

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
