using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;

namespace bacgDL.greenland.parkmanagers
{
    public class parksmanagerDao:Teamax.Common.CommonDatabase
    {
        public PageManage GetAllParksResult(ParksInfo pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@parkname",pat.parkname),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")  
                };

                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                DataSet ds = this.ExecuteDataset("pr_e_getAllpartlist", CommandType.StoredProcedure, param);

                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(param[5].Value.ToString());
                page.pageCount = Convert.ToInt32(param[6].Value.ToString());
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 新增公园绿地数据
        /// <summary>
        /// 新增公园绿地数据
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int insertIntoParks(ParksInfo mat)
        {
            string sql = "insert into g_parks(parkcode,parkname,streetcode,departcode,departname,ownercode,ownername,parkaddress,parktype,redarea,landarea,waterarea,house,establishment,phone,ordate,remark) "
                          + " values('" + mat.parkcode + "','" + mat.parkname + "','" + mat.streetcode + "','" + mat.departcode + "','" + mat.departname + "','" + mat.ownercode + "','" + mat.ownername + "','" + mat.parkaddress + "','" + mat.parktype + "','" + mat.redarea + "','" + mat.landarea + "','" + mat.waterarea + "','" + mat.house + "','" + mat.establishment + "','" + mat.phone + "','" + mat.ordate + "','" + mat.remark + "')";
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

        #region 更新公园绿地数据
        /// <summary>
        /// 更新公园绿地数据
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int updateIntoParks(ParksInfo rec)
        {
            string sql = "update g_parks set parkcode='" + rec.parkcode
                                              + "',parkname='" + rec.parkname
                                              + "',streetcode='" + rec.streetcode
                                              + "',departcode='" + rec.departcode
                                              + "',departname='" + rec.departname
                                              + "',ownercode='" + rec.ownercode
                                              + "',ownername='" + rec.ownername
                                              + "',parkaddress='" + rec.parkaddress
                                              + "',parktype='" + rec.parktype
                                              + "',redarea=" + rec.redarea
                                              + ",landarea=" + rec.landarea
                                              + ",waterarea=" + rec.waterarea
                                              + ",house='" + rec.house
                                              + "',establishment='" + rec.establishment
                                              + "',phone='" + rec.phone
                                              + "',ordate='" + rec.ordate
                                              + "',remark='" + rec.remark + "' where recid=" + rec.recid;
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

        #region 删除公园绿地数据
        /// <summary>
        /// 删除公园绿地数据
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public int deleteFromParks(int recid)
        {
            string sql = "delete from g_parks where recid =" + recid;
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

        #region 根据主键recID得到该公园绿地数据
        /// <summary>
        /// 根据主键recID得到该公园绿地数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader getParksInfoByID(int id)
        {
            string sql = "select a.*,c.streetname from g_parks a, s_street c where a.streetcode = c.streetcode and a.recid=" + id;
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
