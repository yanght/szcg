using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 部门评价
    /// </summary>
    public class DepartAppraise
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

    /// <summary>
    /// 区域评价
    /// </summary>
    public class AreaAppraise
    {
        public string Code { get; set; }
        public string PCode { get; set; }
        public string Name { get; set; }
        public string max_bjja { get; set; }
        public string max_sjja { get; set; }
        public string max_bjjal { get; set; }
        public string max_sjjal { get; set; }
        public string max_cqja { get; set; }
        public string max_bjcqja { get; set; }
        public string max_zqpq { get; set; }
        public string zqpq { get; set; }
        public string bjpq { get; set; }
        public string sjpq { get; set; }
        public string wja { get; set; }
        public string ja_a { get; set; }       
        public string bjja_a { get; set; }
        public string sjja_a { get; set; }
        public string aqja { get; set; }
        public string bjja { get; set; }
        public string sjja { get; set; }
        public string gtzl { get; set; }
        public string gtjzl_a { get; set; }
        public string gtjzl { get; set; }
        public string cqja { get; set; }
        public string bjcqja { get; set; }
        public string sjcqja { get; set; }

    }



}
