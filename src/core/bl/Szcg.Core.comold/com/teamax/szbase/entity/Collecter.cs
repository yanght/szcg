using System;

namespace szcg.com.teamax.szbase.entity
{
	/// <summary>
	/// collecter 的摘要说明。
	/// </summary>
	public class Collecter
	{
		private int collcode;
		private int commcode;
		private string gridcode;
		private string numbercode;
		private string collname;
		private string loginname;
		private string pwd;
		private string sex;
		private string mobile;
		private string age;
		private string url;
		private string tel;
		private string address;
		private string memo;

		public Collecter()
		{
		}

		public int CollCode
		{
			get { return collcode;}
			set { collcode = value;}
		}
		public int CommCode
		{
			get { return commcode;}
			set { commcode = value;}
		}
		public string GridCode
		{
			get { return gridcode;}
			set { gridcode = value;}
		}
		public string NumberCode
		{
			get { return numbercode;}
			set { numbercode = value;}
		}
		public string CollName
		{
			get { return collname;}
			set { collname = value;}
		}
		public string LoginName
		{
			get { return loginname;}
			set { loginname = value;}
		}
		public string Pwd
		{
			get { return pwd;}
			set { pwd = value;}
		}
		public string Sex
		{
			get { return sex;}
			set { sex = value;}
		}
		public string Mobile
		{
			get { return mobile;}
			set { mobile = value;}
		}
		public string Age
		{
			get { return age;}
			set { age = value;}
		}

		public string Url
		{
			get { return url;}
			set { url = value;}
		}
		public string Tel
		{
			get { return tel;}
			set { tel = value;}
		}
		public string Address
		{
			get { return address;}
			set { address = value;}
		}
		public string Memo
		{
			get { return memo;}
			set { memo = value;}
		}
	}
}
