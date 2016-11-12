using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using szcg.com.teamax.business.entity;
using Szcg.Service.Model;

namespace Szcg.Service.IBussiness
{
    public interface IOrganizeService
    {
        /// <summary>
        /// 根据部门Id获取用户列表
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="userName">用户名称</param>
        /// <param name="loginName">用户登录名</param>
        /// <param name="departName">部门名称</param>
        /// <param name="order">排序</param>
        /// <param name="filds">排序字段</param>
        /// <returns></returns>
        List<UserInfo> GetUserByDeptID(int departId, PageInfo pageInfo, int userId, string userName, string loginName, string departName, string order, string filds);

        /// <summary>
        /// 根据用户编码获取用户
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        UserData GetUserById(int userCode);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool InsertUser(UserData user);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="user"></param>
        /// <param name="oldRoleID"></param>
        /// <returns></returns>
        bool UpdateUser(int userCode, UserData user, string oldRoleID);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        bool DeleteUser(int userCode);

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="depart">部门实体</param>
        /// <returns></returns>
        ReturnValue InsertDepart(Depart depart);

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="depart">部门实体</param>
        /// <returns></returns>
        ReturnValue UpdateDepart(Depart depart);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <returns>0：删除发生异常1：标识删除成功2：部门存在人员，删除失败3：部门存在子部门，删除失败</returns>
        int DeleteDepart(string departId);

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <returns></returns>
        Depart GetDepartInfo(string departId);

        /// <summary>
        /// 获取职能部门列表
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        List<Depart> GetDepartList(string areaCode);

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="departCode">部门编码</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        List<Depart> GetDepartList(string areaCode, string departCode, string userCode);

        /// <summary>
        /// 获取职能部门列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="typecode">案卷类型</param>
        /// <returns></returns>
        List<Depart> GetDutyDepart(string areacode, string typecode);

        /// <summary>
        /// 检查部门名称是否存在
        /// </summary>
        /// <param name="parentDepartId">父级部门Id</param>
        /// <param name="departName">部门名称</param>
        /// <returns></returns>
        bool CheckDepartName(string parentDepartId, string departName);



    }
}
