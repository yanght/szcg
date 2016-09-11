using System;
using szcg.com.teamax.util;
using System.Data.SqlClient;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.template
{
	/// <summary>
	/// BASE_Opinion ��ժҪ˵����
	/// </summary>
	public class BASE_addOpinion
	{

		/// <summary>
		/// ��area_rule�������
		/// </summary>
		/// <param name="code">����</param>
		/// <param name="number">��ֵ</param>
		/// <param name="level">����</param>
		/// <param name="color">ϵͳ��ʾ</param>
		public  int insertArea(string number,string level,string color)
		{
			string sql ="insert into area_rule(_numeric,_level,ruledisplay)"+
				"values('"+number+"','"+level+"','"+color+"')";
			int rs = DataAccess.ExecuteNonQuery(sql,null);
			
			//���û���־���в���������
			BASE_logmanageservice.UserLog(4,"10",sql);

			return rs;
			
		}
		/// <summary>
		/// ��station_rule�������
		/// </summary>
		/// <param name="code">����</param>
		/// <param name="number">��ֵ</param>
		/// <param name="level">����</param>
		/// <param name="color">ϵͳ��ʾ</param>
		public  int insertStation(string number,string level,string color)
		{
			string sql ="insert into station_rule(_numeric,rulelevel,ruledisplay)"+
				"values('"+number+"','"+level+"','"+color+"')";
			int rs = DataAccess.ExecuteNonQuery(sql,null);
			
			//���û���־���в���������
			BASE_logmanageservice.UserLog(4,"10",sql);

			return rs;
		
		}
	}
}
