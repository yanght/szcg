using bacgBL.com.teamax.szbase.systemsetting;
using bacgBL.web.szbase.doormanager;
using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    public class SystemService : ISystemService
    {
        string strErr = string.Empty;

        /// <summary>
        /// 获取工作时间列表
        /// </summary>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        public List<WorkTime> GetSetTimeList(PageInfo pageInfo)
        {
            LogManage log = new LogManage();

            List<WorkTime> list = new List<WorkTime>();

            int RowCount = 0, PageCount = 0;

            string ErrorString = string.Empty;

            DataSet ds = log.GetSetTimelist(int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), pageInfo.Order, pageInfo.Field, ref RowCount, ref PageCount, ref ErrorString);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<WorkTime>.GetModelList(ds.Tables[0]);
            }

            pageInfo.RowCount = RowCount.ToString();

            pageInfo.PageCount = PageCount.ToString();

            return list;
        }

        /// <summary>
        /// 添加工作时间
        /// </summary>
        /// <param name="mstartTime">上午工作开始时间</param>
        /// <param name="mendTime">上午工作结束时间</param>
        /// <param name="fstartTime">下午工作开始时间</param>
        /// <param name="fendTime">下午工作结束时间</param>
        /// <param name="qy_date">启用日期</param>
        /// <returns></returns>
        public bool AddSetTime(string mstartTime, string mendTime, string fstartTime, string fendTime, string qy_date)
        {
            EventDepartManage bl = new EventDepartManage();

            int temp = bl.AddSetTime(mstartTime, mendTime, fstartTime, fendTime, qy_date, ref strErr);

            return temp > 0;
        }

        /// <summary>
        /// 删除设置时间点列表
        /// </summary>
        /// <param name="times">时间列表</param>
        /// <returns></returns>
        public bool DeleteSetTime(string[] times)
        {
            int temp = 0;
            for (int i = 0; i < times.Length; i++)
            {
                LogManage bl = new LogManage();
                string strErr = "";
                temp += bl.DeleteSetTime(times[i], ref strErr);
                if (!string.IsNullOrEmpty(strErr))
                {
                    LoggerManager.Instance.logger.ErrorFormat("删除设置时间列表失败 错误信息：{0}", times);
                    return false;
                }
            }
            if (temp > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        /// <param name="name">节假日名称（模糊查询）</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        public List<Holiday> GetHolodayList(string name, string startTime, string endTime, PageInfo pageInfo)
        {
            List<Holiday> list = new List<Holiday>();

            LogManage log = new LogManage();

            int RowCount = 0, PageCount = 0;

            string ErrorString = string.Empty;

            DataSet ds = log.GetHolidaylist(int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), pageInfo.Order, pageInfo.Field, name, startTime, endTime, ref RowCount, ref PageCount, ref ErrorString);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Holiday>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 添加节假日时间
        /// </summary>
        /// <param name="name">节假日名称</param>
        /// <param name="date">借节日日期 格式例如:2010-04-06 </param>
        /// <returns></returns>
        public bool InsertHoliday(string name, string date)
        {
            EventDepartManage bl = new EventDepartManage();

            int temp = bl.InsertHoliday(name, date, ref strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("天假节假日时间失败 错误信息 ：{0}", strErr);
            }

            return temp > 0;

        }

    }
}
