using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace bacgDL.business.group
{
    public class groupmanager:Teamax.Common.CommonDatabase
    {

        #region
        public IDataReader getUsered(string groupid, string userId)
        {
            if (groupid != null)
            {
                string sqlStr = "select a.usercode as code,b.username as name from p_user_group_detail as a,p_user as b" +
                                  " where fid=" + groupid + " and a.username=b.usercode order by name";
               
                try
                {

                    IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                    return dr;
                }
                catch (Exception err)
                {
                    throw err;
                    return null;
                }
              
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region
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
                DeleteGroupUsered(userId, argGroupId);
                DeleteGroupUser(userId, argGroupId);
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

        #region
        public IDataReader getPurviewTreeData(string userId)
        {
            string sqlStr = "select rolecode as code,rolename as name from p_role";
            try
            {
                return (IDataReader)this.ExecuteReader(sqlStr);
            }
            catch (SqlException err)
            {
                return null;
            }
        }
        #endregion

        #region 根据角色取得角色成员，加入到角色节点下面
        /// <summary>
        /// 根据角色取得角色成员，加入到角色节点下面
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public IDataReader getUser(string userId, string rolecode)
        {
            string sqlStr = "SELECT A.username AS name,A.usercode AS code FROM p_user AS A,p_user_role AS B WHERE A.usercode = B.usercode AND B.rolecode = " + rolecode;
            try
            {
                return (IDataReader)this.ExecuteReader(sqlStr);
            }
            catch (SqlException err)
            {
                return null;
            }
        }
        #endregion

        #region  取得群组类型
        /// <summary>
        ///  取得群组类型 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentid"></param>
        /// <param name="GroupTypes"></param>
        /// <returns></returns>
        public IDataReader getGroupType(string userId, int parentid, int GroupType)
        {
            string sqlStr = "SELECT id AS typecode,usercode AS usercode,groupname AS typename,GroupType AS GroupType FROM p_user_group" +
                    "	where usercode=" + userId + " and GroupType=" + GroupType + " and ParentGroupID=" + parentid;
            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                return dr;
            }
            catch
            {
                return null;
            }
           
        }


        public IDataReader getGroupType(int parentid, int GroupType)
        {
            string sqlStr = "SELECT id AS typecode,usercode AS usercode,groupname AS typename,GroupType AS GroupType FROM p_user_group" +
                    "	where  GroupType=" + GroupType + " and ParentGroupID=" + parentid;
            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                return dr;
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region 取得群组用户
        /// <summary>
        /// 取得群组用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="argGroupId"></param>
        /// <returns></returns>
        public IDataReader getGroupUser(int userId, string argGroupId)
        {
            string sqlStr = "SELECT B.id AS code,A.groupname AS name FROM p_user_group AS A,p_user_group_detail AS B WHERE A.id = B.fid ";
            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                return dr;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 删除群组用户
        /// <summary>
        /// 删除群组用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="argGroupId"></param>
        public void DeleteGroupUsered(int userId, string argGroupId)
        {
            string sqlStr = "DELETE from p_user_group_detail  WHERE fid  = " + argGroupId;
            this.ExecuteNonQuery(sqlStr);
        }
        #endregion


        #region  删除群组
        /// <summary>
        ///  删除群组
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="argGroupId"></param>
        public void DeleteGroupUser(int userId, string argGroupId)
        {
            string sqlStr = "DELETE from  p_user_group WHERE id =" + argGroupId;
            this.ExecuteNonQuery(sqlStr);
        }
        #endregion

        #region
        public void delGroupUsers(int userId, string argGroupId)
        {
            string sqlStr = "DELETE p_user_group_detail 	WHERE fid = " + argGroupId;
            this.ExecuteNonQuery(sqlStr);
        }
        #endregion

        #region 增加群组用户
        /// <summary>
        /// 增加群组用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="argFid"></param>
        /// <param name="argUserCode"></param>
        /// <param name="argUserName"></param>
        public void addUsered(string argFid, string argUserCode)
        {
            string sqlStr = "INSERT p_user_group_detail(fid,usercode)values(" + argFid + "," + argUserCode + ")";
            this.ExecuteNonQuery(sqlStr);
            
        }
        #endregion

        #region
        public void AandDUsered(int userId, string argGroupId, string lenstring)
        {
            string sqlStr = "DELETE p_user_group_detail WHERE fid = " + argGroupId;
            this.ExecuteNonQuery(sqlStr);

             string[] len= lenstring.Split(';');
             for (int i = 1; i < len.Length - 1; i++)
             {
                 string[] lens = len[i].Split(',');
                 string text = lens[0];
                 string code = lens[1];

                 string sql = "INSERT p_user_group_detail( fid,usercode) VALUES(" + argGroupId + "," + code + ") ";
                 this.ExecuteNonQuery(sql);
             }

        }

        #endregion


    }
}
