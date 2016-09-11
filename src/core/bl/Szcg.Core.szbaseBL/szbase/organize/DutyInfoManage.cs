using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;
using Teamax.Common;


namespace bacgBL.szbase.organize
{
    public class DutyInfoManage
    {
        #region GetDepartDutyInfo：获取所选定的部门负责人信息
        /// <summary>
        /// 获取所选定的部门负责人信息
        /// </summary>
        /// <param name="departID">部门编号</param>
        /// <returns></returns>
        public DataSet GetDepartDutyInfo(int departcode,string order,string field, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", departcode),
                                                        new SqlParameter("@Order", order),
                                                        new SqlParameter("@Field", field)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartDutyInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region InsertDutyInfo：插入部门责任人信息
        /// <summary>
        /// 插入部门责任人信息
        /// </summary>
        /// <param name="departcode">部门id</param>
        /// <param name="TypeCode">事部件标识</param>
        /// <param name="smallclass">小类编码</param>
        /// <param name="SectionOffice">处理单位</param>
        /// <param name="PrincipalName">责任人名字</param>
        /// <param name="principalship">责任人职务</param>
        /// <param name="principalTel">责任人电话</param>
        /// <param name="HeadName">主管领导名字</param>
        /// <param name="headship">主管领导职务</param>
        /// <param name="HeadTel">主管领导电话</param>
        /// <returns></returns>
        public int InsertDutyInfo(int departcode, string TypeCode, string smallclass,
            string SectionOffice, string PrincipalName, string principalship, string principalTel,
            string HeadName, string headship, string HeadTel)
        {
            string strSql = string.Format(@"insert into dbo.p_depart_DutyInfo values({0},'{1}','{2}','{3}',
                                          '{4}','{5}','{6}','{7}','{8}','{9}')", departcode, TypeCode, smallclass, SectionOffice,
                                          PrincipalName, principalship, principalTel, HeadName, headship, HeadTel);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSql);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region GetDutyInfoByID：根据id编号获取信息
        /// <summary>
        /// 根据id编号获取信息
        /// </summary>
        /// <param name="dutyid">自定义编号</param>
        /// <returns></returns>
        public DataTable GetDutyInfoByID(int dutyid)
        {
            string strSql = "select * from p_depart_DutyInfo where id = " + dutyid + "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                     return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteDutyByID：删除部门负责人信息
        /// <summary>
        /// 删除部门负责人信息
        /// </summary>
        /// <param name="dutyid">自定义编码</param>
        /// <returns></returns>
        public int DeleteDutyByID(int dutyid)
        {
            string strSql = "delete from p_depart_DutyInfo where id = " + dutyid + "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSql);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region UpdateDutyInfoByID：更新部门负责人信息
        /// <summary>
        /// 更新部门负责人信息
        /// </summary>
        /// <param name="userDefinedCode">用户自定义编码</param>
        /// <param name="departname">部门名字</param>
        /// <param name="parentcode">父级编码</param>
        /// <param name="area">区域</param>
        /// <param name="principal">责任人</param>
        /// <param name="Mobile">移动电话</param>
        /// <param name="Tel">电话</param>
        /// <param name="departaddress">部门地址</param>
        /// <param name="memo">备注</param>
        /// <param name="departcode">部门编码</param>
        /// <param name="isChange">用户自定义编码是否改变0:没有改变,1改变了</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpdateDutyInfoByID(int dutyid,string TypeCode, string smallclass,
            string SectionOffice, string PrincipalName, string principalship, string principalTel,
            string HeadName, string headship, string HeadTel)
        {
            string strSql = string.Format(@"update dbo.p_depart_DutyInfo set TypeCode = '{0}',smallcallCode='{1}', 
                                            SectionOffice='{2}',PrincipalName = '{3}', principalship='{4}', principalTel='{5}', 
                                            HeadName= '{6}', headship='{7}', HeadTel='{8}' where ID ={9}",TypeCode,smallclass,
                                            SectionOffice,PrincipalName,principalship,principalTel,HeadName,headship,HeadTel,dutyid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
               return 0;
            }
        }
        #endregion

        #region 获取大类数据列表
        public DataTable getBigClass()
        {
            string strSql = string.Format(@"select distinct bigclasscode,name as bigname, '1' as Typecode 
                                            from dbo.s_bigclass_event with(nolock)
                                            union select distinct bigclasscode,name as bigname, '0' as Typecode 
                                            from  dbo.s_bigclass_part with(nolock)
                                            order by bigclasscode ");
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 获取小类数据列表
        public DataTable getSmallClass(string typecode)
        {
            string strSql = string.Format(@"select distinct bigclasscode,smallcallcode,smallname as smallname,code as typecode
                                            from (select distinct bigclasscode,smallcallcode,name as smallname,'0' as code
                                            from  dbo.s_smallclass_part with(nolock)
                                            union select distinct bigclasscode,smallcallcode,name as  smallname,'1' as code
                                            from  dbo.s_smallclass_event with(nolock) ) c
                                            where c.code = '{0}'
                                            order by smallcallcode asc ",typecode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
