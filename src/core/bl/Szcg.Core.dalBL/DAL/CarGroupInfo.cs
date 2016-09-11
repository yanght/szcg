using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL;
using Teamax.Common;
using bacgDL.Pub;
using bacgDL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// 车辆群组数据操作类。
	/// </summary>
    public class CarGroupInfo : CommonPage
	{
        private const string SQL_SELECT = "SELECT CarGroupId, [Name], [Description] FROM CarGroupInfo WHERE Area LIKE @Area + '%'";
		private const string SQL_SELECT_NAME_EXIST = "SELECT COUNT(*) FROM CarGroupInfo WHERE [Name] = @Name";
		private const string SQL_INSERT = "INSERT INTO CarGroupInfo(Area, [Name], [Description]) VALUES(@Area, @Name, @Description)";
		private const string SQL_UPDATE = "UPDATE CarGroupInfo SET [Name] = @Name, [Description] = @Description WHERE CarGroupId = @CarGroupId";
		private const string SQL_DELETE = "DELETE FROM CarGroupInfo WHERE CarGroupId = @CarGroupId";

		private const string PART_CAR_GROUP_ID = "CarGroupId = @CarGroupId";
		private const string PART_NAME = "[Name] LIKE '%' + @Name + '%'";
		private const string PART_DESCRIPTION = "[Description] LIKE '%' + @Description + '%'";

		private const string PARM_AREA = "@Area";
		private const string PARM_CAR_GROUP_ID = "@CarGroupId";
		private const string PARM_NAME = "@Name";
		private const string PARM_DESCRIPTION = "@Description";

		private string strArea;

		public CarGroupInfo() 
		{
            //strArea = (string)HttpContext.Current.Session["CurrentRoleArea"];
            strArea = this.AreaCode;
		}

		/// <summary>
		/// 查询。
		/// </summary>
		/// <param name="intSexId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strName">群组名称，若不想加入该条件请设 null 。</param>
		/// <param name="strDescription">群组描述，若不想加入该条件请设 null 。</param>
		/// <returns>车辆群组信息表</returns>
		public SystemDs.CarGroupInfoDataTable Select(int intCarGroupId, string strName, string strDescription)
		{
            string strSQL = SQL_SELECT;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();

            #region 为执行准备查询语句及参数

            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
            if (intCarGroupId >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_CAR_GROUP_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
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

            SystemDs.CarGroupInfoDataTable dataTable = new SystemDs.CarGroupInfoDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

            
            return dataTable;

         }

		/// <summary>
		/// 分页查询。
		/// </summary>
		/// <param name="intSexId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strName">群组名称，若不想加入该条件请设 null 。</param>
		/// <param name="strDescription">群组描述，若不想加入该条件请设 null 。</param>
		/// <returns>车辆群组信息表</returns>
        public QueryUtil Select_Of_Page(int intCarGroupId, string strName, string strDescription, int pageindex, int pagesize)
        {
            //调试
            string strselect = "CarGroupId, [Name], [Description]";
            string strfrom = " CarGroupInfo ";
            string strwhere = " 1=1 and  Area LIKE '" + this.AreaCode + "%'";

            if (intCarGroupId >= 0)
            {
                strwhere = strwhere + " and CarGroupId =" + Convert.ToString(intCarGroupId);
            }
            if (strName != null)
            {
                strwhere = strwhere + " and [Name] LIKE '%" + strName + "%'";
            }
            if (strDescription != null)
            {
                strwhere = strwhere + " and [Description] LIKE '%" + strDescription + "%'";
            }
            
            QueryUtil qu = new QueryUtil(strselect, strfrom, strwhere);
            qu.Key = "CarGroupId";
            qu.SortBy = "CarGroupId";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize =  pagesize;
            qu.ExecuteDataset(pageindex, "CONN_GPS_STRING");  
            return qu;
        }

        /// <summary>
        /// 分页查询。
        /// </summary>
        public bacgDL.PageManage Select_V2(int intCarGroupId, string strName, string strDescription, int pageindex, int pagesize)
        {
            bacgDL.PageManage pm = new bacgDL.PageManage();
            QueryUtil qu = Select_Of_Page(intCarGroupId, strName, strDescription, pageindex, pagesize);
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }

        #region
        public SystemDs.CarGroupInfoDataTable Select1(int intCarGroupId, string strName, string strDescription)
        {
            string strSQL = SQL_SELECT;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();

            #region 为执行准备查询语句及参数

            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
            if (intCarGroupId >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_CAR_GROUP_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
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

            SystemDs.CarGroupInfoDataTable dataTable = new SystemDs.CarGroupInfoDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

            return dataTable;
        }
        #endregion
        /// <summary>
		/// 获取所有。
		/// </summary>
		/// <returns>车辆群组信息表</returns>
		public SystemDs.CarGroupInfoDataTable SelectAll()
		{
			return this.Select(-1, null, null);
        }


        /// <summary>
		/// 查询(依据群组标识)。
		/// </summary>
		/// <param name="intCarGroupId">群组标识</param>
		/// <returns>车辆群组信息表</returns>
		public SystemDs.CarGroupInfoDataTable SelectByCarGroupId(int intCarGroupId)
		{
			return this.Select(intCarGroupId, null, null);
		}

		/// <summary>
		/// 查询(依据群组名称)。
		/// </summary>
		/// <param name="strName">群组名称</param>
		/// <returns>车辆群组信息表</returns>
		public SystemDs.CarGroupInfoDataTable SelectByName(string strName)
		{
			return this.Select(-1, strName, null);
		}

		/// <summary>
		/// 判断群组名称已存在
		/// </summary>
		/// <param name="strName">群组名称</param>
		/// <returns>是否已存在</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 插入。
		/// </summary>
		/// <param name="strName">群组名称，若不想插入值请设 null 。</param>
		/// <param name="strDescription">群组描述，若不想插入值请设 null 。</param>
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

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 更新(依据群组标识)。
		/// </summary>
		/// <param name="intCarGroupId">群组标识</param>
		/// <param name="strName">群组名称，若想清空请设 null 。</param>
		/// <param name="strDescription">群组描述，若想清空请设 null 。</param>
		/// <returns>执行是否成功</returns>
		public bool Update(int intCarGroupId, string strName, string strDescription)
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

			parms[2] = new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除(依据群组标识)。
		/// </summary>
		/// <param name="intCarGroupId">群组标识</param>
		/// <returns>执行是否成功</returns>
		public bool Delete(int intCarGroupId)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						// 删除车辆关联
						(new CarGroupRelation()).DeleteByCarGroupId(trans, intCarGroupId);
						// 删除群组
						int intRetVal = (new SQLHelper()).ExecuteNonQuery(
							trans, SQL_DELETE, new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId)
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
