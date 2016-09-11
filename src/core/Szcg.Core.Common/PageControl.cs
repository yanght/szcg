using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;

namespace Teamax.Common
{
    /// <summary>
    /// WebCustomControl1 的摘要说明。
    /// </summary>
    [ToolboxData("<{0}:PageControl runat=server></{0}:PageControl>")]
    public class PageControl : System.Web.UI.WebControls.WebControl, IPostBackEventHandler
    {
        #region Construct method
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageControl()
            : base(HtmlTextWriterTag.Div)
        {
        }
        #endregion

        #region Variables and Constants

        public event EventHandler ChangePageClick;
        private int _TotalRecord = 0;
        private int _TotalPage = 0;
        private int _PageSize = 10;
        private int _CurrentPageIndex = 1;
        private string _path = "";


        #endregion

        #region Properties
        [
        Description("总记录数"),
        Bindable(false),
        Category("Behavior"),
        DefaultValue(0)
        ]
        public int TotalRecord
        {
            get
            {
                object obj = ViewState["_TotalRecord"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                ViewState["_TotalRecord"] = value;
            }
        }


        [
        Description("每页显示记录数"),
        Bindable(true),
        Category("Behavior"),
        DefaultValue(10)
        ]
        public int PageSize
        {
            get
            {
                object obj = ViewState["_PageSize"];
                return (obj == null) ? 10 : (int)obj;
            }
            set
            {
                ViewState["_PageSize"] = value;
            }
        }

        [
        Description("总页数"),
        Bindable(true),
        Category("Behavior"),
        DefaultValue(0)
        ]
        public int TotalPage
        {
            get
            {
                object obj = ViewState["_TotalPage"];
                return (obj == null) ? 0 : (int)obj;
            }
        }

        [
        Description("当前页值"),
        Bindable(true),
        Category("Behavior"),
        DefaultValue(1)
        ]
        public int CurrentPageIndex
        {
            get
            {
                object obj = ViewState["_CurrentPageIndex"];
                return (obj == null) ? 1 : (int)obj;
            }
            set
            {
                ViewState["_CurrentPageIndex"] = value;
            }
        }


        #endregion

        //定义Div的样式
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute("White-space", "nowrap");
            writer.AddStyleAttribute("Width", Width.ToString());
            writer.AddStyleAttribute("Height", Height.ToString());
            base.AddAttributesToRender(writer);
        }

        protected virtual void OnPageChangeClick(EventArgs e)
        {
            if (ChangePageClick != null)
            {
                ChangePageClick(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            int PageIndex = int.Parse(eventArgument);
            ViewState["_CurrentPageIndex"] = PageIndex;
            OnPageChangeClick(new EventArgs());
        }

        /// <summary>
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            _path = this.ResolveClientUrl("~/images/page_v1/");
            _TotalRecord = TotalRecord;
            _TotalPage = TotalPage;
            _PageSize = PageSize;
            _CurrentPageIndex = CurrentPageIndex;
            int eventArgument = 1;
            this._TotalPage = ((this.TotalRecord / PageSize) * this.PageSize == this.TotalRecord) ? (this.TotalRecord / this.PageSize) : ((this.TotalRecord / this.PageSize) + 1);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" height=\"23\" border=\"0\" cellPadding=\"0\" cellSpacing=\"0\" background=\"" + _path + "pagebg.jpg\">");
            sb.Append("<tr><td width=\"68\"></td><td align=\"center\" width=\"199\"><span id=\"");

            sb.Append(this.UniqueID + "\">第" + _CurrentPageIndex.ToString() + "页（共" + _TotalPage.ToString() + "页," + _TotalRecord.ToString() + "条）每页" + PageSize.ToString() + "条</span></td>");
            sb.Append("<td align=\"center\" width=\"117\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "1") + "\"  src=\"" + _path + "dyy.gif\" border=\"0\" /></td>");

            if (_CurrentPageIndex > 1)
            {
                eventArgument = _CurrentPageIndex - 1;
                sb.Append("<td align=\"center\" width=\"48\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, eventArgument.ToString()) + "\" src=\"" + _path + "syy.gif\" border=\"0\" /></td>");
            }
            else
            {
                sb.Append("<td align=\"center\" width=\"48\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "1") + "\" id=\"" + this.UniqueID + "\" src=\"" + _path + "syy.gif\" border=\"0\" /></td>");
            }
            if (_CurrentPageIndex < _TotalPage)
            {
                eventArgument = _CurrentPageIndex + 1;
                sb.Append("<td align=\"center\" width=\"48\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, eventArgument.ToString()) + "\"  src=\"" + _path + "xyy.gif\" border=\"0\" /></td>");
            }
            else
            {
                sb.Append("<td align=\"center\" width=\"48\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, _TotalPage.ToString()) + "\" src=\"" + _path + "xyy.gif\" border=\"0\" /></td>");
            }
            sb.Append("<td align=\"center\" width=\"71\"><input type=\"image\"onclick=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, _TotalPage.ToString()) + "\"  src=\"" + _path + "zhyy.gif\" border=\"0\" /></td>");
            sb.Append("<td align=\"center\" width=\"117\">转到<select onchange=\"javascript:__doPostBack('" + this.UniqueID + "',this.value)\"  style=\"width:54px;\">");
            for (int i = 0; i < _TotalPage; i++)
            {
                if (i + 1 == _CurrentPageIndex)
                {
                    sb.Append("<option value=\"" + _CurrentPageIndex + "\"  selected=\"selected\">" + _CurrentPageIndex + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + (i + 1) + "\" >" + (i + 1) + "</option>");
                }
            }
            sb.Append("</select>页</td></tr><td align=\"center\" width=\"72\"></td></table>");
            output.Write(sb.ToString());
            base.RenderContents(output);
        }
    }
}