using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class AppraiseProjectListRequest
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string Modeid { get; set; }
        public string AreaCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string DateStart { get; set; }
        public string TotalRows { get; set; }
        public string TotalPages { get; set; }
        public string CurrentPage { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string DateEnd { get; set; }

        public string Order { get; set; }
        public string Field { get; set; }

        public string isType { get; set; }
        public string departname { get; set; }
        public string labelname { get; set; }
        public string labeltype { get; set; }
        public string streetcode { get; set; }
        public string DataField { get; set; }
        public string UserDefinedCode { get; set; }
        public string UserCode { get; set; }
        public string AppriseType1 { get; set; }
        public string AppriseType2 { get; set; }

    }
}
