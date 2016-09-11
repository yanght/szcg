using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using szcg.com.teamax.util;
using szcg.com.teamax.business.entity;

namespace szcg.com.teamax.business.projectmanage
{
	/// <summary>
	/// Business_Projregister zhanghuagen 2006-5-23 的摘要说明。
	/// </summary>
	public class Business_Projregister
	{
	
		protected SqlDataReader rs = null;
		protected ArrayList list;
	    protected com.teamax.business.entity.CollecterInfo cinfo;
		protected com.teamax.business.entity.ProjInfo pinfo;
		public Business_Projregister()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// getMaxProjectCode 
		/// </summary>
		/// <returns></returns>
		private int getMaxProjectCode()
		{
			string sql = "select max(projcode) from b_project";
			SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
			int result = 0;
			while(rs.Read())
			{
				result = Convert.ToInt32(rs[0]);
			}
			rs.Close();
			return result;
		}
		/// <summary>
		/// 问题登记(公众举报)
		/// </summary>
		/// <param name="info"></param>

		public void registerNewProject(ProjInfo projInfo)
		{
//			string sql="createProjName";
//			SqlParameter[] output=new SqlParameter[1];
//			output[0]=new SqlParameter("@projname",SqlDbType.VarChar,128);
//			output[0].Direction=ParameterDirection.Output;
//			string projName=DataAccess.ExecuteStoreProcedure1(sql,null,output);
//
//			String[] sqls=new string[3];
//			sqls[0]="insert into b_project(stepid,stateid,role,typecode,startdate,gridcode,fid,release,isdel) "+
//				" values('1','2',1,'0','"+System.DateTime.Now.ToString()+"','"+projInfo.getGridcode()+"','"+projInfo.getFid()+"',0,0)" ;
//			int projcode = getMaxProjectCode()+1;
//			sqls[1]="insert into b_project_detail(projcode,projname,probsource,probclass,bigclass,smallclass,area,street,square,"+
//				"address,probdesc)"+
//				" values('"+projcode+"','"+projName+"','"+projInfo.getProbsource()+"','"+projInfo.getProbclass()+"'," +
//				" '"+projInfo.getBigclass()+"','"+projInfo.getSmallclass()+"','"+projInfo.getArea()+"','"+projInfo.getStreet()+"','"+projInfo.getSquare()+"',"+
//				"'"+projInfo.getAddress()+"','" +projInfo.getProbdesc()+"')" ;	
//			if(projInfo.getProbsource().Equals("0"))
//			{
//				sqls[2]="insert into public_proj(projcode,name,tel,type,retobject) values('"+projcode+"','"+projInfo.getName()+"','"+projInfo.getTel()+"','0','0')";
//			}
//			else
//			{
//				sqls[2]=null;
//			}
//			
//			DataAccess.ExecuteNonQueryForSqlGroup(sqls);
		}
		/// <summary>
		/// 获取监督员
		/// </summary>
		/// <param name="info"></param>
		/// 

		/// <summary>
		/// set jianduyuan 
		/// </summary>
		
		private ArrayList getManList(String sql)
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
					cinfo = new CollecterInfo();
				    cinfo.setCollcode(System.Convert.ToString(rs["collcode"]));
					cinfo.setCollName(System.Convert.ToString(rs["collname"]));
					cinfo.setNumbercode(System.Convert.ToString(rs["numbercode"]));
					cinfo.setSquare(System.Convert.ToString(rs["commname"]));
					//cinfo.setProjcode(System.Convert.ToString(rs["projcode"]));						
					list.Add(cinfo);
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
		/// 获取监督员详细信息 zhanghuagen 0523问题登记时根据网格获取监督员
		/// </summary>
		/// <param name="gridcode"></param>
		/// <returns></returns>
		public ArrayList getManInfo(string gridcode)
		{
			ArrayList manresult = new ArrayList();
			string sql="select a.collcode,a.collname,a.numbercode,b.commname  from  collecter as a,community as b  where a.commcode=b.id and a.gridcode ='"+gridcode+"'";
				//select a.projcode,b.*  from b_project as a,collecter as b where a.gridcode=b.gridcode and b.gridcode ='"+gridcode+"'";

			manresult = getManList(sql);
			return manresult;
		}


		/// <summary>
		/// (公众举报)发送给监督员核查案卷 zhanghuagen 05-23
		/// </summary>
		/// <param name="cinfo"></param>
		/// 
		public void sendtoMan(CollecterInfo cInfo)
		{
			//String projName="深圳城管"+DateTime.Now.Month+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second;
			
			 string sqlm="insert into b_pdamsg(projcode,collcode,cu_date,msgcontent,msgtitle,ioflag,state) "+
				" values('"+cInfo.getProjcode()+"','"+cInfo.getCollcode()+"',GetDate(),'"+cInfo.getMemo()+"','"+cInfo.getMemo()+"','0','0')" ;
			
			DataAccess.ExecuteNonQuery(sqlm,null);
		}
		/// <summary>
		/// 获取街道zhanghuagen 05-31
		/// </summary>
		/// <param name="cinfo"></param>
		public ArrayList getStreetInfo()
		{
			ArrayList streetresult = new ArrayList();
			string sql="select streetname from street";

			streetresult = getStreetList(sql);
			return streetresult;
		}
		private ArrayList getStreetList(String sql)
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
					pinfo = new ProjInfo();
				//	pinfo.setFid(System.Convert.ToInt32(rs["id"]));
					pinfo.setStreet(System.Convert.ToString(rs["streetname"]));		
					list.Add(pinfo);
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
		/// 获取社区zhanghuagen 05-31
		/// </summary>
		/// <param name="cinfo"></param>

		public ArrayList getSquareInfo( string streetnames)
		{
			ArrayList squareresult = new ArrayList();
			string sql="select a.id,a.commname from community as a,street as b where a.fid=b.id and b.streetname='"+streetnames+"'";

			squareresult = getSquaretList(sql);
			return squareresult;
		}

		private ArrayList getSquaretList(String sql)
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
					pinfo = new ProjInfo();
					pinfo.setSquare(System.Convert.ToString(rs["commname"]));		
					list.Add(pinfo);
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
