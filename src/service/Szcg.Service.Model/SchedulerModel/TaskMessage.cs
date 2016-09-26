using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class TaskMessage
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 消息类型（0：普通消息  1：案卷消息）
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 任务状态（0：失败 1：成功）
        /// </summary>
        public int TaskStatus { get; set; }
        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int TryTimes { get; set; }
        /// <summary>
        /// 已执行次数
        /// </summary>
        public int LoopTimes { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecDateTime { get; set; }
        public int Disabled { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
