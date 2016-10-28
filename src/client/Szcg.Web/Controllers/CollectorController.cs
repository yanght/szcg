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
        ICollecterservice svc = new Collecterservice();
        //
        // GET: /Collector/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 获取监督员列表 ]

        public AjaxFxRspJson GetCollecters(CollectorQueryArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Collecter> list = svc.GetCollecters(args);

            ajax.RspData.Add("collecters", JToken.FromObject(list));

            return ajax;
        }

        public AjaxFxRspJson GetCheckCollecters(string streetcode, string projcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Collecter> list = svc.GetCollecters(streetcode, projcode);

            ajax.RspData.Add("collecters", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 添加监督员 ]

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

        #endregion

        #region [ 修改监督员 ]

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

        #endregion

    }
}
