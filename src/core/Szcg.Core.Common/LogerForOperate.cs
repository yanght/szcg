/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：写日志
 * 结构组成：
 * 作    者：袁长工
 * 创建日期：2007-05-21
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
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
    /// 业务操作日志类
    /// </summary>
    public class LogerForOperate
    {
        private static readonly string _ProcedureName = "pr_sys_InsertOperateLog"; //存储过程名
        private static readonly string _LogType = "Operate"; //log类型

        private static Queue _QueueLogForBase;
        private static int _iLogNumber;
        private static bool _IsFileLog;
        private static DateTime _LostUpDateTime;
        private static DateTime _LostLoadDeployTime;
        private static int _Level;//0:All,1:DEBUG,2:INFO,3:WARN,4:ERROR,5:FATAL,6:OFF

        /// <summary>
        /// 构造方法
        /// </summary>
        static LogerForOperate()
        {
            _QueueLogForBase = new Queue();
            LogUtil.LoadDeploy(_LogType, ref _Level, ref _IsFileLog, ref _iLogNumber, ref _LostLoadDeployTime);
            _LostUpDateTime = DateTime.Now;
        }

        /// <summary>
        /// 添加系统级别的日志
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
        /// DEBUG等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void DEBUG(string actioncode, string action)
        {
            DEBUG(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// DEBUG等级写日志
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
        /// INFO等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void INFO(string actioncode, string action)
        {
            INFO(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// INFO等级写日志
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
        /// WARN等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void WARN(string actioncode, string action)
        {
            WARN(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// WARN等级写日志
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
        /// ERROR等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void ERROR(string actioncode, string action)
        {
            ERROR(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// ERROR等级写日志
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
        /// FATAL等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void FATAL(string actioncode, string action)
        {
            FATAL(new MyOperateLog(actioncode, action));
        }

        /// <summary>
        /// FATAL等级写日志
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
        /// ALL等级写日志
        /// </summary>
        /// <param name="actioncode">操作编码</param>
        /// <param name="action">操作</param>
        public static void ALL(string actioncode, string action)
        {
            ALL(new MyOperateLog(actioncode, action)); 
        }

        /// <summary>
        /// ALL等级写日志
        /// </summary>
        /// <param name="log"></param>
        public static void ALL(MyOperateLog log)
        {
            log._Level = 6;
            AddDBLog(log);
        }
    }
}
