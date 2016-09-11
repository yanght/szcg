using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business.workflow
{
    public class Flowinfo
    {
        private string flowinfoid;
        private string flowname;
        private int creatdate;
        private string status;
        private string flowversion;
        private string remark;

        public string Flowinfoid
        {
            get { return flowinfoid; }
            set { flowinfoid = value; }
        }
        public string Flowname
        {
            get { return flowname; }
            set { flowname = value; }
        }
        public int Creatdate
        {
            get { return creatdate; }
            set { creatdate = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Flowversion
        {
            get { return flowversion; }
            set { flowversion = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
