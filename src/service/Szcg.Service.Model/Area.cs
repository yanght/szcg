using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 区域
    /// </summary>
    public class Area
    {
        public string Id { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }
    }
    /// <summary>
    /// 街道
    /// </summary>
    public class Street
    {
        public string Id { get; set; }
        public string StreetCode { get; set; }
        public string AreaCode { get; set; }
        public string StreetName { get; set; }
        public double Population { get; set; }
        public double Area { get; set; }
    }
    /// <summary>
    /// 社区
    /// </summary>
    public class Community
    {
        public string Id { get; set; }
        public string CommCode { get; set; }
        public string StreetCode { get; set; }
        public string CommName { get; set; }
        public string Population { get; set; }
        public double Area { get; set; }
    }
}
