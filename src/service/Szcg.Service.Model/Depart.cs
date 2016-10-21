using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class Depart
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartCode { get; set; }
        public string UserDefinedCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 上级部门编码
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentDepartName { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string Area { get; set; }

        public string Principal { get; set; }
        /// <summary>
        /// 部门手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 部门自定义编码$部门手机$是否接受短信$SJ_RoleCode
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 部门电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 部门地址
        /// </summary>
        public string DepartAddress { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 是否是职能部门
        /// </summary>
        public int IsDuty { get; set; }
        public int ChangeStatus { get; set; }
        public int Type { get; set; }
        public int Max_NoteNum { get; set; }
        /// <summary>
        /// 是否接受信息
        /// </summary>
        public int IsAcceptNote { get; set; }
        public int IsSJ { get; set; }
        public string SJ_RoleCode { get; set; }
        /// <summary>
        /// 是否参加评价
        /// </summary>
        public string NoAppraise { get; set; }
        public int DutyId { get; set; }
        public string RoleName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
    }
}
