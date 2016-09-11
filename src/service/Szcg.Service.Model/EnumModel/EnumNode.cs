using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 案卷流程节点状态
    /// </summary>
    public enum EnumNode
    {
        /// <summary>
        /// 案卷受理
        /// </summary>
        Accept = 1,

        /// <summary>
        /// 举报栏
        /// </summary>
        Report = 2,

        /// <summary>
        /// 注销案卷
        /// </summary>
        Cancel = 3,

        /// <summary>
        /// 登记栏
        /// </summary>
        Register = 4,

        /// <summary>
        /// 案卷删除
        /// </summary>
        Delete = 5,

        /// <summary>
        /// 办理栏
        /// </summary>
        Handle = 6,

        /// <summary>
        /// 办理栏1
        /// </summary>
        Handle1 = 8,

        /// <summary>
        /// 核查栏
        /// </summary>
        Check = 9,

        /// <summary>
        /// 待结案栏
        /// </summary>
        ToBeClosed = 10,

        /// <summary>
        /// 已结案
        /// </summary>
        Closed = 11

    }
}
