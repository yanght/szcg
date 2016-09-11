using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business
{
    /// <summary>
    /// 分页结构体
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public string CurrentPage = "";
        /// <summary>
        /// 总行数
        /// </summary>
        public string RowCount = "";
        /// <summary>
        /// 总页数
        /// </summary>
        public string PageCount = "";
        /// <summary>
        /// 页码大小
        /// </summary>
        public string PageSize = "";
        /// <summary>
        /// 关键字段
        /// </summary>
        public string Field = "";
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Order = "";
        /// <summary>
        /// 查询需要返回记录数
        /// </summary>
        public string ReturnRecordCount = "";
    }
}
