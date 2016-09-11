using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Collections;

namespace SZCG.GPS.DAL.GPS
{
	public class DataAccess
	{
        private static readonly string Connection = System.Configuration.ConfigurationManager.AppSettings.Get("CONN_GPS_STRING");
		// Hashtable to store cached parameters
		private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
	
		///ִ�д洢���� ������������� �������֮���ã�����
		///param Ϊ�洢���̵Ĳ���  ��������д��Ӧ��ҵ����
		///param[i]=new SqlParameter(name,SqlDbType.NVarChar,100);
		///param[i].Direction=ParameterDirection.Input;
		///param[i].value="";
		public static string ExecuteStoreProcedure1(string procedureName,SqlParameter[] input,SqlParameter[] output)
		{
			using (SqlConnection conn = new SqlConnection(Connection))
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				string result="";
				SqlCommand command = new SqlCommand(procedureName,conn);
				command.CommandType=CommandType.StoredProcedure;
				if(input!=null)
				{
					for(int i=0;i<input.Length;i++)
					{  
						command.Parameters.Add(input[i]);
					}
				}
				if(output!=null)
				{
					for(int i=0;i<output.Length;i++)
					{
						command.Parameters.Add(output[i]);
					}
				}
					
				command.ExecuteNonQuery();

				if(output!=null)
				{
					for(int i=0;i<output.Length;i++)
					{
						string paramName=output[i].ParameterName;
						result=result+Convert.ToString(command.Parameters[paramName].Value)+";";
					}
					if(output.Length>0)
					{
						result=result.Substring(0,result.Length-1);
					}
				}
				conn.Close();
				return result;
			}
		}

