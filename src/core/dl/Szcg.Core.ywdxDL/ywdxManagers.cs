using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Data.SqlClient;

namespace ywdxDL
{
    public class ywdxManagers : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="prosoure"></param>
        /// <returns></returns>
        public DataSet GetDXlist(string starttime,string endtime,string prosoure)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@times1 ",starttime),
                        new SqlParameter("@times2",endtime),
                        new SqlParameter("@prosource",prosoure)
                    };
            return this.ExecuteDataset("prd_getdxfklist", CommandType.StoredProcedure, arrSP);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet getCustomer(string type)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@type ",type)
                    };
            return this.ExecuteDataset("prd_getCustomerList", CommandType.StoredProcedure, arrSP);
        }
    }
}
