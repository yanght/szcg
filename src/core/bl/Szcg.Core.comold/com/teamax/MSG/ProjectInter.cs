using System;
using AjaxPro;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Data;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;
using System.Text;

namespace szcg.com.teamax.msg
{
	/// <summary>
	/// ProjectInter 的摘要说明。
	/// </summary>
	public class ProjectInter
	{
		public ProjectInter()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		[AjaxPro.AjaxMethod]
		public string getProjIsaction(string projcode)
		{
			string result = "";
			SqlDataReader rs = null;
			try
			{
				string projcodes = "";
				string sql = "select istransaction from b_project where projcode = '"+projcode+"'";
				rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
				if(rs.Read())
				{
					projcodes = Convert.ToString(rs["istransaction"]);
				}
				if(rs != null)
					rs.Close();
				if(projcodes != null && projcodes.Equals("1"))
				{
					result = "1";
				}
				else
				{
					string sqls = "update b_project set istransaction = '1' where projcode = '"+projcode+"'";
					com.teamax.util.DataAccess.ExecuteNonQuery(sqls,null);
					result = "0";
				}
			}
			catch(Exception e)
			{
				if(rs != null)
					rs.Close();

				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return result;
		}

		//PDA消息更新图片
		[AjaxPro.AjaxMethod]
		public string getMsgInfo(string pages)
		{
			string sqlpda = "select a.*,b.collname,c.projname   FROM  b_pdamsg As a,collecter AS b,b_project_detail AS c  where (a.ioflag = '2' or a.ioflag = '3') and a.collcode=b.collcode and a.projcode=c.projcode";
			int page = Convert.ToInt32(pages);
			int RowCount;
			int PageCount;
			string pdainfo="";
			com.teamax.util.DataAccess.cutPage(sqlpda,15,out RowCount,out PageCount);
			SqlDataReader rs = com.teamax.util.DataAccess.getPageData(sqlpda,15,page,"id","desc",RowCount,PageCount);
			while(rs.Read())
			{
				pdainfo += Convert.ToString(rs["projname"]) + "#" + Convert.ToString(rs["ioflag"]) + "$";
			}
			if(rs != null)
				rs.Close();
			string projnames="";
			if(pdainfo!="")
			{
                projnames = bacgBL.Pub.Tools.StrConv(pdainfo.Substring(0, pdainfo.Length - 1), "GB2312");
			}
			return projnames;
		}

		[AjaxPro.AjaxMethod]
		public string getMyMsgInfo(string usercode,string pages)
		{
			string sql = "SELECT a.*,b.username,c.departname  FROM b_business_msg AS a, loginuser AS b, depart AS c "
				+"WHERE a.go_user = b.usercode and b.departcode=c.departcode and a.to_user='"+usercode+"'";
			int page = Convert.ToInt32(pages);
			int RowCount;
			int PageCount;
			string pdainfo = "";
			com.teamax.util.DataAccess.cutPage(sql,15,out RowCount,out PageCount);
			SqlDataReader rs = com.teamax.util.DataAccess.getPageData(sql,15,page,"id","desc",RowCount,PageCount);
			while(rs.Read())
			{
				pdainfo += Convert.ToString(rs["cu_date"]) + "#" + Convert.ToString(rs["isread"]) + "$";
			}
			if(rs != null)
				rs.Close();
			string projnames="";
			if(pdainfo!="")
			{
                projnames = bacgBL.Pub.Tools.StrConv(pdainfo.Substring(0, pdainfo.Length - 1), "GB2312");
			}
			return projnames;
		}

		[AjaxPro.AjaxMethod]
		public string getProjGreat(string projcode,string usercode,string rolecode)
		{
			try
			{
				//1 标识重大案件 2 标识市处理
				if(usercode=="")
					usercode = "0";
				if(rolecode=="")
					rolecode = "0";
				string sql = string.Format(@"	update b_project 
												set isgreat = '1' 
												where projcode = {0} ; 
												insert into projtrace(projcode,stepid,actionname,
                                                             cu_date,usercode,_opinion,
                                                             returntracetag,roleid)
                                                       values({1},'2','重大案件批转',
                                                          GetDate(),{2},'案件由区批转到市局的值班长',
                                                          '',{3})",projcode,projcode,usercode,rolecode);
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "-1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "-3";
			}
		}

		[AjaxPro.AjaxMethod]
		public string getProjcode(string step,string state,string areacode,string pages,string usercode)
		{
			SqlDataReader rs = null;
			SqlDataReader dr = null;
			//			string sql = "select b.projname,b.smallclass,b.probclass,datediff(hh, a.startdate , getdate()) as times,a.projcode,a.istransaction,a.ispress from b_project as a INNER JOIN b_project_detail as b ON "+
			//					"a.projcode = b.projcode where a.stepid='"+step+"' and a.stateid='"+state+"' ";
			try
			{
				//				int page = Convert.ToInt32(pages);
				//string projname = "";
				StringBuilder sb = new StringBuilder();
				//				
				//				if(step!="0")
				//				{
				//					if( !areacode.Equals("4403") && areacode!=null)
				//					{	
				//						sql = sql+" and substring(gridcode,0,8)='"+areacode+"' and (a.isdel='0' or a.isdel is null)  and (a.isgreat is null or a.isgreat = '0')";//.Replace("%",areacode);
				//					}
				//					else
				//					{
				//						sql = sql+" and (a.isgreat = '1' or a.isgreat = '2') and (a.isdel='0' or a.isdel is null) ";
				//					}
				//				}
				//				else
				//				{
				//					string [] areacodes = areacode.Split(',');
				//					string sqls = "select areaname from area where areacode = '"+areacodes[1]+"'";
				//					rs = DataAccess.ExecuteReader(sqls,null);
				//					string areaname="";
				//					if(rs.Read())
				//					{
				//						areaname = Convert.ToString(rs["areaname"]);
				//					}
				//					if(rs != null)
				//						rs.Close();	
				//					sql = "select b.projname,b.smallclass,b.probclass,datediff(hh, a.startdate , getdate()) as times,a.projcode,a.istransaction,a.ispress from b_project a " +
				//						"INNER JOIN b_project_detail b ON a.ProjCode = b.ProjCode where exists ( select * from ( select projcode from lawer_proj where ip is null union select projcode from web_proj where ip is null union " +
				//						"select projcode from lawer_proj where ip = '"+areacodes[0]+"' union select Projcode from web_proj where ip=   '"+areacodes[0]+"' union " +
				//						"select Projcode from public_proj where seatip =  '"+areacodes[0]+"' union select projcode from other_proj where ip = '"+areacodes[0]+"') as tt " +
				//						"where tt.projcode = a.projcode and a.stepid='0' and a.stateid = '1' and a.isdel = '0' and b.probsource != '1' and a.isdel!='1' )";
				//					//sql = "select b_project_detail.projname,b_project_detail.probclass,b_project_detail.smallclass,b_project.istransaction,b_project.ispress,b_project.projcode from b_project INNER JOIN b_project_detail ON b_project.ProjCode = b_project_detail.ProjCode where (b_project_detail.area = '"+areaname+"' or b_project_detail.area = '') and  b_project.stepid='0' and stateid = '1' and (isdel='0' or isdel is null) and b_project_detail.probsource <> '1' and b_project_detail.probsource <> '0' "+
				//						//" union select b_project_detail.projname,b_project_detail.probclass,b_project_detail.smallclass,b_project.istransaction,b_project.ispress,b_project.projcode from b_project INNER JOIN b_project_detail ON b_project.ProjCode = b_project_detail.ProjCode where b_project.stepid='0' and (isdel='0' or isdel is null) and  stateid = '1' and b_project_detail.probsource ='0'  and b_project.projcode in (select projcode from public_proj where seatIP = '"+areacodes[0]+"')";
				//				}
				//				int RowCount;
				//				int PageCount;
				//
				//				com.teamax.util.DataAccess.cutPage(sql,15,out RowCount,out PageCount);
				string smallclass = "";
				string probclass = "";
				SqlParameter [] input = new SqlParameter[5];
				input[0] = new SqlParameter("@step", System.Data.SqlDbType.NVarChar);
				input[0].Direction = ParameterDirection.Input;
				input[0].Value = step;
				input[1] = new SqlParameter("@state", System.Data.SqlDbType.NVarChar);
				input[1].Direction = ParameterDirection.Input;
				input[1].Value = state;
				input[2] = new SqlParameter("@areacode", System.Data.SqlDbType.NVarChar);
				input[2].Direction = ParameterDirection.Input;
				input[2].Value = areacode;
				input[3] = new SqlParameter("@pages", System.Data.SqlDbType.NVarChar);
				input[3].Direction = ParameterDirection.Input;
				input[3].Value = pages;
				input[4] = new SqlParameter("@usercode", System.Data.SqlDbType.NVarChar);
				input[4].Direction = ParameterDirection.Input;
				input[4].Value = usercode;

				rs = com.teamax.util.DataAccess.ExecuteStoredProcedure2("getProjcodes",input);//com.teamax.util.DataAccess.getPageData(sql,15,page,"projcode","desc",RowCount,PageCount);//com.teamax.util.DataAccess.ExecuteReader(sql,null);
				
				if(rs==null)
				{
					return null;
				}
				while(rs.Read())
				{
					probclass = Convert.ToString(rs["probclass"]);
					smallclass = Convert.ToString(rs["smallclass"]);
					string ret = "";
					int times = 0;
					if(!smallclass.Equals("")&&smallclass.Length>0)
					{
						//DateTime startdate = Convert.ToDateTime(rs["startdate"]);
						string sql1 = "";
						if(probclass.Equals("0"))
						{
							sql1 = "select t_time from smallclass_part where id = '"+smallclass+"'";
						}
						else
						{
							sql1 = "select t_time from smallclass_event where id = '"+smallclass+"'";
						}
						dr = com.teamax.util.DataAccess.ExecuteReader(sql1,null);		
					
						if(dr.Read())
						{
							times = Convert.ToInt32(dr["t_time"]);
						}
						dr.Close();
						//DateTime nowtime = System.DateTime.Now;
						if(step != "0")
						{
							int hours = Convert.ToInt32(rs[3]);//DateUtil.getHourDistanse(startdate,nowtime);
						
							if(hours<times/2)
							{
								ret = "0";
							}
							else if(hours>times/2 && hours<times)
							{
								ret = "1";
							}
							else
							{
								ret = "2";
							}
						}
					}
					//projname += Convert.ToString(rs["projname"])+"," +Convert.ToString(rs["istransaction"]) + "," + Convert.ToString(rs["ispress"]) +","+ret+","+times +"$";
					sb.Append(Convert.ToString(rs["projname"]))
						.Append(",")
						.Append(Convert.ToString(rs["istransaction"]))
						.Append(",")
						.Append(Convert.ToString(rs["ispress"]))
						.Append(",")
						.Append(ret)
						.Append(",")
						.Append(times)
						.Append("$");
				}
				if(rs != null)
					rs.Close();
				string projnames="";
				if(sb.ToString()!="")
				{
                    projnames = bacgBL.Pub.Tools.StrConv(sb.ToString().Substring(0, sb.Length - 1), "GB2312");
				}
				return projnames;
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(Convert.ToInt32(usercode),"getProjcodes",
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"Ajax: szcg.com.teamax.msg.ProjectInter");
				rs.Close();
				dr.Close();
				return "";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateProjIsaction(string projcode)
		{
			try
			{
				string sql = "update b_project set istransaction = '0' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		/// <summary>
		/// 用来标识案件的状态
		/// </summary>
		/// <param name="projcode">案卷编码</param>
		/// <returns></returns>
		[AjaxPro.AjaxMethod]
		public string proj_IsProcess(string projcode)
		{
			try
			{
				string strSQL = string.Format(@"	update b_project 
													set isProcess = Case When IsNull(isProcess,0)=0 then 1 else 0 end
													where ProjCode = '{0}' ",projcode);
				com.teamax.util.DataAccess.ExecuteNonQuery(strSQL);
				return "1";
			}
			catch
			{
				return "-1";
			}
		}

		

		//并发操作
		[AjaxPro.AjaxMethod]
		public string proj_lock(string projcode,string step,string usercode,string state)
		{

			SqlDataReader dr = null;
			SqlDataReader res1 = null;
			SqlDataReader res = null;
			SqlDataReader rs = null;
			string CurrentUsercode = "";
			try
			{
				//举报栏特殊处理
				string result="";
				string sql1 = "";
				
				if(step == "0")
				{
					string [] strRes = usercode.Split(',');
					CurrentUsercode = strRes[1];
					sql1 = "select b_project_detail.projname,b_project.istransaction,b_project.projcode from b_project INNER JOIN b_project_detail ON b_project.ProjCode = b_project_detail.ProjCode where b_project.lockusercode = '"+strRes[1]+"' and b_project.isdel <> '1' and b_project.stepid='"+step+"' and stateid = '"+state+"' and b_project_detail.probsource <> '1' and b_project_detail.probsource <> '0' "+
						" union select b_project_detail.projname,b_project.istransaction,b_project.projcode from b_project INNER JOIN b_project_detail ON b_project.ProjCode = b_project_detail.ProjCode where b_project.lockusercode = '"+strRes[1]+"' and b_project.stepid='"+step+"' and b_project.isdel <> '1' and stateid = '"+state+"' and b_project_detail.probsource ='0'  and b_project.projcode in (select projcode from public_proj where seatIP = '"+strRes[0]+"')";
				}	
				else
				{
					sql1 = "select b_project_detail.projname,b_project.istransaction,b_project.projcode from b_project INNER JOIN b_project_detail ON b_project.ProjCode = b_project_detail.ProjCode where b_project.lockusercode = '"+usercode+"' and b_project.stepid='"+step+"' and b_project.isdel <> '1' and stateid = '"+state+"'";
				}
				//先判断是否有锁定的案卷
				dr = com.teamax.util.DataAccess.ExecuteReader(sql1,null);
				int count = 0;
				string projname = "";
				string istransaction = "";
				while(dr.Read())
				{
					istransaction = Convert.ToString(dr["istransaction"]);
					count = Convert.ToInt32(dr["projcode"]);
					projname = Convert.ToString(dr["projname"]);
					if(istransaction=="1")
					{
						break;
					}
				}
				if(dr != null)
					dr.Close();
				//没有锁定的案卷
				if(istransaction!="1")
				{
					//判断是否当前案卷被锁定
					if(step=="1" && state == "3") //接线员的核查栏
					{
						string sql5 = "select isthrough from b_project where projcode = '"+projcode+"'";
						res1 = com.teamax.util.DataAccess.ExecuteReader(sql5,null);
						string isthroughs = "";
						if(res1.Read())
						{
							isthroughs = Convert.ToString(res1["isthrough"]);
						}
						res1.Close();
						if(isthroughs!="1") //不是快速上报
						{
							string sql4 = "select * from b_pdamsg where projcode = '"+projcode+"'";
							res = com.teamax.util.DataAccess.ExecuteReader(sql4,null);
							string results = "";
							string ioflags = "";
							string states = "";
							if(res.Read())
							{
								ioflags = Convert.ToString(res["ioflag"]);
								states = Convert.ToString(res["state"]); //1是结案核查；2是问题上报。
							}
							res.Close();
							results = ioflags + states;
							if(results!="" && results != "21" && results != "31") // results=""表示没有核实记录;results=21表示核查完
							{
								return "-4";
							}
						}
					}
					string sql = "select istransaction,stepid,stateid,lockusercode,isdel from b_project where projcode = '"+projcode+"'";
					string istransaction1="";
					string stepid="";
					string stateid="";
					string lockusercode="";
					string isdel = "0";
					rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
					if(rs==null) //已经结案了，没有记录
					{
						rs.Close();
						result = "-3";
						return result;
					}
					while(rs.Read())
					{
						istransaction1 = Convert.ToString(rs["istransaction"]);
						stepid = Convert.ToString(rs["stepid"]);
						stateid = Convert.ToString(rs["stateid"]);
						lockusercode=Convert.ToString(rs["lockusercode"]);
						isdel = Convert.ToString(rs["isdel"]);
					}
					rs.Close();
					if(isdel == "1")
					{
						result = "-3";
						return result;
					}
					if(!step.Equals(stepid)||!state.Equals(stateid))
					{
						result = "-3";
						return result;
					}
					if(istransaction1.Equals("1")) //该案件被正在处理
					{
						if(lockusercode == CurrentUsercode) //锁定人为他本人
						{
						 	result = "-1";
						}
						else
						{
							result = "-2";
						}
						return result;
					}
					else 
					{
						try
						{
							if(step == "0")
							{
								string[] strUser = usercode.Split(',');
								usercode = strUser[1];
							}
							string sql2 = "update b_project set istransaction = '1',lockusercode = '"+usercode+"',locktime = GetDate() where projcode = '"+projcode+"'";
							com.teamax.util.DataAccess.ExecuteNonQuery(sql2,null);
							result = "-1";
							return result;
						}
						catch(Exception ex)
						{
							System.Diagnostics.Debug.WriteLine(ex.Message);
							result = "-3";
							return result;
						}
					}
				}
				else
				{
					string sql = "select projcode,lockusercode from b_project where projcode = '"+projcode+"'";
					rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
					string lockusercode="";
					string projcodes = "";
					if(rs==null)
					{
						//rs.Close();
						result = "-3";
						return result;
					}
					while(rs.Read())
					{
						lockusercode=Convert.ToString(rs["lockusercode"]);
						projcodes = Convert.ToString(rs["projcode"]);
					}
					if(rs != null)
						rs.Close();
					if(step == "0")
					{
						string[] strUser = usercode.Split(',');
						usercode = strUser[1];
					}

					if(lockusercode.Equals(usercode))
					{
						result = "-1";
						return result;
					}
					else
					{
                        string projnames = bacgBL.Pub.Tools.StrConv(projname, "GB2312");
						return projnames;
					}
				}
			}
			catch(Exception e)
			{
				dr.Close();
				res1.Close();
				res.Close();
				rs.Close();
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "-3";
			}
		}


		[AjaxPro.AjaxMethod]
		public string proj_unlock(string projcode)
		{
			try
			{
				string sql = "update b_project set istransaction = '0',lockusercode='0',locktime='' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "-1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "-3";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateIsdel(string projcode)
		{
			try
			{
				string sql = "update b_project set isdel = '1' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string getIoflag(string projcode)
		{
			SqlDataReader rs = null;
			try
			{
				string ioflag="";
				string sql = "select ioflag,state from b_pdamsg where projcode = '"+projcode+"'";
				string sql1 = "select isthrough from b_project where projcode = '"+projcode+"'";
				rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
				if(rs.Read())
				{
					ioflag = Convert.ToString(rs["ioflag"])+Convert.ToString(rs["state"]);
				}
				if(rs != null)
					rs.Close();

				string isthrough = "";
				rs = com.teamax.util.DataAccess.ExecuteReader(sql1,null);
				if(rs.Read())
				{
					isthrough = Convert.ToString(rs[0]);
				}
				if(isthrough == "" || isthrough == null)
				{
					isthrough = "0";
				}
				if(rs != null)
					rs.Close();
				return ioflag + "$" + isthrough;
			}
			catch(Exception e)
			{
				if(rs != null) rs.Close();
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "异常";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateEndtime(string projcode,string endtime)
		{
			try
			{
				string sql = "update b_project set enddate = '"+endtime+"' where projcode = '"+projcode+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateIspress(string projname)
		{
			try
			{
				string sql = "update b_project set ispress = '1' where projcode = (select projcode from b_project_detail where projname = '"+projname+"')";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string FtpDown(string filename)
		{
			//-1,表示文件没有找到
			//-2,表示Ftp连接失败
			//-3，表示Ftp服务器没有找到
			string ftpurl = "";
			FtpSupport.FtpConnection ftp = new FtpSupport.FtpConnection();
			try
			{
				ftp.Connect(ConfigSettings.DownFtpIp,ConfigSettings.DownFtpUserName,ConfigSettings.DownFtpPassWord);
				try
				{
					ftp.SetCurrentDirectory("/");
					if(filename!="")
					{
						ftpurl = "ftp://"+ConfigSettings.DownFtpUserName.ToString()+":"+ConfigSettings.DownFtpPassWord.ToString()+"@"+ConfigSettings.DownFtpIp.ToString()+"/"+filename.ToString();
						return ftpurl;
					}
					else
					{
						return "-1";
					}
				}
				catch
				{
					return "-2";
				}
				finally
				{
					ftp.Close();
				}
			}
			catch
			{
				return "-3";
			}
		}

        [AjaxPro.AjaxMethod]
        public string SoundFileUrl(string filename)
        {
            //-1,表示文件没有找到
            
            string SoundUrl = "";
            
                try
                {
                    if (filename != "")
                    {
                        SoundUrl = ConfigSettings.SonudFileUrl.ToString() +"/" + filename.ToString();
                        return SoundUrl;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch
                {
                    return "-2";
                }
               
        }

//		[AjaxMethod]
//		public string jaProject(string projcode)
//		{
//			SqlParameter [] input = new SqlParameter[1];
//			int code = Convert.ToInt32(projcode);
//			input[0] = new SqlParameter("@projcode", System.Data.SqlDbType.Int);
//			input[0].Direction = ParameterDirection.Input;
//			input[0].Value = code;
//			
//			SqlParameter [] output = new SqlParameter[1];
//			output[0] = new SqlParameter("@flag", System.Data.SqlDbType.Int);
//			output[0].Direction = ParameterDirection.Output;
//
//			string result = com.teamax.util.DataAccess.ExecuteStoreProcedure1("runAt_endcase",input,output);
//			
//			return result;
//		}

		[AjaxPro.AjaxMethod]
		public string getCollecterXY(string collcode)
		{
			SqlDataReader rs = null;
			try
			{
//				string sql1 = "select top 1 datediff(dd, cu_date, getdate()) from collecter_xy order by cu_date desc";
//				SqlDataReader dr = com.teamax.util.DataAccess.ExecuteReader(sql1,null);
//				int day = 1;
//				if(dr.Read())
//				{
//					day = Convert.ToInt32(dr[0]);
//				}
//				dr.Close();
//				if(day==0)
//				{
					string sql = "select * from collecter_xy where collcode = '"+collcode+"' order by cu_date desc";
					rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
					string cu_x = "";
					string cu_y = "";

					if(rs==null)
					{
						rs.Close();
						return "-3";
					}
					if(rs.Read())
					{
						cu_x = Convert.ToString(rs["cu_x"])+"$";
						cu_y = Convert.ToString(rs["cu_y"]);
					}
					if(rs != null)
						rs.Close();
					return cu_x+cu_y;
//				}
//				else 
//				{
//					return "";
//				}
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);

				if(rs != null) rs.Close();
				return "-3";
			}
		}

		//发布公文
		[AjaxPro.AjaxMethod]
		public string insertArchive(string title,string author,string content)
		{
			SqlParameter[] input = new SqlParameter[3];
			SqlParameter[] output= new SqlParameter[1];
			input[0] = new SqlParameter("@Title",SqlDbType.VarChar,128);
			input[0].Value=title;
			input[0].Direction=ParameterDirection.Input;
			input[1] = new SqlParameter("@Author",SqlDbType.VarChar,128);
			input[1].Value=author;
			input[1].Direction=ParameterDirection.Input;
			input[2] = new SqlParameter("@Content",SqlDbType.VarChar,4096);
			input[2].Value=content;
			input[2].Direction=ParameterDirection.Input;
			output[0] = new SqlParameter("@result",SqlDbType.Char,1);
			output[0].Direction=ParameterDirection.Output;
			string c =Convert.ToString(DataAccess.ExecuteStoreProcedure1("insertArchive",input,output));
			return c;
		}

		[AjaxPro.AjaxMethod]
		public string sendMsgtoCol(string phones,string msgcontent)
		{
			try
			{
				string[] phone = phones.Split(',');
				for(int i=0; i<phone.Length; i++)
				{
					com.teamax.util.DataAccess.SendMessage("s",phone[i],msgcontent);
				}
				return "-1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}
		
		//是否有新案卷
		[AjaxPro.AjaxMethod]
		public string isNewProject(string stepid,string areacode,string role,string usercode)
		{
			//SqlDataReader rs = null;
			//string sql = "select count(*) from b_project where isdel != '1' ";
			try
			{	
				//				if(stepid!="4")
				//				{
				//					sql += "  and gridcode like '"+areacode+"%' and stepid = '"+stepid+"' ";
				//					if(areacode!="4403")
				//					{
				//						sql += " and (isgreat = '0' or isgreat is null)";
				//					}
				//					else
				//					{
				//						sql += " and isgreat = '1'";
				//					}
				//				}
				//				else 
				//				{
				//					sql += " and role = '"+role+"' and stepid = '"+stepid+"'";
				//					if(areacode!="4403")
				//					{
				//						sql += " and (isgreat = '0' or isgreat is null)";
				//					}
				//					else
				//					{
				//						sql += " and isgreat = '1'";
				//					}
				//				}
				SqlParameter[] input = new SqlParameter[4];
				SqlParameter[] output= new SqlParameter[1];
				input[0] = new SqlParameter("@stepid",SqlDbType.VarChar,2);
				input[0].Value=stepid;
				input[0].Direction=ParameterDirection.Input;
				input[1] = new SqlParameter("@areacode",SqlDbType.VarChar,8);
				input[1].Value=areacode;
				input[1].Direction=ParameterDirection.Input;
				input[2] = new SqlParameter("@role",SqlDbType.VarChar,5);
				input[2].Value=role;
				input[2].Direction=ParameterDirection.Input;
				input[3] = new SqlParameter("@usercode",SqlDbType.VarChar,10);
				input[3].Value=usercode;
				input[3].Direction=ParameterDirection.Input;
				output[0] = new SqlParameter("@isresult",SqlDbType.VarChar,10);
				output[0].Direction=ParameterDirection.Output;
				//rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
				string result = DataAccess.ExecuteStoreProcedure1("isnewproject",input,output);
				//				int iscreate = 0;
				//				if(rs.Read())
				//				{
				//					//string isresult = Convert.ToString(rs["iscreate"]);
				////					if(isresult==null||isresult=="")
				////					{
				////						isresult = "0";
				////					}
				//					iscreate = Convert.ToInt32(rs[0]);
				//				}
				//				rs.Close();
				return result;//Convert.ToString(iscreate);
				//				if(iscreate.IndexOf('0') == -1)
				//				{
				//					return "0";
				//				}
				//				else
				//				{
				//					string sql1="update b_project set iscreate = '1' where substring(gridcode,0,8) = '"+areacode+"'";
				//					if(stepid!="4")
				//					{
				//						sql1 += " and stepid = '"+stepid+"'";
				//					}
				//					else
				//					{
				//						sql1 += " and role = '"+role+"' and stepid = '"+stepid+"'";
				//					}
				//					com.teamax.util.DataAccess.ExecuteNonQuery(sql1,null);
				//					return "1`";
				//				}
			}
			catch(Exception e)
			{
				//rs.Close();
				BASE_logmanageservice.writeSystemLog(Convert.ToInt32(usercode),"isnewproject",
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"Ajax: szcg.com.teamax.msg.ProjectInter");
				return "0";
			}
		}

		//智能报警
		[AjaxPro.AjaxMethod]
		public string updateHelpstate(string id,string usercode)
		{
			try
			{
                string sql = "update m_call_help set state = '1',usercode = '" + usercode + "' where id = '" + id + "'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string getHelpstate(string areacode,string usercode)
		{
			string sql = "select a.*,b.collname from call_help as a,collecter as b where a.collcode=b.collcode where call_help.sendtype = '1' ";
			try
			{	
				if(areacode!="4403")
				{
					sql += " and b.gridcode like '"+areacode+"%'";
				}
				SqlDataReader dr = com.teamax.util.DataAccess.ExecuteReader(sql,null);
				string flag = "";
				while(dr.Read())
				{
					flag += Convert.ToString(dr["state"]);
				}
				dr.Close();
				
				if(flag.IndexOf('0') == -1)
				{
					return "0";
				}
				else
				{
					return "1";
				}
			}
			catch(Exception e)
			{
				BASE_logmanageservice.writeSystemLog(Convert.ToInt32(usercode),sql,
					System.DateTime.Now,System.DateTime.Now,BASE_ModerId.getSystem_ZHYW(),e.ToString()
					,"Ajax: szcg.com.teamax.msg.ProjectInter");
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateOtherinfo(string id)
		{
			try
			{
				string sql = "UPDATE b_pdamsg_other SET state = '2' WHERE id ='"+id+"'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string getAreaName(string gridcode)
		{
			try
			{
				string sql = "select a.areaname,b.streetname,c.commname from area as a,street as b,community as c,grid as d " +
					" where a.id = b.fid and b.id = c.fid and c.id = d.commfid and d.gridcode like '"+gridcode.Substring(0,12)+"%'";
				SqlDataReader rs = com.teamax.util.DataAccess.ExecuteReader(sql,null);
				string names = "0";
				if(rs.Read())
				{
					names = Convert.ToString(rs["areaname"]) + "/" + Convert.ToString(rs["streetname"]) + "/" + Convert.ToString(rs["commname"]);
				}
				rs.Close();
				if(names!="0")
				{
                    names = bacgBL.Pub.Tools.StrConv(names, "GB2312");
				}
				return names;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string getMsgCount(string projcode)
		{
			SqlDataReader rs = null;
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
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string updateDel(string projcode)
		{
			try
			{
				string sql = "update b_project set isdel = '1' where projcode = '"+projcode+"'";
				DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string sendMsgForCollecter(string txt1,string txt2)
		{
			SqlDataReader dr = null;
			try
			{
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				dr.Close();
				return "0";
			}
		}

		/// <summary>
		/// 任务分派时督办
		/// </summary>
		/// <param name="projcode">案卷编号</param>
		/// <param name="content">督办批示</param>
		/// <param name="roleid">经办角色</param>
		/// <param name="usercode">经办人</param>
		/// <param name="dbname">督办人名字</param>
		/// <returns></returns>
		[AjaxPro.AjaxMethod]
		public string DispatchDb(string projcode,string content,string roleid,string usercode,string dbname)
		{
			try
			{
				SqlParameter spProjcode = new SqlParameter("@projcode",projcode);
				SqlParameter spContent = new SqlParameter("@content",content);
				SqlParameter spRoleid = new SqlParameter("@roleid",roleid);
				SqlParameter spUsercode = new SqlParameter("@usercode",usercode);
				SqlParameter spDbname = new SqlParameter("@dbname",dbname);
				DataAccess.ExecuteStoredProcedure("InsertInspectInfo",spProjcode,spContent,spRoleid,spUsercode,spDbname);

				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		[AjaxPro.AjaxMethod]
		public string sendMsgToDepart(string phone,string msg)
		{
			try
			{
				DataAccess.SendMessage("s",phone,msg);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		//插入意见表
		[AjaxPro.AjaxMethod]
		public string insertIdea(string title,string content,string usercode,string memo)
		{
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@title",SqlDbType.VarChar,100);
				input[0].Value=@title;
				input[0].Direction=ParameterDirection.Input;
				input[1] = new SqlParameter("@content",SqlDbType.VarChar,1024);
				input[1].Value=content;
				input[1].Direction=ParameterDirection.Input;
				input[2] = new SqlParameter("@memo",SqlDbType.VarChar,1024);
				input[2].Value=memo;
				input[2].Direction=ParameterDirection.Input;
				input[3] = new SqlParameter("@usercode",SqlDbType.VarChar,20);
				input[3].Value=usercode;
				input[3].Direction=ParameterDirection.Input;
				DataAccess.ExecuteStoredProcedure("insertIdea",input);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}

		//插入反馈意见
		[AjaxPro.AjaxMethod]
		public string insertFeedIdea(string content,string usercode,string fid,string memo)
		{
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@content",SqlDbType.VarChar,1024);
				input[0].Value=content;
				input[0].Direction=ParameterDirection.Input;
				input[1] = new SqlParameter("@memo",SqlDbType.VarChar,1024);
				input[1].Value=memo;
				input[1].Direction=ParameterDirection.Input;
				input[2] = new SqlParameter("@usercode",SqlDbType.VarChar,20);
				input[2].Value=usercode;
				input[2].Direction=ParameterDirection.Input;
				input[3] = new SqlParameter("@fid",SqlDbType.VarChar,10);
				input[3].Value = fid;
				input[3].Direction = ParameterDirection.Input;
				DataAccess.ExecuteStoredProcedure("insertFeedback",input);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "0";
			}
		}
	}
}
