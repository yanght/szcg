using bacgBL.business.group;
using bacgBL.business.wdxxmanage;
using bacgBL.web.szbase.purview;
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
            int index = gm.InsertBusinessMsgs(message.Go_User.ToString(), message.To_User.ToString(), message.MsgTitle, message.MsgContent, "", message.MsgType);
            return index > 0;
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        public bool ReplayMessage(Message message)
        {
            int index = -1;
            if (message.MsgType == "4")
            {
                index = new wdxx().InsertBusinessMsg(message.Go_User.ToString(), message.To_User.ToString(), message.MsgTitle, message.MsgContent, message.Id.ToString(), "5", ref strErr);
            }
            else
            {
                index = new wdxx().InsertBusinessMsg(message.Go_User.ToString(), message.To_User.ToString(), message.MsgTitle, message.MsgContent, ref strErr);
            }
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
        /// 向监督员PDA发送WEB消息
        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="msgcontent">消息主题</param>
        /// <param name="title">消息内容</param>
        /// <param name="usercode">用户编号</param>
        /// <returns></returns>
        public bool SendPDAMsg(string collcode, string msgcontent, string title, string usercode)
        {
            bacgBL.business.collecter.SendPDAMsg(collcode, msgcontent, title, usercode, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                return false;
            }
            return true;
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

            DataSet ds = bacgBL.business.MyMessage.GetMsgList(userCode, int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), pageInfo.Order, pageInfo.Field, userName, beginTime, endTime,
                                                              ref rowCount, ref pageCount, ref strErr);
            pageInfo.PageCount = pageCount.ToString();

            pageInfo.RowCount = rowCount.ToString();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Message>.GetModelList(ds.Tables[0]);
            }
            return list;
        }

        /// <summary>
        /// 获取站内其他消息详细
        /// </summary>
        /// <param name="messageId">消息Id</param>
        /// <returns></returns>
        public Message GetOtherMessageInfo(string messageId)
        {
            DataSet ds = bacgBL.business.MyMessage.GetOtherMsgInfo(messageId, ref strErr);
            if (strErr != "")
            {
                LoggerManager.Instance.logger.ErrorFormat("获取站内其他消息详情异常 消息Id:{0} ,错误信息：{1}", messageId, strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ConvertDtHelper<Message>.GetModel(ds.Tables[0]);
            }
            return null;
        }

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
        public List<Message> GetOtherMessageList(string userCode, string areaCode, string userName, string collName, string beginTime, string endTime, PageInfo pageInfo)
        {
            List<Message> list = new List<Message>();

            int pageCount = 0;

            int rowCount = 0;

            DataSet ds = bacgBL.business.MyMessage.GetOtherMsgList(userCode, areaCode, int.Parse(pageInfo.CurrentPage),
                                                    int.Parse(pageInfo.PageSize), userName, collName, beginTime, endTime,pageInfo.Order, pageInfo.Field, ref rowCount, ref pageCount, ref strErr);
            pageInfo.PageCount = pageCount.ToString();

            pageInfo.RowCount = rowCount.ToString();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Message>.GetModelList(ds.Tables[0]);
            }
            return list;
        }

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
        public List<Message> GetHelpMessageList(string userCode, string areaCode, string userName, string collName, string beginTime, string endTime, PageInfo pageInfo)
        {
            List<Message> list = new List<Message>();

            int pageCount = 0;

            int rowCount = 0;

            DataSet ds = bacgBL.business.MyMessage.GetHelpMsgList(areaCode, int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), userCode, collName, beginTime, endTime, "asc", "id",
                                                                      ref rowCount, ref pageCount, ref strErr);
            pageInfo.PageCount = pageCount.ToString();

            pageInfo.RowCount = rowCount.ToString();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Message>.GetModelList(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// 设置业务消息已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool SetMessageIsRead(string messageId)
        {
            int index = bacgBL.business.MyMessage.SetIsRead(messageId, ref strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("设置消息已读错误 错误信息:{0}", strErr);
                return false;
            }
            return index > 0;
        }

        /// <summary>
        /// 设置PDA消息已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool SetPDAMessageIsRead(string messageId)
        {
            int index = bacgBL.business.MyMessage.SetPDAIsRead(messageId, ref strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("设置PDA消息已读错误 错误信息:{0}", strErr);
                return false;
            }
            return index > 0;
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息Id</param>
        /// <returns></returns>
        public bool DeleteMsg(string id)
        {
            bacgBL.business.MyMessage.DeleteMsg(id, ref strErr);
            if (string.IsNullOrEmpty(strErr)) return true;
            return false;
        }

        /// <summary>
        /// 获取消息部门树
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="departcode">当前用户部门编码</param>
        /// <returns></returns>
        public List<Depart> GetUserTreeList(string areacode, string departcode)
        {
            List<Depart> departs = new List<Depart>();

            Purviews bl = new Purviews();

            ArrayList list = bl.GetUserTreeList(areacode, "0", ref strErr);

            if (list != null & list.Count > 0)
            {
                foreach (var item in list)
                {
                    bacgBL.web.szbase.entity.TreeSuruct ts = (bacgBL.web.szbase.entity.TreeSuruct)item;

                    Depart depart = new Depart()
                    {
                        DepartCode = ts.code,
                        DepartName = ts.text,
                        ParentCode = ts.pcode,
                        Memo = ts.tag
                    };

                    departs.Add(depart);
                }
            }

            return departs;
        }

        /// <summary>
        ///  获取人员树信息（部门，人员）
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public List<Depart> GetUserPhoneTreeList(string areacode)
        {
            List<Depart> departs = new List<Depart>();
            Purviews bl = new Purviews();
            ArrayList list = bl.GetUserPhoneTreeList(areacode, ref strErr);
            if (list != null & list.Count > 0)
            {
                foreach (var item in list)
                {
                    bacgBL.web.szbase.entity.TreeSuruct ts = (bacgBL.web.szbase.entity.TreeSuruct)item;

                    Depart depart = new Depart()
                    {
                        DepartCode = ts.code,
                        DepartName = ts.text,
                        ParentCode = ts.pcode,
                        Memo = ts.text
                    };

                    departs.Add(depart);
                }
            }

            return departs;
        }

        /// <summary>
        /// 获得用户树形结构的信息
        /// </summary>
        /// <param name="usercode">用户代码</param>
        ///// <param name="GroupType">组类型</param>
        /// <param name="strErr">错误返回信息</param>
        /// <returns></returns>
        public List<Depart> GetGroupTreeList2(int usercode)
        {
            List<Depart> departs = new List<Depart>();

            bacgBL.business.group.BUSINESS_GroupManagers bl = new bacgBL.business.group.BUSINESS_GroupManagers();

            ArrayList list = bl.GetGroupTreeList2(usercode, ref strErr);

            if (list != null & list.Count > 0)
            {
                foreach (var item in list)
                {
                    bacgBL.web.szbase.entity.TreeSuruct ts = (bacgBL.web.szbase.entity.TreeSuruct)item;

                    Depart depart = new Depart()
                    {
                        DepartCode = ts.code,
                        DepartName = ts.text,
                        ParentCode = ts.pcode
                    };

                    departs.Add(depart);
                }
            }
            return departs;
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobiles">手机号码列表多个','分隔</param>
        /// <param name="content">短信内容</param>
        /// <returns></returns>
        public bool SendMobileMessage(string mobiles, string content)
        {
            bacgBL.business.Message bl = new bacgBL.business.Message();

            int strErr = -1;

            if (mobiles.Contains(","))
            {
                string[] sth = mobiles.Split(',');
                for (int i = 0; i < sth.Length; i++)
                {
                    strErr = bacgBL.Pub.TxMsgClass.ShortMessage(sth[i], content);
                }
            }
            else
            {
                strErr = bacgBL.Pub.TxMsgClass.ShortMessage(mobiles, content);
            }
            return strErr ==0;
        }


    }
}
