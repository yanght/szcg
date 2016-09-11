using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// SQL Server ���ݿ����������
	/// </summary>
	public class SQLHelper
	{
		// ���ݿ������ַ���
		public string CONN_GPS_STRING = ConfigurationManager.AppSettings["CONN_GPS_STRING"];
        public string CONN_BACG_STRING = ConfigurationManager.AppSettings["ConnString"];

		public SQLHelper() {}

        public SQLHelper(bool tag) 
        {
            if (tag)
                CONN_GPS_STRING = CONN_BACG_STRING;
        }
		/// <summary>
		/// ִ�� Transact-SQL ���(�޷��ؽ����)
		/// </summary>
		/// <param name="cmdText">Transact-SQL ���</param>
		/// <param name="cmdParms">��������</param>
		/// <returns>ִ����Ӱ�������</returns>
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
		/// ִ�� Transact-SQL ���(�޷��ؽ����)(ʹ������)
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="cmdText">Transact-SQL ���</param>
		/// <param name="cmdParms"></param>
		/// <returns>ִ����Ӱ�������</returns>
		public int ExecuteNonQuery(SqlTransaction trans, string cmdText, params SqlParameter[] cmdParms)
		{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, trans.Connection, trans, CommandType.Text, cmdText, cmdParms);
			return cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// ִ�� Transact-SQL ���(��� DataTable)
		/// </summary>
		/// <param name="dataTable">��Ҫ���� DataTable</param>
		/// <param name="cmdText">Transact-SQL ���</param>
		/// <param name="cmdParms">��������</param>
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
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л��С�
		/// </summary>
		/// <param name="cmdText">Transact-SQL ���</param>
		/// <param name="cmdParms">��������</param>
		/// <returns>������е�һ�еĵ�һ�С�</returns>
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
		/// Ϊִ��׼��һ�� SqlCommand ����
		/// </summary>
		/// <param name="cmd">SqlCommand ����</param>
		/// <param name="conn">SqlConnection ����</param>
		/// <param name="trans">SqlTransaction ����</param>
		/// <param name="cmdType">����һ��ֵ����ֵָʾ��ν��� CommandText ����</param>
		/// <param name="cmdText">����Ҫ������Դִ�е� Transact-SQL ����洢����</param>
		/// <param name="cmdParms">Transact-SQL ����洢���̵Ĳ�������</param>
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
		/// Ϊ Transact-SQL �����Ӳ�ѯ����
		/// </summary>
		/// <param name="strSQL">��Ҫ��Ӳ�ѯ������ Transact-SQL ���</param>
		/// <param name="strQualification">��ѯ����</param>
		/// <returns>����Ӳ�ѯ������ Transact-SQL ���</returns>
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
