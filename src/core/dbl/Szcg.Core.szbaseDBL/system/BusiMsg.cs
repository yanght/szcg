using System;
using System.Collections.Generic;

using System.Text;

namespace szbaseDBL.system
{
    public class BusiMsg
    {
        private int _projcode;
        private string _mobile;

        public BusiMsg()
        {
        }

        public int projcode 
        {
            get { return _projcode; }
            set { _projcode = value; }
        }

        public string mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
    }

}
