using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.pub
{
    public class Parking
    {
        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 停车场剩余数量
        /// </summary>
        public int Parkspace { get; set; }
    }

    public class ParkingOption
    {
        public List<Parking1> arrays { get; set; }
    }

    public class Parking1
    {
        public string parkingLot { get; set; }
        public string idleSpots { get; set; }

    }

    public class ProjectWzdj
    {
        public string ProjectCode { get; set; }//案卷编号
        public string IllegalAct { get; set; }//违法类型(1:违章 0：其他)
        public string IllegalName{get;set;}//违法主题
        public DateTime ProjectTime { get; set; }//案卷上报时间
        public string State { get; set; }//案卷状态
        //public string Feadback { get; set; }//案卷处理反馈信息
    }

    public class ProjectDetail
    {
        public string ProjectCode { get; set; }
        public string IllegalAct { get; set; }
        public string IllegalName { get; set; }
        public string ReportName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string GPS { get; set; }
        public string Description { get; set; }
        public string UploadPic { get; set; }
        public string State { get; set; }
        public string Feadback { get; set; }
    }
}
