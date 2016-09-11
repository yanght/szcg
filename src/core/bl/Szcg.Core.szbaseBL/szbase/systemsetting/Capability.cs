/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：性能管理的逻辑层
 * 结构组成：
 * 作    者：何勇
 * 创建日期：2007-05-26
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
using System.Web;
using szcg.com.teamax.business.entity;


namespace bacgBL.com.teamax.szbase.capability
{
    public class Capability
    {
        public Capability()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.capability.";

        #region GetNowUsersList：获得登录用户同一区域的当前在线用户信息列表
        /// <summary>
        /// 获得登录用户同一区域的当前在线用户信息列表
        /// </summary>
        /// <param name="pageIndex">用户信息列表的第几页以后的用户信息</param>
        /// <returns></returns>
        public DataTable GetNowUsersList(int pageIndex)
        {
			UserInfo userinfo = (UserInfo)HttpContext.Current.Session["userinfo"];
            DataTable pageTable = new DataTable();
            try
            {
                // 得到符合条件的Session用户结果集
			    DataTable table;
			    table = new DataTable();
                table = bacgBL.Login.SessionCache.CreateCacheTable();
			    table = (DataTable)HttpContext.Current.Application["SessionCache"];
			    //过虑用户ID为空的数据
			    DataRow[] foundRows;
			    foundRows = table.Select("UserCode > 0 and AreaCode LIKE '" + userinfo.getAreacode().ToString() + "%'");

                //将用户结果集导入DataTable

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


        #region GetSessionLog：获得数据库中的用户登录记录总数
        /// <summary>
        /// 获得数据库中的用户登录记录总数
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="userName">用户名称</param>
        /// <param name="address">ip地址</param>
        /// <param name="stateDate">登录起始时间</param>
        /// <param name="endDate">登录终止时间</param>
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

        #region GetSessionUser：获得数据库中的某个用户登录详细信息
        /// <summary>
        /// 获得数据库中的某个用户登录详细信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="logId">用户登录名称</param>
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


        #region GetAllUser：获得数据库中的所有用户结果集
        /// <summary>
        /// 获得数据库中的所有用户结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
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

        #region GetAllAddress：获得数据库中的所有IP地址结果集
        /// <summary>
        /// 获得数据库中的所有IP地址结果集
        /// </summary>
        /// <param name="userId">当前用户ID</param>
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
