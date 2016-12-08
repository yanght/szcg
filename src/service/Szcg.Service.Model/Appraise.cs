using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 部门评价模型
    /// </summary>
    public class Depart_Appraise
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 上级部门编码
        /// </summary>
        public string PCode { get; set; }
        public string 考评月份 { get; set; }
        public string 部门名称 { get; set; }
        public string 问题应解决数 { get; set; }
        public string 问题解决数 { get; set; }
        public string 问题未解决数 { get; set; }
        public string 超时倍数 { get; set; }
        public string 返工数 { get; set; }
        public string 反复数 { get; set; }
        public string 超多反复数 { get; set; }
        public string 问题解决率 { get; set; }
        public string 问题及时解决率 { get; set; }
        public string 问题按时解决数 { get; set; }
        public string 问题按时解决率 { get; set; }
        public string 排序号 { get; set; }

    }

    /// <summary>
    /// 区域评价模型
    /// </summary>
    public class Area_Appraise
    {
        public string Code { get; set; }
        public string PCode { get; set; }
        public string 区域名称 { get; set; }
        public string 派遣案件量 { get; set; }
        public string 部件派遣量 { get; set; }
        public string 事件派遣量 { get; set; }
        public string 未处理案卷 { get; set; }
        public string 已处理案卷 { get; set; }
        public string 部件结案量 { get; set; }
        public string 事件结案量 { get; set; }
        public string 按期结案量 { get; set; }
        public string 部件按期结案量 { get; set; }
        public string 事件按期结案量 { get; set; }
        public string 公众投诉总量 { get; set; }
        public string 公众投诉结案量 { get; set; }
        public string 公众投诉按期结案量 { get; set; }
        public string 超期结案量 { get; set; }
        public string 部件超期结案量 { get; set; }
        public string 事件超期结案量 { get; set; }
        public string 最大准确派遣案件量 { get; set; }
        public string 最大事件按期结案率 { get; set; }
        public string 最大部件按期结案量 { get; set; }
        public string 最大事件按期结案量 { get; set; }
        public string 最大超期结案总数 { get; set; }
        public string 最大部件超期结案量 { get; set; }
      
    }

    /// <summary>
    /// 事部件评价模型
    /// </summary>
    public class EvePar_Appraise
    {
        public string PCode { get; set; }
        public string Code { get; set; }
        public string 类型 { get; set; }
        public string 大类 { get; set; }
        public string 小类 { get; set; }
        public string 数量 { get; set; }
        public string 结案量 { get; set; }
        public string 占总数百分率 { get; set; }
        public string 占大类类型百分率 { get; set; }
        public string 结案率 { get; set; }


    }

    /// <summary>
    /// 岗位评价模型
    /// </summary>
    public class Duty_Appraise
    {
        public string Code { get; set; }
        public string PCode { get; set; }
        public string 操作员名称 { get; set; }
        public string 所属部门 { get; set; }
        public string 审批数 { get; set; }
        public string 错误审批数 { get; set; }
        public string 超时审批数 { get; set; }
        public string 超时结案数 { get; set; }
    }

    public class Collecter_Apprise
    {
        public string Code { get; set; }
        public string PCode { get; set; }
        public string 巡查考核员姓名 { get; set; }
        public string 错误上报数 { get; set; }
        public string 上报数 { get; set; }
        public string 差错率 { get; set; }
        public string 发送核查数 { get; set; }
        public string 核查回复数 { get; set; }
        public string 核查率 { get; set; }
        public string 按时核查数 { get; set; }
        public string 按时核查率 { get; set; }
        public string GPS超时次数 { get; set; }
    }
}
