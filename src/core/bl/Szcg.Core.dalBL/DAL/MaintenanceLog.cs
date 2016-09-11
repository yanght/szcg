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
	/// ά����־���ݲ�����
	/// </summary>
    public class MaintenanceLog : CommonPage
	{
		private const string SQL_SELECT = "SELECT * FROM MaintenanceLog WHERE CarId IN (SELECT CarId FROM CarInfo WHERE Area LIKE @Area + '%')";
		private const string SQL_SELECT_BY_LOG_ID = "SELECT * FROM MaintenanceLog WHERE LogId = @LogId";
		private const string SQL_INSERT = "INSERT INTO MaintenanceLog(CarId, DriverId, Prices, Address, MaintenanceDate, [Description], RecordTime) VALUES(@CarId, @DriverId, @Prices, @Address, @MaintenanceDate, @Description, @RecordTime)";
		private const string SQL_UPDATE_CLEAR_DRIVER_ID = "UPDATE MaintenanceLog SET DriverId = null WHERE DriverId = @DriverId";
		private const string SQL_DELETE_BY_CAR_ID = "DELETE FROM MaintenanceLog WHERE CarId = @CarId";
		private const string SQL_DELETE_BY_DRIVER_ID = "DELETE FROM MaintenanceLog WHERE DriverId = @DriverId";

		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_DRIVER_ID = "DriverId = @DriverId";
		private const string PART_PRICES = "Prices >= @MinPrices AND Prices <= @MaxPrices";
		private const string PART_ADDRESS = "Address LIKE '%' + @Address + '%'";
		private const string PART_MAINTENANCE_DATE = "MaintenanceDate >= @MinMaintenanceDate AND MaintenanceDate <= @MaxMaintenanceDate";
		private const string PART_DESCRIPTION = "Description LIKE '%' + @Description + '%'";
		private const string PART_RECORD_TIME = "RecordTime >= @MinRecordTime AND RecordTime <= @MaxRecordTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_LOG_ID = "@LogId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_DRIVER_ID = "@DriverId";
		private const string PARM_PRICES = "@Prices";
		private const string PARM_MIN_PRICES = "@MinPrices";
		private const string PARM_MAX_PRICES = "@MaxPrices";
		private const string PARM_ADDRESS = "@Address";
		private const string PARM_MAINTENANCE_DATE = "@MaintenanceDate";
		private const string PARM_MIN_MAINTENANCE_DATE = "@MinMaintenanceDate";
		private const string PARM_MAX_MAINTENANCE_DATE = "@MaxMaintenanceDate";
		private const string PARM_DESCRIPTION = "@Description";
		private const string PARM_RECORD_TIME = "@RecordTime";
		private const string PARM_MIN_RECORD_TIME = "@MinRecordTime";
		private const string PARM_MAX_RECORD_TIME = "@MaxRecordTime";

		private string strArea;

		public MaintenanceLog()
		{
            strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intDriverId">˾����ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="dblMinPrices">��С�������������������踺ֵ���� -1 ��</param>
		/// <param name="dblMaxPrices">���������������������踺ֵ���� -1 ��</param>
		/// <param name="strAddress">ά����ַ�������������������� null ��</param>
		/// <param name="dttMinMaintenanceDate">��Сά��ʱ�䣬������������������ new DateTime() ��</param>
		/// <param name="dttMaxMaintenanceDate">���ά��ʱ�䣬������������������ new DateTime() ��</param>
		/// <param name="strDescription">��־������������������������ null ��</param>
		/// <param name="dttMinRecordTime">��С��¼ʱ�䣬������������������ new DateTime() ��</param>
		/// <param name="dttMaxRecordTime">����¼ʱ�䣬������������������ new DateTime() ��</param>
		/// <returns>ά����־��</returns>
		public SystemDs.MaintenanceLogDataTable Select(int intCarId, int intDriverId, double dblMinPrices, double dblMaxPrices, string strAddress, DateTime dttMinMaintenanceDate, DateTime dttMaxMaintenanceDate, string strDescription, DateTime dttMinRecordTime, DateTime dttMaxRecordTime)
		{
			string strSQL = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime initialDateTime = new DateTime();

			#region Ϊִ��׼����ѯ��估����

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
			if (strAddress != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_ADDRESS);
				arrParms.Add(new SqlParameter(PARM_ADDRESS, strAddress));
			}
			if (dttMinMaintenanceDate != initialDateTime || dttMaxMaintenanceDate != initialDateTime)
			{
				if (dttMinMaintenanceDate == initialDateTime)
					dttMinMaintenanceDate = DateTime.MinValue;
				if (dttMaxMaintenanceDate == initialDateTime)
					dttMaxMaintenanceDate = DateTime.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_MAINTENANCE_DATE);
				arrParms.Add(new SqlParameter(PARM_MIN_MAINTENANCE_DATE, dttMinMaintenanceDate));
				arrParms.Add(new SqlParameter(PARM_MAX_MAINTENANCE_DATE, dttMaxMaintenanceDate));
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

			strSQL += " ORDER BY MaintenanceDate DESC, RecordTime DESC";

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.MaintenanceLogDataTable dataTable = new SystemDs.MaintenanceLogDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ѯ(���ݳ�����ʶ����Сά��ʱ�䡢���ά��ʱ��)��
		/// </summary>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="dtmMinMaintenanceDate">��Сά��ʱ�䣬������������������ new DateTime() ��</param>
		/// <param name="dtmMaxMaintenanceDate">���ά��ʱ�䣬������������������ new DateTime() ��</param>
		/// <returns>ά����־��</returns>
		public SystemDs.MaintenanceLogDataTable Select(int intCarId, DateTime dtmMinMaintenanceDate, DateTime dtmMaxMaintenanceDate)
		{
			return this.Select(intCarId, -1, -1, -1, null, dtmMinMaintenanceDate, dtmMaxMaintenanceDate, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// ��ѯ(������־��ʶ)��
		/// </summary>
		/// <param name="intLogId">��־��ʶ</param>
		/// <returns>�ͺ���־��</returns>
		public SystemDs.MaintenanceLogDataTable SelectByLogId(int intLogId)
		{
			SystemDs.MaintenanceLogDataTable dataTable = new SystemDs.MaintenanceLogDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, SQL_SELECT_BY_LOG_ID, new SqlParameter(PARM_LOG_ID, intLogId));

			return dataTable;
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="intCarId">������ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="intDriverId">˾����ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="dblPrices">�����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="strAddress">��ַ�����������ֵ���� null ��</param>
		/// <param name="dttMaintenanceDate">ά�����ڣ����������ֵ���� new DateTime() ��</param>
		/// <param name="strDescription">��־���������������ֵ���� null ��</param>
		/// <param name="dttRecordTime">��¼ʱ�䣬���������ֵ���� new DateTime() ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(int intCarId, int intDriverId, double dblPrices, string strAddress, DateTime dttMaintenanceDate, string strDescription, DateTime dttRecordTime)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[7];

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

			if (strAddress == null)
				parms[3] = new SqlParameter(PARM_ADDRESS, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_ADDRESS, strAddress);

			if (dttMaintenanceDate == new DateTime())
				parms[4] = new SqlParameter(PARM_MAINTENANCE_DATE, DBNull.Value);
			else
				parms[4] = new SqlParameter(PARM_MAINTENANCE_DATE, dttMaintenanceDate);

			if (strDescription == null)
				parms[5] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[5] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			if (dttRecordTime == new DateTime())
				parms[6] = new SqlParameter(PARM_RECORD_TIME, DBNull.Value);
			else
				parms[6] = new SqlParameter(PARM_RECORD_TIME, dttRecordTime);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ���˾����ʶ(����˾����ʶ)
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool UpdateClearDriverId(SqlTransaction trans, int intDriverId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_UPDATE_CLEAR_DRIVER_ID, new SqlParameter(PARM_DRIVER_ID, intDriverId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ����־(���ݳ�����ʶ)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="intCarId">������ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByCarId(SqlTransaction trans, int intCarId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_DELETE_BY_CAR_ID, new SqlParameter(PARM_CAR_ID, intCarId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ����־(����˾����ʶ)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByDriverId(SqlTransaction trans, int intDriverId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_DELETE_BY_DRIVER_ID, new SqlParameter(PARM_DRIVER_ID, intDriverId)
				);

			return intRetVal > 0 ? true : false;
		}
	}
}
