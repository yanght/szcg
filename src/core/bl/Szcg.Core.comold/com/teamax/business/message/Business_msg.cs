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


namespace szcg.com.teamax.business.message
{
	/// <summary>
	/// Business_msg zhg 2006-5-16我的消息 的摘要说明。
	/// </summary>
	public class Business_msg
	{
		//protected StringBuilder sb = null;
		protected SqlDataReader rs = null;
		protected ArrayList list;
		protected com.teamax.business.entity.MsgInfo minfo;



		public Business_msg()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

		}
		/// <summary>
		///新建消息。同时插入回复信息
		/// </summary>
	
		public void insertMsg(string gousercode,string tousercode,string msgtitle,string msgcontent)
		{
			string bdtimes = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" +DateTime.Now.Minute.ToString() + ":" +DateTime.Now.Second.ToString();
		
			string sql="INSERT INTO b_business_msg(Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread)  values('1','"+gousercode+"','"+tousercode+"','"+bdtimes+"','"+msgtitle+"','"+msgcontent+"','0')";
			        
			DataAccess.ExecuteNonQuery(sql,null);
		}
//		public void insertMsg(string gouser,string touser,string msgtitle,string msgcontent)
//		{
//			
//			string sql1 = "SELECT usercode  FROM loginuser WHERE username='"+gouser+"' ";
//			string sql2 = "SELECT usercode  FROM loginuser WHERE username ='"+touser+"'";
//			int gousercode=0;
//			int tousercode=0;
//			rs = DataAccess.getDataReader(sql1);
//			while(rs.Read())
//			{
//				gousercode = rs.GetInt32(0);	
//	
//			}
//			rs.Close();
//
//			rs = DataAccess.getDataReader(sql2);
//			while(rs.Read())
//			{
//				tousercode=rs.GetInt32(0);
//			}
//			rs.Close();		
//	
//			string bdtimes = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" +DateTime.Now.Minute.ToString() + ":" +DateTime.Now.Second.ToString();
//        
//			string sql="INSERT INTO b_business_msg(Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread)  values('1','"+gousercode+"','"+tousercode+"','"+bdtimes+"','"+msgtitle+"','"+msgcontent+"','0')";
//                    
//			DataAccess.ExecuteNonQuery(sql,null);
//		}
		/// <summary>
		/// 查询消息。
		/// </summary>
		
