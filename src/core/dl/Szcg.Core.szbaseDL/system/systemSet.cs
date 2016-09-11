using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using Teamax.Common;
using System.Collections;
using szbaseDBL.system;


namespace bacgDL.system
{
    public class systemSet : Teamax.Common.CommonDatabase
    {
        //生成评价报表列表
        public void setRptReport()
        {
            DateTime time1 = DateTime.Now;
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@initdate",time1)
            };
            this.ExecuteDataset("pr_a_GetResultSet_init_rpt", CommandType.StoredProcedure, arrSP);

            string sql = string.Format(@"insert into Sys_JobLog (cu_date, sqltext) values ('{0}','生成评价报表列表')",time1);
            this.ExecuteNonQuery(sql);
           
        }

        //生成备份数据库
        public void setSzcg_bak_Data()
        {

        }
        public void setSzcg_bak_Data_Now()
        {
           
        }

        //自动备份数据库
        public void bak_Szcg_Data()
        {
           
            
        }

        //清理数据库，数据量大的表的备份和清理
        public void clear_Szcg_Data()
        {
           
            
        }

        //清理数据库，数据量大的日志表清理
        public void delete_Szcg_Data()
        {
           
        }

        #region GetBusinessList：获取通讯录列表
        //对于停留时间达到核查阶段总时限的60%时，系统自动向监督员的城管通手机发送短信提醒
        /// <summary>
        /// 获取通讯录列表
        /// </summary>
        /// <param name="prj">通讯录结构体</param>        
        /// <param name="page">分页结构体</param>
        /// <returns></returns>
        public DataSet GetMessageList(BusiMsg prj)
        {
          
            
            return null;
                
        }
        #endregion

