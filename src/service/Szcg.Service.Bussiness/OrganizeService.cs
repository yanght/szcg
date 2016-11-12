using bacgBL.web.szbase.entity;
using bacgBL.web.szbase.organize;
using bacgDL.business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using szcg.com.teamax.business.entity;
using Szcg.Service.Common;
using Szcg.Service.IBussiness;
using Szcg.Service.Model;

namespace Szcg.Service.Bussiness
{
    public class OrganizeService : IOrganizeService
    {
        private string strErr = "";

        #region 用户相关
        /// <summary>
        /// 根据部门获取用户列表
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="loginName">用户登录名</param>
        /// <param name="departName">部门名称</param>
        /// <param name="order">排序</param>
        /// <param name="filds">排序字段</param>
        /// <returns></returns>
        public List<UserInfo> GetUserByDeptID(int departId, PageInfo pageInfo, int userId, string userName, string loginName, string departName, string order, string filds)
        {
            List<UserInfo> list = new List<UserInfo>();
            DepartManage bl = new DepartManage();

            DataSet ds = bl.GetUserByDeptID(departId, int.Parse(pageInfo.CurrentPage), int.Parse(pageInfo.PageSize), int.Parse(pageInfo.ReturnRecordCount),
                                                     userId, userName, loginName, departName, order, filds, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("根据部门获取用户列表异常：" + strErr);
            }

            if (ds.Tables.Count > 1)
            {
                pageInfo.RowCount = ds.Tables[1].Rows[0][0].ToString();
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.setUsercode(SqlDataConverter.ToInt32(dr["usercode"].ToString()));
                    userInfo.setUsername(SqlDataConverter.ToString(dr["username"].ToString()));
                    userInfo.setLoginname(SqlDataConverter.ToString(dr["loginname"].ToString()));
                    userInfo.setSex(SqlDataConverter.ToString(dr["sex"].ToString()));
                    userInfo.setTel(SqlDataConverter.ToString(dr["tel"].ToString()));
                    userInfo.setMobile(SqlDataConverter.ToString(dr["mobile"].ToString()));
                    userInfo.setDepartname(SqlDataConverter.ToString(dr["departname"].ToString()));
                    list.Add(userInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据用户编码获取用户
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public UserData GetUserById(int userCode)
        {
            UserManage bl = new UserManage();

            string[] values = bl.GetRoleInfo1(userCode, ref strErr);//BASE_orghelper.getUserInfoByID(id,_userID);

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("根据用户编码获取用户异常：" + strErr);
            }
            UserData userInfo = new UserData()
            {
                loginName = values[0],
                userName = values[1],
                password = values[2],
                parentID = values[3],
                priWeb = values[4],
                pubWeb = values[5],
                tel = values[6],
                hometel = values[7],
                fax = values[8],
                zipCode = values[9],
                mobile = values[10],
                mobile1 = values[11],
                email = values[12],
                address = values[13],
                sex = values[14],
                user_areacode = values[15],
                birthday = values[16],
                strVirPath = values[17],
                memo = values[18],
                departName = values[19],
                callCenterUserID = values[20],
                videolevel = values[21],
                roleids = values[22],
                rolenames = values[23],
                streetname = values[24],
                Hcpower = values[25],
                Sort = values[26],
                userCode = userCode.ToString()
            };
            return userInfo;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public bool InsertUser(UserData user)
        {
            UserManage bl = new UserManage();
            string[] values = new string[24];
            values[0] = user.loginName;
            values[1] = user.userName;
            values[2] = user.password;
            values[3] = user.parentID;
            values[4] = user.priWeb;
            values[5] = user.pubWeb;
            values[6] = user.tel;
            values[7] = user.hometel;
            values[8] = user.fax;
            values[9] = user.zipCode;
            values[10] = user.mobile;
            values[11] = user.mobile1;
            values[12] = user.email;
            values[13] = user.address;
            values[14] = user.sex;
            values[15] = user.user_areacode;
            values[16] = user.birthday;
            values[17] = user.strVirPath;
            values[18] = user.memo;
            values[19] = user.roleids;
            values[20] = user.callCenterUserID;
            values[21] = user.gradeList;
            values[22] = user.Hcpower;
            values[23] = user.Sort;

            int state = bl.InsertUser1(values, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("添加用户异常：" + strErr);
            }
            return state > 0;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <param name="user">用户实体</param>
        /// <param name="oldRoleID">用户角色Id</param>
        /// <returns></returns>
        public bool UpdateUser(int userCode, UserData user, string oldRoleID)
        {
            UserManage bl = new UserManage();
            string[] values = new string[24];
            values[0] = user.loginName;
            values[1] = user.userName;
            values[2] = user.password;
            values[3] = user.parentID;
            values[4] = user.priWeb;
            values[5] = user.pubWeb;
            values[6] = user.tel;
            values[7] = user.hometel;
            values[8] = user.fax;
            values[9] = user.zipCode;
            values[10] = user.mobile;
            values[11] = user.mobile1;
            values[12] = user.email;
            values[13] = user.address;
            values[14] = user.sex;
            values[15] = user.user_areacode;
            values[16] = user.birthday;
            values[17] = user.strVirPath;
            values[18] = user.memo;
            values[19] = user.roleids;
            values[20] = user.callCenterUserID;
            values[21] = user.gradeList;
            values[22] = user.Hcpower;
            values[23] = user.Sort;
            string[] ids = this.compareOldAndNewRoleIDS(oldRoleID, values[19]);
            int state = bl.UpdateUser1(userCode, values, ids[0], ids[1], ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("更新用户异常：" + strErr);
            }
            return state > 0;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public bool DeleteUser(int userCode)
        {
            UserManage bl = new UserManage();
            int i = bl.DeleteUser(userCode.ToString(), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("删除用户异常：" + strErr);
            }
            return i > 0;
        }

        #endregion

        #region 部门相关

        DepartManage bl = new DepartManage();

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="depart">部门实体</param>
        /// <returns></returns>
        public ReturnValue InsertDepart(Depart depart)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };


            string _userDefinedCode = "0001";

            if (!string.IsNullOrEmpty(depart.ParentCode))
            {

                ArrayList arrList = bl.GetUserDefineCode1(depart.ParentCode, ref strErr);
                ArrayList _arrList = bl.getDeptInfo(depart.ParentCode, "area");
                string area = _arrList[0].ToString();
                depart.AreaId = area;

                if (arrList.Count > 0) //添加的为二级组织结构
                {
                    string prentDefinedCode = arrList[0].ToString();
                    string temp = "";
                    //string tt = arrList[1].ToString();
                    if (arrList.Count >= 2 && arrList[1].ToString() != "")
                    {
                        for (int i = 1; i <= arrList.Count - 1; i++)
                        {
                            if (compareTo(arrList[arrList.Count - 1].ToString()) > compareTo(arrList[i].ToString()))
                                temp = arrList[arrList.Count - 1].ToString();
                            else
                                temp = arrList[i].ToString();
                        }
                        _userDefinedCode = bl.increase(temp);
                    }
                    else
                        _userDefinedCode = prentDefinedCode + "001";

                }
                else //添加的为一级组织结构
                {
                    string keyvalue = "UserDefinedCode";
                    string tablename = "p_depart";
                    string maxValue = Convert.ToString(bl.selectMaxValue(tablename, keyvalue, "", ""));
                    if (maxValue.Length > 4)
                        maxValue = maxValue.Substring(0, 4);
                    _userDefinedCode = bl.increase(maxValue);
                }

            }

            int state = bl.InsertDepart(
                string.IsNullOrEmpty(depart.ParentCode) ? 0 : Convert.ToInt32(depart.ParentCode)
                , depart.DepartName
                , depart.DepartAddress
                , depart.AreaId
                , depart.Memo
                , depart.IsDuty.ToString()
                , depart.Mobile
                , depart.Tel
                , depart.Principal
                , _userDefinedCode
                , depart.Max_NoteNum
                , depart.IsAcceptNote.ToString()
                , depart.IsSJ.ToString()
                , depart.SJ_RoleCode
                , depart.NoAppraise
                , ref strErr
                , string.IsNullOrEmpty(depart.Sort) ? "999" : depart.Sort
                );

            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("添加部门异常：" + strErr);
                rtn.ErrorMsg = strErr;
            }
            else
            {
                rtn.ReturnState = state > 0;
            }
            return rtn;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="depart">部门实体</param>
        /// <returns></returns>
        public ReturnValue UpdateDepart(Depart depart)
        {
            ReturnValue rtn = new ReturnValue() { ReturnState = true };

            string parentCode = "";
            string _userDefinedCode = "0001";

            bool refresh_userDefinedCode = !string.Equals(bl.GetParentCode(depart.DepartCode, ref strErr), depart.ParentCode);

            if (!refresh_userDefinedCode)  //无需重定位 UserDefineCode
            {
                parentCode = depart.ParentCode;
                _userDefinedCode = depart.UserDefinedCode;
            }
            else                        //需重定位 UserDefineCode
            {
                parentCode = depart.ParentCode;
                // 自动生成 UserDefineCode
                // wj_add
                // begin  <暂时只保证2级组织结构>
                ArrayList arrList = bl.GetUserDefineCode1(parentCode, ref strErr);
                if (arrList.Count > 0)   //重定位到其他一级目录组织结构
                {
                    string prentDefinedCode = arrList[0].ToString();
                    string temp = "";
                    if (arrList.Count >= 2 && arrList[1].ToString() != "")
                    {
                        for (int i = 1; i <= arrList.Count - 1; i++)
                        {
                            if (Convert.ToInt32(arrList[arrList.Count - 1].ToString()) > Convert.ToInt32(arrList[i].ToString()))
                                temp = arrList[arrList.Count - 1].ToString();
                            else
                                temp = arrList[i].ToString();
                        }
                        _userDefinedCode = bl.increase(temp);
                    }
                    else
                        _userDefinedCode = prentDefinedCode + "001";
                }
                else
                {                 //重定位到目录组织结构根节点，等同于新建一级组织结构
                    string keyvalue = "UserDefinedCode";
                    string tablename = "p_depart";
                    string maxValue = Convert.ToString(bl.selectMaxValue(tablename, keyvalue, "", ""));
                    if (maxValue.Length > 4)
                        maxValue = maxValue.Substring(0, 4);
                    _userDefinedCode = bl.increase(maxValue);
                }
                // end <7/17/2007>
            }


            int state = bl.UpdateDepart(
              _userDefinedCode
                , depart.DepartName
                , depart.ParentCode
                , depart.AreaId
                , depart.Principal
                , depart.Mobile
                , depart.Tel
                , depart.DepartAddress
                , depart.Memo
                , depart.IsDuty.ToString()
                , depart.Max_NoteNum
                , depart.IsAcceptNote.ToString()
                , depart.IsSJ.ToString()
                , depart.SJ_RoleCode
                , depart.NoAppraise
                , depart.DepartCode
                , 0
                , ref strErr
                , string.IsNullOrEmpty(depart.Sort) ? "999" : depart.Sort
                );
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("更新部门异常：" + strErr);
                rtn.ErrorMsg = strErr;
            }
            rtn.ReturnState = state > 0;
            return rtn;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <returns>0：删除发生异常1：标识删除成功2：部门存在人员，删除失败3：部门存在子部门，删除失败</returns>
        public int DeleteDepart(string departId)
        {
            //0：删除发生异常1：标识删除成功2：部门存在人员，删除失败3：部门存在子部门，删除失败
            int flag = 0;
            bl.DeleteDepart(departId, out flag, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.Error("删除部门异常：" + strErr);
            }
            return flag;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门Id</param>
        /// <returns></returns>
        public Depart GetDepartInfo(string departId)
        {
            DataSet ds = bl.GetDepartInfo(int.Parse(departId), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取部门信息异常：" + strErr);
            }
            Depart depart = new Depart();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    depart.DepartCode = SqlDataConverter.ToString(dr["departcode"]);
                    depart.DepartName = SqlDataConverter.ToString(dr["departname"]);
                    depart.ParentDepartName = SqlDataConverter.ToString(dr["parentdepartname"]);
                    depart.Memo = SqlDataConverter.ToString(dr["memo"]);
                    depart.DepartAddress = SqlDataConverter.ToString(dr["departadress"]);
                    depart.ParentCode = SqlDataConverter.ToString(dr["parentcode"]);
                    depart.Mobile = SqlDataConverter.ToString(dr["Mobile"]);
                    depart.Tel = SqlDataConverter.ToString(dr["tel"]);
                    depart.Principal = SqlDataConverter.ToString(dr["principal"]);
                    depart.UserDefinedCode = SqlDataConverter.ToString(dr["UserDefinedCode"]);
                    depart.DutyId = SqlDataConverter.ToInt32(dr["dutyid"]);
                    depart.Area = SqlDataConverter.ToString(dr["area"]);
                    depart.IsDuty = SqlDataConverter.ToInt32(dr["IsDuty"]);
                    depart.IsAcceptNote = SqlDataConverter.ToInt32(dr["IsAcceptNote"]);
                    depart.Max_NoteNum = SqlDataConverter.ToInt32(dr["max_notenum"]);
                    depart.IsSJ = SqlDataConverter.ToInt32(dr["IsSJ"]);
                    depart.SJ_RoleCode = SqlDataConverter.ToString(dr["sj_rolecode"]);
                    depart.NoAppraise = SqlDataConverter.ToString(dr["NoAppraise"]);
                    depart.RoleName = SqlDataConverter.ToString(dr["rolename"]);
                }
            }
            return depart;
        }

        /// <summary>
        /// 获取职能部门列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <returns></returns>
        public List<Depart> GetDepartList(string areaCode)
        {
            DataSet ds = bacgBL.business.Project.GetDepartList(areaCode, out strErr);

            List<Depart> list = new List<Depart>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Depart depart = new Depart()
                    {
                        UserDefinedCode = dr["UserDefinedCode"].ToString(),
                        DepartName = dr["departname"].ToString()
                    };
                    list.Add(depart);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="departCode">当前用户部门编码</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public List<Depart> GetDepartList(string areaCode, string departCode, string userCode)
        {
            UserManage b12 = new UserManage();
            DepartManage bl1 = new DepartManage();

            string userLevel = b12.GetUserLevel(Convert.ToInt32(userCode), ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取用户账户级别异常 用户Code:{0},异常信息：{1}", userCode, strErr);
            }
            string pcode = bl1.GetParentCode(departCode, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取父部门异常 部门Code:{0}，异常信息：{1}", departCode, strErr);
            }

            if (userLevel == "9") //区级系统管理员
            {
                pcode = "0";
            }
            if (userLevel == "8") //街道级管理员
            {
                pcode = bl1.GetStreetDepartCode(areaCode, ref strErr);
            }

            List<Depart> list = new List<Depart>();

            ArrayList al = bl.GetDepartListforTree(areaCode, pcode, userLevel, ref strErr);
            if (!string.IsNullOrEmpty(strErr))
            {
                LoggerManager.Instance.logger.ErrorFormat("获取部门列表异常：" + strErr);
            }

            foreach (var item in al)
            {
                TreeSuruct ts = (TreeSuruct)item;

                Depart depart = new Depart()
                {
                    ParentCode = ts.pcode,
                    DepartCode = ts.code,
                    DepartName = ts.text,
                    Area = ts.tag
                };

                list.Add(depart);
            }

            return list;
        }

        /// <summary>
        /// 获取职能部门列表
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <param name="typecode">案卷类型</param>
        /// <returns></returns>
        public List<Depart> GetDutyDepart(string areacode, string typecode)
        {
            string smallclass = "";
            string strGreat = "0";
            string streetcode = "";
            string iSFristDepart = "1";
            iSFristDepart = "1";//默认是显示主部门
            //现行数据库中取出的type为True/False ,不符合1/0判定标准,暂做如下调整(WJ_add 07-10-29)
            if (typecode.Equals("True"))
                typecode = "1";
            else
                typecode = "0";
            string strAreaCode = areacode;

            if (smallclass != "" && typecode != "" || strGreat != "")
            {
                string strErr = "";
                DataSet ds = bacgBL.business.Project.GetDutyDepart(strAreaCode, typecode, smallclass, "", out strErr);
                if (strErr != "")
                {
                    //this.MessageBox(string.Format("加载数据出错！\\n{0}", strErr));
                    //return;     
                }
                string StrWhere = "1=1";
                if (iSFristDepart != "1")
                    StrWhere = "parentcode<>0";
                else
                    StrWhere = "parentcode=0";// and area='330483'
                ds.Tables[0].DefaultView.RowFilter = StrWhere;
                DataTable dt = ds.Tables[0].DefaultView.ToTable();
                List<Depart> list = ConvertDtHelper<Depart>.GetModelList(dt);
                return list;
            }
            return new List<Depart>();
        }

        /// <summary>
        /// 检查部门名称是否存在
        /// </summary>
        /// <param name="parentDepartId">父级部门Id</param>
        /// <param name="departName">部门名称</param>
        /// <returns></returns>

        public bool CheckDepartName(string parentDepartId, string departName)
        {
            return bl.CheckDepartName(parentDepartId, departName, ref strErr);
        }


        #endregion

        #region 查找新角色与旧角色的差
        //取得修改角色后,需要添加和删除的角色ID;
        private string[] compareOldAndNewRoleIDS(string oldRoleID, string newRoleID)
        {
            string[] values = new string[2];
            string[] old = oldRoleID.Split(',');
            string[] _new = newRoleID.Split(',');

            oldRoleID = oldRoleID + ",";
            newRoleID = newRoleID + ",";
            StringBuilder repeat = new StringBuilder();
            StringBuilder del = new StringBuilder();
            StringBuilder add = new StringBuilder();
            bool flag = false;
            for (int i = 0; i < old.Length; i++)
            {
                for (int j = 0; j < _new.Length; j++)
                {
                    if (_new[j].Equals(old[i]))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    repeat.Append(old[i] + ",");
                    flag = false;
                }
            }

            if (repeat.ToString().Length > 1)
            {
                string _repeat = repeat.ToString();
                _repeat = _repeat.Substring(0, _repeat.Length - 1);
                string[] rep = _repeat.Split(',');


                for (int m = 0; m < old.Length; m++)
                {
                    for (int i = 0; i < rep.Length; i++)
                    {
                        if (old[m].Equals(rep[i]))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        del.Append(old[m] + ",");
                    }
                    else
                    {
                        flag = false;
                    }
                }

                for (int n = 0; n < _new.Length; n++)
                {
                    for (int i = 0; i < rep.Length; i++)
                    {
                        if (_new[n].Equals(rep[i]))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        add.Append(_new[n] + ",");
                    }
                    else
                    {
                        flag = false;
                    }
                }


                oldRoleID = del.ToString();
                newRoleID = add.ToString();
                if (oldRoleID.Length > 1)
                {
                    values[0] = oldRoleID.ToString().Substring(0, oldRoleID.Length - 1);
                }
                else
                {
                    values[0] = "";
                }
                if (newRoleID.Length > 1)
                {
                    values[1] = newRoleID.ToString().Substring(0, newRoleID.Length - 1);
                }
                else
                {
                    values[1] = "";
                }

            }
            else
            {
                values[0] = oldRoleID;
                values[1] = newRoleID;
            }

            return values;
        }
        #endregion

        private int compareTo(string m)
        {
            return Convert.ToInt32(m.Substring(m.Length - 4, 4));
        }

    }
}
