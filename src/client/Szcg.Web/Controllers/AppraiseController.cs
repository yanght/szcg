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
using Szcg.Service.Model.RequestModel;

namespace Szcg.Web.Controllers
{
    public class AppraiseApiController : BaseController
    {
        IAppraiseService svc = new AppraiseService();

        //
        // GET: /Appraise/
        public ActionResult Index()
        {
            return View();
        }

        #region [ 事部件评价 ]

        public AjaxFxRspJson GetEvePartAppraise()
        {
            EveParAppraiseRequestArgs args = new EveParAppraiseRequestArgs()
            {
                StreetCode = "",
                Number = 2016,
                Type = 3,
                Year = 2016
            };
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<EvePar_Appraise> list = svc.GetEvePartAppraise(args);

            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            return ajax;
        }

        #endregion

        #region [ 区域评价 ]

        public JsonResult GetAreaAppraise(string id)
        {
            AreaAppraiseRequestArgs args = new AreaAppraiseRequestArgs()
            {
                AreaCode = this.UserInfo.getAreacode(),
                ModelId = 6,
                Number = 2016,
                RoleId = "2",
                strReportMessage = "",
                Type = 3,
                Year = 2016
            };

            PageInfo pageInfo = new PageInfo() { };

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Area_Appraise> list = svc.GetAreaAppraise(args, pageInfo);

            //ajax.RspData.Add("list", JToken.FromObject(list));
            //ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            //ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));


            //ajax.RspData.Add("page", JToken.FromObject("1"));
            //ajax.RspData.Add("total", JToken.FromObject(list.Count));
            //ajax.RspData.Add("records", JToken.FromObject(list.Count));
            //ajax.RspData.Add("rows", JToken.FromObject(list));

            //  return ajax;
            if (!string.IsNullOrEmpty(id))
            {
                return Json(new { page = "1", total = list.Count, records = list.Count, rows = list.Where(m => m.PCode == id) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { page = "1", total = list.Count, records = list.Count, rows = list.Where(m => string.IsNullOrEmpty(m.PCode)) }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region [ 责任单位评价 ]

        public AjaxFxRspJson GetDepartAppraise()
        {
            List<Depart_Appraise> list = new List<Depart_Appraise>();

            DepartAppraiseRequestArgs args = new DepartAppraiseRequestArgs()
            {
                DepartCode = "",
                AreaCode = "",
                ModelId = 24,
                Number = 2016,
                RoleId = "2",
                strReportMessage = "",
                Type = 3,
                Year = 2016
            };

            PageInfo pageInfo = new PageInfo() { };

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetDepartAppraise(args, pageInfo);

            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            return ajax;
        }

        #endregion

        #region  [ 岗位评价 ]

        public AjaxFxRspJson GetDutyAppraise()
        {
            List<Duty_Appraise> list = new List<Duty_Appraise>();

            DutyAppraiseRequestArgs args = new DutyAppraiseRequestArgs()
            {
                DepartCode = "39",
                AreaCode = "",
                ModelId = 14,
                Number = 2016,
                RoleId = "2",
                strReportMessage = "",
                Type = 3,
                Year = 2016
            };

            PageInfo pageInfo = new PageInfo() { };

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetDutyAppraise(args, pageInfo);

            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            return ajax;
        }

        #endregion

        #region [ 监督员评价 ]

        public AjaxFxRspJson GetCollecterAppraise()
        {
            List<Collecter_Apprise> list = new List<Collecter_Apprise>();

            CollecterAppraiseRequestArgs args = new CollecterAppraiseRequestArgs()
            {
                AreaCode = "",
                StreetCode = "",
                ModelId = 21,
                Number = 2016,
                RoleId = "2",
                strReportMessage = "",
                Type = 3,
                Year = 2016
            };

            PageInfo pageInfo = new PageInfo() { };

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetCollecterAppraise(args, pageInfo);

            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            return ajax;
        }

        #endregion
    }
}
