using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 站内消息实体
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 系统Id
        /// </summary>
        public int SystemId { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public int Go_User { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public int To_User { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Cu_Date { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string MsgTitle { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgContent { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string AttachUrl { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发送人所属部门
        /// </summary>
        public string DepartName { get; set; }

    }
}

