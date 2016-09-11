using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using bacgDL.business;

namespace bacgDL.zhpj
{
    public class PQAJ_dealyl : Teamax.Common.CommonDatabase
    {
        #region GetTracyForPQ:��ȡ�˲���Ϣ�б�
        /// <summary>
        /// ��ȡ�˲���Ϣ�б�
        /// </summary>
        /// <param name="street">�ֵ�����</param>
        /// <param name="loginname">��½��</param>
        /// <param name="collname">����</param>
        /// <param name="mobile">�ǹ�ͨ</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="strHCFlag">�˲��״̬</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="strOrder">����ʽ</param>
        /// <param name="strField">�����ֶ�</param>
        /// <returns></returns>
        public DataSet GetTracyForPQ(string projcode, string UserDefinedCode, string begin, string end,
                                            int curentpage, int pagesize, ref int rowCount,
                                            ref int pageCount, string strOrder, string strField, string IsReturn, string IsOvertime, string AreaCode)
        {
            SqlParameter[] spInputs = new SqlParameter[]
            {             
                new SqlParameter("@projcode",projcode),
                new SqlParameter("@UserDefinedCode", UserDefinedCode),
                new SqlParameter("@DateStart",begin),
                new SqlParameter("@DateEnd", end),  
                new SqlParameter("@CurrentPage",curentpage),
                new SqlParameter("@RowCount",SqlDbType.Int),
                new SqlParameter("@PageCount",SqlDbType.Int),
                new SqlParameter("@PageSize",pagesize),
                new SqlParameter("@Order",strOrder),
                new SqlParameter("@Field",strField),
                new SqlParameter("@IsReturn",IsReturn),
                new SqlParameter("@IsOvertime",IsOvertime),
                new SqlParameter("@AreaCode",AreaCode)
            };

            spInputs[5].Direction = ParameterDirection.Output;
            spInputs[6].Direction = ParameterDirection.Output;
            this._CommandTimeOut=1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetTraceForPQ", CommandType.StoredProcedure, spInputs);
            rowCount = int.Parse(spInputs[5].Value.ToString());
            pageCount = int.Parse(spInputs[6].Value.ToString());

            return ds;
        }
        #endregion

        #region GetTracyForPQ:��ȡ�˲���Ϣ�б�
        /// <summary>
        /// ��ȡ�˲���Ϣ�б�
        /// </summary>
        /// <param name="street">�ֵ�����</param>
        /// <param name="loginname">��½��</param>
        /// <param name="collname">����</param>
        /// <param name="mobile">�ǹ�ͨ</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="strHCFlag">�˲��״̬</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="strOrder">����ʽ</param>
        /// <param name="strField">�����ֶ�</param>
        /// <returns></returns>
        public DataSet GetTracyForPQ(string projcode, string UserDefinedCode, string begin, string end,
                                            int curentpage, int pagesize, ref int rowCount,
                                            ref int pageCount, string strOrder, string strField)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {             
                    new SqlParameter("@projcode",projcode),
                    new SqlParameter("@UserDefinedCode", UserDefinedCode),
                    new SqlParameter("@DateStart",begin),
                    new SqlParameter("@DateEnd", end),  
                    new SqlParameter("@CurrentPage",curentpage),
                    new SqlParameter("@RowCount",SqlDbType.Int),
                    new SqlParameter("@PageCount",SqlDbType.Int),
                    new SqlParameter("@PageSize",pagesize),
                    new SqlParameter("@Order",strOrder),
                    new SqlParameter("@Field",strField),
                };

            spInputs[5].Direction = ParameterDirection.Output;
            spInputs[6].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetTraceForPQ", CommandType.StoredProcedure, spInputs);
            rowCount = int.Parse(spInputs[5].Value.ToString());
            pageCount = int.Parse(spInputs[6].Value.ToString());

            return ds;
        }
        #endregion

