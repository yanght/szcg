using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model.RequestModel
{
    public class DutyAppraiseRequestArgs : AreaAppraiseRequestArgs
    {
        /// <summary>
        /// 人员编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>

        public string DepartCode { get; set; }
    }
}
