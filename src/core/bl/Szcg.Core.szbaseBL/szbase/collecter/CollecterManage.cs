using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using bacgBL.web.szbase.entity;

namespace bacgBL.web.szbase.collecter
{
    public class CollecterManage
    {
        //��"null"ת��Ϊ""
        public string changeNull(string str)
        {
            if (str.ToLower().Equals("null"))
                str = "";
            return str;
        }
        
        #region GetAllCollecter����ȡ���мලԱ�б�
        /// <summary>
        /// ��ȡ���мලԱ�б�
        /// </summary>
        /// <param name="type">����(area,street,community)</param>
        /// <param name="id">��������ֵ߽�id��������id</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="returnRecordCount">��¼��</param>
        /// <param name="name">����</param>
        /// <param name="loginname">��½��</param>
        /// <param name="gridcode">�����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public ArrayList[] GetAllCollecter(string type, int id, int pageIndex,
                                            int pageSize, int returnRecordCount, string name,
                                            string loginname, string gridcode, ref string strErr)
        {
            ArrayList[] list = new ArrayList[2];
            list[0] = new ArrayList();
            list[1] = new ArrayList();
            SqlParameter[] arrSP = new SqlParameter[]{
				                            new SqlParameter("@Type",type),
                                            new SqlParameter("@ID",id),
	                                        new SqlParameter("@PageIndex",pageIndex),
		                                    new SqlParameter("@PageSize",pageSize),
                                            new SqlParameter("@ReturnRecordCount",returnRecordCount),
	                                        new SqlParameter("@name",name),
                                            new SqlParameter("@loginname",loginname),
                                            new SqlParameter("@gridcode",gridcode)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {   
                    SqlDataReader rs = dl.ExecReaderProc("pr_m_GetAllCollecter", ref arrSP);
                    while (rs.Read())
                    {
                        Collecter coll = new Collecter();
                        coll.CollCode = Convert.ToInt32(rs["collcode"]);
                        coll.GridCode = rs["gridcode"].ToString();
                        coll.CollName = rs["collname"].ToString();
                        coll.LoginName = rs["loginname"].ToString();
                        coll.Mobile = changeNull(rs["mobile"].ToString());
                        coll.Tel = changeNull(rs["hometel"].ToString());
                        coll.Address = changeNull(rs["homeaddress"].ToString());
                        coll.Imei = changeNull(rs["imei"].ToString());
                        list[0].Add(coll);
                    }
                    if (returnRecordCount == 1 && rs.NextResult())
                    {
                        rs.Read();
                        list[1].Add(rs[0].ToString());
                    }
                    rs.Close();
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

        #region GetAllMobile����ȡ���гǹ�ͨ�ֻ��б�
        /// <summary>
        /// ��ȡ���гǹ�ͨ�ֻ��б�
        /// </summary>
        /// <param name="type">����(area,street,community)</param>
        /// <param name="id">��������ֵ߽�id��������id</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="returnRecordCount">��¼��</param>
        /// <param name="mobile">�ֻ�����</param>
        /// <param name="IMSI">IMSI����</param>
        /// <param name="IMEI">IMEI����</param>
        /// <param name="gridcode">�����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public ArrayList[] GetAllMobile(string type, int id, int pageIndex,
                                            int pageSize, int returnRecordCount, string mobile, string IMSI,
                                            string IMEI, string gridcode, ref string strErr)
        {
            ArrayList[] list = new ArrayList[2];
            list[0] = new ArrayList();
            list[1] = new ArrayList();
            SqlParameter[] arrSP = new SqlParameter[]{
				                            new SqlParameter("@Type",type),
                                            new SqlParameter("@ID",id),
	                                        new SqlParameter("@PageIndex",pageIndex),
		                                    new SqlParameter("@PageSize",pageSize),
                                            new SqlParameter("@ReturnRecordCount",returnRecordCount),
	                                        new SqlParameter("@mobile",mobile),
                                            new SqlParameter("@IMSI",IMSI),
                                            new SqlParameter("@IMEI",IMEI),
                                            new SqlParameter("@gridcode",gridcode)};
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderProc("pr_m_GetAllMobile", ref arrSP);
                    while (rs.Read())
                    {    
                        Collecter coll = new Collecter();
                        coll.Mobile = (rs["mobile"].ToString());
                        coll.GridCode = rs["gridcode"].ToString();
                        coll.ImsiCard = changeNull(rs["IMSI"].ToString());
                        coll.ImeiCard = changeNull(rs["IMEI"].ToString());
                        coll.Regdate = changeNull(rs["reg_date"].ToString());
                        coll.Cudate = changeNull(rs["cu_date"].ToString());
                        list[0].Add(coll);
                    }
                    if (returnRecordCount == 1 && rs.NextResult())
                    {
                        rs.Read();
                        list[1].Add(rs[0].ToString());
                    }
                    rs.Close();
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

        #region DeleteFromCollecter:ɾ���ලԱ��Ϣ
        /// <summary>
        /// ɾ���ලԱ��Ϣ
        /// </summary>
        /// <param name="collCode">�ලԱ���</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int DeleteFromCollecter(int collCode, ref string strErr)
        {
           string strSQL=string.Format(@"update m_collecter set IsDel =1 
	                                    where collcode='{0}'", collCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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

        #region GetCommInfo:��������id����,��ȡ�����������������

        public string GetCommInfo(int id, ref string strErr)
        {
            string strSQL = string.Format(@"select commcode,commname 
                                            from s_community
                                            where id = '{0}'", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    string retStr = "";
                    while (rs.Read())
                    {
                        retStr = rs["commname"].ToString() + "," + rs["commcode"].ToString();
                    }
                    rs.Close();
                    return retStr;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion


        #region DeleteFromMobile:ɾ���ලԱ��Ϣ
        /// <summary>
        /// ɾ���ǹ�ͨ������Ϣ
        /// </summary>
        /// <param name="collCode">�ǹ�ͨ����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int DeleteFromMobile(string collCode, ref string strErr)
        {
            string strSQL = string.Format(@"delete from m_mobile_info where mobile='{0}'", collCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
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


        #region GetCollecterInfoByID:ͨ���ලԱ���,��ȡ�ලԱ��Ϣ
        /// <summary>
        /// ͨ���ලԱ���,��ȡ�ලԱ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Collecter GetCollecterInfoByID(int id, ref string strErr)
        {
            Collecter retCollecter = new Collecter();
            string strSQL = string.Format(@"select a.collcode,a.commcode,a.gridcode,
		                                        a.numbercode,a.collname,a.loginname,
		                                        a.pwd,a.sex,a.mobile,a.age,
		                                        a.url,a.hometel,a.homeaddress,
		                                        isnull(a.timeout,0) as timeout,a.memo, b.commname,a.imei
                                            from m_collecter a inner join s_community b 
                                            on a.commcode=b.commcode
                                            where a.collcode='{0}'", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {
                        retCollecter.CollCode = Convert.ToInt32(rs["collcode"]);
                        retCollecter.CommCode = rs["commcode"].ToString();//
                        retCollecter.GridCode = changeNull(rs["gridcode"].ToString());//gridcode
                        retCollecter.NumberCode = changeNull(rs["numbercode"].ToString());//numbercode
                        retCollecter.CollName = changeNull(rs["collname"].ToString());//collname
                        retCollecter.LoginName = changeNull(rs["loginname"].ToString());//loginname
                        retCollecter.Pwd = changeNull(rs["pwd"].ToString());//pwd
                        retCollecter.Sex = changeNull(rs["sex"].ToString());//sex
                        retCollecter.Mobile = changeNull(rs["mobile"].ToString());//mobile
                        retCollecter.Age = changeNull(rs["age"].ToString());//age
                        retCollecter.Url = changeNull(rs["url"].ToString());//url
                        retCollecter.Tel = changeNull(rs["hometel"].ToString());//hometel
                        retCollecter.Address = changeNull(rs["homeaddress"].ToString());//homeaddress
                        retCollecter.TimeOut = Convert.ToInt32(rs["timeout"]);
                        retCollecter.Memo = changeNull(rs["memo"].ToString());
                        retCollecter.CommName = rs["commname"].ToString();
                        retCollecter.Imei = changeNull(rs["imei"].ToString());
                    }

                    rs.Close();
                    return retCollecter;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion


        #region GetMobileByID:ͨ���ǹ�ͨ����,��ȡ�ǹ�ͨ��Ϣ
        /// <summary>
        /// ͨ���ǹ�ͨ����,��ȡ�ǹ�ͨ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Collecter GetMobileByID(string id, ref string strErr)
        {
            Collecter retCollecter = new Collecter();
            string strSQL = string.Format(@"select mobile,IMSI,IMEI,GridCode
                                            from m_mobile_info 
                                            where mobile='{0}'", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {  
                        retCollecter.Mobile = changeNull(rs["mobile"].ToString()); 
                        retCollecter.ImsiCard = rs["IMSI"].ToString();
                        retCollecter.ImeiCard = rs["IMEI"].ToString();
                        retCollecter.GridCode = rs["GridCode"].ToString();
                    }
                    rs.Close();
                    return retCollecter;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion


        #region GetFirstLevel:��ȡ���������б�����
        /// <summary>
        /// ��ȡ���������б�����
        /// </summary>
        /// <param name="id">���(areacode,�ֵ�id,����id)</param>
        /// <param name="type">����(area,street,community)</param>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public ArrayList GetFirstLevel(int id, string type, string areacode, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select areacode,areaname from s_area";
            if (!areacode.Equals("3211"))
            {
                strSQL = "select areacode,areaname from s_area where areacode='" + areacode + "'";
            }
            if (id > 0)
            {
                if (type.Equals("area"))
                {
                    strSQL = "select areacode,areaname from s_area where areacode=" + id;
                }
                else if (type.Equals("street"))
                {
                    strSQL = "select streetcode,streetname from s_street where id=" + id;
                }
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {
                        string[] values = new string[2];
                        values[0] = rs.GetInt32(0).ToString();
                        values[1] = rs.GetString(1);
                        list.Add(values);
                    }
                    rs.Close();
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

        #region GetSecondLevel:��ȡ���������б�ĵڶ�������
        /// <summary>
        /// ��ȡ���������б�ĵڶ�������
        /// </summary>
        /// <param name="id">���(areacode,�ֵ�id,����id)</param>
        /// <param name="type">����(area,street,community)</param>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public ArrayList getSecondLevel(int id, string type, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select streetcode,areacode,streetname from s_street";
            if (id > 0)
            {
                if (type.Equals("area"))
                {
                    strSQL = "select streetcode ,areacode,streetname from s_street where areacode=" + id;
                }
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);

                    while (rs.Read())
                    {
                        string[] values = new string[3];
                        values[0] = rs["streetcode"].ToString();
                        values[1] = rs["areacode"].ToString();
                        values[2] = rs["streetname"].ToString();
                        list.Add(values);
                    }

                    rs.Close();
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

        #region GetThirdLevel:��ȡ���������б�ĵ���������
        /// <summary>
        /// ��ȡ���������б�ĵ���������
        /// </summary>
        /// <param name="id">���(areacode,�ֵ�id,����id)</param>
        /// <param name="type">����(area,street,community)</param>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
        public ArrayList GetThirdLevel(int id, string type, ref string strErr)
        {
            ArrayList list = new ArrayList();
            string strSQL = "select * from s_community";
            if (id > 0)
            {
                if (type.Equals("area"))
                {
                    strSQL = string.Format(@"select a.commcode,a.streetcode,a.commname 
                                            from s_community a inner join s_street b 
                                            on a.streetcode=b.streetcode
                                            where b.areacode = '{0}'", id);
                }
                if (type.Equals("street"))
                {
                    strSQL = string.Format(@"select a.commcode,a.streetcode,a.commname 
                                            from s_community a inner join s_street b 
                                            on a.streetcode=b.streetcode
                                            where b.id = '{0}'",id);
                }
                if (type.Equals("comm"))
                {
                    strSQL = string.Format(@"select a.commcode,a.streetcode,a.commname 
                                            from s_community a inner join s_street b 
                                            on a.streetcode=b.streetcode
                                            inner join s_grid s
                                            on a.commcode=s.commcode
                                            where s.id= '{0}'", id);
                }
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);

                    while (rs.Read())
                    {
                        string[] values = new string[3];
                        values[0] = rs["commcode"].ToString();
                        values[1] = rs["streetcode"].ToString();
                        values[2] = rs["commname"].ToString();
                        list.Add(values);
                    }

                    rs.Close();
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

        #region GetGrids:������������,��ȡ�����±ߵ������
        /// <summary>
        /// ������������,��ȡ�����±ߵ������
        /// </summary>
        /// <param name="commcode">��������</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public string GetGrids(string commcode, ref string strErr)
        {
            StringBuilder sb = new StringBuilder();
            string strSQL="";
            if (commcode.Length < 6)
                strSQL = string.Format(@"select gridcode from s_grid where commcode=(select commcode from  s_community where id ='{0}')", commcode);
            else
                strSQL = string.Format(@"select gridcode from s_grid where commcode='{0}'",commcode);

            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {
                        sb.Append(rs.GetString(0) + ",");
                    }
                    rs.Close();
                    if (sb.Length > 0)
                    {
                        return sb.ToString().Substring(0, sb.Length - 1);
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion

        #region GetAllGrids:��������(��,�ֵ�,����),��ȡ�����±ߵ������
        /// <summary>
        /// ��������(��,�ֵ�,����),��ȡ�����±ߵ������
        /// </summary>
        /// <param name="type">����(��,�ֵ�,����)</param>
        /// <param name="commcode">id</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public DataSet GetAllGrids(string type, int id, ref string strErr)
        {
            string strSQL = "";
            StringBuilder sb = new StringBuilder();
            if (type.Equals("area"))
            {
                strSQL = "SELECT s.id, s.gridcode FROM  s_grid s  INNER JOIN s_community b  ON s.commcode = b.commcode INNER JOIN s_street c ON b.streetcode=c.streetcode where c.areacode="+id+"";
            }
            else if (type.Equals("street"))
            {
                strSQL = "SELECT  s.id, s.gridcode FROM  s_grid s INNER JOIN s_community b ON s.commcode = b.commcode INNER JOIN s_street c ON b.streetcode=c.streetcode where c.id = " + id + "";
            }
            else
            {
                strSQL = "SELECT  s.id, s.gridcode FROM  s_grid s INNER JOIN s_community b ON s.commcode = b.commcode where b.id =" + id + "";
            }

            ArrayList list = new ArrayList();
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

        #region CheckLoginName:�ж��û�����ĵ�½���Ƿ��Ѿ�����
        /// <summary>
        /// �ж��û�����ĵ�½���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="loginName">��½��</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int CheckLoginName(string loginName, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from m_collecter  
                                            where loginname='{0}'", loginName);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecScalarSql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckMobile:�жϳǹ�ͨ�����Ƿ��Ѿ�����
        /// <summary>
        /// �жϳǹ�ͨ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="mobile">�ǹ�ͨ����</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int CheckMobile(string mobile, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) 
                                            from m_mobile_info 
                                            where mobile='{0}'", mobile);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecScalarSql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertIntoCollecter:����ලԱ��Ϣ
        /// <summary>
        /// ����ලԱ��Ϣ
        /// </summary>
        /// <param name="values">�ලԱ��Ϣ�б�</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int InsertIntoCollecter(string[] values,ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
                {
                    return Int32.Parse(dl.InsertIntoCollecter(values));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertIntoMobile:����ǹ�ͨ��Ϣ
        /// <summary>
        /// ����ǹ�ͨ��Ϣ
        /// </summary>
        /// <param name="col">�ǹ�ͨ��Ϣ�б�</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int InsertIntoMobile(Collecter col, ref string strErr)
        {
            string strSQL = string.Format(@"insert into m_mobile_info(mobile,IMSI,IMEI,GridCode,reg_date) values('{0}','{1}','{2}','{3}','{4}')",col.Mobile,col.ImsiCard,col.ImeiCard,col.GridCode,DateTime.Now.ToString());
            
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
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

        #region UpdateToCollecter:���¼ලԱ��Ϣ
        /// <summary>
        /// ���¼ලԱ��Ϣ
        /// </summary>
        /// <param name="values">�ලԱ��Ϣ�б�</param>
        /// <param name="collcode">�ලԱ���</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int UpdateToCollecter(string[] values, string collcode, ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
                {
                    return Int32.Parse(dl.UpdateCollecter(values,collcode));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
          }
        #endregion

        #region UpdateToMobile:���³ǹ�ͨ��Ϣ
          /// <summary>
          /// �ǹ�ͨ��Ϣ
          /// </summary>
          /// <param name="values">�ǹ�ͨ��Ϣ�б�</param>
          /// <param name="strErr">���������Ϣ</param>
          /// <returns></returns>
        public int UpdateToMobile(Collecter col, ref string strErr)
        {
            string strSQL = string.Format(@"update m_mobile_info set IMSI='{0}',IMEI='{1}',GridCode='{2}',cu_date='{3}' where mobile='{4}'",col.ImsiCard,col.ImeiCard,col.GridCode,DateTime.Now.ToString(),col.Mobile);
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
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

        #region GetAllLawers����ȡ����ִ����Ա�б�
          /// <summary>
          /// ��ȡ����ִ����Ա�б�
          /// </summary>
          /// <param name="pageIndex">��ǰҳ</param>
          /// <param name="pageSize">ҳ��С</param>
          /// <param name="returnRecordCount">��¼��</param>
          /// <param name="name">����</param>
          /// <param name="loginname">��½��</param>
          /// <param name="gridcode">�����</param>
          /// <param name="strErr">���������Ϣ</param>
          /// <returns></returns>
          public ArrayList[] GetAllLawers(int pageIndex,int pageSize, int returnRecordCount,
                                            string name,string loginname, string gridcode,
                                            ref string strErr)
          {
              ArrayList[] list = new ArrayList[2];
              list[0] = new ArrayList();
              list[1] = new ArrayList();
              SqlParameter[] arrSP = new SqlParameter[]{
	                                        new SqlParameter("@PageIndex",pageIndex),
		                                    new SqlParameter("@PageSize",pageSize),
                                            new SqlParameter("@ReturnRecordCount",returnRecordCount),
	                                        new SqlParameter("@name",name),
                                            new SqlParameter("@loginname",loginname),
                                            new SqlParameter("@gridcode",gridcode)};
              try
              {
                  using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                  {

                      SqlDataReader rs = dl.ExecReaderProc("pr_m_GetAllLawer", ref arrSP);
                      while (rs.Read())
                      {
                          Lawer lawer = new Lawer();
                          lawer.LawerCode = rs["lawercode"].ToString();
                          lawer.GridCode = rs["gridcode"].ToString();
                          lawer.LawerName = rs["lawername"].ToString();
                          lawer.LoginName = rs["loginname"].ToString();
                          lawer.Mobile = changeNull(rs["mobile"].ToString());
                          lawer.Tel = changeNull(rs["hometel"].ToString());
                          lawer.Address = changeNull(rs["homeaddress"].ToString());
                          list[0].Add(lawer);
                      }
                      if (returnRecordCount == 1 && rs.NextResult())
                      {
                          rs.Read();
                          list[1].Add(rs[0].ToString());
                      }
                      rs.Close();
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

        #region DeleteFromLawer:ɾ��ִ����Ա��Ϣ
        /// <summary>
        /// ɾ��ִ����Ա��Ϣ
        /// </summary>
        /// <param name="lawercode">ִ����Ա���</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int DeleteFromLawer(int lawercode, ref string strErr)
        {
            string strSQL = string.Format(@"delete from s_lawer
                                            where lawercode='{0}'", lawercode);
          try
          {
              using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
              {
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

        #region GetLawerInfoByID:ͨ��ִ����Ա��ű��,��ȡִ����Ա��Ϣ
        /// <summary>
        /// ͨ��ִ����Ա��ű��,��ȡִ����Ա��Ϣ
        /// </summary>
        /// <param name="id">ִ����Ա���</param>
        /// <returns></returns>
        public Hashtable GetLawerInfoByID(int id, ref string strErr)
        {
            Hashtable table = new Hashtable();
            string strSQL = string.Format(@"select commcode,gridcode,numbercode,
		                                            lawername,loginname,pwd,
		                                            url,timeout,mobile,
		                                            sex,age,hometel,
		                                            homeaddress,memo
                                            from s_lawer
                                            where lawercode = '{0}'", id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    while (rs.Read())
                    {
                        string commcode = changeNull(rs["commcode"].ToString());
                        table.Add("commcode", commcode);
                        string gridcode = changeNull(rs["gridcode"].ToString());
                        table.Add("gridcode", gridcode);
                        string numbercode = changeNull(rs["numbercode"].ToString());
                        table.Add("numbercode", numbercode);
                        string lawername = rs["lawername"].ToString();
                        table.Add("lawername", lawername);
                        string loginname = rs["loginname"].ToString();
                        table.Add("loginname", loginname);
                        string pwd = rs["pwd"].ToString();
                        table.Add("pwd", pwd);
                        string url = changeNull(rs["url"].ToString());
                        table.Add("url", url);
                        string timeout = changeNull(rs["timeout"].ToString());
                        table.Add("timeout", timeout);
                        string mobile = changeNull(rs["mobile"].ToString());
                        table.Add("mobile", mobile);
                        string sex = changeNull(rs["sex"].ToString());
                        table.Add("sex", sex);
                        string age = changeNull(rs["age"].ToString());
                        table.Add("age", age);
                        string hometel = changeNull(rs["hometel"].ToString());
                        table.Add("hometel", hometel);
                        string homeaddress = changeNull(rs["homeaddress"].ToString());
                        table.Add("homeaddress", homeaddress);
                        string memo = changeNull(rs["memo"].ToString());
                        table.Add("memo", memo);
                    }

                    rs.Close();
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

        #region InsertIntoLawer:����ִ����Ա��Ϣ
        /// <summary>
        /// ����ִ����Ա��Ϣ
        /// </summary>
        /// <param name="values">ִ����Ա��Ϣ�б�</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int InsertIntoLawer(string[] values, ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
                {
                    return dl.InsertIntoLawer(values);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpdateIntoLawer:����ִ����Ա��Ϣ
        /// <summary>
        /// ����ִ����Ա��Ϣ
        /// </summary>
        /// <param name="id">ִ����Ա����</param>
        /// <param name="values">ִ����Ա��Ϣ�б�</param>
        /// <param name="strErr">���������Ϣ</param>
        /// <returns></returns>
        public int UpdateIntoLawer(int id, string[] values, ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.collecter.CollecterManage dl = new bacgDL.szbase.collecter.CollecterManage())
                {
                    return dl.UpdateIntoLawer(id,values);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetCommName:�����������룬��ȡ����������
        public string GetCommName(string id,ref string strErr)
        {
            string strSQL = string.Format(@"select commname from s_community 
                                            where commcode = '{0}'",id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader rs = dl.ExecReaderSql(strSQL);
                    string name = "";
                    while (rs.Read())
                    {
                        name = rs["commname"].ToString();
                    }
                    rs.Close();
                    return name;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return "";
            }
        }
        #endregion

        #region CheckLawerName:�ж�ִ����Ա��½���Ƿ��ظ�
        public int CheckLawerName(string name, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from s_lawer where loginname='{0}'",name);
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
    }
}
