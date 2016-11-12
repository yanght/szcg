/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;�����Žṹ������
 * �ṹ��ɣ�
 * ��    �ߣ�����Ⱥ
 * �������ڣ�2007-05-25
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;
using Teamax.Common;
using bacgDL.szbase.organize;

namespace bacgBL.web.szbase.organize
{
    public class DepartManage
    {
        //��"null"ת��Ϊ""
        private string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }

        #region UpdateIsAcceptNote�����ݲ��ű�����²����Ƿ���ܶ���
        /// <summary>
        ///�����û���������û���Ϣ
        /// </summary>
        /// <param name="IsAcceptNote">�Ƿ���ܶ���</param>
        /// <param name="departcode">���ű���</param>
        public int UpdateIsAcceptNote(string IsAcceptNote, string mobile, int departcode, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"update p_depart set IsAcceptNote='{0}',Mobile='{1}' 
                                            where departcode='{2}'", IsAcceptNote, mobile, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return 0;
            }
        }
        #endregion

        #region GetAreaList����ȡ�����б�
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public DataSet GetAreaList(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"
                                            select areacode,areaname
                                            from s_area
                                            where areacode like '{0}%' ", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartList����ȡ�����б�
        /// <summary>
        /// ��ȡ����ĳ�����ڵ��µĲ��ŵ��б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="pid">���ڵ�</param>
        /// <returns></returns>
        public DataSet GetDepartList(string areacode, string departcode, ref string strErr)
        {
            string strSQL = "";
            if (departcode == "0")//�����û���½��ʱ�򣬿��Կ���ȫ���Ĳ�����Ϣ
                strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ='{1}')
                                            order by departcode", areacode, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartTable����ȡ�����б�
        /// <summary>
        /// ��ȡ����ĳ�����ڵ��µĲ��ŵ��б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="pid">���ڵ�</param>
        /// <returns></returns>
        public DataTable GetDepartTable(string areacode, string departcode)
        {
            DepartManageDAL dal = new DepartManageDAL();
            return dal.GetDepartTable(areacode, departcode);
        }
        /// <summary>
        /// ���ݸ����ű�Ż���Ӳ���
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public DataTable GetChildDepart(string departcode)
        {
            DepartManageDAL dal = new DepartManageDAL();
            return dal.GetChildDepart(departcode);
        }
        #endregion

        #region GetDepartList����ȡ�����б�רΪ��֯����
        /// <summary>
        /// ��ȡ����ĳ�����ڵ��µĲ��ŵ��б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <param name="pid">���ڵ�</param>
        /// <returns></returns>
        public ArrayList GetDepartListforTree(string areacode, string departcode, string userlevel, ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            string strSQL = "";
            if (departcode == "0" || userlevel == "9")//�����û���½��ʱ������ʻ��ȼ�Ϊϵͳ����Ա�������Կ���ȫ���Ĳ�����Ϣdutyid departcode
                strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by isnull(dutyid,999)", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ='{1}')
                                            order by isnull(dutyid,999)", areacode, departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader modelDr = dl.ExecReaderSql(strSQL);
                    while (modelDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelDr["parentcode"].ToString();
                        ts.code = modelDr["departcode"].ToString();
                        ts.text = modelDr["departname"].ToString();
                        ts.tag = modelDr["area"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelDr.Close();
                    return treeStructList;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetParentCode�����ݲ��ű��룬��ȡ�ò��ŵĸ����ڵ�
        /// <summary>
        /// ���ݲ��ű��룬��ȡ�ò��ŵĸ����ڵ�
        /// </summary>
        /// <param name="departcode">���ű���</param>
        /// <returns></returns>
        public string GetParentCode(string departcode, ref string strErr)
        {
            DataSet ds;
            string strSQL = string.Format(@"select parentcode from p_depart
                                        where departcode = '{0}'", departcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (departcode != "0")
                    {
                        ds = dl.ExecDatasetSql(strSQL);
                        if (ds.Tables[0].Rows[0][0] != null)
                            return ds.Tables[0].Rows[0][0].ToString();
                        else return null;
                        //return dl.ExecScalarSql(strSQL).ToString();
                    }
                    else
                        return "0";
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetStreetDepartCode�����ݵ����ţ���ȡ�õ������߻���
        /// <summary>
        /// ���ݵ����ţ���ȡ�õ������߻���,��Ҫ����ͨ��������,��ȡ��ֵ��Ķ�������(Ϊ�ֵ��������Ա����)
        /// </summary>
        /// <param name="arecode">�������</param>
        /// <returns></returns>
        public string GetStreetDepartCode(string arecode, ref string strErr)
        {

            string strSQL = string.Format(@"select departcode from p_depart
                                        where area = '{0}' and parentcode='0'", arecode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecScalarSql(strSQL).ToString();
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartInfo����ȡ��ѡ���Ĳ�����Ϣ
        /// <summary>
        /// ��ȡ��ѡ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="departID">���ű��</param>
        /// <returns></returns>
        public DataSet GetDepartInfo(int departID, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", departID) };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDepartInfo����ȡ��ѡ���Ĳ�����Ϣ
        /// <summary>
        /// ��ȡ��ѡ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="departID">���ű��</param>
        /// <returns></returns>
        public DataSet GetDepartInfoTWO(string UserDefinedCode, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@id", UserDefinedCode) };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecProc("pr_p_GetDepartInfoTWO", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region CheckDepartName���жϲ��������Ƿ����
        /// <summary>
        /// ��ȡ��ѡ���Ĳ�����Ϣ
        /// </summary>
        /// <param name="departID">���ű��</param>
        /// <returns></returns>
        public bool CheckDepartName(string pId, string departName, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from p_depart 
                                            where isnull(IsDel,0)<>1 and parentcode='{0}' and departname='{1}'", pId, departName);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (Convert.ToInt32(dl.ExecuteScalar(strSQL)) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }
        #endregion

        #region CheckUserDepartCode���жϲ����Զ�������Ƿ����
        /// <summary>
        /// �жϲ����Զ�������Ƿ����
        /// </summary>
        /// <param name="departUserCode">�û��Զ������</param>
        /// <returns></returns>
        public int CheckUserDepartCode(string departUserCode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from p_depart 
                                            where isnull(IsDel,0) <> 1 and UserDefinedCode='{0}'", departUserCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecuteScalar(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertDepart�����벿����Ϣ
        /// <summary>
        /// ���벿����Ϣ
        /// </summary>
        /// <param name="pId">������ID���</param>
        /// <param name="departName">��������</param>
        /// <param name="address">���ŵ�ַ</param>
        /// <param name="area">������������</param>
        /// <param name="memo">������Ϣ��ע</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param>
        /// <param name="txt_sort">���</param>
        /// <returns></returns>
        public int InsertDepart(int pId, string departName, string address,
                                    string area, string memo, string IsDuty, string Mobile,
                                    string tel, string principal, string UserDefinedCode, int max_notenum, string IsAcceptNote,
                                    string issj, string SJ_RoleCode, string NoAppraise,
                                    ref string strErr, string txt_sort)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                    new SqlParameter("@departname",departName), 
                                    new SqlParameter("@parentcode", pId),
                                    new SqlParameter("@area",area),
                                    new SqlParameter("@principal",principal),
                                    new SqlParameter("@Mobile",Mobile),
                                    new SqlParameter("@Tel",tel),
                                    new SqlParameter("@departadress",address),
                                    new SqlParameter("@memo",memo),
                                    new SqlParameter("@IsDuty",IsDuty),
                                    new SqlParameter("@UserDefinedCode",UserDefinedCode),
                                    new SqlParameter("@IsAcceptNote",IsAcceptNote),
                                    new SqlParameter("@max_notenum",max_notenum),
                                    new SqlParameter("@issj",issj),
                                    new SqlParameter("@SJ_RoleCode",SJ_RoleCode),
                                    new SqlParameter("@NoAppraise",NoAppraise),
                                    new SqlParameter("@dutyid",int.Parse(txt_sort))
            };
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQueryProc("pr_p_InsertDepartInfo", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpdateDepart�����²�����Ϣ
        /// <summary>
        /// ���²�����Ϣ
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
        /// <param name="txt_sort">����</param>
        /// <returns></returns>
        public int UpdateDepart(string userDefinedCode, string departname, string parentcode,
                                    string area, string principal, string Mobile,
                                    string Tel, string departaddress, string memo, string IsDuty,
                                    int max_notenum, string IsAcceptNote, string issj,
                                    string SJ_ROLECODE, string NOAPPRAISE,
                                    string departcode, int isChange, ref string strErr, string txt_sort)
        {
            string strPDefinedCode = string.Format(@"select UserDefinedCode from p_depart
                                                     where departcode='{0}'", parentcode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    object obj = dl.ExecScalarSql(strPDefinedCode);
                    string pCode = obj == null ? "" : obj.ToString();
                    if (pCode.Length > 0 && isChange == 1)
                    {
                        string subCode = userDefinedCode.Substring(userDefinedCode.Length - 3);
                        userDefinedCode = pCode + subCode;
                        string checkUserDefinedCode = string.Format(@"select count(*) from p_depart where UserDefinedCode = '{0}'", userDefinedCode);
                        //�ж���Ϻ���û������Ƿ������ݿ������
                        object obj1 = dl.ExecScalarSql(checkUserDefinedCode);
                        string flag = obj1 == null ? "" : obj1.ToString();
                        if (flag != "" && flag != "0")
                        {
                            return -1;
                        }
                    }
                    string strSQL = string.Format(@"update p_depart set UserDefinedCode='{0}',departname='{1}',parentcode='{2}',
                                                                    area='{3}',principal='{4}',Mobile ='{5}',
                                                                    Tel = '{6}',departadress='{7}',memo='{8}', IsDuty='{9}',IsAcceptNote='{10}', max_notenum='{11}',issj ='{12}' ,SJ_ROLECODE='{13}' ,NOAPPRAISE='{14}',dutyid='{15}'  
                                            where departcode={16}", userDefinedCode, departname, parentcode, area, principal, Mobile, Tel, departaddress, memo, IsDuty, IsAcceptNote, max_notenum, issj, SJ_ROLECODE, NOAPPRAISE, txt_sort, departcode);
                    return dl.ExecuteNonQuery(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion


        #region  GetsjDutyDepart��  ��ȡ�о����ε�λ
        /// <summary>
        ///   ��ȡ�о����ε�λ
        /// </summary>
        /// <param name="strDepartName">רҵ��������</param>
        /// <param name="ErrMsg">���ش�����Ϣ</param>
        /// <returns></returns>
        public static DataSet GetsjDutyDepart(string strDepartName, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string sql = string.Format(@"select rolecode, 
                                                    rolename+','+isnull(Memo,'') as rolename
                                            from dbo.p_role_sj 
                                            where rolename like '%{0}%'  
                                            order by rolename ", strDepartName);
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecuteDataset(sql);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        #endregion

        #region DeleteDepart��ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="departId">����Id����</param>
        /// <param name="flag">״̬��Ϣ</param>0��ɾ�������쳣1����ʶɾ���ɹ�2�����Ŵ�����Ա��ɾ��ʧ��3�����Ŵ����Ӳ��ţ�ɾ��ʧ��
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public void DeleteDepart(string departId, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",departId), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromDepart", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
                //return null;
            }
        }
        #endregion

        #region GetUserDefineCode�����ݲ��ű����ȡ�������Ӽ��Զ������
        /// <summary>
        /// ���ݲ��ű����ȡ�������Ӽ��Զ������
        /// </summary>
        /// <param name="id">���ű���</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public ArrayList GetUserDefineCode(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select a.UserDefinedCode,b.UserDefinedCode as ParentUserDefineCode 
                                            from p_depart a left join p_depart b
                                            on a.parentcode = b.departcode
                                            where isnull(a.departcode,0) = {0}", id);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (flag == 0)
                            {
                                arrList.Add(row["ParentUserDefineCode"]);
                                flag++;
                            }
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserDefineCode1�����ݸ������ű����ȡ�������Ӽ��Զ������
        /// <summary>
        /// ���ݸ������ű����ȡ�������Ӽ��Զ������
        /// </summary>
        /// <param name="pid">�������ű���</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param>pr_p_GetDepartTree 
        /// <returns></returns>
        public ArrayList GetUserDefineCode1(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select b.UserDefinedCode,a.UserDefinedCode as ParentUserDefineCode
                                            from p_depart a left join p_depart b
                                            on a.departcode = b.parentCode
                                            where isnull(a.IsDel,0) <> 1 and a.departcode={0}", pid);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (flag == 0)
                            {
                                arrList.Add(row["ParentUserDefineCode"]);
                                flag++;
                            }
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserDefineCode2����ȡһ�����ű���
        /// <summary>
        /// ��ȡһ�����ű���
        /// </summary>
        /// <param name="pid">�������ű���</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param>pr_p_GetDepartTree 
        /// <returns></returns>
        public ArrayList GetUserDefineCode2(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select UserDefinedCode from p_depart
                                            where parentcode='{0}'", pid);
            ArrayList arrList = new ArrayList();
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            arrList.Add(row["UserDefinedCode"]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetUserByDeptID�����ݲ��ű�Ż�ȡ�Ĳ����±ߵ���Ա
        /// <summary>
        /// ���ݲ��ű�Ż�ȡ�Ĳ����±ߵ���Ա
        /// </summary>
        /// <param name="departID">���ű��</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="returnRecordCount">���ؼ�¼��</param>
        /// <param name="userId">�û����</param>
        /// <param name="name">�û�����</param>
        /// <param name="loginname">��½����</param>
        /// <param name="departname">��������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public DataSet GetUserByDeptID(int departID, int pageIndex, int pageSize,
                                            int returnRecordCount, int userId, string name,
                                            string loginname, string departname, string Order, string Field, ref string strErr)
        {
           	SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@parentCode",departID), 
                                new SqlParameter("@PageIndex",pageIndex),
                                new SqlParameter("@PageSize",pageSize), 
                                new SqlParameter("@ReturnRecordCount",returnRecordCount),
                                new SqlParameter("@name",name), 
                                new SqlParameter("@loginname",loginname),
                                new SqlParameter("@departname",departname),
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    DataSet  ds = dl.ExecProc("pr_p_GetDepartTree", ref arrSP);
                                   

                    return ds;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        ////��"null"ת��Ϊ""
        //public string changeNull(string str)
        //{
        //    if (str.ToLower().Equals("null"))
        //        str = "";
        //    return str;
        //}

        //string������������1��������������������4λ��
        public string increase(string keyvalue)
        {   //1017010003
            string _result = Convert.ToString(keyvalue);
            if (_result.Length < 4)
            {
                int count = 4 - _result.Length;
                for (int i = 1; i <= count; i++)
                {
                    _result = "0" + _result;
                }
            }
            else
            {
                //���һ��Ĳ���ʱ����ȡ��4λת��Ϊ���֣���������1��
                string m1 = keyvalue.Substring(keyvalue.Length - 4, 4);
                int len = Convert.ToInt32(m1) + 1;
                keyvalue = keyvalue.Substring(0, keyvalue.Length - 4);

                //����ȡ��4λ�ĵ�һλΪ0ʱ����������
                if (m1.Substring(0, 1).Equals("0"))
                {
                    int length = 4 - len.ToString().Length;
                    for (int j = 0; j < length; j++)
                    {
                        keyvalue = keyvalue + "0";
                    }
                }
                _result = keyvalue + len.ToString();
            }
            return _result;
        }

        //ȡ��ǰ�����ֵ
        public int selectMaxValue(string tablename, string keyvalue, string columname, string datavalue)
        {
            string sql = "select isnull(Max(" + keyvalue + "),0) as " + keyvalue + " from " + tablename + " where 1=1 ";

            if (columname != null && columname != "" && datavalue != null && datavalue != "")
                sql = sql + " and " + columname + "='" + datavalue + "'";

            using (CommonDatabase DAO = new CommonDatabase())
            {
                IDataReader rs = DAO.ExecuteReader(sql);
                int result = 0;
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = Convert.ToInt32(rs[keyvalue].ToString());
                    }
                }
                rs.Close();
                return result;
            }
        }

        //���ݲ���IDȡ�����������Ϣ����װ��list
        public ArrayList getDeptInfo(string departcode, string columname)
        {
            string strSQL = string.Format(@"select * from p_depart
                                            where departcode='{0}'", departcode);
            ArrayList arrList = new ArrayList();
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    DataSet ds = dl.ExecDatasetSql(strSQL);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            arrList.Add(row[columname]);
                        }
                        return arrList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //strErr = ex.Message;
                return null;
            }
        }
    }
}
