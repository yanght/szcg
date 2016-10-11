using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface ISystemService
    {
        /// <summary>
        /// 获取工作时间列表
        /// </summary>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        List<WorkTime> GetSetTimeList(PageInfo pageInfo);

        /// <summary>
        /// 添加工作时间
        /// </summary>
        /// <param name="mstartTime">上午工作开始时间</param>
        /// <param name="mendTime">上午工作结束时间</param>
        /// <param name="fstartTime">下午工作开始时间</param>
        /// <param name="fendTime">下午工作结束时间</param>
        /// <param name="qy_date">启用日期</param>
        /// <returns></returns>
        bool AddSetTime(string mstartTime, string mendTime, string fstartTime, string fendTime, string qy_date);

        /// <summary>
        /// 删除设置时间点列表
        /// </summary>
        /// <param name="times">时间列表</param>
        /// <returns></returns>
        bool DeleteSetTime(string[] times);

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        /// <param name="name">节假日名称（模糊查询）</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        List<Holiday> GetHolodayList(string name, string startTime, string endTime, PageInfo pageInfo);

        /// <summary>
        /// 添加节假日时间
        /// </summary>
        /// <param name="name">节假日名称</param>
        /// <param name="date">借节日日期 格式例如:2010-04-06 </param>
        /// <returns></returns>
        bool InsertHoliday(string name, string date);
    }
}
