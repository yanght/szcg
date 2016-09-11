using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;

namespace bacgBL.szbase.projmanager
{
    /// <summary>
    /// BL:案卷管理
    /// </summary>
    public class ProjectManager
    {
        #region SelectProjectDetaills：查询案卷详细信息
        /// <summary>
        /// SelectProjectDetaills：查询案卷详细信息
        /// </summary>
        /// <param name="projcode">案卷号</param>
        /// <returns>案卷详细信息</returns>
        public static DataSet SelectProjectDetaills(string projcode, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.szbase.projmanager.ProjectManager dl = new bacgDL.szbase.projmanager.ProjectManager())
                {
                    return dl.SelectProjectDetaills(projcode);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region GetProjTrace：获取案件的流程
        /// <summary>
        /// SelectProjectDetaills：获取案件的流程
        /// </summary>
        /// <param name="projcode">案卷号</param>
        /// <returns>案卷详细信息</returns>
        public static DataSet GetProjTrace(string projcode, string Year, string IsEnd, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.szbase.projmanager.ProjectManager dl = new bacgDL.szbase.projmanager.ProjectManager())
                {
                    return dl.GetProjTrace(projcode, Year,IsEnd);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region UpdateProjectDetails：修改案卷详细信息
        /// <summary>
        /// UpdateProjectDetails：修改案卷详细信息
        /// </summary>
        /// <param name="pri">案卷信息</param>
        /// <param name="psi">案卷来源</param>
        public static void UpdateProjectDetails(ProjectInfo prj, ProjectSourceInfo psi,string collcode, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.szbase.projmanager.ProjectManager dl = new bacgDL.szbase.projmanager.ProjectManager())
            {
                try
                {
                    dl.BeginTrans();
                    dl.UpdateProjectDetails(prj, psi,collcode);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region DeleteProjtrace：删除案卷流程信息
        /// <summary>
        /// DeleteProjtrace：删除案卷流程信息
        /// </summary>
        /// <param name="projtrace">案卷流程编号</param>
        public static void DeleteProjtrace(string projtrace, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.szbase.projmanager.ProjectManager dl = new bacgDL.szbase.projmanager.ProjectManager())
            {
                try
                {
                    dl.BeginTrans();
                    dl.DeleteProjtrace(projtrace);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion

        #region DeletePdatrace：删除案卷核查信息
        /// <summary>
        /// DeletePdatrace：删除案卷核查信息
        /// </summary>
        /// <param name="id">案卷核查信息编号</param>
        public static void DeletePdatrace(string id, out string ErrMsg)
        {
            ErrMsg = "";
            using (bacgDL.szbase.projmanager.ProjectManager dl = new bacgDL.szbase.projmanager.ProjectManager())
            {
                try
                {
                    dl.BeginTrans();
                    dl.DeletePdatrace(id);
                    dl.Commit();
                }
                catch (Exception e)
                {
                    dl.Rollback();
                    ErrMsg = e.Message;
                }
            }
        }
        #endregion
    }
}
