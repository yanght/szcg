using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL.greenland.maintenances;
using System.Data;
using Teamax.Common;

namespace bacgBL.greenland
{
    public class RCManager
    {
        RCMaintenanceDao rcyhDao = new RCMaintenanceDao();

        //�õ�Ѳ����Ϣ�б�
        public PageManage GetAllRcyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = rcyhDao.GetAllRcyhResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //�õ���ֲ������Ϣ�б�
        public PageManage GetAllZzyhResult(Maintenances pat, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = rcyhDao.GetAllZzyhResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����һ��Ѳ����Ϣ
        public int insertIntoRcyh(Maintenances mat)
        {
            try
            {
                int i = rcyhDao.insertIntoRcyh(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //����һ��Ѳ����Ϣ
        public int updateIntoRcyh(Maintenances mat)
        {
            try
            {
                int i = rcyhDao.updateIntoRcyh(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //ɾ��Ѳ����Ϣ
        public int deleteFromRcyh(int recid)
        {
            try
            {
                int i = rcyhDao.deleteFromRcyh(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //��������recid�õ���Ѳ����Ϣ
        public Maintenances getRcyhInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)rcyhDao.getRcyhInfoByID(id);
                Maintenances per = new Maintenances();
                while (rs.Read())
                {
                    per.recid = id;
                    per.operatetype = Convert.ToInt32(rs["operatetype"]);
                    per.streetcode = rs["streetcode"].ToString();
                    per.streetname = rs["streetname"].ToString();
                    per.areacode = rs["areacode"].ToString();
                    per.commcode = rs["commcode"].ToString();
                    per.commname = rs["commname"].ToString();
                    per.outerdepartcode = rs["outerdepartcode"].ToString();
                    per.outerdepartname = rs["departname"].ToString();
                    per.situation = rs["situation"].ToString();
                    per.spec = rs["spec"].ToString();
                    per.person = rs["person"].ToString();
                    per.address = rs["address"].ToString();
                    per.acreage = rs["acreage"].ToString();
                    per.amount = rs["amount"].ToString();
                    per.dealdate = rs["dealdate"].ToString();
                    per.growname = rs["growname"].ToString();
                    per.remark = rs["remark"].ToString();
                }
                return per;
            }
            catch
            {
                throw;
            }
        }
    }
}
