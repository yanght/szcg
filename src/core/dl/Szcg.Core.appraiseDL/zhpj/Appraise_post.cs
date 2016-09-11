using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace bacgDL.zhpj.dutyappraise
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

    public class DutyAppraise : Teamax.Common.CommonDatabase, IDisposable
    {

        //调用周的存储过程获取一周的评价结果
        public DataTable GetDutyData(int year, int type, int number, DateTime start, DateTime end,string field, string order)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@selectedyear", year),
                                new SqlParameter("@sect_type", type),
                                new SqlParameter("@sect_num", number),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end),
                                new SqlParameter("@field",field),
                                new SqlParameter("@order",order)
                                };
            DataSet ds = ExecuteDataset("pr_a_GetPostData", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            dt.Columns["codename"].Caption = "区域名称";
            dt.Columns["Thing"].Caption = "操作员上岗情况得分";
            dt.Columns["Sends"].Caption = "派遣员准确派遣率(%)";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["pcode"].ToString() == dt.Rows[j]["code"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i]["pcode"] = "";
            }
            return dt;
        }


        public DataSet GetPersonWorkList(string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                           ref int pageCount, string strOrder, string strField)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@name",name),
                                new SqlParameter("@type",type),
                                new SqlParameter("@streetname",streetname),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",strOrder),
                                new SqlParameter("@Field",strField),
                             };

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetPersonWorkList", CommandType.StoredProcedure, arrSP);
            rowCount = int.Parse(arrSP[6].Value.ToString());
            pageCount = int.Parse(arrSP[7].Value.ToString());

            return ds;
        }

        public DataSet GetPersonalList(string num, string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                             ref int pageCount, string strOrder, string strField)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@num",num),
                                new SqlParameter("@name",name),
                                new SqlParameter("@type",type),
                                new SqlParameter("@streetname",streetname),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",strOrder),
                                new SqlParameter("@Field",strField),
                             };

            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetOperatorList", CommandType.StoredProcedure, arrSP);
            rowCount = int.Parse(arrSP[7].Value.ToString());
            pageCount = int.Parse(arrSP[8].Value.ToString());
            
            return ds;
            //string sql = "";
            //int number;
            //if(num=="" && name!="")
            //    sql = "select TelephonistName,NicetyCOUNT,SUMCOUNT,SendPersonCount,ErrorSendCount,Register,Nicety,day from a_sample_operator where TelephonistName='"+name+"'";
            //if(num!="" && name=="")
            //{
            //    number=Convert.ToInt32(num);
            //    sql = "select TelephonistName,NicetyCOUNT,SUMCOUNT,SendPersonCount,ErrorSendCount,Register,Nicety,day from a_sample_operator where TelephonistCode='+num+'";
            //}
            //if (num != "" && name != "")
            //{
            //    number = Convert.ToInt32(num);
            //    sql = "select TelephonistName,NicetyCOUNT,SUMCOUNT,SendPersonCount,ErrorSendCount,Register,Nicety,day from a_sample_operator where TelephonistCode='+num+' and TelephonistName='" + name + "'";

            //}
            //if(num=="" && name=="")
            //    sql = "select TelephonistName,NicetyCOUNT,SUMCOUNT,SendPersonCount,ErrorSendCount,Register,Nicety,day from a_sample_operator";

            //DataSet ds = this.ExecuteDataset(sql);
            //return ds;
        }

/*
        //调用月的存储过程获取一月的评价结果
        public DataTable GetDutyDataByMonth(int year, int type, int number, DateTime start, DateTime end)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@selectedyear", year),
                                new SqlParameter("@sect_type", type),
                                new SqlParameter("@sect_num", number),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end)};
            DataSet ds = ExecuteDataset("GetDutyDataByMonth", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //dt.Columns["pcodename"].Caption = "上级区域名称";
            //dt.Columns["codename"].Caption = "本区域名称";
            ;
            dt.Columns["CodeName"].Caption = "区__街道";
            dt.Columns["Thing"].Caption = "操作员上岗情况得分";
            dt.Columns["Sends"].Caption = "派遣员准确派遣率";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["pcode"].ToString() == dt.Rows[j]["code"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i]["pcode"] = "";
            }
            return dt;
        }

        //调用季的存储过程获取一季的评价结果
        public DataTable GetDutyDataByQuarter(int year, int type, int number, DateTime start, DateTime end)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@selectedyear", year),
                                new SqlParameter("@sect_type", type),
                                new SqlParameter("@sect_num", number),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end)};
            DataSet ds = ExecuteDataset("GetDutyDataByQuarter", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //dt.Columns["pcodename"].Caption = "上级区域名称";
            //dt.Columns["codename"].Caption = "本区域名称";
            ;
            dt.Columns["CodeName"].Caption = "区__街道";
            dt.Columns["Thing"].Caption = "操作员上岗情况得分";
            dt.Columns["Sends"].Caption = "派遣员准确派遣率";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["pcode"].ToString() == dt.Rows[j]["code"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i]["pcode"] = "";
            }
            return dt;
        }

        //调用年的存储过程获取一年的评价结果
        public DataTable GetDutyDataByYear(int year, int type, int number, DateTime start, DateTime end)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@selectedyear", year),
                                new SqlParameter("@sect_type", type),
                                new SqlParameter("@sect_num", number),
                                new SqlParameter("@startdate",start),
                                new SqlParameter("@enddate",end)};
            DataSet ds = ExecuteDataset("GetDutyDataByYear", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //dt.Columns["pcodename"].Caption = "上级区域名称";
            //dt.Columns["codename"].Caption = "本区域名称";
            ;
            dt.Columns["CodeName"].Caption = "区__街道";
            dt.Columns["Thing"].Caption = "操作员上岗情况得分";
            dt.Columns["Sends"].Caption = "派遣员准确派遣率";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool tag = true;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["pcode"].ToString() == dt.Rows[j]["code"].ToString())
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)
                    dt.Rows[i]["pcode"] = "";
            }
            return dt;
        }
*/
    }
}
