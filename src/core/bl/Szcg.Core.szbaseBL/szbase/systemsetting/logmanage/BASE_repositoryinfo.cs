using System;

namespace szcg.web.szbase.capability.logmanage
{
	/// <summary>
	/// RepositoryInfo 的摘要说明。
	/// </summary>
	public class BASE_repositoryinfo
	{
		private String name="";
		private String code = "";
		private String pcode = "";
		private String memo = "";

		public BASE_repositoryinfo()
		{
		}

		public BASE_repositoryinfo(String[] info)
		{
			for(int i=0;i<info.Length;i++)
			{
				if(info[i].IndexOf('=')==-1)
				{
					continue;
				}

				String propery = info[i].Substring(0,info[i].IndexOf('=',0)).Trim();
				String values = info[i].Substring(info[i].IndexOf('=')+1).Trim();

				if(propery.ToLower().Equals("name"))
				{
					this.name = values;
				}
				else if(propery.ToLower().Equals("code"))
				{
					this.code = values;
				}
				else if(propery.ToLower().Equals("pcode"))
				{
					this.pcode = values;
				}
				else if(propery.ToLower().Equals("memo"))
				{
					this.memo = values;
				}
			}
		}
	
		public String getName() 
		{
			return name;
		}
		
		public void setName(String name) 
		{
			this.name = name;
		}
		
		public String getCode() 
		{
			return this.code;
		}

		public void setCode(String code) 
		{
			this.code = code;
		}

		public void setPcode(String pcode) 
		{
			this.pcode = pcode;
		}

		public String getPcode() 
		{
			return pcode;
		}	
	
		public void setMemo(String memo) 
		{
			this.memo = memo;
		}

		public String getMemo() 
		{
			return memo;
		}		
	}
}
