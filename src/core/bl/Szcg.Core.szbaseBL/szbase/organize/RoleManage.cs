using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;

namespace bacgBL.web.szbase.organize
{
    public class RoleManage
    {
        //把"null"转换为""
        private string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }

        #region GetRoleList：获取角色列表
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="departcode">部门编码</param>
        /// <returns></returns>
        public DataSet  GetRoleList(string departcode,ref string strErr)
        {
            string strSQL = string.Format(@"select a.rolecode,a.rolename 
                                            from p_role a left join p_depart  b
                                            on a.departcode=b.departcode
                                            where isnull(a.IsDel,0)<>1 and b.departcode = '{0}'", departcode);
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

        #region GetRole：获取角色列表
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="departcode">部门编码</param>
        /// <returns></returns>
        public DataSet GetRole(ref string strErr)
        {
            string strSQL = string.Format(@"select a.rolecode,a.rolename 
                                            from p_role a 
                                            where isnull(a.IsDel,0)<>1 ");
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

        #region GetRoleList1：获取角色列表
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public ArrayList GetRoleList1(string areacode)
        {
            string strSQL = string.Format(@"select * 
                                            from p_role
                                            where isnull(IsDel,0) <> 1 and areacode like '{0}%'", areacode);
            ArrayList array = new ArrayList();
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {
                        string[] role = new string[4];
                        role[0] = rs["rolecode"].ToString();
                        role[1] = rs["rolename"].ToString();
                        role[2] = changeNull(rs["stepid"].ToString());
                        role[3] = changeNull(rs["areacode"].ToString());
                        array.Add(role);
                    }
                    rs.Close();
                    return array;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region GetRoleInfo：根据角色编号，获取角色信息
        /// <summary>
        /// 根据角色编号，获取角色信息
        /// </summary>
        /// <param name="id">角色编码</param>
        /// <returns></returns>
        public DataSet GetRoleInfo(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select a.*,b.departname,b.departcode 
                                            from p_role a left join p_depart b
                                            on a.departcode=b.departcode
                                            where isnull(a.IsDel,0) <> 1 and a.rolecode = '{0}'",id);
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

        #region CheckRoleName：判断角色名字是否存在
        /// <summary>
        /// 判断角色名字是否存在
        /// </summary>
        /// <param name="roleName">角色名字</param>
        /// <returns></returns>
        public bool CheckRoleName(string roleName,ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from p_role
                                            where isnull(IsDel,0) <> 1 and rolename = '{0}'", roleName);
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

        #region InsertRole：插入角色信息
        /// <summary>
        /// 插入角色信息
        /// </summary>
        /// <param name="roleName">角色名字</param>
        /// <returns></returns>
        public int InsertRole(string rolename,string departcode,string areacode,
                                string stepid,string mobile, ref string strErr)
        {
            string strSQL = string.Format(@"insert into p_role(rolename,areacode,departcode,
					                                            stepid,Mobile)
                                            values('{0}','{1}','{2}',
		                                    '{3}','{4}')",rolename,areacode,departcode,stepid,mobile);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpdateRole：更新角色信息
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="id">角色编码</param>
        /// <returns></returns>
        public int UpdateRole(string rolename, string departcode, string areacode,
                                string stepid, string mobile,int id,
                                ref string strErr)
        {
            string strSQL = string.Format(@"update p_role set rolename = '{0}',areacode ='{1}',departcode='{2}',
					                                            stepid='{3}',Mobile='{4}'
                                            where rolecode = '{5}'", rolename, areacode, departcode, stepid, mobile,id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetStepList：获取阶段列表
        /// <summary>
        /// 获取阶段列表
        /// </summary>
        /// <param name="strErr">输出错去信息</param>
        /// <returns></returns>
        public DataSet GetStepList(ref string strErr)
        {
            string strSQL = "select stepcode,stepname from s_step";
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

        #region DeleteRole：删除角色信息
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Id">角色Id编码</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：角色下边添加了人员
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteRole(int id, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",id), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromRole", ref arrSP);
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

        #region GetUserByRoleID：根据角色编号获取改部门下边的人员
        /// <summary>
        /// 根据角色编号获取改部门下边的人员
        /// </summary>
        /// <param name="id">角色编码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页大小</param>
        /// <param name="returnRecordCount">返回行数</param>
        /// <param name="userId">用户编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public ArrayList[] GetUserByRoleID(int id, int pageIndex, int pageSize,
                                            int returnRecordCount,ref string strErr)
        {
            ArrayList[] list = new ArrayList[2];
            list[0] = new ArrayList();
            list[1] = new ArrayList();
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@roleCode",id), 
                                new SqlParameter("@PageIndex",pageIndex),
                                new SqlParameter("@PageSize",pageSize), 
                                new SqlParameter("@ReturnRecordCount",returnRecordCount)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    SqlDataReader rs = dl.ExecReaderProc("pr_p_GetUsersFromRoleID", ref arrSP);
                    while (rs.Read())
                    {
                        User user = new User();
                        user.UserCode = (int)rs["usercode"];
                        user.UserName = rs["username"].ToString();
                        user.LoginName = rs["loginname"].ToString();
                        user.Sex = rs["sex"].ToString();
                        user.Tel = changeNull(rs["tel"].ToString());
                        user.Mobile = changeNull(rs["mobile"].ToString());
                        user.DepartName = rs["departname"].ToString();
                        list[0].Add(user);
                    }
                    if (rs.NextResult())
                    {
                        rs.Read();
                        list[1].Add(rs["recordcount"].ToString());
                    }
                    rs.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetrRoleListByUserCode:根据用户编码，判断该用户具有什么角色。
        /// <summary>
        /// 根据用户编码，判断该用户具有什么角色。
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public ArrayList GetrRoleListByUserCode(string usercode, ref string strErr)
        {
            string strSQL = string.Format(@"select t.rolecode from p_user_role t right join p_role t1 on t.rolecode=t1.rolecode and isnull(t1.IsDel,0)=0
                                            where usercode = '{0}'", usercode);
            using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
            {
                SqlDataReader dr = dl.ExecReaderSql(strSQL);
                ArrayList list = new ArrayList();
                string rolecode = "";
                while (dr.Read())
                {
                    rolecode = Convert.ToString(dr["rolecode"]) + "8888";
                    list.Add(rolecode);
                }
                dr.Close();
                return list;
            }
        }
        #endregion

        #region GetRoleTreeList：获取角色树信息（部门，角色编码）
        /// <summary>
        ///获取角色树信息（部门，角色编码）
         /// </summary>
         /// <param name="areacode">区域编码</param>
         /// <returns></returns>
        public ArrayList GetRoleTreeList(string areacode, string stepid, ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
//             string departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
//				                                        memo as memo 
//                                                from p_depart
//                                                where area like '{0}%' and isnull(IsDel,0)=0", areacode);
             string userSQL = string.Format(@"  select departcode as parentcode,rolecode as nodecode,rolename as nodename,
                                                        memo as memo
                                                from p_role
                                                where isnull(IsDel,0)=0 and stepid<={0}", stepid);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     //SqlDataReader departDr = dl.ExecReaderSql(departSQL);
                     //while (departDr.Read())
                     //{
                     //    TreeSuruct ts;
                     //    ts.pcode = departDr["parentcode"].ToString();
                     //    ts.code = departDr["nodecode"].ToString();
                     //    ts.text = departDr["nodename"].ToString();
                     //    ts.tag = departDr["memo"].ToString();
                     //    treeStructList.Add(ts);
                     //}
                     //departDr.Close();

                     SqlDataReader userDr = dl.ExecReaderSql(userSQL);
                     while (userDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = userDr["parentcode"].ToString();
                         ts.code = userDr["nodecode"].ToString() + "aaaa";//为了避免混淆，部门和用户id混淆的情况.
                         ts.text = userDr["nodename"].ToString()+"【角色】";
                         ts.tag = userDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     userDr.Close();

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

        #region GetHcPowerTreeList：获取核查栏权限信息
        /// <summary>
        ///获取角色树信息（部门，角色编码）
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public  ArrayList GetHcPowerTreeList(ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            //             string departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
            //				                                        memo as memo 
            //                                                from p_depart
            //                                                where area like '{0}%' and isnull(IsDel,0)=0", areacode);
            string userSQL = string.Format(@"  select areacode as parentcode,streetcode as nodecode,streetname as nodename,
                                                        memo as memo
                                                from s_street");
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    //SqlDataReader departDr = dl.ExecReaderSql(departSQL);
                    //while (departDr.Read())
                    //{
                    //    TreeSuruct ts;
                    //    ts.pcode = departDr["parentcode"].ToString();
                    //    ts.code = departDr["nodecode"].ToString();
                    //    ts.text = departDr["nodename"].ToString();
                    //    ts.tag = departDr["memo"].ToString();
                    //    treeStructList.Add(ts);
                    //}
                    //departDr.Close();

                    SqlDataReader userDr = dl.ExecReaderSql(userSQL);
                    while (userDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = userDr["parentcode"].ToString();
                        ts.code = userDr["nodecode"].ToString() + "aaaa";//为了避免混淆，部门和用户id混淆的情况.
                        ts.text = userDr["nodename"].ToString() + "【核查栏核查权限】";
                        ts.tag = userDr["memo"].ToString();
                        treeStructList.Add(ts);
                    }
                    userDr.Close();

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
    }
}
