using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;
using Teamax.Common;

namespace Szcg.Web.Controllers
{
    public class DepartController : BaseController
    {
        IOrganizeService svc = new OrganizeService();
        //
        // GET: /Depart/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据当前用户获取部门树列表
        /// </summary>
        /// <returns></returns>
        public AjaxFxRspJson GetDepartList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Depart> departs = svc.GetDepartList(UserInfo.getAreacode(), UserInfo.getDepartcode().ToString(), UserInfo.getUsercode().ToString());

            ajax.RspData.Add("departs", JToken.FromObject(departs));

            return ajax;
        }

        [HttpPost]
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public AjaxFxRspJson InsertDepart(Depart depart)
        {

            AjaxFxRspJson ajax = CheckDepart(depart);

            if (ajax.RspCode == 0)
            {
                return ajax;
            }

            if (!svc.InsertDepart(depart))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "添加部门失败!";
                return ajax;
            }

            return ajax;
        }

        [HttpPost]

        public AjaxFxRspJson UpdateDepart(Depart depart)
        {
            AjaxFxRspJson ajax = CheckDepart(depart);

            if (ajax.RspCode == 0)
            {
                return ajax;
            }

            if (!svc.UpdateDepart(depart))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "修改部门失败!";
                return ajax;
            }

            return ajax;
        }

        /// <summary>
        /// 检查部门信息合法性
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public AjaxFxRspJson CheckDepart(Depart depart)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(depart.DepartName))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "名称不能为空!";
                return ajax;
            }
            if (depart.DepartName.Length > 64)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "名称长度不能超过64个字符!";
                return ajax;
            }

            if (!CheckChar(depart.DepartName, "名称"))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "名称中不能含有 , ; @ 等特殊字符!";
                return ajax;
            }

            if (depart.DepartAddress.Length > 512)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "地址长度不能超过512个字符!";
                return ajax;
            }

            if (depart.Memo.Length > 127)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "备注长度不能超过127个字符!";
                return ajax;
            }
            if (!string.IsNullOrEmpty(depart.Mobile) && PublicClass.IsValidMobil(depart.Mobile))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入正确的手机号码!";
                return ajax;
            }

            if (depart.IsSJ == 1)
            {
                if (string.IsNullOrEmpty(depart.ParentDepartName))
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "该部门的【是否市局部门】属性为真，那么必须设置所对应的【市局职能部门】属性！";
                    return ajax;
                }
            }

            if (!svc.CheckDepartName(depart.ParentCode, depart.DepartName))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "部门名称存在重复!";
                return ajax;
            }

            return ajax;
        }

        /// <summary>
        /// 检查部门名称字符合法性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CheckChar(string name, string text)
        {
            if (name.IndexOf(",") >= 0 || name.IndexOf(";") >= 0 || name.IndexOf("@") >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
