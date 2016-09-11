using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using szcg.web.business.repository;

namespace businessDL.business.repository
{
    /// <summary>
	/// Repository 的摘要说明。
	/// </summary>
    public class Repository : Teamax.Common.CommonDatabase
    {
        protected string sql = "";
        protected int rows = 0;

        public String[] getInfoLibString()
        {
            String[] infoLib = null;
            sql = "select count(*) from s_repository";
            object obj = (int)this.ExecuteScalar(sql);
            if (obj != System.DBNull.Value)
            {
                rows = (int)obj;
            }
            if (rows != 0)
            {
                infoLib = new String[rows];
                sql = "select * from s_repository where ID >0 order by ID";
                IDataReader rs = (IDataReader)this.ExecuteReader(sql);
                if (rs != null)
                {
                    int i = 0;
                    while (rs.Read())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("name=" + Convert.ToString(rs["name"]) + ",");
                        sb.Append("code=" + Convert.ToString(rs["id"]) + ",");
                        sb.Append("pcode=" + Convert.ToString(rs["pid"]) + ",");
                        sb.Append("memo=" + Convert.ToString(rs["desc"]));
                        infoLib[i] = sb.ToString();
                        i++;
                    }
                    rs.Close();
                }
            }
            return infoLib;
        }

        #region QueryRepository：知识库查询
        /// <summary>
        /// 知识库查询
        /// </summary>
        /// <param name="strKey">关键字</param>
        /// <returns></returns>
        public DataSet QueryRepository(string strKey, ref string ErrMsg)
        {
            string strSQL = string.Format("select * from s_repository where name like '%{0}%' or desc like '%{0}%'", strKey);

            try
            {
                return this.ExecuteDataset(strSQL);
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        //得到知识库单个记录
        public RepositoryInfo getInfoSingleLib(string id)
        {
            string sql1 = "select * from s_repository where id='" + id + "'";

            RepositoryInfo repositoryInfo = new RepositoryInfo(); ;
            IDataReader dr = this.ExecuteReader(sql1);

            while (dr.Read())
            {

                repositoryInfo.setName(Convert.ToString(dr["name"]));
                repositoryInfo.setTitle(Convert.ToString(dr["title"]));
                repositoryInfo.setDate(Convert.ToString(dr["cu_date"]));
                repositoryInfo.setMemo(Convert.ToString(dr["desc"]));
            }
            dr.Close();

            return repositoryInfo;
        }
        public String addInfoLib(String pcode, String name, String memo)
        {
            object obj = this.ExecuteScalar("select count(*) from s_repository");
            if (obj == System.DBNull.Value)
            {
                return "数据表为空！";
            }
            String code = pcode + Convert.ToString(obj).ToString();
            String sql = "insert into s_repository(pid,name,cu_date,desc) " +
                "values('" + pcode + "','" + name + "',GetDate(),'" + memo + "')";
            try
            {
                this.ExecuteNonQuery(sql);
                return "新增成功";
            }
            catch (Exception se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
                return "新增失败！";
            }
        }

        public String getInfoLibMemo(String code)
        {
            String sql = "select desc from s_repository where id='" + code + "'";
            String memo = "";

            try
            {
                object obj = this.ExecuteScalar(sql, null);
                if (obj != System.DBNull.Value)
                {
                    memo = (String)obj;
                }
            }
            catch (Exception se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
            }
            return memo;
        }

        #region  GetRepositoryByName： 获取知识库内容，根据知识库标题
        /// <summary>
        /// 获取知识库内容，根据知识库标题
        /// </summary>
        /// <param name="small">小类</param>
        public string GetRepositoryByName(string small, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string strSQL = "select desc from s_repository where name=" + small;

                object desc = this.ExecuteScalar(strSQL);
                return desc != null ? desc.ToString() : "没有找到对应的知识库，请通知系统管理员录入完整。";

            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return "";
            }
        }
        #endregion
    }

}
