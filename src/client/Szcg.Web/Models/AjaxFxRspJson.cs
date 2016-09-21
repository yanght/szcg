using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Szcg.Web
{
    /// <summary>
    /// Ajax 通信的返回 Json 串
    /// </summary>
    public class AjaxFxRspJson : ActionResult
    {

        /// <summary>
        /// 调用的方法编码
        /// </summary>
        public string Method
        {
            get;
            set;
        }

        /// <summary>
        /// 返回代码 0 失败 1 成功
        /// </summary>
        public int RspCode
        {
            get;
            set;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string RspMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        public JObject RspData
        {
            get;
            set;
        }

        /// <summary>
        /// miniui 使用
        /// </summary>
        public JObject MiniUI
        {
            get;
            set;
        }

        /// <summary>
        /// miniui 使用
        /// </summary>
        public string MiniUIJson
        {
            get;
            set;
        }

        /// <summary>
        /// Ajax 通信的返回 Json 串
        /// </summary>
        public AjaxFxRspJson()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Ajax 通信的返回 Json 串
        /// </summary>
        /// <param name="method">调用的方法编码</param>
        public AjaxFxRspJson(string method)
        {
            this.Method = method;
            this.RspCode = 0;
            this.RspMsg = string.Empty;
            this.RspData = new JObject();
            this.MiniUI = new JObject();
        }

        /// <summary>
        /// Ajax 通信的返回 Json 串
        /// </summary>
        /// <param name="method">调用的方法编码</param>
        public AjaxFxRspJson(HttpContext context)
        {
            this.Method = context.Request.QueryString["method"];
            this.RspCode = 0;
            this.RspMsg = string.Empty;
            this.RspData = new JObject();
            this.MiniUI = new JObject();
        }

        /// <summary>
        /// 将对象转化为 Json 字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            if (!string.IsNullOrEmpty(this.MiniUIJson))
            {
                return this.MiniUIJson;
            }
            else if (this.MiniUI != null && this.MiniUI.Count > 0)
            {
                return JsonConvert.SerializeObject(this.MiniUI, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            }
            else
            {
                JObject jo = new JObject();
                jo.Add("Method", this.Method);
                jo.Add("RspCode", this.RspCode);
                jo.Add("RspMsg", this.RspMsg);
                jo.Add("RspData", this.RspData);
                return JsonConvert.SerializeObject(jo, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write(this.ToJson());
        }

    }
}