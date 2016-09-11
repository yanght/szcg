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
	/// 部门数据操作类。
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
		/// 查询。
		/// </summary>
		/// <param name="intSexId">部门标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strName">部门名称，若不想加入该条件请设 null 。</param>
		/// <param name="strDescription">部门描述，若不想加入该条件请设 null 。</param>
		/// <returns>部门信息表</returns>
		public SystemDs.DepartmentInfoDataTable Select(int intDepartmentId, string strName, string strDescription)
		{
			string strSQL = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region 为执行准备查询语句及参数

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
		/// 获取所有。
		/// </summary>
		/// <returns>部门信息表</returns>
		public SystemDs.DepartmentInfoDataTable SelectAll()
		{
			return this.Select(-1, null, null);
		}

		/// <summary>
		/// 查询(依据部门标识)。
		/// </summary>
		/// <param name="intDepartmentId">部门标识</param>
		/// <returns>部门信息表</returns>
		public SystemDs.DepartmentInfoDataTable SelectByDepartmentId(int intDepartmentId)
		{
			return this.Select(intDepartmentId, null, null);
		}

		/// <summary>
		/// 判断部门名称已存在
		/// </summary>
		/// <param name="strName">部门名称</param>
		/// <returns>是否已存在</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper(true)).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 插入。
		/// </summary>
		/// <param name="strName">名称，若不想插入值请设 null 。</param>
		/// <param name="strDescription">描述，若不想插入值请设 null 。</param>
		/// <returns>执行是否成功</returns>
		public bool Insert(string strName, string strDescription)
		{
			#region 为执行准备参数

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
		/// 更新(依据部门标识)。
		/// </summary>
		/// <param name="intDepartmentId">部门标识</param>
		/// <param name="strName">部门名称，若想清空请设 null 。</param>
		/// <param name="strDescription">部门描述，若想清空请设 null 。</param>
		/// <returns>执行是否成功</returns>
		public bool Update(int intDepartmentId, string strName, string strDescription)
		{
			#region 为执行准备参数

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
		/// 删除(依据部门标识)。
		/// </summary>
		/// <param name="intDepartmentId">部门标识</param>
		/// <returns>执行是否成功</returns>
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
