using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class QueryArgs
    {
        /// <summary>
        /// 案卷流程节点
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 上报时间开始
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 上报时间结束
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 核查状态
        /// </summary>
        public int PdaIoFlag { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public int DepartmentCode { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 街道编码
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 社区编码
        /// </summary>
        public string Square { get; set; }
        /// <summary>
        /// 大类编码
        /// </summary>
        public string bigClass { get; set; }
        /// <summary>
        /// 小类编码
        /// </summary>
        public string smallclass { get; set; }
        /// <summary>
        /// 事发位置
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 部件编码
        /// </summary>
        public string partID { get; set; }
        /// <summary>
        /// 案卷类型（0：部件 1：事件）
        /// </summary>
        public string typecode { get; set; }
        /// <summary>
        /// 操作按钮编码
        /// </summary>
        public string strButtonId { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string strUserCode { get; set; }


    }
}
