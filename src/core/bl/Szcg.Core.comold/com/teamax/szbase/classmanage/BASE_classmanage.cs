using System;
using szcg.com.teamax.util;
using System.Collections;
using System.Data.SqlClient;

namespace szcg.com.teamax.szbase.classmanage
{
	/// <summary>
	/// BASE_classmanage 的摘要说明。
	/// </summary>
	public class BASE_classmanage
	{
		public BASE_classmanage()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static ArrayList getBigClassEvents()
		{
			string sql="select * from bigclass_event";
			ArrayList array=new ArrayList(); 
			SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
			while(rs.Read())
			{
				string[] events=new string[2];
				events[0]=((int)rs["id"]).ToString();
				events[1]=(string)rs["name"];
				array.Add(events);
			}
			rs.Close();
			return array;
		}

		public static ArrayList getBigClassParts()
		{
			string sql="select * from bigclass_part";
			ArrayList array=new ArrayList(); 
			SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
			while(rs.Read())
			{
				string[] parts=new string[2];
				parts[0]=((int)rs["id"]).ToString();
				parts[1]=(string)rs["name"];
				array.Add(parts);
			}
			rs.Close();
			return array;

		}

		public static ArrayList getSmallClassEvents()
		{
			string sql="select id,fid,name from smallclass_event";
			ArrayList array=new ArrayList(); 
			SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
			while(rs.Read())
			{
				string[] events=new string[3];
				events[0]=((int)rs["id"]).ToString();
				events[1]=((int)rs["id"]).ToString();
				events[2]=(string)rs["name"];
				array.Add(events);
			}
			rs.Close();
			return array;

		}

		public static ArrayList getSmallClassParts()
		{
			string sql="select id,fid,name from smallclass_part";
			ArrayList array=new ArrayList(); 
			SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
			while(rs.Read())
			{
				string[] parts=new string[3];
				parts[0]=((int)rs["id"]).ToString();
				parts[1]=((int)rs["id"]).ToString();
				parts[2]=(string)rs["name"];
				array.Add(parts);
			}
			rs.Close();
			return array;

		}

		//把"null"转换为""
		public static string changeNull(string str)
		{
			if(str.ToLower().Equals("null"))
				str="";
			return str;
		}



	}
}
