using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class ProjectTrace
    {
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 案卷名称
        /// </summary>
        public string ProjName { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 职能部门
        /// </summary>
        public string TargetDepartName { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 受理时间
        /// </summary>
        public DateTime StartDate { get; set; }
        public string Fid { get; set; }
        /// <summary>
        /// 部件编号
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string _Opinion { get; set; }
        /// <summary>
        /// 办理时间
        /// </summary>
        public DateTime Cu_Date { get; set; }
        /// <summary>
        /// 受理部门
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 受理部门
        /// </summary>
        public string ParentDepartName { get; set; }
        /// <summary>
        /// 动作按钮编号
        /// </summary>
        public string ButtonCode { get; set; }
    }
}
