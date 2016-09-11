using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using bacgDL.environment.patrols;
using System.Data;

namespace bacgBL.environment.patrols
{
    
    public class BASE_patrolhelper
    {
        PatrolDao patrolDao = new PatrolDao();
        public BASE_patrolhelper()
        {
            //
            //
        }

        //得到巡查信息列表
        public Teamax.Common.PageManage GetAllPatrolResult(patrol pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = patrolDao.GetAllPatrolResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //判断是否存在重复记录
        public bool GetHasMessage(patrol patrol)
        {
            try
            {
                bool b = patrolDao.GetHasMessage(patrol);
                return b;
            }
            catch
            {
                throw;
            }
        }

        //新增一条巡查信息
        public int insertIntoPatrol(patrol patrol)
        {
            try
            {
                int i = patrolDao.insertIntoPatrol(patrol);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条巡查信息
        public int updateIntoPatrol(patrol patrol)
        {
            try
            {
                int i = patrolDao.updateIntoPatrol(patrol);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除巡查信息
        public int deleteFromPatrol(int patrolid)
        {
            try
            {
                int i = patrolDao.deleteFromPatrol(patrolid);
                return i;
            }
            catch
            {
                throw;
            }

        }

        //根据部门id得到部门名字
        public string getDeptNameByID(string id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)patrolDao.getDeptNameByID(id);
                string name = "";
                while (rs.Read())
                {
                    name = rs["departname"].ToString();

                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据街道id得到街道名字
        public string getStreetNameByID(string id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)patrolDao.getStreetnameByID(id);
                string name = "";
                while (rs.Read())
                {
                    name = rs["streetname"].ToString();
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据主键patrolid得到该巡查信息
        public patrol getPatrolInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)patrolDao.getPatrolInfoByID(id);
                patrol per = new patrol();
                while (rs.Read())
                {
                    per.patrolid = id;
                    per.streetcode = rs["streetcode"].ToString();
                    per.streetname = rs["streetname"].ToString();
                    per.departcode = rs["departcode"].ToString();
                    per.departname = rs["departname"].ToString();
                    per.patroldate = rs["patroldate"].ToString();
                    per.areacode = rs["areacode"].ToString();
                    per.commcode = rs["commcode"].ToString();
                    per.commname = rs["commname"].ToString();
                    per.patrolperson = rs["person"].ToString();
                    per.patroladdress = rs["patroladdress"].ToString();
                    per.actualnum = rs["actualclearnum"].ToString();
                    per.plannum = rs["planclearnum"].ToString();
                    per.problem = rs["existproblem"].ToString();
                    per.censure = rs["censure"].ToString();
                    per.photo = rs["photo"].ToString();
                    per.remark = rs["remark"].ToString();                    
                }
                return per;
            }
            catch
            {
                throw;
            }
        }
    }
}