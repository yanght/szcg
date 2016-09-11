using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{

    /// <summary>
    /// 案卷批转参数
    /// </summary>
    public class ProjectApprovedArgs
    {
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 当前操作用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 当前节点Id
        /// </summary>
        public string NodeId { get; set; }
        public string ButtonCode { get; set; }
        /// <summary>
        /// 当前操作步骤(01:删除,02:注销,03:批转,)
        /// </summary>
        public string CurrentBusiStatus { get; set; }
        /// <summary>
        /// 当前操作用户部门编号
        /// </summary>
        public string DepartCode { get; set; }
        /// <summary>
        /// 派遣部门编号
        /// </summary>
        public string TargetDepartCode { get; set; }
        /// <summary>
        /// 处理类型
        /// </summary>
        public string ProcessType { get; set; }
        /// <summary>
        /// 批转意见
        /// </summary>
        public string Option { get; set; }
    }

    /// <summary>
    /// 案卷立案参数
    /// </summary>
    public class ProjectFilingArgs : ProjectApprovedArgs
    {

        /// <summary>
        /// 案卷类型（1：事件 0：部件）
        /// </summary>
        public string ProjectTypeCode { get; set; }
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string ProjectSource { get; set; }
        /// <summary>
        /// 案卷小类编码
        /// </summary>
        public string SmallClassCode { get; set; }
        /// <summary>
        /// 立案类型
        /// </summary>
        public string FilingType { get; set; }
        /// <summary>
        /// 部门手机号码
        /// </summary>
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 案卷派遣参数
    /// </summary>
    public class ProjectDispatchArgs : ProjectApprovedArgs
    {
        /// <summary>
        /// 派遣节点（8：职能部门派遣  7：派遣员派遣）
        /// </summary>
        public string strPQNode { get; set; }
        /// <summary>
        /// 督办人
        /// </summary>
        public string SuperviseName { get; set; }
        /// <summary>
        /// 督办内容
        /// </summary>
        public string SuperviseContent { get; set; }

        /// <summary>
        /// 部门手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 部门是否接受信息
        /// </summary>
        public string IsAcceptNote { get; set; }

    }

    /// <summary>
    /// 案卷核查参数
    /// </summary>
    public class ProjectCheckArgs : ProjectApprovedArgs
    {
        /// <summary>
        /// 监督员编号
        /// </summary>
        public string CollectorCode { get; set; }
        /// <summary>
        /// 核查信息
        /// </summary>
        public string Message { get; set; }
    }

    public class ProjectClosedArgs : ProjectApprovedArgs
    {
        /// <summary>
        /// 案卷受理年份
        /// </summary>
        public string StartYear { get; set; }
        /// <summary>
        /// 是否重复案卷（1：是 0：不是）
        /// </summary>
        public string IsRepeatProject { get; set; }

        /// <summary>
        /// 市局案卷编号
        /// </summary>
        public string SJProjectCode { get; set; }
    }

}
