﻿using bacgBL.business;
using bacgBL.web.szbase.collecter;
using bacgDL.business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;


namespace Szcg.Service.Bussiness
{
    public class Collecterservice : ICollecterservice
    {
        private string strErr = "";

        private CollecterManage bl = new CollecterManage();

        /// <summary>
        /// 添加监督员
        /// </summary>
        /// <param name="collecter">监督员对象</param>
        /// <returns></returns>
        public ReturnValue AddCollecter(Collecter collecter)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            int temp = bl.CheckLoginName(collecter.LoginName, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("检查用户名重复异常：" + strErr);
            }
            if (temp > 0)
            {
                rtn.ErrorMsg = "登录名不能重复!";
                rtn.ReturnState = false;
                return rtn;
            }

            List<string> list = new List<string>();
            list.Add(collecter.CollCode.ToString());
            list.Add(collecter.GridCode);
            list.Add(collecter.Code);
            list.Add(collecter.CollName);
            list.Add(collecter.LoginName);
            list.Add(collecter.PassWord);
            list.Add(collecter.Sex.ToString());
            list.Add(collecter.Mobile);
            list.Add(collecter.Age.ToString());
            list.Add(collecter.Url);
            list.Add(collecter.HomeTel);
            list.Add(collecter.HomeAddress);
            list.Add(collecter.TimeOut.ToString());
            list.Add(collecter.Memo);
            list.Add(collecter.IMEI);
            int i = bl.InsertIntoCollecter(list.ToArray(), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("添加监督员异常：" + strErr);
                rtn.ErrorMsg = "添加监督员失败!";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 修改监督员信息
        /// </summary>
        /// <param name="collecter">new监督员对象</param>
        /// <returns></returns>
        public ReturnValue ModifyCollector(Collecter collecter)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            int temp = bl.CheckLoginName(collecter.LoginName, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("检查用户名重复异常：" + strErr);
            }
            if (temp > 0)
            {
                rtn.ErrorMsg = "登录名不能重复!";
                rtn.ReturnState = false;
                return rtn;
            }

            List<string> list = new List<string>();
            list.Add(collecter.CollCode.ToString());
            list.Add(collecter.GridCode);
            list.Add(collecter.Code);
            list.Add(collecter.CollName);
            list.Add(collecter.LoginName);
            list.Add(collecter.PassWord);
            list.Add(collecter.Sex.ToString());
            list.Add(collecter.Mobile);
            list.Add(collecter.Age.ToString());
            list.Add(collecter.Url);
            list.Add(collecter.HomeTel);
            list.Add(collecter.HomeAddress);
            list.Add(collecter.TimeOut.ToString());
            list.Add(collecter.Memo);
            list.Add(collecter.IMEI);
            int i = bl.UpdateToCollecter(list.ToArray(), collecter.CollCode.ToString(), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("添加监督员异常：" + strErr);
                rtn.ErrorMsg = "添加监督员失败!";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 删除监督员
        /// </summary>
        /// <param name="collectorCode">监督员编号</param>
        /// <returns></returns>
        public bool DeleteCollector(int collectorcode)
        {
            int temp = bl.DeleteFromCollecter(collectorcode, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("删除监督员异常：" + strErr);
            }
            return temp > 0;
        }

        /// <summary>
        /// 获取指定区域所有监督员列表
        /// </summary>
        /// <param name="CollectorQueryArgs">监督员查询参数</param>
        /// <returns></returns>
        public List<Collecter> GetCollecters(CollectorQueryArgs args)
        {
            List<Collecter> Collecters = new List<Collecter>();
            ArrayList[] list = bl.GetAllCollecter(args.Type, args.Id, args.PageIndex, args.PageSize, args.ReturnRecordCount, args.Name, args.LoginName, args.GridCode, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取监督员列表异常：" + strErr);
            }
            if (args.ReturnRecordCount == 1)
            {
                foreach (var item in list[1])
                {
                    Collecters.Add((Collecter)item);
                }
            }
            else
            {
                foreach (var item in list[0])
                {
                    Collecters.Add((Collecter)item);
                }
            }
            return Collecters;
        }


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
        public List<Collecter> GetCollectereList(string streetcode, string gridcode, string name, string loginname, string collmobile, string isguard, PageInfo pageInfo)
        {
            List<Collecter> list = new List<Collecter>();

            int totalRows = 0; int totalPages = 0;

            DataSet ds = bacgBL.business.collecter.GetCollectereList(streetcode, gridcode, name, loginname, collmobile, isguard, int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), ref totalRows, ref totalPages, pageInfo.Order, pageInfo.Field, ref strErr);

            pageInfo.RowCount = totalRows.ToString();

            pageInfo.PageCount = totalPages.ToString();

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Collecter>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 获取监督员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <returns></returns>
        public List<Collecter> GetCollecters(string areacode, string streetcode, string commcode)
        {
            List<Collecter> list = new List<Collecter>();

            DataSet ds = bacgBL.business.Project.GetCollecterList(areacode, streetcode, commcode, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取监督员列表异常:" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Collecter>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 获取核查用的监督员列表
        /// </summary>
        /// <param name="streetCode">街道编码</param>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        public List<Collecter> GetCollecters(string streetcode, string projcode)
        {
            List<Collecter> collecters = new List<Collecter>();
            DataSet ds = bacgBL.business.collecter.GetCollectereList4HC(streetcode, projcode, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取核查用的监督员列表异常：" + strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                collecters = ConvertDtHelper<Collecter>.GetModelList(ds.Tables[0].Rows);
            }
            return collecters;
        }
    }
}
