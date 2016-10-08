using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 公文实体
    /// </summary>
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
    /// <summary>
    /// 知识库实体
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 上级编号
        /// </summary>
        public int PId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Cu_Date { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string _Desc { get; set; }
    }

    /// <summary>
    /// 字典库
    /// </summary>
    public class DictionSentence
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 步骤编码
        /// </summary>
        public int StepCode { get; set; }
        /// <summary>
        /// 字典库类型
        /// </summary>
        public string FId { get; set; }
        /// <summary>
        /// 索引字典值
        /// </summary>
        public string Short { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Long_Sentence { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }
    }

    /// <summary>
    /// 阶段
    /// </summary>
    public class Step
    {
        /// <summary>
        /// 阶段编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public string Name { get; set; }
    }
}
