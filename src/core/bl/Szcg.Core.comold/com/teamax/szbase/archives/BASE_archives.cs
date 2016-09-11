using System;
using System.Collections;
using szcg.com.teamax.util;
using System.Data.SqlClient;
using System.Data;
namespace szcg.com.teamax.szbase.archives
{
	/// <summary>
	/// BASE_archives 的摘要说明。
	/// </summary>
	public class BASE_archives
	{
		public BASE_archives()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public ArrayList getAllArchives()
		{
			string sql="select id,title from document";
			ArrayList list=new ArrayList();
			SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			while(rs.Read())
			{
				string[] values=new string[2];
				values[0]=rs["id"].ToString();
				values[1]=rs["title"].ToString();
				list.Add(values);

			}
			rs.Close();
			return list;
		}
		public ArrayList getAllArchives1(string sele)
		{
			string sql="select id,title from document where title like'%"+sele+"%'";
			ArrayList list=new ArrayList();
			SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			while(rs.Read())
			{
				string[] values=new string[2];
				values[0]=rs["id"].ToString();
				values[1]=rs["title"].ToString();
				list.Add(values);

			}
			rs.Close();
			return list;
		}

		public Hashtable getArchivesInfoByID(int id)
		{
			string sql="select id,author,title,content from document where id=@id";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@id",SqlDbType.Int);
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
			Hashtable table=new Hashtable();
			SqlDataReader rs=DataAccess.ExecuteReader(sql,input);
			while(rs.Read())
			{
				string _id=rs["id"].ToString();
				string author=rs["author"].ToString();
				string title=rs["title"].ToString();
				string content=rs["content"].ToString();
				table.Add("id",_id);
				table.Add("author",author);
				table.Add("title",title);
				table.Add("content",content);

			}
			rs.Close();
			return table;

		}

