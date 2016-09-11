using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// ������־���ݲ����ࡣ
	/// </summary>
    public class CommandLog : Teamax.Common.CommonPage
	{
		private const string SQL_SELECT = "SELECT log.LogId, log.SIM, car.Name AS CarName, log.CommandName, log.CommandContent, log.HostAddress, log.SendTime FROM CommandLog log, CarInfo car WHERE car.CarId IN (SELECT CarId FROM CarInfo WHERE Area LIKE @Area + '%') AND log.SIM = car.SIM";
		private const string SQL_INSERT = "INSERT INTO CommandLog(SIM, CommandName, CommandContent, HostAddress, SendTime) VALUES(@SIM, @CommandName, @CommandContent, @HostAddress, @SendTime)";
		private const string SQL_DELETE = "DELETE FROM CommandLog WHERE SIM = @SIM";

		private const string PART_SIM = "log.SIM = @SIM";
		private const string PART_SEND_TIME = "SendTime BETWEEN @MinSendTime AND @MaxSendTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_SIM = "@SIM";
		private const string PARM_COMMAND_NAME = "@CommandName";
		private const string PARM_COMMAND_CONTENT = "@CommandContent";
		private const string PARM_HOST_ADDRESS = "@HostAddress";
		private const string PARM_SEND_TIME = "@SendTime";
		private const string PARM_MIN_SEND_TIME = "@MinSendTime";
		private const string PARM_MAX_SEND_TIME = "@MaxSendTime";

		private string strArea;

		public CommandLog()
		{
            //strArea = (string)HttpContext.Current.Session["CurrentRoleArea"];
            strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ������־(���� SIM ���š�����)��
		/// </summary>
		/// <param name="strSIM">SIM ���ţ������������������踺ֵ���� -1 ��</param>
		/// <param name="dtmMinSendTime">��С���ڣ������������������� new DateTime() ��</param>
		/// <param name="dtmMaxSendTime">������ڣ������������������� new DateTime() ��</param>
		/// <returns>������־��</returns>
		public CarGPSDs.CommandLogDataTable Select(string strSIM, DateTime dtmMinSendTime, DateTime dtmMaxSendTime)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime dtmInitial = new DateTime();

			#region Ϊִ��׼����ѯ��估����

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (strSIM != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_SIM);
				arrParms.Add(new SqlParameter(PARM_SIM, strSIM));
			}
			if (dtmMinSendTime != dtmInitial || dtmMaxSendTime != dtmInitial)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_SEND_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_SEND_TIME, dtmMinSendTime));
				arrParms.Add(new SqlParameter(PARM_MAX_SEND_TIME, dtmMaxSendTime));
			}

			#endregion

			strSelect += " ORDER BY SendTime DESC";

			CarGPSDs.CommandLogDataTable dataTable = new CarGPSDs.CommandLogDataTable();

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// ����������־��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="strSIM">SIM ����</param>
		/// <param name="strCommandName">��������</param>
		/// <param name="strCommandContent">��������</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(SqlTransaction trans, string strSIM, string strCommandName, string strCommandContent)
		{
			if (strSIM == null)
				throw new ArgumentNullException("strSIM", "SIM ���Ų���Ϊ��!");
			if (strCommandName == null)
				throw new ArgumentNullException("strCommandName", "����������Ϊ��!");

			string strHostAddress = HttpContext.Current.Request.UserHostAddress;
			DateTime dtmSendTime = DateTime.Now;

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, 
				SQL_INSERT, 
				new SqlParameter(PARM_SIM, strSIM),
				new SqlParameter(PARM_COMMAND_NAME, strCommandName), 
				strCommandContent == null ? new SqlParameter(PARM_COMMAND_CONTENT, DBNull.Value) : new SqlParameter(PARM_COMMAND_CONTENT, strCommandContent), 
				new SqlParameter(PARM_HOST_ADDRESS, strHostAddress), 
				new SqlParameter(PARM_SEND_TIME, dtmSendTime)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��������־(���� SIM ����)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="strSIM">SIM ���š�</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Delete(SqlTransaction trans, string strSIM)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, 
				SQL_DELETE, 
				new SqlParameter(PARM_SIM, strSIM)
				);

			return intRetVal > 0 ? true : false;
		}
	}
}
