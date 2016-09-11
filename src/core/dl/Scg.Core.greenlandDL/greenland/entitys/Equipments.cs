using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.greenland.entitys
{
    public class Equipments
    {
        public int recid;
        public string date1;
        public string date2;
        public string departcode;
        public string departname;
        public int catalog;
        public string spec;
        public string type;
        public string address;
        public string dealdate;
        public string photo;
        public string remark;
        public void setEmpty()
        {
            recid = 0;
            date1 = "";
            date2 = "";
            departcode = "";
            departname = "";
            catalog = 0;
            spec = "";
            type = "";
            address = "";
            dealdate = "";
            photo = "";
            remark = "";
        }
    }
}
