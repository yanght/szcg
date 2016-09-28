using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model.RequestModel
{
    public class CollecterAppraiseRequestArgs : AreaAppraiseRequestArgs
    {
        /// <summary>
        /// 监督员编码
        /// </summary>
        public string CollectorCode { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 街道编码
        /// </summary>
        public string StreetCode { get; set; }

    }
}
