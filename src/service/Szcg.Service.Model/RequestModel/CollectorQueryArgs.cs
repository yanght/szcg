using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 查询监督员列表参数
    /// </summary>
    public class CollectorQueryArgs
    {
        /// <summary>
        /// 类型(area,street,community)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 区编码或者街道id或者社区id
        /// </summary>
        public int  Id { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int ReturnRecordCount { get; set; }
        /// <summary>
        /// 监督员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 网格号
        /// </summary>
        public string GridCode { get; set; }

    }
}
