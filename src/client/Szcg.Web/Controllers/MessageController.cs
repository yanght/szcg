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
    public class MessageController : Controller
    {
        IMessageService svc = new MessageService();
        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View();
        }

        #region [ 发送站内消息 ]

        [HttpPost]
        public AjaxFxRspJson SendMessage(Message message)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool flag = false;

            if (string.IsNullOrEmpty(message.MsgType))
            {
                ajax.Method = "请选择短信类型";
                ajax.RspCode = 0;
                return ajax;
            }

            if (message.To_User == 0)
            {
                ajax.Method = "请选择收件人";
                ajax.RspCode = 0;
                return ajax;
            }
            if (string.IsNullOrEmpty(message.MsgTitle))
            {
                ajax.Method = "请输入消息主题";
                ajax.RspCode = 0;
                return ajax;
            }
            if (string.IsNullOrEmpty(message.MsgContent))
            {
                ajax.Method = "请输入消息内容";
                ajax.RspCode = 0;
                return ajax;
            }
            if (message.MsgType == "1")
            {
                flag = svc.InsertMessage(message);
            }
            else
            {
                flag = svc.InsertGroupMessage(message);
            }
            if (!flag)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "发送消息失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 获取群组用户 ]

        public AjaxFxRspJson GetGroupUser()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            return ajax;
        }

        #endregion

    

    }
}
