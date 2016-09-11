using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;
using Teamax.Common;


namespace bacgBL.szbase.organize
{
    public class DutyInfoManage
    {
        #region GetDepartDutyInfo����ȡ��ѡ���Ĳ��Ÿ�������Ϣ
        /// <summary>
        /// ��ȡ��ѡ���Ĳ��Ÿ�������Ϣ
        /// </summary>
        /// <param name="departID">���ű��</param>
        /// <returns></returns>
        public DataSet GetDepartDutyInfo(int departcode,string order,string field, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", departcode),
                                                        new SqlParameter("@Order", order),
                                                        new SqlParameter("@Field", field)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartDutyInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region InsertDutyInfo�����벿����������Ϣ
        /// <summary>
        /// ���벿����������Ϣ
        /// </summary>
        /// <param name="departcode">����id</param>
        /// <param name="TypeCode">�²�����ʶ</param>
        /// <param name="smallclass">С�����</param>
        /// <param name="SectionOffice">����λ</param>
        /// <param name="PrincipalName">����������</param>
        /// <param name="principalship">������ְ��</param>
        /// <param name="principalTel">�����˵绰</param>
        /// <param name="HeadName">�����쵼����</param>
        /// <param name="headship">�����쵼ְ��</param>
        /// <param name="HeadTel">�����쵼�绰</param>
        /// <returns></returns>
        public int InsertDutyInfo(int departcode, string TypeCode, string smallclass,
            string SectionOffice, string PrincipalName, string principalship, string principalTel,
            string HeadName, string headship, string HeadTel)
        {
            string strSql = string.Format(@"insert into dbo.p_depart_DutyInfo values({0},'{1}','{2}','{3}',
                                          '{4}','{5}','{6}','{7}','{8}','{9}')", departcode, TypeCode, smallclass, SectionOffice,
                                          PrincipalName, principalship, principalTel, HeadName, headship, HeadTel);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSql);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region GetDutyInfoByID������id��Ż�ȡ��Ϣ
        /// <summary>
        /// ����id��Ż�ȡ��Ϣ
        /// </summary>
        /// <param name="dutyid">�Զ�����</param>
        /// <returns></returns>
        public DataTable GetDutyInfoByID(int dutyid)
        {
            string strSql = "select * from p_depart_DutyInfo where id = " + dutyid + "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                     return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteDutyByID��ɾ�����Ÿ�������Ϣ
        /// <summary>
        /// ɾ�����Ÿ�������Ϣ
        /// </summary>
        /// <param name="dutyid">�Զ������</param>
        /// <returns></returns>
        public int DeleteDutyByID(int dutyid)
        {
            string strSql = "delete from p_depart_DutyInfo where id = " + dutyid + "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSql);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region UpdateDutyInfoByID�����²��Ÿ�������Ϣ
        /// <summary>
        /// ���²��Ÿ�������Ϣ
        /// </summary>
        /// <param name="userDefinedCode">�û��Զ������</param>
        /// <param name="departname">��������</param>
        /// <param name="parentcode">��������</param>
        /// <param name="area">����</param>
        /// <param name="principal">������</param>
        /// <param name="Mobile">�ƶ��绰</param>
        /// <param name="Tel">�绰</param>
        /// <param name="departaddress">���ŵ�ַ</param>
        /// <param name="memo">��ע</param>
        /// <param name="departcode">���ű���</param>
        /// <param name="isChange">�û��Զ�������Ƿ�ı�0:û�иı�,1�ı���</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int UpdateDutyInfoByID(int dutyid,string TypeCode, string smallclass,
            string SectionOffice, string PrincipalName, string principalship, string principalTel,
            string HeadName, string headship, string HeadTel)
        {
            string strSql = string.Format(@"update dbo.p_depart_DutyInfo set TypeCode = '{0}',smallcallCode='{1}', 
                                            SectionOffice='{2}',PrincipalName = '{3}', principalship='{4}', principalTel='{5}', 
                                            HeadName= '{6}', headship='{7}', HeadTel='{8}' where ID ={9}",TypeCode,smallclass,
                                            SectionOffice,PrincipalName,principalship,principalTel,HeadName,headship,HeadTel,dutyid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
               return 0;
            }
        }
        #endregion

        #region ��ȡ���������б�
        public DataTable getBigClass()
        {
            string strSql = string.Format(@"select distinct bigclasscode,name as bigname, '1' as Typecode 
                                            from dbo.s_bigclass_event with(nolock)
                                            union select distinct bigclasscode,name as bigname, '0' as Typecode 
                                            from  dbo.s_bigclass_part with(nolock)
                                            order by bigclasscode ");
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ��ȡС�������б�
        public DataTable getSmallClass(string typecode)
        {
            string strSql = string.Format(@"select distinct bigclasscode,smallcallcode,smallname as smallname,code as typecode
                                            from (select distinct bigclasscode,smallcallcode,name as smallname,'0' as code
                                            from  dbo.s_smallclass_part with(nolock)
                                            union select distinct bigclasscode,smallcallcode,name as  smallname,'1' as code
                                            from  dbo.s_smallclass_event with(nolock) ) c
                                            where c.code = '{0}'
                                            order by smallcallcode asc ",typecode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSql).Tables[0];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
