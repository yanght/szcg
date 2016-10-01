using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class DocumentTitle 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public string Cu_Date { get; set; }

    }
}
