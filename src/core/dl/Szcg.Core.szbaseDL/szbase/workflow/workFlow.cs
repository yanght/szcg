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

        #region �õ����������
        /// <summary>
        /// �õ����������
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

        #region �õ����๤���������й���������
        /// <summary>
        /// �õ����๤���������й���������
        /// </summary>
        /// <param name="parentType">���ڵ�����</param>
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

        #region �����µĹ���������ڵ�������ID
        /// <summary>
        /// �����µĹ���������ڵ�������ID
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
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


        #region ��ȡ����������ڵ���������ID
        /// <summary>
        ///��ȡ����������ڵ���������ID
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
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

        #region ��ʼ���ڵ���¼�״̬λ���ƺ����
        /// <summary>
        ///��ʼ���ڵ���¼�״̬λ���ƺ����
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

        #region ���湤��������ڵ�����
        /// <summary>
        /// ���湤��������ڵ�����
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

        #region ɾ������������ڵ�����
        /// <summary>
        /// ɾ������������ڵ�����
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
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

        #region �õ�ͼƬ��ַ������
        /// <summary>
        /// �õ�ͼƬ��ַ������
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

        #region �õ�����������
        /// <summary>
        /// �õ�����������
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

        #region ��ǰҳ���µĹ��������
        /// <summary>
        /// ��ǰҳ���µĹ��������
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

        #region ����ı������̣���ö�Ӧ���̿�ʼ�ڵ��ButtonId
        /// <summary>
        /// ����ı������̣���ö�Ӧ���̿�ʼ�ڵ��ButtonId
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

        #region ��ȡ����״̬
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
