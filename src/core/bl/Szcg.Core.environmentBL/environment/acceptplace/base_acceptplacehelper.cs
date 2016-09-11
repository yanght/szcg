/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统清理垃圾数据库操作

 * 结构组成：

 * 作    者：鲁伟兴

 * 创建日期：2007-09-02
 * 更新日期：2008-01-19
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL.environment.acceptplacelist;
using bacgDL;
using Teamax.Common;

namespace bacgBL.environment.acceptplace
{	/// <summary>
    /// acceptplace 的摘要说明。

    /// </summary>
    public class base_acceptplacehelper
    {
        acceptplacedao acceptplacedao1 = new acceptplacedao();
        public base_acceptplacehelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //根据搜索条件得到清理垃圾列表
        public Teamax.Common.PageManage getallacceptplacelist(acceptplacecls rec, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = acceptplacedao1.getallacceptplacelist(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条清理垃圾信息

        public int insertIntorecord(acceptplacecls rec)
        {

            try
            {
                int i = acceptplacedao1.insertIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //更新一条清理垃圾信息

        public int updateIntorecord(acceptplacecls rec)
        {
            try
            {
                int i = acceptplacedao1.updateIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //删除清理垃圾员工
        public int deleteFromrecord(int id)
        {
            try
            {
                int i = acceptplacedao1.deleteFromrecord(id);
                return i;
            }
            catch
            {
                throw;
            }

        }
        //根据主键id得到清理垃圾信息
        public acceptplacecls getacceptplaceinfobyid(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)acceptplacedao1.getPersonnelInfoByID(id);
                acceptplacecls rec = new acceptplacecls();
                while (rs.Read())
                {
                    rec.id = id;
                    rec.acceptplacecode = rs["acceptplacecode"].ToString();
                    rec.acceptplacename = rs["acceptplacename"].ToString();

                    rec.dealdate = rs["dealdate"].ToString();
                    rec.grefivenum = Convert.ToDecimal(rs["grefivenum"]);
                    rec.lesfivenum = Convert.ToDecimal(rs["lesfivenum"]);
                    rec.acceptdirtnum = Convert.ToDecimal(rs["acceptdirtnum"]);
                    rec.flatsoliddirtnum = Convert.ToDecimal(rs["flatsoliddirtnum"]);

                    rec.germicidal = Convert.ToInt32(rs["germicidal"]);
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
