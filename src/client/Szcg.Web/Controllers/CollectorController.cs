using bacgDL.business;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public JsonResult GetCollecters(CollectorQueryArgs args)
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

            args.PageSize = int.Parse(Request["length"]);
            args.PageIndex = int.Parse(currentpage.ToString());
            args.ReturnRecordCount = 1;

            if (args.Id == 0)
            {
                args.Id = int.Parse(UserInfo.getAreacode());
            }

            List<Collecter> list = svc.GetCollecters(args);

            ajax.RspData.Add("collecters", JToken.FromObject(list));

            return Json(new { draw = Request["draw"], recordsTotal = args.ReturnRecordCount, recordsFiltered = args.ReturnRecordCount, data = list == null ? new List<Collecter>() : list }, JsonRequestBehavior.AllowGet);
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


        public JsonResult GetAllMobile(string type, string id, string mobile, string iesiCard, string iemiCard, string grideCode)
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

            pageInfo.ReturnRecordCount = "1";

            if (string.IsNullOrEmpty(id))
            {
                id = UserInfo.getAreacode();
            }

            List<Collecter> list = svc.GetAllMobile(type, id, pageInfo, mobile, iesiCard, iemiCard, grideCode);

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

            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            ajax = CheckCollecterValue(collector);

            if (ajax.RspCode == 0)
            {
                return ajax;
            }

            if (collector.CollCode == 0)
            {
                rtn = svc.AddCollecter(collector);
            }
            else
            {
                rtn = svc.ModifyCollector(collector);
            }

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

        #region [ 删除监督员 ]

        public AjaxFxRspJson DeleteCollector(string collcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(collcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入监督员编码";
                return ajax;
            }

            bool rtn = svc.DeleteCollector(int.Parse(collcode));

            if (!rtn)
            {

                ajax.RspCode = 0;
                ajax.RspMsg = "删除监督员失败";
                return ajax;
            }


            return ajax;
        }

        #endregion

        #region [ 查询监督员明细 ]

        public AjaxFxRspJson GetCollecterInfoByCode(string collcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(collcode))
            {
                ajax.RspData.Add("collecter", JToken.FromObject(new Collecter()));
                return ajax;
            }
            else
            {
                Collecter collecter = svc.GetCollecterInfoByCode(collcode);
                ajax.RspData.Add("collecter", JToken.FromObject(collecter));
            }

            return ajax;
        }

        #endregion

        #region [ 插入或修改城管通信息 ]

        public AjaxFxRspJson InsertIntoMobile(Collecter collecter)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (collecter.CollCode > 0)
            {
                return UpdateToMobile(collecter);
            }
            else
            {
                ReturnValue rtn = svc.InsertIntoMobile(collecter);

                if (!rtn.ReturnState)
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = rtn.ErrorMsg;
                    return ajax;
                }
            }
            return ajax;
        }

        #endregion

        #region [ 修改城管通信息 ]

        public AjaxFxRspJson UpdateToMobile(Collecter collecter)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.UpdateToMobile(collecter);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "修改失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 通过城管通号码,获取城管通信息 ]

        public AjaxFxRspJson GetMobileByMobile(string mobile)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ajax.RspData.Add("data", JToken.FromObject(svc.GetMobileByMobile(mobile)));

            return ajax;
        }

        #endregion

        public AjaxFxRspJson GetTaskStat(string Projcode, string StreetId, string Name, string Type, string beginTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return ajax;
            }

            string message = string.Empty;

            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            CultureInfo culture = new CultureInfo("zh-CN");
            string time = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            if (beginTime != "")
            {
                begin = Convert.ToDateTime(beginTime, culture);
            }
            else
            {
                string day = time + " 00:00:00";
                begin = Convert.ToDateTime(day, culture);
                beginTime = day;
            }

            if (endTime != "")
            {
                end = Convert.ToDateTime(endTime, culture);
            }
            else
            {
                string day = time + " 23:59:59";
                end = Convert.ToDateTime(day, culture);
                endTime = day;
            }

            if (begin > end)
            {
                DateTime dtTmp = begin;
                begin = end;
                end = dtTmp;
            }

            List<CollecterTask> list = svc.GetTaskStat(Projcode, StreetId, Name, Type, string.Empty, begin, end, out message);

            ajax.RspData.Add("tasks", JToken.FromObject(list));

            ajax.RspData.Add("message", JToken.FromObject(message));

            return ajax;
        }


        public AjaxFxRspJson CheckCollecterValue(Collecter collecter)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            if (string.IsNullOrEmpty(collecter.CollName))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "姓名不能为空";
                return ajax;
            }
            if (collecter.CollName.Length > 9)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "姓名长度不能超过9个字符";
                return ajax;
            }
            if (string.IsNullOrEmpty(collecter.LoginName))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "登录名不能为空";
                return ajax;
            }
            if (collecter.LoginName.Length > 9)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "登录名长度不能超过18个字符";
                return ajax;
            }
            if (string.IsNullOrEmpty(collecter.PassWord))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "口令不能为空";
                return ajax;
            }
            if (collecter.PassWord != null && collecter.PassWord.Length > 18)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "口令长度不能超过18个字符!";
                return ajax;
            }
            if (string.IsNullOrEmpty(collecter.CommCode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "社区名称不能为空";
                return ajax;
            }
            if (string.IsNullOrEmpty(collecter.GridCode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "网格号不能为空";
                return ajax;
            }
            if (collecter.TimeOut.Length > 0)
            {
                if (!Teamax.Common.PublicClass.IsValidInt(collecter.TimeOut))
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "轨迹上传时间请填写数值";
                    return ajax;
                }
            }
            if (collecter.HomeAddress != null && collecter.HomeAddress.Length > 18)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "地址长度不能超过512个字符!";
                return ajax;
            }
            if (collecter.Memo != null && collecter.Memo.Length > 18)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "备注长度不能超过128个字符!";
                return ajax;
            }

            return ajax;
        }
    }
}
