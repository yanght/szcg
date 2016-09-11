/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ�
 * ��    ;���ۺ����۸�λ����ģ���߼���
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-06-19
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
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

                    //if (type == 0)    //ʱ������Ϊ��
                    //{
                    //    sq.intYears = year;
                    //    sq.intWeeks = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByWeek(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 1)    //ʱ������Ϊ��
                    //{
                    //    sq.intYears = year;
                    //    sq.intMonths = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByMonth(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 2)    //ʱ������Ϊ����
                    //{
                    //    sq.intYears = year;
                    //    sq.intQuarter = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDutyDataByQuarter(year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else    //ʱ������Ϊ��
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

            dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable OperatorWorkStatOutPut(string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                   ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetPersonWorkList(name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["TelephonistName"].Caption = "����Ա����";
            dt.Columns["type"].Caption = "������ɫ";
            dt.Columns["streetname"].Caption = "�����ֵ�";
            dt.Columns["NicetyCOUNT"].Caption = "׼ȷ������";
            dt.Columns["pizhuan"].Caption = "��ת��";
            dt.Columns["SUMCOUNT"].Caption = "��������";
            dt.Columns["SendPersonCount"].Caption = "��ǲ����";
            dt.Columns["ErrorSendCount"].Caption = "������ǲ��";
            dt.Columns["Register"].Caption = "����׼ȷ��(%)";
            dt.Columns["Nicety"].Caption = "׼ȷ��ǲ��(%)";
            dt.Columns["day"].Caption = "����";
            dt.Columns["sumPublic"].Caption = "���ھٱ���";
            dt.Columns["sumFeedBack"].Caption = "��������";
            dt.Columns["feedBackRate"].Caption = "��������";
            dt.Columns["cwpz"].Caption = "������ת��";
            dt.Columns["cspz"].Caption = "��ʱ��ת��";
            dt.Columns["still"].Caption = "δ��ʱ�˲���";
            dt.Columns["cwla"].Caption = "����������";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = type + "���˹���ͳ��  ����:" + start + " �� " + end;
            return dt;
        }

        public DataTable ProjectStatOutPutOperator(string num, string name, string type, string streetname, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.dutyappraise.DutyAppraise dl = new bacgDL.zhpj.dutyappraise.DutyAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetPersonalList(num, name, type, streetname, start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["TelephonistName"].Caption = "����Ա����";
            dt.Columns["type"].Caption = "������ɫ";
            dt.Columns["streetname"].Caption = "�����ֵ�";
            dt.Columns["NicetyCOUNT"].Caption = "׼ȷ������";
            dt.Columns["pizhuan"].Caption = "��ת��";
            dt.Columns["SUMCOUNT"].Caption = "��������";
            dt.Columns["SendPersonCount"].Caption = "��ǲ����";
            dt.Columns["ErrorSendCount"].Caption = "������ǲ��";
            dt.Columns["Register"].Caption = "����׼ȷ��(%)";
            dt.Columns["Nicety"].Caption = "׼ȷ��ǲ��(%)";
            dt.Columns["day"].Caption = "����";
            dt.Columns["sumPublic"].Caption = "���ھٱ���";
            dt.Columns["sumFeedBack"].Caption = "��������";
            dt.Columns["feedBackRate"].Caption = "��������";
            dt.Columns["cwpz"].Caption = "������ת��";
            dt.Columns["cspz"].Caption = "��ʱ��ת��";
            dt.Columns["still"].Caption = "δ��ʱ�˲���";
            dt.Columns["cwla"].Caption = "����������";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = "����Ա��������ͳ�����ڣ� " + start + " �� " + end;
            return dt;
        }


    }
}
