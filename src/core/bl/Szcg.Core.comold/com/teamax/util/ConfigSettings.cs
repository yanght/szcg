using System.Configuration;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// 配置设置类,所有AppSetting中字段的值
	/// add by yxw(2006-05-24)
	/// </summary>
	public class ConfigSettings
	{
		private static string connection = null;
		private static string uploadFilePath = null;
		private static string downFtpIp = null;
		private static string downFtpUserName = null;
		private static string downFtpPassWord = null;
		private static string Ip = null;
		private static string GpsIp = null;
		private static string DeasIp = null;
		private static string OAIP = null;
		private static string WebIp = null;
        private static string soundFileUrl = null;

		private ConfigSettings()
		{
		}

		/// <summary>
		/// 连接字符串
		/// </summary>
		public static string Connection
		{
			get
			{
				if (connection == null)
					connection = System.Configuration.ConfigurationManager.AppSettings["ConnString"] as string;
				return connection;
			}
		}

		/// <summary>
		/// 上传文件路径
		/// </summary>
		public static string UploadFilePath
		{
			get
			{
				if (uploadFilePath == null)
                    uploadFilePath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"] as string;
				return uploadFilePath;
			}
		}

		public static string UploadIp
		{
			get
			{
				if (Ip == null)
                    Ip = System.Configuration.ConfigurationManager.AppSettings["IP"] as string;
				return Ip;
			}
		}

		public static string UploadGPSIp
		{
			get
			{
				if (GpsIp == null)
                    GpsIp = System.Configuration.ConfigurationManager.AppSettings["GPSIP"] as string;
				return GpsIp;
			}
		}
		
		public static string UploadDEASIp
		{
			get
			{
				if(DeasIp == null)
                    DeasIp = System.Configuration.ConfigurationManager.AppSettings["DEASIP"] as string;
				return DeasIp;
			}
		}

		public static string UploadOAIp
		{
			get
			{
				if(OAIP == null)
                    OAIP = System.Configuration.ConfigurationManager.AppSettings["OAIP"] as string;
				return OAIP;
			}
		}

		public static string UploadWEBIp
		{
			get
			{
				if(WebIp == null)
                    WebIp = System.Configuration.ConfigurationManager.AppSettings["WEBIP"] as string;
				return WebIp;
			}
		}


		public static string DownFtpIp
		{
			get
			{
				if(downFtpIp == null)
                    downFtpIp = System.Configuration.ConfigurationManager.AppSettings["FtpIP"] as string;
				return downFtpIp;
			}
		}

		public static string DownFtpUserName
		{
			get
			{
				if(downFtpUserName == null)
                    downFtpUserName = System.Configuration.ConfigurationManager.AppSettings["FtpUserName"] as string;
				return downFtpUserName;
			}
		}

		public static string DownFtpPassWord
		{
			get
			{
				if(downFtpPassWord == null)
                    downFtpPassWord = System.Configuration.ConfigurationManager.AppSettings["FtpPassWord"] as string;
				return downFtpPassWord;
			}
		}

        public static string SonudFileUrl
        {
            get
            {
                if (soundFileUrl == null)
                    soundFileUrl = System.Configuration.ConfigurationManager.AppSettings["SoundFileUrl"] as string;
                return soundFileUrl;
            }
        }
	}
}
