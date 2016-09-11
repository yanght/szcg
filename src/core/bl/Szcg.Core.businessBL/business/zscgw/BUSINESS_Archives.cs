using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;
using szcg.com.teamax.util;

namespace szcg.com.teamax.business.archives
{
	/// <summary>
	/// BUSINESS_Archive 的摘要说明。
	/// </summary>
	public class BUSINESS_Archives
	{
		protected StringBuilder sb;
		protected SqlDataReader dr;
		protected ArrayList list;

		public BUSINESS_Archives()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//发布公文
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
            string c = Convert.ToString(DataAccess.ExecuteStoreProcedure1("insertArchive", input, output));
			return c;
		}

		//公文查看
		public String[] getArchive()
		{
            string sql = "select * from s_document order by cu_date desc ";
			dr = DataAccess.ExecuteReader(sql,null);
			list = new ArrayList();
			while(dr.Read())
			{
				sb = new StringBuilder();
				sb.Append("id="+Convert.ToDecimal(dr["id"])+"$");
				sb.Append("title="+Convert.ToString(dr["title"])+"$");
				sb.Append("author="+Convert.ToString(dr["author"])+"$");
				sb.Append("content="+Convert.ToString(dr["content"])+"$");
				sb.Append("cu_date="+Convert.ToString(dr["cu_date"]));
				list.Add(sb.ToString());
			}
			dr.Close();
			return (String[])(list.ToArray(System.Type.GetType("System.String")));
		}

		//pda查询公文
		public String[] getArchivePda(string title,string datenow)
		{
			string sql = "";
			if(!title.Equals("")&&!datenow.Equals(""))
			{
				sql = "select * from s_document where title like '%"+title+"%'";
			}
			else if(!title.Equals(""))
			{
				sql = "select * from s_document where title like '%"+title+"%'";
			}
			else if(!datenow.Equals(""))
			{
				sql = "select * from s_document where cu_date";
			}
			dr = DataAccess.ExecuteReader(sql,null);
			list = new ArrayList();
			while(dr.Read())
			{
				sb = new StringBuilder();
				sb.Append("id="+Convert.ToDecimal(dr["id"])+"$");
				sb.Append("title="+Convert.ToString(dr["title"])+"$");
				sb.Append("author="+Convert.ToString(dr["author"])+"$");
				sb.Append("content="+Convert.ToString(dr["content"])+"$");
				sb.Append("cu_date="+Convert.ToString(dr["cu_date"]));
				list.Add(sb.ToString());
			}
			dr.Close();
			return (String[])(list.ToArray(System.Type.GetType("System.String")));
		}

		//根据ID查公文
		public String getArchives(string id)
		{
			SqlParameter[] input = new SqlParameter[1];
			input[0] = new SqlParameter("@id",SqlDbType.VarChar,10);
	//		SqlParameter[] output= new SqlParameter[1];
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
            dr = DataAccess.ExecuteStoredProcedure2("getArchives", input);
			while(dr.Read())
			{
				sb = new StringBuilder();
				sb.Append("title="+Convert.ToString(dr["title"])+"$");
				sb.Append("author="+Convert.ToString(dr["author"])+"$");
				sb.Append("content="+Convert.ToString(dr["content"])+"$");
				sb.Append("cu_date="+Convert.ToString(dr["cu_date"]));
			}
			dr.Close();
			return sb.ToString();
		}
		//得到部门
		public String[] getDepart()
		{
			String sql = "select * from p_depart";
			SqlDataReader reader = DataAccess.ExecuteReader(sql,null);
			if(reader==null)
			{
				reader.Close();
				return null;
			}

			String[] topic = null;			
			ArrayList list = new ArrayList();
			while(reader.Read())
			{				
				StringBuilder sb = new StringBuilder();
				sb.Append("id="+Convert.ToString(reader["departcode"])+"$");
				sb.Append("departname="+Convert.ToString(reader["departname"])+"$");
				sb.Append("parentcode="+Convert.ToString(reader["parentcode"])+"$");
				sb.Append("memo="+Convert.ToString(reader["memo"]));
				list.Add(sb.ToString());
			}
			reader.Close();

			topic = (String[])(list.ToArray(System.Type.GetType("System.String")));
			return topic;
		}
        /// <summary>
        /// 获取电话号码,返回输入满足条件的电话号码
        /// </summary>
		/// <param name="areacode">区号</param>
		/// <param name="departcode">部门号</param>
        /// <param name="name">名字</param>
        /// <param name="phone">电话号码</param>
        /// <returns></returns>
		public SqlDataReader getPeoInfo(string areacode,string departcode,string name,string phone)
		{
			try
			{
                string sql = string.Format(@"select A.username,A.tel,A.mobile,A.hometel,B.departname from p_user A left join p_depart B
                                      on A.departcode = B.departcode
                                      where B.area = '{0}' and A.username like '%{1}%' and A.mobile like '%{2}%' 
                                      and A.departcode = '{3}'",areacode,name,phone,departcode);
				dr=DataAccess.ExecuteReader(sql,null);
				if(dr==null)
				{
					return null;
				}
				else
				{
					return dr;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		  
		}
		//号码查询
		public string[] getPeoInfo(string name,string phone)
		{
			string sql = "select * from personinfo ";
			if(name!="" && phone!="")
			{
				sql += " where name like '%"+name+"%' or officetel like '%"+phone+"%' or mobiletel like '%"+phone+"%' or officete2 like '%"+phone+"%'" + 
					" or mobiletel2 like '"+phone+"' or homenumber like '%"+phone+"%'";
			}
			else if(phone!="")
			{
				sql += " where officetel like '%"+phone+"%' or mobiletel like '%"+phone+"%' or officete2 like '%"+phone+"%'" + 
					" or mobiletel2 like '"+phone+"' or homenumber like '%"+phone+"%'";
			}
			else if(name!="")
			{
				sql += " where name like '%"+name+"%'";
			}

			string[] peoInfo = null;
			ArrayList list = new ArrayList();
			SqlDataReader dr = DataAccess.ExecuteReader(sql,null);
			
			while(dr.Read())
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(Convert.ToString(dr["name"]) + "$");
				if(!Convert.ToString(dr["officetel"]).Equals(""))
				{
					sb.Append(Convert.ToString(dr["officetel"]) + "$");
				}
				else //if(!Convert.ToString(dr["officete2"]).Equals(""))
				{
					sb.Append(Convert.ToString(dr["officete2"]) + "$");
				}
				if(!Convert.ToString(dr["mobiletel"]).Equals(""))
				{
					sb.Append(Convert.ToString(dr["mobiletel"]) + "$");
				}
				else //if(!Convert.ToString(dr["mobiletel2"]).Equals(""))
				{
					sb.Append(Convert.ToString(dr["mobiletel2"]) + "$");
				}
				sb.Append(Convert.ToString(dr["homenumber"]) + "$");
				sb.Append(Convert.ToString(dr["duty"]));
				list.Add(sb.ToString());
			}
			dr.Close();

			peoInfo = (string[])(list.ToArray(System.Type.GetType("System.String")));
			return peoInfo;
		}
	}
}
