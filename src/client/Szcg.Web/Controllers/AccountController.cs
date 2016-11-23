using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Szcg.Web.Filters;
using Szcg.Web.Models;
using Szcg.Service.Model;
using Szcg.Service.IBussiness;
using Szcg.Service.Bussiness;
using Newtonsoft.Json.Linq;

namespace Szcg.Web.Controllers
{
    public class AccountController : BaseController
    {

        IPermissionService svc = new PermissionService();

        #region [ 权限管理 ]

        /// <summary>
        /// 获取所有系统模块
        /// </summary>
        /// <returns></returns>
        public AjaxFxRspJson GetSystemModels()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<SystemModel> models = svc.GetSystemModels();

            ajax.RspData.Add("models", JToken.FromObject(models));

            return ajax;
        }

        /// <summary>
        /// 获取当前用户角色所拥有的模块权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public AjaxFxRspJson GetSystemModelsByRoleId(string roleId)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(roleId))
            {
                ajax.RspMsg = string.Format("请输入角色编码(roleId:{0})", typeof(string));
                ajax.RspCode = 0;
                return ajax;
            }

            List<SystemModel> models = svc.GetSystemModelsByRoleId(roleId);

            ajax.RspData.Add("models", JToken.FromObject(models));

            return ajax;
        }

        /// <summary>
        /// 更新当前角色权限
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <param name="modelcodes">模块编码列表（多个,分隔）</param>
        /// <returns></returns>
        public AjaxFxRspJson UpdateSystemModel(string roleId, string modelcodes)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(roleId))
            {
                ajax.RspMsg = string.Format("请输入角色编码(roleId:{0})", typeof(string));
                ajax.RspCode = 0;
                return ajax;
            }

            bool rtn = svc.UpdateSystemModel(roleId, modelcodes);

            if (!rtn)
            {
                ajax.RspMsg = "更新权限失败！";
                ajax.RspCode = 0;
                return ajax;
            }

            return ajax;
        }

        /// <summary>
        /// 获取所选定角色的可控制模块和具体操作动作
        /// </summary>
        /// <param name="systemId">系统编号</param>
        /// <returns></returns>
        public AjaxFxRspJson GetFlowNodePower(string modelcode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            modelcode = UserInfo.ModelPowers.Substring(0, 2) + modelcode.Substring(2).ToString();

            List<FlowNodePower> list = svc.GetFlowNodePower(UserInfo.CurrentRole.ToString(), modelcode, UserInfo.CurrentSystemId);

            ajax.RspData.Add("nodepower", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 角色管理 ]

        /// <summary>
        /// 根据角色编码获取角色详情
        /// </summary>
        /// <param name="roleId">角色编码</param>
        /// <returns></returns>
        public AjaxFxRspJson GetRoleInfo(string roleId)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(roleId))
            {
                ajax.RspMsg = string.Format("请输入角色编码(roleId:{0})", typeof(string));
                ajax.RspCode = 0;
                return ajax;
            }

            Role role = svc.GetRole(int.Parse(roleId));

            ajax.RspData.Add("role", JToken.FromObject(role));

            return ajax;
        }

        /// <summary>
        /// 添加或修改角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public AjaxFxRspJson ModifyRole(Role role)
        {
            bool rtn = true;

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (!string.IsNullOrEmpty(role.RoleCode))
            {
                rtn = svc.InsertRole(role);
                if (!rtn)
                {
                    ajax.RspMsg = "添加角色失败！";
                }
            }
            else
            {
                rtn = svc.UpdateRole(role);
                if (!rtn)
                {
                    ajax.RspMsg = "修改角色失败！";
                }
            }
            if (!rtn)
            {
                ajax.RspCode = 0;
                return ajax;
            }

            return ajax;
        }

        /// <summary>
        /// 获取角色步骤列表
        /// </summary>
        /// <returns></returns>
        public AjaxFxRspJson GetRoleStepList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<RoleStep> steps = svc.GetStepList();

            ajax.RspData.Add("steps", JToken.FromObject(steps));

            return ajax;
        }

        #region [ 获取角色列表 ]

        public AjaxFxRspJson GetRoleList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Role> list = svc.GetRoles();

            ajax.RspData.Add("roles", JToken.FromObject(list));

            return ajax;
        }


        public AjaxFxRspJson GetRolePermissionTree(string roleId)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<SystemModel> models = svc.GetSystemModels();

            List<SystemModel> rolemodels = svc.GetSystemModelsByRoleId(roleId);

            List<TreeModel> tree = new List<TreeModel>();

            tree.Add(new TreeModel()
            {
                id = "0",
                pId = "0",
                name = "系统权限",
                open = true
            });

            foreach (var item in models)
            {
                TreeModel roleTree = new TreeModel();
                roleTree.id = item.NodeCode;
                roleTree.pId = item.ParentCode;
                roleTree.name = item.NodeName;
                foreach (var role in rolemodels)
                {
                    if (item.NodeCode == role.NodeCode)
                    {
                        roleTree.@checked = true;
                    }
                }
                int count = tree.Where(m => m.id == item.ParentCode).Count();
                if (count > 0)
                {
                    tree.Add(roleTree);
                }
            }

            ajax.RspData.Add("roles", JToken.FromObject(tree));

            return ajax;
        }

        #endregion

        #endregion

        #region [ 获取用户角色树 ]

        public AjaxFxRspJson GetRoleTree()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<TreeModel> trees = new List<TreeModel>();

            trees.Add(new TreeModel() { id = "0", name = "角色选择", open = true });

            List<Role> roles = svc.GetRoleTree(this.UserInfo.getAreacode(), this.UserInfo.getUsercode());

            List<Role> userroles = svc.GetRoleList(UserInfo.getUsercode());

            foreach (Role role in roles)
            {
                TreeModel tree = new TreeModel()
                {
                    id = role.RoleCode.ToString(),
                    name = role.RoleName,
                    pId = string.IsNullOrEmpty(role.ParentCode) ? "0" : role.ParentCode,
                    tag = role.Memo
                };

                foreach (var item in userroles)
                {
                    if (item.RoleCode == role.RoleCode.Replace("aaaa", ""))
                    {
                        tree.@checked = true;
                        break;
                    }
                    else
                    {
                        tree.@checked = false;
                    }
                }
                trees.Add(tree);
            }

            ajax.RspData.Add("roles", JToken.FromObject(trees));

            return ajax;
        }

        #endregion

    }
}
