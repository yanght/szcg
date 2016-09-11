using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class ReturnValue
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool ReturnState { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public object ReturnObj { get; set; }
    }
}
