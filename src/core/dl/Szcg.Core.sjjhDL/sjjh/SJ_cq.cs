using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data ;
using bacgDL.business;

namespace bacgDL.sjjh
{
    public class SJ_cq : Teamax.Common.CommonDatabase
    {
        DataTable ds = null;
        /// <summary>
        /// 数据抽取
        /// </summary>
        /// <param name="type">类型（用户数据／部门数据..）</param>
        /// <returns>结果集</returns>
        public DataTable get_cqlist(string type)
        {
            ds = new DataTable();
            string sql = "select *from c_interface where facetype='"+type+"'";
            return this.ExecuteDataset(sql).Tables[0];
        }
        /// <summary>
        /// 数据发布　注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet get_sjlist(string type,string currentpage,string pagesize,PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@type",type),                                
                                new SqlParameter("@CurrentPage",currentpage ),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize)};

            arrSP[2].Direction = ParameterDirection.Output;
            arrSP[3].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_c_Getsjlist", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[2].Value.ToString();
            page.PageCount = arrSP[3].Value.ToString();

            return ds;
        }
        /// <summary>
        /// 修改借口状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateState(string id, int state)
        {
            string sql = "update C_INTERFACE set facestate=" + state + " where id='"+id+"'";
            int i= this.ExecuteNonQuery(sql);
            return i != 0;
        }

        /// <summary>
        /// 获得部门数据
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataSet get_Deplist(string departcode, string currentpage, string pagesize, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@departcode",departcode),                                
                                new SqlParameter("@CurrentPage",currentpage ),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize)};

            arrSP[2].Direction = ParameterDirection.Output;
            arrSP[3].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_c_GetDeplist", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[2].Value.ToString();
            page.PageCount = arrSP[3].Value.ToString();

            return ds;
        }
    }
}
