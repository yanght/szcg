using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.patrols
{
    public class GarbagecleanDAO : Teamax.Common.CommonDatabase
    {
        public PageManage GetAllGarbageResult(garbageclean pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@license",pat.license),
                                new SqlParameter("@departcode",pat.departcode),
                                new SqlParameter("@servicedeptname",pat.servicedepartname),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","id")};

                arrSP[6].Direction = ParameterDirection.Output;
                arrSP[7].Direction = ParameterDirection.Output;


                DataSet ds = this.ExecuteDataset("pr_e_getAllGarbagelist", CommandType.StoredProcedure, arrSP);
                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[6].Value);
                page.pageCount = Convert.ToInt32(arrSP[7].Value);
                page.pageSize = pageSize;

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetHasMessage(garbageclean gar)
        {
            string sql = "select id from e_garbageclean where id <> "+gar.id+" and license = '" + gar.license + "' and departcode = " + gar.departcode + " and servicedepartname = '" + gar.servicedepartname + "' and commcode = " + gar.commcode + " and objcode='" + gar.objcode + "' and dealdate = '" + gar.dealdate + "' ";
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
        public int insertIntoGarbage(garbageclean garbage)
        {
            string sql = "insert into e_garbageclean(license,departcode,departname,servicedepartcode,servicedepartname,objcode,objinputtype,areacode,streetcode,commcode,dealdate,actualclearnum,remark) "
                          + " values('" + garbage.license + "','" + garbage.departcode + "','" + garbage.departname + "','" + garbage.servicedepartcode + "','" + garbage.servicedepartname + "','" + garbage.objcode + "','" + garbage.objinputtype + "','" + garbage.areacode + "','" + garbage.streetcode + "','" + garbage.commcode + "','" + garbage.dealdate + "','" + garbage.actualclearnum + "','" + garbage.remark + "')";
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
        public int updateIntoGarbage(garbageclean garbage)
        {
            string sql = "update e_garbageclean set license='" + garbage.license + "',departcode='" + garbage.departcode + "',departname='" + garbage.departname + "',servicedepartcode='" + garbage.servicedepartcode + "',servicedepartname='" + garbage.servicedepartname + "',objcode='" + garbage.objcode + "',  objinputtype='" + garbage.objinputtype + "', areacode='" + garbage.areacode + "',streetcode='" + garbage.streetcode + "',commcode='" + garbage.commcode + "',actualclearnum='" + garbage.actualclearnum + "',dealdate='" + garbage.dealdate + "',remark='" + garbage.remark + "'  where id=" + garbage.id;

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
        public int deleteFromGarbage(int id)
        {
            string sql = "delete from e_garbageclean where id =" + id;
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
        public SqlDataReader getGarbageInfoByID(int id)
        {
            string sql = "select a.*,b.commname from e_garbageclean a,s_community b where a.commcode = b.commcode and a.id=" + id;
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
