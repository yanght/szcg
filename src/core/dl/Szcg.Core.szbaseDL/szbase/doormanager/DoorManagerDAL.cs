using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Diagnostics;

namespace bacgDL.szbase.doormanager
{
    public class DoorManagerDAL : Teamax.Common.CommonDatabase
    {
        #region GetBigEventTreeData：获取大类事件树数据
        /// <summary>
        /// 获取大类事件树数据
        /// </summary>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String[] GetBigEventTreeData(ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select id,bigclasscode,name from s_bigclass_event";
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["id"]) + ",");
                    sb.Append(Convert.ToString(dr["bigclasscode"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallEventTreeData：获取小类事件树数据
        /// <summary>
        /// 获取小类事件树数据
        /// </summary>
        /// <param name="id">大类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns>小类事件ID和名称</returns>
        public String[] GetSmallEventTreeData(string fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.name 
                                            from s_smallclass_event a left join s_bigclass_event b
                                            on a.bigclassCode = b.bigclassCode
                                            where char(b.id)='{0}'", fid);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["id"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取小类事件树数据
        /// </summary>
        /// <param name="id">大类ID</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns>小类事件自定义编码和名称</returns>
        public String[] GetSmallEventTreeData(int id, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.smallcallCode,a.name 
                                            from s_smallclass_event a left join s_bigclass_event b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id={0}", id);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["smallcallCode"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetBigEventInfo：根据大类编码，获取大类事件信息
        /// <summary>
        /// 根据大类编码，获取大类事件信息
        /// </summary>
        /// <param name="bigId">大类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String GetBigEventInfo(string bigId, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select bigclassCode,name 
                                            from s_bigclass_event
                                            where char(id) = '{0}'", bigId);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    retValue = dr["name"].ToString() + "," + dr["bigclassCode"].ToString();
                }

                dr.Close();
                return retValue;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallEventInfo：根据小类编码，获取小类事件信息
        /// <summary>
        /// 根据小类编码，获取小类事件信息
        /// </summary>
        /// <param name="smallId">小类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public Hashtable GetSmallEventInfo(string smallId, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select B.smallcallCode as smallCode,B.name as sname,A.name as bname,A.bigclassCode,
		                                            B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,
		                                            B.t_time_kc as kancha,B.t_time_ts as zonghe  
                                            from s_smallclass_event B left join s_bigclass_event A
                                            on B.bigclassCode=A.bigclassCode
                                            where char(B.id)='{0}'", smallId);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    string scode = dr["smallCode"].ToString();
                    table.Add("scode", scode);
                    string sname = Convert.ToString(dr["sname"]);
                    table.Add("sname", sname);
                    string bname = Convert.ToString(dr["bname"]);
                    table.Add("bname", bname);
                    string bcode = Convert.ToString(dr["bigclassCode"]);
                    table.Add("bigclassCode", bcode);
                    string timelimit = Convert.ToString(dr["timelimit"]);
                    table.Add("timelimit", timelimit);
                    string rolecode = Convert.ToString(dr["rolecode"]);
                    table.Add("rolecode", rolecode);
                    string dutyunit = dr["dutyunit"] == null ? "" : dr["dutyunit"].ToString();
                    table.Add("dutyunit", dutyunit);
                    string kc = Convert.ToString(dr["kancha"]);
                    table.Add("kancha", kc);
                    string zh = Convert.ToString(dr["zonghe"]);
                    table.Add("zonghe", zh);
                }
                dr.Close();
                if (table["dutyunit"].ToString().Equals(""))
                {
                    table.Add("departname", "");
                }
                else
                {
                    string ids = table["dutyunit"].ToString();
                    String sql = "select departname from p_depart where departcode in(" + ids + ")";
                    dr = this.ExecuteReader(sql);
                    string names = "";
                    while (dr.Read())
                    {
                        names += dr["departname"].ToString() + ",";
                    }
                    if (names.Length > 0)
                    {
                        names = names.Substring(0, names.Length - 1);
                    }
                    table.Add("departname", names);
                    dr.Close();
                }
                return table;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteEvent：删除事件数据
        /// <summary>
        /// 删除事件数据
        /// </summary>
        /// <param name="eventId">事件编码</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int DeleteEvent(string eventId, ref string strErr)
        {
            string strSQL = "delete from s_bigclass_event where char(id) = '" + eventId + "'";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region DeleteSmallEvent：删除事件小类数据
        /// <summary>
        /// 删除事件小类数据
        /// </summary>
        /// <param name="smalleventId">小类事件编码</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int DeleteSmallEvent(string smalleventId, ref string strErr)
        {
            string strSQL = "delete from  s_smallclass_event where char(id) = '" + smalleventId + "'";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckEvent：判断事件是否已经存在
        /// <summary>
        /// 判断事件是否已经存在
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckEvent(string eventName, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_event where name ='" + eventName + "'";
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

        #region CheckEventCode：判断事件编号是否已经存在
        /// <summary>
        /// 判断事件编号是否已经存在
        /// </summary>
        /// <param name="eventCode">事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckEventCode(string eventCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_event where bigclassCode ='" + eventCode + "'";
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

        #region InsertEvent：插入事件信息
        /// <summary>
        /// 插入事件信息
        /// </summary>
        /// <param name="eventCode">事件编号</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int InsertEvent(string bigclassCode, string eventName, ref string strErr)
        {
            string strSQL = "insert into s_bigclass_event (bigclassCode,name) values('" + bigclassCode + "','" + eventName + "') ";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckSmallEvent：判断小类事件是否已经存在
        /// <summary>
        /// 判断小类事件是否已经存在
        /// </summary>
        /// <param name="smallEventName">小类事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckSmallEvent(string smallEventName, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_event where name ='" + smallEventName + "'";
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

        #region CheckSmallCode：判断小类事件编号是否已经存在
        /// <summary>
        /// 判断小类事件编号是否已经存在
        /// </summary>
        /// <param name="smallEventName">小类事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckSmallCode(string smallEventCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_event where char(smallcallCode) ='" + smallEventCode + "'";
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

        #region InsertSmallEvent：插入小类事件信息
        /// <summary>
        /// 插入小类事件信息
        /// </summary>
        /// <param name="bigid">大类编码</param>
        /// <param name="classCode">小类编码</param>
        /// <param name="className">小类名称</param>
        /// <param name="limit">时限</param>
        /// <param name="rolecode">角色</param>
        /// <param name="dutyunit">责任单位</param>
        /// <param name="kc">勘查时限</param>
        /// <param name="zh">综合处理时限</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int InsertSmallEvent(int bigid, string smallCode, string className, int limit,
                                        int rolecode, string dutyunit, int kc,
                                        int zh, ref string strErr)
        {

            string strBigClassCode = string.Format(@"select bigclassCode 
                                                        from s_bigclass_event
                                                        where char(id) ='{0}'", bigid);
            try
            {
                string bigClassCode = Convert.ToString(this.ExecuteScalar(strBigClassCode));
                if (bigClassCode.Length > 0)
                {
                    string strSQL = string.Format(@"insert into s_smallclass_event(bigclassCode,smallcallCode,name,
								                                            t_time_kc,t_time,t_time_ts,
								                                            rolecode,dutyunit)
                                            values('{0}','{1}','{2}',
                                                        {3},{4},{5},
                                                        {6},'{7}') ", bigClassCode, smallCode, className, kc, limit, zh, rolecode, dutyunit);
                    return this.ExecuteNonQuery(strSQL);
                }
                return 0;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateEvent：更新事件信息
        /// <summary>
        /// 更新事件信息
        /// </summary>
        /// <param name="bigId">事件编码</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int UpateEvent(string bigId, string bigclassCode, string eventName, ref string strErr)
        {
            string strSQL = "update s_bigclass_event set name='" + eventName + "',bigclassCode='" + bigclassCode + "' where id = " + bigId ;
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateSmallEvent：更新小类事件信息
        /// <summary>
        /// 更新小类事件信息
        /// </summary>
        /// <param name="bigid">大类编码</param>
        /// <param name="classCode">小类编码</param>
        /// <param name="className">小类名称</param>
        /// <param name="limit">时限</param>
        /// <param name="dutyunit">责任单位</param>
        /// <param name="kc">勘查时限</param>
        /// <param name="zh">综合处理时限</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateSmallEvent(string smallid, string smallcode, string bigid, string className, int limit,
                                        string dutyunit, int kc, int zh,
                                        ref string strErr)
        {
            string strSQL = string.Format(@"update s_smallclass_event set bigclassCode ='{0}',smallcallCode='{1}',name='{2}',
								                    t_time_kc={3},t_time={4},t_time_ts={5},dutyunit='{6}'
                                            where id ={7}", bigid, smallcode, className, kc, limit, zh, dutyunit, smallid);
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetBigPartTreeData：获取大类部件树数据
        /// <summary>
        /// 获取大类部件树数据
        /// </summary>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String[] GetBigPartTreeData(ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select id,bigclasscode,name from s_bigclass_part";
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["id"]) + ",");
                    sb.Append(Convert.ToString(dr["bigclasscode"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallPartTreeData：获取小类部件树数据
        /// <summary>
        /// 获取小类部件树数据
        /// </summary>
        /// <param name="fid">大类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns>小类部件id和名称</returns>
        public String[] GetSmallPartTreeData(string fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.name 
                                            from s_smallclass_part a left join s_bigclass_part b
                                            on a.bigclassCode = b.bigclassCode
                                            where char(b.id)='{0}'", fid);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["id"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取小类部件树数据
        /// </summary>
        /// <param name="id">大类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns>小类部件自定义编码和名称</returns>
        public String[] GetSmallPartTreeData(int id, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.smallcallCode,a.name 
                                            from s_smallclass_part a left join s_bigclass_part b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id={0}", id);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["smallcallCode"]) + ",");
                    sb.Append(Convert.ToString(dr["name"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetBigPartInfo：根据大类编码，获取大类部件信息
        /// <summary>
        /// 根据大类编码，获取大类部件信息
        /// </summary>
        /// <param name="bigId">大类id</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String GetBigPartInfo(string bigId, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select name,bigclassCode  
                                            from s_bigclass_part
                                            where char(id) = '{0}'", bigId);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    retValue = dr["name"].ToString() + "," + dr["bigclassCode"].ToString();
                }

                dr.Close();
                return retValue;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallPartInfo：根据小类编码，获取小类部件信息
        /// <summary>
        /// 根据小类编码，获取小类部件信息
        /// </summary>
        /// <param name="smallId">小类编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public Hashtable GetSmallPartInfo(string smallId, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select B.smallcallCode as smallCode,B.name as sname,B.bigclasscode as bigid,A.name as bname,
                                                    B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,
                                                    B.url as photourl,B.t_time_kc as kancha,B.t_time_gc as gongcheng,
                                                    B.t_time_ts as teshu  
                                            from s_smallclass_part B left join s_bigclass_part A
                                            on B.bigclassCode=A.bigclassCode
                                            where char(B.id)='{0}'", smallId);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    string scode = dr["smallCode"].ToString();
                    table.Add("scode", scode);
                    string sname = Convert.ToString(dr["sname"]);
                    table.Add("sname", sname);
                    string bigid = Convert.ToString(dr["bigid"]);
                    table.Add("bigid", bigid);
                    string bname = Convert.ToString(dr["bname"]);
                    table.Add("bname", bname);
                    string timelimit = Convert.ToString(dr["timelimit"]);
                    table.Add("timelimit", timelimit);
                    string rolecode = Convert.ToString(dr["rolecode"]);
                    table.Add("rolecode", rolecode);
                    string dutyunit = Convert.ToString(dr["dutyunit"]);
                    table.Add("dutyunit", dutyunit);
                    string photourl = Convert.ToString(dr["photourl"]);
                    table.Add("photourl", photourl);
                    string kancha = Convert.ToString(dr["kancha"]);
                    table.Add("kancha", kancha);
                    string gongcheng = Convert.ToString(dr["gongcheng"]);
                    table.Add("gongcheng", gongcheng);
                    string teshu = Convert.ToString(dr["teshu"]);
                    table.Add("teshu", teshu);
                }
                dr.Close();
                if (table["dutyunit"].ToString().Equals(""))
                {
                    table.Add("departname", "");
                }
                else
                {
                    string ids = table["dutyunit"].ToString();
                    String sql = "select departname from p_depart where departcode in(" + ids + ")";
                    dr = this.ExecuteReader(sql);
                    string names = "";
                    while (dr.Read())
                    {
                        names += dr["departname"].ToString() + ",";
                    }
                    if (names.Length > 0)
                    {
                        names = names.Substring(0, names.Length - 1);
                    }
                    table.Add("departname", names);
                    dr.Close();
                }
                return table;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeletePart：删除部件数据
        /// <summary>
        /// 删除部件数据
        /// </summary>
        /// <param name="partId">部件id</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int DeletePart(string partId, ref string strErr)
        {
            string strSQL = "delete from s_bigclass_part where char(id) = '" + partId + "'";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region DeleteSmallPart：删除部件小类数据
        /// <summary>
        /// 删除部件小类数据
        /// </summary>
        /// <param name="smallpartId">小类事件id</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int DeleteSmallPart(string smallpartId, ref string strErr)
        {
            string strSQL = "delete from s_smallclass_part where char(id) = '" + smallpartId + "'";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckPart：判断部件是否已经存在
        /// <summary>
        /// 判断部件是否已经存在
        /// </summary>
        /// <param name="partName">部件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckPart(string partName, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_part where name ='" + partName + "'";
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

        #region InsertPart：插入部件信息
        /// <summary>
        /// 插入部件信息
        /// </summary>
        /// <param name="partCode">部件编码</param>
        /// <param name="partName">部件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int InsertPart(string partCode, string partName, ref string strErr)
        {
            string strSQL = "insert into s_bigclass_part (bigclassCode,name) values('" + partCode + "','" + partName + "') ";
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckSmallPartCode：判断小类部件是否已经存在
        /// <summary>
        /// 判断小类部件是否已经存在
        /// </summary>
        /// <param name="smallPartCode">小类部件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckSmallPartCode(string smallPartCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_part where name ='" + smallPartCode + "'";
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

        #region InsertSmallPart：插入小类部件信息
        /// <summary>
        /// 插入小类部件信息
        /// </summary>
        /// <param name="bigid">大类id</param>
        /// <param name="classCode">小类编码</param>
        /// <param name="className">小类名称</param>
        /// <param name="limit">时限</param>
        /// <param name="rolecode">角色</param>
        /// <param name="dutyunit">责任单位</param>
        /// <param name="kc">勘查时限</param>
        /// <param name="zh">综合处理时限</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>bigid, className, limit, 0, _unit, photoUrl, kc, gc, ts, ref strErr
        public int InsertSmallPart(int bigid, string classCode, string className, int limit,
                                        int rolecode, string dutyunit, string photourl,
                                        int kc, int gc, int ts, ref string strErr)
        {
            string strBigClassCode = string.Format(@"select bigclassCode 
                                                        from s_bigclass_part
                                                        where char(id) ='{0}'", bigid);
            try
            {
                string bigClassCode = Convert.ToString(this.ExecuteScalar(strBigClassCode));
                if (bigClassCode.Length > 0)
                {
                    string strSQL = string.Format(@"insert into s_smallclass_part (bigclassCode,smallcallCode,name,
                                                                                        t_time,rolecode,dutyunit,
								                                                        url,t_time_kc,t_time_gc,t_time_ts)
                                                        values('{0}','{1}','{2}',
		                                                        {3},{4},'{5}',
		                                                        '{6}',{7},{8},{9})", bigClassCode, classCode, className, limit, rolecode, dutyunit, photourl, kc, gc, ts);
                    return this.ExecuteNonQuery(strSQL);
                }
                return 0;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpatePart：更新部件信息
        /// <summary>
        /// 更新部件信息
        /// </summary>
        /// <param name="bigId">部件id</param>
        /// <param name="partCode">部件编码</param>
        /// <param name="partName">事件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int UpatePart(string bigId, string partCode, string partName, ref string strErr)
        {
            string strSQL = string.Format(@"update s_bigclass_part set name='{0}',bigclassCode='{1}' where id={2}", partName, partCode, bigId);
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateSmallPart：更新小类部件信息
        /// <summary>
        /// 更新小类部件信息
        /// </summary>
        /// <param name="smallid">小类id</param>
        /// <param name="classCode">小类编码</param>
        /// <param name="className">小类名称</param>
        /// <param name="limit">时限</param>
        /// <param name="rolecode">角色</param>
        /// <param name="dutyunit">责任单位</param>
        /// <param name="kc">勘查时限</param>
        /// <param name="zh">综合处理时限</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateSmallPart(int smallid, string classCode, string className, int limit,
                                    int rolecode, string dutyunit, string photourl,
                                    int kc, int gc, int ts, ref string strErr)
        {
            string strSQL = string.Format(@"update s_smallclass_part set smallcallCode='{0}',name='{1}',t_time={2},rolecode={3},
							                        dutyunit='{4}',url='{5}',t_time_kc={6},
							                        t_time_gc={7},t_time_ts={8} 
                                            where id={9}", classCode, className, limit, rolecode, dutyunit, photourl, kc, gc, ts, smallid);
            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckPartCode：判断部件编号是否已经存在
        /// <summary>
        /// 判断部件编号是否已经存在
        /// </summary>
        /// <param name="partCode">部件名称</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int CheckPartCode(string partCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_part where bigclassCode ='" + partCode + "'";
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

        #region GetPhaseTreeData：根据阶段(接线员,值班长,派遣员),获取阶段树数据
        /// <summary>
        /// 根据阶段(接线员,值班长,派遣员),获取阶段树数据
        /// </summary>
        /// <param name="stepid">阶段编号</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String[] GetPhaseTreeData(int stepid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select id,actiontext
                                            from s_ActionTimeLimit
                                            where stepid = {0}", stepid);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                while (dr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Convert.ToString(dr["id"]) + ",");
                    sb.Append(Convert.ToString(dr["actiontext"]));
                    list.Add(sb.ToString());
                }

                dr.Close();
                return (String[])(list.ToArray(System.Type.GetType("System.String")));
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetPhaseInfo：根据阶段id，获取阶段信息
        /// <summary>
        /// 根据阶段id，获取阶段信息
        /// </summary>
        /// <param name="id">阶段id</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public String GetPhaseInfo(int id, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select actiontext,
                                                coalesce(t_time,0) as t_time,
                                                coalesce(IsLimitWorkHours,0) as IsLimitWorkHours,
                                                convert(varchar(19),StartWorkHours,120) as StartWorkHours,
                                                convert(varchar(19),EndWorkHours,120) as EndWorkHours
                                            from s_actiontimelimit
                                            where id = {0}", id);
            try
            {
                IDataReader dr = this.ExecuteReader(strSQL);
                if (dr.Read())
                {
                    retValue = dr["actiontext"].ToString() + "," + dr["t_time"].ToString() + "," + dr["IsLimitWorkHours"].ToString() + "," + dr["StartWorkHours"].ToString() + "," + dr["EndWorkHours"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }

            return retValue;
        }
        #endregion

        #region UpatePhase：更新阶段信息
        /// <summary>
        /// 更新阶段信息
        /// </summary>
        /// <param name="bigId">阶段编号</param>
        /// <param name="eventName">阶段时限</param>
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public int UpatePhase(string id, string t_time, bool limit, string start, string end, ref string strErr)
        {
            start = DateTime.Now.ToShortDateString() + " " + start;
            end = DateTime.Now.ToShortDateString() + " " + end;
            string strSQL = "update s_actiontimelimit set t_time=" + t_time + ",IsLimitWorkHours=" + limit + ",StartWorkHours='" + start + "',EndWorkHours = '" + end + "' where id=" + id + "";

            try
            {
                return this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region  获取小类事部件信息DataTable
        public DataTable GetSmallClassPartTable(string smallPartID, string parentCode)
        {
            string strSQL = @"select B.*,A.name as BigPartName
                                            from s_smallclass_part B left join s_bigclass_part A
                                            on B.bigclassCode=A.bigclassCode
                                            where 1=1 ";
            if (!string.IsNullOrEmpty(smallPartID))
                strSQL += " and char(B.SmallCallCode)='" + smallPartID + "'";
            if (!string.IsNullOrEmpty(parentCode))
                strSQL += " and char(A.BigClassCode)='" + parentCode + "'";

            DataSet ds = this.ExecuteDataset(strSQL);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }


        public DataTable GetSmallClassEventTable(string smallEventID, string parentCode)
        {
            string strSQL = @"select B.*,A.name as BigEventName
                                            from s_smallclass_event B left join s_bigclass_event A
                                            on B.bigclassCode=A.bigclassCode
                                            where 1=1 ";
            if (!string.IsNullOrEmpty(smallEventID))
                strSQL += " and char(B.smallcallcode)='" + smallEventID + "'";
            if (!string.IsNullOrEmpty(parentCode))
                strSQL += " and char(A.BigClassCode)='" + parentCode + "'";

            DataSet ds = this.ExecuteDataset(strSQL);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion
    }
}
