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

        #region GetFlownodeTree：获取所选定流程的节点树
        /// <summary>
        /// 获取所选定流程的节点树
        /// </summary>
        /// <param name="flowinfoid">流程ID</param>
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

        #region GetFlowNodePower：获取所选定角色的可控制模块和具体操作动作
        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作
        /// </summary>
        /// <param name="roleID">角色编号</param>
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

        #region GetDealWorkList：获取所选定部门及阶段的可处理业务数据
        /// <summary>
        /// 获取所选定部门及阶段的可处理业务数据
        /// </summary>
        /// <param name="projinfo">案件信息对象</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="p_i_field">排序字段</param>
        /// <param name="p_i_order">排序方式</param>
        /// <param name="p_i_pagesize">页面大小</param>
        /// <param name="p_i_CurrentPage">当前页码</param>
        /// <param name="p_o_rowcount">总记录行数</param>
        /// <param name="p_o_pagecount">总页码数</param>
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

        #region GetProjectTraceList：获取所选定案件的处理流程
        /// <summary>
        /// 获取所选定案件的处理流程
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region GetProjectInfo：获取所选定案件的详细信息
        /// <summary>
        /// 获取所选定案件的详细信息
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region GetProjectInfo：获取所选定案件的详细信息
        /// <summary>
        /// 获取所选定案件的详细信息
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region  GetDiction： 取习惯用语内容

        /// <summary>
        /// 取习惯用语内容，比如： 办公用语
        /// </summary>
        /// <param name="DictionCode">字典代码</param>
        /// <param name="stepid">步骤代码</param>
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

        #region UpdateProject：修改更新案件信息 
        /// <summary>
        /// 修改更新案件信息
        /// </summary>
        /// <param name="projinfo">案件信息对象</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="p_i_field">排序字段</param>
        /// <param name="p_i_order">排序方式</param>
        /// <param name="p_i_pagesize">页面大小</param>
        /// <param name="p_i_CurrentPage">当前页码</param>
        /// <param name="p_o_rowcount">总记录行数</param>
        /// <param name="p_o_pagecount">总页码数</param>
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

        #region GetHistroyList：获取历史记录
        /// <summary>
        /// 获取历史记录
        /// </summary>
        /// <param name="projinfo">案件信息对象</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="p_i_field">排序字段</param>
        /// <param name="p_i_order">排序方式</param>
        /// <param name="p_i_pagesize">页面大小</param>
        /// <param name="p_i_CurrentPage">当前页码</param>
        /// <param name="p_o_rowcount">总记录行数</param>
        /// <param name="p_o_pagecount">总页码数</param>
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

        #region GetBusiStatus：获取所选模块的权限按钮编号
        /// <summary>
        /// 获取所选模块的权限按钮编号
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region GetPageTextList：获取所选模块详细界面显示
        /// <summary>
        /// 获取所选模块详细界面显示
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region GetButtoncProperty：获取所选模块详细界面显示
        /// <summary>
        /// 获取所选模块详细界面显示
        /// </summary>
        /// <param name="projcode">案件编号</param>
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

        #region GetQueryList：获取所选定部门处理过的案件
        /// <summary>
        /// 获取所选定部门处理过的案件
        /// </summary>
        /// <param name="projinfo">案件信息对象</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="p_i_field">排序字段</param>
        /// <param name="p_i_order">排序方式</param>
        /// <param name="p_i_pagesize">页面大小</param>
        /// <param name="p_i_CurrentPage">当前页码</param>
        /// <param name="p_o_rowcount">总记录行数</param>
        /// <param name="p_o_pagecount">总页码数</param>
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
