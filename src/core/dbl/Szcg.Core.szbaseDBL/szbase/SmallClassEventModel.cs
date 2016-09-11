using System;


namespace DBbase.szbase
{
    /// <summary>
    /// 小类
    /// </summary>
    public class SmallClassEventModel:BaseModel
    {
        private string _bigEventName;
        /// <summary>
        /// BigEventName
        /// </summary>
        public string BigEventName
        {
            get { return _bigEventName; }
            set { _bigEventName = value; }
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



    }
}
