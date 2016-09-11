using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using szcg.com.teamax.business.entity;

namespace bacgBL.business.wdxxmanage
{
    #region MsgInfo:������Ϣ�ṹ��
    public class MsgInfo
    {

        private int _id;
        /*ϵͳ��� */
        private int _Sysmsgid;
        /*�����û����� */
        private int _go_user;

        /*�����û����� */
        private int _to_user;
        /*���� */
        private string _msgtitle;
        /*���� */
        private string _msgcontent;
        /*����ʱ�� */
        private string _cu_date;
        /*�Ƿ������ */
        private string _isread;
        /*��������*/
        private string _departname;
        /*����������*/
        private string _username;
        /*������*/
        private string _projcode;
        /*��������*/
        private string _projname;
        /*�ලԱ����*/
        private string _collcode;
        /*�ලԱ����*/
        private string _collname;
        private string _codes;
        /*����·��*/
        private string _attachUrl;
        /*��Ϣ����*/
        private string _msgtype;


        public MsgInfo()
        {
        }
        /// <summary>
        /// zhaghuagen
        /// </summary>
        /// <returns></returns>

        public String getProjname()
        {
            return _projname;
        }

        public string getCodes()
        {
            return _codes;
        }

        public void setCodes(string codes)
        {
            this._codes = codes;
        }

        public void setProjname(String projname)
        {
            this._projname = projname;
        }

        public int getId()
        {
            return _id;
        }
        public void setId(int id)
        {
            this._id = id;
        }


        public int getSysmsgid()
        {
            return _Sysmsgid;
        }
        public void setSysmsgid(int Sysmsgid)
        {
            this._Sysmsgid = Sysmsgid;
        }

        public int getGo_user()
        {
            return _go_user;
        }

        public void setGo_user(int go_user)
        {
            this._go_user = go_user;
        }

        public int getTo_user()
        {
            return _to_user;
        }

        public void setTo_user(int to_user)
        {
            this._to_user = to_user;
        }

        public string getMsgtitle()
        {
            return _msgtitle;
        }
        public void setMsgtitle(String msgtitle)
        {
            this._msgtitle = msgtitle;
        }

        public string getMsgcontent()
        {
            return _msgcontent;
        }
        public void setMsgcontent(String msgcontent)
        {
            this._msgcontent = msgcontent;
        }
        public void setAttachUrl(String attachUrl)
        {
            this._attachUrl = attachUrl;
        }
        public void setMsgtype(String msgtype)
        {
            this._msgtype = msgtype;
        }


        public string getCu_date()
        {
            return _cu_date;
        }
        public void setCu_date(String cu_date)
        {
            this._cu_date = cu_date;
        }

        public string getIsread()
        {
            return _isread;
        }
        public void setIsread(String isread)
        {
            this._isread = isread;
        }

        public string getDepartname()
        {
            return _departname;
        }

        public void setDepartname(String departname)
        {
            this._departname = departname;
        }

        public string getUsername()
        {
            return _username;
        }

        public void setUsername(String username)
        {
            this._username = username;
        }
        public string getProjcode()
        {
            return _projcode;
        }

        public void setProjcode(String projcode)
        {
            this._projcode = projcode;
        }
        public string getCollcode()
        {
            return _collcode;
        }

        public string getAttachUrl()
        {
            return this._attachUrl;
        }
        public string getMsgtype()
        {
            return this._msgtype;
        }

        public void setCollcode(String collcode)
        {
            this._collcode = collcode;
        }
        public string getCollname()
        {
            return _collname;
        }

        public void setCollname(String collname)
        {
            this._collname = collname;
        }



        public void reset()
        {
            _id = 0;
            _Sysmsgid = 0;
            _go_user = 0;
            _to_user = 0;

            _msgtitle = "";
            _msgcontent = "";
            _attachUrl = "";

        }

    }
    #endregion

