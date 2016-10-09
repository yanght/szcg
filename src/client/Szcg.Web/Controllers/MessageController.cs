using bacgDL.business;
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
    public class MessageController : BaseController
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

        public AjaxFxRspJson GetGroupUser(int groupId)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<GroupUser> list = svc.GetGroupUsers(groupId);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获取消息详细 ]

        /// <summary>
        /// 获取消息详细
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <param name="type">消息类型（1：业务消息 2：其他信息）</param>
        /// <returns></returns>
        public AjaxFxRspJson GetMessageInfo(string messageId, string type)
        {
            Message message = new Message();

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (type == "1")
            {
                message = svc.GetMessageInfo(messageId);
            }
            if (type == "2")
            {
                message = svc.GetOtherMessageInfo(messageId);
            }

            ajax.RspData.Add("message", JToken.FromObject(message));

            return ajax;
        }

        #endregion

        #region [ 设置消息已读 ]

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <param name="type">消息类型（1：业务消息  2：PDA消息）</param>
        /// <returns></returns>
        public AjaxFxRspJson SetMessageRead(string messageId, string type)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(messageId))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入消息Id！";
                return ajax;
            }
            if (string.IsNullOrEmpty(type))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入消息类型（1：业务消息  2：PDA消息）！";
                return ajax;
            }
            if (type == "1")
            {
                if (!svc.SetMessageIsRead(messageId))
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "设置消息已读失败！";
                    return ajax;
                }
            }
            else if (type == "2")
            {
                if (!svc.SetPDAMessageIsRead(messageId))
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "设置消息已读失败！";
                    return ajax;
                }
            }

            return ajax;
        }

        #endregion

        #region [ 获取站内信息列表 ]

        public AjaxFxRspJson GetMessageList(string userName, string beginTime, string endTime, string currentPage)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            PageInfo pageinfo = new PageInfo() { CurrentPage = currentPage, PageSize = "10" };

            List<Message> list = svc.GetMessageList(this.UserInfo.getUsercode(), userName, beginTime, endTime, pageinfo);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获取其他消息列表 ]

        public AjaxFxRspJson GetOtherMessageList(string userName, string collName, string beginTime, string endTime, string currentPage)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            PageInfo pageinfo = new PageInfo() { CurrentPage = currentPage, PageSize = "10" };

            List<Message> list = svc.GetOtherMessageList(this.UserInfo.getUsercode().ToString(), this.UserInfo.getAreacode(), userName, collName, beginTime, endTime, pageinfo);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 获取智能报警消息列表 ]

        public AjaxFxRspJson GetHelpMessageList(string userName, string collName, string beginTime, string endTime, string currentPage)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            PageInfo pageinfo = new PageInfo() { CurrentPage = currentPage, PageSize = "10" };

            List<Message> list = svc.GetHelpMessageList(this.UserInfo.getUsercode().ToString(), this.UserInfo.getAreacode(), userName, collName, beginTime, endTime, pageinfo);

            ajax.RspData.Add("list", JToken.FromObject(list));

            return ajax;
        }

        #endregion

    }
}
