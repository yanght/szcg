using System;
using System.Collections;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Data;
using System.Text;
using szcg.com.teamax.szbase.entity;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;
using AjaxPro;

namespace szcg.com.teamax.szbase.organize
{
	/// <summary>
	/// OrgHelper 的摘要说明。
	/// </summary>
	public class BASE_orghelper
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.organize.BASE_orghelper.";
	
		public BASE_orghelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        /**
		 * 取得所有用户ID和姓名
		 **/
		public static ArrayList getUsers(int userId)
		{
			ArrayList array=new ArrayList(); 
			string sql="select usercode,username,departcode,mobile from loginuser";
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] user=new string[4];
					user[0]=rs["usercode"].ToString();
					user[1]=rs["username"].ToString();
					user[2]=rs["departcode"].ToString();
					user[3]=rs["mobile"].ToString();
					array.Add(user);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getUsers");
				throw;
			}
		}

		public static ArrayList getUsersByDepartID(int userId,int departID)
		{
			ArrayList array=new ArrayList(); 
			string sql="select usercode,username,departcode from loginuser where departcode="+departID;
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] user=new string[3];
					user[0]=rs["usercode"].ToString();
					user[1]=rs["username"].ToString();
					user[2]=rs["departcode"].ToString();
					array.Add(user);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getUsers");
				throw;
			}
		}

		public static ArrayList getUsers()
		{
			ArrayList array=new ArrayList(); 
			string sql="select usercode,username,departcode,mobile from loginuser";
	
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] user=new string[4];
					user[0]=rs["usercode"].ToString();
					user[1]=rs["username"].ToString();
					user[2]=rs["departcode"].ToString();
					user[3]=rs["mobile"].ToString();
					array.Add(user);
				}
				rs.Close();
				return array;
		}
		 /**
		 *取得所有部门信息
		**/
		public static ArrayList getDepts(int userId,string areacode)
		{
			ArrayList array=new ArrayList();
			string sql="select departcode,departname,parentcode from depart";
			if(!areacode.Equals("4403"))
			{
				sql="select departcode,departname,parentcode from depart where area='"+areacode+"'";
			}
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] dept=new string[3];
					dept[0]=rs["departcode"].ToString();
					dept[1]=rs["departname"].ToString();
					dept[2]=rs["parentcode"].ToString();
					array.Add(dept);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getDepts");
				throw;
			}
		}
        
		public static ArrayList getDepts()
		{
			ArrayList array=new ArrayList();
			string sql="select departcode,departname,parentcode from depart";
			
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] dept=new string[3];
					dept[0]=rs["departcode"].ToString();
					dept[1]=rs["departname"].ToString();
					dept[2]=rs["parentcode"].ToString();
					array.Add(dept);
				}
				rs.Close();
				return array;
			
		}
        
		/**
		 *取得所有角色信息
		**/
		public static ArrayList getRoles(int userId,string areacode)
		{
			ArrayList array=new ArrayList();

			string sql="select * from role order by areacode,rolename desc";
			if(!areacode.Equals("4403"))
			{
				sql="select * from role where areacode='"+areacode+"' order by areacode,rolename desc";
			}
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] role=new string[4];
					role[0]=rs["rolecode"].ToString();
					role[1]=rs["rolename"].ToString();
					role[2]=changeNull(rs["step"].ToString());
					role[3]=changeNull(rs["areacode"].ToString());
					array.Add(role);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getRoles");
				throw;
			}
		}
        
		/**
		 *取得所有已存在的group信息
		**/
		public static ArrayList getGroups(int userId)
		{
			ArrayList array=new ArrayList();
			string sql="select * from szgroup";
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] group=new string[3];
					group[0]=rs["groupcode"].ToString();
					group[1]=rs["groupname"].ToString();
					group[2]=changeNull(rs["memo"].ToString());
				
					array.Add(group);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getGroups");
				throw;
			}
		}
        
		public static Hashtable getRoleInfoByID(int id,int userId)
		{
			Hashtable table=new Hashtable();
			string sql="select * from role where rolecode="+id;
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string rolename=changeNull(Convert.ToString(rs["rolename"]));
					table.Add("rolename",rolename);
					string step=changeNull(Convert.ToString(rs["step"]));
					table.Add("step",step);
					string areacode=changeNull(Convert.ToString(rs["areacode"]));
					table.Add("areacode",areacode);
					string memo=changeNull(Convert.ToString(rs["memo"]));
					table.Add("memo",memo);
					string flag=changeNull(Convert.ToString(rs["flag"]));
					table.Add("flag",flag);

				}
				rs.Close();
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
					NAMESPACE_PATH + "getRoleInfoByID");
				throw;
			}
			
		}
		/**
		 *根据部门ID取得此部门的信息
		 *id 部门ID 
		**/
		public static string[] getDeptInfoByID(int id,int userId)
		{
			string sql="getDepartInfoByID";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;
				SqlParameter[] output=new SqlParameter[6];
				output[0]=new SqlParameter("@departname",SqlDbType.VarChar,128);
				output[0].Direction=ParameterDirection.Output;
				output[1]=new SqlParameter("@parentcode",SqlDbType.Int);
				output[1].Direction=ParameterDirection.Output;
				output[2]=new SqlParameter("@address",SqlDbType.VarChar,1024);
				output[2].Direction=ParameterDirection.Output;
				output[3]=new SqlParameter("@areacode",SqlDbType.VarChar,30);
				output[3].Direction=ParameterDirection.Output;
				output[4]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				output[4].Direction=ParameterDirection.Output;
				output[5]=new SqlParameter("@parentname",SqlDbType.VarChar,128);
				output[5].Direction=ParameterDirection.Output;
				return DataAccess.ExecuteStoreProcedure1(sql,input,output).Split(';');
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getDeptInfoByID");
				throw;
			}
		}

		//向depart表插入新记录
		public static int insertToDepart(string[] values,int userId)
		{
			string sql="insert into depart(departname,parentcode,departadress,area,memo) values(@name,@parentcode,@address,@area,@memo)";
			//string sql="insertToDepart";
			try
			{
				SqlParameter[] input=new SqlParameter[5];
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=values[0];
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@parentcode",SqlDbType.Int);
				input[1].Value=Convert.ToInt32(values[1]);
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@address",SqlDbType.VarChar,1024);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@area",SqlDbType.VarChar,30);
				input[3].Value=values[3];
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
	
				int i=DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertToDepart");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "insertToDepart");
				throw;
			}
		}
		//向depart表更新记录
		public static int updateToDepart(int id,string[] values,int userId)
		{
			string sql="update depart set departname=@name,parentcode=@parentcode,departadress=@address,area=@area,memo=@memo where departcode=@id";
			//string sql="updateToDepart";
			try
			{
				SqlParameter[] input = new SqlParameter[6];
				
				input[0]=new SqlParameter("@name",SqlDbType.VarChar,128);
				input[0].Value=values[0];
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@parentcode",SqlDbType.Int);
				input[1].Value=Convert.ToInt32(values[1]);
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@address",SqlDbType.VarChar,1024);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@area",SqlDbType.VarChar,30);
				input[3].Value=values[3];
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@id",SqlDbType.Int);
				input[5].Value=id;
				input[5].Direction=ParameterDirection.Input;
				
			
				int i=DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.updateToDepart");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updateToDepart");
				throw;
			}
			
		}
		//根据ID删除depart表记录
		public static int deleteFromDepart(int id,int userId)
		{
			string sql="deleteFromDepart";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				SqlParameter[] output=new SqlParameter[1];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.deleteFromDepart");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "deleteFromDepart");
				throw;
			}
			
		}

		//根据用户ID取得用户所有信息
		public static string[] getUserInfoByID(int id,int userId)
		{
            string sql="select a.*,b.departname from loginuser a,depart b where a.departcode=b.departcode and usercode="+id;
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				string[] values=new string[24];
				while(rs.Read())
				{   
					values[0]=rs["loginname"].ToString();
					values[1]=rs["username"].ToString();
					values[2]=rs["password"].ToString();
					values[3]=rs["departcode"].ToString();
					values[4]=changeNull(rs["priweb"].ToString());
					values[5]=changeNull(rs["pubweb"].ToString());
					values[6]=changeNull(rs["tel"].ToString());
					values[7]=changeNull(rs["hometel"].ToString());
					values[8]=changeNull(rs["officefax"].ToString());
					values[9]=changeNull(rs["zipcode"].ToString());
					values[10]=changeNull(rs["mobile"].ToString());
					values[11]=changeNull(rs["mobile2"].ToString());
					values[12]=changeNull(rs["email"].ToString());
					values[13]=changeNull(rs["address"].ToString());
					values[14]=changeNull(rs["sex"].ToString());
					values[15]=changeNull(rs["areacode"].ToString());
					values[16]=changeNull(rs["birthday"].ToString());
					values[17]=changeNull(rs["photo"].ToString());
					values[18]=changeNull(rs["memo"].ToString());
					values[19]=rs["departname"].ToString();
					values[20]=changeNull(rs["centerusercode"].ToString());
					values[21]=changeNull(rs["videolevel"].ToString());

				}
				rs.Close();
				sql="select a.rolecode rolecode,b.rolename rolename from user_role a,role b where a.rolecode=b.rolecode and a.usercode="+id;
				rs=DataAccess.ExecuteReader(sql,null);
				StringBuilder ids=new StringBuilder();
				StringBuilder names=new StringBuilder();
				while(rs.Read())
				{
					ids.Append(rs["rolecode"].ToString()+",");
					names.Append(rs["rolename"].ToString()+",");
				}
				rs.Close();
				//roleids,rolenames
				if(ids.Length>0)
				{
					values[22]=ids.ToString().Substring(0,ids.Length-1);
					values[23]=names.ToString().Substring(0,names.Length-1);
				}
				else
				{
					values[22]="";
					values[23]="";
				}
				return values;
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
					NAMESPACE_PATH + "getUserInfoByID");
				throw;
			}
		}
		
		//向用户表添加记录
		public static int insertToLoginUser(string[] values,int userId)
		{
			string sql="insertToLoginUser";
			try
			{
				SqlParameter[] input=new SqlParameter[24];
				input[0]=new SqlParameter("@loginname",SqlDbType.VarChar,18);
				input[0].Value=values[0];
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@username",SqlDbType.VarChar,18);
				input[1].Value=values[1];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@password",SqlDbType.VarChar,18);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@departcode",SqlDbType.Int);
				input[3].Value=Convert.ToInt32(values[3]);
				input[4]=new SqlParameter("@regdate",SqlDbType.DateTime);
				input[4].Value=DateTime.Now;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@priweb",SqlDbType.VarChar,128);
				input[5].Value=values[4];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@pubweb",SqlDbType.VarChar,128);
				input[6].Value=values[5];
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@tel",SqlDbType.VarChar,18);
				input[7].Value=values[6];
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[8].Value=values[7];
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@officefax",SqlDbType.Char,18);
				input[9].Value=values[8];
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@zipcode",SqlDbType.VarChar,18);
				input[10].Value=values[9];
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@mobile",SqlDbType.VarChar,18);
				input[11].Value=values[10];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@mobile2",SqlDbType.VarChar,18);
				input[12].Value=values[11];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@email",SqlDbType.VarChar,128);
				input[13].Value=values[12];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@address",SqlDbType.VarChar,128);
				input[14].Value=values[13];
				input[14].Direction=ParameterDirection.Input;
				input[15]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[15].Value=Convert.ToChar(values[14]);
				input[15].Direction=ParameterDirection.Input;
				input[16]=new SqlParameter("@areacode",SqlDbType.VarChar,18);
				input[16].Value=values[15];
				input[16].Direction=ParameterDirection.Input;
				input[17]=new SqlParameter("@birthday",SqlDbType.VarChar,18);
				input[17].Value=values[16];
				input[17].Direction=ParameterDirection.Input;
				input[18]=new SqlParameter("@ca",SqlDbType.VarChar,8);
				input[18].Value="";
				input[18].Direction=ParameterDirection.Input;
				input[19]=new SqlParameter("@photo",SqlDbType.VarChar,255);
				input[19].Value=values[17];
				input[19].Direction=ParameterDirection.Input;
				input[20]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[20].Value=values[18];
				input[20].Direction=ParameterDirection.Input;
				input[21]=new SqlParameter("@roleid",SqlDbType.VarChar,255);
				input[21].Value=values[19];
				input[21].Direction=ParameterDirection.Input;
				input[22]=new SqlParameter("@centerusercode",SqlDbType.VarChar,18);
				input[22].Value=values[20];
				input[22].Direction=ParameterDirection.Input;
				input[23]=new SqlParameter("@videolevel",SqlDbType.VarChar,9);
				input[23].Value=values[21];
				input[23].Direction=ParameterDirection.Input;
				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertToLoginUser");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "insertToLoginUser");
				throw;
			}
		}

		//更新用户表的记录
		public static int updateToLoginUser(int id,string[] values,string delRoleID,string addRoleID,int userId)
		{


			string sql="updateToLoginUser";
			try
			{
				SqlParameter[] input=new SqlParameter[25];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@loginname",SqlDbType.VarChar,18);
				input[1].Value=values[0];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@username",SqlDbType.VarChar,18);
				input[2].Value=values[1];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@password",SqlDbType.VarChar,18);
				input[3].Value=values[2];
				input[4]=new SqlParameter("@departcode",SqlDbType.Int);
				input[4].Value=Convert.ToInt32(values[3]);
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@priweb",SqlDbType.VarChar,128);
				input[5].Value=values[4];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@pubweb",SqlDbType.VarChar,128);
				input[6].Value=values[5];
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@tel",SqlDbType.VarChar,18);
				input[7].Value=values[6];
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[8].Value=values[7];
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@officefax",SqlDbType.Char,18);
				input[9].Value=values[8];
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@zipcode",SqlDbType.VarChar,18);
				input[10].Value=values[9];
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@mobile",SqlDbType.VarChar,18);
				input[11].Value=values[10];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@mobile2",SqlDbType.VarChar,18);
				input[12].Value=values[11];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@email",SqlDbType.VarChar,128);
				input[13].Value=values[12];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@address",SqlDbType.VarChar,128);
				input[14].Value=values[13];
				input[14].Direction=ParameterDirection.Input;
				input[15]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[15].Value=Convert.ToChar(values[14]);
				input[15].Direction=ParameterDirection.Input;
				input[16]=new SqlParameter("@areacode",SqlDbType.VarChar,18);
				input[16].Value=values[15];
				input[16].Direction=ParameterDirection.Input;
				input[17]=new SqlParameter("@birthday",SqlDbType.VarChar,18);
				input[17].Value=values[16];
				input[17].Direction=ParameterDirection.Input;
				input[18]=new SqlParameter("@ca",SqlDbType.VarChar,8);
				input[18].Value="";
				input[18].Direction=ParameterDirection.Input;
				input[19]=new SqlParameter("@photo",SqlDbType.VarChar,255);
				input[19].Value=values[17];
				input[19].Direction=ParameterDirection.Input;
				input[20]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[20].Value=values[18];
				input[20].Direction=ParameterDirection.Input;
				input[21]=new SqlParameter("@delroleid",SqlDbType.VarChar,255);
				input[21].Value=delRoleID;
				input[21].Direction=ParameterDirection.Input;
				input[22]=new SqlParameter("@addroleid",SqlDbType.VarChar,255);
				input[22].Value=addRoleID;
				input[22].Direction=ParameterDirection.Input;
				input[23]=new SqlParameter("@centerusercode",SqlDbType.VarChar,18);
				input[23].Value=values[20];
				input[23].Direction=ParameterDirection.Input;
				input[24]=new SqlParameter("@videolevel",SqlDbType.VarChar,9);
				input[24].Value=values[21];
				input[24].Direction=ParameterDirection.Input;
				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.updateToLoginUser");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updateToLoginUser");
				throw;
			}
		}
        //根据ID删除用户表记录
		public static int deleteFromLoginUser(int id,int userId)
		{
			string sql="deleteFromLoginUser";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				SqlParameter[] output=new SqlParameter[1];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.deleteFromLoginUser");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "deleteFromLoginUser");
				throw;
			}
		}
 
		public static bool insertToRole(Hashtable table,int userId)
		{
			string sql="insert into role(rolename,step,areacode,memo,flag) values(@rolename,@step,@areacode,@memo,@flag)";
			try
			{
				SqlParameter[] input=new SqlParameter[5];
				input[0]=new SqlParameter("@rolename",SqlDbType.VarChar,100);
				input[0].Value=table["rolename"].ToString();
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@step",SqlDbType.Int);
				input[1].Value=Convert.ToInt32(table["step"]);
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@areacode",SqlDbType.VarChar,32);
				input[2].Value=table["areacode"].ToString();
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[3].Value=table["memo"].ToString();
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@flag",SqlDbType.Char,1);
				input[4].Value=Convert.ToChar(table["_flag"].ToString());
				input[4].Direction=ParameterDirection.Input;
				bool b=true;
				if(DataAccess.ExecuteNonQuery(sql,input)!=1)
				{
					b= false;
				}
				if(b)
				{
					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertToRole");
				}

				return b;
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
					NAMESPACE_PATH + "insertToRole");
				throw;
			}
			
		}

		public static bool updateToRole(Hashtable table,int id,int userId)
		{
			string sql="update role set rolename=@rolename,step=@step,areacode=@areacode,memo=@memo,flag=@flag where rolecode=@id";
			try
			{
				SqlParameter[] input=new SqlParameter[6];
				input[0]=new SqlParameter("@rolename",SqlDbType.VarChar,100);
				input[0].Value=table["rolename"].ToString();
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@step",SqlDbType.Int);
				input[1].Value=Convert.ToInt32(table["step"]);
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@areacode",SqlDbType.VarChar,32);
				input[2].Value=table["areacode"].ToString();
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[3].Value=table["memo"].ToString();
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@flag",SqlDbType.Char,1);
				input[4].Value=Convert.ToChar(table["_flag"].ToString());
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@id",SqlDbType.Int);
				input[5].Value=id;
				input[5].Direction=ParameterDirection.Input;
				bool b=true;
				if(DataAccess.ExecuteNonQuery(sql,input)!=1)
				{
					b= false;
				}
				if(b)
				{
					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.updateToRole");
				}

				return b;
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
					NAMESPACE_PATH + "updateToRole");
				throw;
			}
			
		}

		//根据ID删除角色表记录
		public static int deleteFromRole(int id,int userId)
		{
			string sql="deleteFromRole";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				SqlParameter[] output=new SqlParameter[1];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.deleteFromRole");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "deleteFromRole");
				throw;
			}

		}
		//向群组表添加记录
