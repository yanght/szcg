/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统清理垃圾数据库操作
 * 结构组成：
 * 作    者：鲁伟兴
 * 创建日期：2007-09-02
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.acceptplacelist
{
    public class acceptplacedao:Teamax.Common.CommonDatabase
    {
        //根据搜索条件得到受纳场信息

        public Teamax.Common.PageManage getallacceptplacelist(acceptplacecls rec, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                                new SqlParameter("@acceptplacename",rec.acceptplacename),
                                new SqlParameter("@Time1",rec.date1),
                                new SqlParameter("@Time2",rec.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","id")  
                };

                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllacceptlist", CommandType.StoredProcedure, param);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(param[4].Value.ToString());
                page.pageCount = Convert.ToInt32(param[5].Value.ToString());
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条受纳场信息
        public int insertIntorecord(acceptplacecls rec)
        {
            string sql = "insert into e_acceptplace(acceptplacecode,acceptplacename,dealdate,grefivenum,lesfivenum,acceptdirtnum,flatsoliddirtnum,germicidal,remark) "
                       + " values('" + rec.acceptplacecode + "','" + rec.acceptplacename + "','" + rec.dealdate + "'," + rec.grefivenum + "," + rec.lesfivenum + "," + rec.acceptdirtnum + "," + rec.flatsoliddirtnum + "," + rec.germicidal + ",'" + rec.remark + "')";
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

        //更新一条受纳场信息
        public int updateIntorecord(acceptplacecls rec)
        {
            string sql = "update e_acceptplace set acceptplacecode='" + rec.acceptplacecode
                                              + "',acceptplacename='" + rec.acceptplacename
                                              + "',dealdate='" + rec.dealdate
                                              + "',grefivenum=" + rec.grefivenum
                                              + ",lesfivenum=" + rec.lesfivenum
                                              + ",acceptdirtnum=" + rec.acceptdirtnum
                                              + ",flatsoliddirtnum=" + rec.flatsoliddirtnum
                                              + ",germicidal=" + rec.germicidal
                                              + ",remark='" + rec.remark + "' where id=" + rec.id;

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
        //删除受纳场信息
        public int deleteFromrecord(int id)
        {
            string sql = "delete from e_acceptplace where id =" + id;
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

        //根据主键id得到该受纳场信息
        public SqlDataReader getPersonnelInfoByID(int id)
        {
            string sql = " select id,acceptplacecode,acceptplacename,dealdate,grefivenum,lesfivenum,acceptdirtnum,flatsoliddirtnum,germicidal,codename,remark "
                       + " from e_acceptplace,(select codeid,codename from s_codedict where codetype=3 and codeid>0) b"
                       + " where b.codeid=germicidal and id=" + id;
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
