using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace bacgDL.zhpj.departappraise
{
    public class StructQuery
    {
        public int intWeeks = 0;
        public int intMonths = 0;
        public int intYears = 0;
        public int intQuarter = 0;
        public DateTime startDate = new DateTime();
        public DateTime endDate = new DateTime();
    }

    public class DepartAppraise : Teamax.Common.CommonDatabase, IDisposable
    {
        //调用存储过程获取评价结果
        public DataTable GetDepartData(int departcode, int year, int type, int number, DateTime start, DateTime end, string field, string order)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@departids", departcode), 
                                new SqlParameter("@selectedyear", year), 
                                new SqlParameter("@sect_type", type),
                                new SqlParameter("@sect_num",number),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end),
                                new SqlParameter("@field",field),
                                new SqlParameter("@order",order),
                                };
            DataSet ds = ExecuteDataset("pr_a_GetDepartData", CommandType.StoredProcedure, arrSP);
            DataTable dt = ds.Tables[0];
            dt.Columns["departname"].Caption = "部门名称";
            dt.Columns["proj_sum"].Caption = "应处理案卷总数";
            dt.Columns["notdeal"].Caption = "未处理案卷数";
            dt.Columns["overdeal"].Caption = "超时处理案卷数";
            dt.Columns["complaint"].Caption = "投诉案卷数";
            dt.Columns["notdeal_point"].Caption = "案卷未处理扣分";
            dt.Columns["overtimedeal_point"].Caption = "案卷超时处理扣分";
            dt.Columns["complaint_point"].Caption = "投诉案件扣分";
            dt.Columns["result_point"].Caption = "部门综合得分";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i][0] = "";
            }
            dt.Columns.Remove("sect_type");
            return dt;
        }


        #region GetDepartList:根据模块大类编码，获取相应的部门数据集
        /// <summary>
        /// 根据模块大类编码，获取相应的部门数据集
        /// </summary>
        /// <param name="bigModelId">哪类模块编码</param>
        /// <returns></returns>
        public DataTable GetDepartList(string bigModelId)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@bigModelId",bigModelId), 
                                };

            DataSet ds = ExecuteDataset("pr_a_GetDepartList", CommandType.StoredProcedure, arrSP);

            return ds.Tables[0];
        }
        #endregion
       
        ////调用周的存储过程获取一周的评价结果
        //public DataTable GetDataByWeek(int departcode, int year, int type, int number, DateTime start, DateTime end)
        //{
        //    SqlParameter[] arrSP = new SqlParameter[] {
        //                        new SqlParameter("@departids", departcode), 
        //                        new SqlParameter("@selectedyear", year), 
        //                        new SqlParameter("@sectertype", type),
        //                        new SqlParameter("@secternum",number),
        //                        new SqlParameter("@startdate",start),
        //                        new SqlParameter("@enddate",end)};
        //    DataSet ds = ExecuteDataset("GetDataByWeek", CommandType.StoredProcedure, arrSP);
        //    DataTable dt = ds.Tables[0];
        //    dt.Columns["parentname"].Caption = "上级部门名称";
        //    dt.Columns["departname"].Caption = "部门名称";
        //    dt.Columns["notdeal_point"].Caption = "案卷未处理扣分";
        //    dt.Columns["overtimedeal_point"].Caption = "案卷超时处理扣分";
        //    dt.Columns["complaint_point"].Caption = "投诉案件扣分";
        //    dt.Columns["result_point"].Caption = "部门综合得分";
           
        //    for(int i=0;i<dt.Rows.Count;i++)
        //    {
        //        bool tag=true;
        //        for(int j=0;j<dt.Rows.Count;j++)
        //        {
        //            if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
        //           {
        //               tag=false;
        //               break;
        //           }
        //        }
        //        if(tag)
        //            dt.Rows[i][0]="";
        //    }
        //    dt.Columns.Remove("sect_type");
        //    return dt;
        //}


        ////调用月的存储过程获取一月的评价结果
        //public DataTable GetDataByMonth(int departcode, int year, int type, int number, DateTime start, DateTime end)
        //{
        //    SqlParameter[] arrSP = new SqlParameter[] {
        //                        new SqlParameter("@departids", departcode), 
        //                        new SqlParameter("@selectedyear", year), 
        //                        new SqlParameter("@sectertype", type),
        //                        new SqlParameter("@secternum",number),
        //                        new SqlParameter("@startdate",start),
        //                        new SqlParameter("@enddate",end)};
        //    DataSet ds = ExecuteDataset("GetDataByMonth", CommandType.StoredProcedure, arrSP);
        //    DataTable dt = ds.Tables[0];
        //    dt.Columns["parentname"].Caption = "上级部门名称";
        //    dt.Columns["departname"].Caption = "部门名称";
        //    dt.Columns["notdeal_point"].Caption = "案卷未处理扣分";
        //    dt.Columns["overtimedeal_point"].Caption = "案卷超时处理扣分";
        //    dt.Columns["complaint_point"].Caption = "投诉案件扣分";
        //    dt.Columns["result_point"].Caption = "部门综合得分";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        bool tag = true;
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
        //            {
        //                tag = false;
        //                break;
        //            }
        //        }
        //        if (tag)
        //            dt.Rows[i][0] = "";
        //    }
        //    dt.Columns.Remove("sect_type");
        //    return dt;
        //}

        ////调用季的存储过程获取一季的评价结果
        //public DataTable GetDataByQuarter(int departcode, int year, int type, int number, DateTime start, DateTime end)
        //{
        //    SqlParameter[] arrSP = new SqlParameter[] {
        //                        new SqlParameter("@departids", departcode), 
        //                        new SqlParameter("@selectedyear", year), 
        //                        new SqlParameter("@sectertype", type),
        //                        new SqlParameter("@secternum",number),
        //                        new SqlParameter("@startdate",start),
        //                        new SqlParameter("@enddate",end)};
        //    DataSet ds = ExecuteDataset("GetDataByQuarter", CommandType.StoredProcedure, arrSP);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        if (ds.Tables[0] != null && Convert.ToInt32(ds.Tables[0].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[0];
        //        else if (ds.Tables[1] != null && Convert.ToInt32(ds.Tables[1].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[1];
        //        else if (ds.Tables[2] != null && Convert.ToInt32(ds.Tables[2].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[2];
        //        else if (ds.Tables[3] != null && Convert.ToInt32(ds.Tables[3].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[3];
        //        else if (ds.Tables[4] != null && Convert.ToInt32(ds.Tables[4].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[4];
        //        else if (ds.Tables[5] != null && Convert.ToInt32(ds.Tables[5].Rows[0]["sect_type"]) == 2)
        //            dt = ds.Tables[5];
        //        else
        //            dt = ds.Tables[6];
        //    }
        //    catch
        //    {
        //    }
        //    dt.Columns["parentname"].Caption = "上级部门名称";
        //    dt.Columns["departname"].Caption = "部门名称";
        //    dt.Columns["notdeal_point"].Caption = "案卷未处理扣分";
        //    dt.Columns["overtimedeal_point"].Caption = "案卷超时处理扣分";
        //    dt.Columns["complaint_point"].Caption = "投诉案件扣分";
        //    dt.Columns["result_point"].Caption = "部门综合得分";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        bool tag = true;
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
        //            {
        //                tag = false;
        //                break;
        //            }
        //        }
        //        if (tag)
        //            dt.Rows[i][0] = "";
        //    }
        //    dt.Columns.Remove("sect_type");
        //    return dt;
        //}

        ////调用年的存储过程获取一年的评价结果
        //public DataTable GetDataByYear(int departcode, int year, int type, int number, DateTime start, DateTime end)
        //{
        //    SqlParameter[] arrSP = new SqlParameter[] {
        //                        new SqlParameter("@departids", departcode), 
        //                        new SqlParameter("@selectedyear", year), 
        //                        new SqlParameter("@sectertype", type),
        //                        new SqlParameter("@secternum",number),
        //                        new SqlParameter("@startdate",start),
        //                        new SqlParameter("@enddate",end)};
        //    DataSet ds = ExecuteDataset("GetDataByYear", CommandType.StoredProcedure, arrSP);
        //    DataTable dt = new DataTable();
        //    if (ds.Tables[0] != null && Convert.ToInt32(ds.Tables[0].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[0];
        //    else if (ds.Tables[1] != null && Convert.ToInt32(ds.Tables[1].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[1];
        //    else if (ds.Tables[2] != null && Convert.ToInt32(ds.Tables[2].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[2];
        //    else if (ds.Tables[3] != null && Convert.ToInt32(ds.Tables[3].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[3];

        //    else if (ds.Tables[4] != null && Convert.ToInt32(ds.Tables[4].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[4];
        //    else if (ds.Tables[5] != null && Convert.ToInt32(ds.Tables[5].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[5];
        //    else if (ds.Tables[6] != null && Convert.ToInt32(ds.Tables[6].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[6];
        //    else if (ds.Tables[7] != null && Convert.ToInt32(ds.Tables[7].Rows[0]["sect_type"]) == 3)
        //        dt = ds.Tables[7];

        //    else
        //        dt = ds.Tables[8];
        //    dt.Columns["parentname"].Caption = "上级部门名称";
        //    dt.Columns["departname"].Caption = "部门名称";
        //    dt.Columns["notdeal_point"].Caption = "案卷未处理扣分";
        //    dt.Columns["overtimedeal_point"].Caption = "案卷超时处理扣分";
        //    dt.Columns["complaint_point"].Caption = "投诉案件扣分";
        //    dt.Columns["result_point"].Caption = "部门综合得分";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        bool tag = true;
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
        //            {
        //                tag = false;
        //                break;
        //            }
        //        }
        //        if (tag)
        //            dt.Rows[i][0] = "";
        //    }
        //    dt.Columns.Remove("sect_type");
        //    return dt;
        //}

    }
}
