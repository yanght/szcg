using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using dl.zhifa;
using dbl.zhifa;
using Teamax.Common;

namespace bl.zhifa
{
    public class FlowDealAction
    {

        #region GetFlownodeTree����ȡ��ѡ�����̵Ľڵ���
        /// <summary>
        /// ��ȡ��ѡ�����̵Ľڵ���
        /// </summary>
        /// <param name="flowinfoid">����ID</param>
        /// <returns></returns>
        public DataSet GetFlownodeTree(string flowinfoid, string flowname, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("p_i_id", flowinfoid),
                                                              new SqlParameter("p_i_name", flowname)};
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_p_GetFlownodeTree", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetFlowNodePower����ȡ��ѡ����ɫ�Ŀɿ���ģ��;����������
        /// <summary>
        /// ��ȡ��ѡ����ɫ�Ŀɿ���ģ��;����������
        /// </summary>
        /// <param name="roleID">��ɫ���</param>
        /// <returns></returns>
        public DataSet GetFlowNodePower(string roleID, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("fid", roleID)};
           
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_p_GetFlowNodePower", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDealWorkList����ȡ��ѡ�����ż��׶εĿɴ���ҵ������
        /// <summary>
        /// ��ȡ��ѡ�����ż��׶εĿɴ���ҵ������
        /// </summary>
        /// <param name="projinfo">������Ϣ����</param>
        /// <param name="departcode">���ű��</param>
        /// <param name="p_i_field">�����ֶ�</param>
        /// <param name="p_i_order">����ʽ</param>
        /// <param name="p_i_pagesize">ҳ���С</param>
        /// <param name="p_i_CurrentPage">��ǰҳ��</param>
        /// <param name="p_o_rowcount">�ܼ�¼����</param>
        /// <param name="p_o_pagecount">��ҳ����</param>
        /// <returns></returns>
        public DataSet GetDealWorkList(projinfo projinfo,int departcode,string p_i_field, string p_i_order,int p_i_pagesize,
                                         int p_i_CurrentPage,out int p_o_rowcount, out int p_o_pagecount, ref string strErr)
        {
            p_o_rowcount = 0;
            p_o_pagecount = 0;
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("fid", projinfo.Nodeid), 
                                                              new SqlParameter("ftype", projinfo.Functiontype),
                                                              new SqlParameter("hid", departcode),
                                                              new SqlParameter("p_i_projcode", projinfo.Projcode),
                                                              new SqlParameter("p_i_projname", projinfo.Projname),
                                                              new SqlParameter("p_i_projaddress", projinfo.Projaddress),
                                                              new SqlParameter("p_i_statrtdate", projinfo.Startdate),
                                                              new SqlParameter("p_i_enddate", projinfo.Enddate),
                                                              new SqlParameter("p_i_field", p_i_field),
                                                              new SqlParameter("p_i_order", p_i_order),
                                                              new SqlParameter("p_i_pagesize", p_i_pagesize),
                                                              new SqlParameter("p_i_CurrentPage", p_i_CurrentPage),
                                                              new SqlParameter("p_o_rowcount", SqlDbType.Int),
                                                              new SqlParameter("p_o_pagecount", SqlDbType.Int),
                                                            new SqlParameter("p_i_summary", projinfo.Summary)};
            arrSP[12].Direction = ParameterDirection.InputOutput;
            arrSP[13].Direction = ParameterDirection.Output;
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    DataSet ds = dl.ExecProc("pr_b_GetDealWorkList_zhifa", ref arrSP);
                    p_o_rowcount = Convert.ToInt32(arrSP[12].Value);
                    p_o_pagecount = Convert.ToInt32(arrSP[13].Value);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetProjectTraceList����ȡ��ѡ�������Ĵ�������
        /// <summary>
        /// ��ȡ��ѡ�������Ĵ�������
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetProjectTraceList(int projcode,ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@p_i_fid", projcode) };
        
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_b_GetProjectTraceList_zhifa", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetProjectInfo����ȡ��ѡ����������ϸ��Ϣ
        /// <summary>
        /// ��ȡ��ѡ����������ϸ��Ϣ
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetProjectInfo(int projcode, ref string strErr)
        {            
            try
            {
                return this.GetProjectInfo(projcode, 0,ref strErr);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetProjectInfo����ȡ��ѡ����������ϸ��Ϣ
        /// <summary>
        /// ��ȡ��ѡ����������ϸ��Ϣ
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetProjectInfo(int projcode,int nodeid, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@p_i_fid", projcode),    
                                                             new SqlParameter("@p_i_nid", nodeid)};
            
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_b_GetProjectInfo_zhifa", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region  GetDiction�� ȡϰ����������

        /// <summary>
        /// ȡϰ���������ݣ����磺 �칫����
        /// </summary>
        /// <param name="DictionCode">�ֵ����</param>
        /// <param name="stepid">�������</param>
        /// <returns></returns>
        public DataTable GetDiction(string DictionCode, string stepid)
        {
            string sql = string.Format("select * from s_diction_sentence where DictionCode='{0}' and stepcode = '{1}'", DictionCode, stepid);
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecuteDataset(sql).Tables[0];
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region UpdateProject���޸ĸ��°�����Ϣ 
        /// <summary>
        /// �޸ĸ��°�����Ϣ
        /// </summary>
        /// <param name="projinfo">������Ϣ����</param>
        /// <param name="departcode">���ű��</param>
        /// <param name="p_i_field">�����ֶ�</param>
        /// <param name="p_i_order">����ʽ</param>
        /// <param name="p_i_pagesize">ҳ���С</param>
        /// <param name="p_i_CurrentPage">��ǰҳ��</param>
        /// <param name="p_o_rowcount">�ܼ�¼����</param>
        /// <param name="p_o_pagecount">��ҳ����</param>
        /// <returns></returns>
        public int UpdateProject(projinfo projinfo, ref int err, ref string Errmsg)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@p_i_nodeid", projinfo.Nodeid), 
                                                              new SqlParameter("@p_i_busistatus",projinfo.busistatus),
                                                              new SqlParameter("@p_i_usercode", projinfo.Usercode),
                                                              new SqlParameter("@p_i_Username", projinfo.Username),
                                                              new SqlParameter("@p_i_Targetdepartcode", projinfo.Targetdepartcode),
                                                              new SqlParameter("@p_i_Probsource", projinfo.Probsource),
                                                              new SqlParameter("@p_i_Isgreat", projinfo.Isgreat),
                                                              new SqlParameter("@p_i_Functiontype", projinfo.Functiontype),
                                                              new SqlParameter("@p_i_Deal_opinion", projinfo.Deal_opinion),
                                                              new SqlParameter("@p_i_statrtdate", projinfo.Startdate),
                                                              new SqlParameter("@p_i_enddate", projinfo.Enddate),
                                                              new SqlParameter("@p_i_cu_date", projinfo.Cu_date),
                                                              new SqlParameter("@p_i_Cu_departcode", projinfo.Cu_departcode),
                                                              new SqlParameter("@p_i_Cu_usercode", projinfo.Cu_usercode),
                                                              new SqlParameter("@p_i_projcode", projinfo.Projcode),
                                                              new SqlParameter("@p_i_projname", projinfo.Projname),
                                                              new SqlParameter("@p_i_projaddress", projinfo.Projaddress),
                                                              new SqlParameter("@p_i_Casedescribe", projinfo.Casedescribe),
                                                              new SqlParameter("@p_i_Party", projinfo.Party),                                                                                                                         
                                                              new SqlParameter("@p_i_Corporation_age", projinfo.Corporation_age),
                                                              new SqlParameter("@p_i_Corporation_job", projinfo.Corporation_job),
                                                              new SqlParameter("@p_i_Corporation_name", projinfo.Corporation_name),
                                                              new SqlParameter("@p_i_Corporation_sxy", projinfo.Corporation_sxy),
                                                              new SqlParameter("@p_i_Punishmentcode", projinfo.Punishmentcode),
                                                              new SqlParameter("@p_i_Punishmenttime", projinfo.Punishmenttime),
                                                              new SqlParameter("@p_i_Punishmentname", projinfo.Punishmentname),
                                                              new SqlParameter("@p_i_Punishmentzxqk", projinfo.Punishmentzxqk),
                                                              new SqlParameter("@p_i_Punishmentstatus", projinfo.Punishmentstatus),
                                                              new SqlParameter("@p_i_Domethod", projinfo.Domethod),
                                                              new SqlParameter("@p_i_Doend", projinfo.Doend),
                                                              new SqlParameter("@p_i_Dodate", projinfo.Dodate),
                                                              new SqlParameter("@p_i_Memo", projinfo.Memo),
                                                              new SqlParameter("@p_i_cu_x", projinfo.addressx),
                                                              new SqlParameter("@p_i_cu_y", projinfo.addressy),
                                                              new SqlParameter("@p_i_Summary", projinfo.Summary),
                                                              new SqlParameter("@p_o_errmsg", SqlDbType.VarChar,200),
                                                              new SqlParameter("@p_o_err", SqlDbType.Int)};

            arrSP[35].Direction = ParameterDirection.Output;
            arrSP[36].Direction = ParameterDirection.Output;
            try
            {

                using (DepartManage dl = new DepartManage())
                {
                    int i = dl.ExecNonQueryProc("pr_b_Insertprojectdetail_zhifa", ref arrSP);
                    if (arrSP[36].Value.ToString() == "")
                        err = 1;
                    else
                        err = Convert.ToInt32(arrSP[36].Value);
                    Errmsg = Convert.ToString(arrSP[35].Value);
                    return i;        
                }
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }
        #endregion

        #region GetHistroyList����ȡ��ʷ��¼
        /// <summary>
        /// ��ȡ��ʷ��¼
        /// </summary>
        /// <param name="projinfo">������Ϣ����</param>
        /// <param name="departcode">���ű��</param>
        /// <param name="p_i_field">�����ֶ�</param>
        /// <param name="p_i_order">����ʽ</param>
        /// <param name="p_i_pagesize">ҳ���С</param>
        /// <param name="p_i_CurrentPage">��ǰҳ��</param>
        /// <param name="p_o_rowcount">�ܼ�¼����</param>
        /// <param name="p_o_pagecount">��ҳ����</param>
        /// <returns></returns>
        public DataSet GetHistroyList(projinfo projinfo,DateTime strdate,DateTime enddate, string p_i_field, string p_i_order, int p_i_pagesize,
                                         int p_i_CurrentPage, out int p_o_rowcount, out int p_o_pagecount, ref string strErr)
        {
            p_o_rowcount = 0;
            p_o_pagecount = 0;
            SqlParameter[] arrSP = new SqlParameter[] {                                                         
                                                              new SqlParameter("p_i_usercode", projinfo.Usercode),
                                                              new SqlParameter("p_i_Memo", projinfo.Memo),
                                                              new SqlParameter("p_i_projaddress", projinfo.Projaddress),
                                                              new SqlParameter("p_i_strdate",strdate),
                                                              new SqlParameter("p_i_enddate",enddate),
                                                              new SqlParameter("p_i_field", p_i_field),
                                                              new SqlParameter("p_i_order", p_i_order),
                                                              new SqlParameter("p_i_pagesize", p_i_pagesize),
                                                              new SqlParameter("p_i_CurrentPage", p_i_CurrentPage),
                                                              new SqlParameter("p_o_rowcount", SqlDbType.Int),
                                                              new SqlParameter("p_o_pagecount", SqlDbType.Int)
            };

            arrSP[9].Direction = ParameterDirection.InputOutput;
            arrSP[10].Direction = ParameterDirection.Output;
            

            try
            {
                using (DepartManage dl = new DepartManage())
                {                     
                    DataSet ds = dl.ExecProc("pr_b_GetPersonDealedList", ref arrSP);
                    p_o_rowcount = Convert.ToInt32(arrSP[9].Value);
                    p_o_pagecount = Convert.ToInt32(arrSP[10].Value);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetBusiStatus����ȡ��ѡģ���Ȩ�ް�ť���
        /// <summary>
        /// ��ȡ��ѡģ���Ȩ�ް�ť���
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetBusiStatus(string buttoncode)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("p_i_buttoncode", buttoncode)};
            
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_b_GetBusiStatus_zhifa", ref arrSP);
                }
            }
            catch (Exception ex)
            {                
                return null;
            }
        }
        #endregion

        #region GetPageTextList����ȡ��ѡģ����ϸ������ʾ
        /// <summary>
        /// ��ȡ��ѡģ����ϸ������ʾ
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetPageTextList(string buttoncode)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("p_i_buttoncode", buttoncode)};
            
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecProc("pr_b_buttoncode_getById", ref arrSP);
                }
            }
            catch (Exception ex)
            {                
                return null;
            }
        }
        #endregion

        #region GetButtoncProperty����ȡ��ѡģ����ϸ������ʾ
        /// <summary>
        /// ��ȡ��ѡģ����ϸ������ʾ
        /// </summary>
        /// <param name="projcode">�������</param>
        /// <returns></returns>
        public DataSet GetButtoncProperty(string modelcode)
        {
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    return dl.ExecDatasetOracle(@"select b.property
                      from  s_flownodeinfo b where modelcode = '" + modelcode + "'");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetQueryList����ȡ��ѡ�����Ŵ�����İ���
        /// <summary>
        /// ��ȡ��ѡ�����Ŵ�����İ���
        /// </summary>
        /// <param name="projinfo">������Ϣ����</param>
        /// <param name="departcode">���ű��</param>
        /// <param name="p_i_field">�����ֶ�</param>
        /// <param name="p_i_order">����ʽ</param>
        /// <param name="p_i_pagesize">ҳ���С</param>
        /// <param name="p_i_CurrentPage">��ǰҳ��</param>
        /// <param name="p_o_rowcount">�ܼ�¼����</param>
        /// <param name="p_o_pagecount">��ҳ����</param>
        /// <returns></returns>
        public DataSet GetQueryList(projinfo projinfo, int departcode, string p_i_field, string p_i_order, int p_i_pagesize,
                                         int p_i_CurrentPage, out int p_o_rowcount, out int p_o_pagecount, ref string strErr)
        {
            p_o_rowcount = 0;
            p_o_pagecount = 0;
            SqlParameter[] arrSP = new SqlParameter[] { 
                                                              new SqlParameter("p_i_ftype", projinfo.Functiontype),
                                                              new SqlParameter("p_i_departid", departcode),                               
                                                              new SqlParameter("p_i_projname", projinfo.Projname),
                                                              new SqlParameter("p_i_projaddress", projinfo.Projaddress),
                                                              new SqlParameter("p_i_statrtdate", projinfo.Startdate),
                                                              new SqlParameter("p_i_enddate", projinfo.Enddate),
                                                              new SqlParameter("p_i_field", p_i_field),
                                                              new SqlParameter("p_i_order", p_i_order),
                                                              new SqlParameter("p_i_pagesize", p_i_pagesize),
                                                              new SqlParameter("p_i_CurrentPage", p_i_CurrentPage),
                                                              new SqlParameter("p_o_rowcount", SqlDbType.Int),
                                                              new SqlParameter("p_o_pagecount", SqlDbType.Int),
                                                              new SqlParameter("p_i_nodeid", projinfo.Nodeid)};
            arrSP[10].Direction = ParameterDirection.InputOutput;
            arrSP[11].Direction = ParameterDirection.Output;
            try
            {
                using (DepartManage dl = new DepartManage())
                {
                    DataSet ds = dl.ExecProc("pr_b_GetQueryList", ref arrSP);
                    p_o_rowcount = Convert.ToInt32(arrSP[10].Value);
                    p_o_pagecount = Convert.ToInt32(arrSP[11].Value);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
