/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：监督员管理－逻辑层访问类。

 * 结构组成：

 * 作    者：yannis
 * 创建日期：2007-05-25
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期：  
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Teamax.Common;

namespace bacgBL.business
{
    public class collecter
    {
        #region SendMsgToPda：向监督员PDA发送WEB消息
        /// <summary>
        /// 向监督员PDA发送WEB消息。

        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="msgtitle">消息主题</param>
        /// <param name="msgcontent">消息内容</param>
        /// <param name="usercode">用户编号</param>
        public static void SendPDAMsg(string collcode, string msgcontent, string msgtitle, string usercode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    dl.SendPDAMsg(collcode, msgcontent, msgtitle, usercode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
            }
        }
        #endregion

        #region GetStreetList:根据登陆用于的区域编码,获取街道列表
        /// <summary>
        /// 根据登陆用于的区域编码,获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public static DataSet GetStreetList(string areacode, ref string strErr)
        {
            try
            {
                using(bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    return dl.GetStreetList(areacode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetCollectereList:获取监督员列表

        /// <summary>
        /// 获取监督员列表

        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="gridcode">网格编码</param>
        /// <param name="collname">监督员名字</param>
        /// <param name="loginname">监督员登陆名字</param>
        /// <param name="collmobile">监督员号码</param>
        /// <param name="isguard">是否在岗</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="strOrder">排序方式</param>
        /// <param name="strField">排序字段</param>
        /// <param name="strErr">错误提示</param>
        /// <returns></returns>
        public static DataSet GetCollectereList(string streetcode, string gridcode, string collname,
                                                    string loginname, string collmobile, string isguard,
                                                    int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField,ref string strErr)
        {

            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    return dl.GetCollectereList(streetcode, gridcode, collname, loginname, collmobile, isguard, curentpage, pagesize,
                            ref rowCount, ref pageCount, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetCollectereList4HC:获取核查用的监督员列表

        /// <summary>
        /// 获取核查用的监督员列表

        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <returns></returns>
        public static DataSet GetCollectereList4HC(string streetcode,string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    return dl.GetCollectereList4HC(streetcode,projcode);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetTaskStat:统计上报信息
        public static DataSet GetTaskStat(string projcode,string street, string collname, string prosoure, DateTime begin, DateTime end,string Hcpower, ref string strErr)
        {
            DataSet ds = new DataSet();
            SqlParameter[] spInputs = new SqlParameter[]
            {
                new SqlParameter("@projcode", projcode),
                new SqlParameter("@areacode", street),
                new SqlParameter("@DateStart",begin),
                new SqlParameter("@DateEnd", end),
                new SqlParameter("@CollName", collname),
                new SqlParameter("@Type", prosoure),
                new SqlParameter("@Hcpower",Hcpower),
            };
            try
            {
                using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString("ConnString")))
                {
                    cdb._CommandTimeOut = 1200;
                    ds = cdb.ExecuteDataset("pr_b_ToGetCollJobTotal", CommandType.StoredProcedure, spInputs);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.ToString();
                return null;
            }
        }
        #endregion

        #region GetCollectereInfo:获取监督员详细信息

        /// <summary>
        /// 获取监督员详细信息

        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public static DataSet GetCollectereInfo(string collcode, ref string strErr)
        {
            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    return dl.GetCollectereInfo(collcode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetCollectereMapInfo:获取监督员详细信息

        /// <summary>
        /// 获取监督员详细信息

        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string GetCollectereMapInfo(string collcode)
        {
            string strResult = "";
            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {

                    DataSet ds = dl.GetCollectereInfo(collcode);
                    if (ds.Tables[0].Rows.Count == 0)
                        return "";
                    strResult = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                        ds.Tables[0].Rows[0]["collcode"].ToString(),
                        ds.Tables[0].Rows[0]["collname"].ToString(),
                        //ds.Tables[0].Rows[0]["numbercode"].ToString(),
                        ds.Tables[0].Rows[0]["gridcode"].ToString(),
                        ds.Tables[0].Rows[0]["sex"].ToString(),
                        ds.Tables[0].Rows[0]["mobile"].ToString(),
                        ds.Tables[0].Rows[0]["age"].ToString(),
                        ds.Tables[0].Rows[0]["commname"].ToString());

                    strResult = bacgBL.Pub.Tools.StrConv(strResult, "GB2312");
                    return strResult;
                }
            }
            catch (Exception ex)
            {
                strResult = string.Format("-1${0}", ex.Message);
                return strResult;
            }
        }
        #endregion

        #region GetCollecterXY：获取指定监督员详细信息
        /// <summary>
        /// 获取指定监督员详细信息

        /// </summary>
        /// <param name="collcode">监督员ID</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string GetCollecterXY(string collcode)
        {
            string strResult = "";
            string[] codes = collcode.Split(',');
            string r = "";
            //没有对cu_date时间进行限制，可能是历史坐标。仅仅取出当天的监督员

            string strSQL = string.Format(@"select isnull(cu_x,'')+'$'+isnull(cu_y,'')+'$'+isnull(collcode,'')+'$'+isnull(collname,'')+'$'+convert(varchar,cu_date,120)
                                            from m_collecter 
                                            where collcode = '{0}' 
                                                and datediff(day,cu_Date,getdate())=0", collcode);

            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    int n = 0;
                    for (int x = 0; x < codes.Length; x++)
                    {
                        strSQL = string.Format(@"select isnull(cu_x,'')+'$'+isnull(cu_y,'') +'#'+convert(varchar,collcode)+'$'+isnull(collname,'')+'$'+convert(varchar,cu_date,120)
                                            from m_collecter 
                                            where collcode = '{0}' 
                                                and datediff(day,cu_Date,getdate())=0", codes[x]);
                        object oResult = dl.ExecuteScalar(strSQL);
                        strResult = oResult == null ? "" : oResult.ToString();

                        if (strResult == "$" || strResult == "")
                        {
                            //strResult = "-1";
                        }
                        else
                        {
                            strResult = "|" + strResult;
                            n++;
                        }
                        
                        r += strResult;
                    }
                    if (n != codes.Length&&n!=0)
                    {
                        r =Convert.ToString (codes.Length-n)+r;
                    }
                }

            }
            catch (Exception ex)
            {
                r = "-9";
                
            }
            return r;
        }
        #endregion

        #region GetAllCollecterXY：获得所有监督员当前的位置坐标

        /// <summary>
        /// 获得所有监督员当前的位置坐标

        /// </summary>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetAllCollecterXY()
        {
            string strResult = "";

            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    IDataReader dr = dl.ExecuteReader("pr_m_GetAllCollecterXY", CommandType.StoredProcedure,true);
                    while (dr.Read())
                    {
                        strResult += dr[0].ToString() + "$";
                    }
                    if (dr != null)
                        dr.Close();

                    return strResult.Trim('$');
                }
            }
            catch 
            {
                return "";
            }
        }
        #endregion

        #region DrawCollectTrack：画监督员坐标轨迹（已坐标点画）

        /// <summary>
        /// 画监督员坐标轨迹
        /// </summary>
        /// <param name="collcode"></param>
        /// <param name="rowNum">记录数目</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string DrawCollectTrack(string collcode, string rowNum)
        {
            try
            {
                string sql = string.Format(@"   SELECT TOP {0} xycom 
                                                FROM m_collecter_xy 
                                                WHERE collcode = {1}
	                                                and DATEDIFF(DAY,cu_date,GETDATE())=0  
                                                order by id desc ", rowNum, collcode);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    IDataReader dr = dl.ExecuteReader(sql);
                    while (dr.Read())
                    {
                        //每个点取三个GPS信号（第一个，中间个，最好个）

                        int intLen = 0;
                        foreach (char c in dr[0].ToString())
                        {
                            if (c == ',')
                            {
                                intLen++;
                            }
                        }

                        if (intLen < 6)
                        {
                            sb.Append(dr[0].ToString()).Append(",");
                        }
                        else
                        {
                            string[] a = dr[0].ToString().Split(',');
                            sb.Append(a[0] + "," + a[1] + "," + a[a.Length / 2 - 1] + "," + a[a.Length / 2] + "," + a[a.Length - 2] + "," + a[a.Length - 1]).Append(",");
                        }


                    }
                    if (dr != null)
                        dr.Close();
                }

                return sb.ToString().Trim(',');
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region DrawCollectTrackDate：画监督员坐标轨迹（已时间画）
        /// <summary>
        /// 画监督员坐标轨迹（已时间画）
        /// </summary>
        /// <param name="collcode"></param>
        /// <param name="stateDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string DrawCollectTrackDate(string collcode, string stateDate, string endDate)
        {
            try
            {
                string sql = string.Format(@"   SELECT   left(cu_x,10)+','+left(cu_y,9) as xycom,cu_date
                                                FROM m_collecter_xy 
                                                WHERE collcode = {0}
	                                                and cu_date BETWEEN '{1}' and  '{2}'
                                                order by id asc ", collcode, stateDate, endDate);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder time = new System.Text.StringBuilder();
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    IDataReader dr = dl.ExecuteReader(sql);
                    while (dr.Read())
                    {
                        //每个点取三个GPS信号（第一个，中间个，最好个）

                        int intLen = 0;
                        foreach (char c in dr[0].ToString())
                        {
                            if (c == ',')
                            {
                                intLen++;
                            }
                        }

                        /*
                        if (intLen < 6)
                        {
                            sb.Append(dr[0].ToString()).Append(",");
                            time.Append(dr[1].ToString()).Append(",");
                        }
                        else
                        {
                            string[] a = dr[0].ToString().Split(',');
                            sb.Append(a[0] + "," + a[1] + "," + a[a.Length / 2 - 1] + "," + a[a.Length / 2] + "," + a[a.Length - 2] + "," + a[a.Length - 1]).Append(",");
                            string[] b = dr[1].ToString().Split(',');
                            time.Append(b[0] + "," + b[1] + "," + b[b.Length / 2 - 1] + "," + b[b.Length / 2] + "," + b[b.Length - 2] + "," + b[b.Length - 1]).Append(",");
                        }*/
                        //modi by yaoch 2012-12-10
                        sb.Append(dr[0].ToString()).Append("|");
                        time.Append(dr[1].ToString()).Append("|");

                    }
                    if (dr != null)
                        dr.Close();
                }

                return sb.ToString().TrimEnd('|') + "&" + time.ToString().TrimEnd('|'); 
            }
            catch
            {
                return "";
            }
        }
        #endregion

       #region GetCollectQueryStat:统计核查信息
        public static DataSet GetCollectQueryStat(string street,string loginname,string collname,
                                                    string mobile, DateTime begin, DateTime end,
                                                    string strHCFlag, int curentpage, int pagesize, 
                                                    ref int rowCount, ref int pageCount,
                                                    string strOrder, string strField,string Hcpower, ref string strErr)
        {
            try
            {
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    return dl.GetCollectQueryStat(street, loginname, collname, mobile, begin, end, strHCFlag, curentpage, pagesize,
                            ref rowCount, ref pageCount, strOrder, strField,Hcpower);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
            
        }
        #endregion

        #region DrawDotCollectTrack：画监督员坐标轨迹（已坐标点画）

        /// <summary>
        /// 画监督员最新点坐标轨迹
        /// </summary>
        /// <param name="collcode"></param>
        /// <param name="rowNum">记录数目</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static string DrawDotCollectTrack(string collcode, string rowNum)
        {
            try
            {
                string sql = string.Format(@"   SELECT TOP {0} xycom 
                                                FROM m_collecter_xy 
                                                WHERE collcode = {1}
	                                            order by id desc ", rowNum, collcode);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                using (bacgDL.business.collecter dl = new bacgDL.business.collecter())
                {
                    IDataReader dr = dl.ExecuteReader(sql);
                    while (dr.Read())
                    {
                        //每个点取三个GPS信号（第一个，中间个，最好个）

                        int intLen = 0;
                        foreach (char c in dr[0].ToString())
                        {
                            if (c == ',')
                            {
                                intLen++;
                            }
                        }

                        if (intLen < 6)
                        {
                            sb.Append(dr[0].ToString()).Append(",");
                        }
                        else
                        {
                            string[] a = dr[0].ToString().Split(',');
                            sb.Append(a[0] + "," + a[1] + "," + a[a.Length / 2 - 1] + "," + a[a.Length / 2] + "," + a[a.Length - 2] + "," + a[a.Length - 1]).Append(",");
                        }


                    }
                    if (dr != null)
                        dr.Close();
                }

                return sb.ToString().Trim(',');
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
