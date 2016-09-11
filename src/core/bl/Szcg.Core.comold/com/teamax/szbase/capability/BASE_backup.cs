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
	/// BASE_backup ��ժҪ˵����
	/// </summary>
	public class BASE_backup
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.capability.";
		
		/**//// <summary>
		/// ������
		/// </summary>
		private string server;
        
		/**//// <summary>
		/// ��¼��
		/// </summary>
		private string uid;
       
		/**//// <summary>
		/// ��¼����
		/// </summary>
		private string pwd;
         
		/**//// <summary>
		/// Ҫ���������ݿ�
		/// </summary>
		//private string database;
         
		/**//// <summary>
		/// ���ݿ������ַ���
		/// </summary>
		private string conn;
 
		/**//// <summary>
		/// BASE_backup��Ĺ��캯��
		/// ����������ַ������и��ȡ����������¼�������룬���ݿ�
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
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/**//// <summary>
		/// �и��ַ���
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
		/// ��ñ������ݿ��б�
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
						"ȡ�����ݿ�����",
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
		/// ���ݿⱸ��
		/// </summary>
		/// <returns>�����Ƿ�ɹ�</returns>
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
						"���ݱ���",
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
//		/// ���ݿ�ָ�
//		/// </summary>
//		public bool DbRestore(int userId, string s1,string s2)
//		{
//			SQLDMO.Restore oRestore = new SQLDMO.RestoreClass();
//			SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
//			try
//			{
//				//�������н���
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
//					"���ݻ�ԭ", 
//					NAMESPACE_PATH + "DbRestore");
//				return true;
//			}
//			catch(Exception err)
//			{
//				BASE_logmanageservice.writeSystemLog(
//					Convert.ToInt16(userId),
//					"���ݻ�ԭ",
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
		/// �������н���
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
					"���ݱ���",
					System.DateTime.Now,
					System.DateTime.Now,
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "backUpCollecterTrack");
				throw;
			}
		}

		/// <summary>
		/// ��ñ������ݿ��б�
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="databaseName">���ݿ�����</param>
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
		/// ��ԭ���ݿ�
		/// </summary>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="disk">�豸����</param>
		/// <param name="fileNo">�ļ���</param>
		/// <returns>��ԭ�ɹ���ʧ��</returns>
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
