using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Collections;

namespace szcg.com.teamax.util
{
	public class DataAccess
	{
		private static readonly string Connection = ConfigSettings.Connection;

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

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="srvno"></param>
		/// <param name="desmp">Ŀ�ĺ���</param>
		/// <param name="msg">��Ϣ����</param>
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

				SqlParameter param3 = new SqlParameter("@msg",SqlDbType.NVarChar, 300);
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
		public static int ExecuteStoredProcedure(string procedureName,params SqlParameter[] cmdParms)
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
				comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
				comm.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@Query",SqlDbType.NVarChar,4000);
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


        #region cutPage����ȡָ�����ݼ���������ҳ��
        /// <summary>
        /// ��ȡָ�����ݼ���������ҳ��
        /// </summary>
        /// <param name="Sql">��ѯ�ַ���</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="RowCount">���ݼ��ܵ�����</param>
        /// <param name="PageCount">���ݼ��ܵ�ҳ��</param>
        public static void cutPage(String Sql, int PageSize, out int RowCount, out int PageCount)
        {
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand comm = new SqlCommand("CutPage", conn);
                comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
                comm.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter("@Query", SqlDbType.NVarChar, 4000);
                param1.Direction = ParameterDirection.Input;
                param1.Value = Sql;

                SqlParameter param2 = new SqlParameter("@PageSize", SqlDbType.Int, 4);
                param2.Direction = ParameterDirection.Input;
                param2.Value = PageSize;

                SqlParameter param3 = new SqlParameter("@RowCount", SqlDbType.Int, 4);
                param3.Direction = ParameterDirection.Output;

                SqlParameter param4 = new SqlParameter("@PageCount", SqlDbType.Int, 4);
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
        #endregion

        /*

		#region GetDbProjectDS: ��ȡ���참���б�
		/// <summary>
		/// ��ȡ���참���б�,����DataSet(2007-03-09)
		/// </summary>
		/// <param name="areacode">����</param>
		/// <param name="Field">������ֶ�����</param>
		/// <param name="Order">����ķ�ʽ</param>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <returns>����DataSet:����ָ��ҳ���С�����ݼ�</returns>
		/// <remarks>
		/// ��д����:2007-03-09
		/// </remarks>
		public static DataTable GetDbProjectDS(string dbname,string projcodes,string times1,string times2,string areacode,int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order)
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				try
				{
					if (conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetDbProjectList",conn);
					comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@dbname",dbname);
					SqlParameter param2 = new SqlParameter("@projcodes",projcodes);
					SqlParameter param3 = new SqlParameter("@times1",times1);
					SqlParameter param4 = new SqlParameter("@times2",times2);
					SqlParameter param5 = new SqlParameter("@areacode",areacode);
					SqlParameter param6 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param7 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param7.Direction = ParameterDirection.Output;
					param7.Value = RowCount;
					SqlParameter param8 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param8.Direction = ParameterDirection.Output;
					param8.Value = PageCount;
					SqlParameter param9 = new SqlParameter("@Field",Field);
					SqlParameter param10 = new SqlParameter("@Order",Order);


					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);
					comm.Parameters.Add(param10);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					conn.Close();
					throw;
				}
			}
		}
		#endregion

		#region GetYJProjectDS: ��ȡ�ƽ����������б�
		/// <summary>
		/// ��ȡ�ƽ����������б�,����DataSet
		/// </summary>
		/// <param name="currentRole">�û����</param>
		/// <param name="Field">������ֶ�����</param>
		/// <param name="Order">����ķ�ʽ</param>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <returns>����DataSet:����ָ��ҳ���С�����ݼ�</returns>
		/// <remarks>
		/// ��д����:2007-03-14
		/// </remarks>
		public static DataTable GetYJProjectDS(string times1, string times2, string projcodes,int step,string userCode,string currentRole,string Field,string Order,int CurrentPage,ref int RowCount,ref int PageCount)
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				try
				{
					if (conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetYJProjectList",conn);
					comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@currentRole",currentRole);
					SqlParameter param2 = new SqlParameter("@Field",Field);
					SqlParameter param3 = new SqlParameter("@Order",Order);
					SqlParameter param4 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param5 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param5.Direction = ParameterDirection.Output;
					param5.Value = RowCount;
					SqlParameter param6 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param6.Direction = ParameterDirection.Output;
					param6.Value = PageCount;
					SqlParameter param7 = new SqlParameter("@times1", times1);
					SqlParameter param8 = new SqlParameter("@times2", times2);
					SqlParameter param9 = new SqlParameter("@projcodes", projcodes);
					SqlParameter param10 = new SqlParameter("@step",step);
					SqlParameter param11 = new SqlParameter("@UserCode",userCode);


					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);
					comm.Parameters.Add(param10);
					comm.Parameters.Add(param11);
			

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					conn.Close();
					throw;
				}
			}
		}
		#endregion


		#region ExecuteNonQueryForSqlGroup��ִ��SQL�����
		/// <summary>
		/// ִ��SQL�����
		/// </summary>
		/// <param name="sqls"></param>
		/// <returns></returns>
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
		#endregion

		#region GetDjDT:��̬����,��ȡ�������б�
		/// <summary>
		/// GetDjDT:��̬����,��ȡ�������б�
		/// </summary>
		/// <param name="actioncode">�������</param>
		/// <param name="step">�����</param>
		/// <param name="areacode">����</param>
		/// <param name="cu_role">��ǰ��ɫ��</param>
		/// <param name="CurrentPage">��ǰҳ</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <returns></returns>
		public static DataTable GetDjDT(string actioncode,string step,string areacode,string cu_role,string projcode,string times1,string times2,int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetDjProjectList",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@areacode",areacode);
					SqlParameter param2 = new SqlParameter("@step",step);
					SqlParameter param3 = new SqlParameter("@actioncode",actioncode);
					SqlParameter param4 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param5 = new SqlParameter("@cu_role",cu_role);
					SqlParameter param6 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					
					param6.Direction = ParameterDirection.Output;
					param6.Value = RowCount;
					SqlParameter param7 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param7.Direction = ParameterDirection.Output;
					param7.Value = PageCount;

					SqlParameter param8 = new SqlParameter("@projcode",projcode);
					SqlParameter param9 = new SqlParameter("@times1",times1);
					SqlParameter param10 = new SqlParameter("@times2",times2);

					SqlParameter param11 = new SqlParameter("@Field",Field);
					SqlParameter param12 = new SqlParameter("@Order",Order);


					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);
					comm.Parameters.Add(param10);
					comm.Parameters.Add(param11);
					comm.Parameters.Add(param12);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetPubProjectList:��ȡ���ھٱ��İ����б�
		/// <summary>
		/// GetPubProjectList:��ȡ���ھٱ��İ����б�
		/// </summary>
		/// <param name="ip">�����û��ĵ�ַ</param>
		/// <param name="areacode">����</param>
		/// <param name="CurrentPage">��ǰҳ</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <returns></returns>
		public static DataTable GetPubProjectList(string times1,string times2,string projcodes,
			string areacode,int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order)
		{
			string groupid = new szcg.com.teamax.business.BUSINESS_ProjectManage().getGroupId(areacode);
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetPubProjectList",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@groupid",groupid);
					SqlParameter param2 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param3 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param3.Direction = ParameterDirection.Output;
					param3.Value = RowCount;
					SqlParameter param4 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param4.Direction = ParameterDirection.Output;
					param4.Value = PageCount;
					SqlParameter param5 = new SqlParameter("@times1",times1);
					SqlParameter param6 = new SqlParameter("@times2",times2);
					SqlParameter param7 = new SqlParameter("@projcodes",projcodes);
					SqlParameter param8 = new SqlParameter("@Field",Field);
					SqlParameter param9 = new SqlParameter("@Order",Order);

					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetProjectDetail:��ȡ��������ϸ��Ϣ
		/// <summary>
		/// ��ȡ��������ϸ��Ϣ
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <returns></returns>
		public static DataTable GetProjectDetail(string projcode)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetProjectDetail",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@projcode",projcode);
					comm.Parameters.Add(param1);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetProjectOtherInfo:��ȡ������������Ϣ
		/// <summary>
		/// ��ȡ������������Ϣ
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <returns></returns>
		public static DataTable GetProjectOtherInfo(string projcode,string probsource)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetProjectOtherInfo",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@projcode",projcode);
					comm.Parameters.Add(param1);
					SqlParameter param2 = new SqlParameter("@probsource",probsource);
					comm.Parameters.Add(param2);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetQueryProList:��ȡ��ѯ��İ������б�
		/// <summary>
		/// ��ȡ��ѯ��İ������б�
		/// </summary>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <returns>��table����ʽ���ز�ѯ���Ľ����</returns>
		public static DataTable GetQueryProList(string probsource,string state,string probclass,
			string bigclass,string smallclass,string street,
			string square,string times1,string times2,
			string projcode,string address,string areacode,
			string collcode,string area,string departcode,
			int CurrentPage,ref int RowCount,ref int PageCount,
			string Field,string Order)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();

					SqlParameter [] input = new SqlParameter[20];
					input[0]= new SqlParameter("@areacode",areacode);
					input[1]= new SqlParameter("@probsource",probsource);
					input[2]= new SqlParameter("@state",state);
					input[3]= new SqlParameter("@probclass",probclass);
					input[4]= new SqlParameter("@bigclass",bigclass);
					input[5]= new SqlParameter("@smallclass",smallclass);
					input[6] = new SqlParameter("@area",area);
					input[7]= new SqlParameter("@street",street);
					input[8]= new SqlParameter("@square",square);
					input[9]= new SqlParameter("@times1",times1);
					input[10]= new SqlParameter("@times2",times2);
					input[11]= new SqlParameter("@projcode",projcode);
					input[12] = new SqlParameter("@address",address);
					input[13] = new SqlParameter("@collcode",collcode);
					input[14] = new SqlParameter("@departcode",departcode);
					input[15]  = new SqlParameter("@CurrentPage",CurrentPage);
					input[16] = new SqlParameter("@RowCount",SqlDbType.Int);
					input[16].Direction = ParameterDirection.Output;
					input[16].Value = RowCount;
					input[17] = new SqlParameter("@PageCount",SqlDbType.Int);
					input[17].Direction = ParameterDirection.Output;
					input[17].Value = PageCount;
					input[18] = new SqlParameter("@Order",Order);
					input[19] = new SqlParameter("@Field",Field); 

					SqlCommand comm = new SqlCommand("QueryProject",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;
					for(int i=0;i<input.Length;i++)
						if(input[i]!=null)
							comm.Parameters.Add(input[i]);

					DataTable dt = new DataTable();
					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetProjTrace:��ȡ����������
		/// <summary>
		///  GetProjTrace:��ȡ����������
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <returns></returns>
		public static DataTable GetProjTrace(string projcode)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetProjTrace",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@projcode",projcode);

					comm.Parameters.Add(param1);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);

					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region GetCDProjList:��ȡ�浵�����б�
		/// <summary>
		///  GetCDProjList:��ȡ�浵�����б�
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <returns></returns>
		public static DataTable GetCDProjList(string areacode,string usercode,string probsource,string startTime,string endTime,int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order,int step,string currentRole)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetCDProjList",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@areacode",areacode);
					SqlParameter param2 = new SqlParameter("@UserCode",usercode);
					SqlParameter param3 = new SqlParameter("@probsource",probsource);
					SqlParameter param4 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param5 = new SqlParameter("@startTime",startTime);
					SqlParameter param6 = new SqlParameter("@endTime",endTime);
					SqlParameter param7 = new SqlParameter("@step",step);
					SqlParameter param8 = new SqlParameter("@CurrentRole",currentRole);

					SqlParameter param9 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param9.Direction = ParameterDirection.Output;
					param9.Value = RowCount;
					SqlParameter param10 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param10.Direction = ParameterDirection.Output;
					param10.Value = PageCount;

					SqlParameter param11 = new SqlParameter("@Field",Field);
					SqlParameter param12 = new SqlParameter("@Order",Order);

					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);
					comm.Parameters.Add(param10);
					comm.Parameters.Add(param11);
					comm.Parameters.Add(param12);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);

					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region  getProjDetail��ȡ�ð������ϸ��Ϣ
		/// <summary>
		/// ȡ�ð������ϸ��Ϣ
		/// </summary>
		/// <param name="id">������</param>
		/// <returns></returns>
		public static DataSet getProjDetail(string projcode)
		{
			using(SqlConnection conn = new SqlConnection(Connection))
			{   
				try
				{
					if(conn.State == ConnectionState.Closed)
						conn.Open();
					DataSet ds = new DataSet();
					SqlCommand comm = new SqlCommand("GetProjDetail",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@projcode",projcode);
					comm.Parameters.Add(param1);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(ds);

					return ds;
				}
				catch
				{
					throw;
				}
				finally
				{
					conn.Close();
				}                
			}
		}
		#endregion

		#region ChangeProcessType:�ı䰸���Ĵ������ͣ�������0����ʾһ���Դ��� 1�������Դ��� 2�������Դ��� �¼���0����ʾһ���Դ��� 1���ۺ��Դ���
		/// <summary>
		/// �ı䰸���Ĵ�������
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <param name="usercode">�û����</param>
		/// <param name="rolecode">��ɫ��</param>
		/// <param name="RequestProcessType">�����������</param>
		/// <param name="RequestContent">�������</param>
		public static void ChangeProcessType(string projcode,string usercode,string rolecode,
			                            string RequestProcessType,string RequestContent)
		{
     	  string sql = string.Format(@" update b_project set RequestProcessType = {0}
										where projcode = {1} ;
										insert into projtrace(projcode,stepid,actionname,
											cu_date,usercode,_opinion,returntracetag,roleid)
										values({2},'4','������Ĵ�������',
												GetDate(),{3},'{4}','',{5})",RequestProcessType,projcode,projcode,
			                                    usercode,RequestContent,rolecode);
		 DataAccess.ExecuteNonQuery(sql,null);
		}
		#endregion

		#region JudgeIsSended:�ж��Ƿ��Ѿ����͹�������ʱ������
		/// <summary>
		/// �ж��Ƿ��Ѿ����͹�������ʱ������
		/// </summary>
		/// <param name="projcode">�������</param>
		public static object JudgeIsSended(string projcode)
		{
			string sql = string.Format(@"	select IsNull(cast(ProcessType as varchar(10)),'')+','+IsNull(cast(RequestProcessType as varchar(10)),'')
											from b_project 
											where projcode = {0} ",projcode);
			return DataAccess.ExecuteScalar(sql,null);
		}
		#endregion

		#region GetRequestProjectList: ��ȡ�����б�
		/// <summary>
		/// �ලָ��������ǲԱ����ȡҪ�����İ����б�����DataSet
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <param name="times1">��ѯ��ʼʱ��</param>
		/// <param name="times2">����ʱ��</param>
		/// <param name="areacode">����</param>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <param name="Field">������ֶ�����</param>
		/// <param name="Order">����ķ�ʽ</param>
		/// <returns>����DataSet:����ָ��ҳ���С�����ݼ�</returns>
		/// <remarks>
		/// ��д����:2007-04-11
		/// </remarks>
		public static DataTable GetRequestProjectList(string projcode,string times1,string times2,string areacode,
				int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order)
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				try
				{
					if (conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetRequestProjectList",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@projcode",projcode);
					SqlParameter param2 = new SqlParameter("@times1",times1);
					SqlParameter param3 = new SqlParameter("@times2",times2);
					SqlParameter param4 = new SqlParameter("@areacode",areacode);
					SqlParameter param5 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param6 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param6.Direction = ParameterDirection.Output;
					param6.Value = RowCount;
					SqlParameter param7 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param7.Direction = ParameterDirection.Output;
					param7.Value = PageCount;
					SqlParameter param8 = new SqlParameter("@Field",Field);
					SqlParameter param9 = new SqlParameter("@Order",Order);


					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					conn.Close();
					throw;
				}
			}
		}
		#endregion

		#region GetRequestInfo:��ȡ����ı䰸���������͵���Ϣ
		/// <summary>
		/// ��ȡ����ı䰸���������͵���Ϣ
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <returns></returns>
		public static DataTable GetRequestInfo(string projcode)
		{
		  string sql = string.Format(@"	select top 1 B.projname,A.RequestProcessType,B.probclass,D.userName,C._opinion 
										from b_project A ,b_project_detail B,projtrace C,Loginuser D
										where A.projcode = B.projcode 
											and A.projcode = C.projcode
											and D.usercode = C.usercode
											and A.projcode = {0} 
											and actionname like '%������Ĵ�������%'
									 order by C.id desc",projcode);
		  DataSet ds= new DataSet();
		  ds = DataAccess.ExecuteDataSet(sql,null);
		  return ds.Tables[0];
		}
		#endregion

		#region ResponseProcessType:��Ӧ�ı䰸���Ĵ������ͣ�������0����ʾһ���Դ��� 1�������Դ��� 2�������Դ��� �¼���0����ʾһ���Դ��� 1���ۺ��Դ���
		/// <summary>
		/// �ı䰸���Ĵ�������
		/// </summary>
		/// <param name="projcode">�������</param>
		/// <param name="usercode">�û����</param>
		/// <param name="rolecode">��ɫ��</param>
		/// <param name="RequestProcessType">�����������</param>
		/// <param name="RequestContent">�������</param>
		/// /// <param name="flag">��ʶ�Ƿ�ͬ��������0����ʾͬ�⣻1����ʾ��ͬ��</param>
		public static void ResponseProcessType(string projcode,string usercode,string rolecode,
			string RequestProcessType,string ResponseContent,string flag)
		{
			string sql = "";
			if(flag == "0")
				sql = string.Format(@"	update b_project 
												set ProcessType = RequestProcessType,
													RequestProcessType = null
												where projcode ={0}  ;

												insert into projtrace(projcode,stepid,actionname,
													cu_date,usercode,_opinion,returntracetag,roleid)
												values({1},'3','ͬ����Ĵ�������',
													GetDate(),{2},'{3}','',{4})",
					projcode,projcode,usercode,ResponseContent,rolecode);
			else
				sql = string.Format(@"	update b_project 
												set RequestProcessType = null
												where projcode ={0}  ;

												insert into projtrace(projcode,stepid,actionname,
													cu_date,usercode,_opinion,returntracetag,roleid)
												values({1},'3','��ͬ����Ĵ�������',
													GetDate(),{2},'{3}','',{4})",
					projcode,projcode,usercode,ResponseContent,rolecode);			
			
			
			DataAccess.ExecuteNonQuery(sql,null);
		}
		#endregion

		#region GetDepartProjectList: ��ȡ����ͳ�Ƶİ����б�
		/// <summary>
		///��ȡ����ͳ�Ƶİ����б�����DataSet
		/// </summary>
		/// <param name="rolecode">��ɫ���</param>
		/// <param name="state">״̬���Ѵ���/δ����</param>
		/// <param name="times1">��ѯ��ʼʱ��</param>
		/// <param name="times2">����ʱ��</param>
		/// <param name="CurrentPage">��ǰҪ��ʾ��ҳ��</param>
		/// <param name="RowCount">������</param>
		/// <param name="PageCount">��ҳ��</param>
		/// <param name="Field">������ֶ�����</param>
		/// <param name="Order">����ķ�ʽ</param>
		/// <returns>����DataSet:����ָ��ҳ���С�����ݼ�</returns>
		/// <remarks>
		/// ��д����:2007-04-13
		/// </remarks>
		public static DataTable GetDepartProjectList(string rolecode,string state,string times1,string times2,
			int CurrentPage,ref int RowCount,ref int PageCount,string Field,string Order)
		{
			using(SqlConnection conn=new SqlConnection(Connection))
			{
				try
				{
					if (conn.State == ConnectionState.Closed)
						conn.Open();
					DataTable dt = new DataTable();
					SqlCommand comm = new SqlCommand("GetDepartProjectList",conn);
                    comm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
					comm.CommandType = CommandType.StoredProcedure;

					SqlParameter param1 = new SqlParameter("@rolecode",rolecode);
					SqlParameter param2 = new SqlParameter("@state",state);
					SqlParameter param3 = new SqlParameter("@times1",times1);
					SqlParameter param4 = new SqlParameter("@times2",times2);
					SqlParameter param5 = new SqlParameter("@CurrentPage",CurrentPage);
					SqlParameter param6 = new SqlParameter("@RowCount",SqlDbType.Int,4);
					param6.Direction = ParameterDirection.Output;
					param6.Value = RowCount;
					SqlParameter param7 = new SqlParameter("@PageCount",SqlDbType.Int,4);
					param7.Direction = ParameterDirection.Output;
					param7.Value = PageCount;
					SqlParameter param8 = new SqlParameter("@Field",Field);
					SqlParameter param9 = new SqlParameter("@Order",Order);


					comm.Parameters.Add(param1);
					comm.Parameters.Add(param2);
					comm.Parameters.Add(param3);
					comm.Parameters.Add(param4);
					comm.Parameters.Add(param5);
					comm.Parameters.Add(param6);
					comm.Parameters.Add(param7);
					comm.Parameters.Add(param8);
					comm.Parameters.Add(param9);

					SqlDataAdapter adapter=new SqlDataAdapter(comm);
					adapter.Fill(dt);
					RowCount = (int)comm.Parameters["@RowCount"].Value;
					PageCount = (int)comm.Parameters["@PageCount"].Value;

					return dt;
				}
				catch
				{
					conn.Close();
					throw;
				}
			}
		}
		#endregion
         * 
         */
	}
}
