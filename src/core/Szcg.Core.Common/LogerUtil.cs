/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：写日志
 * 结构组成：
 * 作    者：袁长工
 * 创建日期：2007-05-21
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：zmn               
 * 修改日期：2007-05-24 
 * 修改说明：补充注释；集合到Teamax.Common组件库   
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;
using System.IO;
using System.Web;
using System.Threading;
using System.Web.Security;


namespace Teamax.Common.Loger
{
    /// <summary>
    /// 系统日志类
    /// </summary>
    public class MyLog
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string _UserCode = "";
        /// <summary>
        /// sql语句
        /// </summary>
        public string _Sql;
        /// <summary>
        /// 当前操作时间
        /// </summary>
        public string _Runtime;
        /// <summary>
        /// 捕获的异常信息
        /// </summary>
        public string _Exception;
        /// <summary>
        /// 客户端的 IP 地址
        /// </summary>
        public string _Ip;
        /// <summary>
        /// 写日志的限制等级
        /// </summary>
        public int _Level;

        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="sql">sql语句</param>
        public MyLog(string sql)
            : this(sql, "")
        {
        }

        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="e">捕获的异常信息</param>
        public MyLog(string sql, string e)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.CurrentHandler is Teamax.Common.CommonPage)
                    _UserCode = (HttpContext.Current.CurrentHandler as Teamax.Common.CommonPage).UserCode;
                else if (HttpContext.Current.Session != null)
                    _UserCode = HttpContext.Current.Session["UserCode"] != null ? HttpContext.Current.Session["UserCode"].ToString() : "";

                _Ip = HttpContext.Current.Request.UserHostAddress;
            }

             _Sql = sql;
            _Exception = e;
            _Runtime = DateTime.Now.ToString();
        }
    }

    /// <summary>
    /// 操作日志类
    /// </summary>
    public class MyOperateLog
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string _UserCode = "";
        /// <summary>
        /// 当前操作时间
        /// </summary>
        public string _Runtime;
        /// <summary>
        /// 动作
        /// </summary>
        public string _Action;
        /// <summary>
        /// 动作编码
        /// </summary>
        public string _ActionCode;
        /// <summary>
        /// 客户端的 IP 地址
        /// </summary>
        public string _Ip;
        /// <summary>
        /// 写日志的限制等级
        /// </summary>
        public int _Level;

        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="sql">sql语句</param>
        public MyOperateLog(string sql)
            : this(sql, "")
        {
        }

        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="actioncode">操作编号</param>
        /// <param name="action">操作</param>
        public MyOperateLog(string actioncode, string action)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.CurrentHandler is Teamax.Common.CommonPage)
                    _UserCode = (HttpContext.Current.CurrentHandler as Teamax.Common.CommonPage).UserCode;
                else if (HttpContext.Current.Session != null)
                    _UserCode = HttpContext.Current.Session["UserCode"] != null ? HttpContext.Current.Session["UserCode"].ToString() : "";

                _Ip = HttpContext.Current.Request.UserHostAddress;
            }

            _ActionCode = actioncode;
            _Action = action;
            _Runtime = DateTime.Now.ToString();
        }

        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="actioncode">操作编号</param>
        /// <param name="action">操作</param>
        /// <param name="usercode">用户编码</param>
        public MyOperateLog(string actioncode, string action, string usercode)
        {
            _UserCode = usercode;

            if (HttpContext.Current != null)
                _Ip = HttpContext.Current.Request.UserHostAddress;

            _ActionCode = actioncode;
            _Action = action;
            _Runtime = DateTime.Now.ToString();
        }
    }


    /// <summary>
    /// 日志通用方法
    /// </summary>
    class LogUtil
    {
        /// <summary>
        /// 装载配置
        /// </summary>
        public static void LoadDeploy(string strLogType, ref int iLevel, ref bool bIsFileLog, ref int iLogNumber, ref DateTime dtLostLoadDeployTime)
        {
            XmlDocument deployDocument = new XmlDocument();
            deployDocument.Load(GetDeployDir() + "\\deploy.xml");
            XmlNode nodeRoot = deployDocument.DocumentElement.SelectSingleNode(strLogType);

            if (nodeRoot == null)
            {
                iLevel = 6;
                bIsFileLog = false;
                iLogNumber = 100;
                return;
            }
            string level = nodeRoot.SelectSingleNode("Level").InnerText;
            if (level != null)
            {
                if (level == "ALL")
                    iLevel = 0;
                else if (level == "DEBUG")
                    iLevel = 1;
                else if (level == "INFO")
                    iLevel = 2;
                else if (level == "WARN")
                    iLevel = 3;
                else if (level == "ERROR")
                    iLevel = 4;
                else if (level == "FATAL")
                    iLevel = 5;
                else if (level == "OFF")
                    iLevel = 6;
            }
            else
            {
                iLevel = 6;
            }

            try
            {
                if (nodeRoot.SelectSingleNode("filelog").InnerText == "1")
                    bIsFileLog = true;
                else
                    bIsFileLog = false;
            }
            catch
            {
                bIsFileLog = false;
            }

            try
            {
                iLogNumber = System.Convert.ToInt32(nodeRoot.SelectSingleNode("lognumber").InnerText);
            }
            catch
            {
                iLogNumber = 100;
            }

            dtLostLoadDeployTime = DateTime.Now;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="QueueLogForBase"></param>
        /// <param name="iLogNumber"></param>
        /// <param name="LostUpDateTime"></param>
        /// <param name="IsFileLog"></param>
        /// <param name="ProcedureName"></param>
        public static void AddLog(object log, Queue QueueLogForBase, int iLogNumber, DateTime LostUpDateTime, bool IsFileLog, string ProcedureName)
        {
            try
            {
                DateTime loginTime = DateTime.Now;

                if (log != null)
                {
                    string strLog = null;
                    Monitor.Enter(QueueLogForBase);
                    QueueLogForBase.Enqueue(log);
                    Monitor.Pulse(QueueLogForBase);
                    Monitor.Exit(QueueLogForBase);
                    if (QueueLogForBase.Count > iLogNumber || (((TimeSpan)(loginTime - LostUpDateTime)).TotalMinutes > 10 && QueueLogForBase.Count > 0))
                    {
                        strLog = UpdateLogBase(loginTime, QueueLogForBase, iLogNumber, ref LostUpDateTime);
                    }
                    if (strLog != null)
                    {
                        try
                        {
                            WriteDBLog(ProcedureName, strLog);
                        }
                        catch
                        {
                            if (IsFileLog)
                            {
                                WriteFileLog(strLog, ProcedureName);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteFileLog(e.Message, ProcedureName);
            }

        }

        /// <summary>
        /// 取得配置目录
        /// </summary>
        /// <returns></returns>
        private static string GetDeployDir()
        {
            return System.Configuration.ConfigurationManager.AppSettings["LogFileSetPath"];
            //return Environment.CurrentDirectory;
            //return HttpContext.Current.Server.MapPath("~\\");
        }

        /// <summary>
        /// 根据消息队列，构造XML字符串
        /// </summary>
        /// <returns></returns>
        private static string UpdateLogBase(DateTime loginTime, Queue QueueLogForBase, int iLogNumber,ref DateTime LostUpDateTime)
        {
            Monitor.Enter(QueueLogForBase);
            if (QueueLogForBase.Count > iLogNumber || (((TimeSpan)(loginTime - LostUpDateTime)).TotalMinutes > 10 && QueueLogForBase.Count > 0))
            {
                string strLog = WriterXml(QueueLogForBase);
                LostUpDateTime = DateTime.Now;
                Monitor.Pulse(QueueLogForBase);
                Monitor.Exit(QueueLogForBase);
                return strLog;
            }
            else
            {
                Monitor.Pulse(QueueLogForBase);
                Monitor.Exit(QueueLogForBase);
                return null;
            }
        }

        /// <summary>
        /// 从消息队列读出内容，生成XML格式的字符串内容
        /// </summary>
        /// <param name="QueueLogForBase"></param>
        /// <returns></returns>
        private static string WriterXml(Queue QueueLogForBase)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartElement("Log");
            object myQueue;
            for (int i = 0; i < QueueLogForBase.Count; i++)
            {
                myQueue = QueueLogForBase.Dequeue();
                if (myQueue.GetType().FullName == "Teamax.Common.Loger.MyLog")
                {
                    MyLog mylog = (MyLog)myQueue;
                    writer.WriteStartElement("Log");
                    writer.WriteAttributeString("UserId", mylog._UserCode);
                    writer.WriteAttributeString("Sql", mylog._Sql);
                    writer.WriteAttributeString("Runtime", mylog._Runtime);
                    writer.WriteAttributeString("Exception", mylog._Exception);
                    writer.WriteAttributeString("Ip", mylog._Ip);
                    writer.WriteEndElement();
                }
                else if (QueueLogForBase.Dequeue().GetType().FullName == "Teamax.Common.Loger.MyOperateLog")
                {
                    MyOperateLog myOperateLog = (MyOperateLog)myQueue;
                    writer.WriteStartElement("Log");
                    writer.WriteAttributeString("UserId", myOperateLog._UserCode);
                    writer.WriteAttributeString("Action", myOperateLog._Action);
                    writer.WriteAttributeString("Runtime", myOperateLog._Runtime);
                    writer.WriteAttributeString("ActionCode", myOperateLog._ActionCode);
                    writer.WriteAttributeString("Ip", myOperateLog._Ip);
                    writer.WriteEndElement();
                }
               
            }
            writer.WriteEndElement();
            writer.Close();
            return sw.ToString();
        }

        /// <summary>
        /// 写日志到数据库中
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="strLog">日志内容</param>
        private static void WriteDBLog(string procedureName, string strLog)
        {
            using (CommonDatabase MyDatabase = new CommonDatabase())
            {
                 SqlParameter Para = new SqlParameter("@LogList", SqlDbType.NText);
                 Para.Value = strLog;
                 MyDatabase.ExecuteNonQuery(procedureName,CommandType.StoredProcedure,false, Para);
            }
        }

        /// <summary>
        /// 写日志到文件中
        /// </summary>
         /// <param name="logInfo">日志信息</param>
        /// <param name="LogFileName">日志文件名称</param>
        private static void WriteFileLog(string logInfo, string LogFileName)
        {
            StreamWriter outStream = new StreamWriter(GetDeployDir() + "\\" + LogFileName + ".txt", true, System.Text.Encoding.Default);
            outStream.Write("/n" + logInfo);
            outStream.Close();
        }

    }
}
