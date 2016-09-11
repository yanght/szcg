using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace szcg.com.teamax
{
	/// <summary>
	/// LoginLog ��ժҪ˵����
	/// </summary>
	public class LoginLog
	{
		private const string SQL_INSERT_SESSION_INFO = "INSERT INTO loginLog(sessionId, hostAddress, platform, browserType, startTime, endTime, areaCode) values(@sessionId, @hostAddress, @platform, @browserType, @startTime, @endTime, @areaCode)";
		private const string SQL_INSERT_USER_LOGIN_INFO = "UPDATE loginLog SET userCode = @userCode, loginTime = @loginTime where sessionId = ( SELECT sessionId FROM loginLog WHERE logId = ( SELECT max(logId) FROM loginLog where sessionId = @sessionId) )";
		private const string SQL_UPDATE_USER_LOGOUT_TIME = "UPDATE loginLog SET logoutTime = @logoutTime where sessionId = ( SELECT sessionId FROM loginLog WHERE logId = ( SELECT max(logId) FROM loginLog where sessionId = @sessionId) )";
		private const string SQL_UPDATE_END_TIME = "UPDATE loginLog SET endTime = @endTime where sessionId = ( SELECT sessionId FROM loginLog WHERE logId = ( SELECT max(logId) FROM loginLog where sessionId = @sessionId) )";
		private const string SQL_INSERT_ONLINE_INFO = "INSERT INTO onlineLog(sessionCount, userCount, currentTime) values(@sessionCount, @userCount, @currentTime)";
		private const string PARM_SESSION_ID = "@sessionId";
		private const string PARM_USER_CODE = "@userCode";
		private const string PARM_HOST_ADDRESS = "@hostAddress";
		private const string PARM_PLATFORM = "@platform";
		private const string PARM_BROWSER_TYPE = "@browserType";
		private const string PARM_START_TIME = "@startTime";
		private const string PARM_LOGIN_TIME = "@loginTime";
		private const string PARM_LOGOUT_TIME = "@logoutTime";
		private const string PARM_END_TIME = "@endTime";
		private const string PARM_SESSION_COUNT = "@sessionCount";
		private const string PARM_USER_COUNT = "@userCount";
		private const string PARM_CURRENT_TIME = "@currentTime";
		private const string PARM_AREA_CODE = "@areacode";

        private string connString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];

		public LoginLog()
		{

		}

		/// <summary>
		/// �����ݿ��в��� Session ��Ϣ���Լ�¼���������û���������Ϣ
		/// </summary>
		/// <param name="strSessionId">Session ��ʶ</param>
		/// <param name="strHostAddress">Զ�̿ͻ��˵� IP ������ַ</param>
		/// <param name="strPlatform">�ͻ���ʹ�õ�ƽ̨����</param>
		/// <param name="strBrowserType">�ͻ�������������ƺ��������������汾��</param>
		/// <param name="dttCurrentTime">��������ǰʱ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool InsertSessionInfo(string strSessionId, string strHostAddress, string strPlatform, string strBrowserType, DateTime dttCurrentTime, string strAreaCode)
		{
			try
			{
				using( SqlConnection conn = new SqlConnection(connString) )
				{
					SqlCommand cmd = new SqlCommand();

					cmd.Connection = conn;
					cmd.CommandText = SQL_INSERT_SESSION_INFO;
					cmd.CommandType = CommandType.Text;

					cmd.Parameters.AddWithValue(PARM_SESSION_ID, strSessionId);
                    cmd.Parameters.AddWithValue(PARM_HOST_ADDRESS, strHostAddress);
                    cmd.Parameters.AddWithValue(PARM_PLATFORM, strPlatform);
                    cmd.Parameters.AddWithValue(PARM_BROWSER_TYPE, strBrowserType);
                    cmd.Parameters.AddWithValue(PARM_START_TIME, dttCurrentTime);
                    cmd.Parameters.AddWithValue(PARM_END_TIME, dttCurrentTime.AddMinutes(HttpContext.Current.Session.Timeout));
                    cmd.Parameters.AddWithValue(PARM_AREA_CODE, strAreaCode);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ���루���£��û���¼��Ϣ
		/// </summary>
		/// <param name="strSessionId">Session ��ʶ</param>
		/// <param name="intUserCode">�û����</param>
		/// <param name="dttLoginTime">��¼ʱ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool InsertUserLoginInfo(string strSessionId, int intUserCode, DateTime dttLoginTime)
		{
			try
			{
				using( SqlConnection conn = new SqlConnection(connString) )
				{
					SqlCommand cmd = new SqlCommand();

					cmd.Connection = conn;
					cmd.CommandText = SQL_INSERT_USER_LOGIN_INFO;
					cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(PARM_SESSION_ID, strSessionId);
                    cmd.Parameters.AddWithValue(PARM_USER_CODE, intUserCode);
                    cmd.Parameters.AddWithValue(PARM_LOGIN_TIME, dttLoginTime);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ���루���£�Session ����ʱ��
		/// </summary>
		/// <param name="strSessionId">Session ��ʶ</param>
		/// <param name="dttCurrentTime">��ǰʱ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool UpdateEndTime(string strSessionId, DateTime dttCurrentTime)
		{
			try
			{
				using( SqlConnection conn = new SqlConnection(connString) )
				{
					SqlCommand cmd = new SqlCommand();

					cmd.Connection = conn;
					cmd.CommandText = SQL_UPDATE_END_TIME;
					cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(PARM_SESSION_ID, strSessionId);
                    cmd.Parameters.AddWithValue(PARM_LOGOUT_TIME, dttCurrentTime);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ���루���£��ǳ�ʱ��
		/// </summary>
		/// <param name="strSessionId">Session ��ʶ</param>
		/// <param name="dttCurrentTime">��ǰʱ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool UpdateLogoutTime(string strSessionId, DateTime dttCurrentTime)
		{
			try
			{
				using( SqlConnection conn = new SqlConnection(connString) )
				{
					SqlCommand cmd = new SqlCommand();

					cmd.Connection = conn;
					cmd.CommandText = SQL_UPDATE_USER_LOGOUT_TIME;
					cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(PARM_SESSION_ID, strSessionId);
                    cmd.Parameters.AddWithValue(PARM_LOGOUT_TIME, dttCurrentTime);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// ���뵱ǰ�û�����ͳ����Ϣ
		/// </summary>
		/// <param name="intSessionCount">��ǰ Session ��</param>
		/// <param name="intUserCount">��ǰ�û���</param>
		/// <param name="dttCurrentTime">��ǰʱ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool InsertOnlineLog(int intSessionCount, int intUserCount, DateTime dttCurrentTime)
		{
			try
			{
				using( SqlConnection conn = new SqlConnection(connString) )
				{
					SqlCommand cmd = new SqlCommand();

					cmd.Connection = conn;
					cmd.CommandText = SQL_INSERT_ONLINE_INFO;
					cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(PARM_SESSION_COUNT, intSessionCount);
                    cmd.Parameters.AddWithValue(PARM_USER_COUNT, intUserCount);
                    cmd.Parameters.AddWithValue(PARM_CURRENT_TIME, dttCurrentTime);

					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
