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
	
		///执行存储过程 并返回输出参数 输出参数之间用；隔开
		///param 为存储过程的参数  具体设置写在应用业务里
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
		/// 执行返回READER的存储过程
		/// </summary>
		/// <param name="procedureName">存储过程名称</param>
		/// <param name="cmdParms">SQL参数</param>
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
		/// 执行无输出参数的存储过程
		/// </summary>
		/// <param name="procedureName">存储过程名称</param>
		/// <param name="cmdParms">SQL参数</param>
		/// <returns>是否执行成功</returns>
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
		/// 执行更新的SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="cmdParms">SQL参数</param>
		/// <returns>更新的记录数</returns>
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
		/// 执行返回READER的SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="cmdParms">SQL参数</param>
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
		/// 执行返回DATASET的SQL语句
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="cmdParms">SQL参数</param>
		/// <returns>DataSet对象</returns>
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
		/// 执行返回第一行第一列的SQL语句
		/// </summary>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="cmdParms">SQL参数</param>
		/// <returns>第一行第一列的值</returns>
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
        /// 用Hashtable存储SQL参数
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cmdParms">SQL参数</param>
		public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms) 
		{
			parmCache[cacheKey] = cmdParms;
		}
        /// <summary>
        /// 获取存储的SQL参数
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns>SQL参数</returns>
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
		/// 添加参数 打开连接
		/// </summary>
		/// <param name="cmd">SqlCommand对象</param>
		/// <param name="conn">连接对象</param>
		/// <param name="cmdText">SQL语句</param>
		/// <param name="cmdParms">SQL参数</param>
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
		/// 运用事务执行多条SQL语句
		/// </summary>
		/// <param name="sqls">SQL语句</param>
		/// <param name="input">SQL参数</param>
		/// <returns>是否执行成功</returns>
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
		/// 获取SqlDataReader对象,使用前请先判断是否为空,使用完毕请关闭reader
		/// </summary>
		/// <param name="sql">要执行的sql语句</param>
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
		/// 获取指定页面的数据
		/// </summary>
		/// <param name="Sql">查询字符串</param>
		/// <param name="PageSize">每页显示的条数</param>
		/// <param name="CurrentPage">当前要显示的页数</param>
		/// <param name="Field">排序的字段名称</param>
		/// <param name="Order">排序的方式</param>
		/// <param name="PageCount">数据集总的页数</param>
		/// <returns>返回具有指定页面大小的reader数据集</returns>
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
		/// 获取指定数据集的行数和页数
		/// </summary>
		/// <param name="Sql">查询字符串</param>
		/// <param name="PageSize">每页显示的条数</param>
		/// <param name="RowCount">数据集总的行数</param>
		/// <param name="PageCount">数据集总的页数</param>
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