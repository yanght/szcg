using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Teamax.Common;
using System.Collections;

namespace bacgDL.business.workflow
{
    public class workflowDAO
    {
        /// <summary>
        /// ȡ��һ���̽ڵ�
        /// </summary>
        /// <param name="corenodeid">��ǰ�ڵ�ID</param>
        /// <param name="Busistatus">��ǰ�ڵ��ҵ�����</param>
        /// <returns>DataSet���ݼ�</returns>
        public DataSet GetNextNode(string corenodeid, string Busistatus)
        {
            CommonDatabase DAO = new CommonDatabase();
            string sql = "select a.* from s_flownodeinfo a, s_flownoderelainfo b where "
                        + " a.flowinfoid = b.flowinfoid and a.flownodeid=b.nextnodeid and b.corenodeid='" + corenodeid+"'"
                        + " and b.Busistatus='" + Busistatus+"'";

            DataSet dt = DAO.ExecuteDataset(sql);
            return dt;
        }


        /// <summary>
        /// ȡ���̻�����Ϣ
        /// </summary>
        /// <param name="flowinfoid">����ID������Ϊ�գ�</param>
        /// <param name="Flowname">�������ƣ�����Ϊ�գ�</param>
        /// <param name="creatdate">�������ڣ�����Ϊ�գ�</param>
        /// <param name="status">��Ч״̬������Ϊ�գ�</param>
        /// <returns>DataSet���ݼ�</returns>
        public DataSet GetFlowinfo(string flowinfoid, string flowname, int creatdate, string status)
        {
            CommonDatabase DAO = new CommonDatabase();
            string sql = "select * from s_flowinfo where 1=1";
            if (flowinfoid != null && flowinfoid!="")
                sql = sql + " and flowinfoid='" + flowinfoid+"'";
            if (flowname != null && flowname != "")
                sql = sql + " and flowname='" + flowname+"'";
            if (creatdate != 0)
                sql = sql + " and creatdate=" + creatdate;
            if (status != null && status != "")
                sql = sql + " and status !='" + status+"'";
            DataSet dt = DAO.ExecuteDataset(sql);
            return dt;
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="flowinfoid">Flowinfo</param>
        /// <returns>����������¼����</returns>
        public int insertFlowinfo(Flowinfo flowinfo)
        {
            string strSql = " INSERT INTO s_flowinfo ([flowinfoid],[flowname],[creatdate],[status],[flowversion],[remark])";
            strSql += " VALUES(@flowinfoid,@flowname,@creatdate,@status,@flowversion,@remark) ";
            SqlParameter[] paras = new SqlParameter[]{new SqlParameter("@flowinfoid", flowinfo.Flowinfoid),
                                                          new SqlParameter("@flowname", flowinfo.Flowname),
                                                          new SqlParameter("@creatdate", flowinfo.Creatdate),
                                                          new SqlParameter("@status", flowinfo.Status),
                                                          new SqlParameter("@flowversion", flowinfo.Flowversion),
                                                          new SqlParameter("@remark", flowinfo.Remark)
                };

            using (CommonDatabase cd = new CommonDatabase())
            {
                int i;
                return i = cd.ExecuteNonQuery(strSql, paras);
            }
        }

        /// <summary>
        /// ȡ���̻�����Ϣ
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="name">�������ƣ�����Ϊ�գ�</param>
        /// <returns></returns>
        public Hashtable GetFlowinfo(string id, string name)
        {
            string sql = "select * from s_flowinfo where 1=1";
            //if (id != null && id != "")
                sql = sql + " and flowinfoid=" + id;
            if (name != null && name != "")
                sql = sql + " and flowname=" + name;
            try
            {
                Hashtable table = new Hashtable();
                using (CommonDatabase DAO = new CommonDatabase())
                {
                    IDataReader rs = DAO.ExecuteReader(sql, null);
                    if (rs != null)
                    {
                        while (rs.Read())
                        {
                            string flowinfoid = rs["flowinfoid"].ToString();
                            table.Add("flowinfoid", flowinfoid);
                            string flowname = changeNull(rs["flowname"].ToString());
                            table.Add("flowname", flowname);
                            //string busistatus = changeNull(rs["busistatus"].ToString());
                            //table.Add("busistatus", busistatus);
                            string flowversion = changeNull(rs["flowversion"].ToString());
                            table.Add("flowversion", flowversion);
                            string status = rs["status"].ToString();
                            table.Add("status", status);
                            string remark = changeNull(rs["remark"].ToString());
                            table.Add("remark", remark);
                        }
                    }
                    rs.Close();
                }
                return table;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="flowinfoid">����ID</param>
        /// <param name="flowname">��������</param>
        /// <param name="status">��Ч״̬</param>
        /// <param name="remark">��ע</param>
        /// <returns>���¼�¼����</returns>
        public int updateFlowinfo(string flowinfoid, string flowname, string status, string remark)
        {
            string sql = "update s_flowinfo set flowname='" + flowname + "',status='" + status + "',remark='" + remark + "' where 1=1";
            //if (flowinfoid != null)
                sql = sql + " and flowinfoid='" + flowinfoid + "'";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                int i = DAO.ExecuteNonQuery(sql);
                return i;
            }
        }


        /// <summary>
        /// ȡ���̽ڵ���Ϣ
        /// </summary>
        /// <param name="flowinfoid">����ID</param>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="flownodename">�ڵ�����</param>
        /// <param name="busideal">ҵ������</param>
        /// <param name="property">�ڵ�����</param>
        /// <returns>DataSet���ݼ�</returns>
        public DataSet GetFlownodeinfo(string flowinfoid, string flownodeid, string flownodename, string busideal, string property)
        {
            CommonDatabase DAO = new CommonDatabase();
            string sql = "select * from s_flownodeinfo where 1=1";
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and flowinfoid=" + flowinfoid;
            if (flownodeid != null && flownodeid != "")
                sql = sql + " and flownodeid=" + flownodeid;
            if (flownodename != null && flownodename != "")
                sql = sql + " and flownodename=" + flownodename;
            if (busideal != null && busideal != "")
                sql = sql + " and busideal=" + busideal;
            if (property != null && property != "")
                sql = sql + " and property=" + property;
            DataSet dt = DAO.ExecuteDataset(sql);
            return dt;
        }

        /// <summary>
        /// ȡ���̽ڵ���Ϣ
        /// </summary>
        /// <param name="flowinfoid">����ID</param>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="flownodename">�ڵ�����</param>
        /// <param name="busideal">ҵ������</param>
        /// <param name="property">�ڵ�����</param>
        /// <returns>DataSet���ݼ�</returns>
        public IDataReader GetFlownodeinfo(string flowinfoid)
        {
            CommonDatabase DAO = new CommonDatabase();
            string sql = "select * from s_flownodeinfo where 1=1";
            if (flowinfoid != null)
                sql = sql + " and flowinfoid=" + flowinfoid;
            //if (flownodeid != null)
            //    sql = sql + " and flownodeid=" + flownodeid;
            IDataReader dt = DAO.ExecuteReader(sql);
            return dt;
        }
        /// <summary>
        /// �������̽ڵ���Ϣ
        /// </summary>
        /// <param name="flowinfoid">����ID</param>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="flownodename">�ڵ�����</param>
        /// <param name="busideal">ҵ������</param>
        /// <param name="property">�ڵ�����</param>
        /// <returns>DataSet���ݼ�</returns>
        public int updateFlownodeinfo(string flownodeid, string flownodename, string busideal, string property, string status, string remark)
        {
            string sql = "update s_flownodeinfo set flownodename='" + flownodename + "',busideal='" + busideal + "', property='" + property + "', status='" + status + "', remark='" + remark + "' where 1=1";
            if (flownodeid != null)
                sql = sql + " and flownodeid='" + flownodeid + "'";    
            using (CommonDatabase DAO = new CommonDatabase()){
                int i = DAO.ExecuteNonQuery(sql);
                return i;
            }
        }

        /// <summary>
        /// �������̽ڵ���Ϣ
        /// </summary>
        /// <param name="flowinfoid">FlowNode</param>
        /// <returns>����������¼����</returns>
        public int insertFlownodeinfo(FlowNode flownode)
        {
            string strSql = " INSERT INTO s_flownodeinfo ([flowinfoid],[flownodeid],[flownodename],[property],[busideal],[status],[sequence],[remark])";
            strSql += " VALUES(@flowinfoid,@flownodeid,@flownodename,@property,@busideal,@status,@sequence,@remark) ";
            SqlParameter[] paras = new SqlParameter[]{new SqlParameter("@flowinfoid", flownode.Flowinfoid),
                                                          new SqlParameter("@flownodeid", flownode.Flownodeid),
                                                          new SqlParameter("@flownodename", flownode.Flownodename),
                                                          new SqlParameter("@property", flownode.Property),
                                                          new SqlParameter("@busideal", flownode.Busideal),
                                                          new SqlParameter("@status", flownode.Status),
                                                          new SqlParameter("@sequence", flownode.Sequence),
                                                          new SqlParameter("@remark", flownode.Remark)
                };

            using (CommonDatabase cd = new CommonDatabase())
            {
                int i;
                return i = cd.ExecuteNonQuery(strSql, paras);
            }
        }

        /// <summary>
        /// ȡ�ڵ�ҵ����Ϣ
        /// </summary>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="businame">�ɰ����ҵ��������</param>
        /// <returns>DataSet</returns>
        public DataSet GetFlownodeBusis(string flowinfoid, string flownodeid, string businame)
        {
            string sql = "select * from s_flowbusistatus where 1=1";
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and flowinfoid=" + flowinfoid;
            if (flownodeid != null && flownodeid != "")
                sql = sql + " and flownodeid=" + flownodeid;
            if (businame != null && businame != "")
                sql = sql + " and businame=" + businame;
            using (CommonDatabase DAO = new CommonDatabase()){
                DataSet dt = DAO.ExecuteDataset(sql);
                return dt;
            }
        }

        /// <summary>
        /// ȡ�ڵ�ҵ����Ϣ
        /// </summary>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="businame">�ɰ����ҵ��������</param>
        /// <returns>IDataReader</returns>
        public IDataReader GetFlownodeBusis(string flowinfoid, string flownodeid)
        {
            
            string sql = "select * from s_flowbusistatus where 1=1";
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and flowinfoid=" + flowinfoid;
            if (flownodeid != null && flownodeid != "")
                sql = sql + " and flownodeid=" + flownodeid;
            //using (CommonDatabase DAO = new CommonDatabase())
            //{
            CommonDatabase DAO = new CommonDatabase();
                IDataReader dt = DAO.ExecuteReader(sql);
                return dt;
            //}
        }

        /// <summary>
        /// ȡ�ڵ�ҵ����Ϣ
        /// </summary>
        /// <param name="flownodeid">ҵ����ID</param>
        /// <param name="businame">�ɰ����ҵ��������</param>
        /// <returns></returns>
        public Hashtable GetFlownodeBusisDetail(string id, string name)
        {
            string sql = "select * from s_flowbusistatus where 1=1";
            if (id != null && id != "")
                sql = sql + " and flowbusistatusid=" + id;
            if (name != null && name != "")
                sql = sql + " and businame=" + name;
            try
            {
                Hashtable table = new Hashtable();
                using (CommonDatabase DAO = new CommonDatabase())
                {
                    IDataReader rs = DAO.ExecuteReader(sql, null);
                    if (rs != null)
                    {
                        while (rs.Read())
                        {
                            string flowbusistatusid = rs["flowbusistatusid"].ToString();
                            table.Add("flowbusistatusid", flowbusistatusid);
                            string nodeid = rs["Flownodeid"].ToString();
                            table.Add("flownodeid", nodeid);
                            string busistatus = changeNull(rs["busistatus"].ToString());
                            table.Add("busistatus", busistatus);
                            string businame = rs["businame"].ToString();
                            table.Add("businame", businame);
                            string status = rs["status"].ToString();
                            table.Add("status", status);
                            string remark = changeNull(rs["remark"].ToString());
                            table.Add("remark", remark);
                        }
                    }
                    rs.Close();
                }
                return table;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ���½ڵ�ҵ����Ϣ
        /// </summary>
        /// <param name="flowinfoid">ҵ����ID</param>
        /// <param name="flownodeid">ҵ����NAME</param>
        /// <param name="flownodename">ҵ����ע</param>
        /// <returns>�Ķ���¼����</returns>
        public int updateFlowbusistatus(string flowbusistatusid, string businame, string remark)
        {
            
            string sql = "update s_flowbusistatus set businame='" + businame + "',remark='" + remark + "' where 1=1";

            if (flowbusistatusid != null)
                sql = sql + " and flowbusistatusid='" + flowbusistatusid + "'";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                int i = DAO.ExecuteNonQuery(sql);
                return i;
            }
        }

        /// <summary>
        /// �������̽ڵ�ҵ����Ϣ
        /// </summary>
        /// <param name="flowinfoid">FlowBusi</param>
        /// <returns>����������¼����</returns>
        public int insertFlowbusistatus(FlowBusi flowbusi)
        {
            string strSql = " INSERT INTO s_flowbusistatus ([flowbusistatusid],[flowinfoid],[flownodeid],[busistatus],[businame],[status],[remark])";
            strSql += " VALUES(@flowbusistatusid,@flowinfoid,@flownodeid,@busistatus,@businame,@status,@remark) ";
            SqlParameter[] paras = new SqlParameter[]{new SqlParameter("@flowbusistatusid", flowbusi.Flowbusistatusid),
                                                          new SqlParameter("@flowinfoid", flowbusi.Flowinfoid),
                                                          new SqlParameter("@flownodeid", flowbusi.Flownodeid),
                                                          new SqlParameter("@busistatus", flowbusi.Busistatus),
                                                          new SqlParameter("@businame", flowbusi.Businame),
                                                          new SqlParameter("@status", flowbusi.Status),
                                                          new SqlParameter("@remark", flowbusi.Remark)
                };

            using (CommonDatabase cd = new CommonDatabase())
            {
                int i;
                return i = cd.ExecuteNonQuery(strSql, paras);
            }
        }

        /// <summary>
        /// ȡ���̽ڵ���
        /// </summary>
        /// <param name="flownodeid">����ID</param>
        /// <param name="flownodename">��������</param>
        /// <returns></returns>
        public DataSet GetFlownodeTree(string flowinfoid, string flowname)
        {
            string sql = "SELECT a.Flowname, a.Flowinfoid, b.Flownodename, b.Flownodeid, b.Sequence, c.Businame, c.Busistatus,"
                         + " isnull(( SELECT COUNT(Busistatus) AS BB FROM s_flowbusistatus WHERE (Flownodeid = b.Flownodeid) GROUP BY Flownodeid ),0) AS dealcount"
                         + " FROM s_flowinfo AS a INNER JOIN  s_flownodeinfo AS b ON a.Flowinfoid = b.Flowinfoid LEFT OUTER JOIN "
                         + " s_flowbusistatus AS c ON b.Flownodeid = c.Flownodeid where 1=1";
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and a.flowinfoid='" + flowinfoid + "'";
            if (flowname != null && flowname != "")
                sql = sql + " and a.flowname='" + flowname + "'";
            sql = sql + "order by b.Sequence";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                DataSet dt = DAO.ExecuteDataset(sql);
                return dt;
            }
        }

        /// <summary>
        /// ȡ���̽ڵ���Ϣ
        /// </summary>
        /// <param name="flownodeid">�ڵ�ID</param>
        /// <param name="flownodename">�ڵ�����</param>
        /// <returns></returns>
        public Hashtable GetFlownodeinfo(string flowinfoid,string flownodeid, string flownodename)
        {
            string sql = "SELECT a.Flowinfoid, a.Flownodeid, a.Flownodename, a.Property, a.Busideal, a.Status, a.Sequence, a.Remark, b.Flowname"
                         + " FROM s_flownodeinfo AS a INNER JOIN s_flowinfo AS b ON a.Flowinfoid = b.Flowinfoid ";
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and a.flowinfoid='" + flowinfoid+"'";
            if (flownodeid != null && flownodeid != "") 
            {
                sql = sql + " and a.flownodeid='" + flownodeid+"'";
            }
            else    //���δ���ݽڵ�ID����ȡ�����̵���ʼ�ڵ�
            {
                sql = sql + " and a.Sequence=1";
            }
            try
            {
                Hashtable table = new Hashtable();
                using (CommonDatabase DAO = new CommonDatabase())
                {
                    IDataReader rs = DAO.ExecuteReader(sql, null);
                    if (rs != null)
                    {
                        while (rs.Read())
                        {
                            string _flowinfoid = rs["Flowinfoid"].ToString();
                            table.Add("flowinfoid", _flowinfoid);
                            string nodeid = rs["Flownodeid"].ToString();
                            table.Add("flownodeid", nodeid);
                            string nodename = changeNull(rs["Flownodename"].ToString());
                            table.Add("flownodename", nodename);

                            string property = rs["Property"].ToString();
                            table.Add("property", property);

                            string busideal = rs["busideal"].ToString();
                            table.Add("busideal", busideal);
                            string nodestatus = rs["status"].ToString();
                            table.Add("nodestatus", nodestatus);
                            string noderemark = changeNull(rs["remark"].ToString());
                            table.Add("noderemark", noderemark);
                        }
                    }
                    rs.Close();
                }
                return table;
            }
            catch //(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// ȡ���̽ڵ��ϵ
        /// </summary>
        /// <param name="flownodeid">����ID</param>
        /// <returns></returns>
        public DataSet GetFlowRelation(string flowinfoid)
        {
            string sql = "SELECT flowinfoid,relainfoid,corenodeid,"
                         + " (SELECT Flownodename  FROM s_flownodeinfo WHERE (Flownodeid = a.corenodeid)) AS corenodename, nextnodeid,"
                         + " (SELECT Flownodename  FROM s_flownodeinfo AS s_flownodeinfo_1 WHERE (Flownodeid = a.nextnodeid)) AS nextnodename, Busistatus,"
                         + " (SELECT Businame FROM s_flowbusistatus WHERE (Flownodeid = a.corenodeid) AND (Busistatus = a.Busistatus)) AS businame, Status, Remark"
                         + " FROM s_flownoderelainfo AS a WHERE  status!='9'";                   
            if (flowinfoid != null && flowinfoid != "")
                sql = sql + " and a.flowinfoid='" + flowinfoid + "'";
            sql = sql + "order by corenodeid";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                DataSet dt = DAO.ExecuteDataset(sql);
                return dt;
            }
        }

        /// <summary>
        /// ȡ���̽ڵ��ϵ�������ж��½��ڵ��ϵ�Ƿ��ظ���
        /// </summary>
        /// <param name="flownodeid">����ID</param>
        /// <returns></returns>
        public int GetFlowRelation(Flowrelainfo flowrelainfo)
        {
            int result = 0;
            string sql = "SELECT count(*) as count "
                         + " FROM s_flownoderelainfo AS a WHERE  1=1";

                sql = sql + " and a.flowinfoid='" + flowrelainfo.Flowinfoid + "'";

                sql = sql + " and a.corenodeid='" + flowrelainfo.Corenodeid + "'";

                sql = sql + " and a.nextnodeid='" + flowrelainfo.Nextnodeid + "'";

                sql = sql + " and a.busistatus='" + flowrelainfo.Busistatus + "'";

            using (CommonDatabase DAO = new CommonDatabase())
            {
                IDataReader rs = DAO.ExecuteReader(sql, null);
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = Convert.ToInt32(rs["count"].ToString());

                    }
                }
                rs.Close();
                return result;
            }
        }

