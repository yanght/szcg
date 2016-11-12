/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：部门结构树管理。
 * 结构组成：
 * 作    者：王超群
 * 创建日期：2007-05-25
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;
using Teamax.Common;
using bacgDL.szbase.organize;

namespace bacgBL.web.szbase.organize
{
    public class DepartManage
    {
        //把"null"转换为""
        private string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }

        #region UpdateIsAcceptNote：根据部门编码更新部门是否接受短信
        /// <summary>
        ///根据用户编码更新用户信息
        /// </summary>
        /// <param name="IsAcceptNote">是否接受短信</param>
        /// <param name="departcode">部门编码</param>
        public int UpdateIsAcceptNote(string IsAcceptNote, string mobile, int departcode, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"update p_depart set IsAcceptNote='{0}',Mobile='{1}' 
                                            where departcode='{2}'", IsAcceptNote, mobile, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return 0;
            }
        }
        #endregion

        #region GetAreaList：获取区域列表
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetAreaList(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"
                                            select areacode,areaname
                                            from s_area
                                            where areacode like '{0}%' ", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartList：获取部门列表
        /// <summary>
        /// 获取属于某个父节点下的部门的列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="pid">父节点</param>
        /// <returns></returns>
        public DataSet GetDepartList(string areacode, string departcode, ref string strErr)
        {
            string strSQL = "";
            if (departcode == "0")//超级用户登陆的时候，可以看到全部的部门信息
                strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ='{1}')
                                            order by departcode", areacode, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartTable：获取部门列表
        /// <summary>
        /// 获取属于某个父节点下的部门的列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="pid">父节点</param>
        /// <returns></returns>
        public DataTable GetDepartTable(string areacode, string departcode)
        {
            DepartManageDAL dal = new DepartManageDAL();
            return dal.GetDepartTable(areacode, departcode);
        }
        /// <summary>
        /// 根据父部门编号获得子部门
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public DataTable GetChildDepart(string departcode)
        {
            DepartManageDAL dal = new DepartManageDAL();
            return dal.GetChildDepart(departcode);
        }
        #endregion

        #region GetDepartList：获取部门列表（专为组织树）
        /// <summary>
        /// 获取属于某个父节点下的部门的列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="pid">父节点</param>
        /// <returns></returns>
        public ArrayList GetDepartListforTree(string areacode, string departcode, string userlevel, ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            string strSQL = "";
            if (departcode == "0" || userlevel == "9")//超级用户登陆的时候或则帐户等级为系统管理员级，可以看到全部的部门信息dutyid departcode
                strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by isnull(dutyid,999)", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ='{1}')
                                            order by isnull(dutyid,999)", areacode, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader modelDr = dl.ExecReaderSql(strSQL);
                    while (modelDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelDr["parentcode"].ToString();
                        ts.code = modelDr["departcode"].ToString();
                        ts.text = modelDr["departname"].ToString();
                        ts.tag = modelDr["area"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelDr.Close();
                    return treeStructList;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetParentCode：根据部门编码，获取该部门的父级节点
        /// <summary>
        /// 根据部门编码，获取该部门的父级节点
        /// </summary>
        /// <param name="departcode">部门编码</param>
        /// <returns></returns>
        public string GetParentCode(string departcode, ref string strErr)
        {
            DataSet ds;
            string strSQL = string.Format(@"select parentcode from p_depart
                                        where departcode = '{0}'", departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (departcode != "0")
                    {
                        ds = dl.ExecDatasetSql(strSQL);
                        if (ds.Tables[0].Rows[0][0] != null)
                            return ds.Tables[0].Rows[0][0].ToString();
                        else return null;
                        //return dl.ExecScalarSql(strSQL).ToString();
                    }
                    else
                        return "0";
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetStreetDepartCode：根据地域编号，获取该地域的最高机构
        /// <summary>
        /// 根据地域编号，获取该地域的最高机构,主要用于通过地域编号,获取其街道的顶级机构(为街道级别管理员服务)
        /// </summary>
        /// <param name="arecode">地域编码</param>
        /// <returns></returns>
        public string GetStreetDepartCode(string arecode, ref string strErr)
        {

            string strSQL = string.Format(@"select departcode from p_depart
                                        where area = '{0}' and parentcode='0'", arecode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecScalarSql(strSQL).ToString();
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartInfo：获取所选定的部门信息
        /// <summary>
        /// 获取所选定的部门信息
        /// </summary>
        /// <param name="departID">部门编号</param>
        /// <returns></returns>
        public DataSet GetDepartInfo(int departID, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", departID) };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartInfo：获取所选定的部门信息
        /// <summary>
        /// 获取所选定的部门信息
        /// </summary>
        /// <param name="departID">部门编号</param>
        /// <returns></returns>
        public DataSet GetDepartInfoTWO(string UserDefinedCode, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", UserDefinedCode) };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartInfoTWO", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region CheckDepartName：判断部门名字是否存在
        /// <summary>
        /// 获取所选定的部门信息
        /// </summary>
        /// <param name="departID">部门编号</param>
        /// <returns></returns>
        public bool CheckDepartName(string pId, string departName, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from p_depart 
                                            where isnull(IsDel,0)<>1 and parentcode='{0}' and departname='{1}'", pId, departName);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (Convert.ToInt32(dl.ExecuteScalar(strSQL)) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }
        #endregion

        #region CheckUserDepartCode：判断部门自定义编码是否存在
        /// <summary>
        /// 判断部门自定义编码是否存在
        /// </summary>
        /// <param name="departUserCode">用户自定义编码</param>
        /// <returns></returns>
        public int CheckUserDepartCode(string departUserCode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from p_depart 
                                            where isnull(IsDel,0) <> 1 and UserDefinedCode='{0}'", departUserCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecuteScalar(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertDepart：插入部门信息
        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="pId">父部门ID编号</param>
        /// <param name="departName">部门名字</param>
        /// <param name="address">部门地址</param>
        /// <param name="area">部门所属区域</param>
        /// <param name="memo">部门信息备注</param>
        /// <param name="strErr">输出的错误信息</param>
        /// <param name="txt_sort">序号</param>
        /// <returns></returns>
        public int InsertDepart(int pId, string departName, string address,
                                    string area, string memo, string IsDuty, string Mobile,
                                    string tel, string principal, string UserDefinedCode, int max_notenum, string IsAcceptNote,
                                    string issj, string SJ_RoleCode, string NoAppraise,
                                    ref string strErr, string txt_sort)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                    new SqlParameter("@departname",departName), 
                                    new SqlParameter("@parentcode", pId),
                                    new SqlParameter("@area",area),
                                    new SqlParameter("@principal",principal),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Tel",tel),
                                    new SqlParameter("@departadress",address),
                                    new SqlParameter("@memo",memo),
                                    new SqlParameter("@IsDuty",IsDuty),
                                    new SqlParameter("@UserDefinedCode",UserDefinedCode),
                                    new SqlParameter("@IsAcceptNote",IsAcceptNote),
                                    new SqlParameter("@max_notenum",max_notenum),
                                    new SqlParameter("@issj",issj),
                                    new SqlParameter("@SJ_RoleCode",SJ_RoleCode),
                                    new SqlParameter("@NoAppraise",NoAppraise),
                                    new SqlParameter("@dutyid",int.Parse(txt_sort))
            };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQueryProc("pr_p_InsertDepartInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpdateDepart：更新部门信息
        /// <summary>
        /// 更新部门信息
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
        /// <param name="txt_sort">排序</param>
        /// <returns></returns>
        public int UpdateDepart(string userDefinedCode, string departname, string parentcode,
                                    string area, string principal, string Mobile,
                                    string Tel, string departaddress, string memo, string IsDuty,
                                    int max_notenum, string IsAcceptNote, string issj,
                                    string SJ_ROLECODE, string NOAPPRAISE,
                                    string departcode, int isChange, ref string strErr, string txt_sort)
        {
            string strPDefinedCode = string.Format(@"select UserDefinedCode from p_depart
                                                     where departcode='{0}'", parentcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    object obj = dl.ExecScalarSql(strPDefinedCode);
                    string pCode = obj == null ? "" : obj.ToString();
                    if (pCode.Length > 0 && isChange == 1)
                    {
                        string subCode = userDefinedCode.Substring(userDefinedCode.Length - 3);
                        userDefinedCode = pCode + subCode;
                        string checkUserDefinedCode = string.Format(@"select count(*) from p_depart where UserDefinedCode = '{0}'", userDefinedCode);
                        //判断组合后的用户编码是否在数据库里存在
                        object obj1 = dl.ExecScalarSql(checkUserDefinedCode);
                        string flag = obj1 == null ? "" : obj1.ToString();
                        if (flag != "" && flag != "0")
                        {
                            return -1;
                        }
                    }
                    string strSQL = string.Format(@"update p_depart set UserDefinedCode='{0}',departname='{1}',parentcode='{2}',
                                                                    area='{3}',principal='{4}',Mobile ='{5}',
                                                                    Tel = '{6}',departadress='{7}',memo='{8}', IsDuty='{9}',IsAcceptNote='{10}', max_notenum='{11}',issj ='{12}' ,SJ_ROLECODE='{13}' ,NOAPPRAISE='{14}',dutyid='{15}'  
                                            where departcode={16}", userDefinedCode, departname, parentcode, area, principal, Mobile, Tel, departaddress, memo, IsDuty, IsAcceptNote, max_notenum, issj, SJ_ROLECODE, NOAPPRAISE, txt_sort, departcode);
                    return dl.ExecuteNonQuery(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion


        #region  GetsjDutyDepart：  获取市局责任单位
        /// <summary>
        ///   获取市局责任单位
        /// </summary>
        /// <param name="strDepartName">专业部门名称</param>
        /// <param name="ErrMsg">返回错误信息</param>
        /// <returns></returns>
        public static DataSet GetsjDutyDepart(string strDepartName, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string sql = string.Format(@"select rolecode, 
                                                    rolename+','+isnull(Memo,'') as rolename
                                            from dbo.p_role_sj 
                                            where rolename like '%{0}%'  
                                            order by rolename ", strDepartName);
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecuteDataset(sql);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region DeleteDepart：删除部门信息
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="departId">部门Id编码</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：部门存在人员，删除失败3：部门存在子部门，删除失败
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteDepart(string departId, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",departId), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromDepart", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
                //return null;
            }
        }
        #endregion

        #region GetUserDefineCode：根据部门编码获取父级和子级自定义编码
        /// <summary>
        /// 根据部门编码获取父级和子级自定义编码
        /// </summary>
        /// <param name="id">部门编码</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public ArrayList GetUserDefineCode(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select a.UserDefinedCode,b.UserDefinedCode as ParentUserDefineCode 
                                            from p_depart a left join p_depart b
                                            on a.parentcode = b.departcode
                                            where isnull(a.departcode,0) = {0}", id);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (flag == 0)
                            {
                                arrList.Add(row["ParentUserDefineCode"]);
                                flag++;
                            }
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserDefineCode1：根据父级部门编码获取父级和子级自定义编码
        /// <summary>
        /// 根据父级部门编码获取父级和子级自定义编码
        /// </summary>
        /// <param name="pid">父级部门编码</param>
        /// <param name="strErr">输出的错误信息</param>pr_p_GetDepartTree 
        /// <returns></returns>
        public ArrayList GetUserDefineCode1(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select b.UserDefinedCode,a.UserDefinedCode as ParentUserDefineCode
                                            from p_depart a left join p_depart b
                                            on a.departcode = b.parentCode
                                            where isnull(a.IsDel,0) <> 1 and a.departcode={0}", pid);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (flag == 0)
                            {
                                arrList.Add(row["ParentUserDefineCode"]);
                                flag++;
                            }
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserDefineCode2：获取一级部门编码
        /// <summary>
        /// 获取一级部门编码
        /// </summary>
        /// <param name="pid">父级部门编码</param>
        /// <param name="strErr">输出的错误信息</param>pr_p_GetDepartTree 
        /// <returns></returns>
        public ArrayList GetUserDefineCode2(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select UserDefinedCode from p_depart
                                            where parentcode='{0}'", pid);
            ArrayList arrList = new ArrayList();
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserByDeptID：根据部门编号获取改部门下边的人员
        /// <summary>
        /// 根据部门编号获取改部门下边的人员
        /// </summary>
        /// <param name="departID">部门编号</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="returnRecordCount">返回记录数</param>
        /// <param name="userId">用户编号</param>
        /// <param name="name">用户名字</param>
        /// <param name="loginname">登陆名字</param>
        /// <param name="departname">部门名字</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public DataSet GetUserByDeptID(int departID, int pageIndex, int pageSize,
                                            int returnRecordCount, int userId, string name,
                                            string loginname, string departname, string Order, string Field, ref string strErr)
        {
           	SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@parentCode",departID), 
                                new SqlParameter("@PageIndex",pageIndex),
                                new SqlParameter("@PageSize",pageSize), 
                                new SqlParameter("@ReturnRecordCount",returnRecordCount),
                                new SqlParameter("@name",name), 
                                new SqlParameter("@loginname",loginname),
                                new SqlParameter("@departname",departname),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    DataSet  ds = dl.ExecProc("pr_p_GetDepartTree", ref arrSP);
                                   

                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        ////把"null"转换为""
        //public string changeNull(string str)
        //{
        //    if (str.ToLower().Equals("null"))
        //        str = "";
        //    return str;
        //}

        //string类型数据自增1（表主键处理，控制至少4位）
        public string increase(string keyvalue)
        {   //1017010003
            string _result = Convert.ToString(keyvalue);
            if (_result.Length < 4)
            {
                int count = 4 - _result.Length;
                for (int i = 1; i <= count; i++)
                {
                    _result = "0" + _result;
                }
            }
            else
            {
                //添加一般的部门时，截取后4位转换为数字，并且自增1；
                string m1 = keyvalue.Substring(keyvalue.Length - 4, 4);
                int len = Convert.ToInt32(m1) + 1;
                keyvalue = keyvalue.Substring(0, keyvalue.Length - 4);

                //当截取后4位的第一位为0时，自增规则；
                if (m1.Substring(0, 1).Equals("0"))
                {
                    int length = 4 - len.ToString().Length;
                    for (int j = 0; j < length; j++)
                    {
                        keyvalue = keyvalue + "0";
                    }
                }
                _result = keyvalue + len.ToString();
            }
            return _result;
        }

        //取当前表最大值
        public int selectMaxValue(string tablename, string keyvalue, string columname, string datavalue)
        {
            string sql = "select isnull(Max(" + keyvalue + "),0) as " + keyvalue + " from " + tablename + " where 1=1 ";

            if (columname != null && columname != "" && datavalue != null && datavalue != "")
                sql = sql + " and " + columname + "='" + datavalue + "'";

            using (CommonDatabase DAO = new CommonDatabase())
            {
                IDataReader rs = DAO.ExecuteReader(sql);
                int result = 0;
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = Convert.ToInt32(rs[keyvalue].ToString());
                    }
                }
                rs.Close();
                return result;
            }
        }

        //根据部门ID取其相关数据信息，组装成list
        public ArrayList getDeptInfo(string departcode, string columname)
        {
            string strSQL = string.Format(@"select * from p_depart
                                            where departcode='{0}'", departcode);
            ArrayList arrList = new ArrayList();
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            arrList.Add(row[columname]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //strErr = ex.Message;
                return null;
            }
        }
    }
}
