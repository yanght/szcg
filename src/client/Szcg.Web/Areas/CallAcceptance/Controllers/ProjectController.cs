﻿using System;
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

        //待办案卷列表
        public ActionResult Reportlist()
        {
            return View();
        }

        //自办件
        public ActionResult ProjectSelfList()
        {
            return View();
        }

        //存档案卷
        public ActionResult ProjectCDList()
        {
            return View();
        }

        public ActionResult ProjectQuery()
        {
            return View();
        }

        public ActionResult Preview(string projectcode, string year, string isend, string nodeid)
        {
            return View();
        }
        public ActionResult ProjectTrace(string projectcode, string year, string isend)
        {
            return View();
        }
        public ActionResult ProjectReport(string projectcode, string year, string isend, string nodeid)
        {
            return View();
        }

        public ActionResult ProjectLA()
        {
            return View();
        }
        public ActionResult ProjectDispatch()
        {
            return View();
        }
        public ActionResult ProjectDispatchRevert()
        {
            return View();
        }

        public ActionResult ProjectCheckMessage()
        {
            return View();
        }

        public ActionResult ProjectCheck()
        {
            return View();
        }

        public ActionResult ProjectEnd()
        {
            return View();
        }

        public ActionResult SelectDepart()
        {
            return View();
        }

        public ActionResult SelectProjectClass()
        {
            return View();
        }

        public ActionResult SelectArea()
        {
            return View();
        }
        public ActionResult ProjectApprovedView(string projectcode, string year, string isend, string nodeid, string action, string buttoncode)
        {
            return View();
        }
    }
}
