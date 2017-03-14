/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：案件业务受理-数据层访问类。

 * 结构组成：

 * 作    者：yannis
 * 创建日期：2007-05-25
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Configuration;

namespace bacgDL.business
{

    public class Project : Teamax.Common.CommonDatabase
    {
        #region GetYJJProjectList：获取移交案件列表

        /// <summary>
        /// 获取移交案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetYJProjectList(ProjectInfo prj, string times1, string times2, string departId, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserCode",prj.TelephonistCode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field),
                                new SqlParameter("@DepartId",departId)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetYJProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }
        #endregion

        #region GetCDProjList：获取存档案件的案件列表

        /// <summary>
        /// 获取存档案件的案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetCDProjList(ProjectInfo prj, string times1, string times2, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@UserCode",prj.TelephonistCode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetCDProjList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }
        #endregion

        #region GetSPProjList：获取存档案件的案件列表
        /// <summary>
        /// 获取存档案件的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetSPProjList(ProjectInfo prj, string times1, string times2, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@areacode",prj.areacode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetSPProjList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }
        #endregion

        #region GetDealProjectList：获取待办案件列表

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetDealProjectList(ProjectInfo prj, string times1, string times2, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetId",prj.street),                                
                                new SqlParameter("@PdaIoFlag",prj.PdaIoFlag),
                                new SqlParameter("@NodeId",prj.NodeId),
                                new SqlParameter("@areacode",prj.areacode),
                                new SqlParameter("@DepartCode",prj.TargetDepartCode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@typecode",prj.typecode),                                
                                new SqlParameter("@bigclass",prj.bigClass),
                                new SqlParameter("@smallclass",prj.smallclass),
                                new SqlParameter("@partID",prj.partID),
                                new SqlParameter("@address",prj.address),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field),
                                new SqlParameter("@SquareID",prj.square),
                                new SqlParameter("@ButtonId",prj.strButtonId),
                                new SqlParameter("@strUserCode",prj.strUserCode)};

            arrSP[14].Direction = ParameterDirection.Output;
            arrSP[15].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetDealProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[14].Value.ToString();
            page.PageCount = arrSP[15].Value.ToString();

