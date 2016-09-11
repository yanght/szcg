using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.greenland.entitys;
using bacgDL;
using bacgDL.greenland.regulation;
using System.Data;

namespace bacgBL.greenland
{
    public class regulationmanage
    {
        regulationdao regulationdao1 = new regulationdao();
        public regulationmanage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        //�������������õ����������б�
        public PageManage getallregulationlist(regulationcls rec, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = regulationdao1.getregulationlist(rec, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //����һ������������Ϣ
        public int insertIntorecord(regulationcls rec)
        {

            try
            {
                int i = regulationdao1.insertIntorecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //����һ������������Ϣ
        public int updateonerecord(regulationcls rec)
        {
            try
            {
                int i = regulationdao1.updateonerecord(rec);
                return i;
            }
            catch
            {
                throw;
            }


        }
        //ɾ����������Ա��
        public int deleteonerecord(int id)
        {
            try
            {
                int i = regulationdao1.deleteonerecord(id);
                return i;
            }
            catch
            {
                throw;
            }

        }
        //��������id�õ�����������Ϣ
        public regulationcls getpatrolinfobyid(int id)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)regulationdao1.getregulationinfobyid(id);
                regulationcls rec = new regulationcls();
                while (rs.Read())
                {
                    rec.recid = id;
                    rec.areacode = rs["areacode"].ToString();
                    rec.streetname = rs["streetname"].ToString();
                    rec.streetcode = rs["streetcode"].ToString();
                    rec.commcode = rs["commcode"].ToString();
                    rec.commname = rs["commname"].ToString();
                    rec.outerdepartcode = Convert.ToInt32(rs["outerdepartcode"]);
                    rec.outerdepartname = rs["departname"].ToString();
                    rec.address = rs["address"].ToString();
                    rec.problem = rs["problem"].ToString();
                    rec.content = rs["content"].ToString();
                    rec.result = rs["result"].ToString();
                    rec.startdate = rs["startdate"].ToString();
                    rec.enddate = rs["enddate"].ToString();
                    rec.dealdate = rs["dealdate"].ToString();
                    rec.person = rs["person"].ToString();

                }
                return rec;
            }
            catch
            {
                throw;
            }
        }

    }
}