        /// <summary>
        /// ȡ���̽ڵ��ϵ
        /// </summary>
        /// <param name="flownodeid">��ϵID</param>
        /// <returns></returns>
        public IDataReader GetFlowRelationForDetail(string relainfoid) 
        {
            
            string sql = "SELECT a.*," 
                         +" (SELECT Flownodename  FROM s_flownodeinfo WHERE (Flownodeid = a.corenodeid)) AS corenodename,"
                         +" (SELECT Flownodename  FROM s_flownodeinfo AS s_flownodeinfo_1 WHERE (Flownodeid = a.nextnodeid)) AS nextnodename"
                         +" from s_flownoderelainfo a where status!='9'";
            if (relainfoid != null && relainfoid != "")
                sql = sql + " and a.relainfoid='" + relainfoid + "'";
            //sql = sql + "order by corenodeid";
            //using (CommonDatabase DAO = new CommonDatabase())
            //{
            CommonDatabase DAO = new CommonDatabase();
            IDataReader dt = DAO.ExecuteReader(sql);
            return dt;
            //}
        }

        /// <summary>
        /// ���½ڵ��ϵ
        /// </summary>
        /// <param name="Flowrelainfo">�ڵ��ϵ���ݼ�</param>
        /// <returns>�Ķ���¼����</returns>
        public int updateFlowrelainfo(Flowrelainfo flowrelation)
        {

            string sql = "update s_flownoderelainfo set nextnodeid='" + flowrelation.Nextnodeid + "', busistatus='" + flowrelation .Busistatus
                        + "',status='" + flowrelation .Status + "',remark='" + flowrelation.Remark + "' where 1=1";

            if (flowrelation.Relainfoid != null)
                sql = sql + " and Relainfoid='" + flowrelation.Relainfoid + "'";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                int i = DAO.ExecuteNonQuery(sql);
                return i;
            }
        }

