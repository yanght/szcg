using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface IArchiveService
    {
        /// <summary>
        /// 获取公文标题列表
        /// </summary>
        /// <returns></returns>
        List<DocumentTitle> GetAllArchives();

        /// <summary>
        /// 根据Id获取公文
        /// </summary>
        /// <param name="id">公文Id</param>
        /// <returns></returns>
        DocumentTitle GetDocumentById(int id);

        /// <summary>
        /// 添加或修改公文
        /// </summary>
        /// <param name="doc">公文实体</param>
        /// <returns></returns>
        bool InsertDocument(DocumentTitle doc);

        /// <summary>
        /// 删除公文
        /// </summary>
        /// <param name="id">公文Id</param>
        /// <returns></returns>
        bool DeleteDocument(int id);
    }
}
