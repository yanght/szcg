using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;

namespace bacgDL.szbase.projmanager
{
    /// <summary>
    /// DL:�������
    /// </summary>
    public class ProjectManager : Teamax.Common.CommonDatabase
    {
        #region SelectProjectDetaills����ѯ������ϸ��Ϣ
        /// <summary>
        /// SelectProjectDetaills����ѯ������ϸ��Ϣ
        /// </summary>
        /// <param name="projcode">�����</param>
        /// <returns>������ϸ��Ϣ</returns>
        public DataSet SelectProjectDetaills(string projcode)
        {
            // string projcode, string strYear, string IsEnd
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",projcode)
            };

            return this.ExecuteDataset("pr_b_s_GetProjectDetail", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region UpdateProjectDetails���޸İ�����ϸ��Ϣ
        /// <summary>
        /// UpdateProjectDetails���޸İ�����ϸ��Ϣ
        /// </summary>
        /// <param name="pri">������Ϣ</param>
        /// <param name="psi">������Դ</param>
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

        #region DeleteProjtrace��ɾ������������Ϣ
        /// <summary>
        /// DeleteProjtrace��ɾ������������Ϣ
        /// </summary>
        /// <param name="projtrace">�������̱��</param>
        public void DeleteProjtrace(string projtrace)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projtrace",projtrace)
            };

            this.ExecuteNonQuery("pr_b_s_DeleteProjectTrace", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region DeletePdatrace��ɾ������˲���Ϣ
        /// <summary>
        /// DeletePdatrace��ɾ������˲���Ϣ
        /// </summary>
        /// <param name="id">����˲���Ϣ���</param>
        public void DeletePdatrace(string id)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@id",id)
            };

            this.ExecuteNonQuery("pr_b_s_DeletePdamsgTrace", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetProjTrace:��ȡ����������
        /// <summary>
        ///  GetProjTrace:��ȡ����������
        /// </summary>
        /// <param name="projcode">�������</param>
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
