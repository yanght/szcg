using System;
using System.Collections.Generic;
using System.Text;

namespace bacgBL.web.szbase.entity
{
    public class Collecter
    {
        private int collcode;
        private string commcode;
        private int timeout;
        private string commname;
        private string gridcode;
        private string numbercode;
        private string collname;
        private string loginname;
        private string pwd;
        private string sex;
        private string mobile;
        private string age;
        private string url;
        private string tel;
        private string address;
        private string memo;
        private string imsiCard;
        private string imeiCard;
        private string regdate;
        private string cudate;
        private string imei;

        public Collecter()
        {
        }

        public string ImsiCard
        {
            get { return imsiCard; }
            set { imsiCard = value; }
        }

        public string ImeiCard
        {
            get { return imeiCard; }
            set { imeiCard = value; }
        }

        public string Regdate
        {
            get { return regdate; }
            set { regdate = value; }
        }

        public string Cudate
        {
            get { return cudate; }
            set { cudate = value; }
        }

        public int CollCode
        {
            get { return collcode; }
            set { collcode = value; }
        }
        public string CommCode
        {
            get { return commcode; }
            set { commcode = value; }
        }
        public string CommName
        {
            get { return commname; }
            set { commname = value; }
        }
        public string GridCode
        {
            get { return gridcode; }
            set { gridcode = value; }
        }
        public string NumberCode
        {
            get { return numbercode; }
            set { numbercode = value; }
        }
        public string CollName
        {
            get { return collname; }
            set { collname = value; }
        }
        public string LoginName
        {
            get { return loginname; }
            set { loginname = value; }
        }
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int TimeOut
        {
            get { return timeout; }
            set { timeout = value; }
        }
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
        public string Imei
        {
            get { return imei; }
            set { imei = value; }
        }
    }
}
