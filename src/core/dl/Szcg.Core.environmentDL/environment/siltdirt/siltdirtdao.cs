using System;
using System.Collections.Generic;
using System.Text;
using Teamax.Common;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;

namespace bacgDL.environment.siltdirt
{
    public class siltdirtdao : Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到淤泥渣土列表
        public Teamax.Common.PageManage getallsiltdirtlist(siltdirtcls rec, int pageIndex, int pageSize)
        {
             try
            {

                SqlParameter[] param = new SqlParameter[]{
                                new SqlParameter("@streetcode",rec.streetcode),
                                new SqlParameter("@dealtype",rec.dealtype),
                                new SqlParameter("@Time1",rec.date1),
                                new SqlParameter("@Time2",rec.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","id")  
                };

                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                DataSet ds = this.ExecuteDataset("pr_e_getallsiltdirtlist", CommandType.StoredProcedure, param);

                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
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

        //新增一条清理垃圾信息

        public int insertonerecord(siltdirtcls rec)
        {
            string sql = "insert into e_siltdirt(dealtype,streetcode,streetname,dealdate,cleardirtnum,washroadnum,sprinklecarnum,newbuilnum,repairbuilnum,remark) "
                       + " values(" + rec.dealtype + ",'" + rec.streetcode + "','" + rec.streetname + "','" + rec.dealdate + "'," + rec.cleardirtnum + "," + rec.washroadnum + "," + rec.sprinklecarnum + "," + rec.newbuilnum + "," + rec.repairbuilnum + ",'" + rec.remark + "')";
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

        //更新一条清理垃圾信息

        public int updateonerecord(siltdirtcls rec)
        {
            string sql = "update e_siltdirt set streetcode=" + rec.streetcode
                                              + ",streetname='" + rec.streetname
                                              + "',dealdate='" + rec.dealdate
                                              + "',dealtype=" + rec.dealtype
                                              + ",cleardirtnum=" + rec.cleardirtnum
                                              + ",washroadnum=" + rec.washroadnum
                                              + ",sprinklecarnum=" + rec.sprinklecarnum
                                              + ",newbuilnum=" + rec.newbuilnum
                                              + ",repairbuilnum=" + rec.repairbuilnum
                                              + ",remark='" + rec.remark + "' where id=" + rec.id;

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
        //删除清理垃圾记录
        public int deleteonerecord(int id)
        {
            string sql = "delete from e_siltdirt where id =" + id;
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

        //根据主键id得到该用户信息

        public SqlDataReader getsiltdirtinfobyid(int id)
        {
            string sql =" select id,dealtype,codename,streetcode,streetname,dealdate,cleardirtnum,washroadnum,sprinklecarnum,newbuilnum,repairbuilnum,remark"
                       + " from e_siltdirt ,(select codeid,codename from s_codedict where codetype=2 and codeid>0) b"
                       + " where  b.codeid=dealtype and  id=" + id;
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
