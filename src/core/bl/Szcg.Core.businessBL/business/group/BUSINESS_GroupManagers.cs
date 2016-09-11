using System;
using AjaxPro;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using bacgBL.business.group;
using DBbase.szbase;

namespace bacgBL.business.group
{
	/// <summary>
	/// BUSINESS_GroupManagers ��ժҪ˵����
	/// </summary>
	/// 
	public class BUSINESS_GroupManagers
    {
        #region �������
        //�õ����ݿ��еĽ����
		protected SqlDataReader dr;
		//�������
		protected StringBuilder sb;
		//�����������
		protected ArrayList list;
		public const string BUTTON_INSERT_1 = "1";
		public const string BUTTON_EDIT_2 = "2";
		private const string NAMESPACE_PATH = "com.teamax.business.groupmanager.";
        #endregion

        #region getPurviewTreeData:ȡ�����ݿ����ݣ�����alai_tree��
        /// <summary>
		/// ȡ�����ݿ����ݣ�����alai_tree��
		/// </summary>
		/// <param name="argUserCode">��½�û�Id</param>
		/// <returns></returns>
		public String[] getPurviewTreeData(int argUserCode)
		{
			StringBuilder strSQL;
			strSQL = new StringBuilder();
			strSQL.Append("	SELECT rolecode, ");
			strSQL.Append("			rolename ");
			strSQL.Append("	FROM role");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToDecimal(dr["rolecode"])+",");
					sb.Append(Convert.ToString(dr["rolename"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    argUserCode,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getPurviewTreeData");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region GetGroupTreeList:������νṹ����Ϣ
        /// <summary>
        /// ������νṹ����Ϣ
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="GroupType"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public ArrayList GetGroupTreeList(int usercode, bool GroupType, ref string strErr)
        {
            using (bacgDL.business.group.BUSINESS_GroupManagers dl = new bacgDL.business.group.BUSINESS_GroupManagers())
            {
                return dl.GetGroupTreeList(usercode, GroupType, ref strErr);
            }

        }
        #endregion

        #region GetGroupTreeList2:������νṹ����Ϣ
        /// <summary>
        /// ������νṹ����Ϣ
        /// </summary>
        /// <param name="usercode">�û�����</param>
        ///// <param name="GroupType">������</param>
        /// <param name="strErr">���󷵻���Ϣ</param>
        /// <returns></returns>
        public ArrayList GetGroupTreeList2(int usercode,  ref string strErr)
        { 
            ArrayList treeStructList = new ArrayList();
            string departSQL = string.Format(@" select	case when GroupType=0 and ParentGroupID=0 then 'a1'
                                                            when GroupType=1 and ParentGroupID=0 then 'a2'
                                                            else cast(parentgroupid as varchar(10)) 
                                                        end as pcode,
                                                        cast(id as varchar(10)) as code,
                                                        groupname as [text],
                                                        usercode as tag 
                                                from p_user_group 
                                                where GroupType=0 or usercode = '{0}'  
                                                union all 
                                                select '0'  ,'a1','����Ⱥ��','0'
                                                union all 
                                                select '0' ,'a2','����Ⱥ��','0'
                                ", usercode);

            string userSQL = string.Format(@"   select  a.fid as pcode, 
                                                        b.mobile as code,
	                                                    b.UserName as [text],
                                                        b.usercode as tag
                                                from p_user_group_detail a
                                                inner join p_user b 
                                                on a.usercode=b.usercode
                                                inner join p_user_group c
                                                on a.fid = c.id
                                                where GroupType=0 or c.usercode = '{0}'", usercode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    IDataReader departDr = dl.ExecuteReader(departSQL);
                    while (departDr.Read())
                    {
                        bacgBL.web.szbase.entity.TreeSuruct ts;
                        ts.pcode = departDr["pcode"].ToString();
                        ts.code = departDr["code"].ToString();
                        ts.text = departDr["text"].ToString();
                        ts.tag = departDr["tag"].ToString();
                        treeStructList.Add(ts);
                    }
                    departDr.Close();

                    string strMobile = "";
                    IDataReader userDr = dl.ExecuteReader(userSQL);
                    while (userDr.Read())
                    {
                        bacgBL.web.szbase.entity.TreeSuruct ts;
                        ts.pcode = userDr["pcode"].ToString();
                        ts.code = userDr["code"].ToString() + "aaaa";
                        //ts.text = userDr["text"].ToString() + "(" + userDr["code"].ToString() + ")";
                        strMobile = userDr["code"].ToString();
                        strMobile = strMobile.Trim() == "" ? "���ֻ�����" : strMobile;
                        ts.text = userDr["text"].ToString() + "��" + strMobile + "��";
                        ts.tag = userDr["tag"].ToString();
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

        #region GetGroupPersion:��ȡȺ����Ա
        /// <summary>
        /// ��ȡȺ����Ա
        /// </summary>
        /// <param name="id">Ⱥ��ID</param>
        /// <returns></returns>
        public ArrayList GetGroupPersion(string id, ref string strErr)
        {
            using (bacgDL.business.group.BUSINESS_GroupManagers dl = new bacgDL.business.group.BUSINESS_GroupManagers())
            {
                return dl.GetGroupPersion(id, ref strErr);
            }
        }
        #endregion

        #region getUser:���ݽ�ɫȡ�ý�ɫ��Ա�����뵽��ɫ�ڵ�����
        /// <summary>
		/// ���ݽ�ɫȡ�ý�ɫ��Ա�����뵽��ɫ�ڵ�����
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="rolecode">��ɫId</param>
		/// <returns></returns>
		public String[] getUser(int userId, string rolecode)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT A.username AS name, ");
			strSQL.Append("		A.usercode AS code ");
			strSQL.Append("	FROM loginuser AS A, ");
			strSQL.Append("		user_role AS B ");
			strSQL.Append("	WHERE A.usercode = B.usercode ");
			strSQL.Append("		AND B.rolecode = @rolecode ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				//command.Parameters.Add("@rolecode", rolecode);
                command.Parameters.AddWithValue("@rolecode", rolecode);
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["name"])+",");
					sb.Append(Convert.ToDecimal(dr["code"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getUser");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}

        }
        #endregion

        #region getGroupType:ȡ��Ⱥ������
        /// <summary>
		/// ȡ��Ⱥ������ 
		/// </summary>
		/// <param name="argUserCode">��½�û�Id</param>
		/// <returns>Ⱥ������</returns>
		public String[] getGroupType(int userId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT groupcode AS code, ");
			strSQL.Append("		groupname AS name ");
			strSQL.Append("	FROM szgroup ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["code"]) + ",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getGroupType");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region getGroupUser:ȡ��Ⱥ���û�
        /// <summary>
		/// ȡ��Ⱥ���û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argGroupId">Ⱥ��Id</param>
		/// <returns></returns>
		public String[] getGroupUser(int userId, string argGroupId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT B.id AS code, ");
			strSQL.Append("		B.groupname AS name ");
			strSQL.Append("	FROM szgroup AS A, ");
			strSQL.Append("		user_group AS B ");
			strSQL.Append("	WHERE A.groupcode = B.groupcode ");
			strSQL.Append("		AND A.groupcode = @groupcode ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				//command.Parameters.Add("@groupcode", argGroupId);
                command.Parameters.AddWithValue("@groupcode", argGroupId);
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["code"]) + ",");
					sb.Append(Convert.ToString(dr["name"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getGroupUser");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region getRoleData:ȡ��Ⱥ���û�
        /// <summary>
		/// ȡ��Ⱥ���û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <returns></returns>
		public DataSet getRoleData(int userId)
		{
            using (SqlConnection myConnection = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString)) 
			{
				SqlDataAdapter dad;
				DataSet dtst;
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append("	SELECT A.rolecode AS rolecode, ");
				strSQL.Append("		A.rolename AS rolename, ");
				strSQL.Append("		C.username AS username, ");
				strSQL.Append("		C.usercode AS usercode ");
				strSQL.Append("	FROM role AS A, ");
				strSQL.Append("		user_role AS B, ");
				strSQL.Append("		loginuser AS C ");
				strSQL.Append("	WHERE A.rolecode = B.rolecode ");
				strSQL.Append("		AND B.usercode = C.usercode ");
				strSQL.Append("	ORDER BY A.rolecode, B.usercode ");
				SqlCommand myCommand = new SqlCommand(strSQL.ToString(), myConnection);
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					myConnection.Open();
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblGroup"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
                    //BASE_logmanageservice.writeSystemLog(
                    //    userId,
                    //    "procedure:getRoleData",
                    //    System.DateTime.Today,
                    //    System.DateTime.Today, 
                    //    BASE_ModerId.getSystem_ZCPT(),
                    //    err.ToString(),
                    //    NAMESPACE_PATH + "getRoleData");
					throw;
				}
				finally
				{
					myCommand.Dispose();
					myConnection.Dispose();
				}
			}

        }
        #endregion

        #region delGroupUsers:����Ⱥ��Idȡ�������û�
        /// <summary>
		/// ����Ⱥ��Idȡ�������û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argGroupId">Ⱥ��Id</param>
		public void delGroupUsers(int userId, string argGroupId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	DELETE user_grouped ");
			strSQL.Append("	WHERE fid = @fid ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				//command.Parameters.Add("@fid", argGroupId);
                command.Parameters.AddWithValue("@fid", argGroupId);
				command.ExecuteNonQuery();
                //BASE_logmanageservice.writeUserLog(
                //    userId,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "delGroupUsers");
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    Convert.ToInt16(userId),
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "delGroupUsers");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region addUsered:����Ⱥ���û�
        /// <summary>
		/// ����Ⱥ���û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argFid">Ⱥ��Id</param>
		/// <param name="argUserCode">�û�Id</param>
		/// <param name="argUserName">�û�����</param>
		public void addUsered(int userId, string argFid, string argUserCode, string argUserName)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	INSERT user_grouped( ");
			strSQL.Append("			fid, ");
			strSQL.Append("			usercode, ");
			strSQL.Append("			username)");
			strSQL.Append("	VALUES( @fid, ");
			strSQL.Append("			@usercode, ");
			strSQL.Append("			@username) ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				command.Parameters.AddWithValue("@fid", argFid);
				command.Parameters.AddWithValue("@usercode", argUserCode);
				command.Parameters.AddWithValue("@username", argUserName);
				command.ExecuteNonQuery();
				
                //BASE_logmanageservice.writeUserLog(
                //    userId,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "addUsered");
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "addUsered");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }

        public void addUsered( string argFid, string argUserCode, string argUserName)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	INSERT user_grouped( ");
			strSQL.Append("			fid, ");
			strSQL.Append("			usercode, ");
			strSQL.Append("			username)");
			strSQL.Append("	VALUES( @fid, ");
			strSQL.Append("			@usercode, ");
			strSQL.Append("			@username) ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				command.Parameters.AddWithValue("@fid", argFid);
				command.Parameters.AddWithValue("@usercode", argUserCode);
                command.Parameters.AddWithValue("@username", argUserName);
				command.ExecuteNonQuery();
			}
			catch(Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    Convert.ToInt16(argUserCode),
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "addUsered");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}

        }
        #endregion

        #region DeleteGroupUsered:ɾ��Ⱥ���û�
        /// <summary>
		/// ɾ��Ⱥ���û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argGroupId">Ⱥ��Id</param>
		public void DeleteGroupUsered(SqlCommand command, int userId, string argGroupId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	DELETE user_grouped ");
			strSQL.Append("		WHERE fid  = @fid");
			try
			{
				command.CommandText = strSQL.ToString();
                command.Parameters.AddWithValue("@fid", argGroupId);
				command.ExecuteNonQuery();
                //BASE_logmanageservice.writeUserLog(
                //    userId,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "DeleteGroupUsered");
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "DeleteGroupUsered");
				throw;
			}
			finally
			{
				command.Dispose();
			}
        }
        #endregion

        #region DeleteGroupUser:ɾ��Ⱥ��
        /// <summary>
		/// ɾ��Ⱥ��
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argGroupId">Ⱥ��Id</param>
		public void DeleteGroupUser(SqlCommand command, int userId, string argGroupId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	DELETE user_group ");
			strSQL.Append("		WHERE id = @id");
			try
			{
				command.CommandText = strSQL.ToString();
                command.Parameters.AddWithValue("@id", argGroupId);
				command.ExecuteNonQuery();
                //BASE_logmanageservice.writeUserLog(
                //    userId,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "DeleteGroupUser");
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "DeleteGroupUser");
				throw;
			}
			finally
			{
				command.Dispose();
			}
        }

        public void DelGroupUser(int userId, string argGroupId)
		{
			SqlConnection conn = null;
			SqlTransaction myTrans = null;
			SqlCommand command = null;
			try
			{
                conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
				conn.Open();
				myTrans = conn.BeginTransaction();
				command = conn.CreateCommand();
				command.Transaction = myTrans;
				DeleteGroupUsered(command, userId, argGroupId);
				DeleteGroupUser(command, userId, argGroupId);
				myTrans.Commit();
			}
			catch
			{
				myTrans.Rollback();
				throw;
			}
			finally
			{
				conn.Dispose();
				myTrans.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region getGroupDataSource:ȡ��Ⱥ������
        /// <summary>
		/// ȡ��Ⱥ������
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <returns>Ⱥ������</returns>
		public ArrayList getGroupDataSource(int userId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT groupcode AS code, ");
			strSQL.Append("		groupname AS name ");
			strSQL.Append("	FROM szgroup");
			strSQL.Append("	WHERE groupname <> '����Ⱥ��'");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					list.Add(new USState(Convert.ToString(dr["name"]), Convert.ToString(dr["code"])));
				}
				dr.Close();
				return (ArrayList)(list);
			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getGroupDataSource");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}

        }
        #endregion

        #region addGroupUser:����Ⱥ���û�
        /// <summary>
        /// ����Ⱥ���û�
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argGroupTypeCode">Ⱥ������Id</param>
        /// <param name="argGroupName">Ⱥ����������</param>
        public void addGroupUser(int argUserCode, bool argGroupTypeCode, string argGroupName, string parentGroupID)
        {
            StringBuilder strSQL = new StringBuilder(); ;
            strSQL.Append("	INSERT INTO p_user_group( ");
            strSQL.Append("			usercode, ");
            strSQL.Append("			groupname, ");
            strSQL.Append("			ParentGroupID, ");
            strSQL.Append("			GroupType) ");
            strSQL.Append("		VALUES( ");
            strSQL.Append("			@usercode, ");
            strSQL.Append("			@groupname, ");
            strSQL.Append("			@ParentGroupID, ");
            strSQL.Append("			@GroupType) ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.Parameters.AddWithValue("@usercode", argUserCode);
                command.Parameters.AddWithValue("@groupname", argGroupName);
                command.Parameters.AddWithValue("@GroupType", argGroupTypeCode);
                command.Parameters.AddWithValue("@ParentGroupID", Convert.ToInt32(parentGroupID));
                command.ExecuteNonQuery();

                //BASE_logmanageservice.writeUserLog(
                //    argUserCode,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "addGroupUser");
            }
            catch (Exception err)
            {
                //BASE_logmanageservice.writeSystemLog(
                //    argUserCode,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "addGroupUser");
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                command.Dispose();
            }
        }
        #endregion

        #region editGroupUser:�޸�Ⱥ��
        /// <summary>
		/// �޸�Ⱥ��
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <param name="argId">Ⱥ���ӦId</param>
		/// <param name="argGroupName">Ⱥ������</param>
		public void editGroupUser(int userId, string argId, string argGroupName)
		{
			StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	UPDATE p_user_group ");
			strSQL.Append(" SET groupname = @groupname ");
			strSQL.Append("	WHERE id = @id ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
                command.Parameters.AddWithValue("@groupname", argGroupName);
                command.Parameters.AddWithValue("@id", argId);
				command.ExecuteNonQuery();

                //BASE_logmanageservice.writeUserLog(
                //    userId,
                //    System.DateTime.Today,
                //    System.DateTime.Today,
                //    BASE_ModerId.getModel_11106(),
                //    strSQL.ToString(),
                //    NAMESPACE_PATH + "editGroupUser");

			}
			catch (Exception err)
			{
                //BASE_logmanageservice.writeSystemLog(
                //    userId,
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "editGroupUser");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region getUsered:ȡ�ý�ɫ�û�
        /// <summary>
		/// ȡ�ý�ɫ�û�
		/// </summary>
		/// <param name="userId">��½�û�Id</param>
		/// <returns></returns>
		[AjaxMethod]
		public ArrayList getUsered(string userId, string argGroupId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT usercode AS code, "); 
			strSQL.Append("			username AS name ");
			strSQL.Append("	FROM user_grouped");
			strSQL.Append("	WHERE fid = @fid");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
                command.Parameters.AddWithValue("@fid", argGroupId);
				dr = command.ExecuteReader();
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToString(dr["name"]) + ",");
					sb.Append(Convert.ToString(dr["code"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (ArrayList)(list);
			}
			catch (Exception err)
			{	
                //BASE_logmanageservice.writeSystemLog(
                //    Convert.ToInt16(userId),
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getUsered");
				throw;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion

        #region getGroup:��ȡȺ��
        /// <summary>
        /// ��ȡȺ��
        /// </summary>
        /// <param name="groupid">Ⱥ��ID</param>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        [AjaxMethod]
		public string getGroup(string groupid, string userId)
		{
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append("	SELECT usercode AS code, "); 
			strSQL.Append("			username AS name ");
			strSQL.Append("	FROM user_grouped");
			strSQL.Append("	WHERE fid = @fid");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
			SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
			try
			{
				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
                command.Parameters.AddWithValue("@fid", groupid);
				SqlDataReader dr = command.ExecuteReader();
				System.Text.StringBuilder sb =null;
				//System.Collections.ArrayList list = new  System.Collections.ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
                    sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]));
					sb.Append("#");
					//list.Add(sb.ToString());
				}
				dr.Close();
				//return (ArrayList)(list);
				
				if(sb != null)
				{
					string ret = sb.ToString();
					return ret.Substring(0, ret.Length-1);
				}

				return "";
				
			}
			catch (Exception err)
			{	
                //BASE_logmanageservice.writeSystemLog(
                //    System.Convert.ToInt32(userId),
                //    strSQL.ToString(),
                //    System.DateTime.Today,
                //    System.DateTime.Today, 
                //    BASE_ModerId.getSystem_ZHYW(),
                //    err.ToString(),
                //    NAMESPACE_PATH + "getUsered");
				return "";
			}
			finally
			{
				conn.Close();
				conn.Dispose();
				command.Dispose();
			}
        }
        #endregion
    }

    #region USState
    /// <summary>
    /// USState
    /// </summary>
    public class USState
	{
		private string myShortName ;
		private string myLongName ;
    
		public  USState(string strLongName, string strShortName)
		{

			this.myShortName = strShortName;
			this.myLongName = strLongName;
		}

		public string ShortName
		{
			get
			{
				return myShortName;
			}
		}

		public string LongName
		{
        
			get
			{
				return myLongName ;
			}
		}

		public override string ToString()
		{
			return this.ShortName + " - " + this.LongName;
		}
    }
    #endregion
}
