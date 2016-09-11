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
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.com.teamax.szbase.systemsetting.SystemSetting";

        /// <summary>
        /// 取得字典库数据 
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns>群组类型</returns>
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
               　throw ex;
            }
        }

        /// <summary>
        /// 取得科室名称 
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns>群组类型</returns>
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
        /// 获得部门人员通信录信息
        /// </summary>
        /// <param name="userId">部门Id</param>
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
        /// 获得字典库数据条数
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="argfid">日志常用语id, 惯用语Id, 办公用语Id</param>
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
        /// 获得阶段信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 获得通信录个人信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 获得字典详细信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 修改字典详细信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
        /// 删除字典信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 删除通信录信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
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
        /// 新增字典信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
        /// 新增通信录信息
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
        /// 修改通信录信息
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="fid"></param>
        /// <param name="short_sentence">简称</param>
        /// <param name="text_sentence">内容</param>
        /// <param name="stepid">阶段ID</param>
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
