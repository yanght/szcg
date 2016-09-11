using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgBL.web.szbase.organize
{
    public class UserManage
    {
        //把"null"转换为""
        private string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }

        #region GetUserLevel：取用户帐户等级(取决于角色的优先级)
        /// <summary>
        /// 取用户帐户等级
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public String GetUserLevel(int id, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                        new SqlParameter("@id",id),
                                        new SqlParameter("@result",SqlDbType.Int)};
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecNonQueryProc("pr_b_GetUserLevel", ref arrSP);
                    return Convert.ToString(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion

        #region GetUserInfo：根据用户编号，获取用户信息
        /// <summary>
        /// 根据用户编号，获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public string[] GetRoleInfo(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select a.*,b.departname
                                            from p_user a left join p_depart b 
                                            on a.departcode=b.departcode 
                                            where isnull(a.IsDel,0)<>1 
		                                        and a.usercode='{0}'", id);
            string strSQL1 = string.Format(@"select a.rolecode rolecode,b.rolename rolename 
                                             from p_user_role a,p_role b 
                                             where a.rolecode=b.rolecode and isnull(b.IsDel,0)<>1
	                                            and a.usercode='{0}'", id);
            string[] values = new string[26];
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        values[0] = dr["loginname"].ToString();
                        values[1] = dr["username"].ToString();
                        values[2] = dr["password"].ToString();
                        values[3] = dr["departcode"].ToString();
                        values[4] = changeNull(dr["priweb"].ToString());
                        values[5] = changeNull(dr["pubweb"].ToString());
                        values[6] = changeNull(dr["tel"].ToString());
                        values[7] = changeNull(dr["hometel"].ToString());
                        values[8] = changeNull(dr["officefax"].ToString());
                        values[9] = changeNull(dr["zipcode"].ToString());
                        values[10] = changeNull(dr["mobile"].ToString());
                        values[11] = changeNull(dr["mobile1"].ToString());
                        values[12] = changeNull(dr["email"].ToString());
                        values[13] = changeNull(dr["address"].ToString());
                        values[14] = changeNull(dr["sex"].ToString());
                        values[15] = changeNull(dr["areacode"].ToString());
                        values[16] = changeNull(dr["birthday"].ToString());
                        values[17] = changeNull(dr["photo"].ToString());
                        values[18] = changeNull(dr["memo"].ToString());
                        values[19] = dr["departname"].ToString();
                        values[20] = changeNull(dr["centerusercode"].ToString());
                        values[21] = changeNull(dr["videolevel"].ToString());

                    }
                    dr.Close();

                    dr = dl.ExecReaderSql(strSQL1);
                    StringBuilder ids = new StringBuilder();
                    StringBuilder names = new StringBuilder();
                    while (dr.Read())
                    {
                        ids.Append(dr["rolecode"].ToString() + ",");
                        names.Append(dr["rolename"].ToString() + ",");
                    }
                    dr.Close();
                    //roleids,rolenames
                    if (ids.Length > 0)
                    {
                        values[22] = ids.ToString().Substring(0, ids.Length - 1);
                        values[23] = names.ToString().Substring(0, names.Length - 1);
                    }
                    else
                    {
                        values[22] = "";
                        values[23] = "";
                    }
                }
                return values;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
        #region GetUserInfo1：根据用户编号，获取用户信息
        /// <summary>
        /// 根据用户编号，获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public string[] GetRoleInfo1(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select a.*,b.departname,c.streetname
                                            from p_user a left join p_depart b 
                                            on a.departcode=b.departcode left join 
                                            s_street c on a.hcpower=c.streetcode
                                            where isnull(a.IsDel,0)<>1 
		                                        and a.usercode='{0}'", id);
            string strSQL1 = string.Format(@"select a.rolecode rolecode,b.rolename rolename 
                                             from p_user_role a,p_role b 
                                             where a.rolecode=b.rolecode and isnull(b.IsDel,0)<>1
	                                            and a.usercode='{0}'", id);
            string[] values = new string[27];
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        values[0] = dr["loginname"].ToString();
                        values[1] = dr["username"].ToString();
                        values[2] = dr["password"].ToString();
                        values[3] = dr["departcode"].ToString();
                        values[4] = changeNull(dr["priweb"].ToString());
                        values[5] = changeNull(dr["pubweb"].ToString());
                        values[6] = changeNull(dr["tel"].ToString());
                        values[7] = changeNull(dr["hometel"].ToString());
                        values[8] = changeNull(dr["officefax"].ToString());
                        values[9] = changeNull(dr["zipcode"].ToString());
                        values[10] = changeNull(dr["mobile"].ToString());
                        values[11] = changeNull(dr["mobile1"].ToString());
                        values[12] = changeNull(dr["email"].ToString());
                        values[13] = changeNull(dr["address"].ToString());
                        values[14] = changeNull(dr["sex"].ToString());
                        values[15] = changeNull(dr["areacode"].ToString());
                        values[16] = changeNull(dr["birthday"].ToString());
                        values[17] = changeNull(dr["photo"].ToString());
                        values[18] = changeNull(dr["memo"].ToString());
                        values[19] = dr["departname"].ToString();
                        values[20] = changeNull(dr["centerusercode"].ToString());
                        values[21] = changeNull(dr["videolevel"].ToString());
                        values[24] = changeNull(dr["streetname"].ToString());
                        values[25] = changeNull(dr["hcpower"].ToString());
                        values[26] = changeNull(dr["sort"].ToString());
                    }
                    dr.Close();

                    dr = dl.ExecReaderSql(strSQL1);
                    StringBuilder ids = new StringBuilder();
                    StringBuilder names = new StringBuilder();
                    while (dr.Read())
                    {
                        ids.Append(dr["rolecode"].ToString() + ",");
                        names.Append(dr["rolename"].ToString() + ",");
                    }
                    dr.Close();
                    //roleids,rolenames
                    if (ids.Length > 0)
                    {
                        values[22] = ids.ToString().Substring(0, ids.Length - 1);
                        values[23] = names.ToString().Substring(0, names.Length - 1);
                    }
                    else
                    {
                        values[22] = "";
                        values[23] = "";
                    }
                }
                return values;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
        #region CheckLoginName：判断登陆名是否存在
        /// <summary>
        /// 判断登陆名是否存在
        /// </summary>
        /// <param name="loginName">登陆名字</param>
        /// <returns></returns>
        public bool CheckLoginName(string loginName, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from p_user
                                            where loginname = '{0}'", loginName);
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

        #region 检查呼叫中心工号
        public bool CheckCallCenterUserID(string callCenterUserID, string usercode, ref string strErr)
        {
            string strSQL = "";
            if (usercode == "-1")
            {
                strSQL = string.Format(@"select count(*) from p_user
                                            where centerusercode = '{0}'", callCenterUserID);
            }
            else
            {
                strSQL = string.Format(@"select count(*) from p_user
                                            where centerusercode = '{0}' and usercode!={1}", callCenterUserID, usercode);
            }
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

        #region UpdateUser：更新用户信息

        public int UpdateUser(int id, string[] values, string delRoleID,
                                    string addRoleID, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                        new SqlParameter("@id",id),
                                        new SqlParameter("@loginname",values[0]),
			                            new SqlParameter("@username",values[1]),
                                        new SqlParameter("@password",values[2]),
				                        new SqlParameter("@departcode",Convert.ToInt32(values[3])),
                                        new SqlParameter("@priweb",values[4]),
                                        new SqlParameter("@pubweb",values[5]),
                                        new SqlParameter("@tel",values[6]),
                                        new SqlParameter("@hometel",values[7]),
                                        new SqlParameter("@officefax",values[8]),
                                        new SqlParameter("@zipcode",values[9]),
                                        new SqlParameter("@mobile",values[10]),
                                        new SqlParameter("@mobile2",values[11]),
                                        new SqlParameter("@email",values[12]),
                                        new SqlParameter("@address",values[13]),
                                        new SqlParameter("@sex",Convert.ToChar(values[14])),
                                        new SqlParameter("@areacode",values[15]),
                                        new SqlParameter("@birthday",values[16]),
                                        new SqlParameter("@ca",""),
                                        new SqlParameter("@photo",values[17]),
                                        new SqlParameter("@memo",values[18]),
                                        new SqlParameter("@delroleid",delRoleID),
                                        new SqlParameter("@addroleid",addRoleID),
                                        new SqlParameter("@centerusercode",values[20]),
                                        new SqlParameter("@videolevel",values[21]),
                                        new SqlParameter("@sort",values[22]),
                                        new SqlParameter("@result",SqlDbType.Int)};
            arrSP[25].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecNonQueryProc("pr_p_UpdateToLoginUser", ref arrSP);
                    return Convert.ToInt32(arrSP[25].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
        #region UpdateUser1：更新用户信息

        public int UpdateUser1(int id, string[] values, string delRoleID,
                                    string addRoleID, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                        new SqlParameter("@id",id),
                                        new SqlParameter("@loginname",values[0]),
			                            new SqlParameter("@username",values[1]),
                                        new SqlParameter("@password",values[2]),
				                        new SqlParameter("@departcode",Convert.ToInt32(values[3])),
                                        new SqlParameter("@priweb",values[4]),
                                        new SqlParameter("@pubweb",values[5]),
                                        new SqlParameter("@tel",values[6]),
                                        new SqlParameter("@hometel",values[7]),
                                        new SqlParameter("@officefax",values[8]),
                                        new SqlParameter("@zipcode",values[9]),
                                        new SqlParameter("@mobile",values[10]),
                                        new SqlParameter("@mobile2",values[11]),
                                        new SqlParameter("@email",values[12]),
                                        new SqlParameter("@address",values[13]),
                                        new SqlParameter("@sex",Convert.ToChar(values[14])),
                                        new SqlParameter("@areacode",values[15]),
                                        new SqlParameter("@birthday",values[16]),
                                        new SqlParameter("@ca",""),
                                        new SqlParameter("@photo",values[17]),
                                        new SqlParameter("@memo",values[18]),
                                        new SqlParameter("@delroleid",delRoleID),
                                        new SqlParameter("@addroleid",addRoleID),
                                        new SqlParameter("@centerusercode",values[20]),
                                        new SqlParameter("@videolevel",values[21]),
                                        new SqlParameter("@hcpower",values[22]),
                                        new SqlParameter("@sort",values[23]),
                                        new SqlParameter("@result",SqlDbType.Int)};
            arrSP[27].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecNonQueryProc("pr_p_UpdateToLoginUser_erqi", ref arrSP);
                    return Convert.ToInt32(arrSP[27].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
        #region InsertUser：插入用户信息

        public int InsertUser(string[] values, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@loginname",values[0]),
                                                new SqlParameter("@username",values[1]),
                                                new SqlParameter("@password",values[2]),
                                                new SqlParameter("@departcode",Convert.ToInt32(values[3])),
                                                new SqlParameter("@regdate",DateTime.Now),
                                                new SqlParameter("@priweb",values[4]),
                                                new SqlParameter("@pubweb",values[5]),
                                                new SqlParameter("@tel",values[6]),
                                                new SqlParameter("@hometel",values[7]),
                                                new SqlParameter("@officefax",values[8]),
                                                new SqlParameter("@zipcode",values[9]),
                                                new SqlParameter("@mobile",values[10]),
                                                new SqlParameter("@mobile2",values[11]),
                                                new SqlParameter("@email",values[12]),
                                                new SqlParameter("@address",values[13]),
                                                new SqlParameter("@sex",Convert.ToChar(values[14])),
                                                new SqlParameter("@areacode",values[15]),
                                                new SqlParameter("@birthday",values[16]),
	                                            new SqlParameter("@ca",""),
                                                new SqlParameter("@photo",values[17]),
	                                            new SqlParameter("@memo",values[18]),
                                                new SqlParameter("@roleid",values[19]),
                                                new SqlParameter("@centerusercode",values[20]),
                                                new SqlParameter("@videolevel",values[21]),
                                                new SqlParameter("@sort",values[22]),
                                                new SqlParameter("@result",SqlDbType.Int)};
            arrSP[24].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecNonQueryProc("pr_p_InsertToLoginUser", ref arrSP);
                    return Convert.ToInt32(arrSP[24].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
        #region InsertUser1：插入用户信息

        public int InsertUser1(string[] values, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@loginname",values[0]),
                                                new SqlParameter("@username",values[1]),
                                                new SqlParameter("@password",values[2]),
                                                new SqlParameter("@departcode",Convert.ToInt32(values[3])),
                                                new SqlParameter("@regdate",DateTime.Now),
                                                new SqlParameter("@priweb",values[4]),
                                                new SqlParameter("@pubweb",values[5]),
                                                new SqlParameter("@tel",values[6]),
                                                new SqlParameter("@hometel",values[7]),
                                                new SqlParameter("@officefax",values[8]),
                                                new SqlParameter("@zipcode",values[9]),
                                                new SqlParameter("@mobile",values[10]),
                                                new SqlParameter("@mobile2",values[11]),
                                                new SqlParameter("@email",values[12]),
                                                new SqlParameter("@address",values[13]),
                                                new SqlParameter("@sex",Convert.ToChar(values[14])),
                                                new SqlParameter("@areacode",values[15]),
                                                new SqlParameter("@birthday",values[16]),
	                                            new SqlParameter("@ca",""),
                                                new SqlParameter("@photo",values[17]),
	                                            new SqlParameter("@memo",values[18]),
                                                new SqlParameter("@roleid",values[19]),
                                                new SqlParameter("@centerusercode",values[20]),
                                                new SqlParameter("@videolevel",values[21]),
                                                new SqlParameter("@hcpower",values[22]),
                                                new SqlParameter("@result",SqlDbType.Int)};
            arrSP[26].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecNonQueryProc("pr_p_InsertToLoginUser_erqi", ref arrSP);
                    return Convert.ToInt32(arrSP[26].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region DeleteUser：删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public int DeleteUser(string usercode, ref string strErr)
        {
            string strSQL = string.Format(@"update p_user set IsDel = 1
                                            where usercode = '{0}'", usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecScalarSql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
        #region GetStreet:获取街道
        public DataSet GetStreet()
        {
            string sql = "select '0' as streetcode ,'全部' as streetname union select streetcode,streetname from s_street";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(sql);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        public int ChagePassWord(string usercode, string passowrd)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                        new SqlParameter("@Password",passowrd),
                                        new SqlParameter("@UserCode",usercode)
                                        };

            string strSQL = string.Format(@"update p_user set password = @Password
                                            where usercode =@UserCode", usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecuteScalar(strSQL, CommandType.Text, arrSP));
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool HasUse(string userCode,string passWord)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                        new SqlParameter("@UserCode",userCode),
                                         new SqlParameter("@PassWord",passWord)
                                        };

            string strSQL = string.Format(@"select * from p_user
                                            where usercode =@UserCode and password=@PassWord and isnull(IsDel,0)<>1", userCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecuteDataset(strSQL, CommandType.Text, arrSP);
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
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
                return false;
            }
        }
      
    }
}
