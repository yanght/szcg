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
        /// 获得用户日志记录条数
        /// </summary>
        /// <param name="argOperator">操作员名称</param>
        /// <param name="argModel">模块名称</param>
        /// <param name="argStartDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        /// <returns>用户日志记录</returns>
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
        /// 获得系统日志记录条数
        /// </summary>
        /// <param name="argOperator"> 操作员Id</param>
        /// <param name="argModel">模块Id</param>
        /// <param name="argStartDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        /// <returns>系统日志记录数</returns>
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
        /// 获得用户日志记录结果集
        /// </summary>
        /// <param name="pageIndex">页编号</param>
        /// <param name="pageSize">页的记录行数</param>
        /// <param name="argOperator">操作员名称</param>
        /// <param name="argModelname">模块名称</param>
        /// <param name="argStateDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        /// <returns>用户日志</returns>
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
        /// 获得系统日志记录结果集
        /// </summary>
        /// <param name="pageIndex">页序号</param>
        /// <param name="pageSize">页的行数</param>
        /// <param name="argOperator">操作员名称</param>
        /// <param name="argModelname">模块名称</param>
        /// <param name="argStateDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        /// <returns>系统日志</returns>
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
        /// 获得指定的一条系统日志记录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="Id">日志Id</param>
        /// <returns>指定的一条系统日志记录</returns>
        public DataSet GetSystemLoged(int userId, int Id)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Id", Id)};
            DataSet ds = ExecuteDataset("getSystemLoged", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// 获得指定的一条用户日志记录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="Id">日志Id</param>
        /// <returns>用户日志</returns>
        public DataSet GetUserLoged(int userId, int Id)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@Id", Id)};
            DataSet ds = ExecuteDataset("getUserLoged", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// 获得操作员数据集
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
        /// 获得数据集
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

        #region GetLoglist:获取登录日志列表
        /// <summary>
        ///获取登录日志列表
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页显示条数年</param>
        /// <param name="ErrorString">返回错误信息</param>
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

        #region GetHolidaylist:获取节假日列表
        /// <summary>
        ///获取节假日列表
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页显示条数年</param>
        /// <param name="ErrorString">返回错误信息</param>
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


        #region DeleteHolidayDay:删除节假日列表
        /// <summary>
        ///删除节假日列表
        /// </summary>
        /// <param name="id">编号</param>
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

        #region DeleteSetTime:删除设置时间点列表
        /// <summary>
        ///删除设置时间点列表
        /// </summary>
        /// <param name="id">编号</param>
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

        #region GetLogCollectorlist:获取登录日志列表
        /// <summary>
        ///获取登录日志列表
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页显示条数年</param>
        /// <param name="ErrorString">返回错误信息</param>
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

        #region GetSetTimelist:获取设置时间点列表
        /// <summary>
        ///获取设置时间点列表
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页显示条数年</param>
        /// <param name="ErrorString">返回错误信息</param>
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
