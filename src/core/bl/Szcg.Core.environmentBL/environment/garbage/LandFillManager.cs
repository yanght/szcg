using System;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL.environment.garbage;
using bacgDL;
using Teamax.Common;

namespace bacgBL.environment.garbage
{
    public class LandFillManager
    {
        LandFillDao landfilldao = new LandFillDao();

        //根据搜索条件得到清理垃圾列表
        public Teamax.Common.PageManage getAllLandFillList(LandFillInfo rec, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = landfilldao.getAllLandFillList(rec, pageIndex, pageSize);
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

            try
            {
                int i = landfilldao.insertIntoLandFill(rec);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条清理垃圾信息
        public int updateIntoLandFill(LandFillInfo rec)
        {
            try
            {
                int i = landfilldao.updateIntoLandFill(rec);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除清
        public int deleteFromLandFill(int recid)
        {
            try
            {
                int i = landfilldao.deleteFromLandFill(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //根据主键id得到清理垃圾信息
        public LandFillInfo getLandFillInfoByid(int recid)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)landfilldao.getLandFillInfoByID(recid);
                LandFillInfo rec = new LandFillInfo();
                while (rs.Read())
                {
                    rec.recid = recid;
                    rec.objcode = rs["objcode"].ToString() ;
                    rec.objname =rs["objname"].ToString();
                    rec.filllayer = rs["filllayer"].ToString();
                    rec.fillarea = rs["fillarea"].ToString();
                    rec.fillno = rs["fillno"].ToString();
                    rec.junktype = Convert.ToInt32(rs["junktype"]);
                    rec.junkorigin = rs["junkorigin"].ToString();
                    rec.junknum = Convert.ToDecimal(rs["junknum"]);
                    rec.air = rs["air"].ToString();
                    rec.percolatenum = Convert.ToDecimal(rs["percolatenum"]);
                    rec.water = Convert.ToDecimal(rs["water"]);
                    rec.evaporationnum = Convert.ToDecimal(rs["evaporationnum"]);
                    rec.runaspect = rs["runaspect"].ToString();
                    rec.malfunction = rs["malfunction"].ToString();
                    rec.causation = rs["causation"].ToString();
                    rec.filldate = rs["filldate"].ToString();
                    rec.person = rs["person"].ToString();
                    rec.remark = rs["remark"].ToString();
                }                
                rs.Close();
                return rec;
            }
            catch
            {
                throw;
            }
        }
    }
}
