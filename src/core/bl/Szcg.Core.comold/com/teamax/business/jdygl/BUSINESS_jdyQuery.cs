using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using szcg.com.teamax.business.entity;
using szcg.com.teamax.util;
using System.Text;


namespace szcg.com.teamax.business.jdygl
{
	/// <summary>
	/// BUSINESS_jdyQuery 的摘要说明。
	/// </summary>
	public class BUSINESS_jdyQuery
	{
		protected SqlDataReader rs = null;
		protected ArrayList list;
		protected szcg.com.teamax.business.entity.CollecterInfo collInfo;
		private static string areaNames = "";
		private static string streetNames = "";
		private static string gridCodes = "";
		private static string collNames = "";
		private static string collCodes = "";
		private static string collTels = "";
		private static string guarDs = "";
	

		public BUSINESS_jdyQuery()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//		
		}


     /// <summary>
     /// 查询所有的监督员
     /// </summary>
     /// <param name="PageSize"></param>
     /// <param name="CurrentPage"></param>
     /// <returns></returns>
		public ArrayList SelectAll(string areacode,int PageSize,int CurrentPage)
		{
			list = new ArrayList();
			string sql = "select a.collcode,a.loginname,a.gridcode,a.mobile,a.collname,a.isguard,b.commname,c.streetname,d.areaname,a.version  from collecter as a,community as b,street as c, area as d"+
                   " where  a.commcode=b.id and b.fid=c.id and c.fid=d.id ";
			if(areacode != "4403")
			{
				 sql += "and substring(a.gridcode,0,8) = '"+areacode+"'";
			}
			int RowCount;
			int PageCount;
			DataAccess.cutPage(sql,PageSize,out RowCount,out PageCount);
			SqlDataReader rs  = DataAccess.getPageData(sql,PageSize,CurrentPage,"collcode","asc",RowCount,PageCount);
			if(rs==null)
			{
				rs.Close();
				return null;
			}
			
		//	com.teamax.business.entity.CollecterInfo.Convert.ToString(RowCount);
			
			
			while(rs != null && rs.Read())
			 {
				collInfo = new CollecterInfo();
				collInfo.setCollcode(System.Convert.ToString(rs["collcode"]));
				collInfo.setCollName(System.Convert.ToString(rs["collname"]));
				collInfo.setGridCode(System.Convert.ToString(rs["gridcode"]));
				collInfo.setAreaname(System.Convert.ToString(rs["areaname"]));
				collInfo.setIsguard(System.Convert.ToString(rs["isguard"]));
				collInfo.setSquare(System.Convert.ToString(rs["commname"]));
				collInfo.setStreet(System.Convert.ToString(rs["streetname"]));
				collInfo.setMobile(System.Convert.ToString(rs["mobile"]));
				collInfo.setVersion(System.Convert.ToString(rs["version"]));
				collInfo.cu_rows=Convert.ToInt32(RowCount);
				collInfo.cu_pags = Convert.ToInt32(PageCount);
				

				list.Add(collInfo);
			 }
			
			if(rs != null)
				rs.Close();
               return list;

	
		
		}

		public string AllIsguard(string areacode)
		{
			string result = "";
			string sql = "";
			if(areacode=="4403")
			{
				sql = "select count(*) from collecter where isguard = '1'";
			}
			else
			{
				sql= "select count(*) from collecter where substring(gridcode,0,8) = '"+areacode+"' and isguard = '1'";
			}
			SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
			if(rs.Read())
			{
				result = Convert.ToString(rs[0]);
			}
			rs.Close();
			return result;
		}

