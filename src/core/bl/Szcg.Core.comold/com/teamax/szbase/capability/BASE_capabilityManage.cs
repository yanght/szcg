using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using szcg.web.szbase.systemsetting.logmanage;
using szcg.com.teamax.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.capability
{
	/// <summary>
	/// BASE_capabilityManage 的摘要说明。
	/// </summary>
	public class BASE_capabilityManageClass
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.capability.";
		//private static StringBuilder strSQL;
		public BASE_capabilityManageClass()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static int GetSessionLogCount(int userId, string userName, string address, string stateDate, string endDate)
		{
			try
			{
				using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
				{
					SqlCommand myCommand = new SqlCommand("getSessionLogCount", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@userName", SqlDbType.VarChar).Value		= userName;
					myCommand.Parameters.Add("@address", SqlDbType.VarChar).Value		= address;
					myCommand.Parameters.Add("@stateDate", SqlDbType.VarChar).Value		= stateDate;
					myCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value		= endDate;
//					myCommand.Parameters.Add("@areaCode", SqlDbType.VarChar).Value		= areaCode;
					myConnection.Open();
					SqlDataReader reader = myCommand.ExecuteReader();
					reader.Read();
					int recodeCount = (int)reader["CNT"];
					return recodeCount;
				}
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"procedure:getSessionLogCount",
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "GetSessionLogCount");
				throw;
			}

		}

		public static DataSet GetSessionLog(int userId, int pageIndex, int pageSize,  string userName, string address, string stateDate, string endDate)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("getSessionLog", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value         = pageIndex;
					myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value          = pageSize;
					myCommand.Parameters.Add("@userName", SqlDbType.VarChar).Value		= userName;
					myCommand.Parameters.Add("@address", SqlDbType.VarChar).Value		= address;
					myCommand.Parameters.Add("@stateDate", SqlDbType.VarChar).Value		= stateDate;
					myCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value		= endDate;
//					myCommand.Parameters.Add("@areaCode", SqlDbType.VarChar).Value		= areaCode;
					myConnection.Open();
					SqlDataAdapter dad = new SqlDataAdapter();
					dad.SelectCommand = myCommand;
					DataSet dataset = new DataSet();
					dad.Fill(dataset, "table0");
					
					return dataset;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getSessionLog",
						System.DateTime.Now,
						System.DateTime.Now, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetSessionLog");
					throw;
				}
				finally
				{myConnection.Close();}
			}
		}

		public static DataSet GetAllUser(int userId)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("getAllUsers", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myConnection.Open();
					SqlDataAdapter dad = new SqlDataAdapter();
					dad.SelectCommand = myCommand;
					DataSet dataset = new DataSet();
					dad.Fill(dataset, "table0");
					return dataset;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getAllUsers",
						System.DateTime.Now,
						System.DateTime.Now, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetAllUser");
					throw;
				}
				finally
				{myConnection.Close();}
			}
		}

		public static DataSet GetAllAddress(int userId)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("getAllAddress", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myConnection.Open();
					SqlDataAdapter dad = new SqlDataAdapter();
					dad.SelectCommand = myCommand;
					DataSet dataset = new DataSet();
					dad.Fill(dataset, "table0");
					return dataset;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getAllAddress",
						System.DateTime.Now,
						System.DateTime.Now, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetAllAddress");
					throw;
				}
				finally
				{myConnection.Close();}
			}
		}

		public static DataSet GetSessionUser(int userId, int logId)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try 
				{
					SqlCommand myCommand = new SqlCommand("getSessionUser", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@LogId", SqlDbType.Int).Value = logId;
					myConnection.Open();
					SqlDataAdapter dad = new SqlDataAdapter();
					dad.SelectCommand = myCommand;
					DataSet dataset = new DataSet();
					dad.Fill(dataset, "table0");
					return dataset;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:getSessionUser",
						System.DateTime.Now,
						System.DateTime.Now, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetSessionUser");
					throw;
				}
				finally
				{myConnection.Close();}
			}
		}




	}
}
