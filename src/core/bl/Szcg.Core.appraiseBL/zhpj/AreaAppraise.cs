/* ****************************************************************************************
 * 版权所有：杭州天夏科技
 * 用    途：综合评价区域模块逻辑层
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-06-16
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using bacgDL.zhpj;
using Teamax.Common;
using System.Data.SqlClient;
using bacgDL.zhpj.areaappraise;

namespace bacgBL.zhpj.area
{
    public class AreaAppraise
    {
        public DataTable getAreaAppraise(int modelid, string parm1, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, "", "", "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, parm2, "", "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string parm3, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                        ref int pageCount, out string cols, out string ReportMessage)
        {
            return getAreaAppraise(modelid, parm1, parm2, parm3, "", roleid, field, order, startdate, enddate, ref rowCount, ref pageCount, out cols, out ReportMessage);
        }
        public DataTable getAreaAppraise(int modelid, string parm1, string parm2, string parm3, string parm4, string roleid, string field, string order, string startdate, string enddate, ref int rowCount,
                                                ref int pageCount, out string cols, out string ReportMessage)
        {
            DataTable dt = null;
            DataTable dtn = null;
            cols = "";
            ReportMessage = "";
            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //桐乡项目修改
                    if (modelid == 25)
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
                        
                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetAreaData(24, parm1, parm2, parm3, parm4, roleid, sqy.startDate, sqy.endDate, field, order, ref rowCount, ref pageCount, out cols, out ReportMessage);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["部门名称"] = array[i - 1].ToString() + "月份";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // 注意：这里的drSource是另一个相同结构的DataTable中的一行。
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        dtn.Columns["部门名称"].ColumnName = "考评月份";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetAreaData(modelid, parm1, parm2, parm3, parm4, roleid, sq.startDate, sq.endDate, field, order, ref rowCount, ref pageCount, out cols, out ReportMessage);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SetStatDate(StructQuery sq)
        {
            DateTime startDate, endDate;
            if (sq.intMonths != 0)
            {
                startDate = new DateTime(sq.intYears, sq.intMonths, 1);
                if (sq.intMonths != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intMonths + 1, 1);
                    endDate = endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intQuarter != 0)
            {
                startDate = new DateTime(sq.intYears, (sq.intQuarter - 1) * 3 + 1, 1);

                if (sq.intQuarter * 3 != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intQuarter * 3 + 1, 1);
                    endDate = endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intWeeks != 0)
            {
                DateTime dt = new DateTime(sq.intYears, 1, 1);
                int weeknow = Convert.ToInt32(dt.DayOfWeek);

                startDate = dt.AddDays(-1 * weeknow + (sq.intWeeks - 1) * 7 + 1);
                endDate = dt.AddDays(-1 * weeknow + (sq.intWeeks) * 7);
            }
            else
            {
                startDate = new DateTime(sq.intYears, 1, 1);
                endDate = new DateTime(sq.intYears, 12, 31);
            }
            sq.startDate = startDate;
            sq.endDate = endDate;
        }

        public DataTable ProjectStatOutPut(int modelid, string qycode, string roleid, string field, string order, string starttime, string endtime, ref int rowCount,
                                                ref int pageCount, DataTable dt_)
        {
            string cols = "";
            string ReportMessage = "";
            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = getAreaAppraise(modelid, qycode, roleid, field, order, starttime, endtime, ref rowCount, ref pageCount, out cols, out ReportMessage).Copy();
            if (modelid == 24)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["pcode"].ToString() != "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }                 
            }
            
            dt.Columns.Remove("pcode");
            dt.Columns.Remove("code");
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }
        public DataTable ProjectStatOutPut1(int modelid, string qycode, string parm2, string parm3, string roleid, string field, string order, string starttime, string endtime, ref int rowCount,
                                           ref int pageCount, DataTable dt_)
        {
            string cols = "";
            string ReportMessage = "";
            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = getAreaAppraise(modelid, qycode, parm2, parm3, roleid, field, order, starttime, endtime, ref rowCount, ref pageCount, out cols, out ReportMessage).Copy();
            if (modelid == 24)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["pcode"].ToString() != "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }
            }

            dt.Columns.Remove("pcode");
            dt.Columns.Remove("code");
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }
        public DataTable getDepart()
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getDepart();
            }
        }
        public DataTable getImgNum(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum(id);
            }
        }
        public DataTable getImgNum1(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum1(id);
            }
        }
        public DataTable getImgNum2(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum2(id);
            }
        }
        public DataTable getImgNum3(int id)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.getImgNum3(id);
            }
        }
        #region GetExpress
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridcode"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetExpress(int modelid, string columnname)
        {
            try
            {
                string strNames = "-1";
                string sql = string.Format(@" select Express from a_appraise_field_detail
                                where ISBASE = 0
                                    and Viewname = '{0}'
	                                and model ={1}", columnname, modelid);
                DataTable dt = new DataTable();
                using (bacgDL.business.Project dl = new bacgDL.business.Project())
                {
                    dt = dl.ExecuteDataset(sql).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    strNames = dt.Rows[0]["Express"].ToString();
                }

                return strNames;
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        public DataTable GetExcelTemple(int modelid)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.GetExcelTemple(modelid);
            }
        }

        public DataTable GetExcelTemple2(int modelid)
        {
            using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
            {
                return dl.GetExcelTemple2(modelid);
            }
        }

        //评价模型一
        public DataTable GetZjcgAreaAppraise(string modelid, string departcode, string roleid, string startdate, string enddate)
        {
            DataTable dt = null;
            DataTable dtn = null;

            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //桐乡项目修改
                    if (modelid == "25")
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };

                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetZjcgAreaDate(departcode, roleid, sqy.startDate, sqy.endDate);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["depName"] = array[i - 1].ToString() + "月份";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // 注意：这里的drSource是另一个相同结构的DataTable中的一行。
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        //dtn.Columns["部门名称"].ColumnName = "考评月份";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetZjcgAreaDate(departcode, roleid, sq.startDate, sq.endDate);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //评价模型2：按桐乡问题解决率评价
        public DataTable GetTxcgAreaAppraise(string modelid, string departcode, string roleid, string startdate, string enddate)
        {
            DataTable dt = null;
            DataTable dtn = null;

            StructQuery sq = new StructQuery();
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    //桐乡项目修改
                    if (modelid == "25")
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        bacgDL.zhpj.areaappraise.StructQuery sqy = new bacgDL.zhpj.areaappraise.StructQuery();
                        intYear = Convert.ToDateTime(startdate).Year;
                        intMonth = DateTime.Now.Month;

                        if (intYear != DateTime.Now.Year)
                        {
                            intMonth = 12;

                        }

                        String[] array = new String[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };

                        try
                        {
                            dl.BeginTrans();
                            for (int i = 1; i <= intMonth; i++)
                            {
                                sqy.intYears = intYear;
                                sqy.intMonths = i;
                                SetStatDate(sqy);

                                dt = dl.GetTxcgAreaDate(departcode, roleid, sqy.startDate, sqy.endDate);

                                if (i == 1)
                                {
                                    dtn = dt.Clone();
                                }

                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    dt.Rows[0]["depName"] = array[i - 1].ToString() + "月份";
                                    DataRow drTarget = dtn.NewRow();
                                    drTarget.ItemArray = dt.Rows[0].ItemArray;
                                    // 注意：这里的drSource是另一个相同结构的DataTable中的一行。
                                    dtn.Rows.Add(drTarget);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            dl.Rollback();
                            ErrMsg = e.Message;
                        }
                        //dtn.Columns["部门名称"].ColumnName = "考评月份";
                        return dtn;
                    }
                    else
                    {
                        sq.startDate = Convert.ToDateTime(startdate);
                        sq.endDate = Convert.ToDateTime(enddate);
                        dt = dl.GetTxcgAreaDate(departcode, roleid, sq.startDate, sq.endDate);
                        //MyCache.Insert(strCacheKey, dt, 1200);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProjectStatOutPut_zjcg(string modelid, string departcode, string roleid, string starttime, string endtime, DataTable dt_)
        {

            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = GetZjcgAreaAppraise(modelid, departcode, roleid, starttime, endtime).Copy();

            dt.Columns.Remove("depCode");

            if (modelid == "24")
            {
                dt.Columns["depName"].ColumnName = "部门名称";
            }
            else
            {
                dt.Columns["depName"].ColumnName = "统计月份";
            }
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable ProjectStatOutPut_txcg(string modelid, string departcode, string roleid, string starttime, string endtime, DataTable dt_)
        {

            DataTable dt = null;
            if (dt_ != null)
                dt = dt_.Copy();
            else
                dt = GetTxcgAreaAppraise(modelid, departcode, roleid, starttime, endtime).Copy();
            
            dt.Columns.Remove("depCode");
            //dt.Columns.Remove("dutyid");
            dt.Columns.Remove("code");
            dt.Columns.Remove("pcode");

            if (modelid == "24")
            {
                //dt.Columns["dutyid"].ColumnName = "序号";
                dt.Columns["depName"].ColumnName = "部门名称";
            }
            else
            {
                dt.Columns["depName"].ColumnName = "统计月份";
            }
            StructQuery sq = new StructQuery();

            sq.startDate = Convert.ToDateTime(starttime);
            sq.endDate = Convert.ToDateTime(endtime);

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }

        public DataTable GetEvePartCount(string starttime, string endtime, out string ReportMessage,string streetcode)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetEvePartCount(starttime, endtime,out ReportMessage,streetcode);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
                }

            }
            catch(Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }


        /// <summary>
        /// 拓展普查统计
        /// </summary>
        public DataTable GetUnEvePartCount(string starttime, string endtime, out string ReportMessage, string streetcode)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetUnEvePartCount(starttime, endtime, out ReportMessage, streetcode);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ReportMessage"></param>
        /// <returns></returns>
        public DataTable GetEventCount(string starttime, string endtime)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetEventCount(starttime, endtime);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// 部件上报
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="strReportMessage"></param>
        /// <returns></returns>
        public DataTable GetPartCount(string starttime, string endtime)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetPartCount(starttime, endtime);
                    StructQuery sq = new StructQuery();
                    sq.startDate = Convert.ToDateTime(starttime);
                    sq.endDate = Convert.ToDateTime(endtime);
                    dt.TableName = "统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                dt = null;
            }
            return dt;
        }

        #region 按月查询评价2012-10-23
        /// <summary>
        /// 按月查询评价
        /// </summary>
        /// <param name="montype">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="DateStart">开始时间</param>
        /// <param name="DateEnd">结束时间</param>
        /// <param name="monthint">哪个月</param>
        /// <param name="code">部门编号</param>
        /// <returns></returns>
        public DataTable GetappraisebyMonth(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyMonth(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region 按周查询评价2012-10-23
        /// <summary>
        /// 按周查询评价
        /// </summary>
        /// <param name="montype">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="DateStart">开始时间</param>
        /// <param name="DateEnd">结束时间</param>
        /// <param name="monthint">哪周</param>
        /// <param name="code">部门编号</param>
        /// <returns></returns>
        public DataTable GetappraisebyWeek(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyWeek(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region 按季度查询评价2012-10-23
        /// <summary>
        /// 按季度查询评价
        /// </summary>
        /// <param name="montype">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="DateStart">开始时间</param>
        /// <param name="DateEnd">结束时间</param>
        /// <param name="monthint">哪季度</param>
        /// <param name="code">部门编号</param>
        /// <returns></returns>
        public DataTable GetappraisebyQuarter(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyQuarter(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion

        #region 按年查询评价2012-10-23
        /// <summary>
        /// 按年查询评价
        /// </summary>
        /// <param name="montype">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="DateStart">开始时间</param>
        /// <param name="DateEnd">结束时间</param>
        /// <param name="monthint">哪年</param>
        /// <param name="code">部门编号</param>
        /// <returns></returns>
        public DataTable GetappraisebyYear(int montype, string DateStart, string DateEnd, int monthint, string code)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraisebyYear(montype, DateStart, DateEnd, monthint, code);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        #endregion
        #region 按年度单位考评查询评价2012-10-23
        /// <summary>
        /// 按年度单位考评查询评价
        /// </summary>
        /// <param name="code">部门编码</param>
        /// <param name="monthint">哪年</param>
        /// <returns></returns>
        public DataTable GetappraiseYearbyYear(string code, int monthint)
        {
            DataTable dt = null;
            try
            {
                using (bacgDL.zhpj.areaappraise.AreaAppraise dl = new bacgDL.zhpj.areaappraise.AreaAppraise())
                {
                    dt = dl.GetappraiseYearbyYear(code, monthint);
                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion
    }
}
