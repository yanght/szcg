using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace bacgDL.szbase.systemsetting
{
    public class LogManage : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// ����û���־��¼����
        /// </summary>
        /// <param name="argOperator">����Ա����</param>
        /// <param name="argModel">ģ������</param>
        /// <param name="argStartDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
        /// <returns>�û���־��¼</returns>
        public int GetUserLogsCount(
                            int userId,
                            string argOperator,
                            string argModel,
                            string argStartDate,
                            string argEndDate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Operator", argOperator),
                                new SqlParameter("@Model", argModel),
                                new SqlParameter("@StateDate", argStartDate),
                                new SqlParameter("@EndDate", argEndDate)};
            IDataReader dr = ExecuteReader("getUserLogsCount", CommandType.StoredProcedure, true, arrSP);
            dr.Read();
            int recodeCount = (int)dr["CNT"];
            return recodeCount;
        }

        /// <summary>
        /// ���ϵͳ��־��¼����
        /// </summary>
        /// <param name="argOperator"> ����ԱId</param>
        /// <param name="argModel">ģ��Id</param>
        /// <param name="argStartDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
        /// <returns>ϵͳ��־��¼��</returns>
        public int GetSystemLogsCount(
                            int userId,
                            string argOperator,
                            string argModel,
                            string argStartDate,
                            string argEndDate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Operator", argOperator),
                                new SqlParameter("@Model", argModel),
                                new SqlParameter("@StateDate", argStartDate),
                                new SqlParameter("@EndDate", argEndDate)};
            IDataReader dr = ExecuteReader("getSystemLogsCount", CommandType.StoredProcedure, true, arrSP);
            dr.Read();
            int recodeCount = (int)dr["CNT"];
            return recodeCount;
        }

        /// <summary>
        /// ����û���־��¼�����
        /// </summary>
        /// <param name="pageIndex">ҳ���</param>
        /// <param name="pageSize">ҳ�ļ�¼����</param>
        /// <param name="argOperator">����Ա����</param>
        /// <param name="argModelname">ģ������</param>
        /// <param name="argStateDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
        /// <returns>�û���־</returns>
        public DataSet GetUserLogs(
                                    int userId,
                                    int pageIndex,
                                    int pageSize,
                                    string argOperator,
                                    string argModelname,
                                    string argStateDate,
                                    string argEndDate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@PageIndex", pageIndex), 
                                new SqlParameter("@PageSize", pageSize), 
                                new SqlParameter("@Operator", argOperator), 
                                new SqlParameter("@Model", argModelname), 
                                new SqlParameter("@StateDate", argStateDate), 
                                new SqlParameter("@EndDate", argEndDate)};
            DataSet ds = ExecuteDataset("getUserLogs", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ���ϵͳ��־��¼�����
        /// </summary>
        /// <param name="pageIndex">ҳ���</param>
        /// <param name="pageSize">ҳ������</param>
        /// <param name="argOperator">����Ա����</param>
        /// <param name="argModelname">ģ������</param>
        /// <param name="argStateDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
        /// <returns>ϵͳ��־</returns>
        public DataSet GetSystemLogs(
                                    int userId,
                                    int pageIndex,
                                    int pageSize,
                                    string argOperator,
                                    string argModelname,
                                    string argStateDate,
                                    string argEndDate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@PageIndex", pageIndex), 
                                new SqlParameter("@PageSize", pageSize), 
                                new SqlParameter("@Operator", argOperator), 
                                new SqlParameter("@Model", argModelname), 
                                new SqlParameter("@StateDate", argStateDate), 
                                new SqlParameter("@EndDate", argEndDate)};
            DataSet ds = ExecuteDataset("getSystemLogs", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ���ָ����һ��ϵͳ��־��¼
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="Id">��־Id</param>
        /// <returns>ָ����һ��ϵͳ��־��¼</returns>
        public DataSet GetSystemLoged(int userId, int Id)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Id", Id)};
            DataSet ds = ExecuteDataset("getSystemLoged", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ���ָ����һ���û���־��¼
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="Id">��־Id</param>
        /// <returns>�û���־</returns>
        public DataSet GetUserLoged(int userId, int Id)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Id", Id)};
            DataSet ds = ExecuteDataset("getUserLoged", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ��ò���Ա���ݼ�
        /// </summary>
        public String[] GetOperator(int userId)
        {
            try
            {
                string sSql = "SELECT B.username AS name FROM  p_user AS B ";
                IDataReader dr = ExecuteReader(sSql, CommandType.Text, true);
                ArrayList list = new ArrayList();
                while (dr.Read())
                {
                    list.Add(dr["name"].ToString());
                }
                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        public String[] GetMolder(int userId)
        {
            try
            {
                string sSql = "SELECT modelname AS name FROM p_model ";
                IDataReader dr = ExecuteReader(sSql, CommandType.Text, true);
                ArrayList list = new ArrayList();
                while (dr.Read())
                {
                    list.Add(dr["name"].ToString());
                }
                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #region GetLoglist:��ȡ��¼��־�б�
        /// <summary>
        ///��ȡ��¼��־�б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
        /// <returns></returns>
        public DataSet GetLoglist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime,ref int RowCount ,ref int PageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CurrentPage",CurrentPage),
                                new SqlParameter("@PageSize",PageSize),
                                new SqlParameter("@LoginName",name),
                                new SqlParameter("@Begintime",begintime),
                                new SqlParameter("@Endtime",endtime)};

            arrSP[0].Direction = ParameterDirection.Output;
            arrSP[1].Direction = ParameterDirection.Output;

            DataSet ds = null;
            try
            {
                ds = this.ExecuteDataset("pr_p_GetlogList", CommandType.StoredProcedure, arrSP);
                if(ds.Tables.Count>0)
                {
                    RowCount = Convert.ToInt32(arrSP[0].Value.ToString());
                    PageCount = Convert.ToInt32(arrSP[1].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        #endregion

        #region GetHolidaylist:��ȡ�ڼ����б�
        /// <summary>
        ///��ȡ�ڼ����б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
        /// <returns></returns>
        public DataSet GetHolidaylist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime, ref int RowCount, ref int PageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CurrentPage",CurrentPage),
                                new SqlParameter("@PageSize",PageSize),
                                new SqlParameter("@HolidayName",name),
                                new SqlParameter("@Begintime",begintime),
                                new SqlParameter("@Endtime",endtime)};

            arrSP[0].Direction = ParameterDirection.Output;
            arrSP[1].Direction = ParameterDirection.Output;

            DataSet ds = null;
            try
            {
                ds = this.ExecuteDataset("pr_p_GetHolidayList", CommandType.StoredProcedure, arrSP);
                if (ds.Tables.Count > 0)
                {
                    RowCount = Convert.ToInt32(arrSP[0].Value.ToString());
                    PageCount = Convert.ToInt32(arrSP[1].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        #endregion


        #region DeleteHolidayDay:ɾ���ڼ����б�
        /// <summary>
        ///ɾ���ڼ����б�
        /// </summary>
        /// <param name="id">���</param>
        /// <returns></returns>
        public int DeleteHolidayDay(string id)
        {
            string sql = "delete from tb_holiday where id = "+id+"";

            try
            {
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteSetTime:ɾ������ʱ����б�
        /// <summary>
        ///ɾ������ʱ����б�
        /// </summary>
        /// <param name="id">���</param>
        /// <returns></returns>
        public int DeleteSetTime(string qydate)
        {
            string sql = "delete from tb_worktime where left(convert(varchar(20),time_start,120),10) = '" + qydate + "'" +
            " or left(convert(varchar(20),time_end,120),10) = '" + qydate + "'";

            try
            {
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetLogCollectorlist:��ȡ��¼��־�б�
        /// <summary>
        ///��ȡ��¼��־�б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
        /// <returns></returns>
        public DataSet GetLogCollectorlist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime, string commname, string txt_outstarttime, string txt_outendtime, ref int RowCount, ref int PageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CurrentPage",CurrentPage),
                                new SqlParameter("@PageSize",PageSize),
                                new SqlParameter("@LoginName",name),
                                new SqlParameter("@Begintime",begintime),
                                new SqlParameter("@commname",commname),
                                new SqlParameter("@outstarttime",txt_outstarttime),
                                new SqlParameter("@outendtime",txt_outendtime),
                                new SqlParameter("@Endtime",endtime)};

            arrSP[0].Direction = ParameterDirection.Output;
            arrSP[1].Direction = ParameterDirection.Output;

            DataSet ds = null;
            try
            {
                ds = this.ExecuteDataset("pr_p_GetCollectorlogList", CommandType.StoredProcedure, arrSP);
                if (ds.Tables.Count > 0)
                {
                    RowCount = Convert.ToInt32(arrSP[0].Value.ToString());
                    PageCount = Convert.ToInt32(arrSP[1].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        #endregion

        #region GetSetTimelist:��ȡ����ʱ����б�
        /// <summary>
        ///��ȡ����ʱ����б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
        /// <returns></returns>
        public DataSet GetSetTimelist(int CurrentPage, int PageSize, string Order, string Field, ref int RowCount, ref int PageCount)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CurrentPage",CurrentPage),
                                new SqlParameter("@PageSize",PageSize)
                               };

            arrSP[0].Direction = ParameterDirection.Output;
            arrSP[1].Direction = ParameterDirection.Output;

            DataSet ds = null;
            try
            {
                ds = this.ExecuteDataset("pr_p_GetSetTimeList", CommandType.StoredProcedure, arrSP);
                if (ds.Tables.Count > 0)
                {
                    RowCount = Convert.ToInt32(arrSP[0].Value.ToString());
                    PageCount = Convert.ToInt32(arrSP[1].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        #endregion
    }
}
