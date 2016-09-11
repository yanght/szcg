using System;
using System.Collections.Generic;
using System.Text;
using bacgDL;
using System.Data;
using System.Collections;
using Teamax.Common;
using System.Data.SqlClient;

namespace bacgBL.system
{
    public class StructQuery
    {
        public int intWeeks = 0;
        public int intMonths = 0;
        public int intYears = 0;
        public int intQuarter = 0;
        public DateTime startDate = new DateTime();
        public DateTime endDate = new DateTime();
    }
    public class systemRun 
    {
        #region  iis重启后根据webconfig配置初始化函数
        /// <summary>
        /// iis重启后根据webconfig配置初始化函数
        /// </summary>
        /// <returns></returns>
        static public void systemIISreset()
        {
           

        }
        #endregion

    }
}
