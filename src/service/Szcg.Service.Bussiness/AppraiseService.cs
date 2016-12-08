using bacgBL.system;
using bacgBL.zhpj.area;
using bacgDL.business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;
using Szcg.Service.Model.RequestModel;


namespace Szcg.Service.Bussiness
{

    public class AppraiseService : IAppraiseService
    {
        string strReportMessage = string.Empty;

        /// <summary>
        /// 获取事部件评价
        /// </summary>
        /// <param name="args">事部件评价查询参数</param>
        /// <returns></returns>
        public List<EvePar_Appraise> GetEvePartAppraise(EveParAppraiseRequestArgs args)
        {
            AreaAppraise areaquery = new AreaAppraise();

            bacgDL.zhpj.areaappraise.StructQuery sq = new bacgDL.zhpj.areaappraise.StructQuery();
            sq.intYears = args.Year;
            if (args.Type == 0) //周
                sq.intWeeks = args.Number;
            if (args.Type == 1) //月
                sq.intMonths = args.Number; ;
            if (args.Type == 2) //季
                sq.intQuarter = args.Number; ;
            if (args.Type == 3) //年
                sq.intYears = args.Number; ;

            areaquery.SetStatDate(sq);

            List<EvePar_Appraise> list = new List<EvePar_Appraise>();

            DataTable dt = areaquery.GetEvePartCount(sq.startDate.ToString("yyyy-MM-dd"), sq.endDate.ToString("yyyy-MM-dd"), out strReportMessage, args.StreetCode);

            args.strReportMessage = strReportMessage;

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<EvePar_Appraise>.GetModelList(dt);
            }
            return list;
        }

