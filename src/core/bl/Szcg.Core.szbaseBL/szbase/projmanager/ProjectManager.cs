using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;

namespace bacgBL.szbase.projmanager
{
    /// <summary>
    /// BL:�������
    /// </summary>
    public class ProjectManager
    {
        #region SelectProjectDetaills����ѯ������ϸ��Ϣ
        /// <summary>
        /// SelectProjectDetaills����ѯ������ϸ��Ϣ
        /// </summary>
        /// <param name="projcode">�����</param>
        /// <returns>������ϸ��Ϣ</returns>
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

        #region GetProjTrace����ȡ����������
        /// <summary>
        /// SelectProjectDetaills����ȡ����������
        /// </summary>
        /// <param name="projcode">�����</param>
        /// <returns>������ϸ��Ϣ</returns>
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

        #region UpdateProjectDetails���޸İ�����ϸ��Ϣ
        /// <summary>
        /// UpdateProjectDetails���޸İ�����ϸ��Ϣ
        /// </summary>
        /// <param name="pri">������Ϣ</param>
        /// <param name="psi">������Դ</param>
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

        #region DeleteProjtrace��ɾ������������Ϣ
        /// <summary>
        /// DeleteProjtrace��ɾ������������Ϣ
        /// </summary>
        /// <param name="projtrace">�������̱��</param>
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

        #region DeletePdatrace��ɾ������˲���Ϣ
        /// <summary>
        /// DeletePdatrace��ɾ������˲���Ϣ
        /// </summary>
        /// <param name="id">����˲���Ϣ���</param>
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
