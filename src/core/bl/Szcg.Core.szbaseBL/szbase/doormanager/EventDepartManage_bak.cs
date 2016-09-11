using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using bacgDL.szbase.doormanager;

namespace bacgBL.web.szbase.doormanager
{
    public class EventDepartManage_bak
    {
        #region GetBigEventTreeData����ȡ�����¼�������
        /// <summary>
        /// ��ȡ�����¼�������
        /// </summary>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public String[] GetBigEventTreeData(ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select id,bigclasscode,name from s_bigclass_event";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallEventTreeData����ȡС���¼�������
        /// <summary>
        /// ��ȡС���¼�������
        /// </summary>
        /// <param name="id">�������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns>С���¼�ID������</returns>
        public String[] GetSmallEventTreeData(string fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.name 
                                            from s_smallclass_event a left join s_bigclass_event b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id='{0}'", fid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ȡС���¼�������
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns>С���¼��Զ�����������</returns>
        public String[] GetSmallEventTreeData(int id, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.smallcallCode,a.name 
                                            from s_smallclass_event a left join s_bigclass_event b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id={0}", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region :��ȡС���¼�ϸ����Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public String[] GetSmallXZEventTreeData(int fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.name 
                                            from s_info_event a left join s_smallclass_event b
                                            on a.smallclasscode = b.smallcallCode
                                            where b.id='{0}'", fid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        public String[] GetSmallXZEventTreeData(string fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.eventinfocode,a.name 
                                            from s_info_event a left join s_smallclass_event b
                                            on a.smallclasscode = b.smallcallCode
                                            where a.smallclasscode='{0}'", fid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(Convert.ToString(dr["eventinfocode"]) + ",");
                        sb.Append(Convert.ToString(dr["name"]));
                        list.Add(sb.ToString());
                    }

                    dr.Close();
                    return (String[])(list.ToArray(System.Type.GetType("System.String")));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetBigEventInfo�����ݴ�����룬��ȡ�����¼���Ϣ
        /// <summary>
        /// ���ݴ�����룬��ȡ�����¼���Ϣ
        /// </summary>
        /// <param name="bigId">�������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public String GetBigEventInfo(string bigId, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select bigclassCode,name 
                                            from s_bigclass_event
                                            where id = '{0}'", bigId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        retValue = dr["name"].ToString() + "," + dr["bigclassCode"].ToString();
                    }

                    dr.Close();
                    return retValue;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallEventInfo������С����룬��ȡС���¼���Ϣ
        /// <summary>
        /// ����С����룬��ȡС���¼���Ϣ
        /// </summary>
        /// <param name="smallId">С�����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public Hashtable GetSmallEventInfo(string smallId, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select B.smallcallCode as smallCode,B.name as sname,A.name as bname,A.bigclassCode,
		                                            B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,
		                                            B.t_time_kc as kancha,B.t_time_ts as zonghe,t1time,t1name,t2time,t2name,t3time,t3name
                                                    ,t4time,t4name,t5time,t5name,t6time,t6name
                                            from s_smallclass_event B left join s_bigclass_event A
                                            on B.bigclassCode=A.bigclassCode
                                            where B.id='{0}'", smallId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
                        string dutyunit = Pub.Tools.changeNull(Convert.ToString(dr["dutyunit"]));
                        table.Add("dutyunit", dutyunit);
                        string kc = Convert.ToString(dr["kancha"]);
                        table.Add("kancha", kc);
                        string zh = Convert.ToString(dr["zonghe"]);
                        table.Add("zonghe", zh);