            return ds;
        }
        #endregion

        #region GetDealProjectList：获取待办案件列表可区分回退和反馈

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="drptype">2,回退,3,反馈,0,默认</param>
        /// <returns></returns>
        public DataSet GetDealProjectList(ProjectInfo prj, string times1, string times2, PageInfo page, int drptype)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetId",prj.street),                                
                                new SqlParameter("@PdaIoFlag",prj.PdaIoFlag),
                                new SqlParameter("@NodeId",prj.NodeId),
                                new SqlParameter("@areacode",prj.areacode),
                                new SqlParameter("@DepartCode",prj.TargetDepartCode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@typecode",prj.typecode),                                
                                new SqlParameter("@bigclass",prj.bigClass),
                                new SqlParameter("@smallclass",prj.smallclass),
                                new SqlParameter("@partID",prj.partID),
                                new SqlParameter("@address",prj.address),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field),
                                new SqlParameter("@SquareID",prj.square),
                                new SqlParameter("@ButtonId",prj.strButtonId),
                                new SqlParameter("@strUserCode",prj.strUserCode),
                                new SqlParameter("@drptype",drptype)};

            arrSP[14].Direction = ParameterDirection.Output;
            arrSP[15].Direction = ParameterDirection.Output;

            DataSet ds;
            //modi by yaoch 2012-10-16
            if (drptype == 0)
            {
                ds = this.ExecuteDataset("pr_b_GetDealProjectList_list", CommandType.StoredProcedure, arrSP);
            }
            else
            {
                ds = this.ExecuteDataset("pr_b_GetDealProjectList_zybm", CommandType.StoredProcedure, arrSP);
            }
            page.RowCount = arrSP[14].Value.ToString();
            page.PageCount = arrSP[15].Value.ToString();

            return ds;
        }
        #endregion


        #region GetDealProjectListWZDJ：获取待办违章搭建案件列表可区分回退和反馈
        /// <summary>
        /// 获取待办违章搭建案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <param name="drptype">2,回退,3,反馈,0,默认</param>
        /// <returns></returns>
        public DataSet GetDealProjectListWZDJ(ProjectInfo prj, string times1, string times2, PageInfo page, int drptype)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetId",prj.street),                                
                                new SqlParameter("@PdaIoFlag",prj.PdaIoFlag),
                                new SqlParameter("@NodeId",prj.NodeId),
                                new SqlParameter("@areacode",prj.areacode),
                                new SqlParameter("@DepartCode",prj.TargetDepartCode),
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@typecode",prj.typecode),                                
                                new SqlParameter("@bigclass",prj.bigClass),
                                new SqlParameter("@smallclass",prj.smallclass),
                                new SqlParameter("@partID",prj.partID),
                                new SqlParameter("@address",prj.address),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),                                
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field),
                                new SqlParameter("@SquareID",prj.square),
                                new SqlParameter("@ButtonId",prj.strButtonId),
                                new SqlParameter("@strUserCode",prj.strUserCode),
                                new SqlParameter("@drptype",drptype)};

            arrSP[14].Direction = ParameterDirection.Output;
            arrSP[15].Direction = ParameterDirection.Output;

            DataSet ds;
            //modi by yaoch 2012-10-16
            if (drptype == 0)
            {
                ds = this.ExecuteDataset("pr_b_GetDealProjectList_list_wzdj", CommandType.StoredProcedure, arrSP);
            }
            else
            {
                ds = this.ExecuteDataset("pr_b_GetDealProjectList_zybm", CommandType.StoredProcedure, arrSP);
            }
            page.RowCount = arrSP[14].Value.ToString();
            page.PageCount = arrSP[15].Value.ToString();

            return ds;
        }

        public DataSet GetProjectWzdj(string times1, string times2, string phonenumber, string keyWords)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2) ,
                                new SqlParameter("@phonenumber",phonenumber),
                                new SqlParameter("@keyWords",keyWords)
            };

            string strSQL = @"select b_project.* from b_project
            left join b_other_proj 
            on b_project.projcode=b_other_proj.projcode
            left join b_project_detail
            on b_project.projcode=b_project_detail.projcode
            where startdate>@times1 and startdate<@times2 
            and isdel<>1 and bigclass=22
            and b_other_proj.tel=@phonenumber
            ";

            if (!string.IsNullOrEmpty(keyWords))
            {
                strSQL += " and b_project_detail.probdesc like '%@keyWords%'";
            }
            return this.ExecuteDataset(strSQL, arrSP);

        }

        public DataSet GetProjectDetailWzdj(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
            };

            string strSQL = @"select * from b_project a left join b_other_proj b
                            on a.projcode=b.projcode
                            left join b_project_detail c
                            on a.projcode=c.projcode
                            where a.projcode=@projcode
                            and a.isDel<>1 and bigclass=22
            ";
            return this.ExecuteDataset(strSQL, arrSP);
        }

        public DataSet getProjectTrace(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
            };
            string strSQL = "select CurrentNodeId, _opinion from b_project_trace where projcode=@projcode ";
            return this.ExecuteDataset(strSQL, arrSP);
        }

        public DataSet GetprojectFiles(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
            };
            string strSQL = "select * from b_project_file where projcode=@projcode and filestate=1";
            return this.ExecuteDataset(strSQL, arrSP);
        }
        public DataSet GetLprojectFiles(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
            };
            string strSQL = "select * from b_project_file where projcode=@projcode ";

            strSQL += string.Format(" union select * from [szcg_bak{0}]..b_project_file where projcode=@projcode ", DateTime.Now.Year);


            return this.ExecuteDataset(strSQL, arrSP);
        }

        #endregion


        #region  GetProjDetail：取得案卷的详细信息
        /// <summary>
        /// 取得案卷的详细信息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">上报年份</param>
        /// <param name="IsEnd">是否结案</param>
        /// <param name="strNodeId">节点（阶段）</param>
        /// <returns></returns>
        public DataSet GetProjDetail(string projcode, string strYear, string IsEnd, string strNodeId)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",projcode),
                new SqlParameter("@year", strYear),
                new SqlParameter("@IsEnd",IsEnd),
                new SqlParameter("@NodeId",strNodeId),
            };

            return this.ExecuteDataset("pr_b_GetProjectDetail", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region 根据小类编号 查找图片名称
        public DataSet GetPartImg(string smallclasscode)
        {

            string strSQL = @"select * from dbo.s_smallClass_partImg where partCode= '" + smallclasscode + "' ";

            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region  GetProjectFile：得到案卷图片文件

        /// <summary>
        /// 得到案卷图片文件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <returns></returns>
        public DataSet GetProjectFile(string projcode, string strYear, string IsEnd)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode), 
                new SqlParameter("@year", strYear),
                new SqlParameter("@IsEnd",IsEnd)
            };

            return this.ExecuteDataset("pr_b_GetProjFiles", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  GetProjectSound：得到案卷声音文件

        /// <summary>
        /// 得到案卷声音文件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <returns></returns>
        public DataSet GetProjectSound(string projcode, string strYear, string IsEnd)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode), 
                new SqlParameter("@year", strYear),
                new SqlParameter("@IsEnd",IsEnd)
            };

            return this.ExecuteDataset("pr_b_GetProjSounds", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  GetProjAllFiles：得到案卷图片和声音文件
        /// <summary>
        /// 得到案卷图片和声音文件

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年份</param>
        /// <param name="IsEnd">是否已结案</param>
        /// <returns></returns>
        public DataSet GetProjAllFiles(string projcode, string strYear, string IsEnd)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode), 
                new SqlParameter("@year", strYear),
                new SqlParameter("@IsEnd",IsEnd)
            };

            return this.ExecuteDataset("pr_b_GetProjAllFiles", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  LockProject：并发操作时，锁定案件记录

        /// <summary>
        /// 并发操作时，锁定案件记录
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="NodeId">节点编码</param>
        /// <param name="usercode">用户编码</param>
        /// <param name="Msg">输出信息</param>
        /// <param name="Ret">执行存储过程的返回值</param>
        public void LockProject(string projcode, string UserCode, string NodeId, out string Ret, out string Msg)
        {
            Ret = "";
            Msg = "";

            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode), 
                new SqlParameter("@UserCode",UserCode),
                new SqlParameter("@NodeId", NodeId),
                new SqlParameter("@Msg",SqlDbType.VarChar,100),
                new SqlParameter("@ret",SqlDbType.Int)
            };

            arrSP[3].Direction = ParameterDirection.Output;
            arrSP[4].Direction = ParameterDirection.ReturnValue;

            this.ExecuteNonQuery("pr_b_LockProject", CommandType.StoredProcedure, arrSP);
            Msg = arrSP[3].Value.ToString();
            Ret = arrSP[4].Value.ToString();
        }
        #endregion


        #region  getNodeId：并发操作时，锁定案件记录

        /// <summary>
        /// 并发操作时，锁定案件记录
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="NodeId">节点编码</param>
        /// <param name="usercode">用户编码</param>
        /// <param name="Msg">输出信息</param>
        /// <param name="Ret">执行存储过程的返回值</param>
        public string getNodeId(string projcode)
        {
            string sql = "select * from b_project where projcode='" + projcode + "' and isnull(isdel,0)<>1";
            DataSet ds = this.ExecuteDataset(sql);
            return ds.Tables[0].Rows[0]["NodeId"].ToString();
        }
        #endregion


        #region  UnLockProject：并发操作时，解锁案件

        /// <summary>
        /// 并发操作时，解锁案件
        /// </summary>
        /// <param name="projcode">案件编号</param>
        public void UnLockProject(string projcode)
        {
            string strSQL = string.Format(@"update b_project 
                                            set istransaction = 0,lockusercode=null,locktime=null 
                                            where projcode = {0}", projcode);
            this.ExecuteNonQuery(strSQL);
        }
        #endregion


        #region  UpObjcodect：更新部件表中objcode字段
        /// <summary>
        /// 更新部件表中objcode字段
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="objcode">部件编码</param>
        public void UpObjcodect(string projcode, string objcode, string userid)
        {
            string strSQL = string.Format(@"update m_newcmptlayer 
                                            set objcode= {1}, PASSPERSONID={2},PASSTIME=GETDATE(),UPDATEFLAG=0,[enable]=1
                                                                where projcode = {0}", projcode, objcode, userid);
            this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region  InsertProject：插入案件

        /// <summary>
        /// 插入案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public string InsertProject(ProjectInfo prj, ProjectSourceInfo psi)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
               new SqlParameter("@ProjCode",SqlDbType.Int),
               new SqlParameter("@Telephonist",prj.Telephonist),
               new SqlParameter("@TelephonistCode",prj.TelephonistCode),
               new SqlParameter("@NodeId",prj.NodeId),
               new SqlParameter("@probsource",prj.probsource),
               new SqlParameter("@typecode",prj.typecode),
               new SqlParameter("@bigClass",prj.bigClass),
               new SqlParameter("@smallclass",prj.smallclass),
               new SqlParameter("@detailedclass",prj.detailedclass),
               new SqlParameter("@area",prj.area),
               new SqlParameter("@street",prj.street),
               new SqlParameter("@square",prj.square),
               new SqlParameter("@gridcode",prj.gridcode),
               new SqlParameter("@WorkGrid",prj.WorkGrid),
               new SqlParameter("@address",prj.address),
               //new SqlParameter("@isthough",prj.isthough),
               new SqlParameter("@isgreat",prj.isgreat),
               new SqlParameter("@withDept",prj.withDept),
               new SqlParameter("@groupid",prj.groupid),
               new SqlParameter("@probdesc",prj.probdesc),
               new SqlParameter("@IsNeedFeedBack",prj.IsNeedFeedBack),
               new SqlParameter("@accept",psi.accept),
               new SqlParameter("@name",psi.name),
               new SqlParameter("@tel",psi.tel),
               new SqlParameter("@ip",psi.ip),
               new SqlParameter("@departcode",prj.departcode),
               new SqlParameter("@strCityName",prj.strCityName),
               new SqlParameter("@imagesource1",prj.Images[0]) ,         //相机上报图片
               new SqlParameter("@imagesource2",prj.Images[1]),           //相机上报图片
               new SqlParameter("@imagesource3",prj.Images[2])          //相机上报图片
            };
            arrSP[0].Direction = ParameterDirection.Output;
            ExecuteNonQuery("pr_b_InsertProject", CommandType.StoredProcedure, arrSP);

            return arrSP[0].Value.ToString();
        }
        #endregion

        #region  UpdateProject：修改案件

        /// <summary>
        /// 修改案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public void UpdateProject(ProjectInfo prj, ProjectSourceInfo psi)
        {

            SqlParameter[] arrSP = new SqlParameter[]{
               new SqlParameter("@ProjCode",prj.projcode),
               new SqlParameter("@IsManual",prj.IsManual),
               new SqlParameter("@probsource",prj.probsource),
               new SqlParameter("@typecode",prj.typecode),
               new SqlParameter("@bigClass",prj.bigClass),
               new SqlParameter("@smallclass",prj.smallclass),
               new SqlParameter("@detailedclass",prj.detailedclass),
               new SqlParameter("@area",prj.area),
               new SqlParameter("@street",prj.street),
               new SqlParameter("@square",prj.square),
               new SqlParameter("@gridcode",prj.gridcode),
               new SqlParameter("@WorkGrid",prj.WorkGrid),
               new SqlParameter("@address",prj.address),
               new SqlParameter("@isgreat",prj.isgreat),
               new SqlParameter("@withDept",prj.withDept),
               new SqlParameter("@probdesc",prj.probdesc),
               new SqlParameter("@IsNeedFeedBack",prj.IsNeedFeedBack),
               new SqlParameter("@name",psi.name),
               new SqlParameter("@tel",psi.tel),
               new SqlParameter("@departcode",prj.departcode)
            };

            ExecuteNonQuery("pr_b_UpdateProject", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  UpdateProjectWithOther：其它方式修改案件

        /// <summary>
        /// 其它修改案件
        /// </summary>
        /// <param name="prj">案件信息</param>
        /// <param name="psi">案件来源信息</param>
        /// <returns>输出案件编号</returns>
        public void UpdateProjectWithOther(ProjectInfo prj, ProjectSourceInfo psi, ProjectTraceInfo pti)
        {

            string strButtoncode = GetCurrentButtonCode(prj.NodeId);

            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("usercode",pti.usercode),
                new SqlParameter("projcode",pti.projcode),
                new SqlParameter("NodeId",pti.PreNodeId),
                new SqlParameter("BusiStatus",pti.CurrentBusiStatus),
                new SqlParameter("DepartCode",pti.DepartCode),
                new SqlParameter("roleid",pti.roleid),
                new SqlParameter("opinion",pti._opinion),
                new SqlParameter("returntracetag",pti.returntracetag),
                new SqlParameter("buttoncode",strButtoncode)
            };


            ExecuteNonQuery("pr_b_ChangeProjectFlow", CommandType.StoredProcedure, arrSP);

            if (prj.ImgFtpPath != "")
            {
                //插入b_projcode_file表

                string strSQL = string.Format(@"	insert b_project_file(projcode,filestate,filepath,cudate,memo) 
                                                    values(?,?,?,?,?)", prj.projcode, prj.UpLoadType, prj.ImgFtpPath, DateTime.Now, prj.Pdamsg);
                this.ExecuteNonQuery(strSQL);
            }

            if (prj.SoundFtpPath != "")
            {
                //插入b_projcode_sound表

                string strSQL = string.Format(@"	insert b_project_sound(projcode,soundstate,soundpath,cudate,memo) 
                                                    values(?,?,?,?,?)", prj.projcode, prj.UpLoadType, prj.SoundFtpPath, DateTime.Now, prj.Pdamsg);
                this.ExecuteNonQuery(strSQL);
            }
        }
        #endregion

        #region ChangeProcessType:修改案卷处理时间
        /// <summary>
        /// 修改案卷处理时间
        /// </summary>
        /// <param name="ProcessType">处理时间 例如4个小时（4*60=240）单位分钟</param>
        /// <param name="projcode">案卷号</param>
        public void ChangeProcessType(string ProcessType, string projcode)
        {
            string strSQL = string.Format(@" update b_project 
                                                                set ProcessType={0},
                                                                dodepartcode=null 
                                                                where projcode = {1}", ProcessType, projcode);
            this.ExecuteNonQuery(strSQL);
        }
        #endregion

        public void ChangeDetailedclass(string detailedclass, string projcode, string departTime, string departTimeType, string isgreat, string ProcessType)
        {

            string strSQL = string.Format(@" update b_project 
                                                                set detailedclass='{0}',departTime='{2}',departTimeType='{3}',isgreat='{4}',ProcessType='{5}',dodepartcode=null 
                                                                where projcode = '{1}' and typecode in (0,1);
                        update b_project 
                                                                set detailedclass='{0}',isgreat={4},dodepartcode=null 
                                                                where projcode = '{1}' and typecode=2;
                        ", detailedclass, projcode, departTime, departTimeType, isgreat, ProcessType);
            this.ExecuteNonQuery(strSQL);

        }
        public void ChangeTraceTime(string projcode, string departtime, string departtimetype)
        {
            DateTime dt = new DateTime();
            string Ret = "";
            dt = DateTime.Now;
            try
            {
                string sql = string.Format(@"select [dbo].fn_GetTreatTime8('" + dt +
                    "'," + projcode +
                    ",8," + departtime + "," + departtimetype + ") ");
                #region Todo yannis 计算时间时报错，死循环，暂时注释
                 Ret = this.ExecuteScalar(sql).ToString();
                //Ret = null;
                #endregion

                if (string.IsNullOrEmpty(Ret) == false)
                {
                    Ret = Ret.Split(',')[3];
                    string Upsql = "update b_project set tracetime='" + Convert.ToDateTime(Ret).ToString("yyyy-MM-dd HH:mm:ss") + "' where projcode='" + projcode + "'";
                    this.ExecuteNonQuery(Upsql);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ChangeTraceTime1(string projcode, string departtime, string departtimetype,DateTime time)
        {
            DateTime dt = new DateTime();
            string Ret = "";
            dt = time;
            try
            {
                string sql = string.Format(@"select [dbo].fn_GetTreatTime8('" + dt +
                    "'," + projcode +
                    ",8," + departtime + "," + departtimetype + ") ");
                #region Todo yannis 计算时间时报错，死循环，暂时注释
                Ret = this.ExecuteScalar(sql).ToString();
                //Ret = null;
                #endregion

                if (string.IsNullOrEmpty(Ret) == false)
                {
                    Ret = Ret.Split(',')[3];
                    string Upsql = "update b_project set tracetime='" + Convert.ToDateTime(Ret).ToString("yyyy-MM-dd HH:mm:ss") + "' where projcode='" + projcode + "'";
                    this.ExecuteNonQuery(Upsql);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public DataTable  Test()
        {
            string sql = @"
select top 20 a.projcode,b.cu_date,smallclass,tracetime from b_project a
left join b_project_trace b on a.projcode=b.projcode
 where a.projcode in (select projcode from b_project_trace where actionname='立案处理')
and  nodeid=8 and tracetime is NULL
union 
select top 20 a.projcode,b.cu_date,smallclass,tracetime from [szcg_bak2016]..b_project a
left join [szcg_bak2016]..b_project_trace b on a.projcode=b.projcode
 where a.projcode in (select projcode from [szcg_bak2016]..b_project_trace where actionname='立案处理')
and  nodeid=12  and tracetime is NULL";
          return  this.ExecuteDataset(sql).Tables[0];


        }

        #region  ChangeProjectNode：更改案件流程节点

        /// <summary>
        /// 案件流转时，更改案件的节点状态

        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="projcode">案件编号</param>
        /// <param name="NodeId">节点编号</param>
        /// <param name="BusiStatus">业务动作</param>
        /// <param name="DepartCode">部门编码</param>
        /// <param name="opinion">办理意见</param>
        /// <param name="returntracetag">是否回退</param>
        /// <param name="TargetDepartCode">派遣时的应处理部门</param>
        /// <param name="DoDepartCode">结果反馈的专业部门</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public void ChangeProjectFlowNode(ProjectTraceInfo pt)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@usercode",pt.usercode),
                new SqlParameter("@projcode",pt.projcode),
                new SqlParameter("@NodeId",pt.PreNodeId),
                new SqlParameter("@BusiStatus",pt.CurrentBusiStatus),
                new SqlParameter("@DepartCode",pt.DepartCode),
                new SqlParameter("@roleid",pt.roleid),
                new SqlParameter("@opinion",pt._opinion),
                new SqlParameter("@returntracetag",pt.returntracetag),
                new SqlParameter("@buttoncode",pt.buttoncode),
                new SqlParameter("@targetdepart",pt.targetdepart)

            };


            ExecuteNonQuery("pr_b_ChangeProjectFlow", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  ChangeChildDepartCode：责任子部门回退


        public void ChangeChildDepartCode(string projcode)
        {

            //modi by yaoch 2012-10-22,标识回退为0
            string sqlstr = "update b_project set IsReturned=0,ChildDepartCode=null,locktime=null,lockusercode=null where projcode='" + projcode + "'";
            int i = ExecuteNonQuery(sqlstr);



        }
        #endregion

        //add by yaoch 2012-10-22
        #region  ChangeChildReturnStatus：责任子部门反馈后清除回退标记
        public void ChangeChildReturnStatus(string projcode)
        {
            string sqlstr = "update b_project set IsReturned=null where projcode='" + projcode + "'";
            int i = ExecuteNonQuery(sqlstr);
        }
        #endregion

        #region  InsertProjTrace：插入案件流程

        /// <summary>
        /// 插入案件流程
        /// </summary>
        /// <param name="pt">案件流程信息</param>
        public void InsertProjTrace(ProjectTraceInfo pt)
        {
            //TODO 修改改代码，不需要传入actionname、CurrentNodeId
            string strSQL = string.Format(@" 

                    DECLARE @buttonid nvarchar(20)
                    select @buttonid = buttoncode from b_project_trace where projcode=@projcode order by id asc 
                    insert into dbo.b_project_trace(
                        projcode,actionname,cu_date,
                        PreNodeId,CurrentNodeId,CurrentBusiStatus,
                        usercode,DepartCode,
                        _opinion,returntracetag,buttoncode) 
                    values(@projcode,@actionname,GetDate(),
                        @PreNodeId,@CurrentNodeId,@CurrentBusiStatus,
                        @usercode,@DepartCode,
                        @_opinion,@returntracetag,@buttonid)");

            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",pt.projcode),
                new SqlParameter("@actionname",pt.actionname),
                new SqlParameter("@PreNodeId",pt.PreNodeId),
                new SqlParameter("@CurrentNodeId",pt.CurrentNodeId),
                new SqlParameter("@CurrentBusiStatus",pt.CurrentBusiStatus),
                new SqlParameter("@usercode",pt.usercode),
                new SqlParameter("@DepartCode",pt.DepartCode),
                new SqlParameter("@_opinion",pt._opinion),
                new SqlParameter("@returntracetag",pt.returntracetag)
                
            };

            this.ExecuteNonQuery(strSQL, arrSP);
        }
        #endregion


        #region  GetLastOpinion：取案件某节点下面的最后一次意见

        /// <summary>
        /// 取案件某节点下面的最后一次意见

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="NodeId">节点</param>
        /// <returns></returns>
        public object GetLastOpinion(string projcode, string NodeId)
        {
            string strSQL = string.Format(@"select top 1 _opinion 
                                            from b_project_trace with (nolock) 
                                            where projcode = {0} 
                                                and CurrentNodeId = '{1}'
                                                and isnull(returntracetag,0)=0
                                            order by id desc", projcode, NodeId);
            return this.ExecuteScalar(strSQL);
        }
        #endregion
        #region GetlTypeList:获取小类事部件列表
        /// <summary>
        /// 获取小类事部件处理类型列表
        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="smallcode">小类事部件编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetTypeList(string typeCode, string smallcode)
        {
            string strSQL = "";
            if (typeCode == "0" || typeCode == "False")
            {
                strSQL = string.Format(@"
                                             select  * 
                                            from s_smallclass_part
                                            where smallcallcode='{0}'  ", smallcode);
            }
            else if (typeCode == "1" || typeCode == "True")
            {
                strSQL = string.Format(@"
                                         select  *
                                            from s_smallclass_event
                                            where smallcallcode='{0}' ", smallcode);
            }
            else if (typeCode == "2")
            {
                strSQL = string.Format(@"
                                         select  *
                                            from s_smallclass_zhzl
                                            where smallcallcode='{0}' ", smallcode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        //        #region GetlTypeList:获取小类事部件列表
        //        /// <summary>
        //        /// 获取小类事部件处理类型列表
        //        /// </summary>
        //        /// <param name="typeCode">标识是事件还是部件</param>
        //        /// <param name="smallcode">小类事部件编码</param>
        //        /// <param name="ErrMsg">输出错误信息</param>
        //        /// <returns></returns>
        //        public DataSet GetTypeList(string typeCode, string smallcode)
        //        {
        //            string strSQL = "";
        //            if (typeCode == "0" || typeCode == "False")
        //            {
        //                strSQL = string.Format(@"
        //                                             select  isnull(t1time,0) as t1time,t1name,
        //                                                        isnull(t2time,0) as t2time,t2name,
        //                                                        isnull(t3time,0) as t3time,t3name,
        //                                                        isnull(t4time,0) as t4time,t4name,
        //                                                        isnull(t5time,0) as t5time,t5name, 
        //                                                        isnull(t6time,0) as t6time,t6name 
        //                                            from s_smallclass_part
        //                                            where smallcallcode='{0}'  ", smallcode);
        //            }
        //            else if (typeCode == "1" || typeCode == "True")
        //            {
        //                strSQL = string.Format(@"
        //                                         select  isnull(t1time,0) as t1time,t1name,
        //                                                        isnull(t2time,0) as t2time,t2name,
        //                                                        isnull(t3time,0) as t3time,t3name,
        //                                                        isnull(t4time,0) as t4time,t4name,
        //                                                        isnull(t5time,0) as t5time,t5name, 
        //                                                        isnull(t6time,0) as t6time,t6name 
        //                                            from s_smallclass_event
        //                                            where smallcallcode='{0}' ", smallcode);
        //            }

        //            return this.ExecuteDataset(strSQL);
        //        }
        //        #endregion

        #region  GetOpinionInfo：取案件某节点下面的最后一次意见，包括批转人以及所在部门

        /// <summary>
        /// 取案件某节点下面的最后一次意见，包括批转人以及所在部门

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="NodeId">节点</param>
        /// <returns></returns>
        public DataSet GetOpinionInfo(string projcode, string NodeId)
        {

            string strSQL = string.Format(@"select top 1 isnull(A._opinion,'') as _opinion,
                                                    isnull(B.userName,'') as userName,
                                                    isnull(C.departname,'') as departname,
                                                    isnull(C.SJ_RoleCode,'') as SJ_RoleCode
                                            from b_project_trace A 
                                            left join dbo.p_user B
                                            on A.usercode = B.usercode
                                            left join p_depart C
                                            on	A.departcode = C.departcode
                                            where  A.projcode = {0} 
	                                            and A.CurrentNodeId = '{1}'
                                                and isnull(A.returntracetag,0)=0
                                            order by A.id desc", projcode, NodeId);

            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetDeleteProjectList：获取删除的案件列表
        /// <summary>
        /// 获取删除的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetDeleteProjectList(string streetcode, ProjectInfo prj, string username, string times1, string times2, string sign, PageInfo page, int deltime)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",prj.projcode), // 0
                                new SqlParameter("@username",username), // 1
                                new SqlParameter("@times1",times1), // 2
                                new SqlParameter("@times2",times2), // 3
                                new SqlParameter("@sign",sign), // 4
                                new SqlParameter("@CurrentPage",page.CurrentPage), // 5
                                new SqlParameter("@RowCount",SqlDbType.Int), // 6
                                new SqlParameter("@PageCount",SqlDbType.Int), // 7
                                new SqlParameter("@PageSize",page.PageSize), // 8
                                new SqlParameter("@Order",page.Order), // 9
                                new SqlParameter("@Field",page.Field), // 10
                                new SqlParameter("@StreetCode",streetcode),
                                new SqlParameter("@deltime",deltime)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetDeleteProjectList", CommandType.StoredProcedure, arrSP);
            //page.RowCount = arrSP[5].Value.ToString();
            //page.PageCount = arrSP[6].Value.ToString();
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
        }
        #endregion

        #region  DeleteProject： 根据案件编号，删除案件信息

        /// <summary>
        /// 根据案件编号，删除案件信息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="deleteSign">标志位</param>
        public void DeleteProject(string projcode, string deleteSign)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode),
                new SqlParameter("@deleteSign",deleteSign)
            };
            this.ExecuteNonQuery("pr_b_DeleteProject", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        //20080617添加
        #region  ReturnProject： 根据案件编号，从bacg_timeout数据库里还原删除案件信息
        /// <summary>
        /// 根据案件编号，删除案件信息

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        public void ReturnProject(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode)
                };
            this.ExecuteNonQuery("pr_b_ReturnProject_timeout", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetGDProjectList：获取归档的案件列表
        /// <summary>
        /// 获取归档的案件列表

        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetGDProjectList(ProjectInfo prj, string times1, string times2, PageInfo page)
        {
            int startYear;
            int endYear;
            int curentYear = System.DateTime.Now.Date.Year;
            System.DateTime startDateTime;
            System.DateTime endDateTime;
            if (times1 != "")
            {
                startDateTime = System.DateTime.Parse(times1);
                if (startDateTime.Year < 2007)
                    startYear = 2007;
                else
                    startYear = startDateTime.Year;
            }
            else
            {
                startYear = curentYear;
            }
            if (times2 != "")
            {
                endDateTime = System.DateTime.Parse(times2);
                if (endDateTime.Year > curentYear)
                    endYear = curentYear;
                else
                    endYear = endDateTime.Year;
            }
            else
            {
                endYear = curentYear;
            }
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetId",prj.street),//modi by yaoch 2012-10-24
                                new SqlParameter("@projcode",prj.projcode),
                                new SqlParameter("@StartYear",startYear),
                                new SqlParameter("@EndYear",endYear),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetGDProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[7].Value.ToString();
            page.PageCount = arrSP[8].Value.ToString();

            return ds;
        }
        #endregion

        #region GetSJPCProjectList：获取普查的案件列表
        /// <summary>
        /// 获取普查的案件列表
        /// </summary>
        /// <param name="prj">案件结构体</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetSJPCProjectList(string smallcode, string type, ProjectInfo prj, string username, string times1, string times2, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",prj.projcode), // 0
                                new SqlParameter("@username",username), // 1
                                new SqlParameter("@times1",times1), // 2
                                new SqlParameter("@times2",times2), // 3
                                new SqlParameter("@CurrentPage",page.CurrentPage), // 4
                                new SqlParameter("@RowCount",SqlDbType.Int), // 5
                                new SqlParameter("@PageCount",SqlDbType.Int), // 6
                                new SqlParameter("@PageSize",page.PageSize), // 7
                                new SqlParameter("@Order",page.Order), // 8
                                new SqlParameter("@Field",page.Field), // 9
                                new SqlParameter("@enable",type), // 10
                                new SqlParameter("@SmallCode",smallcode)}; // 11

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetSJPCProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }
        #endregion

        #region  DeletesjpcProject： 根据案件编号， 删除普查上报的案卷信息
        /// <summary>
        /// 根据案件编号， 删除普查上报的案卷信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="deleteSign">删除标志位: 0 表示修改标志位enable=2,  1 表示从数据库中物理删除</param>
        public void DeletesjpcProject(string projcode, string deleteSign)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode),
                new SqlParameter("@deleteSign",deleteSign)
            };
            this.ExecuteNonQuery("pr_b_DeletesjpcProject", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetDBProjectList：获取督办案件列表

        /// <summary>
        /// 获取督办案件列表
        /// </summary>
        /// <param name="Leader">督办人</param>
        /// <param name="projcode">案件编号</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="areacode">区</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        public DataSet GetDBProjectList(string Leader, string projcode, string times1, string times2, string areacode, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Leader",Leader),
                                new SqlParameter("@projcode",projcode),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),
                                new SqlParameter("@areacode",areacode),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetDbProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
        }
        #endregion

        #region GetGZProjectList：获取挂账案件列表

        /// <summary>
        /// 获取挂账案件列表
        /// </summary>
        /// <param name="Leader">督办人</param>
        /// <param name="projcode">案件编号</param>
        /// <param name="times1">查询时间，从：</param>
        /// <param name="times2">查询时间，到</param>
        /// <param name="areacode">区</param>
        /// <param name="page">分页结构体</param>
        /// <param name="ErrMsg">返回错误信息</param>
        public DataSet GetGZProjectList(string Leader, string projcode, string times1, string times2, string areacode, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Leader",Leader),
                                new SqlParameter("@projcode",projcode),
                                new SqlParameter("@times1",times1),
                                new SqlParameter("@times2",times2),
                                new SqlParameter("@areacode",areacode),
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetGZProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            return ds;
        }
        #endregion

        #region  DB_InsertInspectInfo：督办插入案件流程

        /// <summary>
        /// 督办插入案件流程
        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <param name="Leader">领导</param>
        /// <param name="content">督办批示</param>
        /// <param name="usercode">经办人</param>
        /// <returns></returns>
        public void DB_InsertInspectInfo(string projcode, string Leader, string content, string usercode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",projcode),
                new SqlParameter("@content",content),
                new SqlParameter("@Leader",Leader),
                new SqlParameter("@usercode",usercode)
            };

            ExecuteNonQuery("pr_b_InsertInspectInfo", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  CancelProject： 根据案件编号，问题注销
        /// <summary>
        /// 根据案件编号，问题注销（打问题注销标示）

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        public void CancelProject(string projcode)
        {
            string sql = string.Format("update b_project set isdel = 2,enddate = getdate() where projcode = {0}", projcode);
            this.ExecuteNonQuery(sql);
        }
        #endregion

        #region  DeleteGDProject： 根据案件编号，彻底删除注销的问题

        /// <summary>
        ///  根据案件编号，彻底删除注销的问题

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public void DeleteGDProject(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode)
            };
            this.ExecuteNonQuery("pr_b_DeleteGDProject", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  MoveDataToHistoryDB： 从业务数据移记录到历史数据库
        /// <summary>
        ///  根据案件编号、年份，把记录从业务数据移记录到历史数据库。

        ///  结案或者问题注销的时候，需要移数据操作。

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="Year">案件受理年份</param>
        /// <returns></returns>
        public void MoveDataToHistoryDB(string projcode, string Year)
        {
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode),
                new SqlParameter("@Year", Year)
            };
            this.ExecuteNonQuery("pr_b_EndCaseMoveData", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region  RollBackGDProject： 根据案件编号，还原归档的案件
        /// <summary>
        /// 根据案件编号，还原归档的案件
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public void RollBackGDProject(string projcode, string startdate)
        {
            string curentYear = System.DateTime.Parse(startdate).Year.ToString();
            SqlParameter[] arrSP = new SqlParameter[] { 
                new SqlParameter("@projcode", projcode),
                new SqlParameter("@StartYear", curentYear)
            };
            this.ExecuteNonQuery("pr_b_RollBackGDProject", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetFeedBackPhone：根据案件编号，获取需要反馈的电话号码
        /// <summary>
        /// 根据案件编号，获取需要反馈的电话号码
        /// </summary>
        /// <param name="projcode">案件编码</param>
        /// <param name="strYear">案件发生年</param>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <returns></returns>
        public DataSet GetFeedBackPhone(string projcode, string strYear, string IsEnd)
        {
            string strSQL = "";
            if (IsEnd == "1")
            {
                string strTmp = "bacg_bak" + strYear + "..";
                strSQL = string.Format(@"select a.projname,b.ccid
            												from {1}b_project_detail a 
            												left join {1}b_public_proj b
            												on a.projcode = b.projcode 
            												where a.projcode={0}", projcode, strTmp);
            }
            else
            {
                strSQL = string.Format(@"   select a.projname,b.ccid
									        from b_project_detail a 
									        left join b_public_proj b
									        on a.projcode = b.projcode 
									        where a.projcode={0}", projcode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region FeedBack：把用户的反馈信息插入到表中
        /// <summary>
        ///把用户的反馈信息插入到表中

        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="strYear">年</param>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <param name="Feedbacker">反馈用户</param>
        /// <param name="FeedbackMode">反馈类型</param>
        /// <param name="FeedbackTarget">反馈对象</param>
        /// <param name="FeedbackTargetMobile">反馈号码</param>
        /// <param name="FeedbackContent">反馈内容</param>       
        public void FeedBack(string projcode, string strYear, string IsEnd,
                            string Feedbacker, string FeedbackMode, string FeedbackTarget,
                            string FeedbackTargetMobile, string FeedbackContent)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode),
                                new SqlParameter("@Year",strYear),
                                new SqlParameter("@IsEnd",IsEnd),
                                new SqlParameter("@Feedbacker",Feedbacker),
                                new SqlParameter("@FeedbackMode",FeedbackMode),
                                new SqlParameter("@FeedbackTarget",FeedbackTarget),
                                new SqlParameter("@FeedbackTargetMobile",FeedbackTargetMobile),
                                new SqlParameter("@FeedbackContent",FeedbackContent)};

            this.ExecuteNonQuery("pr_b_FeedBack", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetFeedBackList:根据案件编码，获取案件反馈列表

        /// <summary>
        /// 根据案件编码，获取案件反馈列表

        /// </summary>
        /// <param name="IsEnd">是否结案或注销</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="startYear">案卷开始时间</param>
        /// <returns></returns>
        public DataSet GetFeedBackList(string projcode, string IsEnd, string strYear)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode),
                                new SqlParameter("@Year",strYear),
                                new SqlParameter("@IsEnd",IsEnd)};

            return this.ExecuteDataset("pr_b_GetFeedBackList", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        #region GetBigClassList:获取大类事部件列表

        /// <summary>
        /// 根据案件编码，获取案件反馈列表

        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetBigClassList(string typeCode)
        {
            string strSQL = "";
            if (typeCode == "")
            {
                //                strSQL = @"  select '' as bigclassCode,'全部' as name
                //                            union
                //                            select bigclassCode,name from s_bigclass_part";
                strSQL = @"  select '' as bigclassCode,'全部' as name ";
            }
            else if (typeCode == "0")
            {
                strSQL = @"  select '' as bigclassCode,'全部' as name
                            union
                            select bigclassCode,name from s_bigclass_part";
            }
            else if (typeCode == "1")
            {
                strSQL = @" select '' as bigclassCode,'全部' as name
                            union
                            select bigclassCode,name from s_bigclass_event";
            }

            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetSmallClassList:获取小类事部件列表

        /// <summary>
        /// 获取小类事部件列表

        /// </summary>
        /// <param name="typeCode">标识是事件还是部件</param>
        /// <param name="bigclassCode">大类事部件编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetSmallClassList(string typeCode, string bigclassCode)
        {
            string strSQL = "";
            if (typeCode == "")
            {
                strSQL = string.Format(@"   select '' as smallcallCode,'全部' as name
                                            union
                                            select smallcallCode,name from s_smallclass_part
                                            where bigclassCode='{0}'", bigclassCode);
            }
            else if (typeCode == "0")
            {
                strSQL = string.Format(@"   select '' as smallcallCode,'全部' as name
                                            union
                                            select smallcallCode,name from s_smallclass_part
                                            where bigclassCode='{0}'", bigclassCode);
            }
            else if (typeCode == "1")
            {
                strSQL = string.Format(@"   select '' as smallcallCode,'全部' as name
                                            union
                                            select smallcallCode,name from s_smallclass_event
                                            where bigclassCode='{0}'", bigclassCode);
            }

            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetAreaList:根据市区编码,获取区域列表
        /// <summary>
        /// 根据区域编码,获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetAreaList(string areacode)
        {
            string strSQL = "";
            if (areacode.Length == 4)
            {
                strSQL = "select null as areacode,'全部' as areaname from s_area  union select areacode , areaname  from s_area where areacode<>'" + areacode + "'";
            }
            else
            {
                strSQL = string.Format(@"select null as areacode,'全部' as areaname from s_area  union 
                                                select  areacode , areaname  
                                                from s_area
                                                where areacode like '{0}%' ", areacode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetStreetList:根据区域编码,获取街道列表
        /// <summary>
        /// 根据区域编码,获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetStreetList(string areacode)
        {
            string strSQL = "";
            if (areacode.Equals(""))
            {
                strSQL = @" select 1 as id,null as streetcode,'全部' as streetname
                            union
                            select id,streetcode,streetname from s_street order by id";
            }
            else
            {
                strSQL = string.Format(@"   select 1 as id,null as streetcode,'全部' as streetname
                                            union   
                                            select id,streetcode,streetname from s_street
                                            where streetcode like '{0}%' order by id", areacode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetCommList:根据街道编码,获取社区列表
        /// <summary>
        /// 根据街道编码,获取社区列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <returns></returns>
        public DataSet GetCommList(string areacode, string streetcode)
        {
            string strSQL = "";
            if (streetcode.Equals(""))
            {
                strSQL = string.Format(@"   select '' as commcode,'全部' as commname
                                            union
                                            select commcode,commname 
                                            from s_community
                                            where commcode like '{0}%'", areacode);
            }
            else
            {
                strSQL = string.Format(@"   select '' as commcode,'全部' as commname
                                            union
                                            select commcode,commname 
                                            from s_community
                                            where commcode like '{0}%'", streetcode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetDepartList:根据区域编码，获取部门列表

        /// <summary>
        /// 根据区域编码，获取部门列表

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetDepartList(string areacode)
        {
            string strSqlL = string.Format(@"   select '' as UserDefinedCode,'全部' as departname
                                                union
                                                select UserDefinedCode,departname
                                                from p_depart
                                                where area like '{0}%' 
                                                    and IsDuty=1 and isnull(IsDel,0) <> 1
                                                order by UserDefinedCode", areacode);
            return this.ExecuteDataset(strSqlL);
        }

        /// <summary>
        /// 根据区域编码，获取部门列表

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="IsParentcode">是否要查询父级列</param>
        /// <returns></returns>
        public DataSet GetDepartList(string areacode, bool IsParentcode)
        {
            string strSQL = "";
            if (IsParentcode)
            {
                strSQL = string.Format(@"   
                    select UserDefinedCode,departname,departcode,parentcode,isduty
                    from p_depart
                    where area like '{0}%' 
                        and IsDuty=1 
                        and isnull(IsDel,0) <> 1
                    order by UserDefinedCode", areacode);
            }
            else
            {
                strSQL = string.Format(@"
                    select a.UserDefinedCode,departname,a.departcode,a.parentcode,isduty
                    from p_depart a ,(
	                    select distinct parentcode 
	                    from p_depart
	                    where area like '{0}%' and IsDuty=1 and isnull(isDel,0)<>1
                    ) b
                    where a.departcode=b.parentcode
	                    and isnull(isDel,0)<>1
                    union 
                    select UserDefinedCode,departname,departcode,parentcode,isduty
                    from p_depart
                    where area like '{0}%' and IsDuty=1 and isnull(isDel,0)<>1  and isnull(NoAppraise,0)=0
                    order by UserDefinedCode", areacode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetTelephonistList:根据区域编码，获取接线员列表
        /// <summary>
        /// 根据区域编码，获取接线员列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetTelephonistList(string areacode)
        {
            string strSQL = string.Format(@"   select null as usercode,'全部' as username
                                                union
                                                select distinct a.usercode as usercode,a.username as username
                                                from p_user a 
                                                inner join p_user_role b on a.usercode = b.usercode
                                                inner join p_role c on b.rolecode = c.rolecode
                                                where c.stepid = 1 and isnull(a.isDel,0) = 0 and isnull(a.memo,'') not like '测试%' 
                                                and a.areacode like '{0}%'                                
                                                order by usercode", areacode);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetCollecterList:根据街道编码和社区编码，获取监督员的列表
        /// <summary>
        /// 根据街道编码和社区编码，获取监督员的列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetCollecterList(string areacode, string streetcode, string commcode)
        {
            string strSQL = "";
            if (commcode != "")
            {
                strSQL = string.Format(@" select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and isnull(isdel,'0')='0' and collname not like '%测试%'", commcode);
            }
            else if (streetcode != "")
            {
                strSQL = string.Format(@"  select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and isnull(isdel,'0')='0' and collname not like '%测试%'", streetcode);
            }
            else if (areacode != "")
            {
                strSQL = string.Format(@" select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and isnull(isdel,'0')='0' and collname not like '%测试%'", areacode);
            }
            else
            {
                strSQL = @" select collcode,collname from m_collecter where isnull(isdel,'0')='0' and collname not like '%测试%'";
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetCollCodeByName:根据街道编码和社区编码和监督员姓名，获取监督员编号

        /// <summary>
        /// 根据街道编码和社区编码和监督员姓名，获取监督员编号

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <param name="commcode">社区编码</param>
        /// <param name="collname">监督员姓名</param>
        /// <returns></returns>
        public DataSet GetCollCodeByName(string areacode, string streetcode, string commcode, string collname)
        {
            string strSQL = "";
            if (commcode != "")
            {
                strSQL = string.Format(@" select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and collname like '%{1}%' and isnull(isdel,'0')='0' ", commcode, collname);
            }
            else if (streetcode != "")
            {
                strSQL = string.Format(@" select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and collname like '%{1}%' and isnull(isdel,'0')='0' ", streetcode, collname);
            }
            else if (areacode != "")
            {
                strSQL = string.Format(@" select collcode,collname from m_collecter
                                            where gridcode like '{0}%' and collname like '%{1}%' and isnull(isdel,'0')='0' ", areacode, collname);
            }
            else
            {
                strSQL = string.Format(@"select collcode,collname from m_collecter where collname like '%{0}%' and isnull(isdel,'0')='0' ", collname);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetQueryProList:获取查询箱的案件的列表

        /// <summary>
        /// 获取查询箱的案件的列表

        /// </summary>
        /// <param name="probsource">案卷来源</param>
        /// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        /// <param name="probclass">案卷类型（部件，事件）</param>
        /// <param name="bigclass">大类编码</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="street">街道编码</param>
        /// <param name="square">社区编码</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="address">地址</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="collcode">监督员编号</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataTable GetQueryProList(string probsource, string state, string probclass,
                                                string bigclass, string smallclass, string street,
                                                string square, string times1, string times2,
                                                string projcode, string address, string areacode,
                                                string collcode, string departcode, string projectkind,
                                                PageInfo page, string times3, string times4, string deleted)
        {
            return GetQueryProList(probsource, state, probclass,
                                                bigclass, smallclass, street,
                                                square, times1, times2,
                                                projcode, address, areacode,
                                                collcode, "-1",
                                                departcode, projectkind,
                                                page, times3, times4, deleted, "");
        }

        ///// <summary>
        ///// 获取查询箱的案件的列表

        ///// </summary>
        ///// <param name="probsource">案卷来源</param>
        ///// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        ///// <param name="probclass">案卷类型（部件，事件）</param>
        ///// <param name="bigclass">大类编码</param>
        ///// <param name="smallclass">小类编码</param>
        ///// <param name="street">街道编码</param>
        ///// <param name="square">社区编码</param>
        ///// <param name="times1">开始时间</param>
        ///// <param name="times2">结束时间</param>
        ///// <param name="projcode">案卷编号</param>
        ///// <param name="address">地址</param>
        ///// <param name="areacode">区域编码</param>
        ///// <param name="collcode">监督员编号</param>
        ///// <param name="telephonistcode">接线员编号</param>
        ///// <param name="departcode">部门编号</param>
        ///// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        ///// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        ///// <param name="ErrMsg">输出错误信息</param>
        ///// <returns></returns>
        //public DataTable GetQueryProList(string probsource, string state, string probclass,
        //                                        string bigclass, string smallclass, string street,
        //                                        string square, string times1, string times2,
        //                                        string projcode, string address, string areacode,
        //                                        string collcode, string telephonistcode,
        //                                        string departcode, string projectkind,
        //                                        PageInfo page, string times3, string times4, string deleted)
        //{
        //        //int startYear;
        //        //int endYear;
        //        //int curentYear = System.DateTime.Now.Date.Year;
        //        //System.DateTime startDateTime;
        //        //System.DateTime endDateTime;
        //        //if (times1 != "")
        //        //{
        //        //    startDateTime = System.DateTime.Parse(times1);
        //        //    if (startDateTime.Year < 2007)
        //        //        startYear = 2007;
        //        //    else
        //        //        startYear = startDateTime.Year;
        //        //}
        //        //else
        //        //{
        //        //    startYear = curentYear;
        //        //}
        //        //if (times2 != "")
        //        //{
        //        //    endDateTime = System.DateTime.Parse(times2);
        //        //    if (endDateTime.Year > curentYear)
        //        //        endYear = curentYear;
        //        //    else
        //        //        endYear = endDateTime.Year;
        //        //}
        //        //else
        //        //{
        //        //    endYear = curentYear;
        //        //}
        //        SqlParameter[] arrSP = new SqlParameter[]{
        //                            //new SqlParameter("@StartYear",startYear),
        //                            //new SqlParameter("@EndYear",endYear),
        //                            new SqlParameter("@probsource", probsource),
        //                            new SqlParameter("@state", state),
        //                            new SqlParameter("@typecode", probclass),
        //                            new SqlParameter("@bigclass", bigclass),
        //                            new SqlParameter("@smallclass",smallclass),
        //                            new SqlParameter("@street", street),
        //                            new SqlParameter("@square", square),
        //                            new SqlParameter("@times1", times1),
        //                            new SqlParameter("@times2", times2),
        //                            new SqlParameter("@projcode", projcode),
        //                            new SqlParameter("@address", address),
        //                            new SqlParameter("@areacode", areacode),
        //                            new SqlParameter("@collcode", collcode),
        //                            new SqlParameter("@telephonistcode",telephonistcode),
        //                            new SqlParameter("@departcode", departcode),
        //                            new SqlParameter("@projectkind", projectkind),
        //                            new SqlParameter("@CurrentPage",page.CurrentPage),
        //                            new SqlParameter("@RowCount",SqlDbType.Int),
        //                            new SqlParameter("@PageCount",SqlDbType.Int),
        //                            new SqlParameter("@PageSize",page.PageSize),
        //                            new SqlParameter("@Order",page.Order),
        //                            new SqlParameter("@Field",page.Field),
        //                            new SqlParameter("@times3",times3),
        //                            new SqlParameter("@Deleted",deleted),
        //                            new SqlParameter("@times4",times4)};

        //        arrSP[17].Direction = ParameterDirection.Output;
        //        arrSP[18].Direction = ParameterDirection.Output;

        //        DataSet ds = this.ExecuteDataset("pr_b_GetQueryProList", CommandType.StoredProcedure, arrSP);
        //        page.RowCount = arrSP[17].Value.ToString();
        //        page.PageCount = arrSP[18].Value.ToString();

        //        if (ds.Tables.Count > 0)
        //            return ds.Tables[0];
        //        else
        //            return null;
        //}


        #endregion

        #region GetQueryProList:获取查询箱的案件的列表--2008年06月03日添加

        /// <summary>
        /// 获取查询箱的案件的列表，2008年06月03日添加，添加“案卷号“，”详细信息”查询

        /// </summary>
        /// <param name="probsource">案卷来源</param>
        /// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        /// <param name="probclass">案卷类型（部件，事件）</param>
        /// <param name="bigclass">大类编码</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="street">街道编码</param>
        /// <param name="square">社区编码</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="address">地址</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="collcode">监督员编号</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <param name="probdesc">详细信息</param>
        /// <returns></returns>
        public DataTable GetQueryProList(string probsource, string state, string probclass,
                                                string bigclass, string smallclass, string street,
                                                string square, string times1, string times2,
                                                string projcode, string address, string areacode,
                                                string collcode, string departcode, string projectkind,
                                                PageInfo page, string times3, string times4, string deleted, string probdesc)
        {
            return GetQueryProList(probsource, state, probclass,
                                                bigclass, smallclass, street,
                                                square, times1, times2,
                                                projcode, address, areacode,
                                                collcode, "-1",
                                                departcode, projectkind,
                                                page, times3, times4, deleted, probdesc);
        }

        /// <summary>
        /// 获取查询箱的案件的列表

        /// </summary>
        /// <param name="probsource">案卷来源</param>
        /// <param name="state">案卷状态（预立案，立案，未结案，结案）</param>
        /// <param name="probclass">案卷类型（部件，事件）</param>
        /// <param name="bigclass">大类编码</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="street">街道编码</param>
        /// <param name="square">社区编码</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="address">地址</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="collcode">监督员编号</param>
        /// <param name="telephonistcode">接线员编号</param>
        /// <param name="departcode">部门编号</param>
        /// <param name="projectkind">案卷类型（普通案卷0，区处理案卷1，重大案卷2）</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataTable GetQueryProList(string probsource, string state, string probclass,
                                                string bigclass, string smallclass, string street,
                                                string square, string times1, string times2,
                                                string projcode, string address, string areacode,
                                                string collcode, string telephonistcode,
                                                string departcode, string projectkind,
                                                PageInfo page, string times3, string times4, string deleted, string probdesc)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                    //new SqlParameter("@StartYear",startYear),
                                    //new SqlParameter("@EndYear",endYear),
                                    new SqlParameter("@probsource", probsource),
                                    new SqlParameter("@state", state),
                                    new SqlParameter("@typecode", probclass),
                                    new SqlParameter("@bigclass", bigclass),
                                    new SqlParameter("@smallclass",smallclass),
                                    new SqlParameter("@street", street),
                                    new SqlParameter("@square", square),
                                    new SqlParameter("@times1", times1),
                                    new SqlParameter("@times2", times2),
                                    new SqlParameter("@projcode", projcode),
                                    new SqlParameter("@address", address),
                                    new SqlParameter("@areacode", areacode),
                                    new SqlParameter("@collcode", collcode),
                                    new SqlParameter("@telephonistcode",telephonistcode),
                                    new SqlParameter("@departcode", departcode),
                                    new SqlParameter("@projectkind", projectkind),
                                    new SqlParameter("@CurrentPage",page.CurrentPage),
                                    new SqlParameter("@RowCount",SqlDbType.Int),
                                    new SqlParameter("@PageCount",SqlDbType.Int),
                                    new SqlParameter("@PageSize",page.PageSize),
                                    new SqlParameter("@Order",page.Order),
                                    new SqlParameter("@Field",page.Field),
                                    new SqlParameter("@times3",times3),
                                    new SqlParameter("@Deleted",deleted),
                                    new SqlParameter("@times4",times4),
                                    new SqlParameter("@probdesc",probdesc)};

            arrSP[17].Direction = ParameterDirection.Output;
            arrSP[18].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_b_GetQueryProList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[17].Value.ToString();
            page.PageCount = arrSP[18].Value.ToString();

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion


        #region  查询箱二期
        public DataTable GetQueryProListerqi(string probsource, string state, string probclass,
                                         string bigclass, string smallclass, string street,
                                         string square, string times1, string times2,
                                         string projcode, string address, string areacode,
                                         string collcode, string telephonistcode,
                                         string departcode, string projectkind,
                                         PageInfo page, string times3, string times4, string deleted, string trace, string probdesc)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                    //new SqlParameter("@StartYear",startYear),
                                    //new SqlParameter("@EndYear",endYear),
                                    new SqlParameter("@probsource", probsource),
                                    new SqlParameter("@state", state),
                                    new SqlParameter("@typecode", probclass),
                                    new SqlParameter("@bigclass", bigclass),
                                    new SqlParameter("@smallclass",smallclass),
                                    new SqlParameter("@street", street),
                                    new SqlParameter("@square", square),
                                    new SqlParameter("@times1", times1),
                                    new SqlParameter("@times2", times2),
                                    new SqlParameter("@projcode", projcode),
                                    new SqlParameter("@address", address),
                                    new SqlParameter("@areacode", areacode),
                                    new SqlParameter("@collcode", collcode),
                                    new SqlParameter("@telephonistcode",telephonistcode),
                                    new SqlParameter("@departcode", departcode),
                                    new SqlParameter("@projectkind", projectkind),
                                    new SqlParameter("@CurrentPage",page.CurrentPage),
                                    new SqlParameter("@RowCount",SqlDbType.Int),
                                    new SqlParameter("@PageCount",SqlDbType.Int),
                                    new SqlParameter("@PageSize",page.PageSize),
                                    new SqlParameter("@Order",page.Order),
                                    new SqlParameter("@Field",page.Field),
                                    new SqlParameter("@times3",times3),
                                    new SqlParameter("@Deleted",deleted),
                                    new SqlParameter("@times4",times4),
                                     new SqlParameter("@trace",trace),
                                    new SqlParameter("@probdesc",probdesc)
                                  };

            arrSP[17].Direction = ParameterDirection.Output;
            arrSP[18].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_b_GetQueryProList_erqi", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[17].Value.ToString();
            page.PageCount = arrSP[18].Value.ToString();

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion
        #region InitDepart：初始化部门以及其子部门
        /// <summary>
        /// 初始化部门以及其子部门

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="departcode">部门编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public DataSet InitDepart(string areacode, string departcode)
        {
            string strSQL = "";
            if (departcode == "") //非专业部门
            {
                strSQL = string.Format(@"  select departcode,UserDefinedCode,departname 
                                            from p_depart 
                                            where isnull(IsDel,0)=0 and isDuty='1' and area like '{0}%' 
                                            order by UserDefinedCode ", areacode);
            }
            else
            {
                strSQL = string.Format(@"   select departcode,UserDefinedCode,departname
                                            from p_depart
                                            where isnull(IsDel,0)=0 and isDuty='1' and area like '{0}%' 
                                                and (departcode = '{1}' or parentcode='{1}')
                                            order by UserDefinedCode ", areacode, departcode);
            }
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetDepartProjectList：根据部门用户编码，获取改部门处理过的案卷列表

        /// <summary>
        /// 根据部门用户编码，获取改部门处理过的案卷列表
        /// </summary>
        /// <param name="userDefinedCode">部门用户定义编码</param>
        /// <param name="projectstate">案卷类型（已处理（已结案），已处理（未结案），正在处理）</param>
        /// <param name="times1">开始时间</param>
        /// <param name="times2">结束时间</param>
        /// <param name="page">页面信息（当前页，分页大小，总行数，总列数）</param>
        /// <param name="ErrMsg">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetDepartProjectList(string userDefinedCode, string projectstate, string times1,
                                                      string times2, PageInfo pgInfo)
        {
            int startYear;
            int endYear;
            int curentYear = System.DateTime.Now.Date.Year;
            System.DateTime startDateTime;
            System.DateTime endDateTime;
            if (times1 != "")
            {
                startDateTime = System.DateTime.Parse(times1);
                if (startDateTime.Year < 2007)
                    startYear = 2007;
                else
                    startYear = startDateTime.Year;
            }
            else
            {
                startYear = curentYear;
            }
            if (times2 != "")
            {
                endDateTime = System.DateTime.Parse(times2);
                if (endDateTime.Year > curentYear)
                    endYear = curentYear;
                else
                    endYear = endDateTime.Year;
            }
            else
            {
                endYear = curentYear;
            }
            SqlParameter[] arrSP = new SqlParameter[]{
                                    new SqlParameter("@UserDefinedCode",userDefinedCode),
                                    new SqlParameter("@StartYear",startYear),
                                    new SqlParameter("@EndYear",endYear),
                                    new SqlParameter("@projectstate", projectstate),
                                    new SqlParameter("@times1", times1),
                                    new SqlParameter("@times2", times2),
                                    new SqlParameter("@CurrentPage",pgInfo.CurrentPage),
                                    new SqlParameter("@RowCount",SqlDbType.Int),
                                    new SqlParameter("@PageCount",SqlDbType.Int),
                                    new SqlParameter("@PageSize",pgInfo.PageSize),
                                    new SqlParameter("@Order",pgInfo.Order),
                                    new SqlParameter("@Field",pgInfo.Field)};
            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetDepartProjectList", CommandType.StoredProcedure, arrSP);
            pgInfo.RowCount = arrSP[7].Value.ToString();
            pgInfo.PageCount = arrSP[8].Value.ToString();

            return ds;
        }
        #endregion

        #region 上传媒体信息
        /// <summary>
        /// 根据外部系统ID获取本系统的案卷信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ProjectInfo GetProjectInfoByOutCode(string code)
        {
            string sqlStr = @"select 
                                pro.projCode,
                                detail.projname 
                            from 
                                b_project pro 
                                left join b_project_detail detail on pro.projcode=detail.projcode 
                            where pro.changestatus ='" + code + "'";

            DataSet ds = this.ExecuteDataset(sqlStr);

            ProjectInfo info = new ProjectInfo();
            if (ds != null && ds.Tables.Count > 0)
            {
                info.projcode = ds.Tables[0].Rows[0]["projCode"].ToString();
                info.projcodeName = ds.Tables[0].Rows[0]["projname"].ToString();
            }

            return info;
        }
        #endregion

        #region  GetCurrentButtonCode：并发操作时，解锁案件

        /// <summary>
        /// 并发操作时，解锁案件
        /// </summary>
        /// <param name="projcode">案件编号</param>
        public string GetCurrentButtonCode(string busis)
        {
            string strSQL = string.Format(@"SELECT b.buttoncode  
                                            FROM s_flownoderelainfo a,p_model_power b
	                                        WHERE a.relaid = b.relaid 
		                                    AND a.flowid = b.flowid 
		                                    AND a.busistatus = '{0}'
		                                    AND b.buttoncode like '119%'
                                            fetch first 1 rows only", busis);
            object oResult = this.ExecuteScalar(strSQL);
            return oResult != null ? oResult.ToString() : "";
        }
        #endregion

        #region  pr_b_InsertProjectManyDept：派遣多部门时候生成多条案件

        /// <summary>
        /// 派遣多部门时候生成多条案件

        /// </summary>
        /// <param name="projcode">案件编号</param>
        /// <returns></returns>
        public void InsertProjectManyDept(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                new SqlParameter("@projcode",projcode)
            };

            ExecuteNonQuery("pr_b_InsertProjectManyDept", CommandType.StoredProcedure, arrSP);
        }
        #endregion

        //add by yaoch 2012-10-29
        #region GetProjectCode2ByCode：根据案件编号获取二级案件的编号
        /// <summary>
        /// 根据案件编号获取二级案件的编号
        /// </summary>
        /// <param name="projcode">一级平台案件编号</param>
        /// <returns></returns>
        public String GetProjectCode2ByCode(string projcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)};

            object oResult = this.ExecuteScalar("pr_b_GetProjectCode2ByCode", CommandType.StoredProcedure, arrSP);

            String code = oResult == null ? projcode : oResult.ToString();

            return code;
        }
        #endregion




        #region  领导通案卷接口

        /// <summary>
        /// 查询违法案卷
        /// </summary>
        /// <param name="page">页码信息</param>
        /// <param name="topNumber">取数据行数</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataTable GetLProject(PageInfo page,string usercode, string laTime, string address, string desctribe, string startTime, string endTime)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@CurrentPage",page.CurrentPage),
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field),
                                new SqlParameter("@BeginTime",startTime),
                                new SqlParameter("@EndTime",endTime),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@LaTime",laTime),
                                new SqlParameter("@Address",address),
                                new SqlParameter("@Desctribe",desctribe),
                                new SqlParameter("@UserCode",usercode)
            };


            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[7].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_b_GetLProjectList", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[6].Value.ToString();
            page.PageCount = arrSP[7].Value.ToString();

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public int GetLProjectCount(string strattime, string endtime)
        {
            string sqlstr = @" select COUNT(1) from b_project  where startdate>=@StartTime and startdate<@EndTime and bigclass=22 and isnull(isdel,0)<>1";
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StartTime",strattime),
                                new SqlParameter("@EndTime",endtime)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            else
                return 0;
        }

        public DataTable GetLProjectCountList(string strattime, string endtime)
        {
            string sqlstr = @" select b.address,c.probdesc,b.fid,b.projcode,c.projname,a.name,b.startdate,b.probsource 
                            from b_project b
                             left join b_project_detail c on c.projcode=b.projcode
                             left join b_other_proj a on a.projcode=b.projcode
                            where b.startdate>=@StartTime and b.startdate<@EndTime and b.bigclass=22 and isnull(b.isdel,0)<>1";
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StartTime",strattime),
                                new SqlParameter("@EndTime",endtime)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public DataTable GetLProjectRank(string starttime, string endtime)
        {
            string sqlstr = @" 
                         select top 10 name,COUNT(*) as count from b_other_proj  a
                         left join b_project b on a.projcode=b.projcode
                         where  ISNULL(name,'')<>''
	                        and cudate>=@StartTime and cudate<=@EndTime
	                        and  b.bigclass=22 and isnull(b.isdel,0)<>1
                         group by name order by count desc";

            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StartTime",starttime),
                                new SqlParameter("@EndTime",endtime)
             };

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public DataTable GetLProjectRank(string starttime, string endtime,string username)
        {
            string sqlstr = @" 
                        select b.address,c.probdesc,b.fid,b.projcode,c.projname,a.name,b.startdate,b.probsource from b_other_proj  a
                         left join b_project b on a.projcode=b.projcode
                         left join b_project_detail c on b.projcode=c.projcode
                         where  ISNULL(name,'')<>''
	                        and cudate>=@StartTime and cudate<=@EndTime
	                        and  b.bigclass=22 and isnull(b.isdel,0)<>1
							and a.name=@UserName";

            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StartTime",starttime),
                                new SqlParameter("@EndTime",endtime),
                                new SqlParameter("@UserName",username)
             };

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        public DataTable GetLProjectDetail(string projcode)
        {
            string sqlstr = @" 
                        select b.address,c.probdesc,b.fid,b.projcode,c.projname,a.name,b.startdate,b.probsource from b_other_proj  a
                         left join b_project b on a.projcode=b.projcode
                         left join b_project_detail c on b.projcode=c.projcode
                         where  ISNULL(name,'')<>''
	                        and  b.bigclass=22 and isnull(b.isdel,0)<>1
							and b.projcode=@projcode";

            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@projcode",projcode)
             };

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public int AddProjectSupervise(string projectcode, string content, string usercode, string username, string state)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Projcode",projectcode),
                                new SqlParameter("@Content",content),
                                new SqlParameter("@UserCode",usercode),
                                new SqlParameter("@UserName",username),
                                new SqlParameter("@State",state)
            };

            string sqlstr = @"insert into b_project_supervise(Projcode,Content,Createtime,UserCode,UserName,State) values(@Projcode,@Content,getdate(),@UserCode,@UserName,@State)";

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, arrSP);
        }

        public int UpdateProjectSupervise(string id, string content, string state)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Content",content),
                                new SqlParameter("@State",state),
                                new SqlParameter("@Id",id)
            };

            string sqlstr = @"update b_project_supervise set Content=@Content ,State=@State where Id=@Id";

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, arrSP);
        }

        public DataTable GetProjectSupervise(string procode,string state)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Projcode",procode),
                                new SqlParameter("@State",state)
            };

            string sqlstr = @"select a.*,b.sort from dbo.b_project_supervise a 
                               left join p_user b on a.UserCode=b.usercode
                               where a.projcode=@Projcode ";
            if (!string.IsNullOrEmpty(state))
            {
                sqlstr += " and a.state=@State";
            }
            sqlstr += " order by b.sort,a.Createtime asc";

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, arrSP);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion
    }
}
