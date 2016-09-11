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
        /// 获得本地数据库列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetDbList(int userId)
        {
            DataSet ds = ExecuteDataset("sp_databases");
            return ds;
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <returns>备份是否成功</returns>
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
        /// 获得备份数据库列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="backPath">备份文件路径</param>
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
        /// 还原数据库
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="disk">设备名称</param>
        /// <param name="fileNo">文件号</param>
        /// <returns>还原成功与失败</returns>
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
        /// 监督员轨迹数据备份
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="days">天数</param>
        /// <returns>备份是否成功</returns>
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
