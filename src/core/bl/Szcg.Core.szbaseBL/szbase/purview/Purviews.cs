using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.szbase.organize;
using bacgBL.web.szbase.entity;

namespace bacgBL.web.szbase.purview
{
    public class Purviews
    {
        #region GetModelTreeList����ȡģ���б�����ģ�飬��ģ�飩
        /// <summary>
        /// ��ȡģ���б�����ģ�飬��ģ�飩
        /// </summary>
        /// <returns></returns>
        public ArrayList GetModelTreeList(ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            //��ȡģ�飬��ģ��
            string modelSQL = @"select isnull(parentcode,0) as parentcode,modelcode as nodecode,modelname as nodename,
		                                    memo
                                 from p_model where isnull(del,'')<>1";
            //��ȡ��ģ���µİ�ť�б�
            string modelPowerSQL = @"select modelcode as parentcode,ButtonCode as nodecode,ButtonName as nodename,
		                                memo as memo
                               from p_model_power where isnull(del,'')<>1";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    SqlDataReader modelDr = dl.ExecReaderSql(modelSQL);
                    while (modelDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelDr["parentcode"].ToString();
                        ts.code = modelDr["nodecode"].ToString();
                        ts.text = modelDr["nodename"].ToString();
                        ts.tag = modelDr["memo"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelDr.Close();

                    SqlDataReader modelPowerDr = dl.ExecReaderSql(modelPowerSQL);
                    while (modelPowerDr.Read())
                    {
                        TreeSuruct ts;
                        ts.pcode = modelPowerDr["parentcode"].ToString();
                        ts.code = modelPowerDr["nodecode"].ToString();
                        ts.text = modelPowerDr["nodename"].ToString();
                        ts.tag = modelPowerDr["memo"].ToString();
                        treeStructList.Add(ts);
                    }
                    modelPowerDr.Close();

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

        #region GetPruviewModleCode:���ݽ�ɫIDȡ��ģ��ID�������ã���Ӧ��Ȩ��CheckBox��CheckBox.Checked=true
        /// <summary>
        /// ���ݽ�ɫIDȡ��ģ��ID�������ã���Ӧ��Ȩ��CheckBox��CheckBox.Checked=true
        /// </summary>
        /// <param name="id">��ɫId</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public ArrayList GetPruviewModleCode(string id,ref string strErr)
        {
            string strSQL = string.Format(@"select ButtonCode 
                                         from p_role_modelpower
                                         where rolecode = '{0}'", id);
            using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
            {
                SqlDataReader dr = dl.ExecReaderSql(strSQL);
                ArrayList list = new ArrayList();
                string modelcode = "";
                while (dr.Read())
                {
                    modelcode = Convert.ToString(dr["ButtonCode"]);
                    list.Add(modelcode);
                }
                dr.Close();
                return list;
            }
        }
        #endregion

        #region SavePurview:�����û����õ�Ȩ��
        /// <summary>
        /// �����û����õ�Ȩ��
        /// </summary>
        /// <param name="havaIds">�Ѿ����е�Ȩ��</param>
        /// <param name="noIds">�����ӵ�Ȩ��</param>
        /// <param name="rolecode">��ɫ����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int SavePurview(string haveIds, string noIds, string rolecode, ref string strErr)
        {

                SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@haveIds",haveIds),
                                                new SqlParameter("@noIds",noIds),
                                                new SqlParameter("@rolecode",rolecode),
                                                new SqlParameter("@result",SqlDbType.Int)};
                arrSP[3].Direction = ParameterDirection.Output;
                try
                {
                    using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                    {
                        dl.ExecNonQueryProc("pr_p_EditPurview", ref arrSP);
                        return Convert.ToInt32(arrSP[3].Value);
                    }
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    return 0;
                }
         }
         #endregion

         #region SavePurview:�����û����õ�Ȩ��
         /// <summary>
         /// �����û����õ�Ȩ��
         /// </summary>
         /// <param name="havaIds">�Ѿ����е�Ȩ��</param>
         /// <param name="noIds">�����ӵ�Ȩ��</param>
         /// <param name="rolecode">��ɫ����</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int SavePurview(string nodeIds, string rolecode, ref string strErr)
         {

             SqlParameter[] arrSP = new SqlParameter[]{
                                                new SqlParameter("@changeIDs",nodeIds),
                                                new SqlParameter("@roleID",rolecode),
                                                new SqlParameter("@res",SqlDbType.Int)};
             arrSP[2].Direction = ParameterDirection.Output;
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     dl.ExecNonQueryProc("pr_p_UpdatePurview", ref arrSP);
                     return Convert.ToInt32(arrSP[2].Value);
                 }
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
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
         public ArrayList GetDepartList(string areacode,string departcode,ref string strErr)
         {
             ArrayList array = new ArrayList();
             string strSQL = "";
             if (departcode == "0")//�����û���½��ʱ�򣬿��Կ���ȫ���Ĳ�����Ϣ
                 strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%' and isnull(NoAppraise,0)=0
                                            order by departcode", areacode);
             else
                 strSQL = string.Format(@"   select departcode,departname,isnull(parentcode ,0) as parentcode,area 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
                                            and isnull(NoAppraise,0)=0
	                                                and area like '{0}%'
	                                                and (departcode = '{1}' or parentcode='{1}') 
                                            order by departcode", areacode, departcode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader dr = dl.ExecReaderSql(strSQL);
                     while (dr.Read())
                     {
                         string[] dept = new string[3];
                         dept[0] = dr["departcode"].ToString();
                         dept[1] = dr["departname"].ToString();
                         dept[2] = dr["parentcode"].ToString();
                         array.Add(dept);
                     }
                     dr.Close();
                     return array;
                 }
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

        #region GetUsers:���ݲ��ű�ţ���ȡ�ò��ŵ��û��б�
        /// <summary>
        /// 
        /// </summary>
         /// <param name="departId"></param>
        /// <returns></returns>
        public ArrayList GetUsers(int departId,ref string strErr)
         {
             ArrayList array = new ArrayList();
             string strSQL = string.Format(@"   select usercode,username,departcode,mobile 
                                                from p_user
                                                where departcode = '{0}'", departId);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader dr = dl.ExecReaderSql(strSQL);
                     while (dr.Read())
                     {
                         string[] user = new string[4];
                         user[0] = dr["usercode"].ToString();
                         user[1] = dr["username"].ToString();
                         user[2] = dr["departcode"].ToString();
                         user[3] = dr["mobile"].ToString();
                         array.Add(user);
                     }
                     dr.Close();
                     return array;
                }
            }
            catch (Exception ex)
            {
                 strErr = ex.Message;
                 return null;
            }
         }
        #endregion

        #region GetUserTreeList����ȡ��Ա����Ϣ�����ţ���Ա��
         /// <summary>
         /// ��ȡ��Ա����Ϣ�����ţ���Ա��
         /// </summary>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
        public ArrayList GetUserTreeList(string areacode,string departcode,ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
             string departSQL = "";
             if (departcode == "0")//�����û���½��ʱ�򣬿��Կ���ȫ���Ĳ�����Ϣ
                 departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%'
                                            order by departcode", areacode);
             else
                 departSQL = string.Format(@"  select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 
	                                                and area like '{0}%'
	                                                and (departcode = '{1}' or parentcode='{1}') 
                                            order by departcode", areacode, departcode);
             string userSQL = string.Format(@"  select departcode as parentcode,usercode as nodecode,UserName as nodename,
			                                            memo as memo
                                                from p_user
                                                where AreaCode like '{0}%' and isnull(IsDel,0)=0",areacode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     SqlDataReader departDr = dl.ExecReaderSql(departSQL);
                     while (departDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = departDr["parentcode"].ToString();
                         ts.code = departDr["nodecode"].ToString();
                         ts.text = departDr["nodename"].ToString();
                         ts.tag = departDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     departDr.Close();

                     SqlDataReader userDr = dl.ExecReaderSql(userSQL);
                     while (userDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = userDr["parentcode"].ToString();
                         ts.code = userDr["nodecode"].ToString()+"aaaa";//Ϊ�˱�����������ź��û�id���������.
                         ts.text = userDr["nodename"].ToString()+"����Ա��";
                         ts.tag = userDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     userDr.Close();

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

         #region GetUserPhoneTreeList����ȡ��Ա����Ϣ�����ţ���Ա�绰���룩
         /// <summary>
         /// ��ȡ��Ա����Ϣ�����ţ���Ա��
         /// </summary>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
        public ArrayList GetUserPhoneTreeList(string areacode, ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
             string departSQL = string.Format(@"select isnull(parentcode ,0) as parentcode,departcode as nodecode,departname as nodename,
				                                        memo as memo 
                                                from p_depart
                                                where area like '{0}%' and isnull(IsDel,0)=0", areacode);
             string userSQL = string.Format(@"  select departcode as parentcode,mobile as nodecode,UserName as nodename,
			                                            memo as memo
                                                from p_user
                                                where AreaCode like '{0}%' and isnull(IsDel,0)=0", areacode);
             try
             {
                 using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                 {
                     IDataReader departDr = dl.ExecuteReader(departSQL);
                     while (departDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = departDr["parentcode"].ToString();
                         ts.code = departDr["nodecode"].ToString();
                         ts.text = departDr["nodename"].ToString();
                         ts.tag = departDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     departDr.Close();

                     string strMobile = "";
                     IDataReader userDr = dl.ExecuteReader(userSQL);
                     while (userDr.Read())
                     {
                         TreeSuruct ts;
                         ts.pcode = userDr["parentcode"].ToString();
                         strMobile = userDr["nodecode"].ToString();
                         ts.code = strMobile + "aaaa"; //Ϊ�˱�����������ź��û�id���������.
                         strMobile = strMobile.Trim() == "" ? "���ֻ�����" : strMobile;
                         ts.text = userDr["nodename"].ToString() + "��" + strMobile + "��";
                         ts.tag = userDr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     userDr.Close();

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

        #region GetRoleList:�����û����룬��ȡ���û������еĽ�ɫ
        /// <summary>
         /// �����û����룬��ȡ���û������еĽ�ɫ
        /// </summary>
        /// <param name="userCode">�û�����</param>
         /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public ArrayList GetRoleList(string userCode,ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"  select a.rolecode,b.rolename 
                                            from p_user_role a left join p_role b
                                            on a.rolecode = b.rolecode
                                            where a.usercode = '{0}'",userCode);
            string strRoleName = "";
            string strRoleCode = "";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        strRoleName += dr["rolename"].ToString() + ",";
                        strRoleCode += dr["rolecode"].ToString() + ",";
                    }
                    dr.Close();

                    if (strRoleName != "")
                    {
                        strRoleName = strRoleName.Substring(0, strRoleName.Length - 1);
                        list.Add(strRoleName);
                        strRoleCode = strRoleCode.Substring(0, strRoleCode.Length - 1);
                        list.Add(strRoleCode);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region SaveAccreditPurview:������Ȩ���û�Ȩ��
        /// <summary>
        /// ������Ȩ���û�Ȩ��
        /// </summary>
        /// <param name="consignerId">��Ȩ�˱���</param>
        /// <param name="accepterId">�����˱���</param>
        /// <param name="roleList">��ɫ�б�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int SaveAccreditPurview(string consignerId, string accepterId, string roleList,
                                    string startTime,string endTime,ref string strErr)
        {

            SqlParameter[] arrSP = new SqlParameter[]{
				                                new SqlParameter("@consignerId",consignerId),
                                                new SqlParameter("@accepterId",accepterId),
                                                new SqlParameter("@roleList",roleList),
                                                new SqlParameter("@startTime",startTime),
                                                new SqlParameter("@endTime",endTime)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    return dl.ExecNonQueryProc("pr_p_SaveAccreditPurview", ref arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetUserNameId:����ѡ���id,��ȡ������Ȩ�û����б�
        /// <summary>
        /// ����ѡ���id,��ȡ������Ȩ�û����б�
        /// û���õ��ĺ���.
        /// </summary>
        /// <param name="ids">ѡ��ı���</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public ArrayList GetUserNameId(string ids, ref string strErr)
        {
            string strNames = "";
            string strCodes = "";
            string departCodes = "";
            ArrayList arrList = new ArrayList();
            string[] strIds = ids.Split(',');
            for (int i = 0; i<strIds.Length; i++)
            {
                if (strIds[i].Substring(strIds[i].Length - 4) == "8888")
                {
                    strCodes += strIds[i].Substring(0, strIds[i].Length - 4) + ",";
                }
                else
                {
                    departCodes += strIds[i].Substring(0, strIds[i].Length - 4) + ",";
                }
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (strCodes.Length > 0)
                    {
                        strCodes = strCodes.Substring(0, strCodes.Length - 1);
                        string strSQL = string.Format(@"select username from p_user
                                                        where usercode = ({0})", strCodes);
                        SqlDataReader dr = dl.ExecReaderSql(strSQL);
                        while (dr.Read())
                        {
                            strNames += dr["username"].ToString() + ",";
                        }
                        dr.Close();
                    }
                    if (departCodes.Length > 0)
                    {
                        if (strCodes.Length > 0)
                            strCodes = strCodes + ",";
                        departCodes = departCodes.Substring(0, departCodes.Length - 1);
                        string strSQL1 = string.Format(@"select usercode,username from p_user
                                                            where departcode in ({0})",departCodes);
                        SqlDataReader dr1 = dl.ExecReaderSql(strSQL1);
                        while (dr1.Read())
                        {
                            strCodes += dr1["usercode"].ToString() + ",";
                            strNames += dr1["username"].ToString() + ",";
                        }
                        dr1.Close();
                    }
                    if (strCodes.Length > 0)
                        strCodes = strCodes.Substring(0, strCodes.Length - 1);
                    if (strNames.Length > 0)
                        strNames = strNames.Substring(0, strNames.Length - 1);
                    arrList.Add(strCodes);
                    arrList.Add(strNames);

                    return arrList;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetDutyDepartList����ȡרҵ�����б�
        /// <summary>
        /// ��ȡרҵ�����б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public ArrayList GetDutyDepartList(string areacode, ref string strErr)
        {
            ArrayList array = new ArrayList();
            string strSQL = string.Format(@"select departcode,departname,isnull(parentcode ,0) as parentcode
                                            from p_depart
                                            where isnull(IsDel,0) <> 1 and area like '{0}%' and isnull(IsDuty,0)=1", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] dept = new string[3];
                        dept[0] = dr["departcode"].ToString();
                        dept[1] = dr["departname"].ToString();
                        dept[2] = dr["parentcode"].ToString();
                        array.Add(dept);
                    }
                    dr.Close();
                    return array;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetRoleListByAreaCode�������������,��ȡ��ɫ
        /// <summary>
        /// ��ȡ��ɫ�б�
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public DataSet GetRoleListByAreaCode(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"select a.rolecode,a.rolename 
                                            from p_role a
                                            where isnull(a.IsDel,0)<>1 and a.areacode like '{0}%'",areacode);
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

        #region GetSelectedByIds�������û�ѡ������Ľڵ�,��ȡ�û�ѡ������нڵ�.(�ֲ���ص���)
        /// <summary>
        /// �����û�ѡ������Ľڵ�,��ȡ�û�ѡ������нڵ�.
        /// </summary>
        /// <param name="rolecode">�����û��Ľ�ɫ����</param>
        /// <param name="Ids">�û�ѡ������ڵ�</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public string GetSelectedByIds(string rolecode,string Ids, ref string strErr)
        {
            string strSQL = "";
            string retString = "";
            string temp="";
            string pIds = "";
            string[] arrIds = Ids.Split(',');
            for (int i = 0; i < arrIds.Length && Ids != ""; i++)
            {
                int flag = 0;
                temp = arrIds[i];
                for (int j = 0; j < arrIds.Length; j++)
                {
                    if (temp == arrIds[j])
                        continue;
                    if (temp.Length < arrIds[j].Length)
                    {
                        if (temp == arrIds[j].Substring(0, temp.Length))
                        {
                            retString += temp + ",";
                            flag = 1;
                            break;
                        }
                    }
                }
                if (flag != 1)
                    pIds += arrIds[i] + ",";
            }
            if (pIds.Length > 0)
                pIds = pIds.Substring(0, pIds.Length - 1);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    string[] arrpIds = pIds.Split(',');
                    if (arrpIds.Length > 0 && pIds!="")
                    {
                        for (int k = 0; k < arrpIds.Length; k++)
                        {
                            strSQL = string.Format(@"   select modelcode from p_model
                                                        where modelcode like '{0}%'",arrpIds[k]);
                            SqlDataReader modeldr = dl.ExecReaderSql(strSQL);
                            while (modeldr.Read())
                            {
                                retString += modeldr["modelcode"].ToString() + ",";
                            }
                            modeldr.Close();

                            strSQL = string.Format(@"   select ButtonCode from p_model_power
                                                        where ButtonCode like '{0}%'",arrpIds[k]);
                            SqlDataReader dr = dl.ExecReaderSql(strSQL);
                            while (dr.Read())
                            {
                                retString += dr["ButtonCode"].ToString()+",";
                            }
                            dr.Close();
                        }
                    }
                    if (retString.Length > 0)
                        retString = retString.Substring(0, retString.Length - 1);
                    return retString;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion
    }
}
