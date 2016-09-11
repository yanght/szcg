using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.zhpj
{
    public class Appraise_xcy : Teamax.Common.CommonDatabase, IDisposable
    {
        //调用存储过程获取区的评价结果
        public DataTable GetXcyData(int modelid, string parm1, string parm2, string parm3, string parm4, string roleid, DateTime start, DateTime end, string field, string order, ref int rowCount,
                                            ref int pageCount, out string cols, out string ReportMessage)
        {


            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@mode_id", modelid),
                                new SqlParameter("@roleid", roleid),
                                new SqlParameter("@CurrentPage",1),
                                new SqlParameter("@DateStart",start),
                                new SqlParameter("@DateEnd",end),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",-1),           
                                new SqlParameter("@field",field),
                                new SqlParameter("@order",order),
                                new SqlParameter("@parm1",parm1),
                                new SqlParameter("@parm2",parm2),
                                new SqlParameter("@parm3",parm3),
                                new SqlParameter("@parm4",parm4),
                                new SqlParameter("@Out_ReportMessage",SqlDbType.VarChar,500)
            };
            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[14].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetResultSet", CommandType.StoredProcedure, 600, arrSP);
            ReportMessage = arrSP[14].Value.ToString();
            rowCount = arrSP[5].Value == null || arrSP[5].Value.ToString() == "" ? 0 : int.Parse(arrSP[5].Value.ToString());

            DataTable dt = ds.Tables[0];//建一个新的实例
            if (ds.Tables.Count > 1)
                cols = ds.Tables[1].Rows[0][0].ToString();
            else
                cols = "";

            if (rowCount == -1)
            {
                return null;
            }
            return dt;
        }
    }
}
