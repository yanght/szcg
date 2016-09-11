using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Teamax.Common;
using bacgDL.zhpj;
using bacgDL.business;
using DBbase.zhpj;

namespace bacgBL.zhpj
{
    public class PQAJ_Dealyl
    {
        #region GetTracyForPQ:统计核查信息
        public static DataSet GetTracyForPQ(string projcode, string UserDefinedCode, string begin, string end,
                                                int curentpage, int pagesize, ref int rowCount,
                                                ref int pageCount, string strOrder, string strField, string IsReturn, string IsOvertime, string AreaCode, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetTracyForPQ(projcode, UserDefinedCode, begin, end, curentpage, pagesize,
                            ref rowCount, ref pageCount, strOrder, strField, IsReturn, IsOvertime, AreaCode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }

        }
        #endregion

        #region GetTracyForPQ:统计核查信息
        public static DataSet GetTracyForPQ(string projcode, string UserDefinedCode, string begin, string end,
                                                int curentpage, int pagesize, ref int rowCount,
                                                ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetTracyForPQ(projcode, UserDefinedCode, begin, end, curentpage, pagesize,
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

        public static DataSet GetDealProjectCount(string begin, string end, string strOrder, string strField, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetDealProjectCount(begin, end, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        public static DataSet GetDealProjectCountDeatail(string departcode, string column, string IsStart, string IsEnd, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetDealProjectCountDeatail(departcode, column, IsStart, IsEnd);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取区域评价的详细信息
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataSet GetAreaProjectDetail(string areacode, string column, string IsStart, string IsEnd, PageInfo page, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetAreaProjectDetail(areacode, column, IsStart, IsEnd, page);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取部门评价的详细信息
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataSet GetDepartProjectDetail(string areacode, string column, string IsStart, string IsEnd, PageInfo page, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetDepartProjectDetail(areacode, column, IsStart, IsEnd, page);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取岗位评价的详细信息
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="usercode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataSet GetWorkProjectDetail(string departcode, string usercode, string column, string IsStart, string IsEnd, PageInfo page, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetWorkProjectDetail(departcode, usercode, column, IsStart, IsEnd, page);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取详细信息需要查看列的列名
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static DataTable GetColumnName(string column)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetColumnName(column);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取区域名
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public static DataTable GetAreaName(string areacode)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetAreaName(areacode);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetEventPartTotals(StructQuery sq)
        {
            string strCacheKey;
            if (sq.smallClassCode != "")
            {
                strCacheKey = sq.typecode + sq.bigClassCode + sq.smallClassCode + "GetEventPartTotal";
            }
            else
            {
                strCacheKey = sq.typecode + sq.bigClassCode + "GetEventPartTotal";
            }
            DataTable dt = (DataTable)MyCache.Get(strCacheKey);
            if (dt != null)
                return dt;

            dt = GetEventPartTotal(sq);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["PBGCODE"].ToString() == dt.Rows[j]["BGCODE"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i]["PBGCODE"] = "";
            }
            MyCache.Insert(strCacheKey, dt, 1200);
            return dt;
        }

        public static DataTable GetEventPartTotal(StructQuery sq)
        {
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetEventPartTotal(sq.bgCode, sq.typecode, sq.bigClassCode, sq.smallClassCode, sq.startDate, sq.endDate);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region GetStreetDataInfo：获取街道统计案卷的列表
        /// <summary>
        /// 获取街道统计案卷的列表
        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="DataField">查询列名</param>
        /// <param name="DateStart">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <param name="SearchYear">查询年</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetStreetDataInfo(string streetcode,string DataField,string DateStart, string DateEnd, string SearchYear, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetStreetDataInfo(streetcode,DataField,DateStart,DateEnd,SearchYear,page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetZRDWDataInfo：获取责任单位统计案卷的列表
        /// <summary>
        /// 获取责任单位统计案卷的列表
        /// </summary>
        /// <param name="UserDefinedCode">单位自定义编码</param>
        /// <param name="DataField">查询列名</param>
        /// <param name="DateStart">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <param name="SearchYear">查询年</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetZRDWDataInfo(string UserDefinedCode, string DataField, string DateStart, string DateEnd, string SearchYear, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetZRDWDataInfo(UserDefinedCode, DataField, DateStart, DateEnd, SearchYear, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetGWDataInfo：获取岗位评价统计案卷的列表
        /// <summary>
        /// 获取岗位评价统计案卷的列表
        /// </summary>
        /// <param name="UserCode">用户编码</param>
        /// <param name="DataField">查询列名</param>
        /// <param name="DateStart">开始日期</param>
        /// <param name="DateEnd">结束日期</param>
        /// <param name="SearchYear">查询年</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetGWDataInfo(string UserCode, string DataField, string DateStart, string DateEnd, string SearchYear, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetGWDataInfo(UserCode, DataField, DateStart, DateEnd, SearchYear, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetXCYDataInfo：获取巡查运评价统计案卷的列表
        public static DataSet GetXCYDataInfo(string UserCode,  string DateStart, string DateEnd, string SearchYear, PageInfo page, out string ErrMsg)
        {
            
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetXCYataInfo(UserCode, DateStart, DateEnd, SearchYear, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetXCYDataInfo：获取总案卷统计案卷的列表
        public static DataSet GetZAJDataInfo(string strType,PageInfo page,out string ErrMsg)
        {

            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetZAJDataInfo(strType, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region RptCenterDailyDeal：加载区监督指挥中心日处理信息一览表
        /// <summary>
        /// 加载区监督指挥中心日处理信息一览表
        /// </summary>
        /// <param name="DateStart">查询开始日期</param>
        /// <param name="DateEnd">查询结束日期</param>
        /// <param name="ErrMsg">错误信息</param>
        /// <returns>结果集</returns>
        public DataTable RptCenterDailyDeal(string DateStart, string DateEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    SqlParameter[] arrSP = new SqlParameter[] {
                        new SqlParameter("@DateStart", DateStart),
                        new SqlParameter("@DateEnd", DateEnd)
                    };
                    DataSet ds = dl.ExecuteDataset("pr_a_Rpt_CenterDailyDeal", CommandType.StoredProcedure, arrSP);
                    return ds.Tables[0];
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region RptCenterDailyDeal_Detail：加载区监督指挥中心日处理信息一览表的某列明细记录
        /// <summary>
        /// 加载区监督指挥中心日处理信息一览表的某列明细记录
        /// </summary>
        /// <param name="StreetID">街道</param>
        /// <param name="column">列名</param>
        /// <param name="page">页面对象</param>
        /// <param name="DateStart">查询开始日期</param>
        /// <param name="DateEnd">查询结束日期</param>
        /// <param name="ErrMsg">错误信息</param>
        /// <returns>结果集</returns>
        public static DataTable RptCenterDailyDeal_Detail(string StreetID, string column, string DateStart, string DateEnd, bacgDL.business.PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@StreetID",StreetID),
                        new SqlParameter("@DataField",column),
                        new SqlParameter("@DateStart",DateStart),
                        new SqlParameter("@DateEnd",DateEnd),
                        new SqlParameter("@CurrentPage",page.CurrentPage),
                        new SqlParameter("@RowCount",SqlDbType.Int),
                        new SqlParameter("@PageCount",SqlDbType.Int),
                        new SqlParameter("@PageSize",page.PageSize),
                        new SqlParameter("@Order",page.Order),
                        new SqlParameter("@Field",page.Field)
                    };
                    arrSP[5].Direction = ParameterDirection.Output;
                    arrSP[6].Direction = ParameterDirection.Output;

                    DataSet ds = dl.ExecuteDataset("pr_a_Rpt_CenterDailyDeal_Detail", CommandType.StoredProcedure, arrSP);
                    page.RowCount = arrSP[5].Value.ToString();
                    page.PageCount = arrSP[6].Value.ToString();
                    return ds.Tables[0];
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetDropListDataInfo：获取综合评价页面下拉列表一些绑定信息
        /// <summary>
        /// 重载方法
        /// </summary>
        public static DataSet GetDropListDataInfo(string areacode,out string ErrMsg)
        {
            return GetDropListDataInfo(areacode, "", "", "", "", "", out ErrMsg);
        }
        public static DataSet GetDropListDataInfo(string areacode, string parm1,out string ErrMsg)
        {
            return GetDropListDataInfo(areacode, parm1,"", "", "", "", out ErrMsg);
        }
        public static DataSet GetDropListDataInfo(string areacode, string parm1, string parm2,out string ErrMsg)
        {
            return GetDropListDataInfo(areacode, parm1, parm2, "","", "", out ErrMsg);
        }
        public static DataSet GetDropListDataInfo(string areacode, string parm1, string parm2, string parm3,out string ErrMsg)
        {
            return GetDropListDataInfo(areacode, parm1, parm2, parm3, "", "", out ErrMsg);
        }
        public static DataSet GetDropListDataInfo(string areacode, string parm1, string parm2, string parm3, string parm4,out string ErrMsg)
        {
            return GetDropListDataInfo(areacode, parm1, parm2, parm3, parm4, "",out ErrMsg);
        }
        /// <summary>
        /// 获取综合评价页面下拉列表一些绑定信息
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="parm1">可选参数1</param>
        /// <param name="parm2">可选参数2</param>
        /// <param name="parm3">可选参数3</param>
        /// <param name="parm4">可选参数4</param>
        /// <param name="parm5">可选参数5</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetDropListDataInfo(string areacode, string parm1, string parm2, string parm3, string parm4, string parm5, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetDropListDataInfo(areacode, parm1, parm2, parm3, parm4, parm5);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetZRDWDataInfo1：获取责任单位评价统计案卷的列表
        public static DataSet GetZRDWDataInfo1(string UserDefinedCode, string AreaCode, string AppriseType1, string AppriseType2, string DateStart, string DateEnd, PageInfo page, out string ErrMsg)
        {

            ErrMsg = "";
            try
            {
                using (bacgDL.zhpj.PQAJ_dealyl dl = new bacgDL.zhpj.PQAJ_dealyl())
                {
                    return dl.GetZRDWDataInfo1(UserDefinedCode, AreaCode, AppriseType1, AppriseType2, DateStart, DateEnd, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion
    }
    }

