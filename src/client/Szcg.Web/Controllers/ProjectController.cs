using bacgDL.business;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;
using Teamax.Common;
using Szcg.Web;
using Szcg.Web.Models;

namespace Szcg.Web.Controllers
{
    public class ProjectController : BaseController
    {
        IProjectService svc = new ProjectService();

        //
        // GET: /CallAcceptance/Project/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 案卷流程 ]

        #region [ 案卷上报 ]

        [HttpPost]
        public AjaxFxRspJson ProjectReport(Szcg.Service.Model.Project project)
        {
            AjaxFxRspJson ajax = ValidateProject(project);

            if (project.NodeId == 0)
            {
                project.NodeId = 2;
            }

            if (ajax.RspCode == 0) return ajax;

            string projcode = svc.ProjectReport(project, UserInfo);

            if (string.IsNullOrEmpty(projcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷上报失败！";
                return ajax;
            }

            ajax.RspData.Add("projcode", JToken.FromObject(projcode));

            return ajax;
        }

        #endregion

        #region [ 案卷办理 ]

        [HttpPost]
        public AjaxFxRspJson ProjectHandler(Szcg.Service.Model.Project project)
        {
            AjaxFxRspJson ajax = ValidateProject(project);

            if (ajax.RspCode == 0) return ajax;

            if (!svc.ProjectHandler(project, UserInfo))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷办理失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 案卷批转 ]

        [HttpPost]
        public AjaxFxRspJson ProjectApproved(Szcg.Service.Model.Project project)
        {
            AjaxFxRspJson ajax = ValidateProject(project);

            if (ajax.RspCode == 0) return ajax;

            if (!svc.ProjectApproved(project, UserInfo))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷批转失败！";
                return ajax;
            }
            return ajax;
        }

        #endregion

        #region [ 受理员案卷批转 ]

        [HttpPost]
        public AjaxFxRspJson ProjectAcceptApproved(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            if (string.IsNullOrEmpty(args.Option))
            {
                ajax.RspCode = 0;
                if (args.CurrentBusiStatus == "02")
                {
                    ajax.RspMsg = "请输入注销意见！";
                }
                if (args.CurrentBusiStatus == "03")
                {
                    ajax.RspMsg = "请输入批转意见！";
                }
                if (args.CurrentBusiStatus == "04")
                {
                    ajax.RspMsg = "请输入回退意见！";
                }

                return ajax;
            }


            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();


            if (!svc.ProjectAcceptApproved(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "受理员案卷批转失败！";
                return ajax;
            }
            return ajax;
        }

        #endregion

        #region [ 值班长案卷立案 ]

        [HttpPost]
        public AjaxFxRspJson ProjectFiling(ProjectFilingArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(args.Option))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入批转意见！";
                return ajax;
            }

            if (!string.IsNullOrEmpty(args.Mobile) && !PublicClass.IsValidMobil(args.Mobile))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入正确的手机号码！";
                return ajax;
            }

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectFiling(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷立案失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 值班长案卷立案回退 ]

