using Newtonsoft.Json;
using System;
using System.Collections;
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
                        string[] arrs = userdata.Split('$');

                        user.setAreacode(arrs[0]);
                        user.CurrentNodeID = arrs[1];
                        user.CurrentSystemId = arrs[2];
                        user.setDepartcode(int.Parse(arrs[3]));
                        user.setDepartDefinedcode(arrs[4]);
                        user.setDepartname(arrs[5]);
                        user.setHcpower(arrs[6]);
                        user.CurrentRole = int.Parse(arrs[7]);
                        user.setIs_ca(bool.Parse(arrs[8]));
                        user.setLoginname(arrs[9]);
                        user.ModelPowers = arrs[10];
                        user.setRole(ArrayList.Adapter(arrs[11].Split(',')));
                        user.setSystemid(ArrayList.Adapter(arrs[12].Split(',')));
                        user.setUsercode(int.Parse(arrs[13]));
                        user.setUsername(arrs[14]);

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
