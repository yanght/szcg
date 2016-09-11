/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;��д��־
 * �ṹ��ɣ�
 * ��    �ߣ�Ԭ����
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
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
    /// ҵ�������־��
    /// </summary>
    public class LogerForOperate
    {
        private static readonly string _ProcedureName = "pr_sys_InsertOperateLog"; //�洢������
        private static readonly string _LogType = "Operate"; //log����

        private static Queue _QueueLogForBase;
        private static int _iLogNumber;
        private static bool _IsFileLog;
        private static DateTime _LostUpDateTime;
        private static DateTime _LostLoadDeployTime;
        private static int _Level;//0:All,1:DEBUG,2:INFO,3:WARN,4:ERROR,5:FATAL,6:OFF

        /// <summary>
        /// ���췽��
        /// </summary>
        static LogerForOperate()
        {
            _QueueLogForBase = new Queue();
            LogUtil.LoadDeploy(_LogType, ref _Level, ref _IsFileLog, ref _iLogNumber, ref _LostLoadDeployTime);
            _LostUpDateTime = DateTime.Now;
        }

        /// <summary>
        /// ���ϵͳ�������־
        /// </summary>
        /// <param name="log"></param>
        private static void AddDBLog(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void DEBUG(string actioncode, string action)
        {
            DEBUG(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// DEBUG�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void DEBUG(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void INFO(string actioncode, string action)
        {
            INFO(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// INFO�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void INFO(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void WARN(string actioncode, string action)
        {
            WARN(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// WARN�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void WARN(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void ERROR(string actioncode, string action)
        {
            ERROR(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// ERROR�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void ERROR(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void FATAL(string actioncode, string action)
        {
            FATAL(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// FATAL�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void FATAL(MyOperateLog log)
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
        /// <param name="actioncode">��������</param>
        /// <param name="action">����</param>
        public static void ALL(string actioncode, string action)
        {
            ALL(new MyOperateLog(actioncode, action)); 
        }

        /// <summary>
        /// ALL�ȼ�д��־
        /// </summary>
        /// <param name="log"></param>
        public static void ALL(MyOperateLog log)
        {
            log._Level = 6;
            AddDBLog(log);
        }
    }
}
