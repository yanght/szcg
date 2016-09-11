using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Text;


namespace DBbase.pub
{
    public class ReturnValue
    {
        public bool State { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public PageInfo Page { get; set; }
        public object Rtn { get; set; }
    }

    /// <summary>
    /// 领导通用户
    /// </summary>
    public class LUser
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DepartCode { get; set; }
        public string DepartName { get; set; }
        public string LoginName { get; set; }
    }


    /// <summary>
    /// 违章案卷
    /// </summary>
    public class LProject
    {
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 案卷名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 案卷描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public DateTime ReportTime { get; set; }
        /// <summary>
        /// 举报人姓名
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string Resource { get; set; }
        /// <summary>
        /// 案卷位置（经纬度坐标）
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 案卷领导批示信息
        /// </summary>
        public List<ProjectSupervise> ProjectSupervis { get; set; }

    }

    /// <summary>
    ///每月案卷数量
    /// </summary>
    public class LProjectCount
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 案卷数量
        /// </summary>
        public int Count { get; set; }
    }

    public class LProjectRank
    {
        public string UserName { get; set; }
        public int Count { get; set; }
    }

    public class ProjectSupervise
    {
        /// <summary>
        /// 批示Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 批示人Id
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 批示内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 批示人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 批示时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 批示状态（0：已删除 1：批示中 2：已批示）
        /// </summary>
        public string State { get; set; }
    }

    /// <summary>
    /// 案卷流程
    /// </summary>
    public class ProjectTrace
    {
        /// <summary>
        /// 经办人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 受理部门
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public string FlowName { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 受理时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 办理阶段
        /// </summary>
        public string HandlerTime { get; set; }
    }

    public class ProjectFile
    {
        public string FilePath { get; set; }
        public string FileState { get; set; }
    }
}
