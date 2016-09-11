using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;
using System.Data.SqlClient;
using System.IO;

namespace dl.zhifa
{
    public class SystemSetting : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// ȡ���ֵ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetZidiankuInfo(int pagesize)
        {
            try
            {
                String sql = "select distinct dictioncode AS code,dictioncode AS name from s_diction_sentence order by code desc";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ȡ������ģ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetWenshuInfo()
        {
            try
            {
                String sql = "select A.short_sentence,A.content, A.id from s_diction_sentence A where A.dictioncode='�������'";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int InsertWenshuData(string type, string title, string content, int filetype, int projcode, int usercode)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[] {
                                        new SqlParameter("p_type",type),
                                        new SqlParameter("p_title",title),
                                        new SqlParameter("p_content",content),
                                        new SqlParameter("p_filetype",filetype),
                                        new SqlParameter("p_projcode",projcode),
                                        new SqlParameter("p_usercode",usercode) };
                string sql = "insert into B_PROJECT_FILE(id,type,title,content,filetype,projcode,usercode) values(B_PROJECT_FILE_SEQ.nextval, :p_type, :p_title, :p_content, :p_filetype, :p_projcode, :p_usercode)";
               
                return  ExecuteNonQuery(sql, CommandType.Text, arrSP);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int InsertLaws(string lawIds,string projcode,string lawnames)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[] {
                                        new SqlParameter("p_projcode",projcode),
                                        new SqlParameter("p_lawIds",lawIds),
                                        new SqlParameter("p_lawnames",lawnames)};
                string sql = "insert into B_PROJECT_LAW(id,projcode,lawids, LAWNAMES) values(B_PROJECT_FILE_SEQ.nextval, :p_projcode, :p_lawIds,:p_lawnames)";

                return ExecuteNonQuery(sql, CommandType.Text, arrSP);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ɾ�������µ����з�������
        /// </summary>
        public int deletetLaws(string projcode)
        {
            try
            {
                string sql = "delete  b_project_law t where t.projcode='" + projcode + "'";

                return ExecuteNonQuery(sql);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �鿴�����µ����з�������
        /// </summary>
        public DataSet GetLaws(string projcode)
        {
            try
            {
                string sql = "select * from  b_project_law t where t.projcode='" + projcode + "'";

                DataSet ds = ExecuteDataset(sql);

                return ds;

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        public DataSet GetWenshuData(int projcode)
        {
            try
            {
                String sql = "select A.id,A.title from b_project_file A where A.projcode=" + projcode + " and A.filetype=0 ";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// �����б�1
        /// </summary>
        public DataSet GetWenshuData1(int projcode)
        {
            try
            {
                String sql = "select A.id,A.title from b_project_file A where A.projcode=" + projcode + " and A.filetype in (2,4)";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ������ϸ��Ϣ
        /// </summary>
        public DataSet GetWsDetail(int id)
        {
            try
            {
                String sql = "select A.id,A.title,A.content,A.addtime from b_project_file A where A.id=" + id + "";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public DataSet GetLawData(string name)
        {
            try
            {
                String sql = "select * from k_kbm t where t.parentid in (select p.id from k_kbm p where p.kbmtitle='" + name + "')";

                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ����ֵ������
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="argfid">��־������id, ������Id, �칫����Id</param>
        /// <param name="argPageIndex">ҳ���</param>
        /// <param name="argPageSize">ҳ������</param>
        /// <returns></returns>
        public DataSet GetInfoD(int userId, String argfid, int argPageIndex, int argPageSize)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("p_i_fid", argfid), 
                                new SqlParameter("p_i_PageIndex", argPageIndex), 
                                new SqlParameter("p_i_PageSize", argPageSize)};
            DataSet ds = ExecuteDataset("pr_p_getDictions", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ����ֵ����������
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="argfid">��־������id, ������Id, �칫����Id</param>
        /// <returns></returns>
        public int GetDictionRecodeCount(int userId, String argfid)
        {
            String sql = string.Format(@"SELECT COUNT(*) AS CNT FROM s_diction_sentence A, s_step C WHERE A.stepcode = C.stepcode AND A.DictionCode ='{0}'", argfid);
            IDataReader dr = ExecuteReader(sql, CommandType.Text, true);
            dr.Read();
            int recodeCount = Convert.ToInt32(dr["CNT"].ToString());
            return recodeCount;
        }

        /// <summary>
        /// ȡ�ý׶����� 
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="pagesize">ÿҳ��С</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetJieDuanInfo(int pagesize)
        {
            try
            {
                String sql = "select stepcode AS code,stepname AS name from s_step";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ����ֵ���ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <returns>DataSet</returns>
        public string[] GetDPInfo(int id)
        {
            string[] array = new string[4];
            try
            {
                string sSql = string.Format(@"select A.short_sentence,A.text_sentence,A.content, DictionCode as name from  s_diction_sentence A where A.id = {0} ", id);
                IDataReader dr = ExecuteReader(sSql , CommandType.Text, true);
                while (dr.Read())
                {
                    array[0] = dr["short_sentence"].ToString();
                    array[1] = dr["text_sentence"].ToString();
                    array[2] = dr["name"].ToString();
                    array[3] = dr["content"].ToString();
                }
                dr.Close();
            }
            catch (Exception err)
            {
                throw err;
            }
            return array;
        }

        /// <summary>
        /// �޸��ֵ���ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>DataSet</returns>
        public int UpdateInfo(int id, String dictioncode, string short_sentence, string text_sentence, string content)
        {
            try
            {
                string sql = "update s_diction_sentence set DictionCode='" + dictioncode + "', text_sentence='" + text_sentence + "', content='" + content + "', short_sentence='" + short_sentence + "' where id=" + id;
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ɾ���ֵ���Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int DeleteDiction(int userId, int id)
        {
            try
            {
                string sql = "delete s_diction_sentence where id=" + id;
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �����ֵ���Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>int</returns>
        public int InsertData(int userId, String fid, string short_sentence, string text_sentence, string content)
        {
            try
            {
                string sql = "insert into s_diction_sentence(id,DictionCode,text_sentence,short_sentence,content) values(s_diction_sentence_sequence.nextval,'" + fid + "','" + text_sentence + "','" + short_sentence + "','" + content+"')";
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int InsertFujian(int type, string title, string content, int filetype)
        {
            try
            {
                int fileID = 0;
                SqlParameter[] arrSP = new SqlParameter[] {
                                        new SqlParameter("p_id",fileID),
                                        new SqlParameter("p_type",type),
                                        new SqlParameter("p_title",title),
                                        new SqlParameter("p_content",content),
                                        new SqlParameter("p_filetype",filetype) };

                arrSP[0].Direction = ParameterDirection.Output;
                DataSet ds = ExecuteDataset("PR_B_PROJECT_FILE_INSERT", CommandType.StoredProcedure, arrSP);
                fileID = Convert.ToInt32(arrSP[0].Value);

                return fileID;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DataSet GetFujian(int projcode)
        {
            try
            {
                String sql = "select t.* from b_project_file t where t.projcode = "+projcode;
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int InsertFujianData(string fileIds, int filetype, int projcode, int usercode)
        {
            try
            {
                int result = 0;
                SqlParameter[] arrSP = new SqlParameter[] {
                                        new SqlParameter("p_fileIds",fileIds),
                                        new SqlParameter("p_filetype",filetype),
                                        new SqlParameter("p_projcode",projcode),
                                        new SqlParameter("p_usercode",usercode),
                                        new SqlParameter("p_result",SqlDbType.Int)};

                arrSP[4].Direction = ParameterDirection.Output;
                DataSet ds = ExecuteDataset("PR_B_PROJECT_FILE_UPDATE", CommandType.StoredProcedure, arrSP);
                result = Convert.ToInt32(arrSP[4].Value);

                return result;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