//		public static int insertToGroup(string[] values,int currentUserID,DateTime loginTime)
//		{
//			string sql="insertToGroup";
//			SqlParameter[] input=new SqlParameter[2];
//			SqlParameter[] output=new SqlParameter[1];
//			input[0]=new SqlParameter("@name",SqlDbType.VarChar,18);
//			input[0].Value=values[0];
//			input[0].Direction=ParameterDirection.Input;
//			input[1]=new SqlParameter("@memo",SqlDbType.VarChar,225);
//			input[1].Value=values[1];
//			input[1].Direction=ParameterDirection.Input;
//			output[0]=new SqlParameter("@result",SqlDbType.Int);
//			output[0].Direction=ParameterDirection.Output;
//			
//			int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
//
//			BASE_logmanageservice.writeUserLog(currentUserID,loginTime,loginTime,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertToGroup");
//
//			return i;
//		}
		//根据ID更新群组表记录
//		public static int updateToGroup(int id,string[] values)
//		{
//			string sql="updateToGroup";
//			SqlParameter[] input=new SqlParameter[3];
//			SqlParameter[] output=new SqlParameter[1];
//			input[0]=new SqlParameter("@id",SqlDbType.Int);
//			input[0].Value=id;
//			input[0].Direction=ParameterDirection.Input;
//			input[1]=new SqlParameter("@name",SqlDbType.VarChar,18);
//			input[1].Value=values[0];
//			input[1].Direction=ParameterDirection.Input;
//			input[2]=new SqlParameter("@memo",SqlDbType.VarChar,255);
//			input[2].Value=values[1];
//			input[2].Direction=ParameterDirection.Input;
//			output[0]=new SqlParameter("@result",SqlDbType.Int);
//			output[0].Direction=ParameterDirection.Output;
//			return Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
//		}
		//根据ID删除群组表记录
