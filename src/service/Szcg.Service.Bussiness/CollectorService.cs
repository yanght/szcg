using bacgBL.business;
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

            bacgBL.web.szbase.entity.Collecter oldcollecter = bl.GetCollecterInfoByID(collecter.CollCode, ref strErr);

            if (oldcollecter.CollName != collecter.CollName)
            {

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
            }

            List<string> list = new List<string>();
            list.Add(collecter.CommCode);
            list.Add(collecter.GridCode);
            list.Add(collecter.Code);
            list.Add(collecter.CollName);
            list.Add(collecter.LoginName);
            list.Add(collecter.PassWord);
            list.Add(collecter.Sex);
            list.Add(collecter.Mobile);
            list.Add(collecter.Age);
            list.Add(collecter.Url);
            list.Add(collecter.HomeTel);
            list.Add(collecter.HomeAddress);
            list.Add(collecter.TimeOut);
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
            ArrayList[] list = bl.GetAllCollecter(args.Type, args.Id, args.PageIndex, args.PageSize, args.ReturnRecordCount, args.Name == null ? "" : args.Name == null ? "" : args.Name, args.LoginName == null ? "" : args.LoginName, args.GridCode == null ? "" : args.GridCode, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取监督员列表异常：" + strErr);
            }

            if (list[0] != null && list[0].Count > 0)
            {
                foreach (bacgBL.web.szbase.entity.Collecter item in list[0])
                {
                    Collecters.Add(new Collecter()
                    {
                        CollCode = item.CollCode,
                        CollName = item.CollName,
                        GridCode = item.GridCode,
                        LoginName = item.LoginName,
                        Mobile = item.Mobile,
                        IMEI = item.Imei,
                        HomeTel = item.Tel,
                        HomeAddress = item.Address
                    });
                }
            }

            if (args.ReturnRecordCount == 1)
            {

                args.ReturnRecordCount = int.Parse(list[1][0].ToString());
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

        /// <summary>
        /// 查询监督员明细
        /// </summary>
        /// <param name="collcode">监督员编码</param>
        /// <returns></returns>
        public Collecter GetCollecterInfoByCode(string collcode)
        {
            bacgBL.web.szbase.entity.Collecter collObject = bl.GetCollecterInfoByID(int.Parse(collcode), ref strErr);

            Collecter collecter = new Collecter()
            {
                CollCode = collObject.CollCode,
                CollName = collObject.CollName,
                GridCode = collObject.GridCode,
                CommCode = collObject.CommCode,
                LoginName = collObject.LoginName,
                PassWord = collObject.Pwd,
                Sex = collObject.Sex,
                Mobile = collObject.Mobile,
                Age = collObject.Age,
                Url = collObject.Url,
                HomeTel = collObject.Tel,
                HomeAddress = collObject.Address,
                TimeOut = collObject.TimeOut.ToString(),
                Memo = collObject.Memo,
                Code = collObject.NumberCode,
                CommName = collObject.CommName,
                IMEI = collObject.Imei
            };
            return collecter;
        }

        /// <summary>
        /// 获取监督员工作任务统计
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="streetId">街道编码</param>
        /// <param name="name">监督员姓名</param>
        /// <param name="type">案卷类型（1：普通案卷  1：快速上报案卷）</param>
        /// <param name="hcpower">用户核查权限</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="message">统计信息描述</param>
        /// <returns></returns>
        public List<CollecterTask> GetTaskStat(string projcode, string streetId, string name, string type, string hcpower, DateTime beginTime, DateTime endTime, out string message)
        {
            message = string.Empty;

            List<CollecterTask> list = new List<CollecterTask>();

            DataSet ds = bacgBL.business.collecter.GetTaskStat(projcode, streetId, name, type, beginTime, endTime, hcpower, ref strErr);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<CollecterTask>.GetModelList(ds.Tables[0]);
            }

            if (ds.Tables.Count > 1)
            {
                message = string.Format("{0},{1},{2},{3},{4},{5}", ds.Tables[1].Rows[0][0].ToString(), ds.Tables[1].Rows[1][0].ToString(), ds.Tables[1].Rows[2][0].ToString(), ds.Tables[1].Rows[3][0].ToString(), ds.Tables[1].Rows[4][0].ToString(), ds.Tables[1].Rows[5][0].ToString());
            }

            return list;
        }

        /// <summary>
        /// 获取所有城管通手机列表
        /// </summary>
        /// <param name="type">类型(area,street,community)</param>
        /// <param name="id">区编码或者街道id或者社区id</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="iesiCard">IMSI卡号</param>
        /// <param name="iemiCard">IMEI卡号</param>
        /// <param name="gridCode">网格号</param>
        /// <returns></returns>
        public List<Collecter> GetAllMobile(string type, string id, PageInfo pageInfo, string mobile, string iesiCard, string iemiCard, string gridCode)
        {
            List<Collecter> colls = new List<Collecter>();

            CollecterManage bl = new CollecterManage();

            ArrayList[] list = bl.GetAllMobile(type, int.Parse(id), int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), int.Parse(pageInfo.ReturnRecordCount), mobile, iesiCard, iemiCard, gridCode, ref strErr);

            for (int i = 0; i < list[0].Count; i++)
            {
                bacgBL.web.szbase.entity.Collecter coll = (bacgBL.web.szbase.entity.Collecter)list[0][i];

                Collecter c = new Collecter()
                {
                    CollCode = coll.CollCode,
                    GridCode = coll.GridCode,
                    CollName = coll.CollName,
                    LoginName = coll.LoginName,
                    Mobile = coll.Mobile,
                    IMEI = coll.Imei,
                    HomeTel = coll.Tel,
                    IMSI = coll.ImsiCard,
                    HomeAddress = coll.Address,
                    Regdate = coll.Regdate,
                    Cu_Date = coll.Cudate
                };

                colls.Add(c);

            }

            pageInfo.RowCount = list[1][0].ToString();

            return colls;
        }

        /// <summary>
        /// 插入城管通信息
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public ReturnValue InsertIntoMobile(Collecter col)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            int t = bl.CheckMobile(col.Mobile, ref strErr);
            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (t > 0)
            {
                rtn.ReturnState = false;
                rtn.ErrorMsg = "城管通号码不能重复!";
                return rtn;
            }

            bacgBL.web.szbase.entity.Collecter collecter = new bacgBL.web.szbase.entity.Collecter()
            {
                Mobile = col.Mobile,
                ImsiCard = col.IMSI,
                ImeiCard = col.IMEI,
                GridCode = col.GridCode
            };

            rtn.ReturnState = bl.InsertIntoMobile(collecter, ref strErr) > 0;

            return rtn;
        }

        /// <summary>
        /// 更新城管通信息
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool UpdateToMobile(Collecter col)
        {
            bacgBL.web.szbase.entity.Collecter collecter = new bacgBL.web.szbase.entity.Collecter()
            {
                Mobile = col.Mobile,
                ImsiCard = col.IMSI,
                ImeiCard = col.IMEI,
                GridCode = col.GridCode
            };

            return bl.UpdateToMobile(collecter, ref strErr) > 0;
        }

        /// <summary>
        /// 通过城管通号码,获取城管通信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public Collecter GetMobileByMobile(string mobile)
        {
            Collecter coll = new Collecter();

            bacgBL.web.szbase.entity.Collecter collObject = bl.GetMobileByID(mobile, ref strErr);
            if (collObject != null)
            {

                coll.Mobile = collObject.Mobile;
                coll.IMEI = collObject.Imei;
                coll.IMSI = collObject.ImsiCard;
                coll.GridCode = collObject.GridCode;
            }

            return coll;

        }

    }
}
