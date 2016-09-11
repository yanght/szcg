/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：部门结构树管理。
 * 结构组成：
 * 作    者：王超群
 * 创建日期：2007-05-25
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.szbase.organize
{
    public class DepartManage : Teamax.Common.CommonDatabase, IDisposable
    {
        #region ExecDatasetSql：执行sql语句，返回dataset类型的数据集
        /// <summary>
        /// 执行sql不带参数语句，返回dataset类型的数据集
        /// </summary>
        /// <param name="strSQL">要查询的sql语句</param>
        /// <returns></returns>
        public DataSet ExecDatasetSql(string strSQL)
        {
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion

        #region ExecScalarSql：执行sql语句,返回第一行第一列
        /// <summary>
        /// 执行sql不带参数语句,返回第一行第一列
        /// </summary>
        /// <param name="strSQL">要查询的sql语句</param>
        /// <returns></returns>
        public object ExecScalarSql(string strSQL)
        {
            return this.ExecuteScalar(strSQL);
        }
        #endregion

        #region ExecNonQuerySql：执行sql语句,返回受影响的行数
        /// <summary>
        /// 执行sql不带参数语句,返回受影响的行数
        /// </summary>
        /// <param name="strSQL">要查询的sql语句</param>
        /// <returns></returns>
        public int ExecNonQuerySql(string strSQL)
        {
            return this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region ExecReaderSql：执行sql语句,返回datareader类型的数据集
        /// <summary>
        /// 执行sql语句,返回datareader类型的数据集
        /// </summary>
        /// <param name="strSQL">要查询的sql语句</param>
        /// <returns></returns>
        public SqlDataReader ExecReaderSql(string strSQL)
        {
            return (SqlDataReader)this.ExecuteReader(strSQL);
        }
        #endregion

        #region ExecProc：执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="pid">父节点</param>
        /// <returns></returns>
        public DataSet ExecProc(string procName,ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            DataSet ds = ExecuteDataset(procName, CommandType.StoredProcedure, arrSp);
            return ds;
        }
        #endregion


        #region ExecNonQueryProc：执行存储过程，返回受影响的行数
        /// <summary>
        /// 执行存储过程，返回受影响的行数
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <returns></returns>
        public int ExecNonQueryProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return ExecuteNonQuery(procName, CommandType.StoredProcedure, arrSp);
        }
        #endregion

        #region ExecReaderProc：执行存储过程,返回Datareader
        /// <summary>
        /// 执行存储过程,返回Datareader
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <returns></returns>
        public SqlDataReader ExecReaderProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return (SqlDataReader)ExecuteReader(procName,CommandType.StoredProcedure,false,arrSp);
        }
        #endregion

    }
}
