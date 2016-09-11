/* ****************************************************************************************
 * 版权所有：杭州天夏科技
 * 用    途：综合评价区级平台运行评价模块逻辑层
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-06-19
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using bacgDL.zhpj;
using Teamax.Common;
using bacgDL.zhpj.platform;

namespace bacgBL.zhpj.platform
{
    public class PlatformAppraise
    {

        public DataTable getPlatformAppraise(int year, int type, int number)
        {
            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise())
                {
                    sq.intYears = year;
                    if (type == 0)
                        sq.intWeeks = number;
                    if (type == 1)
                        sq.intMonths = number;
                    if (type == 2)
                        sq.intQuarter = number;
                    if (type == 3)
                        sq.intYears = year;
                    SetStatDate(sq);
                    return dl.GetPlatformData(year, type, number, sq.startDate, sq.endDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetStatDate(StructQuery sq)
        {
            DateTime startDate, endDate;
            if (sq.intMonths != 0)
            {
                startDate = new DateTime(sq.intYears, sq.intMonths, 1);
                if (sq.intMonths != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intMonths + 1, 1);
                    endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intQuarter != 0)
            {
                startDate = new DateTime(sq.intYears, (sq.intQuarter - 1) * 3 + 1, 1);

                if (sq.intQuarter * 3 != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intQuarter * 3 + 1, 1);
                    endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intWeeks != 0)
            {
                DateTime dt = new DateTime(sq.intYears, 1, 1);
                int weeknow = Convert.ToInt32(dt.DayOfWeek);
                startDate = dt.AddDays(-1 * weeknow + (sq.intWeeks - 1) * 7 + 1);
                endDate = dt.AddDays(-1 * weeknow + (sq.intWeeks) * 7);
            }
            else
            {
                startDate = new DateTime(sq.intYears, 1, 1);
                endDate = new DateTime(sq.intYears, 12, 31);
            }
            sq.startDate = startDate;
            sq.endDate = endDate;
        }

        public static DataSet GetPersonalList(string street, string num, string name, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {

            try
            {
                using (bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise())
                {
                    return dl.GetPersonalList(street, num, name, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        public static DataSet GetjdList(string street,string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                   ref int pageCount, string strOrder, string strField, ref string strErr)
        {

            try
            {
                using (bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise())
                {
                    return dl.GetjdList(street, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        public DataTable ProjectStatOutPutJdy(string street, string num, string name, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetPersonalList(street, num, name, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["StreetName"].Caption = "街道名称";
            dt.Columns["SupervisorName"].Caption = "监督员姓名";
            dt.Columns["SupervisorSum"].Caption = "上报总数";
            dt.Columns["Common"].Caption = "普通上报数";
            dt.Columns["Speediness"].Caption = "快速上报数";
            dt.Columns["Nicety"].Caption = "有效上报数";
            dt.Columns["Availabity"].Caption = "有效上报率(%)";
            dt.Columns["TODAY"].Caption = "上报日期";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = "监督员评价统计周期： " + start + " 至 " + end;
            return dt;
        }


        public DataTable ProjectStatOutPutJd(string street, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                  ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetjdList(street,start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["streetname"].Caption = "街道名称";
            dt.Columns["sumreport"].Caption = "上报总数";
            dt.Columns["common"].Caption = "普通上报数";
            dt.Columns["speed"].Caption = "快速上报数";
            dt.Columns["nice"].Caption = "有效上报数";
            dt.Columns["availabity"].Caption = "有效上报率(%)";
            dt.Columns["date"].Caption = "上报日期";
      
            dt.TableName = "各街道监督员案卷上报信息评价统计周期： " + start + " 至 " + end;
            return dt;
        }


    }
}
