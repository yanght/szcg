using bacgDL.business;
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
    public class CollectorController : BaseController
    {
        ICollecterservice svc = new Collecterservice();
        //
        // GET: /Collector/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 获取指定区域所有监督员列表 ]

        public AjaxFxRspJson GetCollecters(CollectorQueryArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Collecter> list = svc.GetCollecters(args);

            ajax.RspData.Add("collecters", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获取监督员列表 ]

        public JsonResult GetCollecterList(string streetcode, string gridcode, string name, string loginname, string collmobile, string isguard)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            int currentpage = int.Parse(Request["start"]);

            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            PageInfo pageInfo = new PageInfo();

            pageInfo.PageSize = Request["length"];

            pageInfo.CurrentPage = currentpage.ToString();

            pageInfo.Field = "collcode";

            pageInfo.Order = "asc";

            List<Collecter> list = svc.GetCollectereList(streetcode, gridcode, name, loginname, collmobile, isguard, pageInfo);

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Collecter>() : list }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region [ 获取核查案卷监督员列表 ]

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
