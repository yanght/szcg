using System;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Data;
using System.Text;
using System.Collections;
using szcg.com.teamax.szbase.entity;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.doormanager
{
	/// <summary>
	/// BASE_doormanager 的摘要说明。
	/// </summary>
	public class BASE_doormanager
	{
		//得到数据库中的结果集
		protected static SqlDataReader dr;
		//存放数据
		protected static StringBuilder sb;
		//存放数组数据
		protected static ArrayList list;

		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.doormanager.BASE_doormanager.";

		public BASE_doormanager()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static String[] getBigEventTreeData(int userId)
		{
			string sql = "select id,name from s_bigclass_event";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
			
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToInt32(dr["id"])+",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getBigEventTreeData");
				throw;
			}

		}

		public static String[] getSmallEventTreeData(int fid,int userId)
		{
			string sql = "select DISTINCT B.id as id,B.name as name from bigclass_event A,smallclass_event B where A.id =B.fid AND A.id='"+fid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
			
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToInt32(dr["id"])+",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getSmallEventTreeData");
				throw;
			}

		}

		public static String[] getBigPartTreeData(int userId)
		{
			string sql = "select id,name from bigclass_part";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
			
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToInt32(dr["id"])+",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getBigPartTreeData");
				throw;
			}
		}

		public static String[] getSmallPartTreeData(int fid,int userId)
		{
			string sql = "select DISTINCT B.id as id,B.name as name from bigclass_part A,smallclass_part B where A.id =B.fid AND A.id='"+fid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
			
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToInt32(dr["id"])+",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getSmallPartTreeData");
				throw;
			}

		}

		public static bool uploadfile(byte[] b,string fileName,string filePath)
		{
			return FileUpload.uploadFile(b,fileName,filePath);
		}
		
		public static Hashtable setPartClassValue(int smallid,int userId)
		{
			Hashtable table=new Hashtable();
			string sql="select B.name as sname,B.fid as bigid,A.name as bname,B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,B.url as photourl,B.t_time_kc as kancha,B.t_time_gc as gongcheng,B.t_time_ts as teshu from bigclass_part A,smallclass_part B where A.id=B.fid AND B.id='"+smallid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				if(dr!=null)
				{
					while(dr.Read())
					{
						string sname=Convert.ToString(dr["sname"]);
						table.Add("sname",sname);
						string bigid=Convert.ToString(dr["bigid"]);
						table.Add("bigid",bigid);
						string bname=Convert.ToString(dr["bname"]);
						table.Add("bname",bname);
						string timelimit=Convert.ToString(dr["timelimit"]);
						table.Add("timelimit",timelimit);
						string rolecode=Convert.ToString(dr["rolecode"]);
						table.Add("rolecode",rolecode);
						string dutyunit=Convert.ToString(dr["dutyunit"]);
						table.Add("dutyunit",dutyunit);
						string photourl=Convert.ToString(dr["photourl"]);
						table.Add("photourl",photourl);
						string kancha=Convert.ToString(dr["kancha"]);
						table.Add("kancha",kancha);
						string gongcheng=Convert.ToString(dr["gongcheng"]);
						table.Add("gongcheng",gongcheng);
						string teshu=Convert.ToString(dr["teshu"]);
						table.Add("teshu",teshu);
					}	
				}
				else
				{
					return null;
				}
				dr.Close();
				if(table["dutyunit"].ToString().Equals(""))
				{
					table.Add("departname","");
				}
				else
				{
					//					string[] ids=table["dutyunit"].ToString().Split(',');
					//					for(int i=0;i<ids.Length;i++)
					//					{
					//						
					//					}

					string ids=table["dutyunit"].ToString();
					sql="select rolename from role where rolecode in("+ids+")";
					dr=DataAccess.ExecuteReader(sql,null);
					string names="";
					while(dr.Read())
					{
						names+=dr["rolename"].ToString()+",";
					}
					if(names.Length>0)
					{
						names=names.Substring(0,names.Length-1);
					}
					table.Add("departname",names);

				}
				dr.Close();
				return table;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "setPartClassValue");
				throw;
			}

		}

		public static Hashtable setEventClassValue(int smallid,int userId)
		{
			Hashtable table=new Hashtable();
			string sql="select B.name as sname,A.name as bname,B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,B.t_time_kc as kancha,B.t_time_ts as zonghe from bigclass_event A,smallclass_event B where A.id=B.fid AND B.id='"+smallid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				if(dr!=null)
				{
					while(dr.Read())
					{
						string sname=Convert.ToString(dr["sname"]);
						table.Add("sname",sname);
						string bname=Convert.ToString(dr["bname"]);
						table.Add("bname",bname);
						string timelimit=Convert.ToString(dr["timelimit"]);
						table.Add("timelimit",timelimit);
						string rolecode=Convert.ToString(dr["rolecode"]);
						table.Add("rolecode",rolecode);
                        string dutyunit = bacgBL.Pub.Tools.changeNull(Convert.ToString(dr["dutyunit"]));
						table.Add("dutyunit",dutyunit);
						string kc=Convert.ToString(dr["kancha"]);
						table.Add("kancha",kc);
						string zh=Convert.ToString(dr["zonghe"]);
						table.Add("zonghe",zh);
					}	
				}

				else
				{
					return null;
				}
				dr.Close();
				if(table["dutyunit"].ToString().Equals(""))
				{
					table.Add("departname","");
				}
				else
				{
//					string[] ids=table["dutyunit"].ToString().Split(',');
//					for(int i=0;i<ids.Length;i++)
//					{
//						
//					}

					string ids=table["dutyunit"].ToString();
					sql="select rolename from role where rolecode in("+ids+")";
					dr=DataAccess.ExecuteReader(sql,null);
					string names="";
					while(dr.Read())
					{
						names+=dr["rolename"].ToString()+",";
					}
					if(names.Length>0)
					{
						names=names.Substring(0,names.Length-1);
					}
					table.Add("departname",names);

				}
				dr.Close();
				return table;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "setEventClassValue");
				throw;
			}

		}
		public static string setEventValue(int bigid,int userId)
		{
			string name="";
			string sql = "select name from bigclass_event where id='"+bigid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				list = new ArrayList();
				while(dr.Read())
				{
					name=Convert.ToString(dr["name"]);
				}
				dr.Close();
				return name;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "setEventValue");
				throw;
			}
		}

		public static string setPartValue(int bigid,int userId)
		{
			string name="";
			string sql = "select name from bigclass_part where id='"+bigid+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					name=Convert.ToString(dr["name"]);
				}
				dr.Close();
				return name;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "setPartValue");
				throw;
			}
		}

		public static void addEvent(string className,int userId)
		{
			string sql = "insert into bigclass_event (name) values(@name)";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;

				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.addEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "addEvent");
				throw;
			}
		}

		public static void updateEvent(string className,int bigid,int userId)
		{
			string sql = "update bigclass_event set name=@name where id=@id";
			try
			{
				SqlParameter[] input = new SqlParameter[2];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@id",SqlDbType.Int);
				input[1].Value=bigid;
				input[1].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.updateEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updateEvent");
				throw;
			}
		}

		public static void delEvent(int bigid,int userId)
		{
			string sql="delete bigclass_event where id = '"+bigid+"'";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.delEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "delEvent");
				throw;
			}

		}

		public static void addPart(string className,int userId)
		{
			string sql = "insert into bigclass_part (name) values(@name)";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.addPart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "addPart");
				throw;
			}

		}

		public static void updatePart(string className,int bigid,int userId)
		{
			string sql = "update bigclass_part set name=@name where id=@id";
			try
			{
				SqlParameter[] input = new SqlParameter[2];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@id",SqlDbType.Int);
				input[1].Value=bigid;
				input[1].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.updatePart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updatePart");
				throw;
			}
		}

		public static void delPart(int bigid,int userId)
		{
			string sql="delete bigclass_part where id = '"+bigid+"'";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.delPart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "delPart");
				throw;
			}
		}

		public static ArrayList InitDepartmentData(int userId)
		{
			StringBuilder strSQL;
			strSQL = new StringBuilder();
			strSQL.Append("	SELECT rolecode AS code, ");
			strSQL.Append("	rolename AS name ");
			strSQL.Append("	FROM role where step=4");
			try
			{
				dr = DataAccess.ExecuteReader(strSQL.ToString(), null);
				list = new ArrayList();
				while(dr.Read())
				{
					list.Add(new USState(Convert.ToString(dr["name"]), Convert.ToString(dr["code"])));
				}
				dr.Close();
				return (ArrayList)(list);
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					strSQL.ToString(),
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InitDepartmentData");
				throw;
			}
		}

		public static ArrayList getDutyunit(int userId,string areacode)
		{
			
			string sql="select rolecode,rolename from role where step=4 order by areacode,rolename desc";
			if(!areacode.Equals("4403"))
			{
				sql="select rolecode,rolename from role where step=4 and areacode='"+areacode+"' order by areacode,rolename desc";
			}
			try
			{
				dr = DataAccess.ExecuteReader(sql, null);
				list = new ArrayList();
				while(dr.Read())
				{
					string[] unit=new string[2];
					unit[0]=dr["rolecode"].ToString();
					unit[1]=dr["rolename"].ToString();
					list.Add(unit);
				}
				dr.Close();
				return (ArrayList)(list);
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InitDepartmentData");
				throw;
			}
		}

		public static string getEventClassRoleName(int rolecode,int userId)
		{
			string rolename="";
			string sql="SELECT rolename FROM role where rolecode='"+rolecode+"'";
			try
			{
				dr=DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					rolename=Convert.ToString(dr["rolename"]);
				}
				dr.Close();
				return rolename;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getEventClassRoleName");
				throw;
			}
		}


		public static void addClassEvent(int bigid,string className,int limit,int rolecode,string dutyunit,int userId,int kc,int zh)
		{
			string sql = "insert into smallclass_event (fid,name,t_time,rolecode,dutyunit,t_time_kc,t_time_ts) values(@fid,@name,@t_time,@rolecode,@dutyunit,@t_time_kc,@t_time_ts)";
			try
			{
				SqlParameter[] input=new SqlParameter[7];
				input[0]=new SqlParameter("@fid",SqlDbType.Int);
				input[0].Value=bigid;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[1].Value=className;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@t_time",SqlDbType.Int);
				input[2].Value=limit;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@rolecode",SqlDbType.Int);
				input[3].Value=rolecode;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@dutyunit",SqlDbType.VarChar,255);
				input[4].Value=dutyunit;
				input[4].Direction=ParameterDirection.Input;

				//=======================================4.23
				input[5]=new SqlParameter("@t_time_kc",SqlDbType.Int);
				input[5].Value=kc;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@t_time_ts",SqlDbType.Int);
				input[6].Value=zh;
				input[6].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);
			
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.addClassEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "addClassEvent");
				throw;
			}


		}

		public static void updatClassEvent(int smallid,string className,int limit ,int rolecode,string dutyunit,int userId,int kc,int zh)
		{
			string sql="update smallclass_event set name=@name,rolecode=@rolecode,t_time=@t_time,dutyunit=@dutyunit,t_time_kc=@t_time_kc,t_time_ts=@t_time_ts where id=@smallid";
			try
			{
				SqlParameter[] input=new SqlParameter[7];

				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@t_time",SqlDbType.Int);
				input[1].Value=limit;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@rolecode",SqlDbType.Int);
				input[2].Value=rolecode;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@dutyunit",SqlDbType.VarChar,255);
				input[3].Value=dutyunit;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@smallid",SqlDbType.Int);
				input[4].Value=smallid;
				input[4].Direction=ParameterDirection.Input;
				//=======================4.23
				input[5]=new SqlParameter("@t_time_kc",SqlDbType.Int);
				input[5].Value=kc;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@t_time_ts",SqlDbType.Int);
				input[6].Value=zh;
				input[6].Direction=ParameterDirection.Input;

				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.updatClassEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updatClassEvent");
				throw;
			}

		}

		public static void addClassPart(int bigid,string className,int limit,int rolecode,string dutyunit,string photourl,int userId,int kc,int gc,int ts)
		{
			string sql="insert into smallclass_part (fid,name,t_time,rolecode,dutyunit,url,t_time_kc,t_time_gc,t_time_ts) values(@fid,@name,@t_time,@rolecode,@dutyunit,@url,@t_time_kc,@t_time_gc,@t_time_ts)";
			try
			{
				SqlParameter[] input=new SqlParameter[9];
				input[0]=new SqlParameter("@fid",SqlDbType.Int);
				input[0].Value=bigid;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[1].Value=className;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@t_time",SqlDbType.Int);
				input[2].Value=limit;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@rolecode",SqlDbType.Int);
				input[3].Value=rolecode;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@dutyunit",SqlDbType.VarChar,255);
				input[4].Value=dutyunit;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@url",SqlDbType.VarChar,512);
				input[5].Value=photourl;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@t_time_kc",SqlDbType.Int);
				input[6].Value=kc;
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@t_time_gc",SqlDbType.Int);
				input[7].Value=gc;
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@t_time_ts",SqlDbType.Int);
				input[8].Value=ts;
				input[8].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.addClassPart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "addClassPart");
				throw;
			}

		}

		public static void updatClassPart(int smallid,string className,int limit ,int rolecode,string dutyunit,string photourl,int userId,int kc,int gc,int ts)
		{
			string sql="update smallclass_part set name=@name,t_time=@t_time,rolecode=@rolecode,dutyunit=@dutyunit,url=@url,t_time_kc=@t_time_kc,t_time_gc=@t_time_gc,t_time_ts=@t_time_ts where id=@id";
			try
			{
				SqlParameter[] input=new SqlParameter[9];

				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=className;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@t_time",SqlDbType.Int);
				input[1].Value=limit;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@rolecode",SqlDbType.Int);
				input[2].Value=rolecode;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@dutyunit",SqlDbType.VarChar,255);
				input[3].Value=dutyunit;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@url",SqlDbType.VarChar,512);
				input[4].Value=photourl;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@id",SqlDbType.Int);
				input[5].Value=smallid;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@t_time_kc",SqlDbType.Int);
				input[6].Value=kc;
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@t_time_gc",SqlDbType.Int);
				input[7].Value=gc;
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@t_time_ts",SqlDbType.Int);
				input[8].Value=ts;
				input[8].Direction=ParameterDirection.Input;
				DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.updatClassPart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updatClassPart");
				throw;
			}
		}

		public static void delClassPart(int smallid,int userId)
		{
			string sql="delete smallclass_part where id = '"+smallid+"'";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.delClassPart");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "delClassPart");
				throw;
			}
		}

		public static void delClassEvent(int smallid,int userId)
		{
			string sql="delete smallclass_event where id = '"+smallid+"'";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10100(),sql,"szcg.com.teamax.szbase.doormanager.BASE_doormanager.delClassEvent");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "delClassEvent");
				throw;
			}
		}

		public static int getBigClassID(string name,int userId)
		{
			int bigid=0;
			string sql="select id from bigclass_event where name='"+name+"'";
			try
			{
				dr=DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					bigid=Convert.ToInt32(dr["id"]);
				}
				dr.Close();
				return bigid;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getBigClassID");
				throw;
			}
		}

		

		public class USState
		{
			private string myShortName ;
			private string myLongName ;
    
			public  USState(string strLongName, string strShortName)
			{

				this.myShortName = strShortName;
				this.myLongName = strLongName;
			}

			public string ShortName
			{
				get
				{
					return myShortName;
				}
			}

			public string LongName
			{
        
				get
				{
					return myLongName ;
				}
			}

			public override string ToString()
			{
				return this.ShortName + " - " + this.LongName;
			}
		}

		public static bool checkBigClassEventName(string name,int userId)
		{
			string sql="select count(*) from bigclass_event where name=@name";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=name;
				input[0].Direction=ParameterDirection.Input;
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "checkUserGroupName");
				throw;
			}
		}

		public static bool checkBigClassPartName(string name,int userId)
		{
			string sql="select count(*) from bigclass_part where name=@name";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=name;
				input[0].Direction=ParameterDirection.Input;
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "checkUserGroupName");
				throw;
			}
		}

		public static bool checkSmallClassEventName(string name,int userId)
		{
			string sql="select count(*) from smallclass_event where name=@name";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=name;
				input[0].Direction=ParameterDirection.Input;
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "checkUserGroupName");
				throw;
			}
		}

					public static bool checkSmallClassPartName(string name,int userId)
					{
						string sql="select count(*) from smallclass_part where name=@name";
						try
						{
							SqlParameter[] input=new SqlParameter[1];
							input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
							input[0].Value=name;
							input[0].Direction=ParameterDirection.Input;
							if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
							{
								return true;
							}
							return false;
						}
						catch(Exception ex)
						{
							BASE_logmanageservice.writeSystemLog(
								userId,
								sql,
								System.DateTime.Now,
								System.DateTime.Now, 
								BASE_ModerId.getSystem_ZCPT(),
								ex.ToString(),
								NAMESPACE_PATH + "checkUserGroupName");
							throw;
						}
						

		}
	}
}
