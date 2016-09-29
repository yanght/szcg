using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using szcg.com.teamax.business.entity;
using Szcg.Service.Model;
using Project = Szcg.Service.Model.Project;


namespace Szcg.Service.IBussiness
{
    public interface IProjectService
    {
        #region [ 案卷相关 ]

        /// <summary>
        /// 获取待办案件列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        List<Project> GetDealProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

        /// <summary>
        /// 获取自办件案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="departCode">职能部门编码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        List<Project> GetZbjProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string departCode, string startTime, string endTime);

        /// <summary>
        /// 获取存档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        List<Project> GetCDProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

        /// <summary>
        /// 获取案件信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="year">年份</param>
        /// <param name="isEnd">是否已结案</param>
        /// <param name="nodeId">节点Id</param>
        /// <returns></returns>
        Project GetProjectDetail(string projcode, int year, bool isEnd);

        /// <summary>
        /// 获取案卷流程
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="year">年份</param>
        /// <param name="isEnd">是否已结案</param>
        /// <returns></returns>
        List<ProjectTrace> GetProjectTrace(string projcode, int year, bool isEnd);

        /// <summary>
        /// 新增案卷
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        string AddProject(Project project, UserInfo userInfo);

        /// <summary>
        /// 修改案卷
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        bool EditProject(Project project, UserInfo userInfo);


        /// <summary>
        /// 获取核查标志信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <returns></returns>
        string GetIoFlag(string projcode);

        #region 案卷批转

        /// <summary>
        /// 案卷上报
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        string ProjectReport(Project project, UserInfo userInfo);

        /// <summary>
        /// 案卷办理
        /// </summary>
        /// <param name="project">案卷详细</param>  
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        bool ProjectHandler(Project project, UserInfo userInfo);

        /// <summary>
        /// 案卷批转
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        bool ProjectApproved(Project project, UserInfo userInfo);

        /// <summary>
        /// 案卷核实
        /// </summary>
        /// <param name="project">案卷详细</param>
        /// <param name="userInfo">当前操作用户</param>
        /// <returns></returns>
        bool ProjectVerify(Project project, UserInfo userInfo, Collecter collecte);

        /// <summary>
        /// 受理员案卷批转
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectApproved(ProjectApprovedArgs args);

        /// <summary>
        /// 值班长案卷立案
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectFiling(ProjectFilingArgs args);

        /// <summary>
        /// 值班长立案回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectFilingBack(ProjectFilingArgs args);

        /// <summary>
        /// 值班长立案删除
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectFilingDelete(ProjectFilingArgs args);

        /// <summary>
        /// 案卷派遣
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectDispatch(ProjectDispatchArgs args);

        /// <summary>
        /// 案卷派遣回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectDispatchBack(ProjectDispatchArgs args);

        /// <summary>
        /// 案卷任务核查
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectCheck(ProjectApprovedArgs args);

        /// <summary>
        /// 案卷任务核查回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectCheckBack(ProjectApprovedArgs args);

        /// <summary>
        /// 发送核查指令
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectSendCheckMessage(ProjectCheckArgs args);

        /// <summary>
        /// 核查通过
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectCheckSuccess(ProjectApprovedArgs args);

        /// <summary>
        /// 核查不通过
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectCheckFailed(ProjectApprovedArgs args);

        /// <summary>
        /// 案卷结案
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectClosed(ProjectClosedArgs args);

        /// <summary>
        /// 案卷结案回退
        /// </summary>
        /// <param name="args">批转参数</param>
        /// <returns></returns>
        bool ProjectClosedBack(ProjectClosedArgs args);

        #endregion

        #endregion

        #region [ 区域相关 ]

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        List<Area> GetAreaList();

        /// <summary>
        /// 获取街道列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <returns></returns>
        List<Street> GetStreetList(string areaCode);

        /// <summary>
        /// 获取社区列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="streetCode">街道编码</param>
        /// <returns></returns>
        List<Community> GetCommunityList(string areaCode, string streetCode);

        #endregion

        #region [ 大小类 ]

        /// <summary>
        /// 获取案卷大类列表
        /// </summary>
        /// <param name="eventType">类型（0：部件 1：事件）</param>
        /// <returns></returns>
        List<ProjectBigClass> GetBigClassList(string classType);

        /// <summary>
        /// 获取案卷小类列表
        /// </summary>
        /// <param name="eventType">类型（0：部件 1：事件）</param>
        /// <returns></returns>
        List<ProjectSmallClass> GetSmallClassList(string classType, string bigclassCode);


        /// <summary>
        /// 添加或修改事件大类
        /// </summary>
        /// <param name="bigclass">大类实体</param>
        /// <returns></returns>
        ReturnValue InsertEvent(ProjectBigClass bigclass);

        /// <summary>
        /// 添加或修改事件小类
        /// </summary>
        /// <param name="smallclass">小类实体</param>
        /// <returns></returns>
        ReturnValue InsertEventSmallClass(ProjectSmallClass smallclass);

        /// <summary>
        /// 添加或修改部件大类
        /// </summary>
        /// <param name="bigclass">大类实体</param>
        /// <returns></returns>
        ReturnValue InsertPart(ProjectBigClass bigclass);

        /// <summary>
        /// 添加或修改部件小类
        /// </summary>
        /// <param name="smallclass">小类实体</param>
        /// <returns></returns>
        ReturnValue InsertPartSmallClass(ProjectSmallClass smallclass);

        #endregion

    }
}
