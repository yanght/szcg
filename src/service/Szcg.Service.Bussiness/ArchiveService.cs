using bacgBL.com.teamax.szbase.systemsetting;
using bacgBL.web.szbase.repositoryandarchives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    public class ArchiveService : IArchiveService
    {
        BASE_archivesmanage arch = new BASE_archivesmanage();

        SystemSetting clsSystemsetting = new SystemSetting();

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

        /// <summary>
        /// 获取知识库列表
        /// </summary>
        /// <returns></returns>
        public List<Repository> GetAllRepository()
        {
            List<Repository> list = new List<Repository>();

            ArrayList arrs = arch.getAllRepository();

            foreach (var item in arrs)
            {
                string[] values = (string[])item;
                Repository resp = new Repository()
                {
                    Id = int.Parse(values[0]),
                    PId = int.Parse(values[1]),
                    Name = values[2],
                };
                list.Add(resp);
            }
            return list;
        }

        /// <summary>
        /// 根据Id获取知识库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Repository GetRepositoryById(int id)
        {
            Hashtable table = (Hashtable)arch.getRepositoryInfoByID(id);

            Repository resp = new Repository()
            {
                Id = int.Parse(table["pid"].ToString()),
                Name = table["name"].ToString(),
                _Desc = table["desc"].ToString(),
                PId = int.Parse(table["pid"].ToString())
            };

            return resp;
        }

        /// <summary>
        /// 添加或修改知识库
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        public bool InsertRepository(Repository rep)
        {
            bool rtn = false;

            if (rep.Id != 0)
            {
                rtn = arch.updateIntoRepository(rep.Name, rep.PId, rep._Desc, rep.Id);
            }
            else
            {
                rtn = arch.insertIntoRepository(rep.Name, rep.PId, rep._Desc);
            }

            return rtn;
        }

        /// <summary>
        /// 删除知识库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteRepository(int id)
        {
            return arch.deleteFromRepository(id);
        }

        /// <summary>
        /// 获取阶段列表
        /// </summary>
        /// <returns></returns>
        public List<Step> GetJieDuanInfo()
        {
            List<Step> list = new List<Step>();

            DataSet ds = clsSystemsetting.GetJieDuanInfo(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Step step = new Step()
                    {
                        Code = dr[0].ToString(),
                        Name = dr[1].ToString(),
                    };

                    list.Add(step);
                }
            }

            return list;
        }

        /// <summary>
        /// 获得字典库数据
        /// </summary>
        /// <param name="stpCode">步骤编码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<DictionSentence> GetDicSentenceList(int stpCode, int pageIndex, int pageSize)
        {
            List<DictionSentence> list = new List<DictionSentence>();

            DataSet ds = clsSystemsetting.GetInfoD(0, stpCode.ToString(), pageIndex, pageSize);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<DictionSentence>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 根据Id和步骤名称获取字典库
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="stepName">当前阶段名称</param>
        /// <returns></returns>
        public DictionSentence GetDicSentence(int id, string stepName)
        {
            DictionSentence dic = new DictionSentence();

            string[] arrs = clsSystemsetting.GetDPInfo(0, id, stepName);

            if (arrs != null && arrs.Length > 0)
            {
                dic.Short = arrs[0];
                dic.Long_Sentence = arrs[1];
                dic.StepName = arrs[2];
                dic.FId = arrs[3];
            }

            return dic;
        }

        /// <summary>
        /// 添加或修改字典库
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool InsertDictionSentence(DictionSentence dic)
        {
            if (dic.Id > 0)
            {
                return clsSystemsetting.UpdateInfo(0, dic.Id, dic.FId, dic.Short, dic.Long_Sentence, dic.StepCode.ToString()) > 0;
            }
            else
            {
                return clsSystemsetting.InsertData(0, dic.FId, dic.Short, dic.Long_Sentence, dic.StepCode.ToString()) > 0;
            }
        }

    }
}
