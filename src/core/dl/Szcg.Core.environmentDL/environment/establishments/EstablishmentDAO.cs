/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统人员数据库操作


 * 结构组成：


 * 作    者：吴林波


 * 创建日期：2007-05-26
 * 历史记录：


 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using Teamax.Common;

namespace bacgDL.environment.establishments
{
    public class EstablishmentDAO
    {
        #region GetEstablishmentListByType ：　根据环卫设施类型得到设施类表

        public Teamax.Common.PageManage GetEstablishmentListByType(string table, Establishment per, string select, int pageIndex, int pageSize)
        {
            string[] len = per.deptname.Split(',');
            string[] len1 = per.ownername.Split(',');
            ////string bgcode = per.bgcode;
            string where = " 1=1 ";  //and bgcode like '" + bgcode + "%'

            string[] bgcodes = per.bgcode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (bgcodes.Length > 0)
            {
                for (int i = 0; i < bgcodes.Length; i++)
                {
                    if (i == 0)
                        where += " and (charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                    else
                        where += " or charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                }
                where += ")";
            }
            else
            {
                where = where + " and ( bgcode = '' )"; 
            }

            if (per.deptname != null && !"".Equals(per.deptname))
            {
                if (len.Length > 1)
                {
                    where += " and charindex('" + len[0] + "',deptname)>0 ";
                    for (int i = 1; i < len.Length; i++)
                    {
                        //where += "or deptname like'%" + len[i] + "%'";
                        where += " or charindex('" + len[i] + "',deptname)>0 ";
                    }
                }
                else
                {
                    where += " and charindex('" + per.deptname + "',deptname)>0 ";
                }
            }
            if (per.ownername != null && !"".Equals(per.ownername))
            {
                if (len1.Length > 1)
                {
                    where += "and charindex('" + len1[0] + "',ownername)>0";
                    for (int i = 1; i < len1.Length; i++)
                    {
                        where += "or charindex('" + len1[i] + "',ownername)>0";
                    }
                }
                else
                {
                    where += "and charindex('" + per.ownername + "',ownername)>0";
                }

            }
            if (per.objstate != null && !"".Equals(per.objstate))
            {
                where += "and charindex('" + per.objstate + "',objstate)>0";
            }
            string from = table;

            try
            {
                QueryUtil qu = new QueryUtil(select, from, where);
                qu.PageSize = pageSize;
                qu.SortBy = "OBJECTID";
                qu.SortOrder = Teamax.Common.SortOrder.Descending;
                DataSet ds = qu.ExecuteDataset(pageIndex, "SdeConnString");
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = qu.RowCount;
                page.pageCount = qu.PageCount;
                page.pageSize = qu.PageSize;

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //得到环卫设施的最大ID
        public DataTable getMaxOBJID(string name)
        {
            try
            {
                string sql = "select max(OBJECTID) as OBJECTID from " + name;
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                DataSet rs = commondatabase.ExecuteDataset(sql);
                return rs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //得到环卫设施信息
        public SqlDataReader getOBJInfo(string select, string from, string where)
        {
            try
            {
                string sql = "select " + select + " from " + from + " where " + where;
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //新增一条设施信息


        public int insertIntoEstablishment(Hashtable obj, string table, string select)
        {
            string[] values = select.Split(',');
            string insertValues = "";
            string tmpKey = "";
            string tmpValue = "";
            string select1 = "";
            for (int i = 0; i < values.Length; i++)
            {
                tmpKey = Convert.ToString(values.GetValue(i));
                tmpValue = Convert.ToString(obj[tmpKey.ToLower()]);
                if (!"".Equals(tmpKey) && !"".Equals(tmpValue) && !"objectid".Equals(tmpKey))
                {
                    select1 += tmpKey + ",";
                    insertValues += "'";
                    insertValues += tmpValue;
                    insertValues += "',";
                }
            }
            select1 = select1.Substring(0, select1.Length - 1);
            insertValues = insertValues.Substring(0, insertValues.Length - 1);
            string sql = "insert into " + table + "(" + select1 + ") "
                          + " values(" + insertValues + ")";
            try
            {
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //更新一条设施信息


        public int updateEstablishment(Hashtable obj, string table, string select)
        {
            select = select.ToLower();
            string[] values = select.Split(',');
            string updateValues = "";
            string tmpKey = "";
            string tmpValue = "";
            string where = "";
            for (int i = 0; i < values.Length; i++)
            {
                tmpKey = Convert.ToString(values.GetValue(i));
                tmpValue = Convert.ToString(obj[tmpKey]);
                if (!"".Equals(tmpKey) && !"".Equals(tmpValue) && !"objectid".Equals(tmpKey))
                {
                    updateValues += tmpKey + " = '" + tmpValue + "',";
                }

            }
            updateValues = updateValues.Substring(0, updateValues.Length - 1);
            where = " where objectid = '" + Convert.ToString(obj["objectid"]) + "'";
            string sql = "update " + table + " set " + updateValues + where;
            try
            {
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int deleEstablishment(string table, string objectids)
        {
            try
            {
                string sql = "delete from " + table + " where objectid = '" + objectids + "'";
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
