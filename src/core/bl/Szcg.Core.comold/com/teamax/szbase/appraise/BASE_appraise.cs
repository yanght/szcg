using System;
using AjaxPro;
using System.Collections;
using szcg.com.teamax.util;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using szcg.com.teamax.szbase.entity;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.appraise
{
	/// <summary>
	/// BASE_appraise 的摘要说明。
	/// </summary>
	public class BASE_appraise
	{
		public BASE_appraise()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public ArrayList getAreaAppraise(int id,int usercode)
		{
			ArrayList list=new ArrayList();
				string sql="select a.id,a.objectname from appraise_object a,appraise_target b where b.isdel=0 and a.targetcode=b.id and a.appraisetype="+id+" and a.usercode="+usercode;
			SqlDataReader dr=DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
				string[] str=new string[2];
				str[0]=dr["id"].ToString();
				str[1]=dr["objectname"].ToString();
				list.Add(str);
			}
			dr.Close();
			return list;
		}

		public Hashtable getAppraiseInfoByID(int id)
		{
			Hashtable table=new Hashtable();
			string sql="select a.usercode usercode,a.appraisetype appraisetype,a.objectname objectname, a.targetcode targetcode,a.objectcode areacode,a.ttype ispublic,b.target_name targetname   from appraise_object a,appraise_target b where  a.targetcode=b.id and a.id="+id;
			SqlDataReader dr=DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
				string usercode=dr["usercode"].ToString();
				table.Add("usercode",usercode);
				string appraisetype=dr["appraisetype"].ToString();
				table.Add("appraisetype",appraisetype);
				string objectname=dr["objectname"].ToString();
				table.Add("objectname",objectname);
				string targetcode=dr["targetcode"].ToString();
				table.Add("targetcode",targetcode);
                string areacode = bacgBL.Pub.Tools.changeNull(dr["areacode"].ToString());
				table.Add("areacode",areacode);
                string ispublic = bacgBL.Pub.Tools.changeNull(dr["ispublic"].ToString());
				table.Add("ispublic",ispublic);
				string targetname=dr["targetname"].ToString();
				table.Add("targetname",targetname);

			}
			dr.Close();
			sql="select code,name from appraise_subobject where fid="+id;
			dr=DataAccess.ExecuteReader(sql,null);
			StringBuilder sb=new StringBuilder();
			StringBuilder ss=new StringBuilder();
			while(dr.Read())
			{
				sb.Append(dr["code"].ToString()+",");
				ss.Append(dr["name"].ToString()+",");
			}
			dr.Close();
			if(sb.Length>0)
			{
				table.Add("subcode",sb.ToString().Substring(0,sb.Length-1));
				table.Add("subname",ss.ToString().Substring(0,ss.Length-1));
			}
			else
			{
				table.Add("subcode","");
				table.Add("subname","");
			}
			return table;
		}

		public ArrayList getAllTarget(int id)
		{
			ArrayList list=new ArrayList();
			string sql="select id,target_name,default_exp from appraise_target where isdel=0 and appraise_type="+id;
			SqlDataReader dr=DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
				string[] str=new string[3];
				str[0]=dr["id"].ToString();
				str[1]=dr["target_name"].ToString();
				str[2]=dr["default_exp"].ToString();
				list.Add(str);
			}
			dr.Close();
			return list;
		}

		public int deleteAppraiseObject(int id,string userId)
		{
			string sql="DeleteAppraiseObject";
			int userid=Convert.ToInt32(userId);
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@objectID",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;

				int i= Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userid,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.appraise.BASE_appraise.deleteAppraiseObject");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userid,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					"szcg.com.teamax.szbase.appraise.BASE_appraise.deleteAppraiseObject");
				throw;
			}
		}

		public int insertAppraiseObject(int usercode,string appraisetype,DateTime cudate,string objectname,
			string targetcode,string subcode,string subname,string areacode,int ispublic)
		{
			string sql="InsertIntoAppraiseObject";
			try
			{
				SqlParameter[] input=new SqlParameter[9];
				input[0]=new SqlParameter("@usercode",SqlDbType.Int);
				input[0].Value=usercode;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@appraisetype",SqlDbType.VarChar,32);
				input[1].Value=appraisetype;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@cudate",SqlDbType.DateTime);
				input[2].Value=cudate;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@objectname",SqlDbType.VarChar,255);
				input[3].Value=objectname;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@targetcode",SqlDbType.VarChar,128);
				input[4].Value=targetcode;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@subcode",SqlDbType.VarChar,4096);
				input[5].Value=subcode;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@subname",SqlDbType.VarChar,4096);
				input[6].Value=subname;
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@areacode",SqlDbType.VarChar,32);
				input[7].Value=areacode;
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@ispublic",SqlDbType.Int);
				input[8].Value=ispublic;
				input[8].Direction=ParameterDirection.Input;
				
				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
				
				int i= Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(usercode,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.appraise.BASE_appraise.insertAppraiseObject");

				return i;

			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					usercode,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					"szcg.com.teamax.szbase.appraise.BASE_appraise.insertAppraiseObject");
				throw;
			}
		}

		public int updateAppraiseObject(int usercode,string appraisetype,DateTime cudate,string objectname,
			string targetcode,string subcode,string subname,int id,string areacode,int ispublic)
		{
			string sql="UpdateIntoAppraiseObject";
			try
			{
				SqlParameter[] input=new SqlParameter[10];
				input[0]=new SqlParameter("@usercode",SqlDbType.Int);
				input[0].Value=usercode;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@appraisetype",SqlDbType.VarChar,32);
				input[1].Value=appraisetype;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@cudate",SqlDbType.DateTime);
				input[2].Value=cudate;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@objectname",SqlDbType.VarChar,255);
				input[3].Value=objectname;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@targetcode",SqlDbType.VarChar,128);
				input[4].Value=targetcode;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@subcode",SqlDbType.VarChar,4096);
				input[5].Value=subcode;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@subname",SqlDbType.VarChar,4096);
				input[6].Value=subname;
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@theid",SqlDbType.Int);
				input[7].Value=id;
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@areacode",SqlDbType.VarChar,32);
				input[8].Value=areacode;
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@ispublic",SqlDbType.Int);
				input[9].Value=ispublic;
				input[9].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
				
				int i= Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(usercode,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.appraise.BASE_appraise.updateAppraiseObject");

				return i;

			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					usercode,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					"szcg.com.teamax.szbase.appraise.BASE_appraise.updateAppraiseObject");
				throw;
			}
		}

		public bool checkAppraiseOjbectName(string name)
		{
			string sql="select count(*) from appraise_object where objectname=@objectname";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@objectname",SqlDbType.VarChar,255);
			input[0].Value=name;
			input[0].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		[AjaxMethod]
		public string getGridTreeInfo(string areacode)
		{
			int code=Convert.ToInt32(areacode);
			string result="";
			StringBuilder areas=new StringBuilder();
			StringBuilder streets=new StringBuilder();
			StringBuilder comms=new StringBuilder();
			StringBuilder grids=new StringBuilder();
			string sql="GetGridTreeInfo";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=code;
			input[0].Direction=ParameterDirection.Input;
			SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
			
			while(rs.Read())
			{
				areas.Append(rs["id"].ToString()+",");
				areas.Append(rs["areacode"].ToString()+",");
                areas.Append(bacgBL.Pub.Tools.StrConv(rs["areaname"].ToString(), "GB2312") + ";");
			}
			if(areas.Length>0)
			{
				result=areas.ToString().Substring(0,areas.Length-1);
				if(rs.NextResult())
				{
					while(rs.Read())
					{
						streets.Append(rs["id"].ToString()+",");
						streets.Append(rs["streetcode"].ToString()+",");
                        streets.Append(bacgBL.Pub.Tools.StrConv(rs["streetname"].ToString(), "GB2312") + ",");
						streets.Append(rs["fid"].ToString()+";");
					}
				}
				if(streets.Length>0)
				{
					result=result+"@"+streets.ToString().Substring(0,streets.Length-1);
					if(rs.NextResult())
					{
						while(rs.Read())
						{
							comms.Append(rs["id"].ToString()+",");
							comms.Append(rs["commcode"].ToString()+",");
                            comms.Append(bacgBL.Pub.Tools.StrConv(rs["commname"].ToString(), "GB2312") + ",");
							comms.Append(rs["fid"].ToString()+";");
						}
					}

					if(comms.Length>0)
					{
						result=result+"@"+comms.ToString().Substring(0,comms.Length-1);
						if(rs.NextResult())
						{
							while(rs.Read())
							{
								grids.Append(rs["id"].ToString()+",");
								grids.Append(rs["gridcode"].ToString()+",");
								grids.Append(rs["commfid"].ToString()+";");
							}
						}

						if(grids.Length>0)
						{
							result=result+"@"+grids.ToString().Substring(0,grids.Length-1);
						}

					}
				}
			}
			return result;
		}

		[AjaxMethod]
		public string getCollecterTreeInfo(string areacode)
		{
			int code=Convert.ToInt32(areacode);
			string result="";
			StringBuilder areas=new StringBuilder();
			StringBuilder streets=new StringBuilder();
			StringBuilder comms=new StringBuilder();
			StringBuilder grids=new StringBuilder();
			string sql="GetCollecterTreeInfo";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=code;
			input[0].Direction=ParameterDirection.Input;
			SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
			
			while(rs.Read())
			{
				areas.Append(rs["id"].ToString()+",");
				areas.Append(rs["areacode"].ToString()+",");
                areas.Append(bacgBL.Pub.Tools.StrConv(rs["areaname"].ToString(), "GB2312") + ";");
			}
			if(areas.Length>0)
			{
				result=areas.ToString().Substring(0,areas.Length-1);
				if(rs.NextResult())
				{
					while(rs.Read())
					{
						streets.Append(rs["id"].ToString()+",");
						streets.Append(rs["streetcode"].ToString()+",");
                        streets.Append(bacgBL.Pub.Tools.StrConv(rs["streetname"].ToString(), "GB2312") + ",");
						streets.Append(rs["fid"].ToString()+";");
					}
				}
				if(streets.Length>0)
				{
					result=result+"@"+streets.ToString().Substring(0,streets.Length-1);
					if(rs.NextResult())
					{
						while(rs.Read())
						{
							comms.Append(rs["id"].ToString()+",");
							comms.Append(rs["commcode"].ToString()+",");
                            comms.Append(bacgBL.Pub.Tools.StrConv(rs["commname"].ToString(), "GB2312") + ",");
							comms.Append(rs["fid"].ToString()+";");
						}
					}

					if(comms.Length>0)
					{
						result=result+"@"+comms.ToString().Substring(0,comms.Length-1);
						if(rs.NextResult())
						{
							while(rs.Read())
							{
								grids.Append(rs["collcode"].ToString()+",");
                                grids.Append(bacgBL.Pub.Tools.StrConv(rs["collname"].ToString(), "GB2312") + ",");
								grids.Append(rs["commcode"].ToString()+";");
							}
						}

						if(grids.Length>0)
						{
							result=result+"@"+grids.ToString().Substring(0,grids.Length-1);
						}

					}
				}
			}
			return result;
		}

		[AjaxMethod]
		public string getOperatorTreeInfo(string areacode)
		{
			int code=Convert.ToInt32(areacode);
			string result="";
			StringBuilder roles=new StringBuilder();
			//StringBuilder users=new StringBuilder();
			string sql="GetOperatorTreeInfo";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=code;
			input[0].Direction=ParameterDirection.Input;
			SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
			
			while(rs.Read())
			{
				roles.Append(rs["rolecode"].ToString()+",");
                roles.Append(bacgBL.Pub.Tools.StrConv(rs["rolename"].ToString(), "GB2312") + ";");
			}
			if(roles.Length>0)
			{
				result=roles.ToString().Substring(0,roles.Length-1);
//				if(rs.NextResult())
//				{
//					while(rs.Read())
//					{
//						users.Append(rs["rolecode"].ToString()+",");
//						users.Append(rs["usercode"].ToString()+",");
//						users.Append(Tools.StrConv(rs["username"].ToString(),"GB2312")+";");
//					}
//				}
//
//				if(users.Length>0)
//				{
//					result=result+"@"+users.ToString().Substring(0,users.Length-1);
//				}
				
			}
			return result;
		}


		[AjaxMethod]
		public string getMonitorTreeInfo(string areacode)
		{
			int code=Convert.ToInt32(areacode);
			string result="";
			StringBuilder roles=new StringBuilder();
			//StringBuilder users=new StringBuilder();
			string sql="GetMonitorTreeInfo";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=code;
			input[0].Direction=ParameterDirection.Input;
			SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
			
			while(rs.Read())
			{
				roles.Append(rs["rolecode"].ToString()+",");
                roles.Append(bacgBL.Pub.Tools.StrConv(rs["rolename"].ToString(), "GB2312") + ";");
			}
			if(roles.Length>0)
			{
				result=roles.ToString().Substring(0,roles.Length-1);
//				if(rs.NextResult())
//				{
//					while(rs.Read())
//					{
//						users.Append(rs["rolecode"].ToString()+",");
//						users.Append(rs["usercode"].ToString()+",");
//						users.Append(Tools.StrConv(rs["username"].ToString(),"GB2312")+";");
//					}
//				}
//
//				if(users.Length>0)
//				{
//					result=result+"@"+users.ToString().Substring(0,users.Length-1);
//				}
				
			}
			return result;
		}

		[AjaxMethod]
		public string getCommandTreeInfo(string areacode)
		{
			int code=Convert.ToInt32(areacode);
			string result="";
			StringBuilder roles=new StringBuilder();
			//StringBuilder users=new StringBuilder();
			string sql="GetCommandTreeInfo";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=code;
			input[0].Direction=ParameterDirection.Input;
			SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
			
			while(rs.Read())
			{
				roles.Append(rs["rolecode"].ToString()+",");
                roles.Append(bacgBL.Pub.Tools.StrConv(rs["rolename"].ToString(), "GB2312") + ";");
			}
			if(roles.Length>0)
			{
				result=roles.ToString().Substring(0,roles.Length-1);
//				if(rs.NextResult())
//				{
//					while(rs.Read())
//					{
//						users.Append(rs["rolecode"].ToString()+",");
//						users.Append(rs["usercode"].ToString()+",");
//						users.Append(Tools.StrConv(rs["username"].ToString(),"GB2312")+";");
//					}
//				}
//
//				if(users.Length>0)
//				{
//					result=result+"@"+users.ToString().Substring(0,users.Length-1);
//				}
				
			}
			return result;
		}

		

		[AjaxMethod]
		public string getSelectCollecter(string streetID)
		{
			StringBuilder collID=new StringBuilder();
			StringBuilder collName=new StringBuilder();
			string sql="select collcode,collname from collecter coll inner join community comm on comm.id = coll.commcode inner join street st on st.id =comm.fid and st.streetcode in ("+streetID+")";
			SqlDataReader dr=DataAccess.ExecuteReader(sql,null);
			while(dr.Read())
			{
				collID.Append(dr["collcode"].ToString()+",");
                collName.Append(bacgBL.Pub.Tools.StrConv(dr["collname"].ToString(), "GB2312") + ",");
			}
			dr.Close();
			string result="";
			if(collID.Length>0)
			{
				result=collID.ToString().Substring(0,collID.Length-1);
				result=result+";"+collName.ToString().Substring(0,collName.Length-1);
			}
			return result;
			
		}
	}
}
