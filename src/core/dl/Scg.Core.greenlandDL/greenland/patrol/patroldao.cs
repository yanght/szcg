/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;���̻�ϵͳ�ճ�Ѳ�����ݿ����
 * �ṹ��ɣ�
 * ��    �ߣ�³ΰ��
 * �������ڣ�2007-09-20
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵���� 
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using Teamax.Common;


namespace bacgDL.greenland.patrol
{
    public class patroldao:Teamax.Common.CommonDatabase
    {
        //�������������õ����������б�
        public PageManage getallpatrollist(patrolcls pat, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@streetcode",pat.streetcode),
                                new SqlParameter("@Time1",pat.date1),
                                new SqlParameter("@Time2",pat.date2),  
                                new SqlParameter("@CurrentPage",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","desc"),
                                new SqlParameter("@Field","recid")};

                arrSP[4].Direction = ParameterDirection.Output;
                arrSP[5].Direction = ParameterDirection.Output;

                DataSet ds = this.ExecuteDataset("pr_e_getAllpatrollist", CommandType.StoredProcedure, arrSP);
                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[4].Value);
                page.pageCount = Convert.ToInt32(arrSP[5].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����һ������������Ϣ
        public int insertIntorecord(patrolcls rec)
        {
            string sql = " insert into g_patrol( areacode,  streetcode, commcode, "
                        + " departcode ,person, patroladdress ,outerdepartcode,"
                        + " problem ,situation, patroldate,remark) "
                        + " values('" + rec.areacode + "','" + rec.streetcode + "','" + rec.commcode + "',"
                        + rec.departcode + ",'" + rec.person + "','" + rec.patroladdress + "'," + rec.outerdepartcode + ",'"
                        + rec.problem + "','" + rec.situation + "','" + rec.patroldate + "','" + rec.remark + "')";
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

        //����һ������������Ϣ
        public int updateonerecord(patrolcls rec)
        {
            string sql = "update g_patrol set areacode='" + rec.areacode
                                              + "',streetcode='" + rec.streetcode
                                              + "',commcode='" + rec.commcode
                                              + "',departcode=" + rec.departcode
                                              + ",person='" + rec.person
                                              + "',patroladdress='" + rec.patroladdress
                                              + "',outerdepartcode=" + rec.outerdepartcode
                                              + ",problem='" + rec.problem
                                              + "',situation='" + rec.situation
                                              + "',patroldate='" + rec.patroldate
                                              + "',remark='" + rec.remark + "' where recid=" + rec.recid;

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
        //ɾ������������¼
        public int deleteonerecord(int id)
        {
            string sql = "delete from g_patrol where recid =" + id;
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

        //��������id�õ����û���Ϣ
        public SqlDataReader getpatrolInfobyid(int id)
        {
            string sql = " select a.*,b.streetname,c.commname,isnull(d.departname,'') as  departname, "
                       + " isnull(e.departname,'') as outerdepartname "
                       + " from g_patrol a "
                       + " left join s_community c on a.commcode = c.commcode   "
                       + " left join s_street b on a.streetcode = b.streetcode  "
                       + " left join "
	                   + "   (select departcode,departname from p_depart "
	                   + "    union select departcode,departname from s_depart_outer) d "
                       + "    on a.departcode = d.departcode "
                       + " left join "
	                   + "   (select departcode,departname from p_depart "
	                   + "    union select departcode,departname from s_depart_outer) e "
                       + "    on a.outerdepartcode=e.departcode "
                       + " where 1=1 and a.recid=" + id;
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
    }
}
