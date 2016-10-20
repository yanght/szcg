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
        ReturnValue GetDealProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

        /// <summary>
        /// 获取自办件案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="departCode">职能部门编码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        ReturnValue GetZbjProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string departCode, string startTime, string endTime);

        /// <summary>
        /// 获取存档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        ReturnValue GetCDProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

        /// <summary>
        /// 获取待反馈案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        ReturnValue GetWaitFeedBackProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

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
        ReturnValue GetDBProjectList(string leader, string projcode, string startTime, string endTime, string areacode, PageInfo pageInfo);

        /// <summary>
        /// 查询箱查询案卷列表
        /// </summary>
        /// <param name="args">查询参数</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        ReturnValue QueryProjectList(ProjectQueryArgs args, PageInfo pageInfo);

        /// <summary>
        /// 查询归档案卷列表
        /// </summary>
        /// <param name="projectInfo">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        ReturnValue GetGDProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime);

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
        ReturnValue GetDeleteProjectList(ProjectInfo projectInfo, PageInfo pageInfo, string startTime, string endTime, string userName, int deleteTimeType);

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
        ///  从业务数据库中获取案件的部分主要信息 
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="nodeid">案卷节点Id</param>
        /// <returns></returns>
        Project GetProjectSomeInfo(string projcode, string nodeid);


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
        bool ProjectAcceptApproved(ProjectApprovedArgs args);

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

        /// <summary>
        /// 还原案卷
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="userCode">操作人</param>
        /// <param name="departCode">操作人所属部门</param>
        /// <param name="deleteSign">删除标志（0：删除案卷 1：归档案卷）</param>
        /// <returns></returns>
        bool ProjectRollBack(string projcode, string userCode, string departCode, string deleteSign = "1");

        /// <summary>
        /// 物理删除案卷信息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
        /// <param name="deleteSign">删除标志（0：删除案卷 1：归档案卷）</param>
        /// <returns></returns>
        bool ProjectDelete(string projcode, string deleteSign = "0");

         /// <summary>
        /// 获取处置时间和处置时间类型
        /// </summary>
        /// <param name="typecode">案卷事部件类型</param>
        /// <param name="smallcode">小类编码</param>
        /// <param name="typeVaue">立案类型</param>
        /// <param name="processtype">处理类型</param>
        /// <returns></returns>
        string GetHandleTime(string typecode, string smallcode, string typeVaue, string processtype);

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

        /// <summary>
        /// 获取小类事部件处理类型列表
        /// </summary>
        /// <param name="typecode">标识是事件还是部件</param>
        /// <param name="smallcode">小类事部件编码</param>
        /// <returns></returns>
        List<ProjectClassType> GetTypeList(string typecode, string smallcode);

        #endregion

    }
}
