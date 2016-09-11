using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.szbase.capability
{
    public class Backup : Teamax.Common.CommonDatabase, IDisposable
    {
        /// <summary>
        /// ��ñ������ݿ��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetDbList(int userId)
        {
            DataSet ds = ExecuteDataset("sp_databases");
            return ds;
        }

        /// <summary>
        /// ���ݿⱸ��
        /// </summary>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool DbBackup(int userId, string database)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@database", database)};
                ExecuteNonQuery("backupDb", CommandType.StoredProcedure, arrSP);
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ��ñ������ݿ��б�
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="databaseName">���ݿ�����</param>
        /// <param name="backPath">�����ļ�·��</param>
        /// <returns></returns>
        public IDataReader GetBackupList(int userId, string databaseName, string backPath)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@backPath", backPath), 
                                new SqlParameter("@databaseName", databaseName)};
            IDataReader dr = ExecuteReader("getBackupList", CommandType.StoredProcedure, true, arrSP);
            return dr;
        }

        /// <summary>
        /// ��ԭ���ݿ�
        /// </summary>
        /// <param name="databaseName">���ݿ�����</param>
        /// <param name="disk">�豸����</param>
        /// <param name="fileNo">�ļ���</param>
        /// <returns>��ԭ�ɹ���ʧ��</returns>
        public bool RevertDb(string databaseName, string disk, int fileNo)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@databaseName", databaseName),
                                new SqlParameter("@disk", disk),
                                new SqlParameter("@fileNo", fileNo)};
                ExecuteNonQuery("revertDb", CommandType.StoredProcedure, true, arrSP);
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �ලԱ�켣���ݱ���
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="days">����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public int backUpCollecterTrack(int userId, int days)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@_runtime", days)};
                int iRet = ExecuteNonQuery("runAt_collectxy",CommandType.StoredProcedure, true, arrSP);
                return iRet;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
