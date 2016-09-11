/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���ҵ���Ϣ-���ݲ�����ࡣ
 * �ṹ��ɣ�
 * ��    �ߣ�����Ⱥ
 * �������ڣ�2007-06-17
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
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

        #region InsertBusinessMsg:�½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        /// <param name="msgcontent">�ظ�id��</param>
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

        #region InsertBusinessMsg:���뷢��Ⱥ����Ϣ
        /// <summary>
        /// ���뷢��Ⱥ����Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        /// <param name="msgcontent">�ظ�id��</param>
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

        #region GetMsgDetail:���ݰ�����,��ȡ��Ϣ����ϸ��Ϣ
        /// <summary>
        /// ���ݰ�����,��ȡ��Ϣ����ϸ��Ϣ
        /// </summary>
        /// <param name="id">������</param>
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
