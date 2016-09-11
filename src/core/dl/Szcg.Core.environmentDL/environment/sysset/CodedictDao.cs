using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.sysset
{
    public class CodedictDao : Teamax.Common.CommonDatabase
    {
        
        /// <summary>
        /// 根据输入条件得到代码类别列表或者某一类别的代码值

        /// </summary>
        public Teamax.Common.PageManage GetAllcodedictList(codedictcls cls1, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Kind",cls1.kind),
                                new SqlParameter("@ValCode",cls1.codetype),
                                new SqlParameter("@SystemId",cls1.systemid),
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","codetype")};

                arrSP[4].Direction = ParameterDirection.Output;
                arrSP[5].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getcodelist", CommandType.StoredProcedure, arrSP);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[4].Value);
                page.pageCount = Convert.ToInt32(arrSP[5].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 新增一条代码类别或者某一类别的代码值
        /// </summary>
        public int insertIntocodedict(codedictcls cls1)
        {
            string sql = "insert into s_codedict(codetype,codeid,codename,inputcode,standardcode,systemid,status) "
                       + " values('" + cls1.codetype + "','" + cls1.codeid + "','" + cls1.codename + "','" + cls1.inputcode
                       + "','" + cls1.standardcode + "','" + cls1.systemid + "','" + cls1.status + "')";
            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新一条代码类别或者某一类别的代码值

        /// </summary>
        public int updateIntocodedict(codedictcls cls1)
        {
            string sql = " update s_codedict set codename='" + cls1.codename +"',"
                       + " inputcode='" + cls1.inputcode + "',"
                       + " standardcode='" + cls1.standardcode + "',"
                       + " status='" + cls1.status + "'"
                       + " where codetype =" + cls1.codetype
                       + " and codeid =" + cls1.codeid;
            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除一条代码类别或者某一类别的代码值

        /// </summary>
        public int deleteFromcodedict(codedictcls cls1)
        {
            string sql = " delete from s_codedict where codetype =" + cls1.codetype
                       + " and codeid =" + cls1.codeid;
            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获得一条代码类别或者某一类别的代码值信息

        /// </summary>
        public SqlDataReader GetCodeInfoByID(codedictcls cls1)
        {
            string sql = "select * from s_codedict where codetype =" + cls1.codetype
                       + " and codeid =" + cls1.codeid;
            try
            {
                SqlDataReader rs = (SqlDataReader)this.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMaxCodeid(int codetype)
        {
            string sql = " select max(codeid) as codeid from s_codedict where codetype =" + codetype;
            DataSet ds = this.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];
            try
            {
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMaxCodeType()
        {
            string sql = "select max(codetype) as codetype from s_codedict ";
            DataSet ds = this.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];
            try
            {
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}
