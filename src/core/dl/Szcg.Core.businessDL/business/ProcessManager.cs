/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：案件受理数据层公共访问类。
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
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace bacgDL.business
{
    public class ProcessManager : Teamax.Common.CommonDatabase
    {
        #region GetActionWorkFlow：获取业务工作流程
        /// <returns></returns>
        public IDataReader GetActionWorkFlow()
        {
            return this.ExecuteReader("select * from s_action");
        }
        #endregion
    }
}
