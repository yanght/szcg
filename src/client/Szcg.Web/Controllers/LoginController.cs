﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Model;
using Szcg.Service.IBussiness;
using Szcg.Service.Bussiness;
using log4net;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Web.Security;
using Szcg.Service.Common;
using Newtonsoft.Json;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Szcg.Web.Controllers
{
    public class LoginController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Login/

        public ActionResult Index()
        {
            log.InfoFormat("logger....");
            return View();
        }

        #region [ 用户登录 ]

        //[HttpPost]
        public AjaxFxRspJson Login(string userName, string passWord)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                ajax.RspMsg = "用户名及密码不能为空！";
                ajax.RspCode = 0;
                return ajax;
            }

            IPermissionService permission = new PermissionService();

            var userInfo = permission.LoginValidate(userName, passWord);

            if (userInfo == null)
            {
                ajax.RspMsg = "登录失败,请输入正确的用户名或密码！";
                ajax.RspCode = 0;
                return ajax;
            }

           // string userdata = JsonConvert.SerializeObject(userInfo);

            string userdata = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}${8}${9}${10}${11}${12}${13}${14}", userInfo.getAreacode(), userInfo.CurrentNodeID, userInfo.CurrentSystemId, userInfo.getDepartcode(), userInfo.getDepartDefinedcode(), userInfo.getDepartname(), userInfo.getHcpower(), userInfo.CurrentRole, userInfo.getIs_ca(), userInfo.getLoginname(), userInfo.ModelPowers, string.Join(",", userInfo.getRole()), string.Join(",", userInfo.getSystemid()), userInfo.getUsercode(), userInfo.getUsername());


            try
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(2), false, userdata);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                HttpContext.Response.Cookies.Add(cookie);
                Session["UserInfo"] = userInfo;

            }
            catch (Exception ex)
            {
                LoggerManager.Instance.logger.ErrorFormat("登录信息写入cookie失败,错误信息：{0}", ex.ToString());
            }

            ajax.RspData.Add("userInfo", JToken.FromObject(userInfo));
            return ajax;
        }

        public ActionResult LoginOut()
        {
            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(cookie);
            Session["UserInfo"] = null;
            return RedirectToAction("Index");
        }

        #endregion

    }
}
