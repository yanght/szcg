using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// 车辆群组关系数据操作类。
	/// </summary>
	public class CarGroupRelation
	{
		private const string SQL_SELECT = "SELECT * FROM CarGroupRelation";
		private const string SQL_INSERT = "INSERT INTO CarGroupRelation(CarId, CarGroupId) VALUES(@CarId, @CarGroupId)";
		private const string SQL_DELETE = "DELETE FROM CarGroupRelation";

		private const string PART_RELATION_ID = "RelationId = @RelationId";
		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_CAR_GROUP_ID = "CarGroupId = @CarGroupId";

		private const string PARM_RELATION_ID = "@RelationId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_CAR_GROUP_ID = "@CarGroupId";

		public CarGroupRelation() {}

		/// <summary>
		/// 查询。
		/// </summary>
		/// <param name="intRelationId">关系标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarGroupId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆群组关系表</returns>
		public SystemDs.CarGroupRelationDataTable Select(int intRelationId, int intCarId, int intCarGroupId)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region 为执行准备查询语句及参数

			if (intRelationId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_RELATION_ID);
				arrParms.Add(new SqlParameter(PARM_RELATION_ID, intRelationId));
			}
			if (intCarId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_ID, intCarId));
			}
			if (intCarGroupId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_GROUP_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarGroupRelationDataTable dataTable = new SystemDs.CarGroupRelationDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// 获取所有。
		/// </summary>
		/// <returns>车辆群组关系表</returns>
		public SystemDs.CarGroupRelationDataTable SelectAll()
		{
			return this.Select(-1, -1, -1);
		}

		/// <summary>
		/// 查询(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆群组关系表</returns>
		public SystemDs.CarGroupRelationDataTable SelectByCarId(int intCarId)
		{
			return this.Select(-1, intCarId, -1);
		}

		/// <summary>
		/// 查询(依据群组标识)。
		/// </summary>
		/// <param name="intDriverId">群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆群组关系表</returns>
		public SystemDs.CarGroupRelationDataTable SelectByCarGroupId(int intGroupId)
		{
			return this.Select(-1, -1, intGroupId);
		}

		/// <summary>
		/// 插入。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想插入值请设负值，如 -1 。</param>
		/// <param name="intCarGroupId">群组标识，若不想插入值请设负值，如 -1 。</param>
		/// <returns>执行是否成功</returns>
		public bool Insert(int intCarId, int intCarGroupId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_INSERT, 
				new SqlParameter(PARM_CAR_ID, intCarId), 
				new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除。
		/// </summary>
		/// <param name="trans">现有事务，若不想使用事务请设 null 。</param>
		/// <param name="intRelationId">关系标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarGroupId">群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>执行是否成功</returns>
		private bool Delete(SqlTransaction trans, int intRelationId, int intCarId, int intCarGroupId)
		{
			string strDelete = SQL_DELETE;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region 为执行准备查询语句及参数

			if (intRelationId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_RELATION_ID);
				arrParms.Add(new SqlParameter(PARM_RELATION_ID, intRelationId));
			}
			if (intCarId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_CAR_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_ID, intCarId));
			}
			if (intCarGroupId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_CAR_GROUP_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			int intRetVal;
			if (trans == null)
				intRetVal = (new SQLHelper()).ExecuteNonQuery(strDelete, parms);
			else
				intRetVal = (new SQLHelper()).ExecuteNonQuery(trans, strDelete, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识(值不能为负数)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByCarId(int intCarId)
		{
			if (intCarId < 0)
				throw new Exception("车辆标识，值不能为负数。");
			else
				return this.Delete(null, -1, intCarId, -1);
		}

		/// <summary>
		/// 删除(依据车辆标识)。
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="intCarId">车辆标识(值不能为负数)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByCarId(SqlTransaction trans, int intCarId)
		{
			if (intCarId < 0)
				throw new Exception("车辆标识，值不能为负数。");
			else
				return this.Delete(trans, -1, intCarId, -1);
		}

		/// <summary>
		/// 删除(依据群组标识)。
		/// </summary>
		/// <param name="intCarGroupId">群组标识(值不能为负数)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByCarGroupId(int intCarGroupId)
		{
			if (intCarGroupId < 0)
				throw new Exception("车辆群组标识，值不能为负数。");
			else
				return this.Delete(null, -1, -1, intCarGroupId);
		}

		/// <summary>
		/// 删除(依据群组标识)。
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="intCarGroupId">群组标识(值不能为负)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByCarGroupId(SqlTransaction trans, int intCarGroupId)
		{
			if (intCarGroupId < 0)
				throw new Exception("车辆群组标识，值不能为负数。");
			else
				return this.Delete(trans, -1, -1, intCarGroupId);
		}
	}
}
