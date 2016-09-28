using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model.RequestModel
{
    /// <summary>
    /// 事部件评价参数
    /// </summary>
    public class EveParAppraiseRequestArgs
    {
        /// <summary>
        /// 评价时间类型（0：按周  1：按月  2：按季度 3：按年）
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 按周或按月或按季度的具体数值
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 街道编码
        /// </summary>
        public string StreetCode { get; set; }

        /// <summary>
        /// 返回的信息
        /// </summary>
        public string strReportMessage { get; set; }
    }
}
