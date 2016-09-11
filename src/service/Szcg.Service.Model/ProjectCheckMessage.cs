using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 案卷核查信息
    /// </summary>
    public class ProjectCheckMessage
    {
        /// <summary>
        ///序号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 巡查考核员
        /// </summary>
        public string CollectorName { get; set; }
        /// <summary>
        /// 核查回复时间
        /// </summary>
        public string Cu_Date { get; set; }
        /// <summary>
        /// 核查回复内容
        /// </summary>
        public string _Option { get; set; }
    }
}
