using System;
using AjaxPro;
using szcg.com.teamax.util;
using System.Data.SqlClient;


namespace szcg.com.teamax.business.idea
{
	/// <summary>
	/// BUSINESS_idea 的摘要说明。
	/// </summary>
	public class BUSINESS_idea
	{
		public BUSINESS_idea()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			


		}
		[AjaxMethod]
		public string deleteResponse(string id)
		{
			string sql="delete b_opinion_collect where id="+id;
			string result="1";
			if(DataAccess.ExecuteNonQuery(sql,null)!=1)
			{
				result="0";
			}

			return result;
		} 
	}
}
