/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ�
 * ��    ;���ۺ���������ģ���߼���
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-06-16
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
using System.Data.SqlClient;
using bacgDL.zhpj.areaappraise;

namespace bacgBL.zhpj.area
{
    public class AreaAppraise
    {
        public DataTable getAreaAppraise(int modelid, string parm1, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, "", "", "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, parm2, "", "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string parm3, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, parm2, parm3, "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string parm3, string parm4, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                                ref int pageCount, out string cols, out string ReportMessage)
        {
            DataTable dt = null;
            DataTable dtn = null;
            cols = "";
            ReportMessage = "";
            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //ͩ����Ŀ�޸�
                    if (modelid == 25)
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "ʮ��" };
                        
                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetAreaData(24, parm1, parm2, parm3, parm4, roleid, sqy.startDate, sqy.endDate, field, order, ref rowCount, ref pageCount, out cols, out ReportMessage);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["��������"] = array[i - 1].ToString() + "�·�";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // ע�⣺�����drSource����һ����ͬ�ṹ��DataTable�е�һ�С�
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        dtn.Columns["��������"].ColumnName = "�����·�";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetAreaData(modelid, parm1, parm2, parm3, parm4, roleid, sq.startDate, sq.endDate, field, order, ref rowCount, ref pageCount, out cols, out ReportMessage);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
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
                    endDate = endDate.AddDays(-1);
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
                    endDate = endDate.AddDays(-1);
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

        public DataTable ProjectStatOutPut(int modelid, string qycode, string roleid, string field, string order, string starttime, string endtime, ref int rowCount,
                                                ref int pageCount, DataTable dt_)
        {
            string cols = "";
            string ReportMessage = "";
            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = getAreaAppraise(modelid, qycode, roleid, field, order, starttime, endtime, ref rowCount, ref pageCount, out cols, out ReportMessage).Copy();
            if (modelid == 24)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["pcode"].ToString() != "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }                 
            }
            
            dt.Columns.Remove("pcode");
            dt.Columns.Remove("code");
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
            return dt;
        }
        public DataTable ProjectStatOutPut1(int modelid, string qycode, string parm2, string parm3, string roleid, string field, string order, string starttime, string endtime, ref int rowCount,
                                           ref int pageCount, DataTable dt_)
        {
            string cols = "";
            string ReportMessage = "";
            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = getAreaAppraise(modelid, qycode, parm2, parm3, roleid, field, order, starttime, endtime, ref rowCount, ref pageCount, out cols, out ReportMessage).Copy();
            if (modelid == 24)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["pcode"].ToString() != "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }
            }

