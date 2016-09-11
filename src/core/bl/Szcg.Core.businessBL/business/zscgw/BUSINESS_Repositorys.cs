using System;
using System.Data.SqlClient;
using System.Data;
using szcg.com.teamax.util;
using System.Text;
using System.Collections;
using szcg.web.business.repository;

namespace bacgBL.business
{
	/// <summary>
	/// Repository 的摘要说明。
	/// </summary>
	public class Repository
	{
		protected string sql = "";
		protected int rows=0;
		protected SqlDataReader rs;

		public String[] getInfoLibString() 
		{		  
			String[] infoLib = null;
			sql = "select count(*) from s_repository";
			object obj = (int)DataAccess.ExecuteScalar(sql,null);
			if(obj!=System.DBNull.Value)
			{
				rows = (int)obj;
			}
			if(rows!=0)
			{
				infoLib = new String[rows];
				sql = "select * from s_repository where ID >0 order by ID";
				rs = DataAccess.ExecuteReader(sql,null);
				if(rs!=null)
				{
					int i = 0;
					while(rs.Read())
					{
						StringBuilder sb = new StringBuilder();							
						sb.Append("name="+Convert.ToString(rs["name"])+",");
						sb.Append("code="+Convert.ToString(rs["id"])+",");
						sb.Append("pcode="+Convert.ToString(rs["pid"])+",");
						sb.Append("memo="+Convert.ToString(rs["_desc"]));
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
        public static DataSet QueryRepository(string strKey, ref string ErrMsg)
        {
            string strSQL = string.Format("select * from s_repository where name like '%{0}%' or _desc like '%{0}%'", strKey);

            try
            {
                using (Teamax.Common.CommonDatabase dl = new Teamax.Common.CommonDatabase())
                {
                    return dl.ExecuteDataset(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

		//得到知识库单个记录
		public static RepositoryInfo getInfoSingleLib(string id)
		{
			string sql1="select * from s_repository where id='"+id+"'";
		  
			RepositoryInfo repositoryInfo=new RepositoryInfo();;
			SqlDataReader dr = szcg.com.teamax.util.DataAccess.ExecuteReader(sql1,null);

			while(dr.Read())
			{

				repositoryInfo.setName(Convert.ToString(dr["name"]));
				repositoryInfo.setTitle(Convert.ToString(dr["title"]));
				repositoryInfo.setDate(Convert.ToString(dr["cu_date"]));
				repositoryInfo.setMemo(Convert.ToString(dr["_desc"]));
			}
			dr.Close();

			return repositoryInfo;
		}
		public String addInfoLib(String pcode,String name,String memo)
		{
			object obj = DataAccess.ExecuteScalar("select count(*) from s_repository");
			if(obj==System.DBNull.Value)
			{
				return "数据表为空！";
			}
			String code = pcode + Convert.ToString(obj).ToString();
			String sql = "insert into s_repository(pid,name,cu_date,_desc) "+
				"values('"+pcode+"','"+name+"',GetDate(),'"+memo+"')";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);
				return "新增成功";
			}
			catch(SqlException se)
			{
				System.Diagnostics.Debug.WriteLine(se.Message);
				return "新增失败！";
			}
		}

		public String getInfoLibMemo(String code)
		{
			String sql = "select _desc from s_repository where id='"+code+"'";
			String memo = "";
			 
			try
			{
				object obj = DataAccess.ExecuteScalar(sql,null);
				if(obj!=System.DBNull.Value)
				{
					memo = (String)obj;
				}
			}
			catch(SqlException se)
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
        public static string GetRepositoryByName(string small, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.business.Project dl = new bacgDL.business.Project())
            {
                try
                {
                    string strSQL = "select _desc from s_repository where name=@small";

                    SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@small",small)
                    };

                    object desc = dl.ExecuteScalar(strSQL, arrSP);
                    return desc != null ? desc.ToString() : "没有找到对应的知识库，请通知系统管理员录入完整。";

                }
                catch (Exception e)
                {
                    ErrMsg = e.Message;
                    return "";
                }
            }
        }
        #endregion

    }
}
