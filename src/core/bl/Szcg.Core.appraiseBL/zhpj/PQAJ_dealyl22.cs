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
        #region GetTracyForPQ:ͳ�ƺ˲���Ϣ
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

        #region GetTracyForPQ:ͳ�ƺ˲���Ϣ
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
        /// ��ȡ�������۵���ϸ��Ϣ
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
        /// ��ȡ�������۵���ϸ��Ϣ
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
        /// ��ȡ��λ���۵���ϸ��Ϣ
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
        /// ��ȡ��ϸ��Ϣ��Ҫ�鿴�е�����
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
        /// ��ȡ������
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

        #region GetStreetDataInfo����ȡ�ֵ�ͳ�ư�����б�
        /// <summary>
        /// ��ȡ�ֵ�ͳ�ư�����б�
        /// </summary>
        /// <param name="streetcode">�ֵ�����</param>
        /// <param name="DataField">��ѯ����</param>
        /// <param name="DateStart">��ʼ����</param>
        /// <param name="DateEnd">��������</param>
        /// <param name="SearchYear">��ѯ��</param>
        /// <param name="page">��ҳ�ṹ��</param>
        /// <param name="ErrMsg">���ش�����Ϣ</param>
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

        #region GetZRDWDataInfo����ȡ���ε�λͳ�ư�����б�
        /// <summary>
        /// ��ȡ���ε�λͳ�ư�����б�
        /// </summary>
        /// <param name="UserDefinedCode">��λ�Զ������</param>
        /// <param name="DataField">��ѯ����</param>
        /// <param name="DateStart">��ʼ����</param>
        /// <param name="DateEnd">��������</param>
        /// <param name="SearchYear">��ѯ��</param>
        /// <param name="page">��ҳ�ṹ��</param>
        /// <param name="ErrMsg">���ش�����Ϣ</param>
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

        #region GetGWDataInfo����ȡ��λ����ͳ�ư�����б�
        /// <summary>
        /// ��ȡ��λ����ͳ�ư�����б�
        /// </summary>
        /// <param name="UserCode">�û�����</param>
        /// <param name="DataField">��ѯ����</param>
        /// <param name="DateStart">��ʼ����</param>
        /// <param name="DateEnd">��������</param>
        /// <param name="SearchYear">��ѯ��</param>
        /// <param name="page">��ҳ�ṹ��</param>
        /// <param name="ErrMsg">���ش�����Ϣ</param>
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

        #region GetXCYDataInfo����ȡѲ��������ͳ�ư�����б�
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

        #region GetXCYDataInfo����ȡ�ܰ���ͳ�ư�����б�
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

        #region RptCenterDailyDeal���������ලָ�������մ�����Ϣһ����
        /// <summary>
        /// �������ලָ�������մ�����Ϣһ����
        /// </summary>
        /// <param name="DateStart">��ѯ��ʼ����</param>
        /// <param name="DateEnd">��ѯ��������</param>
        /// <param name="ErrMsg">������Ϣ</param>
        /// <returns>�����</returns>
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

        #region RptCenterDailyDeal_Detail���������ලָ�������մ�����Ϣһ�����ĳ����ϸ��¼
        /// <summary>
        /// �������ලָ�������մ�����Ϣһ�����ĳ����ϸ��¼
        /// </summary>
        /// <param name="StreetID">�ֵ�</param>
        /// <param name="column">����</param>
        /// <param name="page">ҳ�����</param>
        /// <param name="DateStart">��ѯ��ʼ����</param>
        /// <param name="DateEnd">��ѯ��������</param>
        /// <param name="ErrMsg">������Ϣ</param>
        /// <returns>�����</returns>
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

        #region GetDropListDataInfo����ȡ�ۺ�����ҳ�������б�һЩ����Ϣ
        /// <summary>
        /// ���ط���
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
        /// ��ȡ�ۺ�����ҳ�������б�һЩ����Ϣ
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="parm1">��ѡ����1</param>
        /// <param name="parm2">��ѡ����2</param>
        /// <param name="parm3">��ѡ����3</param>
        /// <param name="parm4">��ѡ����4</param>
        /// <param name="parm5">��ѡ����5</param>
        /// <param name="ErrMsg">���ش�����Ϣ</param>
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

        #region GetZRDWDataInfo1����ȡ���ε�λ����ͳ�ư�����б�
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

