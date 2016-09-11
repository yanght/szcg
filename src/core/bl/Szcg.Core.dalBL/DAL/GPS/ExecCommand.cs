using System;
using System.Data;
using System.Data.SqlClient;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// 对 GPS 车载终端执行命令数据操作类。
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
		/// 请求终端状态(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool RequestGPSState(string strSIM)
		{
			return this.Command(strSIM, 1, "点名查看", null);
		}

		/// <summary>
		/// 答复终端状态(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>终端状态表</returns>
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
		/// 启动报警(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool Warn(string strSIM)
		{
			return this.Command(strSIM, 3, "启动报警", null);
		}

		/// <summary>
		/// 关闭报警(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool Unwarn(string strSIM)
		{
			return this.Command(strSIM, 4, "关闭报警", null);
		}

		/// <summary>
		/// 关闭油路(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool CloseOil(string strSIM)
		{
			return this.Command(strSIM, 5, "关闭油路", null);
		}

		/// <summary>
		/// 恢复油路(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>执行是否成功</returns>
		public bool SupplyOil(string strSIM)
		{
			return this.Command(strSIM, 6, "恢复油路", null);
		}

		/// <summary>
		/// 车辆调度(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <param name="strMessage">调度信息</param>
		/// <returns>执行是否成功</returns>
		public bool SendMessage(string strSIM, string strMessage)
		{
			return this.Command(strSIM, 7, "车辆调度", strMessage);
		}

		/// <summary>
		/// 设定发送间隔。
		/// </summary>
		/// <param name="strSIM">SIM 卡号。</param>
		/// <param name="strTimeSlot">时间间隔</param>
		/// <returns>执行是否成功</returns>
		public bool SetTimeSlot(string strSIM, string strTimeSlot)
		{
			return this.Command(strSIM, 8, "设定发送间隔", strTimeSlot);
		}
        /// <summary>
        /// 电话监听
        /// </summary>
        /// <param name="strSIM"></param>
        /// <returns></returns>
        public bool TListen(string strSIM,string strSIM2)
        {
            return this.Command(strSIM, 11, "启动电话监听", strSIM2);
        }

		/// <summary>
		/// 请求图象(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>图象标识</returns>
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
						isSuc = (new CommandLog()).Insert(trans, strSIM, "实时图像", "") ? isSuc : false;
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
		/// 答复图像(依据图象标识)。
		/// </summary>
		/// <param name="intImageId">图像标识</param>
		/// <returns>图像内容</returns>
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
		/// 执行命令(依据 SIM 卡号)。
		/// </summary>
		/// <param name="trans">现有事务</param>
		/// <param name="strSIM">SIM 卡号</param>
		/// <param name="intCommand">命令代号</param>
		/// <param name="strContent">命令内容</param>
		/// <returns>执行是否成功</returns>
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
		/// 执行命令(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <param name="intCommand">命令代号</param>
		/// <param name="strCommandName">命令名称</param>
		/// <param name="strContent">命令内容</param>
		/// <returns>执行是否成功</returns>
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
