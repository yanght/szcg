using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.szbase.grid
{
    public class GridManage:Teamax.Common.CommonDatabase
    {
        #region GetGridTreeList��ȡ����������Ϣ�������ֵ�������������
        /// <summary>
        /// ȡ����������Ϣ�������ֵ�������������
        /// </summary>
        /// <param name="areacode">�������</param>
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
