using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using bacgDL.business;
namespace bacgBL.sjjh
{
    public class SJ_cq
    {
        bacgDL.sjjh.SJ_cq cq = new bacgDL.sjjh.SJ_cq();
        DataTable dt = null;
        /// <summary>
        /// 数据抽取
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable getSJ_CqList(string type)
        {
            try
            {
                dt = new DataTable();
                dt=cq.get_cqlist(type);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 数据注册发布
        /// </summary>
        /// <param name="type"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public DataSet getfb_zc(string type, string currentpage, string pagesize, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.sjjh.SJ_cq sj=new bacgDL.sjjh.SJ_cq ())
                {
                    return sj.get_sjlist(type, currentpage, pagesize, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 修改接口状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public bool updateState(string id, int state,out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.sjjh.SJ_cq sj = new bacgDL.sjjh.SJ_cq())
                {
                    return sj.UpdateState (id,state );
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
        }
        /// <summary>
        /// 获得部门数据
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public DataSet getDepList(string departcode, string currentpage, string pagesize, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (bacgDL.sjjh.SJ_cq sj = new bacgDL.sjjh.SJ_cq())
                {
                    return sj.get_Deplist(departcode, currentpage, pagesize, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
    }
}
