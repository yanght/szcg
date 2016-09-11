/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：LoginModel 登陆模块的业务逻辑层类
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-05-29
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using szcg.com.teamax.business.entity;

namespace szcg.com.teamax
{
   
	/// <summary>
	/// LoginModel 登陆的业务类。
	/// </summary>
	public class LoginModel
	{
        #region login_validate：系统登陆，用户身份校验
        /// <summary>
        /// 系统登陆，用户身份校验
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserInfo LoginValidate(string loginname, string password,out string ErrMsg)
        {
            ErrMsg = "";
            DataSet ds;
            try
            {
                using (bacgDL.login.Login dl = new bacgDL.login.Login())
                {
                    object oReturn = null;
                    ds = dl.LoginValidate(loginname, password, ref oReturn);
                    if (ds == null || oReturn == null || oReturn.ToString() == "0")
                        return null;
                }
            }
            catch(Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
            
            //用户相关信息
            szcg.com.teamax.business.entity.UserInfo uinfo = new szcg.com.teamax.business.entity.UserInfo();
            uinfo.setUsercode(System.Convert.ToInt32(ds.Tables[0].Rows[0]["usercode"]));
            uinfo.setUsername(System.Convert.ToString(ds.Tables[0].Rows[0]["username"]));
            uinfo.setLoginname(System.Convert.ToString(ds.Tables[0].Rows[0]["loginname"]));
            uinfo.setDepartcode(System.Convert.ToInt32(ds.Tables[0].Rows[0]["departcode"]));
            uinfo.setAreacode(System.Convert.ToString(ds.Tables[0].Rows[0]["areacode"]));
            uinfo.setDepartname(System.Convert.ToString(ds.Tables[0].Rows[0]["departname"]));
            uinfo.setDepartDefinedcode(System.Convert.ToString(ds.Tables[0].Rows[0]["UserDefinedCode"]));
            uinfo.setHcpower(System.Convert.ToString(ds.Tables[0].Rows[0]["hcpower"]));
            //用户拥有的角色
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            foreach(DataRow dr in ds.Tables[1].Rows)
            {
                al.Add(System.Convert.ToString(dr[0]));
            }
            uinfo.setRole(al);
            if (al.Count != 0)
                uinfo.CurrentRole = int.Parse(al[0].ToString());
            else
                uinfo.CurrentRole = 0;

            //用户能访问的系统ID
            al.Clear();
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                al.Add(dr[0]);
            }
            uinfo.setSystemid(al);

            al.Clear();
            al = null;

            return uinfo;
        }
        #endregion      

        #region GetRoleStepBySysCode：根据系统编码syscode获得角色所在区域和阶段
        /// <summary>
        /// 根据角色code获得角色所在区域和阶段
        /// </summary>
        /// <param name="syscode">角色编码</param>
        /// <param name="rolecode">输出阶段</param>
        /// <param name="strAreacode">输出区域</param>
        /// <returns></returns>
        public static void GetRoleStepBySysCode(string syscode, out string rolecode, out string strAreacode)
        {
            rolecode = "";
            strAreacode = "";

            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr = dl.GetRoleInfos(syscode);
                if (dr.Read())
                {
                    rolecode = dr["rolecode"].ToString();
                    strAreacode = dr["areacode"].ToString();
                }
                dr.Close();
            }
        }
        #endregion

