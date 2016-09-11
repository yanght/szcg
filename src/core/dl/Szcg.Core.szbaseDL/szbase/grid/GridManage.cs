using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.szbase.grid
{
    public class GridManage:Teamax.Common.CommonDatabase
    {
        #region GetGridTreeList：取得网格树信息（区，街道，社区，网格）
        /// <summary>
        /// 取得网格树信息（区，街道，社区，网格）
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetGridTreeList(string areacode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{new SqlParameter("@areacode",areacode)};
            DataSet ds = this.ExecuteDataset("pr_p_GetDGridList", CommandType.StoredProcedure, arrSP);
            return ds;
        }
        #endregion
    }
}