		public bool deleteFromDocument(int id)
		{
			string sql="delete document where id=@id";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@id",SqlDbType.Int);
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool insertIntoDocument(string title,string content,string author)
		{
			string sql="insert into document(title,content,author,cu_date) values(@title,@content,@author,@cu_date)";
			SqlParameter[] input=new SqlParameter[4];
			input[0]=new SqlParameter("@title",SqlDbType.VarChar,128);
			input[0].Value=title;
			input[0].Direction=ParameterDirection.Input;
			input[1]=new SqlParameter("@content",SqlDbType.VarChar,4096);
			input[1].Value=content;
			input[1].Direction=ParameterDirection.Input;
			input[2]=new SqlParameter("@author",SqlDbType.VarChar,128);
			input[2].Value=author;
			input[2].Direction=ParameterDirection.Input;
			input[3]=new SqlParameter("@cu_date",SqlDbType.DateTime);
			input[3].Value=DateTime.Now;
			input[3].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
			

		}


		public bool updateIntoDocument(string title,string content,string author,int id)
		{
			string sql="update document set title=@title,content=@content,author=@author,cu_date=@cu_date where id=@id";
			SqlParameter[] input=new SqlParameter[5];
			input[0]=new SqlParameter("@title",SqlDbType.VarChar,128);
			input[0].Value=title;
			input[0].Direction=ParameterDirection.Input;
			input[1]=new SqlParameter("@content",SqlDbType.VarChar,4096);
			input[1].Value=content;
			input[1].Direction=ParameterDirection.Input;
			input[2]=new SqlParameter("@author",SqlDbType.VarChar,128);
			input[2].Value=author;
			input[2].Direction=ParameterDirection.Input;
			input[3]=new SqlParameter("@cu_date",SqlDbType.DateTime);
			input[3].Value=DateTime.Now;
			input[3].Direction=ParameterDirection.Input;
			input[4]=new SqlParameter("@id",SqlDbType.Int);
			input[4].Value=id;
			input[4].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}

		public ArrayList getAllRepository()
		{
			string sql="select id,pid,name from repository";
			ArrayList list=new ArrayList();
			SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			while(rs.Read())
			{
				string[] values=new string[3];
				values[0]=rs["id"].ToString();
				values[1]=rs["pid"].ToString();
				values[2]=rs["name"].ToString();
				list.Add(values);

			}
			rs.Close();
			return list;

		}
		public ArrayList getAllRepository1(string sele)
		{
			string sql="select id,pid,name from repository where name like'%"+sele+"%'";
			ArrayList list=new ArrayList();
			SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			while(rs.Read())
			{
				string[] values=new string[3];
				values[0]=rs["id"].ToString();
				values[1]=rs["pid"].ToString();
				values[2]=rs["name"].ToString();
				list.Add(values);

			}
			rs.Close();
			return list;
		}

		public Hashtable getRepositoryInfoByID(int id)
		{
			string sql="select id,pid,name,_desc from repository where id=@id";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@id",SqlDbType.Int);
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
			Hashtable table=new Hashtable();
			SqlDataReader rs=DataAccess.ExecuteReader(sql,input);
			while(rs.Read())
			{
				string _id=rs["id"].ToString();
				string pid=rs["pid"].ToString();
				string name=rs["name"].ToString();
				string desc=rs["_desc"].ToString();
				table.Add("id",_id);
				table.Add("pid",pid);
				table.Add("name",name);
				table.Add("desc",desc);

			}
			rs.Close();
			return table;

		}

		public bool deleteFromRepository(int id)
		{
			string sql="delete repository where id=@id";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@id",SqlDbType.Int);
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool insertIntoRepository(string name,int pid,string desc)
		{
			string sql="insert into repository(pid,name,cu_date,_desc) values(@pid,@name,@cu_date,@_desc)";
			SqlParameter[] input=new SqlParameter[4];
			input[0]=new SqlParameter("@pid",SqlDbType.Int);
			input[0].Value=pid;
			input[0].Direction=ParameterDirection.Input;
			input[1]=new SqlParameter("@name",SqlDbType.VarChar,128);
			input[1].Value=name;
			input[1].Direction=ParameterDirection.Input;
			input[2]=new SqlParameter("@cu_date",SqlDbType.DateTime);
			input[2].Value=DateTime.Now;
			input[2].Direction=ParameterDirection.Input;
			input[3]=new SqlParameter("@_desc",SqlDbType.VarChar,4096);
			input[3].Value=desc;
			input[3].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
			

		}


		public bool updateIntoRepository(string name,int pid,string desc,int id)
		{
			string sql="update repository set pid=@pid,name=@name,cu_date=@cu_date,_desc=@_desc where id=@id";
			SqlParameter[] input=new SqlParameter[5];
			input[0]=new SqlParameter("@pid",SqlDbType.Int);
			input[0].Value=pid;
			input[0].Direction=ParameterDirection.Input;
			input[1]=new SqlParameter("@name",SqlDbType.VarChar,128);
			input[1].Value=name;
			input[1].Direction=ParameterDirection.Input;
			input[2]=new SqlParameter("@cu_date",SqlDbType.DateTime);
			input[2].Value=DateTime.Now;
			input[2].Direction=ParameterDirection.Input;
			input[3]=new SqlParameter("@_desc",SqlDbType.VarChar,4096);
			input[3].Value=desc;
			input[3].Direction=ParameterDirection.Input;
			input[4]=new SqlParameter("@id",SqlDbType.Int);
			input[4].Value=id;
			input[4].Direction=ParameterDirection.Input;
			if(Convert.ToInt32(DataAccess.ExecuteNonQuery(sql,input))>0)
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}

		public int getMinParentID()
		{
			string sql="select min(pid) from repository";
			return Convert.ToInt32(DataAccess.ExecuteScalar(sql,null));
			
		}


		public string getPidName(int id)
		{
			string sql="select name from repository where id=@id";
			SqlParameter[] input=new SqlParameter[1];
			input[0]=new SqlParameter("@id",SqlDbType.Int);
			input[0].Value=id;
			input[0].Direction=ParameterDirection.Input;
			Hashtable table=new Hashtable();
			SqlDataReader rs=DataAccess.ExecuteReader(sql,input);
			string name="";
			while(rs.Read())
			{
				name=rs["name"].ToString();
			}
			rs.Close();
			return name;

		}
	}
}