        #region GetRoleStepBySysCode：根据系统编码syscode获得角色所在区域和阶段
        /// <summary>
        /// 根据角色code获得角色所在区域和阶段
        /// </summary>
        /// <param name="syscode">角色编码(子系统模块ID如：11,27)</param>
        /// <param name="rolecode">输出阶段</param>
        /// <param name="strAreacode">输出区域</param>
        /// <param name="rolecodes">当前用户拥有的角色如:1,15,23</param>
        /// <returns></returns>
        public static void GetRoleStepBySysCode(string syscode, out string rolecode, out string strAreacode, string rolecodes)
        {
            rolecode = "";
            strAreacode = "";

            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr = dl.GetRoleInfos(syscode,rolecodes);
                if (dr.Read())
                {
                    rolecode = dr["rolecode"].ToString();
                    strAreacode = dr["areacode"].ToString();
                }
                dr.Close();
            }
        }
        #endregion

        #region GetRoleStepidAreaCode：根据角色code获得角色所在区域和阶段
        /// <summary>
        /// 根据角色code获得角色所在区域和阶段
        /// </summary>
        /// <param name="rolecode">角色编码</param>
        /// <param name="iStepid">输出阶段</param>
        /// <param name="strAreacode">输出区域</param>
        /// <returns></returns>
        public static void GetRoleStepidAreaCode(string rolecode, out int iStepid, out string strAreacode)
        {
            strAreacode = "";
            iStepid = 0;

            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr = dl.GetRoleInfo(rolecode);
                if (dr.Read())
                {
                    iStepid = Convert.ToInt32(dr["stepid"]);
                    strAreacode = dr["areacode"].ToString();
                }
                dr.Close();
            }
        }
        #endregion

        #region GetRoleInfo：根据角色code获得该角色信息
        /// <summary>
        ///根据角色code获得该角色信息
        /// </summary>
        /// <param name="rolecode">角色编码</param>
        /// <param name="iStepid">输出阶段</param>
        /// <param name="strAreacode">输出区域</param>
        /// <returns></returns>
        public static IDataReader GetRoleInfo(string rolecode)
        {
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                return dl.GetRoleInfo(rolecode);
            }
        }
        #endregion

        #region GetRoleAreacode：根据角色code获得角色所在区域
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public static string GetRoleAreacode(string rolecode)
		{
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                return dl.GetRoleAreacode(rolecode).ToString();
            }
        }
        #endregion

        #region GetRolename：根据角色code获得角色名称
        /// <summary>
        /// 根据角色code获得角色名称
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetRolename(object rolecode)
        {
            string strCacheKey = "GetRolename" + rolecode.ToString();
            string strReturn = (string)Teamax.Common.MyCache.Get(strCacheKey);//获取缓存信息
            if (strReturn != null)
                return strReturn;

            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                strReturn = dl.GetRolename(rolecode.ToString()).ToString();
                Teamax.Common.MyCache.Insert(strCacheKey, strReturn, 7200);
                return strReturn;
            }
        }
        #endregion

        #region GetSystemModelPowerFromRole：根据角色编码获得该角色有权限操作的某个系统的权限模块
        /// <summary>
        /// 根据角色编码获得该角色有权限操作的系统模块
        /// </summary>
        /// <param name="modelcode">某个系统编码，比如：GPS系统</param>
        /// <param name="rolecode">某个人所拥有的角色集合</param>
        /// <returns></returns>
        public static string GetSystemModelPowerFromRole(string modelcode, string[] rolecodes)
        {
            if (rolecodes == null)
                return "";
            string strReturn = "";
            string rolename, strSystemid, areacode;
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                for (int i = 0; i < rolecodes.Length; i++)
                {
                    rolename = dl.GetRolename(rolecodes[i]).ToString();

                    strSystemid = "";
                    IDataReader dr = dl.GetSystemIds(modelcode, rolecodes[i]);
                    while (dr.Read())
                    {
                        strSystemid += Convert.ToString(dr["isvisible"]);
                    }
                    dr.Close();

                    areacode = dl.GetRoleAreacode(rolecodes[i]).ToString();

                    strReturn += rolecodes[i] + "|" + rolename + "|" + strSystemid + "|" + areacode + ";";
                }
            }
            return strReturn.Trim(';');
        }
        #endregion

        #region GetRoleModelPower：根据角色code获得该角色，指定系统下面有权限操作的模块
        /// <summary>
        /// 根据角色code获得该角色，指定系统下面有权限操作的模块
        /// </summary>
        /// <param name="modelcode">子系统ID</param>
        /// <param name="rolecode">角色code</param>
        /// <param name="len">截至模块编码的长度</param>
        /// <returns></returns>
        public static string GetRoleModelPower(string modelcode, string rolecode, int len)
        {
            string strReturn = "";
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr = dl.GetRoleModelPower(modelcode, rolecode,len);
                while (dr.Read())
                {
                    strReturn += dr[0].ToString() + ",";
                }
                dr.Close();
            }

            return strReturn.Trim(',');
        }
        #endregion

        #region GetUserModelPower：根据用户编码获得该用户在指定系统下面有权限操作的模块
        /// <summary>
        /// 根据用户编码获得该用户在指定系统下面有权限操作的模块，精确到按钮级别
        /// </summary>
        /// <param name="modelcode">子系统ID</param>
        /// <param name="rolecode">用户编码</param>
        /// <returns></returns>
        public static string GetUserModelPower(string SysId, int UserCode)
        {
            string strReturn = "";
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                string strSQL = string.Format(@"
                            select distinct A.ButtonCode
                            from dbo.p_role_modelpower A,(
	                            select rolecode
	                            from dbo.p_user_role
	                            where UserCode = {0}
	                            union 
	                            select rolecode
	                            from dbo.p_consignrole
	                            where Accepter = {0}
		                            and getdate() between StartDate and EndDate
                            ) B
                            where A.Rolecode=B.rolecode
	                            and A.ButtonCode like '{1}%'
                            order by ButtonCode", UserCode,SysId);

                IDataReader dr = dl.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    strReturn += dr[0].ToString() + ",";
                }
                dr.Close();
            }

            return strReturn.Trim(',');
        }
        #endregion

        #region GetAllRoles：根据角色编码集获得角色集相关的所有角色信息
        /// <summary>
        /// 根据角色编码集获得角色集相关的所有角色信息
        /// </summary>
        /// <param name="modelcode">系统编码</param>
        /// <param name="rolecode">角色</param>
        /// <returns></returns>
        public static string GetAllRoles(string[] rolecodes)
        {
            string strReturn = "";
            string step ="", areacode="", strRoleName="";

            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr;

                for (int i = 0; i < rolecodes.Length; i++)
                {
                    dr = dl.GetRoleInfo(rolecodes[i]);
                    if (dr.Read())
                    {
                        step = dr["stepid"].ToString();
                        areacode = dr["areacode"].ToString();
                        strRoleName = dr["rolename"].ToString();
                    }
                    dr.Close();

                    strReturn += strRoleName + "$" + step + "$" + rolecodes[i] + "$" + areacode + ","; 
                }
            }

            return strReturn.Trim(',');
        }
        #endregion

        #region GetAllRoles：根据角色编码集获得角色集相关的所有角色信息
        /// <summary>
        /// 根据角色编码集获得角色集相关的所有角色信息
        /// </summary>
        /// <param name="modelcode">系统编码</param>
        /// <param name="rolecode">角色</param>
        /// <returns></returns>
        public static string GetUserAndDepartInfo(string UserCode)
        {
            string strSQL = string.Format(@"
                        select 
	                        a.usercode,
	                        a.loginname,
	                        a.UserName,
	                        case when isnull(a.sex,0)=1 then '男' else '女' end as sex,
	                        b.departcode,
	                        b.UserDefinedCode,
	                        b.departname,
	                        b.parentcode,
	                        c.UserDefinedCode as parent_UserDefinedCode,
	                        c.departname as parent_departname,
	                        c.parentcode as parent_parentcode
                        from P_user a 
                        inner join dbo.p_depart b 
                        on a.departcode = b.departcode
                        left join dbo.p_depart c 
                        on b.parentcode = c.departcode
                        where UserCode={0}", UserCode);

            string strResult = "";
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                IDataReader dr = dl.ExecuteReader(strSQL);
                if (dr.Read())
                {
                    //strResult = string.Format("usercode={0},loginname={1},UserName={2},sex={3},departcode={4},UserDefinedCode={5},departname={6},parentcode={7},parent_UserDefinedCode={8},parent_departname={9},parent_parentcode={10}",
                    strResult = string.Format("a1={0},a2={1},a3={2},a4={3},b1={4},b2={5},b3={6},b4={7},c1={8},c2={9},c3={10}",
                        dr["usercode"],
                        dr["loginname"],
                        dr["UserName"],
                        dr["sex"],
                        dr["departcode"],
                        dr["UserDefinedCode"],
                        dr["departname"],
                        dr["parentcode"],
                        dr["parent_UserDefinedCode"],
                        dr["parent_departname"],
                        dr["parent_parentcode"]);
                }
                dr.Close();
            }

            return strResult;
        }
        #endregion

        #region GetUserInfo 根据用户代码获取用户信息  webService方法
        /// <summary>
        /// 根据用户代码获取用户信息
        /// </summary>
        /// <param name="userCode">用户代码</param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string userCode)
        {
            DataSet ds;
            using (bacgDL.login.Login dl = new bacgDL.login.Login())
            {
                ds = dl.GetUserInfo(userCode);
            }

            if (ds == null) return null;

            //用户相关信息
            szcg.com.teamax.business.entity.UserInfo uinfo = new szcg.com.teamax.business.entity.UserInfo();
            uinfo.setUsercode(System.Convert.ToInt32(ds.Tables[0].Rows[0]["usercode"]));
            uinfo.setUsername(System.Convert.ToString(ds.Tables[0].Rows[0]["username"]));
            uinfo.setLoginname(System.Convert.ToString(ds.Tables[0].Rows[0]["loginname"]));
            uinfo.setDepartcode(System.Convert.ToInt32(ds.Tables[0].Rows[0]["departcode"]));
            uinfo.setAreacode(System.Convert.ToString(ds.Tables[0].Rows[0]["areacode"]));

            return uinfo;
        }
        #endregion  
    }
}
