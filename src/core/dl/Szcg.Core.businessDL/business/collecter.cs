/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：监督员管理-数据层访问类。

 * 结构组成：

 * 作    者：yannis
 * 创建日期：2007-05-25
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace bacgDL.business
{
    public class collecter : Teamax.Common.CommonDatabase
    {
        #region SendMsgToPda：向监督员PDA发送WEB消息
        /// <summary>
        /// 向监督员PDA发送WEB消息。

        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <param name="msgtitle">消息主题</param>
        /// <param name="msgcontent">消息内容</param>
        /// <param name="usercode">用户编号</param>
        public void SendPDAMsg(string collcode, string msgcontent, string msgtitle, string usercode)
        {
            string[] codes = collcode.Split(',');
            foreach (string code in codes)
            {
                if (code.Trim() == "")
                    continue;

                string sql = string.Format(@"   INSERT INTO m_pdamsg_other(collcode,cu_date,msgcontent,state,title,memo,usercode)  
                                                values({0},getdate(),'{1}',0,'{2}','1',{3})",
                                code, msgcontent, msgtitle, usercode);

                string sql1 = string.Format(@"   INSERT INTO [m_pdamsg_android]([fromcollcode],[tocollcode],[title],[msgcontent],[state],[senddate],[receivedate])  
                                                values({0},{1},'{2}','{3}',{4},{5},{6}) ",
                              0, code, msgtitle, msgcontent, 0, "getdate()", "NULL");


                this.ExecuteNonQuery(sql+sql1);
            }
        }
        #endregion

        #region GetStreetList:根据登陆用于的区域编码,获取街道列表
        /// <summary>
        /// 根据登陆用于的区域编码,获取区域列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetStreetList(string areacode)
        {
            string strSQL = string.Format(@"select '全部' as streetname,'321102' as streetcode,'' as xh  
                                            union 
                                            select streetname,streetcode,xh 
                                            from s_street
                                            where streetcode like '{0}%'
                                                and xh>0 
                                            order by xh", areacode);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetCollectereList:获取监督员列表

        /// <summary>
        /// 获取监督员列表

        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="gridcode">网格编码</param>
        /// <param name="collname">监督员名字</param>
        /// <param name="loginname">监督员登陆名字</param>
        /// <param name="collmobile">监督员号码</param>
        /// <param name="isguard">是否在岗</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="strOrder">排序方式</param>
        /// <param name="strField">排序字段</param>
        /// <returns></returns>
        public DataSet GetCollectereList(string streetcode, string gridcode, string collname,
                                                    string loginname, string collmobile, string isguard,
                                                    int curentpage,int pagesize,ref int rowCount,
                                                    ref int pageCount, string strOrder, string strField)
        {

            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",streetcode),
                                new SqlParameter("@gridcode",gridcode),
                                new SqlParameter("@collname",collname),
                                new SqlParameter("@loginname",loginname),
                                new SqlParameter("@collmobile",collmobile),
                                new SqlParameter("@isguard",isguard),
                                new SqlParameter("@CurrentPage",curentpage),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pagesize),
                                new SqlParameter("@Order",strOrder),
                                new SqlParameter("@Field",strField),
                             };

            arrSP[7].Direction = ParameterDirection.Output;
            arrSP[8].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_GetCollecterList", CommandType.StoredProcedure, arrSP);
            rowCount = int.Parse(arrSP[7].Value.ToString());
            pageCount = int.Parse(arrSP[8].Value.ToString());
    
            return ds;
        }
        #endregion

        #region GetCollectereList4HC:获取核查用的监督员列表

        /// <summary>
        /// 获取核查用的监督员列表

        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <returns></returns>
        public DataSet GetCollectereList4HC(string streetcode,string projcode)
        {

            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",streetcode),
                                new SqlParameter("@projcode",projcode)
                             };

            DataSet ds = this.ExecuteDataset("pr_b_GetCollecterList4HC", CommandType.StoredProcedure, arrSP);

            return ds;
        }
        #endregion

        #region GetCollectereInfo:获取监督员详细信息

        /// <summary>
        /// 获取监督员详细信息

        /// </summary>
        /// <param name="collcode">监督员编号</param>
        /// <returns></returns>
        public DataSet GetCollectereInfo(string collcode)
        {
            string strSQL = string.Format(@"select a.*,b.commname
                                            from m_collecter a left join s_community b
                                            on a.commcode = b.commcode
                                            where collcode = '{0}'", collcode);
            return this.ExecuteDataset(strSQL);
        }
        #endregion

        #region GetCollectQueryStat:获取核查信息列表
        /// <summary>
        /// 获取核查信息列表
        /// </summary>
        /// <param name="street">街道编码</param>
        /// <param name="loginname">登陆名</param>
        /// <param name="collname">姓名</param>
        /// <param name="mobile">城管通</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="strHCFlag">核查的状态</param>
        /// <param name="curentpage">当前页</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="strOrder">排序方式</param>
        /// <param name="strField">排序字段</param>
        /// <returns></returns>
        public DataSet GetCollectQueryStat(string street, string loginname, string collname,
                                                    string mobile, DateTime begin, DateTime end,
                                                    string strHCFlag, int curentpage, int pagesize,
                                                    ref int rowCount, ref int pageCount, string strOrder, string strField,string Hcpower)
        {            
            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@StreetID", street),
                    new SqlParameter("@loginname",loginname),
                    new SqlParameter("@collname",collname),
                    new SqlParameter("@mobile",mobile),
                    new SqlParameter("@DateStart",Convert.ToString(begin)),
                    new SqlParameter("@DateEnd", Convert.ToString(end)),  
                    new SqlParameter("@State",Convert.ToInt32(strHCFlag)),
                    new SqlParameter("@CurrentPage",curentpage),
                    new SqlParameter("@RowCount",SqlDbType.Int),
                    new SqlParameter("@PageCount",SqlDbType.Int),
                    new SqlParameter("@PageSize",pagesize),
                    new SqlParameter("@Order",strOrder),
                    new SqlParameter("@Field",strField),
                    new SqlParameter("@Hcpower",Hcpower),
                };

            spInputs[8].Direction = ParameterDirection.Output;
            spInputs[9].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_b_CollHCTotal", CommandType.StoredProcedure, spInputs);
            rowCount = int.Parse(spInputs[8].Value.ToString());
            pageCount = int.Parse(spInputs[9].Value.ToString());

            return ds;
        }
        #endregion
    }
}
