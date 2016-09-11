/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;��д��־
 * �ṹ��ɣ�
 * ��    �ߣ�Ԭ����
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��zmn               
 * �޸����ڣ�2007-05-24 
 * �޸�˵��������ע�ͣ����ϵ�Teamax.Common�����   
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵���� 
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
    /// ϵͳ��־��
    /// </summary>
    public class MyLog
    {
        /// <summary>
        /// �û����
        /// </summary>
        public string _UserCode = "";
        /// <summary>
        /// sql���
        /// </summary>
        public string _Sql;
        /// <summary>
        /// ��ǰ����ʱ��
        /// </summary>
        public string _Runtime;
        /// <summary>
        /// ������쳣��Ϣ
        /// </summary>
        public string _Exception;
        /// <summary>
        /// �ͻ��˵� IP ��ַ
        /// </summary>
        public string _Ip;
        /// <summary>
        /// д��־�����Ƶȼ�
        /// </summary>
        public int _Level;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sql">sql���</param>
        public MyLog(string sql)
            : this(sql, "")
        {
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="e">������쳣��Ϣ</param>
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
    /// ������־��
    /// </summary>
    public class MyOperateLog
    {
        /// <summary>
        /// �û����
        /// </summary>
        public string _UserCode = "";
        /// <summary>
        /// ��ǰ����ʱ��
        /// </summary>
        public string _Runtime;
        /// <summary>
        /// ����
        /// </summary>
        public string _Action;
        /// <summary>
        /// ��������
        /// </summary>
        public string _ActionCode;
        /// <summary>
        /// �ͻ��˵� IP ��ַ
        /// </summary>
        public string _Ip;
        /// <summary>
        /// д��־�����Ƶȼ�
        /// </summary>
        public int _Level;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sql">sql���</param>
        public MyOperateLog(string sql)
            : this(sql, "")
        {
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="actioncode">�������</param>
        /// <param name="action">����</param>
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
        /// ������
        /// </summary>
        /// <param name="actioncode">�������</param>
        /// <param name="action">����</param>
        /// <param name="usercode">�û�����</param>
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
    /// ��־ͨ�÷���
    /// </summary>
    class LogUtil
    {
        /// <summary>
        /// װ������
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
        /// д����־
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
        /// ȡ������Ŀ¼
        /// </summary>
        /// <returns></returns>
        private static string GetDeployDir()
        {
            return System.Configuration.ConfigurationManager.AppSettings["LogFileSetPath"];
            //return Environment.CurrentDirectory;
            //return HttpContext.Current.Server.MapPath("~\\");
        }

        /// <summary>
        /// ������Ϣ���У�����XML�ַ���
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
        /// ����Ϣ���ж������ݣ�����XML��ʽ���ַ�������
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
        /// д��־�����ݿ���
        /// </summary>
        /// <param name="procedureName">�洢��������</param>
        /// <param name="strLog">��־����</param>
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
        /// д��־���ļ���
        /// </summary>
         /// <param name="logInfo">��־��Ϣ</param>
        /// <param name="LogFileName">��־�ļ�����</param>
        private static void WriteFileLog(string logInfo, string LogFileName)
        {
            StreamWriter outStream = new StreamWriter(GetDeployDir() + "\\" + LogFileName + ".txt", true, System.Text.Encoding.Default);
            outStream.Write("/n" + logInfo);
            outStream.Close();
        }

    }
}
