/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统清理垃圾数据库操作
 * 结构组成：
 * 作    者：鲁伟兴
 * 创建日期：2007-09-02
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.environment.entitys
{
    public class acceptplacecls
	{
        public int id;
        public string acceptplacecode;
        public string acceptplacename;
        public string dealdate;
        public decimal grefivenum;
        public decimal lesfivenum;
        public decimal acceptdirtnum;
        public decimal flatsoliddirtnum;
        public int germicidal;
        public string germicidalname;
        public string remark;

        //查询过程中使用
        public string date1;
        public string date2;
        
        public void setEmpty()
        {
            id = 0;
            acceptplacecode    ="";
            acceptplacename    ="";
            dealdate           ="";
            grefivenum         = 0;
            lesfivenum         = 0;
            acceptdirtnum      = 0;
            flatsoliddirtnum   = 0;
            germicidal         = 0;
            germicidalname     ="";
            remark             ="";

            date1              ="";
            date2              ="";   
        }
    }
    

}

