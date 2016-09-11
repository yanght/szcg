using System;
using System.Collections.Generic;
using System.Text;

namespace dbl.zhifa
{
    public class projinfo
    {
        private int projcode;
        private string projname;
        private string projaddress;
        private string party;
        private int nodeid;
        private int usercode;
        private string username;
        private int lockusercode;
        private int targetdepartcode;
        private DateTime startdate;
        private DateTime enddate;
        private DateTime locktime;
        private string probsource;
        private int isgreat;
        private int isdel;
        private string functiontype;
        public string busistatus;

        private string casedescribe;
        private string corporation_name;
        private int corporation_sxy;
        private string corporation_age;
        private string corporation_job;
        private string punishmentcode;
        private string punishmentname;
        private string punishmenttime;
        private string doend;
        private string domethod;
        private string punishmentzxqk;
        private string punishmentstatus;
        private string dodate;
        private string summary;
        private string memo;
        private string enddealoption;
        private int currentnodeid;
        private string currentnodename;
        private int currentbusistatus;
        private string currentbusistatusname;

        private DateTime cu_date;
        private int cu_usercode;
        private int cu_departcode;
        private string deal_opinion;
        public string addressx;
        public string addressy;

        public int Projcode
        {
            get { return projcode; }
            set { projcode = value; }
        }
        public string Projname
        {
            get { return projname; }
            set
            {
                if (value.Length > 20)
                    projname = value.Substring(0, 20);
                else
                    projname = value;
            }
        }
        public string Projaddress
        {
            get { return projaddress; }
            set
            {
                if (value.Length > 50)
                    projaddress = value.Substring(0, 50);
                else
                    projaddress = value;
            }
        }

        public string Party
        {
            get { return party; }
            set
            {
                if (value.Length > 20)
                    party = value.Substring(0, 20);
                else
                    party = value;
            }
        }
        public int Nodeid
        {
            get { return nodeid; }
            set { nodeid = value; }
        }

        public int Usercode
        {
            get { return usercode; }
            set { usercode = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        public int Lockusercode
        {
            get { return lockusercode; }
            set { lockusercode = value; }
        }
        public int Targetdepartcode
        {
            get { return targetdepartcode; }
            set { targetdepartcode = value; }
        }
        public DateTime Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }

        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        public DateTime Locktime
        {
            get { return locktime; }
            set { locktime = value; }
        }

        public string Probsource
        {
            get { return probsource; }
            set
            {
                if (value.Length > 40)
                    probsource = value.Substring(0, 40);
                else
                    probsource = value;
            }
        }
        public int Isgreat
        {
            get { return isgreat; }
            set { isgreat = value; }
        }
        public int Isdel
        {
            get { return isdel; }
            set { isdel = value; }
        }
        public string Functiontype
        {
            get { return functiontype; }
            set { functiontype = value; }
        }

        public string Casedescribe
        {
            get { return casedescribe; }
            set
            {
                if (value.Length > 2000)
                    casedescribe = value.Substring(0, 2000);
                else
                    casedescribe = value;
            }
        }
        public string Corporation_name
        {
            get { return corporation_name; }
            set
            {
                if (value.Length > 20)
                    corporation_name = value.Substring(0, 20);
                else
                    corporation_name = value;
            }
        }
        public int Corporation_sxy
        {
            get { return corporation_sxy; }
            set { corporation_sxy = value; }
        }
        public string Corporation_age
        {
            get { return corporation_age; }
            set
            {
                if (value.Length > 10)
                    corporation_age = value.Substring(0, 10);
                else
                    corporation_age = value;
            }
        }
        public string Corporation_job
        {
            get { return corporation_job; }
            set
            {
                if (value.Length > 20)
                    corporation_job = value.Substring(0, 20);
                else
                    corporation_job = value;
            }
        }
        public string Punishmentcode
        {
            get { return punishmentcode; }
            set
            {
                if (value.Length > 20)
                    punishmentcode = value.Substring(0, 20);
                else
                    punishmentcode = value;
            }
        }
        public string Punishmentname
        {
            get { return punishmentname; }
            set
            {
                if (value.Length > 2000)
                    punishmentname = value.Substring(0, 2000);
                else
                    punishmentname = value;
            }
        }
        public string Punishmenttime
        {
            get { return punishmenttime; }
            set { punishmenttime = value; }
        }
        public string Doend
        {
            get { return doend; }
            set
            {
                if (value.Length > 50)
                    doend = value.Substring(0, 50);
                else
                    doend = value;
            }
        }
        public string Domethod
        {
            get { return domethod; }
            set
            {
                if (value.Length > 50)
                    domethod = value.Substring(0, 50);
                else
                    domethod = value;
            }
        }
        public string Punishmentzxqk
        {
            get { return punishmentzxqk; }
            set
            {
                if (value.Length > 255)
                    punishmentzxqk = value.Substring(0, 255);
                else
                    punishmentzxqk = value;
            }
        }
        public string Punishmentstatus
        {
            get { return punishmentstatus; }
            set
            {
                if (value.Length > 255)
                    punishmentstatus = value.Substring(0, 255);
                else
                    punishmentstatus = value;
            }
        }
        public string Dodate
        {
            get { return dodate; }
            set { dodate = value; }
        }
        public string Summary
        {
            get { return summary; }
            set
            {
                if (value.Length > 2000)
                    summary = value.Substring(0, 2000);
                else
                    summary = value;
            }
        }
        public string Memo
        {
            get { return memo; }
            set
            {
                if (value.Length > 2000)
                    memo = value.Substring(0, 2000);
                else
                    memo = value;
            }
        }

        public string Enddealoption
        {
            get { return enddealoption; }
            set
            {
                if (value.Length > 2000)
                    enddealoption = value.Substring(0, 2000);
                else
                    enddealoption = value;
            }
        }

        public int Currentnodeid
        {
            get { return currentnodeid; }
            set { currentnodeid = value; }
        }
        public int Currentbusistatus
        {
            get { return currentbusistatus; }
            set { currentbusistatus = value; }
        }
        public string Currentbusistatusname
        {
            get { return currentbusistatusname; }
            set { currentbusistatusname = value; }
        }

        public DateTime Cu_date
        {
            get { return cu_date; }
            set { cu_date = value; }
        }
        public int Cu_usercode
        {
            get { return cu_usercode; }
            set { cu_usercode = value; }
        }
        public int Cu_departcode
        {
            get { return cu_departcode; }
            set { cu_departcode = value; }
        }
        public string Deal_opinion
        {
            get { return deal_opinion; }
            set
            {
                if (value.Length > 2000)
                    deal_opinion = value.Substring(0, 2000);
                else
                    deal_opinion = value;
            }
        }
    }
}
