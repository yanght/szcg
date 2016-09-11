using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.szbase.capability
{
    public class Capability : Teamax.Common.CommonDatabase, IDisposable
    {

        #region GetSessionLogCount��������ݿ��е��û���¼��¼�����
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
        /// <returns></returns>
        public int GetSessionLogCount(int userId,  string userName, string address, string stateDate, string endDate)
        {

            int recodeCount = 0;
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@userName", userName), 
                                new SqlParameter("@address", address), 
                                new SqlParameter("@stateDate", stateDate), 
                                new SqlParameter("@endDate", endDate) };
            IDataReader dr = ExecuteReader("getSessionLogCount", CommandType.StoredProcedure, true, arrSP);
            while (dr.Read())
            {
                recodeCount =int.Parse(dr["CNT"].ToString());
            }
            dr.Close();

            return recodeCount;
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
        /// <returns></returns>
        public DataSet GetSessionLog(int userId, int pageIndex, int pageSize, string userName, string address, string stateDate, string endDate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@PageIndex", pageIndex), 
                                new SqlParameter("@PageSize", pageSize), 
                                new SqlParameter("@userName", userName), 
                                new SqlParameter("@address", address), 
                                new SqlParameter("@stateDate", stateDate), 
                                new SqlParameter("@endDate", endDate) };
            DataSet ds = ExecuteDataset("getSessionLog", CommandType.StoredProcedure, arrSP);
            return ds;
        }
        #endregion

        #region GetSessionUser��������ݿ��е�ĳ���û���¼��ϸ��Ϣ
        /// <summary>
        /// ������ݿ��е�ĳ���û���¼��ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <param name="logId">�û���¼����</param>
        /// <returns>DataSet</returns>
        public DataSet GetSessionUser(int userId, int logId)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@LogId", logId) };
            DataSet ds = ExecuteDataset("getSessionUser", CommandType.StoredProcedure, arrSP);
            return ds;
        }
        #endregion

        #region GetAllUser��������ݿ��е������û������
        /// <summary>
        /// ������ݿ��е������û������
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllUser(int userId)
        {
            DataSet ds = ExecuteDataset("getAllUsers", CommandType.StoredProcedure);
            return ds;            
        }
        #endregion

        #region GetAllAddress��������ݿ��е�����IP��ַ�����
        /// <summary>
        /// ������ݿ��е�����IP��ַ�����
        /// </summary>
        /// <param name="userId">��ǰ�û�ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllAddress(int userId)
        {
            DataSet ds = ExecuteDataset("getAllAddress", CommandType.StoredProcedure);
            return ds;
        }
        #endregion

        //#region GetNowUsersList����ȡĳ�������µ����������û�
        /// <summary>
        /// ��ȡ����ĳ�������µ����������û��б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        //public DataSet GetNowUsersList(string areacode)
        //{
            //SqlParameter[] arrSP = new SqlParameter[] {
            //                    new SqlParameter("@areacode", areacode) };
            //DataSet ds = ExecuteDataset("pr_p_GetDepartList", CommandType.StoredProcedure, arrSP);
            //return ds;


            //string select = "id,personID,name,phone,area,street,organ";
            //string where = " 1=1 ";
            //if (per.name != null && !"".Equals(per.name))
            //{
            //    where += " and name like '%" + per.name + "%'";
            //}
            //if (per.phone != null && !"".Equals(per.phone))
            //{
            //    where += " and phone like '%" + per.phone + "%'";
            //}
            //if (per.area != null && !"".Equals(per.area))
            //{
            //    where += " and area like '%" + per.area + "%'";
            //}
            //if (per.street != null && !"".Equals(per.street))
            //{
            //    where += " and street like '%" + per.street + "%'";
            //}
            //if (per.organ != null && !"".Equals(per.organ))
            //{
            //    where += " and organ like '%" + per.organ + "%'";
            //}
            //string from = "personnnel";

            
            //try
            //{
            //    QueryUtil qu = new QueryUtil(select, from, where);
            //    qu.PageSize = pageSize;
            //    qu.SortBy = "id";
            //    qu.SortOrder = SortOrder.Descending;
            //    DataSet ds = qu.ExecuteDataset(pageIndex);
            //    PageManage page = new PageManage();
            //    page.ds = ds;
            //    page.rowCount = qu.RowCount;
            //    page.pageCount = qu.PageCount;
            //    page.pageSize = qu.PageSize;
              
            //    return page;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        //}
        //#endregion
    }
}
/*
            // �������ݿ⣬ִ��SQL���õ����������Ľ����
			DataTable table;
			table = new DataTable();
			table = com.teamax.SessionCache.CreateCacheTable();
			table = (DataTable)Application["SessionCache"];
			//�����û�IDΪ�յ�����
			DataRow[] foundRows;
			foundRows = table.Select("UserCode > 0 and AreaCode LIKE '" + userinfo.getAreacode().ToString() + "%'");
			
            //�����������DataTable
            DataTable pageTable = new DataTable();
			pageTable = com.teamax.SessionCache.CreateCacheTable();
			SetPageCount(foundRows.Length);
			int beginRecode = argPageIndex * pageSize;
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

*/