		public static void SendMessage(string srvno, string desmp, string msg)
		{
		
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				if (conn.State != ConnectionState.Open)
					conn.Open();

				SqlCommand comm = new SqlCommand("msg_remotelogin",conn);
				comm.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@srvno",SqlDbType.NVarChar,20);
				param1.Direction = ParameterDirection.Input;
				param1.Value = srvno;

				SqlParameter param2 = new SqlParameter("@desmp",SqlDbType.NVarChar,20);
				param2.Direction = ParameterDirection.Input;
				param2.Value = desmp;

				SqlParameter param3 = new SqlParameter("@msg",SqlDbType.NVarChar, 70);
				param3.Direction = ParameterDirection.Input;
				param3.Value = msg;

				comm.Parameters.Add(param1);
				comm.Parameters.Add(param2);
				comm.Parameters.Add(param3);
				comm.ExecuteNonQuery();

				conn.Close();

			}

		}
		/// <summary>
		/// ִ�з���READER�Ĵ洢����
		/// </summary>
		/// <param name="procedureName">�洢��������</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns></returns>
     	public static SqlDataReader ExecuteStoredProcedure2(string procedureName,SqlParameter[] cmdParms)
		{
			SqlConnection conn = new SqlConnection(Connection);
			if (conn.State == ConnectionState.Closed)
			{
				
					conn.Open();
				
			}
			try 
			{
				SqlCommand command = new SqlCommand(procedureName, conn);
				command.CommandType=CommandType.StoredProcedure;
				if(cmdParms!=null)
				{
					for(int i=0;i<cmdParms.Length;i++)
					{
						command.Parameters.Add(cmdParms[i]);
					}
				}

				SqlDataReader rs = command.ExecuteReader(CommandBehavior.CloseConnection);
				command.Parameters.Clear();
				return rs;
			}
			catch 
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
				throw;
			}
		}

		/// <summary>
		/// ִ������������Ĵ洢����
		/// </summary>
		/// <param name="procedureName">�洢��������</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns>�Ƿ�ִ�гɹ�</returns>
		public static int ExecuteStoredProcedure(string procedureName, SqlParameter[] cmdParms)
		{
			using (SqlConnection conn = new SqlConnection(Connection)) 
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				SqlCommand command = new SqlCommand(procedureName,conn);
				command.CommandType=CommandType.StoredProcedure;
				if(cmdParms!=null)
				{
					for(int i=0;i<cmdParms.Length;i++)
					{
						command.Parameters.Add(cmdParms[i]);
					}
				}
				int val=command.ExecuteNonQuery();
				command.Parameters.Clear();
				conn.Close();
				return val;
			}
		}

		/// <summary>
		/// ִ�и��µ�SQL���
		/// </summary>
		/// <param name="cmdText">SQL���</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns>���µļ�¼��</returns>
		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) 
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				if (conn.State != ConnectionState.Open)
					conn.Open();
				SqlCommand command = new SqlCommand(cmdText,conn);
				
				PrepareCommand(command,conn, cmdText, cmdParms);
				int val = command.ExecuteNonQuery();
				command.Parameters.Clear();
				return val;
			}
		}

		/// <summary>
		/// ִ�з���READER��SQL���
		/// </summary>
		/// <param name="cmdText">SQL���</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns></returns>
	   public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] cmdParms) 
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = new SqlConnection(Connection);

			try 
			{
				PrepareCommand(cmd,conn,cmdText,cmdParms);
				SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				cmd.Parameters.Clear();
				return rdr;
			}
			catch 
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
				throw;
			}
		}

		/// <summary>
		/// ִ�з���DATASET��SQL���
		/// </summary>
		/// <param name="sql">SQL���</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns>DataSet����</returns>
		public static DataSet ExecuteDataSet( string sql, params SqlParameter[] cmdParms)
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				if (conn.State != ConnectionState.Open)
					conn.Open();
				DataSet dataSet=new DataSet();
				SqlCommand command=new SqlCommand(sql,conn);
				if (cmdParms != null) 
				{
					for(int i=0;i<cmdParms.Length;i++)
					{
						command.Parameters.Add(cmdParms[i]);
					}					
				}
				SqlDataAdapter adapter=new SqlDataAdapter(command);
				adapter.Fill(dataSet);
				command.Parameters.Clear();
				conn.Close();
				return dataSet;
			}
		}

		/// <summary>
		/// ִ�з��ص�һ�е�һ�е�SQL���
		/// </summary>
		/// <param name="cmdText">SQL���</param>
		/// <param name="cmdParms">SQL����</param>
		/// <returns>��һ�е�һ�е�ֵ</returns>
		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) 
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				if (conn.State != ConnectionState.Open)
					conn.Open();
				SqlCommand cmd = new SqlCommand();
				PrepareCommand(cmd, conn, cmdText, cmdParms);
				object val = cmd.ExecuteScalar();
				cmd.Parameters.Clear();
				conn.Close();
				return val;
			}
		}
        /// <summary>
        /// ��Hashtable�洢SQL����
        /// </summary>
        /// <param name="cacheKey">��</param>
        /// <param name="cmdParms">SQL����</param>
		public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms) 
		{
			parmCache[cacheKey] = cmdParms;
		}
        /// <summary>
        /// ��ȡ�洢��SQL����
        /// </summary>
        /// <param name="cacheKey">��</param>
        /// <returns>SQL����</returns>
		public static SqlParameter[] GetCachedParameters(string cacheKey) 
		{
			SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
			
			if (cachedParms == null)
				return null;
			
			SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

			for (int i = 0, j = cachedParms.Length; i < j; i++)
				clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

			return clonedParms;
		}
		/// <summary>
		/// ��Ӳ��� ������
		/// </summary>
		/// <param name="cmd">SqlCommand����</param>
		/// <param name="conn">���Ӷ���</param>
		/// <param name="cmdText">SQL���</param>
		/// <param name="cmdParms">SQL����</param>
		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, string cmdText, SqlParameter[] cmdParms) 
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();

			cmd.Connection = conn;
			cmd.CommandText = cmdText;

			
			if (cmdParms != null) 
			{
				for(int i=0;i<cmdParms.Length;i++)
				{
					cmd.Parameters.Add(cmdParms[i]);
				}
			}
		}
		/// <summary>
		/// ��������ִ�ж���SQL���
		/// </summary>
		/// <param name="sqls">SQL���</param>
		/// <param name="input">SQL����</param>
		/// <returns>�Ƿ�ִ�гɹ�</returns>
		public static bool ExecuteNonQueryForSqlGroup(string[] sqls,SqlParameter[][] input)
		{
			if(sqls.Length!=input.Length)
			{
				return false;
			}
			
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				SqlTransaction sqlTransaction=null;
				try
				{
				   sqlTransaction=conn.BeginTransaction();
					for(int i=0;i<sqls.Length;i++)
					{
						SqlCommand command = new SqlCommand(sqls[i],conn);
						command.Transaction=sqlTransaction;
						PrepareCommand(command,conn, sqls[i], input[i]);
						int val = command.ExecuteNonQuery();
						command.Parameters.Clear();
					}

                  sqlTransaction.Commit();
					return true;
				}
				catch(Exception e)
				{
                   System.Diagnostics.Debug.WriteLine(e.Message);
				   sqlTransaction.Rollback();
                   return false;
				}
			}
		}

		/// <summary>
		/// ��ȡSqlDataReader����,ʹ��ǰ�����ж��Ƿ�Ϊ��,ʹ�������ر�reader
		/// </summary>
		/// <param name="sql">Ҫִ�е�sql���</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader getDataReader(String sql)
		{
			SqlConnection conn=new SqlConnection(Connection);
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			SqlDataReader sqlReader = null;
			SqlCommand sqlCommand = new SqlCommand(sql,conn);			
			sqlReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);				
			return sqlReader;
		}


		/// <summary>
		/// ��ȡָ��ҳ�������
		/// </summary>
		/// <param name="Sql">��ѯ�ַ���</param>
		/// <param name="PageSize">ÿҳ��ʾ������</param>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="Field">������ֶ�����</param>
		/// <param name="Order">����ķ�ʽ</param>
		/// <param name="PageCount">���ݼ��ܵ�ҳ��</param>
		/// <returns>���ؾ���ָ��ҳ���С��reader���ݼ�</returns>
		public static SqlDataReader getPageData(String Sql,int PageSize,int CurrentPage,
			String Field,String Order,int RowCount,int PageCount)
		{
//			using(SqlConnection conn=new SqlConnection(Connection))
//			{
			SqlConnection conn=new SqlConnection(Connection);
			try
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				SqlDataReader reader = null;

				SqlCommand comm = new SqlCommand("PageData",conn);
				comm.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@Query",SqlDbType.NVarChar,1024);
				param1.Direction = ParameterDirection.Input;
				param1.Value = Sql;

				SqlParameter param2 = new SqlParameter("@PageSize",SqlDbType.Int,4);
				param2.Direction = ParameterDirection.Input;
				param2.Value = PageSize;

				SqlParameter param3 = new SqlParameter("@CurrentPage",SqlDbType.Int,4);
				param3.Direction = ParameterDirection.Input;
				param3.Value = CurrentPage;

				SqlParameter param4 = new SqlParameter("@Field",SqlDbType.NVarChar,20);
				param4.Direction = ParameterDirection.Input;
				param4.Value = Field;

				SqlParameter param5 = new SqlParameter("@Order",SqlDbType.NVarChar,10);
				param5.Direction = ParameterDirection.Input;
				param5.Value = Order;

				SqlParameter param6 = new SqlParameter("@RowCount",SqlDbType.Int,4);
				param6.Direction = ParameterDirection.Input;
				param6.Value = RowCount;

				SqlParameter param7 = new SqlParameter("@PageCount",SqlDbType.Int,4);
				param7.Direction = ParameterDirection.Input;
				param7.Value = PageCount;

				comm.Parameters.Add(param1);
				comm.Parameters.Add(param2);
				comm.Parameters.Add(param3);
				comm.Parameters.Add(param4);
				comm.Parameters.Add(param5);
				comm.Parameters.Add(param6);
				comm.Parameters.Add(param7);

				reader = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);		
				return reader;
			}
			catch
			{
				conn.Close();
				throw;
			}
