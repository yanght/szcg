using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface IMessageService
    {
        /// <summary>
        /// 发送个人站内消息
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        bool InsertMessage(Szcg.Service.Model.Message message);

        /// <summary>
        /// 发送群组站内消息
        /// </summary>
        /// <returns></returns>
        bool InsertGroupMessage(Szcg.Service.Model.Message message);

        /// <summary>
        /// 获取用户群组
        /// </summary>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="groupType">群组类型（0：公共群组 1：个人群组）</param>
        /// <returns></returns>
        List<UserGroup> GetUserGroups(int userCode, bool groupType);

        /// <summary>
        /// 获取群组用户
        /// </summary>
        /// <param name="groupId">群组Id</param>
        /// <returns></returns>
        List<GroupUser> GetGroupUsers(int groupId);

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="userName">发件人姓名</param>
        /// <param name="beginTime">发送时间开始</param>
        /// <param name="endTime">结束时间结束</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        List<Szcg.Service.Model.Message> GetMessageList(int userCode, string userName, string beginTime, string endTime, PageInfo pageInfo);

    }
}
