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

        public JsonResult GetEvePartAppraise(string Type, string Year, string Number)
        {
            if (string.IsNullOrEmpty(Number) || Number == "null")
            {
                Number = Year;
            }

            EveParAppraiseRequestArgs args = new EveParAppraiseRequestArgs()
            {
                StreetCode = "",
                Number = int.Parse(Number),
                Type = int.Parse(Type),
                Year = int.Parse(Year)
            };
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

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

            List<EvePar_Appraise> list = svc.GetEvePartAppraise(args);

            ajax.RspData.Add("list", JToken.FromObject(list));

            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            // return ajax;
            return Json(new { draw = Request["draw"], recordsTotal = list.Count, recordsFiltered = list.Count, data = list.Skip(pagesize * (currentpage - 1)).Take(pagesize) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 区域评价 ]

        public JsonResult GetAreaAppraise(string id, string StreetId, string SquareId, string Type, string Year, string Number)
        {
            if (string.IsNullOrEmpty(Number) || Number == "null")
            {
                Number = Year;
            }

            AreaAppraiseRequestArgs args = new AreaAppraiseRequestArgs()
            {
                AreaCode = this.UserInfo.getAreacode(),
                ModelId = 6,
                Number = int.Parse(Number),
                RoleId = UserInfo.CurrentRole.ToString(),
                strReportMessage = "",
                Type = int.Parse(Type),
                Year = int.Parse(Year)
            };

            if (string.IsNullOrEmpty(SquareId))
            {
                if (string.IsNullOrEmpty(StreetId))
                {
                    args.AreaCode = Convert.ToString(this.UserInfo.getAreacode());
                }
                else
                {
                    args.AreaCode = Convert.ToString(StreetId);
                }
            }
            else
            {
                args.AreaCode = SquareId;
            }

            PageInfo pageInfo = new PageInfo() { };

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Area_Appraise> list = svc.GetAreaAppraise(args, pageInfo);
            Session["startTime"] = args.startTime;
            Session["endTime"] = args.endTime;
            Session["cols"] = args.cols;
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

        public JsonResult GetDepartAppraise(string Type, string Year, string Number)
        {
            List<Depart_Appraise> list = new List<Depart_Appraise>();

            if (string.IsNullOrEmpty(Number) || Number == "null")
            {
                Number = Year;
            }

            DepartAppraiseRequestArgs args = new DepartAppraiseRequestArgs()
            {
                DepartCode = "",
                AreaCode = this.UserInfo.getAreacode(),
                ModelId = 24,
                Number = int.Parse(Number),
                RoleId = this.UserInfo.CurrentRole.ToString(),
                strReportMessage = "",
                Type = int.Parse(Type),
                Year = int.Parse(Year)
            };

            PageInfo pageInfo = new PageInfo() { };

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

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetDepartAppraise(args, pageInfo);
            ViewBag.Cols = args.cols;

            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            //  return ajax;

            return Json(new { draw = Request["draw"], recordsTotal = list.Count, recordsFiltered = list.Count, data = list.Skip(pagesize * (currentpage - 1)).Take(pagesize) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  [ 岗位评价 ]

        public JsonResult GetDutyAppraise(string Code, string Name, string DepartCode, string Type, string Year, string Number, string ModelId)
        {
            List<Duty_Appraise> list = new List<Duty_Appraise>();

            if (string.IsNullOrEmpty(Number) || Number == "null")
            {
                Number = Year;
            }

            DutyAppraiseRequestArgs args = new DutyAppraiseRequestArgs()
            {
                DepartCode = DepartCode,
                AreaCode = "",
                ModelId = !string.IsNullOrEmpty(ModelId) ? int.Parse(ModelId) : 0,
                Number = int.Parse(Number),
                RoleId = "2",
                strReportMessage = "",
                Type = int.Parse(Type),
                Year = int.Parse(Year),
                Code = Code,
                Name = Name
            };

            PageInfo pageInfo = new PageInfo() { };

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

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetDutyAppraise(args, pageInfo);
            ViewBag.Cols = args.cols;
            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            //return ajax;

            return Json(new { draw = Request["draw"], recordsTotal = list.Count, recordsFiltered = list.Count, data = list.Skip(pagesize * (currentpage - 1)).Take(pagesize) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 监督员评价 ]

        public JsonResult GetCollecterAppraise(string CollectorCode, string LoginName, string StreetId, string Type, string Year, string Number)
        {
            List<Collecter_Apprise> list = new List<Collecter_Apprise>();
            if (string.IsNullOrEmpty(Number) || Number == "null")
            {
                Number = Year;
            }

            CollecterAppraiseRequestArgs args = new CollecterAppraiseRequestArgs()
            {
                CollectorCode = CollectorCode,
                LoginName = LoginName,
                AreaCode = this.UserInfo.getAreacode(),
                StreetCode = StreetId,
                ModelId = 21,
                Number = int.Parse(Number),
                RoleId = this.UserInfo.getUsercode().ToString(),
                strReportMessage = "",
                Type = int.Parse(Type),
                Year = int.Parse(Year)
            };

         
            PageInfo pageInfo = new PageInfo() { };

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

            pageInfo.PageSize = Request["length"];

            pageInfo.CurrentPage = currentpage.ToString();

            pageInfo.Field = "code";

            pageInfo.Order = "desc";

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            list = svc.GetCollecterAppraise(args, pageInfo);
            ViewBag.Cols = args.cols;
            ajax.RspData.Add("list", JToken.FromObject(list));
            ajax.RspData.Add("pageInfo", JToken.FromObject(pageInfo));
            ajax.RspData.Add("reportMessage", JToken.FromObject(args.strReportMessage));

            return Json(new { draw = Request["draw"], recordsTotal = list.Count, recordsFiltered = list.Count, data = list.Skip(pagesize * (currentpage - 1)).Take(pagesize) }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
