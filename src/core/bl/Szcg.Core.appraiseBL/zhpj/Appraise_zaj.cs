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
   public class Appraise_zaj
    {
        public DataTable getZajAppraise()
        {
            DataTable dt = null;

            #region  用来缓存数据，但是有一段时间数据一样
            //string strCacheKey;

            //strCacheKey = modelid.ToString() + qycode + roleid + startdate + enddate + field.ToString() + order.ToString() + "_GetAreaData";

            //dt = (DataTable)MyCache.Get(strCacheKey);
            //if (dt != null)
            //    return dt;
            #endregion

           try
            {
                using (bacgDL.zhpj.Appraise_zaj dl = new bacgDL.zhpj.Appraise_zaj())
                {
                    //sq.startDate = Convert.ToDateTime(startdate);
                   //sq.endDate = Convert.ToDateTime(enddate);
                    dt = dl.GetZajData();
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
