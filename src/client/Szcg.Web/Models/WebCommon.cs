using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Szcg.Web.Models
{
    public class WebCommon
    {
        public string GetProjectSource(string projsource)
        {
            string rtn = string.Empty;

            switch (projsource)
            { 
                case "0":
                    rtn = "公众举报";
                    break;
                case "1":
                    rtn = "监督员上报";
                    break;
                case "2":
                    rtn = "领导交办";
                    break;
                case "3":
                    rtn = "网站举报";
                    break;
                case "4":
                    rtn = "传真举报";
                    break;
                case "5":
                    rtn = "短信举报";
                    break;
                case "6":
                    rtn = "信访举报";
                    break;
                case "7":
                    rtn = "媒体举报";
                    break;
                case "8":
                    rtn = "邮件举报";
                    break;
                case "9":
                    rtn = "其他举报";
                    break;
                case "10":
                    rtn = "监督员快速上报";
                    break;
                case "-1":
                    rtn = "坐席拒接";
                    break;
                case "11":
                    rtn = "电话举报";
                    break;
                case "15":
                    rtn = "微信举报";
                    break;
                case "16":
                    rtn = "监控抓拍";
                    break;
                default:
                    rtn = "未知";
                    break;
            }

            return rtn;
        }
    }
}