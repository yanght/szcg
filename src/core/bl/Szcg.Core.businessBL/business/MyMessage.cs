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

        #region GetMsgList:获取消息列表
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="usercode">当前用户编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
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

        #region DeleteMsg:删除消息
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id">消息id</param>
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

        #region GetMsgInfo:获取消息信息
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="id">消息编码</param>
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

        #region SetIsRead:设置已经读取了消息
        /// <summary>
        /// 设置已经读取了消息
        /// </summary>
        /// <param name="id">消息编码</param>
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

        #region GetPDAMsgList:获取PDA消息列表
        /// <summary>
        /// 获取PDA消息列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
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

        #region GetPDAMsgInfo:获取PDA消息信息
        /// <summary>
        /// 获取PDA消息信息
        /// </summary>
        /// <param name="id">消息编码</param>
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

        #region GetPDAProjectMsgInfo:获取PDA案件的消息信息
        /// <summary>
        /// 获取PDA案件的消息信息
        /// </summary>
        /// <param name="id">案件编号</param>
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

        #region SetPDAIsRead:设置已经读取了PDA消息
        /// <summary>
        /// 设置已经读取了PDA消息
        /// </summary>
        /// <param name="projcode">案卷编号</param>
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

        #region GetOtherMsgList:获取其他消息列表
        /// <summary>
        /// 获取其他消息列表
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
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

        #region DeleteOtherMsg:删除其他消息
        /// <summary>
        /// 删除其他消息
        /// </summary>
        /// <param name="id">消息id</param>
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

        #region GetOtherMsgInfo:获取其他消息信息
        /// <summary>
        /// 获取其他消息信息
        /// </summary>
        /// <param name="id">消息编码</param>
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

        #region GetHelpMsgList:获取帮助消息列表
        /// <summary>
        /// 获取帮助消息列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
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

        #region DeleteHelpMsg:删除帮助消息
        /// <summary>
        /// 删除帮助消息
        /// </summary>
        /// <param name="id">消息id</param>
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

        #region GetUserData:根据区域信息,获取用户信息树
        /// <summary>
        /// 根据区域信息,获取用户信息树
        /// </summary>
        /// <param name="areacode">区域编码</param>
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
