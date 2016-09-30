using bacgBL.business.group;
using bacgDL.business;
using DBbase.business.group;
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
    public class MessageService : IMessageService
    {
        private string strErr = "";

        groupmanager gm = new groupmanager();

        /// <summary>
        /// 发送个人站内消息
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        public bool InsertMessage(Message message)
        {
            int index = gm.InsertBusinessMsg(message.Go_User.ToString(), message.To_User.ToString(), message.MsgTitle, message.MsgContent);
            return index > 0;
        }

        /// <summary>
        /// 发送群组站内消息
        /// </summary>
        /// <returns></returns>
        public bool InsertGroupMessage(Message message)
        {
            int index = gm.InsertGroupMsg(message.Go_User.ToString(), message.To_User.ToString(), message.MsgTitle, message.MsgContent);
            return index > 0;
        }

        /// <summary>
        /// 获取用户群组
        /// </summary>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="groupType">群组类型（0：公共群组 1：个人群组）</param>
        /// <returns></returns>
        public List<UserGroup> GetUserGroups(int userCode, bool groupType)
        {
            List<UserGroup> list = new List<UserGroup>();
            BUSINESS_GroupManagers bl = new BUSINESS_GroupManagers();
            ArrayList arrs = bl.GetGroupTreeList(userCode, groupType, ref strErr);
            foreach (var item in arrs)
            {
                GroupTreeSuruct gt = (GroupTreeSuruct)item;

                UserGroup group = new UserGroup()
                {
                    Id = gt.id,
                    ParentGroupId = gt.ParentGroupID,
                    GroupName = gt.groupname,
                    GroupType = gt.GroupType ? 1 : 0,
                };
                list.Add(group);
            }
            return list;
        }

        /// <summary>
        /// 获取群组用户
        /// </summary>
        /// <param name="groupId">群组Id</param>
        /// <returns></returns>
        public List<GroupUser> GetGroupUsers(int groupId)
        {
            List<GroupUser> list = new List<GroupUser>();
            BUSINESS_GroupManagers bl = new BUSINESS_GroupManagers();
            ArrayList arrs = bl.GetGroupPersion(groupId.ToString(), ref strErr);
            foreach (var item in arrs)
            {
                GroupPerson person = (GroupPerson)item;
                GroupUser user = new GroupUser()
                {
                    UserCode = person.usercode,
                    UserName = person.username
                };
                list.Add(user);
            }
            return list;
        }

        /// <summary>
        /// 获取站内消息详细
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <returns></returns>
        public Message GetMessageInfo(string messageId)
        {
            DataSet ds = bacgBL.business.MyMessage.GetMsgInfo(messageId, ref strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("获取站内消息详情异常 消息Id:{0} ,错误信息：{1}", messageId, strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ConvertDtHelper<Message>.GetModel(ds.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="userName">发件人姓名</param>
        /// <param name="beginTime">发送时间开始</param>
        /// <param name="endTime">结束时间结束</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        public List<Message> GetMessageList(int userCode, string userName, string beginTime, string endTime, PageInfo pageInfo)
        {
            List<Message> list = new List<Message>();

            int pageCount = 0;

            int rowCount = 0;

            DataSet ds = bacgBL.business.MyMessage.GetMsgList(userCode, int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), "asc", "to_user", userName, beginTime, endTime,
                                                              ref rowCount, ref pageCount, ref strErr);
            pageInfo.PageCount = pageCount.ToString();

            pageInfo.RowCount = rowCount.ToString();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Message>.GetModelList(ds.Tables[0]);
            }
            return list;
        }

    }
}
