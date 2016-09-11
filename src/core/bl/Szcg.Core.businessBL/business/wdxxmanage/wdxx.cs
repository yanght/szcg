using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using szcg.com.teamax.business.entity;

namespace bacgBL.business.wdxxmanage
{
    #region MsgInfo:案卷消息结构体
    public class MsgInfo
    {

        private int _id;
        /*系统编号 */
        private int _Sysmsgid;
        /*发送用户编码 */
        private int _go_user;

        /*接收用户编码 */
        private int _to_user;
        /*主题 */
        private string _msgtitle;
        /*内容 */
        private string _msgcontent;
        /*发送时间 */
        private string _cu_date;
        /*是否被浏览过 */
        private string _isread;
        /*部门名称*/
        private string _departname;
        /*发送人姓名*/
        private string _username;
        /*案卷编号*/
        private string _projcode;
        /*案卷名称*/
        private string _projname;
        /*监督员工号*/
        private string _collcode;
        /*监督员姓名*/
        private string _collname;
        private string _codes;
        /*附件路径*/
        private string _attachUrl;
        /*消息类型*/
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
        #region InsertBusinessMsg:新建业务平台消息。同时插入业务平台回复信息
        /// <summary>
        /// 新建业务平台消息。同时插入业务平台回复信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
        public int InsertBusinessMsg(string gousercode, string tousercode, string msgtitle, string msgcontent, ref string strErr)
        {
            return InsertBusinessMsg(gousercode, tousercode, msgtitle, msgcontent, "", "", ref strErr);
        }

        /// <summary>
        /// 新建业务平台消息。同时插入业务平台回复信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
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

        #region InsertGroupMsg:插入发送群组信息
        /// <summary>
        /// 插入群组信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
        public int InsertGroupMsg(string gousercode, string tousercode, string msgtitle, string msgcontent, ref string strErr)
        {
            return InsertGroupMsg(gousercode, tousercode, msgtitle, msgcontent, "", "", ref strErr);
        }

        /// <summary>
        /// 新建业务平台消息。同时插入业务平台回复信息
        /// </summary>
        /// <param name="gousercode">来自人(From)</param>
        /// <param name="tousercode">接收人(To)</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msgcontent">消息内容</param>
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

        #region GetMsgDetail:根据案卷编号,获取消息的详细信息
        /// <summary>
        /// 根据案卷编号,获取消息的详细信息
        /// </summary>
        /// <param name="id">案卷编号</param>
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

        #region GetUserPwd：根据用户编码得到用户的密码
        /// <summary>
        ///根据用户编码得到用户的密码
        /// </summary>
        /// <param name="usercode">用户编号</param>
        /// <param name="ErrMsg">输出错误信息</param>
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

        #region UpdateUserPwd：根据用户编码更改用户的密码
        /// <summary>
        ///根据用户编码得到用户的密码
        /// </summary>
        ///  <param name="pwd">新的密码</param>
        /// <param name="usercode">用户编号</param>
        /// <param name="ErrMsg">输出错误信息</param>
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

        #region GetUserInfo：根据用户编码获取用户信息
        /// <summary>
        ///根据用户编码获取用户信息
        /// </summary>
        /// <param name="usercode">用户编号</param>
        /// <param name="ErrMsg">输出错误信息</param>
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

        #region UpdateUserInfo：根据用户编码更新用户信息
        /// <summary>
        ///根据用户编码更新用户信息
        /// </summary>
        /// <param name="usercode">用户编号</param>
        /// <param name="ErrMsg">输出错误信息</param>
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
