using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL;
using bacgDL.greenland.monthinspections;
using System.Data;

namespace bacgBL.greenland
{
    public class YjInspection
    {
        monthinspectionDao yjdjDao = new monthinspectionDao();

        #region �õ��¼�Ǽ���Ϣ�б�
        /// <summary>
        /// �õ��¼�Ǽ���Ϣ�б�
        /// </summary>
        /// <param name="pat"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageManage GetAllYjdjResult(Monthinspections pat, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = yjdjDao.GetAllYJdjResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region �����¼�Ǽ���Ϣ
        /// <summary>
        /// �����¼�Ǽ���Ϣ
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int insertIntoYjdj(Monthinspections mat)
        {
            try
            {
                int i = yjdjDao.insertIntoYjdj(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region �����¼�Ǽ���Ϣ
        /// <summary>
        /// �����¼�Ǽ���Ϣ
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int updateIntoYjdj(Monthinspections mat)
        {
            try
            {
                int i = yjdjDao.updateIntoYjdj(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ɾ���¼�Ǽ���Ϣ
        /// <summary>
        /// ɾ���¼�Ǽ���Ϣ
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public int deleteFromYjdj(int recid)
        {
            try
            {
                int i = yjdjDao.deleteFromYjdj(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��������recid�õ����¼�Ǽ���Ϣ
        /// <summary>
        /// ��������recid�õ����¼�Ǽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Monthinspections getRcyhInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)yjdjDao.getYjdjInfoByID(id);
                Monthinspections per = new Monthinspections();
                while (rs.Read())
                {
                    per.recid = id;
                    per.type = Convert.ToInt32(rs["type"]);
                    per.streetcode = rs["streetcode"].ToString();
                    per.streetname = rs["streetname"].ToString();
                    per.areacode = rs["areacode"].ToString();
                    per.commcode = rs["commcode"].ToString();
                    per.commname = rs["commname"].ToString();
                    per.outerdepartcode = rs["outerdepartcode"].ToString();
                    per.departname = rs["departname"].ToString();
                    per.problem = rs["problem"].ToString();
                    per.person = rs["person"].ToString();
                    per.address = rs["address"].ToString();
                    per.dealdate = rs["dealdate"].ToString();
                    per.remark = rs["remark"].ToString();
                }
                return per;
            }
            catch
            {
                throw;
            }
        }
#endregion
    }
}
