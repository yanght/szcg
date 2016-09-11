using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;
using szcg.com.teamax.util;

namespace szcg.com.teamax.szbase.capability
{
	/// <summary>
	/// BASE_backup 的摘要说明。
	/// </summary>
	public class BASE_backup
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.capability.";
		
		/**//// <summary>
		/// 服务器
		/// </summary>
		private string server;
        
		/**//// <summary>
		/// 登录名
		/// </summary>
		private string uid;
       
		/**//// <summary>
		/// 登录密码
		/// </summary>
		private string pwd;
         
		/**//// <summary>
		/// 要操作的数据库
		/// </summary>
		//private string database;
         
		/**//// <summary>
		/// 数据库连接字符串
		/// </summary>
		private string conn;
 
		/**//// <summary>
		/// BASE_backup类的构造函数
		/// 在这里进行字符串的切割，获取服务器，登录名，密码，数据库
		/// </summary>
		/// 
		public BASE_backup()
		{

			//conn=System.Configuration.ConfigurationManager.AppSettings["conn"].ToString();
			conn=System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
			server = StringCut(conn,"server=",";");
			uid = StringCut(conn,"user id=",";");
			pwd = StringCut(conn,"password=",";");
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/**//// <summary>
		/// 切割字符串
		/// </summary>
		/// <param name="str"></param>
		/// <param name="bg"></param>
		/// <param name="ed"></param>
		/// <returns></returns>
		public string StringCut(string str,string bg,string ed)
		{
			string sub;
			sub=str.Substring(str.IndexOf(bg)+bg.Length);
			sub=sub.Substring(0,sub.IndexOf(";"));
			return sub;
		}
		/// <summary>
		/// 获得本地数据库列表
		/// </summary>
		/// <param name="strServerName"></param>
		/// <param name="strUserName"></param>
		/// <param name="strPwd"></param>
		/// <returns></returns>

		public DataSet GetDbList(int userId) 
		{ 
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					SqlCommand myCommand = new SqlCommand("sp_databases", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "databases"); 
					myConnection.Close();
					return dtst;
				}
				catch(Exception err) 
				{ 
					BASE_logmanageservice.writeSystemLog(
						userId,
						"取得数据库名称",
						System.DateTime.Now,
						System.DateTime.Now,
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetDbList");
					throw; 
				} 
				finally 
				{ 
					myConnection.Dispose();
				} 
			}
		} 
 
		/// <summary>
		/// 数据库备份
		/// </summary>
		/// <returns>备份是否成功</returns>
		public bool DbBackup(int userId, string database)
		{

			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("backupDb", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.AddWithValue("@database", database);
					myConnection.Open();
					myCommand.ExecuteNonQuery();
					return true;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"数据备份",
						System.DateTime.Now,
						System.DateTime.Now,
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "DbBackup");
					return false;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

//		/**//// <summary>
//		/// 数据库恢复
//		/// </summary>
//		public bool DbRestore(int userId, string s1,string s2)
//		{
//			SQLDMO.Restore oRestore = new SQLDMO.RestoreClass();
//			SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
//			try
//			{
//				//结束所有进程
//				exepro(userId, s2);
//				oSQLServer.LoginSecure = false;
//				oSQLServer.Connect(server, uid, pwd);
//				oRestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
//				oRestore.Database = s2;
//				oRestore.Files = s1;
//				oRestore.FileNumber = 1;
//				oRestore.ReplaceDatabase = true;
//				oRestore.SQLRestore(oSQLServer);
//				BASE_logmanageservice.writeUserLog(
//					userId,
//					System.DateTime.Now,
//					System.DateTime.Now,
//					BASE_ModerId.getSystem_ZCPT(),
//					"数据还原", 
//					NAMESPACE_PATH + "DbRestore");
//				return true;
//			}
//			catch(Exception err)
//			{
//				BASE_logmanageservice.writeSystemLog(
//					Convert.ToInt16(userId),
//					"数据还原",
//					System.DateTime.Now,
//					System.DateTime.Now,
//					BASE_ModerId.getSystem_ZCPT(),
//					err.ToString(),
//					NAMESPACE_PATH + "DbRestore");
//				return false;
//			}
//			finally
//			{
//				oSQLServer.DisConnect();
//			}
//		}
        
		/**//// <summary>
		/// 结束所有进程
		/// </summary>
		/// <returns></returns>
		private bool exepro(int userId, string s)
		{
			SqlConnection conn1 = new SqlConnection("server="+server+";uid="+uid+";pwd="+pwd+";database=master");
			SqlCommand cmd = new SqlCommand("killspid",conn1);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@dbname",s);
			try
			{
				conn1.Open();
				cmd.ExecuteNonQuery();
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				conn1.Close();
			}    
		}

		public static int backUpCollecterTrack(int userId, int days)
		{
			try
			{
				string sql="runAt_collectxy";
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@_runtime",SqlDbType.Int);
				input[0].Value=days;
				input[0].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@flag",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;

				return  Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"数据备份",
					System.DateTime.Now,
					System.DateTime.Now,
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "backUpCollecterTrack");
				throw;
			}
		}

		/// <summary>
		/// 获得备份数据库列表
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="databaseName">数据库名称</param>
		/// <returns></returns>
		public ArrayList GetBackupList(int userId, string databaseName)
		{
			SqlDataReader reader;
			DataSet dtst;
			ArrayList list;
			using(SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection)) 
			{
				try 
				{
					string backPath = System.Configuration.ConfigurationManager.AppSettings["backupFilePath"];
					dtst = new DataSet();
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("getBackupList", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@backPath", SqlDbType.VarChar).Value         = backPath;
					myCommand.Parameters.Add("@databaseName", SqlDbType.VarChar).Value     = databaseName;
					reader = myCommand.ExecuteReader();
					list = new ArrayList();
					while(reader.Read())
					{
						string backupCode = Convert.ToString(reader["Position"]);
						string backupDate = Convert.ToString(reader["BackupFinishDate"]);
						backupDate = backupDate.Replace("-", "");
						backupDate = backupDate.Replace(" ", "");
						backupDate = backupDate.Replace(":", "");
						backupDate = backupDate.Replace(".", "");
						list.Add(backupCode + "," + databaseName + backupDate);
					}
					return list;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"StoredProcedure:GetBackupList",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetBackupList");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 还原数据库
		/// </summary>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="disk">设备名称</param>
		/// <param name="fileNo">文件号</param>
		/// <returns>还原成功与失败</returns>
		public bool RevertDb(string databaseName, string disk, int fileNo)
		{
			string masterConn = System.Configuration.ConfigurationManager.AppSettings["masterConn"];
			using(SqlConnection myConnection = new SqlConnection(masterConn))
			{
				try
				{
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("revertDb", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@databaseName", SqlDbType.VarChar).Value	= databaseName;
					myCommand.Parameters.Add("@disk", SqlDbType.VarChar).Value			= disk;
					myCommand.Parameters.Add("@fileNo", SqlDbType.Int).Value			= fileNo;
					myCommand.ExecuteNonQuery();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

	}
}
