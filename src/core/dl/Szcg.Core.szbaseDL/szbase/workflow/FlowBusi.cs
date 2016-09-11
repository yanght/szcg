using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business.workflow
{
    public class FlowBusi
    {
        private string flowbusistatusid;
        private string flowinfoid;
        private string flownodeid;
        private string businame;
        private string busistatus;
        private string status;
        private string remark;

        public string Flowbusistatusid
        {
            get { return flowbusistatusid; }
            set { flowbusistatusid = value; }
        }
        public string Flowinfoid
        {
            get { return flowinfoid; }
            set { flowinfoid = value; }
        }
        public string Flownodeid
        {
            get { return flownodeid; }
            set { flownodeid = value; }
        }

        public string Businame
        {
            get { return businame; }
            set { businame = value; }
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
