/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;�����Žṹ������
 * �ṹ��ɣ�
 * ��    �ߣ�����Ⱥ
 * �������ڣ�2007-05-25
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
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
        #region ExecDatasetOracle��ִ��Oracle��䣬����dataset���͵����ݼ�
        /// <summary>
        /// ִ��Oracle����������䣬����dataset���͵����ݼ�
        /// </summary>
        /// <param name="strOracle">Ҫ��ѯ��Oracle���</param>
        /// <returns></returns>
        public DataSet ExecDatasetOracle(string strOracle)
        {
            DataSet ds = this.ExecuteDataset(strOracle);
            return ds;
        }
        #endregion

        #region ExecScalarOracle��ִ��Oracle���,���ص�һ�е�һ��
        /// <summary>
        /// ִ��Oracle�����������,���ص�һ�е�һ��
        /// </summary>
        /// <param name="strOracle">Ҫ��ѯ��Oracle���</param>
        /// <returns></returns>
        public object ExecScalarOracle(string strOracle)
        {
            return this.ExecuteScalar(strOracle);
        }
        #endregion

        #region ExecNonQueryOracle��ִ��Oracle���,������Ӱ�������
        /// <summary>
        /// ִ��Oracle�����������,������Ӱ�������
        /// </summary>
        /// <param name="strOracle">Ҫ��ѯ��Oracle���</param>
        /// <returns></returns>
        public int ExecNonQueryOracle(string strOracle)
        {
            return this.ExecuteNonQuery(strOracle);
        }
        #endregion

        #region ExecReaderOracle��ִ��Oracle���,����datareader���͵����ݼ�
        /// <summary>
        /// ִ��Oracle���,����datareader���͵����ݼ�
        /// </summary>
        /// <param name="strOracle">Ҫ��ѯ��Oracle���</param>
        /// <returns></returns>
        public IDataReader ExecReaderOracle(string strOracle)
        {
            return (IDataReader)this.ExecuteReader(strOracle);
        }
        #endregion

        #region ExecProc��ִ�д洢����
        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="pid">���ڵ�</param>
        /// <returns></returns>
        public DataSet ExecProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            DataSet ds = this.ExecuteDataset(procName, CommandType.StoredProcedure, arrSp);
            return ds;
        }
        #endregion


        #region ExecNonQueryProc��ִ�д洢���̣�������Ӱ�������
        /// <summary>
        /// ִ�д洢���̣�������Ӱ�������
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <returns></returns>
        public int ExecNonQueryProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return ExecuteNonQuery(procName, CommandType.StoredProcedure, arrSp);
        }
        #endregion

        #region ExecReaderProc��ִ�д洢����,����Datareader
        /// <summary>
        /// ִ�д洢����,����Datareader
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <returns></returns>
        public IDataReader ExecReaderProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return (IDataReader)ExecuteReader(procName, CommandType.StoredProcedure, false, arrSp);
        }
        #endregion

    }
}
