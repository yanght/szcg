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

namespace Szcg.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            //throw new Exception("testq111");
            UserInfo user = UserInfo;
          
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ChageRole("21");
            return View();
        }

        public void ChageRole(string systemId)
        {
            string strRoleId = string.Empty;
            string strAreacode = string.Empty;

            string[] strRoles = UserInfo.getRole();

            UserInfo.CurrentSystemId = systemId;

            new PermissionService().GetRoleStepBySysCode(systemId, out strRoleId, out strAreacode, string.Join(",", strRoles));//多角色

            UserInfo.CurrentRole = int.Parse(strRoleId);

            UserInfo.ModelPowers = new PermissionService().GetUserModelPower(systemId, UserInfo.getUsercode());

            string strModels = new PermissionService().GetUserModelPower(systemId, UserInfo.getUsercode());

            ResetUserCookie(systemId, strModels);
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
                    userInfo._ModelPowers = strModelPowers.Substring(0, strModelPowers.Length - 1).Trim();
                    userInfo.ModelPowers = strModelPowers.Substring(0, strModelPowers.Length - 1).Trim();
                }

                string userdata = JsonConvert.SerializeObject(userInfo);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, this.UserInfo._username, DateTime.Now, DateTime.Now.AddHours(2), false, userdata);
                HttpCookie cookie_ = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                HttpContext.Response.Cookies.Add(cookie_);
                Session["UserInfo"] = this.UserInfo;
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

    }
}
