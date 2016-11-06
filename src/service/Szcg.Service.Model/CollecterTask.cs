using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class CollecterTask
    {
        /// <summary>
        /// 监督员姓名
        /// </summary>
        public string CollName { get; set; }
        /// <summary>
        /// 案卷开始时间
        /// </summary>
        public string StartYear { get; set; }
        /// <summary>
        /// 街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 是否结案
        /// </summary>
        public string IsEnd { get; set; }
        public string Proj { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string ProbSource { get; set; }
        /// <summary>
        /// 批转
        /// </summary>
        public string NodeId3 { get; set; }
        /// <summary>
        /// 立案
        /// </summary>
        public string NodeId6 { get; set; }
        /// <summary>
        /// 派遣
        /// </summary>
        public string NodeId7 { get; set; }
        /// <summary>
        /// 发送核查
        /// </summary>
        public string PdaFlg01 { get; set; }
        /// <summary>
        /// 核查完毕
        /// </summary>
        public string PdaFlg21 { get; set; }
        /// <summary>
        /// 结案
        /// </summary>
        public string PdaFlg12 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Beizhu { get; set; }
    }
}
