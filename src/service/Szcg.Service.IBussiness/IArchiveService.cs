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

        /// <summary>
        /// 获取知识库列表
        /// </summary>
        /// <returns></returns>
        List<Repository> GetAllRepository();

        /// <summary>
        /// 根据Id获取知识库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Repository GetRepositoryById(int id);

        /// <summary>
        /// 添加或修改知识库
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        bool InsertRepository(Repository rep);

        /// <summary>
        /// 删除知识库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteRepository(int id);

        /// <summary>
        /// 获取阶段列表
        /// </summary>
        /// <returns></returns>
        List<Step> GetJieDuanInfo();

        /// <summary>
        /// 获得字典库数据
        /// </summary>
        /// <param name="stpCode">步骤编码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<DictionSentence> GetDicSentenceList(int stpCode, int pageIndex, int pageSize);

        /// <summary>
        /// 根据Id和步骤名称获取字典
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="stepName">当前阶段名称</param>
        /// <returns></returns>
        DictionSentence GetDicSentence(int id, string stepName);

        /// <summary>
        /// 添加或修改字典库
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        bool InsertDictionSentence(DictionSentence dic);
    }
}
