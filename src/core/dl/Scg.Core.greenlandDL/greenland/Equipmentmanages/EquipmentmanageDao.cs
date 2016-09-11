using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;

namespace bacgDL.greenland.Equipmentmanages
{
    public class EquipmentmanageDao:Teamax.Common.CommonDatabase
    {
        //�������������õ������豸��������
        public PageManage GetAllLhsbResult(Equipments pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@departcode",pat.departcode),
                                new SqlParameter("@catalog",pat.catalog),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")};

                arrSP[5].Direction = ParameterDirection.Output;
                arrSP[6].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllLhsblist", CommandType.StoredProcedure, arrSP);
                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[5].Value);
                page.pageCount = Convert.ToInt32(arrSP[6].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ���������豸��������
        /// <summary>
        /// ���������豸��������
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int insertIntoLhsb(Equipments mat)
        {
            string sql = "insert into g_equipmentmanage(departcode,equipmentcatalog,equipmentspec,address,equipmenttype,dealdate,photo,remark) "
                          + " values('" + mat.departcode + "','" + mat.catalog + "','" + mat.spec + "','" + mat.address + "','" + mat.type + "','" + mat.dealdate + "','" + mat.photo + "','" + mat.remark + "')";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ���������豸������Ϣ
        /// <summary>
        /// ���������豸������Ϣ
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int updateIntoLhsb(Equipments mat)
        {
            string sql = "update g_equipmentmanage set departcode='" + mat.departcode + "',address='" + mat.address + "',equipmentcatalog='" + mat.catalog + "',equipmentspec='" + mat.spec + "',equipmenttype='" + mat.type + "',dealdate='" + mat.dealdate + "',photo='" + mat.photo + "',remark='" + mat.remark + "'  where recid=" + mat.recid;

            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public int deleteFromLhsb(int recid)
        {
            string sql = "delete from g_equipmentmanage where recid =" + recid;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region ��������recID�õ���������Ϣ
        /// <summary>
        /// ��������recID�õ���������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader getLhsbInfoByID(int id)
        {
            string sql = "select a.*,d.departname from g_equipmentmanage a, p_depart d where  a.departcode = d.departcode and a.recid=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region �õ������豸���ͻ�������;
        /// <summary>
        /// �õ������豸���ͻ�������;
        /// </summary>
        /// <returns>���ݼ�</returns>
        public DataSet GetEquipment()
        {
            string sql = "select codeid,codename from s_codedict where codetype =12 and codeid>0 and isnull(status,0)=0";
            try
            {
                using (CommonDatabase comm = new CommonDatabase())
                {
                    return comm.ExecuteDataset(sql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion;
    }
}
