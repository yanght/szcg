using System;
using System.Collections.Generic;
using System.Text;
using szcg.com.teamax.util;

namespace bacgBL.business.wdxxmanage
{
    public class wdxxAjax
    {
        #region updateHelpstate:智能报警
        /// <summary>
        /// 智能报警
        /// </summary>
        /// <param name="id">更新的ID</param>
        /// <param name="usercode">更新用户(To)</param>
        [AjaxPro.AjaxMethod]
        public string updateHelpstate(string id, string usercode)
        {
            try
            {
                string sql = "update m_call_help set state = '1',usercode = '" + usercode + "' where id = '" + id + "'";
                DataAccess.ExecuteNonQuery(sql, null);
                return "1";
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return "0";
            }
        }
        #endregion
    }
}
