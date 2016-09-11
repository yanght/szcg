using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


using Teamax.Common;
using DBbase.business.group;

namespace bacgDL.business.group
{
    public class BUSINESS_GroupManagers : Teamax.Common.CommonDatabase
    {
        protected StringBuilder sb;
        private ArrayList list;

        #region getPurviewTreeData:取出数据库数据，加入alai_tree中
        /// <summary>
        /// 取出数据库数据，加入alai_tree中
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns></returns>
        public String[] getPurviewTreeData(int argUserCode)
        {
            string sqlStr = "select rolecode ,rolename from role";

            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                ArrayList list = new ArrayList();
                while (dr.Read())
                {
                    sb = new StringBuilder();
                    sb.Append(Convert.ToDecimal(dr["rolecode"]) + ",");
                    sb.Append(Convert.ToString(dr["rolename"]));
                    list.Add(sb.ToString());
                }
                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        #endregion

        #region GetGroupTreeList:获得树形结构的信息
        /// <summary>
        /// 获得树形结构的信息
        /// </summary>
        /// <param name="usercode">用户代码</param>
        /// <param name="GroupType">组类型</param>
        /// <param name="strErr">错误返回信息</param>
        /// <returns></returns>
        public ArrayList GetGroupTreeList(int usercode, bool GroupType, ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            string departSQL = "";
            if (GroupType)
            {
                if (usercode == 0)//超级用户登陆的时候，可以看到全部的部门信息
                    departSQL = string.Format(@"select * from p_user_group where GroupType='{0}' order by groupname", GroupType);
                else
                {
                    departSQL = string.Format(@"select * from p_user_group where usercode='{0}' and GroupType='{1}' order by groupname", usercode, GroupType);
                }
            }
            else
            {
                departSQL = string.Format(@"select * from p_user_group where GroupType='{0}' order by groupname ", GroupType);
            }
            try
            {
                IDataReader departDr = (IDataReader)this.ExecuteReader(departSQL);
                while (departDr.Read())
                {
                    GroupTreeSuruct ts = new GroupTreeSuruct();
                    ts.id = Convert.ToInt32(departDr["id"]);
                    ts.ParentGroupID = Convert.ToInt32(departDr["ParentGroupID"]);
                    ts.groupname = departDr["groupname"].ToString();
                    ts.GroupType = Convert.ToBoolean(departDr["GroupType"]);
                    treeStructList.Add(ts);
                }
                departDr.Close();
                return treeStructList;
                
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion;

        #region GetGroupTreeList2:获得树形结构的信息
        /// <summary>
        /// 获得树形结构的信息
        /// </summary>
        /// <param name="usercode">用户代码</param>
        ///// <param name="GroupType">组类型</param>
        /// <param name="strErr">错误返回信息</param>
        /// <returns></returns>
        public ArrayList GetGroupTreeList2(int usercode, ref string strErr)
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
                                                select '0'  ,'a1','公共群组','0'
                                                union all 
                                                select '0' ,'a2','个人群组','0'
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
                
                IDataReader departDr = this.ExecuteReader(departSQL);
                while (departDr.Read())
                {
                    TreeSuruct ts;
                    ts.pcode = departDr["pcode"].ToString();
                    ts.code = departDr["code"].ToString();
                    ts.text = departDr["text"].ToString();
                    ts.tag = departDr["tag"].ToString();
                    treeStructList.Add(ts);
                }
                departDr.Close();

                string strMobile = "";
                IDataReader userDr = this.ExecuteReader(userSQL);
                while (userDr.Read())
                {
                    TreeSuruct ts;
                    ts.pcode = userDr["pcode"].ToString();
                    ts.code = userDr["code"].ToString() + "aaaa";
                    ////ts.text = userDr["text"].ToString() + "(" + userDr["code"].ToString() + ")";
                    strMobile = userDr["code"].ToString();
                    strMobile = strMobile.Trim() == "" ? "无手机号码" : strMobile;
                    ts.text = userDr["text"].ToString() + "【" + strMobile + "】";
                    ts.tag = userDr["tag"].ToString();
                    treeStructList.Add(ts);
                }
                userDr.Close();

                return treeStructList;
                
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetGroupPersion:获取群组人员
        /// <summary>
        /// 获取群组人员
        /// </summary>
        /// <param name="id">群组ID</param>
        /// <returns></returns>
        public ArrayList GetGroupPersion(string id, ref string strErr)
        {
            ArrayList GroupPersontree = new ArrayList();
            string strsql = string.Format(@"select a.usercode as usercode,b.UserName as username,b.mobile as mobile,b.memo as memo from p_user_group_detail as a,p_user as b where a.fid={0} and a.usercode=b.usercode", id);
            try
            {
                IDataReader GroupPersion = (IDataReader)this.ExecuteReader(strsql);
                while (GroupPersion.Read())
                {
                    GroupPerson ts = new GroupPerson();
                    ts.usercode = Convert.ToInt32(GroupPersion["usercode"]);
                    ts.username = GroupPersion["username"].ToString();
                    GroupPersontree.Add(ts);
                }
                GroupPersion.Close();
                return GroupPersontree;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region getUser:根据角色取得角色成员，加入到角色节点下面
        /// <summary>
        /// 根据角色取得角色成员，加入到角色节点下面
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="rolecode">角色Id</param>
        /// <returns></returns>
        public String[] getUser(int userId, string rolecode)
        {
            string sqlStr = "select a.username as name ,a.usercode as code from loginuser as a ,user_role as b where a.usercode=b.usercode and b.rolecode=" + rolecode;
            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                ArrayList list = new ArrayList();
                while (dr.Read())
                {
                    sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["name"]) + ",");
                    sb.Append(Convert.ToDecimal(dr["code"]));
                    list.Add(sb.ToString());
                }
                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception err)
            {

                throw;
            }

        }
        #endregion

        #region getGroupType:取得群组类型
        /// <summary>
        /// 取得群组类型 
        /// </summary>
        /// <param name="argUserCode">登陆用户Id</param>
        /// <returns>群组类型</returns>
        public String[] getGroupType(int userId)
        {
            string sqlStr = "SELECT groupcode AS code,groupname AS name FROM szgroup";

            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                list = new ArrayList();
                while (dr.Read())
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

                throw err;
            }

        }
        #endregion

        #region getGroupUser:取得群组用户
        /// <summary>
        /// 取得群组用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argGroupId">群组Id</param>
        /// <returns></returns>
        public String[] getGroupUser(int userId, string argGroupId)
        {
            string sqlStr = "SELECT B.id AS code,B.groupname AS name FROM szgroup AS A,user_group AS B WHERE A.groupcode = B.groupcode AND A.groupcode =" + argGroupId;

            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                list = new ArrayList();
                while (dr.Read())
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

                throw err ;
            }

        }
        #endregion

