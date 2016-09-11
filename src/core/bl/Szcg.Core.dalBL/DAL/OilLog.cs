using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using Teamax.Common;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// 油耗日志数据操作类。
	/// </summary>
    public class OilLog : CommonPage
	{
		private const string SQL_SELECT = "SELECT * FROM OilLog WHERE CarId IN (SELECT CarId FROM CarInfo WHERE Area LIKE @Area + '%')";
		private const string SQL_SELECT_BY_LOG_ID = "SELECT * FROM OilLog WHERE LogId = @LogId";
		private const string SQL_INSERT = "INSERT INTO OilLog(CarId, DriverId, Prices, Volume, Address, RefuellingDate, [Description], RecordTime) VALUES(@CarId, @DriverId, @Prices, @Volume, @Address, @RefuellingDate, @Description, @RecordTime)";
		private const string SQL_DELETE_BY_CAR_ID = "DELETE FROM OilLog WHERE CarId = @CarId";
		private const string SQL_DELETE_BY_DRIVER_ID = "DELETE FROM OilLog WHERE DriverId = @DriverId";

		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_DRIVER_ID = "DriverId = @DriverId";
		private const string PART_PRICES = "Prices >= @MinPrices AND Prices <= @MaxPrices";
		private const string PART_VOLUME = "Volume >= @MinVolume AND Volume <= @MaxVolume";
		private const string PART_ADDRESS = "Address LIKE '%' + @Address + '%'";
		private const string PART_REFUELLING_DATE = "RefuellingDate >= @MinRefuellingDate AND RefuellingDate <= @MaxRefuellingDate";
		private const string PART_DESCRIPTION = "Description LIKE '%' + @Description + '%'";
		private const string PART_RECORD_TIME = "RecordTime >= @MinRecordTime AND RecordTime <= @MaxRecordTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_LOG_ID = "@LogId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_DRIVER_ID = "@DriverId";
		private const string PARM_PRICES = "@Prices";
		private const string PARM_MIN_PRICES = "@MinPrices";
		private const string PARM_MAX_PRICES = "@MaxPrices";
		private const string PARM_VOLUME = "@Volume";
		private const string PARM_MIN_VOLUME = "@MinVolume";
		private const string PARM_MAX_VOLUME = "@MaxVolume";
		private const string PARM_ADDRESS = "@Address";
		private const string PARM_REFUELLING_DATE = "@RefuellingDate";
		private const string PARM_MIN_REFUELLING_DATE = "@MinRefuellingDate";
		private const string PARM_MAX_REFUELLING_DATE = "@MaxRefuellingDate";
		private const string PARM_DESCRIPTION = "@Description";
		private const string PARM_RECORD_TIME = "@RecordTime";
		private const string PARM_MIN_RECORD_TIME = "@MinRecordTime";
		private const string PARM_MAX_RECORD_TIME = "@MaxRecordTime";

		private string strArea;

		public OilLog()
		{
            strArea = this.AreaCode;
		}

		/// <summary>
		/// 查询。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intDriverId">司机标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMinPrices">最小价格，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMaxPrices">最大价格，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMinVolume">最小油量，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMaxVolume">最大油量，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strAddress">加油地址，若不想加入该条件请设 null 。</param>
		/// <param name="dttMinRefuellingDate">最小加油时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxRefuellingDate">最大加油时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="strDescription">日志描述，若不想加入该条件请设 null 。</param>
		/// <param name="dttMinRecordTime">最小记录时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxRecordTime">最大记录时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <returns>油耗日志表</returns>
		public SystemDs.OilLogDataTable Select(int intCarId, int intDriverId, double dblMinPrices, double dblMaxPrices, double dblMinVolume, double dblMaxVolume, string strAddress, DateTime dttMinRefuellingDate, DateTime dttMaxRefuellingDate, string strDescription, DateTime dttMinRecordTime, DateTime dttMaxRecordTime)
		{
			string strSQL = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime initialDateTime = new DateTime();

			#region 为执行准备查询语句及参数

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (intCarId >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_CAR_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_ID, intCarId));
			}
			if (intDriverId >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
			}
			if (dblMinPrices >= 0 || dblMaxPrices >= 0)
			{
				if (dblMinPrices < 0)
					dblMinPrices = double.MinValue;
				if (dblMaxPrices < 0)
					dblMinPrices = double.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_PRICES);
				arrParms.Add(new SqlParameter(PARM_MIN_PRICES, dblMinPrices));
				arrParms.Add(new SqlParameter(PARM_MAX_PRICES, dblMaxPrices));
			}
			if (dblMinVolume >= 0 || dblMaxVolume >= 0)
			{
				if (dblMinVolume < 0)
					dblMinVolume = double.MinValue;
				if (dblMaxVolume < 0)
					dblMaxVolume = double.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_VOLUME);
				arrParms.Add(new SqlParameter(PARM_MIN_VOLUME, dblMinVolume));
				arrParms.Add(new SqlParameter(PARM_MAX_VOLUME, dblMaxVolume));
			}
			if (strAddress != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_ADDRESS);
				arrParms.Add(new SqlParameter(PARM_ADDRESS, strAddress));
			}
			if (dttMinRefuellingDate != initialDateTime || dttMaxRefuellingDate != initialDateTime)
			{
				if (dttMinRefuellingDate == initialDateTime)
					dttMinRefuellingDate = DateTime.MinValue;
				if (dttMaxRefuellingDate == initialDateTime)
					dttMaxRefuellingDate = DateTime.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_REFUELLING_DATE);
				arrParms.Add(new SqlParameter(PARM_MIN_REFUELLING_DATE, dttMinRefuellingDate));
				arrParms.Add(new SqlParameter(PARM_MAX_REFUELLING_DATE, dttMaxRefuellingDate));
			}
			if (strDescription != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DESCRIPTION);
				arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
			}
			if (dttMinRecordTime != initialDateTime || dttMaxRecordTime != initialDateTime)
			{
				if (dttMinRecordTime == initialDateTime)
					dttMinRecordTime = DateTime.MinValue;
				if (dttMaxRecordTime == initialDateTime)
					dttMaxRecordTime = DateTime.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_RECORD_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_RECORD_TIME, dttMinRecordTime));
				arrParms.Add(new SqlParameter(PARM_MAX_RECORD_TIME, dttMaxRecordTime));
			}

			#endregion

			strSQL += " ORDER BY RefuellingDate DESC, RecordTime DESC";

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.OilLogDataTable dataTable = new SystemDs.OilLogDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

			return dataTable;
		}

		/// <summary>
		/// 查询(依据车辆标识、最小加油时间、最大加油时间)。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dtmMinRefuellingDate">最小加油时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dtmMaxRefuellingDate">最大加油时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <returns>油耗日志表</returns>
		public SystemDs.OilLogDataTable Select(int intCarId, DateTime dtmMinRefuellingDate, DateTime dtmMaxRefuellingDate)
		{
			return this.Select(intCarId, -1, -1, -1, -1, -1, null, dtmMinRefuellingDate, dtmMaxRefuellingDate, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// 查询(依据日志标识)。
		/// </summary>
		/// <param name="intLogId">日志标识</param>
		/// <returns>油耗日志表</returns>
		public SystemDs.OilLogDataTable SelectByLogId(int intLogId)
		{
			SystemDs.OilLogDataTable dataTable = new SystemDs.OilLogDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, SQL_SELECT_BY_LOG_ID, new SqlParameter(PARM_LOG_ID, intLogId));

			return dataTable;
		}

		/// <summary>
		/// 插入。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想插入值请设负值，如 -1 。</param>
		/// <param name="intDriverId">司机标识，若不想插入值请设负值，如 -1 。</param>
		/// <param name="dblPrices">金额，若不想插入值请设负值，如 -1 。</param>
		/// <param name="dblVolume">油量，若不想插入值请设负值，如 -1 。</param>
		/// <param name="strAddress">地址，若不想插入值请设 null 。</param>
		/// <param name="dttRefuellingDate">加油日期，若不想插入值请设 new DateTime() 。</param>
		/// <param name="strDescription">日志描述，若不想插入值请设 null 。</param>
		/// <param name="dttRecordTime">记录时间，若不想插入值请设 new DateTime() 。</param>
		/// <returns>油耗日志表</returns>
		public bool Insert(int intCarId, int intDriverId, double dblPrices, double dblVolume, string strAddress, DateTime dttRefuellingDate, string strDescription, DateTime dttRecordTime)
		{
			#region 为执行准备参数

			SqlParameter[] parms = new SqlParameter[8];

			if (intCarId < 0)
				parms[0] = new SqlParameter(PARM_CAR_ID, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_CAR_ID, intCarId);

			if (intDriverId < 0)
				parms[1] = new SqlParameter(PARM_DRIVER_ID, DBNull.Value);
			else
				parms[1] = new SqlParameter(PARM_DRIVER_ID, intDriverId);

			if (dblPrices < 0)
				parms[2] = new SqlParameter(PARM_PRICES, DBNull.Value);
			else
				parms[2] = new SqlParameter(PARM_PRICES, dblPrices);

			if (dblVolume < 0)
				parms[3] = new SqlParameter(PARM_VOLUME, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_VOLUME, dblVolume);

			if (strAddress == null)
				parms[4] = new SqlParameter(PARM_ADDRESS, DBNull.Value);
			else
				parms[4] = new SqlParameter(PARM_ADDRESS, strAddress);

			if (dttRefuellingDate == new DateTime())
				parms[5] = new SqlParameter(PARM_REFUELLING_DATE, DBNull.Value);
			else
				parms[5] = new SqlParameter(PARM_REFUELLING_DATE, dttRefuellingDate);

			if (strDescription == null)
				parms[6] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[6] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			if (dttRecordTime == new DateTime())
				parms[7] = new SqlParameter(PARM_RECORD_TIME, DBNull.Value);
			else
				parms[7] = new SqlParameter(PARM_RECORD_TIME, dttRecordTime);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除日志(依据车辆标识)
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="intCarId">车辆标识</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByCarId(SqlTransaction trans, int intCarId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_DELETE_BY_CAR_ID, new SqlParameter(PARM_CAR_ID, intCarId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// 删除日志(依据司机标识)
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="intDriverId">司机标识</param>
		/// <returns>执行是否成功</returns>
		public bool DeleteByDriverId(SqlTransaction trans, int intDirverId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_DELETE_BY_DRIVER_ID, new SqlParameter(PARM_DRIVER_ID, intDirverId)
				);

			return intRetVal > 0 ? true : false;
		}
	}
}
