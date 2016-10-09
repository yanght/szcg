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
    public class SystemController : Controller
    {
        ISystemService svc = new SystemService();

        //
        // GET: /System/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 获取工作时间列表 ]

        public AjaxFxRspJson GetSetTimeList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            PageInfo pageInfo = new PageInfo()
            {
                CurrentPage = "1",
                Field = "qy_date",
                Order = "desc",
                PageSize = "20"
            };

            List<WorkTime> list = svc.GetSetTimeList(pageInfo);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 添加工作时间 ]

        [HttpPost]
        public AjaxFxRspJson AddSetTime(WorkTime time)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            int i = Convert.ToInt32(time.MstartTime.Replace(":", ""));
            int j = Convert.ToInt32(time.MendTime.Replace(":", ""));
            int m = j - i;

            if (string.IsNullOrEmpty(time.MstartTime))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入时间！";
                return ajax;
            }

            if (i > j)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "开始时间应小于结束时间！";
                return ajax;
            }

            if (m < 300)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "设置的工作时间不合理，工作时间太短！";
                return ajax;
            }
            if (string.IsNullOrEmpty(time.qy_date))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入启用日期！";
                return ajax;
            }

            int ret_1 = time.qy_date.CompareTo(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if (ret_1 < 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "启用日期要大于当前日期！";
                return ajax;
            }

            bool rtn = svc.AddSetTime(time.MstartTime, time.MendTime, time.FstartTime, time.FendTime, time.qy_date);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "添加工作时间失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 删除设置时间点列表 ]

        public AjaxFxRspJson DeleteSetTime(string[] times)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.DeleteSetTime(times);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "删除工作时间失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取节假日列表 ]

        public AjaxFxRspJson GetHolodayList(string name, string starttime, string endtime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            try
            {
                if (starttime != "")
                {
                    dt1 = Convert.ToDateTime(starttime);
                }
                if (endtime != "")
                {
                    dt2 = Convert.ToDateTime(endtime);
                }
                if (dt1 > dt2)
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "开始时间不能大于结束时间！";
                    return ajax;
                }
            }
            catch (Exception ex)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入正确的日期类型！";
                return ajax;
            }

            PageInfo pageInfo = new PageInfo()
            {
                CurrentPage = "1",
                PageSize = "20",
                Field = "hdate",
                Order = "asc"
            };

            List<Holiday> list = svc.GetHolodayList(name, starttime, endtime, pageInfo);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 添加节假日时间 ]

        [HttpPost]
        public AjaxFxRspJson InsertHoliday(string name, string date)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(name))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "名称不能为空！";
                return ajax;
            }

            if (string.IsNullOrEmpty(date))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "日期不能为空！";
                return ajax;
            }

            bool rtn = svc.InsertHoliday(name, date);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "添加节假日失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

    }
}
