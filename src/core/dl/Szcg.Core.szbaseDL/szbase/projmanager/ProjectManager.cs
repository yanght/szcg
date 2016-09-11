using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;

namespace bacgDL.szbase.projmanager
{
    /// <summary>
    /// DL:案卷管理
    /// </summary>
    public class ProjectManager : Teamax.Common.CommonDatabase
    {
        #region SelectProjectDetaills：查询案卷详细信息
        /// <summary>
        /// SelectProjectDetaills：查询案卷详细信息
        /// </summary>
        /// <param name="projcode">案卷号</param>
        /// <returns>案卷详细信息</returns>
        public DataSet SelectProjectDetaills(string projcode)
        {
            // string projcode, string strYear, string IsEnd
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",projcode)
            };

            return this.ExecuteDataset("pr_b_s_GetProjectDetail", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region UpdateProjectDetails：修改案卷详细信息
        /// <summary>
        /// UpdateProjectDetails：修改案卷详细信息
        /// </summary>
        /// <param name="pri">案卷信息</param>
        /// <param name="psi">案卷来源</param>
        public void UpdateProjectDetails(ProjectInfo prj, ProjectSourceInfo psi,string collcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
               new SqlParameter("@ProjCode",prj.projcode),
               new SqlParameter("@IsManual",prj.IsManual),
               new SqlParameter("@probsource",prj.probsource),
               new SqlParameter("@typecode",prj.typecode),
               new SqlParameter("@bigClass",prj.bigClass),
               new SqlParameter("@smallclass",prj.smallclass),
               new SqlParameter("@area",prj.area),
               new SqlParameter("@street",prj.street),
               new SqlParameter("@square",prj.square),
               new SqlParameter("@fid",prj.Fid),
               new SqlParameter("@gridcode",prj.gridcode),
               new SqlParameter("@WorkGrid",prj.WorkGrid),
               new SqlParameter("@address",prj.address),
               new SqlParameter("@isgreat",prj.isgreat),
               new SqlParameter("@isdel",prj.IsDel),
               new SqlParameter("@startdate",prj.startdate),
               new SqlParameter("@nodeid",prj.NodeId),
               new SqlParameter("@probdesc",prj.probdesc),
               new SqlParameter("@IsFeedBack",prj.isFeadback),
               new SqlParameter("@ProcessType",prj.ProcessType),
               new SqlParameter("@Telephonist",prj.Telephonist),
               new SqlParameter("@ispress",prj.ispress),
               new SqlParameter("@TelephonistCode",prj.TelephonistCode),
               new SqlParameter("@IsNeedFeedBack",prj.IsNeedFeedBack),
               new SqlParameter("@TargetDepartCode",prj.TargetDepartCode),
               new SqlParameter("@ioflag",prj.PdaIoFlag.Substring(0,1)),
               new SqlParameter("@state",prj.PdaIoFlag.Substring(1,1)),
               new SqlParameter("@result",prj.Pdamsg),
               new SqlParameter("@name",psi.name),
               new SqlParameter("@tel",psi.tel),
               new SqlParameter("@collcode",collcode)
            };

            this.ExecuteNonQuery("pr_b_s_UpdateProjectDetail", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region DeleteProjtrace：删除案卷流程信息
        /// <summary>
        /// DeleteProjtrace：删除案卷流程信息
        /// </summary>
        /// <param name="projtrace">案卷流程编号</param>
        public void DeleteProjtrace(string projtrace)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projtrace",projtrace)
            };

            this.ExecuteNonQuery("pr_b_s_DeleteProjectTrace", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region DeletePdatrace：删除案卷核查信息
        /// <summary>
        /// DeletePdatrace：删除案卷核查信息
        /// </summary>
        /// <param name="id">案卷核查信息编号</param>
        public void DeletePdatrace(string id)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@id",id)
            };

            this.ExecuteNonQuery("pr_b_s_DeletePdamsgTrace", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetProjTrace:获取案件的流程
        /// <summary>
        ///  GetProjTrace:获取案件的流程
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <returns></returns>
        public DataSet GetProjTrace(string projcode, string Year, string IsEnd)
        { 
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@projcode",projcode)
                    };
            return this.ExecuteDataset("pr_b_s_GetProjTrace", CommandType.StoredProcedure, arrSP);
        }
        #endregion
    }
}
