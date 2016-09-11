/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：绿化系统养护整改数据库操作
 * 结构组成：
 * 作    者：鲁伟兴
 * 创建日期：2007-09-20
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;


namespace bacgDL.greenland.regulation
{
    public class regulationdao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到清理垃圾列表  
        public PageManage getregulationlist(regulationcls pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")};

                arrSP[4].Direction = ParameterDirection.Output;
                arrSP[5].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllReglist", CommandType.StoredProcedure, arrSP);
                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[4].Value);
                page.pageCount = Convert.ToInt32(arrSP[5].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条清理垃圾信息
        public int insertIntorecord(regulationcls rec)
        {
            string sql = " insert into g_maintenanceregulation( areacode,  streetcode, commcode, "
                        + " outerdepartcode, address, problem, content,"
                        + " result,  startdate,  enddate,  dealdate,   person) "
                        + " values('" + rec.areacode + "','" + rec.streetcode + "','" + rec.commcode + "',"
                        + rec.outerdepartcode + ",'" + rec.address + "','" + rec.problem + "','" + rec.content + "','"
                        + rec.result + "','" + rec.startdate + "','" + rec.enddate + "','" + rec.dealdate + "','"
                        + rec.person + "')";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //更新一条清理垃圾信息
        public int updateonerecord(regulationcls rec)
        {
            string sql = "update g_maintenanceregulation set areacode='" + rec.areacode
                                              + "',streetcode='" + rec.streetcode
                                              + "',commcode='" + rec.commcode
                                              + "',outerdepartcode=" + rec.outerdepartcode
                                              + ",address='" + rec.address
                                              + "',problem='" + rec.problem
                                              + "',content='" + rec.content
                                              + "',result='" + rec.result
                                              + "',startdate='" + rec.startdate
                                              + "',enddate='" + rec.enddate
                                              + "',dealdate='" + rec.dealdate
                                              + "',person='" + rec.person + "' where recid=" + rec.recid;

            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //删除清理垃圾记录
        public int deleteonerecord(int id)
        {
            string sql = "delete from g_maintenanceregulation where recid =" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //根据主键id得到该用户信息
        public SqlDataReader getregulationinfobyid(int id)
        {
            string sql = " select a.*,b.commname,c.streetname ,d.departname "
                        + " from g_maintenanceregulation a, s_community b ,s_street c ,(select departname,departcode from s_depart_outer union select departname,departcode from p_depart ) d "
                        + " where a.commcode = b.commcode and a.streetcode = c.streetcode and a.outerdepartcode = d.departcode and a.recid=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
