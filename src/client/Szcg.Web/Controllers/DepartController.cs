using bacgDL.business;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using szcg.com.teamax.business.entity;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;
using Szcg.Web.Models;
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

        #region [ 获取部门列表 ]

        /// <summary>
        /// 根据当前用户获取部门树列表
        /// </summary>
        /// <returns></returns>
        public AjaxFxRspJson GetDepartList()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<TreeModel> tree = new List<TreeModel>();

            List<Depart> departs = svc.GetDepartList(UserInfo.getAreacode(), UserInfo.getDepartcode().ToString(), UserInfo.getUsercode().ToString());

            foreach (var item in departs)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.UserDefinedCode,
                    pId = item.ParentCode,
                    name = item.DepartName
                };
                tree.Add(depart);
            }

            ajax.RspData.Add("departs", JToken.FromObject(tree));

            return ajax;
        }

        #region [ 获取表格部门列表 ]

        public AjaxFxRspJson GetDepartListView()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Depart> departs = svc.GetDepartList(UserInfo.getAreacode(), UserInfo.getDepartcode().ToString(), UserInfo.getUsercode().ToString());

            ajax.RspData.Add("departs", JToken.FromObject(departs));

            return ajax;
        }

        #endregion

        #region [ 根据区域获取部门列表 ]

        public AjaxFxRspJson GetDepartListByAreaCode()
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            List<Depart> list = svc.GetDepartList(UserInfo.getAreacode());

            ajax.RspData.Add("departs", JToken.FromObject(list));

            return ajax;
        }

        #endregion

        #region [ 根据区域获取部门树 ]

        public AjaxFxRspJson GetDutyDepartTree(string typecode = "0")
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            List<TreeModel> tree = new List<TreeModel>();
            List<Depart> departs = svc.GetDutyDepart(UserInfo.getAreacode(), typecode);
            foreach (var item in departs)
            {
                TreeModel depart = new TreeModel()
                {
                    id = item.UserDefinedCode,
                    pId = item.ParentCode,
                    name = item.DepartName,
                    phone = string.IsNullOrEmpty(item.UserMobile) ? string.Empty : item.UserMobile.Split('$')[1],
                    tag = item.DepartCode
                };
                tree.Add(depart);
            }
            ajax.RspData.Add("departs", JToken.FromObject(tree));

            return ajax;
        }

        #endregion

        #endregion

        #region [ 获取部门信息 ]

        public AjaxFxRspJson GetDepartInfo(string departId)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            Depart depart = svc.GetDepartInfo(departId);

            ajax.RspData.Add("depart", JToken.FromObject(depart));

            return ajax;
        }

        #endregion

        #region [ 添加部门 ]

        [HttpPost]
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public AjaxFxRspJson InsertDepart(Depart depart)
        {
            // bool rtn = false;
            ReturnValue rtn = new ReturnValue();

            AjaxFxRspJson ajax = CheckDepart(depart);

            if (ajax.RspCode == 0)
            {
                return ajax;
            }
            if (string.IsNullOrEmpty(depart.DepartCode) || depart.DepartCode == "0")
            {
                if (!string.IsNullOrEmpty(depart.ParentCode) && svc.CheckDepartName(depart.ParentCode, depart.DepartName))
                {
                    ajax.RspCode = 0;
                    ajax.RspMsg = "部门名称存在重复!";
                    return ajax;
                }
                rtn = svc.InsertDepart(depart);
            }
            else
            {
                rtn = svc.UpdateDepart(depart);
            }

            if (!rtn.ReturnState)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = rtn.ErrorMsg;
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 修改部门 ]

        [HttpPost]
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="depart">部门实体</param>
        /// <returns></returns>
        public AjaxFxRspJson UpdateDepart(Depart depart)
        {
            AjaxFxRspJson ajax = CheckDepart(depart);

            if (ajax.RspCode == 0)
            {
                return ajax;
            }

            ReturnValue rtn = svc.UpdateDepart(depart);

            if (!rtn.ReturnState)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = rtn.ErrorMsg;
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 删除部门 ]

        public AjaxFxRspJson DeleteDepart(string departId)
        {

            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(departId))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入部门id";
                return ajax;
            }

            //0：删除发生异常1：标识删除成功2：部门存在人员，删除失败3：部门存在子部门，删除失败
            int result = svc.DeleteDepart(departId);

            if (result == 0)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "删除部门失败";
                return ajax;
            }
            if (result == 2)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "部门存在人员,删除失败";
                return ajax;
            }
            if (result == 3)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "部门存在子部门，删除失败";
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 根据部门获取用户列表 ]

        public JsonResult GetUserByDept(string departId, string userName, string loginName, string departName)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (UserInfo == null)
            {
                ajax.RspMsg = "用户未登录！";
                ajax.RspCode = 0;
                return Json(ajax);
            }

            int currentpage = int.Parse(Request["start"]);
            int pagesize = int.Parse(Request["length"]);

            if (currentpage != 0)
            {
                currentpage = (currentpage / pagesize) + 1;
            }
            else
            {
                currentpage = 1;
            }
            PageInfo pageInfo = new PageInfo();
            pageInfo.PageSize = Request["length"];
            pageInfo.CurrentPage = currentpage.ToString();
            pageInfo.Field = "username";
            pageInfo.Order = "asc";
            pageInfo.ReturnRecordCount = "1";//是否返回记录数（1：是 0：否）

            if (string.IsNullOrEmpty(departId))
            {
                departId = "0";
            }

            List<UserInfo> list = svc.GetUserByDeptID(int.Parse(departId), pageInfo, UserInfo.getUsercode(), userName, loginName, departName, pageInfo.Order, pageInfo.Field);

            List<UserData> users = new List<UserData>();

            foreach (UserInfo item in list)
            {
                UserData data = new UserData()
                {
                    userCode = item.getUsercode().ToString(),
                    userName = item.getUsername(),
                    loginName = item.getLoginname(),
                    sex = item.getSex(),
                    tel = item.getTel(),
                    mobile = item.getMobile(),
                    departName = item.getDepartname()
                };
                users.Add(data);
            }

            return Json(new { draw = Request["draw"], recordsTotal = pageInfo.RowCount, recordsFiltered = pageInfo.RowCount, data = users == null ? new List<UserData>() : users }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [ 获取用户详细信息 ]

        public AjaxFxRspJson GetUserById(string userCode)
        {
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };

            if (string.IsNullOrEmpty(userCode))
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "请输入用户编码";
                return ajax;
            }

            UserData user = svc.GetUserById(int.Parse(userCode));

            ajax.RspData.Add("user", JToken.FromObject(user == null ? new UserData() : user));

            return ajax;
        }

        #endregion

        #region [ 添加用户 ]

        public AjaxFxRspJson InsertUser(UserData user)
        {
            bool rtn = false;
            AjaxFxRspJson ajax = new AjaxFxRspJson() { RspCode = 1 };
            if (!string.IsNullOrEmpty(user.userCode))
            {

                UserData olduser = svc.GetUserById(int.Parse(user.userCode));

                string oldroleid = olduser.roleids;

                UpdateModel<UserData>(olduser);

                rtn = svc.UpdateUser(int.Parse(user.userCode), olduser, oldroleid);
            }
            else
            {
                rtn = svc.InsertUser(user);
            }
            if (!rtn)
            {
                ajax.RspMsg = "添加失败！";
                ajax.RspCode = 0;
                return ajax;
            }

            return ajax;
        }

        #endregion

        #region [ 验证部门信息输入 ]

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

            if (depart.DepartAddress != null && depart.DepartAddress.Length > 512)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "地址长度不能超过512个字符!";
                return ajax;
            }

            if (!string.IsNullOrEmpty(depart.Memo) && depart.Memo.Length > 127)
            {
                ajax.RspCode = 0;
                ajax.RspMsg = "备注长度不能超过127个字符!";
                return ajax;
            }

            if (!string.IsNullOrEmpty(depart.Mobile) && !PublicClass.IsValidMobil(depart.Mobile))
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
            return ajax;
        }

        #endregion

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
