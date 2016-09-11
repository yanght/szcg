using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using Teamax.Common;
using System.Collections;

namespace bacgDL.szbase.workflow
{
    public class workFlow : Teamax.Common.CommonDatabase
    {

        #region 得到工作流类别
        /// <summary>
        /// 得到工作流类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetWorkFlowName()
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                ds = com.ExecuteDataset("pr_s_GetWorkFlowType", CommandType.StoredProcedure);
                return ds;
            }

        }
        #endregion

        #region 得到各类工作流的所有工作流名称
        /// <summary>
        /// 得到各类工作流的所有工作流名称
        /// </summary>
        /// <param name="parentType">父节点类型</param>
        /// <returns></returns>
        public DataSet GetSubFlowName(int parentType)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@flowType",parentType)};
                DataSet ds = new DataSet();
                ds = com.ExecuteDataset("pr_s_GetSubWorkFlowName", CommandType.StoredProcedure, arrSP);
                return ds;
            }
        }
        #endregion

        #region 增加新的工作流，或节点或操作的ID
        /// <summary>
        /// 增加新的工作流，或节点或操作的ID
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
        public void InsertNodeId(int actionType)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@actionType",actionType)};
                com.ExecuteNonQuery("pr_s_InsertWorkFlowId", CommandType.StoredProcedure, arrSP);
            }
        }
        #endregion


        #region 获取工作流，或节点或操作最大的ID
        /// <summary>
        ///获取工作流，或节点或操作最大的ID
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
        /// <returns></returns>
        public string GetNodeId(int actionType)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@actionType",actionType)};
                string ID = com.ExecuteScalar("pr_s_GetWorkFlowId", CommandType.StoredProcedure, arrSP).ToString();
                return ID;
            }
        }
        #endregion

        #region 初始化节点或事件状态位名称和类别
        /// <summary>
        ///初始化节点或事件状态位名称和类别
        /// </summary>
        /// <returns></returns>
        public DataSet InitNodeEventState(string strType)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("type",strType)};
                DataSet ds = com.ExecuteDataset("pr_s_GetNodeEventState", CommandType.StoredProcedure, arrSP);
                return ds;
            }
        }
        #endregion

        #region 保存工作流，或节点或操作
        /// <summary>
        /// 保存工作流，或节点或操作
        /// </summary>
        public string SaveFlowInfo(string tag, int flowid, string xml)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@FlowId",SqlDbType.Int),
                                new SqlParameter("@tag",SqlDbType.VarChar),            
                                new SqlParameter("@xml", SqlDbType.NText)};
                arrSP[0].Value = flowid;
                arrSP[1].Value = tag;
                arrSP[2].Value = xml;

                string flowname = (string)com.ExecuteScalar("pr_s_InsertFlowInfo", CommandType.StoredProcedure, arrSP);
                return flowname;
            }
        }
        #endregion

        #region 删除工作流，或节点或操作
        /// <summary>
        /// 删除工作流，或节点或操作
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
        public int DeleteFlowInfo(int actionType, int id)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@id",id),
                                new SqlParameter("@tag",actionType)};

                int tag = com.ExecuteNonQuery("pr_s_DeleteFlowInfo", CommandType.StoredProcedure, arrSP);
                return tag;
            }
        }
        #endregion

        #region 得到图片地址和名称
        /// <summary>
        /// 得到图片地址和名称
        /// </summary>
        /// <returns></returns>
        public DataSet GetImageAddAndName(string strName)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@name",strName)};

                ds = com.ExecuteDataset("pr_s_GetImageAddAndName", CommandType.StoredProcedure, arrSP);
                return ds;
            }
        }
        #endregion

        #region 得到工作流类型
        /// <summary>
        /// 得到工作流类型
        /// </summary>
        /// <returns></returns>
        public DataSet GetWorkFlowInfo()
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                ds = com.ExecuteDataset("pr_s_GetWorkFlowInfo", CommandType.StoredProcedure);
                return ds;
            }

        }
        #endregion

        #region 当前页面下的工作流类别
        /// <summary>
        /// 当前页面下的工作流类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageFlowType(string strButId)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@buttoncode",strButId)};

                ds = com.ExecuteDataset("pr_s_GetPageWorkFlowInfo", CommandType.StoredProcedure, arrSP);
                return ds;
            }

        }
        #endregion

        #region 如果改变了流程，获得对应流程开始节点的ButtonId
        /// <summary>
        /// 如果改变了流程，获得对应流程开始节点的ButtonId
        /// </summary>
        /// <returns></returns>
        public DataSet GetChangeFlowButtonId(string strFlowType)
        {
            using (CommonDatabase com = new CommonDatabase())
            {
                DataSet ds = new DataSet();
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@flowtype",strFlowType)};

                ds = com.ExecuteDataset("GetChangeFlowButtonId", CommandType.StoredProcedure, arrSP);
                return ds;
            }

        }
        #endregion

        #region 获取流程状态
        public DataTable GetFlowState(string type)
        {
            string strSql = @"select * from S_FLOWSTATE where 1=1 ";
            if (type == "1")
                strSql += " and type='1'";
            else if (type == "0")
                strSql += " and type='0'";

            DataSet ds = this.ExecuteDataset(strSql);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion
    }
}
