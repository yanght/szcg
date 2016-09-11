using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace bacgBL.com.teamax.szbase.capability
{
    public class Backup
    {
        public Backup()
        {
        
        }

        /// <summary>
        /// 获得本地数据库列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetDbList(int userId)
        {
            try
            {
                using (bacgDL.szbase.capability.Backup dl = new bacgDL.szbase.capability.Backup())
                {
                    return dl.GetDbList(userId);
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <returns>备份是否成功</returns>
        public bool DbBackup(int userId, string database)
        {
            try
            {
                using (bacgDL.szbase.capability.Backup dl = new bacgDL.szbase.capability.Backup())
                {
                    return dl.DbBackup(userId, database);
                }
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
        /// <returns></returns>
        public ArrayList GetBackupList(int userId, string databaseName)
        {
            string backPath = System.Configuration.ConfigurationManager.AppSettings["backupFilePath"];
            bacgDL.szbase.capability.Backup dl = new bacgDL.szbase.capability.Backup();

            IDataReader reader;
            ArrayList list;
            try
            {                
                reader = dl.GetBackupList(userId,databaseName,backPath);
                list = new ArrayList();
                while (reader.Read())
                {
                    string backupCode = Convert.ToString(reader["Position"]);
                    string backupDate = Convert.ToString(reader["BackupFinishDate"]);
                    backupDate = backupDate.Replace("-", "");
                    backupDate = backupDate.Replace(" ", "");
                    backupDate = backupDate.Replace(":", "");
                    backupDate = backupDate.Replace(".", "");
                    list.Add(backupCode + "," + databaseName + backupDate);
                }
                return list;
            }
            catch (Exception err)
            {
                throw err;
            }
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
                using (bacgDL.szbase.capability.Backup dl = new bacgDL.szbase.capability.Backup())
                {
                    return dl.RevertDb(databaseName, disk,fileNo);
                }
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
        public static int backUpCollecterTrack(int userId, int days)
        {
            try
            {
                using (bacgDL.szbase.capability.Backup dl = new bacgDL.szbase.capability.Backup())
                {
                    return Convert.ToInt32(dl.backUpCollecterTrack(userId, days));
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
