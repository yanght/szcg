/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：养护整改登记类
 * 结构组成：
 * 作    者：lwx
 * 创建日期：2007-09-20
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.greenland.entitys
{
    public class regulationcls
    {
        public int recid;
        public string date1;
        public string date2;
        public string areacode;
        public string streetcode;
        public string streetname;
        public string commcode;
        public string commname;
        public int outerdepartcode;
        public string outerdepartname;
        public string address;
        public string problem;
        public string content;
        public string result;
        public string startdate;
        public string enddate;
        public string dealdate;
        public string person;
        // public  photo;
        //public string remark;

        public void setEmpty()
        {
            recid=-1;
            date1 = "";
            date2 = "";
            areacode="";
            streetcode="";
            streetname="";
            commcode="";
            commname = "";
            outerdepartcode = -1;
            outerdepartname = "";
            address = "";
            problem = "";
            content = "";
            result = "";
            startdate = "";
            enddate = "";
            dealdate = "";
            person = "";
            // public  photo;
            //remark = "";
        }
    }

}
