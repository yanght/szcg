using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL.greenland.maintenances;
using System.Data;
using Teamax.Common;

namespace bacgBL.greenland
{
    public class RCManager
    {
        RCMaintenanceDao rcyhDao = new RCMaintenanceDao();

        //得到巡查信息列表
        public PageManage GetAllRcyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = rcyhDao.GetAllRcyhResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //得到种植养护信息列表
        public PageManage GetAllZzyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = rcyhDao.GetAllZzyhResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条巡查信息
        public int insertIntoRcyh(Maintenances mat)
        {
            try
            {
                int i = rcyhDao.insertIntoRcyh(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条巡查信息
        public int updateIntoRcyh(Maintenances mat)
        {
            try
            {
                int i = rcyhDao.updateIntoRcyh(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除巡查信息
        public int deleteFromRcyh(int recid)
        {
            try
            {
                int i = rcyhDao.deleteFromRcyh(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //根据主键recid得到该巡查信息
        public Maintenances getRcyhInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)rcyhDao.getRcyhInfoByID(id);
                Maintenances per = new Maintenances();
                while (rs.Read())
                {
                    per.recid = id;
                    per.operatetype = Convert.ToInt32(rs["operatetype"]);
                    per.streetcode = rs["streetcode"].ToString();
                    per.streetname = rs["streetname"].ToString();
                    per.areacode = rs["areacode"].ToString();
                    per.commcode = rs["commcode"].ToString();
                    per.commname = rs["commname"].ToString();
                    per.outerdepartcode = rs["outerdepartcode"].ToString();
                    per.outerdepartname = rs["departname"].ToString();
                    per.situation = rs["situation"].ToString();
                    per.spec = rs["spec"].ToString();
                    per.person = rs["person"].ToString();
                    per.address = rs["address"].ToString();
                    per.acreage = rs["acreage"].ToString();
                    per.amount = rs["amount"].ToString();
                    per.dealdate = rs["dealdate"].ToString();
                    per.growname = rs["growname"].ToString();
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
