using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface ICollectorService
    {
        /// <summary>
        /// 添加监督员
        /// </summary>
        /// <param name="collecter"></param>
        /// <returns></returns>
        ReturnValue AddCollecter(Collecter collecter);

        /// <summary>
        /// 修改监督员
        /// </summary>
        /// <param name="collecter"></param>
        /// <returns></returns>
        ReturnValue ModifyCollector(Collecter collecter);

        /// <summary>
        /// 删除监督员
        /// </summary>
        /// <param name="collectorCode"></param>
        /// <returns></returns>
        bool DeleteCollector(int collectorCode);

        /// <summary>
        /// 获取监督员集合
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        List<Collecter> GetCollectors(CollectorQueryArgs args);

        /// <summary>
        /// 获取核查用的监督员列表
        /// </summary>
        /// <param name="streetCode">街道编码</param>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        List<Collecter> GetCollectors(string streetcode, string projcode);

        /// <summary>
        /// 获取监督员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <returns></returns>
        List<Collecter> GetCollectors(string areacode, string streetcode, string commcode);


    }
}
