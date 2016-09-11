using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using DBbase.szbase;
using DBbase.business.group;

namespace bacgBL.business
{
    public class MyMessage
    {

        #region GetMsgList:��ȡ��Ϣ�б�
        /// <summary>
        /// ��ȡ��Ϣ�б�
        /// </summary>
        /// <param name="usercode">��ǰ�û�����</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <returns></returns>
        public static DataSet GetMsgList(int usercode, int curentpage, int pagesize, string Order, string Field,string username,string begintime,string endtime, ref int rowCount,
                                                    ref int pageCount,ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetMsgList(usercode, curentpage, pagesize, Order, Field, username, begintime, endtime, ref rowCount, ref pageCount);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteMsg:ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="id">��Ϣid</param>
        /// <returns></returns>
        public static void DeleteMsg(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    dl.DeleteMsg(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }
        #endregion

        #region GetMsgInfo:��ȡ��Ϣ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ�б�
        /// </summary>
        /// <param name="id">��Ϣ����</param>
        /// <returns></returns>
        public static DataSet GetMsgInfo(string id,ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetMsgInfo(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region SetIsRead:�����Ѿ���ȡ����Ϣ
        /// <summary>
        /// �����Ѿ���ȡ����Ϣ
        /// </summary>
        /// <param name="id">��Ϣ����</param>
        /// <returns></returns>
        public static int SetIsRead(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.SetIsRead(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetPDAMsgList:��ȡPDA��Ϣ�б�
        /// <summary>
        /// ��ȡPDA��Ϣ�б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <returns></returns>
        public static DataSet GetPDAMsgList(string areacode, int curentpage, int pagesize,string collname,string begintime,string endtime,string projcode, ref int rowCount,string Order,string Field,
                                                    ref int pageCount,string streetcode, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetPDAMsgList(areacode, curentpage, pagesize, collname, begintime, endtime, projcode, Order, Field, ref rowCount, ref pageCount, streetcode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetPDAMsgInfo:��ȡPDA��Ϣ��Ϣ
        /// <summary>
        /// ��ȡPDA��Ϣ��Ϣ
        /// </summary>
        /// <param name="id">��Ϣ����</param>
        /// <returns></returns>
        public static DataSet GetPDAMsgInfo(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetPDAMsgInfo(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetPDAProjectMsgInfo:��ȡPDA��������Ϣ��Ϣ
        /// <summary>
        /// ��ȡPDA��������Ϣ��Ϣ
        /// </summary>
        /// <param name="id">�������</param>
        /// <returns></returns>
        public static DataSet GetPDAProjectMsgInfo(string projectcode, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetPDAProjectMsgInfo(projectcode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region SetPDAIsRead:�����Ѿ���ȡ��PDA��Ϣ
        /// <summary>
        /// �����Ѿ���ȡ��PDA��Ϣ
        /// </summary>
        /// <param name="projcode">������</param>
        /// <returns></returns>
        public static int SetPDAIsRead(string projcode,ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.SetPDAIsRead(projcode);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetOtherMsgList:��ȡ������Ϣ�б�
        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <param name="usercode">�û�����</param>
        /// <param name="areacode">�������</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <returns></returns>
        public static DataSet GetOtherMsgList(string usercode,string areacode, int curentpage,
                                                int pagesize,string username,string collname,string begintime,string endtime, string Order, string Field, ref int rowCount, ref int pageCount, 
                                                ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetOtherMsgList(usercode, areacode, curentpage, pagesize, username, collname, begintime, endtime,Order, Field, ref rowCount, ref pageCount);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteOtherMsg:ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="id">��Ϣid</param>
        /// <returns></returns>
        public static void DeleteOtherMsg(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    dl.DeleteOtherMsg(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }
        #endregion

        #region GetOtherMsgInfo:��ȡ������Ϣ��Ϣ
        /// <summary>
        /// ��ȡ������Ϣ��Ϣ
        /// </summary>
        /// <param name="id">��Ϣ����</param>
        /// <returns></returns>
        public static DataSet GetOtherMsgInfo(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetOtherMsgInfo(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetHelpMsgList:��ȡ������Ϣ�б�
        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="curentpage">��ǰҳ</param>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <param name="pageCount">��ҳ��</param>
        /// <returns></returns>
        public static DataSet GetHelpMsgList(string areacode, int curentpage, int pagesize,string usercode,string collname,string begintime,string endtime,string Order,string Field,
                                                ref int rowCount, ref int pageCount,ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.GetHelpMsgList(areacode, curentpage, pagesize, usercode, collname, begintime, endtime, Order, Field, ref rowCount, ref pageCount);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteHelpMsg:ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="id">��Ϣid</param>
        /// <returns></returns>
        public static void DeleteHelpMsg(string id, ref string strErr)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    dl.DeleteHelpMsg(id);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }
        #endregion

        #region GetUserData:����������Ϣ,��ȡ�û���Ϣ��
        /// <summary>
        /// ����������Ϣ,��ȡ�û���Ϣ��
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public static ArrayList GetUserData(string areacode, ref string strErr)
        {
            try
            {
                ArrayList TreeStructList = new ArrayList();
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    DataSet ds = dl.GetUserData(areacode);
                    if (ds.Tables.Count > 1)
                    { 

                        foreach(DataRow dr in ds.Tables[0].Rows)
                        {
		                    DBbase.szbase.TreeSuruct ts;
		                    ts.pcode=dr["parentcode"].ToString();
		                    ts.code=dr["departcode"].ToString();
		                    ts.text=dr["departname"].ToString();
		                    ts.tag="";
                            TreeStructList.Add(ts);
                        }
                        foreach(DataRow dr in ds.Tables[1].Rows)
                        {
		                    DBbase.szbase.TreeSuruct ts;
		                    ts.pcode=dr["departcode"].ToString();
		                    ts.code=dr["usercode"].ToString()+"8888";
		                    ts.text=dr["username"].ToString();
		                    ts.tag=dr["mobile"].ToString();
                            TreeStructList.Add(ts);
                        }
                        return TreeStructList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
