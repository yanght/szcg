using System;

namespace szcg.com.teamax.business.entity
{
	/// <summary>
	/// GroupInfo 的摘要说明。
	/// </summary>
	public class GroupInfo
	{
		private int usercode;
		private int groupcode;
		private string groupname;
		private string username;
		private string memo;

		public GroupInfo()
		{
			
		}

		public void setUsercode(int usercode)
		{
			this.usercode=usercode;
		}

		public int getUsercode()
		{
			return this.usercode;
		}

		public int getGroupcode()
		{
			return this.groupcode;
		}

		public void setGroupcode(int groupcode)
		{
			this.groupcode=groupcode;
		}

		public void setGroupname(string groupname)
		{
			this.groupname=groupname;
		}

		public string getGroupname()
		{
			return this.groupname;
		}

		public void setUsername(string username)
		{
			this.username=username;
		}

		public string getUsername()
		{
			return this.username;
		}

		public void setMemo(string memo)
		{
			this.memo=memo;
		}

		public string getMemo()
		{
			return this.memo;
		}
	}
}
