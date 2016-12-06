using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.IBussiness;
using Szcg.Service.Bussiness;
using szcg.com.teamax.business.entity;
using System.Web.Security;
using Newtonsoft.Json;
using Szcg.Service.Common;
using Szcg.Service.Model;
using Newtonsoft.Json.Linq;

namespace Szcg.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            //throw new Exception("testq111");
            return View();
        }

        public ActionResult Main()
        {
            IPermissionService svc = new PermissionService();

            List<FlowNodePower> list = svc.GetFlowNodePower(UserInfo.CurrentRole.ToString(), string.Empty, UserInfo.CurrentSystemId);

            foreach (var power in list)
            {
                if (power.ButtonCode.Length == 11 && power.ButtonCode.Substring(9) == "01")
                {
                    if (power.ModelCode.StartsWith("27") || power.ModelCode.StartsWith("28"))
                        power.ModelCode = "11" + power.ModelCode.Substring(2);
                }

                if (power.ButtonCode.Length == 11 && power.ButtonCode.Substring(0, 8) == power.ButtonCode.Substring(0, 8) && power.ButtonCode.Substring(9) != "01")
                {
                    string strButtoncode = power.ButtonCode;
                    if (strButtoncode.StartsWith("27") || strButtoncode.StartsWith("28"))
                        power.ButtonCode = "11" + strButtoncode.Substring(2);
                }
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            //ChageRole("11");
            return View();
        }

        /// <summary>
        /// 检查权限，跳转至相应的系统
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        public AjaxFxRspJson SelectSystem(string systemId)
        {
            string rtnurl = "/main.html";

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 0 };

            if (string.IsNullOrEmpty(systemId))
            {
                ajax.RspMsg = "请输入系统Id";
                return ajax;
            }

            if (UserInfo != null)
            {
                string[] strSysIds = UserInfo.getSystemid();
                if (strSysIds.Contains(systemId))
                {
                    ChageRole(systemId);

                    if (systemId == "10")
                    {
                        rtnurl = "/manager/main.html";
                    }
                    if (systemId == "21")
                    {
                        rtnurl = "/Appraise/main.html";
                    }

                    ajax.RspData.Add("url", JToken.FromObject(rtnurl));
                    ajax.RspCode = 1;
                    return ajax;
                }
                else
                {
                    ajax.RspMsg = "没有权限！";
                    return ajax;
                }
            }
            else
            {
                ajax.RspMsg = "获取用户信息失败！";
                return ajax;
            }
        }

        /// <summary>
        /// 切换用户角色
        /// </summary>
        /// <param name="systemId">系统Id</param>
        public void ChageRole(string systemId)
        {
            string strRoleId = string.Empty;
            string strAreacode = string.Empty;

            string[] strRoles = UserInfo.getRole();

            UserInfo.CurrentSystemId = systemId;

            new PermissionService().GetRoleStepBySysCode(systemId, out strRoleId, out strAreacode, string.Join(",", strRoles));//多角色

            UserInfo.CurrentRole = int.Parse(strRoleId);

            UserInfo.ModelPowers = new PermissionService().GetUserModelPower(systemId, UserInfo.getUsercode());

            ResetUserCookie(systemId, UserInfo.ModelPowers);
        }

        #region ResetUserCookie：重新设置Forms 身份验证票证

        /// <summary>
        /// 重新设置Forms 身份验证票证
        /// </summary>
        /// <param name="SystemID"></param>
        private void ResetUserCookie(string SystemID, string strModels)
        {
            try
            {
                string strModelPowers = string.Empty;
                UserInfo userInfo = this.UserInfo;
                userInfo.CurrentSystemId = SystemID;

                string[] strsystemid = strModels.Split(',');

                for (int i = 0; i < strsystemid.Length; i++)
                {
                    if (strsystemid[i].Length > 2 && (strsystemid[i].StartsWith("28") || strsystemid[i].StartsWith("27")))
                    {

                        strsystemid[i] = "11" + strsystemid[i].Substring(2);
                    }

                    strModelPowers = strModelPowers + strsystemid[i] + ",";
                }

                if (strModelPowers != "")
                {
                    userInfo.ModelPowers = strModelPowers.Substring(0, strModelPowers.Length - 1).Trim();
                }

                // string userdata = JsonConvert.SerializeObject(_username = "测试员");


                string userdata = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}${8}${9}${10}${11}${12}${13}${14}", userInfo.getAreacode(), userInfo.CurrentNodeID, userInfo.CurrentSystemId, userInfo.getDepartcode(), userInfo.getDepartDefinedcode(), userInfo.getDepartname(), userInfo.getHcpower(), userInfo.CurrentRole, userInfo.getIs_ca(), userInfo.getLoginname(), userInfo.ModelPowers, string.Join(",", userInfo.getRole()), string.Join(",", userInfo.getSystemid()), userInfo.getUsercode(), userInfo.getUsername());

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, this.UserInfo._username, DateTime.Now, DateTime.Now.AddHours(2), false, userdata);
                HttpCookie cookie_ = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                HttpContext.Response.Cookies.Add(cookie_);
                Session["UserInfo"] = userInfo;
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.logger.ErrorFormat("登录信息重新写入cookie失败,错误信息：{0}", ex.ToString());
            }

        }

        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
