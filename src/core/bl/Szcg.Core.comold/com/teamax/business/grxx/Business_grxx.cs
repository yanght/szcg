using System;
using System.Data;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using szcg.com.teamax.business.entity;//����
using szcg.com.teamax;//����

namespace szcg.com.teamax.business.grxx
{
	/// <summary>
	/// Business_grxx ��ժҪ˵����
	/// </summary>
	public class Business_grxx
	{
		protected string sql;
		protected SqlDataReader dr;
		public Business_grxx()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string getDepartName(int departcode)
		{
		 string departname="";
		 sql="select departname from depart where departcode='"+departcode+"'";
		 dr=DataAccess.ExecuteReader(sql,null);
			while(dr!=null&&dr.Read())
			{
			 departname=System.Convert.ToString(dr["departname"]);
			}
			dr.Close();

		  return departname;
		}
		public void updateUserInfo(int usercode,string username,string phone,string mobile,string birthday,string email,string address)
		{
		  sql="update loginuser set username='"+username+"',tel='"+phone+"',mobile='"+mobile+"',birthday='"+birthday+"',email='"+email+"',"
			  +"address='"+address+"' where usercode='"+usercode+"'";
			DataAccess.ExecuteNonQuery(sql,null);
		}
		public void UpdateUserPassword(int usercode, string password)
		{
			sql="update loginuser set password = '" + password + "' where usercode = '" + usercode.ToString() + "'";
			DataAccess.ExecuteNonQuery(sql,null);
		}
	}
}
