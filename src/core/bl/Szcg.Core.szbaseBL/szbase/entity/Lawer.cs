using System;

namespace bacgBL.web.szbase.entity
{
	/// <summary>
	/// Lawer 的摘要说明。
	/// </summary>
	public class Lawer
	{
		private string lawercode;
		private string gridcode;
		private string lawername;
		private string loginname;
		private string mobile;
		private string tel;
		private string address;

		public Lawer()
		{
		}

		public string LawerCode
		{
			get { return lawercode;}
			set { lawercode = value;}
		}
		public string GridCode
		{
			get { return gridcode;}
			set { gridcode = value;}
		}
		public string LawerName
		{
			get { return lawername;}
			set { lawername = value;}
		}
		public string LoginName
		{
			get { return loginname;}
			set { loginname = value;}
		}
		public string Mobile
		{
			get { return mobile;}
			set { mobile = value;}
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
		
	}
}
