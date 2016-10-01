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

    }
}
