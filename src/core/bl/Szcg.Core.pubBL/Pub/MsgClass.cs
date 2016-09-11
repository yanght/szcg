using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;
using AjaxPro;
namespace bacgBL.Pub
{
    public class MsgClass
    {
        [DllImport("SMEntry.dll")]
        private static extern int SP_Startup(string DBName, string Account, string Password);

        [DllImport("SMEntry.dll")]
        public static extern int SP_Login(string Operator, string Password);

        [DllImport("SMEntry.dll")]
        public static extern void SP_Logout();

        [DllImport("SMEntry.dll")]
        private static extern void SP_Cleanup();

        [DllImport("SMEntry.dll")]
        private static extern int SubmitShortMessage(string AtTime, string SourceAddr, string DestAddr, string Content, uint ContentLen, byte NeedStateReport, string ServiceID, string FeeType, string FeeCode);

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string _ConnectionString = "";

        public static string ConnectionString
        {
            get
            {
                return _ConnectionString == "" ? ConfigurationManager.AppSettings["msgConnString"] : _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }
       
        public static int ShortMessage(string SourceAddr,string DestAddr,string Content)
        {
            string[] strCon  = ConnectionString.Split(';');
            if (SourceAddr.Length != 11)
                SourceAddr = strCon[6];
            try{
               SP_Cleanup();
               int tag= SP_Startup(strCon[0], strCon[1], strCon[2]);
               if (tag == 0)
               {
                   byte[] bContent = Encoding.GetEncoding("GB2312").GetBytes(Content.ToCharArray());
                   SP_Login(strCon[3], strCon[4]);
                   tag = SubmitShortMessage(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), SourceAddr, DestAddr, Content, (uint)bContent.Length, 0, strCon[5], "01", "0");//"MSG0019901"
                   SP_Logout();
                   if (tag != 0)
                   {
                       Teamax.Common.Loger.LogerForOperate.DEBUG(new Teamax.Common.Loger.MyOperateLog("11100", "发送业务短信：" + tag));
                   
                       return 1; 
                   }
                   SP_Cleanup();
                   return 0;
               }
               else
               {
                   Teamax.Common.Loger.LogerForOperate.DEBUG(new Teamax.Common.Loger.MyOperateLog("11100", "发送业务短信：" + tag));
                   return 1;
               }
            }catch (Exception ex){

                Teamax.Common.Loger.LogerForOperate.DEBUG(new Teamax.Common.Loger.MyOperateLog("11100", "发送业务短信："+ex.Message));   //操作日志添加
	
                return 1;
            }
        }

        public static string ShortMessageMulti(string SourceAddr, string[] DestAddr, string Content)
        {
            string[] strCon = ConnectionString.Split(';');
            if (SourceAddr.Length != 11)
                SourceAddr = strCon[6];
            string errlist="";
            try
            {
                SP_Cleanup();
                int tag = SP_Startup(strCon[0], strCon[1], strCon[2]);
                if (tag == 0)
                {
                    byte[] bContent = Encoding.GetEncoding("GB2312").GetBytes(Content.ToCharArray());
                    SP_Login(strCon[3], strCon[4]);
                    for (int i = 0; i < DestAddr.Length; i++)
                    {
                        try
                        {
                            SubmitShortMessage(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), SourceAddr, DestAddr[i], Content, (uint)bContent.Length, 0, strCon[5], "01", "0");
                        }
                        catch
                        {
                            errlist += DestAddr[i]+";";
                        }
                    }
                    errlist.TrimEnd(';');
                    SP_Logout();
                    SP_Cleanup();
                    if (errlist == "")
                        return "0";
                    else
                        return errlist;
                }
                else
                {
                    return "1";
                }
            }
            catch
            {
                return "1";
            }
        }

        [AjaxPro.AjaxMethod]
        public int AjaxShortMessage(string SourceAddr, string DestAddr, string Content)
        {
            string[] strCon = ConnectionString.Split(';');
            if (SourceAddr.Length != 11)
                SourceAddr = strCon[6];
            try
            {
                SP_Cleanup();
                int tag = SP_Startup(strCon[0], strCon[1], strCon[2]);
                if (tag == 0)
                {
                    byte[] bContent = Encoding.GetEncoding("GB2312").GetBytes(Content.ToCharArray());
                    SP_Login(strCon[3], strCon[4]);
                    tag = SubmitShortMessage(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), SourceAddr, DestAddr, Content, (uint)bContent.Length, 0, strCon[5], "01", "0");//"MSG0019901"
                    SP_Logout();
                    if (tag != 0)
                    {
                        return 1;
                    }
                    SP_Cleanup();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
