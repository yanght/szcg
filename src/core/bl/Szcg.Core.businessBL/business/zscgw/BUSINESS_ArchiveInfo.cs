using System;

namespace szcg.com.teamax.business.archives
{
	/// <summary>
	/// BUSINESS_ArchiveInfo 的摘要说明。
	/// </summary>
	public class BUSINESS_ArchiveInfo
	{
		private decimal id;
		private string title;
		private string author;
		private string content;
		private string cu_date;

		public BUSINESS_ArchiveInfo()
		{
			
		}

		public BUSINESS_ArchiveInfo(String[] data)
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
					this.id = Convert.ToDecimal(values);
				}
				else if(propery.ToLower().Equals("title"))
				{
					this.title = values;
				}
				else if(propery.ToLower().Equals("author"))
				{
					this.author = values;
				}
				else if(propery.ToLower().Equals("content"))
				{
					this.content = values;
				}
				else if(propery.ToLower().Equals("cu_date"))
				{
					this.cu_date = values;
				}
			}
		}

		public void setId(decimal id)
		{
			this.id = id;
		}

		public decimal getId()
		{
			return this.id;
		}

		public void setTitle(string title)
		{
			this.title = title;
		}

		public string getTitle()
		{
			return this.title;
		}

		public void setAuthor(string author)
		{
			this.author = author;
		}

		public string getAuthor()
		{
			return this.author;
		}

		public void setContent(string content)
		{
			this.content = content;
		}

		public string getContent()
		{
			return this.content;
		}

		public void setDatenow(string cu_date)
		{
			this.cu_date = cu_date;
		}

		public string getDatenow()
		{
			return this.cu_date;
		}
	}
}
