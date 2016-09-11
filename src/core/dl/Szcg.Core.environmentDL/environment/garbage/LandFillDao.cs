using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.garbage
{
    public class LandFillDao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到清理垃圾列表
        public Teamax.Common.PageManage getAllLandFillList(LandFillInfo rec, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@objname",rec.objname),
                                new SqlParameter("@junktype",rec.junktype),
                                new SqlParameter("@time1",rec.date1),
                                new SqlParameter("@time2",rec.date2),
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","recid")
                };

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;


                DataSet ds = this.ExecuteDataset("pr_e_getAllLandFillList", CommandType.StoredProcedure, arrSP);
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

        //新增一条清理垃圾信息
        public int insertIntoLandFill(LandFillInfo rec)
        {
            string sql = "insert into e_landfill(objcode,objname,filllayer,fillarea,fillno,junktype,junkorigin,junknum,percolatenum,air,water,evaporationnum,runaspect,malfunction,causation,filldate,person,remark) "
                       + " values('" + rec.objcode + "','" + rec.objname + "','" + rec.filllayer + "','" + rec.fillarea + "','" + rec.fillno + "'," + rec.junktype + ",'" + rec.junkorigin + "'," + rec.junknum + "," + rec.percolatenum + ",'" + rec.air + "'," + rec.water + ",'" + rec.evaporationnum + "','" + rec.runaspect + "','" + rec.malfunction + "','" + rec.causation + "','" + rec.filldate + "','" + rec.person + "','" + rec.remark + "')";
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
        public int updateIntoLandFill(LandFillInfo rec)
        {
            string sql = "update e_landfill set objcode='" + rec.objcode
                                              + "',objname='" + rec.objname 
                                              + "',filllayer='" + rec.filllayer
                                              + "',fillarea='" + rec.fillarea
                                              + "',fillno='" + rec.fillno
                                              + "',junktype=" + rec.junktype
                                              + ",junkorigin='" + rec.junkorigin
                                              + "',junknum=" + rec.junknum
                                              + ",percolatenum=" + rec.percolatenum
                                              + ",air='" + rec.air
                                              + "',water=" + rec.water
                                              + ",evaporationnum=" + rec.evaporationnum
                                              + ",runaspect='" + rec.runaspect
                                              + "',malfunction='" + rec.malfunction
                                              + "',causation='" + rec.causation
                                              + "',filldate='" + rec.filldate
                                              + "',person='" + rec.person
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

        //删除清理垃圾记录
        public int deleteFromLandFill(int recid)
        {
            string sql = "delete from e_landfill where recid =" + recid;
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
        public SqlDataReader getLandFillInfoByID(int recid)
        {
            string sql = " select a.recid,a.objcode,a.objname,a.filllayer,a.fillarea,a.fillno,a.junktype,a.junkorigin,isnull(a.junknum,0.00) as junknum, "
                        + "isnull(a.percolatenum,0.00) as percolatenum,isnull(a.air,'') as air,isnull(a.water,0.00) as water,isnull(a.evaporationnum,0.00) as evaporationnum,isnull(a.runaspect,'') as runaspect, "
                        + "isnull(a.malfunction,'') as malfunction,isnull(a.causation,'') as causation,a.filldate,isnull(a.person,'') as person,isnull(a.remark,'') as remark, isnull(b.codename,'') as codename from e_landfill a left join "
                       + " (select codeid,codename from s_codedict where codetype=12 and codeid > 0 )  b"
                       + " on a.junktype = b.codeid where a.recid=" + recid;
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