        #region GetDealProjectCount:��ȡ�˲���Ϣ�б�
        /// <summary>
        /// ��ȡ�˲���Ϣ�б�
        /// </summary>
        /// <param name="street">�ֵ�����</param>
        /// <param name="loginname">��½��</param>
        /// <param name="collname">����</param>
        /// <param name="mobile">�ǹ�ͨ</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="strHCFlag">�˲��״̬</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <param name="strOrder">����ʽ</param>
        /// <param name="strField">�����ֶ�</param>
        /// <returns></returns>
        public DataSet GetDealProjectCount(string begin, string end, string strOrder, string strField)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {            
                    new SqlParameter("@times1",begin),
                    new SqlParameter("@times2", end),  
                    new SqlParameter("@Order",strOrder),
                    new SqlParameter("@Field",strField),
                };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetDealProjectCount", CommandType.StoredProcedure, spInputs);

            return ds;
        }
        #endregion

        public DataSet GetDealProjectCountDeatail(string departcode, string column, string IsStart, string IsEnd)
        {
            SqlParameter[] spInputs = new SqlParameter[]
            {
                new SqlParameter("@DepartCode",departcode),
                new SqlParameter("@Column",column),
                new SqlParameter("@times1",IsStart),
                new SqlParameter("@times2",IsEnd),
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetDealCountForDetail", CommandType.StoredProcedure, spInputs);

            return ds;
        }

        public DataTable GetEventPartTotal(string areacode, string typecode, string bigCode, string smallCode, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] spInputs = new SqlParameter[]{  
                new SqlParameter("@areacode", areacode),
                new SqlParameter("@typecode", typecode),
                new SqlParameter("@bigclass", bigCode),
                new SqlParameter("@smallclass",smallCode),
                new SqlParameter("@DateStart", startDate),
                new SqlParameter("@DateEnd", endDate),
            };
            this._CommandTimeOut = 1200;
            DataSet ds = ExecuteDataset("pr_a_Rpt_EventPartTotal", CommandType.StoredProcedure, spInputs);
            return ds.Tables[0];
        }

        /// <summary>
        /// ��ȡ�������۰�������ϸ��Ϣ
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataSet GetAreaProjectDetail(string areacode, string column, string IsStart, string IsEnd, PageInfo page)
        {
            SqlParameter[] spInputs = new SqlParameter[]
            {
                new SqlParameter("@Areacode",areacode),
                new SqlParameter("@DataField",column),
                new SqlParameter("@DateStart",Convert.ToDateTime(IsStart)),
                new SqlParameter("@DateEnd",Convert.ToDateTime(IsEnd)),
                new SqlParameter("@CurrentPage",page.CurrentPage),
                new SqlParameter("@RowCount",SqlDbType.Int),
                new SqlParameter("@PageCount",SqlDbType.Int),
                new SqlParameter("@PageSize",page.PageSize),
                new SqlParameter("@Order",page.Order),
                new SqlParameter("@Field",page.Field)};

            spInputs[5].Direction = ParameterDirection.Output;
            spInputs[6].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetAreaDataInfo", CommandType.StoredProcedure, spInputs);
            page.RowCount = spInputs[5].Value.ToString();
            page.PageCount = spInputs[6].Value.ToString();
            return ds;
        }

        /// <summary>
        /// ��ȡ�������۰�������ϸ�б�
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataSet GetDepartProjectDetail(string departcode, string column, string IsStart, string IsEnd, PageInfo page)
        {
            SqlParameter[] spInputs = new SqlParameter[]
            {
                new SqlParameter("@departcode",departcode),
                new SqlParameter("@DataField",column),
                new SqlParameter("@DateStart",Convert.ToDateTime(IsStart)),
                new SqlParameter("@DateEnd",Convert.ToDateTime(IsEnd)),
                new SqlParameter("@CurrentPage",page.CurrentPage),
                new SqlParameter("@RowCount",SqlDbType.Int),
                new SqlParameter("@PageCount",SqlDbType.Int),
                new SqlParameter("@PageSize",page.PageSize),
                new SqlParameter("@Order",page.Order),
                new SqlParameter("@Field",page.Field)};

            spInputs[5].Direction = ParameterDirection.Output;
            spInputs[6].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetDepartDataInfo", CommandType.StoredProcedure, spInputs);
            page.RowCount = spInputs[5].Value.ToString();
            page.PageCount = spInputs[6].Value.ToString();
            return ds;
        }

        /// <summary>
        /// ��ȡ��λ���۵İ�������ϸ�б�
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="usercode"></param>
        /// <param name="column"></param>
        /// <param name="IsStart"></param>
        /// <param name="IsEnd"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataSet GetWorkProjectDetail(string departcode, string usercode, string column, string IsStart, string IsEnd, PageInfo page)
        {
            SqlParameter[] spInputs = new SqlParameter[]
            {
                new SqlParameter("@departcode",departcode),
                new SqlParameter("@usercode",usercode),
                new SqlParameter("@DataField",column),
                new SqlParameter("@DateStart",Convert.ToDateTime(IsStart)),
                new SqlParameter("@DateEnd",Convert.ToDateTime(IsEnd)),
                new SqlParameter("@CurrentPage",page.CurrentPage),
                new SqlParameter("@RowCount",SqlDbType.Int),
                new SqlParameter("@PageCount",SqlDbType.Int),
                new SqlParameter("@PageSize",page.PageSize),
                new SqlParameter("@Order",page.Order),
                new SqlParameter("@Field",page.Field)};

            spInputs[6].Direction = ParameterDirection.Output;
            spInputs[7].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetWorkDataInfo", CommandType.StoredProcedure, spInputs);
            page.RowCount = spInputs[6].Value.ToString();
            page.PageCount = spInputs[7].Value.ToString();
            return ds;
        }

        /// <summary>
        /// ��ȡ��Ҫ�鿴��Ϣ������
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public DataTable GetColumnName(string column)
        {
            string strSql = string.Format(@"select viewname from a_appraise_field_detail where express='{0}'", column);
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(strSql);

            return ds.Tables[0];
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public DataTable GetAreaName(string areacode)
        {
            string strSql = "";
            if (areacode.Length == 9)
            {
                strSql = string.Format(@"select streetname as areaname from s_street where streetcode='{0}'", areacode);
            }
            else
            {
                strSql = string.Format(@"select commname as areaname from s_community where commcode='{0}'", areacode);
            }

            DataSet ds = this.ExecuteDataset(strSql);

            return ds.Tables[0];
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
        /// <returns></returns>
        public DataSet GetStreetDataInfo(string streetcode, string DataField, string DateStart, string DateEnd, string SearchYear, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetCode",streetcode),
                                new SqlParameter("@DataField",DataField),
                                new SqlParameter("@SearchYear",SearchYear),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetStreetDataInfo", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
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
        /// <returns></returns>
        public DataSet GetZRDWDataInfo(string UserDefinedCode, string DataField, string DateStart, string DateEnd, string SearchYear, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserDefinedCode",UserDefinedCode),
                                new SqlParameter("@DataField",DataField),
                                new SqlParameter("@SearchYear",SearchYear),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetZFDDataInfo", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
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
        /// <returns></returns>
        public DataSet GetGWDataInfo(string UserCode, string DataField, string DateStart, string DateEnd, string SearchYear, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserCode",UserCode),
                                new SqlParameter("@DataField",DataField),
                                new SqlParameter("@SearchYear",SearchYear),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetCZYDataInfo", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
        }
        #endregion

        #region GetXCYataInfo����ȡ�ලԱ��ͳ�ư�����б�
        /// <summary>
        /// ��ȡ��λ����ͳ�ư�����б�
        /// </summary>
        /// <param name="UserCode">�û�����</param>
        /// <param name="DataField">��ѯ����</param>
        /// <param name="DateStart">��ʼ����</param>
        /// <param name="DateEnd">��������</param>
        /// <param name="SearchYear">��ѯ��</param>
        /// <param name="page">��ҳ�ṹ��</param>
        /// <returns></returns>
        public DataSet GetXCYataInfo(string UserCode,  string DataField,string DateStart, string DateEnd, string SearchYear, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@collcode",UserCode),
                                new SqlParameter("@SearchYear",SearchYear),
                                new SqlParameter("@DateStart_",DateStart),
                                new SqlParameter("@DateEnd_",DateEnd),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
				new SqlParameter("@DataField",DataField),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetXCYDataInfo", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }
        #endregion

        #region GetXCYataInfo����ȡ�ලԱ��ͳ�ư�����б�
        /// <summary>
        /// ��ȡ��λ����ͳ�ư�����б�
        /// </summary>
        /// <param name="UserCode">�û�����</param>
        /// <param name="DataField">��ѯ����</param>
        /// <param name="DateStart">��ʼ����</param>
        /// <param name="DateEnd">��������</param>
        /// <param name="SearchYear">��ѯ��</param>
        /// <param name="page">��ҳ�ṹ��</param>
        /// <returns></returns>
        public DataSet GetZAJDataInfo(string strType ,PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@strType",strType),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[2].Direction = ParameterDirection.Output;
            arrSP[3].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetZAJDataInfo", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[2].Value.ToString();
            page.PageCount = arrSP[3].Value.ToString();

            return ds;
        }
        #endregion

        #region GetDropListDataInfo����ȡ�ۺ�����ҳ�������б�һЩ����Ϣ
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
        public DataSet GetDropListDataInfo(string areacode, string parm1, string parm2, string parm3, string parm4, string parm5)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@areacode",areacode),
                                new SqlParameter("@parm1",parm1),
                                new SqlParameter("@parm2",parm2),
                                new SqlParameter("@parm3",parm3),
                                new SqlParameter("@parm4",parm4),                                
                                new SqlParameter("@parm5",parm5)};
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetDropListDataInfo", CommandType.StoredProcedure, arrSP);

            return ds;
        }
        #endregion

        #region GetZRDWDataInfo1����ȡ���ε�λ����ͳ�ư�����б�
        /// <summary>
        /// ��ȡ���ε�λ����ͳ�ư�����б� 
        /// </summary>
        public DataSet GetZRDWDataInfo1(string UserDefinedCode, string AreaCode, string AppriseType1, string AppriseType2, string DateStart, string DateEnd, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserDefinedCode",UserDefinedCode),
                                new SqlParameter("@AreaCode",AreaCode),
                                new SqlParameter("@AppriseType1",AppriseType1),
                                new SqlParameter("@AppriseType2",AppriseType2),                                
                                new SqlParameter("@DateStart_",DateStart),
                                new SqlParameter("@DateEnd_",DateEnd),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};
            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;
            // DataSet ds = this.ExecuteDataset("pr_a_ProjectList", CommandType.StoredProcedure, arrSP);
            DataSet ds = this.ExecuteDataset("pr_a_ProjectList_TX", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[7].Value.ToString();
            page.PageCount = arrSP[8].Value.ToString();
            return ds;
        }
        #endregion

        #region GetZRDWDataInfo1����ȡ���ε�λ����ͳ�ư�����б�
        /// <summary>
        /// ��ȡ���ε�λ����ͳ�ư�����б� 
        /// </summary>
        public DataSet GetZRDWDataInfo2(string UserDefinedCode, string AreaCode, string AppriseType1, string AppriseType2, string DateStart, string DateEnd, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserDefinedCode",UserDefinedCode),
                                new SqlParameter("@AreaCode",AreaCode),
                                new SqlParameter("@AppriseType1",AppriseType1),
                                new SqlParameter("@AppriseType2",AppriseType2),                                
                                new SqlParameter("@DateStart_",DateStart),
                                new SqlParameter("@DateEnd_",DateEnd),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};
            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;
            // DataSet ds = this.ExecuteDataset("pr_a_ProjectList", CommandType.StoredProcedure, arrSP);
            DataSet ds = this.ExecuteDataset("pr_a_ProjectList_TX_depart", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[7].Value.ToString();
            page.PageCount = arrSP[8].Value.ToString();
            return ds;
        }
        #endregion

    }
}
