using System;
using System.Data;
using System.Data.SqlClient;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// �� GPS �����ն�ִ���������ݲ����ࡣ
	/// </summary>
	public class ExecCommand
	{
		private const string SQL_SELECT_GPS_STATE = "SELECT car.SIM, car.Name AS CarName, real.LON AS Longitude, real.LAT AS Latitude, real.SPEED AS Speed, real.HEADING AS Angle, real.StarCount, real.RECTIME AS ReceiveTime FROM P_HIS_GPS real, CarInfo car WHERE real.VCode = @VirtualIP AND real.VCode = car.VirtualIP";
		private const string SQL_SELECT_IMAGE_ID = "SELECT ID AS ImageId FROM TBImage WHERE VCODE = @VirtualIP AND SendTime = @SendTime";
		private const string SQL_SELECT_RESPONSE_IMAGE = "SELECT Image FROM TBImage WHERE ID = @ImageId AND State = 0";
		private const string SQL_INSERT_REQUEST_IMAGE = "INSERT INTO TBImage(VCode, SendTime, State) VALUES(@VirtualIP, @SendTime, 1)";
		private const string SQL_UPDATE_COMMAND = "UPDATE P_REAL_GPS SET VCommand = @Command, CommandCtnt = @Content WHERE VCode = @VirtualIP";

		private const string PARM_COMMAND = "@Command";
		private const string PARM_CONTENT = "@Content";
		private const string PARM_IMAGE_ID = "@ImageId";
		private const string PARM_SEND_TIME = "@SendTime";
		private const string PARM_VIRTUAL_IP = "@VirtualIP";

		public ExecCommand() {}

		/// <summary>
		/// �����ն�״̬(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool RequestGPSState(string strSIM)
		{
			return this.Command(strSIM, 1, "�����鿴", null);
		}

		/// <summary>
		/// ���ն�״̬(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>�ն�״̬��</returns>
		public CarGPSDs.RealGPSStateDataTable ResponseGPSState(string strSIM)
		{
			string strVirtualIP = (new VirtualIP()).Encode(strSIM);

			CarGPSDs.RealGPSStateDataTable dataTable = new CarGPSDs.RealGPSStateDataTable();

			(new SQLHelper()).ExecuteDataTable(
				dataTable, 
				SQL_SELECT_GPS_STATE, 
				new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP)
				);

			return dataTable;
		}

		/// <summary>
		/// ��������(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Warn(string strSIM)
		{
			return this.Command(strSIM, 3, "��������", null);
		}

		/// <summary>
		/// �رձ���(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Unwarn(string strSIM)
		{
			return this.Command(strSIM, 4, "�رձ���", null);
		}

		/// <summary>
		/// �ر���·(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool CloseOil(string strSIM)
		{
			return this.Command(strSIM, 5, "�ر���·", null);
		}

		/// <summary>
		/// �ָ���·(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool SupplyOil(string strSIM)
		{
			return this.Command(strSIM, 6, "�ָ���·", null);
		}

		/// <summary>
		/// ��������(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <param name="strMessage">������Ϣ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool SendMessage(string strSIM, string strMessage)
		{
			return this.Command(strSIM, 7, "��������", strMessage);
		}

		/// <summary>
		/// �趨���ͼ����
		/// </summary>
		/// <param name="strSIM">SIM ���š�</param>
		/// <param name="strTimeSlot">ʱ����</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool SetTimeSlot(string strSIM, string strTimeSlot)
		{
			return this.Command(strSIM, 8, "�趨���ͼ��", strTimeSlot);
		}
        /// <summary>
        /// �绰����
        /// </summary>
        /// <param name="strSIM"></param>
        /// <returns></returns>
        public bool TListen(string strSIM,string strSIM2)
        {
            return this.Command(strSIM, 11, "�����绰����", strSIM2);
        }

		/// <summary>
		/// ����ͼ��(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <returns>ͼ���ʶ</returns>
		public int RequestImage(string strSIM)
		{
			string strVirtualIP = (new VirtualIP()).Encode(strSIM);
			DateTime dtmSendTime = DateTime.Now;

			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						bool isSuc = true;
						int intRetVal = (new SQLHelper()).ExecuteNonQuery(
							trans, 
							SQL_INSERT_REQUEST_IMAGE, 
							new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP), 
							new SqlParameter(PARM_SEND_TIME, dtmSendTime)
							);
						isSuc = intRetVal > 0 ? isSuc : false;
						isSuc = (new CommandLog()).Insert(trans, strSIM, "ʵʱͼ��", "") ? isSuc : false;
						trans.Commit();
						if(!isSuc) return -1;
					}
					catch
					{
						trans.Rollback();
						throw;
					}
				}
			}

			object objRetVal = (new SQLHelper()).ExecuteScalar(
				SQL_SELECT_IMAGE_ID, 
				new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP), 
				new SqlParameter(PARM_SEND_TIME, dtmSendTime)
				);

			if (objRetVal == null)
				return -1;
			else
				return (int)objRetVal;
		}

		/// <summary>
		/// ��ͼ��(����ͼ���ʶ)��
		/// </summary>
		/// <param name="intImageId">ͼ���ʶ</param>
		/// <returns>ͼ������</returns>
		public byte[] ResponseImage(int intImageId)
		{
			object retObj = (new SQLHelper()).ExecuteScalar(
				SQL_SELECT_RESPONSE_IMAGE, 
				new SqlParameter(PARM_IMAGE_ID, intImageId)
				);

			if (retObj != DBNull.Value)
				return (byte[])retObj;
			else
				return null;
		}

		/// <summary>
		/// ִ������(���� SIM ����)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="strSIM">SIM ����</param>
		/// <param name="intCommand">�������</param>
		/// <param name="strContent">��������</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		private bool Command(SqlTransaction trans, string strSIM, int intCommand, string strContent)
		{
			string strVirtualIP = (new VirtualIP()).Encode(strSIM);

			SqlParameter[] parms = new SqlParameter[3];

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, 
				SQL_UPDATE_COMMAND, 
				new SqlParameter(PARM_COMMAND, intCommand), 
				strContent != null ? new SqlParameter(PARM_CONTENT, strContent) : new SqlParameter(PARM_CONTENT, DBNull.Value), 
				new SqlParameter(PARM_VIRTUAL_IP, strVirtualIP)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ִ������(���� SIM ����)��
		/// </summary>
		/// <param name="strSIM">SIM ����</param>
		/// <param name="intCommand">�������</param>
		/// <param name="strCommandName">��������</param>
		/// <param name="strContent">��������</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		private bool Command(string strSIM, int intCommand, string strCommandName, string strContent)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						bool isSuc = true;
						isSuc = this.Command(trans, strSIM, intCommand, strContent) ? isSuc : false;
						isSuc = (new CommandLog()).Insert(trans, strSIM, strCommandName, strContent) ? isSuc : false;
						trans.Commit();
						return isSuc;
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
