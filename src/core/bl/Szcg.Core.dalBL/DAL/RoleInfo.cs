using System;

namespace SZCG.GPS.Web
{
	public class RoleInfo
	{
		private string strRoleId;
		private string strName;
		private string strPower;
		private string strArea;

		public RoleInfo(string strRoleId, string strName, string strPower, string strArea)
		{
			this.strRoleId = strRoleId;
			this.strName = strName;
			this.strPower = strPower;
			this.strArea = strArea;
		}

		public string RoleId
		{
			get { return this.strRoleId; }
		}

		public string Name
		{
			get { return this.strName; }
		}

		public string Power
		{
			get { return this.strPower; }
		}

		public string Area
		{
			get { return this.strArea; }
		}
	}
}
