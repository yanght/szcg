using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.szbase.collecter
{
    public class CollecterManage:Teamax.Common.CommonDatabase
    {
        #region InsertIntoCollecter:插入监督员信息
        /// <summary>
        /// 插入监督员信息
        /// </summary>
        /// <param name="values">监督员信息列表</param>
        /// <returns></returns>
        public string InsertIntoCollecter(string[] values)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@commcode",values[0]),
                                new SqlParameter("@gridcode",values[1]),
                                new SqlParameter("@numbercode",values[2]),
                                new SqlParameter("@collname",values[3]),
                                new SqlParameter("@loginname",values[4]),
                                new SqlParameter("@pwd",values[5]),
                                new SqlParameter("@sex",Convert.ToChar(values[6])),
                                new SqlParameter("@mobile",values[7]),
                                new SqlParameter("@age",values[8]),
                                new SqlParameter("@url",values[9]),
                                new SqlParameter("@hometel",values[10]),
                                new SqlParameter("@homeaddress",values[11]),
                                new SqlParameter("@timeout",values[12]),
                                new SqlParameter("@memo",values[13]),
                                new SqlParameter ("@imei",values[14]),
                                new SqlParameter("@isguard",'0'),
                                new SqlParameter("@result", SqlDbType.Int)};
            arrSP[16].Direction = ParameterDirection.Output;


            this.ExecuteNonQuery("pr_m_InsertToCollecter", CommandType.StoredProcedure, arrSP);
            return arrSP[16].Value.ToString();
        }
        #endregion

        #region UpdateCollecter:更新监督员信息
        /// <summary>
        /// 更新监督员信息
        /// </summary>
        /// <param name="values">监督员信息列表</param>
        /// <param name="collcode">监督员编号</param>
        /// <returns></returns>
        public string UpdateCollecter(string[] values,string collcode)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@collcode",collcode),
                                new SqlParameter("@commcode",values[0]),
                                new SqlParameter("@gridcode",values[1]),
                                new SqlParameter("@numbercode",values[2]),
                                new SqlParameter("@collname",values[3]),
                                new SqlParameter("@loginname",values[4]),
                                new SqlParameter("@pwd",values[5]),
                                new SqlParameter("@sex",Convert.ToChar(values[6])),
                                new SqlParameter("@mobile",values[7]),
                                new SqlParameter("@age",values[8]),
                                new SqlParameter("@url",values[9]),
                                new SqlParameter("@hometel",values[10]),
                                new SqlParameter("@homeaddress",values[11]),
                                new SqlParameter("@timeout",values[12]),
                                new SqlParameter("@memo",values[13]),
                                new SqlParameter("@imei",values[14]),
                                new SqlParameter("@result", SqlDbType.Int)};
            arrSP[16].Direction = ParameterDirection.Output;

            this.ExecuteNonQuery("pr_m_UpdateToCollecter", CommandType.StoredProcedure, arrSP);
            return arrSP[16].Value.ToString();
        }
        #endregion

        #region InsertIntoLawer:插入执法人员信息
        /// <summary>
        /// 插入执法人员信息
        /// </summary>
        /// <param name="values">执法人员信息列表</param>
        /// <returns></returns>
        public int InsertIntoLawer(string[] values)
        {
            string strSQL = string.Format(@"insert into s_lawer(commcode,gridcode,numbercode,
                                                                    lawername,loginname,pwd,
                                                                    url,cu_date,timeout,
                                                                    mobile,sex,age,
                                                                    hometel,homeaddress,memo) 
                                            values('{0}','{1}','{2}',
                                                   '{3}','{4}','{5}',
                                                    '{6}','{7}','{8}',
                                                    '{9}','{10}','{11}',
                                                    '{12}','{13}','{14}')",
                                                 values[0],values[1],values[2],
                                                 values[3],values[4],values[5],
                                                values[6],DateTime.Now,values[7],
                                                values[8],Convert.ToChar(values[9]),values[10],
                                                values[11],values[12],values[13]);


            return this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region UpdateIntoLawer:更新执法人员信息
        /// <summary>
        /// 更新执法人员信息
        /// </summary>
        /// <param name="id">执法人员编码</param>
        /// <param name="values">执法人员信息列表</param>
        /// <returns></returns>
        public int UpdateIntoLawer(int id,string[] values)
        {
            string strSQL=string.Format(@"update s_lawer set commcode='{0}',gridcode='{1}',numbercode='{2}',
                                            lawername='{3}',loginname='{4}',pwd='{5}',
                                            url='{6}',timeout='{7}',mobile='{8}',
                                            sex='{9}',age='{10}',hometel='{11}',
                                            homeaddress='{12}',memo='{13}'
                            where lawercode='{14}'", values[0], values[1], values[2],
                                                values[3], values[4], values[5],
                                                values[6], values[7], values[8],
                                                Convert.ToChar(values[9]), values[10], values[11],
                                                values[12], values[13],id);
            return this.ExecuteNonQuery(strSQL);
        }
        #endregion


    }
}
