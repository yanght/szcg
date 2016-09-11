using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;

using Teamax.Common;
using bacgDL.business;
namespace bacgBL.zhpj
{
    public class Apraise_lead
    {

        public bool Addfeedback(string FeedUserCode, string FeedUserName, string FeedbackContent, string AppraiseModule, string AppraiseModuleWhere, string AppraiseTimeArea,string dcode)
        {
                bacgDL.zhpj.Appraise_lead al = new bacgDL.zhpj.Appraise_lead();
                bool r = al.add(FeedUserCode, FeedUserName, FeedbackContent, AppraiseModule, AppraiseModuleWhere, AppraiseTimeArea);
                //添加业务消息
                DataSet ds = al.getUserCode(dcode);
                string do_usercode = ds.Tables[0].Rows[0][0].ToString();
                if (do_usercode != "")
                {
                    wdxx x = new wdxx();
                    string refmessage = "";
                    x.InsertBusinessMsg(FeedUserCode, do_usercode, FeedUserName + "对" + AppraiseModule +AppraiseTimeArea+"评价批示", FeedbackContent, refmessage, "");
                }
                return r;
    

        }
        public DataSet Getfeedback(string appraisemodule)
        {
            bacgDL.zhpj.Appraise_lead al = new bacgDL.zhpj.Appraise_lead();
            return al.get(appraisemodule);
        }
    }
}
