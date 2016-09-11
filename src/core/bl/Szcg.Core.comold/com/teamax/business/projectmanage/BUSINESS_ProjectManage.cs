using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using szcg.com.teamax.util;
using szcg.com.teamax.business.entity;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;


namespace szcg.com.teamax.business
{
	/// <summary>
	/// ProjectManage 的摘要说明。
	/// </summary>
	
	public class BUSINESS_ProjectManage 
	{
		protected ProjInfo projInfo;
		protected ArrayList list;
		protected CollecterInfo coll;
		protected SqlDataReader rs;
		private static string QueryClass = "select s_bigclass_part.id as bid, s_bigclass_part.name as bigclassname ,s_smallclass_part.rolecode, s_smallclass_part.id as sid, s_smallclass_part.name as smallname, s_smallclass_part.t_time from s_bigclass_part, s_smallclass_part where s_bigclass_part.id = s_smallclass_part.fid and  s_smallclass_part.id= '?' ";
		private static string QueryEventClass = "select s_bigclass_event.id as bid,s_bigclass_event.name as bigclassname,s_smallclass_event.id as sid,s_smallclass_event.name as smallname,s_smallclass_event.t_time from s_bigclass_event,s_smallclass_event where s_bigclass_event.id=s_smallclass_event.fid and s_smallclass_event.id='?'";

		//插入Pda消息
		public void insertPDAMsg(string sql)
		{
			DataAccess.ExecuteNonQuery(sql,null);
		}
		
		//得到接到数据
		public ArrayList getStreet(string gridcode,int usercode)
		{
			string sql = "select  area.areacode, area.areaname,street.streetcode, street.streetname,community.commcode,community.commname from s_grid ,"+
				"s_community,s_street, s_area where gridcode =@gridcode  and  commcode = substring(gridcode, 0, 13) and streetcode = substring(gridcode, 0,10) and areacode = substring(gridcode, 0,8)";
			list = new ArrayList();
			try
			{
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@gridcode",gridcode);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				if(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setStreet(Convert.ToString(rs["streetname"]));
					projInfo.setSquare(Convert.ToString(rs["commname"]));
					list.Add(projInfo);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(usercode,sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"szcg.com.teamax.business.getStreet");
				throw;
			}
			return list;
		}

		//判断是否是督办
		public string getIspress(string projcode,int usercode)
		{
			string sql = "select ispress from b_project where projcode =@projcode";
			try
			{
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				string ispress = "";
				SqlDataReader rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				if(rs.Read())
				{
					ispress = Convert.ToString(rs["ispress"]);
				}
				rs.Close();
				return ispress;
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(usercode,sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"szcg.com.teamax.business.getIspress");
				throw;
			}
		}

		//		//取公众信息
		//		public ArrayList getPubInfo(string projcode,int usercode)
		//		{
		//			string sql = "select * from public_proj where projcode = '"+projcode+"'";
		//			try
		//			{
		//				list = new ArrayList();
		//				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
		//				if(rs==null)
		//				{
		//					rs.Close();
		//					return null;
		//				}
		//				if(rs.Read())
		//				{
		//
		//				}
		//				rs.Close();
		//				return list;
		//			}
		//			catch(Exception e)
		//			{
		//				BASE_logmanageservice.writeSystemLog(usercode,sql,
		//					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
		//					,"szcg.com.teamax.business.getPubInfo");
		//				throw;
		//			}
		//		}

		//取习惯用语
		public ArrayList getDictionarylib(int usercode)
		{
			string sql = "select * from dictionarylib";
			try
			{
				list = new ArrayList();
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					return null;
				}
				while(rs.Read())
				{
					string id = Convert.ToString(rs["id"])+"$";
					string name = Convert.ToString(rs["name"]);
					list.Add(id+name);
				}
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(usercode,sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"szcg.com.teamax.business.getDictionarylib");
				throw;
			}
		}

		//判断是否存在附件(声音和图片)
		public int getAnnex(string projcode)
		{
			try
			{
				string sql = "select count(*) from b_project_file where projcode = '"+projcode+"'";
				string sql1 = "select count(*) from b_project_sound where projcode = '"+projcode+"'";
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				int filecount = 0;
				int soundcount = 0;
				if(rs.Read())
				{
					filecount = Convert.ToInt32(rs[0]);
				}
				rs.Close();
				rs = DataAccess.ExecuteReader(sql1,null);
				if(rs.Read())
				{
					soundcount = Convert.ToInt32(rs[0]);
				}
				rs.Close();
				if(filecount+soundcount==0)
				{
					string sql2 = "select count(*) from szcg_bak..b_project_file where projcode = '"+projcode+"'";
					string sql3 = "select count(*) from szcg_bak..b_project_sound where projcode = '"+projcode+"'";
					SqlDataReader rs1 = DataAccess.ExecuteReader(sql2,null);
//					int filecount = 0;
//					int soundcount = 0;
					if(rs1.Read())
					{
						filecount = Convert.ToInt32(rs1[0]);
					}
					rs1.Close();
					rs1 = DataAccess.ExecuteReader(sql3,null);
					if(rs1.Read())
					{
						soundcount = Convert.ToInt32(rs1[0]);
					}
					rs1.Close();
				}
				return filecount+soundcount;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return 0;
			}
		}

