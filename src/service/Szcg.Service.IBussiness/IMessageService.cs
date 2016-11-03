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
        /// 回复消息
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        bool ReplayMessage(Szcg.Service.Model.Message message);

        /// <summary>
        /// 发送群组站内消息
        /// </summary>
        /// <returns></returns>
        bool InsertGroupMessage(Szcg.Service.Model.Message message);

        /// <summary>
        /// 向监督员PDA发送WEB消息
        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="msgcontent">消息主题</param>
        /// <param name="title">消息内容</param>
        /// <param name="usercode">用户编号</param>
        /// <returns></returns>
        bool SendPDAMsg(string collcode, string msgcontent, string title, string usercode);

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
        /// 获取站内消息详细
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <returns></returns>
        Szcg.Service.Model.Message GetMessageInfo(string messageId);

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

        /// <summary>
        /// 获取站内其他消息详细
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <returns></returns>
        Szcg.Service.Model.Message GetOtherMessageInfo(string messageId);

        /// <summary>
        /// 获取其他消息列表
        /// </summary>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="areaCode">区域编码</param>
        /// <param name="userName">发件人姓名</param>
        /// <param name="collName">监督员姓名</param>
        /// <param name="beginTime">发件时间开始</param>
        /// <param name="endTime">发件时间结束</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        List<Szcg.Service.Model.Message> GetOtherMessageList(string userCode, string areaCode, string userName, string collName, string beginTime, string endTime, PageInfo pageInfo);

        /// <summary>
        /// 获取智能报警消息
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="collName">监督员姓名</param>
        /// <param name="beginTime">消息时间开始</param>
        /// <param name="endTime">消息时间结束</param>
        /// <param name="pageInfo">分页信息</param>
        /// <returns></returns>
        List<Szcg.Service.Model.Message> GetHelpMessageList(string userCode, string areaCode, string userName, string collName, string beginTime, string endTime, PageInfo pageInfo);

        /// <summary>
        /// 设置业务消息已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool SetMessageIsRead(string messageId);

        /// <summary>
        /// 设置PDA消息已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool SetPDAMessageIsRead(string messageId);

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息Id</param>
        /// <returns></returns>
        bool DeleteMsg(string id);

        /// <summary>
        /// 获取短信部门树
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="departcode">当前用户部门编码</param>
        /// <returns></returns>

        List<Depart> GetUserTreeList(string areacode, string departcode);

        /// <summary>
        ///  获取人员树信息（部门，人员）
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        List<Depart> GetUserPhoneTreeList(string areacode);

        /// <summary>
        /// 获得用户树形结构的信息
        /// </summary>
        /// <param name="usercode">用户代码</param>
        ///// <param name="GroupType">组类型</param>
        /// <param name="strErr">错误返回信息</param>
        /// <returns></returns>
        List<Depart> GetGroupTreeList2(int usercode);

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobiles">手机号码列表多个','分隔</param>
        /// <param name="content">短信内容</param>
        /// <returns></returns>
        bool SendMobileMessage(string mobiles, string content);
    }
}
