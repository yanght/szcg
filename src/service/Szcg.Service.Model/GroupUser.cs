using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{

    /// <summary>
    /// 用户群组
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// 群组Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 上级群组Id
        /// </summary>
        public int ParentGroupId { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public int UserCode { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群组类型（0：公共群组 1：个人群组）
        /// </summary>
        public int GroupType { get; set; }
        public string GroupAction { get; set; }
    }


    /// <summary>
    /// 群组用户
    /// </summary>
    public class GroupUser
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public int UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }
}
