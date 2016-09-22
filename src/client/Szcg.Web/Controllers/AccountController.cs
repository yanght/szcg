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
    public class AccountController : Controller
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

            if (role.RoleCode <= 0)
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

        #endregion

    }
}
