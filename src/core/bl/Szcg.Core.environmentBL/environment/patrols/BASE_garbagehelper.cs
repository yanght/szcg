using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using bacgDL.environment.patrols;
using System.Data;
using Teamax.Common;

namespace bacgBL.environment.patrols
{
    public class BASE_garbagehelper
    {
        GarbagecleanDAO garbagecleanDAO = new GarbagecleanDAO();

        public BASE_garbagehelper()
        {
            //
            //
        }

        //�õ���������������Ϣ�б�
        public Teamax.Common.PageManage GetAllGarbageResult(garbageclean pat, int pageIndex, int pageSize)
        {
            try
            {
                Teamax.Common.PageManage page = garbagecleanDAO.GetAllGarbageResult(pat, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //�ж��Ƿ�����ظ���¼
        public bool GetHasMessage(garbageclean gar)
        {
            try
            {
                bool b = garbagecleanDAO.GetHasMessage(gar);
                return b;
            }
            catch
            {
                throw;
            }
        }

        //����һ���������˹�����Ϣ
        public int insertIntoGarbage(garbageclean garbage)
        {
            try
            {
                int i = garbagecleanDAO.insertIntoGarbage(garbage);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //����һ���������˹�����Ϣ
        public int updateIntoGarbage(garbageclean garbage)
        {
            try
            {
                int i = garbagecleanDAO.updateIntoGarbage(garbage);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //ɾ��һ���������˹�����Ϣ
        public int deleteFromGarbage(int id)
        {
            try
            {
                int i = garbagecleanDAO.deleteFromGarbage(id);
                return i;
            }
            catch
            {
                throw;
            }
        }


        //��������id�õ����������˹�����Ϣ
        public garbageclean getGarbageInfoByID(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)garbagecleanDAO.getGarbageInfoByID(id);
                garbageclean gar = new garbageclean();
                while (rs.Read())
                {
                    gar.id = id;
                    gar.license = rs["license"].ToString();
                    gar.departcode = rs["departcode"].ToString();
                    gar.departname = rs["departname"].ToString();
                    gar.servicedepartcode = rs["servicedepartcode"].ToString();
                    gar.servicedepartname = rs["servicedepartname"].ToString();
                    gar.objcode = rs["objcode"].ToString();
                    gar.commcode = rs["commcode"].ToString();
                    gar.commname = rs["commname"].ToString();
                    gar.areacode = rs["areacode"].ToString();
                    //gar.objinputtype = rs["objinputtype"].ToString();
                    gar.dealdate = rs["dealdate"].ToString();
                    gar.actualclearnum = Convert.ToDecimal(rs["actualclearnum"]);
                    gar.remark = rs["remark"].ToString();
                }
                return gar;
            }
            catch
            {
                throw;
            }
        }
    }
}
