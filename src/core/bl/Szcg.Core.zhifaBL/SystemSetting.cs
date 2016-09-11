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
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "dl.zhifa";

        /// <summary>
        /// 取得字典库数据 
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns>群组类型</returns>
        public DataSet GetZidiankuInfo()
        {
            try
            {
                dl.zhifa.SystemSetting dl = new dl.zhifa.SystemSetting();
                return dl.GetZidiankuInfo(pageSize);
            }
            catch (Exception ex)
            {
               　throw ex;
            }
        }


        /// <summary>
        /// 取得文书模板数据 
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns>群组类型</returns>
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
        /// 新增文书
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
        /// 新增法律条文
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
        /// 删除案卷下的所有法律条文
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
        /// 查看案卷下的所有法律条文
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
        /// 案卷文书列表
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
        /// 案卷文书列表
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
        /// 文书详细信息
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
        /// 法律条文列表
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
        /// 获得字典库数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="argfid">日志常用语id, 惯用语Id, 办公用语Id</param>
        /// <param name="argPageIndex">页序号</param>
        /// <param name="argPageSize">页的行数</param>
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
        /// 获得字典库数据条数
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="argfid">日志常用语id, 惯用语Id, 办公用语Id</param>
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
        /// 获得阶段信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 获得字典详细信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 修改字典详细信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
        /// 删除字典信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 新增字典信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
        /// 新增附件
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
        /// 附件浏览
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
        /// 附件添加
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
