using System;
using  szcg.com.teamax.util;
using szcg.com.teamax.business.entity;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using szcg.com.teamax.business.message;


namespace szcg.com.teamax.business.message
{
	/// <summary>
	/// DepartTree  zhanghuagen 2006-5-18的摘要说明。
	/// </summary>
	public class DepartTree
	{
		protected string sql = "";
		protected int rows=0;
		protected SqlDataReader rs;


		public DepartTree()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public String[] getDepartInfo() 
		{		  
			String[] infoLib = null;
			sql="select count(*) from depart";
			object obj = (int)DataAccess.ExecuteScalar(sql,null);
			if(obj!=System.DBNull.Value)
			{
				rows = (int)obj;
			}
			if(rows!=0)
			{
				infoLib = new String[rows];
				sql="select * from depart where parentcode >0 order by departcode";
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs!=null)
				{
					int i = 0;
					while(rs.Read())
					{
						StringBuilder sb = new StringBuilder();							
						
						sb.Append("code="+Convert.ToString(rs["departcode"])+",");
						sb.Append("name="+Convert.ToString(rs["departname"])+",");
						sb.Append("pcode="+Convert.ToString(rs["parentcode"])+",");
						sb.Append("memo="+Convert.ToString(rs["memo"]+","));
						infoLib[i] = sb.ToString();
						i++;
					}
					rs.Close();
				}
			}
			return infoLib;	
	
		}

	}
}
