using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        public int RoleCode { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 时都删除
        /// </summary>
        public string IsDel { get; set; }
        /// <summary>
        /// 角色等级
        /// </summary>
        public string RoleLevel { get; set; }

    }

    /// <summary>
    /// 系统模块
    /// </summary>
    public class SystemModel
    {
        public string ParentCode { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }
        public string Memo { get; set; }
    }

    /// <summary>
    ///角色授权
    /// </summary>
    public class AccreditPurview
    {
        /// <summary>
        /// 授权人
        /// </summary>
        public string consignerId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string accepterId { get; set; }
        /// <summary>
        /// 授权角色
        /// </summary>
        public string roleList { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }
    }
}
