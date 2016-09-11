/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;��SessionCache ��½ģ���ҵ���߼�����
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-05-29
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace bacgBL.Login
{
	/// <summary>
	/// CacheMaintain ��ժҪ˵����
	/// </summary>
	public class SessionCache
    {
        #region CreateCacheTable�������Ự�����
        public static DataTable CreateCacheTable()
		{
			DataTable table = new DataTable("SessionCache");

			DataColumn column;

			column = new DataColumn();
			column.ColumnName = "SessionId";
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "UserCode";   //�û�ID
			column.DataType = System.Type.GetType("System.Int32");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "LoginName";   //�û���
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "HostAddress";    //IP��ַ
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
			column.ColumnName = "LoginTime";      //��½ʱ��
			column.DataType = System.Type.GetType("System.DateTime");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "EndTime";
			column.DataType = System.Type.GetType("System.DateTime");
			table.Columns.Add(column);

			column = new DataColumn();
			column.ColumnName = "AreaCode";      //����
			column.DataType = System.Type.GetType("System.String");
			table.Columns.Add(column);

			return table;
		}
		#endregion

        #region SessionStart���Ự��ʼʱ�ѵ�½��Ϣд�뵽�Ự�����
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

        #region UserLogin�����û���¼��Ϣд��Session�����
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

        #region SessionEnd���Ự����ʱ�ѻỰ������е�����д�뵽�������ݿ����
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
