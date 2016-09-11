using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace bacgDL
{
    public class PageManage
    {
        private DataSet _ds = null;
        private int _PageSize = 15;
        private int _RowCount = 0;
        private int _PageCount = 0;
        public DataSet ds
        {
            get { return _ds; }
            set {
                _ds = value; 
                if (_ds == null)
                {
                    _ds = new DataSet();
                }
                
            }
        }
        public int pageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;
                if (_PageSize == 0)
                {
                    _PageSize = 1;
                }
            }
        }

        public int rowCount
        {
            get { return _RowCount; }
            set
            {
                _RowCount = value;
           
            }
        }

        public int pageCount
        {
            get { return _PageCount; }
            set
            {
                _PageCount = value;

            }
        }
        
    }
}
