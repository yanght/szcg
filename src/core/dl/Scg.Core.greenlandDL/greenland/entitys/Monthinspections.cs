using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.greenland.entitys
{
    public class Monthinspections
    {
        public int recid;
        public string date1;
        public string date2;
        public string areacode;
        public string streetcode;
        public string streetname;
        public string commcode;
        public string commname;
        public int type;
        public string person;
        public string dealdate;
        public string address;
        public string outerdepartcode;
        public string departname;
        public string problem;
        public string remark;
        public void setEmpty()
        {
            recid = 0;
            date1 = "";
            date2 = "";
            areacode = "";
            streetcode = "";
            streetname = "";
            commcode = "";
            commname = "";
            type = 0;
            person = "";
            dealdate = "";
            address = "";
            outerdepartcode = "";
            departname = "";
            problem = "";
            remark = "";
        }
    }
}
