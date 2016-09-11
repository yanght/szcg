using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using szbaseDBL.system;

namespace bacgBL.system
{
    public class systemSet
    {


        #region 之前的系统用的
        //自动生成评价报表列表
        public void setRptReport()
        {          
            try
            {
                using (bacgDL.system.systemSet sy=new bacgDL.system.systemSet())
                {
                    sy.setRptReport();
                }
            }
            catch (Exception e)
            {
                
            }
        }

        //自动生成每年备份数据库
        public void setSzcg_bak_Data()
        {
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    sy.setSzcg_bak_Data();
                }
            }
            catch (Exception e)
            {

            }
        }
        public void setSzcg_bak_Data_Now()
        {
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    sy.setSzcg_bak_Data_Now();
                }
            }
            catch (Exception e)
            {

            }
        }

        //自动备份数据库
        public void bak_Szcg_Data()
        {
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    sy.bak_Szcg_Data();
                }
            }
            catch (Exception e)
            {

            }
        }

        //清理数据库，数据量大的表的备份和清理
        public void clear_Szcg_Data()
        {
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    sy.clear_Szcg_Data();
                }
            }
            catch (Exception e)
            {

            }
        }

        //清理数据库，数据量大的日志表清理
        public void delete_Szcg_Data()
        {
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    sy.delete_Szcg_Data();
                }
            }
            catch (Exception e)
            { 
                
            }
        }
        #endregion

        #region GetBusinessList：获取通讯录列表
        //对于停留时间达到核查阶段总时限的60%时，系统自动向监督员的城管通手机发送短信提醒
        /// <summary>
        /// 获取通讯录列表
        /// </summary>
        /// <param name="prj">通讯录结构体</param>        
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetMessageList(BusiMsg prj, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
                {
                    return sy.GetMessageList(prj);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion


        #region 生成考评

        #region 按月生成(每月最后一天生成)
        /// <summary>
        /// 按月生成(每月最后一天生成)
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_ByMonth(int modelid, int typemodel)
        {

            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            int year=DateTime.Now.Year;
            int month=DateTime.Now.Month;
            //string monstime = FirstDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd"); //"2012-01-01";
           // string monetime = LastDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd"); //"2012-01-31";
            string monstime="";
            if(month==1)
            {
                monstime=Convert.ToString((year-1).ToString()+"-12-26");
            }
            else{
            monstime=Convert.ToString(year.ToString()+"-"+(month-1).ToString()+"-26");
            }
            string monetime = Convert.ToString(year.ToString()+"-"+month.ToString()+"-25");
            string stime = monstime + " 00:00:00";
            string etime = monetime + " 23:59:59";
            DataTable dt = getAreaAppraise(modelid, "", "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
            //增加dutyid部门排序
            DataTable dep_dt = getDepart();
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
            ReportSecCompany_ByMonth(dt, FirstDayOfMonth(DateTime.Now).ToString("yyyy-MM-dd"), typemodel, DateTime.Now.Month, DateTime.Now.ToString("yyyy-MM-dd"));
            //ReportSecCompany_ByMonth(dt, "2012-01-01", typemodel, 1, "2012-01-31");
        }
        /// <summary>
        /// 生成考评按月
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">开始日期如：2012-01-01</param>
        /// <param name="type">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="monthint">第几月</param>
        /// <param name="dateend">截止日期</param>
        public void ReportSecCompany_ByMonth(DataTable dt, string datenum, int type, int monthint, string dateend)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = dt.Rows[i]["排序号"].ToString() == "" ? 999 : int.Parse(dt.Rows[i]["排序号"].ToString());
                    dl.ReportSecCompany_ByMonth(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs,ljwtyjjzs, wtjsjjl, wtjjl,df, sortnum, datenum, type, dateend, monthint);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 按季度生成(每季度最后一天生成)
        /// <summary>
        /// 按季度生成(每季度最后一天生成)
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_ByQuarter(int modelid, int typemodel)
        {

            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            DateTime Ntime = DateTime.Now;  //当前时间
            DateTime startQuarter = Ntime.AddMonths(0 - (Ntime.Month - 1) % 3).AddDays(1 - Ntime.Day);  //本季度初
            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末
            string monstime = startQuarter.ToString("yyyy-MM-dd"); //"2012-01-01";
            string monetime = endQuarter.ToString("yyyy-MM-dd"); //"2012-01-31";
            string stime = monstime + " 00:00:00";
            string etime = monetime + " 23:59:59";
            DataTable dt = getAreaAppraise(modelid, "", "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
            //增加dutyid部门排序
            DataTable dep_dt = getDepart();
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
            ReportSecCompany_ByQuarter(dt, startQuarter.ToString("yyyy-MM-dd"), typemodel, GetQuarterByMonth(DateTime.Now.Month), endQuarter.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 生成考评按季度
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">开始日期如：2012-01-01</param>
        /// <param name="type">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="monthint">第几季度</param>
        /// <param name="dateend">截止日期</param>
        public void ReportSecCompany_ByQuarter(DataTable dt, string datenum, int type, int monthint, string dateend)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = dt.Rows[i]["排序号"].ToString() == "" ? 999 : int.Parse(dt.Rows[i]["排序号"].ToString());
                    dl.ReportSecCompany_ByQuarter(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs, ljwtyjjzs, wtjsjjl, wtjjl, df, sortnum, datenum, type, dateend, monthint);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 按年生成(每年度最后一天生成)
        /// <summary>
        /// 按年生成(每年最后一天生成)
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_ByYear(int modelid, int typemodel)
        {

            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            DateTime Ntime = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(Ntime.Year, 1, 1);  //本年年初
            DateTime endYear = new DateTime(Ntime.Year, 12, 31);  //本年年末 
            string monstime = startYear.ToString("yyyy-MM-dd"); //"2012-01-01";
            string monetime = endYear.ToString("yyyy-MM-dd"); //"2012-01-31";
            string stime = monstime + " 00:00:00";
            string etime = monetime + " 23:59:59";
            DataTable dt = getAreaAppraise(modelid, "", "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
            //增加dutyid部门排序
            DataTable dep_dt = getDepart();
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
            ReportSecCompany_ByYear(dt, startYear.ToString("yyyy-MM-dd"), typemodel, DateTime.Now.Year, endYear.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 生成考评按季度
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">开始日期如：2012-01-01</param>
        /// <param name="type">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="monthint">第几季度</param>
        /// <param name="dateend">截止日期</param>
        public void ReportSecCompany_ByYear(DataTable dt, string datenum, int type, int monthint, string dateend)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = dt.Rows[i]["排序号"].ToString() == "" ? 999 : int.Parse(dt.Rows[i]["排序号"].ToString());
                    dl.ReportSecCompany_ByYear(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs, ljwtyjjzs, wtjsjjl, wtjjl, df, sortnum, datenum, type, dateend, monthint);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 按单位年考评生成(每年度最后一天生成)
        /// <summary>
        /// 按单位年考评生成(每年最后一天生成)
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_YearByYear(int modelid, int typemodel)
        {

            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            DateTime Ntime = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(Ntime.Year, 1, 1);  //本年年初
            DateTime endYear = new DateTime(Ntime.Year, 12, 31);  //本年年末 
            string monstime = startYear.ToString("yyyy-MM-dd"); //"2012-01-01";
            string monetime = endYear.ToString("yyyy-MM-dd"); //"2012-01-31";
            string stime = monstime + " 00:00:00";
            string etime = monetime + " 23:59:59";
            //增加dutyid部门排序
            DataTable depdata = null;
            DataTable dep_dt = getDepart();
            for (int i = 0; i < dep_dt.Rows.Count; i++)
            {
                string Scode = dep_dt.Rows[i]["UserDefinedCode"].ToString();
                depdata = getAreaAppraise(modelid, Scode, "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
                DataColumn dc1 = new DataColumn("排序号", System.Type.GetType("System.Int32"));
                ReportSecCompany_YearByYear(depdata, "", typemodel, DateTime.Now.Year, "");
            }
        }
        /// <summary>
        /// 生成单位年度考评
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">开始日期如：2012-01-01</param>
        /// <param name="type">没用到</param>
        /// <param name="monthint">年份</param>
        /// <param name="dateend">截止日期</param>
        public void ReportSecCompany_YearByYear(DataTable dt, string datenum, int type, int monthint, string dateend)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = 0;
                    dl.ReportSecCompany_YearByYear(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs, ljwtyjjzs, wtjsjjl, wtjjl, df, sortnum, datenum, type, dateend, monthint);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 按周生成(每个礼拜天晚上生成)
        /// <summary>
        /// 按周生成(每个礼拜天晚上生成)
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_ByWeek(int modelid, int typemodel)
        {

            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            string stime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + " 00:00:00";
            string etime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            int weekint = GetWeekOfYear(DateTime.Now) - 1;//如果当前日期为礼拜天则应该减去1取上一周
            DataTable dt = getAreaAppraise(modelid, "", "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
            //增加dutyid部门排序
            DataTable dep_dt = getDepart();
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
            ReportSecCompany_ByWeek(dt, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), typemodel, weekint, DateTime.Now.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 生成考评按周
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">开始日期如：2012-01-01</param>
        /// <param name="type">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        /// <param name="monthint">第几周</param>
        /// <param name="dateend">截止日期</param>
        public void ReportSecCompany_ByWeek(DataTable dt, string datenum, int type, int monthint, string dateend)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = dt.Rows[i]["排序号"].ToString() == "" ? 999 : int.Parse(dt.Rows[i]["排序号"].ToString());
                    dl.ReportSecCompany_ByWeek(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs, ljwtyjjzs, wtjsjjl, wtjjl, df, sortnum, datenum, type, dateend, monthint);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 按天生成
        /// <summary>
        /// 按天生成
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="typemodel"></param>
        public void ReportSecCompany_ByDate(int modelid, int typemodel)
        {
            
            int rowCount = 0;
            int pageCount = 0;
            string strReportMessage = "";
            string cols = "";
            string stime =  DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00";
            string etime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
            DataTable dt = getAreaAppraise(modelid, "", "331125", "", "", "2", "code", "asc", stime, etime, ref rowCount, ref pageCount, out cols, out strReportMessage);
            //增加dutyid部门排序
            DataTable dep_dt = getDepart();
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
            ReportSecCompany_ByDate(dt, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), typemodel);
        }
        /// <summary>
        /// 生成考评按天
        /// </summary>
        /// <param name="dt">生成的数据</param>
        /// <param name="datenum">日期如：2012-01-01</param>
        /// <param name="type">类型：1，二级平台职能部门考核，2，职能部门内部考核3、责任单位考核</param>
        public void ReportSecCompany_ByDate(DataTable dt, string datenum, int type)
        {
            bacgDL.system.systemSet dl = new bacgDL.system.systemSet();
            SqlConnection conn = null;
            SqlTransaction myTrans = null;
            SqlCommand command = null;
            try
            {
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                conn.Open();
                myTrans = conn.BeginTransaction();
                command = conn.CreateCommand();
                command.Transaction = myTrans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int wtjjs = dt.Rows[i]["问题解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题解决数"].ToString());
                    int wtyjjs = dt.Rows[i]["问题应解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题应解决数"].ToString());
                    int wtjsjjs = dt.Rows[i]["问题及时解决数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["问题及时解决数"].ToString());
                    int ljwtyjjzs = dt.Rows[i]["累计问题应解决总数"].ToString() == "" ? 0 : int.Parse(dt.Rows[i]["累计问题应解决总数"].ToString());
                    float wtjsjjl = dt.Rows[i]["问题及时解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题及时解决率"].ToString().Replace("%", ""));
                    float wtjjl = dt.Rows[i]["问题解决率"].ToString().Replace("%", "") == "" ? 0 : float.Parse(dt.Rows[i]["问题解决率"].ToString().Replace("%", ""));
                    float df = dt.Rows[i]["得分"].ToString() == "" ? 0 : float.Parse(dt.Rows[i]["得分"].ToString());
                    int sortnum = dt.Rows[i]["排序号"].ToString() == "" ? 999 : int.Parse(dt.Rows[i]["排序号"].ToString());
                    dl.ReportSecCompany_ByDate(dt.Rows[i][0].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["pcode"].ToString(),
                        wtjjs, wtyjjs, wtjsjjs, ljwtyjjzs, wtjsjjl, wtjjl, df, sortnum, datenum, type);
                }
                myTrans.Commit();
            }
            catch
            {
                myTrans.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                myTrans.Dispose();
                command.Dispose();
            }

        }
        #endregion

        #region 返回需要生成的评价数据
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
                using (bacgDL.system.systemSet dl = new bacgDL.system.systemSet())
                {
                    //桐乡项目修改
                    if (modelid == 25)
                    {
                        string ErrMsg = "";
                        int intYear;
                        int intMonth;

                        StructQuery sqy = new StructQuery();
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
        public DataTable getDepart()
        {
            using (bacgDL.system.systemSet sy = new bacgDL.system.systemSet())
            {
                return sy.getDepart();
            }
        }
        #endregion

        #region 获取第几周
        public  int GetWeekOfYear()
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = DateTime.Today.DayOfYear;

            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //     刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }
        public int GetWeekOfYear(DateTime dt)
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = dt.DayOfYear;//

            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //     刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }
        #endregion

        #region 获取某月是第几季度
        /// <summary>
        /// 获取某月是第几季度
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public int GetQuarterByMonth(int month)
        {
            int rus=0;
            switch (month)
            {
                case 12: rus = 1; break;
                case 1: rus = 1; break;
                case 2: rus = 1; break;
                case 3: rus = 2; break;
                case 4: rus = 2; break;
                case 5: rus = 2; break;
                case 6: rus = 3; break;
                case 7: rus = 3; break;
                case 8: rus = 3; break;
                case 9: rus = 4; break;
                case 10: rus = 4; break;
                case 11: rus = 4; break;
            }
            return rus;
        }
        #endregion

        #region 取得某月的第一天和最后一天
        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        private DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /**/
        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        private DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
        #endregion

        #endregion


    }      

}
