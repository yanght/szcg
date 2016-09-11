using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace bacgDL.zhpj.areaappraise
{
    public class StructQuery
    {
        public int intWeeks = 0;
        public int intMonths = 0;
        public int intYears = 0;
        public int intQuarter = 0;
        public DateTime startDate = new DateTime();
        public DateTime endDate = new DateTime();
    }

    public class AreaAppraise : Teamax.Common.CommonDatabase, IDisposable
    {

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
            DataSet ds = this.ExecuteDataset("pr_a_GetResultSet", CommandType.StoredProcedure,600,arrSP);
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
        public DataTable getImgNum(int id)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"  select id as model,modelname from a_appraise_model where parentmodel={0} and isbase = '1' and isnull(isdel,0)=0", id);

            ds = this.ExecuteDataset(sql,null);

            DataTable dt = ds.Tables[0];
            return dt;
        }
        public DataTable getImgNum1(int id)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"  select id as model,modelname from a_appraise_model where parentmodel={0} and isbase = '1' and isnull(isdel,0)=0 and ID not in(29,28) ", id);

            ds = this.ExecuteDataset(sql, null);

            DataTable dt = ds.Tables[0];
            return dt;
        }
        public DataTable getImgNum2(int id)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"  select id as model,modelname from a_appraise_model where parentmodel={0} and isbase = '1' and isnull(isdel,0)=0 and ID not in(13,14) ", id);

            ds = this.ExecuteDataset(sql, null);

            DataTable dt = ds.Tables[0];
            return dt;
        }
        public DataTable getImgNum3(int id)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"  select id as model,modelname from a_appraise_model where parentmodel={0} and isbase = '1' and isnull(isdel,0)=0 and ID not in(24,33) ", id);

            ds = this.ExecuteDataset(sql, null);

            DataTable dt = ds.Tables[0];
            return dt;
        }
        public DataTable GetExcelTemple(int modelid)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@" select t.TempName as viewname from a_appraise_TFile  t
                                    left join a_appraise_model m on t.id = m.FORMTEMPLATE
                                    where m.ID={0} and m.isbase = '1' and isnull(m.isdel,0) = 0" , modelid);

            ds = this.ExecuteDataset(sql, null);

            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        public DataTable GetExcelTemple2(int modelid)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@" select t.TempName as viewname from a_appraise_TFile  t
                                    left join a_appraise_model m on t.id = m.FORMTEMPLATE
                                    where m.ID={0} and m.isbase = '1'", modelid);

            ds = this.ExecuteDataset(sql, null);

            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        //
        public DataTable GetZjcgAreaDate(string UserDefinedCode, string AreaCode, DateTime startdate, DateTime enddate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@UserDefinedCode", UserDefinedCode),
                                new SqlParameter("@AreaCode", AreaCode),
                                new SqlParameter("@DateStart_",startdate),
                                new SqlParameter("@DateEnd_",enddate)
            };

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_M_DepartAreaModel_T5", CommandType.StoredProcedure, 600, arrSP);
            DataTable dt = ds.Tables[0];//建一个新的实例
            return dt;
        }

        ////评价模型2：按桐乡问题解决率评价
        public DataTable GetTxcgAreaDate(string UserDefinedCode, string AreaCode, DateTime startdate, DateTime enddate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@UserDefinedCode", UserDefinedCode),
                                new SqlParameter("@AreaCode", AreaCode),
                                new SqlParameter("@DateStart_",startdate),
                                new SqlParameter("@DateEnd_",enddate)
            };

            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_M_DepartAreaModel_T4", CommandType.StoredProcedure, 600, arrSP);
            DataTable dt = ds.Tables[0];//建一个新的实例
            return dt;
        }


        public DataTable GetEvePartCount(string starttime, string endtime, out string ReportMessage,string streetcode)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@startdate",starttime),
                                new SqlParameter("@enddate",endtime),
                                new SqlParameter("@retResult",SqlDbType.VarChar,500),
                                new SqlParameter("@streetcode",streetcode)
            };
            arrSP[2].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetEveParTJ", CommandType.StoredProcedure, 600, arrSP);
            ReportMessage = arrSP[2].Value.ToString();
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
            }
        }
        /// <summary>
        /// 拓展普查统计
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ReportMessage"></param>
        /// <returns></returns>
        public DataTable GetUnEvePartCount(string starttime, string endtime, out string ReportMessage, string streetcode)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@startdate",starttime),
                                new SqlParameter("@enddate",endtime),
                                new SqlParameter("@retResult",SqlDbType.VarChar,500),
                                new SqlParameter("@streetcode",streetcode)
            };
            arrSP[2].Direction = ParameterDirection.Output;
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetUnEveParTJ", CommandType.StoredProcedure, 600, arrSP);
            ReportMessage = arrSP[2].Value.ToString();
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
            }
        }




        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public DataTable GetEventCount(string starttime, string endtime)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@startdate",starttime),
                                new SqlParameter("@enddate",endtime),
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_sj_sjsb", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
            }
        }


        /// <summary>
        /// 部件上报
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public DataTable GetPartCount(string starttime, string endtime)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@startdate",starttime),
                                new SqlParameter("@enddate",endtime),
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_bj_sjsb", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
            }
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@montype",montype),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),
                                new SqlParameter("@monthint",monthint),
                                new SqlParameter("@code",code)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetappraisebyMonth", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@montype",montype),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),
                                new SqlParameter("@monthint",monthint),
                                new SqlParameter("@code",code)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetappraisebyWeek", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@montype",montype),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),
                                new SqlParameter("@monthint",monthint),
                                new SqlParameter("@code",code)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetappraisebyQuarter", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@montype",montype),
                                new SqlParameter("@DateStart",DateStart),
                                new SqlParameter("@DateEnd",DateEnd),
                                new SqlParameter("@monthint",monthint),
                                new SqlParameter("@code",code)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetappraisebyYear", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
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
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@code",code),
                                new SqlParameter("@monthint",monthint)
            };
            this._CommandTimeOut = 1200;
            DataSet ds = this.ExecuteDataset("pr_a_GetappraiseYearbyYear", CommandType.StoredProcedure, 600, arrSP);
            if (ds.Tables.Count == 0)
                return null;
            else
            {
                DataTable dt = ds.Tables[0];//建一个新的实例
                return dt;
            }
        }
        #endregion
    }
}
