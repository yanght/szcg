using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.szbase
{
    public class SmallClassPartModel :BaseModel
    {
        private string _bigPartName;
        /// <summary>
        /// BigPartName
        /// </summary>
        public string BigPartName
        {
            get { return _bigPartName; }
            set { _bigPartName = value; }
        }

        private int _id;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _bigClassCode;
        /// <summary>
        /// 大类代码
        /// </summary>
        public string BigClassCode
        {
            get { return _bigClassCode; }
            set { _bigClassCode = value; }
        }

        private string _smallCallCode;
        /// <summary>
        /// 小类代码
        /// </summary>
        public string SmallCallCode
        {
            get { return _smallCallCode; }
            set { _smallCallCode = value; }
        }

        private string _name;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private int _t_Time_Kc;

        public int T_Time_Kc
        {
            get { return _t_Time_Kc; }
            set { _t_Time_Kc = value; }
        }

        private int _t_Time;

        public int T_Time
        {
            get { return _t_Time; }
            set { _t_Time = value; }
        }

        private int _t_Time_Ts;

        public int T_Time_Ts
        {
            get { return _t_Time_Ts; }
            set { _t_Time_Ts = value; }
        }

        private int _t_Time_Gc;

        public int T_Time_Gc
        {
            get { return _t_Time_Gc; }
            set { _t_Time_Gc = value; }
        }

        private string _gisType;

        public string GisType
        {
            get { return _gisType; }
            set { _gisType = value; }
        }



        private int _partSign;

        public int PartSign
        {
            get { return _partSign; }
            set { _partSign = value; }
        }


        private int _roleCode;

        public int RoleCode
        {
            get { return _roleCode; }
            set { _roleCode = value; }
        }


        private string _dutyUnit;

        public string DutyUnit
        {
            get { return _dutyUnit; }
            set { _dutyUnit = value; }
        }

        private string _memo;

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _link_SmallCode;

        public string Link_SmallCode
        {
            get { return _link_SmallCode; }
            set { _link_SmallCode = value; }
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _url2;

        public string Url2
        {
            get { return _url2; }
            set { _url2 = value; }
        }


    }
}
