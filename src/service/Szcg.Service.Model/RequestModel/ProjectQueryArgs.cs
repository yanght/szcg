using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 查询箱查询参数模型
    /// </summary>
    public class ProjectQueryArgs
    {
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string ProbSource { get; set; }
        /// <summary>
        /// 问题分类（0：部件 1：事件）
        /// </summary>
        public string ProbClass { get; set; }
        /// <summary>
        /// 大类编码
        /// </summary>
        public string BigClass { get; set; }
        /// <summary>
        /// 小类编码
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string StreetId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string SquareId { get; set; }
        /// <summary>
        /// 受理时间开始
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 受理时间结束
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 案卷详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 案卷所属区域
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 巡查员编码
        /// </summary>
        public string CollCode { get; set; }
        /// <summary>
        /// 受理员编码
        /// </summary>
        public string Telephonist { get; set; }
        /// <summary>
        /// 职能部门编码
        /// </summary>
        public string TargetDepartCode { get; set; }
        /// <summary>
        /// 案卷类型（0：举报一般案卷 1：举报重大案卷 2：督办案卷）
        /// </summary>
        public string ProjectKind { get; set; }
        /// <summary>
        /// 办理时间开始
        /// </summary>
        public string DoStartTime { get; set; }
        /// <summary>
        /// 办理时间结束
        /// </summary>
        public string DoEndTime { get; set; }
        /// <summary>
        /// 案卷状态（0：未立案，1:已立案（全部），11：已立案（未派遣），2：已派遣（全部），21：已派遣（未处理），3：已处理（全部），31：已处理（未结案），9：已结案（全部），99：已结案（超时））
        /// </summary>
        public string ProjectState { get; set; }
        /// <summary>
        /// 案卷删除状态（0：正常 1:已删除 2已归档 3:回退案卷）
        /// </summary>
        public string DeleteState { get; set; }
        /// <summary>
        /// 案卷重要级别（0：一级案卷 1:二级案卷）
        /// </summary>
        public string ProjectImport { get; set; }

    }
}
