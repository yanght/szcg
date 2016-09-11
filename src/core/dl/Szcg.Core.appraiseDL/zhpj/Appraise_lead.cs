using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.zhpj
{
    public class Appraise_lead : Teamax.Common.CommonDatabase
    {
        public bool add( string FeedUserCode, string FeedUserName, string FeedbackContent, string AppraiseModule, string AppraiseModuleWhere, string AppraiseTimeArea)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@FeedbackTime",DateTime.Now),
                new SqlParameter("@FeedUserCode",FeedUserCode),
                new SqlParameter("@FeedUserName",FeedUserName ),
                new SqlParameter("@FeedbackContent",FeedbackContent),
                new SqlParameter("@AppraiseModule", AppraiseModule),
                new SqlParameter("@AppraiseModuleWhere", AppraiseModuleWhere),
                new SqlParameter("@AppraiseTimeArea", AppraiseTimeArea),

            };

            //同时添加业务消息

            int r=this.ExecuteNonQuery("pr_a_SetAppraiselead", CommandType.StoredProcedure, param);
            return r != 0;
        }
        public DataSet get(string apraisemodule)
        {
            DataSet ds = new DataSet();
            string sql = " select * from a_appraise_lead_feedback order by id ";

            ds = this.ExecuteDataset(sql, null);

            if (ds == null)
                return null;

            return ds;
        }
        public DataSet getUserCode(string UserDefinedCode)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@UserDefinedCode",UserDefinedCode),
            };
            DataSet ds = new DataSet();
            try
            {
                 ds = ExecuteDataset("pr_a_GetUserCodeByUserDefinedCode", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
    }
}
