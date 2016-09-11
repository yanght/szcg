using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// SQL Server 数据库操作帮助类
	/// </summary>
	public class SQLHelper
	{
		// 数据库连接字符串
		public string CONN_GPS_STRING = ConfigurationManager.AppSettings["CONN_GPS_STRING"];
        public string CONN_BACG_STRING = ConfigurationManager.AppSettings["ConnString"];

		public SQLHelper() {}

        public SQLHelper(bool tag) 
        {
            if (tag)
                CONN_GPS_STRING = CONN_BACG_STRING;
        }
		/// <summary>
		/// 执行 Transact-SQL 语句(无返回结果集)
		/// </summary>
		/// <param name="cmdText">Transact-SQL 语句</param>
		/// <param name="cmdParms">参数数组</param>
		/// <returns>执行所影响的行数</returns>
		public int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();

			using (SqlConnection conn = new SqlConnection(CONN_GPS_STRING))
			{
				PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
				return cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 执行 Transact-SQL 语句(无返回结果集)(使用事务)
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="cmdText">Transact-SQL 语句</param>
		/// <param name="cmdParms"></param>
		/// <returns>执行所影响的行数</returns>
		public int ExecuteNonQuery(SqlTransaction trans, string cmdText, params SqlParameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, trans.Connection, trans, CommandType.Text, cmdText, cmdParms);
			return cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// 执行 Transact-SQL 语句(填充 DataTable)
		/// </summary>
		/// <param name="dataTable">需要填充的 DataTable</param>
		/// <param name="cmdText">Transact-SQL 语句</param>
		/// <param name="cmdParms">参数数组</param>
		public void ExecuteDataTable(DataTable dataTable, string cmdText, params SqlParameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter ada = new SqlDataAdapter(cmd);

			using (SqlConnection conn = new SqlConnection(CONN_GPS_STRING))
			{
				PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
				ada.Fill(dataTable);
			}
		}

		/// <summary>
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
		/// </summary>
		/// <param name="cmdText">Transact-SQL 语句</param>
		/// <param name="cmdParms">参数数组</param>
		/// <returns>结果集中第一行的第一列。</returns>
		public object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();

			using (SqlConnection conn = new SqlConnection(CONN_GPS_STRING))
			{
				PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
				return cmd.ExecuteScalar();
			}
		}

		/// <summary>
		/// 为执行准备一个 SqlCommand 对象
		/// </summary>
		/// <param name="cmd">SqlCommand 对象</param>
		/// <param name="conn">SqlConnection 对象</param>
		/// <param name="trans">SqlTransaction 对象</param>
		/// <param name="cmdType">设置一个值，该值指示如何解释 CommandText 属性</param>
		/// <param name="cmdText">设置要对数据源执行的 Transact-SQL 语句或存储过程</param>
		/// <param name="cmdParms">Transact-SQL 语句或存储过程的参数数组</param>
		private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms) 
		{

			if (conn.State != ConnectionState.Open)
				conn.Open();

			cmd.Connection = conn;
			cmd.CommandText = cmdText;

			if (trans != null)
				cmd.Transaction = trans;

			cmd.CommandType = cmdType;

			if (cmdParms != null) 
			{
				foreach (SqlParameter parm in cmdParms)
					cmd.Parameters.Add(parm);
			}
		}
	}

	public class SQLUtility
	{
		public SQLUtility() {}

		/// <summary>
		/// 为 Transact-SQL 语句添加查询条件
		/// </summary>
		/// <param name="strSQL">需要添加查询条件的 Transact-SQL 语句</param>
		/// <param name="strQualification">查询条件</param>
		/// <returns>已添加查询条件的 Transact-SQL 语句</returns>
		public string AddQualification(string strSQL, string strQualification)
		{
			if (strSQL.ToUpper().IndexOf(" WHERE ") < 0)
				strSQL += " WHERE ";
			else
				strSQL += " AND ";
			strSQL += strQualification;
			return strSQL;
		}
	}
}
