using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.szbase.organize;
using bacgBL.web.szbase.entity;

namespace bacgBL.web.szbase.purview
{
    public class Purviews
    {
        #region GetModelTreeList：获取模块列表树（模块，子模块）
        /// <summary>
        /// 获取模块列表树（模块，子模块）
        /// </summary>
        /// <returns></returns>
        public ArrayList GetModelTreeList(ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            //获取模块，子模块
            string modelSQL = @"select isnull(parentcode,0) as parentcode,modelcode as nodecode,modelname as nodename,
		                                    memo
                                 from p_model where isnull(del,'')<>1";
            //获取子模块下的按钮列表
            string modelPowerSQL = @"select modelcode as parentcode,ButtonCode as nodecode,ButtonName as nodename,
		                                memo as memo
                               from p_model_power where isnull(del,'')<>1";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    SqlDataReader modelDr = dl.ExecReaderSql(modelSQL);
                    while (modelDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelDr["parentcode"].ToString();
                        ts.code = modelDr["nodecode"].ToString();
                        ts.text = modelDr["nodename"].ToString();
                        ts.tag = modelDr["memo"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelDr.Close();

                    SqlDataReader modelPowerDr = dl.ExecReaderSql(modelPowerSQL);
                    while (modelPowerDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelPowerDr["parentcode"].ToString();
                        ts.code = modelPowerDr["nodecode"].ToString();
                        ts.text = modelPowerDr["nodename"].ToString();
                        ts.tag = modelPowerDr["memo"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelPowerDr.Close();

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

        #region GetPruviewModleCode:根据角色ID取出模块ID用来设置，对应的权限CheckBox让CheckBox.Checked=true
        /// <summary>
        /// 根据角色ID取出模块ID用来设置，对应的权限CheckBox让CheckBox.Checked=true
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public ArrayList GetPruviewModleCode(string id,ref string strErr)
        {
            string strSQL = string.Format(@"select ButtonCode 
                                         from p_role_modelpower
                                         where rolecode = '{0}'", id);
            using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
            {
                SqlDataReader dr = dl.ExecReaderSql(strSQL);
                ArrayList list = new ArrayList();
                string modelcode = "";
                while (dr.Read())
                {
                    modelcode = Convert.ToString(dr["ButtonCode"]);
                    list.Add(modelcode);
                }
                dr.Close();
                return list;
            }
        }
        #endregion

        #region SavePurview:保存用户设置的权限
        /// <summary>
        /// 保存用户设置的权限
        /// </summary>
        /// <param name="havaIds">已经具有的权限</param>
        /// <param name="noIds">新增加的权限</param>
        /// <param name="rolecode">角色编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int SavePurview(string haveIds, string noIds, string rolecode, ref string strErr)
        {

                SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@haveIds",haveIds),
                                                new SqlParameter("@noIds",noIds),
                                                new SqlParameter("@rolecode",rolecode),
                                                new SqlParameter("@result",SqlDbType.Int)};
                arrSP[3].Direction = ParameterDirection.Output;
                try
                {
                    using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                    {
                        dl.ExecNonQueryProc("pr_p_EditPurview", ref arrSP);
                        return Convert.ToInt32(arrSP[3].Value);
                    }
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    return 0;
                }
         }
         #endregion

         #region SavePurview:保存用户设置的权限
         /// <summary>
         /// 保存用户设置的权限
         /// </summary>
         /// <param name="havaIds">已经具有的权限</param>
         /// <param name="noIds">新增加的权限</param>
         /// <param name="rolecode">角色编码</param>
         /// <param name="strErr">输出错误信息</param>
         /// <returns></returns>
         public int SavePurview(string nodeIds, string rolecode, ref string strErr)
         {

             SqlParameter[] arrSP = new SqlParameter[]{
                                                new SqlParameter("@changeIDs",nodeIds),
                                                new SqlParameter("@roleID",rolecode),
                                                new SqlParameter("@res",SqlDbType.Int)};
             arrSP[2].Direction = ParameterDirection.Output;
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     dl.ExecNonQueryProc("pr_p_UpdatePurview", ref arrSP);
                     return Convert.ToInt32(arrSP[2].Value);
                 }
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
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
         public ArrayList GetDepartList(string areacode,string departcode,ref string strErr)
         {
             ArrayList array = new ArrayList();
             string strSQL = "";
             if (departcode == "0")//超级用户登陆的时候，可以看到全部的部门信息
                 strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%' and isnull(NoAppraise,0)=0
                                            order by departcode", areacode);
             else
                 strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
                                            and isnull(NoAppraise,0)=0
	                                                and area like '{0}%'
	                                                and (departcode = '{1}' or parentcode='{1}') 
                                            order by departcode", areacode, departcode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader dr = dl.ExecReaderSql(strSQL);
                     while (dr.Read())
                     {
                         string[] dept = new string[3];
                         dept[0] = dr["departcode"].ToString();
                         dept[1] = dr["departname"].ToString();
                         dept[2] = dr["parentcode"].ToString();
                         array.Add(dept);
                     }
                     dr.Close();
                     return array;
                 }
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

        #region GetUsers:根据部门编号，获取该部门的用户列表
        /// <summary>
        /// 
        /// </summary>
         /// <param name="departId"></param>
        /// <returns></returns>
        public ArrayList GetUsers(int departId,ref string strErr)
         {
             ArrayList array = new ArrayList();
             string strSQL = string.Format(@"   select usercode,username,departcode,mobile 
                                                from p_user
                                                where departcode = '{0}'", departId);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader dr = dl.ExecReaderSql(strSQL);
                     while (dr.Read())
                     {
                         string[] user = new string[4];
                         user[0] = dr["usercode"].ToString();
                         user[1] = dr["username"].ToString();
                         user[2] = dr["departcode"].ToString();
                         user[3] = dr["mobile"].ToString();
                         array.Add(user);
                     }
                     dr.Close();
                     return array;
                }
            }
            catch (Exception ex)
            {
                 strErr = ex.Message;
                 return null;
            }
         }
        #endregion

        #region GetUserTreeList：获取人员树信息（部门，人员）
         /// <summary>
         /// 获取人员树信息（部门，人员）
         /// </summary>
         /// <param name="areacode">区域编码</param>
         /// <returns></returns>
        public ArrayList GetUserTreeList(string areacode,string departcode,ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
             string departSQL = "";
             if (departcode == "0")//超级用户登陆的时候，可以看到全部的部门信息
                 departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
             else
                 departSQL = string.Format(@"  select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%'
	                                                and (departcode = '{1}' or parentcode='{1}') 
                                            order by departcode", areacode, departcode);
             string userSQL = string.Format(@"  select departcode as parentcode,usercode as nodecode,UserName as nodename,
			                                            memo as memo
                                                from p_user
                                                where AreaCode like '{0}%' and isnull(IsDel,0)=0",areacode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader departDr = dl.ExecReaderSql(departSQL);
                     while (departDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = departDr["parentcode"].ToString();
                         ts.code = departDr["nodecode"].ToString();
                         ts.text = departDr["nodename"].ToString();
                         ts.tag = departDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     departDr.Close();

                     SqlDataReader userDr = dl.ExecReaderSql(userSQL);
                     while (userDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = userDr["parentcode"].ToString();
                         ts.code = userDr["nodecode"].ToString()+"aaaa";//为了避免混淆，部门和用户id混淆的情况.
                         ts.text = userDr["nodename"].ToString()+"【人员】";
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

         #region GetUserPhoneTreeList：获取人员树信息（部门，人员电话号码）
         /// <summary>
         /// 获取人员树信息（部门，人员）
         /// </summary>
         /// <param name="areacode">区域编码</param>
         /// <returns></returns>
        public ArrayList GetUserPhoneTreeList(string areacode, ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
             string departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                                from p_depart
                                                where area like '{0}%' and isnull(IsDel,0)=0", areacode);
             string userSQL = string.Format(@"  select departcode as parentcode,mobile as nodecode,UserName as nodename,
			                                            memo as memo
                                                from p_user
                                                where AreaCode like '{0}%' and isnull(IsDel,0)=0", areacode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     IDataReader departDr = dl.ExecuteReader(departSQL);
                     while (departDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = departDr["parentcode"].ToString();
                         ts.code = departDr["nodecode"].ToString();
                         ts.text = departDr["nodename"].ToString();
                         ts.tag = departDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     departDr.Close();

                     string strMobile = "";
                     IDataReader userDr = dl.ExecuteReader(userSQL);
                     while (userDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = userDr["parentcode"].ToString();
                         strMobile = userDr["nodecode"].ToString();
                         ts.code = strMobile + "aaaa"; //为了避免混淆，部门和用户id混淆的情况.
                         strMobile = strMobile.Trim() == "" ? "无手机号码" : strMobile;
                         ts.text = userDr["nodename"].ToString() + "【" + strMobile + "】";
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

        #region GetRoleList:根据用户编码，获取该用户所具有的角色
        /// <summary>
         /// 根据用户编码，获取该用户所具有的角色
        /// </summary>
        /// <param name="userCode">用户编码</param>
         /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public ArrayList GetRoleList(string userCode,ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"  select a.rolecode,b.rolename 
                                            from p_user_role a left join p_role b
                                            on a.rolecode = b.rolecode
                                            where a.usercode = '{0}'",userCode);
            string strRoleName = "";
            string strRoleCode = "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        strRoleName += dr["rolename"].ToString() + ",";
                        strRoleCode += dr["rolecode"].ToString() + ",";
                    }
                    dr.Close();

                    if (strRoleName != "")
                    {
                        strRoleName = strRoleName.Substring(0, strRoleName.Length - 1);
                        list.Add(strRoleName);
                        strRoleCode = strRoleCode.Substring(0, strRoleCode.Length - 1);
                        list.Add(strRoleCode);
                    }
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

        #region SaveAccreditPurview:保存授权的用户权限
        /// <summary>
        /// 保存授权的用户权限
        /// </summary>
        /// <param name="consignerId">授权人编码</param>
        /// <param name="accepterId">接收人编码</param>
        /// <param name="roleList">角色列表</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int SaveAccreditPurview(string consignerId, string accepterId, string roleList,
                                    string startTime,string endTime,ref string strErr)
        {

            SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@consignerId",consignerId),
                                                new SqlParameter("@accepterId",accepterId),
                                                new SqlParameter("@roleList",roleList),
                                                new SqlParameter("@startTime",startTime),
                                                new SqlParameter("@endTime",endTime)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    return dl.ExecNonQueryProc("pr_p_SaveAccreditPurview", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetUserNameId:根据选择的id,获取接收授权用户的列表
        /// <summary>
        /// 根据选择的id,获取接收授权用户的列表
        /// 没有用到的函数.
        /// </summary>
        /// <param name="ids">选择的编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public ArrayList GetUserNameId(string ids, ref string strErr)
        {
            string strNames = "";
            string strCodes = "";
            string departCodes = "";
            ArrayList arrList = new ArrayList();
            string[] strIds = ids.Split(',');
            for (int i = 0; i<strIds.Length; i++)
            {
                if (strIds[i].Substring(strIds[i].Length - 4) == "8888")
                {
                    strCodes += strIds[i].Substring(0, strIds[i].Length - 4) + ",";
                }
                else
                {
                    departCodes += strIds[i].Substring(0, strIds[i].Length - 4) + ",";
                }
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (strCodes.Length > 0)
                    {
                        strCodes = strCodes.Substring(0, strCodes.Length - 1);
                        string strSQL = string.Format(@"select username from p_user
                                                        where usercode = ({0})", strCodes);
                        SqlDataReader dr = dl.ExecReaderSql(strSQL);
                        while (dr.Read())
                        {
                            strNames += dr["username"].ToString() + ",";
                        }
                        dr.Close();
                    }
                    if (departCodes.Length > 0)
                    {
                        if (strCodes.Length > 0)
                            strCodes = strCodes + ",";
                        departCodes = departCodes.Substring(0, departCodes.Length - 1);
                        string strSQL1 = string.Format(@"select usercode,username from p_user
                                                            where departcode in ({0})",departCodes);
                        SqlDataReader dr1 = dl.ExecReaderSql(strSQL1);
                        while (dr1.Read())
                        {
                            strCodes += dr1["usercode"].ToString() + ",";
                            strNames += dr1["username"].ToString() + ",";
                        }
                        dr1.Close();
                    }
                    if (strCodes.Length > 0)
                        strCodes = strCodes.Substring(0, strCodes.Length - 1);
                    if (strNames.Length > 0)
                        strNames = strNames.Substring(0, strNames.Length - 1);
                    arrList.Add(strCodes);
                    arrList.Add(strNames);

                    return arrList;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDutyDepartList：获取专业部门列表
        /// <summary>
        /// 获取专业部门列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public ArrayList GetDutyDepartList(string areacode, ref string strErr)
        {
            ArrayList array = new ArrayList();
            string strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%' and isnull(IsDuty,0)=1", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] dept = new string[3];
                        dept[0] = dr["departcode"].ToString();
                        dept[1] = dr["departname"].ToString();
                        dept[2] = dr["parentcode"].ToString();
                        array.Add(dept);
                    }
                    dr.Close();
                    return array;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetRoleListByAreaCode：根据区域编码,获取角色
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetRoleListByAreaCode(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"select a.rolecode,a.rolename 
                                            from p_role a
                                            where isnull(a.IsDel,0)<>1 and a.areacode like '{0}%'",areacode);
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

        #region GetSelectedByIds：根据用户选择的树的节点,获取用户选择的所有节点.(分层加载的树)
        /// <summary>
        /// 根据用户选择的树的节点,获取用户选择的所有节点.
        /// </summary>
        /// <param name="rolecode">设置用户的角色编码</param>
        /// <param name="Ids">用户选择的树节点</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public string GetSelectedByIds(string rolecode,string Ids, ref string strErr)
        {
            string strSQL = "";
            string retString = "";
            string temp="";
            string pIds = "";
            string[] arrIds = Ids.Split(',');
            for (int i = 0; i < arrIds.Length && Ids != ""; i++)
            {
                int flag = 0;
                temp = arrIds[i];
                for (int j = 0; j < arrIds.Length; j++)
                {
                    if (temp == arrIds[j])
                        continue;
                    if (temp.Length < arrIds[j].Length)
                    {
                        if (temp == arrIds[j].Substring(0, temp.Length))
                        {
                            retString += temp + ",";
                            flag = 1;
                            break;
                        }
                    }
                }
                if (flag != 1)
                    pIds += arrIds[i] + ",";
            }
            if (pIds.Length > 0)
                pIds = pIds.Substring(0, pIds.Length - 1);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    string[] arrpIds = pIds.Split(',');
                    if (arrpIds.Length > 0 && pIds!="")
                    {
                        for (int k = 0; k < arrpIds.Length; k++)
                        {
                            strSQL = string.Format(@"   select modelcode from p_model
                                                        where modelcode like '{0}%'",arrpIds[k]);
                            SqlDataReader modeldr = dl.ExecReaderSql(strSQL);
                            while (modeldr.Read())
                            {
                                retString += modeldr["modelcode"].ToString() + ",";
                            }
                            modeldr.Close();

                            strSQL = string.Format(@"   select ButtonCode from p_model_power
                                                        where ButtonCode like '{0}%'",arrpIds[k]);
                            SqlDataReader dr = dl.ExecReaderSql(strSQL);
                            while (dr.Read())
                            {
                                retString += dr["ButtonCode"].ToString()+",";
                            }
                            dr.Close();
                        }
                    }
                    if (retString.Length > 0)
                        retString = retString.Substring(0, retString.Length - 1);
                    return retString;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion
    }
}
