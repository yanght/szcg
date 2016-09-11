using System;
using System.Collections.Generic;
using System.Text;
//using sjjhDBL.WebSjjh;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace szbaseBL.system
{
    //参数设置中最少5分钟运行一次Execute方法

    public class Runjob : Teamax.Common.ScheduledTasks.IJob
    {
        public void Execute(System.Xml.XmlNode node)
        {
            string minseconds = node.Attributes["seconds"].Value;//隔多少秒执行一次
            int minMinute = (int.Parse(minseconds) / 60) * 6 - 1;   //保证能够执行一次
            //minMinute = minMinute < 2 ? 2 : minMinute;
            DateTime lastmonth = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1);//当前月的最后一天
            DateTime Ntime = DateTime.Now;  //当前时间
            DateTime startQuarter = Ntime.AddMonths(0 - (Ntime.Month - 1) % 3).AddDays(1 - Ntime.Day);  //本季度初
            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末

            DateTime startYear = new DateTime(Ntime.Year, 1, 1);  //本年年初
            DateTime endYear = new DateTime(Ntime.Year, 12, 31);  //本年年末 

            if (node.Attributes["name"].Value == "system_index")  
            {
                bacgBL.system.systemSet st = new bacgBL.system.systemSet();

                #region 按日生成
                //每天凌晨1点10分生成一次二级平台职能部门考核评价按天
                if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 10)
                {
                    st.ReportSecCompany_ByDate(32, 1);
                }

                //每天凌晨1点20分生成职能部门内部考核考核评价按天
                if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 20)
                {
                    st.ReportSecCompany_ByDate(33, 2);
                }

                //每天凌晨1点30分生成职能部门内部考核考核评价按天Sunday
                if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 30)
                {
                    st.ReportSecCompany_ByDate(24, 3);
                }
                #endregion

                #region 按周
                //if (DateTime.Now.DayOfWeek.ToString() == "Sunday")
                //{
                    //每周日晚上11点10分生成二级平台职能部门考核评价 
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 10)
                    {
                        st.ReportSecCompany_ByWeek(32, 1);
                    }
                    //每周日晚上11点20分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 20)
                    {
                        st.ReportSecCompany_ByWeek(33, 2);
                    }
                    //每周日晚上11点30分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 30)
                    {
                        st.ReportSecCompany_ByWeek(24, 3);
                    }
                //}
                #endregion

                #region 按月
                //if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
              //  if (DateTime.Now.ToString("yyyy-MM-dd") == lastmonth.ToString("yyyy-MM-dd"))
               // {
                    
                    //每月最后一天晚上10点10分生成二级平台职能部门考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 10)
                    {
                        st.ReportSecCompany_ByMonth(32, 1);
                    }
                    //每月最后一天晚上10点20分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 20)
                    {
                        st.ReportSecCompany_ByMonth(33, 2);
                    }
                    //每月最后一天晚上10点30分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 30)
                    {
                        st.ReportSecCompany_ByMonth(24, 3);
                    }
               // }
                #endregion

                #region 按季度
                //if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
             //   if (DateTime.Now.ToString("yyyy-MM-dd") ==endQuarter.ToString("yyyy-MM-dd"))
             //   {

                    //每季度最后一天晚上10点5分生成二级平台职能部门考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 5)
                    {
                        st.ReportSecCompany_ByQuarter(32, 1);
                    }
                    //每季度最后一天晚上10点15分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 15)
                    {
                        st.ReportSecCompany_ByQuarter(33, 2);
                    }
                    //每季度最后一天晚上10点25分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 25)
                    {
                        st.ReportSecCompany_ByQuarter(24, 3);
                    }
            //    }
                #endregion

                #region 按年
                //if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
        //        if (DateTime.Now.ToString("yyyy-MM-dd") == endYear.ToString("yyyy-MM-dd"))
         //       {

                    //每年最后一天晚上9点5分生成二级平台职能部门考核评价
                    if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 5)
                    {
                        st.ReportSecCompany_ByYear(32, 1);
                    }
                    //每年最后一天晚上9点15分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 15)
                    {
                        st.ReportSecCompany_ByYear(33, 2);
                    }
                    //每年最后一天晚上9点25分生成职能部门内部考核考核评价
                    if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 25)
                    {
                        st.ReportSecCompany_ByYear(24, 3);
                    }
        //        }
                #endregion

                #region 按部门按年生成全年的考评
                //if (DateTime.Now.DayOfWeek.ToString() == "Thursday")
             //   if (DateTime.Now.ToString("yyyy-MM-dd") == endYear.ToString("yyyy-MM-dd"))
            //    {

                    //每年最后一天晚上11点2分生成年度考评
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 2)
                    {
                        st.ReportSecCompany_YearByYear(25, 4);
                    }
            //    }
                #endregion

            } 
        }

        #region 写文本日志
        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="txtinfo">内容</param>
        public void Writertextlogtest(string txtinfo)
        {
            string Txtpath = string.Empty;
            //文件夹路径
            try
            {
                Txtpath = "C:/测试项目/szcgCode/SzcgWeb/" + DateTime.Now.ToShortDateString() + "CCC.txt";
                if (!System.IO.File.Exists(Txtpath))
                {
                    System.IO.StreamWriter rw1 = File.AppendText(Txtpath);
                    rw1.WriteLine(txtinfo);
                    rw1.Close();
                    rw1.Dispose();
                }
                else
                {

                    System.IO.StreamWriter rw1 = File.AppendText(Txtpath);
                    rw1.WriteLine(txtinfo);
                    rw1.Close();
                    rw1.Dispose();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

    }

    
}
