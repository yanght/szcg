using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;

namespace bacgDL.greenland.monthinspections
{
    public class monthinspectionDao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到月检登记数据
        public PageManage GetAllYJdjResult(Monthinspections pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@type",pat.type),
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

                DataSet ds = this.ExecuteDataset("pr_e_getAllYJdjlist", CommandType.StoredProcedure, arrSP);
                PageManage page = new PageManage();
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

        #region 新增月检登记数据
        /// <summary>
        /// 新增月检登记数据
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int insertIntoYjdj(Monthinspections mat)
        {
            string sql = "insert into g_monthinspection(streetcode,areacode,commcode,type,outerdepartcode,address,person,problem,dealdate,remark) "
                          + " values('" + mat.streetcode + "','" + mat.areacode + "','" + mat.commcode + "','" + mat.type + "','" + mat.outerdepartcode + "','" + mat.address + "','" + mat.person + "','" + mat.problem + "','" + mat.dealdate + "','" + mat.remark + "')";
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

        #region 更新月检登记数据
        /// <summary>
        /// 更新月检登记数据
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int updateIntoYjdj(Monthinspections mat)
        {
            string sql = "update g_monthinspection set type = '" + mat.type + "',streetcode='" + mat.streetcode + "',areacode='" + mat.areacode + "',commcode='" + mat.commcode + "',outerdepartcode='" + mat.outerdepartcode + "',address='" + mat.address + "',person='" + mat.person + "',problem='" + mat.problem + "',dealdate='" + mat.dealdate + "',remark='" + mat.remark + "'  where recid=" + mat.recid;

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

        #region 删除月检登记数据
        /// <summary>
        /// 删除月检登记数据
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public int deleteFromYjdj(int recid)
        {
            string sql = "delete from g_monthinspection where recid =" + recid;
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

        #region 根据主键recID得到该月检登记数据
        /// <summary>
        /// 根据主键recID得到该月检登记数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader getYjdjInfoByID(int id)
        {
            string sql = "select a.*,b.streetname,c.commname,isnull(d.departname,'') as departname"
                          +" from dbo.g_monthinspection a left join s_community c"
                          +" on a.commcode = c.commcode left join s_street b"
                          +" on a.streetcode = b.streetcode left join (select departcode,departname from p_depart"
                          +" union select departcode,departname from s_depart_outer) d"
                          +" on a.outerdepartcode = d.departcode where 1=1"
                          +" and a.recid=" + id;
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
