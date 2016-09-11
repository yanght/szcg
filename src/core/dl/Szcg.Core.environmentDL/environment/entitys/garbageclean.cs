namespace bacgDL.environment.entitys
{
    public class garbageclean
    {
        public int id;
        public string license;
        public string date1;
        public string date2;
        public string departcode;
        public string departname;
        public string servicedepartcode;
        public string servicedepartname;
        public string objcode;
        public string objinputtype;
        public string commcode;
        public string commname;
        public string areacode;
        public string streetcode;
        public string dealdate;
        public decimal actualclearnum;
        public string remark;
        public void setEmpty()
        {
            id = 0;
            date1 = "";
            date2 = "";
            license = "";
            departcode = "";
            departname = "";
            servicedepartcode = "";
            servicedepartname = "";
            objcode = "";
            objinputtype = "";
            commcode = "";
            commname = "";
            areacode = "";
            streetcode = "";
            dealdate = "";
            actualclearnum = 0;
            remark = "";
        }
    }
}
