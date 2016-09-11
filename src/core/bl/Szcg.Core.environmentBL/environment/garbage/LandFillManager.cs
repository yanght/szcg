using System;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL.environment.garbage;
using bacgDL;
using Teamax.Common;

namespace bacgBL.environment.garbage
{
    public class LandFillManager
    {
        LandFillDao landfilldao = new LandFillDao();

        //�������������õ����������б�
        public Teamax.Common.PageManage getAllLandFillList(LandFillInfo rec, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = landfilldao.getAllLandFillList(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����һ������������Ϣ
        public int insertIntoLandFill(LandFillInfo rec)
        {

            try
            {
                int i = landfilldao.insertIntoLandFill(rec);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //����һ������������Ϣ
        public int updateIntoLandFill(LandFillInfo rec)
        {
            try
            {
                int i = landfilldao.updateIntoLandFill(rec);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //ɾ����
        public int deleteFromLandFill(int recid)
        {
            try
            {
                int i = landfilldao.deleteFromLandFill(recid);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //��������id�õ�����������Ϣ
        public LandFillInfo getLandFillInfoByid(int recid)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)landfilldao.getLandFillInfoByID(recid);
                LandFillInfo rec = new LandFillInfo();
                while (rs.Read())
                {
                    rec.recid = recid;
                    rec.objcode = rs["objcode"].ToString() ;
                    rec.objname =rs["objname"].ToString();
                    rec.filllayer = rs["filllayer"].ToString();
                    rec.fillarea = rs["fillarea"].ToString();
                    rec.fillno = rs["fillno"].ToString();
                    rec.junktype = Convert.ToInt32(rs["junktype"]);
                    rec.junkorigin = rs["junkorigin"].ToString();
                    rec.junknum = Convert.ToDecimal(rs["junknum"]);
                    rec.air = rs["air"].ToString();
                    rec.percolatenum = Convert.ToDecimal(rs["percolatenum"]);
                    rec.water = Convert.ToDecimal(rs["water"]);
                    rec.evaporationnum = Convert.ToDecimal(rs["evaporationnum"]);
                    rec.runaspect = rs["runaspect"].ToString();
                    rec.malfunction = rs["malfunction"].ToString();
                    rec.causation = rs["causation"].ToString();
                    rec.filldate = rs["filldate"].ToString();
                    rec.person = rs["person"].ToString();
                    rec.remark = rs["remark"].ToString();
                }                
                rs.Close();
                return rec;
            }
            catch
            {
                throw;
            }
        }
    }
}
