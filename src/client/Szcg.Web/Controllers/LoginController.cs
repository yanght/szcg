using System;
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

        [HttpPost]
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
            ajax.RspData.Add("userInfo", JToken.FromObject(userInfo));
            return ajax;
        }

    }
}