        /// <summary>
        /// �����ڵ��ϵ
        /// </summary>
        /// <param name="flowrelation">Flowrelainfo</param>
        /// <returns>����������¼����</returns>
        public int insertFlowrelainfo(Flowrelainfo flowrelation)
        {
            string strSql = " INSERT INTO s_flownoderelainfo ([relainfoid],[corenodeid],[nextnodeid],[busistatus],[flowinfoid],[status],[remark])";
            strSql += " VALUES(@relainfoid,@corenodeid,@nextnodeid,@busistatus,@flowinfoid,@status,@remark) ";
            SqlParameter[] paras = new SqlParameter[]{new SqlParameter("@relainfoid", flowrelation.Relainfoid),
                                                          new SqlParameter("@corenodeid", flowrelation.Corenodeid),
                                                          new SqlParameter("@nextnodeid", flowrelation.Nextnodeid),
                                                          new SqlParameter("@busistatus", flowrelation.Busistatus),
                                                          new SqlParameter("@flowinfoid", flowrelation.Flowinfoid),
                                                          new SqlParameter("@status", flowrelation.Status),
                                                          new SqlParameter("@remark", flowrelation.Remark)
                };

            using (CommonDatabase cd = new CommonDatabase())
            {
                int i;
                return i = cd.ExecuteNonQuery(strSql, paras);
            }
        }

