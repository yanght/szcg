using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Web.Controllers
{
    public class DocumentController : Controller
    {
        IArchiveService svc = new ArchiveService();

        //
        // GET: /Document/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 获取公文列表 ]

        public AjaxFxRspJson GetAllArchives()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<DocumentTitle> list = svc.GetAllArchives();

            ajax.RspData.Add("docs", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 根据Id获取公文详细 ]

        public AjaxFxRspJson GetDocumentById(int id)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (id <= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入公文Id";
                return ajax;
            }

            DocumentTitle doc = svc.GetDocumentById(id);

            ajax.RspData.Add("doc", JToken.FromObject(doc));

            return ajax;
        }

        #endregion

        #region [ 添加或修改公文 ]

        [HttpPost]
        public AjaxFxRspJson InsertDocument(DocumentTitle doc)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(doc.Title))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "公文主题不能为空！";
                return ajax;
            }

            if (doc.Title.Length > 64)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "公文主题长度不能超过64个字符！";
                return ajax;
            }

            if (doc.Title.IndexOf(",") >= 0 || doc.Title.IndexOf(";") >= 0 || doc.Title.IndexOf("@") >= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = doc.Title + "中不能含有 , ; @ 等特殊字符!";
                return ajax;
            }

            if (string.IsNullOrEmpty(doc.Author))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "发布部门不能为空！";
                return ajax;
            }

            if (string.IsNullOrEmpty(doc.Content))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "公文内容不能为空！";
                return ajax;
            }


            if (!svc.InsertDocument(doc))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "修改公文失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 删除公文 ]

        public AjaxFxRspJson DeleteDocument(int id)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (id <= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入公文Id";
                return ajax;
            }

            if (!svc.DeleteDocument(id))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "删除公文失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取知识库列表 ]

        public AjaxFxRspJson GetAllRepository()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Repository> list = svc.GetAllRepository();

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 根据Id获取公文详细 ]

        public AjaxFxRspJson GetRepositoryById(int id)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (id <= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入知识库Id";
                return ajax;
            }

            Repository doc = svc.GetRepositoryById(id);

            ajax.RspData.Add("repository", JToken.FromObject(doc));

            return ajax;
        }

        #endregion

        #region [ 添加或修改知识库 ]

        [HttpPost]
        public AjaxFxRspJson InsertDocument(Repository rep)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(rep.Name))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "知识点名称不能为空！";
                return ajax;
            }

            if (rep.Name.Length > 64)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "知识点名称长度不能超过64个字符！";
                return ajax;
            }

            if (rep.Name.IndexOf(",") >= 0 || rep.Name.IndexOf(";") >= 0 || rep.Name.IndexOf("@") >= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = rep.Name + "中不能含有 , ; @ 等特殊字符!";
                return ajax;
            }

            if (string.IsNullOrEmpty(rep._Desc))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "知识点内容不能为空！";
                return ajax;
            }

            if (rep._Desc.Length > 2048)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "知识点内容长度不能超过2048个字符!";
                return ajax;
            }

            if (!svc.InsertRepository(rep))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "修改知识库失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 删除知识库 ]

        public AjaxFxRspJson DeleteRepository(int id)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (id <= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入知识库Id";
                return ajax;
            }

            if (!svc.DeleteRepository(id))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "删除知识库失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取字典库阶段列表 ]

        public AjaxFxRspJson GetJieDuanInfo()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Step> list = svc.GetJieDuanInfo();

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获得字典库数据 ]

        public AjaxFxRspJson GetDicSentenceList(int stpCode, int pageIndex)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (stpCode <= 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入步骤名称";
                return ajax;
            }

            List<DictionSentence> list = svc.GetDicSentenceList(stpCode, pageIndex, 20);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 根据Id和步骤名称获取字典 ]

        public AjaxFxRspJson GetDicSentence(int id, string stepName)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            DictionSentence dic = svc.GetDicSentence(id, stepName);

            ajax.RspData.Add("dic", JToken.FromObject(dic));

            return ajax;
        }

        #endregion

        #region [ 添加或修改字典库 ]

        [HttpPost]
        public AjaxFxRspJson InsertDictionSentence(DictionSentence dic)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(dic.Short))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入索引字典值!";
                return ajax;
            }

            bool rtn = svc.InsertDictionSentence(dic);
            if (!rtn)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "修改字典库失败!";
                return ajax;
            }

            return ajax;
        }

        #endregion

    }
}