//		public static int deleteFromGroup(int id)
//		{
//			string sql="deleteFromGroup";
//			SqlParameter[] input=new SqlParameter[1];
//			SqlParameter[] output=new SqlParameter[1];
//			input[0]=new SqlParameter("@id",SqlDbType.Int);
//			input[0].Value=id;
//			input[0].Direction=ParameterDirection.Input;
//			output[0]=new SqlParameter("@result",SqlDbType.Int);
//			output[0].Direction=ParameterDirection.Output;
//			return Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
//		}

		//把"null"转换为""
        public static string changeNull(string str)
		{
			if(str.ToLower().Equals("null"))
				str="";
            return str;
		}

		public static bool checkDeptName(string name,int id,int userId)
		{
			name=name.Replace("'","");
			string sql="select count(*) from depart where parentcode="+id+" and departname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
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
					NAMESPACE_PATH + "checkDeptName");
				throw;
			}
		}

		public static bool checkRoleName(string name,int userId)
		{
			name=name.Replace("'","");
			string sql="select count(*) from role where rolename='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
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
					NAMESPACE_PATH + "checkRoleName");
				throw;
			}

		}

		public static bool checkLoginName(string name,int userId)
		{
			name=name.Replace("'","");
			string sql="select count(*) from loginuser where loginname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
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
					NAMESPACE_PATH + "checkLoginName");
				throw;
			}

		}

		public static bool checkGroupName(string name,int userId)
		{
			name=name.Replace("'","");
			string sql="select count(*) from szgroup where groupname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
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
					NAMESPACE_PATH + "checkGroupName");
				throw;
			}
		}

		public static int getMinParentDepartID(int userId)
		{
			string sql="select min(parentcode) from depart";
			try
			{

				return Convert.ToInt32(DataAccess.ExecuteScalar(sql,null));
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
					NAMESPACE_PATH + "getMinParentDepartID");
				throw;
			}
		}

		public static int getMinParentDepartID()
		{
			string sql="select min(parentcode) from depart";
			

				return Convert.ToInt32(DataAccess.ExecuteScalar(sql,null));
			
		}


		public static ArrayList getUserGroup(int userId)
		{
			ArrayList list=new ArrayList();
			string sql="select a.id id,a.usercode usercode,a.groupcode,a.groupname groupname, b.username username from user_group a inner join loginuser b on a.usercode=b.usercode ";
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] group=new string[5];
					group[0]=rs["id"].ToString();
					group[1]=rs["usercode"].ToString();
					group[2]=rs["groupcode"].ToString();
					group[3]=rs["groupname"].ToString();
					group[4]=rs["username"].ToString();
					list.Add(group);
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getUserGroup");
				throw;
			}
		}

		public static ArrayList getUserGrouped(int userId)
		{
			ArrayList list=new ArrayList();
			string sql="select * from user_grouped";
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] grouped=new string[4];
					grouped[0]=rs["id"].ToString();
					grouped[1]=rs["fid"].ToString();
					grouped[2]=rs["usercode"].ToString();
					grouped[3]=rs["username"].ToString();
					list.Add(grouped);
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getUserGrouped");
				throw;
			}
		}

