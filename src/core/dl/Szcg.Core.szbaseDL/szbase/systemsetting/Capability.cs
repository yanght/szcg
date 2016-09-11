using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.szbase.capability
{
    public class Capability : Teamax.Common.CommonDatabase, IDisposable
    {

        #region GetSessionLogCount：获得数据库中的用户登录记录结果集
        /// <summary>
        /// 获得数据库中的用户登录记录结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="pageIndex">登录信息列表的第几页以后的用户登录信息</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="userName">用户名称</param>
        /// <param name="address">ip地址</param>
        /// <param name="stateDate">登录起始时间</param>
        /// <param name="endDate">登录终止时间</param>
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

        #region GetSessionLog：获得数据库中的用户登录记录结果集
        /// <summary>
        /// 获得数据库中的用户登录记录结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="pageIndex">登录信息列表的第几页以后的用户登录信息</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="userName">用户名称</param>
        /// <param name="address">ip地址</param>
        /// <param name="stateDate">登录起始时间</param>
        /// <param name="endDate">登录终止时间</param>
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

        #region GetSessionUser：获得数据库中的某个用户登录详细信息
        /// <summary>
        /// 获得数据库中的某个用户登录详细信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="logId">用户登录名称</param>
        /// <returns>DataSet</returns>
        public DataSet GetSessionUser(int userId, int logId)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@LogId", logId) };
            DataSet ds = ExecuteDataset("getSessionUser", CommandType.StoredProcedure, arrSP);
            return ds;
        }
        #endregion

        #region GetAllUser：获得数据库中的所有用户结果集
        /// <summary>
        /// 获得数据库中的所有用户结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllUser(int userId)
        {
            DataSet ds = ExecuteDataset("getAllUsers", CommandType.StoredProcedure);
            return ds;            
        }
        #endregion

        #region GetAllAddress：获得数据库中的所有IP地址结果集
        /// <summary>
        /// 获得数据库中的所有IP地址结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllAddress(int userId)
        {
            DataSet ds = ExecuteDataset("getAllAddress", CommandType.StoredProcedure);
            return ds;
        }
        #endregion

        //#region GetNowUsersList：获取某个区域下的所有在线用户
        /// <summary>
        /// 获取属于某个区域下的所有在线用户列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
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
            // 连接数据库，执行SQL语句得到符合条件的结果集
			DataTable table;
			table = new DataTable();
			table = com.teamax.SessionCache.CreateCacheTable();
			table = (DataTable)Application["SessionCache"];
			//过虑用户ID为空的数据
			DataRow[] foundRows;
			foundRows = table.Select("UserCode > 0 and AreaCode LIKE '" + userinfo.getAreacode().ToString() + "%'");
			
            //将结果集导入DataTable
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