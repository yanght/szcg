using bacgBL.web.szbase.repositoryandarchives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    public class ArchiveService
    {
        BASE_archivesmanage arch = new BASE_archivesmanage();

        /// <summary>
        /// 获取公文标题列表
        /// </summary>
        /// <returns></returns>
        public List<DocumentTitle> getAllArchives()
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

        public bool insertDocument()
        {
            return false;
        }



    }
}
