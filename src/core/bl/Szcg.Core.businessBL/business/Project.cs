/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：综合业务系统－案件受理－逻辑层访问类。

 * 结构组成：

 * 作    者：yannis
 * 创建日期：2007-05-25
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;

namespace bacgBL.business
{
    public class Project
    {
        #region GetYJJProjectList：获取待办案件列表

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetYJProjectList(ProjectInfo prj, string times1, string times2, string departId, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetYJProjectList(prj, times1, times2, departId, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetCDProjList：获取存档案件的案件列表
        /// <summary>
        /// 获取存档案件的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetCDProjList(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetCDProjList(prj, times1, times2, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetSPProjList： 获取申请更改案件状态的案件列表
        /// <summary>
        ///  获取申请更改案件状态的案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetSPProjList(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetSPProjList(prj, times1, times2, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDealProjectList：获取待办案件列表

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="strPdaIoFlag">核查状态</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetDealProjectList(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDealProjectList(prj, times1, times2, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDealProjectList：获取待办案件列表

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="strPdaIoFlag">核查状态</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <param name="drptype">2,回退,3,反馈,0,默认</param>
        /// <returns></returns>
        public static DataSet GetDealProjectList(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg, int drptype)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDealProjectList(prj, times1, times2, page, drptype);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDealProjectList：获取待办案件列表

        /// <summary>
        /// 获取违章搭建待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="strPdaIoFlag">核查状态</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <param name="drptype">2,回退,3,反馈,0,默认</param>
        /// <returns></returns>
        public static DataSet GetDealProjectListWZDJ(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg, int drptype)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDealProjectListWZDJ(prj, times1, times2, page, drptype);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取违章案卷
        /// </summary>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="ErrMsg">促物信息</param>
        /// <returns></returns>
        public static DataSet GetProjectWzdj(string times1, string times2, string phonenumber, string keyWords, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjectWzdj(times1, times2, phonenumber, keyWords);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        public static DataSet GetProjectDetailWzdj(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjectDetailWzdj(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        public static DataSet getProjectTrace(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.getProjectTrace(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        public static DataSet GetprojectFiles(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetprojectFiles(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        public static DataSet GetLprojectFiles(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLprojectFiles(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjDetail：取得案卷的详细信息
        /// <summary>
        /// 取得案卷的详细信息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">上报年份</param>
        /// <param name="IsEnd">是否结案</param>
        /// <param name="strNodeId">节点（阶段）</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjDetail(string projcode, string strYear, string IsEnd, string strNodeId, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjDetail(projcode, strYear, IsEnd, strNodeId);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 根据部件编号 找图片

        /// </summary>
        /// <param name="projcode"></param>
        /// <param name="strYear"></param>
        /// <param name="IsEnd"></param>
        /// <param name="strNodeId"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static DataSet GetPartImg(string smallcalsscode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetPartImg(smallcalsscode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjectFile：得到案卷图片文件

        /// <summary>
        /// 得到案卷图片文件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjectFile(string projcode, string strYear, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjectFile(projcode, strYear, IsEnd);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjectSound：得到案卷声音文件

        /// <summary>
        /// 得到案卷声音文件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjectSound(string projcode, string strYear, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjectSound(projcode, strYear, IsEnd);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjAllFiles：得到案卷图片和声音文件
        /// <summary>
        /// 得到案卷图片和声音文件

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjAllFiles(string projcode, string strYear, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjAllFiles(projcode, strYear, IsEnd);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  LockProject：并发操作时，锁定案件记录

        /// <summary>
        /// 并发操作时，锁定案件记录
        /// </summary>
        /// <param name="projcode"></param>
        /// <param name="NodeId"></param>
        /// <param name="usercode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string LockProject(string projcode, string usercode, string NodeId)
        {
            string strResult = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    string Ret, Msg;
                    dl.LockProject(projcode, usercode, NodeId, out Ret, out Msg);

                    strResult = Ret + "$" + Msg;
                }
            }
            catch (Exception e)
            {
                strResult = "-9$" + e.Message;
            }

            return bacgBL.Pub.Tools.StrConv(strResult, "GB2312");
        }
        #endregion


        #region  LockProject：并发操作时，锁定案件记录

        /// <summary>
        /// 并发操作时，锁定案件记录
        /// </summary>
        /// <param name="projcode"></param>
        /// <param name="NodeId"></param>
        /// <param name="usercode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string getNodeId(string projcode)
        {
            string strResult = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                return dl.getNodeId(projcode);

            }
        }
        #endregion

        #region  UnLockProject：并发操作时，解锁案件

        /// <summary>
        /// 并发操作时，解锁案件
        /// </summary>
        /// <param name="projcode"></param>
        /// <param name="NodeId"></param>
        /// <param name="usercode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string UnLockProject(string projcode)
        {
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.UnLockProject(projcode);
                    return "1";
                }
            }
            catch
            {
                return "-3";
            }
        }
        #endregion

        #region  UpObjcodect：更新部件表中objcode字段
        /// <summary>
        /// 更新部件表中objcode字段
        /// </summary>
        /// <param name="projcode">案卷号</param>
        /// <param name="objcode">部件编码</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public void UpObjcodect(string projcode, string objcode, string userid)
        {
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.UpObjcodect(projcode, objcode, userid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  GetProjSomeInfo： 从业务数据库中获取案件的部分主要信息
        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static string GetDBContent(string projcode, out string ErrMsg)
        {
            ErrMsg = "";

            try
            {
                string strSQL = @"  select top 1 content
                                from b_project_inspect
                                where projcode = @projcode
                                order by id desc ";

                SqlParameter[] arrSP = new SqlParameter[]{
                    new SqlParameter("@projcode",projcode)
                };

                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    object oReturn = dl.ExecuteScalar(strSQL, arrSP);
                    return oReturn != null ? oReturn.ToString() : "";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjSomeInfo： 从业务数据库中获取案件的部分主要信息
        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="IsQuerySource">是否要查询该案件相关的来源信息</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, bool IsQuerySource, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@IsQuerySource",IsQuerySource)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="IsExistPart">判断是否存在该部件</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, string IsExistPart, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@IsExistPart",IsExistPart)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="NodeID">当前节点</param>
        /// <param name="LastOpinion">最后批转意见</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, string NodeID, out string LastOpinion, out string ErrMsg)
        {
            LastOpinion = "";
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    //-------------最近的批转意见
                    object oResult = dl.GetLastOpinion(projcode, NodeID);
                    LastOpinion = oResult != null ? oResult.ToString() : "";

                    //得到案件主要信息
                    SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }


        /// <summary>
        /// 从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="NodeID">当前节点</param>
        /// <param name="IsQueryTrace">是否要查询该案件流程</param>
        /// <param name="LastOpinion">最后批转意见</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, string NodeID, bool IsQueryTrace, out string LastOpinion, out string ErrMsg)
        {
            LastOpinion = "";
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    //-------------最近的批转意见
                    object oResult = dl.GetLastOpinion(projcode, NodeID);
                    LastOpinion = oResult != null ? oResult.ToString() : "";

                    //得到案件主要信息
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@IsQueryTrace",IsQueryTrace)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        #region GetlTypeList:获取小类事部件列表
        /// <summary>
        /// 获取小类事部件列表
        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="bigclassCode">大类事部件编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataTable GetTypeList(string typeCode, string smallcode)
        {
            DataSet ds = null;
            DataTable tblBugs = null;
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    ds = dl.GetTypeList(typeCode, smallcode);
                    if (ds.Tables.Count > 0)
                    {
                        //创建数据表
                        tblBugs = new DataTable("tb");
                        DataColumn newColumn;
                        //创建typetime的列
                        newColumn = tblBugs.Columns.Add("detailclass", Type.GetType("System.Int32"));
                        //newColumn.AllowDBNull = false;
                        //创建typename 列
                        newColumn = tblBugs.Columns.Add("typename", Type.GetType("System.String"));
                        //newColumn.AllowDBNull = false;
                        newColumn = tblBugs.Columns.Add("time1", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time2", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time3", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time4", Type.GetType("System.String"));

                        newColumn = tblBugs.Columns.Add("type1", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type2", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type3", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type4", Type.GetType("System.String"));
                        //基于所有创建的数据表架构添加行
                        DataRow newRow;//声明一个新行
                        //判断是否为空
                        int allnum = 10;
                        if (typeCode == "2")
                        {
                            allnum = 1;
                        }
                        for (int x = 0; x < allnum; x++)
                        {
                            if (ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"] != System.DBNull.Value &&
                                ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString() != "0" &&
                                ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString() != "")
                            {
                                //不为空创建行，填充数据；
                                newRow = tblBugs.NewRow();
                                newRow["detailclass"] = x + 1;
                                newRow["typename"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "name"].ToString();

                                newRow["time1"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString();
                                newRow["time2"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_2"].ToString();
                                newRow["time3"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_3"].ToString();
                                newRow["time4"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_4"].ToString();

                                newRow["type1"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type"].ToString();
                                newRow["type2"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_2"].ToString();
                                newRow["type3"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_3"].ToString();
                                newRow["type4"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_4"].ToString();

                                tblBugs.Rows.Add(newRow);
                            }
                        }

                    }
                    else
                    {
                        tblBugs = null;
                    }
                }
                return tblBugs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region GetlTypeListAjax:获取小类事部件列表
        /// <summary>
        /// 获取小类事部件列表
        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="bigclassCode">大类事部件编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public DataTable GetTypeListAjax(string typeCode, string smallcode)
        {
            DataSet ds = null;
            DataTable tblBugs = null;
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    ds = dl.GetTypeList(typeCode, smallcode);
                    if (ds.Tables.Count > 0)
                    {
                        //创建数据表
                        tblBugs = new DataTable("tb");
                        DataColumn newColumn;
                        //创建typetime的列
                        newColumn = tblBugs.Columns.Add("detailclass", Type.GetType("System.Int32"));
                        //newColumn.AllowDBNull = false;
                        //创建typename 列
                        newColumn = tblBugs.Columns.Add("typename", Type.GetType("System.String"));
                        //newColumn.AllowDBNull = false;
                        newColumn = tblBugs.Columns.Add("time1", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time2", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time3", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("time4", Type.GetType("System.String"));

                        newColumn = tblBugs.Columns.Add("type1", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type2", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type3", Type.GetType("System.String"));
                        newColumn = tblBugs.Columns.Add("type4", Type.GetType("System.String"));
                        //基于所有创建的数据表架构添加行
                        DataRow newRow;//声明一个新行
                        //判断是否为空
                        int allnum = 10;
                        if (typeCode == "2")
                        {
                            allnum = 1;
                        }
                        for (int x = 0; x < allnum; x++)
                        {
                            if (ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"] != System.DBNull.Value &&
                                ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString() != "0" &&
                                ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString() != "")
                            {
                                //不为空创建行，填充数据；
                                newRow = tblBugs.NewRow();
                                newRow["detailclass"] = x + 1;
                                newRow["typename"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "name"].ToString();

                                newRow["time1"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time"].ToString();
                                newRow["time2"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_2"].ToString();
                                newRow["time3"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_3"].ToString();
                                newRow["time4"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "time_4"].ToString();

                                newRow["type1"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type"].ToString();
                                newRow["type2"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_2"].ToString();
                                newRow["type3"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_3"].ToString();
                                newRow["type4"] = ds.Tables[0].Rows[0]["t" + (x + 1).ToString() + "type_4"].ToString();

                                tblBugs.Rows.Add(newRow);
                            }
                        }

                    }
                    else
                    {
                        tblBugs = null;
                    }
                }
                return tblBugs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="NodeID">当前节点</param>
        /// <param name="userName">批转人</param>
        /// <param name="departname">所在部门</param>
        /// <param name="LastOpinion">最后批转意见</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, string NodeID,
                    out string LastOpinion, out string userName, out string departname, out string SJ_RoleCode,
                    out string ErrMsg)
        {
            LastOpinion = "";
            userName = "";
            departname = "";
            SJ_RoleCode = "";
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    //-------------最近的批转意见
                    DataSet dsOpinion = dl.GetOpinionInfo(projcode, NodeID);
                    if (dsOpinion != null && dsOpinion.Tables[0].Rows.Count > 0)
                    {
                        LastOpinion = dsOpinion.Tables[0].Rows[0]["_opinion"].ToString();
                        userName = dsOpinion.Tables[0].Rows[0]["userName"].ToString();
                        departname = dsOpinion.Tables[0].Rows[0]["departname"].ToString();
                        SJ_RoleCode = dsOpinion.Tables[0].Rows[0]["SJ_RoleCode"].ToString();
                    }

                    //得到案件主要信息
                    SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="Year">年份</param>
        /// <param name="IsEnd">是否结案</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, string Year, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@year",Year),
                        new SqlParameter("@IsEnd",IsEnd)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo2", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="NodeID1">要查询的节点1</param>
        /// <param name="NodeID2">要查询的节点2</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetProjSomeInfo(string projcode, int NodeID1, int NodeID2, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    //得到案件主要信息
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@NodeID1",NodeID1),
                        new SqlParameter("@NodeID2",NodeID2)
                    };

                    return dl.ExecuteDataset("pr_b_GetProjSomeInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetProjHcResult： 获取案件的核查结果

        /// <summary>
        /// 获取案件的核查结果 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static string GetProjHcResult(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    string strSQL = @"  select top 1 msgcontent 
                                        from b_pdamsg 
                                        where projcode = @projcode 
                                           and ioflag<>'0'"; ;

                    SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
                    };

                    object oResult = dl.ExecuteScalar(strSQL, arrSP);
                    return oResult == null ? "" : oResult.ToString();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return "";
            }
        }
        #endregion

        #region  GetDiction： 取习惯用语内容

        /// <summary>
        /// 取习惯用语内容，比如： 办公用语
        /// </summary>
        /// <param name="DictionCode">字典代码</param>
        /// <param name="stepid">步骤代码</param>
        /// <returns></returns>
        public DataTable GetDiction(string DictionCode, string stepid)
        {
            string sql = string.Format("select * from s_diction_sentence where DictionCode='{0}' and stepcode = '{1}'", DictionCode, stepid);
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.ExecuteDataset(sql).Tables[0];
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region  GetNextNodeId：根据【当前节点编号】和【业务动作】，获取下一个节点编号

        /// <summary>
        /// 根据【当前节点编号】和【业务动作】，获取下一个节点编号

        /// </summary>
        /// <param name="projcode">当前节点编号</param>
        /// <param name="strYear">业务状态ID</param>
        /// <returns>下一个节点编号</returns>
        public static string GetNextNodeId(string CurrentNodeId, string Busistatus)
        {
            string strSQL = string.Format(@"select NextNodeId from s_flownoderelainfo 
                                            where CoreNodeId='{0}' and Busistatus='{1}' and Status='1'", CurrentNodeId, Busistatus);

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                object oResult = dl.ExecuteScalar(strSQL);
                return oResult != null ? oResult.ToString() : "";
            }
        }
        #endregion

        #region  ChangeProjectFlowNode：更改案件流程节点

        /// <summary>
        /// 案件流转时，更改案件的节点状态

        /// </summary>
        /// <param name="pt">案件流程信息</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void ChangeProjectFlowNode(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    Teamax.Common.CommonDatabase dal = new Teamax.Common.CommonDatabase();
                    dl.BeginTrans();
                    string sql = "select ChildDepartCode from b_project where projcode='" + pt.projcode + "'";
                    string childdepartcode = dal.ExecuteScalar(sql).ToString();
                    if (childdepartcode != "")
                    {
                        dl.ChangeChildDepartCode(pt.projcode);
                        //插入职能回退流程
                        sql = string.Format(@" 

 
                    insert into dbo.b_project_trace_depart(
                        projcode,actionname,cu_date,
                        usercode,DepartCode,
                        _opinion) 
                    values(@projcode,@actionname,GetDate(),
                        @usercode,(select UserDefinedCode from p_depart where departcode=@DepartCode),
                        @_opinion)");

                        SqlParameter[] arrSP = new SqlParameter[]{
                    new SqlParameter("@projcode",pt.projcode),
                    new SqlParameter("@actionname",pt.actionname),
                    new SqlParameter("@usercode",pt.usercode),
                    new SqlParameter("@DepartCode",pt.DepartCode),
                    new SqlParameter("@_opinion",pt._opinion)};

                        dl.ExecuteNonQuery(sql, arrSP);
                    }
                    else
                    {
                        dl.ChangeProjectFlowNode(pt);
                    }
                    dl.Commit();
                    // string sqlstr = "update b_project set ChildDepartCode=null, ButtonCode='11900034001', nodeid=8 where projcode=" + pt.projcode;
                    // bacgBL.web.szbase.organize.DepartManage bll = new bacgBL.web.szbase.organize.DepartManage();
                    // DataSet dataset = new DataSet();
                    /*  if (pt.DepartCode != "")
                      {
                          dataset = bll.GetDepartInfo(int.Parse(pt.DepartCode), ref  ErrMsg);

                          if (dataset.Tables[0].Rows[0]["parentcode"].ToString() != "0" && pt.buttoncode == "11900034017" && dataset.Tables[0].Rows[0]["area"].ToString().Length == 6)
                          {
                              dal.ExecuteNonQuery(sqlstr);
                          }
                          else if (dataset.Tables[0].Rows[0]["parentcode"].ToString() == "0" && pt.buttoncode == "11900034017" && pt.buttoncode == "11900034017" && dataset.Tables[0].Rows[0]["area"].ToString().Length == 6)
                          {
                              dal.ExecuteNonQuery("update b_project set ButtonCode='11900032001' where projcode=" + pt.projcode);
                          }
                      }
                      if (pt.PreNodeId == "7" && pt.buttoncode == "11100301003")
                      {
                          dal.ExecuteNonQuery("update b_project set ButtonCode='11900032001' where projcode=" + pt.projcode);
                      }    */
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion


        #region LA_Project：案件立案

        /// <summary>
        /// 案件立案
        /// </summary>
        /// <param name="pi">案件信息</param>
        /// <param name="pt">案件流程</param>
        /// <param name="ErrMsg">返回错误信息</param>
        public static void Project_LA(ProjectInfo pi, ProjectTraceInfo pt, string DBContent, string Leader, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    //更改应处理部门

                    string Tsql = "select isnull(dodepartcode,targetdepartcode) as targetdepartcode from b_project where projcode='" + pi.projcode + "'";
                    string Tdepartcode = dl.ExecuteScalar(Tsql).ToString();
                    if (pt.targetdepart.Equals(Tdepartcode))
                    {
                        dl.ChangeProjectFlowNode(pt);  //改变案件流程
                        string sSQL = "select detailedclass,departTime,departTimeType from b_project where projcode='" + pi.projcode + "'";
                        DataSet ds = dl.ExecuteDataset(sSQL);
                        string cdetailedclass = ds.Tables[0].Rows[0]["detailedclass"].ToString().Trim();
                        string cdepartTime = ds.Tables[0].Rows[0]["departTime"].ToString().Trim();
                        string cdepartTimeType = ds.Tables[0].Rows[0]["departTimeType"].ToString().Trim();
                        if (cdetailedclass == pi.detailedclass && cdepartTime == pi.departTime && cdepartTimeType == pi.departTimeType)
                        {

                        }
                        else
                        {
                            dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                            string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                            string nodeid = dl.ExecuteScalar(strSQL).ToString();
                            if (nodeid == "8")
                            {
                                dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                            }
                        }
                    }
                    else
                    {
                        // dl.ChangeProcessType(pi.ProcessType, pi.projcode);
                        dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                        dl.ChangeProjectFlowNode(pt);  //改变案件流程
                        string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                        string nodeid = dl.ExecuteScalar(strSQL).ToString();
                        if (nodeid == "8")
                        {
                            dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                        }
                    }
                    if (DBContent != "" && Leader != "")
                    {

                    }
                    dl.UnLockProject(pt.projcode);  //解锁
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region DelProject：案件打删除标志
        /// <summary>
        /// 案件打删除标志。不写案件流程

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <returns></returns>
        public static string DelProject(ProjectTraceInfo pt)
        {
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {

                    dl.BeginTrans();

                    //---------插入案件流程---------
                    dl.InsertProjTrace(pt);

                    //---------打删除标志---------
                    string sql = string.Format("update b_project set isdel = 1 where projcode = {0}", pt.projcode);
                    dl.ExecuteNonQuery(sql, null);

                    dl.Commit();
                    return "1";
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    return string.Format("0${0}", e.Message);
                }
            }
        }
        #endregion

        #region  GetProbSource：获取案件来源

        /// <summary>
        /// 获取案件来源
        /// </summary>
        /// <param name="projcode">案件编号</param>
        [AjaxPro.AjaxMethod]
        public static string GetProbSource(string projcode)
        {
            try
            {
                string strSQL = string.Format(@"select probsource from dbo.b_project  where projcode='{0}'", projcode);

                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    object oResult = dl.ExecuteScalar(strSQL);
                    return oResult != null ? oResult.ToString() : "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region DelProjectAndInsertTrace：删除案件，并且插入案件流程
        /// <summary>
        /// 删除案件，并且插入案件流程

        /// </summary>
        /// <param name="pt">案件流程</param>
        /// <param name="ErrMsg"></param>
        public static void DelProjectAndInsertTrace(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    string strSQL = string.Format("update b_project set isdel = 1 where projcode = {0}", pt.projcode);
                    dl.BeginTrans();
                    dl.InsertProjTrace(pt);         //插入案件流程
                    dl.ExecuteNonQuery(strSQL);     //打删除标志

                    dl.UnLockProject(pt.projcode);  //解锁
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetProjTrace:获取案件的流程

        /// <summary>
        ///  GetProjTrace:获取案件的流程

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <returns></returns>
        public static DataSet GetProjTrace(string projcode, string Year, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@Year",Year),
                        new SqlParameter("@IsEnd",IsEnd)
                    };

                    //return dl.ExecuteDataset("pr_b_GetProjTrace", CommandType.StoredProcedure, arrSP);
                    return dl.ExecuteDataset("pr_b_GetProjTrace_Depart", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetIoFlag： 获得核查标示信息
        /// <summary>
        ///  获得核查标示信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="state">类型</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string GetIoFlag(string projcode)
        {
            string strResult = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode)
                    };

                    object oReturn = dl.ExecuteScalar("pr_b_GetIoFlag", CommandType.StoredProcedure, arrSP);

                    strResult = oReturn != null ? oReturn.ToString() : "-9$没有对应的案件记录信息！";
                }
            }
            catch (Exception e)
            {
                strResult = "-9$" + e.Message;
            }

            return bacgBL.Pub.Tools.StrConv(strResult, "GB2312");
        }
        #endregion

        #region  GetMsgCount： 判断是否向监督员发送过消息。

        /// <summary>
        ///  判断是否向监督员发送过消息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="state">类型</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static int GetMsgCount(string projcode, string state)
        {
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    string sql = @" select count(1) from b_pdamsg 
                                    where projcode = @projcode 
                                        and ioflag='0' 
                                        and state=@state";

                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@state",state)
                    };

                    object oReturn = dl.ExecuteScalar(sql, arrSP);
                    return oReturn != null ? int.Parse(oReturn.ToString()) : 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region  InsertProject： 插入案件
        /// <summary>
        /// 插入案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static string InsertProject(ProjectInfo prj, ProjectSourceInfo psi, out string ErrMsg)
        {
            ErrMsg = "";
            string projcode = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    projcode = dl.InsertProject(prj, psi);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
            return projcode;
        }
        #endregion

        #region  UpdateProject：修改案件

        /// <summary>
        /// 修改案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static void UpdateProject(ProjectInfo prj, ProjectSourceInfo psi, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.UpdateProject(prj, psi);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region  UpdateProjectWithOther：其它方式修改案件

        /// <summary>
        /// 修改案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static void UpdateProjectWithOther(ProjectInfo prj, ProjectSourceInfo psi, ProjectTraceInfo pti, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.UpdateProjectWithOther(prj, psi, pti);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region  InsertProjTrace：插入案件流程

        /// <summary>
        /// 插入案件流程
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static void InsertProjTrace(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.InsertProjTrace(pt);
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_HS：案件核实

        /// <summary>
        /// 核实任务时，向B_PDAMSG插入一条任务；更改直通车标示；插入案件流程记录。

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="collcode">监督员</param>
        /// <param name="msgcontent">消息</param>
        /// <param name="ErrMsg">输出错误信息</param>
        public static void Project_HS(ProjectTraceInfo pt, string collcode, string msgcontent, out string ErrMsg, string strButtonCode)
        {
            ErrMsg = "";
            InsertPdaMsg(pt.projcode, pt.usercode, pt.DepartCode, collcode, msgcontent, "0", "0", "0", out ErrMsg, strButtonCode);
        }
        #endregion

        #region InsertPdaMsg：发送PDA消息。比如：核查
        /// <summary>
        /// 发送PDA消息。比如：核查
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="ioflag">IO标志</param>
        /// <param name="usercode">操作员编号</param>
        /// <param name="DepartCode">部门</param>
        /// <param name="collcode">监督员</param>
        /// <param name="msgcontent">消息</param>
        /// <param name="IsHC">是否核查。1核查；0核实</param>
        /// <param name="state">状态</param>
        /// <param name="ErrMsg">输出错误信息</param>
        public static void InsertPdaMsg(string projcode, string usercode, string DepartCode,
                string collcode, string msgcontent,
                string IsHC, string ioflag, string state, out string ErrMsg, string strButtonCode)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode),
                        new SqlParameter("@IsHC",IsHC),
                        new SqlParameter("@usercode",usercode),
                        new SqlParameter("@DepartCode",DepartCode),
                        new SqlParameter("@collcode",collcode),
                        new SqlParameter("@msgcontent",msgcontent),
                        new SqlParameter("@ioflag",ioflag),
                        new SqlParameter("@state",state),
                        new SqlParameter("@strButtonCode",strButtonCode)
                };

                try
                {
                    dl.BeginTrans();
                    dl.ExecuteNonQuery("pr_b_InsertPdaMsg", CommandType.StoredProcedure, arrSP); //向B_PDAMSG插入一条任务

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetGridCollector：根据网格得到监督员的详细信息

        /// <summary>
        /// 根据网格得到监督员的详细信息
        /// </summary>
        /// <param name="gridcode">网格</param>
        /// <returns></returns>
        public static DataSet GetGridCollector(string strcollcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string sql = @"select	a.collcode,
		                                a.collname,
		                                a.gridcode,
		                                a.mobile,
		                                case when isnull(a.isguard,0)=0 then '<font color=Red>不在岗</font>'
			                                when datediff(mi,a.cu_date,getdate())<=30  then '在岗'
			                                when datediff(mi,e.cu_date,getdate())<=30 then '在岗' 
			                                else '<font color=Red>不在岗</font>' 
		                                end as IsGuardName
                                from m_collecter a 
                                left join (
			                                select collcode,max(cu_Date) cu_Date
			                                from  b_pdamsg_Trace  
			                                where datediff(day,cu_Date,getdate())=0
			                                group by collcode
                                ) e
                                on a.collcode=e.collcode
                                where a.collcode = @strcollcode
	                                and isnull(a.isdel,0) = 0
                                union all
                                select	a.collcode,
		                                a.collname,
		                                a.gridcode,
		                                a.mobile,
		                                case when isnull(a.isguard,0)=0 then '<font color=Red>不在岗</font>'
			                                when datediff(mi,a.cu_date,getdate())<=30  then '在岗'
			                                when datediff(mi,e.cu_date,getdate())<=30 then '在岗' 
			                                else '<font color=Red>不在岗</font>' 
		                                end as IsGuardName
                                from m_collecter a 
                                left join (
			                                select collcode,max(cu_Date) cu_Date
			                                from  b_pdamsg_Trace  
			                                where datediff(day,cu_Date,getdate())=0
			                                group by collcode
                                ) e
                                on a.collcode=e.collcode
                                where a.collcode <> @strcollcode
	                                and isnull(a.isdel,0) = 0 ";
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@strcollcode", strcollcode)
                    };

                    return dl.ExecuteDataset(sql, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetAreaName：根据网格得到对应的区/街道/社区名称
        /// <summary>
        /// 根据网格得到对应的区/街道/社区名称
        /// </summary>
        /// <param name="gridcode"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetAreaName(string gridcode)
        {
            try
            {
                string strNames = "0";
                string sql = @" select top 1  a.areaname+'/'+b.streetname+'/'+c.commname+','+cast(a.areacode as varchar(20))+','+cast(b.streetcode as varchar(20))+','+cast(c.commcode as varchar(20))
                                from s_area as a,s_street as b,s_community as c
                                where a.areacode = b.areacode 
	                                and b.streetcode = c.streetcode 
	                                and c.commcode = Left(@gridcode,12)";
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@gridcode", gridcode)
                    };

                    object oResult = dl.ExecuteScalar(sql, arrSP);
                    if (oResult != null)
                    {
                        strNames = bacgBL.Pub.Tools.StrConv(oResult.ToString(), "GB2312");
                    }
                    return strNames;
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region GetProjectDepartInfo：获取当前案件部件信息（事部件标示$小类$部件编号$坐标）

        /// <summary>
        /// 获取当前案件部件信息（事部件标示$小类$部件编号$坐标）

        /// </summary>
        /// <param name="gridcode"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string GetProjectDepartInfo(string projcode)
        {
            try
            {
                string strNames = "1";
                string sql = @" select cast(typecode as char(1))+'$'+b.name+'$'+isnull(PartID,'')+'$'+isnull(fid,'')
	                            from dbo.b_project a , dbo.s_smallclass_part b
	                            where a.smallclass = b.smallcallCode
                                    and a.projcode = @projcode";
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode", projcode)
                    };

                    object oResult = dl.ExecuteScalar(sql, arrSP);
                    if (oResult != null)
                    {
                        strNames = bacgBL.Pub.Tools.StrConv(oResult.ToString(), "GB2312");
                    }
                    return strNames;
                }
            }
            catch
            {
                return "1";
            }
        }
        #endregion

        #region GetDeleteProjectList：获取删除的案件列表
        /// <summary>
        /// 获取删除的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetDeleteProjectList(string streetcode, ProjectInfo prj, string username, string times1, string times2, PageInfo page, string sign, out string ErrMsg, int deltime)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDeleteProjectList(streetcode, prj, username, times1, times2, sign, page, deltime);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetKnowledge： 根据事部件大小类取得知识库

        /// <summary>
        ///  根据事部件大小类取得知识库 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="state">类型</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static string GetKnowledge(string strName, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    string sql = @" select _desc from s_repository where name=@name ";

                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@name",strName)
                    };

                    object oResult = dl.ExecuteScalar(sql, arrSP);
                    return oResult != null ? oResult.ToString() : "";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return "";
            }
        }
        #endregion

        #region  DeleteProject： 根据案件编号，物理删除案件信息

        /// <summary>
        /// 根据案件编号，物理删除案件信息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void DeleteProject(string projcode, string deleteSign, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.DeleteProject(projcode, deleteSign);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
            }
        }
        #endregion

        #region  RollBackProject： 根据案件编号，还原删除的案件
        /// <summary>
        /// 根据案件编号，还原删除的案件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void RollBackProject(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"update b_project set isdel = '0',istransaction=null,locktime=null,lockusercode=null where projcode = '{0}'", pt.projcode);
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();

                    dl.ExecuteNonQuery(strSQL);
                    dl.InsertProjTrace(pt);

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        //20080617日添加

        #region  RetrunProject： 根据案件编号，从BACG_TIMEOUT里还原删除的案件
        /// <summary>
        /// 根据案件编号，还原删除的案件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void ReturnProject(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.ReturnProject(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return;
            }
        }
        #endregion

        #region GetGDProjectList：获取归档的案件列表
        /// <summary>
        /// 获取归档的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetGDProjectList(ProjectInfo prj, string times1, string times2, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetGDProjectList(prj, times1, times2, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion


        #region GetSJPCProjectList：获取普查的案件列表
        /// <summary>
        /// 获取普查的案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetSJPCProjectList(string smallcode, string type, ProjectInfo prj, string username, string times1, string times2, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetSJPCProjectList(smallcode, type, prj, username, times1, times2, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  DeletesjpcProject： 根据案件编号， 删除普查上报的案卷信息
        /// <summary>
        /// 根据案件编号， 删除普查上报的案卷信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void DeletesjpcProject(string projcode, string deleteSign, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.DeletesjpcProject(projcode, deleteSign);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
            }
        }
        #endregion

        #region Project_DB：案件督办

        /// <summary>
        /// 案件督办
        /// </summary>
        /// <param name="Leader">督办人名字</param>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_DB(string IsRepeatDB, string Leader, ProjectInfo pi, ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    //-----------------插入督办流程-----------------
                    dl.DB_InsertInspectInfo(pt.projcode, Leader, pt._opinion, pt.usercode);
                    if (IsRepeatDB != "1")
                    {
                        //-----------------改变案件流程-----------------
                        string Tsql = "select isnull(dodepartcode,targetdepartcode) as targetdepartcode  from b_project where projcode='" + pi.projcode + "'";
                        string Tdepartcode = dl.ExecuteScalar(Tsql).ToString();
                        if (pt.targetdepart.Equals(Tdepartcode))
                        {
                            dl.ChangeProjectFlowNode(pt);  //改变案件流程
                            string sSQL = "select detailedclass,departTime,departTimeType from b_project where projcode='" + pi.projcode + "'";
                            DataSet ds = dl.ExecuteDataset(sSQL);
                            string cdetailedclass = ds.Tables[0].Rows[0]["detailedclass"].ToString().Trim();
                            string cdepartTime = ds.Tables[0].Rows[0]["departTime"].ToString().Trim();
                            string cdepartTimeType = ds.Tables[0].Rows[0]["departTimeType"].ToString().Trim();
                            if (cdetailedclass == pi.detailedclass && cdepartTime == pi.departTime && cdepartTimeType == pi.departTimeType)
                            {

                            }
                            else
                            {
                                dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                                string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                                string nodeid = dl.ExecuteScalar(strSQL).ToString();
                                if (nodeid == "8")
                                {
                                    dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                                }
                            }
                        }
                        else
                        {
                            // dl.ChangeProcessType(pi.ProcessType, pi.projcode);
                            dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                            dl.ChangeProjectFlowNode(pt);  //改变案件流程
                            string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                            string nodeid = dl.ExecuteScalar(strSQL).ToString();
                            if (nodeid == "8")
                            {
                                dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                            }
                        }
                        //-----------------更改区局处理标示-----------------
                        string strSQL1 = string.Format(@"update b_project 
                                                    set isgreat = '{1}'
                                                    where projcode = {0}", pi.projcode, pi.isgreat);
                        dl.ExecuteNonQuery(strSQL1);
                    }
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_PQ：案件派遣

        /// <summary>
        /// 案件派遣
        /// </summary>
        /// <param name="Leader">督办人名字</param>
        /// <param name="DBContent">督办批示</param>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_PQ(string TargetDepartCode, string Leader, string DBContent, ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    if (DBContent != "" && Leader != "")
                        dl.DB_InsertInspectInfo(pt.projcode, Leader, DBContent, pt.usercode); //插入督办流程

                    //更改应处理部门
                    /*  string Tsql = "select isnull(dodepartcode,targetdepartcode) as targetdepartcode from b_project where projcode='" + pi.projcode + "'";
                    string Tdepartcode = dl.ExecuteScalar(Tsql).ToString();
                    if (pt.targetdepart.Equals(Tdepartcode))
                    {
                        dl.ChangeProjectFlowNode(pt);  //改变案件流程
                        string sSQL = "select detailedclass,departTime,departTimeType from b_project where projcode='" + pi.projcode + "'";
                        DataSet ds = dl.ExecuteDataset(sSQL);
                        string cdetailedclass = ds.Tables[0].Rows[0]["detailedclass"].ToString();
                        string cdepartTime = ds.Tables[0].Rows[0]["departTime"].ToString();
                        string cdepartTimeType = ds.Tables[0].Rows[0]["departTimeType"].ToString();
                        if (cdetailedclass == pi.detailedclass && cdepartTime == pi.departTime && cdepartTimeType == pi.departTimeType)
                        {

                        }
                        else
                        {
                            dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                            string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                            string nodeid = dl.ExecuteScalar(strSQL).ToString();
                            if (nodeid == "8")
                            {
                                dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                            }
                        }
                    }
                    else
                    {
                        // dl.ChangeProcessType(pi.ProcessType, pi.projcode);
                        dl.ChangeDetailedclass(pi.detailedclass, pi.projcode, pi.departTime, pi.departTimeType, pi.isgreat, pi.ProcessType);
                        dl.ChangeProjectFlowNode(pt);  //改变案件流程
                        string strSQL = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                        string nodeid = dl.ExecuteScalar(strSQL).ToString();
                        if (nodeid == "8")
                        {
                            dl.ChangeTraceTime(pi.projcode, pi.departTime, pi.departTimeType);
                        }
                    }*/
                    string Tsql = "select isnull(dodepartcode,targetdepartcode) as targetdepartcode from b_project where projcode='" + pt.projcode + "'";
                    string Tdepartcode = dl.ExecuteScalar(Tsql).ToString();
                    if (pt.targetdepart.Equals(Tdepartcode))
                    {
                        string strSQL = string.Format(@"	update b_project 
	                                                    set TargetDepartCode = '{0}',
                                                            DoDepartCode = null
	                                                    where projcode={1}", TargetDepartCode, pt.projcode);
                        dl.ExecuteNonQuery(strSQL);

                        dl.ChangeProjectFlowNode(pt);  //改变案件流程

                    }
                    else
                    {
                        string strSQL = string.Format(@"	update b_project 
	                                                    set TargetDepartCode = '{0}',
                                                            DoDepartCode = null
	                                                    where projcode={1}", TargetDepartCode, pt.projcode);
                        dl.ExecuteNonQuery(strSQL);

                        dl.ChangeProjectFlowNode(pt);  //改变案件流程
                        string strSQL1 = "select nodeid from b_project where projcode='" + pt.projcode + "'";
                        string nodeid = dl.ExecuteScalar(strSQL1).ToString();
                        if (nodeid == "8")
                        {
                            string strGetTime = "select departTime from b_project where projcode='" + pt.projcode + "'";
                            string strGetTimeType = "select departTimeType from b_project where projcode='" + pt.projcode + "'";
                            string departTime = dl.ExecuteScalar(strGetTime).ToString();
                            string departTimeType = dl.ExecuteScalar(strGetTimeType).ToString();
                            dl.ChangeTraceTime(pt.projcode, departTime, departTimeType);
                        }
                    }
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion
        #region Project_DepartPQ：案件派遣


        public static void Project_DepartPQ(string TargetDepartCode, string projcode, out string ErrMsg, ProjectTraceInfo pt)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();



                    string strSQL = string.Format(@"	update b_project 
	                                                    set ChildDepartCode = '{0}',
                                                            DoDepartCode = null
	                                                    where projcode={1}", TargetDepartCode, projcode);
                    dl.ExecuteNonQuery(strSQL);

                    //插入职能部门案件分派流程
                    strSQL = string.Format(@" 

 
                    insert into dbo.b_project_trace_depart(
                        projcode,actionname,cu_date,
                        usercode,DepartCode,
                        _opinion) 
                    values(@projcode,@actionname,GetDate(),
                        @usercode,@DepartCode,
                        @_opinion)");

                    SqlParameter[] arrSP = new SqlParameter[]{
                    new SqlParameter("@projcode",pt.projcode),
                    new SqlParameter("@actionname",pt.actionname),
                    new SqlParameter("@usercode",pt.usercode),
                    new SqlParameter("@DepartCode",pt.DepartCode),
                    new SqlParameter("@_opinion",pt._opinion)};

                    dl.ExecuteNonQuery(strSQL, arrSP);

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion


        #region Project_DBMPQ：案件派遣

        /// <summary>
        /// 多部门派遣
        /// </summary>
        /// <param name="Leader">督办人名字</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_DBMPQ(string projcode, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.InsertProjectManyDept(projcode);  //多部门派遣
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_PQ_ToSJ：案件派遣到市局职能部门
        /// <summary>
        /// 案件派遣到市局职能部门
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_PQ_ToSJ(string projcode, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    SqlParameter[] arrSP = new SqlParameter[] { 
                        new SqlParameter("@projcode", projcode)
                    };
                    dl.ExecuteNonQuery("pr_b_PQ_ToSJ", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_FK：案件处理结果反馈

        /// <summary>
        /// 案件处理结果反馈
        /// </summary>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_FK(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    string strSQL = string.Format(@"
                                select UserDefinedCode
		                        from dbo.p_depart
		                        where departcode = {0}", pt.DepartCode);
                    string DoDepartCode = dl.ExecuteScalar(strSQL).ToString();

                    //更改处理部门
                    string sql1 = "select dodepartcode from b_project where projcode='" + pt.projcode + "'";
                    string dodepartcode = dl.ExecuteScalar(sql1).ToString();
                    string sql = "select ChildDepartCode from b_project where projcode='" + pt.projcode + "'";
                    string childdepartcode = dl.ExecuteScalar(sql).ToString();
                    if (string.IsNullOrEmpty(dodepartcode) == true)
                    {
                        strSQL = string.Format(@"	update b_project 
	                                                    set DoDepartCode = '{0}'
	                                                    where projcode={1}", DoDepartCode, pt.projcode);
                        dl.ExecuteNonQuery(strSQL);
                    }

                    if (childdepartcode != "")
                    {
                        dl.ChangeChildDepartCode(pt.projcode);
                        //add by yaoch 2012-10-22
                        dl.ChangeChildReturnStatus(pt.projcode);
                        //插入职能部门反馈流程
                        strSQL = string.Format(@" 

 
                    insert into dbo.b_project_trace_depart(
                        projcode,actionname,cu_date,
                        usercode,DepartCode,
                        _opinion) 
                    values(@projcode,@actionname,GetDate(),
                        @usercode,(select UserDefinedCode from p_depart where departcode=@DepartCode),
                        @_opinion)");

                        SqlParameter[] arrSP = new SqlParameter[]{
                    new SqlParameter("@projcode",pt.projcode),
                    new SqlParameter("@actionname",pt.actionname),
                    new SqlParameter("@usercode",pt.usercode),
                    new SqlParameter("@DepartCode",pt.DepartCode),
                    new SqlParameter("@_opinion",pt._opinion)};

                        dl.ExecuteNonQuery(strSQL, arrSP);

                    }
                    else
                    {
                        dl.ChangeProjectFlowNode(pt);
                    }
                    SqlParameter[] arrSP1 = new SqlParameter[]{
                        new SqlParameter("@projcode",pt.projcode)
                    };
                    dl.ExecuteNonQuery("pr_b_SetIsTimeOut", CommandType.StoredProcedure, arrSP1);

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetMobile：根据案卷号，获取最后一个处理部门的手机号码
        /// <summary>
        /// 根据案卷号，获取最后一个处理部门的手机号码
        /// </summary>
        /// <param name="pt">案件流程结构体</param>
        /// <returns></returns>
        public static string GetMobile(ProjectTraceInfo pt)
        {
            string mobile = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    //-------------------1、根据案卷号，获取最后一个处理部门的手机号码-------------------
                    string strSQL = string.Format(@"	select Mobile from p_depart
                                                                        where UserDefinedCode=(
                                                                        select TargetDepartCode from b_project 
                                                                        where projcode = {0} ) ", pt.projcode);

                    object oResult = dl.ExecuteScalar(strSQL);
                    mobile = oResult == null ? "" : oResult.ToString();
                }
                catch (Exception e)
                {
                    mobile = "";
                }
            }
            return mobile;
        }
        #endregion

        #region Project_BTG：核查不通过
        /// <summary>
        /// 核查不通过
        /// </summary>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_BTG(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    //开启事务

                    dl.BeginTrans();

                    //-------------------1、更改处理部门-------------------2008-10-21 ycg edit
                    //                    string strSQL = string.Format(@"	update b_project 
                    //	                                                    set TargetDepartCode = null,
                    //                                                            DoDepartCode = null
                    //	                                                    where projcode={0}", pt.projcode);
                    //                    dl.ExecuteNonQuery(strSQL);

                    //-------------------2、接线员核查不属实时，需要重置PDA消息表-------------------
                    string strSQL1 = string.Format(@"select 
                    fgDepart from b_project where projcode={0}", pt.projcode);
                    string fgDepart = dl.ExecuteScalar(strSQL1).ToString();
                    string strSQL2 = string.Format(@"select
                    isnull(dodepartcode,targetdepartcode) as dodepartcode from b_project
                    where projcode={0}", pt.projcode);
                    string dodepartcode = dl.ExecuteScalar(strSQL2).ToString();
                    string UpSQL = "";
                    if (string.IsNullOrEmpty(fgDepart) == true)
                    {
                        UpSQL = "update b_project set fgDepart=isnull(dodepartcode,targetdepartcode),fgs=1 where projcode='" + pt.projcode + "'";

                    }
                    else
                    {
                        if (fgDepart == dodepartcode)
                        {
                            UpSQL = "update b_project set fgs=fgs+1 where projcode='" + pt.projcode + "'";
                        }
                        else
                        {
                            UpSQL = "update b_project set fgDepart=isnull(dodepartcode,targetdepartcode),fgs=1 where projcode='" + pt.projcode + "'";
                        }
                    }
                    dl.ExecuteNonQuery(UpSQL);
                    string strSQL = string.Format(@"	update b_pdamsg
	                                            set ioflag = null,
                                                    state = null
	                                            where projcode={0}", pt.projcode);
                    dl.ExecuteNonQuery(strSQL);

                    //-------------------3、改变案件流程-------------------
                    dl.ChangeProjectFlowNode(pt);

                    //递交事务
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_JA：结案

        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_JA(string strYear, ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    string UpSql = "update b_project set ffs='" + pt.ffs + "' where projcode='" + pt.projcode + "'";
                    dl.ExecuteNonQuery(UpSql);
                    //----------更改专业部门处理是否超期标示----------
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",pt.projcode)
                    };
                    dl.ExecuteNonQuery("pr_b_SetIsTimeOut", CommandType.StoredProcedure, arrSP);
                    SqlParameter[] arrSP1 = new SqlParameter[]{
                        new SqlParameter("@projcode",pt.projcode)
                    };
                    dl.ExecuteNonQuery("pr_b_SetHowManyTime", CommandType.StoredProcedure, arrSP1);
                    //----------改变案件流程----------
                    dl.ChangeProjectFlowNode(pt);

                    //----------把结案案件记录移到结案历史数据库----------
                    dl.MoveDataToHistoryDB(pt.projcode, strYear);

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_JA_ToSJ：结案后，从宝安交换到市局去

        /// <summary>
        /// 与市局发生过交换关系的案件，在结案后，从宝安交换到市局去。

        /// </summary>
        /// <param name="strProjCode">案件编号</param>
        /// <param name="strYear">案件年份</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_JA_ToSJ(string strProjCode, string strYear, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {

                    System.Data.SqlClient.SqlParameter[] arrSP = new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@projcode", strProjCode),
                        new System.Data.SqlClient.SqlParameter("@Year", strYear),
                    };
                    dl.ExecuteNonQuery("pr_b_JA_ToSJ", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region Project_JAHT：结案回退
        /// <summary>
        /// 结案回退
        /// </summary>
        /// <param name="pt">案件流程结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void Project_JAHT(ProjectTraceInfo pt, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();

                    //----------更改PDA发送标志----------
                    string strSQL = string.Format("update b_pdamsg set ioflag = '0',state = '2' where projcode = {0}", pt.projcode);
                    dl.ExecuteNonQuery(strSQL);

                    //----------改变案件流程----------
                    dl.ChangeProjectFlowNode(pt);

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetDBProjectList：获取督办案件列表

        /// <summary>
        /// 获取督办案件列表
        /// </summary>
        /// <param name="Leader">督办人</param>
        /// <param name="projcode">案件编号</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="areacode">区</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetDBProjectList(string Leader, string projcode, string times1, string times2, string areacode, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDBProjectList(Leader, projcode, times1, times2, areacode, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion
        #region GetGZProjectList：获取挂账案卷

        /// <summary>
        /// 获取挂账案卷
        /// </summary>
        /// <param name="Leader">督办人</param>
        /// <param name="projcode">案件编号</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="areacode">区</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetGZProjectList(string Leader, string projcode, string times1, string times2, string areacode, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetGZProjectList(Leader, projcode, times1, times2, areacode, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDBProjectInfo:绑定督办信息
        /// <summary>
        /// 绑定督办信息
        /// </summary>
        /// <param name="projcode"></param>
        public static DataSet GetDBProjectInfo(string projcode, ref string ErrMsg)
        {
            try
            {
                string strSQL = string.Format(@"select  B.UserName AS 经办人,C.departname as 受理部门,
                                                        A.Leader AS 督办人, A.content AS 督办批示,
		                                                convert(VARCHAR(19),A.cudate,120) AS 督办时间
                                                from b_project_inspect a,dbo.p_user b,dbo.p_depart c
                                                where a.usercode = b.usercode  
	                                                AND b.departcode = c.departcode
	                                                AND A.projcode = {0}", projcode);
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.ExecuteDataset(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDBProjectEditEnable:判断该案件是否可以进行督办编辑

        /// <summary>
        /// 判断该案件是否可以进行督办编辑。在值班长的督办栏需要使用该方法。

        /// </summary>
        /// <param name="projcode">案件编号</param>
        [AjaxPro.AjaxMethod]
        public static string GetDBProjectEditEnable(string projcode)
        {
            try
            {
                string strSQL = string.Format(@"	if exists(
		                                                    select 1
		                                                    from b_project 
		                                                    where  projcode = {0}
			                                                    and IsManual = 1
			                                                    and ispress =1 )
		                                                    select 1
	                                                else
		                                                    select 0", projcode);
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.ExecuteScalar(strSQL).ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region  InsertProject_DB： 督办登记时，插入新案件

        /// <summary>
        /// 插入案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static string InsertProject_DB(ProjectInfo prj, ProjectSourceInfo psi, ProjectTraceInfo pt,
                    string Leader, string content, out string ErrMsg)
        {
            ErrMsg = "";
            string projcode = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();

                    projcode = dl.InsertProject(prj, psi);  //插入新案件

                    dl.DB_InsertInspectInfo(projcode, Leader, content, psi.accept); //插入督办流程

                    pt.projcode = projcode;
                    dl.InsertProjTrace(pt); //插入案件流程

                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
            return projcode;
        }
        #endregion

        #region  UpdateProject：督办登记时，修改案件

        /// <summary>
        /// 督办登记时，修改案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public static void UpdateProject_DB(ProjectInfo prj, ProjectSourceInfo psi,
            string Leader, string content, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.UpdateProject(prj, psi);
                    dl.DB_InsertInspectInfo(prj.projcode, Leader, content, psi.accept); //插入督办流程
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region  CancelProject： 根据案件编号，问题注销
        /// <summary>
        /// 根据案件编号，问题注销（打删除标示，移入历史备份库）

        /// </summary>
        /// <param name="projcode">案卷编号</param>

        public static string CancelProject(ProjectTraceInfo pt, ref string ErrMsg)
        {
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();

                    //----------插入案件流程----------
                    dl.InsertProjTrace(pt);

                    //----------打删除标示----------
                    dl.CancelProject(pt.projcode);

                    //----------移入历史备份库----------
                    dl.MoveDataToHistoryDB(pt.projcode, null);

                    dl.Commit();

                    return "1";
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;

                    return string.Format("0${0}", ErrMsg);
                }
            }
        }
        #endregion

        #region  DeleteGDProject： 根据案件编号，删除归档的案件信息
        /// <summary>
        /// 根据案件编号，删除归档的案件信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void DeleteGDProject(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.DeleteGDProject(projcode);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                    dl.Rollback();
                }
            }
        }
        #endregion

        #region  RollBackGDProject： 根据案件编号，还原归档的案件
        /// <summary>
        /// 根据案件编号，还原归档的案件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static void RollBackGDProject(ProjectTraceInfo pt, string startdate, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {

                    dl.BeginTrans();
                    dl.RollBackGDProject(pt.projcode, startdate);
                    dl.InsertProjTrace(pt);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                    dl.Rollback();
                }
            }
        }
        #endregion

        #region GetFeedBackPhone：根据案件编号，获取需要反馈的电话号码
        /// <summary>
        /// 根据案件编号，获取需要反馈的电话号码
        /// </summary>
        /// <param name="projcode">案件编码</param>
        /// <param name="strYear">案件发生年</param>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetFeedBackPhone(string projcode, string strYear, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetFeedBackPhone(projcode, strYear, IsEnd);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region FeedBack：把用户的反馈信息插入到表中
        /// <summary>
        ///把用户的反馈信息插入到表中

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年</param>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <param name="Feedbacker">反馈用户</param>
        /// <param name="FeedbackMode">反馈类型</param>
        /// <param name="FeedbackTarget">反馈对象</param>
        /// <param name="FeedbackTargetMobile">反馈号码</param>
        /// <param name="FeedbackContent">反馈内容</param>
        /// <param name="ErrMsg">输出错误信息</param>
        public static void FeedBack(string projcode, string strYear, string IsEnd,
                                    string Feedbacker, string FeedbackMode, string FeedbackTarget,
                                    string FeedbackTargetMobile, string FeedbackContent, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    dl.BeginTrans();
                    dl.FeedBack(projcode, strYear, IsEnd,
                                  Feedbacker, FeedbackMode, FeedbackTarget,
                                  FeedbackTargetMobile, FeedbackContent);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetFeedBackList:根据案件编码，获取案件反馈列表

        /// <summary>
        /// 根据案件编码，获取案件反馈列表

        /// </summary>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="startYear">案卷开始日期</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetFeedBackList(string projcode, string IsEnd, string startYear, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetFeedBackList(projcode, IsEnd, startYear);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetBigClassList:获取大类事部件列表

        /// <summary>
        /// 获取大类事部件列表

        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetBigClassList(string typeCode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetBigClassList(typeCode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallClassList:获取小类事部件列表

        /// <summary>
        /// 获取小类事部件列表

        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="bigclassCode">大类事部件编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetSmallClassList(string typeCode, string bigclassCode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetSmallClassList(typeCode, bigclassCode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region  GetDutyDepart：  获取事部件的责任单位
        /// <summary>
        ///   获取事部件的责任单位 
        /// </summary>
        /// <param name="area">区</param>
        /// <param name="typecode">事部件标示</param>
        /// <param name="Smallclass">小类</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetDutyDepart(string area, string typecode, string Smallclass, string strGreat, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@area",area),
                        new SqlParameter("@typecode",typecode),
                        new SqlParameter("@Smallclass",Smallclass),
                        new SqlParameter("@strGreat",strGreat)
                    };

                    return dl.ExecuteDataset("pr_b_GetDutyDepart", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetAreaList:根据市区编码,获取区域列表
        /// <summary>
        /// 根据区域编码,获取区域列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetAreatList(string areacode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetAreaList(areacode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetStreetList:根据区编码,获取街道列表
        /// <summary>
        /// 根据区域编码,获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetStreetList(string areacode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetStreetList(areacode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetCommList:根据街道编码,获取社区列表
        /// <summary>
        /// 根据街道编码,获取社区列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetCommList(string areacode, string streetcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetCommList(areacode, streetcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartList:根据区域编码，获取部门列表

        /// <summary>
        /// 根据区域编码，获取部门列表

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetDepartList(string areacode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDepartList(areacode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据区域编码，获取部门列表

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="parentcode">是否要查询父级列</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetDepartList(string areacode, bool IsParentcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDepartList(areacode, IsParentcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据区域编码/部门名称，获取部门列表

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="departname">部门名称</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetDepartList(string areacode, string departname, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string strSQL = string.Format(@"
                    select departcode,UserDefinedCode,
                        departname,
                        UserDefinedCode+'$'+ISNULL(Mobile,'')+'$'+CAST(ISNULL(IsAcceptNote,0) AS VARCHAR(1))+'$'+ISNULL(SJ_RoleCode,'') as UserMobile
		            from dbo.p_depart
		            where area like '{0}%' 
			            and departname like '%{1}%'
                        and IsDuty=1
                        and isnull(IsDel,0) = 0 
                    order by area,UserDefinedCode", areacode, departname);
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.ExecuteDataset(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetTelephonistList:根据区域编码，获取接线员列表
        /// <summary>
        /// 根据区域编码，获取接线员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetTelephonistList(string areacode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetTelephonistList(areacode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetCollecterList:根据街道编码和社区编码，获取监督员的列表
        /// <summary>
        /// 根据街道编码和社区编码，获取监督员的列表
        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetCollecterList(string areacode, string streetcode, string commcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetCollecterList(areacode, streetcode, commcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetCollCodeByName:根据街道编码和社区编码和监督员姓名，获取监督员编号

        /// <summary>
        /// 根据街道编码和社区编码和监督员姓名，获取监督员编号

        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <param name="collname">监督员姓名</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataTable GetCollCodeByName(string areacode, string streetcode, string squarecode, string collname, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {

                    return dl.GetCollCodeByName(areacode, streetcode, squarecode, collname).Tables[0];
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetQueryProList:获取查询箱的案件的列表

        /// <summary>
        /// 获取查询箱的案件的列表

        /// </summary>
        /// <param name="probsource">案卷来源</param>
        /// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        /// <param name="probclass">案卷类型（部件，事件）</param>
        /// <param name="bigclass">大类编码</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="street">街道编码</param>
        /// <param name="square">社区编码</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="address">地址</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="collcode">监督员编号</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataTable GetQueryProList(string probsource, string state, string probclass,
                                                string bigclass, string smallclass, string street,
                                                string square, string times1, string times2,
                                                string projcode, string address, string areacode,
                                                string collcode, string telephonistcode,
                                                string departcode, string projectkind,
                                                PageInfo page, string times3, string times4, string deleted, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {

                    return dl.GetQueryProList(probsource, state, probclass,
                                                bigclass, smallclass, street,
                                                square, times1, times2,
                                                projcode, address, areacode,
                                                collcode, telephonistcode, departcode, projectkind,
                                                page, times3, times4, deleted, "");
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetQueryProListerqi:获取查询箱的案件的列表二期

        /// <summary>
        /// 获取查询箱的案件的列表

        /// </summary>
        /// <param name="probsource">案卷来源</param>
        /// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        /// <param name="probclass">案卷类型（部件，事件）</param>
        /// <param name="bigclass">大类编码</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="street">街道编码</param>
        /// <param name="square">社区编码</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="address">地址</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="collcode">监督员编号</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataTable GetQueryProListerqi(string probsource, string state, string probclass,
                                                string bigclass, string smallclass, string street,
                                                string square, string times1, string times2,
                                                string projcode, string address, string areacode,
                                                string collcode, string telephonistcode,
                                                string departcode, string projectkind,
                                                PageInfo page, string times3, string times4, string deleted, string trace, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {

                    return dl.GetQueryProListerqi(probsource, state, probclass,
                                                bigclass, smallclass, street,
                                                square, times1, times2,
                                                projcode, address, areacode,
                                                collcode, telephonistcode, departcode, projectkind,
                                                page, times3, times4, deleted, trace, "");
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        #endregion

        #region InitDepart：初始化部门以及其子部门
        /// <summary>
        /// 初始化部门以及其子部门

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="departcode">部门编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public static DataSet InitDepart(string areacode, string departcode, out string strErr)
        {
            strErr = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.InitDepart(areacode, departcode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartProjectList：根据部门用户编码，获取改部门处理过的案卷列表

        /// <summary>
        /// 根据部门用户编码，获取改部门处理过的案卷列表
        /// </summary>
        /// <param name="userDefinedCode">部门用户定义编码</param>
        /// <param name="projectstate">案卷类型（已处理（已结案），已处理（未结案），正在处理）</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetDepartProjectList(string userDefinedCode, string projectstate, string times1,
                                                                       string times2, PageInfo pgInfo, out string strErr)
        {
            strErr = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetDepartProjectList(userDefinedCode, projectstate, times1,
                                                    times2, pgInfo);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region SetProjIsProcess：专业部门处理阶段，用来标识案件的处理状态

        /// <summary>
        /// 专业部门处理阶段，用来标识案件的处理状态

        /// </summary>
        /// <param name="projcode">案卷编码</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string SetProjIsProcess(string projcode)
        {
            try
            {
                string strSQL = string.Format(@"	update b_project 
													set isProcess = Case When IsNull(isProcess,0)=0 then 1 else 0 end
													where ProjCode = '{0}' ", projcode);
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dl.ExecuteNonQuery(strSQL);
                }
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region JudgeIsSended:判断是否已经发送过请求处理时限类型
        /// <summary>
        /// 判断是否已经发送过请求处理时限类型
        /// </summary>
        /// <param name="projcode">案件编号</param>
        public static string JudgeIsSended(string projcode)
        {
            string strSQL = string.Format(@"	select IsNull(cast(ProcessType as varchar(10)),'')+','+IsNull(cast(RequestProcessType as varchar(10)),'')
											    from b_project 
											    where projcode = {0} ", projcode);
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    object oResult = dl.ExecuteScalar(strSQL);
                    return oResult != null ? oResult.ToString() : ",";
                }
            }
            catch
            {
                return ",";
            }
        }
        #endregion

        #region ChangeProcessType：申请改变案件的处理类型
        /// <summary>
        /// 申请改变案件的处理类型

        /// 部件：  0：表示一般性处理 1：工程性处理 2：特殊性处理 ；

        /// 事件：  0：表示一般性处理 1：综合性处理

        /// </summary>
        /// <param name="pt">案件流程</param>
        /// <param name="RequestProcessType">请求更改类型</param>
        /// <param name="ErrMsg"></param>
        public static void ChangeProcessType(ProjectTraceInfo pt, string RequestProcessType, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    string strSQL = string.Format(@"    update b_project 
                                                        set RequestProcessType = {0}
										                where projcode = {1} ", RequestProcessType, pt.projcode);

                    dl.BeginTrans();
                    dl.ExecuteNonQuery(strSQL);     //更改处理类型
                    dl.InsertProjTrace(pt);         //插入案件流程
                    //dl.UnLockProject(pt.projcode);  //解锁
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region ResponseProcessType：响应改变案件的处理类型
        /// <summary>
        /// 响应改变案件的处理类型

        /// 部件：  0：表示一般性处理 1：工程性处理 2：特殊性处理 ；

        /// 事件：  0：表示一般性处理 1：综合性处理

        /// </summary>
        /// <param name="pt">案件流程</param>
        /// <param name="RequestProcessType">请求更改类型</param>
        /// <param name="IsAgree">标识是否同意审批。true：表示同意；false：表示不同意</param>
        /// <param name="ErrMsg"></param>
        public static void ResponseProcessType(ProjectTraceInfo pt, string RequestProcessType, bool IsAgree, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    string strSQL = "";
                    if (IsAgree)
                    {
                        strSQL = string.Format(@"    update b_project 
                                                        set ProcessType = RequestProcessType,
													        RequestProcessType = null
										                where projcode = {0} ", pt.projcode);
                    }
                    else
                    {
                        strSQL = string.Format(@"    update b_project 
                                                        set RequestProcessType = null
										                where projcode = {0} ", pt.projcode);
                    }

                    dl.BeginTrans();
                    dl.ExecuteNonQuery(strSQL);     //更改处理类型
                    dl.InsertProjTrace(pt);         //插入案件流程
                    dl.UnLockProject(pt.projcode);  //解锁
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region ResponseProcessType_ToSJ：把响应申请更改处理类型的处理流程交换到市局
        /// <summary>
        ///     由宝安派遣到市局职能部门的案件，当市局职能部门申请更改处理类型，宝安区指挥中心对申请进行审批。

        /// 审批之后，把处理流程交换到市局。

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="ErrMsg"></param>
        public static void ResponseProcessType_ToSJ(string projcode, out string ErrMsg)
        {
            ErrMsg = "";

            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    System.Data.SqlClient.SqlParameter[] arrSP = new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@projcode", projcode)
                    };
                    dl.ExecuteNonQuery("pr_b_Response_ToSJ", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region GetDealProjectList：获取待反馈案件列表
        /// <summary>
        /// 获取待反馈案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="strPdaIoFlag">核查状态</param>
        /// <param name="DateStart">查询时间，从：</param>
        /// <param name="DateEnd">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet WaitFeedBackList(ProjectInfo prj, string DateStart, string DateEnd, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@RowCount",SqlDbType.Int),
                        new SqlParameter("@PageCount",SqlDbType.Int),
                        new SqlParameter("@StreetId",prj.street),
                        new SqlParameter("@FeedState",prj.PdaIoFlag), //反馈状态

                        new SqlParameter("@projcode",prj.projcode),
                        new SqlParameter("@DateStart",DateStart),
                        new SqlParameter("@DateEnd",DateEnd),    
                        new SqlParameter("@CurrentPage",page.CurrentPage),
                        new SqlParameter("@PageSize",page.PageSize),
                        new SqlParameter("@Order",page.Order),
                        new SqlParameter("@Field",page.Field),
                        new SqlParameter("@Telephonist",prj.Telephonist)
                    };
                    arrSP[0].Direction = ParameterDirection.Output;
                    arrSP[1].Direction = ParameterDirection.Output;

                    DataSet ds = dl.ExecuteDataset("pr_b_WaitFeedBackList", CommandType.StoredProcedure, arrSP);
                    page.RowCount = arrSP[0].Value.ToString();
                    page.PageCount = arrSP[1].Value.ToString();

                    return ds;
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetFlowNodePower：获取所选定角色的可控制模块和具体操作动作

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作

        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public static DataSet GetFlowNodePower(string roleID, string ModelCode, string strBigModel, string strErr)
        {
            strErr = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@rolecode",roleID),
                        new SqlParameter ("@modelcode",ModelCode),
                        new SqlParameter ("@strBigModel",strBigModel)
                    };

                    return dl.ExecuteDataset("pr_p_GetFlowNodePower", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    strErr = e.Message;
                    return null;

                }
            }

        }
        #endregion

        #region GetFlowNodePower_zhifa：获取执法模块所选定角色的可控制模块和具体操作动作

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作

        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public static DataSet GetFlowNodePower_zhifa(string roleID, string ModelCode, string strBigModel, string strErr)
        {
            strErr = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@rolecode",roleID),
                        new SqlParameter ("@modelcode",ModelCode),
                        new SqlParameter ("@strBigModel",strBigModel)
                    };

                    return dl.ExecuteDataset("pr_p_GetFlowNodePower_zhifa", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    strErr = e.Message;
                    return null;

                }
            }

        }
        #endregion

        #region GetDataGridTier：根据按纽显示对应的数据列

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作

        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public static DataSet GetDataGridTier(string buttoncode, out string strErr)
        {
            strErr = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@buttoncode",buttoncode)
                    };

                    return dl.ExecuteDataset("pr_p_GetDataGridTier", CommandType.StoredProcedure, arrSP);
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    strErr = e.Message;
                    return null;

                }
            }

        }
        #endregion

        #region 根据外部案卷ID查找本系统ID，案卷名等信息

        public static ProjectInfo GetProjectInfoByOutCode(string code)
        {
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                return dl.GetProjectInfoByOutCode(code);

            }
        }
        #endregion

        // add by yaoch 2012-10-29
        #region 根据案件编号获取二级案件的编号

        public static String GetProjectCode2ByCode(string code)
        {
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                return dl.GetProjectCode2ByCode(code);

            }
        }
        #endregion


        #region 领导通案卷接口
        /// <summary>
        /// 查询违法案卷
        /// </summary>
        /// 《<param name="laTime">立案时间</param>
        /// <param name="address">案卷地址</param>
        /// <param name="desctribe">案卷详细描述</param>
        /// <param name="page">分页信息</param>
        /// <param name="startTime">上报时间开始</param>
        /// <param name="endTime">善报时间结束</param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static DataTable GetLProject(PageInfo page, string usercode,string laTime, string address, string desctribe, string startTime, string endTime, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProject(page,usercode, laTime, address, desctribe, startTime, endTime);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 统计违法案卷数量
        /// </summary>
        /// <param name="strattime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="ErrMsg">错误信息</param>
        /// <returns></returns>
        public static int GetLProjectCount(string strattime, string endtime, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProjectCount(strattime, endtime);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return 0;
            }
        }

        public static DataTable GetLProjectCountList(string strattime, string endtime, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProjectCountList(strattime, endtime);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 查询上报案卷用户排名
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static DataTable GetLProjectRank(string starttime, string endtime, string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProjectRank(starttime, endtime);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        public static DataTable GetLProjectRank(string starttime, string endtime, string username,string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProjectRank(starttime, endtime, username);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        public static DataTable GetLProjectDetail(string projcode, string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetLProjectDetail(projcode);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 新增领导批示内容
        /// </summary>
        /// <param name="projectcode">案卷编号</param>
        /// <param name="content">批示内容</param>
        /// <param name="usercode">用户id</param>
        /// <param name="username">用户名称</param>
        /// <param name="state">批示状态</param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static int AddProjectSupervise(string projectcode, string content, string usercode, string username, string state, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.AddProjectSupervise(projectcode, content, usercode, username, state);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 修改案卷批示
        /// </summary>
        /// <param name="id">批示Id</param>
        /// <param name="content">批示内容</param>
        /// <param name="state">批示状态</param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static int UpdateProjectSupervise(string id, string content, string state, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.UpdateProjectSupervise(id, content, state);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 获取案卷批示列表
        /// </summary>
        /// <param name="procode">案卷列表</param>
        /// <param name="state">批示状态</param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public static DataTable GetProjectSupervise(string procode, string state, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    return dl.GetProjectSupervise(procode, state);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        #endregion
    }
}