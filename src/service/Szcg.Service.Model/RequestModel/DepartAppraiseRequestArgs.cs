using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model.RequestModel
{
    public class DepartAppraiseRequestArgs : AreaAppraiseRequestArgs
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartCode { get; set; }
    }
}
