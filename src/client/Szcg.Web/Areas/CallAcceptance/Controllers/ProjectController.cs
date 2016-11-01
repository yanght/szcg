using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web.Areas.CallAcceptance.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /CallAcceptance/Project/

        public ActionResult Index()
        {
            return View();
        }

        //待办案卷列表
        public ActionResult Reportlist()
        {
            return View();
        }

        //自办件
        public ActionResult ProjectSelfList()
        {
            return View();
        }

        //存档案卷
        public ActionResult ProjectCDList()
        {
            return View();
        }

        //查询箱
        public ActionResult ProjectQuery()
        {
            return View();
        }

        //案卷详情
        public ActionResult Preview(string projectcode, string year, string isend, string nodeid)
        {
            return View();
        }

        //案卷流程
        public ActionResult ProjectTrace(string projectcode, string year, string isend)
        {
            return View();
        }

        //案卷上报
        public ActionResult ProjectReport(string projectcode, string year, string isend, string nodeid)
        {
            return View();
        }

        //值班长立案
        public ActionResult ProjectLA()
        {
            return View();
        }

        //任务派遣
        public ActionResult ProjectDispatch()
        {
            return View();
        }

        //任务派遣回退
        public ActionResult ProjectDispatchRevert()
        {
            return View();
        }

        //发送核查信息
        public ActionResult ProjectCheckMessage()
        {
            return View();
        }

        //核查
        public ActionResult ProjectCheck()
        {
            return View();
        }

        //结案
        public ActionResult ProjectEnd()
        {
            return View();
        }

        //选择部门树
        public ActionResult SelectDepart()
        {
            return View();
        }

        //选择大小类树
        public ActionResult SelectProjectClass()
        {
            return View();
        }

        //选择区域树
        public ActionResult SelectArea()
        {
            return View();
        }

        //案卷批转详情
        public ActionResult ProjectApprovedView(string projectcode, string year, string isend, string nodeid, string action, string buttoncode)
        {
            return View();
        }

        //案卷详情消息发送
        public ActionResult ProjectSendMessage(string collcode, string collname)
        {
            return View();
        }
    }
}
