/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;�����Žṹ������
 * �ṹ��ɣ�
 * ��    �ߣ�����Ⱥ
 * �������ڣ�2007-05-25
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using szbase=bacgBL.web.szbase.organize;
using bacgDL.environment.organizes;

namespace bacgBL.environment.organizes
{
    public class DepartManage
    {
        #region GetDepartList����ȡ�����б�
        /// <summary>
        /// ��ȡ����ĳ�����ڵ��µĲ��ŵ��б�(����֪����λ)
        /// </summary>
        public DataTable GetDepartList(string departcode,string Type)
        {
            using (DepartManageDL dl = new DepartManageDL())
            {
                DataTable dt = dl.GetDepartList(departcode, Type);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool tag = true;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
                        {
                            tag = false;
                            break;
                        }
                    }
                    if (tag)
                        dt.Rows[i]["parentcode"] = departcode;
                }
                return dt;
            }
        }
        #endregion

        #region GetDepartListDetail����ȡ�����б�
        /// <summary>
        /// ��ȡ����ĳ�����ڵ��µĲ��ŵ��б�(��������һ���Ӳ���)
        /// </summary>
        public DataTable GetDepartListDetail(string departcode, string Type)
        {
            using (DepartManageDL dl = new DepartManageDL())
            {
                DataTable dt = dl.GetDepartListDetail(departcode, Type);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool tag = true;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["parentcode"].ToString() == dt.Rows[j]["departcode"].ToString())
                        {
                            tag = false;
                            break;
                        }
                    }
                    if (tag)
                        dt.Rows[i]["parentcode"] = departcode;
                }
                return dt;
            }
        }
        #endregion

        #region GetDepartInfo����ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public DepartInfo GetDepartInfo(string departcode)
        {
            DepartInfo di;
            using (DepartManageDL dl = new DepartManageDL())
            {
                if (departcode.Length == 5 && departcode.StartsWith("9"))
                {
                    di = dl.GetDepartOuterInfo(departcode);
                }
                else
                {
                    bacgBL.web.szbase.organize.DepartManage dm = new bacgBL.web.szbase.organize.DepartManage();
                    string err="";
                    DataSet ds = dm.GetDepartInfo(int.Parse(departcode),ref err);
                    di = new DepartInfo();
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        di.UserDefinedCode=ds.Tables[0].Rows[0]["UserDefinedCode"].ToString();
                        di.DepartName=ds.Tables[0].Rows[0]["departname"].ToString();
                        di.ParentCode=ds.Tables[0].Rows[0]["parentcode"].ToString();
                        di.Area=ds.Tables[0].Rows[0]["area"].ToString();
                        di.Principal=ds.Tables[0].Rows[0]["principal"].ToString();
                        di.Mobile=ds.Tables[0].Rows[0]["Mobile"].ToString();
                        di.Tel=ds.Tables[0].Rows[0]["Tel"].ToString();
                        di.DepartAdress=ds.Tables[0].Rows[0]["departadress"].ToString();
                        di.Memo=ds.Tables[0].Rows[0]["memo"].ToString();
                        di.DepartCode=departcode;
                        di.ParentName = ds.Tables[0].Rows[0]["parentdepartname"].ToString();
                        di.IsOuter = false;
                        
                    }
                }
                string[] strGuide = dl.GetGuideDepart(departcode);
                di.GuideCode = strGuide[0];
                di.GuideName = strGuide[1];
            }
            return di; 
        }
        #endregion

        #region DelDepartInfo��ɾ��������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public int DelDepartInfo(string departcode)
        {
            int tag;
            using (DepartManageDL dl = new DepartManageDL())
            {
                tag = dl.DelDepartInfo(departcode);
            }
            return tag;
        }
        #endregion

        #region SetDepartInfo�����ò�����Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public string SetDepartInfo(DepartInfo di)
        {
            string tag;
            using (DepartManageDL dl = new DepartManageDL())
            {
                tag = dl.SetDepartInfo(di);
                dl.SetGuideDepart(tag, di.GuideCode, di.GuideName);
            }
            return tag;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int SetDutyAreaInfo(string departcode,string DutyArea)
        {
            int tag;
            using (DepartManageDL dl = new DepartManageDL())
            {
                
                string areacode = "", areaname = "", areanumber = "";
                if (DutyArea != "-1")
                {
                    string[] temp = DutyArea.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < temp.Length; i++)
                    {
                        string[] tempinfo = temp[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (areacode == "")
                        {
                            areaname = tempinfo[0];
                            areacode = tempinfo[1];
                            areanumber = tempinfo[2];
                        }
                        else
                        {
                            areaname += "," + tempinfo[0];
                            areacode += "," + tempinfo[1];
                            areanumber += "," + tempinfo[2];
                        }
                    }
                }
                tag=dl.SetDutyAreaInfo(departcode, areacode, areaname, areanumber);
            }
            return tag;
        }

        public DataTable GetDutyArea(string departcode)
        {
            using (DepartManageDL dl = new DepartManageDL())
            {
                return dl.GetDutyArea(departcode);
            }
        }
        #endregion
        
        [AjaxPro.AjaxMethod]
        public string CheckDepartName(string name)
        {
            DepartManageDL dl = new DepartManageDL();
            bool f = dl.HasDeptName(name);
           
            try
            {
                if (f==true)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception e)
            {
                return "0";
            }
           
        }
        

        public DepartInfo GetDepartType(int departcode)
        {
            DepartManageDL dl = new DepartManageDL();
            SqlDataReader rs = (SqlDataReader)dl.getDepartType(departcode);

            DepartInfo di = new DepartInfo();

            while(rs.Read())
            {
                di.type = rs["type"].ToString();                
            }
            rs.Close();
            return di;
        }

        public int SetDepartType(DepartInfo di)
        {
            using (DepartManageDL dl = new DepartManageDL())
            try
            {
                int i = dl.SetDepartType(di);
                return i;
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetDepartSignList()
        {
            try
            {
                using (bacgDL.environment.organizes.DepartManageDL dl = new bacgDL.environment.organizes.DepartManageDL())
                {
                    return dl.GetDepartSignList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetDepartGnSignList()
        {
            try
            {
                using (bacgDL.environment.organizes.DepartManageDL dl = new bacgDL.environment.organizes.DepartManageDL())
                {
                    return dl.GetDepartGnSignList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
