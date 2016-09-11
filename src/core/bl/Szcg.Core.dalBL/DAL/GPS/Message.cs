using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// ������Ϣ���ݲ����ࡣ
	/// </summary>
    public class Message : Teamax.Common.CommonPage
	{
		private const string SQL_SELECT = "SELECT msg.MsgId AS MessageId, car.SIM, car.Name AS CarName, msg.MSG AS Content, msg.DIRECTION AS Direction, msg.ISREAD AS IsRead, msg.RECTIME AS ReceiveTime FROM P_MSG_GPS msg, CarInfo car WHERE msg.VCode IN (SELECT VirtualIP FROM CarInfo WHERE Area LIKE @Area + '%') AND msg.VCode = car.VirtualIP";
		private const string SQL_UPDATE = "UPDATE P_MSG_GPS SET ISREAD = 1 WHERE MsgId = @MessageId";
		private const string SQL_DELETE = "DELETE FROM P_MSG_GPS WHERE VCode = @VirtualIP";

		private const string PART_MESSAGE_ID = "MsgId = @MessageId";
		private const string PART_DIRECTION = "DIRECTION = @Direction";
		private const string PART_IS_READ = "ISREAD = @IsRead";
		private const string PART_RECEIVE_TIME = "RECTIME BETWEEN @MinReceiveTime AND @MaxReceiveTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_VIRTUAL_IP = "@VirtualIP";
		private const string PARM_MESSAGE_ID = "@MessageId";
		private const string PARM_DIRECTION = "@Direction";
		private const string PARM_IS_READ = "@IsRead";
		private const string PARM_MIN_RECEIVE_TIME = "@MinReceiveTime";
		private const string PARM_MAX_RECEIVE_TIME = "@MaxReceiveTime";

		private string strArea;

		public Message() 
		{
            //strArea = (string)HttpContext.Current.Session["CurrentRoleArea"];
            strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ��Ϣ(������Ϣ��ʶ����Ϣ�������ڣ��Ƿ��Ѷ�)��
		/// </summary>
		/// <param name="intMessageId">��Ϣ��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="strDirection">��Ϣ���������������������� null ��</param>
		/// <param name="dtmDate">���ڣ������������������� new DateTime() ��</param>
		/// <param name="intIsRead">�Ƿ��Ѷ��������������������踺ֵ���� -1 ��</param>
		/// <returns>������Ϣ��</returns>
		public CarGPSDs.MessageDataTable Select(int intMessageId, string strDirection, DateTime dtmDate, int intIsRead)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime initDateTime = new DateTime();

			#region Ϊִ��׼����ѯ��估����

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (intMessageId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_MESSAGE_ID);
				arrParms.Add(new SqlParameter(PARM_MESSAGE_ID, intMessageId));
			}
			if (strDirection != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_DIRECTION);
				arrParms.Add(new SqlParameter(PARM_DIRECTION, strDirection));
			}
			if (dtmDate != initDateTime)
			{
				DateTime dtmMinReceiveTime = Convert.ToDateTime(dtmDate.ToShortDateString());
				DateTime dtmMaxReceiveTime = dtmMinReceiveTime.AddDays(1).AddSeconds(-1);

				strSelect = sqlUtility.AddQualification(strSelect, PART_RECEIVE_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_RECEIVE_TIME, dtmMinReceiveTime));
				arrParms.Add(new SqlParameter(PARM_MAX_RECEIVE_TIME, dtmMaxReceiveTime));
			}
			if (intIsRead >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_IS_READ);
				arrParms.Add(new SqlParameter(PARM_IS_READ, intIsRead));
			}

			strSelect += " ORDER BY RECTIME DESC";

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			CarGPSDs.MessageDataTable dataTable = new CarGPSDs.MessageDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ѯ��Ϣ(������Ϣ��ʶ)��
		/// </summary>
		/// <param name="intMessageId">��Ϣ��ʶ</param>
		/// <returns>������Ϣ��</returns>
		public CarGPSDs.MessageDataTable SelectById(int intMessageId)
		{
			return this.Select(intMessageId, null, new DateTime(), -1);
		}

		/// <summary>
		/// �Ķ���Ϣ(��ָ������Ϣ����Ϊ�Ѷ�״̬)��
		/// </summary>
		/// <param name="intMessageId">��Ϣ��ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool ReadMessage(int intMessageId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_UPDATE, 
				new SqlParameter(PARM_MESSAGE_ID, intMessageId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��������Ϣ(����ָ�� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Delete(string strSIM)
		{
			string strVirtualIP = (new VirtualIP()).Encode(strSIM);

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_DELETE, 
				new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP)
				);

			return intRetVal > 0 ? true : false;
		}
	}
}
