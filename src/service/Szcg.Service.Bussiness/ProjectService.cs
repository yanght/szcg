using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Szcg.Service.IBussiness;
using Szcg.Service.Common;
using Szcg.Core.bussinesDBL;
using bacgDL.business;
using Szcg.Service.Model;
using szcg.com.teamax.business.entity;
using bacgBL.web.szbase.doormanager;

namespace Szcg.Service.Bussiness
{
    public class ProjectService : IProjectService
    {
        private string strErr = "";

        #region [ 案卷相关 ]

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ReturnValue GetDealProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {

            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            string Error = string.Empty;

            DataSet ds = bacgBL.business.Project.GetDealProjectList(projectInfo, startTime, endTime, pageInfo, out Error);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取待办案件列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
            }
            rtn.ReturnObj = list;

            return rtn;
        }

        /// <summary>
        /// 获取自办件案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="departCode">职能部门编码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ReturnValue GetZbjProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string departCode, string startTime, string endTime)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.GetYJProjectList(projectInfo, startTime, endTime, departCode, pageInfo, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取自办件案件列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 获取存档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ReturnValue GetCDProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.GetCDProjList(projectInfo, startTime, endTime, pageInfo, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取存档案件列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 获取待反馈案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ReturnValue GetWaitFeedBackProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.WaitFeedBackList(projectInfo, startTime, endTime, pageInfo, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取待反馈案件列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 获取督办案卷列表
        /// </summary>
        /// <param name="leader">督办人姓名</param>
        /// <param name="projcode">案卷编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        public ReturnValue GetDBProjectList(string leader, string projcode, string startTime, string endTime, string areacode, PageInfo pageInfo)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.GetDBProjectList(leader, projcode, startTime, endTime, areacode, pageInfo, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取督办案卷列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 查询箱查询案卷列表
        /// </summary>
        /// <param name="args">查询参数</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        public ReturnValue QueryProjectList(ProjectQueryArgs args, PageInfo pageInfo)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataTable dt = bacgBL.business.Project.GetQueryProListerqi(args.ProbSource
                , args.ProjectState
                , args.ProbClass
                , args.BigClass
                , args.SmallClass
                , args.Street
                , args.Square
                , args.StartTime
                , args.EndTime
                , args.Projcode
                , args.Address
                , args.AreaCode
                , args.CollCode
                , args.Telephonist
                , args.TargetDepartCode
                , args.ProjectKind
                , pageInfo
                , args.DoStartTime
                , args.DoEndTime
                , args.DeleteState
                , args.ProjectImport
                , out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("查询箱查询案卷列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(dt);
                rtn.ReturnObj = list;
            }

