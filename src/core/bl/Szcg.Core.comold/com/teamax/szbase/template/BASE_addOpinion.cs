using System;
using szcg.com.teamax.util;
using System.Data.SqlClient;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.template
{
	/// <summary>
	/// BASE_Opinion 的摘要说明。
	/// </summary>
	public class BASE_addOpinion
	{

		/// <summary>
		/// 向area_rule表插数据
		/// </summary>
		/// <param name="code">编码</param>
		/// <param name="number">数值</param>
		/// <param name="level">级别</param>
		/// <param name="color">系统显示</param>
		public  int insertArea(string number,string level,string color)
		{
			string sql ="insert into area_rule(_numeric,_level,ruledisplay)"+
				"values('"+number+"','"+level+"','"+color+"')";
			int rs = DataAccess.ExecuteNonQuery(sql,null);
			
			//向用户日志表中插入所数据
			BASE_logmanageservice.UserLog(4,"10",sql);

			return rs;
			
		}
		/// <summary>
		/// 向station_rule表插数据
		/// </summary>
		/// <param name="code">编码</param>
		/// <param name="number">数值</param>
		/// <param name="level">级别</param>
		/// <param name="color">系统显示</param>
		public  int insertStation(string number,string level,string color)
		{
			string sql ="insert into station_rule(_numeric,rulelevel,ruledisplay)"+
				"values('"+number+"','"+level+"','"+color+"')";
			int rs = DataAccess.ExecuteNonQuery(sql,null);
			
			//向用户日志表中插入所数据
			BASE_logmanageservice.UserLog(4,"10",sql);

			return rs;
		
		}
	}
}