//			}
		}

		/// <summary>
		/// ��ȡָ�����ݼ���������ҳ��
		/// </summary>
		/// <param name="Sql">��ѯ�ַ���</param>
		/// <param name="PageSize">ÿҳ��ʾ������</param>
		/// <param name="RowCount">���ݼ��ܵ�����</param>
		/// <param name="PageCount">���ݼ��ܵ�ҳ��</param>
		public static void cutPage(String Sql,int PageSize,out int RowCount,out int PageCount)
		{
		  using(SqlConnection conn=new SqlConnection(Connection))
		
		  {
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				SqlCommand comm = new SqlCommand("CutPage",conn);
				comm.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@Query",SqlDbType.NVarChar,1024);
				param1.Direction = ParameterDirection.Input;
				param1.Value = Sql;

				SqlParameter param2 = new SqlParameter("@PageSize",SqlDbType.Int,4);
				param2.Direction = ParameterDirection.Input;
				param2.Value = PageSize;

				SqlParameter param3 = new SqlParameter("@RowCount",SqlDbType.Int,4);
				param3.Direction = ParameterDirection.Output;

				SqlParameter param4 = new SqlParameter("@PageCount",SqlDbType.Int,4);
				param4.Direction = ParameterDirection.Output;

				comm.Parameters.Add(param1);
				comm.Parameters.Add(param2);
				comm.Parameters.Add(param3);
				comm.Parameters.Add(param4);

				comm.ExecuteNonQuery();
				RowCount = (int)comm.Parameters["@RowCount"].Value;
				PageCount = (int)comm.Parameters["@PageCount"].Value;
			    conn.Close();
			}


			
		}


		public static string StrConv(string strIn, string encoding) 
		{ 
			return System.Web.HttpUtility.UrlEncode(strIn,System.Text.Encoding.GetEncoding(encoding)); 
		} 


		public static bool ExecuteNonQueryForSqlGroup(string[] sqls)
		{
			
			
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				
				if (conn.State != ConnectionState.Open)
					conn.Open();

				try
				{
			
					for(int i=0;i<sqls.Length;i++)
					{
						SqlCommand command = new SqlCommand(sqls[i],conn);
						
						int val = command.ExecuteNonQuery();
						
					}

					conn.Close();

					return true;
				}
				catch(Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
					
					return false;
				}
				
				
				
			}
		}
		
	}
}