using System;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Data;
using System.Text;
using System.Collections;
using szcg.com.teamax.szbase.collecter;
using szcg.com.teamax.szbase.entity;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.collecter
{
	/// <summary>
	/// collecter 的摘要说明。
	/// </summary>
	public class BASE_collecterhelper
{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.collecter.BASE_collecterhelper.";
		public BASE_collecterhelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public string getCommName(int id,int userId)
		{
			string sql="select commname from community where id="+id;
			try
			{
				
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
				string name="";
				while(rs.Read())
				{
					name=rs["commname"].ToString();
				}
				rs.Close();
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
					NAMESPACE_PATH + "getCommName");
				throw;
			}

		}
		public ArrayList GetFirstLevel(int id,string type,int userId,string areacode)
		{
			ArrayList list = new ArrayList(); 
			string sql = "select id,areaname from area";
			if(!areacode.Equals("4403"))
			{
				sql = "select id,areaname from area where areacode='"+areacode+"'";
			}
			try
			{
				if(id>0)
				{
					if(type.Equals("area"))
					{
						sql = "select id,areaname from area where id="+id;
					}
					else if(type.Equals("street"))
					{
						sql = "select id ,streetname from street where id="+id;
					}
				}
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] values = new string[2];
					values[0] = rs.GetInt32(0).ToString();
					values[1] = rs.GetString(1);
					list.Add(values);
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
					NAMESPACE_PATH + "GetFirstLevel");
				throw;
			}
		}

		public ArrayList getSecondLevel(int id,string type,int userId)
		{
			ArrayList list=new ArrayList();
			string sql="select id,fid,streetname from street";
			try
			{
				if(id>0)
				{
					if(type.Equals("area"))
					{
						sql = "select id ,fid,streetname from street where fid="+id;
					}
				
				}
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] values = new string[3];
					values[0] = rs["id"].ToString();
					values[1] = rs["fid"].ToString();
					values[2] = rs["streetname"].ToString();
					list.Add(values);
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
					NAMESPACE_PATH + "getSecondLevel");
				throw;
			}

		}


		public ArrayList getThirdLevel(int id,string type,int userId)
		{
			ArrayList list=new ArrayList();
			string sql = "select * from community";
			try
			{
				if(id>0)
				{
					if(type.Equals("area"))
					{
						sql = "select a.id id,a.fid fid,a.commname commname from community a inner join street b on a.fid=b.id and b.fid="+id;
					}
					if(type.Equals("street"))
					{
						sql = "select id, fid, commname from community where fid="+id;
					}
				}
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] values = new string[3];
					values[0] = rs["id"].ToString();
					values[1] = rs["fid"].ToString();
					values[2] = rs["commname"].ToString();
					list.Add(values);
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
					NAMESPACE_PATH + "getThirdLevel");
				throw;
			}
		}

		

		public string[] getCollecterInfoByID(int id,int userId)
		{
			string[] info=new string[16];
			string sql="select a.collcode collcode,a.commcode commcode,a.gridcode gridcode,a.numbercode numbercode,a.collname collname,a.loginname loginname,a.pwd pwd,a.sex sex,a.mobile mobile,a.age age,a.url url,a.hometel hometel,a.homeaddress homeaddress,a.timeout timeout,a.memo memo, b.commname commname from collecter a inner join community b on a.commcode=b.id and a.collcode="+id; 
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					info[0]=rs["collcode"].ToString();
					info[1]=rs["commcode"].ToString();//
					info[2]=changeNull(rs["gridcode"].ToString());//gridcode
					info[3]=changeNull(rs["numbercode"].ToString());//numbercode
					info[4]=changeNull(rs["collname"].ToString());//collname
					info[5]=changeNull(rs["loginname"].ToString());//loginname
					info[6]=changeNull(rs["pwd"].ToString());//pwd
					info[7]=changeNull(rs["sex"].ToString());//sex
					info[8]=changeNull(rs["mobile"].ToString());//mobile
					info[9]=changeNull(rs["age"].ToString());//age
					info[10]=changeNull(rs["url"].ToString());//url
					info[11]=changeNull(rs["hometel"].ToString());//hometel
					info[12]=changeNull(rs["homeaddress"].ToString());//homeaddress
					info[13]=changeNull(rs["timeout"].ToString());
					info[14]=changeNull(rs["memo"].ToString());
					info[15]=rs["commname"].ToString();
				}

				rs.Close();
				return info;
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
					NAMESPACE_PATH + "getCollecterInfoByID");
				throw;
			}
		}
        
		public int deleteFromCollecter(int id,int userId)
		{
            string sql="DeleteCollecter";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@id",SqlDbType.Int);
				input[0].Value=id;
				input[0].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.deleteFromCollecter");

				return i;
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
					NAMESPACE_PATH + "deleteFromCollecter");
				throw;
			}
			
		}

		public int insertIntoCollecter(string[] values,int userId)
		{
			string sql="InsertToCollecter";
			try
			{
				SqlParameter[] input=new SqlParameter[15];
				input[0]=new SqlParameter("@commcode",SqlDbType.Int);
				input[0].Value=Convert.ToInt32(values[0]);
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@gridcode",SqlDbType.VarChar,18);
				input[1].Value=values[1];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@numbercode",SqlDbType.VarChar,64);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@collname",SqlDbType.VarChar,18);
				input[3].Value=values[3];
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@loginname",SqlDbType.VarChar,128);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@pwd",SqlDbType.VarChar,128);
				input[5].Value=values[5];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[6].Value=Convert.ToChar(values[6]);
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@mobile",SqlDbType.VarChar,18);
				input[7].Value=values[7];
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@age",SqlDbType.VarChar,18);
				input[8].Value=values[8];
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@url",SqlDbType.VarChar,255);
				input[9].Value=values[9];
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[10].Value=values[10];
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@homeaddress",SqlDbType.VarChar,1024);
				input[11].Value=values[11];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@timeout",SqlDbType.Int);
				input[12].Value=values[12];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@memo",SqlDbType.VarChar,256);
				input[13].Value=values[13];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@isguard",SqlDbType.Char,1);
				input[14].Value='0';
				input[14].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;
			

				int i = Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.insertIntoCollecter");
			
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
					NAMESPACE_PATH + "insertIntoCollecter");
				throw;
			}


		}

		public int updateToCollecter(string[] values,int id,int userId)
		{
			string sql="UpdateToCollecter";
			try
			{
				SqlParameter[] input=new SqlParameter[15];
				input[0]=new SqlParameter("@commcode",SqlDbType.Int);
				input[0].Value=Convert.ToInt32(values[0]);
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@gridcode",SqlDbType.VarChar,18);
				input[1].Value=values[1];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@numbercode",SqlDbType.VarChar,64);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@collname",SqlDbType.VarChar,18);
				input[3].Value=values[3];
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@loginname",SqlDbType.VarChar,128);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@pwd",SqlDbType.VarChar,128);
				input[5].Value=values[5];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[6].Value=Convert.ToChar(values[6]);
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@mobile",SqlDbType.VarChar,18);
				input[7].Value=values[7];
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@age",SqlDbType.VarChar,18);
				input[8].Value=values[8];
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@url",SqlDbType.VarChar,255);
				input[9].Value=values[9];
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[10].Value=values[10];
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@homeaddress",SqlDbType.VarChar,1024);
				input[11].Value=values[11];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@timeout",SqlDbType.Int);
				input[12].Value=values[12];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@memo",SqlDbType.VarChar,256);
				input[13].Value=values[13];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@id",SqlDbType.Int);
				input[14].Value=id;
				input[14].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;

				int i = Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.insertIntoCollecter");
			
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
					NAMESPACE_PATH + "updateToCollecter");
				throw;
			}
		}

		//把"null"转换为""
		public string changeNull(string str)
		{
			if(str.ToLower().Equals("null"))
				str="";
			return str;
		}

		public ArrayList getCollecter(int userId)
		{
			ArrayList array=new ArrayList(); 
			string sql="select collcode,collname,commcode from collecter";
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] coll=new string[3];
					coll[0]=rs["collcode"].ToString();
					coll[1]=rs["collname"].ToString();
					coll[2]=rs["commcode"].ToString();
					array.Add(coll);
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
					NAMESPACE_PATH + "getCollecter");
				throw;
			}
		}

		public string getGrids(int id,int userId)
		{
			StringBuilder sb=new StringBuilder();
			string sql="select gridcode from grid where commfid="+id;
			try
			{
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					sb.Append(rs.GetString(0)+",");
				}
				rs.Close();
				if(sb.Length>0)
				{
					return sb.ToString().Substring(0,sb.Length-1);
				}
				return "";
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
					NAMESPACE_PATH + "getGrids");
				throw;
			}

		}

		public ArrayList[] GetAllCollecter(string type, int id, int pageIndex, int pageSize, int returnRecordCount,int userId,string name,string loginname,string gridcode) 
		{
			string sql="GetAllCollecter";
			try
			{
				SqlParameter[] input=new SqlParameter[8];
				input[0]=new SqlParameter("@Type",SqlDbType.VarChar,20);
				input[0].Value=type;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@ID",SqlDbType.Int);
				input[1].Value=id;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@PageIndex",SqlDbType.Int);
				input[2].Value=pageIndex;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@PageSize",SqlDbType.Int);
				input[3].Value=pageSize;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@ReturnRecordCount",SqlDbType.Bit);
				input[4].Value=returnRecordCount;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@name",SqlDbType.VarChar,18);
				input[5].Value=name;
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@loginname",SqlDbType.VarChar,128);
				input[6].Value=loginname;
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@gridcode",SqlDbType.VarChar,18);
				input[7].Value=gridcode;
				input[7].Direction=ParameterDirection.Input;
				SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
				ArrayList[] list=new ArrayList[2];
				list[0]=new ArrayList();
				list[1]=new ArrayList();
				while(rs.Read())
				{
					Collecter coll = new Collecter();
					coll.CollCode = (int) rs["collcode"];
					coll.GridCode = (string) rs["gridcode"];
					coll.CollName = (string) rs["collname"];
					coll.LoginName = (string) rs["loginname"];
					coll.Mobile = changeNull(rs["mobile"].ToString());
					coll.Tel = changeNull(rs["hometel"].ToString());
					coll.Address = changeNull(rs["homeaddress"].ToString());
					list[0].Add(coll);
				}
				if(returnRecordCount==1 && rs.NextResult())
				{
					rs.Read();
					list[1].Add(rs[0].ToString());
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
					NAMESPACE_PATH + "GetAllCollecter");
				throw;
			}


		}
        
		public bool checkLoginName(string loginName,int userId)
		{
			string sql="select count(*) from collecter  where loginname=@loginname";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@loginname",SqlDbType.VarChar,32);
				input[0].Value=loginName;
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
		
		public Hashtable getLawerInfoByID(int id,int userId)
		{
			string sql="select * from lawer where lawercode="+id;
			try
			{
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
				Hashtable table = new Hashtable(); 
				if(rs!=null)
				{
					while(rs.Read())
					{
						string commcode=changeNull(rs["commcode"].ToString());
						table.Add("commcode",commcode);
						string gridcode=changeNull(rs["gridcode"].ToString());
						table.Add("gridcode",gridcode);
						string numbercode=changeNull(rs["numbercode"].ToString());
						table.Add("numbercode",numbercode);
						string lawername=rs["lawername"].ToString();
						table.Add("lawername",lawername);
						string loginname=rs["loginname"].ToString();
						table.Add("loginname",loginname);
						string pwd=rs["pwd"].ToString();
						table.Add("pwd",pwd);
						string url=changeNull(rs["url"].ToString());
						table.Add("url",url);
						string timeout=changeNull(rs["timeout"].ToString());
						table.Add("timeout",timeout);
						string mobile=changeNull(rs["mobile"].ToString());
						table.Add("mobile",mobile);
						string sex=changeNull(rs["sex"].ToString());
						table.Add("sex",sex);
						string age=changeNull(rs["age"].ToString());
						table.Add("age",age);
						string hometel=changeNull(rs["hometel"].ToString());
						table.Add("hometel",hometel);
						string homeaddress=changeNull(rs["homeaddress"].ToString());
						table.Add("homeaddress",homeaddress);
						string memo=changeNull(rs["memo"].ToString());
						table.Add("memo",memo);

					}
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
					NAMESPACE_PATH + "getLawerInfoByID");
				throw;
			}
		}

		public ArrayList[] getAllLawers(int pageIndex,int pageSize,int returnRecordCount,int userId,string name,string loginname,string gridcode)
		{
			string sql="GetAllLawer";
			try
			{
				SqlParameter[] input=new SqlParameter[6];
				input[0]=new SqlParameter("@PageIndex",SqlDbType.Int);
				input[0].Value=pageIndex;
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@PageSize",SqlDbType.Int);
				input[1].Value=pageSize;
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@ReturnRecordCount",SqlDbType.Int);
				input[2].Value=returnRecordCount;
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@name",SqlDbType.VarChar,18);
				input[3].Value=name;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@loginname",SqlDbType.VarChar,128);
				input[4].Value=loginname;
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@gridcode",SqlDbType.VarChar,18);
				input[5].Value=gridcode;
				input[5].Direction=ParameterDirection.Input;
				SqlDataReader rs = DataAccess.ExecuteStoredProcedure2(sql,input);
				ArrayList[] list=new ArrayList[2];
				list[0]=new ArrayList();
				list[1]=new ArrayList();
				while(rs.Read())
				{
					Lawer lawer=new Lawer();
					lawer.LawerCode=rs["lawercode"].ToString();
					lawer.GridCode=changeNull(rs["gridcode"].ToString());
					lawer.LawerName=rs["lawername"].ToString();
					lawer.LoginName=rs["loginname"].ToString();
					lawer.Mobile=changeNull(rs["mobile"].ToString());
					lawer.Tel=changeNull(rs["hometel"].ToString());
					lawer.Address=changeNull(rs["homeaddress"].ToString());

					list[0].Add(lawer);

				}
				if(returnRecordCount==1 && rs.NextResult())
				{
					rs.Read();
					list[1].Add(rs[0].ToString());
				}
				rs.Close();
				return list;
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
					NAMESPACE_PATH + "getAllLawers");
				throw;
			}

		}

		public bool insertIntoLawer(string[] values,int userId)
		{
			string sql="insert into lawer(commcode,gridcode,numbercode,lawername,loginname,pwd,url,cu_date,timeout,mobile,sex,age,hometel,homeaddress,memo) values(@commcode,@gridcode,@numbercode,@lawername,@loginname,@pwd,@url,@cu_date,@timeout,@mobile,@sex,@age,@hometel,@homeaddress,@memo)";
			try
			{
				SqlParameter[] input=new SqlParameter[15];
				input[0]=new SqlParameter("@commcode",SqlDbType.VarChar,32);
				input[0].Value=values[0];
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@gridcode",SqlDbType.VarChar,32);
				input[1].Value=values[1];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@numbercode",SqlDbType.VarChar,32);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@lawername",SqlDbType.VarChar,32);
				input[3].Value=values[3];
				input[4]=new SqlParameter("@loginname",SqlDbType.VarChar,32);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@pwd",SqlDbType.VarChar,32);
				input[5].Value=values[5];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@url",SqlDbType.VarChar,255);
				input[6].Value=values[6];
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@cu_date",SqlDbType.DateTime);
				input[7].Value=DateTime.Now;
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@timeout",SqlDbType.Int);
				input[8].Value=Convert.ToInt32(values[7]);
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@mobile",SqlDbType.VarChar,32);
				input[9].Value=values[8];
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[10].Value=Convert.ToChar(values[9]);
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@age",SqlDbType.VarChar,18);
				input[11].Value=values[10];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[12].Value=values[11];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@homeaddress",SqlDbType.VarChar,1024);
				input[13].Value=values[12];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[14].Value=values[13];
				input[14].Direction=ParameterDirection.Input;

				int i=DataAccess.ExecuteNonQuery(sql,input);

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.insertIntoLawer");
			
				if(i>0)
				{
					return true;
				}
				else
				{
					return false;
				}
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
					NAMESPACE_PATH + "insertIntoLawer");
				throw;
			}
		}

		public bool updateIntoLawer(string[] values,int id,int userId)
		{
			string sql="update lawer set commcode=@commcode,gridcode=@gridcode,numbercode=@numbercode,lawername=@lawername,loginname=@loginname,pwd=@pwd,url=@url,timeout=@timeout,mobile=@mobile,sex=@sex,age=@age,hometel=@hometel,homeaddress=@homeaddress,memo=@memo where lawercode=@id";
			try
			{
				SqlParameter[] input=new SqlParameter[15];
				input[0]=new SqlParameter("@commcode",SqlDbType.VarChar,32);
				input[0].Value=values[0];
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@gridcode",SqlDbType.VarChar,32);
				input[1].Value=values[1];
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@numbercode",SqlDbType.VarChar,32);
				input[2].Value=values[2];
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@lawername",SqlDbType.VarChar,32);
				input[3].Value=values[3];
				input[4]=new SqlParameter("@loginname",SqlDbType.VarChar,32);
				input[4].Value=values[4];
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@pwd",SqlDbType.VarChar,32);
				input[5].Value=values[5];
				input[5].Direction=ParameterDirection.Input;
				input[6]=new SqlParameter("@url",SqlDbType.VarChar,255);
				input[6].Value=values[6];
				input[6].Direction=ParameterDirection.Input;
				input[7]=new SqlParameter("@timeout",SqlDbType.Int);
				input[7].Value=Convert.ToInt32(values[7]);
				input[7].Direction=ParameterDirection.Input;
				input[8]=new SqlParameter("@mobile",SqlDbType.VarChar,32);
				input[8].Value=values[8];
				input[8].Direction=ParameterDirection.Input;
				input[9]=new SqlParameter("@sex",SqlDbType.Char,1);
				input[9].Value=Convert.ToChar(values[9]);
				input[9].Direction=ParameterDirection.Input;
				input[10]=new SqlParameter("@age",SqlDbType.VarChar,18);
				input[10].Value=values[10];
				input[10].Direction=ParameterDirection.Input;
				input[11]=new SqlParameter("@hometel",SqlDbType.VarChar,18);
				input[11].Value=values[11];
				input[11].Direction=ParameterDirection.Input;
				input[12]=new SqlParameter("@homeaddress",SqlDbType.VarChar,1024);
				input[12].Value=values[12];
				input[12].Direction=ParameterDirection.Input;
				input[13]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[13].Value=values[13];
				input[13].Direction=ParameterDirection.Input;
				input[14]=new SqlParameter("@id",SqlDbType.Int);
				input[14].Value=id;
				input[14].Direction=ParameterDirection.Input;

				int i=DataAccess.ExecuteNonQuery(sql,input);
				
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.updateIntoLawer");
				if(i>0)
				{
					return true;
				}
				else
				{
					return false;
				}
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
					NAMESPACE_PATH + "updateIntoLawer");
				throw;
			}
		}

		public bool deleteFromLawer(int id,int userId)
		{
			string sql="delete lawer where lawercode="+id;
			try
			{
				int i=DataAccess.ExecuteNonQuery(sql,null);
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getSystem_ZCPT(),sql,"szcg.com.teamax.szbase.collecter.BASE_collecterhelper.deleteFromLawer");
				if(i>0)
				{
					return true;
				}
				else
				{
					return false;
				}
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
					NAMESPACE_PATH + "deleteFromLawer");
				throw;
			}
			

		}

		public bool checkLawerName(string name,int userId)
		{
			string sql="select count(*) from lawer where loginname=@loginname";
			try
			{
				SqlParameter[] input=new SqlParameter[1];
				input[0]=new SqlParameter("@loginname",SqlDbType.VarChar,32);
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
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "checkLawerName");
				throw;
			}
			
			
		}

		public string getAreaID(string areacode)
		{
			string sql="select id from area where areacode=@areacode";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@areacode",SqlDbType.Int);
			input[0].Value=Convert.ToInt32(areacode);
			input[0].Direction=ParameterDirection.Input;
			return DataAccess.ExecuteScalar(sql,input).ToString();
		}


		
	}
}
