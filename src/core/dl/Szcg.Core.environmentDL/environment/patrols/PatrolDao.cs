using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.patrols
{
    public class PatrolDao : Teamax.Common.CommonDatabase
    {
        
        /// <summary>
        /// 根据搜索条件得到巡查结果数据
        /// </summary>
        public Teamax.Common.PageManage GetAllPatrolResult(patrol pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@StreetCode",pat.streetcode),
                                new SqlParameter("@UserCode",""),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","patrolid")};

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getpatrollist", CommandType.StoredProcedure, arrSP);
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

        public bool GetHasMessage(patrol patrol)
        {
            string sql = "select patrolid from e_patrol where streetcode = " + patrol.streetcode + " and departcode =" + patrol.departcode + " and commcode = " + patrol.commcode + " and person = '" + patrol.patrolperson + "' and patroldate = '" + patrol.patroldate + "' and patroladdress = '" + patrol.patroladdress + " ' and patrolid <> "+patrol.patrolid+"";
            try
            {
                DataSet ds =  ExecuteDataset(sql);
                if (ds.Tables[0].Rows.Count!=0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增一条巡查数据
        /// </summary>
        public int insertIntoPatrol(patrol patrol)
        {
            string sql = "insert into e_patrol(streetcode,streetname,areacode,commcode,departcode,departname,patroldate,person,patroladdress,actualclearnum,planclearnum,censure,existproblem,photo,remark) "
                       + " values('" + patrol.streetcode + "','" + patrol.streetname + "','" + patrol.areacode + "','" + patrol.commcode 
                       + "','" + patrol.departcode + "','" + patrol.departname + "','" + patrol.patroldate + "','" + patrol.patrolperson + "','" + patrol.patroladdress
                       + "','" + patrol.actualnum + "','" + patrol.plannum + "','" + patrol.censure + "','" + patrol.problem + "','" + patrol.photo + "','" + patrol.remark + "')";
            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新一条巡查数据
        /// </summary>
        public int updateIntoPatrol(patrol patrol)
        {
            string sql = "update e_patrol set streetcode='" + patrol.streetcode + "',streetname='" + patrol.streetname + "',areacode='" + patrol.areacode + "',commcode='" + patrol.commcode 
                       + "',departcode='" + patrol.departcode + "',departname='" + patrol.departname + "',patroldate='" + patrol.patroldate + "',actualclearnum='" + patrol.actualnum 
                       + "',planclearnum='" + patrol.plannum + "',censure='" + patrol.censure + "',existproblem='" + patrol.problem + "',person='" + patrol.patrolperson + "',patroladdress='"
                       + patrol.patroladdress + "',photo='" + patrol.photo + "',remark='" + patrol.remark + "'  where patrolid=" + patrol.patrolid;

            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除一条巡查数据
        /// </summary>
        public int deleteFromPatrol(int patrolid)
        {
            string sql = "delete from e_patrol where patrolid =" + patrolid;
            try
            {
                int i = this.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获得被考评的单位名称
        /// </summary>
        public SqlDataReader getDeptNameByID(string id)
        {
            string sql = "select departcode,departname from p_depart where departcode=" + id;
            try
            {
                SqlDataReader rs = (SqlDataReader)this.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得巡查街道名称
        /// </summary>
        public SqlDataReader getStreetnameByID(string id)
        {
            string sql = "select streetcode,streetname from s_street where streetcode=" + id;
            try
            {
                SqlDataReader rs = (SqlDataReader)this.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据主键patrolid得到该巡查信息
        /// </summary>
        public SqlDataReader getPatrolInfoByID(int id)
        {
            string sql = "select a.*,b.commname from e_patrol a, s_community b where a.commcode = b.commcode and a.patrolid=" + id;
            try
            {
                SqlDataReader rs = (SqlDataReader)this.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
