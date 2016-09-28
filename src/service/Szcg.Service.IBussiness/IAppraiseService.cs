using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;
using Szcg.Service.Model.RequestModel;

namespace Szcg.Service.IBussiness
{
    public interface IAppraiseService
    {
        /// <summary>
        /// 获取事部件评价
        /// </summary>
        /// <param name="args">事部件评价查询参数</param>
        /// <returns></returns>
        List<EvePar_Appraise> GetEvePartAppraise(EveParAppraiseRequestArgs args);

        /// <summary>
        /// 获取区域评价
        /// </summary>
        /// <param name="args">区域评价查询参数</param>
        /// <returns></returns>
        List<Area_Appraise> GetAreaAppraise(AreaAppraiseRequestArgs args, PageInfo pageInfo);

        /// <summary>
        /// 责任单位评价
        /// </summary>
        /// <param name="args">责任单位评价查询参数</param>
        /// <returns></returns>
        List<Depart_Appraise> GetDepartAppraise(DepartAppraiseRequestArgs args, PageInfo pageInfo);

        /// <summary>
        /// 岗位评价
        /// </summary>
        /// <param name="args">岗位评价查询参数</param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<Duty_Appraise> GetDutyAppraise(DutyAppraiseRequestArgs args, PageInfo pageInfo);

        /// <summary>
        /// 监督员评价
        /// </summary>
        /// <param name="args">监督员评价查询参数</param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<Collecter_Apprise> GetCollecterAppraise(CollecterAppraiseRequestArgs args, PageInfo pageInfo);
    }
}
