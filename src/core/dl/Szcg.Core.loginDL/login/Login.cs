/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：Login 登陆模块的数据访问层类

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
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.login
{
    public class Login : Teamax.Common.CommonDatabase
    {
        #region LoginValidate：登陆身份校验

        /// <summary>
        /// 登陆身份校验
        /// </summary>
        /// <param name="loginname">登陆用户名</param>
        /// <param name="password">密码</param>
        /// <param name="iReturn"></param>
        /// <returns>用户相关的数据集</returns>
        public DataSet LoginValidate(string loginname, string password,ref object iReturn)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@loginname", loginname),
                new SqlParameter("@password", password),
                new SqlParameter("@ret",SqlDbType.Int)
            };
            spInputs[2].Direction = ParameterDirection.ReturnValue;

            DataSet ds = this.ExecuteDataset("pr_sys_validate", CommandType.StoredProcedure, spInputs);
            iReturn = spInputs[2].Value;

            return ds; 
        }
        #endregion

        #region GetRolename：根据角色code获得角色名称
        /// <summary>
        /// 根据角色code获得角色名称
        /// </summary>
        /// <param name="rolecode">角色code</param>
        /// <returns>角色名称</returns>
        public object GetRolename(string rolecode)
        {
            string strSQL = string.Format(@"select rolename from p_role where rolecode = {0}",rolecode);

            return this.ExecuteScalar(strSQL);
        }
        #endregion

        #region GetRoleAreacode：根据角色code获得角色所在区域

        /// <summary>
        /// 根据角色code获得角色所在区域

        /// </summary>
        /// <param name="rolecode">角色code</param>
        /// <returns>角色名称</returns>
        public object GetRoleAreacode(string rolecode)
        {
            string strSQL = string.Format(@"select areacode from p_role where rolecode = {0}", rolecode);

            return this.ExecuteScalar(strSQL);
        }
        #endregion

        #region GetRoleInfos：根据系统syscode获得角色信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syscode"></param>
        /// <returns></returns>
        public IDataReader GetRoleInfos(string syscode)
        {
            string strSQL = string.Format(@"select top 1  *  From p_role 
                                                            where rolecode in (
                                                            select rolecode From dbo.p_role_modelpower
                                                            where buttoncode = '{0}')  order by stepid asc ", syscode);
            return this.ExecuteReader(strSQL);
        }
        #endregion

        #region GetRoleInfos：根据系统syscode获得角色信息
        /// <summary>
        /// GetRoleInfos：根据系统syscode获得角色信息
        /// </summary>
        /// <param name="syscode">子系统模块ID如：11,27</param>
        /// <param name="rolecodes">当前用户拥有的角色如:1,15,23</param>
        /// <returns></returns>
        public IDataReader GetRoleInfos(string syscode, string rolecodes)
        {
            string strSQL = string.Format(@"select top 1  *  From p_role 
                                                            where rolecode in (
                                                            select rolecode From dbo.p_role_modelpower
                                                            where buttoncode = '{0}' and rolecode in({1}))  order by stepid asc ", syscode, rolecodes);
            return this.ExecuteReader(strSQL);
        }
        #endregion

        #region GetRoleInfo：根据角色code获得该角色信息

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public IDataReader GetRoleInfo(string rolecode)
        {
            string strSQL = string.Format(@"select * from p_role where rolecode = {0}", rolecode);

            return this.ExecuteReader(strSQL);
        }
        #endregion

        #region GetSystemIds：根据角色code获得该角色有权限操作的系统模块

        /// <summary>
        /// 根据角色code获得该角色有权限操作的系统模块

        /// </summary>
        /// <param name="modelcode">子系统ID</param>
        /// <param name="rolecode">角色code</param>
        /// <returns></returns>
        public IDataReader GetSystemIds(string modelcode, string rolecode)
        {
            string sql = string.Format(@" 
                            select a.modelcode,isnull(isvisible,0) as isvisible
                            from dbo.p_model a 
                            left join 
                            (
	                            select distinct rolecode,left(ButtonCode,5) modelcode,1 as isvisible
	                            FROM dbo.p_role_modelpower 
	                            where rolecode={0}
                            ) b
                            on a.modelcode= b.modelcode
                            where  a.modelcode like '{1}%' and len(a.modelcode)=5", rolecode, modelcode);

            return this.ExecuteReader(sql, CommandType.Text, false);
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
        public IDataReader GetRoleModelPower(string modelcode, string rolecode,int len)
        {
            string sql = string.Format(@" 
                                select distinct left(ButtonCode,{2}) modelcode
	                            FROM dbo.p_role_modelpower 
	                            where rolecode={0} 
                                    and ButtonCode like '{1}%'
                                order by modelcode", rolecode, modelcode, len);

            return this.ExecuteReader(sql);
        }
        #endregion

        #region GetUserInfo 根据用户代码获取用户信息  webService方法
        /// <summary>
        /// 根据用户代码获取用户信息
        /// </summary>
        /// <param name="userCode">用户代码</param>
        public DataSet GetUserInfo(string userCode)
        {
            string strSql = @"select * from P_user where usercode=" + userCode;
            return this.ExecuteDataset(strSql);

        }
        #endregion
    }
}
