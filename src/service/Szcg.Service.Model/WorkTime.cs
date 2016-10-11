using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 工作时间
    /// </summary>
    public class WorkTime
    {
        /// <summary>
        /// 启用日期
        /// </summary>
        public string qy_date { get; set; }
        /// <summary>
        /// 上午工作时间段
        /// </summary>
        public string sw_date { get; set; }
        /// <summary>
        /// 下午工作时间段
        /// </summary>
        public string xw_date { get; set; }

        /// <summary>
        /// 上午工作开始时间
        /// </summary>
        public string MstartTime { get; set; }
        /// <summary>
        /// 上午工作结束时间
        /// </summary>
        public string MendTime { get; set; }

        /// <summary>
        /// 下午工作开始时间
        /// </summary>
        public string FstartTime { get; set; }
        /// <summary>
        /// 下午工作结束时间
        /// </summary>
        public string FendTime { get; set; }

    }

    /// <summary>
    /// 节假日
    /// </summary>
    public class Holiday
    {
        public int Id { get; set; }
        /// <summary>
        /// 节假日日期
        /// </summary>
        public string HDate { get; set; }
        /// <summary>
        /// 节假日名称
        /// </summary>
        public string Name { get; set; }
    }
}
