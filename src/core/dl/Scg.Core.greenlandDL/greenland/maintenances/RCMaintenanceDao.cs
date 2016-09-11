using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;

namespace bacgDL.greenland.maintenances
{
    public class RCMaintenanceDao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到日常养护数据
        public Teamax.Common.PageManage GetAllRcyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@operatetype",pat.operatetype),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")};

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllRcyhlist", CommandType.StoredProcedure, arrSP);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[5].Value);
                page.pageCount = Convert.ToInt32(arrSP[6].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据搜索条件得到日常养护数据  
        public Teamax.Common.PageManage GetAllZzyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@operatetype",pat.operatetype),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")};

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllZzyhlist", CommandType.StoredProcedure, arrSP);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[5].Value);
                page.pageCount = Convert.ToInt32(arrSP[6].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 新增养护数据
        /// <summary>
        /// 新增养护数据
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int insertIntoRcyh(Maintenances mat)
        {
            string sql = "insert into g_everydaymaintenance(operatetype,streetcode,areacode,commcode,outerdepartcode,address,person,growname,spec,amount,acreage,situation,dealdate,remark) "
                          + " values('" + mat.operatetype + "','" + mat.streetcode + "','" + mat.areacode + "','" + mat.commcode + "','" + mat.outerdepartcode + "','" + mat.address + "','" + mat.person + "','" + mat.growname + "','" + mat.spec + "','" + mat.amount + "','" + mat.acreage + "','" + mat.situation + "','" + mat.dealdate + "','" + mat.remark + "')";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新养护信息
        /// <summary>
        /// 更新养护信息
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int updateIntoRcyh(Maintenances mat)
        {
            string sql = "update g_everydaymaintenance set operatetype = '" + mat.operatetype + "',streetcode='" + mat.streetcode + "',areacode='" + mat.areacode + "',commcode='" + mat.commcode + "',outerdepartcode='" + mat.outerdepartcode + "',address='" + mat.address + "',person='" + mat.person + "',growname='" + mat.growname + "',spec='" + mat.spec + "',amount='" + mat.amount + "',acreage='" + mat.acreage + "',situation='" + mat.situation + "',dealdate='" + mat.dealdate + "',remark='" + mat.remark + "'  where recid=" + mat.recid;

            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除养护信息
        /// <summary>
        /// 删除养护信息
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public int deleteFromRcyh(int recid)
        {
            string sql = "delete from g_everydaymaintenance where recid =" + recid;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 根据主键recID得到该养护信息
        /// <summary>
        /// 根据主键recID得到该养护信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader getRcyhInfoByID(int id)
        {
            string sql = "select a.*,b.commname,c.streetname,d.departname from g_everydaymaintenance a, s_community b ,s_street c,(select departname,departcode from s_depart_outer union select departname,departcode from p_depart ) d where a.commcode = b.commcode and a.streetcode = c.streetcode and a.outerdepartcode = d.departcode and a.recid=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
