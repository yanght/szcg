/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
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

namespace bacgDL.szbase.organize
{
    public class DepartManage : Teamax.Common.CommonDatabase, IDisposable
    {
        #region ExecDatasetSql��ִ��sql��䣬����dataset���͵����ݼ�
        /// <summary>
        /// ִ��sql����������䣬����dataset���͵����ݼ�
        /// </summary>
        /// <param name="strSQL">Ҫ��ѯ��sql���</param>
        /// <returns></returns>
        public DataSet ExecDatasetSql(string strSQL)
        {
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion

        #region ExecScalarSql��ִ��sql���,���ص�һ�е�һ��
        /// <summary>
        /// ִ��sql�����������,���ص�һ�е�һ��
        /// </summary>
        /// <param name="strSQL">Ҫ��ѯ��sql���</param>
        /// <returns></returns>
        public object ExecScalarSql(string strSQL)
        {
            return this.ExecuteScalar(strSQL);
        }
        #endregion

        #region ExecNonQuerySql��ִ��sql���,������Ӱ�������
        /// <summary>
        /// ִ��sql�����������,������Ӱ�������
        /// </summary>
        /// <param name="strSQL">Ҫ��ѯ��sql���</param>
        /// <returns></returns>
        public int ExecNonQuerySql(string strSQL)
        {
            return this.ExecuteNonQuery(strSQL);
        }
        #endregion

        #region ExecReaderSql��ִ��sql���,����datareader���͵����ݼ�
        /// <summary>
        /// ִ��sql���,����datareader���͵����ݼ�
        /// </summary>
        /// <param name="strSQL">Ҫ��ѯ��sql���</param>
        /// <returns></returns>
        public SqlDataReader ExecReaderSql(string strSQL)
        {
            return (SqlDataReader)this.ExecuteReader(strSQL);
        }
        #endregion

        #region ExecProc��ִ�д洢����
        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="pid">���ڵ�</param>
        /// <returns></returns>
        public DataSet ExecProc(string procName,ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            DataSet ds = ExecuteDataset(procName, CommandType.StoredProcedure, arrSp);
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
        public SqlDataReader ExecReaderProc(string procName, ref SqlParameter[] Sp)
        {
            SqlParameter[] arrSp = Sp;
            return (SqlDataReader)ExecuteReader(procName,CommandType.StoredProcedure,false,arrSp);
        }
        #endregion

    }
}
