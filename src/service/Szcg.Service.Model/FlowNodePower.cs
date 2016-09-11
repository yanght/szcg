using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class FlowNodePower
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 动作按钮编码
        /// </summary>
        public string ButtonCode { get; set; }
        /// <summary>
        ///按钮名称
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// 流程节点
        /// </summary>
        public string FlowId { get; set; }
        public string RelaId { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        public string NodeId { get; set; }
        /// <summary>
        /// 按钮标志
        /// </summary>
        public string ButtonId { get; set; }

        public string ClassId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 按钮显示名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string Del { get; set; }
        /// <summary>
        /// 按钮图片地址
        /// </summary>
        public string ImageAdd { get; set; }
        public string Property { get; set; }
    }
}
