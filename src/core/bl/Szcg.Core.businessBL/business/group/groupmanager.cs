using System;
using AjaxPro;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace bacgBL.business.group
{
    /// <summary>
    /// groupmanager ��ժҪ˵����
    /// </summary>
    public class groupmanager
    {
        //�õ����ݿ��еĽ����
        protected SqlDataReader dr;
        //�������
        protected StringBuilder sb;
        //�����������
        protected ArrayList list;
        public const string BUTTON_INSERT_1 = "1";
        public const string BUTTON_EDIT_2 = "2";
        private const string NAMESPACE_PATH = "com.teamax.business.groupmanager.";
        public groupmanager()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        [AjaxMethod]
        public string getUsered(string groupid, string userId)
        {
            if (groupid != null)
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("	SELECT A.usercode AS code, ");
                strSQL.Append("			B.username AS name ");
                strSQL.Append("	FROM p_user_group_detail as A,p_user as B");
                strSQL.Append("	WHERE fid = @fid and A.usercode=B.usercode");
                strSQL.Append("	ORDER BY name ");
                SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command.Parameters.AddWithValue("@fid", groupid);
                    SqlDataReader dr = command.ExecuteReader();
                    System.Text.StringBuilder sb = new StringBuilder();
                    while (dr.Read())
                    {
                        sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]) + "#");
                    }
                    dr.Close();
                    if (sb != null && sb.ToString().Length > 0)
                    {
                        string ret = sb.ToString();
                        return ret.Substring(0, ret.Length - 1);
                    }
                    else
                    {
                        return "";
                    }
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
            else
            {
                return "";
            }
        }

        #region InsertBusinessMsg:�½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        [AjaxMethod]
        public int InsertBusinessMsg(string gousercode, string tousercode, string msgtitle, string msgcontent)
        {
            return InsertBusinessMsgs(gousercode, tousercode, msgtitle, msgcontent, "", "");
        }

        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertBusinessMsgs(string gousercode, string tousercode, string msgtitle, string msgcontent, string refid, string msgtype)
        {
            try
            { 
                using (bacgDL.business.wdxx dl = new bacgDL.business.wdxx())
                {
                    return dl.InsertBusinessMsg(gousercode, tousercode, msgtitle, msgcontent, refid, msgtype);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region InsertGroupMsg:���뷢��Ⱥ����Ϣ
        /// <summary>
        /// ����Ⱥ����Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        [AjaxMethod]
        public int InsertGroupMsg(string gousercode, string tousercode, string msgtitle, string msgcontent)
        {
            return InsertGroupMsgs(gousercode, tousercode, msgtitle, msgcontent, "", "");
        }

        /// <summary>
        /// �½�ҵ��ƽ̨��Ϣ��ͬʱ����ҵ��ƽ̨�ظ���Ϣ
        /// </summary>
        /// <param name="gousercode">������(From)</param>
        /// <param name="tousercode">������(To)</param>
        /// <param name="msgtitle">��Ϣ����</param>
        /// <param name="msgcontent">��Ϣ����</param>
        public int InsertGroupMsgs(string gousercode, string tousercode, string msgtitle, string msgcontent, string refid, string msgtype)
        {
            try
            {
                using (bacgDL.business.wdxx dl = new bacgDL.business.wdxx())
                {
                    return dl.InsertGroupMsg(gousercode, tousercode, msgtitle, msgcontent, refid, msgtype);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// ȡ�����ݿ����ݣ�����alai_tree��
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns></returns>
        [AjaxMethod]
        public string getPurviewTreeData(string userId)
        {
            StringBuilder strSQL;
            strSQL = new StringBuilder();
            strSQL.Append("	SELECT rolecode AS code, ");
            strSQL.Append("			rolename AS name ");
            strSQL.Append("	FROM p_role where ISNULL(IsDel,0)=0 ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                dr = command.ExecuteReader();
                System.Text.StringBuilder sb = new StringBuilder();
                while (dr.Read())
                {
                    sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]) + "#");
                }
                dr.Close();
                if (sb != null && sb.ToString().Length > 0)
                {
                    string ret = sb.ToString();
                    return ret.Substring(0, ret.Length - 1);
                }
                else
                {
                    return "";
                }
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

        /// <summary>
        /// ���ݽ�ɫȡ�ý�ɫ��Ա�����뵽��ɫ�ڵ�����
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="rolecode">��ɫId</param>
        /// <returns></returns>
        [AjaxMethod]
        public string getUser(string userId, string rolecode)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	SELECT A.username AS name, ");
            strSQL.Append("		A.usercode AS code ");
            strSQL.Append("	FROM p_user AS A, ");
            strSQL.Append("		p_user_role AS B ");
            strSQL.Append("	WHERE A.usercode = B.usercode ");
            strSQL.Append("		AND B.rolecode = @rolecode ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.Parameters.AddWithValue("@rolecode", rolecode);
                dr = command.ExecuteReader();
                System.Text.StringBuilder sb = new StringBuilder();
                while (dr.Read())
                {
                    sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]) + "#");
                }
                dr.Close();
                if (sb != null && sb.ToString().Length > 0)
                {
                    string ret = sb.ToString();
                    return ret.Substring(0, ret.Length - 1);
                }
                else
                {
                    return "";
                }
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

        /// <summary>
        /// ȡ��Ⱥ������ 
        /// </summary>
        /// <param name="argUserCode">��½�û�Id</param>
        /// <returns>Ⱥ������</returns>
        [AjaxMethod]
        public string getGroupType(string userId, int parentid, string GroupTypes)
        {
            bool GroupType;
            if (GroupTypes == "True")
            {
                GroupType = true;
            }
            else
            {
                GroupType = false;
            }
            if (userId != "-1" && GroupType)
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("	SELECT id AS typecode, ");
                strSQL.Append("	    usercode AS usercode, ");
                strSQL.Append("		groupname AS typename, ");
                strSQL.Append("		GroupType AS GroupType ");
                strSQL.Append("	    FROM p_user_group ");
                strSQL.Append("	where usercode=@userId and GroupType=@GroupType and ParentGroupID=@parentid");
                SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@GroupType", GroupType);
                    command.Parameters.AddWithValue("@parentid", parentid);
                    dr = command.ExecuteReader();
                    System.Text.StringBuilder sb = new StringBuilder();
                    while (dr.Read())
                    {
                        sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["typename"]), "GB2312") + "," + Convert.ToString(dr["typecode"]) + ";" + Convert.ToString(dr["GroupType"]) + "," + Convert.ToString(dr["GroupType"]) + "#");
                    }
                    dr.Close();
                    if (sb != null && sb.ToString().Length > 0)
                    {
                        string ret = sb.ToString();
                        return ret.Substring(0, ret.Length - 1);
                    }
                    else
                    {
                        return "";
                    }
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
            else
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("	SELECT id AS typecode, ");
                strSQL.Append("	    usercode AS usercode, ");
                strSQL.Append("		groupname AS typename, ");
                strSQL.Append("		GroupType AS GroupType ");
                strSQL.Append("	    FROM p_user_group ");
                strSQL.Append("	where GroupType=@GroupType and ParentGroupID=@parentid");
                SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
                SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    command.Parameters.AddWithValue("@GroupType", GroupType);
                    command.Parameters.AddWithValue("@parentid", parentid);
                    dr = command.ExecuteReader();
                    System.Text.StringBuilder sb = new StringBuilder();
                    while (dr.Read())
                    {
                        sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["typename"]), "GB2312") + "," + Convert.ToString(dr["typecode"]) + ";" + Convert.ToString(dr["GroupType"]) + "," + Convert.ToString(dr["GroupType"]) + "#");
                    }
                    dr.Close();
                    if (sb != null && sb.ToString().Length > 0)
                    {
                        string ret = sb.ToString();
                        return ret.Substring(0, ret.Length - 1);
                    }
                    else
                    {
                        return "";
                    }
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
        }

        /// <summary>
        /// ȡ��Ⱥ���û�
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argGroupId">Ⱥ��Id</param>
        /// <returns></returns>
        [AjaxMethod]
        public string getGroupUser(int userId, string argGroupId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	SELECT B.id AS code, ");
            strSQL.Append("		A.groupname AS name ");
            strSQL.Append("	FROM p_user_group AS A, ");
            strSQL.Append("		p_user_group_detail AS B ");
            strSQL.Append("	WHERE A.id = B.fid ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.Parameters.AddWithValue("@groupcode", argGroupId);
                dr = command.ExecuteReader();
                System.Text.StringBuilder sb = new StringBuilder();
                while (dr.Read())
                {
                    sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]) + "#");
                }
                dr.Close();
                if (sb != null && sb.ToString().Length > 0)
                {
                    string ret = sb.ToString();
                    return ret.Substring(0, ret.Length - 1);
                }
                else
                {
                    return "";
                }
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

        [AjaxMethod]
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

        /// <summary>
        /// ɾ��Ⱥ���û�
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argGroupId">Ⱥ��Id</param>
        [AjaxMethod]
        public void DeleteGroupUsered(SqlCommand command, int userId, string argGroupId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	DELETE p_user_group_detail ");
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

        /// <summary>
        /// ɾ��Ⱥ��
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argGroupId">Ⱥ��Id</param>
        [AjaxMethod]
        public void DeleteGroupUser(SqlCommand command, int userId, string argGroupId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	DELETE p_user_group ");
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

        /// <summary>
        /// ����Ⱥ��Idȡ�������û�
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argGroupId">Ⱥ��Id</param>
        [AjaxMethod]
        public void delGroupUsers(int userId, string argGroupId)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	DELETE p_user_group_detail ");
            strSQL.Append("	WHERE fid = @fid ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
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

        /// <summary>
        /// ����Ⱥ���û�
        /// </summary>
        /// <param name="userId">��½�û�Id</param>
        /// <param name="argFid">Ⱥ��Id</param>
        /// <param name="argUserCode">�û�Id</param>
        /// <param name="argUserName">�û�����</param>
        [AjaxMethod]
        public void addUsered(int userId, string argFid, string argUserCode, string argUserName)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	INSERT p_user_group_detail( ");
            strSQL.Append("			fid, ");
            strSQL.Append("			usercode) ");
            strSQL.Append("	VALUES( @fid, ");
            strSQL.Append("			@usercode) ");
            SqlConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            SqlCommand command = new SqlCommand(strSQL.ToString(), conn);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command.Parameters.AddWithValue("@fid", argFid);
                command.Parameters.AddWithValue("@usercode", argUserCode);
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

        [AjaxMethod]
        public void AandDUsered(int userId, string argGroupId, string lenstring)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("	DELETE p_user_group_detail ");
            strSQL.Append("	WHERE fid = @fid ");
            StringBuilder strSQLs = new StringBuilder();
            strSQLs.Append("	INSERT p_user_group_detail( ");
            strSQLs.Append("			fid, ");
            strSQLs.Append("			usercode) ");
            strSQLs.Append("	VALUES( @fid, ");
            strSQLs.Append("			@usercode) ");
            IDbConnection conn = new SqlConnection(Teamax.Common.CommonDatabase.ConnectionString);
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            IDbCommand command = new SqlCommand(strSQL.ToString(), (SqlConnection)conn);
            IDbTransaction transaction = conn.BeginTransaction();
            try
            {
                command.Transaction = transaction;
                SqlParameter[] spInputs = new SqlParameter[]
                {
                    new SqlParameter("@fid", argGroupId),
                };
                foreach (SqlParameter p in spInputs)
                {
                    command.Parameters.Add(p);
                }

                command.ExecuteNonQuery();
                string[] len= lenstring.Split(';');
                for (int i = 1; i < len.Length - 1; i++)
                {
                    IDbCommand commands = new SqlCommand(strSQLs.ToString(), (SqlConnection)conn);
                    string[] lens=len[i].Split(',');
                    string text = lens[0];
                    string code = lens[1];
                    commands.Transaction = transaction;

                    SqlParameter[] spInput = new SqlParameter[]
                    {
                        new SqlParameter("@fid", argGroupId),
                        new SqlParameter("@usercode", code),
                    };
                    foreach (SqlParameter p in spInput)
                    {
                        commands.Parameters.Add(p);
                    }
                    commands.ExecuteNonQuery();
                    commands.Dispose();
                }

                transaction.Commit();
            }
            catch (Exception err)
            {
                transaction.Rollback();

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

    }
}
