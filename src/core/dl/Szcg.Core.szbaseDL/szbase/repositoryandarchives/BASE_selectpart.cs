/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：对知识库管理访问部门数据操作层类。对ADO.NET进行简单封装，方便使用。
 * 结构组成：对bacgBL.web.szbase.repositoryandarchives进行封装。
 * 作    者：何伟
 * 创建日期：2007-06-11
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;
using System.Data.SqlClient;

namespace bacgDL.szbase.repositoryandarchives
{
    public class BASE_selectpart : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// 从部门表中取数据 
        /// </summary>
        /// <param name="argUserCode">userId，area</param>
        /// <returns>部门相关信息</returns>
        public  ArrayList getDepts(int userId, string areacode)
        {
            ArrayList array = new ArrayList();

            string strSQL = "";

            if (!areacode.Equals("331125"))
            {
                strSQL = string.Format(@"
                    select a.UserDefinedCode,departname,a.departcode,a.parentcode,isduty
                    from p_depart a ,(
	                    select distinct parentcode 
	                    from p_depart
	                    where area like '{0}%' and IsDuty=1 and isnull(isDel,0)<>1
                    ) b
                    where a.departcode=b.parentcode
	                    and isnull(isDel,0)<>1
                    union 
                    select UserDefinedCode,departname,departcode,parentcode,isduty
                    from p_depart
                    where area like '{0}%' and IsDuty=1 and isnull(isDel,0)<>1  and isnull(NoAppraise,0)=0
                    order by UserDefinedCode", areacode);
            }
            else
            {
                strSQL = "SELECT DEPARTCODE,DEPARTNAME,PARENTCODE FROM P_DEPART where isnull(IsDel,0) = 0 ";
            }

            try
            {
                //SqlDataReader rs = DataAccess.ExecuteReader(strSQL, null);
                IDataReader rs = ExecuteReader(strSQL, CommandType.Text, true);
                while (rs.Read())
                {
                    string[] dept = new string[3];
                    dept[0] = rs["departcode"].ToString();
                    dept[1] = rs["departname"].ToString();
                    dept[2] = rs["parentcode"].ToString();
                    array.Add(dept);
                }
                rs.Close();
                return array;
            }
            catch (Exception ex)
            {
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL,
                //    System.DateTime.Now,
                //    System.DateTime.Now, 
                //    BASE_ModerId.getSystem_ZCPT(),
                //    ex.ToString(),
                //    NAMESPACE_PATH + "getDepts");
                throw ex;
            }
        }

        /// <summary>
        /// 从部门表中取数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：部门相关信息</returns>
        public  ArrayList getDepts()
        {
            ArrayList array = new ArrayList();
            //string sql="select departcode,departname,parentcode from depart";
            string sql = "SELECT DEPARTCODE,DEPARTNAME,PARENTCODE FROM S_DEPART";
            try
            {
                //SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                IDataReader rs = ExecuteReader(sql, CommandType.Text, true);
                while (rs.Read())
                {
                    string[] dept = new string[3];
                    dept[0] = rs["departcode"].ToString();
                    dept[1] = rs["departname"].ToString();
                    dept[2] = rs["parentcode"].ToString();
                    array.Add(dept);
                }
                rs.Close();
                return array;
            }

            catch (Exception ex)
            {
               
                throw ex;
            }



        }

        /// <summary>
        /// 从部门表中取最小的父节点代码数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：部门相关信息</returns>

        public  int getMinParentDepartID(int userId)
        {
            //string sql="select min(parentcode) from depart";
            string sql = "SELECT MIN(PARENTCODE) FROM P_DEPART";
            try
            {

                if (this.ExecuteNonQuery(sql) > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception err)
            {
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    sql,
                //    System.DateTime.Now,
                //    System.DateTime.Now, 
                //    BASE_ModerId.getSystem_ZCPT(),
                //    ex.ToString(),
                //    NAMESPACE_PATH + "getMinParentDepartID");
                //DEL BY HEWEI AT 2007-6-9
                throw err;
            }
            
                
        }

    }
}
