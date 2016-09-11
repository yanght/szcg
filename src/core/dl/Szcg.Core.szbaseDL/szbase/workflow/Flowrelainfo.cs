using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business.workflow
{
    public class Flowrelainfo
    {
        private string relainfoid;
        private string corenodeid;
        private string nextnodeid;
        private string busistatus;
        private string flowinfoid;
        private string status;
        private string remark;

        public string Relainfoid
        {
            get { return relainfoid; }
            set { relainfoid = value; }
        }
        public string Corenodeid
        {
            get { return corenodeid; }
            set { corenodeid = value; }
        }
        public string Nextnodeid
        {
            get { return nextnodeid; }
            set { nextnodeid = value; }
        }

        public string Flowinfoid
        {
            get { return flowinfoid; }
            set { flowinfoid = value; }
        }
        public string Busistatus
        {
            get { return busistatus; }
            set { busistatus = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
