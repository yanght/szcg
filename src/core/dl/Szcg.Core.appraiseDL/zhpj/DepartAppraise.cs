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
        //���ô洢���̻�ȡ���۽��
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
            dt.Columns["departname"].Caption = "��������";
            dt.Columns["proj_sum"].Caption = "Ӧ����������";
            dt.Columns["notdeal"].Caption = "δ��������";
            dt.Columns["overdeal"].Caption = "��ʱ��������";
            dt.Columns["complaint"].Caption = "Ͷ�߰�����";
            dt.Columns["notdeal_point"].Caption = "����δ����۷�";
            dt.Columns["overtimedeal_point"].Caption = "����ʱ����۷�";
            dt.Columns["complaint_point"].Caption = "Ͷ�߰����۷�";
            dt.Columns["result_point"].Caption = "�����ۺϵ÷�";

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


        #region GetDepartList:����ģ�������룬��ȡ��Ӧ�Ĳ������ݼ�
        /// <summary>
        /// ����ģ�������룬��ȡ��Ӧ�Ĳ������ݼ�
        /// </summary>
        /// <param name="bigModelId">����ģ�����</param>
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
       
        ////�����ܵĴ洢���̻�ȡһ�ܵ����۽��
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
        //    dt.Columns["parentname"].Caption = "�ϼ���������";
        //    dt.Columns["departname"].Caption = "��������";
        //    dt.Columns["notdeal_point"].Caption = "����δ����۷�";
        //    dt.Columns["overtimedeal_point"].Caption = "����ʱ����۷�";
        //    dt.Columns["complaint_point"].Caption = "Ͷ�߰����۷�";
        //    dt.Columns["result_point"].Caption = "�����ۺϵ÷�";
           
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


        ////�����µĴ洢���̻�ȡһ�µ����۽��
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
        //    dt.Columns["parentname"].Caption = "�ϼ���������";
        //    dt.Columns["departname"].Caption = "��������";
        //    dt.Columns["notdeal_point"].Caption = "����δ����۷�";
        //    dt.Columns["overtimedeal_point"].Caption = "����ʱ����۷�";
        //    dt.Columns["complaint_point"].Caption = "Ͷ�߰����۷�";
        //    dt.Columns["result_point"].Caption = "�����ۺϵ÷�";
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

        ////���ü��Ĵ洢���̻�ȡһ�������۽��
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
        //    dt.Columns["parentname"].Caption = "�ϼ���������";
        //    dt.Columns["departname"].Caption = "��������";
        //    dt.Columns["notdeal_point"].Caption = "����δ����۷�";
        //    dt.Columns["overtimedeal_point"].Caption = "����ʱ����۷�";
        //    dt.Columns["complaint_point"].Caption = "Ͷ�߰����۷�";
        //    dt.Columns["result_point"].Caption = "�����ۺϵ÷�";
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

        ////������Ĵ洢���̻�ȡһ������۽��
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
        //    dt.Columns["parentname"].Caption = "�ϼ���������";
        //    dt.Columns["departname"].Caption = "��������";
        //    dt.Columns["notdeal_point"].Caption = "����δ����۷�";
        //    dt.Columns["overtimedeal_point"].Caption = "����ʱ����۷�";
        //    dt.Columns["complaint_point"].Caption = "Ͷ�߰����۷�";
        //    dt.Columns["result_point"].Caption = "�����ۺϵ÷�";
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
