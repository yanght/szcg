using System;

namespace szcg.com.teamax.business.archives
{
	/// <summary>
	/// BUSINESS_Depart 的摘要说明。
	/// </summary>
	public class BUSINESS_Departs
	{
		protected string departcode;
		protected string departname;
		protected string parentcode;
		protected string memo;

		public BUSINESS_Departs()
		{
			
		}

		public BUSINESS_Departs(String[] data)
		{
			for(int i=0;i<data.Length;i++)
			{
				if(data[i].IndexOf('=')==-1)
				{
					continue;
				}

				String propery = data[i].Substring(0,data[i].IndexOf('=',0)).Trim();
				String values = data[i].Substring(data[i].IndexOf('=')+1).Trim();

				if(propery.ToLower().Equals("id"))
				{
					this.departcode = values;
				}
				else if(propery.ToLower().Equals("departname"))
				{
					this.departname = values;
				}
				else if(propery.ToLower().Equals("parentcode"))
				{
					this.parentcode = values;
				}
				else if(propery.ToLower().Equals("memo"))
				{
					this.memo = values;
				}
			}
		}

		public string getPcode()
		{
			return this.parentcode;
		}

		public void setPcode(string parentcode)
		{
			this.parentcode = parentcode;
		}

		public string getDepartcode()
		{
			return this.departcode;
		}

		public void setDepartcode(string departcode)
		{
			this.departcode = departcode;
		}

		public string getDepartname()
		{
			return this.departname;
		}

		public void setDepartname(string departname)
		{
			this.departname = departname;
		}

		public string getMemo()
		{
			return this.memo;
		}

		public void setMemo(string memo)
		{
			this.memo = memo;
		}
	}
}
