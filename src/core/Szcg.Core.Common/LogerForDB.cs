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
using System.Collections;
using System.Threading;
using System.Xml;
using System.IO;

namespace Teamax.Common.Loger
{
    /// <summary>
    /// ���ݿ����ϵͳ��־��
    /// </summary>
   
    public class LogerForDB 
    {
        /// <summary>
        /// �洢������
        /// </summary>
        private static readonly string _ProcedureName = "pr_sys_InsertSystemLog";
        private static readonly string _LogType = "DB"; //log����

        private static Queue _QueueLogForBase;
        private static int _iLogNumber;
        private static bool _IsFileLog;
        private static DateTime _LostUpDateTime;
        private static DateTime _LostLoadDeployTime;
        private static int _Level;//0:All,1:DEBUG,2:INFO,3:WARN,4:ERROR,5:FATAL,6:OFF

        /// <summary>
        /// ���췽��
        /// </summary>
        static LogerForDB()
        {
            _QueueLogForBase = new Queue();
            LogUtil.LoadDeploy(_LogType, ref _Level, ref _IsFileLog, ref _iLogNumber, ref _LostLoadDeployTime);
            _LostUpDateTime = DateTime.Now;
        }

        /// <summary>
        /// ���ϵͳ�������־
        /// </summary>
        /// <param name="log"></param>
        private static void AddDBLog(MyLog log)
        {
            DateTime loginTime = DateTime.Now;
            if (((TimeSpan)(loginTime - _LostLoadDeployTime)).TotalMinutes > 10)
            {
                LogUtil.LoadDeploy(_LogType, ref _Level, ref _IsFileLog, ref _iLogNumber,ref _LostLoadDeployTime);                
            }
            LogUtil.AddLog(log,_QueueLogForBase,_iLogNumber,_LostUpDateTime,_IsFileLog,_ProcedureName);
        }

        /// <summary>
        /// DEBUG�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void DEBUG(string sql, string Exception)
        {
            DEBUG(new MyLog(sql, Exception));
        }

        /// <summary>
        /// DEBUG�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void DEBUG(MyLog log)
        {
            if (_Level <= 1)
            {
                log._Level = 1;
                AddDBLog(log);
            }
        }

        /// <summary>
        /// INFO�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void INFO(string sql, string Exception)
        {
            INFO(new MyLog(sql, Exception));
        }

        /// <summary>
        /// INFO�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void INFO(MyLog log)
        {
            if (_Level <= 2)
            {
                log._Level = 2;
                AddDBLog(log);
            }
        }

        /// <summary>
        /// WARN�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void WARN(string sql, string Exception)
        {
            WARN(new MyLog(sql, Exception));
        }

        /// <summary>
        /// WARN�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void WARN(MyLog log)
        {
            if (_Level <= 3)
            {
                log._Level = 3;
                AddDBLog(log);
            }
        }

        /// <summary>
        /// ERROR�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void ERROR(string sql, string Exception)
        {
            ERROR(new MyLog(sql, Exception));
        }

        /// <summary>
        /// ERROR�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void ERROR(MyLog log)
        {
            if (_Level <= 4)
            {
                log._Level = 4;
                AddDBLog(log);
            }
        }

        /// <summary>
        /// FATAL�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void FATAL(string sql, string Exception)
        {
            FATAL(new MyLog(sql, Exception));
        }

        /// <summary>
        /// FATAL�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void FATAL(MyLog log)
        {
            if (_Level <= 5)
            {
                log._Level = 5;
                AddDBLog(log);
            }
        }

        /// <summary>
        /// ALL�ȼ�д��־
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Exception"></param>
        public static void ALL(string sql, string Exception)
        {
            ALL(new MyLog(sql, Exception)); 
        }

        /// <summary>
        /// ALL�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void ALL(MyLog log)
        {
            log._Level = 6;
            AddDBLog(log);
        }
    }
}
