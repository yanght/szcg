namespace bacgDL.environment.entitys
{
    public class patrol
    {
        public int patrolid;
        public string streetcode;
        public string streetname;
        public string departcode;
        public string departname;
        public string patroldate;
        public string areacode;
        public string commcode;
        public string commname;
        public string patrolperson;
        public string patroladdress;
        public string actualnum;
        public string plannum;
        public string censure;
        public string problem;
        public string remark;
        public string photo;

        //以下为查询过程中用的查询条件
        public string date1;  //开始日期
        public string date2;  //结束日期
        public void setEmpty()
        {
            patrolid = 0;
            streetcode = "";
            streetname = "";
            departcode = "";
            departname = "";
            patroldate = "";
            areacode = "";
            commcode = "";
            commname = "";
            patrolperson = "";
            patroladdress = "";
            actualnum = "";
            plannum = "";
            censure = "";
            problem = "";
            photo = "";
            remark = "";
            date1 = "";
            date2 = "";
        }
    }
}
