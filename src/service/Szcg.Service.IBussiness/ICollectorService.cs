﻿using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface ICollecterservice
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
        /// 获取指定区域所有监督员列表
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        List<Collecter> GetCollecters(CollectorQueryArgs args);

        /// <summary>
        /// 获取监督员列表
        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="gridcode">网格编码</param>
        /// <param name="name">监督员姓名</param>
        /// <param name="loginname">登录名</param>
        /// <param name="collmobile">城管通号</param>
        /// <param name="isguard">在岗状态（0：不在岗  1：在岗）</param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        List<Collecter> GetCollectereList(string streetcode, string gridcode, string name, string loginname, string collmobile, string isguard, PageInfo pageInfo);

        /// <summary>
        /// 获取核查用的监督员列表
        /// </summary>
        /// <param name="streetCode">街道编码</param>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        List<Collecter> GetCollecters(string streetcode, string projcode);

        /// <summary>
        /// 获取监督员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <returns></returns>
        List<Collecter> GetCollecters(string areacode, string streetcode, string commcode);


    }
}
