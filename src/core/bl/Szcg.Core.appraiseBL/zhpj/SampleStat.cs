using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


using bacgDL.zhpj;

namespace bacgBL.zhpj
{
    class SampleStat
    {
        private static DateTime dateSample1;
        private static DateTime dateSample2;
        private static DateTime dt1;
        private static DateTime dt2;
        private static bool isSampled1;
        private static bool isSampled2;
        private static bool isSampled3;
        private static bool isSampled4;
        static SampleStat()
        {
           dateSample1=new DateTime();
           dateSample2=new DateTime();
           isSampled1 = false;
           isSampled2 = false;
           isSampled3 = false;
           isSampled4 = false;
        }

        public static void SetSampleData(Hashtable ht,int seconds)
        {
            //SampleStatManage ssm = new SampleStatManage();
            TimeSpan  timespan;
            UpdateSampleInit();
            timespan = DateTime.Now - dt1;
            if (System.Math.Abs(timespan.TotalSeconds) <= seconds)
            {
                if (!isSampled1)
                {
                    //ssm.InsertSampleData(ht, dt1);
                    isSampled1 = true;
                }
                return;
            }
            timespan = DateTime.Now - dt2;
            if (System.Math.Abs(timespan.TotalSeconds) <= seconds)
            {
                if (!isSampled2)
                {
                    //ssm.InsertSampleData(ht, dt2);
                    isSampled2 = true;
                }
                return;
            }
            timespan = DateTime.Now - dateSample1;
            if (System.Math.Abs(timespan.TotalSeconds) <= seconds)
            {
                if (!isSampled3)
                {
                    //ssm.InsertSampleData(ht, dateSample1);
                    isSampled3 = true;
                }
                return;
            }
            timespan = DateTime.Now - dateSample2;
            if (System.Math.Abs(timespan.TotalSeconds) <= seconds)
            {
                if (!isSampled4)
                {
                    //ssm.InsertSampleData(ht, dateSample2);
                    isSampled4 = true;
                }
                return;
            }
        }

        public static void UpdateSampleInit()
        {
            bool tag=false;
            if(dateSample1==new DateTime())
            {
                tag=true;
            }
            else
            {
                if(DateTime.Now.ToString("yyMMdd")!=dateSample1.ToString("yyMMdd"))
                {
                    tag=true;
                }
            }
            if (dateSample2 == new DateTime())
            {
                tag=true;
            }
            else
            {
                if(DateTime.Now.ToString("yyMMdd")!=dateSample2.ToString("yyMMdd"))
                {
                    tag=true;
                }
            }
            if(tag)
            {
                dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 10, 0);//‘§¡Ùª∫≥Â ±º‰
                dt2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 50, 0);//‘§¡Ùª∫≥Â ±º‰
                isSampled1 = false;
                isSampled2 = false;
                isSampled3 = false;
                isSampled4 = false;
                Random random = new Random();
                int[] hour=new int[]{7,8,9,10,11,14,15,16,17,20,21,22};
                DateTime date=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
                int indexStart=0,indexEnd=0;
                while(System.Math.Abs(indexStart-indexEnd)>3)
                {
                    indexStart=random.Next(0,11);
                    indexEnd =random.Next(0,11);
                }
                dateSample1 =date.AddHours(hour[indexStart]);
                dateSample1.AddMinutes(random.Next(10,50));//‘§¡Ùª∫≥Â ±º‰
                dateSample2 =date.AddHours(hour[indexEnd]);
                dateSample2.AddMinutes(random.Next(10, 50));//‘§¡Ùª∫≥Â ±º‰
            }
        }

    }
}