        #region 生成考评
        #region 评价生成按天
        /// <summary>
        /// 评价生成按天
        /// </summary>
        /// <param name="ar_captionname">第一列名称</param>
        /// <param name="code">部门编码</param>
        /// <param name="pcode">部门父编码</param>
        /// <param name="wtjjs">问题解决数</param>
        /// <param name="wtyjjs">问题应解决数</param>
        /// <param name="wtjsjjs">问题及时解决数</param>
        /// <param name="ljwtyjjzs">累计问题应解决总数</param>
        /// <param name="wtjsjjl">问题及时解决率</param>
        /// <param name="wtjjl">问题解决率</param>
        /// <param name="df">得分</param>
        /// <param name="sortnum">排序号</param>
        /// <param name="datenum">生成日期</param>
        /// <param name="artype">类型</param>
        /// <returns></returns>
        public int ReportSecCompany_ByDate(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend","")
            };
            //dateend目前没用
            return this.ExecuteNonQuery("pr_a_InsertreportbyDate", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        #region 评价生成按周
        /// <summary>
        /// 评价生成按周
        /// </summary>
        public int ReportSecCompany_ByWeek(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype, string dateend, int monthint)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),//开始日期
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend",dateend),//截止日期
                                new SqlParameter("@monthint",monthint)
            };
            return this.ExecuteNonQuery("pr_a_InsertreportbyWeek", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        #region 评价生成按月
        /// <summary>
        /// 评价生成按月
        /// </summary>
        public int ReportSecCompany_ByMonth(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype, string dateend, int monthint)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),//开始日期
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend",dateend),//截止日期
                                new SqlParameter("@monthint",monthint)
            };
            return this.ExecuteNonQuery("pr_a_InsertreportbyMonth", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        #region 评价生成按季度
        /// <summary>
        /// 评价生成按季度
        /// </summary>
        public int ReportSecCompany_ByQuarter(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype, string dateend, int monthint)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),//开始日期
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend",dateend),//截止日期
                                new SqlParameter("@monthint",monthint)
            };
            return this.ExecuteNonQuery("pr_a_InsertreportbyQuarter", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        #region 评价生成按年
        /// <summary>
        /// 评价生成按年
        /// </summary>
        public int ReportSecCompany_ByYear(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype, string dateend, int monthint)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),//开始日期
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend",dateend),//截止日期
                                new SqlParameter("@monthint",monthint)
            };
            return this.ExecuteNonQuery("pr_a_InsertreportbyYear", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        #region 责任单位年度考评生成
        /// <summary>
        /// 责任单位年度考评生成
        /// </summary>
        public int ReportSecCompany_YearByYear(string ar_captionname, string code, string pcode, int wtjjs, int wtyjjs, int wtjsjjs, int ljwtyjjzs, float wtjsjjl, float wtjjl, float df, int sortnum, string datenum, int artype, string dateend, int monthint)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@ar_captionname", ar_captionname),
                                new SqlParameter("@code", code),
                                new SqlParameter("@pcode",pcode),
                                new SqlParameter("@wtjjs",wtjjs),
                                new SqlParameter("@wtyjjs",wtyjjs),
                                new SqlParameter("@wtjsjjs",wtjsjjs),
                                new SqlParameter("@ljwtyjjzs",ljwtyjjzs),
                                new SqlParameter("@wtjsjjl",wtjsjjl),           
                                new SqlParameter("@wtjjl",wtjjl),
                                new SqlParameter("@df",df),
                                new SqlParameter("@sortnum",sortnum),
                                new SqlParameter("@datenum",datenum),//开始日期
                                new SqlParameter("@artype",artype),
                                new SqlParameter("@dateend",dateend),//截止日期
                                new SqlParameter("@monthint",monthint)
            };
            return this.ExecuteNonQuery("pr_a_InsertreportYearbyYear", CommandType.StoredProcedure, arrSP);

        }
        #endregion

        //调用存储过程获取区的评价结果
        public DataTable GetAreaData(int modelid, string parm1, string parm2, string parm3, string parm4, string roleid, DateTime start, DateTime end, string field, string order, ref int rowCount,
                                            ref int pageCount, out string cols, out string ReportMessage)
        {


            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@mode_id", modelid),
                                new SqlParameter("@roleid", roleid),
                                new SqlParameter("@CurrentPage",1),
                                new SqlParameter("@DateStart",start),
                                new SqlParameter("@DateEnd",end),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",-1),           
                                new SqlParameter("@field",field),
                                new SqlParameter("@order",order),
                                new SqlParameter("@parm1",parm1),
                                new SqlParameter("@parm2",parm2),
                                new SqlParameter("@parm3",parm3),
                                new SqlParameter("@parm4",parm4),
                                new SqlParameter("@Out_ReportMessage",SqlDbType.VarChar,500)
            };
            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;
            arrSP[14].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetResultSet", CommandType.StoredProcedure, 600, arrSP);
            ReportMessage = arrSP[14].Value.ToString();
            rowCount = arrSP[5].Value == null || arrSP[5].Value.ToString() == "" ? 0 : int.Parse(arrSP[5].Value.ToString());

            DataTable dt = ds.Tables[0];//建一个新的实例
            if (ds.Tables.Count > 1)
                cols = ds.Tables[1].Rows[0][0].ToString();
            else
                cols = "";

            if (rowCount == -1)
            {
                return null;
            }
            else
            {
                #region 不用代码
                //for (int i = 0; i < dr.FieldCount; i++)
                //{
                //    DataColumn mydc = new DataColumn();//关键的一步
                //    mydc.DataType = dr.GetFieldType(i);
                //    mydc.ColumnName = dr.GetName(i);
                //    dt.Columns.Add(mydc);//关键的第二步
                //}

                //while (dr.Read())
                //{
                //    DataRow mydr = dt.NewRow();//关键的第三步
                //    for (int i = 0; i < dr.FieldCount; i++)
                //    {
                //        mydr[i] = dr[i].ToString();
                //    }

                //    dt.Rows.Add(mydr);//关键的第四步
                //    mydr = null;
                //}

                //dr.Close();
                #endregion

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool tag = true;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["pcode"].ToString() == dt.Rows[j]["code"].ToString())
                        {
                            tag = false;
                            break;
                        }
                    }
                    if (tag)
                        dt.Rows[i]["pcode"] = "";
                }
            }


            return dt;
        }
        public DataTable getDepart()
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"select UserDefinedCode,departcode,isnull(dutyid,999) 排序号 from p_depart");

            ds = this.ExecuteDataset(sql, null);

            DataTable dt = ds.Tables[0];
            return dt;
        }
        #endregion
    }
}