            dt.Columns.Remove("pcode");
            dt.Columns.Remove("code");
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
            return dt;
        }
        public DataTable getDepart()
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getDepart();
            }
        }
        public DataTable getImgNum(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum(id);
            }
        }
        public DataTable getImgNum1(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum1(id);
            }
        }
        public DataTable getImgNum2(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum2(id);
            }
        }
        public DataTable getImgNum3(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum3(id);
            }
        }
        #region GetExpress
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridcode"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetExpress(int modelid, string columnname)
        {
            try
            {
                string strNames = "-1";
                string sql = string.Format(@" select Express from a_appraise_field_detail
                                where ISBASE = 0
                                    and Viewname = '{0}'
	                                and model ={1}", columnname, modelid);
                DataTable dt = new DataTable();
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dt = dl.ExecuteDataset(sql).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    strNames = dt.Rows[0]["Express"].ToString();
                }

                return strNames;
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        public DataTable GetExcelTemple(int modelid)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.GetExcelTemple(modelid);
            }
        }

        public DataTable GetExcelTemple2(int modelid)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.GetExcelTemple2(modelid);
            }
        }

        //����ģ��һ
        public DataTable GetZjcgAreaAppraise(string modelid, string departcode, string roleid, string startdate, string enddate)
        {
            DataTable dt = null;
            DataTable dtn = null;

            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //ͩ����Ŀ�޸�
                    if (modelid == "25")
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "ʮ��" };

                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetZjcgAreaDate(departcode, roleid, sqy.startDate, sqy.endDate);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["depName"] = array[i - 1].ToString() + "�·�";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // ע�⣺�����drSource����һ����ͬ�ṹ��DataTable�е�һ�С�
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        //dtn.Columns["��������"].ColumnName = "�����·�";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetZjcgAreaDate(departcode, roleid, sq.startDate, sq.endDate);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����ģ��2����ͩ��������������
        public DataTable GetTxcgAreaAppraise(string modelid, string departcode, string roleid, string startdate, string enddate)
        {
            DataTable dt = null;
            DataTable dtn = null;

            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //ͩ����Ŀ�޸�
                    if (modelid == "25")
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "ʮ��" };

                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetTxcgAreaDate(departcode, roleid, sqy.startDate, sqy.endDate);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["depName"] = array[i - 1].ToString() + "�·�";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // ע�⣺�����drSource����һ����ͬ�ṹ��DataTable�е�һ�С�
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        //dtn.Columns["��������"].ColumnName = "�����·�";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetTxcgAreaDate(departcode, roleid, sq.startDate, sq.endDate);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProjectStatOutPut_zjcg(string modelid, string departcode, string roleid, string starttime, string endtime, DataTable dt_)
        {

            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = GetZjcgAreaAppraise(modelid, departcode, roleid, starttime, endtime).Copy();

            dt.Columns.Remove("depCode");

            if (modelid == "24")
            {
                dt.Columns["depName"].ColumnName = "��������";
            }
            else
            {
                dt.Columns["depName"].ColumnName = "ͳ���·�";
            }
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable ProjectStatOutPut_txcg(string modelid, string departcode, string roleid, string starttime, string endtime, DataTable dt_)
        {

            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = GetTxcgAreaAppraise(modelid, departcode, roleid, starttime, endtime).Copy();
            
            dt.Columns.Remove("depCode");
            //dt.Columns.Remove("dutyid");
            dt.Columns.Remove("code");
            dt.Columns.Remove("pcode");

            if (modelid == "24")
            {
                //dt.Columns["dutyid"].ColumnName = "���";
                dt.Columns["depName"].ColumnName = "��������";
            }
            else
            {
                dt.Columns["depName"].ColumnName = "ͳ���·�";
            }
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable GetEvePartCount(string starttime, string endtime, out string ReportMessage,string streetcode)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetEvePartCount(starttime, endtime,out ReportMessage,streetcode);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
                }

            }
            catch(Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }


        /// <summary>
        /// ��չ�ղ�ͳ��
        /// </summary>
        public DataTable GetUnEvePartCount(string starttime, string endtime, out string ReportMessage, string streetcode)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetUnEvePartCount(starttime, endtime, out ReportMessage, streetcode);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "����ͳ��  ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// �¼��ϱ�
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ReportMessage"></param>
        /// <returns></returns>
        public DataTable GetEventCount(string starttime, string endtime)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetEventCount(starttime, endtime);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// �����ϱ�
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="strReportMessage"></param>
        /// <returns></returns>
        public DataTable GetPartCount(string starttime, string endtime)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetPartCount(starttime, endtime);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "ͳ�����ڣ� " + sq.startDate.ToShortDateString() + "��" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }

        #region ���²�ѯ����2012-10-23
        /// <summary>
        /// ���²�ѯ����
        /// </summary>
        /// <param name="montype">���ͣ�1������ƽְ̨�ܲ��ſ��ˣ�2��ְ�ܲ����ڲ�����3�����ε�λ����</param>
        /// <param name="DateStart">��ʼʱ��</param>
        /// <param name="DateEnd">����ʱ��</param>
        /// <param name="monthint">�ĸ���</param>
        /// <param name="code">���ű��</param>
        /// <returns></returns>
        public DataTable GetappraisebyMonth(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyMonth(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region ���ܲ�ѯ����2012-10-23
        /// <summary>
        /// ���ܲ�ѯ����
        /// </summary>
        /// <param name="montype">���ͣ�1������ƽְ̨�ܲ��ſ��ˣ�2��ְ�ܲ����ڲ�����3�����ε�λ����</param>
        /// <param name="DateStart">��ʼʱ��</param>
        /// <param name="DateEnd">����ʱ��</param>
        /// <param name="monthint">����</param>
        /// <param name="code">���ű��</param>
        /// <returns></returns>
        public DataTable GetappraisebyWeek(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyWeek(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region �����Ȳ�ѯ����2012-10-23
        /// <summary>
        /// �����Ȳ�ѯ����
        /// </summary>
        /// <param name="montype">���ͣ�1������ƽְ̨�ܲ��ſ��ˣ�2��ְ�ܲ����ڲ�����3�����ε�λ����</param>
        /// <param name="DateStart">��ʼʱ��</param>
        /// <param name="DateEnd">����ʱ��</param>
        /// <param name="monthint">�ļ���</param>
        /// <param name="code">���ű��</param>
        /// <returns></returns>
        public DataTable GetappraisebyQuarter(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyQuarter(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region �����ѯ����2012-10-23
        /// <summary>
        /// �����ѯ����
        /// </summary>
        /// <param name="montype">���ͣ�1������ƽְ̨�ܲ��ſ��ˣ�2��ְ�ܲ����ڲ�����3�����ε�λ����</param>
        /// <param name="DateStart">��ʼʱ��</param>
        /// <param name="DateEnd">����ʱ��</param>
        /// <param name="monthint">����</param>
        /// <param name="code">���ű��</param>
        /// <returns></returns>
        public DataTable GetappraisebyYear(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyYear(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion
        #region ����ȵ�λ������ѯ����2012-10-23
        /// <summary>
        /// ����ȵ�λ������ѯ����
        /// </summary>
        /// <param name="code">���ű���</param>
        /// <param name="monthint">����</param>
        /// <returns></returns>
        public DataTable GetappraiseYearbyYear(string code, int monthint)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraiseYearbyYear(code, monthint);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
    }
}
