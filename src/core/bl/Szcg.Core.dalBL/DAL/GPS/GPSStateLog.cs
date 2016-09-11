using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// 车载 GPS 终端状态日志数据操作类。
	/// </summary>
    public class GPSStateLog : Teamax.Common.CommonPage
	{
		private const string SQL_SELECT = "SELECT his.HisId AS LogId, car.SIM, car.Name AS CarName, his.LON AS Longitude, his.LAT AS Latitude, his.SPEED AS Speed, his.HEADING AS Angle, his.StarCount, his.RECTIME AS ReceiveTime FROM P_HIS_GPS his, CarInfo car WHERE his.VCode IN (SELECT VirtualIP FROM CarInfo WHERE Area LIKE @Area + '%') AND his.VCode = car.VirtualIP";
		private const string SQL_DELETE = "DELETE FROM P_HIS_GPS WHERE VCode = @VirtualIP";

		private const string PART_SIM = "SIM = @SIM";
		private const string PART_RECEIVE_TIME = "RECTIME BETWEEN @MinReceiveTime AND @MaxReceiveTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_SIM = "@SIM";
		private const string PARM_VIRTUAL_IP = "@VirtualIP";
		private const string PARM_MIN_RECEIVE_TIME = "@MinReceiveTime";
		private const string PARM_MAX_RECEIVE_TIME = "@MaxReceiveTime";

		private string strArea;

		public GPSStateLog() 
		{
            //strArea = (string)HttpContext.Current.Session["CurrentRoleArea"];
            this.strArea = this.AreaCode;
		}

		/// <summary>
		/// 查询终端状态日志(依据 SIM 卡号、最小日期、最大日期)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dtmMinReceiveTime">最小日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dtmMaxReceiveTime">最大日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <returns></returns>
		public CarGPSDs.GPSStateLogDataTable Select(string strSIM, DateTime dtmMinReceiveTime, DateTime dtmMaxReceiveTime)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime dtmInitial = new DateTime();

			#region 为执行准备查询语句及参数

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (strSIM != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_SIM);
				arrParms.Add(new SqlParameter(PARM_SIM, strSIM));
			}
			if (dtmMinReceiveTime != dtmInitial || dtmMaxReceiveTime != dtmInitial)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_RECEIVE_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_RECEIVE_TIME, dtmMinReceiveTime));
				arrParms.Add(new SqlParameter(PARM_MAX_RECEIVE_TIME, dtmMaxReceiveTime));
			}

			#endregion

			CarGPSDs.GPSStateLogDataTable dataTable = new CarGPSDs.GPSStateLogDataTable();

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// 删除终端状态日志(依据 SIM 卡号)。
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool Delete(SqlTransaction trans, string strSIM)
		{
			string strVirtualIP = (new VirtualIP()).Encode(strSIM);

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, 
				SQL_DELETE, 
				new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP)
				);

			return intRetVal > 0 ? true : false;
		}
	}
}
