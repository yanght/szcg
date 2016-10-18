using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using szcg.com.teamax.business.entity;
using Szcg.Service.Bussiness;

namespace Szcg.Web.Controllers
{
    public class BaseController : Controller
    {
        public szcg.com.teamax.business.entity.UserInfo UserInfo
        {
            get
            {
                UserInfo user = new UserInfo();

                if (Session["UserInfo"] == null)
                {
                    HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie == null)
                        return null;

                    FormsAuthenticationTicket _ticket = null;
                    try
                    {
                        _ticket = FormsAuthentication.Decrypt(cookie.Value);
                        if (_ticket == null)
                            return null;
                    }
                    catch
                    {
                        _ticket = null;
                        return null;
                    }

                    string userdata = _ticket.UserData;

                    if (!string.IsNullOrEmpty(userdata))
                    {
                        user = JsonConvert.DeserializeObject<UserInfo>(userdata);
                        Session["UserInfo"] = user;
                    }

                    return user;
                }
                else
                {
                    return (UserInfo)Session["UserInfo"];
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserInfo == null)
            {
                filterContext.Result = Redirect("/login");
            }
            var methodInfo = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo;
            foreach (var p in methodInfo.GetParameters())
            {
                if (p.ParameterType.IsValueType)
                {
                    filterContext.ActionParameters[p.Name] = Activator.CreateInstance(p.ParameterType);
                }
            }
        }

    }
}
