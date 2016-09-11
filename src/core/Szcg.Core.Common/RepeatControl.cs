using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Teamax.Common
{
    /// <summary>
    /// 分级展现数据控件
    /// </summary>
    [ToolboxData("<{0}:RepeatControl runat=server></{0}:RepeatControl>")]
    public class RepeatControl : Literal
    {
        private DataTable _dt;
        private int _pageSize = 10;
        private int _width = 1024;
        private int _height = 400;
        private string _id;
        private string _pid;
        private string _order = "desc";
        private string _field = "1";
        private string _requestPage = "RequestRepeat";
        private string _onClick = "";
        private string _rowData = "";
        private string _columnData = "";
        private string _tagData = "";
       

        /// <summary>
        /// 构造函数

        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="Id"></param>
        public RepeatControl(string Pid, string Id)
        {
            _id = Id;
            _pid = Pid;
        }

        public RepeatControl()
        {
        }

        /// <summary>
        /// 数据源

        /// </summary>
        public DataTable SourceData
        {
             get
            {
                 if(TagData!="" &&_dt==null)
                     _dt = (DataTable)HttpContext.Current.Session[TagData];
                 if (ViewState["RepeatControl_SourceData"] != null && _dt == null)
                    _dt = (DataTable)ViewState["RepeatControl_SourceData"];
                return _dt;
            }
            set
            {
                if (value != null)
                {
                    _dt = value;
                    if (TagData=="")
                        ViewState["RepeatControl_SourceData"] = _dt;
                }
            }
        }

        /// <summary>
        /// 附带数据
        /// </summary>
        public string TagData
        {
            get
            {
                if (ViewState["RepeatControl_tagData"] != null)
                    _tagData = (string)ViewState["RepeatControl_tagData"];
                return _tagData;
            }
            set
            {
                if (value != null)
                {
                    _tagData = value;
                    ViewState["RepeatControl_tagData"] = _tagData;
                }
            }
        }


        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order
        {
            get
            {
                if (ViewState["RepeatControl_order"] != null)
                    _order = (string)ViewState["RepeatControl_order"];
                return _order;
            }
            set
            {
                if (value != null)
                {
                    _order = value;
                    ViewState["RepeatControl_order"] = _order;
                }
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Field
        {
            get
            {
                if (ViewState["RepeatControl_field"] != null)
                    _field = (string)ViewState["RepeatControl_field"];
                return _field;
            }
            set
            {
                if (value != null)
                {
                    _field = value;
                    ViewState["RepeatControl_field"] = _field;
                }
            }
        }

        /// <summary>
        /// 电击事件
        /// </summary>
        public string OnClick
        {
            get
            {
                if (ViewState["RepeatControl_OnClick"] != null)
                    _onClick = (string)ViewState["RepeatControl_OnClick"];
                return _onClick;
            }
            set
            {
                if (value != null)
                {
                    int indexonclick = value.IndexOf(':');
                    if (indexonclick != -1)
                        _onClick = value.Substring(indexonclick + 1);
                    else
                        _onClick = value;
                    ViewState["RepeatControl_OnClick"] = _onClick;
                }
            }
        }

        /// <summary>
        /// 元数据列
        /// </summary>
        public string ColumnData
        {
            get
            {
                if (ViewState["RepeatControl_columnData"] != null)
                    _columnData = (string)ViewState["RepeatControl_columnData"];
                return _columnData;
            }
            set
            {
                if (value != null)
                {
                    _columnData = value;
                   
                    ViewState["RepeatControl_columnData"] = _columnData;
                }
            }
        }
        
        /// <summary>
        /// 需要注册到前台的数据

        /// </summary>
        public string RowData
        {
            get
            {
                if (ViewState["RepeatControl_rowData"] != null)
                    _rowData = (string)ViewState["RepeatControl_rowData"];
                return _rowData;
            }
            set
            {
                if (value != null)
                {
                    _rowData = value;
                   
                    ViewState["RepeatControl_rowData"] = _rowData;
                }
            }
        }
        
        /// <summary>
        /// 控件宽度
        /// </summary>
        public int Width
        {
            get
            {
                if (ViewState["RepeatControl_width"] != null)
                    _width = (int)ViewState["RepeatControl_width"];
                return _width;
            }
            set
            {
                if (value != 0)
                {
                    _width = value;
                    ViewState["RepeatControl_width"] = _width;
                }
            }
        }

        /// <summary>
        /// 控件高度
        /// </summary>
        public int Height
        {
            get
            {
                if (ViewState["RepeatControl_height"] != null)
                    _height = (int)ViewState["RepeatControl_height"];
                return _height;
            }
            set
            {
                if (value != 0)
                {
                    _height = value;
                    ViewState["RepeatControl_height"] = _height;
                }
            }
        }
        
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value != 0)
                    _pageSize = value;
            }
        }
        
        /// <summary>
        /// 数据标示
        /// </summary>
        public string DataID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// AJAX回调页面
        /// </summary>
        public string RequestPage
        {
            get
            {
                return _requestPage;
            }
            set
            {
                _requestPage = value;
            }
        }

        /// <summary>
        /// 数据父标示

        /// </summary>
        public string DataPID
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value;
            }
        }

        /// <summary>
        /// 创建子控件

        /// </summary>
        protected override void CreateChildControls()
        {
            StringBuilder sb = new StringBuilder();
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), BuildingScript());
            sb.Append("<style> .ViewTitle_RC { color:#FFFFFF; background-color: #317A84; font-size:14px; height:22px; text-align:center; top:expression(this.offsetParent.scrollTop-2); }");
            sb.Append(".ViewTitle_RC A:link {color:#FBD939;} .ViewTitle_RC A:visited {color:#FBD939;} .ViewTitle_RC A:hover {color:#FFF;}");
            sb.Append(".Tr_Dark_RC { background-color: #D4E1E5; text-align:center; } .herf_Dark_RC { CURSOR: hand;color: green; text-decoration: underline }</style>");
            sb.Append("<div style=\"SCROLLBAR-HIGHLIGHT-COLOR:#c7ebfb;OVERFLOW:auto;WIDTH:1024px;SCROLLBAR-SHADOW-COLOR:#fff;SCROLLBAR-3DLIGHT-COLOR:#c7ebfb;SCROLLBAR-ARROW-COLOR:#51636b;SCROLLBAR-TRACK-COLOR: #d5f1ff; SCROLLBAR-DARKSHADOW-COLOR:#8cb7c1;SCROLLBAR-BASE-COLOR:#a1c3d2;HEIGHT:" + Height + "px;srollbar-Face-color:#fff\">");
            sb.Append("<table width=\"" + Width + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ellspacing=\"0\"><tr><td id=\"sub_\">");
            sb.Append(GetHtmlTable("", 1, SourceData));
            sb.Append("</td></tr></table></div>");
            
            this.Text = sb.ToString();
        }


        private string GetCacheKey()
        {
            string key = "";
            for (int i = 0; i < SourceData.Columns.Count; i++)
            {
                key += "_" + SourceData.Columns[i].ColumnName;
            }
            key += SourceData.Rows.Count.ToString();
            return key;
        }


        /// <summary>
        /// 获取子页
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetHtmlTable(string pid, int pageIndex, DataTable dt)
        {
            if (dt == null)
                return "";
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            DataRow[] drs=null;
            if(Field!="1")
                drs = dt.Select(_pid + "='" + pid + "'", Field + " " + Order);
            else
                drs = dt.Select(_pid + "='" + pid + "'");
            StringBuilder sb = new StringBuilder();
            sb.Append("\n<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\"><tr height=\"22\" class=\"ViewTitle_RC\" style=\"font-size:12px;\">");
            int colCount = dt.Columns.Count;
            int intWidth = _width / (colCount - 2);
            bool booltag = true;
            for (int i = 0; i < colCount; i++)
            {
                if (dt.Columns[i].ColumnName != DataPID && dt.Columns[i].ColumnName != DataID)
                {
                    string orderHtml = "";
                    if (dt.Columns[i].ColumnName == Field)
                        orderHtml = Order == "asc" ? "↑" : "↓";
                    if (booltag)
                    {
                        booltag = false ;
                        sb.Append("\n<td height=\"22\"  width=\"" + intWidth + "\">");
                    }
                    else
                    {
                        sb.Append("\n<td  height=\"22\" width=\"" + intWidth + "\">");
                    }
                    sb.Append("<a href=\"JavaScript:xl_order(" + pageIndex + ",'" + pid + "','sub_" + pid + "','" + (Order == "asc" ? "desc" : "asc") + "','" + dt.Columns[i].ColumnName + "');\">" + dt.Columns[i].Caption + orderHtml + "</a></td>");
                }
            }
            sb.Append("</tr></table>");

            for (int i = _pageSize * (pageIndex - 1); i < drs.Length && i < (_pageSize * pageIndex) && i >= (_pageSize * (pageIndex - 1)); i++)
            {
                sb.Append("\n<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\"><tr onclick='changeColor(this);' class=\"Tr_Dark_RC\">");
                int index = 0;
                booltag = true;
                for (int j = 0; j < colCount; j++)
                {
                    if (dt.Columns[j].ColumnName != DataPID && dt.Columns[j].ColumnName != DataID)
                    {
                        index++;
                        if (booltag)
                        {
                            booltag = false;
                            sb.Append("\n<td height=\"22\"  width=\"" + intWidth + "\">");
                            //sb.Append("\n<td height=\"22\">");
                        }
                        else
                        {
                            sb.Append("\n<td height=\"22\"  width=\"" + intWidth + "\">");
                            //sb.AppendFormat("\n<td width=\"{0}\">", intWidth);
                        }
                        if (index == 1)
                            sb.Append(GetExIMGString(drs[i][_id].ToString(), dt));
                        
                        if (OnClick == "" || _columnData=="")
                        {
                            sb.AppendFormat("{0}</td>", drs[i][j].ToString());
                        }
                        else
                        {
                            bool __tag = true;
                            string[] strColumnData = null;
                            if (_columnData.Trim() != "")
                                strColumnData = _columnData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            for (int k = 0; k < strColumnData.Length; k++)
                            {
                                if (strColumnData[k] == dt.Columns[j].ColumnName)
                                {
                                    if (_rowData == "")
                                    {
                                        sb.AppendFormat("<span class=\"herf_Dark_RC\" value=\"{0}\" onclick=\"JavaScript:{1}\">{2}</span></td>", dt.Columns[j].ColumnName, OnClick, drs[i][j].ToString());
                                    }
                                    else
                                    {
                                        string rowvalue = "";
                                        string[] strRowdata=null;
                                         if (_rowData.Trim() != "")
                                             strRowdata = _rowData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        if (strRowdata != null)
                                        {
                                            for (int m = 0; m < strRowdata.Length; m++)
                                            {
                                                if (m == 0)
                                                    rowvalue = drs[i][strRowdata[m]].ToString();
                                                else
                                                    rowvalue += "," + drs[i][strRowdata[m]].ToString();
                                            }
                                        }
                                        sb.AppendFormat("<span class=\"herf_Dark_RC\" value=\"{0}\" onclick=\"JavaScript:{1}\">{2}</span></td>", dt.Columns[j].ColumnName + ";" + rowvalue, OnClick, drs[i][j].ToString());
                                    }
                                    __tag = false;
                                    break;
                                }
                            }
                            if(__tag)
                                sb.AppendFormat("{0}</td>", drs[i][j].ToString());
                        }
                    }
                }
                sb.AppendFormat("\n</tr></table><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"10\"></td><td id=\"sub_{0}\"></td></tr></table>", drs[i][_id].ToString());

            }
            sb.Append(GetPageString(pageIndex, drs.Length, pid));
            return sb.ToString();
        }

        private string GetPageString(int pageIndex, int count, string pid)
        {
            string strpage = "\n<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>";
            if (pageIndex > 1)
                strpage += "<td align=\"right\"><a href=\"JavaScript:xl_order(" + (pageIndex - 1) + ", '" + pid + "','sub_" + pid + "','" + Order + "','" + Field + "');\">上一页</a></td>";
            else
                strpage += "<td></td>";
            if (pageIndex * _pageSize < count)
                strpage += "<td align=\"right\"><a href=\"JavaScript:xl_order(" + (pageIndex + 1) + ", '" + pid + "','sub_" + pid + "','" + Order + "','" + Field + "');\">下一页</a></td>";
            else
                strpage += "<td></td>";
            strpage += "\n</tr></table>";
            return strpage;
        }

        private string GetExIMGString(string pid, DataTable dt)
        {
            string exImg = "";
            if (dt.Select(_pid + "='" + pid + "'").Length > 0)
            {
                exImg = "<IMG style=\"CURSOR: hand\" onclick=\"xl_mc(1, " + pid + ", this,'sub_" + pid + "','" + Order + "','" + Field + "');\" value='0' src=\"images/plus.gif\" border=\"0\"/>";

            }
            return exImg;
        }

        ///// <summary>
        ///// 注册Javascript
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnPreRender(EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), BuildingScript());
        //    base.OnPreRender(e);
        //}

        private string BuildingScript()
        {
            JavaScriptWriter js = new JavaScriptWriter(true);
            js.AddConnentLine("创建XMLHTTPRequest对象");
            js.AddLine(" var canstar=0;");
            js.AddLine("var xRequest=null;");
            js.AddLine("function getXMLHTTPRequest()");
            js.AddLine("{");
            js.AddLine("if(window.XMLHttpRequest)");
            js.AddLine("{");
            js.AddLine("xRequest=new XMLHttpRequest();");
            js.AddLine("}");
            js.AddLine("else if(typeof ActiveXObject)");
            js.AddLine("{");
            js.AddLine("xRequest=new ActiveXObject(\"Microsoft.XMLHTTP\");");
            js.AddLine("}");
            js.AddLine("return xRequest;");
            js.AddLine("}");

            js.AddConnentLine("发送请求");
            js.AddLine("function sendRequest(callback,imgobj,id,url,params,HttpMethod){");
            js.AddLine("if (!HttpMethod){");
            js.AddLine("HttpMethod=\"POST\";");
            js.AddLine("}");
            js.AddLine("var req=getXMLHTTPRequest();");
            js.AddLine("if (req)");
            js.AddLine("{");
            js.AddLine("req.onreadystatechange= function() ");
            js.AddLine("{");
            js.AddLine("var ready=req.readyState;");
            js.AddLine("var data=null;");
            js.AddLine("if (ready==4)");
            js.AddLine("{");
            js.AddLine("data=req.responseText;");
            js.AddLine("if(imgobj!=''){callback(data,imgobj,id);}else{callback(data,id);}");
            js.AddLine("} else ");
            js.AddLine("{");
            js.AddLine("data=\"loading...[\"+ready+\"]\";");
            js.AddLine("}");
            js.AddLine("};");
            js.AddLine("req.open(HttpMethod,url,true);");
            js.AddLine("req.setRequestHeader(\"Content-Type\",\"application/x-www-form-urlencoded\");");
            js.AddLine("req.send(params);");
            js.AddLine("}");
            js.AddLine("}");

            js.AddConnentLine("获取getElementById对象");
            js.AddLine("function $(idname){");
            js.AddLine("if (document.getElementById){ return document.getElementById(idname); } ");
            js.AddLine("else if (document.all) { return document.all[idname];} ");
            js.AddLine("else if (document.layers) {	return document.layers[idname];	}");
            js.AddLine("else { return null; }");
            js.AddLine("}");

            js.AddLine("function xl_mc(pageindex,pid,imgobj,id,order,field)");
            js.AddLine("{if(imgobj.value==1){$(id).innerHTML='';imgobj.value='0';imgobj.src=\"images/plus.gif\";return;}");
            js.AddLine("var httprequest= sendRequest(repeat_callback,imgobj,id,\"" + _requestPage + ".aspx\",\"columndata=\"+escape('" + ColumnData + "')+\"&tagdata=" + TagData + "&pageindex=\"+pageindex+\"&order=\"+order+\"&field=\"+escape(field)+\"&pid=\"+pid,\"POST\");");
            js.AddLine("");
            js.AddLine("}");
            js.AddLine("");

            js.AddLine("function xl_order(pageindex,pid,id,order,field)");
            js.AddLine("{");
            js.AddLine("var httprequest= sendRequest(page_callback,'',id,\"" + _requestPage + ".aspx\",\"columndata=\"+escape('" + ColumnData + "')+\"&tagdata=" + TagData + "&pageindex=\"+pageindex+\"&order=\"+order+\"&field=\"+escape(field)+\"&pid=\"+pid,\"POST\");");
            js.AddLine("");
            js.AddLine("}");
            js.AddLine("");

            js.AddLine("function page_callback(data,id)");
            js.AddLine("{");
            js.AddLine("");
            js.AddLine("if(data!=null)");
            js.AddLine("{ ");
            js.AddLine("    $(id).innerHTML=data;}");
            js.AddLine("}");

            js.AddLine("function repeat_callback(data,imgobj,id)");
            js.AddLine("{");
            js.AddLine("");
            js.AddLine("if(data!=null)");
            js.AddLine("{ ");
            js.AddLine("    $(id).innerHTML=data;imgobj.src=\"images/minus.gif\";imgobj.value='1';}");
            js.AddLine("}");

            js.AddLine("");
            js.AddLine("");
            return js.ToString();
        }
    }
}
