using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL;
using bacgDL.greenland.parkmanagers;
using System.Data;

namespace bacgBL.greenland
{
    public class Parksmanager
    {
        parksmanagerDao parksDao = new parksmanagerDao();

        //�õ���԰�̵���Ϣ�б�
        public PageManage GetAllParksResult(ParksInfo pat, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = parksDao.GetAllParksResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����һ����԰�̵���Ϣ
        public int insertIntoParks(ParksInfo mat)
        {
            try
            {
                int i = parksDao.insertIntoParks(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //����һ����԰�̵���Ϣ
        public int updateIntoParks(ParksInfo mat)
        {
            try
            {
                int i = parksDao.updateIntoParks(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //ɾ��Ѳ����Ϣ
        public int deleteFromParks(int recid)
        {
            try
            {
                int i = parksDao.deleteFromParks(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //��������recid�õ���Ѳ����Ϣ
        public ParksInfo getYhsbInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)parksDao.getParksInfoByID(id);
                ParksInfo per = new ParksInfo();
                while (rs.Read())
                {
                    per.recid = id;
                    per.parkcode = rs["parkcode"].ToString();
                    per.parkname = rs["parkname"].ToString();
                    per.streetcode = rs["streetcode"].ToString();
                    per.streetname = rs["streetname"].ToString();
                    per.departcode = rs["departcode"].ToString();
                    per.departname = rs["departname"].ToString();
                    per.ownercode = rs["ownercode"].ToString();
                    per.ownername = rs["ownername"].ToString();
                    per.parkaddress = rs["parkaddress"].ToString();
                    per.parktype = rs["parktype"].ToString();
                    per.redarea = Convert.ToDecimal(rs["redarea"]);
                    per.landarea = Convert.ToDecimal(rs["landarea"]);
                    per.waterarea = Convert.ToDecimal(rs["waterarea"]);
                    per.house = rs["house"].ToString();
                    per.phone = rs["phone"].ToString();
                    per.establishment = rs["establishment"].ToString();
                    per.ordate = rs["ordate"].ToString();                    
                    per.remark = rs["remark"].ToString();
                }
                rs.Close();
                return per;
            }
            catch
            {
                throw;
            }
        } 
    }
}