        /// <summary>
        /// 获取区域评价
        /// </summary>
        /// <param name="args">区域评价查询参数</param>
        /// <returns></returns>
        public List<Area_Appraise> GetAreaAppraise(AreaAppraiseRequestArgs args, PageInfo pageInfo)
        {
            List<Area_Appraise> list = new List<Area_Appraise>();

            AreaAppraise areaquery = new AreaAppraise();

            bacgDL.zhpj.areaappraise.StructQuery sq = new bacgDL.zhpj.areaappraise.StructQuery();
            sq.intYears = args.Year; ;
            if (args.Type == 0) //周
                sq.intWeeks = args.Number;
            if (args.Type == 1) //月
                sq.intMonths = args.Number; ;
            if (args.Type == 2) //季
                sq.intQuarter = args.Number; ;
            if (args.Type == 3) //年
                sq.intYears = args.Year; ;

            areaquery.SetStatDate(sq);

            int rowCount = 0;
            int pageCount = 0;
            string field = "code";
            string order = "asc";
            string cols = string.Empty;
            string strReportMessage = string.Empty;

            DataTable dt = areaquery.getAreaAppraise(args.ModelId, args.AreaCode, args.RoleId, field, order, sq.startDate.ToString("yyyy-MM-dd"), sq.endDate.ToString("yyyy-MM-dd"), ref rowCount, ref pageCount, out cols, out strReportMessage);

            pageInfo.RowCount = rowCount.ToString();
            pageInfo.PageCount = pageCount.ToString();
            args.strReportMessage = strReportMessage;

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<Area_Appraise>.GetModelList(dt);
            }
            return list;
        }

        /// <summary>
        /// 责任单位评价
        /// </summary>
        /// <param name="args">责任单位评价查询参数</param>
        /// <returns></returns>
        public List<Depart_Appraise> GetDepartAppraise(DepartAppraiseRequestArgs args, PageInfo pageInfo)
        {
            List<Depart_Appraise> list = new List<Depart_Appraise>();

            AreaAppraise areaquery = new AreaAppraise();
            bacgDL.zhpj.areaappraise.StructQuery sq = new bacgDL.zhpj.areaappraise.StructQuery();
            sq.intYears = args.Year; ;
            if (args.Type == 0) //周
                sq.intWeeks = args.Number;
            if (args.Type == 1) //月
                sq.intMonths = args.Number; ;
            if (args.Type == 2) //季
                sq.intQuarter = args.Number; ;
            if (args.Type == 3) //年
                sq.intYears = args.Number; ;

            areaquery.SetStatDate(sq);

            string field = "code";
            string order = "asc";
            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = string.Empty;

            DataTable dt = areaquery.getAreaAppraise(args.ModelId, args.DepartCode, args.AreaCode, args.RoleId, field, order, sq.startDate.ToString("yyyy-MM-dd"), sq.endDate.ToString("yyyy-MM-dd"), ref rowCount, ref pageCount, out cols, out strReportMessage);

            DataTable dep_dt = areaquery.getDepart();
            DataColumn dc1 = new DataColumn("排序号", System.Type.GetType("System.Int32"));
            dt.Columns.Add(dc1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool isSort = false;
                for (int j = 0; j < dep_dt.Rows.Count; j++)
                {

                    string Fcode = dt.Rows[i]["code"].ToString();
                    string Pcode = dt.Rows[i]["pcode"].ToString();
                    string Scode = dep_dt.Rows[j]["UserDefinedCode"].ToString();
                    //pcode
                    if (Pcode == "" && Fcode.Length > 4)
                    {
                        dt.Rows[i]["pcode"] = Fcode.Substring(0, Fcode.Length - 3);
                    }
                    //序号
                    if (Fcode == Scode)
                    {
                        dt.Rows[i]["排序号"] = dep_dt.Rows[j]["排序号"];
                        isSort = true;
                        break;
                    }
                }
                if (isSort == false)
                    dt.Rows[i]["排序号"] = 999;
            }


            pageInfo.RowCount = rowCount.ToString();
            pageInfo.PageCount = pageCount.ToString();
            args.strReportMessage = strReportMessage;

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<Depart_Appraise>.GetModelList(dt);
            }

            return list;
        }

        /// <summary>
        /// 岗位评价
        /// </summary>
        /// <param name="args">岗位评价查询参数</param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<Duty_Appraise> GetDutyAppraise(DutyAppraiseRequestArgs args, PageInfo pageInfo)
        {
            List<Duty_Appraise> list = new List<Duty_Appraise>();

            AreaAppraise areaquery = new AreaAppraise();

            bacgDL.zhpj.areaappraise.StructQuery sq = new bacgDL.zhpj.areaappraise.StructQuery();
            sq.intYears =args.Year;
            if (args.Type == 0) //周
                sq.intWeeks = args.Number;
            if (args.Type == 1) //月
                sq.intMonths = args.Number; 
            if (args.Type == 2) //季
                sq.intQuarter = args.Number; 
            if (args.Type == 3) //年
                sq.intYears = args.Number; 

            areaquery.SetStatDate(sq);

            int rowCount = 0;
            int pageCount = 0;
            string field = "code";
            string order = "asc";
            string cols = string.Empty;
            string strReportMessage = string.Empty;

            DataTable dt = areaquery.getAreaAppraise(args.ModelId, args.DepartCode, args.Code, args.Name, args.RoleId, field, order, sq.startDate.ToString("yyyy-MM-dd"), sq.endDate.ToString("yyyy-MM-dd"), ref rowCount, ref pageCount, out cols, out strReportMessage);

            pageInfo.RowCount = rowCount.ToString();
            pageInfo.PageCount = pageCount.ToString();
            args.strReportMessage = strReportMessage;

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<Duty_Appraise>.GetModelList(dt);
            }

            return list;
        }

        /// <summary>
        /// 监督员评价
        /// </summary>
        /// <param name="args">监督员评价查询参数</param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<Collecter_Apprise> GetCollecterAppraise(CollecterAppraiseRequestArgs args, PageInfo pageInfo)
        {
            List<Collecter_Apprise> list = new List<Collecter_Apprise>();
            bacgBL.zhpj.Appraise_xcy xcy = new bacgBL.zhpj.Appraise_xcy();
            AreaAppraise areaquery = new AreaAppraise();

            bacgDL.zhpj.areaappraise.StructQuery sq = new bacgDL.zhpj.areaappraise.StructQuery();

            if (args.Type == 0) //周
                sq.intWeeks = args.Number;
            if (args.Type == 1) //月
                sq.intMonths = args.Number; ;
            if (args.Type == 2) //季
                sq.intQuarter = args.Number; ;
            if (args.Type == 3) //年
                sq.intYears = args.Number; ;

            areaquery.SetStatDate(sq);

            int rowCount = 0;
            int pageCount = 0;
            string field = "code";
            string order = "asc";
            string cols = string.Empty;
            string strReportMessage = string.Empty;

            DataTable dt = xcy.getXcyAppraise(args.ModelId, args.CollectorCode, args.LoginName, args.StreetCode, string.Empty, args.RoleId, field, order, sq.startDate.ToString("yyyy-MM-dd"), sq.endDate.ToString("yyyy-MM-dd"), ref rowCount, ref pageCount, out cols, out strReportMessage);

            pageInfo.RowCount = rowCount.ToString();
            pageInfo.PageCount = pageCount.ToString();
            args.strReportMessage = strReportMessage;

            if (dt != null && dt.Rows.Count > 0)
            {
                list = ConvertDtHelper<Collecter_Apprise>.GetModelList(dt);
            }

            return list;
        }


    }
}
