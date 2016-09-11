/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���Թ��ĺ�֪ʶ���߼����������������á�
 * �ṹ��ɣ�
 * ��    �ߣ���ΰ
 * �������ڣ�2007-06-08
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace bacgBL.web.szbase.repositoryandarchives
{
    public class BASE_archivesmanage
    {
        public BASE_archivesmanage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.systemsetting.SystemSetting";


        /// <summary>
        /// �ӹ��ı���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">��</param>
        /// <returns>�б���أ�����ID�����ı���</returns>
        public ArrayList getAllArchives()
        {
             try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getAllArchives ();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// �ӹ��ı���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">����</param>
        /// <returns>�б���أ�����ID�����ı���</returns>
        public ArrayList getAllArchives1(string sele)
        {

            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getAllArchives1(sele);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// �ӹ��ı���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>Hashtable�б���أ�����ID�����ߣ����ı��⣬����</returns>
        public Hashtable getArchivesInfoByID(int id)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getArchivesInfoByID(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        

        }
        /// <summary>
        /// �ӹ��ı���ɾ������ 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>�б���أ�����ID�����ı���</returns>

        public bool deleteFromDocument(int id)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.deleteFromDocument(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
  
        }

        /// <summary>
        /// �ӹ��ı��в������� 
        /// </summary>
        /// <param name="argUserCode">title��content��author��system.now</param>
        /// <returns>�б���أ�����ID�����ı���</returns>
        public bool insertIntoDocument(string title, string content, string author)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.insertIntoDocument( title,  content,  author);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
  
        }

        /// <summary>
        /// ��֪ʶ����и����������� 
        /// </summary>
        /// <param name="argUserCode">���⣬���ݣ����ߣ�id</param>
        /// <returns>�б����</returns>
        public bool updateIntoDocument(string title, string content, string author, int id)
        {
            /////string sql="update document set title=@title,content=@content,author=@author,cu_date=@cu_date where id=@id";
            //string sql = "UPDATE S_DOCUMENT SET TITLE=@title,CONTENT=@content,AUTHOR=@author,CU_DATE=@cu_date WHERE ID=@id";
            //SqlParameter[] input = new SqlParameter[5];
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
            //input[4] = new SqlParameter("@id", SqlDbType.Int);
            //input[4].Value = id;
            //input[4].Direction = ParameterDirection.Input;
            //if (Convert.ToInt32(DataAccess.ExecuteNonQuery(sql, input)) > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //string sql = "UPDATE S_DOCUMENT SET TITLE='" + title + "',CONTENT='" + content
            //           + "',AUTHOR='" + author + "',CU_DATE='" + DateTime.Now + "' WHERE ID='"
            //           + id + "'";
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.updateIntoDocument(title, content, author,id );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ��֪ʶ����в�ѯ�������� 
        /// </summary>
        /// <param name="argUserCode">��</param>
        /// <returns>�б����</returns>

        public ArrayList getAllRepository()
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getAllRepository();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }
        /// <summary>
        /// ��֪ʶ����в�ѯ�������� 
        /// </summary>
        /// <param name="argUserCode">name</param>
        /// <returns>�б����</returns>
        public ArrayList getAllRepository1(string sele)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getAllRepository1(sele);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        
        }
        /// <summary>
        /// ��֪ʶ����в�ѯ�������� 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>�б����</returns>
        public Hashtable getRepositoryInfoByID(int id)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getRepositoryInfoByID(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
            
        }

        /// <summary>
        /// ��֪ʶ�����ɾ���������� 
        /// </summary>
        /// <param name="argUserCode">id</param>
        /// <returns>�б����</returns>
        public bool deleteFromRepository(int id)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.deleteFromRepository(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
            
        }
        /// <summary>
        /// ��֪ʶ����в����������� 
        /// </summary>
        /// <param name="argUserCode">name��pid��desc��ʱ��</param>
        /// <returns>�б����</returns>
        public bool insertIntoRepository(string name, int pid, string desc)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.insertIntoRepository(name,pid,desc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
    
        }

        /// <summary>
        /// ��֪ʶ����и����������� 
        /// </summary>
        /// <param name="argUserCode">name��pid��desc��id</param>
        /// <returns>�б����</returns>
        public bool updateIntoRepository(string name, int pid, string desc, int id)
        {
            ///string sql="update repository set pid=@pid,name=@name,cu_date=@cu_date,_desc=@_desc where id=@id";
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
            //if (Convert.ToInt32(DataAccess.ExecuteNonQuery(sql, input)) > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
         
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.updateIntoRepository(name,pid ,desc ,id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
           

        }
        /// <summary>
        /// ��֪ʶ����в�ѯ��С��pid���� 
        /// </summary>
        /// <param name="argUserCode">name��pid��desc��id</param>
        /// <returns>�б����</returns>
        public int getMinParentID()
        {
            ///string sql="select min(pid) from repository";
            //string sql = "SELECT MIN(PID) FROM S_REPOSITORY";
            //return Convert.ToInt32(DataAccess.ExecuteScalar(sql, null));
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getMinParentID();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }

        /// <summary>
        /// ��֪ʶ����в�ѯ���� 
        /// </summary>
        /// <param name="argUserCode">name��pid��desc��id</param>
        /// <returns>�б����</returns>
        public string getPidName(int id)
        {
            ///string sql="select name from repository where id=@id";
            //string sql = "SELECT NAME FROM S_REPOSITORY WHERE ID=@id";
            //SqlParameter[] input = new SqlParameter[1];
            //input[0] = new SqlParameter("@id", SqlDbType.Int);
            //input[0].Value = id;
            //input[0].Direction = ParameterDirection.Input;
            //Hashtable table = new Hashtable();
            //SqlDataReader rs = DataAccess.ExecuteReader(sql, input);
            //string name = "";
            //while (rs.Read())
            //{
            //    name = rs["name"].ToString();
            //}
            //rs.Close();
            //return name;
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
                {
                    return dl.getPidName(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
   

    
    