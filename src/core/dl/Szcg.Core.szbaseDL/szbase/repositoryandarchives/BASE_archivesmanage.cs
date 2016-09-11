/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：对公文和知识库管理数据操作层类。对ADO.NET进行简单封装，方便使用。
 * 结构组成：
 * 作    者：何伟
 * 创建日期：2007-06-08
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;
using System.Data.SqlClient;

namespace bacgDL.szbase.repositoryandarchives
{
    public class BASE_archivesmanage : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// 从公文表中取数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：公文ID，公文标题</returns>
        public ArrayList getAllArchives()
        {
            ArrayList list = new ArrayList();
            try
            {
                //string sql="select id,title from document";
                string sql = "SELECT ID,TITLE FROM S_DOCUMENT";
                IDataReader dr = ExecuteReader(sql, CommandType.Text, true);
                while (dr.Read())
                {
                    string[] values = new string[2];
                    values[0] = dr["id"].ToString();
                    values[1] = dr["title"].ToString();
                    list.Add(values);

                }
                dr.Close();
                return list;

            }
            catch (Exception err)
            {
                throw err;
            }

        }



        /// <summary>
        /// 从公文表中取数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：公文ID，公文标题</returns>
        public ArrayList getAllArchives1(string sele)
        {
            ArrayList list = new ArrayList();
            try
            {
                ///string sql="select id,title from sdocument where title like'%"+sele+"%'";
                string sql = "SELECT ID,TITLE FROM S_DOCUMENT WHERE TITLE LIKE '%" + sele + "%'";
                IDataReader dr = ExecuteReader(sql, CommandType.Text, true);

                while (dr.Read())
                {
                    string[] values = new string[2];
                    values[0] = dr["id"].ToString();
                    values[1] = dr["title"].ToString();
                    list.Add(values);

                }
                dr.Close();
                return list;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 从公文表中取数据 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>Hashtable列表相关：公文ID，作者，公文标题，内容</returns>
        public Hashtable getArchivesInfoByID(int id)
        {
            SqlParameter[] input = new SqlParameter[1];
            Hashtable table = new Hashtable();
            ///string sql="select id,author,title,content from document where id=@id";
            string sql = "SELECT ID,AUTHOR,TITLE,CONTENT FROM S_DOCUMENT WHERE ID='"+id+"'";


            try
            {
                IDataReader dr = ExecuteReader(sql, CommandType.Text, true);
                while (dr.Read())
                {
                    string _id = dr["id"].ToString();
                    string author = dr["author"].ToString();
                    string title = dr["title"].ToString();
                    string content = dr["content"].ToString();
                    table.Add("id", _id);
                    table.Add("author", author);
                    table.Add("title", title);
                    table.Add("content", content);

                }
                dr.Close();
                return table;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// 从公文表中删除数据 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>Hashtable列表相关：公文ID，作者，公文标题，内容</returns>
        public bool deleteFromDocument(int id)
        {
            ///string sql="delete document where id=@id";
            string sql = "DELETE S_DOCUMENT WHERE ID='" + id + "'";
            ///SqlParameter[] input=new SqlParameter[1];DataAccess
            //SqlParameter[] input = new SqlParameter[1];
            //input[0] = new SqlParameter("@id", SqlDbType.Int);
            //input[0].Value = id;
            //input[0].Direction = ParameterDirection.Input;
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        /// <summary>
        /// 从公文表中插入数据 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>列表相关：公文ID，公文标题</returns>
        public bool insertIntoDocument(string title, string content, string author)
        {
            ///string sql="insert into document(title,content,author,cu_date) values(@title,@content,@author,@cu_date)";
            string sql = "INSERT INTO S_DOCUMENT(TITLE,CONTENT,AUTHOR,CU_DATE) VALUES ('"
                          + title + "','" + content + "','" + author + "','" + DateTime.Now + "')";
            //SqlParameter[] input = new SqlParameter[4];
            //input[0] = new SqlParameter("@title", SqlDbType.VarChar, 128);
            //input[0].Value = title;
            //input[0].Direction = ParameterDirection.Input;
            //input[1] = new SqlParameter("@content", SqlDbType.VarChar, 4096);
            //input[1].Value = content;
            //input[1].Direction = ParameterDirection.Input;
            //input[2] = new SqlParameter("@author", SqlDbType.VarChar, 128);
            //input[2].Value = author;
            //input[2].Direction = ParameterDirection.Input;
            //input[3] = new SqlParameter("@cu_date", SqlDbType.DateTime);
            //input[3].Value = DateTime.Now;
            //input[3].Direction = ParameterDirection.Input;

            //if (Convert.ToInt32(DataAccess.ExecuteNonQuery(sql, input)) > 0)
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        /// <summary>
        /// 从知识库表中更新数据数据 
        /// </summary>
        /// <param name="argUserCode">标题，内容，作者，id</param>
        /// <returns>列表相关</returns>
        public bool updateIntoDocument(string title, string content, string author, int id)
        {
            
            string sql = "UPDATE S_DOCUMENT SET TITLE='" + title + "',CONTENT='" + content
                       + "',AUTHOR='" + author + "',CU_DATE='" + DateTime.Now + "' WHERE ID='"
                       + id + "'";
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 从知识库表中得到数据 
        /// </summary>
        /// <param name="argUserCode">id，pid，name</param>
        /// <returns>列表相关</returns>
        public ArrayList getAllRepository()
        {  
            ArrayList list = new ArrayList();
            ///string sql="select id,pid,name from repository";
            string sql = "SELECT ID,PID,NAME FROM S_REPOSITORY";
            try 
            {
                IDataReader rs = ExecuteReader(sql, CommandType.Text, true);
                while (rs.Read())
                {
                    string[] values = new string[3];
                    values[0] = rs["id"].ToString();
                    values[1] = rs["pid"].ToString();
                    values[2] = rs["name"].ToString();
                    list.Add(values);

                }
                rs.Close();
                return list;
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        /// <summary>
        /// 从知识库表中查询数据数据 
        /// </summary>
        /// <param name="argUserCode">name</param>
        /// <returns>列表相关</returns>
        public ArrayList getAllRepository1(string sele)
        {
            ArrayList list = new ArrayList();
            ///string sql="select id,pid,name from repository where name like'%"+sele+"%'";
            string sql = "SELECT ID,PID,NAME FROM S_REPOSITORY WHERE NAME LIKE '%" + sele + "%'";
            
            //SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
            try
            {
                IDataReader rs = ExecuteReader(sql, CommandType.Text, true);
                while (rs.Read())
                {
                    string[] values = new string[3];
                    values[0] = rs["id"].ToString();
                    values[1] = rs["pid"].ToString();
                    values[2] = rs["name"].ToString();
                    list.Add(values);

                }
                rs.Close();
                return list;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 从知识库表中查询数据数据 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>列表相关</returns>
        public Hashtable getRepositoryInfoByID(int id)
        {
            Hashtable table = new Hashtable();
            ///string sql="select id,pid,name,_desc from repository where id=@id";
            string sql = "SELECT ID,PID,NAME,_DESC FROM S_REPOSITORY WHERE ID='" + id + "'";
            //SqlParameter[] input = new SqlParameter[1];
            //input[0] = new SqlParameter("@id", SqlDbType.Int);
            //input[0].Value = id;
            //input[0].Direction = ParameterDirection.Input;

            //SqlDataReader rs = DataAccess.ExecuteReader(sql, input);
            try
            {
                IDataReader rs = ExecuteReader(sql, CommandType.Text, true);
                while (rs.Read())
                {
                    string _id = rs["id"].ToString();
                    string pid = rs["pid"].ToString();
                    string name = rs["name"].ToString();
                    string desc = rs["_desc"].ToString();
                    table.Add("id", _id);
                    table.Add("pid", pid);
                    table.Add("name", name);
                    table.Add("desc", desc);

                }
                rs.Close();
                return table;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 从知识库表中删除数据数据 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>列表相关</returns>
        public bool deleteFromRepository(int id)
        {
            ///string sql="delete repository where id=@id";
            string sql = "DELETE S_REPOSITORY WHERE ID='"+id+"'";
            //SqlParameter[] input = new SqlParameter[1];
            //input[0] = new SqlParameter("@id", SqlDbType.Int);
            //input[0].Value = id;
            //input[0].Direction = ParameterDirection.Input;
          
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 从知识库表中插入数据数据 
        /// </summary>
        /// <param name="argUserCode">name，pid，desc，时间</param>
        /// <returns>列表相关</returns>
        public bool insertIntoRepository(string name, int pid, string desc)
        {
            ///string sql = "insert into repository(pid,name,cu_date,_desc) values(@pid,@name,@cu_date,@_desc)";
            //string sql = "INSERT INTO S_REPOSITORY(PID,NAME,CU_DATE,_DESC) VALUES (@pid,@name,@cu_date,@_desc)";
            //SqlParameter[] input = new SqlParameter[4];
            //input[0] = new SqlParameter("@pid", SqlDbType.Int);
            //input[0].Value = pid;
            //input[0].Direction = ParameterDirection.Input;
            //input[1] = new SqlParameter("@name", SqlDbType.VarChar, 128);
            //input[1].Value = name;
            //input[1].Direction = ParameterDirection.Input;
            //input[2] = new SqlParameter("@cu_date", SqlDbType.DateTime);
            //input[2].Value = DateTime.Now;
            //input[2].Direction = ParameterDirection.Input;
            //input[3] = new SqlParameter("@_desc", SqlDbType.VarChar, 4096);
            //input[3].Value = desc;
            //input[3].Direction = ParameterDirection.Input;
            string sql = "INSERT INTO S_REPOSITORY(PID,NAME,CU_DATE,_DESC) VALUES ('"
                          + pid + "','" + name + "','" + DateTime.Now + "','" + desc + "')";
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }
        /// <summary>
        /// 从知识库表中更新数据数据 
        /// </summary>
        /// <param name="argUserCode">name，pid，desc，id</param>
        /// <returns>列表相关</returns>
        public bool updateIntoRepository(string name, int pid, string desc, int id)
        {
            /////string sql="update repository set pid=@pid,name=@name,cu_date=@cu_date,_desc=@_desc where id=@id";
            //string sql = "UPDATE S_REPOSITORY SET PID=@pid,NAME=@name,CU_DATE=@cu_date,_DESC=@_desc WHERE ID=@id";
            //SqlParameter[] input = new SqlParameter[5];
            //input[0] = new SqlParameter("@pid", SqlDbType.Int);
            //input[0].Value = pid;
            //input[0].Direction = ParameterDirection.Input;
            //input[1] = new SqlParameter("@name", SqlDbType.VarChar, 128);
            //input[1].Value = name;
            //input[1].Direction = ParameterDirection.Input;
            //input[2] = new SqlParameter("@cu_date", SqlDbType.DateTime);
            //input[2].Value = DateTime.Now;
            //input[2].Direction = ParameterDirection.Input;
            //input[3] = new SqlParameter("@_desc", SqlDbType.VarChar, 4096);
            //input[3].Value = desc;
            //input[3].Direction = ParameterDirection.Input;
            //input[4] = new SqlParameter("@id", SqlDbType.Int);
            //input[4].Value = id;
            //input[4].Direction = ParameterDirection.Input;
            string sql = "UPDATE S_REPOSITORY SET PID='" +pid+"',NAME='"+name+"',CU_DATE='"+DateTime.Now
                         + "',_DESC='" + desc + "' WHERE ID='" + id + "'";
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        /// <summary>
        /// 从知识库表中查询最小的pid数据 
        /// </summary>
        /// <param name="argUserCode">name，pid，desc，id</param>
        /// <returns>列表相关</returns>
        public int getMinParentID()
        {
            ///string sql="select min(pid) from repository";
            string sql = "SELECT MIN(PID) FROM S_REPOSITORY";
            //return Convert.ToInt32(DataAccess.ExecuteScalar(sql, null));
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        /// <summary>
        /// 从知识库表中查询数据 
        /// </summary>
        /// <param name="argUserCode">name，pid，desc，id</param>
        /// <returns>列表相关</returns>
        public string getPidName(int id)
        {
            ///string sql="select name from repository where id=@id";
            string sql = "SELECT NAME FROM S_REPOSITORY WHERE ID='"+id+"'";
            //SqlParameter[] input = new SqlParameter[1];
            //input[0] = new SqlParameter("@id", SqlDbType.Int);
            //input[0].Value = id;
            //input[0].Direction = ParameterDirection.Input;
            //Hashtable table = new Hashtable();
            //SqlDataReader rs = DataAccess.ExecuteReader(sql, input);
            try
            {
                IDataReader rs = ExecuteReader(sql, CommandType.Text, true);
                string name = "";
                while (rs.Read())
                {
                    name = rs["name"].ToString();
                }
                rs.Close();
                return name;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


    }

}