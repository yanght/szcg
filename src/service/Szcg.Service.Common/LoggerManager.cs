using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Szcg.Service.Common
{
    public class LoggerManager
    {
        private static LoggerManager _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();

        LoggerManager()
        {
        }

        public static LoggerManager Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new LoggerManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public ILog logger
        {
            get
            {
                return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
        }
    }
}
