using bacgBL.web.szbase.entity;
using bacgBL.web.szbase.organize;
using bacgBL.web.szbase.purview;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using szcg.com.teamax;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    /// <summary>
    /// 角色权限类
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private string strErr = "";

        #region [ 权限 ]

        /// <summary>
        /// 获取所有系统模块
        /// </summary>
        /// <returns></returns>
        public List<SystemModel> GetSystemModels()
        {
            List<SystemModel> list = new List<SystemModel>();

            Purviews bl = new Purviews();

            ArrayList treeStructList = bl.GetModelTreeList(ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取所有系统模块异常：" + strErr);
            }

            foreach (var item in treeStructList)
            {
                TreeSuruct ts = (TreeSuruct)item;

                SystemModel model = new SystemModel()
                {
                    Memo = ts.tag,
                    NodeCode = ts.code,
                    NodeName = ts.text,
                    ParentCode = ts.pcode
                };

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取当前角色所拥有的系统模块
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public List<SystemModel> GetSystemModelsByRoleId(string roleId)
        {
            List<SystemModel> list = new List<SystemModel>();
            Purviews bl = new Purviews();
            ArrayList al = bl.GetPruviewModleCode(roleId, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取当前角色所拥有的系统模块异常：" + strErr);
            }
            foreach (var item in al)
            {
                SystemModel model = new SystemModel()
                {
                    NodeCode = (string)item
                };
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 更新当前角色所拥有的系统模块
        /// </summary>
        /// <param name="roleid">角色Id</param>
        /// <param name="nodelist">系统模块列表</param>
        /// <returns></returns>
        public bool UpdateSystemModel(string roleid, string nodelist)
        {
            Purviews bl = new Purviews();
            int i = bl.SavePurview(nodelist, roleid, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("更新当前角色所拥有的系统模块异常：" + strErr);
            }
            return i > 0;
        }

        #endregion

        #region [ 角色 ]

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public bool InsertRole(Role role)
        {
            RoleManage bl = new RoleManage();
            int temp = bl.InsertRole(role.RoleName, "", role.AreaCode,
                        role.StepId, "", ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("添加角色异常：" + strErr);
            }
            return temp > 0;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">新角色实体</param>
        /// <returns></returns>
        public bool UpdateRole(Role role)
        {
            RoleManage bl = new RoleManage();
            int temp = bl.UpdateRole(role.RoleName, "", role.AreaCode,
                                                     role.StepId, "", role.RoleCode,
                                                      ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("更新角色异常：" + strErr);
            }
            return temp > 0;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <returns>0：删除发生异常1：标识删除成功2：角色下边添加了人员</returns>
        public int DeleteRole(int roleId)
        {
            int flag = 0;
            RoleManage bl = new RoleManage();
            bl.DeleteRole(roleId, out flag, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("删除角色异常：" + strErr);
            }
            return flag;
        }

        /// <summary>
        /// 根据角色编码获取角色
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <returns></returns>
        public Role GetRole(int roleId)
        {
            Role role = new Role();
            RoleManage bl = new RoleManage();
            DataSet ds = bl.GetRoleInfo(roleId, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("根据角色获取角色异常：" + strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                role = ConvertDtHelper<Role>.GetModel(ds.Tables[0]);
            }
            return role;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            List<Role> list = new List<Role>();
            RoleManage bl = new RoleManage();
            DataSet ds = bl.GetRole(ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("获取角色列表异常：" + strErr);
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = ConvertDtHelper<Role>.GetModelList(ds.Tables[0].Rows);
            }
            return list;
        }

        /// <summary>
        /// 获取当前用户的角色列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<Role> GetRoleList(int userCode)
        {
            List<Role> list = new List<Role>();
            Purviews bl = new Purviews();
            ArrayList roleList = bl.GetRoleList(userCode.ToString(), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("案获取用户角色列表异常：" + strErr);
            }
            string roleNames = roleList[0].ToString();
            string rolecodes = roleList[1].ToString();
            if (string.IsNullOrEmpty(roleNames)) return list;
            string[] names = roleNames.Split(',');
            string[] codes = rolecodes.Split(',');

            for (int i = 0; i < names.Length; i++)
            {
                Role role = new Role()
                {
                    RoleName = names[i],
                    RoleCode = int.Parse(codes[i])
                };
                list.Add(role);
            }
            return list;
        }

        /// <summary>
        /// 获取角色步骤列表
        /// </summary>
        /// <returns></returns>
        public List<RoleStep> GetStepList()
        {
            List<RoleStep> list = new List<RoleStep>();

            RoleManage bl = new RoleManage();

            DataSet ds = bl.GetStepList(ref strErr);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("案获取用户角色步骤列表异常：" + strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RoleStep step = new RoleStep()
                    {
                        StepCode = SqlDataConverter.ToInt32(dr["stepcode"]),
                        StepName = SqlDataConverter.ToString(dr["stepname"]),
                    };
                    list.Add(step);
                }
            }
            return list;
        }

        #endregion

        #region [ 授权 ]

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="purview"></param>
        /// <returns></returns>
        public bool AccreditPurview(AccreditPurview purview)
        {
            Purviews bl = new Purviews();
            int flag = bl.SaveAccreditPurview(purview.consignerId, purview.accepterId, purview.roleList,
                                                purview.startTime.ToString(), purview.endTime.ToString(), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("角色授权异常：" + strErr);
            }
            return flag > 0;
        }

        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public szcg.com.teamax.business.entity.UserInfo LoginValidate(string userName, string passWord)
        {
            szcg.com.teamax.business.entity.UserInfo userInfo = LoginModel.LoginValidate(userName, passWord, out strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("用户登录异常：" + strErr);
            }
            return userInfo;
        }

        /// <summary>
        /// 根据角色code获得角色所在区域和阶段
        /// </summary>
        /// <param name="systemId">角色编码(子系统模块ID如：11,27)</param>
        /// <param name="roleId">输出阶段</param>
        /// <param name="areaCode">输出区域</param>
        /// <param name="roleIds">当前用户拥有的角色如:1,15,23</param>
        /// <returns></returns>
        public void GetRoleStepBySysCode(string systemId, out string roleId, out string areaCode, string roleIds)
        {
            szcg.com.teamax.LoginModel.GetRoleStepBySysCode(systemId, out roleId, out areaCode, string.Join(",", roleIds));//多角色
        }

        /// <summary>
        /// 根据用户编码获得该用户在指定系统下面有权限操作的模块，精确到按钮级别
        /// </summary>
        /// <param name="systemId">子系统ID</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public string GetUserModelPower(string systemId, int userCode)
        {
            return szcg.com.teamax.LoginModel.GetUserModelPower(systemId, userCode);
        }

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="modelCode">父模块编号</param>
        /// <param name="systemId">系统编号</param>
        /// <returns></returns>
        public List<FlowNodePower> GetFlowNodePower(string roleCode, string modelCode, string systemId)
        {
            List<FlowNodePower> rtn = new List<FlowNodePower>();

            DataSet ds = bacgBL.business.Project.GetFlowNodePower(roleCode, modelCode, systemId, strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("案件立案批转异常：" + strErr);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<FlowNodePower> list = ConvertDtHelper<FlowNodePower>.GetModelList(ds.Tables[0].Rows);
                rtn = list.FindAll((x) =>
                {
                    return x.ButtonCode.Substring(x.ButtonCode.Length-2,2) == "01";
                });

                foreach (FlowNodePower nodepower in rtn)
                {
                    nodepower.ChildPowers = list.FindAll((x) =>
                    {
                        return x.ModelCode == nodepower.ModelCode && x.ButtonCode.Substring(x.ButtonCode.Length - 2, 2) != "01";
                    });
                }

                return rtn;
            }

            return null;
        }

    }
}
