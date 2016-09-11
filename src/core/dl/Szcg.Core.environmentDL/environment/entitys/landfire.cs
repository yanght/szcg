/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫管理系统垃圾焚烧场数据库操作
 * 结构组成：
 * 作    者：鲁伟兴
 * 创建日期：2007-10-12
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
    public class landfirecls
    {
        public int   recid  =0;
        public string objcode;
        public string date1;
        public string date2;
        public string objname;
        public Decimal sootchroma;
        public Decimal dioxin;
        public string   blackness;
        public Decimal airdelivery;
        public Decimal conum;
        public Decimal hfnum;
        public Decimal hclnum;
        public Decimal hgnum;
        public Decimal asnum;
        public Decimal pbnum;
        public Decimal snnum;
        public Decimal sbnum;
        public Decimal cunum;
        public Decimal mnnum;
        public Decimal heatrate;
        public string dealdate;
        public string person;
        public string remark;

        public void setEmpty()
        {
           recid  =0;
           date1 = "";
           date2 = "";
           objcode ="";
           objname = "";
           sootchroma  =0;
           dioxin   =0;
           blackness  ="";
           airdelivery =0;
           conum  =0;
           hfnum  =0;
           hclnum  =0;
           hgnum  =0;
           asnum  =0;
           pbnum =0;
           snnum =0;
           sbnum  =0;
           cunum =0;
           mnnum =0;
           heatrate =0;
           dealdate  ="";
           person   ="";
           remark = "";
        }
    }

}