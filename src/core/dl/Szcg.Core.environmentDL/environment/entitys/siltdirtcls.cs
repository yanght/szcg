using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.environment.entitys
{
    public class siltdirtcls
    {
        public int id;
        public int dealtype;
        public string dealtypename;
        public string streetcode;
        public string streetname;
        public string dealdate;
        public decimal cleardirtnum;
        public decimal washroadnum;
        public decimal sprinklecarnum;
        public decimal newbuilnum;
        public decimal repairbuilnum;
        public string remark;

        //查询统计过程中使用

        public string date1;
        public string date2;

        public void setEmpty()
        {
            id = 0;
            dealtype = 0;
            dealtypename = "";
            streetcode = "";
            streetname = "";
            dealdate = "";
            cleardirtnum = 0;
            washroadnum = 0;
            sprinklecarnum = 0;
            newbuilnum = 0;
            repairbuilnum = 0;
            remark = "";

            date1 = "";
            date2 = "";
        }
    }
    
}
