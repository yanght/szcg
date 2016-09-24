using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class AreaAppraise
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }
        public string DutyId { get; set; }
        /// <summary>
        /// 问题及时解决数
        /// </summary>
        public string 问题及时解决数 { get; set; }
        public string 问题未及时解决数 { get; set; }
        public string 问题应解决数 { get; set; }
        public string 问题未解决数 { get; set; }
        public string 问题及时解决率 { get; set; }
        public string 问题解决数 { get; set; }
        public string 累计问题应解决总数 { get; set; }
        public string 返工数 { get; set; }
        public string 反复数 { get; set; }
        public string 超多反复数 { get; set; }
        public string 超时倍数 { get; set; }
        public string 问题解决率 { get; set; }
        public string 得分 { get; set; }
     
    }
}
