using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.zhpj
{
    public class Appraise_zaj : Teamax.Common.CommonDatabase, IDisposable
    {
        public DataTable GetZajData()
        {


            SqlParameter[] arrSP = new SqlParameter[] {};
            
            DataSet ds = this.ExecuteDataset("pr_a_M_HavingProject", CommandType.StoredProcedure, 600, arrSP);

            DataTable dt = ds.Tables[0];//建一个新的实例


            if (dt == null)
            {
                return null;
            }
            return dt;
        }
    
    }
}
