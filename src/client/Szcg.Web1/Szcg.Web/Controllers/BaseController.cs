using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                if (Session["UserInfo"] == null)
                {
                    szcg.com.teamax.business.entity.UserInfo userinfo = new PermissionService().LoginValidate("zjw", "123");
                    Session["UserInfo"] = userinfo;
                    return userinfo;
                }
                else
                {
                    return (UserInfo)Session["UserInfo"];
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
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