    public class wdxx
    {
        #region InsertBusinessMsg:�½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertBusinessMsg(string gousercode, string tousercode, string msgtitle, string msgcontent, ref string strErr)
        {
            return InsertBusinessMsg(gousercode, tousercode, msgtitle, msgcontent, "", "", ref strErr);
        }

        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertBusinessMsg(string gousercode, string tousercode, string msgtitle, string msgcontent,string refid,string msgtype,ref string strErr)
        {
            try
            {
                using (bacgDL.business.wdxx dl = new bacgDL.business.wdxx())
                {
                    return dl.InsertBusinessMsg(gousercode, tousercode, msgtitle, msgcontent, refid, msgtype);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertGroupMsg:���뷢��Ⱥ����Ϣ
        /// <summary>
        /// ����Ⱥ����Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertGroupMsg(string gousercode, string tousercode, string msgtitle, string msgcontent, ref string strErr)
        {
            return InsertGroupMsg(gousercode, tousercode, msgtitle, msgcontent, "", "", ref strErr);
        }

        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertGroupMsg(string gousercode, string tousercode, string msgtitle, string msgcontent, string refid, string msgtype, ref string strErr)
        {
            try
            {
                using (bacgDL.business.wdxx dl = new bacgDL.business.wdxx())
                {
                    return dl.InsertGroupMsg(gousercode, tousercode, msgtitle, msgcontent, refid, msgtype);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetMsgDetail:���ݰ�����,��ȡ��Ϣ����ϸ��Ϣ
        /// <summary>
        /// ���ݰ�����,��ȡ��Ϣ����ϸ��Ϣ
        /// </summary>
        /// <param name="id">������</param>
        /// <returns></returns>
        public MsgInfo GetMsgDetail(int id, ref string strErr)
        {
            MsgInfo minfo = new MsgInfo();
            try
            {
                using (bacgDL.business.wdxx dl = new bacgDL.business.wdxx())
                {
                    DataSet ds = dl.GetMsgDetail(id);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dt in ds.Tables[0].Rows)
                        {
                            minfo.setId(int.Parse(dt["id"].ToString()));
                            minfo.setSysmsgid(int.Parse(dt["Sysmsgid"].ToString()));
                            minfo.setGo_user(int.Parse(dt["to_user"].ToString()));
                            minfo.setTo_user(int.Parse(dt["go_user"].ToString()));
                            minfo.setMsgtitle(dt["msgtitle"].ToString());
                            minfo.setMsgcontent(dt["msgcontent"].ToString());
                            minfo.setCu_date(dt["cu_date"].ToString());
                            minfo.setIsread(dt["isread"].ToString());
                            minfo.setUsername(dt["username"].ToString());
                            minfo.setDepartname(dt["departname"].ToString());
                            minfo.setAttachUrl(dt["AttachUrl"].ToString());
                            minfo.setMsgtype(dt["msgtype"].ToString());
                        }
                        return minfo;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserPwd�������û�����õ��û�������
        /// <summary>
        ///�����û�����õ��û�������
        /// </summary>
        /// <param name="usercode">�û����</param>
        /// <param name="ErrMsg">���������Ϣ</param>
        public static string GetUserPwd(string usercode, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"select password from p_user
                                            where usercode = '{0}'", usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecScalarSql(strSQL).ToString();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return "";
            }
        }
        #endregion

        #region UpdateUserPwd�������û���������û�������
        /// <summary>
        ///�����û�����õ��û�������
        /// </summary>
        ///  <param name="pwd">�µ�����</param>
        /// <param name="usercode">�û����</param>
        /// <param name="ErrMsg">���������Ϣ</param>
        public static int UpdateUserPwd(string pwd, string usercode, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"update  p_user set password = '{0}'
                                            where usercode = '{1}'", pwd,usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return 0;
            }
        }
        #endregion

        #region GetUserInfo�������û������ȡ�û���Ϣ
        /// <summary>
        ///�����û������ȡ�û���Ϣ
        /// </summary>
        /// <param name="usercode">�û����</param>
        /// <param name="ErrMsg">���������Ϣ</param>
        public static UserInfo GetUserInfo(string usercode, out string ErrMsg)
        {
            ErrMsg = "";
            UserInfo userInfo = new UserInfo();
            string strSQL = string.Format(@"select * from p_user a left join  p_depart b
                                            on a.departcode=b.departcode
                                            where usercode='{0}'",usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds= dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            userInfo.setUsername(dr["UserName"].ToString());
                            userInfo.setDepartname(dr["departname"].ToString());
                            userInfo.setBirthday(dr["birthday"].ToString());
                            userInfo.setAddress(dr["address"].ToString());
                            userInfo.setEmail(dr["email"].ToString());
                            userInfo.setTel(dr["hometel"].ToString());
                            userInfo.setMobile(dr["mobile"].ToString());
                        }
                        return userInfo;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region UpdateUserInfo�������û���������û���Ϣ
        /// <summary>
        ///�����û���������û���Ϣ
        /// </summary>
        /// <param name="usercode">�û����</param>
        /// <param name="ErrMsg">���������Ϣ</param>
        public static int UpdateUserInfo(UserInfo userInfo,string usercode, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"update p_user set UserName='{0}',birthday='{1}',address='{2}',
                                                                email='{3}',hometel='{4}',mobile='{5}'
                                            where usercode='{6}'",userInfo.getUsername(),userInfo.getBirthday(),userInfo.getAddress(),
                                                                    userInfo.getEmail(),userInfo.getTel(),userInfo.getMobile(),
                                                                    usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return 0;
            }
        }
        #endregion
    }
}