                        //��������1
                        string t1time = Convert.ToString(dr["t1time"]);
                        table.Add("t1time", t1time);
                        string t1name = Convert.ToString(dr["t1name"]);
                        table.Add("t1name", t1name);
                        //��������2
                        string t2time = Convert.ToString(dr["t2time"]);
                        table.Add("t2time", t2time);
                        string t2name = Convert.ToString(dr["t2name"]);
                        table.Add("t2name", t2name);
                        //��������3
                        string t3time = Convert.ToString(dr["t3time"]);
                        table.Add("t3time", t3time);
                        string t3name = Convert.ToString(dr["t3name"]);
                        table.Add("t3name", t3name);
                        //��������4
                        string t4time = Convert.ToString(dr["t4time"]);
                        table.Add("t4time", t4time);
                        string t4name = Convert.ToString(dr["t4name"]);
                        table.Add("t4name", t4name);
                        //��������5
                        string t5time = Convert.ToString(dr["t5time"]);
                        table.Add("t5time", t5time);
                        string t5name = Convert.ToString(dr["t5name"]);
                        table.Add("t5name", t5name);
                        //��������6
                        string t6time = Convert.ToString(dr["t6time"]);
                        table.Add("t6time", t6time);
                        string t6name = Convert.ToString(dr["t6name"]);
                        table.Add("t6name", t6name);
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
                        dr = dl.ExecReaderSql(sql);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallEventInfo������С����룬��ȡС���¼���Ϣ
        /// <summary>
        /// ����С����룬��ȡС���¼���Ϣ
        /// </summary>
        /// <param name="smallId">С�����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public Hashtable GetSmallXZEventInfo(string smallXZId, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select a.bigclasscode as bigcode,a.smallclasscode as smallcode,a.eventinfocode as eventinfocode,a.name as name,a.markNum as markNum
                                            from s_smallclass_event B left join s_info_event A 
                                            on B.smallcallcode=A.smallclassCode
                                            where A.id='{0}'", smallXZId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string bcode = dr["bigcode"].ToString();
                        table.Add("bcode", bcode);
                        string scode = dr["smallCode"].ToString();
                        table.Add("scode", scode);
                        string code = Convert.ToString(dr["eventinfocode"]);
                        table.Add("code", code);
                        string name = Convert.ToString(dr["name"]);
                        table.Add("name", name);
                        string markNum = Convert.ToString(dr["markNum"]);
                        table.Add("markNum", markNum);

                    }
                    dr.Close();
                    //if (table["dutyunit"].ToString().Equals(""))
                    //{
                    //    table.Add("departname", "");
                    //}
                    //else
                    //{
                    //    string ids = table["dutyunit"].ToString();
                    //    String sql = "select departname from p_depart where departcode in(" + ids + ")";
                    //    dr = dl.ExecReaderSql(sql);
                    //    string names = "";
                    //    while (dr.Read())
                    //    {
                    //        names += dr["departname"].ToString() + ",";
                    //    }
                    //    if (names.Length > 0)
                    //    {
                    //        names = names.Substring(0, names.Length - 1);
                    //    }
                    //    table.Add("departname", names);
                    //    dr.Close();
                    //}
                    return table;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteEvent��ɾ���¼�����
        /// <summary>
        /// ɾ���¼�����
        /// </summary>
        /// <param name="eventId">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int DeleteEvent(string eventId, ref string strErr)
        {
            string strSQL = "delete s_bigclass_event where id = '" + eventId + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region DeleteSmallEvent��ɾ���¼�С������
        /// <summary>
        /// ɾ���¼�С������
        /// </summary>
        /// <param name="smalleventId">С���¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int DeleteSmallEvent(string smalleventId, ref string strErr)
        {
            string strSQL = "delete s_smallclass_event where id = '" + smalleventId + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region DeleteSmallXZEvent��ɾ���¼�С������
        /// <summary>
        /// ɾ���¼�С������
        /// </summary>
        /// <param name="smalleventId">С���¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int DeleteSmallXZEvent(string id, ref string strErr)
        {
            string strSQL = "delete s_info_event where id = '" + id + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region CheckEvent���ж��¼��Ƿ��Ѿ�����
        /// <summary>
        /// �ж��¼��Ƿ��Ѿ�����
        /// </summary>
        /// <param name="eventName">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckEvent(string eventName, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_event where name ='" + eventName + "'";
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

        #region �ж��¼�ϸ���Ƿ��Ѿ�����
        public int CheckXZ(string code, ref string strErr)
        {
            string strSQL = "select count(*) from s_info_event where eventinfocode ='" + code + "'";
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

        #region CheckEventCode���ж��¼�����Ƿ��Ѿ�����
        /// <summary>
        /// �ж��¼�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="eventCode">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckEventCode(string eventCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_event where bigclassCode ='" + eventCode + "'";
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

        #region InsertEvent�������¼���Ϣ
        /// <summary>
        /// �����¼���Ϣ
        /// </summary>
        /// <param name="eventCode">�¼����</param>
        /// <param name="eventName">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int InsertEvent(string bigclassCode, string eventName, ref string strErr)
        {
            string strSQL = "insert into s_bigclass_event (bigclassCode,name) values('" + bigclassCode + "','" + eventName + "') ";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region CheckSmallEvent���ж�С���¼��Ƿ��Ѿ�����
        /// <summary>
        /// �ж�С���¼��Ƿ��Ѿ�����
        /// </summary>
        /// <param name="smallEventName">С���¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckSmallEvent(string smallEventName, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_event where name ='" + smallEventName + "'";
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

        #region CheckSmallCode���ж�С���¼�����Ƿ��Ѿ�����
        /// <summary>
        /// �ж�С���¼�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="smallEventName">С���¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckSmallCode(string smallEventCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_event where smallcallCode ='" + smallEventCode + "'";
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

        #region InsertSmallEvent������С���¼���Ϣ
        /// <summary>
        /// ����С���¼���Ϣ
        /// </summary>
        /// <param name="bigid">�������</param>
        /// <param name="classCode">С�����</param>
        /// <param name="className">С������</param>
        /// <param name="limit">ʱ��</param>
        /// <param name="rolecode">��ɫ</param>
        /// <param name="dutyunit">���ε�λ</param>
        /// <param name="kc">����ʱ��</param>
        /// <param name="zh">�ۺϴ���ʱ��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int InsertSmallEvent(int bigid, string smallCode, string className, int limit,
                                        int rolecode, string dutyunit, int kc,int zh,string t1name,int t1time,string t2name,int t2time,string t3name,int t3time,string t4name,int t4time,
                                       ref string strErr)
        {

            string strBigClassCode = string.Format(@"select bigclassCode 
                                                        from s_bigclass_event
                                                        where id ='{0}'", bigid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    string bigClassCode = Convert.ToString(dl.ExecScalarSql(strBigClassCode));
                    if (bigClassCode.Length > 0)
                    {
                        string strSQL = string.Format(@"insert into s_smallclass_event(bigclassCode,smallcallCode,name,
								                                            t_time_kc,t_time,t_time_ts,
								                                            rolecode,dutyunit,t1name,t1time,t2name,t2time,t3name,t3time,t4name,t4time)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}') ",
                                            bigClassCode, smallCode, className, kc, limit, zh, rolecode, dutyunit, t1name, t1time, t2name, t2time, t3name, t3time, t4name, t4time);
                        return dl.ExecNonQuerySql(strSQL);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region:������С��ϸ����Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bigcode"></param>
        /// <param name="smallCode"></param>
        /// <param name="eventCode"></param>
        /// <param name="className"></param>
        /// <param name="num"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public int InsertSmallXZEvent(string bigcode, string smallCode,string eventCode, string className, int num,ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    
                        string strSQL = string.Format(@"insert into s_info_event(bigclassCode,smallclassCode,eventinfocode,name )
                                            values('{0}','{1}','{2}','{3}') ",bigcode,smallCode,eventCode,className);
                        return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region:������С��ϸ����Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bigcode"></param>
        /// <param name="smallCode"></param>
        /// <param name="eventCode"></param>
        /// <param name="className"></param>
        /// <param name="num"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public int UpdateSmallXZEvent( string id,string eventCode, string className, ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    string strSQL = string.Format(@"update  s_info_event set eventinfocode='{0}',name='{1}' where id={2}
					                       ",  eventCode, className,id);
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
        #region UpateEvent�������¼���Ϣ
        /// <summary>
        /// �����¼���Ϣ
        /// </summary>
        /// <param name="bigId">�¼�����</param>
        /// <param name="eventName">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int UpateEvent(string bigId, string bigclassCode, string eventName, ref string strErr)
        {
            string strSQL = "update s_bigclass_event set name='" + eventName + "',bigclassCode='" + bigclassCode + "' where id = '" + bigId + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region UpateSmallEvent������С���¼���Ϣ
        /// <summary>
        /// ����С���¼���Ϣ
        /// </summary>
        /// <param name="bigid">�������</param>
        /// <param name="classCode">С�����</param>
        /// <param name="className">С������</param>
        /// <param name="limit">ʱ��</param>
        /// <param name="dutyunit">���ε�λ</param>
        /// <param name="kc">����ʱ��</param>
        /// <param name="zh">�ۺϴ���ʱ��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int UpateSmallEvent(string smallid, string smallcode, string bigid, string className, int limit,
                                        string dutyunit, int kc, int zh, string t1name, int t1time, string t2name, int t2time, string t3name, int t3time, string t4name, int t4time,
                                        ref string strErr)
        {
            string strSQL = string.Format(@"update s_smallclass_event set bigclassCode ='{0}',smallcallCode='{1}',name='{2}',
								                    t_time_kc='{3}',t_time='{4}',t_time_ts='{5}',dutyunit='{6}',t1name='{7}',t1time='{8}',t2name='{10}',t2time='{11}',t3name='{12}',t3time='{13}',t4name='{14}',t4time='{15}'
                                            where id ='{9}'", bigid, smallcode, className, kc, limit, zh, dutyunit,t1name,t1time, smallid,t2name,t2time,t3name,t3time,t4name,t4time);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region GetBigPartTreeData����ȡ���ಿ��������
        /// <summary>
        /// ��ȡ���ಿ��������
        /// </summary>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public String[] GetBigPartTreeData(ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select id,bigclasscode,name from s_bigclass_part";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallPartTreeData����ȡС�ಿ��������
        /// <summary>
        /// ��ȡС�ಿ��������
        /// </summary>
        /// <param name="fid">�������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns>С�ಿ��id������</returns>
        public String[] GetSmallPartTreeData(string fid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.name 
                                            from s_smallclass_part a left join s_bigclass_part b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id='{0}'", fid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ȡС�ಿ��������
        /// </summary>
        /// <param name="id">�������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns>С�ಿ���Զ�����������</returns>
        public String[] GetSmallPartTreeData(int id, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select a.smallcallCode,a.name 
                                            from s_smallclass_part a left join s_bigclass_part b
                                            on a.bigclassCode = b.bigclassCode
                                            where b.id={0}", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetBigPartInfo�����ݴ�����룬��ȡ���ಿ����Ϣ
        /// <summary>
        /// ���ݴ�����룬��ȡ���ಿ����Ϣ
        /// </summary>
        /// <param name="bigId">����id</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public String GetBigPartInfo(string bigId, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select name,bigclassCode  
                                            from s_bigclass_part
                                            where id = '{0}'", bigId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        retValue = dr["name"].ToString() + "," + dr["bigclassCode"].ToString();
                    }

                    dr.Close();
                    return retValue;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetSmallPartInfo������С����룬��ȡС�ಿ����Ϣ
        /// <summary>
        /// ����С����룬��ȡС�ಿ����Ϣ
        /// </summary>
        /// <param name="smallId">С�����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public Hashtable GetSmallPartInfo(string smallId, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select B.smallcallCode as smallCode,B.name as sname,B.bigclasscode as bigid,A.name as bname,
                                                    B.t_time as timelimit,B.rolecode as rolecode,B.dutyunit as dutyunit,
                                                    B.url as photourl,B.t_time_kc as kancha,B.t_time_gc as gongcheng,
                                                    B.t_time_ts as teshu,t1time,t1name,t2time,t2name,t3time,t3name
                                                    ,t4time,t4name,t5time,t5name,t6time,t6name
                                            from s_smallclass_part B left join s_bigclass_part A
                                            on B.bigclassCode=A.bigclassCode
                                            where B.id='{0}'", smallId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
                        //��������1
                        string t1time = Convert.ToString(dr["t1time"]);
                        table.Add("t1time", t1time);
                        string t1name = Convert.ToString(dr["t1name"]);
                        table.Add("t1name", t1name);
                        //��������2
                        string t2time = Convert.ToString(dr["t2time"]);
                        table.Add("t2time", t2time);
                        string t2name = Convert.ToString(dr["t2name"]);
                        table.Add("t2name", t2name);
                        //��������3
                        string t3time = Convert.ToString(dr["t3time"]);
                        table.Add("t3time", t3time);
                        string t3name = Convert.ToString(dr["t3name"]);
                        table.Add("t3name", t3name);
                        //��������4
                        string t4time = Convert.ToString(dr["t4time"]);
                        table.Add("t4time", t4time);
                        string t4name = Convert.ToString(dr["t4name"]);
                        table.Add("t4name", t4name);
                        //��������5
                        string t5time = Convert.ToString(dr["t5time"]);
                        table.Add("t5time", t5time);
                        string t5name = Convert.ToString(dr["t5name"]);
                        table.Add("t5name", t5name);
                        //��������6
                        string t6time = Convert.ToString(dr["t6time"]);
                        table.Add("t6time", t6time);
                        string t6name = Convert.ToString(dr["t6name"]);
                        table.Add("t6name", t6name);
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
                        dr = dl.ExecReaderSql(sql);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeletePart��ɾ����������
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="partId">����id</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int DeletePart(string partId, ref string strErr)
        {
            string strSQL = "delete s_bigclass_part where id = '" + partId + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region DeleteSmallPart��ɾ������С������
        /// <summary>
        /// ɾ������С������
        /// </summary>
        /// <param name="smallpartId">С���¼�id</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int DeleteSmallPart(string smallpartId, ref string strErr)
        {
            string strSQL = "delete s_smallclass_part where id = '" + smallpartId + "'";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region CheckPart���жϲ����Ƿ��Ѿ�����
        /// <summary>
        /// �жϲ����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="partName">��������</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
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

        #region InsertPart�����벿����Ϣ
        /// <summary>
        /// ���벿����Ϣ
        /// </summary>
        /// <param name="partCode">��������</param>
        /// <param name="partName">��������</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int InsertPart(string partCode, string partName, ref string strErr)
        {
            string strSQL = "insert into s_bigclass_part (bigclassCode,name) values('" + partCode + "','" + partName + "') ";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region CheckSmallPartCode���ж�С�ಿ���Ƿ��Ѿ�����
        /// <summary>
        /// �ж�С�ಿ���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="smallPartCode">С�ಿ������</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckSmallPartCode(string smallPartCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_smallclass_part where name ='" + smallPartCode + "'";
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

        #region InsertSmallPart������С�ಿ����Ϣ
        /// <summary>
        /// ����С�ಿ����Ϣ
        /// </summary>
        /// <param name="bigid">����id</param>
        /// <param name="classCode">С�����</param>
        /// <param name="className">С������</param>
        /// <param name="limit">ʱ��</param>
        /// <param name="rolecode">��ɫ</param>
        /// <param name="dutyunit">���ε�λ</param>
        /// <param name="kc">����ʱ��</param>
        /// <param name="zh">�ۺϴ���ʱ��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>bigid, className, limit, 0, _unit, photoUrl, kc, gc, ts, ref strErr
        public int InsertSmallPart(int bigid, string classCode, string className, int limit,
                                        int rolecode, string dutyunit, string photourl,
                                        int kc, int gc, int ts,
                                       string t1name, int t1time, string t2name, int t2time, string t3name, int t3time,
            string t4name, int t4time, string t5name, int t5time, string t6name, int t6time, ref string strErr)
        {
            string strBigClassCode = string.Format(@"select bigclassCode 
                                                        from s_bigclass_part
                                                        where id ='{0}'", bigid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    string bigClassCode = Convert.ToString(dl.ExecScalarSql(strBigClassCode));
                    if (bigClassCode.Length > 0)
                    {
                        string strSQL = string.Format(@"insert into s_smallclass_part (bigclassCode,smallcallCode,name,t_time,
								                                                        rolecode,dutyunit,url,
								                                                        t_time_kc,t_time_gc,t_time_ts,
                                                                                        t1name,t1time,t2name,t2time,t3name,t3time,
                                                                                        t4name,t4time,t5name,t5time,t6name,t6time)
                                                        values('{0}','{1}','{2}',
		                                                        '{3}','{4}','{5}',
		                                                        '{6}','{7}','{8}','{9}'
                                                                , '{10}','{11}','{12}','{13}', '{14}','{15}'
                                                                 , '{16}','{17}','{18}','{19}', '{20}','{21}')", bigClassCode, classCode, className, limit, rolecode, dutyunit, photourl, kc, gc, ts, t1name, t1time, t2name, t2time, t3name, t3time,
                                                                                        t4name, t4time, t5name, t5time, t6name, t6time);
                        return dl.ExecNonQuerySql(strSQL);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpatePart�����²�����Ϣ
        /// <summary>
        /// ���²�����Ϣ
        /// </summary>
        /// <param name="bigId">����id</param>
        /// <param name="partCode">��������</param>
        /// <param name="partName">�¼�����</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int UpatePart(string bigId, string partCode, string partName, ref string strErr)
        {
            string strSQL = string.Format(@"update s_bigclass_part set name='{0}',bigclassCode='{1}' where id='{2}'", partName, partCode, bigId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region UpateSmallPart������С�ಿ����Ϣ
        /// <summary>
        /// ����С�ಿ����Ϣ
        /// </summary>
        /// <param name="smallid">С��id</param>
        /// <param name="classCode">С�����</param>
        /// <param name="className">С������</param>
        /// <param name="limit">ʱ��</param>
        /// <param name="rolecode">��ɫ</param>
        /// <param name="dutyunit">���ε�λ</param>
        /// <param name="kc">����ʱ��</param>
        /// <param name="zh">�ۺϴ���ʱ��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int UpateSmallPart(int smallid, string classCode, string className, int limit,
                                    int rolecode, string dutyunit, string photourl,
                                    int kc, int gc, int ts,
             string t1name, int t1time, string t2name, int t2time, string t3name, int t3time,
            string t4name, int t4time, string t5name, int t5time, string t6name, int t6time, ref string strErr)
        {
            string strSQL = string.Format(@"update s_smallclass_part set smallcallCode='{0}',name='{1}',t_time='{2}',rolecode='{3}',
							                        dutyunit='{4}',url='{5}',t_time_kc='{6}',
							                        t_time_gc='{7}',t_time_ts='{8}' 
                                                    ,t1name='{9}',t1time='{10}' ,t2name='{11}',t2time='{12}' ,t3name='{13}',t3time='{14}' 
                                                    ,t4name='{15}',t4time='{16}' ,t5name='{17}',t5time='{18}' ,t6name='{19}',t6time='{20}' 
                                            where id='{21}'", classCode, className, limit, rolecode, dutyunit, photourl, kc, gc, ts, t1name, t1time, t2name, t2time, t3name, t3time, t4name, t4time, t5name, t5time, t6name, t6time, smallid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region CheckPartCode���жϲ�������Ƿ��Ѿ�����
        /// <summary>
        /// �жϲ�������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="partCode">��������</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int CheckPartCode(string partCode, ref string strErr)
        {
            string strSQL = "select count(*) from s_bigclass_part where bigclassCode ='" + partCode + "'";
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

        #region GetPhaseTreeData�����ݽ׶�(����Ա,ֵ�೤,��ǲԱ),��ȡ�׶�������
        /// <summary>
        /// ���ݽ׶�(����Ա,ֵ�೤,��ǲԱ),��ȡ�׶�������
        /// </summary>
        /// <param name="stepid">�׶α��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public String[] GetPhaseTreeData(int stepid, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Format(@"select id,actiontext
                                            from s_ActionTimeLimit
                                            where stepid = '{0}'", stepid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetPhaseInfo�����ݽ׶�id����ȡ�׶���Ϣ
        /// <summary>
        /// ���ݽ׶�id����ȡ�׶���Ϣ
        /// </summary>
        /// <param name="id">�׶�id</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public static String GetPhaseInfo(int id, ref string strErr)
        {
            string retValue = "";
            string strSQL = string.Format(@"select actiontext,
                                                isnull(t_time,0) as t_time,
                                                isnull(IsLimitWorkHours,0) as IsLimitWorkHours,
                                                convert(varchar(19),StartWorkHours,120) as StartWorkHours,
                                                convert(varchar(19),EndWorkHours,120) as EndWorkHours
                                            from s_actiontimelimit
                                            where id = '{0}'", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    IDataReader dr = dl.ExecuteReader(strSQL);
                    if (dr.Read())
                    {
                        retValue = dr["actiontext"].ToString() + "," + dr["t_time"].ToString() + "," + dr["IsLimitWorkHours"].ToString() + "," + dr["StartWorkHours"].ToString() + "," + dr["EndWorkHours"].ToString();
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }

            return retValue;
        }
        #endregion

        #region GetWorkTime����ȡ����ʱ����Ϣ
        /// <summary>
        /// ��ȡ����ʱ����Ϣ
        /// </summary>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public static String GetWorkTime(ref string strErr)
        {
            string retValue = "";
            string Mwork = "";
            string Fwork ="";
            string strSQL = "select  *  from tb_worktime ";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    IDataReader dr = dl.ExecuteReader(strSQL);
                   while(dr.Read())
                    {
                        if (Convert.ToInt32(dr["id"]) == 1)
                            Mwork = dr["id"].ToString() + "," + Convert.ToDateTime(dr["time_start"]).ToString("yyyy-MM-dd HH:mm:ss") + "," + Convert.ToDateTime(dr["time_end"]).ToString("yyyy-MM-dd HH:mm:ss");
                        else if (Convert.ToInt32(dr["id"]) == 2)
                            Fwork = dr["id"].ToString() + "," + Convert.ToDateTime(dr["time_start"]).ToString("yyyy-MM-dd HH:mm:ss") + "," + Convert.ToDateTime(dr["time_end"]).ToString("yyyy-MM-dd HH:mm:ss");
                   }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            retValue=Mwork+","+Fwork;
            return retValue;
        }
        #endregion

        #region UpatePhase�����½׶���Ϣ
        /// <summary>
        /// ���½׶���Ϣ
        /// </summary>
        /// <param name="bigId">�׶α��</param>
        /// <param name="eventName">�׶�ʱ��</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int UpatePhase(string id, string t_time,bool limit,string start,string end, ref string strErr)
        {
            start = DateTime.Now.ToShortDateString() +" "+ start;
            end = DateTime.Now.ToShortDateString() + " " + end;
            string strSQL = "update s_actiontimelimit set t_time='" + t_time + "',IsLimitWorkHours='" + limit + "',StartWorkHours='" + start + "',EndWorkHours = '" + end + "' where id=" + id + "";

            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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


        #region UpateSetTime������ʱ����Ϣ
        /// <summary>
        /// ����ʱ����Ϣ
        /// </summary>
        /// <param name="eventName">�׶�ʱ��</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int UpateSetTime(string mstarttime, string mendtime, string fstarttime, string fendtime,string logtime, ref string strErr)
        {
            mstarttime = DateTime.Now.ToShortDateString() + " " + mstarttime;
            mendtime = DateTime.Now.ToShortDateString() + " " + mendtime;
            fstarttime = DateTime.Now.ToShortDateString() + " " + fstarttime;
            fendtime = DateTime.Now.ToShortDateString() + " " + fendtime;


            //string strSQL = "update tb_worktime set time_start='" + mstarttime + "',time_end='" + mendtime + "' where id= 1 ";
            //string strSQL2 = "  update tb_worktime set time_start='" + fstarttime + "',time_end='" + fendtime + "' where id= 2 ";
            //string sql = strSQL + strSQL2;

            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                   // return dl.ExecuteNonQuery(sql);
                    //�޸Ĺ���ʱ�ޣ������޸��²����Ĵ���ʱ�ޣ�һ��Ϊ��λ���޸ģ�
                  SqlParameter[] arrSP = new SqlParameter[] { 
                        new SqlParameter("@mstarttime", mstarttime),
                        new SqlParameter("@mendtime",mendtime),
                        new SqlParameter("@fstarttime", fstarttime),
                        new SqlParameter("@fendtime", fendtime),
                        new SqlParameter("@logtime", logtime)
                    };
                    return dl.ExecuteNonQuery("pr_p_UpdateWorktime", CommandType.StoredProcedure, arrSP);
 
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertHoliday������ʱ����Ϣ
        /// <summary>
        /// ����ʱ����Ϣ
        /// </summary>
        /// <param name="eventName">�׶�ʱ��</param>
        /// <param name="strErr">����Ĵ�����Ϣ</param> 
        /// <returns></returns>
        public int InsertHoliday(string name, string mstarttime, ref string strErr)
        {
            string strSQL = " insert into tb_holiday(hdate,name) values('" + mstarttime + "','" + name + "')";
 
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region  ��ȡС���²�����ϢDataTable
        public DataTable GetSmallClassPartTable(string smallPartID, string parentCode)
        {
            DoorManagerDAL dal = new DoorManagerDAL();
            return dal.GetSmallClassPartTable(smallPartID, parentCode);
        }

        public DataTable GetSmallClassEventTable(string smallEventID, string parentCode)
        {
            DoorManagerDAL dal = new DoorManagerDAL();
            return dal.GetSmallClassEventTable(smallEventID, parentCode);
        }
        #endregion

        #region AddSetTime�����ʱ��������Ϣ
        /// <summary>
        /// ���ʱ��������Ϣ
        /// </summary>        
        /// <returns></returns>
        public int AddSetTime(string mstarttime, string mendtime, string fstarttime, string fendtime, string qy_time, ref string strErr)
        {
            string sql = "";
            try
            {
                mstarttime = qy_time + " " + mstarttime;
                mendtime = qy_time + " " + mendtime;
                fstarttime = qy_time + " " + fstarttime;
                fendtime = qy_time + " " + fendtime;

                //int long_1 = Convert.ToInt16(DateTime.Parse(mendtime).Subtract(DateTime.Parse(mstarttime)).TotalMinutes);
                //int long_2 =Convert.ToInt16( DateTime.Parse(fendtime).Subtract(DateTime.Parse(fstarttime)).TotalMinutes);

                string sql1 = "insert into  tb_worktime(time_start,time_end) values('" + mstarttime + "','" + mendtime + "')";
                string sql2 = "insert into  tb_worktime(time_start,time_end) values('" + fstarttime + "','" + fendtime + "')";

                sql = sql1 + ";" + sql2;

            }

            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }

            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    //�жϸ�����ʱ���Ƿ������
                    string sql3 = "select count(*) from tb_worktime where left(CONVERT(varchar,time_start,120),10)='" + qy_time + "'";
                    int ret_1 = int.Parse(dl.ExecScalarSql(sql3).ToString());
                    if (ret_1 > 0)
                    {
                        strErr = "��ǰ���������Ѿ�����,������ѡ��";
                        return 0;
                    }
                    else
                    {
                        return dl.ExecuteNonQuery(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion
    }
}
