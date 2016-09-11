/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;�����ܹ�����߼���
 * �ṹ��ɣ�
 * ��    �ߣ�����
 * �������ڣ�2007-05-26
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using szcg.com.teamax.business.entity;


namespace bacgBL.com.teamax.szbase.capability
{
    public class Capability
    {
        public Capability()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.capability.";

        #region GetNowUsersList����õ�¼�û�ͬһ����ĵ�ǰ�����û���Ϣ�б�
        /// <summary>
        /// ��õ�¼�û�ͬһ����ĵ�ǰ�����û���Ϣ�б�
        /// </summary>
        /// <param name="pageIndex">�û���Ϣ�б�ĵڼ�ҳ�Ժ���û���Ϣ</param>
        /// <returns></returns>
        public DataTable GetNowUsersList(int pageIndex)
        {
			UserInfo userinfo = (UserInfo)HttpContext.Current.Session["userinfo"];
            DataTable pageTable = new DataTable();
            try
            {
                // �õ�����������Session�û������
			    DataTable table;
			    table = new DataTable();
                table = bacgBL.Login.SessionCache.CreateCacheTable();
			    table = (DataTable)HttpContext.Current.Application["SessionCache"];
			    //�����û�IDΪ�յ�����
			    DataRow[] foundRows;
			    foundRows = table.Select("UserCode > 0 and AreaCode LIKE '" + userinfo.getAreacode().ToString() + "%'");

                //���û����������DataTable

                pageTable = bacgBL.Login.SessionCache.CreateCacheTable();
                int beginRecode = (pageIndex-1) * pageSize;
			    int endRecode = beginRecode + pageSize - 1;
                for (int i = 0; i <= foundRows.Length - 1; i++)
                {
                    if (i >= beginRecode && i <= endRecode)
                    {
                        pageTable.ImportRow(foundRows[i]);
                    }
                    if (i > endRecode)
                    {
                        break;
                    }
                }
            
            }
            catch (Exception err)
            {
                throw err;
            }
            return pageTable;
        }
        #endregion


        #region GetSessionLog��������ݿ��е��û���¼��¼�����
        /// <summary>
        /// ������ݿ��е��û���¼��¼�����
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <param name="pageIndex">��¼��Ϣ�б�ĵڼ�ҳ�Ժ���û���¼��Ϣ</param>
        /// <param name="pageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="userName">�û�����</param>
        /// <param name="address">ip��ַ</param>
        /// <param name="stateDate">��¼��ʼʱ��</param>
        /// <param name="endDate">��¼��ֹʱ��</param>
        /// <returns>DataSet</returns>
        public static DataSet GetSessionLog(int userId, int pageIndex, int pageSize, string userName, string address, string stateDate, string endDate)
        {
            try
            {
                using (bacgDL.szbase.capability.Capability dl = new bacgDL.szbase.capability.Capability())
                {
                    return dl.GetSessionLog(userId, pageIndex, pageSize, userName, address, stateDate, endDate);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion


        #region GetSessionLog��������ݿ��е��û���¼��¼����
        /// <summary>
        /// ������ݿ��е��û���¼��¼����
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <param name="userName">�û�����</param>
        /// <param name="address">ip��ַ</param>
        /// <param name="stateDate">��¼��ʼʱ��</param>
        /// <param name="endDate">��¼��ֹʱ��</param>
        /// <returns>int</returns>
        public static int GetSessionLogCount(int userId, string userName, string address, string stateDate, string endDate)
        {
            try
            {
                using (bacgDL.szbase.capability.Capability dl = new bacgDL.szbase.capability.Capability())
                {
                    return dl.GetSessionLogCount(userId,userName, address, stateDate, endDate);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region GetSessionUser��������ݿ��е�ĳ���û���¼��ϸ��Ϣ
        /// <summary>
        /// ������ݿ��е�ĳ���û���¼��ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <param name="logId">�û���¼����</param>
        /// <returns>DataSet</returns>
        public static DataSet GetSessionUser(int userId, int logId)
        {
            try
            {
                using (bacgDL.szbase.capability.Capability dl = new bacgDL.szbase.capability.Capability())
                {
                    return dl.GetSessionUser(userId,logId);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion


        #region GetAllUser��������ݿ��е������û������
        /// <summary>
        /// ������ݿ��е������û������
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetAllUser(int userId)
        {
            try
            {
                using (bacgDL.szbase.capability.Capability dl = new bacgDL.szbase.capability.Capability())
                {
                    return dl.GetAllUser(userId);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region GetAllAddress��������ݿ��е�����IP��ַ�����
        /// <summary>
        /// ������ݿ��е�����IP��ַ�����
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetAllAddress(int userId)
        {
            try
            {
                using (bacgDL.szbase.capability.Capability dl = new bacgDL.szbase.capability.Capability())
                {
                    return dl.GetAllAddress(userId);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
    }
}