        #region getRoleData:取得群组用户
        /// <summary>
        /// 取得群组用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <returns></returns>
        public DataSet getRoleData(int userId)
        {
            string sqlStr = "SELECT A.rolecode AS rolecode,A.rolename AS rolename,C.username AS username,C.usercode AS usercode FROM role AS A, user_role AS B,loginuser AS C" +
                       "WHERE A.rolecode = B.rolecode AND B.usercode = C.usercode ORDER BY A.rolecode, B.usercode";

            DataSet dtst = this.ExecuteDataset(sqlStr);
            dtst.Tables[0].TableName = "tblGroup";
            return dtst;

        }
        #endregion

        #region delGroupUsers:根据群组Id删除所有用户
        /// <summary>
        /// 根据群组Id删除所有用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argGroupId">群组Id</param>
        public void delGroupUsers(int userId, string argGroupId)
        {
            string sqlStr = "delete from user_grouped where fid=" + argGroupId;

            int i = this.ExecuteNonQuery(sqlStr);

        }
        #endregion

        #region addUsered:增加群组用户
        /// <summary>
        /// 增加群组用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argFid">群组Id</param>
        /// <param name="argUserCode">用户Id</param>
        /// <param name="argUserName">用户名称</param>
        public void addUsered(int userId, string argFid, string argUserCode, string argUserName)
        {
            string sqlStr = "insert into user_grouped(fid,usercode,username)values(" + argFid + "," + argUserCode + "," + argUserName + ")";
            int i = this.ExecuteNonQuery(sqlStr);
        }



