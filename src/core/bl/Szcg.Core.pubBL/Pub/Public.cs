/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：公用数据访问-逻辑层
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-06-10 
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：                
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;


namespace bacgBL.Pub
{
    public class Public
    {
        #region GetEventpartBigclassHT：获取事部件大类
        /// <summary>
        /// 获取事部件大类
        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>哈希表对象</returns>
        public Hashtable GetEventpartBigclassHT(string probclass)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = GetEventpartBigclassDT(probclass);
            foreach (DataRow dr in dt.Rows)
               ht.Add(dr["code"], dr["name"]);
            return ht;
        }
        #endregion

        #region GetEventpartBigclassDT：获取事部件大类
        /// <summary>
        /// 获取事部件大类
        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>结果集对象</returns>
        public DataTable GetEventpartBigclassDT(string probclass)
        {
            string strCacheKey = "EventpartBigclassDT" + probclass;
            DataTable dt = (DataTable)MyCache.Get(strCacheKey);//获取缓存信息
            if (dt != null)
                return dt;

            using (bacgDL.Pub.Public dl = new bacgDL.Pub.Public())
            {
                dt = dl.GetEventpartBigclassDT(probclass);
                MyCache.Insert(strCacheKey, dt, 3600);

                return dt;
            }
        }
        #endregion

        #region GetEventpartBigclassHT：获取事部件小类
        /// <summary>
        /// 获取事部件小类
        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>哈希表对象</returns>
        public Hashtable GetEventpartSmallclassHT(string probclass)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = GetEventpartSmallclassDT(probclass);
            foreach (DataRow dr in dt.Rows)
                ht.Add(dr["code"], dr["name"]);
            return ht;
        }
        #endregion

        #region GetEventpartSmallclassDT：获取事部件小类
        /// <summary>
        /// 获取事部件小类
        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>结果集对象</returns>
        public DataTable GetEventpartSmallclassDT(string probclass)
        {
            string strCacheKey = "GetEventpartSmallclassDT" + probclass;
            DataTable dt = (DataTable)MyCache.Get(strCacheKey);//获取缓存信息
            if (dt != null)
                return dt;

            using (bacgDL.Pub.Public dl = new bacgDL.Pub.Public())
            {
                dt = dl.GetEventpartSmallclassDT(probclass);
                MyCache.Insert(strCacheKey, dt, 3600);

                return dt;
            }
        }
        #endregion

        #region GetStreetByAreaId：根据区域编码获取街道列表
        /// <summary>
        /// 根据区域编码获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public static DataTable GetStreetByAreaId(string areacode, ref string strErr)
        {

            string strSQL = string.Format(@"select 1 as id,null as streetcode,'全部' as streetname from s_street  union 
                                                select id , streetcode , streetname  
                                                from s_street
                                                where streetcode like '{0}%' order by id", areacode);
            try
            {
                //string strCacheKey = "s_street_code_name" + areacode;
                //DataTable dt = (DataTable)MyCache.Get(strCacheKey);//获取缓存信息
                //if (dt != null)
                  //  return dt;

                using (bacgDL.Pub.Public dl = new bacgDL.Pub.Public())
                {
                   DataTable dt = dl.ExecuteDataset(strSQL).Tables[0];
                    //MyCache.Insert(strCacheKey, dt, 3600);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region　GetAreaByCityId：根据市获取区域列表
        public static DataTable GetAreaByCityId(string areacode, ref string strErr)
        {
            string strSQL="";
            if (areacode.Length == 4)
            {
                strSQL = "select null as areacode,'全部' as areaname from s_area  union select areacode , areaname  from s_area where areacode<>'"+areacode+"'";
            }
            else
            {
                strSQL = string.Format(@"select null as areacode,'全部' as areaname from s_area  union 
                                                select  areacode , areaname  
                                                from s_area
                                                where areacode like '{0}%' ", areacode);
            }
            try
            {
                string strCacheKey = "s_area_code_name" + areacode;
                DataTable dt = (DataTable)MyCache.Get(strCacheKey);//获取缓存信息
                if (dt != null)
                    return dt;

                using (bacgDL.Pub.Public dl = new bacgDL.Pub.Public())
                {
                    dt = dl.ExecuteDataset(strSQL).Tables[0];
                    MyCache.Insert(strCacheKey, dt, 3600);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
