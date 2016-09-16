using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Web.Controllers
{
    public class CollectorController : Controller
    {
        ICollectorService svc = new CollectorService();
        //
        // GET: /Collector/

        public ActionResult Index()
        {
            return View();
        }

        public AjaxFxRspJson GetCollectors(CollectorQueryArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Collecter> list = svc.GetCollectors(args);

            ajax.RspData.Add("collectors", JToken.FromObject(list));

            return ajax;
        }

        [HttpPost]
        public AjaxFxRspJson AddCollecter(Collecter collector)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.AddCollecter(collector);

            if (!rtn.ReturnState)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = rtn.ErrorMsg;
                return ajax;
            }

            return ajax;
        }

        [HttpPost]
        public AjaxFxRspJson ModifyCollecter(Collecter collector)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.ModifyCollector(collector);

            if (!rtn.ReturnState)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = rtn.ErrorMsg;
                return ajax;
            }

            return ajax;
        }

    }
}
