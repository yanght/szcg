using System;

namespace szcg.com.teamax.szbase.entity
{
	/// <summary>
	/// User 的摘要说明。
	/// </summary>
	public class User
	{
		int usercode = 0;
		string username = "";
		string loginname = "";
		string sex = "";
		string tel = "";
		string mobile = "";
        string birthday = "";
		string address = "";
		string departname = "";
		string areacode ="";
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
		public string LoginName
		{
			get {return loginname;}
			set { loginname = value;}
		}
		public string Sex
		{
			get { return sex;}
			set { sex = value;}
		}
		public string Tel
		{
			get { return tel;}
			set { tel = value;}
		}
		public string Mobile
		{
			get { return mobile;}
			set { mobile = value;}
		}
		public string Birthday
		{
			get { return birthday;}
			set { birthday = value;}
		}
		public string Address
		{
			get { return address;}
			set { address = value;}
		}
		public string DepartName
		{
			get { return departname;}
			set { departname = value;}
		}
		public string AreaCode
		{
			get { return areacode;}
			set { areacode = value;}
		}
	}
}
