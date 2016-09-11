/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：综合业务系统－消息相关－逻辑层访问类。

 * 结构组成：

 * 作    者：yannis
 * 创建日期：2007-05-25
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using AjaxPro;
using Teamax.Common;

namespace bacgBL.business
{
    /// <summary>
    /// 综合业务系统－消息相关的逻辑层访问类。

    /// </summary>
    public class Message
    {
        #region IsNewMessage：判断是否有新消息

        /// <summary>
        /// 判断是否有新消息
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <param name="strAreaCode"></param>
        /// <returns></returns>
        [AjaxMethod]
        public string IsNewMessage(string strUserCode ,string strAreaCode)
        {
            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    return dl.IsNewMessage(strUserCode, strAreaCode);
                }
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region IsNewProject：判断是否有新案件

        /// <summary>
        /// 判断是否有新案件
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <param name="strAreaCode"></param>
        /// <returns></returns>
        [AjaxMethod]
        public string IsNewProject(string strStepId, string strAreaCode, string strDepartCode)
        {
            string strCacheKey = "IsNewProject_StepId_" + strStepId + "_Area_" + strAreaCode + "_Depart_" + strDepartCode;
            string strValue = (string)MyCache.Get(strCacheKey);//获取缓存信息
            if (strValue != null)
                return strValue;

            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    strValue = dl.IsNewProject(strStepId, strAreaCode, strDepartCode);
                    strValue = bacgBL.Pub.Tools.StrConv(strValue, "GB2312"); //编码
                    MyCache.Insert(strCacheKey, strValue, 60); //12秒刷新一次，其实最终是1分钟。


                    return strValue;
                }
            }
            catch
            {
                return "";
            }
        }
         [AjaxMethod]
        public string LIsNewProject(string strStepId, string strAreaCode, string strDepartCode)
        {
            string strCacheKey = "LIsNewProject_StepId_" + strStepId + "_Area_" + strAreaCode + "_Depart_" + strDepartCode;
            string strValue = (string)MyCache.Get(strCacheKey);//获取缓存信息
            if (strValue != null)
                return strValue;

            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    strValue = dl.LIsNewProject(strStepId, strAreaCode, strDepartCode);
                    strValue = bacgBL.Pub.Tools.StrConv(strValue, "GB2312"); //编码
                    MyCache.Insert(strCacheKey, strValue, 60); //12秒刷新一次，其实最终是1分钟。


                    return strValue;
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region SendMsgtoCol:发送消息给监督员,电话列表用逗号分割.
        /// <summary>
        /// 发送消息给监督员,电话列表用逗号分割.
        /// </summary>
        /// <param name="phones">电话号码列表</param>
        /// <param name="msgcontent">消息内容</param>
        /// <returns></returns>
        [AjaxMethod]
        public static string SendMsgtoCol(string phones, string msgcontent)
        {
            try
            {
                string[] phone = phones.Split(',');
                for (int i = 0; i < phone.Length; i++)
                {
                    using (bacgDL.business.Message dl = new bacgDL.business.Message())
                    {
                        dl.SendMessage("s",phone[i],msgcontent);
                    }
                }
                return "1";
            }
            catch  
            {
                return "0";
            }
        }
        #endregion

        #region GetProjNum：获取案件统计信息（监督员在岗人数、结案数，正在处理数、立案数）等信息
        /// <summary>
        /// 监督员在岗人数、结案数，正在处理数、立案数
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public static string GetProjNum(string areacode)
        {
            string retString = "";

            try
            {
                using (bacgDL.business.Message dl = new bacgDL.business.Message())
                {
                    DataSet ds = dl.GetProjNum(areacode);
                    if(ds.Tables.Count > 1)
                    {
                        //监督员在岗人数、结案数，正在处理数、立案数
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            retString += dr["num"]+",";
                        }
                        retString = retString.Trim(',');

                        return retString;
                    }
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region GetProjNum_UseCatch：使用缓存来获取案件统计信息
        /// <summary>
        /// 获取案件统计信息（监督员在岗人数、结案数，正在处理数、立案数）等信息，改方法使用了缓存

        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        [AjaxMethod]
        public static string GetProjNum_UseCatch(string areacode)
        {
            string strCacheKey = "GetProjNum_UseCatch_" + areacode;
            string strValue = (string)MyCache.Get(strCacheKey);//获取缓存信息
            if (strValue != null)
                return strValue;

            strValue = GetProjNum(areacode);
            if(strValue!="")
                MyCache.Insert(strCacheKey, strValue, 24); //MyCache的Insert方法默认把秒×5，24×5=120秒，保留2分钟
            return strValue;
        }
        #endregion

        #region GetSmsSendNum：校验当日短信发送量是否超出限制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="SmsMaxNum"></param>
        /// <returns>是否超过限制。如果为真，那么达到了限制</returns>
        public bool GetSmsSendNum(string usercode, out string SmsMaxNum)
        {
            SmsMaxNum = "";
            bool bReturn = false;

            using (bacgDL.business.Message dl = new bacgDL.business.Message())
            {
                IDataReader dr = dl.GetSmsSendNum(usercode);
                if (dr.Read())
                {
                    bReturn = dr[0].ToString() == "1"; 
                    SmsMaxNum = dr[1].ToString();
                }
                dr.Close();
            }
            return bReturn;
        }
        #endregion

        #region SendMsgtoCols:发送消息给监督员,电话列表用逗号分割.
        /// <summary>
        /// 发送消息给监督员,电话列表用逗号分割.
        /// </summary>
        /// <param name="phones">电话号码列表</param>
        /// <param name="msgcontent">消息内容</param>
        /// <returns></returns>
        public static int SendMsgtoCols(string phones, string msgcontent)
        {
            int retult = 1;
            try
            {
                string[] phone = phones.Split(',');
                for (int i = 0; i < phone.Length; i++)
                {
                    using (bacgDL.business.Message dl = new bacgDL.business.Message())
                    {
                        dl.SendMessage("s", phone[i], msgcontent);
                    }
                }
                retult = 0;
            }
            catch
            {
                retult = 1;
            }
            return retult;
        }
        #endregion
    }
}
