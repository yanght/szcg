using bacgBL.business;
using bacgBL.web.szbase.collecter;
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
    public class CollectorService : ICollectorService
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
        /// 获取监督员列表
        /// </summary>
        /// <param name="CollectorQueryArgs">监督员查询参数</param>
        /// <returns></returns>
        public List<Collecter> GetCollectors(CollectorQueryArgs args)
        {
            List<Collecter> collectors = new List<Collecter>();
            ArrayList[] list = bl.GetAllCollecter(args.Type, args.Id, args.PageIndex, args.PageSize, args.ReturnRecordCount, args.Name, args.LoginName, args.GridCode, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取监督员列表异常：" + strErr);
            }
            if (args.ReturnRecordCount == 1)
            {
                foreach (var item in list[1])
                {
                    collectors.Add((Collecter)item);
                }
            }
            else
            {
                foreach (var item in list[0])
                {
                    collectors.Add((Collecter)item);
                }
            }
            return collectors;
        }

        /// <summary>
        /// 获取监督员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <returns></returns>
        public List<Collecter> GetCollectors(string areacode, string streetcode, string commcode)
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
        public List<Collecter> GetCollectors(string streetcode, string projcode)
        {
            List<Collecter> collectors = new List<Collecter>();
            DataSet ds = bacgBL.business.collecter.GetCollectereList4HC(streetcode, projcode, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取核查用的监督员列表异常：" + strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                collectors = ConvertDtHelper<Collecter>.GetModelList(ds.Tables[0].Rows);
            }
            return collectors;
        }
    }
}
