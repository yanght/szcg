using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL;
using bacgDL.greenland.Equipmentmanages;
using System.Data;

namespace bacgBL.greenland
{
    public class Lhsbmanage
    {
        EquipmentmanageDao lhsbDao = new EquipmentmanageDao();

        //得到巡查信息列表
        public PageManage GetAllLhsbResult(Equipments pat, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = lhsbDao.GetAllLhsbResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条巡查信息
        public int insertIntoYhsb(Equipments mat)
        {
            try
            {
                int i = lhsbDao.insertIntoLhsb(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条巡查信息
        public int updateIntoYhsb(Equipments mat)
        {
            try
            {
                int i = lhsbDao.updateIntoLhsb(mat);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除巡查信息
        public int deleteFromYhsb(int recid)
        {
            try
            {
                int i = lhsbDao.deleteFromLhsb(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //根据主键recid得到该巡查信息
        public Equipments getYhsbInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)lhsbDao.getLhsbInfoByID(id);
                Equipments per = new Equipments();
                while (rs.Read())
                {
                    per.recid = id;
                    per.departcode = rs["departcode"].ToString();
                    per.departname = rs["departname"].ToString();
                    per.spec = rs["equipmentspec"].ToString();
                    per.catalog = Convert.ToInt32(rs["equipmentcatalog"]);
                    per.address = rs["address"].ToString();
                    per.type = rs["equipmenttype"].ToString();
                    per.dealdate = rs["dealdate"].ToString();
                    per.photo = rs["photo"].ToString();
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

        // 得到养护设备类型基础数据;
        public DataSet GetEquipment()
        {
            try
            {
                return lhsbDao.GetEquipment();
            }
            catch
            {
                throw;
            }
        }
    }
}
