using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using bacgDL.zhpj;
using Teamax.Common;
using System.Data.SqlClient;
using bacgDL.zhpj.areaappraise;
namespace bacgBL.zhpj
{
    public class Appraise_xcy
    {
        public DataTable getXcyAppraise(int modelid, string parm1, string parm2,string parm3,string parm4,string roleid,string field,string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            DataTable dt = null;

            #region  用来缓存数据，但是有一段时间数据一样
            //string strCacheKey;

            //strCacheKey = modelid.ToString() + qycode + roleid + startdate + enddate + field.ToString() + order.ToString() + "_GetAreaData";

            //dt = (DataTable)MyCache.Get(strCacheKey);
            //if (dt != null)
            //    return dt;
            #endregion

            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.Appraise_xcy dl = new bacgDL.zhpj.Appraise_xcy ())
                {
                    sq.startDate = Convert.ToDateTime(startdate);
                    sq.endDate = Convert.ToDateTime(enddate);
                    dt = dl.GetXcyData(modelid, parm1, parm2, parm3, parm4, roleid, sq.startDate, sq.endDate, field, order, ref rowCount, ref pageCount, out cols, out ReportMessage);
                    //MyCache.Insert(strCacheKey, dt, 1200);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
