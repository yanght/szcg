using System;
using System.Data.SqlClient;
using bacgDL;
using bacgDL.environment.entitys;
using Teamax.Common;
using bacgDL.environment.siltdirt;

namespace bacgBL.environment.siltdirt
{
    public class base_siltdirthelper
    {
        siltdirtdao siltdirtdao1 = new siltdirtdao();
        public base_siltdirthelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //根据搜索条件得到清理垃圾列表
        public Teamax.Common.PageManage getallsiltdirtlist(siltdirtcls rec, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = siltdirtdao1.getallsiltdirtlist(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条清理垃圾信息

        public int insertIntorecord(siltdirtcls rec)
        {

            try
            {
                int i = siltdirtdao1.insertonerecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //更新一条清理垃圾信息

        public int updateonerecord(siltdirtcls rec)
        {
            try
            {
                int i = siltdirtdao1.updateonerecord(rec);
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
                int i = siltdirtdao1.deleteonerecord(id);
                return i;
            }
            catch
            {
                throw;
            }

        }
        //根据主键id得到清理垃圾信息
        public siltdirtcls getsiltdirtinfobyid(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)siltdirtdao1.getsiltdirtinfobyid(id);
                siltdirtcls rec = new siltdirtcls();
                while (rs.Read())
                {
                    rec.id = id;
                    rec.dealtype =Convert.ToInt32(rs["dealtype"]);
                    rec.dealtypename = rs["codename"].ToString();
                    rec.streetcode = rs["streetcode"].ToString();
                    rec.streetname = rs["streetname"].ToString();
                    rec.dealdate = rs["dealdate"].ToString();

                    rec.cleardirtnum = Convert.ToDecimal(rs["cleardirtnum"]);
                    rec.washroadnum = Convert.ToDecimal(rs["washroadnum"]);
                    rec.sprinklecarnum = Convert.ToDecimal(rs["sprinklecarnum"]);
                    rec.newbuilnum = Convert.ToDecimal(rs["newbuilnum"]);
                    rec.repairbuilnum = Convert.ToDecimal(rs["repairbuilnum"]);

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