		//szcg_bak取附件
		public int getAnnexs(string projcode)
		{
			try
			{
				string sql = "select count(*) from szcg_bak.dbo.b_project_file where projcode = '"+projcode+"'";
				string sql1 = "select count(*) from szcg_bak.dbo.b_project_sound where projcode = '"+projcode+"'";
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				int filecount = 0;
				int soundcount = 0;
				if(rs.Read())
				{
					filecount = Convert.ToInt32(rs[0]);
				}
				rs.Close();
				rs = DataAccess.ExecuteReader(sql1,null);
				if(rs.Read())
				{
					soundcount = Convert.ToInt32(rs[0]);
				}
				rs.Close();

				return filecount+soundcount;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return 0;
			}
		}
		//取习惯用语内容
		public ArrayList getDiction(string stepid)
		{
			string sql = "select * from s_diction_sentence where fid = '3' and stepid = '"+stepid+"'";
			try
			{
				list = new ArrayList();
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					return null;
				}
				string text_sentence = "";
				while(rs.Read())
				{
					text_sentence = Convert.ToString(rs["short_sentence"]);
					list.Add(text_sentence);
				}
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到并发操作标识
		public string getIspresses(string projname,int usercode)
		{
			string sql = "select ispress from b_project where projcode = (select projcode from b_project_detail where projname = @projname)";
			try
			{
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projname",projname);
				string ispress = "";
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				if(rs.Read())
				{
					ispress = Convert.ToString(rs["ispress"]);
				}
				rs.Close();
				return ispress;
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(usercode,sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"szcg.com.teamax.business.getIspresses");
				throw;
			}
		}

		//插入回复信息
		public void insertFeedback(string usercode,string optionid,string content)
		{
//			string sql = "insert into feedback values(@usercode,@optionid,@DateTime,@content,'')";
			string sql = "insert into b_opinion_feedback values(@usercode,@optionid,GetDate(),@content,'')";
			try
			{
				SqlParameter[] param = new SqlParameter[3];
				param[0] = new SqlParameter("@usercode",usercode);
				param[1] = new SqlParameter("@optionid",optionid);
//				param[2] = new SqlParameter("@DateTime",Convert.ToString(System.DateTime.Now));
				param[2] = new SqlParameter("@content",content);
				DataAccess.ExecuteNonQuery(sql,param);
				BASE_logmanageservice.writeUserLog(
					Convert.ToInt16(usercode),
					System.DateTime.Today,
					System.DateTime.Today,
					BASE_ModerId.getModel_111051001(),
					sql,
					"szcg.com.teamax.business.insertFeedback");
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(Convert.ToInt32(usercode),sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"szcg.com.teamax.business.insertFeedback");
				throw;
			}
		}

		//得到回复信息
		public ArrayList getFeedback(string optionid)
		{
			try
			{
				//				string sql1 = "select count(*) from feedback where fid = @optionid";
                string sql = "select * from b_opinion_feedback where fid = @optionid order by id desc";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@optionid",optionid);
				//				SqlDataReader drs = DataAccess.ExecuteReader(sql1,param);
				//				
				//				if(drs.Read())
				//				{
				//					
				//				}
				//				drs.Close();
				SqlDataReader dr = DataAccess.ExecuteReader(sql,param);
				string usernames = "";
				string content = "";
				string cudate = "";
				list = new ArrayList();
				while(dr.Read())
				{
					string sqls = "select username from p_user where usercode = '"+Convert.ToInt32(dr["usercode"])+"'";
					SqlDataReader rs = DataAccess.ExecuteReader(sqls,null);
					if(rs.Read())
					{
						usernames = Convert.ToString(rs["username"])+"#";
					}
					rs.Close();
					content = Convert.ToString(dr["content"])+"#";
					cudate = Convert.ToString(dr["cudate"]);
					list.Add(usernames+content+cudate);
				}
				dr.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}
		//得到属于区的所有案卷
		public ArrayList getAllProject(string areacode,string stepid,string stateid)
		{
			try
			{
				list = new ArrayList();
				string sql = "SELECT b_project.projcode, b_project.stepid, b_project.stateid, b_project.role, b_project.typecode, " +
					"b_project.startdate, b_project.gridcode, b_project.fid, b_project_detail.projname, b_project_detail.square AS _square, b_project_detail.smallclass ,"+
					"b_project_detail.street, b_project_detail.probsource,b_project_detail.probdesc,b_project_detail.area, b_project_detail.probclass, b_project_detail.bigclass FROM b_project "+
					"INNER JOIN b_project_detail ON b_project.projcode = b_project_detail.projcode and stepid=@stepid and stateid=@stateid and isdel = '0' and substring(gridcode, 0,8) = @areacode order by b_project.projcode";
				SqlParameter[] param = new SqlParameter[3];
				param[0] = new SqlParameter("@stepid",stepid);
				param[1] = new SqlParameter("@stateid",stateid);
				param[2] = new SqlParameter("@areacode",areacode);
				SqlDataReader dr = DataAccess.ExecuteReader(sql,param);
				if(dr==null)
				{
					dr.Close();
					return null;
				}
				while(dr.Read())
				{
					projInfo = new ProjInfo();

					projInfo.setProjcode(System.Convert.ToInt32(dr["projcode"]));
					projInfo.setProjname(System.Convert.ToString(dr["projname"]));
					projInfo.setStartdate(System.Convert.ToString(dr["startdate"]));
					projInfo.setStepid(System.Convert.ToString(dr["stepid"]));
					projInfo.setStateid(System.Convert.ToString(dr["stateid"]));
					projInfo.setRole(System.Convert.ToInt32(dr["role"]));
					projInfo.setTypecode(System.Convert.ToString(dr["typecode"]));
					projInfo.setGridcode(System.Convert.ToString(dr["gridcode"]));
					projInfo.setFid(System.Convert.ToString(dr["fid"]));
					projInfo.setSmallclass(System.Convert.ToString(dr["smallclass"]));
					projInfo.setBigclass(System.Convert.ToString(dr["bigclass"]));
					projInfo.setStreet(System.Convert.ToString(dr["street"]));
					projInfo.setSquare(System.Convert.ToString(dr["_square"]));
					projInfo.setProbclass(System.Convert.ToString(dr["probclass"]));
					projInfo.setProbdesc(System.Convert.ToString(dr["probdesc"]));
					projInfo.setArea(System.Convert.ToString(dr["area"]));
					projInfo.setProbsource(System.Convert.ToString(dr["probsource"]));
					string tempsql = "";
					string probclass = Convert.ToString(dr["probclass"]);
					if(probclass=="0")
					{
						tempsql = QueryClass.Replace("?",System.Convert.ToString(dr["smallclass"]));
					}
					else
					{
						tempsql = QueryEventClass.Replace("?",System.Convert.ToString(dr["smallclass"]));
					}
					SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
					if(temp_dr.Read())
					{
						projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
						projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
						//projInfo.setRolecode(System.Convert.ToString(temp_dr["rolecode"]));
					}
					temp_dr.Close();

					list.Add(projInfo);
				}
				dr.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}
		//判断案卷是否在处理状态
		public string getIstransaction(string projcode)
		{
			try
			{
				string istransaction = "";
				string sql = "select istransaction from b_project where projcode = @projcode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				if(rs.Read())
				{
					istransaction = Convert.ToString(rs["istransaction"]);
				}
				rs.Close();
				return istransaction;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public string getAreaRole(string areacode)
		{
			try
			{
				string rolename = "";
				string sql = "select rolename from p_role where areacode = @areacode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@areacode",areacode);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				while(rs.Read())
				{
					rolename += Convert.ToString(rs["rolename"]) + ",";
				}
				rs.Close();
				return rolename.Substring(0,rolename.Length-1);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}
		//插入督办内容
		public void insertInspect(string projname,int usercode,string content,string title,string roleid)
		{
			try
			{
				string projcode = "";
				string sql = "select projcode from b_project_detail where projname = @projname";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projname",projname);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs.Read())
				{
					projcode = Convert.ToString(rs["projcode"]);
				}
				rs.Close();
				SqlParameter[] parames = new SqlParameter[5];
				parames[0] = new SqlParameter("@projcode",projcode);
				parames[1] = new SqlParameter("@usercode",Convert.ToString(usercode));
				parames[2] = new SqlParameter("@content",content);
				parames[3] = new SqlParameter("@title",title);
				parames[4] = new SqlParameter("@roleid",roleid);
//				parames[4] = new SqlParameter("@DateTime",Convert.ToString(System.DateTime.Now));
				string sqls = "insert into inspect values(@projcode,@usercode,@content,@title,GetDate(),@roleid)";
				DataAccess.ExecuteNonQuery(sqls,parames);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}
		//得到权限ID
		public string getSystemId(int rolecode)
		{
			try
			{
				string systemmodel="";
				string systemfid = "";
                //
				string sql = "select system_model_fid from role_model where rolecode = @rolecode and isvisible = '1'";
                //
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@rolecode",Convert.ToString(rolecode));
				SqlDataReader rs = com.teamax.util.DataAccess.ExecuteReader(sql,param);
				while(rs.Read())
				{
					systemmodel = Convert.ToString(rs["system_model_fid"]);
					string sqls = "select modelcode from system_model where id = '"+systemmodel+"'";
					SqlDataReader dr = com.teamax.util.DataAccess.ExecuteReader(sqls,null);
					if(dr.Read())
					{
						systemfid += Convert.ToString(dr["modelcode"])+","; 
					}
					dr.Close();
				}
				rs.Close();
				return systemfid;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		public ArrayList getAreaDepart(string areacode)
		{
			try
			{
				string sql = "select rolecode,rolename from p_role where areacode = @areacode and step = '4'";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@areacode",areacode);
				string rolename="";
				string rolecode="";
				list = new ArrayList();
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
				
					return null;
				}
				while(rs.Read())
				{
					rolename = Convert.ToString(rs["rolename"])+",";
					rolecode = Convert.ToString(rs["rolecode"]);
					list.Add(rolename+rolecode);
				}
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到RoleName
		public string getRoleName(int rolecode)
		{
			try
			{
				string sql = "select rolename from p_role where rolecode = '"+rolecode+"'";
				rs = DataAccess.ExecuteReader(sql,null);
				string rolename = "";
				if(rs.Read())
				{
					rolename = Convert.ToString(rs["rolename"]);
				}
				rs.Close();
				return rolename;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到监督员数据
		public ArrayList getCollector(string collcode)
		{
			string sql = "select a.*,b.commname,c.streetname from m_collecter as a,s_community as b,s_street as c where collcode = @collcode and a.commcode=b.id and b.fid=c.id";
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@collcode",collcode);
			list = new ArrayList();
			try
			{
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				if(rs.Read())
				{
					CollecterInfo collecter = new CollecterInfo();
					
					collecter.setCollName(Convert.ToString(rs["collname"]));
					collecter.setMobile(Convert.ToString(rs["mobile"]));
					collecter.setCollcode(Convert.ToString(rs["collcode"]));
					collecter.setAge(Convert.ToString(rs["age"]));
					collecter.setSex(Convert.ToString(rs["sex"]));
					collecter.setNumbercode(Convert.ToString(rs["numbercode"]));
					collecter.setHomeaddress(Convert.ToString(rs["homeaddress"]));
					collecter.setHometel(Convert.ToString(rs["hometel"]));
					collecter.setSquare(Convert.ToString(rs["commname"]));
					collecter.setStreet(Convert.ToString(rs["streetname"]));
					collecter.seturl(Convert.ToString(rs["url"]));

					list.Add(collecter);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//得到监督员的信息
		public ArrayList getCollectors(string id,string projcode)
		{
			string sql = "";
			if(id!="")
			{
				sql = "select collcode from b_pdamsg where projcode = @id";
			}
			else if(projcode!="")
			{
				sql = "select collcode from szcg_bak.dbo.b_pdamsg where projcode = '"+projcode+"'";
			}
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@id",id);
			rs = DataAccess.ExecuteReader(sql,param);
			string collcode = "";
			if(rs.Read())
			{
				collcode = Convert.ToString(rs["collcode"]);
			}
			rs.Close();
			if(collcode!=null)
			{
				list = getCollector(collcode);
			}
			return list;
		}

		//根据网格得到监督员的详细信息
		public ArrayList getGridCollector(string gridcode)
		{
			try
			{
				string sql = "select * from m_collecter where gridcode =@gridcode and isguard = '1'";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@gridcode",gridcode);
				rs = DataAccess.ExecuteReader(sql,param);
				list = new ArrayList();
				//string collcode="";
				if(rs==null)
				{
					return null;
				}
				//string mobile="";
				while(rs.Read())
				{
					coll = new CollecterInfo();
					coll.setCollcode(Convert.ToString(rs["collcode"]));
					coll.setCollName(Convert.ToString(rs["collname"]));
					coll.setMobile(Convert.ToString(rs["mobile"]));
					coll.setGridCode(Convert.ToString(rs["gridcode"]));
					list.Add(coll);
				}
				
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//核实任务时，向B_PDAMSG插入一条任务
		public void insertPdamsg(string projcode,string collcode,string msgcontent,int usercode)
		{
			string sql = "";
			try
			{
				string sql1 = "select count(*) from b_pdamsg where projcode = @projcode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql1,param);
				int count = 0;
				if(rs.Read())
				{
					count = Convert.ToInt32(rs[0]);
				}
				rs.Close();
				if(count==0)
				{
					sql = "insert into b_pdamsg values(@projcode,@collcode,GetDate(),@msgcontent,'','0','0','')";
				}
				else
				{
					sql = "update b_pdamsg set collcode = @collcode,cu_date=GetDate(),msgcontent=@msgcontent where projcode = @projcode";
				}
				SqlParameter[] parames = new SqlParameter[3];
				parames[0] = new SqlParameter("@projcode",projcode);
				parames[1] = new SqlParameter("@collcode",collcode);
//				parames[2] = new SqlParameter("@DateTime",Convert.ToString(System.DateTime.Now));
				parames[2] = new SqlParameter("@msgcontent",msgcontent);
				DataAccess.ExecuteNonQuery(sql,parames);
				BASE_logmanageservice.writeUserLog(usercode,System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getModel_111001001()
					,sql,"szcg.com.teamax.business.insertPdamsg");
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(usercode,sql,System.DateTime.Now,System.DateTime.Now
					,BASE_ModerId.getSystem_ZHYW(),e.ToString(),"szcg.com.teamax.business.insertPdamsg");
			}
		}

		public void updateLockProj(string projcode)
		{
			try
			{
				string sql = "update b_project set istransaction = '0',lockusercode='0',locktime='' where projcode = @projcode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				DataAccess.ExecuteNonQuery(sql,param);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}
		//取得针对案卷操作的意见
		public ArrayList getProjOption(string sql)
		{
			list = new ArrayList();
			try
			{
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					
					return null;
				}
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					int usercode = Convert.ToInt32(rs["usercode"]);
					string sqls = "select username from p_user where usercode = '"+usercode+"'";
					SqlDataReader temp = DataAccess.ExecuteReader(sqls,null);
					if(temp.Read())
					{
						projInfo.setUsercode(Convert.ToString(temp["username"]));
					}
					temp.Close();
					projInfo.setOpinion(Convert.ToString(rs["_opinion"]));
					projInfo.setStepid(Convert.ToString(rs["stepid"]));
					projInfo.setRedback(Convert.ToString(rs["returntracetag"]));
					projInfo.setCudate(Convert.ToString(rs["cu_date"]));
					list.Add(projInfo);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//取案卷意见
		public ArrayList getOption(string id)
		{
			string sql = "select * from b_project_trace where projcode = '"+id+"' order by id desc";
			list = getProjOption(sql);
			return list;
		}
		//取案卷最后一次回退意见
		public ArrayList getBackOption(string id)
		{
			string sql = string.Format(@"select  top 1* from projtrace
                                        where projcode = '{0}' and returntracetag = '1'
                                        order by id desc",id);
			list = getProjOption(sql);
			return list;
		}
		//得到案件上以步的操作员
		public string getOperator(string id)
		{
			string retValue="";
			string sql = string.Format(@"select top 1 username from loginuser
										 where usercode in (
										 select top 1 usercode  From projtrace
										 where projcode = {0}
										 order by id)",id);
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
				  retValue=dr["username"].ToString();
				}
			    dr.Close();
			return retValue;
		}
		//取案卷最后一次接线员意见
		public ArrayList getReceiverOption(string id)
		{
			string sql = string.Format(@"select  top 1* from projtrace
                                        where projcode = '{0}' and stepid = '1'
                                        order by id desc",id);
			list = getProjOption(sql);
			return list;
		}
		
		//得到案卷属于哪个部门
		public ArrayList getDepart(string smallcode,string probclass,string areacode)
		{
			string sql = "";
			string sql1 = "";
			if(probclass=="0")
			{
				sql1 = "select dutyunit from smallclass_part where id = '"+smallcode+"'";
				
			}
			else
			{
				sql1 ="select dutyunit from smallclass_event where id = '"+smallcode+"'";
			}
			string rolecodes = "";
			SqlDataReader rs = DataAccess.ExecuteReader(sql1,null);
			
			if(rs.Read())
			{
				rolecodes = Convert.ToString(rs[0]);
			}
			rs.Close();
			string[] rolecode = rolecodes.Split(',');
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<rolecode.Length; i++)
			{
				sb.Append("'" + rolecode[i] +"'" + ",");
			}
			if(sb.Length>0)
			{    
				if(areacode.Length == 4) //如果是市级，那么显示全部默认责任部门
                   sql = "select rolename,rolecode,memo from role where rolecode in ("+sb.ToString().Substring(0,sb.Length-1).Trim()+")";
				else
				   sql = "select rolename,rolecode,memo from role where rolecode in ("+sb.ToString().Substring(0,sb.Length-1).Trim()+") and areacode = '"+areacode+"'";
				list = getDeparts(sql,areacode);
			}
			return list;
		}
		
		public ArrayList getDeparts(string sql,string areacode)
		{
			list = new ArrayList();
			try
			{
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null|| !rs.HasRows)
				{
					string sql1 = "select rolename,rolecode,memo from role where step=4 and areacode = '"+areacode+"'";
					rs = DataAccess.ExecuteReader(sql1,null);
				}
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setDepart(Convert.ToString(rs["rolename"]));
					projInfo.setRolecode(Convert.ToString(rs["rolecode"])+"$"+Convert.ToString(rs["memo"]));
					list.Add(projInfo);
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
		/// 查询属于这个区的所有部门 2007.1.18
		/// </summary>
		/// <param name="areacode">区号</param>
		/// <returns></returns>
		public SqlDataReader getAreaDepartName(string areacode)
		{
			try
			{
				string sql = "select departcode,departname from depart where area = @areacode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@areacode",areacode);
				list = new ArrayList();
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				else
				{
				  return rs;
				}
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		private ArrayList getCheckList(String sql,int page)
		{
			list = new ArrayList();
			try
			{
				int RowCount;
				int PageCount;
				com.teamax.util.DataAccess.cutPage(sql,15,out RowCount,out PageCount);
				SqlDataReader rs = com.teamax.util.DataAccess.getPageData(sql,15,page,"projcode","desc",RowCount,PageCount);
				list.Add(Convert.ToString(PageCount));
				if(rs==null)
				{			
					return null;
				}
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setAllrow(RowCount);
					projInfo.setProjcode(Convert.ToInt32(rs["projCode"]));
					projInfo.setProjname(Convert.ToString(rs["projname"]));
					projInfo.setProbsource(Convert.ToString(rs["probsource"]));
					projInfo.setProbclass(Convert.ToString(rs["probclass"]));
					projInfo.setBigclass(Convert.ToString(rs["bigclass"]));
					projInfo.setSmallclass(Convert.ToString(rs["smallclass"]));
					projInfo.setStartdate(Convert.ToString(rs["startdate"]));
					projInfo.setFid(Convert.ToString(rs["fid"]));
					projInfo.setStreet(Convert.ToString(rs["street"]));
					projInfo.setSquare(Convert.ToString(rs["square"]));
					projInfo.setArea(Convert.ToString(rs["area"]));
					string tempsql = "";
					if(Convert.ToString(rs["probclass"]).Equals("0"))
					{
						tempsql = "select bigclass_part.id as bid, bigclass_part.name as bigclassname , smallclass_part.id as sid, smallclass_part.name as smallname, smallclass_part.t_time from bigclass_part, smallclass_part where bigclass_part.id = smallclass_part.fid and  smallclass_part.id= '"+Convert.ToString(rs["smallclass"])+"'";
					}
					else
					{
						tempsql = "select bigclass_event.id as bid, bigclass_event.name as bigclassname , smallclass_event.id as sid, smallclass_event.name as smallname, smallclass_event.t_time from bigclass_event, smallclass_event where bigclass_event.id = smallclass_event.fid and  smallclass_event.id= '"+Convert.ToString(rs["smallclass"])+"'";
					}
					SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
					if(temp_dr.Read())
					{
						projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
						projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
					}
					temp_dr.Close();
					string tempsql1="select username from public_proj where projcode='"+Convert.ToInt32(rs["projCode"])+"'";
					SqlDataReader temp_dr1=com.teamax.util.DataAccess.ExecuteReader(tempsql1,null);
					if(temp_dr1.Read())
					{
					  projInfo.setReceiver(System.Convert.ToString(temp_dr1["username"]));
					}
					temp_dr1.Close();
					projInfo.setGridcode(Convert.ToString(rs["gridcode"]));
					projInfo.setAddress(Convert.ToString(rs["address"]));
					projInfo.setProbdesc(Convert.ToString(rs["probdesc"]));
					list.Add(projInfo);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//单个案卷
		private ArrayList getList(String sql)
		{
			try
			{
				list = new ArrayList();
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs==null)
				{
					
					return null;
				}
				if(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setIspress(rs["Ispress"].ToString());
					projInfo.setProjcode(Convert.ToInt32(rs["projCode"]));
					projInfo.setProjname(Convert.ToString(rs["projname"]));
					projInfo.setProbsource(Convert.ToString(rs["probsource"]));
                    projInfo.setProbclass(Convert.ToString(rs["typecode"]));
					projInfo.setBigclass(Convert.ToString(rs["bigclass"]));
					projInfo.setSmallclass(Convert.ToString(rs["smallclass"]));
					projInfo.setStartdate(Convert.ToString(rs["startdate"]));
					projInfo.setFid(Convert.ToString(rs["fid"]));
					projInfo.setStreet(Convert.ToString(rs["street"]));
					projInfo.setSquare(Convert.ToString(rs["square"]));
					projInfo.setArea(Convert.ToString(rs["area"]));
					string tempsql="";
                    if (Convert.ToString(rs["typecode"]).Equals("False"))
					{
                        tempsql = "select a.id as bid, a.name as bigclassname , b.id as sid, b.name as smallname, b.t_time from s_bigclass_part as a, s_smallclass_part as b where a.bigclassCode = b.bigclassCode and  b.smallcallCode= '" + Convert.ToString(rs["smallclass"]) + "'";
					}
					else
					{
                        tempsql = "select a.id as bid, a.name as bigclassname , b.id as sid, b.name as smallname, b.t_time from s_bigclass_event as a, s_smallclass_event as b where a.bigclassCode = b.bigclassCode and  b.smallcallCode= '" + Convert.ToString(rs["smallclass"]) + "'";
					}
					SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
					if(temp_dr.Read())
					{
						projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
						projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
					}
					temp_dr.Close();
					string roleid = Convert.ToString(rs["role"]);
					if(roleid!="")
					{
                        string rolesql = "select rolename from p_role where rolecode = '" + roleid + "'";
						SqlDataReader roledr = com.teamax.util.DataAccess.ExecuteReader(rolesql,null);
						if(roledr.Read())
						{
							projInfo.setDepart(Convert.ToString(roledr["rolename"]));
						}
						roledr.Close();
					}
					projInfo.setGridcode(Convert.ToString(rs["gridcode"]));
					projInfo.setAddress(Convert.ToString(rs["address"]));
					projInfo.setProbdesc(Convert.ToString(rs["probdesc"]));
					projInfo.setIsthrough(Convert.ToString(rs["isthrough"]));
					projInfo.setIsgreat(Convert.ToString(rs["isgreat"]));
                    //string process = rs["ProcessType"] == null ? "0" : rs["ProcessType"].ToString();
                    //projInfo.setIsProcess(process);
                    string stepdate = rs["dealtime"] == null ? "": rs["dealtime"].ToString();
                    projInfo.setStepdate(stepdate);
					list.Add(projInfo);
				}
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//取得案卷的详细信息
		public ArrayList getProjDetail(string id,string projcode)
		{
			string sql = "";
			if(id!="")
			{
                sql = "select isnull(dbo.f_workdayadd(isnull(a.StepDate,a.startdate),a.ProcessType),'') as dealtime,a.*,b.* from b_project a " +
					",b_project_detail b where a.ProjCode = b.ProjCode and a.projcode='"+id+"'";
			}
			
			list = getList(sql);
			return list;
		}

        //取得案卷的详细信息
        public int getworktime(string start, string  end)
        {
            string sql = "";
            int i = 0;
            if (start!="")
            {
                sql = "select dbo.f_worktime('" + DateTime.Parse(start).ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Parse(end).ToString("yyyy-MM-dd HH:mm:ss") + "') as dealtime ";
            }

            rs = DataAccess.ExecuteReader(sql, null);
            
            if (rs == null)
            {

                i = 0;
            }
            if (rs.Read())
            {
                i = Convert.ToInt32(rs["dealtime"]);
            }
            rs.Close();

            return i;
        }


        //取得专业部门处理阶段案卷的详细信息
        public ArrayList getProjDetail(string projcode)
        {
            string sql = "";
            if (projcode != "")
            {
                sql  = "select  (select top 1 t3=dbo.f_workdayadd(max(b.cu_date),dbo.GetProcessType(b.projcode)) from b_project_trace b with(nolock) where(1=1 and b.CurrentNodeId='8' and b.CurrentBusiStatus='03' "
 +"and b.projcode = '" + projcode + "') group by b.projcode) as dealtime, a.*,b.*  from b_project a ,b_project_detail b "
 +"where a.ProjCode = b.projCode and a.projcode='" + projcode + "'";
            }
             
            list = getList(sql);
            return list;
        }

		/// <summary>
		/// 用来处理市里边督办的案件，回退不能回退到市里边
		/// </summary>
		/// <param name="projcode"></param>
		public void checkDBProject(string projcode)
		{
			string areacode ="";
			string sql = string.Format(@"select areacode from role
										where rolecode in(
											select roleid from inspect
											where projcode ={0})",projcode);
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
			  areacode=dr["areacode"].ToString();
			}
			dr.Close();
			if(areacode=="4403")
			{
			 string sql1=string.Format(@"update b_project set isgreat = 2
                                         where projcode = {0}",projcode);
			 DataAccess.ExecuteNonQuery(sql1,null);
			}
		}
		public ArrayList getProjTail(string projname)
		{
			string sql = "select a.startdate,a.role,a.gridcode,a.fid,a.isgreat,a.isthrough,b.* from b_project a "+
				",b_project_detail b where a.ProjCode = b.ProjCode and b.projname='"+projname+"' union select a.startdate,a.role,a.gridcode,a.fid,a.isgreat,a.isthrough,b.* from szcg_bak..b_project a "+
				",szcg_bak..b_project_detail b where a.ProjCode = b.ProjCode and b.projname='"+projname+"'";
		
			
			//			}
			//			else if(projname!="")
			//			{
			//				sql = "select a.startdate,a.role,a.gridcode,a.fid,b.* from b_project a "+
			//					",b_project_detail b where a.ProjCode = b.ProjCode and b.projname='"+projname+"'";
			//			}
			list = getList(sql);
			//			if(list.Count==0)
			//			{
			//				string sqls = "select a.startdate,a.gridcode,a.fid,b.* from szcg_bak.dbo.b_project a "+
			//					",szcg_bak.dbo.b_project_detail b where a.ProjCode = b.ProjCode and a.projcode='"+id+"'";
			//				list = getList(sqls);
			//				return list;
			//			}
			return list;
		}

		//核查栏判断是否处理过
		public string getPdaIoflag(string projcode)
		{
			try
			{
				string sql = "select * from b_pdamsg where projcode = '"+projcode+"'";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				string ioflag = "";
				string state = "";
				if(dr.Read())
				{
					ioflag = Convert.ToString(dr["ioflag"]);
					state = Convert.ToString(dr["state"]);
				}
				dr.Close();
				return ioflag+state;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}

		//取案卷核查结果
		public string getProjResult(string projcode)
		{
			try
			{
				string msgcontent = "";
				string sql = "select msgcontent from b_pdamsg where projcode = '"+projcode+"'";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					msgcontent = Convert.ToString(dr["msgcontent"]);
				}
				dr.Close();
				return msgcontent;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}

		public string saveProjcode(string projname,string projnames)
		{
			string sql = "";
			if(projname!="")
			{
				sql = "select projcode from b_project_detail where projname = '"+projname+"'";
			}
			else if(projnames!="")
			{
				sql = "select projcode from szcg_bak.dbo.b_project_detail where projname = '"+projnames+"' union select projcode from b_project_detail where projname = '"+projnames+"'";
			}
			string projcode = "";
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			if(dr.Read())
			{
				projcode = Convert.ToString(dr["projcode"]);
			}
			dr.Close();
			return projcode;
		}

		//得到案卷图片文件
		public ArrayList getFilepath(string projcode)
		{
			string sql = " exec GetProjFiles @projcode";
			list = new ArrayList();
			try
			{
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setFilestate(Convert.ToString(rs["filestate"]));
					projInfo.setFilepath(Convert.ToString(rs["filepath"]));
					projInfo.setCudate(Convert.ToString(rs["cudate"]));
					list.Add(projInfo);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//得到案卷声音文件
		public ArrayList getSoundpath(string projcode)
		{
			string sql = " exec GetProjSounds @projcode";
			list = new ArrayList();
			try
			{
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs==null)
				{
					return null;
				}
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setSoundstate(Convert.ToString(rs["soundstate"]));
					projInfo.setSoundpath(Convert.ToString(rs["soundpath"]));
					projInfo.setCudate(Convert.ToString(rs["cudate"]));
					list.Add(projInfo);
				}
				rs.Close();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//取出所有的监督员上报的案卷，以及案卷所对应的信息
		//		public ArrayList getProjectInfo(int id)
		//		{
		//			try
		//			{
		//				string sql = "select a.startdate,a.gridcode,a.fid,b.* from b_project a "+
		//					"INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode where role="+id+"";
		//				list = getCheckList(sql);
		//			}
		//			catch(Exception e)
		//			{
		//				System.Diagnostics.Debug.WriteLine(e.Message);
		//			}
		//			return list;
		//		}

		//取出所有举报的案卷,以及案卷所对应的信息
		public ArrayList getProjectPub(string ip,string areacode,int page)
		{
			try
			{
				string groupId = getGroupId(areacode);
				string sqls = "select a.startdate,a.gridcode,a.fid,b.* from b_project a INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode "+
					"where exists ( select * from ( select projcode from web_proj union " +
					"select projcode from lawer_proj  union select Projcode from web_proj union " +
					"select Projcode from public_proj where groupid = '"+groupId+"' union select projcode from other_proj ) as tt " +
					"where tt.projcode = a.projcode and a.stepid='0' and a.stateid = '1' and a.isdel = '0' and b.probsource != '1' and a.isdel!='1' and a.groupid = '"+groupId+"')";
				////根据操作员IP取案卷号
				//string sql1 = "select * from public_proj where seatIP='"+ip+"'";
				////取区名
				//string sqls = "select areaname from area where areacode = '"+areacode+"'";
				////取公众举报的案卷内容
				//string sql3 = "select * from (select a.startdate,a.gridcode,a.fid,b.* from b_project a INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode where  a.stepid='0' and stateid = '1' and b.probsource != '1' and a.isdel!='1') as t where exists(";
				//string areaname = "";
				//rs = DataAccess.ExecuteReader(sqls,null);
				//if(rs.Read())
				//{
				//	areaname = Convert.ToString(rs["areaname"]);
				//}
				//rs.Close();
				////用IP取属于本机处理的其他案卷
				////取执法举报和网站举报内容
				//string sql2 = "select a.startdate,a.gridcode,a.fid,b.* from b_project a "+
				//"INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode where (b.area = '"+areaname+"' or b.area = '') and a.stepid='0' and a.stateid = '1' and a.isdel = '0' and b.probsource != '1' and b.probsource != '0' and a.isdel!='1' ";
				//string sql = sql2 +"union "+sql3+sql1+" and public_proj.projcode = t.projcode)";
				list = getCheckList(sqls,page);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return list;
		}

		//得到公众的信息
		public string getPublicProj(string projcode,string projcodes)
		{
			try
			{
				string sql = "";
				if(projcode!="")
				{
					sql = "select * from public_proj where projcode = '"+projcode+"'";
				}
				else if(projcodes !="")
				{
					//sql = "select * from szcg_bak.dbo.public_proj where projcode = '"+projcodes+"'";
					sql = "select a.*,b.username from szcg_bak.dbo.public_proj a left join szcg.dbo.public_proj b on a.projcode = b.projcode where a.projcode = '"+projcodes+"'";
				}
				//				SqlParameter[] param = new SqlParameter[1];
				//				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql,null);
				string acceptcode = "";
				string ccid = "";
				string cudate = "";
				string filename = "";
				string receiver ="";
				string username = "0";
				if(rs.Read())
				{
					acceptcode = Convert.ToString(rs["acceptcode"])+"$";
					ccid = Convert.ToString(rs["ccid"])+"$";
					cudate = Convert.ToString(rs["cudate"])+"$";
					filename = Convert.ToString(rs["filename"])+"$";
                    receiver = Convert.ToString(rs["username"])+"$";//
					string sql1 = "select name,duty from personinfo where officetel = '"+Convert.ToString(rs["ccid"])+"' or mobiletel2 = '"+Convert.ToString(rs["ccid"])+"' or mobiletel = '"+Convert.ToString(rs["ccid"])+"' or addresstel2 = '"+Convert.ToString(rs["ccid"])+"' or addresstel = '"+Convert.ToString(rs["ccid"])+"' or homenumber = '"+Convert.ToString(rs["ccid"])+"'";
					SqlDataReader dr = DataAccess.ExecuteReader(sql1,null);
					if(dr.Read())
					{
						username = Convert.ToString(dr["name"])+","+Convert.ToString(dr["duty"]);
					}
					dr.Close();
				}
				rs.Close();
				return acceptcode+ccid+cudate+filename+receiver+username;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到执法人员的信息
		public string getLawerInfo(string projcode,string projcodes)
		{
			try
			{
				string sql ="";
				if(projcode!="")
				{
					sql = "select * from lawer where lawercode = (select lawercode from lawer_proj where projcode = '"+projcode+"')";
				}
				else if(projcodes!="")
				{
					sql = "select * from lawer where lawercode = (select lawercode from szcg_bak.dbo.lawer_proj where projcode = '"+projcodes+"'))";
				}
				
				//				SqlParameter[] param = new SqlParameter[1];
				//				param[0] = new SqlParameter("@projcode",projcode);
				if(projcode=="")
				{
					projcode = projcodes;
				}
				string sql1 = "select id,cu_date from lawer_proj where projcode = '"+projcode+"'";
				rs = DataAccess.ExecuteReader(sql,null);
				string code = "";
				string lawername = "";
				string mobile = "";
				string cu_date = "";
				//	string filename = "";
				if(rs.Read())
				{

					lawername = Convert.ToString(rs["lawername"])+"$";
					mobile = Convert.ToString(rs["mobile"])+"$";
					//cu_date = Convert.ToString(rs["cu_date"]);
					//filename = Convert.ToString(rs["filename"]);
				}
				rs.Close();
				SqlDataReader dr = DataAccess.ExecuteReader(sql1,null);
				if(dr.Read())
				{
					code = Convert.ToString(dr["id"])+"$";
					cu_date = Convert.ToString(dr["cu_date"]);
				}
				dr.Close();
				return code+lawername+mobile+cu_date;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到网站上报人的信息
		public string getWebInfo(string projcode,string projcodes)
		{
			try
			{
				string sql = "";
				if(projcode!="")
				{
					sql = "select * from web_proj where projcode = '"+projcode+"'";
				}
				else if(projcodes!="")
				{
					sql = "select * from szcg_bak_dbo.web_proj where projcode = '"+projcodes+"'";
				}
				
				//				SqlParameter[] param = new SqlParameter[1];
				//				param[0] = new SqlParameter("@projcode",projcode);
				rs = DataAccess.ExecuteReader(sql,null);
				string name = "";
				string tel = "";
				string cudate = "";
				//	string filename = "";
				if(rs.Read())
				{
					name = Convert.ToString(rs["name"])+"$";
					tel = Convert.ToString(rs["tel"])+"$";
					cudate = Convert.ToString(rs["cudate"]);
					//filename = Convert.ToString(rs["filename"]);
				}
				rs.Close();
				return name+tel+cudate;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//判断案卷是否已发送消息
		public string getMsgCount(string projcode)
		{
			try
			{
				string sql = "select count(*) from b_pdamsg where projcode = @projcode";
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@projcode",projcode);
				string count="";
				rs = DataAccess.ExecuteReader(sql,param);
				if(rs.Read())
				{
					count = Convert.ToString(rs[0]);
				}
				rs.Close();
				return count;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}

		//得到存档案卷的信息，需要到另外数据库去读，szcg_bak
		public ArrayList getCdProject(string areacode,int page,string ischeck)
		{
			try
			{
				string sql = "";
				if(areacode=="4403")
				{
					sql = "SELECT szcg_bak.dbo.b_project.projcode, szcg_bak.dbo.b_project.stepid, szcg_bak.dbo.b_project.stateid, szcg_bak.dbo.b_project.role, szcg_bak.dbo.b_project.typecode, " +
						"szcg_bak.dbo.b_project.startdate, szcg_bak.dbo.b_project.gridcode, szcg_bak.dbo.b_project.fid, szcg_bak.dbo.b_project_detail.projname, szcg_bak.dbo.b_project_detail.square, szcg_bak.dbo.b_project_detail.smallclass ,"+
						"szcg_bak.dbo.b_project_detail.street, szcg_bak.dbo.b_project_detail.probsource,szcg_bak.dbo.b_project_detail.address,szcg_bak.dbo.b_project_detail.probdesc,szcg_bak.dbo.b_project_detail.area, szcg_bak.dbo.b_project_detail.probclass, szcg_bak.dbo.b_project_detail.bigclass FROM szcg_bak.dbo.b_project "+
						"INNER JOIN szcg_bak.dbo.b_project_detail ON szcg_bak.dbo.b_project.projcode = szcg_bak.dbo.b_project_detail.projcode and (szcg_bak.dbo.b_project.isgreat = '1' or szcg_bak.dbo.b_project.isgreat = '2')";
					if(ischeck=="0")
					{
						sql += " and b_project_detail.probsource <> '10' ";
					}
				}
				else
				{
					sql = "SELECT szcg_bak.dbo.b_project.projcode, szcg_bak.dbo.b_project.stepid, szcg_bak.dbo.b_project.stateid, szcg_bak.dbo.b_project.role, szcg_bak.dbo.b_project.typecode, " +
						"szcg_bak.dbo.b_project.startdate, szcg_bak.dbo.b_project.gridcode, szcg_bak.dbo.b_project.fid, szcg_bak.dbo.b_project_detail.projname, szcg_bak.dbo.b_project_detail.square, szcg_bak.dbo.b_project_detail.smallclass ,"+
						"szcg_bak.dbo.b_project_detail.street, szcg_bak.dbo.b_project_detail.probsource,szcg_bak.dbo.b_project_detail.address,szcg_bak.dbo.b_project_detail.probdesc,szcg_bak.dbo.b_project_detail.area, szcg_bak.dbo.b_project_detail.probclass, szcg_bak.dbo.b_project_detail.bigclass FROM szcg_bak.dbo.b_project "+
						"INNER JOIN szcg_bak.dbo.b_project_detail ON szcg_bak.dbo.b_project.projcode = szcg_bak.dbo.b_project_detail.projcode and substring(gridcode, 0,8) = '"+areacode+"' and (szcg_bak.dbo.b_project.isgreat = '0' or szcg_bak.dbo.b_project.isgreat is null)";
					if(ischeck=="0")
					{
						sql += " and b_project_detail.probsource <> '10' ";
					}
				}
				list = getCheckList(sql,page);
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}
		public ArrayList getCdtProject(string areacode,int page,string ischeck,string probsource,string times1,string times2)
		{
			try
			{
				string sql = "";
				if(areacode=="4403")
				{
					sql = "SELECT A.projcode, A.stepid, A.stateid, A.role, A.typecode,A.startdate, A.gridcode, A.fid, B.projname, B.square, B.smallclass ,"+
						"B.street, B.probsource,B.address,B.probdesc,B.area, B.probclass, "+
						"B.bigclass FROM szcg_bak.dbo.b_project as A INNER JOIN szcg_bak.dbo.b_project_detail as B ON A.projcode = B.projcode and (A.isgreat = '1' or A.isgreat = '2')";
					sql += " where stepid = '5' and stateid = '5'";
					if(probsource != "请选择")
					{
						sql += " and B.probsource = '"+probsource+"'";
					}
					if(times1 != string.Empty)
					{
						sql += " and startdate >= '" + times1 + "'";
					}
					if(times2 != string.Empty)
					{
						sql += " and startdate <= '" + times2 + "'";
					}
					if(ischeck=="0")
					{
						sql += " and B.probsource <> '10' ";
					}
					
				}
				else
				{
					//					sql = "SELECT szcg_bak.dbo.b_project.projcode, szcg_bak.dbo.b_project.stepid, szcg_bak.dbo.b_project.stateid, szcg_bak.dbo.b_project.role, szcg_bak.dbo.b_project.typecode, " +
					//						"szcg_bak.dbo.b_project.startdate, szcg_bak.dbo.b_project.gridcode, szcg_bak.dbo.b_project.fid, szcg_bak.dbo.b_project_detail.projname, szcg_bak.dbo.b_project_detail.square, szcg_bak.dbo.b_project_detail.smallclass ,"+
					//						"szcg_bak.dbo.b_project_detail.street, szcg_bak.dbo.b_project_detail.probsource,szcg_bak.dbo.b_project_detail.address,szcg_bak.dbo.b_project_detail.probdesc,szcg_bak.dbo.b_project_detail.area, szcg_bak.dbo.b_project_detail.probclass, szcg_bak.dbo.b_project_detail.bigclass FROM szcg_bak.dbo.b_project "+
					//	"INNER JOIN szcg_bak.dbo.b_project_detail ON szcg_bak.dbo.b_project.projcode = szcg_bak.dbo.b_project_detail.projcode and substring(gridcode, 0,8) = '"+areacode+"' and (szcg_bak.dbo.b_project.isgreat = '0' or szcg_bak.dbo.b_project.isgreat is null) where szcg_bak.dbo.b_project_detail.probsource='"+probsource+"'and szcg_bak.dbo.b_project_detail.area='"+area+"' and szcg_bak.dbo.b_project.startdate>='"+times1+"' and szcg_bak.dbo.b_project.enddate='"+times2+"'";
					sql = "SELECT szcg_bak.dbo.b_project.projcode, szcg_bak.dbo.b_project.stepid, szcg_bak.dbo.b_project.stateid, szcg_bak.dbo.b_project.role, szcg_bak.dbo.b_project.typecode, " +
						"szcg_bak.dbo.b_project.startdate, szcg_bak.dbo.b_project.gridcode, szcg_bak.dbo.b_project.fid, szcg_bak.dbo.b_project_detail.projname, szcg_bak.dbo.b_project_detail.square, szcg_bak.dbo.b_project_detail.smallclass ,"+
						"szcg_bak.dbo.b_project_detail.street, szcg_bak.dbo.b_project_detail.probsource,szcg_bak.dbo.b_project_detail.address,szcg_bak.dbo.b_project_detail.probdesc,szcg_bak.dbo.b_project_detail.area, szcg_bak.dbo.b_project_detail.probclass, szcg_bak.dbo.b_project_detail.bigclass FROM szcg_bak.dbo.b_project "+
						"INNER JOIN szcg_bak.dbo.b_project_detail ON szcg_bak.dbo.b_project.projcode = szcg_bak.dbo.b_project_detail.projcode and substring(gridcode, 0,8) = '"+areacode+"' and (szcg_bak.dbo.b_project.isgreat = '0' or szcg_bak.dbo.b_project.isgreat is null)";
					sql += " where stepid = '5' and stateid = '5'";
					if(probsource != "请选择")
					{
						sql += " and b_project_detail.probsource = '"+probsource+"'";
					}
					if(times1 != string.Empty)
					{
						sql += " and startdate >= '" + times1 + "'";
					}
					if(times2 != string.Empty)
					{
						sql += " and startdate <= '" + times2 + "'";
					}
					if(ischeck=="0")
					{
						sql += " and b_project_detail.probsource <> '10' ";
					}

					//					sql = "SELECT A.projcode, A.stepid, A.stateid, A.role, A.typecode, A.startdate,"+
					//						"A.gridcode, A.fid, B.projname, B.square, B.smallclass ,B.street, B.probsource,B.address,B.probdesc,"+
					//						"B.area, B.probclass, B.bigclass FROM szcg_bak.dbo.b_project as A INNER JOIN szcg_bak.dbo.b_project_detail as B  ON A.projcode = B.projcode where stepid = '5' and stateid = '5'";
					//					if(probsource != "请选择")
					//					{
					//						sql += " and probsource = '"+probsource+"'";
					//					}
					////					if(area !="请选择")
					////					{
					////						sql += " and area = '"+area+"'";
					////					}
					//					if(times1 != string.Empty)
					//					{
					//						sql += " and startdate >= '" + times1 + "'";
					//					}
					//					if(times2 != string.Empty)
					//					{
					//						sql += " and startdate <= '" + times2 + "'";
					//					}
					////					if(times1 != "" && times2 == "")
					////					{
					////						sql += " and startdate >= '"+times1+" 00:00:00' and startdate <= '"+times1+" 23:59:59'";
					////					}
					////					else if(times2 != "" && times1 =="")
					////					{
					////						sql += " and startdate >= '"+times2+" 00:00:00' and startdate <= '"+times2+" 23:59:59'";
					////					}
					////					else if(times1 != "" && times2 != "")
					////					{
					////						sql += " and startdate >= '"+times1+" 00:00:00' and startdate <= '"+times2+" 23:59:59'";
					////					}
					//					if(ischeck=="0")
					//					{
					//						sql += " and b_project_detail.probsource <> '10' ";
					//					}
					//					
					

				}
				list = getCheckList(sql,page);
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//公众举报登记更新记录
		public void updateProject(string projcode,string bigclass,string smallclass,string area,string street,string square,string address,string probdesc,string probclass,string isgreat)
		{
			try
			{
				string sql = "update b_project_detail set probclass=@probclass,bigclass=@bigclass,smallclass=@smallclass,area=@area,street=@street,square=@square,address=@address,probdesc=@probdesc where projcode = @projcode";
				string sql1 = "update b_project set isgreat = '"+isgreat+"' where projcode = '"+projcode+"'";
				SqlParameter[] param = new SqlParameter[9];
				param[0] = new SqlParameter("@projcode",projcode);
				param[1] = new SqlParameter("@bigclass",bigclass);
				param[2] = new SqlParameter("@smallclass",smallclass);
				param[3] = new SqlParameter("@area",area);
				param[4] = new SqlParameter("@street",street);
				param[5] = new SqlParameter("@square",square);
				param[6] = new SqlParameter("@address",address);
				param[7] = new SqlParameter("@probdesc",probdesc);
				param[8] = new SqlParameter("@probclass",probclass);
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,param);
				com.teamax.util.DataAccess.ExecuteNonQuery(sql1,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public void updateStepid(string stepid,string projcode,string gridcode)
		{
			try
			{
				string isthrough = "";
				if(stepid=="2")
				{
					isthrough = "1";
				}
				else
				{
					isthrough = "0";
				}
				string sql = "update b_project set stepid = '"+stepid+"',gridcode='"+gridcode+"',isthrough = '"+isthrough+"' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public string getPubProjcode(string ip)
		{
			try
			{
				string projcode = "";
				string sql = "select projcode from public_proj where seatIP = '"+ip+"' order by id";
				SqlDataReader rs = DataAccess.ExecuteReader(sql,null);
				if(rs.Read())
				{
					projcode = Convert.ToString(rs["projcode"]);
				}
				rs.Close();
				return projcode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public string insertProject(string stepid,string gridcode,string probsource,string bigclass,string smallclass,string address,string probdesc,string probclass,string name,string tel,string date,string area,string square,string street,string ip,string isgreat,string areacode)
		{
			try
			{
				string groupid = getGroupId(areacode);
				SqlParameter [] input = new SqlParameter[17];
				input[0] = new SqlParameter("@gridcode", System.Data.SqlDbType.VarChar,18);
				input[0].Direction = ParameterDirection.Input;
				input[0].Value = gridcode;
				input[1] = new SqlParameter("@probsource", System.Data.SqlDbType.VarChar,18);
				input[1].Direction = ParameterDirection.Input;
				input[1].Value = probsource;
				input[2] = new SqlParameter("@ProbClass", System.Data.SqlDbType.VarChar,18);
				input[2].Direction = ParameterDirection.Input;
				input[2].Value = probclass;
				input[3] = new SqlParameter("@bigClass", System.Data.SqlDbType.VarChar,18);
				input[3].Direction = ParameterDirection.Input;
				input[3].Value = bigclass;
				input[4] = new SqlParameter("@smallclass", System.Data.SqlDbType.VarChar,18);
				input[4].Direction = ParameterDirection.Input;
				input[4].Value = smallclass;
				input[5] = new SqlParameter("@address", System.Data.SqlDbType.VarChar,512);
				input[5].Direction = ParameterDirection.Input;
				input[5].Value = address;
				input[6] = new SqlParameter("@probdesc", System.Data.SqlDbType.VarChar,1024);
				input[6].Direction = ParameterDirection.Input;
				input[6].Value = probdesc;
				input[7] = new SqlParameter("@name", System.Data.SqlDbType.VarChar,64);
				input[7].Direction = ParameterDirection.Input;
				input[7].Value = name;
				input[8] = new SqlParameter("@tel", System.Data.SqlDbType.VarChar,32);
				input[8].Direction = ParameterDirection.Input;
				input[8].Value = tel;
				input[9] = new SqlParameter("@cudate", System.Data.SqlDbType.VarChar,32);
				input[9].Direction = ParameterDirection.Input;
				input[9].Value = date;
				input[10] = new SqlParameter("@area", System.Data.SqlDbType.VarChar,32);
				input[10].Direction = ParameterDirection.Input;
				input[10].Value = area;
				input[11] = new SqlParameter("@square", System.Data.SqlDbType.VarChar,32);
				input[11].Direction = ParameterDirection.Input;
				input[11].Value = square;
				input[12] = new SqlParameter("@street", System.Data.SqlDbType.VarChar,32);
				input[12].Direction = ParameterDirection.Input;
				input[12].Value = street;
				input[13] = new SqlParameter("@stepid", System.Data.SqlDbType.VarChar,18);
				input[13].Direction = ParameterDirection.Input;
				input[13].Value = stepid;
				input[14] = new SqlParameter("@ip",System.Data.SqlDbType.VarChar,32);
				input[14].Direction = ParameterDirection.Input;
				input[14].Value = ip;
				input[15] = new SqlParameter("@isgreat",System.Data.SqlDbType.Char,1);
				input[15].Direction = ParameterDirection.Input;
				input[15].Value = isgreat;
				input[16] = new SqlParameter("@groupid",System.Data.SqlDbType.VarChar,18);
				input[16].Direction = ParameterDirection.Input;
				input[16].Value = groupid;

				SqlParameter [] output = new SqlParameter[1];
				output[0] = new SqlParameter("@ProjCode",System.Data.SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
				
				string projcode = com.teamax.util.DataAccess.ExecuteStoreProcedure1("insertProject",input,output);
				return projcode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridcode"></param>
		/// <param name="probsource"></param>
		/// <param name="bigclass"></param>
		/// <param name="smallclass"></param>
		/// <param name="address"></param>
		/// <param name="probdesc"></param>
		/// <param name="probclass"></param>
		/// <param name="dbname"></param>
		/// <param name="dbcontent"></param>
		/// <param name="date"></param>
		/// <param name="ip"></param>
		/// <param name="isgreat"></param>
		/// <param name="areacode"></param>
		/// <param name="usercode"></param>
		/// <param name="rolecode"></param>
		/// <returns></returns>
		public string insertDBProject(string gridcode,string probsource,
			string bigclass,string smallclass,string address,string probdesc,
			string probclass,string dbname,string dbcontent,
			string ip,string isgreat,string areacode,
			int usercode,string rolecode)
		{
			try
			{
				string groupid = getGroupId(areacode);
				SqlParameter [] input = new SqlParameter[14];
				input[0] = new SqlParameter("@rolecode",rolecode);
				input[1] = new SqlParameter("@gridcode", gridcode);
				input[2] = new SqlParameter("@probsource", probsource);
				input[3] = new SqlParameter("@bigClass", bigclass);
				input[4] = new SqlParameter("@smallclass",smallclass);
				input[5] = new SqlParameter("@address", address);
				input[6] = new SqlParameter("@probdesc", probdesc);
				input[7] = new SqlParameter("@ProbClass", probclass);
				input[8] = new SqlParameter("@dbname", dbname);
				input[9] = new SqlParameter("@dbcontent", dbcontent);
				input[10] = new SqlParameter("@ip",ip);
				input[11] = new SqlParameter("@isgreat",isgreat);
				input[12] = new SqlParameter("@groupid",groupid);
				input[13] = new SqlParameter("@usercode",usercode);			
				
				SqlParameter [] output = new SqlParameter[1];
				output[0] = new SqlParameter("@ret",System.Data.SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
 

				string retValue = com.teamax.util.DataAccess.ExecuteStoreProcedure1("insertDBProject",input,output);
                return retValue;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridcode"></param>
        /// <param name="probsource"></param>
        /// <param name="bigclass"></param>
        /// <param name="smallclass"></param>
        /// <param name="address"></param>
        /// <param name="probdesc"></param>
        /// <param name="probclass"></param>
        /// <param name="dbname"></param>
        /// <param name="dbcontent"></param>
        /// <param name="date"></param>
        /// <param name="ip"></param>
        /// <param name="isgreat"></param>
        /// <param name="areacode"></param>
        /// <param name="usercode"></param>
        /// <param name="rolecode"></param>
        /// <returns></returns>
		public string updateDBProject(string gridcode,string probsource,
			string bigclass,string smallclass,string address,string probdesc,
			string probclass,string dbname,string dbcontent,
			string ip,string isgreat,string areacode,
			int usercode,string rolecode,int projcode)
		{
			try
			{
				string groupid = getGroupId(areacode);
				SqlParameter [] input = new SqlParameter[15];
				input[0] = new SqlParameter("@rolecode",rolecode);
				input[1] = new SqlParameter("@gridcode", gridcode);
				input[2] = new SqlParameter("@probsource", probsource);
				input[3] = new SqlParameter("@bigClass", bigclass);
				input[4] = new SqlParameter("@smallclass",smallclass);
				input[5] = new SqlParameter("@address", address);
				input[6] = new SqlParameter("@probdesc", probdesc);
				input[7] = new SqlParameter("@ProbClass", probclass);
				input[8] = new SqlParameter("@dbname", dbname);
				input[9] = new SqlParameter("@dbcontent", dbcontent);
				input[10] = new SqlParameter("@ip",ip);
				input[11] = new SqlParameter("@isgreat",isgreat);
				input[12] = new SqlParameter("@groupid",groupid);
				input[13] = new SqlParameter("@usercode",usercode);
		        input[14] = new SqlParameter("@projcode",projcode);	

				
				SqlParameter [] output = new SqlParameter[1];
				output[0] = new SqlParameter("@ret",System.Data.SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
 

				string retValue = com.teamax.util.DataAccess.ExecuteStoreProcedure1("updateDBProject",input,output);
				return retValue;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		public void insertProjtrace(string projcode,string actionname,string usercode,string roleid,string _opinion)
		{
			try
			{
				string sql = "insert into projtrace(projcode,stepid,actionname,cu_date,usercode,_opinion,roleid) values ('"+projcode+"','1','"+actionname+"',getdate(),'"+usercode+"','"+_opinion+"','"+roleid+"')";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public string getOtherInfo(string projcode,string projcodes)
		{
			try
			{
				string sql = "";
				if(projcode!="")
				{
					sql = "select * from other_proj where projcode = '"+projcode+"'";
				}
				else if(projcodes!="")
				{
					sql = "select * from szcg_bak.dbo.other_proj where projcode = '"+projcode+"'";
				}
				
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				string name = "";
				string tel = "";
				string times = "";
				if(dr.Read())
				{
					name = Convert.ToString(dr["name"]);
					tel = Convert.ToString(dr["tel"]);
					times = Convert.ToString(dr["cudate"]);
				}
				dr.Close();
				return name + "$" + tel +"$"+ times;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public void updateOtherInfo(string projcode,string name,string tel,string date)
		{
			try
			{
				string sql = "update other_proj set name = '"+name+"',tel = '"+tel+"',cudate = '"+date+"' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public void updateAllFeedBack(string projcode,string probsource,string content)
		{
			try
			{
				string sql = "";
				if(probsource.Equals("0"))
				{
					sql = "update public_proj set memo = '"+content+"' where projcode = '"+projcode+"'";
				}
				else if(probsource.Equals("2"))
				{
					sql = "";
				}
				else if(probsource.Equals("3"))
				{
					sql = "update web_proj set memo = '"+content+"' where projcode = '"+projcode+"'";
				}
				else
				{
					sql = "update other_proj set memo = '"+ content +"' where projcode = '"+projcode+"'";
				}
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		//根据社区id取社区网格
		public static string getAreaCode(string id)
		{
			try
			{
				string sql = "select commcode from community where id in( '"+id+"')";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					areacode = "3," + Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//根据社区id取社区网格
		public static string getAreaCodes(string id)
		{
			try
			{
				string sql = "select gridcode from grid where commfid in( '"+id+"')";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					areacode = Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//根据街道选Gridcode
		public static string getStreetCodes(string id)
		{
			try
			{
				string sql = "select gridcode from grid where commfid in ( select id from community where fid ='"+id+"')";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					areacode = Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//根据街道选Gridcode
		public static string getStreetCode(string id)
		{
			try
			{
				string sql = "select streetcode from street where id in ('"+id+"')";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					areacode = "2," +Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public static string getAreacode(string id)
		{
			try
			{
				string sql = "select areacode from area where id = '"+id+"'";// commfid in ( select id from community where fid in (select id from street where fid = '"+id+"'))";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					areacode = "1," + Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public static string getAreacodes(string id)
		{
			try
			{
				string sql = "select gridcode from grid where commfid in ( select id from community where fid in (select id from street where fid = '"+id+"'))";
				string areacode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					areacode = Convert.ToString(dr[0]);
				}
				dr.Close();
				return areacode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}
		//查询对应的专业处理部门
		public ArrayList getRolenames(string rolename)
		{
			try
			{
				list = new ArrayList();
				string sql = "select * from role where step=4 and rolename like '%"+rolename+"%'"; //只允许取出专业部门。
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setRolecode(Convert.ToString(dr["rolecode"])+"$"+Convert.ToString(dr["memo"]));
					projInfo.setDepart(Convert.ToString(dr["rolename"]));
					list.Add(projInfo);
				}
				dr.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//单条导出
		public ArrayList getExprotDetail(string projcode)
		{
			string sql = "select a.ispress,a.startdate,a.role,a.gridcode,a.fid,a.isgreat,a.isthrough,b.* from szcg_bak.dbo.b_project a "+
				",szcg_bak.dbo.b_project_detail b where a.ProjCode = b.ProjCode and a.projcode='"+projcode+"'";
			list = getList(sql);
			return list;
		}

        //单条导出
        public ArrayList getExprotDetail(string projcode,string startYear)
        {
            string sql = "select (select top 1 t3=dbo.f_workdayadd(max(b.cu_date),dbo.GetProcessType(b.projcode)) from szcg_bak" + startYear + ".dbo.b_project_trace b with(nolock) where(1=1 and b.CurrentNodeId='8' and b.CurrentBusiStatus='03' "
 + "and b.projcode = '" + projcode + "') group by b.projcode) as dealtime, a.*,b.*,1 as dealtime from szcg_bak" + startYear + ".dbo.b_project a " +
                ",szcg_bak"+startYear+".dbo.b_project_detail b where a.ProjCode = b.ProjCode and a.projcode='" + projcode + "'";
            list = getList(sql);
            return list;
        }

		//更新 lawer_proj IP
		public void updateLawerIP(string projcode,string ip)
		{
			try
			{
				string sql = "update lawer_proj set ip = '"+ip+"' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public void updateWebIP(string projcode,string ip)
		{
			try
			{
				string sql = "update web_proj set ip = '"+ip+"' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public string getTraceOpinion(string projcode)
		{
			try
			{
				string sql = "select _opinion from projtrace where projcode = '"+projcode+"' and returntracetag='1' order by id desc";
				string opinion = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					opinion = Convert.ToString(dr["_opinion"]);
				}
				dr.Close();
				return opinion;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}

		//取案卷流程
		public ArrayList getTrace(string projname)
		{
			try
			{
//				string sql = "select projtrace.*,loginuser.username,b_project.startdate from projtrace,loginuser,b_project where exists ( "+
//					" select * from(select projcode from b_project_detail where projname = '"+projname+"') as tt " +
//					" where projtrace.projcode = tt.projcode and loginuser.usercode = projtrace.usercode and b_project.projcode = projtrace.projcode)";
				string sql = string.Format(@"select IsNULL(B.username,'已删除') as username,C.startdate,A.*
											from projtrace A
											left join loginuser B
											on A.usercode = B.usercode
											left join b_project C
											on A.projcode = C.projcode
											LEFT JOIN b_project_detail D
											ON  A.projcode = D.projcode
											where D.projname = '{0}'",projname);
				string sql3 = "select count(*) from projtrace,b_project_detail where b_project_detail.projcode = projtrace.projcode and projname = '"+projname+"'";
				SqlDataReader dr4 = DataAccess.ExecuteReader(sql3,null);
				int counts = 0;
				if(dr4.Read())
				{
					counts = Convert.ToInt32(dr4[0]);
				}
				dr4.Close();
				list = new ArrayList();
				string stepid = "";
				string stateid = "";
				string sql4="select b_project.stepid,b_project.stateid from b_project,b_project_detail where b_project.projcode = b_project_detail.projcode and b_project_detail.projname = '"+projname+"'";
				SqlDataReader drs1 = DataAccess.ExecuteReader(sql4,null);
				if(drs1.Read())
				{
					stepid = Convert.ToString(drs1["stepid"]);
					stateid = Convert.ToString(drs1["stateid"]);
				}
				drs1.Close();
				if(stepid==""&&stateid=="")
				{
					string sql5="select b_project.stepid,b_project.stateid from szcg_bak.dbo.b_project,szcg_bak.dbo.b_project_detail where b_project.projcode = b_project_detail.projcode and b_project_detail.projname = '"+projname+"'";
					SqlDataReader drs2 = DataAccess.ExecuteReader(sql5,null);
					if(drs2.Read())
					{
						stepid = Convert.ToString(drs2["stepid"]);
						stateid = Convert.ToString(drs2["stateid"]);
					}
					drs2.Close();
				}
				list.Add(getActionName(stepid,stateid));
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setName(Convert.ToString(dr["username"]));
					projInfo.setNum(Convert.ToString(dr["actionname"]));
					projInfo.setOpinion(Convert.ToString(dr["_opinion"]));
					projInfo.setEnddate(Convert.ToString(dr["cu_date"]));
					projInfo.setStartdate(Convert.ToString(dr["startdate"]));

					//根据案卷信息,取专业部门
					if(Convert.ToString(dr["stepid"])=="4")
					{
						string sql1 = "select rolename from role where rolecode = '"+Convert.ToString(dr["roleid"])+"'";
						SqlDataReader dr1 = DataAccess.ExecuteReader(sql1,null);
						if(dr1.Read())
						{
							projInfo.setDepart(Convert.ToString(dr1["rolename"]));
						}
						dr1.Close();
					}
					else if(Convert.ToString(dr["stepid"])=="3")
					{
						string sql2 = "select rolename from role where step = 4 and rolecode = (select role from b_project,b_project_detail where b_project.projcode = b_project_detail.projcode and b_project_detail.projname = '"+projname+"')";
						SqlDataReader dr2 = DataAccess.ExecuteReader(sql2,null);
						if(dr2.Read())
						{
							projInfo.setDepart(Convert.ToString(dr2["rolename"]));
						}
						dr2.Close();
					}
					//根据usercode取它所在的部门
					string sql6 = string.Format(@"select rolename,* from role
                                                  where rolecode = {0}",dr["roleid"]);
					SqlDataReader dr6 = DataAccess.ExecuteReader(sql6,null);
					if(dr6.Read())
					{
						projInfo.setRolecode(Convert.ToString(dr6["rolename"]));
					}
					dr6.Close();
					list.Add(projInfo);
				}
				dr.Close();
				
				if(list.Count==1)
				{
//			 		sql = "select projtrace.*,loginuser.username,b_project.startdate from szcg_bak.dbo.projtrace,loginuser,szcg_bak.dbo.b_project where exists ( "+
//						" select * from(select projcode from szcg_bak.dbo.b_project_detail where projname = '"+projname+"') as tt " +
//						" where szcg_bak.dbo.projtrace.projcode = tt.projcode and loginuser.usercode = szcg_bak.dbo.projtrace.usercode and szcg_bak.dbo.b_project.projcode = szcg_bak.dbo.projtrace.projcode)";
					sql =string.Format(@"select IsNULL(B.username,'已删除') as username,C.startdate,A.*
										from szcg_bak..projtrace A
										left join loginuser B
										on A.usercode = B.usercode
										left join szcg_bak..b_project C
										on A.projcode = C.projcode
										LEFT JOIN szcg_bak..b_project_detail D
										ON  A.projcode = D.projcode
										where D.projname = '{0}'",projname);

					SqlDataReader drs = DataAccess.ExecuteReader(sql,null);
					while(drs.Read())
					{
						projInfo = new ProjInfo();
						projInfo.setName(Convert.ToString(drs["username"]));
						projInfo.setNum(Convert.ToString(drs["actionname"]));
						projInfo.setOpinion(Convert.ToString(drs["_opinion"]));
						projInfo.setEnddate(Convert.ToString(drs["cu_date"]));
						projInfo.setStartdate(Convert.ToString(drs["startdate"]));

						//根据案卷信息,取专业部门
						if(Convert.ToString(drs["stepid"])=="4")
						{
							string sql1 = "select rolename from role where rolecode = '"+Convert.ToString(drs["roleid"])+"'";
							SqlDataReader dr1 = DataAccess.ExecuteReader(sql1,null);
							if(dr1.Read())
							{
								projInfo.setDepart(Convert.ToString(dr1["rolename"]));
							}
							dr1.Close();
						}
						//根据usercode取它所在的部门
						string sql2 = string.Format(@"select rolename,* from role
                                                  where rolecode = {0}",drs["roleid"]);
						SqlDataReader dr2 = DataAccess.ExecuteReader(sql2,null);
						if(dr2.Read())
						{
							projInfo.setRolecode(Convert.ToString(dr2["rolename"]));
						}
						dr2.Close();

						list.Add(projInfo);
					}
					drs.Close();
				}
				
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		//得到部门人员统计数据
		public DataSet getPeoTj(string departid,int page,string startdate,string enddate,out int totalpages,out int totalrows)
		{
			try
			{
				string sql = "select usercode,username from loginuser where departcode = '"+departid+"'";
				string usercode = "";
				DataTable table = new DataTable();
				table.Columns.Add("用户名称");
				table.Columns.Add("操作员办理数");
				table.Columns.Add("值班长办理数");
				table.Columns.Add("指挥中心办理数");
				table.Columns.Add("专业部门办理数");
				//				table.Columns.Add("派核查数");
				//				table.Columns.Add("待结案数");
				//				table.Columns.Add("结案数");
				DataRow row;
				int RowCount;
				int PageCount;
				DataAccess.cutPage(sql,20,out RowCount,out PageCount);
				SqlDataReader dr = DataAccess.getPageData(sql,20,page,"usercode","desc",RowCount,PageCount);
				totalpages = PageCount;
				totalrows = RowCount;
				while(dr.Read())
				{
					row = table.NewRow();
					usercode = Convert.ToString(dr["usercode"]);
					row[0] = Convert.ToString(dr["username"]);
					int count = 0;
					int count1 = 0;
					int count2 = 0;
					int count3 = 0;
					int count4 = 0;
					int count5 = 0;
					int count6 = 0;
					int count7 = 0;
					string sql1="select count(*) from projtrace where usercode = '"+usercode+"' and stepid = '1'";
					string sql2="select count(*) from szcg_bak.dbo.projtrace where usercode = '"+usercode+"' and stepid = '1'";
					string sql3="select count(*) from projtrace where usercode = '"+usercode+"' and stepid = '2'";
					string sql5="select count(*) from projtrace where usercode = '"+usercode+"' and stepid = '3'";
					string sql7="select count(*) from projtrace where usercode = '"+usercode+"' and stepid = '4'";
					string sql4="select count(*) from szcg_bak.dbo.projtrace where usercode = '"+usercode+"' and stepid = '2'";
					string sql6="select count(*) from szcg_bak.dbo.projtrace where usercode = '"+usercode+"' and stepid = '3'";
					string sql8="select count(*) from szcg_bak.dbo.projtrace where usercode = '"+usercode+"' and stepid = '4'";
					if(startdate!=""&&enddate!="")
					{
						string sql0 = " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"' ) ";
						string sqls = " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"' )";
						sql1 += sql0;
						sql3 += sql0;
						sql5 += sql0;
						sql7 += sql0;
						sql2 += sqls;
						sql4 += sqls;
						sql6 += sqls;
						sql8 += sqls;
					}
					else if(startdate!="")
					{
						string sqls1 = " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"')";
						string sqls2 = " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"')";
						sql1 += sqls1;
						sql3 += sqls1;
						sql5 += sqls1;
						sql7 += sqls1;
						sql2 += sqls2;
						sql4 += sqls2;
						sql6 += sqls2;
						sql8 += sqls2;
					}
					else if(enddate!="")
					{
						string sqls3 = " and projcode in (select projcode from b_project where startdate < '"+enddate+" 23:59:59"+"')";
						string sqls4 = " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate < '"+enddate+" 23:59:59"+"')";
						sql1 += sqls3;
						sql3 += sqls3;
						sql5 += sqls3;
						sql7 += sqls3;
						sql2 += sqls4;
						sql4 += sqls4;
						sql6 += sqls4;
						sql8 += sqls4;
					}
					SqlDataReader rs = DataAccess.ExecuteReader(sql1,null);
					if(rs.Read())
					{
						count = Convert.ToInt32(rs[0]);
					}
					rs.Close();
					SqlDataReader rs1 = DataAccess.ExecuteReader(sql2,null);
					if(rs1.Read())
					{
						count1 = Convert.ToInt32(rs1[0]);
					}
					rs1.Close();
					
					SqlDataReader rs2 = DataAccess.ExecuteReader(sql3,null);
					if(rs2.Read())
					{
						count2 = Convert.ToInt32(rs2[0]);
					}
					rs2.Close();
					SqlDataReader rs3 = DataAccess.ExecuteReader(sql4,null);
					if(rs3.Read())
					{
						count3 = Convert.ToInt32(rs3[0]);
					}
					rs3.Close();
					
					SqlDataReader rs4 = DataAccess.ExecuteReader(sql5,null);
					if(rs4.Read())
					{
						count4 = Convert.ToInt32(rs4[0]);
					}
					rs4.Close();
					SqlDataReader rs5 = DataAccess.ExecuteReader(sql6,null);
					if(rs5.Read())
					{
						count5 = Convert.ToInt32(rs5[0]);
					}
					rs5.Close();
					
					SqlDataReader rs6 = DataAccess.ExecuteReader(sql7,null);
					if(rs6.Read())
					{
						count6 = Convert.ToInt32(rs6[0]);
					}
					rs6.Close();
					SqlDataReader rs7 = DataAccess.ExecuteReader(sql8,null);
					if(rs7.Read())
					{
						count7 = Convert.ToInt32(rs7[0]);
					}
					rs7.Close();
					row[1] = Convert.ToString(count+count1);
					row[2] = Convert.ToString(count2+count3);
					row[3] = Convert.ToString(count4+count5);
					row[4] = Convert.ToString(count6+count7);
					table.Rows.Add(row);
				}
				dr.Close();
				DataSet ds = new DataSet();
				ds.Tables.Add(table);
				return ds;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				totalpages = 0;
				totalrows = 0;
				return null;
			}
		}

		public DataSet getColTj(string areacode,int page,string startdate,string enddate,out int totalpages,out int totalrows)
		{
			try
			{
				string sql = "select collcode,collname,mobile from collecter where gridcode like '"+areacode+"%'";
				//string counts = "";
				//list = new ArrayList();
				int RowCount;
				int PageCount;
				DataAccess.cutPage(sql,19,out RowCount,out PageCount);
				SqlDataReader dr = DataAccess.getPageData(sql,19,page,"collcode","desc",RowCount,PageCount);
				totalpages = PageCount;
				totalrows = RowCount;
				DataTable table = new DataTable();
				table.Columns.Add("监督员姓名");
				table.Columns.Add("手机号码");
				table.Columns.Add("上报总数");
				table.Columns.Add("核查总数");

				int counts = 0;
				int counts1 = 0;
				DataRow row;
				
				while(dr.Read())
				{
					row = table.NewRow();
					//projInfo = new ProjInfo();
					//projInfo.setName(Convert.ToString(dr["collname"]));
					row[0] = Convert.ToString(dr["collname"]);
					row[1] = Convert.ToString(dr["mobile"]);
					string sql1 = "select count(*) from b_pdamsg where (collcode = '"+Convert.ToString(dr["collcode"])+"') or (memo='"+Convert.ToString(dr["collcode"])+"' and state = 1)";
					string sql2 = "select count(*) from szcg_bak.dbo.b_pdamsg where (collcode = '"+Convert.ToString(dr["collcode"])+"' and memo = '') or memo = '"+Convert.ToString(dr["collcode"])+"'";
					string sql3 = "select count(*) from b_pdamsg where collcode = '"+Convert.ToString(dr["collcode"])+"' and memo <> ''";
					string sql4 = "select count(*) from szcg_bak.dbo.b_pdamsg where collcode = '"+Convert.ToString(dr["collcode"])+"'";
					if(startdate!="" && enddate!="")
					{
						sql1 += " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"')";
						sql2 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"')";
						sql3 += " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"')";
						sql4 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"' and startdate < '"+enddate+" 23:59:59"+"')";
					}
					else if(startdate!="")
					{
						sql1 += " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"')";
						sql2 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"')";
						sql3 += " and projcode in (select projcode from b_project where startdate > '"+startdate+" 00:00:00"+"')";
						sql4 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate > '"+startdate+" 00:00:00"+"')";
					}
					else if(enddate!="")
					{
						sql1 += " and projcode in (select projcode from b_project where startdate < '"+enddate+" 23:59:59"+"')";
						sql2 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate < '"+enddate+" 23:59:59"+"')";
						sql3 += " and projcode in (select projcode from b_project where startdate < '"+enddate+" 23:59:59"+"')";
						sql4 += " and projcode in (select projcode from szcg_bak.dbo.b_project where startdate < '"+enddate+" 23:59:59"+"')";
					}
					int count = 0;
					int count1 = 0;
					int count2 = 0;
					int count3 = 0;
					SqlDataReader rs = DataAccess.ExecuteReader(sql1,null);
					if(rs.Read())
					{
						count = Convert.ToInt32(rs[0]);
					}
					rs.Close();
					SqlDataReader rs1 = DataAccess.ExecuteReader(sql2,null);
					if(rs1.Read())
					{
						count1 = Convert.ToInt32(rs1[0]);
					}
					rs1.Close();
					
					SqlDataReader rs2 = DataAccess.ExecuteReader(sql3,null);
					if(rs2.Read())
					{
						count2 = Convert.ToInt32(rs2[0]);
					}
					rs2.Close();
					SqlDataReader rs3 = DataAccess.ExecuteReader(sql4,null);
					if(rs3.Read())
					{
						count3 = Convert.ToInt32(rs3[0]);
					}
					rs3.Close();
					row[2] = Convert.ToString(count+count1);
					row[3] = Convert.ToString(count2+count3);
					counts += count+count1;
					counts1 += count2+count3;

					table.Rows.Add(row);
					//projInfo.setNum(count);
					//list.Add(projInfo);
				}
				dr.Close();

				row = table.NewRow();
				row[0] = "";
				row[1] = "";
				row[2] = Convert.ToString(counts);
				row[3] = Convert.ToString(counts1);
				table.Rows.Add(row);

				DataSet ds = new DataSet();
				ds.Tables.Add(table);
				return ds;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				totalpages = 0;
				totalrows = 0;
				return null;
			}
		}

		public string getProjCode(string projname)
		{
			try
			{
				string sql = "select projcode from b_project_detail where projname = '"+projname+"' union select projcode from szcg_bak..b_project_detail where projname = '"+projname+"'";
				string projcode = "";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				if(dr.Read())
				{
					projcode = Convert.ToString(dr["projcode"]);
				}
				dr.Close();
				return projcode;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}
		//部件事件统计
		public DataSet getPartEvent(string values,string startdate,string enddate,int page,out int totalpages,out int totalrows)
		{
			try
			{
				string sql = "";
				//string sql1 = "";
				int RowCount;
				int PageCount;
				DataTable table = new DataTable();
				DataRow row;
				if(values=="1")
				{
					sql = "select id,name from smallclass_part";
					
				}
				else
				{
					sql = "select id,name from smallclass_event";
				}
				list = new ArrayList();
				string sql1 = getSql(values,startdate,enddate);
				SqlDataReader drs = DataAccess.ExecuteReader(sql1,null);
				while(drs.Read())
				{
					list.Add(Convert.ToString(drs["areacode"])+";"+Convert.ToString(drs["id"])+";" +Convert.ToString(drs["num"]));
				}
				drs.Close();
				DataAccess.cutPage(sql,20,out RowCount,out PageCount);
				SqlDataReader dr = DataAccess.getPageData(sql,20,page,"id","desc",RowCount,PageCount);
				//int i=0;
				totalpages = PageCount;
				totalrows = RowCount;
				
				table.Columns.Add("类别");
				table.Columns.Add("罗湖区");
				table.Columns.Add("福田区");
				table.Columns.Add("南山区");
				table.Columns.Add("盐田区");
				table.Columns.Add("宝安区");
				table.Columns.Add("龙岗区");
				table.Columns.Add("市城管局");
				table.Columns.Add("合计");
				while(dr.Read())
				{
					row = table.NewRow();
					
					row[0] = Convert.ToString(dr["name"]);
					int count = 0;
					for(int i=0; i<list.Count; i++)
					{
						string[] strs = list[i].ToString().Split(';');
						if(strs[0].Equals("4403030") && strs[1].Equals(Convert.ToString(dr["id"])))
						{
							row[1] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0].Equals("4403040") && strs[1].Equals(Convert.ToString(dr["id"])))
						{
							row[2] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403050" && strs[1] == Convert.ToString(dr["id"]))
						{
							row[3] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403080" && strs[1] == Convert.ToString(dr["id"]))
						{
							row[4] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="3211020" && strs[1] == Convert.ToString(dr["id"]))
						{
							row[5] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403070" && strs[1] == Convert.ToString(dr["id"]))
						{
							row[6] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
						else if(strs[0].Equals("4403") && strs[1].Equals(Convert.ToString(dr["id"])))
						{
							row[7] = strs[2];
							count += Convert.ToInt32(strs[2]);
						}
					}
					if(count==0)
					{
						row[8] = "";
					}
					else
					{
						row[8] = Convert.ToString(count);
					}
					table.Rows.Add(row);
				}

				if(page == PageCount)
				{
					int counts1 = 0;
					int counts2 =0;
					int counts3 =0;
					int counts4 =0;
					int counts5 =0;
					int counts6 =0;
					int counts7 =0;
					for(int j = 0; j<list.Count; j++)
					{
						string[] strs = list[j].ToString().Split(';');
						if(strs[0]=="4403030")
						{
							counts1 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403040")
						{
							counts2 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403050")
						{
							counts3 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403080")
						{
							counts4 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="3211020")
						{
							counts5 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0]=="4403070")
						{
							counts6 += Convert.ToInt32(strs[2]);
						}
						else if(strs[0].Equals("4403"))
						{
							counts7 += Convert.ToInt32(strs[2]);
						}
					}
					row = table.NewRow();
					row[0] = "累计";
					string ret1 =  Convert.ToString(counts1);
					if(ret1=="0")
					{
						ret1 = "";
					}
					string ret2 =  Convert.ToString(counts2);
					if(ret2=="0")
					{
						ret2 = "";
					}
					string ret3 =  Convert.ToString(counts3);
					if(ret3=="0")
					{
						ret3 = "";
					}
					string ret4 =  Convert.ToString(counts4);
					if(ret4=="0")
					{
						ret4 = "";
					}
					string ret5 =  Convert.ToString(counts5);
					if(ret5=="0")
					{
						ret5 = "";
					}
					string ret6 =  Convert.ToString(counts6);
					if(ret6=="0")
					{
						ret6 = "";
					}
					string ret7 =  Convert.ToString(counts7);
					if(ret7=="0")
					{
						ret7 = "";
					}
					row[1] = ret1;
					row[2] = ret2;
					row[3] = ret3;
					row[4] = ret4;
					row[5] = ret5;
					row[6] = ret6;
					row[7] = ret7;
					row[8] =Convert.ToString(Convert.ToInt32(counts1)+Convert.ToInt32(counts2)+Convert.ToInt32(counts3)
						+Convert.ToInt32(counts4)+Convert.ToInt32(counts5)+Convert.ToInt32(counts6)+Convert.ToInt32(counts7));
					table.Rows.Add(row);
				}
				dr.Close();			
				DataSet ds = new DataSet();
				ds.Tables.Add(table);
				return ds;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				totalpages = 0;
				totalrows = 0;
				return null;
			}
		}

		public string getSql(string values,string startdate,string enddate)
		{
			string sql = "";
			string times = " and startdate > '"+startdate+" 00:00:00' ";
			string times1 = " and startdate < '"+enddate+" 23:59:59' ";
			if(values=="1")
			{
				sql = " select tt.areacode,tt.id,tt.name, sum(num) as num from ( "+
					" select substring(b_project.gridcode,0,8) as areacode ,smallclass_part.id,smallclass_part.name, "+
					" count(b_project_detail.projcode) as num "+
					" from  smallclass_part , b_project_detail, b_project ,area "+
					" where b_project_detail.projcode = b_project.projcode and b_project.isdel <> 1 and stepid <> '0' "+
					" and smallclass_part.id = b_project_detail.smallclass and probclass = '0' ";

				if(startdate!="")
				{
					sql += times;
				}
				if(enddate!="")
				{
					sql += times1;
				}
				sql+= " and substring(b_project.gridcode,0,8) =   convert(char(8),area.areacode) "+
					" and (len(gridcode) = 14 or len(gridcode) = 4) "+
					" group by smallclass_part.id,smallclass_part.name ,substring(b_project.gridcode,0,8) "+
					" union all "+
					" select substring(b_project.gridcode,0,8) as areacode ,smallclass_part.id,smallclass_part.name, "+
					" count(b_project_detail.projcode) as num "+
					" from  smallclass_part , szcg_bak.dbo.b_project_detail, szcg_bak.dbo.b_project ,area "+
					" where b_project_detail.projcode = b_project.projcode and b_project.isdel <> 1 and stepid <> '0' "+
					" and smallclass_part.id = b_project_detail.smallclass and probclass = '0' ";
				if(startdate!="")
				{
					sql += times;
				}
				if(enddate!="")
				{
					sql += times1;
				}
				sql+=" and substring(b_project.gridcode,0,8) =   convert(char(8),area.areacode) "+
					" and (len(gridcode) = 14 or len(gridcode) = 4) "+
					" group by smallclass_part.id,smallclass_part.name ,substring(b_project.gridcode,0,8) "+
					" ) as tt group by tt.id,tt.name,tt.areacode";
			}
			else
			{
				sql = " select tt.areacode,tt.id,tt.name, sum(num) as num from ( "+
					" select substring(b_project.gridcode,0,8) as areacode ,smallclass_event.id,smallclass_event.name, "+
					" count(b_project_detail.projcode) as num "+
					" from  smallclass_event , b_project_detail, b_project ,area "+
					" where b_project_detail.projcode = b_project.projcode and b_project.isdel <> 1 and stepid <> '0' "+
					" and smallclass_event.id = b_project_detail.smallclass and probclass = '1' ";

				if(startdate!="")
				{
					sql += times;
				}
				if(enddate!="")
				{
					sql += times1;
				}
				sql+= " and substring(b_project.gridcode,0,8) =   convert(char(8),area.areacode) "+
					" and (len(gridcode) = 14 or len(gridcode) = 4) "+
					" group by smallclass_event.id,smallclass_event.name ,substring(b_project.gridcode,0,8) "+
					" union all "+
					" select substring(b_project.gridcode,0,8) as areacode ,smallclass_event.id,smallclass_event.name, "+
					" count(b_project_detail.projcode) as num "+
					" from  smallclass_event , szcg_bak.dbo.b_project_detail, szcg_bak.dbo.b_project ,area "+
					" where b_project_detail.projcode = b_project.projcode and b_project.isdel <> 1 and stepid <> '0' "+
					" and smallclass_event.id = b_project_detail.smallclass and probclass = '1' ";
				if(startdate!="")
				{
					sql += times;
				}
				if(enddate!="")
				{
					sql += times1;
				}
				sql+=" and substring(b_project.gridcode,0,8) =   convert(char(8),area.areacode) "+
					" and (len(gridcode) = 14 or len(gridcode) = 4) "+
					" group by smallclass_event.id,smallclass_event.name ,substring(b_project.gridcode,0,8) "+
					" ) as tt group by tt.id,tt.name,tt.areacode";
			}
			//			string counts = "";
			//			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			//			if(dr.Read())
			//			{
			//				counts = Convert.ToString(dr["num"]);
			//			}
			//			dr.Close();
			return sql;
		}

		//查询箱
		public ArrayList QueryProject(string probsource,string state,string probclass,string bigclass,string smallclass,string street,string square,string times1,string times2,string projcode,string address,string areacode,int page,string collcode,string area,int totalpages,int totalRows)
		{
			try
			{
				SqlParameter [] input = new SqlParameter[14];
				input[0] = new SqlParameter("@probsource", System.Data.SqlDbType.VarChar,18);
				input[0].Direction = ParameterDirection.Input;
				input[0].Value = probsource;
				input[1] = new SqlParameter("@state", System.Data.SqlDbType.VarChar,18);
				input[1].Direction = ParameterDirection.Input;
				input[1].Value = state;
				input[2] = new SqlParameter("@probclass", System.Data.SqlDbType.VarChar,18);
				input[2].Direction = ParameterDirection.Input;
				input[2].Value = probclass;
				input[3] = new SqlParameter("@bigclass", System.Data.SqlDbType.VarChar,18);
				input[3].Direction = ParameterDirection.Input;
				input[3].Value = bigclass;
				input[4] = new SqlParameter("@smallclass", System.Data.SqlDbType.VarChar,18);
				input[4].Direction = ParameterDirection.Input;
				input[4].Value = smallclass;
				input[5] = new SqlParameter("@street", System.Data.SqlDbType.VarChar,512);
				input[5].Direction = ParameterDirection.Input;
				input[5].Value = street;
				input[6] = new SqlParameter("@square", System.Data.SqlDbType.VarChar,1024);
				input[6].Direction = ParameterDirection.Input;
				input[6].Value = square;
				input[7] = new SqlParameter("@times1", System.Data.SqlDbType.VarChar,64);
				input[7].Direction = ParameterDirection.Input;
				input[7].Value = times1;
				input[8] = new SqlParameter("@times2", System.Data.SqlDbType.VarChar,32);
				input[8].Direction = ParameterDirection.Input;
				input[8].Value = times2;
				input[9] = new SqlParameter("@projcode", System.Data.SqlDbType.VarChar,32);
				input[9].Direction = ParameterDirection.Input;
				input[9].Value = projcode;
				input[10] = new SqlParameter("@address", System.Data.SqlDbType.VarChar,32);
				input[10].Direction = ParameterDirection.Input;
				input[10].Value = address;
				input[11] = new SqlParameter("@areacode", System.Data.SqlDbType.VarChar,32);
				input[11].Direction = ParameterDirection.Input;
				input[11].Value = areacode;
				input[12] = new SqlParameter("@collcode", System.Data.SqlDbType.VarChar,10);
				input[12].Direction = ParameterDirection.Input;
				input[12].Value = collcode;
				input[13] = new SqlParameter("@area", System.Data.SqlDbType.VarChar,10);
				input[13].Direction = ParameterDirection.Input;
				input[13].Value = area;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@sqls",System.Data.SqlDbType.VarChar,4000);
				output[0].Direction = ParameterDirection.Output;

				list = new ArrayList();
				string sql = DataAccess.ExecuteStoreProcedure1("QueryProject",input,output);//.ExecuteStoredProcedure2("QueryProject",input);
				int RowCount = totalRows;
				int PageCount = totalpages;
				if(RowCount==0)
				{
					DataAccess.cutPage(sql,12,out RowCount ,out PageCount);
				}
				rs = DataAccess.getPageData(sql,12,page,"projcode","desc",RowCount,PageCount);
				if(rs==null)
				{
					return null;
				}
				list.Add(Convert.ToString(PageCount));
				while(rs.Read())
				{
					projInfo = new ProjInfo();
					projInfo.setAllrow(RowCount);//添加总地记录数
					projInfo.setProjcode(Convert.ToInt32(rs["projCode"]));
					projInfo.setProjname(Convert.ToString(rs["projname"]));
					projInfo.setProbsource(Convert.ToString(rs["probsource"]));
					projInfo.setProbclass(Convert.ToString(rs["probclass"]));
					projInfo.setBigclass(Convert.ToString(rs["bigclass"]));
					projInfo.setSmallclass(Convert.ToString(rs["smallclass"]));
					projInfo.setStartdate(Convert.ToString(rs["startdate"]));
					projInfo.setStreet(Convert.ToString(rs["street"]));
					projInfo.setSquare(Convert.ToString(rs["square"]));
					projInfo.setArea(Convert.ToString(rs["area"]));
					string tempsql = "";
					if(Convert.ToString(rs["probclass"]).Equals("0"))
					{
						tempsql = "select bigclass_part.id as bid, bigclass_part.name as bigclassname , smallclass_part.id as sid, smallclass_part.name as smallname, smallclass_part.t_time from bigclass_part, smallclass_part where bigclass_part.id = smallclass_part.fid and  smallclass_part.id= '"+Convert.ToString(rs["smallclass"])+"'";
					}
					else
					{
						tempsql = "select bigclass_event.id as bid, bigclass_event.name as bigclassname , smallclass_event.id as sid, smallclass_event.name as smallname, smallclass_event.t_time from bigclass_event, smallclass_event where bigclass_event.id = smallclass_event.fid and  smallclass_event.id= '"+Convert.ToString(rs["smallclass"])+"'";
					}
					SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
					if(temp_dr.Read())
					{
						projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
						projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
					}
					temp_dr.Close();
					projInfo.setAddress(Convert.ToString(rs["address"]));
					projInfo.setProbdesc(Convert.ToString(rs["probdesc"]));
					list.Add(projInfo);
				}
				rs.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public ArrayList getYjProject(string usercode,int page)
		{
			string sql = "SELECT  a.startdate,a.stepid,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where exists ( " +
				" select distinct  projcode from projtrace where usercode = '"+usercode+"' and a.projcode = projtrace.projcode and (a.isdel = 0 or isdel is null))";
			list = getGridProject(sql,page);
			return list;
		}

//		public ArrayList getDepartProject(string rolecode,string state,int page,string startdate,string enddate)
//		{
//			string sql = "";
//			if(state=="0")
//			{
//				sql = "select b_project.startdate,b_project_detail.* from b_project,b_project_detail "+
//					" where b_project_detail.projcode = b_project.projcode and b_project.role = '"+rolecode+"'";
//				if(startdate!="")
//				{
//					sql += " and b_project.startdate > '"+startdate+" 00:00:00"+"' ";;
//				}
//				if(enddate != "")
//				{
//					sql += " and b_project.startdate < '"+enddate+" 23:59:59"+"'";
//				}
//			}
//			else if(state=="1")
//			{
//				sql = "select b_project.startdate,b_project_detail.* from szcg_bak..b_project,szcg_bak..b_project_detail "+
//					" where exists ( select * from (select projcode from szcg_bak..projtrace where roleid = '"+rolecode+"' and returntracetag is null) as tt "+
//					" where tt.projcode = b_project.projcode and b_project.projcode = b_project_detail.projcode ";
//				if(startdate!="")
//				{
//					sql += " and b_project.startdate > '"+startdate+" 00:00:00"+"'";
//				}
//				if(enddate != "")
//				{
//					sql += " and b_project.startdate < '"+enddate+" 23:59:59"+"'";
//				}
//				sql += ")";
//			}
//			list = getGridProject(sql,page);
//			
//			return list;
//		}

		//得到专业部门的手机号
		public string getPhoneDepart(string rolecode)
		{
			string phones = "";
			if(rolecode!="")
			{
				string sql = "select memo from role where rolecode = '"+rolecode+"'";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				
				if(dr.Read())
				{
					phones = Convert.ToString(dr["memo"]);
				}
				dr.Close();
			}
			return phones;
		}

		public ArrayList getDbProject(string areacode,int page)
		{
			string sql ="";
			if(areacode=="4403")
			{
				sql = "SELECT distinct a.startdate,a.stepid,a.ispress,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where ispress='1' and isgreat = '1'";
			}
			else
			{
				sql ="SELECT distinct a.startdate,a.stepid,a.ispress,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where ispress='1' and a.isdel='0' and (isgreat = '0' or isgreat is null) and substring(a.gridcode,0,8)='"+areacode+"'";
			}
			list = getGridProject(sql,page);
			return list;
		}


		public ArrayList getGridProject(string sql,int page)
		{
			int RowCount;
			int PageCount;
			DataAccess.cutPage(sql,15,out RowCount,out PageCount);
			list = new ArrayList();
			SqlDataReader rs = DataAccess.getPageData(sql,15,page,"projcode","desc",RowCount,PageCount);
			list.Add(Convert.ToString(PageCount));
			while(rs.Read())
			{
				projInfo = new ProjInfo();
				projInfo.setProjcode(Convert.ToInt32(rs["projCode"]));
				projInfo.setProjname(Convert.ToString(rs["projname"]));
				projInfo.setProbsource(Convert.ToString(rs["probsource"]));
				projInfo.setProbclass(Convert.ToString(rs["probclass"]));
				projInfo.setBigclass(Convert.ToString(rs["bigclass"]));
				projInfo.setSmallclass(Convert.ToString(rs["smallclass"]));
				projInfo.setStartdate(Convert.ToString(rs["startdate"]));
				projInfo.setStreet(Convert.ToString(rs["street"]));
				projInfo.setSquare(Convert.ToString(rs["square"]));
				projInfo.setArea(Convert.ToString(rs["area"]));
				string tempsql = "";
				if(Convert.ToString(rs["probclass"]).Equals("0"))
				{
					tempsql = "select bigclass_part.id as bid, bigclass_part.name as bigclassname , smallclass_part.id as sid, smallclass_part.name as smallname, smallclass_part.t_time from bigclass_part, smallclass_part where bigclass_part.id = smallclass_part.fid and  smallclass_part.id= '"+Convert.ToString(rs["smallclass"])+"'";
				}
				else
				{
					tempsql = "select bigclass_event.id as bid, bigclass_event.name as bigclassname , smallclass_event.id as sid, smallclass_event.name as smallname, smallclass_event.t_time from bigclass_event, smallclass_event where bigclass_event.id = smallclass_event.fid and  smallclass_event.id= '"+Convert.ToString(rs["smallclass"])+"'";
				}
				SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
				if(temp_dr.Read())
				{
					projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
					projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
				}
				temp_dr.Close();
				projInfo.setAddress(Convert.ToString(rs["address"]));
				projInfo.setProbdesc(Convert.ToString(rs["probdesc"]));
				list.Add(projInfo);
			}
			rs.Close();
			return list;
		}
		public string getGroupId(string areacode)
		{
			if(areacode == "4403")
			{
				return "1";
			}
			else if(areacode == "4403030")
			{
				return "2";
			}
			else if(areacode == "4403040")
			{
				return "3";
			}
			else if(areacode == "4403050")
			{
				return "4";
			}
			else if(areacode == "3211020")
			{
				return "6";
			}
			else if(areacode == "4403070")
			{
				return "7";
			}
			else if(areacode == "4403080")
			{
				return "5";
			}
			else 
			{
				return "0";
			}
		}

		public string getActionName(string stepid,string stateid)
		{
			string actionname = "";
			if(stepid=="1")
			{
				if(stateid=="3")
				{
					actionname = "监督员核查阶段";
				}
				else if(stateid=="1")
				{
					actionname = "操作员批转阶段";
				}
			}
			else if(stepid=="2")
			{
				if(stateid=="1")
				{
					actionname = "值班长立案阶段";
				}
				else if(stateid=="4")
				{
					actionname = "值班长待结案阶段";
				}
			}
			else if(stepid=="3")
			{
				if(stateid=="1")
				{
					actionname = "派遣阶段";
				}
				else if(stateid=="2")
				{
					actionname = "派遣核查阶段";
				}
			}
			else if(stepid=="4")
			{
				actionname = "专业部门处理阶段";
			}
			else if(stepid=="5")
			{
				actionname = "处理完毕";
			}
			return actionname;
		}
		

		
		public ArrayList getFeedContent(string fid)
		{
			try
			{
				ArrayList list = new ArrayList();
				string sql = "";
                sql = "select * from b_opinion_feedback where fid = '" + fid + "' order by id desc";
				SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
				string contents = "";
				string usernames = "";
				string datanow = "";
				string optionid = "";
				while(dr.Read())
				{
					contents = Convert.ToString(dr["content"])+"#";
					datanow = Convert.ToString(dr["cudate"])+"#";
                    string sqls = "select username from p_user where usercode = '" + Convert.ToInt32(dr["usercode"]) + "'";
					SqlDataReader rs = DataAccess.ExecuteReader(sqls,null);
					if(rs.Read())
					{
						usernames = Convert.ToString(rs["username"])+"#";
					}
					optionid = Convert.ToString(dr["id"]);
					rs.Close();
					list.Add(contents+datanow+usernames+optionid);
				}
				dr.Close();
				return list;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
		}

		public ArrayList getStepInfo(string argStepId,string areacode,int page)
		{
			string strSql = "";
			
			//UserInfo userinfo = (UserInfo)Session["userinfo"];
			//string areacode = userinfo.getAreacode();
			strSql= "SELECT b_project.stepid, b_project.stateid, b_project.role, b_project.typecode, " +
				"b_project.startdate, b_project.gridcode, b_project.fid,b_project_detail.* FROM b_project,b_project_detail WHERE b_project.isdel=0 and b_project.projcode = b_project_detail.projcode "+
				"AND b_project.stepid = '" + argStepId + "'";
			if(!areacode.Equals("4403")&&areacode!=null)
			{	
				strSql = strSql+" and substring(gridcode,0,8)='"+areacode+"' and (isgreat is null or isgreat = '0')";//.Replace("%",areacode);
			}
			else
			{
				strSql = strSql+" and isgreat = '1'";
			}
			//			}
			int RowCount;
			int PageCount;
			list = new ArrayList();
			DataAccess.cutPage(strSql,15,out RowCount ,out PageCount );
			SqlDataReader rs = com.teamax.util.DataAccess.getPageData(strSql,15,page,"projcode","desc",RowCount,PageCount);
			list.Add(Convert.ToString(PageCount));
			if(rs==null)
			{			
				return null;
			}
			while(rs.Read())
			{
				projInfo = new ProjInfo();
				projInfo.setAllrow(RowCount);
				projInfo.setProjcode(Convert.ToInt32(rs["projCode"]));
				projInfo.setProjname(Convert.ToString(rs["projname"]));
				projInfo.setProbsource(Convert.ToString(rs["probsource"]));
				projInfo.setProbclass(Convert.ToString(rs["probclass"]));
				projInfo.setBigclass(Convert.ToString(rs["bigclass"]));
				projInfo.setSmallclass(Convert.ToString(rs["smallclass"]));
				projInfo.setStartdate(Convert.ToString(rs["startdate"]));
				projInfo.setFid(Convert.ToString(rs["fid"]));
				projInfo.setStreet(Convert.ToString(rs["street"]));
				projInfo.setSquare(Convert.ToString(rs["square"]));
				projInfo.setArea(Convert.ToString(rs["area"]));
				string tempsql = "";
				if(Convert.ToString(rs["probclass"]).Equals("0"))
				{
					tempsql = "select bigclass_part.id as bid, bigclass_part.name as bigclassname , smallclass_part.id as sid, smallclass_part.name as smallname, smallclass_part.t_time from bigclass_part, smallclass_part where bigclass_part.id = smallclass_part.fid and  smallclass_part.id= '"+Convert.ToString(rs["smallclass"])+"'";
				}
				else
				{
					tempsql = "select bigclass_event.id as bid, bigclass_event.name as bigclassname , smallclass_event.id as sid, smallclass_event.name as smallname, smallclass_event.t_time from bigclass_event, smallclass_event where bigclass_event.id = smallclass_event.fid and  smallclass_event.id= '"+Convert.ToString(rs["smallclass"])+"'";
				}
				SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql,null);
				if(temp_dr.Read())
				{
					projInfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
					projInfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
				}
				temp_dr.Close();
				string tempsql1="select username from public_proj where projcode='"+Convert.ToInt32(rs["projCode"])+"'";
				SqlDataReader temp_dr1=com.teamax.util.DataAccess.ExecuteReader(tempsql1,null);
				if(temp_dr1.Read())
				{
					projInfo.setReceiver(System.Convert.ToString(temp_dr1["username"]));
				}
				temp_dr1.Close();
				projInfo.setGridcode(Convert.ToString(rs["gridcode"]));
				projInfo.setAddress(Convert.ToString(rs["address"]));
				projInfo.setProbdesc(Convert.ToString(rs["probdesc"]));
				list.Add(projInfo);
			}
			rs.Close();
			return list;
		}
		/// <summary>
		/// 获取单个督办案件的案件信息
		/// </summary>
		/// <param name="projcode">案件编号</param>
		/// <returns></returns>
		public DataSet getDBProjectInfo(string projcode)
		{
			string strSql=string.Format(@"	
					select top 1 c.title as dbname,c.content as dbcontent,a.startdate,
						a.isgreat,a.gridcode as cell,
						b.area,b.street,b.square,b.address,
						b.probdesc,b.probsource,
						b.bigclass as bigid,
						b.smallclass as smallid,
						b.area+'/'+b.street+'/'+b.square as streets,
						case when b.probclass='0' then '部件' else '事件' end  as probclass,
						dbo.GetPartEventName(b.probclass,1,b.bigclass) as bigclass,
						dbo.GetPartEventName(b.probclass,0,b.smallclass) as smallclass
					from b_project a ,b_project_detail b,inspect c
					where a.projcode = b.projcode 
							and a.projcode = c.projcode 
							and a.projcode = {0}",projcode);
			return com.teamax.util.DataAccess.ExecuteDataSet(strSql);
		}
	}
}