        [HttpPost]
        public AjaxFxRspJson ProjectFilingBack(ProjectFilingArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(args.Option))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入批转意见！";
                return ajax;
            }

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectFilingBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷立案回退失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 值班长案卷立案删除 ]

        [HttpPost]
        public AjaxFxRspJson ProjectFilingDelete(ProjectFilingArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(args.Option))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入批转意见！";
                return ajax;
            }

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectFilingDelete(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷立案删除失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 责任单位案卷派遣 ]

        [HttpPost]
        public AjaxFxRspJson ProjectDispatch(ProjectDispatchArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(args.TargetDepartCode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请选择部门！";
                return ajax;
            }

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectDispatch(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷派遣失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 责任单位案卷派遣回退 ]

        [HttpPost]
        public AjaxFxRspJson ProjectDispatchBack(ProjectDispatchArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectDispatchBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷派遣回退失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 责任单位案卷结果反馈 ]

        [HttpPost]
        public AjaxFxRspJson ProjectDispatchRevert(ProjectDispatchArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectDispatchBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷派遣回退失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 指挥中心任务核查 ]

        [HttpPost]
        public AjaxFxRspJson ProjectCheck(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectCheck(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷任务核查失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 指挥中心任务核查回退 ]

        [HttpPost]
        public AjaxFxRspJson ProjectCheckBack(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectCheckBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "任务核查回退失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 发送任务核查指令 ]

        [HttpPost]
        public AjaxFxRspJson ProjectSendCheckMessage(ProjectCheckArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (string.IsNullOrEmpty(args.CollectorCode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请选择监督员！";
                return ajax;
            }

            if (!svc.ProjectSendCheckMessage(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "发送核查指令失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 核查通过 ]

        [HttpPost]
        public AjaxFxRspJson ProjectCheckSuccess(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            string ioFlag = svc.GetIoFlag(args.ProjectCode);
            if (string.IsNullOrEmpty(ioFlag))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "获取核查标志信息异常，请通知系统管理员！";
                return ajax;
            }
            if (!string.IsNullOrEmpty(ioFlag) && ioFlag != "20" && ioFlag != "21" && ioFlag != "31" && ioFlag != "99")
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "此案卷没有核查完，不能处理！";
                return ajax;
            }

            if (!svc.ProjectCheckSuccess(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "核查通过操作失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 核查不通过 ]

        [HttpPost]
        public AjaxFxRspJson ProjectCheckFailed(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            string ioFlag = svc.GetIoFlag(args.ProjectCode);
            if (string.IsNullOrEmpty(ioFlag))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "获取核查标志信息异常，请通知系统管理员！";
                return ajax;
            }
            if (!string.IsNullOrEmpty(ioFlag) && ioFlag != "20" && ioFlag != "21" && ioFlag != "31" && ioFlag != "99")
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "此案卷没有核查完，不能处理！";
                return ajax;
            }

            if (!svc.ProjectCheckFailed(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "核查不通过操作失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 案卷结案 ]

        [HttpPost]
        public AjaxFxRspJson ProjectClosed(ProjectClosedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectClosed(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷结案失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 结案回退 ]

        [HttpPost]
        public AjaxFxRspJson ProjectClosedBack(ProjectClosedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            args.UserCode = UserInfo.getUsercode().ToString();
            args.DepartCode = UserInfo.getDepartcode().ToString();

            if (!svc.ProjectClosedBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷结案回退失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 回收站案卷还原 ]

        public AjaxFxRspJson ProjectRollBack(string projcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.ProjectRollBack(projcode, this.UserInfo.getUsercode().ToString(), this.UserInfo.getDepartcode().ToString());

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷还原失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 还原问题归档案卷 ]

        public AjaxFxRspJson RollBackGDProject(string projcode, string startdate)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.RollBackGDProject(projcode, this.UserInfo.getUsercode().ToString(), this.UserInfo.getDepartcode().ToString(), startdate);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "问题归档案卷还原失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 删除问题归档案卷 ]

        public AjaxFxRspJson DeleteGDProject(string projcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.DeleteGDProject(projcode);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "问题归档案卷删除失败！";
                return ajax;
            }

            return ajax;

        }

        #endregion

        #region [ 案卷物理删除 ]

        public AjaxFxRspJson ProjectDelete(string projcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool rtn = svc.ProjectDelete(projcode);

            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷删除失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #endregion

        #region [ 获取案卷信息 ]

        #region [ 获取案卷流程 ]

        public AjaxFxRspJson ProjectTrace(string projcode, int year, bool isEnd)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(projcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入案卷编号！";
                return ajax;
            }

            List<ProjectTrace> list = svc.GetProjectTrace(projcode, year, isEnd);

            ajax.RspData.Add("projectTrace", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获取待办案卷列表 ]

        /// <summary>
        /// 获取代办案卷列表
        /// </summary>
        /// <param name="projInfo">案卷查询信息</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public JsonResult GetDelProjectList(Project project, PageInfo pageInfo, string startTime, string endTime)
        {

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            ProjectInfo projInfo = new ProjectInfo()
            {
                areacode = project.AreaId,
                street = project.StreetId,
                square = project.SquareId,
                strUserCode = UserInfo.getUsercode().ToString(),
                TargetDepartCode = UserInfo.getDepartcode().ToString(),
                NodeId = project.NodeId.ToString(),
                strButtonId = project.ButtonCode,
                projcode = project.Projcode
            };
            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";

            ReturnValue rtn = svc.GetDealProjectList(projInfo, pageInfo, startTime, endTime);
            List<Szcg.Service.Model.Project> list = new List<Project>();
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;

                ajax.RspData.Add("data", JToken.FromObject(list));
                ajax.RspData.Add("draw", Request["draw"]);
                ajax.RspData.Add("recordsTotal", pageInfo.RowCount);
                ajax.RspData.Add("recordsFiltered", pageInfo.RowCount);
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取自办件案卷列表 ]

        /// <summary>
        /// 获取自办件案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="departCode">职能部门编码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public JsonResult GetZbjProjectList(Project project, PageInfo pageInfo, string Depart, string startTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            ProjectInfo projInfo = new ProjectInfo()
            {
                TelephonistCode = UserInfo.getUsercode().ToString(),
                projcode = project.Projcode
            };

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";

            ReturnValue rtn = svc.GetZbjProjectList(projInfo, pageInfo, Depart == "null" ? "" : Depart, startTime, endTime);
            List<Szcg.Service.Model.Project> list = new List<Project>();
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                ajax.RspData.Add("data", JToken.FromObject(list));
                ajax.RspData.Add("draw", Request["draw"]);
                ajax.RspData.Add("recordsTotal", pageInfo.RowCount);
                ajax.RspData.Add("recordsFiltered", pageInfo.RowCount);
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Project>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取存档案卷列表 ]

        /// <summary>
        /// 获取存档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public JsonResult GetCDProjectList(Project project, PageInfo pageInfo, string startTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            ProjectInfo projInfo = new ProjectInfo()
            {
                TelephonistCode = UserInfo.getUsercode().ToString(),
                projcode = project.Projcode
            };

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";


            ReturnValue rtn = svc.GetCDProjectList(projInfo, pageInfo, startTime, endTime);
            List<Szcg.Service.Model.Project> list = new List<Project>();
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                //ajax.RspData.Add("projectList", JToken.FromObject(list));
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Project>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取待反馈案卷列表 ]

        public AjaxFxRspJson GetWaitFeedBackProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.GetWaitFeedBackProjectList(projectInfo, pageInfo, startTime, endTime);

            if (rtn.ReturnState)
            {
                List<Szcg.Service.Model.Project> list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                ajax.RspData.Add("projectList", JToken.FromObject(list));
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取督办案卷列表 ]

        public AjaxFxRspJson GetDBProjectList(string leader, string projcode, string startTime, string endTime, string areacode, PageInfo pageInfo)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.GetDBProjectList(leader, projcode, startTime, endTime, areacode, pageInfo);

            if (rtn.ReturnState)
            {
                List<Szcg.Service.Model.Project> list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                ajax.RspData.Add("projectList", JToken.FromObject(list));
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取查询箱案卷列表 ]

        public JsonResult QueryProjectList(ProjectQueryArgs args, PageInfo pageInfo)
        {
            if (args.AreaId == "全部") args.AreaId = "";
            if (args.StreetId == "全部") args.StreetId = "";
            if (args.SquareId == "全部") args.SquareId = "";

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";

            ReturnValue rtn = svc.QueryProjectList(args, pageInfo);

            List<Szcg.Service.Model.Project> list = new List<Project>();
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                //ajax.RspData.Add("projectList", JToken.FromObject(list));
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Project>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取归档案卷列表 ]

        public JsonResult GetGDProjectList(Project project, PageInfo pageInfo, string startTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }


            ProjectInfo projInfo = new ProjectInfo()
            {
                street = project.StreetId,
                projcode = project.Projcode
            };

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";

            ReturnValue rtn = svc.GetGDProjectList(projInfo, pageInfo, startTime, endTime);
            List<Szcg.Service.Model.Project> list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
                //ajax.RspData.Add("projectList", JToken.FromObject(list));
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Project>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取回收站案卷列表 ]

        public JsonResult GetDeleteProjectList(Project project, PageInfo pageInfo, string startTime, string endTime, string userName, int deleteTimeType)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            ProjectInfo projInfo = new ProjectInfo()
            {
                street = project.StreetId,
                projcode = project.Projcode
               
            };

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "projcode";
            pageInfo.Order = "desc";

            ReturnValue rtn = svc.GetDeleteProjectList(projInfo, pageInfo, startTime, endTime, userName, deleteTimeType);

            List<Szcg.Service.Model.Project> list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.Project>;
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Project>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取监督员已核查案卷 ]

        public JsonResult GetCheckProjectList(string StreetId, string LoginName, string CollName, string Mobile, string startTime, string endTime, string hcFlag)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "案卷号";
            pageInfo.Order = "asc";

            ReturnValue rtn = svc.GetCheckProjectList(StreetId, LoginName, CollName, Mobile, startTime, endTime, hcFlag, UserInfo.getHcpower(), pageInfo);

            List<Szcg.Service.Model.CollecterProject> list = new List<CollecterProject>();

            if (rtn.ReturnState)
            {
                list = rtn.ReturnObj as List<Szcg.Service.Model.CollecterProject>;
            }
            else
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return Json(ajax);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<CollecterProject>() : list }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取案卷核查状态 ]

        public AjaxFxRspJson GetIoFlag(string projcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            string result = svc.GetIoFlag(projcode);

            ajax.RspData.Add("data", JToken.FromObject(result));

            return ajax;
        }

        #endregion

        #endregion

        #region [ 获取案卷详细 ]

        public AjaxFxRspJson GetProjectDetail(string projcode, int year, bool isend)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(projcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入案卷编号！";
                return ajax;
            }

            Szcg.Service.Model.Project project = svc.GetProjectDetail(projcode, year, isend);


            ajax.RspData.Add("project", JToken.FromObject(project));
            if (Request["action"] != null)
            {
                ajax.RspData.Add("action", JToken.FromObject(Request["action"]));
            }
            if (Request["buttoncode"] != null)
            {
                ajax.RspData.Add("buttoncode", JToken.FromObject(Request["buttoncode"]));
            }

            return ajax;
        }

        public AjaxFxRspJson GetProjectDetailWithCollecter(string dotype, string projcode, string nodeid)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };


            if (string.IsNullOrEmpty(projcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入案卷编号！";
                return ajax;
            }

            Szcg.Service.Model.Project project = svc.GetProjectSomeInfo(projcode, nodeid);

            List<ProjectClassType> list = svc.GetTypeList(project.TypeCode, project.SmallClass);

            List<Collecter> list1 = new Collecterservice().GetCollecters(project.StreetId, projcode);



            project.HandlerTime = svc.GetHandleTime(project.TypeCode, project.SmallClass, list[0].TypeCode, "0").Split('$')[0];
            ajax.RspData.Add("usercode", JToken.FromObject(UserInfo.getUsercode()));
            ajax.RspData.Add("username", JToken.FromObject(UserInfo.getUsername()));
            ajax.RspData.Add("project", JToken.FromObject(project));
            ajax.RspData.Add("typelist", JToken.FromObject(list));
            ajax.RspData.Add("dotype", JToken.FromObject(dotype));
            ajax.RspData.Add("collecters", JToken.FromObject(list1));
            ajax.RspData.Add("reccontent", JToken.FromObject(svc.GetProjHcResult(projcode)));
            return ajax;
        }

        public AjaxFxRspJson GetProjectDetailLA(string dotype, string projcode, string nodeid)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };


            if (string.IsNullOrEmpty(projcode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入案卷编号！";
                return ajax;
            }

            Szcg.Service.Model.Project project = svc.GetProjectSomeInfo(projcode, nodeid);

            List<ProjectClassType> list = svc.GetTypeList(project.TypeCode, project.SmallClass);

            project.HandlerTime = svc.GetHandleTime(project.TypeCode, project.SmallClass, list[0].TypeCode, "0").Split('$')[0];
            ajax.RspData.Add("usercode", JToken.FromObject(UserInfo.getUsercode()));
            ajax.RspData.Add("username", JToken.FromObject(UserInfo.getUsername()));
            ajax.RspData.Add("project", JToken.FromObject(project));
            ajax.RspData.Add("typelist", JToken.FromObject(list));
            ajax.RspData.Add("dotype", JToken.FromObject(dotype));

            return ajax;
        }

        public AjaxFxRspJson GetProjectHandleTime(string typecode, string smallcode, string filingType, string processtype)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            string time = svc.GetHandleTime(typecode, smallcode, filingType, processtype);

            ajax.RspData.Add("time", JToken.FromObject(time));

            return ajax;
        }

        #endregion

        #region [ 区域相关 ]

        public AjaxFxRspJson GetAreaTree()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<TreeModel> treeModel = new List<TreeModel>();

            List<Area> list_Area = svc.GetAreaList();

            foreach (Area area in list_Area)
            {
                if (area.AreaName == "全部") continue;

                treeModel.Add(new TreeModel() { id = area.AreaCode, name = area.AreaName, pId = "0", open = true ,tag=area.Id});
                List<Street> list_Street = svc.GetStreetList(area.AreaCode);

                foreach (Street street in list_Street)
                {
                    if (street.StreetName == "全部") continue;
                    treeModel.Add(new TreeModel() { id = street.StreetCode, name = street.StreetName, pId = area.AreaCode, tag=street.Id });
                    List<Community> list_Community = svc.GetCommunityList(area.AreaCode, street.StreetCode);

                    foreach (Community community in list_Community)
                    {
                        if (community.CommName == "全部") continue;
                        treeModel.Add(new TreeModel() { id = community.CommCode, name = community.CommName, pId = street.StreetCode ,tag=street.Id});
                    }
                }

                // 
            }
            ajax.RspData.Add("areaTree", JToken.FromObject(treeModel));
            return ajax;
        }


        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public AjaxFxRspJson GetAreaList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Area> list = svc.GetAreaList();

            ajax.RspData.Add("areas", JToken.FromObject(list));

            return ajax;
        }

        /// <summary>
        /// 获取街道列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public AjaxFxRspJson GetStreetList(string areacode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Street> list = svc.GetStreetList(areacode);

            ajax.RspData.Add("streets", JToken.FromObject(list));

            return ajax;
        }

        /// <summary>
        /// 获取社区列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="streetcode">街道编码</param>
        /// <returns></returns>
        public AjaxFxRspJson GetCommunityList(string areacode, string streetcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Community> list = svc.GetCommunityList(areacode, streetcode);

            ajax.RspData.Add("commoditys", JToken.FromObject(list));

            return ajax;
        }


        #endregion

        #region [ 大小类 ]

        #region [ 获取案卷大类列表 ]

        /// <summary>
        /// 获取案卷大类列表
        /// </summary>
        /// <param name="classType">类型（0：部件 1：事件）</param>
        /// <returns></returns>
        public AjaxFxRspJson GetBigClassList(string classType)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (classType != "0" && classType != "1")
            {
                ajax.RspMsg = "请输入正确的类型！";
                ajax.RspCode = 0;
                return ajax;
            }

            List<ProjectBigClass> bigclass = svc.GetBigClassList(classType);

            ajax.RspData.Add("bigclass", JToken.FromObject(bigclass));

            return ajax;
        }

        #endregion

        #region [ 获取案卷小类列表 ]

        /// <summary>
        /// 获取案卷小类列表
        /// </summary>
        /// <param name="classType">类型（0：部件 1：事件）</param>
        /// <param name="bigclassCode">大类编码</param>
        /// <returns></returns>
        public AjaxFxRspJson GetSmallClassList(string classType, string bigclassCode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<ProjectSmallClass> smallclass = svc.GetSmallClassList(classType, bigclassCode);

            ajax.RspData.Add("smallclass", JToken.FromObject(smallclass));

            return ajax;
        }

        #endregion

        #region [ 获取大小类属性结构 ]

        public AjaxFxRspJson GetProjectClassTree()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<TreeModel> treemodel = new List<TreeModel>();

            treemodel.Add(new TreeModel() { id = "0", pId = "-1", name = "部件", open = true });
            treemodel.Add(new TreeModel() { id = "1", pId = "-1", name = "事件", open = true });

            List<ProjectBigClass> bigclass_part = svc.GetBigClassList("0");
            List<ProjectBigClass> bigclass_event = svc.GetBigClassList("1");

            foreach (var item in bigclass_part)
            {
                if (item.Name != "全部")
                {
                    treemodel.Add(new TreeModel() { id = item.BigClassCode + "_" + 0, pId = "0", name = item.Name });
                    List<ProjectSmallClass> smallclass = svc.GetSmallClassList("0", item.BigClassCode);
                    foreach (var s_class in smallclass)
                    {
                        if (s_class.Name != "全部")
                        {
                            treemodel.Add(new TreeModel() { id = s_class.SmallCallCode, pId = item.BigClassCode + "_" + 0, name = s_class.Name });
                        }
                    }
                }

            }


            foreach (var item in bigclass_event)
            {
                if (item.Name != "全部")
                {
                    treemodel.Add(new TreeModel() { id = item.BigClassCode + "_" + 1, pId = "1", name = item.Name });
                    List<ProjectSmallClass> smallclass = svc.GetSmallClassList("1", item.BigClassCode);
                    foreach (var s_class in smallclass)
                    {
                        if (s_class.Name != "全部")
                        {
                            treemodel.Add(new TreeModel() { id = s_class.SmallCallCode, pId = item.BigClassCode + "_" + 1, name = s_class.Name });
                        }
                    }
                }
            }

            ajax.RspData.Add("projectclass", JToken.FromObject(treemodel));

            return ajax;
        }

        #endregion

        #region [ 添加或修改事件大类 ]

        public AjaxFxRspJson InsertEvent(ProjectBigClass bigclass)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.InsertEvent(bigclass);

            if (!rtn.ReturnState)
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }
            return ajax;
        }

        #endregion

        #region [ 添加或修改部件大类 ]

        public AjaxFxRspJson InsertPart(ProjectBigClass bigclass)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.InsertPart(bigclass);

            if (!rtn.ReturnState)
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }
            return ajax;
        }

        #endregion

        #region [ 添加或修改事件小类 ]

        public AjaxFxRspJson InsertEventSmallClass(ProjectSmallClass smallclass)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.InsertEventSmallClass(smallclass);

            if (!rtn.ReturnState)
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }
            return ajax;

        }

        #endregion

        #region [ 添加或修改部件小类 ]

        public AjaxFxRspJson InsertPartSmallClass(ProjectSmallClass smallclass)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            ReturnValue rtn = svc.InsertPartSmallClass(smallclass);

            if (!rtn.ReturnState)
            {
                ajax.RspMsg = rtn.ErrorMsg;
                ajax.RspCode = 0;
                return ajax;
            }
            return ajax;

        }

        #endregion

        #endregion

        /// <summary>
        /// 验证案卷上报信息
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public AjaxFxRspJson ValidateProject(Szcg.Service.Model.Project project)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(project.BigClass))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入问题大类！";
                return ajax;
            }
            if (string.IsNullOrEmpty(project.SmallClass))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入问题小类！";
                return ajax;
            }
            if (string.IsNullOrEmpty(project.StreetId))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入一个街道或获取街道！";
                return ajax;
            }
            if (string.IsNullOrEmpty(project.Address))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入事发位置！";
                return ajax;
            }
            if (string.IsNullOrEmpty(project.ProbDesc))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入问题情况描述！";
                return ajax;
            }

            return ajax;
        }

    }
}
