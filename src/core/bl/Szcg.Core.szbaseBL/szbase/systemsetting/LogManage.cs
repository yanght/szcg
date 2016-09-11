using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace bacgBL.com.teamax.szbase.systemsetting
{
    public class LogManage
    {
        public LogManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.systemsetting.LogManage";

        /// <summary>
        /// 获得日志记录条数
        /// </summary>
        /// <param name="code">系统日志=0,用户日志=1</param>
        /// <param name="argOperator">操作员名称</param>
        /// <param name="argModel">模块名称</param>
        /// <param name="argStartDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        /// <returns>日志记录数</returns>
        public static int GetLogsCount(
                            int userId,
                            string code,
                            string argOperator,
                            string argModel,
                            string argStartDate,
                            string argEndDate)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    int recodeCount = 0;
                    if (code == "1")
                    {
                        recodeCount = dl.GetUserLogsCount(userId, argOperator, argModel, argStartDate, argEndDate);
                    }
                    else if (code == "0")
                    {
                        recodeCount = dl.GetSystemLogsCount(userId, argOperator, argModel, argStartDate, argEndDate);
                    }
                    return recodeCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得日志记录
        /// </summary>
        /// <param name="code">系统日志=0,用户日志=1</param>
        /// <param name="pageIndex">页编号</param>
        /// <param name="pageSize">页的记录行数</param>
        /// <param name="argOperator">操作员名称</param>
        /// <param name="argModelname">模块名称</param>
        /// <param name="argStateDate">开始时间</param>
        /// <param name="argEndDate">结束时间</param>
        public static DataSet GetLogs(
                        int userId,
                        string code,
                        int pageIndex,
                        int pageSize,
                        string argOperator,
                        string argModelname,
                        string argStateDate,
                        string argEndDate)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    DataSet dtst = new DataSet();
                    if (code == "1")
                    {
                        dtst = dl.GetUserLogs(userId, pageIndex, pageSize, argOperator, argModelname, argStateDate, argEndDate);
                    }
                    else if (code == "0")
                    {
                        dtst = dl.GetSystemLogs(userId, pageIndex, pageSize, argOperator, argModelname, argStateDate, argEndDate);
                    }
                    return dtst;
                }                
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }

        /// <summary>
        /// 获得指定的一条日志记录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="code">系统日志=0,用户日志=1</param>
        /// <returns>指定的一条日志记录</returns>
        public static DataSet GetLoged(int userId, int Id, string code)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    DataSet dtst = new DataSet();
                    if (code == "1")
                    {
                        dtst = dl.GetUserLoged(userId, Id);
                    }
                    else if (code == "0")
                    {
                        dtst = dl.GetSystemLoged(userId, Id);
                    }
                    return dtst;
                }                
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获得操作员数据集
        /// </summary>
        public static String[] GetOperator(int userId)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetOperator(userId);
                }                
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        public static String[] GetMolder(int userId)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetMolder(userId);
                }
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
        public DataSet GetLoglist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime, ref int  RowCount,ref int PageCount,ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetLoglist(CurrentPage, PageSize, Order, Field, name, begintime, endtime, ref RowCount, ref PageCount);
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return null;
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
        public DataSet GetLogCollectorlist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime, string commname,string txt_outstarttime,string txt_outendtime,ref int RowCount, ref int PageCount, ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetLogCollectorlist(CurrentPage, PageSize, Order, Field, name, begintime, endtime,commname,txt_outstarttime,txt_outendtime, ref RowCount, ref PageCount);
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return null;
            }
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
        public DataSet GetHolidaylist(int CurrentPage, int PageSize, string Order, string Field, string name, string begintime, string endtime, ref int RowCount, ref int PageCount, ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetHolidaylist(CurrentPage, PageSize, Order, Field, name, begintime, endtime, ref RowCount, ref PageCount);
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return null;
            }
        }
        #endregion

        #region DeleteHolidayDay:删除节假日列表
        /// <summary>
        ///删除节假日列表
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="ErrorString">返回错误信息</param>
        /// <returns></returns>
        public int DeleteHolidayDay(string id, ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.DeleteHolidayDay(id); ;
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return 0;
            }
        }
        #endregion

        #region DeleteHolidayDay:删除设置时间点列表
        /// <summary>
        ///删除设置时间点列表
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="ErrorString">返回错误信息</param>
        /// <returns></returns>
        public int DeleteSetTime(string qy_date, ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.DeleteSetTime(qy_date); ;
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return 0;
            }
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
        public DataSet GetSetTimelist(int CurrentPage, int PageSize, string Order, string Field, ref int RowCount, ref int PageCount, ref string ErrorString)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.LogManage dl = new bacgDL.szbase.systemsetting.LogManage())
                {
                    return dl.GetSetTimelist(CurrentPage, PageSize, Order, Field, ref RowCount, ref PageCount);
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return null;
            }
        }
        #endregion
    }
}
