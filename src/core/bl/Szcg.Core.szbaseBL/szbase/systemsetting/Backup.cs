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
        /// ��ñ������ݿ��б�
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
        /// ���ݿⱸ��
        /// </summary>
        /// <returns>�����Ƿ�ɹ�</returns>
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
        /// ��ñ������ݿ��б�
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="databaseName">���ݿ�����</param>
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
        /// �ලԱ�켣���ݱ���
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="days">����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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
