using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using szcg.com.teamax.business.entity;
using Szcg.Service.Bussiness;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            IProjectService svc = new ProjectService();

            //svc.GetDealProjectList();
            //getprojectlist();
            //svc.GetProjectTrace("231526", 2016, false);
            //svc.GetProjectDetail("231526", 2016, false);
            //Console.WriteLine(addproject());
            //bigclassList(0);
            //bigclassList(1);
            //smallclassList(0, "01");

            //getCollecters("331125","","");


            //new OrganizeService().GetDepartList("331125", "183", "2");

            // List<Role> list = new PermissionService().GetRoles();
            List<FlowNodePower> flowers = new PermissionService().GetFlowNodePower("23", string.Empty, "11");

            foreach (var item in flowers)
            {
                Console.Write(item.ShowName + ",");
            }


            Console.ReadLine();
        }

        public static string addproject()
        {
            IProjectService svc = new ProjectService();

            UserInfo userinfo = new UserInfo();
            userinfo.setDepartcode(934);
            userinfo.setUsername("yannis");
            userinfo.setUsercode(2);

            Project project = new Project()
            {
                ReportTel = "18806521795",
                Address = "宁虹路1101弄",
                AreaId = "331125",
                StreetId = "331125001",
                SquareId = "331125001001",
                BigClass = "01",
                Telephonist = userinfo.getUsername(),
                TelephonistCode = userinfo.getUsercode().ToString(),
                NodeId = 2,
                ProbSource = "11",
                TypeCode = "1",
                SmallClass = "0101",
                IsGreat = "0",
                ProbDesc = "违章搭建",
                IsNeedFeedBack = false,
                TargetDepartCode = userinfo.getDepartcode(),
                ReportName = "yannis",

            };

            return svc.AddProject(project, userinfo);
        }

        public static List<Project> getprojectlist()
        {
            bacgDL.business.ProjectInfo prj = new bacgDL.business.ProjectInfo()
            {
                NodeId = "2",
                areacode = "331125",
                TargetDepartCode = "",
                projcode = "",
                street = "",
                square = "",
                PdaIoFlag = "",
                bigClass = "",
                smallclass = "",
                address = "",
                partID = "",
                typecode = "",
                strButtonId = "",
                strUserCode = "",

            };


            bacgDL.business.PageInfo pgInfo = new bacgDL.business.PageInfo();
            pgInfo.CurrentPage = "1";
            pgInfo.PageSize = "10";
            pgInfo.Order = "asc";
            pgInfo.Field = "projcode";

            IProjectService svc = new ProjectService();
            ReturnValue rtn = svc.GetDealProjectList(prj, pgInfo, "2016-1-1", "2016-12-30");
            if (rtn.ReturnState)
            {
                return (List<Project>)rtn.ReturnObj;
            }
            return null;

        }

        public static List<ProjectBigClass> bigclassList(string type)
        {
            IProjectService svc = new ProjectService();
            return svc.GetBigClassList(type);
        }

        public static List<ProjectSmallClass> smallclassList(string type, string bigclassCode)
        {
            IProjectService svc = new ProjectService();
            return svc.GetSmallClassList(type, bigclassCode);
        }

        public static List<Collecter> getCollecters(string areacode, string streetcode, string commcode)
        {
            return new Collecterservice().GetCollecters(areacode, streetcode, commcode);
        }

    }
}
