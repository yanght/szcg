using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.szbase
{
    public class BigClass:BaseModel
    {
        private string _code;
        /// <summary>
        /// ����
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _name;
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
