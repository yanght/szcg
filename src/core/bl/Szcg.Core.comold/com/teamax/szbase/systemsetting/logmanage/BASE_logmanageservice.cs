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
		//�������
		protected StringBuilder sb;
		//�õ����ݿ��еĽ����
		protected SqlDataReader dr;
		//�����������
		protected ArrayList list;


		#region �����������ɵĴ���
		
		//Web ����������������
//		private IContainer components = null;
				
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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
		/// ����һ��DataSet
		/// </summary>
		/// <param name="a">һ��SQL���</param>
		/// <returns></returns>
		public static DataSet selectLog(string a)
		{
			DataSet myDataSet =DataAccess.ExecuteDataSet(a,null);
			return myDataSet;
		}

		/// <summary>
		/// ���ڵ�½ҳ��
		/// </summary>
		public void updateDate(string time,string id)
		{
			string sql = "update userlog set logintime = '"+time+"' where roleid = '"+id+"'";
			DataAccess.ExecuteNonQuery(sql,null);
		}

		/// <summary>
		/// �����û���־����
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
				executeName = "��ѯ��";
				int a = sql.IndexOf("from");//ȡ��"from"��λ��
				string b = " ";
				int d = sql.IndexOf(b,a+5);//ȡ����"from"֮��ڶ����ո��λ��
				string tableName = sql.Substring(a+4,d-(a+4)).Trim();
				msg.Append(executeName);
				msg.Append(tableName+"��");
			}
			else if(executeName.Equals("insert"))
			{
				executeName = "����";
				//ȡ��"into"��λ��
				int a = sql.IndexOf("into");
				//ȡ�õ�һ��"("��λ��
				int b = sql.IndexOf("(");
				//ȡ�ñ���
				string tableName = sql.Substring(a+5,b-(a+5)).Trim();

				msg.Append("��"+tableName+"�������"+executeName+"����");
			}
			else if (executeName.Equals("delete"))
			{
				executeName = "ɾ��";
				string a = " ";
				int b = sql.IndexOf(a);
				int c = sql.IndexOf("where");
				string tableName = sql.Substring(b,c-b).Trim();
				msg.Append("��"+tableName+"�������"+executeName+"����");

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
		/// ��¼ϵͳ��־���ı��ļ�
		/// </summary>
		/// <param name="message">��¼������</param>
		public static void SysLog(string message)
		{	//��Ҫ���쳣д��ϵͳ��־����,ֻ��������дSQL������syslog����

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
		/// ��ò���Ա���ݼ�
		/// </summary>
		public static String[] getOperator(int userId)
		{
			StringBuilder strSQL;
			strSQL = new StringBuilder();
			try
			{
				//�������
				StringBuilder sb;
				//�õ����ݿ��еĽ����
				SqlDataReader dr;
				//�����������
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
		/// ��ò���Ա���ݼ�
		/// </summary>
		public static String[] getMolder(int userId)
		{
			//�������
			StringBuilder sb;
			//�õ����ݿ��еĽ����
			SqlDataReader dr;
			//�����������
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
		/// �����־��¼����
		/// </summary>
		/// <param name="code">ϵͳ��־=0,�û���־=1</param>
		/// <param name="argOperator">����Ա����</param>
		/// <param name="argModel">ģ������</param>
		/// <param name="argStartDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
		/// <returns>��־��¼��</returns>
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
		/// �����־��¼
		/// </summary>
		/// <param name="code">ϵͳ��־=0,�û���־=1</param>
		/// <param name="pageIndex">ҳ���</param>
		/// <param name="pageSize">ҳ�ļ�¼����</param>
		/// <param name="argOperator">����Ա����</param>
		/// <param name="argModelname">ģ������</param>
		/// <param name="argStateDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
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
		/// ���ָ����һ����־��¼
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="code">ϵͳ��־=0,�û���־=1</param>
		/// <returns>ָ����һ����־��¼</returns>
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
		/// ����û���־��¼����
		/// </summary>
		/// <param name="argOperator">����Ա����</param>
		/// <param name="argModel">ģ������</param>
		/// <param name="argStartDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
		/// <returns>�û���־��¼</returns>
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
		/// ���ϵͳ��־��¼����
		/// </summary>
		/// <param name="argOperator"> ����ԱId</param>
		/// <param name="argModel">ģ��Id</param>
		/// <param name="argStartDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
		/// <returns>ϵͳ��־��¼��</returns>
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
		/// ���ָ����һ���û���־��¼
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="Id">��־Id</param>
		/// <returns>�û���־</returns>
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
		/// ����û���־��¼
		/// </summary>
		/// <param name="pageIndex">ҳ���</param>
		/// <param name="pageSize">ҳ�ļ�¼����</param>
		/// <param name="argOperator">����Ա����</param>
		/// <param name="argModelname">ģ������</param>
		/// <param name="argStateDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
		/// <returns>�û���־</returns>
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
		/// ���ָ����һ��ϵͳ��־��¼
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="Id">��־Id</param>
		/// <returns>ָ����һ��ϵͳ��־��¼</returns>
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

					// ���ò���
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
		/// ���ϵͳ��־��¼
		/// </summary>
		/// <param name="pageIndex">ҳ���</param>
		/// <param name="pageSize">ҳ������</param>
		/// <param name="argOperator">����Ա����</param>
		/// <param name="argModelname">ģ������</param>
		/// <param name="argStateDate">��ʼʱ��</param>
		/// <param name="argEndDate">����ʱ��</param>
		/// <returns>ϵͳ��־</returns>
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

					// ���ò���
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
		/// �����û���־
		/// </summary>
		/// <param name="argUserId">�û�Id</param>
		/// <param name="argLoginDateTime">��½ʱ��</param>
		/// <param name="argOutputDateTime">�˳�ʱ��</param>
		/// <param name="argModelId">ģ��Id</param>
		/// <param name="argSystemId">ϵͳId</param>
		/// <param name="argSqlText">SQL���</param>
		/// <param name="argMemo">��ע</param>
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
		/// ����ϵͳ��־
		/// </summary>
		/// <param name="argUserId">�û�Id</param>
		/// <param name="argSqlText">SQL���</param>
		/// <param name="argLoginDateTime">��½ʱ��</param>
		/// <param name="argOutputDateTime">�˳�ʱ��</param>
		/// <param name="argModelId">ģ��Id</param>
		/// <param name="argException">�쳣���</param>
		/// <param name="argMemo">��ע</param>
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