        //ͨ���������Ʊ�Ϊ������¼��ȡ����ֵ
        public string getKeyValue(string tablename)
        {
            string result = "";
            CommonDatabase DAO = new CommonDatabase();
            string sql = "select * from s_flowKeyControl where 1=1";
            if (tablename != null && tablename != "")
                sql = sql + " and tablename='" + tablename+"'";
            try
            {
                IDataReader rs = DAO.ExecuteReader(sql, null);
                Hashtable table = new Hashtable();
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = rs["keyvalue"].ToString();
                    }
                }
                rs.Close();
                string _result = increase(result);
                string SQL = "update s_flowKeyControl set keyvalue='" + _result + "' where 1=1"
                            + " and tablename='" + tablename + "'";
                int i = DAO.ExecuteNonQuery(SQL);
                return result;
            }
            catch //(Exception ex)
            {
                throw;
            }
        }

        //��"null"ת��Ϊ""
        public string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }

        //string������������1������������
        public string increase(string keyvalue)
        {
            int tmp = Convert.ToInt32(keyvalue) + 1;
            string _result = Convert.ToString(tmp);
            if (_result.Length < 2)
                _result = "0" + _result;
            return _result;
        }

        //ȡ��ǰ�����������ֵ������1
        public int selectMaxIndex(string tablename,string keyvalue,string columname, string datavalue)
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
                        result = Convert.ToInt32(rs[keyvalue].ToString()) + 1;
                    }
                }
                rs.Close();
                return result;
            }
        }

        //ȡ�ڵ��ҵ����ǰ���ֵ����ͨ���ԣ�
        public string getMaxStatusidkey(string flowinfoid, string flownodeid)
        {
            string sql = "select  MAX(Busistatus) as  busistatus from s_flowbusistatus a where 1=1";
                   sql = sql + " and a.flowinfoid='" + flowinfoid + "'";
                   sql = sql + " and a.flownodeid='" + flownodeid + "'";
            using (CommonDatabase DAO = new CommonDatabase())
            {
                IDataReader rs = DAO.ExecuteReader(sql);
                string result = "";
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = rs["busistatus"].ToString();
                    }
                }
                rs.Close();
                if (result == "")  //����Ϊ��ǰ�ڵ�ĵ�һ��ҵ����
                {
                    result = "01";
                }else
                result = increase(result);
                return result;
            }
        }
    }
}