		private ArrayList getMsgList(String sql)
		{
			
			list = new ArrayList();
			try
			{
				
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					rs.Close();
					return null;
				}
				while(rs.Read())
				{
					minfo = new MsgInfo();
					minfo.setId(System.Convert.ToInt32(rs["id"]));
					minfo.setSysmsgid(System.Convert.ToInt32(rs["Sysmsgid"]));
					minfo.setGo_user(System.Convert.ToInt32(rs["go_user"]));
					minfo.setTo_user(System.Convert.ToInt32(rs["to_user"]));
					minfo.setMsgtitle(System.Convert.ToString(rs["msgtitle"]));
					minfo.setMsgcontent(System.Convert.ToString(rs["msgcontent"]));
					minfo.setCu_date(System.Convert.ToString(rs["cu_date"]));   
					minfo.setIsread(System.Convert.ToString(rs["isread"]));
					minfo.setUsername(System.Convert.ToString(rs["username"]));
					minfo.setDepartname(System.Convert.ToString(rs["departname"]));
					list.Add(minfo);
				}
				rs.Close();
				
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		/// <summary>
		/// 消息个数
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		//		public int findMsgCount(string username)
		//		{
		//			int counts = 0;
		//			string sql = "SELECT COUNT(*) FROM b_business_msg AS a,loginuser AS b WHERE a.to_user=b.usercode and b.username='"+username+"'";
		//			rs = dataAccess.getDataReader(sql);
		//			while(rs.Read())
		//			{
		//				counts = rs.GetInt32(0);
		//			}
		//			return counts;
		//		}

		//查询消息
		public ArrayList getMsgInfo(string usercode, int page)
		{
			//ArrayList msgresult = new ArrayList();
			//		   string sql = "SELECT a.id,a.Sysmsgid,a.go_user,a.cu_date,a.msgtitle,a.msgcontent,b.username,c.departname  FROM b_business_msg AS a, loginuser AS b, depart AS c "
			//				+"WHERE a.go_user = b.usercode and b.departcode=c.departcode and a.go_user='"+usercode+"' ";
			string sql = "SELECT a.*,b.username,c.departname  FROM b_business_msg AS a, loginuser AS b, depart AS c "
				+"WHERE a.go_user = b.usercode and b.departcode=c.departcode and a.to_user='"+usercode+"'";
			int RowCount;
			int PageCount;
			com.teamax.util.DataAccess.cutPage(sql,15,out RowCount,out PageCount);
			SqlDataReader dr = com.teamax.util.DataAccess.getPageData(sql,15,page,"id","desc",RowCount,PageCount);
			System.Collections.ArrayList al = new System.Collections.ArrayList();
			al.Add(Convert.ToString(PageCount));
		//	msgresult = getMsgList(sql);
		//	return msgresult;
			if(dr==null)
			{
				dr.Close();
				return null;
			}
			while(dr.Read())
			{

				minfo = new MsgInfo();
				minfo.setCodes(Convert.ToString(RowCount));
				minfo.setId(System.Convert.ToInt32(dr["id"]));
				minfo.setSysmsgid(System.Convert.ToInt32(dr["Sysmsgid"]));
				minfo.setGo_user(System.Convert.ToInt32(dr["go_user"]));
				minfo.setTo_user(System.Convert.ToInt32(dr["to_user"]));
				minfo.setMsgtitle(System.Convert.ToString(dr["msgtitle"]));
				minfo.setMsgcontent(System.Convert.ToString(dr["msgcontent"]));
				minfo.setCu_date(System.Convert.ToString(dr["cu_date"]));   
				minfo.setIsread(System.Convert.ToString(dr["isread"]));
				minfo.setUsername(System.Convert.ToString(dr["username"]));
				minfo.setDepartname(System.Convert.ToString(dr["departname"]));
				//list.Add(minfo);

				al.Add(minfo);
			}
			dr.Close();

			return al;


		}

		//获取消息的详细信息
		public ArrayList getMsgDetail(int id)
		{
			ArrayList msgresult = new ArrayList();
			//string sql1 = "select a.*  from b_business_msg As a "
			//+"WHERE  a.id='"+id+"'";
			string sql ="SELECT a.*,b.username,c.departname  FROM b_business_msg AS a, loginuser AS b, depart AS c WHERE a.go_user = b.usercode and b.departcode=c.departcode and a.id='"+id+"'";
			//string sql2 ="UPDATE b_business_msg SET isread = '1' WHERE id ='"+id+"'";
			//"INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode where a.projcode='"+id+"'";
		//	string sql=sql1+ sql2;
			msgresult = getMsgList(sql);
			return msgresult;
		}

		/// <summary>
		/// 删除消息。
		/// </summary>
		public void deleteMsg(int id)
		{
		
			string sql = "delete b_business_msg where  id = '"+id+"'";
			DataAccess.ExecuteNonQuery(sql,null);
		}
        
		/// <summary>
		/// 获取该用户是否有新消息
		/// </summary>
		/// <param name="intUserCode">用户编码</param>
		/// <returns>1：有新消息；0：没有新消息</returns>
		public string HaveNewMessage(int intUserCode)
		{
			string strSQL = "select count(*) from b_business_msg where to_user = '" + intUserCode.ToString() + "' and isread = 0";
			int intNum = (int)DataAccess.ExecuteScalar(strSQL, null);
			if( intNum > 0 )
				return "1";
			else
				return "0";
		}
		///zhanghuagen05-26		
		private ArrayList getPDAmsgList(String sql)
		{
			
			list = new ArrayList();
			try
			{
				
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					rs.Close();
					return null;
				}
				while(rs.Read())
				{
					minfo = new MsgInfo();
					minfo.setId(System.Convert.ToInt32(rs["id"]));
					minfo.setMsgtitle(System.Convert.ToString(rs["msgtitle"]));
					minfo.setMsgcontent(System.Convert.ToString(rs["msgcontent"]));
					minfo.setCu_date(System.Convert.ToString(rs["cu_date"])); 
					minfo.setProjcode(System.Convert.ToString(rs["projcode"]));
					minfo.setProjname(System.Convert.ToString(rs["projname"]));
					minfo.setCollcode(System.Convert.ToString(rs["collcode"]));
					minfo.setCollname(System.Convert.ToString(rs["collname"]));
					minfo.setIsread(System.Convert.ToString(rs["ioflag"]));
					minfo.setUsername(Convert.ToString(rs["address"])+","+Convert.ToString(rs["probdesc"]));
					list.Add(minfo);
				}
				rs.Close();
				
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		/// <summary>
		/// 查询PDA消息zhanghuagen05-26
		/// </summary>
		/// <returns></returns>
		public ArrayList getPDAmsgInfo(int page,string areacode)
		{
			//ArrayList pdaresult = new ArrayList();
			string sqlpda = sqlpda = "select a.*,b.collname,c.projname   FROM  b_pdamsg As a,collecter AS b,b_project_detail AS c  where (a.ioflag = '2' or a.ioflag = '3') and a.collcode=b.collcode and a.projcode=c.projcode ";
			if(areacode!="4403")
			{
				sqlpda += " and a.projcode in (select projcode from b_project where substring(gridcode,0,8) = '"+areacode+"')";
			}
			
			int RowCount;
			int PageCount;
			com.teamax.util.DataAccess.cutPage(sqlpda,15,out RowCount,out PageCount);
			SqlDataReader dr = com.teamax.util.DataAccess.getPageData(sqlpda,15,page,"id","desc",RowCount,PageCount);
			System.Collections.ArrayList al = new System.Collections.ArrayList();
			al.Add(Convert.ToString(PageCount));
			if(dr==null)
			{
				dr.Close();
				return null;
			}
			while(dr.Read())
			{
				minfo = new MsgInfo();
				minfo.setCodes(Convert.ToString(RowCount));
				minfo.setId(System.Convert.ToInt32(dr["id"]));
				minfo.setMsgtitle(System.Convert.ToString(dr["msgtitle"]));
				minfo.setMsgcontent(System.Convert.ToString(dr["msgcontent"]));
				minfo.setCu_date(System.Convert.ToString(dr["cu_date"])); 
				minfo.setProjcode(System.Convert.ToString(dr["projname"]));
				minfo.setCollcode(System.Convert.ToString(dr["collcode"]));
				minfo.setCollname(System.Convert.ToString(dr["collname"]));
				minfo.setIsread(System.Convert.ToString(dr["ioflag"]));
				//minfo.setProjname(Convert.ToString(dr["address"])+","+Convert.ToString(dr["probdesc"]));
				al.Add(minfo);
			}
			dr.Close();

			return al;

		}
		/// <summary>
		/// 查询PDA详细消息zhanghuagen05-26
		/// </summary>
		/// <returns></returns>
		public ArrayList getPDAdetail(int id)
		{
			ArrayList pdaresult = new ArrayList();
			string sql = "select a.*,b.collname,c.projname,c.probdesc,c.address   FROM  b_pdamsg As a,collecter AS b,b_project_detail AS c  where a.collcode=b.collcode and a.projcode=c.projcode  and  a.id = '"+id+"'";
		//	string sql2 ="  UPDATE b_pdamsg SET ioflag = '0' WHERE id ='"+id+"'";
           // string sql=sql1+ sql2;
			pdaresult = getPDAmsgList(sql);
			return pdaresult;
		}
		/*其他消息*/
		///zhanghuagen05-31		
		private ArrayList getOthermsgList(String sql)
		{
			
			list = new ArrayList();
			try
			{
				
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					rs.Close();
					return null;
				}
				while(rs.Read())
				{
					minfo = new MsgInfo();
					minfo.setId(System.Convert.ToInt32(rs["id"]));
					minfo.setMsgtitle(System.Convert.ToString(rs["title"]));
					minfo.setMsgcontent(System.Convert.ToString(rs["msgcontent"]));
					minfo.setCu_date(System.Convert.ToString(rs["cu_date"])); 
					minfo.setCollcode(System.Convert.ToString(rs["collcode"]));
					minfo.setCollname(System.Convert.ToString(rs["collname"]));
					minfo.setIsread(System.Convert.ToString(rs["state"]));
	
					list.Add(minfo);
				}
				rs.Close();		
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		/// <summary>
		/// 查询其他消息 监督中心发给监督员zhanghuagen05-31
		/// </summary>
		/// <returns></returns>
		public ArrayList getOthermsgInfo(int page,string areacode,string usercode)
		{
			//ArrayList otherresult = new ArrayList();
			string sqlOther = "select a.*,b.collname  FROM  b_pdamsg_other As a,collecter AS b  where  a.collcode=b.collcode and (a.usercode = '"+usercode+"' or a.usercode is null)";
			if(areacode!="4403")
			{
				sqlOther += " and a.collcode in (select collcode from collecter where substring(gridcode,0,8) like '"+areacode+"%')";
			}
			int RowCount;
			int PageCount;
			com.teamax.util.DataAccess.cutPage(sqlOther,15,out RowCount,out PageCount);
			SqlDataReader dr = com.teamax.util.DataAccess.getPageData(sqlOther,15,page,"id","desc",RowCount,PageCount);
			System.Collections.ArrayList al = new System.Collections.ArrayList();
			al.Add(Convert.ToString(PageCount));
			if(dr==null)
			{
				dr.Close();
				return null;
			}
			while(dr.Read())
			{

				minfo = new MsgInfo();
				minfo.setCodes(Convert.ToString(RowCount));
				minfo.setId(System.Convert.ToInt32(dr["id"]));
				minfo.setMsgtitle(System.Convert.ToString(dr["title"]));
				minfo.setMsgcontent(System.Convert.ToString(dr["msgcontent"]));
				minfo.setCu_date(System.Convert.ToString(dr["cu_date"])); 
				minfo.setCollcode(System.Convert.ToString(dr["collcode"]));
				minfo.setCollname(System.Convert.ToString(dr["collname"]));
				minfo.setIsread(System.Convert.ToString(dr["state"]));

				al.Add(minfo);
			}
			dr.Close();

			return al;
		
		}
		/// <summary>
		/// 查询其他详细消息zhanghuagen06-1
		/// </summary>
		/// <returns></returns>
		public ArrayList getOtherdetail(int id)
		{
			ArrayList otherresult = new ArrayList();
			string sql1 = "select a.*,b.collname  FROM  b_pdamsg_other As a,collecter AS b  where a.collcode=b.collcode  and  id = '"+id+"'";
			
			string sql2 =" UPDATE b_pdamsg_other SET state = '2' WHERE id ='"+id+"'";
			string sql=sql1+sql2;
            
			otherresult = getOthermsgList(sql);
			return otherresult;
		}

		//智能报警
		public ArrayList getHelpdetail(int page,string areacode)
		{
			ArrayList list  = new ArrayList();
			string sql = "select a.*,b.collname from call_help as a,collecter as b where a.collcode=b.collcode and sendtype = '1' ";
			if(areacode!="4403")
			{
				sql += " and substring(b.gridcode,0,8) = '"+areacode+"'";
			}
			list  = getHelpList(sql,page);
			return list;
		}

		private ArrayList getHelpList(String sql,int page)
		{
			list = new ArrayList();
			try
			{
				int RowCount;
				int PageCount;
				com.teamax.util.DataAccess.cutPage(sql,15,out RowCount ,out PageCount);

				SqlDataReader rs = DataAccess.getPageData(sql,15,page,"id","desc",RowCount,PageCount);
				list.Add(Convert.ToString(PageCount));
				if(rs==null)
				{	
					return null;
				}
				while(rs.Read())
				{
					minfo = new MsgInfo();
					minfo.setId(System.Convert.ToInt32(rs["id"]));
					minfo.setCodes(Convert.ToString(RowCount));
					string usercode = Convert.ToString(rs["usercode"]);
					if(usercode!=null&&usercode!="")
					{
						string sql1 = "select username from loginuser where usercode = '"+usercode+"'";
						SqlDataReader dr = DataAccess.ExecuteReader(sql1,null);
						if(dr.Read())
						{
							minfo.setUsername(Convert.ToString(dr["username"]));
						}
						dr.Close();
					}
					//minfo.setMsgtitle(System.Convert.ToString(rs["title"]));
					minfo.setMsgcontent(System.Convert.ToString(rs["content"]));
					minfo.setCu_date(System.Convert.ToString(rs["cudate"])); 
					minfo.setCollcode(System.Convert.ToString(rs["collcode"]));
					minfo.setCollname(System.Convert.ToString(rs["collname"]));
					minfo.setIsread(System.Convert.ToString(rs["state"]));
	
					list.Add(minfo);
				}
				rs.Close();		
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}
	}
}
