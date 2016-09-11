/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：案件业务受理数据层访问类。

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
using System.Collections;
using System.Data.SqlClient;

namespace bacgDL.business
{
    ///// <summary>
    ///// 案件信息结构体

    ///// </summary>
    //class ProjectInfo
    //{
    //    public string actioncode = "";
    //    public string step = "";
    //    public string areacode = "";
    //    public string role = "";
    //    public string projcode = "";
    //}

    ///// <summary>
    ///// 分页结构体

    ///// </summary>
    //class PageInfo
    //{
    //    /// <summary>
    //    /// 当前页码
    //    /// </summary>
    //    public string CurrentPage = "";
    //    /// <summary>
    //    /// 总行数

    //    /// </summary>
    //    public string RowCount = "";
    //    /// <summary>
    //    /// 总页数

    //    /// </summary>
    //    public string PageCount = "";
    //    /// <summary>
    //    /// 页码大小
    //    /// </summary>
    //    public string Size = "";
    //    /// <summary>
    //    /// 关键字段
    //    /// </summary>
    //    public string Field = "";
    //    /// <summary>
    //    /// 排序字段
    //    /// </summary>
    //    public string Order = "";
    //}


    public class Message : Teamax.Common.CommonDatabase
    {
        #region IsNewMessage：判断是否有新消息

        /// <summary>
        /// 判断是否有新消息
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <param name="strAreaCode"></param>
        /// <returns></returns>
        public string IsNewMessage(string strUserCode, string strAreaCode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@usercode", strUserCode),
                new SqlParameter("@areacode", strAreaCode),
                new SqlParameter("@ret",SqlDbType.Int)
            };
            spInputs[2].Direction = ParameterDirection.ReturnValue;

            this.ExecuteNonQuery("pr_b_IsNewMessage", CommandType.StoredProcedure, spInputs);
            return spInputs[2].Value.ToString();
        }
        #endregion

        #region IsNewProject：判断是否有新案件

        /// <summary>
        /// 判断是否有新案件
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <param name="strAreaCode"></param>
        /// <returns></returns>
        public string IsNewProject(string strStepId, string strAreaCode, string strDepartCode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@stepid", strStepId),
                new SqlParameter("@areacode", strAreaCode),
                new SqlParameter("@DepartCode", strDepartCode)
            };

