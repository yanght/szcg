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
using Szcg.Web.Models;

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


            if (message.To_User =="")
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

            message.Go_User = UserInfo.getUsercode().ToString();

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


        public AjaxFxRspJson ReplayMessage(Message message)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool flag = false;

            if (string.IsNullOrEmpty(message.MsgType))
            {
                ajax.Method = "请选择短信类型";
                ajax.RspCode = 0;
                return ajax;
            }


            if (message.To_User == "")
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

            message.Go_User = UserInfo.getUsercode().ToString();

            flag = svc.ReplayMessage(message);

            if (!flag)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "发送消息失败！";
                return ajax;
            }

            return ajax;

        }

        #endregion

        [HttpPost]
        public AjaxFxRspJson SendPDAMsg(string collcode, string collname, string msgcontent, string title)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(collname))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "发送对象不能为空！";
                return ajax;
            }

            if (string.IsNullOrEmpty(msgcontent) || string.IsNullOrEmpty(title))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "消息主题与消息内容不允许为空！";
                return ajax;
            }

            if (!svc.SendPDAMsg(collcode, msgcontent, title, UserInfo.getUsercode().ToString()))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "向监督员发送消息失败！";
                return ajax;
            }

            return ajax;
        }


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
        public AjaxFxRspJson GetMessageInfo(string messageId, string type, string option)
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
            svc.SetMessageIsRead(messageId);
            ajax.RspData.Add("message", JToken.FromObject(message));
            ajax.RspData.Add("option", JToken.FromObject(option));

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

        public JsonResult GetMessageList(string userName, string beginTime, string endTime, string currentPage)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }
            PageInfo pageInfo = new PageInfo();
            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "to_user";
            pageInfo.Order = "asc";

            List<Message> list = svc.GetMessageList(this.UserInfo.getUsercode(), userName, beginTime, endTime, pageInfo);

            // ajax.RspData.Add("list", JToken.FromObject(list));

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = list == null ? new List<Message>() : list }, JsonRequestBehavior.AllowGet);
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

        #region [ 删除消息 ]

        public AjaxFxRspJson DeleteMsg(string id)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            bool result = svc.DeleteMsg(id);

            if (!result)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "消息删除失败！";
                return ajax;
            }

            return ajax;
        }

        #endregion

        public AjaxFxRspJson GetUserTreeList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            List<TreeModel> tree = new List<TreeModel>();
            List<Depart> list = svc.GetUserTreeList(UserInfo.getAreacode(), UserInfo.getDepartcode().ToString());
            foreach (var item in list)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.DepartCode,
                    pId = item.ParentCode,
                    name = item.DepartName
                };
                tree.Add(depart);
            }

            ajax.RspData.Add("groups", JToken.FromObject(tree));

            return ajax;
        }

        public AjaxFxRspJson GetUserGroupList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<TreeModel> tree = new List<TreeModel>();

            tree.Add(new TreeModel() { id = "001", name = "群组", pId = "0", open = true });

            tree.Add(new TreeModel() { id = "0_1", name = "个人群组", pId = "001", open = true });

            tree.Add(new TreeModel() { id = "0_0", name = "公共群组", pId = "001", open = true });

            List<UserGroup> personGroup = svc.GetUserGroups(UserInfo.getUsercode(), true);

            List<UserGroup> publicGroup = svc.GetUserGroups(UserInfo.getUsercode(), false);

            foreach (var item in personGroup)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.Id.ToString(),
                    pId = item.ParentGroupId.ToString() + "_1",
                    name = item.GroupName
                };
                tree.Add(depart);
            }

            foreach (var item in publicGroup)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.Id.ToString(),
                    pId = item.ParentGroupId.ToString() + "_0",
                    name = item.GroupName
                };
                tree.Add(depart);
            }

            ajax.RspData.Add("groups", JToken.FromObject(tree));

            return ajax;
        }

        public AjaxFxRspJson GetUserPhoneTreeList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            List<TreeModel> tree = new List<TreeModel>();
            List<Depart> list = svc.GetUserPhoneTreeList(UserInfo.getAreacode());
            foreach (var item in list)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.DepartCode,
                    pId = item.ParentCode,
                    name = item.DepartName
                };
                tree.Add(depart);
            }

            ajax.RspData.Add("groups", JToken.FromObject(tree));

            return ajax;
        }
    }
}
