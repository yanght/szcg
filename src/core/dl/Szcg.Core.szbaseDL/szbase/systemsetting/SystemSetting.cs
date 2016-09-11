using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;
using System.Data.SqlClient;

namespace bacgDL.szbase.systemsetting
{
    public class SystemSetting : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// ȡ���ֵ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetZidiankuInfo(int userId,int pagesize)
        {
            try
            {
                String sql = " select distinct dictioncode AS code,dictioncode AS name from s_diction_sentence order by code desc ";
                DataSet ds = ExecuteDataset(sql);
                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// ȡ�ÿ������� 
        /// </summary>

        public DataSet GetPhoneBookInfo()
        {
            try
            {
                String sql = "select id, officesname,remark from dbo.t_phonebook_depart where isnull(isdel,'0')=0 order by id asc";
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
                                new SqlParameter("@fid", argfid), 
                                new SqlParameter("@PageIndex", argPageIndex), 
                                new SqlParameter("@PageSize", argPageSize)};
            DataSet ds = ExecuteDataset("getDictions", CommandType.StoredProcedure, arrSP);
            return ds;
        }

        /// <summary>
        /// ��ò���ͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">����Id</param>

        /// <returns></returns>
        public DataSet GetPhoneBookInfoD(int userId)
        {
            string select = " select * from dbo.t_phonebook_Name where isnull(isDel,'0')='0' and departID = " + userId + " ORDER BY ID ASC ";

            DataSet ds = this.ExecuteDataset(select);
            return ds;
        }

        /// <summary>
        /// ���ͨ��¼������Ϣ
        /// </summary>
        /// <param name="userId">���Id</param>

        /// <returns></returns>
        public DataSet GetPhoneBookGRInfo(int id)
        {
            string select = " select a.id as id,b.officesName as officesName ,departid,name,role,officetel,insidetel,mobileTel,housetel,remarks,a.adddatetime as adddatetime from t_phonebook_Name a left join dbo.t_phonebook_depart b on a.departid = b.id  where isnull(a.isDel,'0')='0' and a.id = "+id+" ORDER BY ID ASC ";

            DataSet ds = this.ExecuteDataset(select);
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@fid", argfid)};
            IDataReader dr = ExecuteReader("getDictionsCount", CommandType.StoredProcedure, true, arrSP);
            dr.Read();
            int recodeCount = (int)dr["CNT"];
            return recodeCount;
        }

        /// <summary>
        /// ȡ�ý׶����� 
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="pagesize">ÿҳ��С</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetJieDuanInfo(int userId,int pagesize)
        {
            string select = " stepcode AS code,stepname AS name ";
            string from = " s_step ";
            string where = "";
            try
            {
                QueryUtil qu = new QueryUtil(select, from, where);
                qu.PageSize = pagesize;
                qu.SortBy = "code";
                qu.SortOrder = Teamax.Common.SortOrder.Ascending;
                DataSet ds = qu.ExecuteDataset(0);
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
        public string[] GetDPInfo(int userId, int id, String fid)
        {
            string[] array = new string[4];
            try
            {
                string select = "select A.short_sentence,A.text_sentence,A.stepcode as stepid,DictionCode as name ";
                string from = " from s_diction_sentence AS A ";
                string where = " where A.id = " + id + " AND A.DictionCode = '" + fid + "'";
                string sSql = select + from + where;
                IDataReader dr = ExecuteReader(sSql , CommandType.Text, true);
                while (dr.Read())
                {
                    array[0] = dr["short_sentence"].ToString();
                    array[1] = dr["text_sentence"].ToString();
                    array[2] = dr["name"].ToString();
                    array[3] = dr["stepid"].ToString();
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
        public int UpdateInfo(int userId, int id, String fid, string short_sentence, string text_sentence, string stepid)
        {
            try
            {
                string sql = "update s_diction_sentence set DictionCode='" + fid + "', text_sentence='" + text_sentence + "', short_sentence='" + short_sentence + "', stepcode='" + stepid + "' where id=" + id;
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
        /// ɾ��ͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int DeleteTXL(int id)
        {
            try
            {
                string sql = "delete t_phonebook_Name where id=" + id;
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
        public int InsertData(int userId, String fid, string short_sentence, string text_sentence, string stepid)
        {
            try
            {
                string sql = "insert into s_diction_sentence(DictionCode,text_sentence,short_sentence,stepcode) values('" + fid + "','" + text_sentence + "','" + short_sentence + "','" + stepid + "')";
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        ///�޸� �ֵ���Ϣ
        /// </summary>

        public int UpdatePhoneBookData(int id, string departId, string role, string name, string officeTel, string insideTel, string mobileTel, string houseTel, string remarks)
        {
            try
            {
                string sql = "update  t_phonebook_Name set departid ='" + departId + "',name ='" + name + "',role='" + role + "',officetel='" + officeTel + "',insidetel='" + insideTel + "',mobileTel='" + mobileTel + "',housetel='" + houseTel + "',remarks='" + remarks + "',adddatetime='" + DateTime.Now + "' where id ='"+id+"'";
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// �����ֶ���Ϣ
        /// </summary>

        public int InsertPhoneBookData(string departId, string role, string name, string officeTel, string insideTel, string mobileTel, string houseTel, string remarks)
        {
            try
            {
                string sql = "insert into t_phonebook_Name(departid,name,role,officetel,insidetel,mobileTel,housetel,remarks,adddatetime) values('" + departId + "','" + role + "','" + name + "','" + officeTel + "','" + insideTel + "','" + mobileTel + "','" + houseTel + "','" + remarks + "','" + DateTime.Now + "')";
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
