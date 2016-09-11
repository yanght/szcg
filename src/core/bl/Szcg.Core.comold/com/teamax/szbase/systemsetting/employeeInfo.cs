using System;
using System.Data;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.systemsetting
{
	/// <summary>
	/// employeeInfo ��ժҪ˵����
	/// </summary>
	public class employeeInfo
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.employeeInfo.";
		public employeeInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ȡ��Ա����Ϣ
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="employeeName">Ա������</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <param name="pageSize">ҳ��</param>
		/// <returns></returns>
		public DataSet GetEmployeeInfo(int userId, string employeeName, int pageIndex, int pageSize)
		{
			SqlDataAdapter dad;
			DataSet dtst;
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection)) 
			{
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("getEmployeeInfo", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value		= pageIndex;
					myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value		= pageSize;
					myCommand.Parameters.Add("@employee", SqlDbType.VarChar).Value	= employeeName;
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "EmployeeInfo"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"StoredProcedure:getEmployeeInfo",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetEmployeeInfo");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// ����Id��ȡ��Ա����Ϣ
		/// </summary>
		/// <param name="userId">�û�Id</param>
		/// <param name="employeeName">Ա��Id</param>
		/// <returns></returns>
		public DataSet GetEmployee(int userId, int employeeId)
		{
			SqlDataAdapter dad;
			DataSet dtst;
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection)) 
			{
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("getEmployee", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@employeeId", SqlDbType.Int).Value	= employeeId;
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "EmployeeInfo"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"StoredProcedure:getEmployee",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetEmployee");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// ȡ��Ա����Ϣ����
		/// </summary>
		/// <param name="argOperator">�û�Id</param>
		/// <param name="argModel">Ա������</param>
		/// <returns>��¼����</returns>
		public int GetEmployeeCount(int userId, string employeeName)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("getEmployeeCount", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@employee", SqlDbType.VarChar).Value = employeeName;
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
						"procedure:getEmployeeCount",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "GetEmployeeCount");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// ����Ա����Ϣ
		/// </summary>
		/// <param name="argOperator">�û�Id</param>
		/// <param name="argModel">Ա������</param>
		/// <returns>��¼����</returns>
		public void AddEmployee(int userId, 
						string employeeName, 
						string officetel1,
						string officetel2,
						string mobiletel2,
						string mobiletel,
						string addresstel2,
						string addresstel,
						string homenumber,
						string duty)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("InsertEmployee", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = employeeName;
					myCommand.Parameters.Add("@officetel", SqlDbType.VarChar).Value = officetel1;
					myCommand.Parameters.Add("@officete2", SqlDbType.VarChar).Value = officetel2;
					myCommand.Parameters.Add("@mobiletel2", SqlDbType.VarChar).Value = mobiletel2;
					myCommand.Parameters.Add("@mobiletel", SqlDbType.VarChar).Value = mobiletel;
					myCommand.Parameters.Add("@addresstel2", SqlDbType.VarChar).Value = addresstel2;
					myCommand.Parameters.Add("@addresstel", SqlDbType.VarChar).Value = addresstel;
					myCommand.Parameters.Add("@homenumber", SqlDbType.VarChar).Value = homenumber;
					myCommand.Parameters.Add("@duty", SqlDbType.VarChar).Value = duty;
					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:InsertEmployee",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "AddEmployee");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// �޸�Ա����Ϣ
		/// </summary>
		/// <param name="argOperator">�û�Id</param>
		/// <param name="argModel">Ա������</param>
		/// <returns>��¼����</returns>
		public void EditEmployee(int userId, 
						int id,
						string employeeName, 
						string officetel1,
						string officetel2,
						string mobiletel2,
						string mobiletel,
						string addresstel2,
						string addresstel,
						string homenumber,
						string duty)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("EditEmployee", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
					myCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = employeeName;
					myCommand.Parameters.Add("@officetel", SqlDbType.VarChar).Value = officetel1;
					myCommand.Parameters.Add("@officete2", SqlDbType.VarChar).Value = officetel2;
					myCommand.Parameters.Add("@mobiletel2", SqlDbType.VarChar).Value = mobiletel2;
					myCommand.Parameters.Add("@mobiletel", SqlDbType.VarChar).Value = mobiletel;
					myCommand.Parameters.Add("@addresstel2", SqlDbType.VarChar).Value = addresstel2;
					myCommand.Parameters.Add("@addresstel", SqlDbType.VarChar).Value = addresstel;
					myCommand.Parameters.Add("@homenumber", SqlDbType.VarChar).Value = homenumber;
					myCommand.Parameters.Add("@duty", SqlDbType.VarChar).Value = duty;
					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:EditEmployee",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "EditEmployee");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// ɾ��Ա����Ϣ
		/// </summary>
		/// <param name="argOperator">�û�Id</param>
		/// <param name="argModel">Ա������</param>
		/// <returns>��¼����</returns>
		public void DeleteEmployee(int userId, int id)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection) ) 
			{
				try
				{
					SqlCommand myCommand = new SqlCommand("DeleteEmployee", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
					myConnection.Open();
					myCommand.ExecuteNonQuery();
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"procedure:DeleteEmployee",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "DeleteEmployee");
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
