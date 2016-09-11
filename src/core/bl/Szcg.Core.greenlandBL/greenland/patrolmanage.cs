using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL;
using bacgDL.greenland.patrol;
using System.Data;
using Teamax.Common;

namespace bacgBL.greenland
{
    public class patrolmanage
    {
        patroldao patroldao1 = new patroldao();
        public patrolmanage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //根据搜索条件得到清理垃圾列表
        public bacgDL.PageManage getallpatrollist(patrolcls rec, int pageIndex, int pageSize)
        {
            try
            {
               bacgDL.PageManage page = patroldao1.getallpatrollist(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条清理垃圾信息
        public int insertIntorecord(patrolcls rec)
        {

            try
            {
                int i = patroldao1.insertIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //更新一条清理垃圾信息
        public int updateonerecord(patrolcls rec)
        {
            try
            {
                int i = patroldao1.updateonerecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //删除清理垃圾员工
        public int deleteonerecord(int id)
        {
            try
            {
                int i = patroldao1.deleteonerecord(id);
                return i;
            }
            catch
            {
                throw;
            }

        }
        //根据主键id得到清理垃圾信息
        public patrolcls getpatrolinfobyid(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)patroldao1.getpatrolInfobyid(id);
                patrolcls rec = new patrolcls();
                while (rs.Read())
                {
                    rec.recid = id;
                    rec.areacode = rs["areacode"].ToString();
                    rec.streetcode = rs["streetcode"].ToString();
                    rec.streetname = rs["streetname"].ToString();
                    rec.commcode = rs["commcode"].ToString();
                    rec.commname = rs["commname"].ToString();
                    rec.departcode = Convert.ToInt32(rs["departcode"]);
                    rec.departname = rs["departname"].ToString();
                    rec.departname = rs["departname"].ToString();
                    rec.person = rs["person"].ToString();
                    rec.patroladdress = rs["patroladdress"].ToString();
                    rec.outerdepartcode = Convert.ToInt32(rs["outerdepartcode"]);
                    rec.outerdepartname = rs["outerdepartname"].ToString();
                    rec.problem = rs["problem"].ToString();
                    rec.situation = rs["situation"].ToString();
                    rec.patroldate = rs["patroldate"].ToString();
                    rec.remark = rs["remark"].ToString();

                }
                return rec;
            }
            catch
            {
                throw;
            }
        }

    }
}
