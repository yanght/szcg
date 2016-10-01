using bacgBL.web.szbase.repositoryandarchives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    public class ArchiveService : IArchiveService
    {
        BASE_archivesmanage arch = new BASE_archivesmanage();

        /// <summary>
        /// 获取公文标题列表
        /// </summary>
        /// <returns></returns>
        public List<DocumentTitle> GetAllArchives()
        {
            List<DocumentTitle> list = new List<DocumentTitle>();

            ArrayList arrs = arch.getAllArchives();

            foreach (var item in arrs)
            {
                string[] values = item as string[];

                DocumentTitle doc = new DocumentTitle()
                {
                    Id = int.Parse(values[0]),
                    Title = values[1]
                };
                list.Add(doc);
            }
            return list;
        }

        /// <summary>
        /// 根据Id获取公文
        /// </summary>
        /// <param name="id">公文Id</param>
        /// <returns></returns>
        public DocumentTitle GetDocumentById(int id)
        {
            DocumentTitle doc = new DocumentTitle();
            Hashtable table = (Hashtable)arch.getArchivesInfoByID(id);
            if (table != null)
            {
                doc.Title = table["title"].ToString();
                doc.Content = table["content"].ToString();
                doc.Author = table["author"].ToString();
            }
            return doc;
        }

        /// <summary>
        /// 添加或修改公文
        /// </summary>
        /// <param name="doc">公文实体</param>
        /// <returns></returns>
        public bool InsertDocument(DocumentTitle doc)
        {
            bool rtn = false;

            if (doc.Id != 0)
            {
                rtn = arch.updateIntoDocument(doc.Title, doc.Content, doc.Author, doc.Id);
            }
            else
            {
                rtn = arch.insertIntoDocument(doc.Title, doc.Content, doc.Author);
            }

            return rtn;
        }

        /// <summary>
        /// 删除公文
        /// </summary>
        /// <param name="id">公文Id</param>
        /// <returns></returns>
        public bool DeleteDocument(int id)
        {
            return arch.deleteFromDocument(id);
        }
    }
}
