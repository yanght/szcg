using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;


namespace bacgDL.environment.garbage
{
    public class landfiredao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到焚烧厂排放气体检测指标信息

        public Teamax.Common.PageManage getalllandfirelist(landfirecls rec, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] { 
                                new SqlParameter("@objname",rec.objname),
                                new SqlParameter("@time1",rec.date1),
                                new SqlParameter("@time2",rec.date2),
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","recid")
                };

                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAlllandlist", CommandType.StoredProcedure, param);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(param[4].Value);
                page.pageCount = Convert.ToInt32(param[5].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条焚烧厂排放气体检测指标信息
        public int insertIntorecord(landfirecls rec)
        {
            string sql = " insert into e_landfiretarget(objcode ,objname ,sootchroma ,dioxin ,blackness ,airdelivery ,conum ,hfnum ,hclnum ,"
                       + " hgnum ,asnum ,pbnum ,snnum ,sbnum ,cunum ,mnnum ,heatrate ,dealdate ,person ,remark) "
                       + " values('" + rec.objcode + "','" + rec.objname + "'," + rec.sootchroma + "," + rec.dioxin + ",'" + rec.blackness + "'," 
                       + rec.airdelivery + "," + rec.conum + "," + rec.hfnum + "," + rec.hclnum + ","
                       + rec.hgnum + "," + rec.asnum + "," + rec.pbnum + "," + rec.snnum + ","
                       + rec.sbnum + "," + rec.cunum + "," + rec.mnnum + "," + rec.heatrate + ",'"
                       + rec.dealdate + "','" + rec.person + "','" + rec.remark + "')";
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

        //更新一条焚烧厂排放气体检测指标信息
        public int updateIntorecord(landfirecls rec)
        {
            string sql = "update e_landfiretarget set objcode='" + rec.objcode
                                              + "', objname='" + rec.objname
                                              + "', sootchroma=" + rec.sootchroma
                                              + ",  dioxin =" + rec.dioxin
                                              + ",  blackness='" + rec.blackness
                                              + "', airdelivery=" + rec.airdelivery
                                              + ",  conum=" + rec.conum
                                              + ",  hfnum=" + rec.hfnum
                                              + ",  hclnum=" + rec.hclnum
                                              + ",  hgnum =" + rec.hgnum
                                              + ",  asnum =" + rec.asnum
                                              + ",  pbnum =" + rec.pbnum
                                              + ",  snnum =" + rec.sbnum
                                              + ",  sbnum =" + rec.sbnum
                                              + ",  cunum =" + rec.cunum
                                              + ",  mnnum =" + rec.mnnum
                                              + ",  heatrate =" + rec.heatrate
                                              + ",  dealdate  ='" + rec.dealdate
                                              + "', person   ='" + rec.person
                                              + "',  remark='" + rec.remark
                                              + "' where recid=" + rec.recid;

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
        //删除焚烧厂排放气体检测指标信息
        public int deleteFromrecord(int id)
        {
            string sql = "delete from e_landfiretarget where recid =" + id;
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

        //根据主键id得到该焚烧厂排放气体检测指标信息
        public SqlDataReader getlandfiretargetinfobyid(int id)
        {
            string sql = " select *  from e_landfiretarget where recid=" + id;
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

    }
}
