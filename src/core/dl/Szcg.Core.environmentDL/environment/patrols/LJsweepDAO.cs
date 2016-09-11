using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using Teamax.Common;

namespace bacgDL.environment.patrols
{
    public class LJsweepDAO : Teamax.Common.CommonDatabase
    {
        public PageManage GetAllLJsweepResult(LJsweep pat, int pageIndex, int pageSize)
        {
           try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@departname",pat.departname),
                                new SqlParameter("@servicedeptname",pat.servicedeptname),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","id")};

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;


                DataSet ds = this.ExecuteDataset("pr_e_getAllLJsweeplist", CommandType.StoredProcedure, arrSP);
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

        public bool GetHasMessage(LJsweep gar)
        {
            string sql = "select id from e_garbagesweep where id <> "+gar.id+" and departcode = " + gar.departcode + " and servicedepartname='" + gar.servicedeptname + "' and commcode=" + gar.commcode + " and sweepdate= '" + gar.sweepdate + "' and sweepaddress ='" + gar.sweepaddress + "'";
            try
            {
                DataSet ds = ExecuteDataset(sql);
                if (ds.Tables[0].Rows.Count != 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条垃圾清运管理数据
        public int insertIntoLJSweep(LJsweep sweep)
        {
            string sql = "insert into e_garbagesweep(departcode,departname,servicedepartcode,servicedepartname,areacode,streetcode,commcode,sweepaddress,sweepdate,actualsweepnum,plansweepnum,existproblem,censure,remark) "
                          + " values('" + sweep.departcode + "','" + sweep.departname + "','" + sweep.servicedeptcode + "','" + sweep.servicedeptname + "','" + sweep.areacode + "','" + sweep.strcode + "','" + sweep.commcode + "','" + sweep.sweepaddress + "','" + sweep.sweepdate + "','" + sweep.aclswpnum + "','" + sweep.planswpnum + "','" + sweep.problem + "','" + sweep.censure + "','" + sweep.remark + "')";
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

        //更新一条垃圾清运管理数据
        public int updateIntoLJSweep(LJsweep sweep)
        {
            string sql = "update e_garbagesweep set departcode='" + sweep.departcode + "',departname='" + sweep.departname + "',servicedepartcode='" + sweep.servicedeptcode + "',servicedepartname='" + sweep.servicedeptname + "', areacode='" + sweep.areacode + "',streetcode='" + sweep.strcode + "',commcode='" + sweep.commcode + "',sweepaddress='" + sweep.sweepaddress + "',sweepdate='" + sweep.sweepdate + "',actualsweepnum='" + sweep.aclswpnum + "',plansweepnum='" + sweep.planswpnum + "',censure='" + sweep.censure + "',existproblem='" + sweep.problem + "',remark='" + sweep.remark + "'  where id=" + sweep.id;

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

        //删除垃圾清运管理信息
        public int deleteFromLJSweep(int id)
        {
            string sql = "delete from e_garbagesweep where id =" + id;
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

        //根据主键id得到该垃圾清运管理信息
        public SqlDataReader getLJSweepInfoByID(int id)
        {
            string sql = "select a.*,b.commname,c.streetname from e_garbagesweep a,s_community b ,s_street c where a.commcode = b.commcode and a.streetcode = c.streetcode and a.id=" + id;
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
