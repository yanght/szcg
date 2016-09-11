using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// 车辆司机关系数据操作类。
	/// </summary>
	public class CarDriverRelation
	{
		private const string SQL_SELECT = "SELECT * FROM CarDriverRelation";
		private const string SQL_INSERT = "INSERT INTO CarDriverRelation(CarId, DriverId) VALUES(@CarId, @DriverId)";
		private const string SQL_DELETE = "DELETE FROM CarDriverRelation";

		private const string PART_RELATION_ID = "RelationId = @RelationId";
		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_DRIVER_ID = "DriverId = @DriverId";

		private const string PARM_RELATION_ID = "@RelationId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_DRIVER_ID = "@DriverId";

		public CarDriverRelation() {}

		/// <summary>
		/// 查询。
		/// </summary>
		/// <param name="intRelationId">关系标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intDriverId">司机标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆司机关系表</returns>
		public SystemDs.CarDriverRelationDataTable Select(int intRelationId, int intCarId, int intDriverId)
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
			if (intDriverId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarDriverRelationDataTable dataTable = new SystemDs.CarDriverRelationDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// 获取所有。
		/// </summary>
		/// <returns>车辆司机关系表</returns>
		public SystemDs.CarDriverRelationDataTable SelectAll()
		{
			return this.Select(-1, -1, -1);
		}

		/// <summary>
		/// 查询(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆司机关系表</returns>
		public SystemDs.CarDriverRelationDataTable SelectByCarId(int intCarId)
		{
			return this.Select(-1, intCarId, -1);
		}

		/// <summary>
		/// 查询(依据司机标识)。
		/// </summary>
		/// <param name="intDriverId">司机标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆司机关系表</returns>
		public SystemDs.CarDriverRelationDataTable SelectByDriverId(int intDriverId)
		{
			return this.Select(-1, -1, intDriverId);
		}

		/// <summary>
		/// 插入。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想插入值请设负值，如 -1 。</param>
		/// <param name="intCarGroupId">司机标识，若不想插入值请设负值，如 -1 。</param>
		/// <returns>执行是否成功</returns>
		public bool Insert(int intCarId, int intDriverId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_INSERT, 
				new SqlParameter(PARM_CAR_ID, intCarId), 
				new SqlParameter(PARM_DRIVER_ID, intDriverId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除。
		/// </summary>
		/// <param name="intRelationId">关系标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intDriverId">司机标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="trans">现有事务，若不想使用事务请设 null 。</param>
		/// <returns>执行是否成功</returns>
		private bool Delete(SqlTransaction trans, int intRelationId, int intCarId, int intDriverId)
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
			if (intDriverId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
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
		/// 删除(依据司机标识)。
		/// </summary>
		/// <param name="intDriverId">司机标识(值不能为负数)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByDriverId(int intDriverId)
		{
			if (intDriverId < 0)
				throw new Exception("司机标识，值不能为负数。");
			else
				return this.Delete(null, -1, -1, intDriverId);
		}

		/// <summary>
		/// 删除(依据司机标识)。
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="intDriverId">司机标识(值不能为负数)</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByDriverId(SqlTransaction trans, int intDriverId)
		{
			if (intDriverId < 0)
				throw new Exception("司机标识，值不能为负数。");
			else
				return this.Delete(trans, -1, -1, intDriverId);
		}
	}
}
