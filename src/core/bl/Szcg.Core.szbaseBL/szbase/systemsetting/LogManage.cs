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
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.systemsetting.LogManage";

        /// <summary>
        /// �����־��¼����
        /// </summary>
        /// <param name="code">ϵͳ��־=0,�û���־=1</param>
        /// <param name="argOperator">����Ա����</param>
        /// <param name="argModel">ģ������</param>
        /// <param name="argStartDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
        /// <returns>��־��¼��</returns>
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
        /// �����־��¼
        /// </summary>
        /// <param name="code">ϵͳ��־=0,�û���־=1</param>
        /// <param name="pageIndex">ҳ���</param>
        /// <param name="pageSize">ҳ�ļ�¼����</param>
        /// <param name="argOperator">����Ա����</param>
        /// <param name="argModelname">ģ������</param>
        /// <param name="argStateDate">��ʼʱ��</param>
        /// <param name="argEndDate">����ʱ��</param>
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
        /// ���ָ����һ����־��¼
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="code">ϵͳ��־=0,�û���־=1</param>
        /// <returns>ָ����һ����־��¼</returns>
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
        /// ��ò���Ա���ݼ�
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
        /// ������ݼ�
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

         #region GetLoglist:��ȡ��¼��־�б�
        /// <summary>
        ///��ȡ��¼��־�б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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

        #region GetLogCollectorlist:��ȡ��¼��־�б�
        /// <summary>
        ///��ȡ��¼��־�б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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

        #region GetHolidaylist:��ȡ�ڼ����б�
        /// <summary>
        ///��ȡ�ڼ����б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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

        #region DeleteHolidayDay:ɾ���ڼ����б�
        /// <summary>
        ///ɾ���ڼ����б�
        /// </summary>
        /// <param name="id">���</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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

        #region DeleteHolidayDay:ɾ������ʱ����б�
        /// <summary>
        ///ɾ������ʱ����б�
        /// </summary>
        /// <param name="id">���</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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

        #region GetSetTimelist:��ȡ����ʱ����б�
        /// <summary>
        ///��ȡ����ʱ����б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
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
