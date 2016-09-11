using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace bacgBL.com.teamax.szbase.systemsetting
{
    public class SystemSetting
    {
        public SystemSetting()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.systemsetting.SystemSetting";

        /// <summary>
        /// ȡ���ֵ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetZidiankuInfo(int userId)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetZidiankuInfo(userId, pageSize);
                }
            }
            catch (Exception ex)
            {
               ��throw ex;
            }
        }

        /// <summary>
        /// ȡ�ÿ������� 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetPhoneBookInfo()
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetPhoneBookInfo();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����ֵ������
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="argfid">��־������id, ������Id, �칫����Id</param>
        /// <param name="argPageIndex">ҳ���</param>
        /// <param name="argPageSize">ҳ������</param>
        /// <returns></returns>
        public DataSet GetInfoD(int userId, String argfid, int argPageIndex, int argPageSize)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetInfoD(userId, argfid, argPageIndex, argPageSize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ��ò�����Աͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">����Id</param>
        /// <returns></returns>
        public DataSet GetPhoneBookInfoD(int userId)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetPhoneBookInfoD(userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����ֵ����������
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="argfid">��־������id, ������Id, �칫����Id</param>
        /// <returns></returns>
        public int GetDictionRecodeCount(int userId, String argfid)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetDictionRecodeCount(userId, argfid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ý׶���Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <returns>DataSet</returns>
        public DataSet GetJieDuanInfo(int userId)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetJieDuanInfo(userId, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���ͨ��¼������Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <returns>DataSet</returns>
        public DataSet GetPhoneBookGRInfo(int id)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetPhoneBookGRInfo(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����ֵ���ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <returns>DataSet</returns>
        public string[] GetDPInfo(int userId, int id, String fid)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.GetDPInfo(userId, id,fid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �޸��ֵ���ϸ��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>int</returns>
        public int UpdateInfo(int userId, int id, String fid, string short_sentence, string text_sentence, string stepid)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.UpdateInfo(userId, id, fid, short_sentence, text_sentence, stepid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ɾ���ֵ���Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int DeleteDiction(int userId, int id)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.DeleteDiction(userId, id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ɾ��ͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int DeleteTXL(int id)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.DeleteTXL(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �����ֵ���Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>int</returns>
        public int InsertData(int userId, String fid, string short_sentence, string text_sentence, string stepid)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.InsertData(userId, fid,short_sentence,text_sentence ,stepid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ����ͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>int</returns>
        
        public int InsertPhoneBookData(string departId,string role,string name, string officeTel, string insideTel,string mobileTel,string houseTel, string remarks)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.InsertPhoneBookData(departId, role, name, officeTel, insideTel, mobileTel, houseTel, remarks);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �޸�ͨ��¼��Ϣ
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">���</param>
        /// <param name="text_sentence">����</param>
        /// <param name="stepid">�׶�ID</param>
        /// <returns>int</returns>

        public int UpdatePhoneBookData(int id, string departId, string role, string name, string officeTel, string insideTel, string mobileTel, string houseTel, string remarks)
        {
            try
            {
                using (bacgDL.szbase.systemsetting.SystemSetting dl = new bacgDL.szbase.systemsetting.SystemSetting())
                {
                    return dl.UpdatePhoneBookData(id, departId, role, name, officeTel, insideTel, mobileTel, houseTel, remarks);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