            return this.ExecuteScalar("pr_b_IsNewProject", CommandType.StoredProcedure, spInputs).ToString();
        }

        public string LIsNewProject(string strStepId, string strAreaCode, string strDepartCode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@stepid", strStepId),
                new SqlParameter("@areacode", strAreaCode),
                new SqlParameter("@DepartCode", strDepartCode)
            };

            return this.ExecuteScalar("pr_b_LIsNewProject", CommandType.StoredProcedure, spInputs).ToString();
        }
        #endregion

        #region SendMessage:发送短信

        /// <summary>
        /// 发送短信

        /// </summary>
        /// <param name="srvno"></param>
        /// <param name="desmp">目的号码</param>
        /// <param name="msg">消息内容</param>
        public void SendMessage(string srvno, string desmp, string msg)
        {
            SqlParameter[] arrSPP=new SqlParameter[]{
                            new SqlParameter("@srvno",srvno),
                            new SqlParameter("@desmp",desmp),
                            new SqlParameter("@msg",msg)};
            this.ExecuteNonQuery("msg_remotelogin",CommandType.StoredProcedure,arrSPP);
        }
        #endregion

        #region GetProjNum:获取立案数，结案数，正在处理数，监督员在岗人数等信息
        /// <summary>
        /// 获取立案数，结案数，正在处理数，监督员在岗人数等信息
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetProjNum(string areacode)
        {
            SqlParameter[] arrSPP = new SqlParameter[]{
                            new SqlParameter("@areacode",areacode)};
            return this.ExecuteDataset("pr_b_GetProjNumber", CommandType.StoredProcedure, arrSPP);
        }
        #endregion

        #region GetMsgList:获取消息列表
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public DataSet GetMsgList(int usercode,int curentpage, int pagesize,string Order, string Field,string username,string begintime,string endtime,
                                        ref int rowCount,ref int pageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserCode",usercode),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@UserName",username),
                                new SqlParameter("@BeginTime",begintime),
                                new SqlParameter("@EndTime",endtime)};
            arrSP[2].Direction = ParameterDirection.Output;
            arrSP[3].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetMsgList", CommandType.StoredProcedure,arrSP);
            rowCount = arrSP[2].Value.ToString() == "" ? 0 : int.Parse(arrSP[2].Value.ToString());
            pageCount = arrSP[3].Value.ToString() == "" ? 0 : int.Parse(arrSP[3].Value.ToString());
            return ds;

        }
        #endregion

        #region DeleteMsg:删除消息
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息id</param>
        /// <returns></returns>
        public void DeleteMsg(string id)
        {
           string strSQL=string.Format(@"delete from s_message 
                                            where id = '{0}'",id);
           this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region GetMsgInfo:获取消息信息
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="id">消息编码</param>
        /// <returns></returns>
        public DataSet GetMsgInfo(string id)
        {
            string strSQL = string.Format(@"SELECT a.*,b.username,c.departname
                                            FROM s_message AS a, p_user AS b, p_depart AS c 
                                            WHERE a.go_user = b.usercode and b.departcode=c.departcode and a.id='{0}'", id);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region SetIsRead:设置已经读取了消息

        /// <summary>
        /// 设置已经读取了消息

        /// </summary>
        /// <param name="id">消息编码</param>
        /// <returns></returns>
        public int SetIsRead(string id)
        {
            string strSQL = string.Format(@"update s_message set isread=1
                                            where id='{0}'", id);
            return this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region GetPDAMsgList:获取PDA消息列表
        /// <summary>
        /// 获取PDA消息列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public DataSet GetPDAMsgList(string areacode, int curentpage, int pagesize, string collname, string begintime, string endtime, string projcode, string Order, string Field,
                                        ref int rowCount, ref int pageCount, string streetcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@AreaCode",areacode),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CollName",collname),
                                new SqlParameter("@BeginTime",begintime),
                                new SqlParameter("@EndTime",endtime),
                                new SqlParameter("@ProjCode",projcode),
                                new SqlParameter("@StreetCode",streetcode)};
            arrSP[2].Direction = ParameterDirection.Output;
            arrSP[3].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetPDAMsgList", CommandType.StoredProcedure, arrSP);
            rowCount = arrSP[2].Value.ToString() == "" ? 0 : int.Parse(arrSP[2].Value.ToString());
            pageCount = arrSP[3].Value.ToString() == "" ? 0 : int.Parse(arrSP[3].Value.ToString());
            return ds;
        }
        #endregion

        #region GetPDAMsgInfo:获取PDA消息信息
        /// <summary>
        /// 获取PDA消息信息
        /// </summary>
        /// <param name="id">消息编码</param>
        /// <returns></returns>
        public DataSet GetPDAMsgInfo(string id)
        {
            string strSQL = string.Format(@"select a.*,b.collname,d.projname,d.probdesc,c.address
                                            FROM  b_pdamsg As a,m_collecter AS b,b_project AS c,b_project_detail as d
                                            where a.collcode=b.collcode	
		                                            and a.projcode=c.projcode
		                                            and a.projcode=d.projcode
		                                            and  a.id = '{0}'", id);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetPDAProjectMsgInfo:获取PDA案件的消息信息

        /// <summary>
        /// 获取PDA案件的消息信息

        /// </summary>
        /// <param name="id">案件编号</param>
        /// <returns></returns>
        public DataSet GetPDAProjectMsgInfo(string projectcode)
        {
            SqlParameter[] str = new SqlParameter[]{
                                new SqlParameter("@projcode",Convert.ToInt32(projectcode))};
            return this.ExecuteDataset("pr_b_GetPDAProjectMsgList", CommandType.StoredProcedure, str);
        }
        #endregion

        #region SetPDAIsRead:设置已经读取了消息

        /// <summary>
        /// 设置已经读取了消息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        public int SetPDAIsRead(string projcode)
        {
            string strSQL = string.Format(@"update b_pdamsg set ioflag=3
                                            where projcode = '{0}'", projcode);
            return this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region GetOtherMsgList:获取其他消息列表
        /// <summary>
        /// 获取其他消息列表
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public DataSet GetOtherMsgList(string usercode,string areacode, int curentpage,
                                        int pagesize, string username, string collname, string begintime, string endtime, string Order, string Field, ref int rowCount, ref int pageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@usercode",usercode),
                                new SqlParameter("@areacode",areacode),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@UserName",username),
                                new SqlParameter("@CollName",collname),
                                new SqlParameter("@BeginTime",begintime),
                                new SqlParameter("@EndTime",endtime)};
            arrSP[3].Direction = ParameterDirection.Output;
            arrSP[4].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetOtherMsgList", CommandType.StoredProcedure, arrSP);
            rowCount = arrSP[3].Value.ToString() == "" ? 0 : int.Parse(arrSP[3].Value.ToString());
            pageCount = arrSP[4].Value.ToString() == "" ? 0 : int.Parse(arrSP[4].Value.ToString());
            return ds;
        }
        #endregion

        #region DeleteOtherMsg:删除其他消息
        /// <summary>
        /// 删除其他消息
        /// </summary>
        /// <param name="id">消息id</param>
        /// <returns></returns>
        public void DeleteOtherMsg(string id)
        {
            string strSQL = string.Format(@"delete from m_pdamsg_other 
                                            where id = '{0}'", id);
            this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region GetOtherMsgInfo:获取其他消息信息
        /// <summary>
        /// 获取其他消息信息
        /// </summary>
        /// <param name="id">消息编码</param>
        /// <returns></returns>
        public DataSet GetOtherMsgInfo(string id)
        {

            string strSQL = string.Format(@"select a.*,b.collname
                                            FROM  m_pdamsg_other As a,m_collecter AS b
                                            where a.collcode=b.collcode  
		                                            and  id = '{0}';
                                            UPDATE m_pdamsg_other SET state = '2' 
                                            WHERE id ='{1}'", id,id);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetHelpMsgList:获取帮助消息列表
        /// <summary>
        /// 获取帮助消息列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public DataSet GetHelpMsgList(string areacode, int curentpage, int pagesize, string usercode, string collname, string begintime, string endtime, string Order, string Field,
                                        ref int rowCount, ref int pageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@areacode",areacode),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@UserCode",Convert.ToInt32(usercode)),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CollName",collname),
                                new SqlParameter("@BeginTime",begintime),
                                new SqlParameter("@EndTime",endtime)};
            arrSP[3].Direction = ParameterDirection.Output;
            arrSP[4].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetHelpMsgList", CommandType.StoredProcedure, arrSP);
            rowCount = arrSP[2].Value.ToString() == "" ? 0 : int.Parse(arrSP[3].Value.ToString());
            pageCount = arrSP[3].Value.ToString() == "" ? 0 : int.Parse(arrSP[4].Value.ToString());
            return ds;
        }
        #endregion

        #region DeleteHelpMsg:删除帮助消息
        /// <summary>
        /// 删除帮助消息
        /// </summary>
        /// <param name="id">消息id</param>
        /// <returns></returns>
        public void DeleteHelpMsg(string id)
        {
            string strSQL = string.Format(@"delete from m_call_help where id = '{0}'", id);
            this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region GetUserData:根据区域信息,获取用户信息树

        /// <summary>
        /// 根据区域信息,获取用户信息树

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetUserData(string areacode)
        {
            string strSQL = string.Format(@"select * from p_depart where area like '{0}%'
                                            ;
                                            select * from p_user where areacode like '{1}%'",areacode,areacode);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetSmsSendNum：校验当日短信发送量是否超出限制
        /// <summary>
        /// 校验当前用户当日短信发送量是否超出限制，如果没有超出限制，那么把短信发送

        /// </summary>
        /// <param name="usercode">用户编号</param>
        /// <returns></returns>
        public IDataReader GetSmsSendNum(string usercode)
        {

            string strSQL = string.Format(@"if exists(
 			                                            select 1
                                                        from p_user 
                                                        where usercode = '{0}' 
                                                            and IsNull(SmsMaxNum,10)<=IsNull(SmsNum,0) 
                                                            and DateDiff(day,SmsDate,GetDate())=0
                                            ) 
                                            begin
	                                            select 1,SmsMaxNum
                                                from p_user 
                                                where usercode = '{0}'  
                                            end	
                                            else
                                            begin
	                                            update p_user
	                                            set SmsNum = case when DateDiff(day,SmsDate,GetDate())=0 then 
						                                            IsNull(SmsNum,0)+1 
					                                            else 1 end,
		                                            SmsDate=getdate()
	                                            where usercode = '{0}' ;
                                            	
	                                            select 0,SmsMaxNum
                                                from p_user 
                                                where usercode = '{0}' 
                                            end", usercode);
            return this.ExecuteReader(strSQL);
        }
        #endregion
    }
}
