/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;���̻��ճ�Ѳ����
 * �ṹ��ɣ�
 * ��    �ߣ�lwx
 * �������ڣ�2007-09-20
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
namespace bacgDL.greenland.entitys
{
    public class patrolcls
    {
        public int recid;
        public string date1;
        public string date2;
        public string areacode;
        public string streetcode;
        public string streetname;
        public string commcode;
        public string commname;
        public int departcode;
        public string departname;
        public string person;
        public string patroladdress;
        public int outerdepartcode;
        public string outerdepartname;
        public string problem;
        public string situation;
        public string patroldate;
        // public  photo;
        public string remark;

        public void setEmpty()
        {
            recid = -1;
            date1 = "";
            date2 = "";
            areacode = "";
            streetcode = "";
            streetname = "";
            commcode = "";
            commname = "";
            departcode = -1;
            departname = "";
            person = "";
            patroladdress = "";
            outerdepartcode = -1;
            outerdepartname = "";
            problem = "";
            situation = "";
            patroldate = "";
          // public  photo;
            remark = "";
        }
    }

}
