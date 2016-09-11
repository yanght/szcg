/* ****************************************************************************************
 * 版权所有：杭州天夏科技
 * 用    途：综合评价岗位评价模块逻辑层
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
using bacgDL.zhpj.dutyappraise;

namespace bacgBL.zhpj.duty
{
    public class DutyAppraise
    {

        public DataTable getDutyAppraise(int year, int type, int number,string field, string order)
        {
            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise())
                {
                    //sq.intYears = year;
                    //sq.intWeeks = number;
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
                    return dl.GetDutyData(year, type, number, sq.startDate, sq.endDate, field, order);

                    //if (type == 0)    //时段类型为周
                    //{
                    //    sq.intYears = year;
                    //    sq.intWeeks = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByWeek(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 1)    //时段类型为月
                    //{
                    //    sq.intYears = year;
                    //    sq.intMonths = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByMonth(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 2)    //时段类型为季度
                    //{
                    //    sq.intYears = year;
                    //    sq.intQuarter = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByQuarter(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else    //时段类型为年
                    //{
                    //    sq.intYears = year;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByYear(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SetStatDate(StructQuery sq)
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

        public static DataSet GetPersonWorkList(string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {

            try
            {
                using (bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise())
                {
                    return dl.GetPersonWorkList(name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }


        public static DataSet GetPersonalList(string num, string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {

            try
            {
                using (bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise())
                {
                    return dl.GetPersonalList(num, name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }


        public DataTable ProjectStatOutPut(int year,int type,int number,string field,string order)
        {
            DataTable dt = getDutyAppraise(year,type,number,field,order).Copy();
            dt.Columns.Remove("pcode");
            dt.Columns.Remove("code");
            StructQuery sq = new StructQuery();
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

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable OperatorWorkStatOutPut(string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                   ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetPersonWorkList(name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["TelephonistName"].Caption = "操作员姓名";
            dt.Columns["type"].Caption = "所属角色";
            dt.Columns["streetname"].Caption = "所属街道";
            dt.Columns["NicetyCOUNT"].Caption = "准确立案数";
            dt.Columns["pizhuan"].Caption = "批转数";
            dt.Columns["SUMCOUNT"].Caption = "立案总数";
            dt.Columns["SendPersonCount"].Caption = "派遣总数";
            dt.Columns["ErrorSendCount"].Caption = "错误派遣数";
            dt.Columns["Register"].Caption = "立案准确率(%)";
            dt.Columns["Nicety"].Caption = "准确派遣率(%)";
            dt.Columns["day"].Caption = "日期";
            dt.Columns["sumPublic"].Caption = "公众举报数";
            dt.Columns["sumFeedBack"].Caption = "案卷反馈数";
            dt.Columns["feedBackRate"].Caption = "案卷反馈率";
            dt.Columns["cwpz"].Caption = "错误批转数";
            dt.Columns["cspz"].Caption = "超时批转数";
            dt.Columns["still"].Caption = "未及时核查数";
            dt.Columns["cwla"].Caption = "错误立案数";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = type + "个人工作统计  周期:" + start + " 至 " + end;
            return dt;
        }

        public DataTable ProjectStatOutPutOperator(string num, string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetPersonalList(num, name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["TelephonistName"].Caption = "操作员姓名";
            dt.Columns["type"].Caption = "所属角色";
            dt.Columns["streetname"].Caption = "所属街道";
            dt.Columns["NicetyCOUNT"].Caption = "准确立案数";
            dt.Columns["pizhuan"].Caption = "批转数";
            dt.Columns["SUMCOUNT"].Caption = "立案总数";
            dt.Columns["SendPersonCount"].Caption = "派遣总数";
            dt.Columns["ErrorSendCount"].Caption = "错误派遣数";
            dt.Columns["Register"].Caption = "立案准确率(%)";
            dt.Columns["Nicety"].Caption = "准确派遣率(%)";
            dt.Columns["day"].Caption = "日期";
            dt.Columns["sumPublic"].Caption = "公众举报数";
            dt.Columns["sumFeedBack"].Caption = "案卷反馈数";
            dt.Columns["feedBackRate"].Caption = "案卷反馈率";
            dt.Columns["cwpz"].Caption = "错误批转数";
            dt.Columns["cspz"].Caption = "超时批转数";
            dt.Columns["still"].Caption = "未及时核查数";
            dt.Columns["cwla"].Caption = "错误立案数";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = "操作员个人评价统计周期： " + start + " 至 " + end;
            return dt;
        }


    }
}
