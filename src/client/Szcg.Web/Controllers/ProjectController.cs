﻿using bacgDL.business;
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
        public AjaxFxRspJson ProjectApproved(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            if (string.IsNullOrEmpty(args.Option))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入批转意见！";
                return ajax;
            }

            if (!svc.ProjectApproved(args))
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

            if (!string.IsNullOrEmpty(args.Mobile) && PublicClass.IsValidMobil(args.Mobile))
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

        #region [ 指挥中心案卷派遣 ]

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

        #region [ 指挥中心案卷派遣回退 ]

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

        #region [ 指挥中心任务核查 ]

        [HttpPost]
        public AjaxFxRspJson ProjectCheck(ProjectApprovedArgs args)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (!svc.ProjectCheck(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷任务核失败！";
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
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 0 };

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

            if (!svc.ProjectClosedBack(args))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "案卷结案回退失败！";
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
        public AjaxFxRspJson GetDelProjectList(ProjectInfo projInfo, PageInfo pageInfo, string startTime, string endTime)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Szcg.Service.Model.Project> list = svc.GetDealProjectList(projInfo, pageInfo, startTime, endTime);

            ajax.RspData.Add("projectList", JToken.FromObject(list));

            return ajax;
        }

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

            return ajax;
        }

        #endregion

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

        #endregion

        #region [ 区域相关 ]

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
