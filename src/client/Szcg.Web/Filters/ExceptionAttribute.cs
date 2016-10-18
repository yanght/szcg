using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Common;

namespace Szcg.Web.Filters
{
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 自定义异常处理
        /// </summary>
        public ExceptionAttribute()
        { }

        public virtual void OnException(ExceptionContext filterContext)
        {
            string message = string.Format(" 消息类型：{0} \n 消息内容：{1}  \n 引发异常的方法：{2}  \n 引发异常源：{3}"
                , filterContext.Exception.GetType().Name
                , filterContext.Exception.Message
                , filterContext.Exception.TargetSite
                , filterContext.Exception.Source + filterContext.Exception.StackTrace
                );

            //记录日志
            LoggerManager.Instance.logger.ErrorFormat(message);

            //抛出异常信息
            filterContext.Controller.TempData["ExceptionAttributeMessages"] = message;

            //转向
            filterContext.ExceptionHandled = true;
           // filterContext.Result = new RedirectResult("/Home/Error");
        }
    }
}