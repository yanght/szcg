using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model.RequestModel
{
    public class AreaAppraiseRequestArgs
    {
        /// <summary>
        /// 模块Id (区域评价：6 , 责任单位考核：24 , 责任单位年考核：25 , 二级平台职能部门考核：32 , 职能部门内部考核：33 , 岗位评价（平台受理员）：13 , 岗位评价（值班长）：14 , 岗位评价（二级值班长）：30 , 监督员评价：21 )
        /// </summary>
        public int ModelId { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 当前用户角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 评价时间类型（0：按周  1：按月  2：按季度 3：按年）
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 按周或按月或按季度的具体数值
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 返回的信息
        /// </summary>
        public string strReportMessage { get; set; }

        /// <summary>
        /// 数据库返回的列字符串（返回时使用）
        /// </summary>
        public string cols { get; set; }
        /// <summary>
        /// 查询开始时间（返回时使用）
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 查询结束时间（返回时使用）
        /// </summary>
        public string endTime { get; set; }

    }
}
