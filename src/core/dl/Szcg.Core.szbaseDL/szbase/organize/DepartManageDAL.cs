using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using DBbase.szbase;
namespace bacgDL.szbase.organize
{
    public class DepartManageDAL : Teamax.Common.CommonDatabase
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
            string strSQL = string.Format(@"update p_depart set IsAcceptNote={0},Mobile='{1}' 
                                            where departcode={2}", IsAcceptNote, mobile, departcode);
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return 0;
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
                strSQL = string.Format(@"select departcode,departname,coalesce(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where coalesce(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,coalesce(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where coalesce(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ={1})
                                            order by departcode", areacode, departcode);
            try
            {
                return this.ExecuteDataset(strSQL);
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
        public DataTable GetDepartTable(string areacode, string departcode)
        {
            string strSQL = @"select * from p_depart where 1=1 ";
            if (!string.IsNullOrEmpty(areacode))
                strSQL += " and char(area)='" + areacode + "'";
            if (!string.IsNullOrEmpty(departcode))
                strSQL += " and char(departcode)='" + departcode + "'";

            DataSet ds = this.ExecuteDataset(strSQL);
            if (ds == null)
                return null;
            else
                return ds.Tables[0];

        }
        #endregion
        /// <summary>
        /// ���ݸ����ű�Ż���Ӳ���
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public DataTable GetChildDepart(string departcode)
        {
            string strSQL = @"select * from p_depart where parentcode=" + departcode + "and (isdel<>1 or isnull(isdel,'0')=0)";
            DataSet ds = this.ExecuteDataset(strSQL);
            if (ds == null)
                return null;
            else
                return ds.Tables[0];
        }

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
            if (departcode == "0" || userlevel == "9")//�����û���½��ʱ������ʻ��ȼ�Ϊϵͳ����Ա�������Կ���ȫ���Ĳ�����Ϣ
                strSQL = string.Format(@"select departcode,departname,coalesce(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where coalesce(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
            else
                strSQL = string.Format(@"   select departcode,departname,coalesce(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where coalesce(IsDel,0) <> 1 
	                                                and area like '{0}%' and  UserDefinedCode like 
                                         (select (UserDefinedCode+'%') as  UserDefinedCode from p_depart where departcode ={1})
                                            order by departcode", areacode, departcode);
            try
            {
                IDataReader modelDr = this.ExecuteReader(strSQL) as IDataReader;
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
                                        where departcode = {0}", departcode);
            try
            {
                    if (departcode != "0")
                    {
                        ds = this.ExecuteDataset(strSQL);
                        if (ds.Tables[0].Rows[0][0] != null)
                            return ds.Tables[0].Rows[0][0].ToString();
                        else return null;
                        //return dl.ExecScalarSql(strSQL).ToString();
                    }
                    else
                        return "0";
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
                                        where area = '{0}' and parentcode= 1", arecode);
            try
            {
                return this.ExecuteScalar(strSQL).ToString();
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
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("id", departID) };
            try
            {
                return ExecuteDataset("pr_p_GetDepartInfo", CommandType.StoredProcedure, arrSP);
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
            SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("id", UserDefinedCode) };
            try
            {
                return ExecuteDataset("pr_p_GetDepartInfoTWO", CommandType.StoredProcedure, arrSP);
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
                                            where coalesce(IsDel,0)<>1 and parentcode={0} and departname='{1}'", pId, departName);
            try
            {
                return Convert.ToInt32(this.ExecuteScalar(strSQL)) > 0;
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
                                            where coalesce(IsDel,0) <> 1 and UserDefinedCode='{0}'", departUserCode);
            try
            {
                return Convert.ToInt32(this.ExecuteScalar(strSQL));
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
        /// <returns></returns>
        public int InsertDepart(int pId, string departName, string address,
                                    string area, string memo, string IsDuty, string Mobile,
                                    string tel, string principal, string UserDefinedCode, int max_notenum, string IsAcceptNote,
                                    string issj, string SJ_RoleCode, string NoAppraise,
                                    ref string strErr)
        {
            string _IsDuty=Convert.ToInt32(IsDuty).ToString();
            string _IsAcceptNote=Convert.ToInt32(Convert.ToBoolean(IsAcceptNote)).ToString();
            string _issj=Convert.ToInt32(Convert.ToBoolean(issj)).ToString();
            string _NoAppraise=Convert.ToInt32(Convert.ToBoolean(NoAppraise)).ToString();

            SqlParameter[] arrSP = new SqlParameter[] {
                                    new SqlParameter("departname",departName), 
                                    new SqlParameter("parentcode", pId),
                                    new SqlParameter("area",area),
                                    new SqlParameter("principal",principal),
                                    new SqlParameter("Mobile",Mobile),
                                    new SqlParameter("Tel",tel),
                                    new SqlParameter("departadress",address),
                                    new SqlParameter("memo",memo),
                                    new SqlParameter("IsDuty",_IsDuty),
                                    new SqlParameter("UserDefinedCode",UserDefinedCode),
                                    new SqlParameter("IsAcceptNote",_IsAcceptNote),
                                    new SqlParameter("max_notenum",max_notenum),
                                    new SqlParameter("issj",_issj),
                                    new SqlParameter("SJ_RoleCode",SJ_RoleCode),
                                    new SqlParameter("NoAppraise",_NoAppraise)
            };
            try
            {
                return this.ExecuteNonQuery("pr_p_InsertDepartInfo", CommandType.StoredProcedure, arrSP);
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
        /// <returns></returns>
        public int UpdateDepart(string userDefinedCode, string departname, string parentcode,
                                    string area, string principal, string Mobile,
                                    string Tel, string departaddress, string memo, string IsDuty,
                                    int max_notenum, string IsAcceptNote, string issj,
                                    string SJ_ROLECODE, string NOAPPRAISE,
                                    string departcode, int isChange, ref string strErr)
        {
            string strPDefinedCode = string.Format(@"select UserDefinedCode from p_depart
                                                     where departcode={0}", parentcode);
            try
            {
                object obj = this.ExecuteScalar(strPDefinedCode);
                string pCode = obj == null ? "" : obj.ToString();
                if (pCode.Length > 0 && isChange == 1)
                {
                    string subCode = userDefinedCode.Substring(userDefinedCode.Length - 3);
                    userDefinedCode = pCode + subCode;
                    string checkUserDefinedCode = string.Format(@"select count(*) from p_depart where UserDefinedCode = '{0}'", userDefinedCode);
                    //�ж���Ϻ���û������Ƿ������ݿ������
                    object obj1 = this.ExecuteScalar(checkUserDefinedCode);
                    string flag = obj1 == null ? "" : obj1.ToString();
                    if (flag != "" && flag != "0")
                    {
                        return -1;
                    }
                }
                string _IsDuty = Convert.ToInt32(IsDuty).ToString();
                string _IsAcceptNote = Convert.ToInt32(Convert.ToBoolean(IsAcceptNote)).ToString();
                string _issj = Convert.ToInt32(Convert.ToBoolean(issj)).ToString();
                string _NoAppraise = Convert.ToInt32(Convert.ToBoolean(NOAPPRAISE)).ToString();
   
                string strSQL = string.Format(@"update p_depart set UserDefinedCode='{0}',departname='{1}',parentcode={2},
                                                                    area='{3}',principal='{4}',Mobile ='{5}',
                                                                    Tel = '{6}',departadress='{7}',memo='{8}', IsDuty='{9}',IsAcceptNote={10}, max_notenum={11},issj ={12} ,SJ_ROLECODE={13} ,NOAPPRAISE={14}  
                                            where departcode={15}", userDefinedCode, departname, parentcode, area, principal, Mobile, Tel, departaddress, memo, _IsDuty, _IsAcceptNote, max_notenum, _issj, 0, _NoAppraise, departcode);
                return this.ExecuteNonQuery(strSQL);
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
        public DataSet GetsjDutyDepart(string strDepartName, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string sql = string.Format(@"select rolecode,  rolename||','||coalesce(Memo,'') as rolename
                                            from p_role_sj 
                                            where rolename like '%{0}%'  
                                            order by rolename ", strDepartName);
                return this.ExecuteDataset(sql);
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
                                new SqlParameter("id",departId), 
                                new SqlParameter("result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                ExecuteDataset("pr_p_DeleteFromDepart", CommandType.StoredProcedure, arrSP);
                flag = Convert.ToInt32(arrSP[1].Value);
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
                                            where coalesce(a.departcode,0) = {0}", id);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                DataSet ds = this.ExecuteDataset(strSQL);
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
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public DataSet GetAreaList(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"select char(areacode) as streetcode, areaname as streetname, 1 id from s_area 
                                            union
                                            select char(streetcode) as streetcode,char(streetname) as streetname, id
                                            from s_street
                                            where char(streetcode) like '{0}%' order by id", areacode);
            try
            {
               return this.ExecuteDataset(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        #region GetUserDefineCode1�����ݸ������ű����ȡ�������Ӽ��Զ������
        /// <summary>
        /// ���ݸ������ű����ȡ�������Ӽ��Զ������
        /// </summary>
        /// <param name="pid">�������ű���</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public ArrayList GetUserDefineCode1(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select b.UserDefinedCode,a.UserDefinedCode as ParentUserDefineCode
                                            from p_depart a left join p_depart b
                                            on a.departcode = b.parentCode
                                            where coalesce(a.IsDel,0) <> 1 and a.departcode={0}", pid);
            ArrayList arrList = new ArrayList();
            int flag = 0;
            try
            {
                DataSet ds = this.ExecuteDataset(strSQL);
                if (ds!=null && ds.Tables.Count > 0)
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
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        #region GetUserDefineCode2����ȡһ�����ű���
        /// <summary>
        /// ��ȡһ�����ű���
        /// </summary>
        /// <param name="pid">�������ű���</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public ArrayList GetUserDefineCode2(string pid, ref string strErr)
        {
            string strSQL = string.Format(@"select UserDefinedCode from p_depart
                                            where parentcode={0}", pid);
            ArrayList arrList = new ArrayList();
            try
            {
                DataSet ds = this.ExecuteDataset(strSQL);
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
                                            out int returnRecordCount, int userId, string name,
                                            string loginname, string departname, string Order, string Field, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("ParentCode",departID), 
                                new SqlParameter("name",name), 
                                new SqlParameter("loginname",loginname),
                                new SqlParameter("departname",departname),
                                new SqlParameter("CurrentPage", pageIndex), 
                                new SqlParameter("RowCount",SqlDbType.Int), 
                                new SqlParameter("PageCount", SqlDbType.Int), 
                                new SqlParameter("PageSize", pageSize), 
                                new SqlParameter("Order", Order), 
                                new SqlParameter("Field", Field)};
            try
            {
                //arrSP[5].DB2TypeOutput = true;
                DataSet ds= ExecuteDataset("pr_p_GetDepartTree", CommandType.StoredProcedure, arrSP);
                if (!string.IsNullOrEmpty(arrSP[5].Value.ToString()))
                    returnRecordCount = Convert.ToInt32(arrSP[5].Value);
                else
                    returnRecordCount = 0;
                return ds;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                returnRecordCount = 0;
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
            string sql = "select coalesce(Max(" + keyvalue + "),'1000') as " + keyvalue + " from " + tablename + " where 1=1 ";

            if (columname != null && columname != "" && datavalue != null && datavalue != "")
                sql = sql + " and " + columname + "='" + datavalue + "'";

            IDataReader rs = ExecuteReader(sql);
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

        //���ݲ���IDȡ�����������Ϣ����װ��list
        public ArrayList getDeptInfo(string departcode, string columname)
        {
            string strSQL = string.Format(@"select * from p_depart
                                            where departcode={0}", departcode);
            ArrayList arrList = new ArrayList();
            try
            {
                DataSet ds = this.ExecuteDataset(strSQL);
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
            catch (Exception ex)
            {
                //strErr = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
