using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    /// <summary>
    /// 权限类接口
    /// </summary>
    public interface IPermissionService
    {

        #region [ 权限 ]

        /// <summary>
        /// 获取所有系统模块
        /// </summary>
        /// <returns></returns>
        List<SystemModel> GetSystemModels();

        /// <summary>
        /// 获取当前角色所拥有的系统模块
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        List<SystemModel> GetSystemModelsByRoleId(string roleId);

        /// <summary>
        /// 更新当前角色所拥有的系统模块
        /// </summary>
        /// <param name="roleid">角色Id</param>
        /// <param name="nodelist">系统模块列表</param>
        /// <returns></returns>
        bool UpdateSystemModel(string roleid, string nodelist);

        #endregion

        #region [ 角色 ]

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoles();


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        bool InsertRole(Role role);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">新角色实体</param>
        /// <returns></returns>
        bool UpdateRole(Role role);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <returns></returns>
        int DeleteRole(int roleId);

        /// <summary>
        /// 根据角色编码获取角色
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <returns></returns>
        Role GetRole(int roleId);

        /// <summary>
        /// 获取当前用户的角色列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        List<Role> GetRoleList(int userCode);
        
        /// <summary>
        /// 获取角色步骤列表
        /// </summary>
        /// <returns></returns>
        List<RoleStep> GetStepList();

        #endregion

        #region [ 授权 ]

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="purview"></param>
        /// <returns></returns>
        bool AccreditPurview(AccreditPurview purview);


        #endregion

        /// <summary>
        /// 获取用户角色树
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        List<Role> GetRoleTree(string areacode, int userCode);


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        szcg.com.teamax.business.entity.UserInfo LoginValidate(string userName, string passWord);

        /// <summary>
        /// 根据角色code获得角色所在区域和阶段
        /// </summary>
        /// <param name="systemId">角色编码(子系统模块ID如：11,27)</param>
        /// <param name="roleId">输出阶段</param>
        /// <param name="areaCode">输出区域</param>
        /// <param name="roleIds">当前用户拥有的角色如:1,15,23</param>
        /// <returns></returns>
        void GetRoleStepBySysCode(string systemId, out string roleId, out string areaCode, string roleIds);

        /// <summary>
        /// 根据用户编码获得该用户在指定系统下面有权限操作的模块，精确到按钮级别
        /// </summary>
        /// <param name="systemId">子系统ID</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        string GetUserModelPower(string systemId, int userCode);

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="modelCode">父模块编号</param>
        /// <param name="systemId">系统编号</param>
        /// <returns></returns>
        List<FlowNodePower> GetFlowNodePower(string roleCode, string modelCode, string systemId);

    }
}
