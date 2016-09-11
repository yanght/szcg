
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Collections;
using System.Security.Cryptography;

namespace szcg.com.teamax.util
{

	/// <summary>
	/// PageBase 的摘要说明。
	/// </summary>
	public class PageBase: System.Web.UI.Page
	{

		public PageBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		protected override void OnInit(EventArgs e)
		{
			int nFormOrder=0;
			for(int i = 0; i < this.Page.Controls.Count; i++)
			{
				if(this.Page.Controls[i].GetType().FullName == "System.Web.UI.HtmlControls.HtmlForm")
				{
					nFormOrder = i;
					break;
				}
			}
			HtmlForm Form1 = (HtmlForm)this.Page.Controls[nFormOrder];
            
			this.ClientScript.RegisterStartupScript(this.GetType(),"onclick", "<script>window.onload = function () {document.all.LCMS_inSubmit.style.visibility = 'hidden';document.all.imb_Background.style.visibility = 'hidden';};function LCMS_Export_Ex_Fun(){document.getElementById('hd_LCMS_Export_ex').value = '9';} function LCMS_DoSubmit(form){if(document.getElementById('hd_LCMS_Export_ex').value == '9'){document.getElementById('hd_LCMS_Export_ex').value = ''; return true;} document.all.imb_Background.style.visibility = 'visible';document.all.LCMS_inSubmit.style.visibility = 'visible';return true;}</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script>function DoSubmit(form){document.all.LCMS_inSubmit.style.visibility = 'visible';return true;}</script>");
            if( Form1.Attributes["onsubmit"] != null && Form1.Attributes["onsubmit"].ToString() != string.Empty && Form1.Attributes["onsubmit"].IndexOf("LCMS_DoSubmit") == -1)
			{
				Form1.Attributes["onsubmit"] = Form1.Attributes["onsubmit"].ToString() + ";return LCMS_DoSubmit(this);";
			}
			else
			{
				Form1.Attributes.Add("onsubmit","return LCMS_DoSubmit(this);");
			}
			string sWaiting = "<DIV id=\"LCMS_inSubmit\" style=\"BORDER-RIGHT: #82AAD2; BORDER-TOP: #82AAD2 ; Z-INDEX: 10001; ; LEFT:";
			sWaiting += "expression((document.body.clientWidth - 200)/2); VISIBILITY: visible; BORDER-LEFT: #82AAD2 ; WIDTH: 200px; BORDER-BOTTOM: #82AAD2;";
			sWaiting += "POSITION: absolute; ; TOP: expression((document.body.clientHeight - 100)/2); HEIGHT: 50px; BACKGROUND-COLOR: #ffffff\">";
			sWaiting += "<table width=\"100%\" height=\"100%\" align=\"center\" class=\"tsxxk\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr>";
			sWaiting += "<td align=\"center\" class=\"tsnr\">系统正在处理，请稍候......";
			sWaiting += "<marquee style=\"border:1px solid #82AAD2\" direction=\"right\" width=\"200\" scrollamount=\"3\" scrolldelay=\"10\" bgcolor=\"#ffffff\">";
			sWaiting += "<table cellspacing=\"0\" border=\"0\">";
			sWaiting += "<tr height=8><td bgcolor=#D4DDEC width=8></td>";
			sWaiting += "<td></td><td bgcolor=#C1D1E0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#9AAFD3 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#6F8DC0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#6F8DC0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#6F8DC0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#6F8DC0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#9AAFD3 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#C1D1E0 width=8></td>";
			sWaiting += "<td></td><td bgcolor=#D4DDEC width=8></td>";
			sWaiting += "<td></td></tr></table></marquee></td></tr>";
			sWaiting += "</table></div><input type=hidden id=hd_LCMS_Export_ex>";;
			sWaiting +="<div id=\"imb_Background\"style=\" FILTER:alpha(opacity=100);position:absolute;left:0;top:0;WIDTH:expression(document.body.clientWidth);HEIGHT:expression(document.body.clientHeight);Z-INDEX: 10000; visibility:hidden;\"><TABLE style=\"WIDTH:expression(document.body.clientWidth);HEIGHT:expression(document.body.clientHeight)\" BORDER=0 CELLSPACING=0 CELLPADDING=0><TR><TD align=center><br></td></tr></table></div>";
            this.ClientScript.RegisterStartupScript(this.GetType(),"Div", sWaiting);
            this.ClientScript.RegisterStartupScript(this.GetType(),"DivEnd","</div>");
			base.OnInit (e);
		}

	}
}
