using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace bl.zhifa
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
        private const string NAMESPACE_PATH = "dl.zhifa";

        /// <summary>
        /// ȡ���ֵ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetZidiankuInfo()
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetZidiankuInfo(pageSize);
            }
            catch (Exception ex)
            {
               ��throw ex;
            }
        }


        /// <summary>
        /// ȡ������ģ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        public DataSet GetWenshuInfo()
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetWenshuInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int InsertWenshuData(string type, string title, string content, int filetype, int projcode, int usercode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.InsertWenshuData(type, title,content, filetype, projcode, usercode);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int InsertLaws(string lawIds, string projcode, string lawnames)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.InsertLaws(lawIds, projcode, lawnames);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ɾ�������µ����з�������
        /// </summary>
        public int deletetLaws(string projcode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.deletetLaws(projcode);

            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// �鿴�����µ����з�������
        /// </summary>
        public DataSet GetLaws(string projcode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetLaws(projcode);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public DataSet GetWenshuData(int projcode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetWenshuData(projcode);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public DataSet GetWenshuData1(int projcode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetWenshuData1(projcode);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ������ϸ��Ϣ
        /// </summary>
        public DataSet GetWsDetail(int id)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetWsDetail(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public DataSet GetLawData(string name)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetLawData(name);
            }
            catch (Exception err)
            {
                throw err;
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
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.GetInfoD(userId, argfid, argPageIndex, argPageSize);
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
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.GetDictionRecodeCount(userId, argfid);
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
        public DataSet GetJieDuanInfo()
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.GetJieDuanInfo(pageSize);
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
        public string[] GetDPInfo(int id)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.GetDPInfo(id);
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
        public int UpdateInfo(int id, String dictioncode, string short_sentence, string text_sentence,string content)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.UpdateInfo(id, dictioncode, short_sentence, text_sentence,content);
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
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.DeleteDiction(userId, id);
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
        public int InsertData(int userId, String fid, string short_sentence, string text_sentence, string content)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                    return dl.InsertData(userId, fid,short_sentence,text_sentence, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int InsertFujian(int type, string title, string content, int filetype)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.InsertFujian(type, title, content, filetype);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DataSet GetFujian(int projcode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetFujian(projcode);
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        public int InsertFujianData(string fileIds,  int filetype, int projcode, int usercode)
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.InsertFujianData( fileIds, filetype, projcode, usercode);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
