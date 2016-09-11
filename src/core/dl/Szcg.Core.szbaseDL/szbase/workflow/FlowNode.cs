using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business.workflow
{
    public class FlowNode
    {
        private string flowinfoid;
        private string flownodeid;
        private string flownodename;
        private string busideal;
        private int sequence;
        private string status;
        private string property;
        private string remark;

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
        public string Flownodename
        {
            get { return flownodename; }
            set { flownodename = value; }
        }
        public string Busideal
        {
            get { return busideal; }
            set { busideal = value; }
        }
        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Property
        {
            get { return property; }
            set { property = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
