using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.greenland.entitys
{
    public class Maintenances
    {
        public int recid;
        public string date1;
        public string date2;
        public int operatetype;
        public string areacode;
        public string streetcode;
        public string streetname;
        public string commcode;
        public string commname;
        public string outerdepartcode;
        public string outerdepartname; 
        public string address;
        public string person;
        public string growname;
        public string spec;
        public string amount;
        public string acreage;
        public string situation;
        public string dealdate;
        public string remark;
        public void setEmpty()
        {
            recid = 0;
            date1 = "";
            date2 = "";
            operatetype = -1;
            areacode = "";
            streetcode = "";
            streetname = "";
            commcode = "";
            commname = "";
            outerdepartcode = "";
            outerdepartname = "";
            address = "";
            person = "";
            growname = "";
            spec = "";
            amount = "";
            acreage = "";
            situation = "";
            dealdate = "";
            remark = "";
        }
    }
}
