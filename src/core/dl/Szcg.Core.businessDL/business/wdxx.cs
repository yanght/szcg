/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：我的消息-数据层访问类。
 * 结构组成：
 * 作    者：王超群
 * 创建日期：2007-06-17
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.business
{

    public class wdxx:Teamax.Common.CommonDatabase
    {

        #region InsertBusinessMsg:新建业务平台消息。同时插入业务平台回复信息
        /// <summary>
        /// 新建业务平台消息。同时插入业务平台回复信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
        /// <param name="msgcontent">回复id号</param>
        public int InsertBusinessMsg(string gousercode, string tousercode, string msgtitle, string msgcontent,string refid,string msgtype)
        {
            string strSQL="";
            if (tousercode.IndexOf(',') != -1)
            {
                string[] arrToUserCode = tousercode.Split(',');
                for (int i = 0; i < arrToUserCode.Length; i++)
                {
                    strSQL = SetInsertBusinessMsgSql(gousercode, msgtitle, msgcontent, refid, msgtype, strSQL, arrToUserCode[i]);
                    this.ExecuteNonQuery(strSQL);
                }
                return 1;
            }
            else
            {
                strSQL = SetInsertBusinessMsgSql(gousercode, msgtitle, msgcontent, refid, msgtype, strSQL, tousercode);

                return this.ExecuteNonQuery(strSQL);
            }
        }

        private static string SetInsertBusinessMsgSql(string gousercode, string msgtitle, string msgcontent, string refid, string msgtype, string strSQL, string arrToUserCode)
        {
            if (msgtype == "")
            {
                strSQL = string.Format(@"insert into s_message (Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread,AttachUrl)
                                            values('1','{0}','{1}',getdate(),'{2}','{3}','0','{4}')", gousercode, arrToUserCode, msgtitle, msgcontent, refid);
            }
            else
            {
                strSQL = string.Format(@"insert into s_message (Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread,AttachUrl,msgtype)
                                            values('1','{0}','{1}',getdate(),'{2}','{3}','0','{4}','{5}')", gousercode, arrToUserCode, msgtitle, msgcontent, refid, msgtype);
            }
            return strSQL;
        }
        #endregion

        #region InsertBusinessMsg:插入发送群组信息
        /// <summary>
        /// 插入发送群组信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
        /// <param name="msgcontent">回复id号</param>
        public int InsertGroupMsg(string gousercode, string togroupid, string msgtitle, string msgcontent, string refid, string msgtype)
        {
            string strSQL = "";
            string strsqls = "";
            bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage();
            if (togroupid.IndexOf(',') != -1)
            {
                string[] arrToGroupID = togroupid.Split(',');
                for (int i = 0; i < arrToGroupID.Length; i++)
                {
                    strsqls = GetSelectGroupMsgSql(arrToGroupID[i]);
                    DataSet GroupInUser = dl.ExecDatasetSql(strsqls);
                    DataTable UserCode = GroupInUser.Tables[0];
                    for(int j=0;j<UserCode.Rows.Count;j++)
                    {
                        string tousercode = UserCode.Rows[j]["usercode"].ToString();
                        strSQL = SetInsertGroupMsgSql(gousercode, msgtitle, msgcontent, refid, msgtype, strSQL, tousercode);
                        this.ExecuteNonQuery(strSQL);
                    }
                }
                return 1;
            }
            else
            {
                strsqls = GetSelectGroupMsgSql(togroupid);
                DataSet GroupInUser = dl.ExecDatasetSql(strsqls);
                DataTable UserCode = GroupInUser.Tables[0];
                for (int i = 0; i < UserCode.Rows.Count; i++)
                {
                    string tousercode = UserCode.Rows[i]["usercode"].ToString();
                    strSQL = SetInsertGroupMsgSql(gousercode, msgtitle, msgcontent, refid, msgtype, strSQL, tousercode);
                    this.ExecuteNonQuery(strSQL);
                }
                return 1;
            }
        }

        private static string SetInsertGroupMsgSql(string gousercode, string msgtitle, string msgcontent, string refid, string msgtype, string strSQL, string arrToUserCode)
        {
            if (msgtype == "")
            {
                strSQL = string.Format(@"insert into s_message (Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread,AttachUrl)
                                            values('1','{0}','{1}',getdate(),'{2}','{3}','0','{4}')", gousercode, arrToUserCode, msgtitle, msgcontent, refid);
            }
            else
            {
                strSQL = string.Format(@"insert into s_message (Sysmsgid,go_user,to_user,cu_date,msgtitle,msgcontent,isread,AttachUrl,msgtype)
                                            values('1','{0}','{1}',getdate(),'{2}','{3}','0','{4}','{5}')", gousercode, arrToUserCode, msgtitle, msgcontent, refid, msgtype);
            }
            return strSQL;
        }

        private static string GetSelectGroupMsgSql(string GroupID)
        {
            string strstring = "";
            strstring = string.Format(@"select * from p_user_group_detail where fid={0}", GroupID);
            return strstring;
        }
        #endregion

        #region GetMsgDetail:根据案卷编号,获取消息的详细信息
        /// <summary>
        /// 根据案卷编号,获取消息的详细信息
        /// </summary>
        /// <param name="id">案卷编号</param>
        /// <returns></returns>
        public DataSet GetMsgDetail(int id)
        {
            string strSQL=string.Format(@"SELECT a.*,b.username,c.departname
                                            FROM s_message AS a left join p_user AS b
                                            on a.to_user = b.usercode
                                            left join p_depart AS c 
                                            on b.departcode=c.departcode
                                            WHERE a.id='{0}'",id);
            return this.ExecuteDataset(strSQL);
        }
        #endregion
    }
}