        public void addUsered(string argFid, string argUserCode, string argUserName)
        {
            string sqlStr = "insert into user_grouped(fid,usercode,username)values(" + argFid + "," + argUserCode + "," + argUserName + ")";
            int i = this.ExecuteNonQuery(sqlStr);
        }

        #endregion

        #region DeleteGroupUsered:删除群组用户
        /// <summary>
        /// 删除群组用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argGroupId">群组Id</param>
        public void DeleteGroupUsered(int userId, string argGroupId)
        {
            string sqlStr = "delete from user_grouped where fid=" + argGroupId;
            this.ExecuteNonQuery(sqlStr);
        }
        #endregion

        #region DeleteGroupUser:删除群组
        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argGroupId">群组Id</param>
        public void DeleteGroupUser(int userId, string argGroupId)
        {
            string sqlStr = "delete from user_group where id=" + argGroupId;
            this.ExecuteNonQuery(sqlStr);
        }


        public void DelGroupUser(int userId, string argGroupId)
        {
            DeleteGroupUsered(userId, argGroupId);
            DeleteGroupUser(userId, argGroupId);
        }
        #endregion

        #region getGroupDataSource:取得群组类型
        /// <summary>
        /// 取得群组类型
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <returns>群组类型</returns>
        public ArrayList getGroupDataSource(int userId)
        {
            string sqlStr = "SELECT groupcode AS code,groupname AS name FROM szgroup WHERE groupname <> '机构群组'";

            try
            {
                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                list = new ArrayList();
                while (dr.Read())
                {
                    list.Add(new USState(Convert.ToString(dr["name"]), Convert.ToString(dr["code"])));
                }
                dr.Close();
                return (ArrayList)(list);
            }
            catch (Exception err)
            {

                throw;
            }
        }
        #endregion

        #region addGroupUser:新增群组用户
        /// <summary>
        /// 新增群组用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argGroupTypeCode">群组类型Id</param>
        /// <param name="argGroupName">群组类型名称</param>
        public void addGroupUser(int argUserCode, int argGroupTypeCode, string argGroupName, string parentGroupID)
        {
            string sqlStr = "insert into p_user_group(usercode,groupname,ParentGroupID,GroupType)values(" + argUserCode + ",'" +
                         argGroupName + "'," + Convert.ToInt32(parentGroupID) + ","+argGroupTypeCode + ") ";
            this.ExecuteNonQuery(sqlStr);

        }
        #endregion

        #region editGroupUser:修改群组
        /// <summary>
        /// 修改群组
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <param name="argId">群组对应Id</param>
        /// <param name="argGroupName">群组名称</param>
        public void editGroupUser(int userId, string argId, string argGroupName)
        {
            string sqlStr = "update p_user_group set groupname='" + argGroupName + "' where id=" + argId;
            this.ExecuteNonQuery(sqlStr);
        }
        #endregion

        #region getUsered:取得角色用户
        /// <summary>
        /// 取得角色用户
        /// </summary>
        /// <param name="userId">登陆用户Id</param>
        /// <returns></returns>
        public ArrayList getUsered(string userId, string argGroupId)
        {
            string sqlStr = "SELECT usercode AS code,username AS name FROM user_grouped WHERE fid=" + argGroupId;

            try
            {

                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                ArrayList list = new ArrayList();
                while (dr.Read())
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

                throw;
            }
        }
        #endregion

        #region getGroup:获取群组
        /// <summary>
        /// 获取群组
        /// </summary>
        /// <param name="groupid">群组ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string getGroup(string groupid, string userId)
        {
            string sqlStr = "SELECT usercode AS code,username AS name FROM user_grouped WHERE fid=" + groupid;

            try
            {

                IDataReader dr = (IDataReader)this.ExecuteReader(sqlStr);
                System.Text.StringBuilder sb = null;

                while (dr.Read())
                {
                    sb = new StringBuilder();
                    //sb.Append(bacgBL.Pub.Tools.StrConv(Convert.ToString(dr["name"]), "GB2312") + "," + Convert.ToString(dr["code"]));
                    sb.Append("#");
                }
                dr.Close();

                if (sb != null)
                {
                    string ret = sb.ToString();
                    return ret.Substring(0, ret.Length - 1);
                }

                return "";

            }
            catch (Exception err)
            {

                return "";
            }

        }
        #endregion
    }
}
