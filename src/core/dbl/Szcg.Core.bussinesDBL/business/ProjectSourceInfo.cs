using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business
{
    /// <summary>
    /// 案件来源信息结构体
    /// </summary>
    public class ProjectSourceInfo
    {
        public string projcode = ""; //案件编号
        public string name = ""; //举报人
        public string tel = ""; //举报人电话
        public string ip = ""; //举报人IP
        public string accept = ""; //受理人编号
        public string type = ""; //问题来源类型
    }
}
