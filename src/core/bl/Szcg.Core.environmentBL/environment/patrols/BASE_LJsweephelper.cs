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
    public class BASE_LJsweephelper
    {
        LJsweepDAO ljsweepdao = new LJsweepDAO();

        //得到垃圾清运数据信息列表
        public Teamax.Common.PageManage GetAllLJSweepResult(LJsweep pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = ljsweepdao.GetAllLJsweepResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //判断是否存在重复记录
        public bool GetHasMessage(LJsweep gar)
        {
            try
            {
                bool b = ljsweepdao.GetHasMessage(gar);
                return b;
            }
            catch
            {
                throw;
            }
        }

        //新增一条垃圾清运管理信息
        public int insertIntoLJSweep(LJsweep ljsweep)
        {
            try
            {
                int i = ljsweepdao.insertIntoLJSweep(ljsweep);
                return i;
            }
            catch
            {
                throw;
            }
        }

        public int updateIntoLJSweep(LJsweep ljsweep)
        {
            try
            {
                int i = ljsweepdao.updateIntoLJSweep(ljsweep);
                return i;
            }
            catch
            {
                throw;
            }
        }

        public int deleteFromLJSweep(int id)
        {
            try
            {
                int i = ljsweepdao.deleteFromLJSweep(id);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //根据主键id得到该垃圾清运管理信息
        public LJsweep getLJSweepInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)ljsweepdao.getLJSweepInfoByID(id);
                LJsweep ljs = new LJsweep();
                while (rs.Read())
                {
                    ljs.id = id;
                    ljs.departcode = rs["departcode"].ToString();
                    ljs.departname = rs["departname"].ToString();
                    ljs.servicedeptcode = rs["servicedepartcode"].ToString();
                    ljs.servicedeptname = rs["servicedepartname"].ToString();
                    ljs.strcode = rs["streetcode"].ToString();
                    ljs.strname = rs["streetname"].ToString();
                    ljs.commcode = rs["commcode"].ToString();
                    ljs.commname = rs["commname"].ToString();
                    ljs.areacode = rs["areacode"].ToString();
                    ljs.sweepaddress = rs["sweepaddress"].ToString();
                    ljs.sweepdate = rs["sweepdate"].ToString();
                    ljs.aclswpnum = rs["actualsweepnum"].ToString();
                    ljs.planswpnum = rs["plansweepnum"].ToString();
                    ljs.problem = rs["existproblem"].ToString();
                    ljs.censure = rs["censure"].ToString();
                    ljs.remark = rs["remark"].ToString();
                }
                return ljs;
            }
            catch
            {
                throw;
            }
        }
    }
}
