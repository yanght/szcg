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

        #region 案卷相关

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<Project> GetDealProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            List<Model.Project> list = new List<Project>();

            string Error = string.Empty;

            DataSet ds = bacgBL.business.Project.GetDealProjectList(projectInfo, startTime, endTime, pageInfo, out Error);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取待办案件列表异常：" + strErr);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                list = ConvertDtHelper<Project>.GetModelList(ds.Tables[0]);
            }

            return list;
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

            return project;
        }

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
        /// 添加事件大类
        /// </summary>
        /// <param name="bigclass"></param>
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

            temp = bl.InsertEvent(bigclass.BigClassCode, bigclass.Name, ref strErr);
            if (strErr != "")
            {
                rtn.ErrorMsg = strErr;
                rtn.ReturnState = false;
                return rtn;
            }
            if (temp <= 0)
            {
                rtn.ErrorMsg = "添加失败！";
                rtn.ReturnState = false;
                return rtn;
            }
            return rtn;
        }

        /// <summary>
        /// 添加事件小类
        /// </summary>
        /// <param name="smallclass"></param>
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

            rtn = checkEventCode(smallclass.BigClassCode, smallclass.SmallCallCode, "");

            if (!rtn.ReturnState)
            {
                return rtn;
            }


            bl.InsertSmallEvent(smallclass.BigClassCode, smallclass.SmallCallCode, smallclass.Name, 0, smallclass.Dutyunit, smallclass.T1Name, smallclass.T1Time, smallclass.T1Time_2, smallclass.T1Time_3, smallclass.T1Time_4, smallclass.T2Name, smallclass.T2Time, smallclass.T2Time_2, smallclass.T2Time_3, smallclass.T2Time_4, smallclass.T3Name, smallclass.T3Time, smallclass.T3Time_2, smallclass.T3Time_3, smallclass.T3Time_4, smallclass.T4Name, smallclass.T4Time, smallclass.T4Time_2, smallclass.T4Time_3, smallclass.T4Time_4,
                                                    t5name, t5time, t5time_2, t5time_3, t5time_4, t6name, t6time, t6time_2, t6time_3, t6time_4, t7name, t7time, t7time_2, t7time_3, t7time_4, t8name, t8time, t8time_2, t8time_3, t8time_4, t9name, t9time, t9time_2, t9time_3, t9time_4,
                                                    t10name, t10time, t10time_2, t10time_3, t10time_4, t1type, t1type_2, t1type_3, t1type_4, t2type, t2type_2, t2type_3, t2type_4, t3type, t3type_2, t3type_3, t3type_4, t4type, t4type_2, t4type_3, t4type_4, t5type, t5type_2, t5type_3,
                                                    t5type_4, t6type, t6type_2, t6type_3, t6type_4, t7type, t7type_2, t7type_3, t7type_4, t8type, t8type_2, t8type_3, t8type_4, t9type, t9type_2, t9type_3, t9type_4, t10type, t10type_2, t10type_3, t10type_4, ref strErr);


            return rtn;
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
                    rtn.ErrorMsg = "小类事件编码必须以大类事件编码" + bigcode + "开头.";
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
            prj.square = project.StreetId;
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

        /// <summary>
        /// 获取案卷流程
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="year">案卷年份</param>
        /// <param name="isend">是否已结案</param>
        /// <returns></returns>

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
        public bool ProjectApproved(ProjectApprovedArgs args)
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

            return false;
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

        #endregion

        #endregion

        #region  区域相关

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

    }
}
