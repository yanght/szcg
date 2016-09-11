/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;������ϵͳ��Ա����
 * �ṹ��ɣ�
 * ��    �ߣ����ֲ�
 * �������ڣ�2007-05-26
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա�� ������               
 * �޸����ڣ� 2007-09-14
 * �޸�˵���� 
 * ****************************************************************************************/
using System;
using System.Data.SqlClient;
using System.Collections;
using bacgDL.environment.entitys;
using bacgDL.environment.personnels;
using bacgDL;
using System.Data;
using Teamax.Common;
using szcg.com.teamax.util;

namespace bacgBL.environment.personnels
{
	/// <summary>
	/// collecter ��ժҪ˵����
	/// </summary>
    public class BASE_personnelhelper
{
        PersonnelDAO personDAO = new PersonnelDAO();
        public BASE_personnelhelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        public int isDelSuscessbyDept(string departcode)
        {
            try
            {
                using (bacgDL.environment.personnels.PersonnelDAO dl = new bacgDL.environment.personnels.PersonnelDAO())
                {
                    return dl.isDelSuscessbyDept(departcode);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region GetCharActerList : ��ȡ��Ա����

        public DataSet GetCharActerList()
        {
            try
            {
                using (bacgDL.environment.personnels.PersonnelDAO dl = new bacgDL.environment.personnels.PersonnelDAO())
                {
                    return dl.GetCharActerList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region  GetPeopleType :��ȡ��Ա����
        public DataSet GetPeopleType()
        {
            try
            {
                using (bacgDL.environment.personnels.PersonnelDAO dl = new bacgDL.environment.personnels.PersonnelDAO())
                {
                    return dl.GetPeopleType();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region  GetDegreeType :��ȡѧ�������б�
        public DataSet GetDegreeType()
        {
            try
            {
                using (bacgDL.environment.personnels.PersonnelDAO dl = new bacgDL.environment.personnels.PersonnelDAO())
                {
                    return dl.GetDegreeType();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region  GetTechnologyType :��ȡ����ְ�������б�
        public DataSet GetTechnologyType()
        {
            try
            {
                using (bacgDL.environment.personnels.PersonnelDAO dl = new bacgDL.environment.personnels.PersonnelDAO())
                {
                    return dl.GetTechnologyType();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        #region GetAllPersonnel : �������������õ�������Ա�б�
        public Teamax.Common.PageManage GetAllPersonnel(person per, int pageIndex, int pageSize)
        {       
            try
            {
                Teamax.Common.PageManage page = personDAO.GetAllPersonnels(per, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAllPersonnel : �������������õ�������Ա�б�(��)
        public Teamax.Common.PageManage GetAllPersonnelS(person per, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = personDAO.GetAllPersonnels(per, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAllPersonnel : �������������õ�������Ա�б�(���ô洢����)
        public Teamax.Common.PageManage GetAllPersonnels(string departcode, string type, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = personDAO.GetDepartPersonList(departcode, type, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    

        #region hasPersonID : �ж��û������Ա������Ƿ��Ѿ�����

        public bool hasPersonID(string personID)
        {
            try
            {
                bool flag = personDAO.hasPersonID(personID);
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        
        #region insertIntoPersonnel : ����һ��Ա����Ϣ

        public int insertIntoPersonnel(person person)
        {
          
            try
            {
                int i = personDAO.insertIntoPersonnel(person);
                return i;
            }
            catch  
            {
                throw;
            }


        }
        #endregion
        
        #region updateIntoPersonnel : ����һ��Ա����Ϣ

        public int updateIntoPersonnel(person person)
        {
            try
            {
                int i = personDAO.updateIntoPersonnel(person);
                return i;
            }
            catch  
            {
                throw;
            }


        }
        #endregion
        
        #region deleteFromPersonnel : ɾ������Ա��

        public int deleteFromPersonnel(int id)
        {
            try
            {
                int i = personDAO.deleteFromPersonnel(id);
                return i;
            }
            catch  
            {
                throw;
            }

        }
        #endregion
        
        #region getPersonnelInfoByID : ��������id�õ����û���Ϣ

        public person getPersonnelInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)personDAO.getPersonnelInfoByID(id);
                person per = new person();
                while (rs.Read())
                {
                    per.id = id;
                    per.personID = rs["personID"].ToString();
                    per.name = rs["name"].ToString();
                    per.sex = Convert.ToInt32(rs["sex"]);
                    per.date = rs["date"].ToString();
                    per.area = getAreaNameByID(rs["area"].ToString());
                    per.areaid = rs["area"].ToString();
                    //if (rs["street"].ToString() != "")
                    //{
                    //    per.street = getStreetNameByID(rs["street"].ToString());
                    //}
                    //else
                    //{
                    //    per.street = rs["street"].ToString();
                    //}
                    //per.street = rs["street"].ToString();
                    //per.streetid = rs["street"].ToString();
                    if (rs["community"].ToString() != "")
                    {
                        per.community = getCommunityNameByID(rs["community"].ToString());
                        if (rs["street"].ToString() != "")
                        {
                            per.street = getStreetNameByID(rs["street"].ToString()) + "/" + per.community + "����";
                        }
                        else
                        {
                            per.street = per.community + "����";
                        }
                        per.streetid = rs["community"].ToString();
                    }
                    else
                    {
                        if (rs["street"].ToString() != "")
                        {
                            per.street = getStreetNameByID(rs["street"].ToString());
                            per.streetid = rs["street"].ToString();
                        }
                        else
                        {
                            per.streetid = rs["area"].ToString();
                        }
                    }
                    per.communityid = rs["community"].ToString();
                    per.phone = rs["phone"].ToString();
                    per.telephone = rs["telephone"].ToString();
                    per.organid = rs["organ"].ToString().Trim();
                    if (rs["organ"].ToString() != "")
                    {
                        per.organ = getDeptNameByID(rs["organ"].ToString());
                    }
                    else
                    {
                        per.organ = rs["organ"].ToString();
                    }
                    per.degree = rs["degree"].ToString();
                    per.technology = rs["technology"].ToString();
                    per.type = rs["type"].ToString();
                    per.character = rs["character"].ToString();
                    per.address = rs["address"].ToString();
                }
                return per;
            }
            catch(Exception ex)  
            {
                throw ex;
            }
        }
        #endregion
        
        #region getDeptNameByID : ���ݲ���id�õ���������

        public string getDeptNameByID(string id)
        {
            SqlDataReader rs = null;
            try
            {
                if (id.Length != 5)
                {
                   rs = (SqlDataReader)personDAO.getDeptNameByID(id);
                }
                else
                {
                    rs = (SqlDataReader)personDAO.getDeptNameByIDOutDept(id);
                }
                string name = "";
                while (rs.Read())
                {
                    name = rs["departname"].ToString();
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getAreaNameByID : ����id�õ����������

        public string getAreaNameByID(string id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)personDAO.getAreaNameByID(id);
                string name = "";
                while (rs.Read())
                {
                    name = rs["areaname"].ToString();
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getStreetNameByID : ����id�õ��ֵ�����
        public string getStreetNameByID(string id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)personDAO.getStreetNameByID(id);
                string name = "";
                while (rs.Read())
                {
                    name = rs["streetname"].ToString();
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getCommunityNameByID : ����id�õ�����������

        public string getCommunityNameByID(string id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)personDAO.getCommunityNameByID(id);
                string name = "";
                while (rs.Read())
                {
                    name = rs["commname"].ToString();
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetStreetByAreaId : �ֵ�����
        public ArrayList GetStreetByAreaId(string areacode)
        {
            ArrayList streets = new ArrayList();
            string sql = "select streetcode,streetname from s_street where areacode='" + areacode + "'";
            try
            {
                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);

                while (rs.Read())
                {
                    string[] street = new string[2];
                    street[1] = rs["streetname"].ToString();
                    street[0] = rs["streetcode"].ToString();
                    streets.Add(street);
                }

                rs.Close();
                return streets;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
#endregion
	}
}
