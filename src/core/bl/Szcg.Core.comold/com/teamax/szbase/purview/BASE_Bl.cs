using System;

namespace szcg.com.teamax.szbase.purview
{
	/// <summary>
	/// BASE_Bl 的摘要说明。
	/// </summary>
	public class BASE_Bl
	{
		public static BASE_Bl instanse = null;

		public BASE_Bl()
		{
			
		}
		//静态的方法，用来初始构造函数
		public static BASE_Bl getInstanse()
		{
			if(instanse==null)
			{
				instanse=new BASE_Bl();
			}
			return instanse;
		}

		//根据传入的string解析成不含有','的数组
		public string[] getModelId(string ids)
		{
			if(ids!=null && ids.Length>0 && ids.IndexOf(',')!=-1)
			{
				ids=ids.Substring(0,ids.Length-1);
			}
			
			string[] str = null;
			str = ids.Split(',');
			return str;
		}
	}
}
