using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.szbase
{
    public class DepartModel:BaseModel
    {

        private string _departCode;
        public string DepartCode
        {
            get { return _departCode; }
            set { _departCode = value; }
        }

        private string _userDefineCode;
        public string UserDefineCode
        {
            get { return _userDefineCode; }
            set { _userDefineCode = value; }
        }
        private string _departName;
        public string DepartName
        {
            get { return _departName; }
            set { _departName = value; }

        }
        private string _parentCode;
        public string ParentCode
        {
            get { return _parentCode; }
            set { _parentCode = value; }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _principal;
        public string Principal
        {
            get { return _principal; }
            set { _principal = value; }
        }

        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }        
        
        
        private string _tel;
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }        
        private string _departAddress;
        public string DepartAddress
        {
            get { return _departAddress; }
            set { _departAddress = value; }
        }
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
