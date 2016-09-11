using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL;
using Teamax.Common;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// ����״̬���ݲ����ࡣ
	/// </summary>
    public class CarStateInfo : CommonPage
	{
		private const string SQL_SELECT = "SELECT CarStateId, [Name], [Description] FROM CarStateInfo WHERE Area LIKE @Area + '%'";
		private const string SQL_SELECT_NAME_EXIST = "SELECT COUNT(*) FROM CarStateInfo WHERE [Name] = @Name";
		private const string SQL_INSERT = "INSERT INTO CarStateInfo(Area, [Name], [Description]) VALUES(@Area, @Name, @Description)";
		private const string SQL_UPDATE = "UPDATE CarStateInfo SET [Name] = @Name, [Description] = @Description WHERE CarStateId = @CarStateId";
		private const string SQL_DELETE = "DELETE FROM CarStateInfo WHERE CarStateId = @CarStateId";

		private const string PART_CAR_STATE_ID = "CarStateId = @CarStateId";
		private const string PART_NAME = "[Name] LIKE '%' + @Name + '%'";
		private const string PART_DESCRIPTION = "[Description] LIKE '%' + @Description + '%'";

		private const string PARM_AREA = "@Area";
		private const string PARM_CAR_STATE_ID = "@CarStateId";
		private const string PARM_NAME = "@Name";
		private const string PARM_DESCRIPTION = "@Description";

		private string strArea;

		public CarStateInfo() 
		{
            strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <param name="intSexId">״̬��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="strName">״̬���ƣ������������������� null ��</param>
		/// <param name="strDescription">״̬������������������������ null ��</param>
		/// <returns>����״̬��Ϣ��</returns>
		public SystemDs.CarStateInfoDataTable Select(int intCarStateId, string strName, string strDescription)
		{
			string strSQL = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region Ϊִ��׼����ѯ��估����

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (intCarStateId >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_CAR_STATE_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_STATE_ID, intCarStateId));
			}
			if (strName != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_NAME);
				arrParms.Add(new SqlParameter(PARM_NAME, strName));
			}
			if (strDescription != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DESCRIPTION);
				arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarStateInfoDataTable dataTable = new SystemDs.CarStateInfoDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ȡ���С�
		/// </summary>
		/// <returns>����״̬��Ϣ��</returns>
		public SystemDs.CarStateInfoDataTable SelectAll()
		{
			return this.Select(-1, null, null);
		}

		/// <summary>
		/// ��ѯ(����״̬��ʶ)��
		/// </summary>
		/// <param name="intCarStateId">״̬��ʶ</param>
		/// <returns>����״̬��Ϣ��</returns>
		public SystemDs.CarStateInfoDataTable SelectByCarStateId(int intCarStateId)
		{
			return this.Select(intCarStateId, null, null);
		}

		/// <summary>
		/// �ж�״̬�����Ѵ���
		/// </summary>
		/// <param name="strName">״̬����</param>
		/// <returns>�Ƿ��Ѵ���</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="strName">״̬���ƣ����������ֵ���� null ��</param>
		/// <param name="strDescription">״̬���������������ֵ���� null ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(string strName, string strDescription)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[3];

			if (strArea == null)
				parms[0] = new SqlParameter(PARM_AREA, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_AREA, strArea);

			if (strName == null)
				parms[1] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[1] = new SqlParameter(PARM_NAME, strName);

			if (strDescription == null)
				parms[2] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[2] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ����(����״̬��ʶ)��
		/// </summary>
		/// <param name="intCarStateId">״̬��ʶ</param>
		/// <param name="strName">״̬���ƣ������������ null ��</param>
		/// <param name="strDescription">״̬����������������� null ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Update(int intCarStateId, string strName, string strDescription)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[3];

			if (strName == null)
				parms[0] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_NAME, strName);

			if (strDescription == null)
				parms[1] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[1] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			parms[2] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��(����״̬��ʶ)��
		/// </summary>
		/// <param name="intCarStateId">״̬��ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Delete(int intCarStateId)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						(new CarInfo()).UpdateClearCarStateId(intCarStateId, trans);
						int intRetVal = (new SQLHelper()).ExecuteNonQuery(
							trans, SQL_DELETE, new SqlParameter(PARM_CAR_STATE_ID, intCarStateId)
							);
						trans.Commit();
						return intRetVal > 0 ? true : false;
					}
					catch
					{
						trans.Rollback();
						throw;
					}
				}
			}			
		}
	}
}