            return rtn;

        }

        /// <summary>
        /// 查询归档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ReturnValue GetGDProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.GetGDProjectList(projectInfo, startTime, endTime, pageInfo, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("查询归档案卷列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 查询已删除案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="userName">删除人姓名</param>
        /// <param name="type">删除时间（0：按删除 1：按上报）</param>
        /// <returns></returns>
        public ReturnValue GetDeleteProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime, string userName, int deleteTimeType)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            List<Model.Project> list = new List<Project>();

            DataSet ds = bacgBL.business.Project.GetDeleteProjectList(projectInfo.street, projectInfo, userName, startTime, endTime, pageInfo, "0", out strErr, deleteTimeType);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("查询已删除案卷列表异常：" + strErr);
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
                rtn.ReturnObj = list;
            }

            return rtn;
        }

        /// <summary>
        /// 获取案件信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="year">年份</param>
        /// <param name="isEnd">是否已结案</param>
        /// <param name="nodeId">节点Id</param>
        /// <returns></returns>
        public Project GetProjectDetail(string projcode, int year, bool isEnd)
        {
            Project project = new Project();
            string Error = string.Empty;

            DataSet ds = bacgBL.business.Project.GetProjDetail(projcode, year.ToString(), isEnd ? "1" : "0", null, out Error);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取案件信息异常：" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                project = ConvertDtHelper<Project>.GetModel(ds.Tables[0]);
                project.ProjectFiles = ConvertDtHelper<ProjectFile>.GetModelList(ds.Tables[1]);
                project.ProjectSounds = ConvertDtHelper<ProjectSound>.GetModelList(ds.Tables[2]);
                project.Collecters = ConvertDtHelper<Collecter>.GetModelList(ds.Tables[3]);
                List<ProjectCheckMessage> messages = new List<ProjectCheckMessage>();

                #region [ 案卷核查信息 ]
                if (ds.Tables.Count > 4 && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        ProjectCheckMessage message = new ProjectCheckMessage()
                        {
                            Id = dr["序号"].ToString(),
                            CollectorName = dr["巡查考核员"].ToString(),
                            Cu_Date = dr["核查回复时间"].ToString(),
                            _Option = dr["核查回复内容"].ToString()
                        };
                        messages.Add(message);
                    }
                }
                #endregion

                project.CollectorMessages = messages;
            }

            if (project.NodeId != 12 && project.NodeId != 2)
            {
                if (!string.IsNullOrEmpty(project.TreatTime))
                {
                    string[] myStr = new string[project.TreatTime.Split(',').Length];
                    myStr = project.TreatTime.Split(',');
                    project.RacTime = project.TreatTime.Split(',')[0];
                    if (myStr.Length == project.TreatTime.Split(',').Length && myStr.Length > 2)//add by yaoch 2012-10-24
                    {
                        if (myStr[2] != "0") //处理时限为0,表示视情况定
                        {
                            //剩余时间
                            if (project.NodeId == 8) //专门部门办理阶段
                                project.LimitTime = string.Format("{0:0.0}小时", double.Parse(myStr[1]) / 60);
                            else
                                project.LimitTime = string.Format("{0}分钟", myStr[1]);

                            //终止时间 
                            //CultureInfo culture = new CultureInfo("zh-CN");
                            //System.DateTime dt = Convert.ToDateTime(myStr[0], culture);
                            //dt = dt.AddMinutes(Convert.ToDouble(myStr[2])); 
                            //this.lbl_endTime.Text = dt.ToString("yyyy-MM-dd HH:mm:ss"); 
                            //终止时间  UP BY STEVE 返回值中增加了终止时间，参照数据库中函数dbo.GetTreatTime
                            project.EndTime = myStr[3].ToString();
                        }
                        else
                        {
                            project.LimitTime = "视情况定";
                            project.EndTime = "视情况定";
                        }
                    }
                }
            }
            else
            {
                project.RacTime = project.EndTime;
            }

            return project;
        }

        /// <summary>
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="IsQuerySource">是否要查询该案件相关的来源信息</param>
        /// <returns></returns>
        public Project GetProjectSomeInfo(string projcode, string nodeid)
        {
            Project project = new Project();

            string strLastOpinion = string.Empty;

            DataSet ds = bacgBL.business.Project.GetProjSomeInfo(projcode, nodeid, true, out strLastOpinion, out strErr);

            if (strErr != "" || ds.Tables[0].Rows.Count == 0)
            {
                LoggerManager.Instance.logger.ErrorFormat("加载案卷详情出错，案卷编号:{0}  错误信息：{1}", projcode, strErr);
                return null;
            }

            DataRow dr1 = ds.Tables[0].Rows[0];

            project.Projcode = dr1["projcode"].ToString();

            project.ProjName = dr1["projname"].ToString();
            project.ProbSource = dr1["probsource"].ToString();

            project.Square = dr1["square"].ToString();
            project.Street = dr1["street"].ToString();//所属街道
            project.BigClassName = dr1["bigclassname"].ToString();
            project.AreaId = dr1["AreaID"].ToString();
            project.StreetId = dr1["StreetID"].ToString();
            project.ProbClassName = dr1["probclassName"].ToString();

            project.Operator = dr1["username"].ToString();

            project.StartDate = SqlDataConverter.ToDateTime(dr1["startdate"]);
            project.Address = dr1["address"].ToString();
            project.ProbDesc = dr1["probdesc"].ToString();
            project.SmallClassName = dr1["smallclassname"].ToString();
            project.TargetDepartCode = SqlDataConverter.ToInt32(dr1["TargetDepartCode"]);
            project.TargetDepartMobile = dr1["Mobile"].ToString();
            project.TargetDepartName = dr1["departname"].ToString();

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                project.Option = SqlDataConverter.ToString(ds.Tables[1].Rows[0]["_opinion"]);
            }
            project.IsGreat = dr1["isgreat"].ToString();
            project.TypeCode = dr1["typecode"].ToString() == "True" ? "1" : "0";
            project.SmallClass = dr1["smallclass"].ToString();
            project.LastOpinion = strLastOpinion;
            return project;

        }

        /// <summary>
        /// 获取案卷流程
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="year">年份</param>
        /// <param name="isEnd">是否已结案</param>
        /// <returns></returns>
        public List<ProjectTrace> GetProjectTrace(string projcode, int year, bool isEnd)
        {
            List<Model.ProjectTrace> list = new List<ProjectTrace>();
            string Error = string.Empty;

            DataSet ds = bacgBL.business.Project.GetProjTrace(projcode, year.ToString(), isEnd ? "1" : "0", out Error);

            if (!string.IsNullOrEmpty(Error))
            {
                LoggerManager.Instance.logger.Error("获取案卷流程异常：" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {

                list = ConvertDtHelper<ProjectTrace>.GetModelList(ds.Tables[0].Rows);

            }

            return list;
        }

        /// <summary>
        /// 新增案卷
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public string AddProject(Project project, UserInfo userInfo)
        {
            string rtn = string.Empty;
            string strErr = string.Empty;

            //---------插入案件---------
            bacgDL.business.ProjectInfo prj = new bacgDL.business.ProjectInfo();
            prj.Telephonist = userInfo.getUsername();
            prj.TelephonistCode = userInfo.getUsercode().ToString();
            prj.NodeId = project.NodeId.ToString();
            prj.probsource = project.ProbSource;
            prj.typecode = project.TypeCode.ToString();
            prj.bigClass = project.BigClass;
            prj.smallclass = project.SmallClass;
            //prj.detailedclass = txt_khxzid.Value;
            prj.area = project.AreaId;
            prj.street = project.StreetId;
            prj.square = project.SquareId;
            prj.gridcode = project.GrideCode; //基础网格（单元网格）
            prj.WorkGrid = project.GrideCode; //工作网格
            prj.address = project.Address;
            //prj.isthough = "1"; //直通车，0表示经过核实
            prj.isgreat = project.IsGreat;
            prj.withDept = "0";
            prj.groupid = project.AreaId;
            prj.probdesc = project.ProbDesc;
            prj.IsNeedFeedBack = project.IsNeedFeedBack ? "1" : "0";
            prj.departcode = project.TargetDepartCode.ToString();
            prj.strCityName = System.Configuration.ConfigurationManager.AppSettings["strTitle"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["strTitle"];

            bacgDL.business.ProjectSourceInfo psi = new bacgDL.business.ProjectSourceInfo();
            psi.name = project.ReportName;
            psi.tel = project.ReportTel;
            psi.accept = userInfo.getUsercode().ToString();
            psi.ip = "";

            rtn = bacgBL.business.Project.InsertProject(prj, psi, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("新增案卷异常：" + strErr);
            }
            return rtn;
        }

        /// <summary>
        /// 修改案卷
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public bool EditProject(Project project, UserInfo userInfo)
        {
            string isgreat = "1"; //this.DropAjlx.SelectedValue;是否重大案件
            string withDept = "0"; //this.chkIsInArea.SelectedValue;//案件派发部门类型


            bacgDL.business.ProjectInfo prj = new bacgDL.business.ProjectInfo();
            prj.projcode = project.Projcode;
            prj.IsManual = project.IsManual;
            prj.probsource = project.ProbSource;
            prj.typecode = project.TypeCode.ToString();
            prj.bigClass = project.BigClass;
            prj.smallclass = project.SmallClass;
            //prj.detailedclass = this.txt_khxzid.Value;
            prj.area = project.AreaId;
            prj.street = project.StreetId;
            prj.square = project.SquareId;
            prj.gridcode = project.GrideCode; //基础网格（单元网格）
            prj.WorkGrid = project.WorkGrid; //工作网格
            prj.address = project.Address;
            prj.isgreat = project.IsGreat;//是否重大案件
            prj.withDept = "0";//案件派发部门类型
            prj.probdesc = project.ProbDesc;
            prj.IsNeedFeedBack = project.IsNeedFeedBack ? "1" : "0";
            prj.departcode = project.TargetDepartCode.ToString();

            bacgDL.business.ProjectSourceInfo psi = new bacgDL.business.ProjectSourceInfo();
            //if (project.IsManual == "True" || project.IsManual == "1")
            //{
            //    psi.name = this.txt_othername.Text;
            //    psi.tel = this.txt_othertel.Text;
            //}
            //else if (this.txtIsManual.Value == "False" || this.txtIsManual.Value == "" || this.txtIsManual.Value == "0")
            //{
            //    //公众举报通过呼叫中心上报
            //    psi.name = this.txt_PublicName.Text;
            //    psi.tel = this.txt_RetTel.Text;
            //}
            psi.name = project.ReportName;
            psi.tel = project.ReportTel;
            psi.accept = userInfo.getUsercode().ToString();
            psi.ip = "";


            bacgBL.business.Project.UpdateProject(prj, psi, out strErr);
            if (string.IsNullOrEmpty(strErr))
            {
                return true;
            }
            else
            {
                LoggerManager.Instance.logger.Error("修改案卷异常：" + strErr);
            }
            return false;
        }

        #region 案卷批转

        /// <summary>
        /// 案卷上报
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public string ProjectReport(Project project, UserInfo userInfo)
        {
            string rtn = string.Empty;
            if (!string.IsNullOrEmpty(project.Projcode))
            {
                if (EditProject(project, userInfo))
                {
                    rtn = project.Projcode;
                }
            }
            else
            {
                rtn = AddProject(project, userInfo);
            }
            return rtn;
        }

        /// <summary>
        /// 案卷办理
        /// </summary>
        /// <param name="project">案卷详细</param>  
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public bool ProjectHandler(Project project, UserInfo userInfo)
        {
            bool rtn = false;

            #region 保存案件
            rtn = !string.IsNullOrEmpty(ProjectReport(project, userInfo));
            #endregion

            #region 修改案件流程

            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = userInfo.getUsercode().ToString();
            pt.projcode = project.Projcode;
            pt.PreNodeId = "2";
            pt.CurrentBusiStatus = "03";
            pt.DepartCode = userInfo.getDepartcode().ToString();
            pt._opinion = "普通案件，批转到区局值班长";
            pt.returntracetag = "0";
            pt.buttoncode = project.ButtonCode;

            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            rtn = string.IsNullOrEmpty(strErr);
            if (!rtn)
            {
                LoggerManager.Instance.logger.Error("案卷办理异常：" + strErr);
            }
            #endregion

            return rtn;
        }

        /// <summary>
        /// 案卷批转
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public bool ProjectApproved(Project project, UserInfo userInfo)
        {
            bool rtn = false;

            #region 保存案件
            rtn = !string.IsNullOrEmpty(ProjectReport(project, userInfo));
            #endregion

            #region 修改案件流程

            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = userInfo.getUsercode().ToString();
            pt.projcode = project.Projcode;
            pt.PreNodeId = "3";
            pt.CurrentBusiStatus = "03";
            pt.DepartCode = userInfo.getDepartcode().ToString();
            pt._opinion = "普通案件，接线员批转";
            pt.returntracetag = "0";

            //判断是否改变流程
            //if (Convert.ToString(drpWrokFlowType.SelectedValue) == GetPageWorkFlowType(project.ButtonCode))
            //{
            //    //判断是否为一般案卷-- 需要修改DropAjlx.SelectedValue
            //    if ("1" == "0")
            //        pt.buttoncode = "11100003016";//pt.buttoncode = "11900003016";
            //    else
            pt.buttoncode = project.ButtonCode;

            //}
            //else
            //{
            //如果改变了流程，获得对应流程开始节点的ButtonId
            // pt.buttoncode = GetChangeFlowButtonId("8");
            //}

            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            rtn = string.IsNullOrEmpty(strErr);
            if (!rtn)
            {
                LoggerManager.Instance.logger.Error("案卷批转异常：" + strErr);
            }
            #endregion

            return rtn;

        }

        /// <summary>
        /// 案卷核实
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        public bool ProjectVerify(Project project, UserInfo userInfo, Collecter collecter)
        {
            bool rtn = false;

            #region 保存案件
            rtn = !string.IsNullOrEmpty(ProjectReport(project, userInfo));
            #endregion

            string strMsg = "请核实";
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.projcode = project.Projcode;
            pt.actionname = "核实";
            pt.PreNodeId = "2";
            pt.CurrentNodeId = "2";
            pt.CurrentBusiStatus = "04";
            pt.usercode = userInfo.getUsercode().ToString();
            pt.DepartCode = userInfo.getDepartcode().ToString();
            pt._opinion = "向监督员发核实指令";
            pt.returntracetag = "0";

            bacgBL.business.Project.Project_HS(pt, collecter.CollCode.ToString(), strMsg, out strErr, "11900003008");

            if (strErr != "")
            {
                LoggerManager.Instance.logger.Error("案卷核实异常：" + strErr);
                return false;
            }

            #region 发送核实短信
            string strMobile = collecter.Mobile;
            if (strMobile != "" && strMobile != "&nbsp;")
            {

                int strErr2 = bacgBL.Pub.MsgClass.ShortMessage("", collecter.Mobile, string.Format("您有核实任务，请查收！案件编号：{0}", project.Projcode));

                if (strErr2 == 1)
                {
                    LoggerManager.Instance.logger.Error("向监督员发送核实任务通知短信异常：" + strErr);
                    return false;
                }
            }
            #endregion


            return rtn;
        }

        /// <summary>
        /// 受理员案卷批转
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectAcceptApproved(ProjectApprovedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = args.NodeId;//当前节
            pt.CurrentBusiStatus = args.CurrentBusiStatus;//03,批转,01,删除,02,注销

            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            //派遣部门编号
            pt.targetdepart = args.TargetDepartCode;

            pt.buttoncode = args.ButtonCode;
            if (string.IsNullOrEmpty(args.TargetDepartCode))
            {
                //pt.DepartCode = args.DepartCode;
                //pt.targetdepart = args.TargetDepartCode;
            }
            if (pt.CurrentBusiStatus == "01")
            {
                pt.CurrentNodeId = args.NodeId; //TODO  以后要优化改代码，不需要传入该参数。
                pt.actionname = "案件删除";
                bacgBL.business.Project.DelProject(pt);
            }
            else if (pt.CurrentBusiStatus == "02")
            {
                pt.CurrentNodeId = args.NodeId; //TODO  以后要优化改代码，不需要传入该参数。
                pt.actionname = "问题注销";
                bacgBL.business.Project.CancelProject(pt, ref strErr);
            }
            else if (pt.CurrentBusiStatus == "03")
            {
                bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            }
            else if (pt.CurrentBusiStatus == "04")
            {
                pt.returntracetag = "1";
                bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            }
            if (strErr != "")
            {
                LoggerManager.Instance.logger.Error("案件批转异常：" + strErr);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 值班长案卷立案
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectFiling(ProjectFilingArgs args)
        {
            bacgBL.web.szbase.organize.DepartManage bll = new bacgBL.web.szbase.organize.DepartManage();
            bacgDL.business.ProjectInfo pi = new bacgDL.business.ProjectInfo();
            pi.projcode = args.ProjectCode;
            //增加处理类型的时间  UP BY steve
            pi.ProcessType = args.ProcessType;

            //chkIsInArea “0”区处理 “1”市职能部门 “2”其它职能部门
            //pi.isgreat = this.chkIsInArea.SelectedValue;
            //pi.isgreat = this.chkIsGreat.Checked ? "2" : pi.isgreat;

            //“0”区处理 “1”市职能部门 “2”其它职能部门
            //pi.withDept = this.chkIsInArea.SelectedValue;

            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "6";
            pt.CurrentBusiStatus = "03";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.ButtonCode;

            if (!string.IsNullOrEmpty(args.TargetDepartCode))
            {
                pt.targetdepart = args.TargetDepartCode;
            }
            //获取处置时间
            DataTable dt = bacgBL.business.Project.GetTypeList(args.ProjectTypeCode, args.SmallClassCode);
            if (string.IsNullOrEmpty(args.ProcessType))
            {
                pi.departTime = dt.Select("detailclass=" + args.FilingType)[0]["time1"].ToString();
                pi.departTimeType = dt.Select("detailclass=" + args.FilingType)[0]["type1"].ToString();
            }
            else
            {
                pi.departTime = dt.Select("detailclass=" + args.FilingType)[0]["time" + (int.Parse(args.ProcessType) + 1).ToString()].ToString();
                pi.departTimeType = dt.Select("detailclass=" + args.FilingType)[0]["type" + (int.Parse(args.ProcessType) + 1).ToString()].ToString();
            }
            pi.ProcessType = args.ProcessType;

            //     bacgBL.business.Project.Project_LA(pi, pt, out strErr);
            bacgBL.business.Project.Project_LA(pi, pt, "", "", out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.Error("案件立案批转异常：" + strErr);
                return false;
            }

            ////手机发送
            string strMobile = args.Mobile;
            if (strMobile != "" & strMobile != null) //如果不未空,那么发送短信
            {
                // add by yaoch 2012-10-29,二级平台案件，按照二级编号发送短信!
                String code = args.ProjectCode;
                String info = bacgBL.business.Project.GetProjectCode2ByCode(code);

                if (info != null)
                {
                    code = info;
                }
                string strContent = string.Format("您部门有新任务，请登陆系统查收！案件编号：{0}", code);

                //20081205日修改发短息的方法
                //int strResult = bacgBL.Pub.MsgClass.ShortMessage("", strMobile, strContent);
                //int strResult = bacgBL.business.Message.SendMsgtoCols(strMobile, strContent);
                int strResult = bacgBL.Pub.TxMsgClass.ShortMessage(strMobile, strContent);
                if (strResult == 1)
                {
                    LoggerManager.Instance.logger.InfoFormat("审核处理成功 但是在给责任单位发送通知短信失败!");
                }
            }

            return true;
        }

        /// <summary>
        /// 值班长立案回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectFilingBack(ProjectFilingArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "6";
            pt.CurrentBusiStatus = "09";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "1";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("值班长立案回退异常：" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 值班长立案删除
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectFilingDelete(ProjectFilingArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.projcode = args.ProjectCode;
            pt.actionname = "值班长立案删除";
            pt.PreNodeId = "6";
            pt.CurrentNodeId = "6";
            pt.CurrentBusiStatus = "07";
            pt.usercode = args.UserCode;
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.DelProjectAndInsertTrace(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("值班长立案删除异常：" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 案卷派遣
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectDispatch(ProjectDispatchArgs args)
        {
            string strErr = "";
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.targetdepart = args.TargetDepartCode;
            if (args.strPQNode == "8")//职能部门派遣
            {
                pt.PreNodeId = "8";
                pt.CurrentBusiStatus = "01";
            }
            else if (args.strPQNode == "7")//派遣员派遣
            {
                pt.PreNodeId = "7";
                pt.CurrentBusiStatus = "00";
            }
            pt.DepartCode = args.DepartCode;
            pt.roleid = args.TargetDepartCode; //派遣对象。
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.ButtonCode;
            bacgBL.business.Project.Project_PQ(args.TargetDepartCode, args.SuperviseName, args.SuperviseContent, pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷派遣异常：" + strErr);
                return false;
            }

            //派遣多部门
            if (!string.IsNullOrEmpty(args.TargetDepartCode))
            {
                bacgBL.business.Project.Project_DBMPQ(pt.projcode, out strErr);
            }
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷多部门派遣异常：" + strErr);
                return false;
            }

            if (args.IsAcceptNote == "1" && !string.IsNullOrEmpty(args.Mobile)) //派遣的应处理部门有手机号码,那么发送短信
            {
                // add by yaoch 2012-10-29,二级平台案件，按照二级编号发送短信!

                String info = bacgBL.business.Project.GetProjectCode2ByCode(args.ProjectCode);

                if (info != null)
                {
                    args.ProjectCode = info;
                }

                string strContent = string.Format("您部门有新任务，请登陆系统查收！案件编号：{0}", args.ProjectCode);

                //20081205日修改发短息的方法
                //add by yaoch 2012-10-24
                //int strResult = bacgBL.Pub.MsgClass.ShortMessage("",strMobile,strContent);

                int strResult = bacgBL.Pub.TxMsgClass.ShortMessage(args.Mobile, strContent);

                //string strResult = bacgBL.business.Message.SendMsgtoCol(strMobile, strContent);
                if (strResult == 1)
                {
                    LoggerManager.Instance.logger.InfoFormat("派遣成功，但是在给责任单位发送通知短信失败！");

                }
            }
            return true;
        }

        /// <summary>
        /// 案卷结果反馈
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectDispathRevert(ProjectDispatchArgs args)
        {
            string strErr = "";
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            if (args.strPQNode == "1")
            {
                pt.PreNodeId = "8";
                pt.CurrentBusiStatus = "00";
            }
            else if (args.strPQNode == "2")
            {
                pt.PreNodeId = "8";
                pt.CurrentBusiStatus = "02";
            }
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.actionname = "结果反馈";
            pt.returntracetag = "0";
            //modi by yaoch on 2012-10-16
            pt.buttoncode = args.ButtonCode == "11100039019" ? "11100034019" : args.ButtonCode;
            bacgBL.business.Project.Project_FK(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷结果反馈失败:错误信息:{0}", strErr);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 案卷派遣回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectDispatchBack(ProjectDispatchArgs args)
        {

            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            if (args.strPQNode == "7")//派遣员回退
            {
                pt.PreNodeId = "7";
                pt.CurrentBusiStatus = "09";
            }
            else if (args.strPQNode == "8")//专业部门回退
            {
                pt.PreNodeId = "8";
                pt.CurrentBusiStatus = "03";
            }
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "1";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);

            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷派遣回退异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 案卷任务核查
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectCheck(ProjectApprovedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "9";
            pt.CurrentBusiStatus = "00";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷任务核查异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 案卷任务核查回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectCheckBack(ProjectApprovedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "9";
            pt.CurrentBusiStatus = "09";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "1";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷任务核查回退异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送核查指令
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectSendCheckMessage(ProjectCheckArgs args)
        {
            bacgBL.business.Project.InsertPdaMsg(args.ProjectCode, args.UserCode, args.DepartCode, args.CollectorCode, args.Message, "1", "0", "1", out strErr, args.ButtonCode);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("发送核查指令异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 核查通过
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectCheckSuccess(ProjectApprovedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "102";
            pt.CurrentBusiStatus = "01";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.ButtonCode;

            bacgBL.business.Project.ChangeProjectFlowNode(pt, out strErr);

            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("核查通过异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 核查不通过
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectCheckFailed(ProjectApprovedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "102";
            pt.CurrentBusiStatus = "02";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "1";
            pt.buttoncode = args.ButtonCode;
            string strErr = "";
            bacgBL.business.Project.Project_BTG(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.InfoFormat("核查不通过异常:" + strErr);
                //this.MessageBox(string.Format("操作出错，请重试！\\n{0}", strErr));
                return false;
            }

            // add steve 手机发送短信 //
            string strMobile = bacgBL.business.Project.GetMobile(pt);  // this.txt_phone.Text.Trim();
            if (strMobile != "" & strMobile != null) //如果不未空,那么发送短信
            {
                string strContent = string.Format("您部门有新任务，请登陆系统查收！案件编号：{0}", args.ProjectCode);

                //20081205日修改发短息的方法  up by steve  ///
                int strResult = bacgBL.Pub.TxMsgClass.ShortMessage(strMobile, strContent);
                if (strResult == 1)
                {
                    LoggerManager.Instance.logger.InfoFormat("审核处理成功，但是在给责任单位发送通知短信失败！");
                }
            }
            return true;
        }

        /// <summary>
        /// 案卷结案
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectClosed(ProjectClosedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "11";
            pt.CurrentBusiStatus = "00";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "0";
            pt.buttoncode = args.Option;
            pt.ffs = args.IsRepeatProject;
            bacgBL.business.Project.Project_JA(args.StartYear, pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷结案异常:" + strErr);
                //this.MessageBox(string.Format("操作出错，请重试！\\n{0}", strErr));
                return false;
            }

            //TODO  发布版本时，记得打开下面代码 -----begin
            //与市局发生过交换的案件，比如：由市局批转到桐乡的案件；由桐乡派遣到市局职能部门的案件。结案时，从桐乡交换到市局去
            if (!string.IsNullOrEmpty(args.SJProjectCode))
            {
                strErr = "";
                bacgBL.business.Project.Project_JA_ToSJ(args.ProjectCode, args.StartYear, out strErr);
                if (strErr != "")
                {
                    LoggerManager.Instance.logger.ErrorFormat("与市局发生过交换的案件异常:" + strErr);
                    //this.MessageBox(string.Format("案件编号：{0}，年份：{1}\\n系统把结案后的案件从桐乡交换到市局数据库时出现错误！\\n请通知系统管理员手工交换该记录。\\n以下错误信息供系统管理员排查问题：\\n{2}", this.txt_id.Text, this.txt_StartYear.Value, strErr));
                }
            }
            return true;
        }

        /// <summary>
        /// 案卷结案回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        public bool ProjectClosedBack(ProjectClosedArgs args)
        {
            bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
            pt.usercode = args.UserCode;
            pt.projcode = args.ProjectCode;
            pt.PreNodeId = "11";
            pt.CurrentBusiStatus = "09";
            pt.DepartCode = args.DepartCode;
            pt._opinion = args.Option;
            pt.returntracetag = "1";
            pt.buttoncode = args.ButtonCode;

            string strErr = "";
            bacgBL.business.Project.Project_JAHT(pt, out strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("案卷结案回退异常:" + strErr);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 还原案卷
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="userCode">操作人</param>
        /// <param name="departCode">操作人所属部门</param>
        /// <param name="deleteSign">删除标志（0：删除案卷 1：归档案卷）</param>
        /// <returns></returns>
        public bool ProjectRollBack(string projcode, string userCode, string departCode, string deleteSign = "0")
        {
            if (deleteSign == "0")
            {
                bacgDL.business.ProjectTraceInfo pt = new bacgDL.business.ProjectTraceInfo();
                pt.projcode = projcode;
                pt.actionname = "回收站还原案件";
                pt.PreNodeId = "";
                pt.CurrentNodeId = "";
                pt.CurrentBusiStatus = "";
                pt.usercode = userCode;
                pt.DepartCode = departCode;
                pt._opinion = "还原案件";
                pt.returntracetag = "0";

                string strErr = "";
                bacgBL.business.Project.RollBackProject(pt, out strErr);
                if (strErr != "")
                {
                    LoggerManager.Instance.logger.ErrorFormat("还原案卷异常 案卷编号:{0} 错误信息:{1}", projcode, strErr);
                    return false;
                }
            }
            else if (deleteSign == "1")
            {
                string strErr = "";
                bacgBL.business.Project.ReturnProject(projcode, out strErr);
                if (strErr != "")
                {
                    LoggerManager.Instance.logger.ErrorFormat("还原案卷异常 案卷编号:{0} 错误信息:{1}", projcode, strErr);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 物理删除案卷信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="deleteSign">删除标志（0：删除案卷 1：归档案卷）</param>
        /// <returns></returns>
        public bool ProjectDelete(string projcode, string deleteSign = "0")
        {
            bacgBL.business.Project.DeleteProject(projcode, deleteSign, out strErr);

            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("物理删除案卷异常 案卷编号:{0} 错误信息:{1}", projcode, strErr);
                return false;
            }
            return true;
        }

        #endregion

        #region 公用方法

        //得到当前页面所在的流程
        protected string GetPageWorkFlowType(string strButId)
        {
            bacgBL.szbase.workflow.workFlow flow = new bacgBL.szbase.workflow.workFlow();
            DataSet list = flow.GetPageFlowInfo(strButId);

            if (list.Tables.Count != 0)
            {
                return list.Tables[0].Rows[0]["flowid"].ToString();
            }
            else
            {
                return "";
            }
        }

        protected string GetChangeFlowButtonId(string strFlowType)
        {
            bacgBL.szbase.workflow.workFlow flow = new bacgBL.szbase.workflow.workFlow();
            DataSet list = flow.GetChangeFlowButtonId(strFlowType);

            if (list.Tables.Count != 0)
            {
                return list.Tables[0].Rows[0]["buttoncode"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取核查标志信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        public string GetIoFlag(string projcode)
        {
            string rtn = bacgBL.business.Project.GetIoFlag(projcode);
            if (!string.IsNullOrEmpty(rtn))
            {
                if (rtn.Split('$')[0] == "-9")
                {
                    LoggerManager.Instance.logger.Error("获取核查标志信息异常：" + strErr);
                    return string.Empty;
                }
                else
                {
                    return rtn;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 获取处置时间和处置时间类型
        /// </summary>
        /// <param name="typecode">案卷事部件类型</param>
        /// <param name="smallcode">小类编码</param>
        /// <param name="typeVaue">立案类型</param>
        /// <param name="processtype">处理类型</param>
        /// <returns></returns>
        public string GetHandleTime(string typecode, string smallcode, string typeVaue, string processtype)
        {
            DataTable dt = bacgBL.business.Project.GetTypeList(typecode, smallcode);
            processtype = (int.Parse(processtype) + 1).ToString();
            string departTime = dt.Select("detailclass=" + typeVaue)[0]["time" + processtype].ToString();
            string departTimeType = dt.Select("detailclass=" + typeVaue)[0]["type" + processtype].ToString();
            if (departTimeType == "0")
                departTime = departTime + "工作时";
            if (departTimeType == "1")
                departTime = departTime + "紧急工作时";
            if (departTimeType == "2")
                departTime = departTime + "工作日";
            if (departTimeType == "3")
                departTime = departTime + "紧急工作日";

            return departTime + "$" + departTimeType;
        }

        /// <summary>
        /// 获取案件的核查结果 
        /// </summary>
        /// <param name="projectcode">案卷编号</param>
        /// <returns></returns>
        public string GetProjHcResult(string projectcode)
        {
            return bacgBL.business.Project.GetProjHcResult(projectcode, out strErr); //监督员核查结果
        }

        #endregion

        #endregion

        #region  [ 区域相关 ]

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAreaList()
        {
            List<Area> list = new List<Area>();
            DataSet ds = bacgBL.business.Project.GetAreatList(string.Empty, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取区域列表异常:" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Area>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 获取街道列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <returns></returns>
        public List<Street> GetStreetList(string areaCode)
        {
            List<Street> list = new List<Street>();
            DataSet ds = bacgBL.business.Project.GetStreetList(areaCode, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取街道列表异常:" + strErr);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Street>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 获取社区列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="streetCode">街道编码</param>
        /// <returns></returns>
        public List<Community> GetCommunityList(string areaCode, string streetCode)
        {
            List<Community> list = new List<Community>();
            DataSet ds = bacgBL.business.Project.GetCommList(areaCode, streetCode, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取社区列表异常:" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Community>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        #endregion

        #region [ 大小类 ]

        /// <summary>
        /// 获取案卷大类列表
        /// </summary>
        /// <param name="eventType">类型（0：部件 1：事件）</param>
        /// <returns></returns>
        public List<ProjectBigClass> GetBigClassList(string classType)
        {
            string strErr = string.Empty;
            List<ProjectBigClass> list = new List<ProjectBigClass>();

            DataSet ds = bacgBL.business.Project.GetBigClassList(classType, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取案卷大类列表异常：" + strErr);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                list = ConvertDtHelper<ProjectBigClass>.GetModelList(dt.Rows);
            }
            return list;
        }

        /// <summary>
        /// 获取案卷小类列表
        /// </summary>
        /// <param name="eventType">类型（0：部件 1：事件）</param>
        /// <returns></returns>
        public List<ProjectSmallClass> GetSmallClassList(string classType, string bigclassCode)
        {
            string strErr = string.Empty;

            List<ProjectSmallClass> list = new List<ProjectSmallClass>();

            DataSet ds = bacgBL.business.Project.GetSmallClassList(classType, bigclassCode, out strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取案卷小类列表异常：" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                list = ConvertDtHelper<ProjectSmallClass>.GetModelList(dt.Rows);
            }
            return list;
        }

        /// <summary>
        /// 添加或修改事件大类
        /// </summary>
        /// <param name="bigclass">大类实体</param>
        /// <returns></returns>
        public ReturnValue InsertEvent(ProjectBigClass bigclass)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            EventDepartManage bl = new EventDepartManage();

            if (string.IsNullOrEmpty(bigclass.BigClassCode))
            {
                rtn.ErrorMsg = "事件编号不能为空";
                rtn.ReturnState = false;
                return rtn;
            }
            if (!checkEventName(bigclass.Name, "名称").ReturnState)
            {
                return rtn;
            }
            if (bigclass.BigClassCode.Length > 2)
            {
                rtn.ErrorMsg = "大类事件编号不能超过二位.";
                rtn.ReturnState = false;
                return rtn;
            }
            if (!Teamax.Common.PublicClass.IsValidInt(bigclass.BigClassCode))
            {
                rtn.ErrorMsg = "事件编码必需是数字.";
                rtn.ReturnState = false;
                return rtn;
            }
            int temp = bl.CheckEvent(bigclass.Name, ref strErr);
            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp > 0)
            {
                rtn.ErrorMsg = "事件名称不能重复!";
                rtn.ReturnState = false;
                return rtn;
            }
            if (!string.IsNullOrEmpty(bigclass.Id))
            {
                temp = bl.UpateEvent(bigclass.Id, bigclass.BigClassCode, bigclass.Name, ref strErr);
            }
            else
            {
                temp = bl.InsertEvent(bigclass.BigClassCode, bigclass.Name, ref strErr);
            }

            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp <= 0)
            {
                rtn.ErrorMsg = "事件打类更新失败！";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 添加或修改事件小类
        /// </summary>
        /// <param name="smallclass">小类实体</param>
        /// <returns></returns>
        public ReturnValue InsertEventSmallClass(ProjectSmallClass smallclass)
        {

            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            EventDepartManage bl = new EventDepartManage();

            rtn = checkEventName(smallclass.Name, "名称");

            if (!rtn.ReturnState)
            {
                return rtn;
            }

            if (string.IsNullOrEmpty(smallclass.Dutyunit))
            {
                rtn.ErrorMsg = "责任单位不能为空!";
                rtn.ReturnState = false;
                return rtn;
            }

            if (string.IsNullOrEmpty(smallclass.SmallCallCode))
            {
                rtn.ErrorMsg = "事件编号不能为空!";
                rtn.ReturnState = false;
                return rtn;
            }

            if (!Teamax.Common.PublicClass.IsValidInt(smallclass.Name))
            {
                rtn.ErrorMsg = "事件编码必需是数字!";
                rtn.ReturnState = false;
                return rtn;
            }
            if (smallclass.SmallCallCode.Length != 4)
            {
                rtn.ErrorMsg = "小类事件编码的长度必须是四位数字!";
                rtn.ReturnState = false;
                return rtn;
            }

            //TODO
            //if (!smallclass.Name.Trim().Equals(Server.UrlDecode(Request["smallname"])))
            if (!smallclass.Name.Trim().Equals(""))
            {
                int temp = bl.CheckSmallEvent(smallclass.Name, ref strErr);
                if (strErr != "")
                {
                    rtn.ErrorMsg = strErr;
                    rtn.ReturnState = false;
                    return rtn;
                }
                if (temp > 1)
                {
                    rtn.ErrorMsg = "事件名称不能重复!";
                    rtn.ReturnState = false;
                    return rtn;
                }
                temp = bl.CheckSmallCode(smallclass.SmallCallCode, ref strErr);
                if (strErr != "")
                {
                    rtn.ErrorMsg = strErr;
                    rtn.ReturnState = false;
                    return rtn;
                }
                if (temp > 1)
                {
                    rtn.ErrorMsg = "事件编码不能重复!";
                    rtn.ReturnState = false;
                    return rtn;
                }
            }

            rtn = checkEventCode(smallclass.BigClassCode.ToString(), smallclass.SmallCallCode, "");

            if (!rtn.ReturnState)
            {
                return rtn;
            }

            if (!string.IsNullOrEmpty(smallclass.Id))
            {
                bl.InsertSmallEvent(int.Parse(smallclass.BigClassCode), smallclass.SmallCallCode, smallclass.Name, 0, smallclass.Dutyunit, smallclass.T1Name, smallclass.T1Time, smallclass.T1Time_2, smallclass.T1Time_3, smallclass.T1Time_4, smallclass.T2Name, smallclass.T2Time, smallclass.T2Time_2, smallclass.T2Time_3, smallclass.T2Time_4, smallclass.T3Name, smallclass.T3Time, smallclass.T3Time_2, smallclass.T3Time_3, smallclass.T3Time_4, smallclass.T4Name, smallclass.T4Time, smallclass.T4Time_2, smallclass.T4Time_3, smallclass.T4Time_4,
                                                        smallclass.T5Name, smallclass.T5Time, smallclass.T5Time_2, smallclass.T5Time_3, smallclass.T5Time_4, smallclass.T6Name, smallclass.T6Time, smallclass.T6Time_2, smallclass.T6Time_3, smallclass.T6Time_4, smallclass.T7Name, smallclass.T7Time, smallclass.T7Time_2, smallclass.T7Time_3, smallclass.T7Time_4, smallclass.T8Name, smallclass.T8Time, smallclass.T8Time_2, smallclass.T8Time_3, smallclass.T8Time_4, smallclass.T9Name, smallclass.T9Time, smallclass.T9Time_2, smallclass.T9Time_3, smallclass.T9Time_4,
                                                        smallclass.T10Name, smallclass.T10Time, smallclass.T10Time_2, smallclass.T10Time_3, smallclass.T10Time_4, smallclass.T1Type, smallclass.T1Type_2, smallclass.T1Type_3, smallclass.T1Type_4, smallclass.T2Type, smallclass.T2Type_2, smallclass.T2Type_3, smallclass.T2Type_4, smallclass.T3Type, smallclass.T3Type_2, smallclass.T3Type_3, smallclass.T3Type_4, smallclass.T4Type, smallclass.T4Type_2, smallclass.T4Type_3, smallclass.T4Type_4, smallclass.T5Type, smallclass.T5Type_2, smallclass.T5Type_3,
                                                        smallclass.T5Type_4, smallclass.T6Type, smallclass.T6Type_2, smallclass.T6Type_3, smallclass.T6Type_4, smallclass.T7Type, smallclass.T7Type_2, smallclass.T7Type_3, smallclass.T7Type_4, smallclass.T8Type, smallclass.T8Type_2, smallclass.T8Type_3, smallclass.T8Type_4, smallclass.T9Type, smallclass.T9Type_2, smallclass.T9Type_3, smallclass.T9Type_4, smallclass.T10Type, smallclass.T10Type_2, smallclass.T10Type_3, smallclass.T10Type_4, ref strErr);
            }
            else
            {
                bl.UpateSmallEvent(smallclass.Id, smallclass.SmallCallCode, smallclass.BigClassCode, smallclass.Name, 0, smallclass.Dutyunit, smallclass.T_Time_Kc, 0, smallclass.T1Name, smallclass.T1Time, smallclass.T1Time_2, smallclass.T1Time_3, smallclass.T1Time_4, smallclass.T2Name, smallclass.T2Time, smallclass.T2Time_2, smallclass.T2Time_3, smallclass.T2Time_4, smallclass.T3Name, smallclass.T3Time, smallclass.T3Time_2, smallclass.T3Time_3, smallclass.T3Time_4, smallclass.T4Name, smallclass.T4Time, smallclass.T4Time_2, smallclass.T4Time_3, smallclass.T4Time_4,
                                                       smallclass.T5Name, smallclass.T5Time, smallclass.T5Time_2, smallclass.T5Time_3, smallclass.T5Time_4, smallclass.T6Name, smallclass.T6Time, smallclass.T6Time_2, smallclass.T6Time_3, smallclass.T6Time_4, smallclass.T7Name, smallclass.T7Time, smallclass.T7Time_2, smallclass.T7Time_3, smallclass.T7Time_4, smallclass.T8Name, smallclass.T8Time, smallclass.T8Time_2, smallclass.T8Time_3, smallclass.T8Time_4, smallclass.T9Name, smallclass.T9Time, smallclass.T9Time_2, smallclass.T9Time_3, smallclass.T9Time_4,
                                                       smallclass.T10Name, smallclass.T10Time, smallclass.T10Time_2, smallclass.T10Time_3, smallclass.T10Time_4, smallclass.T1Type, smallclass.T1Type_2, smallclass.T1Type_3, smallclass.T1Type_4, smallclass.T2Type, smallclass.T2Type_2, smallclass.T2Type_3, smallclass.T2Type_4, smallclass.T3Type, smallclass.T3Type_2, smallclass.T3Type_3, smallclass.T3Type_4, smallclass.T4Type, smallclass.T4Type_2, smallclass.T4Type_3, smallclass.T4Type_4, smallclass.T5Type, smallclass.T5Type_2, smallclass.T5Type_3,
                                                       smallclass.T5Type_4, smallclass.T6Type, smallclass.T6Type_2, smallclass.T6Type_3, smallclass.T6Type_4, smallclass.T7Type, smallclass.T7Type_2, smallclass.T7Type_3, smallclass.T7Type_4, smallclass.T8Type, smallclass.T8Type_2, smallclass.T8Type_3, smallclass.T8Type_4, smallclass.T9Type, smallclass.T9Type_2, smallclass.T9Type_3, smallclass.T9Type_4, smallclass.T10Type, smallclass.T10Type_2, smallclass.T10Type_3, smallclass.T10Type_4, ref strErr);
            }
            if (strErr != "")
            {
                rtn.ReturnState = false;
                rtn.ErrorMsg = "更新小类失败！";
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 添加或修改部件大类
        /// </summary>
        /// <param name="bigclass">大类实体</param>
        /// <returns></returns>
        public ReturnValue InsertPart(ProjectBigClass bigclass)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            EventDepartManage bl = new EventDepartManage();
            rtn = checkEventName(bigclass.Name, "名称");
            if (!rtn.ReturnState)
            {
                return rtn;
            }
            if (string.IsNullOrEmpty(bigclass.BigClassCode))
            {
                rtn.ErrorMsg = "部件编码不能为空.";
                rtn.ReturnState = false;
                return rtn;
            }
            if (!Teamax.Common.PublicClass.IsValidInt(bigclass.BigClassCode))
            {
                rtn.ErrorMsg = "部件编码必需是数字.";
                rtn.ReturnState = false;
                return rtn;
            }
            if (bigclass.BigClassCode.Length != 2)
            {
                rtn.ErrorMsg = "大类部件编码的长度必须二位.";
                rtn.ReturnState = false;
                return rtn;
            }
            int temp = bl.CheckPart(bigclass.BigClassCode, ref strErr);
            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp > 0)
            {
                rtn.ErrorMsg = "部件名称不能重复.";
                rtn.ReturnState = false;
                return rtn;
            }

            temp = bl.CheckPartCode(bigclass.BigClassCode, ref strErr);
            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp > 0)
            {
                rtn.ErrorMsg = "部件编号不能重复";
                rtn.ReturnState = false;
                return rtn;
            }
            if (!string.IsNullOrEmpty(bigclass.Id))
            {
                temp = bl.InsertPart(bigclass.BigClassCode, bigclass.Name, ref strErr);
            }
            else
            {
                temp = bl.UpatePart(bigclass.Id, bigclass.BigClassCode, bigclass.Name, ref strErr);
            }

            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp <= 0)
            {
                rtn.ErrorMsg = "部件大类更新失败！";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 添加或修改部件小类
        /// </summary>
        /// <param name="smallclass">小类实体</param>
        /// <returns></returns>
        public ReturnValue InsertPartSmallClass(ProjectSmallClass smallclass)
        {

            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            EventDepartManage bl = new EventDepartManage();

            rtn = checkEventName(smallclass.Name, "名称");

            if (!rtn.ReturnState)
            {
                return rtn;
            }

            //if (string.IsNullOrEmpty(smallclass.Dutyunit))
            //{
            //    rtn.ErrorMsg = "责任单位不能为空!";
            //    rtn.ReturnState = false;
            //    return rtn;
            //}

            if (string.IsNullOrEmpty(smallclass.SmallCallCode))
            {
                rtn.ErrorMsg = "部件编号不能为空!";
                rtn.ReturnState = false;
                return rtn;
            }

            if (!Teamax.Common.PublicClass.IsValidInt(smallclass.Name))
            {
                rtn.ErrorMsg = "部件编码必需是数字!";
                rtn.ReturnState = false;
                return rtn;
            }
            if (smallclass.SmallCallCode.Length != 4)
            {
                rtn.ErrorMsg = "小类部件编码的长度必须是四位数字!";
                rtn.ReturnState = false;
                return rtn;
            }

            //TODO
            //if (!smallclass.Name.Trim().Equals(Server.UrlDecode(Request["smallname"])))
            if (!smallclass.Name.Trim().Equals(""))
            {
                int temp = bl.CheckPart(smallclass.Name, ref strErr);
                if (strErr != "")
                {
                    rtn.ErrorMsg = strErr;
                    rtn.ReturnState = false;
                    return rtn;
                }
                if (temp > 1)
                {
                    rtn.ErrorMsg = "部件名称不能重复!";
                    rtn.ReturnState = false;
                    return rtn;
                }
                temp = bl.CheckSmallPartCode(smallclass.SmallCallCode, ref strErr);
                if (strErr != "")
                {
                    rtn.ErrorMsg = strErr;
                    rtn.ReturnState = false;
                    return rtn;
                }
                if (temp > 1)
                {
                    rtn.ErrorMsg = "部件编码不能重复!";
                    rtn.ReturnState = false;
                    return rtn;
                }
            }

            rtn = checkEventCode(smallclass.BigClassCode.ToString(), smallclass.SmallCallCode, "");

            if (!rtn.ReturnState)
            {
                return rtn;
            }

            if (!string.IsNullOrEmpty(smallclass.Id))
            {
                bl.InsertSmallPart(int.Parse(smallclass.BigClassCode), smallclass.SmallCallCode, smallclass.Name, 0, smallclass.Dutyunit, string.Empty,
                   smallclass.T1Name, smallclass.T1Time,
                   smallclass.T2Name, smallclass.T2Time,
                   smallclass.T3Name, smallclass.T3Time,
                   smallclass.T4Name, smallclass.T4Time,
                   smallclass.T5Name, smallclass.T5Time,
                   smallclass.T6Name, smallclass.T6Time,
                   smallclass.T7Name, smallclass.T7Time,
                   smallclass.T8Name, smallclass.T8Time,
                   smallclass.T9Name, smallclass.T9Time,
                   smallclass.T10Name, smallclass.T10Time,
                   smallclass.T1Time_2, smallclass.T1Time_3, smallclass.T1Time_4,
                   smallclass.T2Time_2, smallclass.T2Time_3, smallclass.T2Time_4,
                   smallclass.T3Time_2, smallclass.T3Time_3, smallclass.T3Time_4,
                   smallclass.T4Time_2, smallclass.T4Time_3, smallclass.T4Time_4,
                   smallclass.T5Time_2, smallclass.T5Time_3, smallclass.T5Time_4,
                   smallclass.T6Time_2, smallclass.T6Time_3, smallclass.T6Time_4,
                   smallclass.T7Time_2, smallclass.T7Time_3, smallclass.T7Time_4,
                   smallclass.T8Time_2, smallclass.T8Time_3, smallclass.T8Time_4,
                   smallclass.T9Time_2, smallclass.T9Time_3, smallclass.T9Time_4,
                   smallclass.T10Time_2, smallclass.T10Time_3, smallclass.T10Time_4,
                   smallclass.T1Type, smallclass.T1Type_2, smallclass.T1Type_3, smallclass.T1Type_4,
                   smallclass.T2Type, smallclass.T2Type_2, smallclass.T2Type_3, smallclass.T2Type_4,
                   smallclass.T3Type, smallclass.T3Type_2, smallclass.T3Type_3, smallclass.T3Type_4,
                   smallclass.T4Type, smallclass.T4Type_2, smallclass.T4Type_3, smallclass.T4Type_4,
                   smallclass.T5Type, smallclass.T5Type_2, smallclass.T5Type_3, smallclass.T5Type_4,
                   smallclass.T6Type, smallclass.T6Type_2, smallclass.T6Type_3, smallclass.T6Type_4,
                   smallclass.T7Type, smallclass.T7Type_2, smallclass.T7Type_3, smallclass.T7Type_4,
                   smallclass.T8Type, smallclass.T8Type_2, smallclass.T8Type_3, smallclass.T8Type_4,
                   smallclass.T9Type, smallclass.T9Type_2, smallclass.T9Type_3, smallclass.T9Type_4,
                   smallclass.T10Type, smallclass.T10Type_2, smallclass.T10Type_3, smallclass.T10Type_4, ref strErr);
            }
            else
            {
                bl.UpateSmallPart(int.Parse(smallclass.Id), smallclass.SmallCallCode, smallclass.Name, 0, 0, smallclass.Dutyunit, string.Empty, 0, 0, 0,
                   smallclass.T1Name, smallclass.T1Time, smallclass.T1Time_2, smallclass.T1Time_3, smallclass.T1Time_4,
                   smallclass.T2Name, smallclass.T2Time, smallclass.T2Time_2, smallclass.T2Time_3, smallclass.T2Time_4,
                   smallclass.T3Name, smallclass.T3Time, smallclass.T3Time_2, smallclass.T3Time_3, smallclass.T3Time_4,
                   smallclass.T4Name, smallclass.T4Time, smallclass.T4Time_2, smallclass.T4Time_3, smallclass.T4Time_4,
                   smallclass.T5Name, smallclass.T5Time, smallclass.T5Time_2, smallclass.T5Time_3, smallclass.T5Time_4,
                   smallclass.T6Name, smallclass.T6Time, smallclass.T6Time_2, smallclass.T6Time_3, smallclass.T6Time_4,
                   smallclass.T7Name, smallclass.T7Time, smallclass.T7Time_2, smallclass.T7Time_3, smallclass.T7Time_4,
                   smallclass.T8Name, smallclass.T8Time, smallclass.T8Time_2, smallclass.T8Time_3, smallclass.T8Time_4,
                   smallclass.T9Name, smallclass.T9Time, smallclass.T9Time_2, smallclass.T9Time_3, smallclass.T9Time_4,
                   smallclass.T10Name, smallclass.T10Time, smallclass.T10Time_2, smallclass.T10Time_3, smallclass.T10Time_4,
                   smallclass.T1Type, smallclass.T1Type_2, smallclass.T1Type_3, smallclass.T1Type_4,
                   smallclass.T2Type, smallclass.T2Type_2, smallclass.T2Type_3, smallclass.T2Type_4,
                   smallclass.T3Type, smallclass.T3Type_2, smallclass.T3Type_3, smallclass.T3Type_4,
                   smallclass.T4Type, smallclass.T4Type_2, smallclass.T4Type_3, smallclass.T4Type_4,
                   smallclass.T5Type, smallclass.T5Type_2, smallclass.T5Type_3, smallclass.T5Type_4,
                   smallclass.T6Type, smallclass.T6Type_2, smallclass.T6Type_3, smallclass.T6Type_4,
                   smallclass.T7Type, smallclass.T7Type_2, smallclass.T7Type_3, smallclass.T7Type_4,
                   smallclass.T8Type, smallclass.T8Type_2, smallclass.T8Type_3, smallclass.T8Type_4,
                   smallclass.T9Type, smallclass.T9Type_2, smallclass.T9Type_3, smallclass.T9Type_4,
                   smallclass.T10Type, smallclass.T10Type_2, smallclass.T10Type_3, smallclass.T10Type_4, ref strErr);

            }
            if (strErr != "")
            {
                rtn.ReturnState = false;
                rtn.ErrorMsg = "更新小类失败！";
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 获取小类事部件处理类型列表
        /// </summary>
        /// <param name="typecode">标识是事件还是部件</param>
        /// <param name="smallcode">小类事部件编码</param>
        /// <returns></returns>
        public List<ProjectClassType> GetTypeList(string typecode, string smallcode)
        {
            List<ProjectClassType> list = new List<ProjectClassType>();

            DataTable dt = bacgBL.business.Project.GetTypeList(typecode, smallcode);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectClassType type = new ProjectClassType()
                    {
                        TypeName = SqlDataConverter.ToString(dr["typename"]),
                        TypeCode = SqlDataConverter.ToString(dr["detailclass"])
                    };
                    list.Add(type);
                }
            }
            return list;
        }



        #region checkEventName:验证输入的名称是否合法
        /// <summary>
        /// 验证输入的大类名称是否合法
        /// </summary>
        /// <param name="name">需要验证的字符串</param>
        /// <param name="text">名称</param>
        /// <returns></returns>
        public ReturnValue checkEventName(string name, string text)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };
            if (name.Length == 0)
            {
                rtn.ErrorMsg = "名称不能为空!!";
                rtn.ReturnState = false;
                return rtn;
            }
            if (name.Length > 64)
            {
                rtn.ErrorMsg = "名称长度不能超过64个字符!!";
                rtn.ReturnState = false;
                return rtn;
            }
            if (name.IndexOf(",") >= 0 || name.IndexOf(";") >= 0 || name.IndexOf("@") >= 0)
            {
                rtn.ErrorMsg = text + "中不能含有 , ; @ 等特殊字符!";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }
        #endregion

        #region checkEventCode:验证输入事件编码是否合法
        /// <summary>
        /// 验证输入事件编码是否合法
        /// </summary>
        /// <param name="name">需要验证的字符串</param>
        /// <param name="text">名称</param>
        /// <returns></returns>
        public ReturnValue checkEventCode(string bigclassCode, string name, string text)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            string bigcode = bigclassCode;
            if (bigcode.Length > 0)
            {
                string temp = name.Substring(0, bigcode.Length);
                if (bigcode != temp)
                {
                    rtn.ErrorMsg = "小类事部件编码必须以大类事件编码" + bigcode + "开头.";
                    rtn.ReturnState = false;
                    return rtn;
                }
                else
                {
                    return rtn;
                }
            }
            return rtn;
        }
        #endregion

        #endregion

    }
}
