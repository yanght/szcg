namespace bacgDL.environment.entitys
{
    public class LJsweep
    {
        public int id;
        public string departcode;
        public string departname;
        public string date1;
        public string date2;
        public string servicedeptcode;
        public string servicedeptname;
        public string areacode;
        public string strcode;
        public string strname;
        public string commcode;
        public string commname;
        public string sweepaddress;
        public string sweepdate;
        public string aclswpnum;
        public string planswpnum;
        public string censure;
        public string problem;
        public string remark;
        public void setEmpty()
        {
            id = 0;
            date1 = "";
            date2 = "";
            departcode = "";
            departname = "";
            servicedeptcode = "";
            servicedeptname = "";
            areacode = "";
            strcode = "";
            strname = "";
            commcode = "";
            commname = "";
            sweepaddress = "";
            sweepdate = "";
            aclswpnum = "";
            planswpnum = "";
            censure = "";
            problem = "";
            remark = "";
        }
    }
}
