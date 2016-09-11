//* ****************************************************************************************
// * 版权所有：嘉兴康思网络科技有限公司 
// * 用    途：对知识库部门进行显示，方便调用。
// * 结构组成： 对szcg.web.szbase.repositoryandarchives.BASE_selectpart页面业务进行封装
// * 作    者：何伟
// * 创建日期：2007-06-11
// * 历史记录：
// * ****************************************************************************************
// * 修改人员：               
// * 修改日期： 
// * 修改说明：   
// * **************************************************************************************using System;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using AjaxPro;

namespace bacgBL.web.szbase.repositoryandarchives
{
    public class BASE_selectpart
    {
        public BASE_selectpart()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.web.szbase.repositoryandarchives";


        /// <summary>
        /// 从公文表中取数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：公文ID，公文标题</returns>
        //public ArrayList getAllArchives()
        //{
        //     try
        //    {
        //        using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
        //        {
        //            return dl.getAllArchives ();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 从部门表中取数据 
        /// </summary>
        /// <param name="argUserCode">userId，area</param>
        /// <returns>部门相关信息</returns>
        public static ArrayList getDepts(int userId, string areacode)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getDepts(userId, areacode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



        /// <summary>
        /// 从部门表中取数据 
        /// </summary>
        /// <param name="argUserCode">userId，area</param>
        /// <returns>部门相关信息</returns>
        public  ArrayList getDepts()
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getDepts();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
          

        }



        /// <summary>
        /// 从部门表中取最小的父节点代码数据 
        /// </summary>
        /// <param name="argUserCode">无</param>
        /// <returns>列表相关：部门相关信息</returns>

        public static int getMinParentDepartID(int userId)
        {
            //string sql="select min(parentcode) from depart";
            //string sql = "SELECT MIN(PARENTCODE) FROM P_DEPART";
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getMinParentDepartID( userId );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }


    }
}
