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
	/// �������ݲ����ࡣ
	/// </summary>
    public class DepartmentInfo : CommonPage
	{

        //private const string SQL_SELECT = "SELECT departcode as DepartmentId, departname as [Name], isnull(memo,'')  as [Description] FROM  p_depart WHERE Area LIKE @Area + '%'";
        private const string SQL_SELECT = "SELECT a.departcode as DepartmentId, a.departname as [Name], isnull(a.memo,'')  as [Description] FROM  p_depart as a LEFT JOIN p_depart_guide as b ON a.departcode = b.departcode WHERE isnull(a.isdel,0)=0 and a.Area LIKE @Area + '%'";
        private const string SQL_SELECT_NAME_EXIST = "SELECT COUNT(*) FROM  p_depart WHERE [departname] = @Name";
        private const string SQL_INSERT = "INSERT INTO p_depart(area, [departname], [memo]) VALUES(@Area, @Name, @Description)";
        private const string SQL_UPDATE = "UPDATE p_depart SET [departname] = @Name, [memo] = @Description WHERE departcode = @DepartmentId";
        private const string SQL_DELETE = "DELETE FROM p_depart WHERE departcode = @DepartmentId";

        //private const string PART_DEPARTMENT_ID = "departcode = @DepartmentId";
        private const string PART_DEPARTMENT_ID = "a.UserDefinedCode LIKE (select UserDefinedCode from p_depart where departcode = @DepartmentId) + '%'";
        private const string PART_NAME = "a.departname LIKE '%' + @Name + '%'";
        private const string PART_DESCRIPTION = "a.memo LIKE '%' + @Description + '%'";

		private const string PARM_AREA = "@Area";
		private const string PARM_DEPARTMENT_ID = "@DepartmentId";
		private const string PARM_NAME = "@Name";
		private const string PARM_DESCRIPTION = "@Description";

		private string strArea;

		public DepartmentInfo() 
		{
            strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <param name="intSexId">���ű�ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="strName">�������ƣ������������������� null ��</param>
		/// <param name="strDescription">���������������������������� null ��</param>
		/// <returns>������Ϣ��</returns>
		public SystemDs.DepartmentInfoDataTable Select(int intDepartmentId, string strName, string strDescription)
		{
			string strSQL = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region Ϊִ��׼����ѯ��估����

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (intDepartmentId >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DEPARTMENT_ID);
				arrParms.Add(new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId));
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

			SystemDs.DepartmentInfoDataTable dataTable = new SystemDs.DepartmentInfoDataTable();

			(new SQLHelper(true)).ExecuteDataTable(dataTable, strSQL, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ȡ���С�
		/// </summary>
		/// <returns>������Ϣ��</returns>
		public SystemDs.DepartmentInfoDataTable SelectAll()
		{
			return this.Select(-1, null, null);
		}

		/// <summary>
		/// ��ѯ(���ݲ��ű�ʶ)��
		/// </summary>
		/// <param name="intDepartmentId">���ű�ʶ</param>
		/// <returns>������Ϣ��</returns>
		public SystemDs.DepartmentInfoDataTable SelectByDepartmentId(int intDepartmentId)
		{
			return this.Select(intDepartmentId, null, null);
		}

		/// <summary>
		/// �жϲ��������Ѵ���
		/// </summary>
		/// <param name="strName">��������</param>
		/// <returns>�Ƿ��Ѵ���</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper(true)).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="strName">���ƣ����������ֵ���� null ��</param>
		/// <param name="strDescription">���������������ֵ���� null ��</param>
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

			int intRetVal = (new SQLHelper(true)).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ����(���ݲ��ű�ʶ)��
		/// </summary>
		/// <param name="intDepartmentId">���ű�ʶ</param>
		/// <param name="strName">�������ƣ������������ null ��</param>
		/// <param name="strDescription">��������������������� null ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Update(int intDepartmentId, string strName, string strDescription)
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

			parms[2] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

			#endregion

			int intRetVal = (new SQLHelper(true)).ExecuteNonQuery(SQL_UPDATE, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��(���ݲ��ű�ʶ)��
		/// </summary>
		/// <param name="intDepartmentId">���ű�ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Delete(int intDepartmentId)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						(new CarInfo()).UpdateClearDepartmentId(intDepartmentId, trans);
						int intRetVal = (new SQLHelper(true)).ExecuteNonQuery(
							trans, SQL_DELETE, new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId)
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