//		public static string[] getGroupedUserInfo(int id)
//		{
//			string sql="select a.username,a.loginname,b.departname,a.tel from loginuser a inner join depart b on a.departcode=b.departcode and usercode="+id;
//			string[] user=new string[5];
//			SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
//			while(rs.Read())
//			{				
//				user[0]=Convert.ToString(id);
//				user[1]=rs.GetString(0);
//				user[2]=rs.GetString(1);
//				user[3]=rs.GetString(2);
//				user[4]=rs.GetSqlString(3).ToString();
//				user[4]=changeNull(user[4]);
//			}
//			rs.Close();
//			return user;
//		}



		public static ArrayList getGroupedUserCode(int id,int userId)
		{
			ArrayList list=new ArrayList();
			string sql="select usercode,username from user_grouped where fid="+id;
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] user=new string[2];
					user[0]=rs["usercode"].ToString();
					user[1]=rs["username"].ToString();
					list.Add(user);

				}
				rs.Close();
			
				return list;
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
					NAMESPACE_PATH + "getGroupedUserCode");
				throw;
			}
		}


		public static void deleteFromUserGrouped(int groupID,int userId)
		{
			string sql="delete user_grouped where fid="+groupID;
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.deleteFromUserGrouped");
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
					NAMESPACE_PATH + "deleteFromUserGrouped");
				throw;
			}
		}

			public static bool insertIntoUserGrouped(int groupID,string userCode,string userName,int userId)
			{
				string sql="insert into user_grouped values(@groupID,@userCode,@userName)";
				try
				{
					SqlParameter[] input=new SqlParameter[3];
					input[0]=new SqlParameter("@groupID",SqlDbType.Int);
					input[0].Value=groupID;
					input[0].Direction=ParameterDirection.Input;
					input[1]=new SqlParameter("@userCode",SqlDbType.VarChar,18);
					input[1].Value=userCode;
					input[1].Direction=ParameterDirection.Input;
					input[2]=new SqlParameter("@userName",SqlDbType.VarChar,18);
					input[2].Value=userName;
					input[2].Direction=ParameterDirection.Input;
					bool b=true;
					if(DataAccess.ExecuteNonQuery(sql,input)!=1)
					{
						b= false;
					}
					if(b)
					{
						BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertIntoUserGrouped");
					}

					return b;
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
						NAMESPACE_PATH + "insertIntoUserGrouped");
					throw;
				}
			
		}

		public static bool insertIntoUserGroup(int groupCode,int creater,string groupName,int userId)
		{
			string sql="insert into user_group values(@creater,@groupcode,@groupName)";
			try
			{
				SqlParameter[] input=new SqlParameter[3];
				input[0]=new SqlParameter("@creater",SqlDbType.Int);
				input[0].Value=creater;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@groupcode",SqlDbType.Int);
				input[1].Value=groupCode;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[2].Value=groupName;
				input[2].Direction=ParameterDirection.Input;
				bool b=true;
				if(DataAccess.ExecuteNonQuery(sql,input)!=1)
				{
					b= false;
				}
				if(b)
				{
					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertIntoUserGroup");
				}

				return b;
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
					NAMESPACE_PATH + "insertIntoUserGroup");
				throw;
			}
			
		}

		public static int getGroupID(string groupName,int userId)
		{
			string sql="select id from user_group where groupname=@groupName";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[0].Value=groupName;
				input[0].Direction=ParameterDirection.Input;
				return Convert.ToInt32(DataAccess.ExecuteScalar(sql,input));
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
					NAMESPACE_PATH + "getGroupID");
				throw;
			}
			
		}

		public static bool checkUserGroupName(string groupName,int userId)
		{
			string sql="select count(*) from user_group where groupname=@groupName";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[0].Value=groupName;
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


		public static int deleteGroup(int groupID,int userId)
		{
			string sql="DeleteGroup";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@groupID",SqlDbType.Int);
				input[0].Value=groupID;
				input[0].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;

				int i= Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.deleteGroup");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "deleteGroup");
				throw;
			}
		}

		public static bool updateGroup(int id,string groupName,int userId)
		{
			string sql="update user_group set groupname=@groupName where id=@id";
			try
			{
				SqlParameter[] input=new SqlParameter[2];
				input[0]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[0].Value=groupName;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@id",SqlDbType.Int);
				input[1].Value=id;
				input[1].Direction=ParameterDirection.Input;
				bool b=false;
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
				{
					b= true;
				}

				if(b)
				{

					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.updateGroup");
				}
				return b;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "updateGroup");
				throw;
			}
		}


		public static ArrayList[] getUserByDeptID(int departID,int pageIndex,int pageSize,int returnRecordCount,int userId,string name,string loginname,string departname )
		{
			string sql="getDepartTree";
			try
			{
				SqlParameter[] input=new SqlParameter[7];
				input[0]=new SqlParameter("@parentCode",SqlDbType.Int);
				input[0].Value=departID;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@PageIndex",SqlDbType.Int);
				input[1].Value=pageIndex;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@PageSize",SqlDbType.Int);
				input[2].Value=pageSize;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@ReturnRecordCount",SqlDbType.Bit);
				input[3].Value=returnRecordCount;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@name",SqlDbType.VarChar,18);
				input[4].Value=name;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@loginname",SqlDbType.VarChar,18);
				input[5].Value=loginname;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@departname",SqlDbType.VarChar,128);
				input[6].Value=departname;
				input[6].Direction=ParameterDirection.Input;
				SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
				ArrayList[] list=new ArrayList[2];
				list[0]=new ArrayList();
				list[1]=new ArrayList();
				while(rs.Read())
				{
					User user=new User();
					user.UserCode=(int)rs["usercode"];
					user.UserName=rs["username"].ToString();
					user.LoginName=rs["loginname"].ToString();
					user.Sex=rs["sex"].ToString();
					user.Tel=changeNull(rs["tel"].ToString());
					user.Mobile=changeNull(rs["mobile"].ToString());
					user.DepartName=rs["departname"].ToString();
					list[0].Add(user);
				}
				if(rs.NextResult())
				{
					rs.Read();
					list[1].Add(rs["recordcount"].ToString());
				}
				rs.Close();
				return list;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getUserByDeptID");
				throw;
			}
		}

		public static bool uploadfile(byte[] b,string fileName,string filePath)
		{
			return FileUpload.uploadFile(b,fileName,filePath);
		}

		public static ArrayList getAreas(int userId,string areacode)
		{
			string sql="select areacode,areaname from area";
			if(!areacode.Equals("4403"))
			{
				sql="select areacode,areaname from area where areacode='"+areacode+"'";
			}
			try
			{
				ArrayList list=new ArrayList();
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] area=new string[2];
					area[0]=rs["areacode"].ToString();
					area[1]=rs["areaname"].ToString();
					list.Add(area);
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getAreas");
				throw;
			}

		}

		public static ArrayList getSteps(int userId)
		{
			string sql="select stepname,stepcode from step";
			try
			{
				ArrayList list=new ArrayList();
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] area=new string[2];
					area[0]=rs["stepname"].ToString();
					area[1]=rs["stepcode"].ToString();
					list.Add(area);
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getSteps");
				throw;
			}

		}


		public static ArrayList[] getUserByRoleID(int roleCode,int pageIndex,int pageSize,int returnRecordCount,int userId )
		{
			string sql="getUsersFromRoleID";
			try
			{
				SqlParameter[] input=new SqlParameter[4];
				input[0]=new SqlParameter("@roleCode",SqlDbType.Int);
				input[0].Value=roleCode;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@PageIndex",SqlDbType.Int);
				input[1].Value=pageIndex;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@PageSize",SqlDbType.Int);
				input[2].Value=pageSize;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@ReturnRecordCount",SqlDbType.Bit);
				input[3].Value=returnRecordCount;
				input[3].Direction=ParameterDirection.Input;
				SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
				ArrayList[] list=new ArrayList[2];
				list[0]=new ArrayList();
				list[1]=new ArrayList();
				while(rs.Read())
				{
					User user=new User();
					user.UserCode=(int)rs["usercode"];
					user.UserName=rs["username"].ToString();
					user.LoginName=rs["loginname"].ToString();
					user.Sex=rs["sex"].ToString();
					user.Tel=changeNull(rs["tel"].ToString());
					user.Mobile=changeNull(rs["mobile"].ToString());
					user.DepartName=rs["departname"].ToString();
					list[0].Add(user);
				}
				if(rs.NextResult())
				{
					rs.Read();
					list[1].Add(rs["recordcount"].ToString());
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getUserByRoleID");
				throw;
			}
		}

	   
		[AjaxMethod]
		public string insertIntoUserGroup1(int groupCode,int creater,string groupName,string userIDs,string userNames,int userId)
		{
			string sql="select count(*) from user_group where groupname=@groupName";
				
			
		
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[0].Value=groupName;
				input[0].Direction=ParameterDirection.Input;
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,input))>0)
				{
					return "0";
				}

                
				sql="insert into user_group values(@creater,@groupcode,@groupName)";
				 input=new SqlParameter[3];
				input[0]=new SqlParameter("@creater",SqlDbType.Int);
				input[0].Value=creater;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@groupcode",SqlDbType.Int);
				input[1].Value=groupCode;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[2].Value=groupName;
				input[2].Direction=ParameterDirection.Input;
				bool b=true;
				if(DataAccess.ExecuteNonQuery(sql,input)!=1)
				{
					b= false;
				}
				if(b)
				{
					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertIntoUserGroup");
				}
				else
				{
					return "1";
				}


				sql="select id from user_group where groupname=@groupName";
				input=new SqlParameter[1];
				input[0]=new SqlParameter("@groupName",SqlDbType.VarChar,18);
				input[0].Value=groupName;
				input[0].Direction=ParameterDirection.Input;
				int _id= Convert.ToInt32(DataAccess.ExecuteScalar(sql,input));

                string[] ids=userIDs.Split(',');
				string[] names=userNames.Split(',');
				for(int i=0;i<ids.Length;i++)
				{

					sql="insert into user_grouped values(@groupID,@userCode,@userName)";
					input=new SqlParameter[3];
					input[0]=new SqlParameter("@groupID",SqlDbType.Int);
					input[0].Value=_id;
					input[0].Direction=ParameterDirection.Input;
					input[1]=new SqlParameter("@userCode",SqlDbType.VarChar,18);
					input[1].Value=ids[i];
					input[1].Direction=ParameterDirection.Input;
					input[2]=new SqlParameter("@userName",SqlDbType.VarChar,18);
					input[2].Value=names[i];
					input[2].Direction=ParameterDirection.Input;
					 b=true;
					if(DataAccess.ExecuteNonQuery(sql,input)!=1)
					{
						b= false;
					}
					if(b)
					{
						BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10101(),sql,"szcg.com.teamax.szbase.organize.BASE_orghelper.insertIntoUserGrouped");
					}
					else
					{
						return "1";
					}
				}
				return "2";

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
					NAMESPACE_PATH + "insertIntoUserGroup");
				throw;
			}
		}
		
			

	}
}
