/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：SessionCache 登陆模块的业务逻辑层类
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-05-29
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace bacgBL.Login
{
	/// <summary>
	/// CacheMaintain 的摘要说明。
	/// </summary>
	public class SessionCache
    {
        #region CreateCacheTable：创建会话缓存表
        public static DataTable CreateCacheTable()
		{
			DataTable table = new DataTable("SessionCache");

			DataColumn column;

			column = new DataColumn();
			column.ColumnName = "SessionId";
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "UserCode";   //用户ID
			column.DataType = System.Type.GetType("System.Int32");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "LoginName";   //用户名
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "HostAddress";    //IP地址
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "PlatForm";
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "BrowserType";
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "StartTime";
			column.DataType = System.Type.GetType("System.DateTime");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "LoginTime";      //登陆时间
			column.DataType = System.Type.GetType("System.DateTime");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "EndTime";
			column.DataType = System.Type.GetType("System.DateTime");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "AreaCode";      //区域
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			return table;
		}
		#endregion

        #region SessionStart：会话开始时把登陆信息写入到会话缓存表
        public static void SessionStart(DataTable table, 
            string strSessionId, string strHostAddress, string strPlatForm, string strBrowserType, 
            DateTime dtNow)
		{
			DataRow row = table.NewRow();

			DateTime now = DateTime.Now;

			row["SessionId"] = strSessionId;
			row["HostAddress"] = strHostAddress;
			row["PlatForm"] = strPlatForm;
			row["BrowserType"] = strBrowserType;
			row["StartTime"] = dtNow;

			table.Rows.Add(row);
        }
        #endregion

        #region UserLogin：把用户登录信息写入Session缓存表
        public static void UserLogin(DataTable table,
                string strSessionId,int intUserCode, string strUserName,string strAreaCode, DateTime dtNow)
		{
			DataRow[] rows = table.Select(string.Format("SessionId = '{0}'", strSessionId));

			if( rows.Length == 0 )
				return;

			rows[0]["UserCode"] = intUserCode;
			rows[0]["LoginName"] = strUserName;
			rows[0]["AreaCode"] = strAreaCode;
			rows[0]["LoginTime"] = dtNow;
		}
        #endregion

        #region SessionEnd：会话结束时把会话缓存表中的内容写入到物理数据库表中
        public static void SessionEnd(DataTable table, string strSessionId)
        {
            DataRow[] rows = table.Select(string.Format("SessionId = '{0}'", strSessionId));

            if (rows.Length == 0)
                return;

            try
            {
                using (bacgDL.login.Login dl = new bacgDL.login.Login())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@SessionId",rows[0]["SessionId"]),
                        new SqlParameter("@UserCode",rows[0]["UserCode"]),
                        new SqlParameter("@HostAddress",rows[0]["HostAddress"]),
                        new SqlParameter("@PlatForm",rows[0]["PlatForm"]),
                        new SqlParameter("@BrowserType",rows[0]["BrowserType"]),
                        new SqlParameter("@StartTime",rows[0]["StartTime"]),
                        new SqlParameter("@LoginTime",rows[0]["LoginTime"])
                    };

                    dl.ExecuteNonQuery("pr_sys_InsertLoginLog", CommandType.StoredProcedure, arrSP);
                }
            }
            finally
            {
                table.Rows.Remove(rows[0]);
            }
        }
        #endregion
	}
}
