using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class CollecterProject
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 城管通号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 案卷号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Proj { get; set; }
        /// <summary>
        /// 小类
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 派发时间
        /// </summary>
        public string PFTime { get; set; }
        /// <summary>
        /// 核查时间
        /// </summary>
        public string CheckTime { get; set; }
        /// <summary>
        /// 是否超时
        /// </summary>
        public string IsDelay { get; set; }
        /// <summary>
        /// 社区
        /// </summary>
        public string Square { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 核查情况
        /// </summary>
        public string CheckState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string ProbSource { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string StreetId { get; set; }
        /// <summary>
        /// 开始年份
        /// </summary>
        public string StartYear { get; set; }
        /// <summary>
        /// 是否结案
        /// </summary>
        public string IsEnd { get; set; }

    }
}
