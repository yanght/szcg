using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.CallAcceptance.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /CallAcceptance/Message/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusinessMessage()
        {
            return View();
        }

        public ActionResult OtherMessage()
        {
            return View();
        }

        public ActionResult WarningMessage()
        {
            return View();
        }

        public ActionResult MessageDetail(string id,string type)
        {
            return View();
        }

    }
}