		/// <summary>
		/// zhanghuagen监督员条件查询2005-5-24
		/// </summary>
		/// <param name="areaname"></param>
		/// <param name="street"></param>
		/// <param name="gridcode"></param>
		/// <param name="collname"></param>
		/// <param name="collcode"></param>
		/// <param name="colltel"></param>
		/// <param name="guard"></param>
		/// <param name="PageSize"></param>
		/// <param name="CurrentPage"></param>
		/// <returns></returns>
		public ArrayList SelectColl(string areacode,string areacodes, string streetname,string gridcode, string collname, string collcode, string colltel, string guard,int PageSize,int CurrentPage)
		{
			list = new ArrayList();

			string sql="";
			if (areacode == "" && streetname == "" && gridcode == "" && collname == "" && collcode == "" && colltel == "" && guard == "")
			{
				sql = "select a.collcode,a.loginname,a.gridcode,a.mobile,a.collname,a.isguard,b.commname,c.streetname,d.areaname,a.version  from collecter as a,community as b,street as c, area as d"+
					" where  a.commcode=b.id and b.fid=c.id and c.fid=d.id";
			}
			else
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("select a.collcode,a.loginname,a.gridcode,a.mobile,a.collname,a.isguard,b.commname,c.streetname,d.areaname,a.version  from collecter as a,community as b,street as c, area as d"+
					" where  a.commcode=b.id and b.fid=c.id and c.fid=d.id and ");

				string areaName = areacode != null ? areacode :"";
				string streetName = streetname != null ? streetname : "";
				string gridCode = gridcode != null ? gridcode : "";
				string collName = collname != null ? collname : "";
				string collCode = collcode != null ? collcode : "";
				string collTel = colltel != null ? colltel : "";
				string guarD = guard != null ? guard : "";
				
				if(areaName != "" )
				{
					if(areacodes=="4403")
					{
						areaNames = "d.areaname = '"+areacode+"' AND ";
					}
					else
					{
						areaNames = " substring(a.gridcode,0,8) = '"+areacode+"' AND ";
					}
				}
				else
				{
					areaNames = "";
				}
				if(streetName != "")
				{
					streetNames = "c.streetname LIKE '%"+streetName+"%' AND " ; 
				}
				else
				{
					streetNames = "";
				}
				if(gridCode != "")
				{
					gridCodes = "a.gridcode LIKE '%"+gridCode+"%' AND ";
				}
				else
				{
					gridCodes = "";
				}
				if(collName != "")
				{
					collNames = "a.collname LIKE '%"+collName+"%' AND ";
				}
				else
				{
					collNames = "";
				}
				if(collCode != "")
				{
					collCodes = "a.loginname LIKE '%"+collCode+"%' AND ";
				}
				else
				{
					collCodes = "";
				}
				if(collTel != "")
				{
					collTels = "a.mobile LIKE '%"+collTel+"%' AND ";
				}
				else
				{
					collTels = "";
				}
				if(guarD != "")
				{
					guarDs = "a.isguard LIKE '%"+guarD+"%' AND ";
				}
				else
				{
					guarDs = "";
				}
		
				sb.Append(areaNames);
				sb.Append(streetNames);
				sb.Append(gridCodes);
				sb.Append(collNames);
				sb.Append(collCodes);
				sb.Append(collTels);
				sb.Append(guarDs);
	
				sql = sb.ToString().Substring(0,sb.Length-4);
			}
			int RowCount;
			int PageCount;
			DataAccess.cutPage(sql,PageSize,out RowCount,out PageCount);
			SqlDataReader rs  = DataAccess.getPageData(sql,PageSize,CurrentPage,"collcode","asc",RowCount,PageCount);
			if(rs==null)
			{
				rs.Close();
				return null;
			}
			//list.Add(Convert.ToString(RowCount));
			//list.Add(Convert.ToString(PageCount));
			while(rs.Read())
			{
				collInfo = new CollecterInfo();
				collInfo.setCollcode(System.Convert.ToString(rs["collcode"]));
				collInfo.setCollName(System.Convert.ToString(rs["collname"]));
				collInfo.setGridCode(System.Convert.ToString(rs["gridcode"]));
				collInfo.setAreaname(System.Convert.ToString(rs["areaname"]));
				collInfo.setIsguard(System.Convert.ToString(rs["isguard"]));
				collInfo.setSquare(System.Convert.ToString(rs["commname"]));
				collInfo.setStreet(System.Convert.ToString(rs["streetname"]));
				collInfo.setMobile(System.Convert.ToString(rs["mobile"]));	
				collInfo.setVersion(System.Convert.ToString(rs["version"]));
				collInfo.cu_rows=Convert.ToInt32(RowCount);
				collInfo.cu_pags = Convert.ToInt32(PageCount);
				list.Add(collInfo);
			}
			rs.Close();
			return list;


		}
       ///send to jdy
       ///06-1
		public void sendMsg(string collcode,string msgcontent,string msgtitle,string usercode)
		{
	
			string bdtimes = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" +DateTime.Now.Minute.ToString() + ":" +DateTime.Now.Second.ToString();
        
			string sql="INSERT INTO b_pdamsg_other(collcode,cu_date,msgcontent,state,title,memo,usercode)  values('"+collcode+"','"+bdtimes+"','"+msgcontent+"','0','"+msgtitle+"','1','"+usercode+"')";
                    
			DataAccess.ExecuteNonQuery(sql,null);
		}

		public string getAreaname(string areacode)
		{
			string sql = "";
			if(areacode!="4403")
			{
				sql = "select areaname from area where areacode = '"+areacode+"'";
			}
			else
			{
				sql = "select areaname from area";
			}
			string areaname = "";
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
				areaname += Convert.ToString(dr["areaname"])+",";
			}
			dr.Close();
			return areaname.Substring(0,areaname.Length-1);
		}

		public int getGpsState(string id)
		{
			string sql = "select datediff(hh,cu_date , getdate()) as times from collecter_xy where collcode = '"+id+"' order by id desc";
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			int times = 1;
			if(dr.Read())
			{
				times = Convert.ToInt32(dr["times"]);
			}
			dr.Close();
			return times;
		}
	}
}
