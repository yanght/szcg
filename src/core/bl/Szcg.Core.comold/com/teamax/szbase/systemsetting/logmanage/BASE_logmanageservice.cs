using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using szcg.com.teamax.util;
using System.IO;
using szcg.com.teamax.szbase.entity;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.systemsetting.logmanage
{
	public class BASE_logmanageservice
	{
		private const string FILE_NAME = "e:\\udslog.txt";
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.capability.";
		//存放数据
		protected StringBuilder sb;
		//得到数据库中的结果集
		protected SqlDataReader dr;
		//存放数组数据
		protected ArrayList list;


		#region 组件设计器生成的代码
		
		//Web 服务设计器所必需的
//		private IContainer components = null;
				
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if(disposing && components != null)
//			{
//				components.Dispose();
//			}
//			base.Dispose(disposing);		
//		}
//		
		#endregion

		/// <summary>
		/// 返回一个DataSet
		/// </summary>
		/// <param name="a">一个SQL语句</param>
		/// <returns></returns>
		public static DataSet selectLog(string a)
		{
			DataSet myDataSet =DataAccess.ExecuteDataSet(a,null);
			return myDataSet;
		}

		/// <summary>
		/// 用于登陆页面
		/// </summary>
		public void updateDate(string time,string id)
		{
			string sql = "update userlog set logintime = '"+time+"' where roleid = '"+id+"'";
			DataAccess.ExecuteNonQuery(sql,null);
		}

		/// <summary>
		/// 用于用户日志跟踪
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="usercode"></param>
		/// <param name="modelcode"></param>
		public static void UserLog(int usercode,string modelcode,string sql)
		{	
			StringBuilder msg = new StringBuilder();
			DateTime currentTime = new DateTime();
			currentTime = DateTime.Now;
			string executeName = sql.Substring(0,6);
			if(executeName.Equals("select"))
			{
				executeName = "查询了";
				int a = sql.IndexOf("from");//取得"from"的位置
				string b = " ";
				int d = sql.IndexOf(b,a+5);//取得在"from"之后第二个空格的位置
				string tableName = sql.Substring(a+4,d-(a+4)).Trim();
				msg.Append(executeName);
				msg.Append(tableName+"表");
			}
			else if(executeName.Equals("insert"))
			{
				executeName = "插入";
				//取得"into"的位置
				int a = sql.IndexOf("into");
				//取得第一个"("的位置
				int b = sql.IndexOf("(");
				//取得表名
				string tableName = sql.Substring(a+5,b-(a+5)).Trim();

				msg.Append("对"+tableName+"表进行了"+executeName+"操作");
			}
			else if (executeName.Equals("delete"))
			{
				executeName = "删除";
				string a = " ";
				int b = sql.IndexOf(a);
				int c = sql.IndexOf("where");
				string tableName = sql.Substring(b,c-b).Trim();
				msg.Append("对"+tableName+"表进行了"+executeName+"操作");

			}

			string sql1 = "insert into userlog(usercode,cu_data,modelcode,sqltext)"+
				"values("+usercode+",'"+currentTime+"','"+modelcode+"','"+msg.ToString()+"')";
			DataAccess.ExecuteNonQuery(sql1,null);
			string sql2 = "insert into syslog(usercode,cu_data,_sql,modelcode) values("+usercode+",'"+currentTime+"',@sql,'"+modelcode+"')";
		
			SqlConnection conn = new SqlConnection(ConfigSettings.Connection);
			SqlCommand command=new SqlCommand(sql2,conn);
			command.Parameters.Add("@sql",SqlDbType.VarChar,500);
			command.Parameters["@sql"].Value=sql;
			command.ExecuteNonQuery();
			conn.Close();
		}

		/// <summary>
		/// 记录系统日志至文本文件
		/// </summary>
		/// <param name="message">记录的内容</param>
		public static void SysLog(string message)
		{	//若要将异常写入系统日志表中,只需在下面写SQL语句插入syslog表即可

			if(File.Exists(FILE_NAME))
			{                  
				StreamWriter sr = File.AppendText(FILE_NAME);
				sr.WriteLine ("\n");
				sr.WriteLine (DateTime.Now.ToString()+message);
				sr.Close();
			}
			else
			{
				StreamWriter sr = File.CreateText(FILE_NAME);
				sr.Close();
			}
		}

		

		/// <summary>
		/// 获得操作员数据集
		/// </summary>
		public static String[] getOperator(int userId)
		{
			StringBuilder strSQL;
			strSQL = new StringBuilder();
			try
			{
				//存放数据
				StringBuilder sb;
				//得到数据库中的结果集
				SqlDataReader dr;
				//存放数组数据
				ArrayList list;
				strSQL.Append("	SELECT B.username AS name ");
				strSQL.Append("	FROM  loginuser AS B ");
				dr = DataAccess.ExecuteReader(strSQL.ToString(), null);
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					strSQL.ToString(),
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "getOperator");
				throw;
			}
			finally
			{}
		}

		/// <summary>
		/// 获得操作员数据集
		/// </summary>
		public static String[] getMolder(int userId)
		{
			//存放数据
			StringBuilder sb;
			//得到数据库中的结果集
			SqlDataReader dr;
			//存放数组数据
			ArrayList list;
			StringBuilder strSQL;
			strSQL = new StringBuilder();
			try
			{
				strSQL.Append("	SELECT modelname AS name");
				strSQL.Append("	FROM system_model ");
				dr = DataAccess.ExecuteReader(strSQL.ToString(), null);
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					strSQL.ToString(),
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "getMolder");
				throw;
			}
		}

		/// <summary>
		/// 获得日志记录条数
		/// </summary>
		/// <param name="code">系统日志=0,用户日志=1</param>
		/// <param name="argOperator">操作员名称</param>
		/// <param name="argModel">模块名称</param>
		/// <param name="argStartDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		/// <returns>日志记录数</returns>
		public static int GetLogsCount(
							int userId, 
							string code, 
							string argOperator, 
							string argModel, 
							string argStartDate, 
							string argEndDate)
		{
			try
			{
				int recodeCount = 0;
				if (code == "1") 
				{
					recodeCount = GetUserLogsCount(userId, argOperator, argModel, argStartDate, argEndDate);
				}
				else if (code == "0")
				{
					recodeCount = GetSystemLogsCount(userId, argOperator, argModel, argStartDate, argEndDate);
				}
				return recodeCount;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 获得日志记录
		/// </summary>
		/// <param name="code">系统日志=0,用户日志=1</param>
		/// <param name="pageIndex">页编号</param>
		/// <param name="pageSize">页的记录行数</param>
		/// <param name="argOperator">操作员名称</param>
		/// <param name="argModelname">模块名称</param>
		/// <param name="argStateDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		public static DataSet GetLogs(
						int userId, 
						string code, 
						int pageIndex, 
						int pageSize, 
						string argOperator, 
						string argModelname, 
						string argStateDate, 
						string argEndDate) 
		{
			try
			{
				//EntitySet logs = new EntitySet();
				DataSet dtst = new DataSet();
				if (code == "1") 
				{
					dtst = GetUserLogs(userId, pageIndex, pageSize, argOperator, argModelname, argStateDate, argEndDate);
				}
				else if (code == "0")
				{
					dtst = GetSystemLogs(userId, pageIndex, pageSize, argOperator, argModelname, argStateDate, argEndDate);
				}
				return dtst;
			}
			catch
			{
				throw;
			}
			finally
			{}
		}

		/// <summary>
		/// 获得指定的一条日志记录
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="code">系统日志=0,用户日志=1</param>
		/// <returns>指定的一条日志记录</returns>
		public static DataSet GetLoged(int userId, int Id, string code) 
		{
			try
			{
				DataSet dtst = new DataSet();
				if (code == "1") 
				{
					dtst = GetUserLoged(userId, Id);
				}
				else if (code == "0")
				{
					dtst = GetSystemLoged(userId, Id);
				}
				return dtst;
			}
			catch
			{
				throw;
			}
			finally
			{}
		}

		/// <summary>
		/// 获得用户日志记录条数
		/// </summary>
		/// <param name="argOperator">操作员名称</param>
		/// <param name="argModel">模块名称</param>
		/// <param name="argStartDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		/// <returns>用户日志记录</returns>
		public static int GetUserLogsCount(
							int userId, 
							string argOperator, 
							string argModel, 
							string argStartDate, 
							string argEndDate)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("getUserLogsCount", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@Operator", SqlDbType.VarChar).Value			= argOperator;
					myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value				= argModel;
					myCommand.Parameters.Add("@StateDate", SqlDbType.VarChar).Value			= argStartDate;
					myCommand.Parameters.Add("@EndDate", SqlDbType.VarChar).Value			= argEndDate;
					myConnection.Open();
					SqlDataReader reader = myCommand.ExecuteReader();
					reader.Read();
					int recodeCount = (int)reader["CNT"];
					return recodeCount;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getUserLogsCount",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetUserLogsCount");
					throw;
				}
					finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 获得系统日志记录条数
		/// </summary>
		/// <param name="argOperator"> 操作员Id</param>
		/// <param name="argModel">模块Id</param>
		/// <param name="argStartDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		/// <returns>系统日志记录数</returns>
		public static int GetSystemLogsCount(
							int userId, 
							string argOperator, 
							string argModel, 
							string argStartDate, 
							string argEndDate)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("getSystemLogsCount", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@Operator", SqlDbType.VarChar).Value			= argOperator;
					myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value				= argModel;
					myCommand.Parameters.Add("@StateDate", SqlDbType.VarChar).Value			= argStartDate;
					myCommand.Parameters.Add("@EndDate", SqlDbType.VarChar).Value			= argEndDate;
					myConnection.Open();
					SqlDataReader reader = myCommand.ExecuteReader();
					reader.Read();
					int recodeCount = (int)reader["CNT"];
					return recodeCount;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getSystemLogsCount",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetSystemLogsCount");
						throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 获得指定的一条用户日志记录
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="Id">日志Id</param>
		/// <returns>用户日志</returns>
		public static DataSet GetUserLoged(int userId, int Id) 
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					SqlCommand myCommand = new SqlCommand("getUserLoged", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					EntitySet logs = new EntitySet();
					myCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblLoged"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getUserLoged",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetUserLoged");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 获得用户日志记录
		/// </summary>
		/// <param name="pageIndex">页编号</param>
		/// <param name="pageSize">页的记录行数</param>
		/// <param name="argOperator">操作员名称</param>
		/// <param name="argModelname">模块名称</param>
		/// <param name="argStateDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		/// <returns>用户日志</returns>
		public static DataSet GetUserLogs(
									int userId, 
									int pageIndex, 
									int pageSize, 
									string argOperator, 
									string argModelname, 
									string argStateDate, 
									string argEndDate) 
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					SqlCommand myCommand = new SqlCommand("getUserLogs", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					EntitySet logs = new EntitySet();
					myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value         = pageIndex;
					myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value          = pageSize;
					myCommand.Parameters.Add("@Operator", SqlDbType.VarChar).Value		= argOperator;
					myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value			= argModelname;
					myCommand.Parameters.Add("@StateDate", SqlDbType.VarChar).Value		= argStateDate;
					myCommand.Parameters.Add("@EndDate", SqlDbType.VarChar).Value		= argEndDate;
					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblDictions"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getUserLogs",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetUserLogs");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 获得指定的一条系统日志记录
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="Id">日志Id</param>
		/// <returns>指定的一条系统日志记录</returns>
		public static DataSet GetSystemLoged(int userId, int Id) 
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				try
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					SqlCommand myCommand = new SqlCommand("getSystemLoged", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;

					// 设置参数
					myCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblLoged"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getSystemLoged",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetSystemLoged");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}


		/// <summary>
		/// 获得系统日志记录
		/// </summary>
		/// <param name="pageIndex">页序号</param>
		/// <param name="pageSize">页的行数</param>
		/// <param name="argOperator">操作员名称</param>
		/// <param name="argModelname">模块名称</param>
		/// <param name="argStateDate">开始时间</param>
		/// <param name="argEndDate">结束时间</param>
		/// <returns>系统日志</returns>
		public static DataSet GetSystemLogs(
									int userId, 
									int pageIndex, 
									int pageSize, 
									string argOperator, 
									string argModelname, 
									string argStateDate, 
									string argEndDate) 
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				try
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					SqlCommand myCommand = new SqlCommand("getSystemLogs", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					EntitySet logs = new EntitySet();

					// 设置参数
					myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value         = pageIndex;
					myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value          = pageSize;
					myCommand.Parameters.Add("@Operator", SqlDbType.VarChar).Value		= argOperator;
					myCommand.Parameters.Add("@Model", SqlDbType.VarChar).Value			= argModelname;
					myCommand.Parameters.Add("@StateDate", SqlDbType.VarChar).Value		= argStateDate;
					myCommand.Parameters.Add("@EndDate", SqlDbType.VarChar).Value		= argEndDate;

					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblDictions"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getSystemLogs",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetSystemLogs");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}


		/// <summary>
		/// 插入用户日志
		/// </summary>
		/// <param name="argUserId">用户Id</param>
		/// <param name="argLoginDateTime">登陆时间</param>
		/// <param name="argOutputDateTime">退出时间</param>
		/// <param name="argModelId">模块Id</param>
		/// <param name="argSystemId">系统Id</param>
		/// <param name="argSqlText">SQL语句</param>
		/// <param name="argMemo">备注</param>
		public static void writeUserLog(
							int argUserId,
							DateTime argLoginDateTime, 
							DateTime argOutputDateTime, 
							string argSystemId, 
							string argSqlText,
							string argMemo)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("WriteUserLog", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@userId", SqlDbType.Int).Value				= argUserId;
					myCommand.Parameters.Add("@loginDateTime", SqlDbType.DateTime).Value	= argLoginDateTime;
					myCommand.Parameters.Add("@outputDateTime", SqlDbType.DateTime).Value	= argOutputDateTime;
					myCommand.Parameters.Add("@todateTime", SqlDbType.DateTime).Value		= System.DateTime.Today.ToString();
					myCommand.Parameters.Add("@systemId", SqlDbType.VarChar).Value			= argSystemId;
					myCommand.Parameters.Add("@sqlText", SqlDbType.VarChar).Value			= argSqlText;
					myCommand.Parameters.Add("@memo", SqlDbType.VarChar).Value				= argMemo;
					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(argUserId, 
						"Log", 
						argLoginDateTime,
						argOutputDateTime,
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "writeUserLog");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 插入系统日志
		/// </summary>
		/// <param name="argUserId">用户Id</param>
		/// <param name="argSqlText">SQL语句</param>
		/// <param name="argLoginDateTime">登陆时间</param>
		/// <param name="argOutputDateTime">退出时间</param>
		/// <param name="argModelId">模块Id</param>
		/// <param name="argException">异常语句</param>
		/// <param name="argMemo">备注</param>
		public static void writeSystemLog(
							int argUserId,
							string argSqlText,
							DateTime argLoginDateTime, 
							DateTime argOutputDateTime, 
							string argModelId, 
							string argException,
							string argMemo)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("WriteSystemLog", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@userId", SqlDbType.Int).Value				= argUserId;
					myCommand.Parameters.Add("@sqlText", SqlDbType.VarChar).Value				= argSqlText;
					myCommand.Parameters.Add("@loginDateTime", SqlDbType.DateTime).Value	= argLoginDateTime;
					myCommand.Parameters.Add("@outputDateTime", SqlDbType.DateTime).Value	= argOutputDateTime;
					myCommand.Parameters.Add("@todateTime", SqlDbType.DateTime).Value		= System.DateTime.Today;
					myCommand.Parameters.Add("@modelId", SqlDbType.VarChar).Value			= argModelId;
					myCommand.Parameters.Add("@exception", SqlDbType.VarChar).Value			= argException;
					myCommand.Parameters.Add("@memo", SqlDbType.VarChar).Value				= argMemo;
					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
				catch
				{
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}
	}


}

