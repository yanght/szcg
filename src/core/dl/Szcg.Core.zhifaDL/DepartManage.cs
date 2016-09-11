/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
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

namespace dl.zhifa
{
    public class DepartManage : Teamax.Common.CommonDatabase, IDisposable
    {
        #region ExecDatasetOracle：执行Oracle语句，返回dataset类型的数据集
        /// <summary>
        /// 执行Oracle不带参数语句，返回dataset类型的数据集
        /// </summary>
        /// <param name="strOracle">要查询的Oracle语句</param>
        /// <returns></returns>
        public DataSet ExecDatasetOracle(string strOracle)
        {
            DataSet ds = this.ExecuteDataset(strOracle);
            return ds;
        }
        #endregion

        #region ExecScalarOracle：执行Oracle语句,返回第一行第一列
        /// <summary>
        /// 执行Oracle不带参数语句,返回第一行第一列
        /// </summary>
        /// <param name="strOracle">要查询的Oracle语句</param>
        /// <returns></returns>
        public object ExecScalarOracle(string strOracle)
        {
            return this.ExecuteScalar(strOracle);
        }
        #endregion

        #region ExecNonQueryOracle：执行Oracle语句,返回受影响的行数
        /// <summary>
        /// 执行Oracle不带参数语句,返回受影响的行数
        /// </summary>
        /// <param name="strOracle">要查询的Oracle语句</param>
        /// <returns></returns>
        public int ExecNonQueryOracle(string strOracle)
        {
            return this.ExecuteNonQuery(strOracle);
        }
        #endregion

        #region ExecReaderOracle：执行Oracle语句,返回datareader类型的数据集
        /// <summary>
        /// 执行Oracle语句,返回datareader类型的数据集
        /// </summary>
        /// <param name="strOracle">要查询的Oracle语句</param>
        /// <returns></returns>
        public IDataReader ExecReaderOracle(string strOracle)
        {
            return (IDataReader)this.ExecuteReader(strOracle);
        }
        #endregion

        #region ExecProc：执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="pid">父节点</param>
        /// <returns></returns>
        public DataSet ExecProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            DataSet ds = this.ExecuteDataset(procName, CommandType.StoredProcedure, arrSp);
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
        public IDataReader ExecReaderProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return (IDataReader)ExecuteReader(procName, CommandType.StoredProcedure, false, arrSp);
        }
        #endregion

    }
}
