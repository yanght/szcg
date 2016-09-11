/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ�
 * ��    ;���ۺ���������ƽ̨��������ģ���߼���
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
            dt.Columns["StreetName"].Caption = "�ֵ�����";
            dt.Columns["SupervisorName"].Caption = "�ලԱ����";
            dt.Columns["SupervisorSum"].Caption = "�ϱ�����";
            dt.Columns["Common"].Caption = "��ͨ�ϱ���";
            dt.Columns["Speediness"].Caption = "�����ϱ���";
            dt.Columns["Nicety"].Caption = "��Ч�ϱ���";
            dt.Columns["Availabity"].Caption = "��Ч�ϱ���(%)";
            dt.Columns["TODAY"].Caption = "�ϱ�����";
            //StructQuery sq = new StructQuery();
            //sq.startDate = Convert.ToDateTime(start);
            //sq.endDate = Convert.ToDateTime(end);
            dt.TableName = "�ලԱ����ͳ�����ڣ� " + start + " �� " + end;
            return dt;
        }


        public DataTable ProjectStatOutPutJd(string street, string start, string end, int curentpage, int pagesize, ref int rowCount,
                                                  ref int pageCount, string strOrder, string strField, ref string strErr)
        {
            bacgDL.zhpj.platform.PlatformAppraise dl = new bacgDL.zhpj.platform.PlatformAppraise();
            DataTable dt = new DataTable();
            dt = dl.GetjdList(street,start, end, curentpage, pagesize, ref rowCount, ref pageCount, strOrder, strField).Tables[0].Copy();
            dt.Columns["streetname"].Caption = "�ֵ�����";
            dt.Columns["sumreport"].Caption = "�ϱ�����";
            dt.Columns["common"].Caption = "��ͨ�ϱ���";
            dt.Columns["speed"].Caption = "�����ϱ���";
            dt.Columns["nice"].Caption = "��Ч�ϱ���";
            dt.Columns["availabity"].Caption = "��Ч�ϱ���(%)";
            dt.Columns["date"].Caption = "�ϱ�����";
      
            dt.TableName = "���ֵ��ලԱ�����ϱ���Ϣ����ͳ�����ڣ� " + start + " �� " + end;
            return dt;
        }


    }
}
