/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫管理系统垃圾总站数据库操作

 * 结构组成：

 * 作    者：鲁伟兴

 * 创建日期：2007-10-12
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL.environment.garbage;
using bacgDL;
using Teamax.Common;

namespace bacgBL.environment.garbage
{
    public class base_landfirehelper
    {
        landfiredao landfiredao1 = new landfiredao();
        public base_landfirehelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //根据搜索条件得到垃圾焚烧场指标信息列表

        public Teamax.Common.PageManage getalllandfirelist(landfirecls rec, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = landfiredao1.getalllandfirelist(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条垃圾焚烧场指标信息
        public int insertIntorecord(landfirecls rec)
        {

            try
            {
                int i = landfiredao1.insertIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //更新一条垃圾焚烧场指标信息
        public int updateIntorecord(landfirecls rec)
        {
            try
            {
                int i = landfiredao1.updateIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //删除垃圾焚烧场指标信息

        public int deleteFromrecord(int id)
        {
            try
            {
                int i = landfiredao1.deleteFromrecord(id);
                return i;
            }
            catch
            {
                throw;
            }

        }
        //根据主键id得到垃圾焚烧场指标信息

        public landfirecls getlandfireinfobyid(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)landfiredao1.getlandfiretargetinfobyid(id);
                landfirecls rec = new landfirecls();
                while (rs.Read())
                {
                    rec.recid = id;
                    rec.objcode = rs["objcode"].ToString();
                    rec.objname = rs["objname"].ToString();
                    rec.dealdate = rs["dealdate"].ToString();

                    rec.sootchroma = Convert.ToDecimal(rs["sootchroma"]);
                    rec.dioxin = Convert.ToDecimal(rs["dioxin"]);
                    rec.blackness = rs["blackness"].ToString();
                    rec.airdelivery = Convert.ToDecimal(rs["airdelivery"]);
                    rec.conum = Convert.ToDecimal(rs["conum"]);
                    rec.hfnum = Convert.ToDecimal(rs["hfnum"]);

                    rec.hclnum = Convert.ToDecimal(rs["hclnum"]);
                    rec.hgnum = Convert.ToDecimal(rs["hgnum"]);

                    rec.asnum = Convert.ToDecimal(rs["asnum"]);
                    rec.pbnum = Convert.ToDecimal(rs["pbnum"]);

                    rec.snnum = Convert.ToDecimal(rs["snnum"]);
                    rec.sbnum = Convert.ToDecimal(rs["sbnum"]);

                    rec.cunum = Convert.ToDecimal(rs["cunum"]);
                    rec.mnnum = Convert.ToDecimal(rs["mnnum"]);
                    rec.heatrate = Convert.ToDecimal(rs["heatrate"]);

                    rec.person = rs["person"].ToString();
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
