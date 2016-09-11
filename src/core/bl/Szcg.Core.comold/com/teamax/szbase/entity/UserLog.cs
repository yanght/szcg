using System;

namespace szcg.com.teamax.szbase.entity
{
	public class UserLog
	{
		int id = 0;
		int usercode = 0;
		string username = "";
		string logdate = "";
		string modelcode = "";
		string modelname = "";
		string detail = "";

		public int ID
		{
			get { return id;}
			set { id = value;}
		}
		public int UserCode
		{
			get { return usercode;}
			set { usercode = value;}
		}
		public string UserName
		{
			get { return username;}
			set { username = value;}
		}
		public string LogDate
		{
			get { return logdate;}
			set { logdate = value;}
		}
		public string ModelCode
		{
			get {return modelcode;}
			set { modelcode = value;}
		}
		public string ModelName
		{
			get { return modelname;}
			set { modelname = value;}
		}
		public string Detail
		{
			get { return detail;}
			set { detail = value;}
		}
	}
}
