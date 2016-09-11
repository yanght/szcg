using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using bacgDL.environment.patrols;
using System.Data;
using Teamax.Common;

namespace bacgBL.environment.patrols
{
    public class BASE_garbagehelper
    {
        GarbagecleanDAO garbagecleanDAO = new GarbagecleanDAO();

        public BASE_garbagehelper()
        {
            //
            //
        }

        //得到垃圾清运数据信息列表
        public Teamax.Common.PageManage GetAllGarbageResult(garbageclean pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = garbagecleanDAO.GetAllGarbageResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //判断是否存在重复记录
        public bool GetHasMessage(garbageclean gar)
        {
            try
            {
                bool b = garbagecleanDAO.GetHasMessage(gar);
                return b;
            }
            catch
            {
                throw;
            }
        }

        //新增一条垃圾清运管理信息
        public int insertIntoGarbage(garbageclean garbage)
        {
            try
            {
                int i = garbagecleanDAO.insertIntoGarbage(garbage);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条垃圾清运管理信息
        public int updateIntoGarbage(garbageclean garbage)
        {
            try
            {
                int i = garbagecleanDAO.updateIntoGarbage(garbage);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除一条垃圾清运管理信息
        public int deleteFromGarbage(int id)
        {
            try
            {
                int i = garbagecleanDAO.deleteFromGarbage(id);
                return i;
            }
            catch
            {
                throw;
            }
        }


        //根据主键id得到该垃圾清运管理信息
        public garbageclean getGarbageInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)garbagecleanDAO.getGarbageInfoByID(id);
                garbageclean gar = new garbageclean();
                while (rs.Read())
                {
                    gar.id = id;
                    gar.license = rs["license"].ToString();
                    gar.departcode = rs["departcode"].ToString();
                    gar.departname = rs["departname"].ToString();
                    gar.servicedepartcode = rs["servicedepartcode"].ToString();
                    gar.servicedepartname = rs["servicedepartname"].ToString();
                    gar.objcode = rs["objcode"].ToString();
                    gar.commcode = rs["commcode"].ToString();
                    gar.commname = rs["commname"].ToString();
                    gar.areacode = rs["areacode"].ToString();
                    //gar.objinputtype = rs["objinputtype"].ToString();
                    gar.dealdate = rs["dealdate"].ToString();
                    gar.actualclearnum = Convert.ToDecimal(rs["actualclearnum"]);
                    gar.remark = rs["remark"].ToString();
                }
                return gar;
            }
            catch
            {
                throw;
            }
        }
    }
}
