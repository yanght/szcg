/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：页面基类。对System.Web.UI.Page进行简单封装，方便使用。
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-05-21
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Principal;
using System.Web.Security;


namespace Teamax.Common
{
	/// <summary>
	/// 页面类。
	/// 自定义的基础页面，添加对客户端JavaScript脚本处理功能，以便能响应客户端事件
	/// <seealso cref="Teamax.Common.JScript"/>
	/// </summary>
	public class CommonPage:System.Web.UI.Page
	{
		const char CommonSeparator='$';

		/// <summary>
		/// 存储临时文件的目录
		/// </summary>
		public string TMP_PATH="../TmpFiles/";
		/// <summary>
		/// 报表模板存储路径
		/// </summary>
		public string REPORT_PATH="../res/";

        /// <summary>
        /// 数据表格的分页大小
        /// </summary>
        public string PageSize
        {
            get
            {
                try
                {
                    return System.Configuration.ConfigurationManager.AppSettings["PageSize"];
                }
                catch
                {
                    return "15"; //默认15条
                }
            }
        }

		/// <summary>
		/// 构造函数
		/// </summary>
		public CommonPage()
		{
		
		}

		/// <summary>
		/// 子系统ID。
		/// </summary>
		public string SystemID
		{
			get
			{
				try
				{
					string[] MyUserData = this.userdata.Split('$');
					return MyUserData[0];
				}
				catch
				{
					return "";
				}
			}
		}

		/// <summary>
		/// 用户编码
		/// </summary>
        public string UserCode
		{
			get
			{
				try
				{
					string[] MyUserData = this.userdata.Split('$');
					return MyUserData[1];
				}
				catch
				{
					return "";
				}
			}
		}

		/// <summary>
        /// 用户名称 
		/// </summary>
        public string UserName
		{
			get
			{
				try
				{
					string[] MyUserData = this.userdata.Split('$');
					return MyUserData[2];
				}
				catch
				{
					return "";
				}
			}
		}
        
		/// <summary>
		/// 部门编码 
		/// </summary>
        public string DepartmentCode
		{
			get
			{
				try
				{
					string[] MyUserData = this.userdata.Split('$');
					return MyUserData[3];
				}
				catch
				{
					return "";
				}
			}
		}

        /// <summary>
        /// 部门自定义编码 
        /// </summary>
        public string DepartmentDefinedCode
        {
            get
            {
                try
                {
                    string[] MyUserData = this.userdata.Split('$');
                    return MyUserData[7];
                }
                catch
                {
                    return "";
                }
            }
        }

		/// <summary>
		/// 角色编码
		/// </summary>
        public string RoleCode
		{
			get
			{
				try
				{
					string[] MyUserData = this.userdata.Split('$');
					return MyUserData[4];
				}
				catch
				{
					return "";
				}
			}
		}

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode
        {
            get
            {
                try
                {
                    string[] MyUserData = this.userdata.Split('$');
                    return MyUserData[5];
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get
            {
                try
                {
                    string[] MyUserData = this.userdata.Split('$');
                    return MyUserData[6];
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 用户有权限操作的模块列表。模块之间用逗号分割
        /// </summary>
        public string ModelPowers
        {
            get
            {
                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[Session["UserCode"].ToString() + "BUTTON_POWER"];
                    return cookie.Value;
                }
                catch
                {
                    return "";
                }
            }
        }

		/// <summary>
		/// 用户数据
		/// </summary>
		public string userdata
		{
			get
			{
                HttpCookie cookie = HttpContext.Current.Request.Cookies[Session["UserCode"].ToString() + "login_bacg"];			
				if(cookie==null)
					return "";

				FormsAuthenticationTicket _ticket = null;
				try
				{
					_ticket=FormsAuthentication.Decrypt(cookie.Value);
					if(_ticket==null)
						return "";
				}
				catch
				{
					_ticket=null;
					return "";
				}

				return _ticket.UserData;
			}
        }

		#region	CloseWindow：关闭窗体
		/// <summary>
		/// 关闭IE窗体
		/// </summary>
		/// <example>
		/// <code>
		///		private void BUTTON1_ServerClick(object sender, System.EventArgs e)
		///		{
		///			CloseWindow();
		///		}
		///	</code>
		///	</example>
		public void CloseWindow()
		{
            base.ClientScript.RegisterStartupScript(this.GetType(),"CloseWindow", String.Concat("<script>", JScript.WinClose, "</script>"));
		}

		/// <summary>
		/// 重载方法：给控件注册关闭窗体客户端事件
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//为BUTTON1按钮注册客户端事件，当BUTTON1按钮被点击时关闭窗体。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				CloseWindow(BUTTON1);
		///			}
		///	</code>
		///	</example>
		public void CloseWindow(WebControl control)
		{
			control.Attributes.Add("onclick", String.Concat(JScript.ParentWinClose, JScript.ReturnFalse));
		}
        #endregion

		#region	SetTitle：设置窗体标题		
		/// <summary>
		/// 设置窗体标题
		/// </summary>
		/// <param name="pTitle"></param>
		/// <example>
		/// <code>
		///		private void BUTTON1_ServerClick(object sender, System.EventArgs e)
		///		{
		///			SetTitle("我的窗口标题");
		///		}
		///	</code>
		///	</example>
		public void SetTitle(string pTitle)
		{
            base.ClientScript.RegisterStartupScript(this.GetType(), "setTitle", "<script>" + string.Format(JScript.SetWindowTitle, pTitle) + "</script>");
		}
		#endregion

		#region	HiddenControl：隐藏控件
		/// <summary>
		/// 隐藏控件
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//隐藏TextBox1控件
		/// 		private void Button1_Click(object sender, System.EventArgs e)
		///			{
		///				HiddenControl(TextBox1);
		///			}
		///	</code>
		///	</example>
		public void HiddenControl(WebControl control)
		{
			HiddenControl(new WebControl[]{control});
		}

		/// <summary>
		/// 重载方法。隐藏控件集
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//隐藏TextBox1,TextBox2,TextBox3控件
		/// 		private void Button1_Click(object sender, System.EventArgs e)
		///			{
		///				HiddenControl(TextBox1,TextBox2,TextBox3);
		///			}
		///	</code>
		///	</example>		
		public void HiddenControl(params WebControl[] control)
		{
			System.Text.StringBuilder script = new System.Text.StringBuilder();
			script.Append("<script>");
			foreach(WebControl item in control)
			{
				script.Append(string.Format(JScript.HiddenControl,item.ClientID) +";");
			}
			script.Append("</script>");
            base.ClientScript.RegisterStartupScript(this.GetType(), "hiddencontrols", script.ToString());
		}
		#endregion

		#region MessageBox：显示提示框		
		/// <summary>
		/// 显示提示框
		/// </summary>
		/// <param name="Message">显示的信息</param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		/// 		private void Button1_Click(object sender, System.EventArgs e)
		///			{
		///				MessageBox("OK!");
		///			}
		///	</code>
		/// </example>
		public void MessageBox(string Message)
		{
            MessageBox(this, Message);
  		}

        /// <summary>
        /// 显示提示框	
        /// </summary>
        /// <param name="sender">页面类</param>
        /// <param name="Message">提示内容</param>
        public static void MessageBox(System.Web.UI.Page sender, string Message)
        {
            if (sender == null) throw new ArgumentNullException("sender参数为空!");
            Message = Message.Replace("\""," ").Replace("\r\n", "");
            sender.ClientScript.RegisterStartupScript(sender.GetType(), String.Concat("message", Guid.NewGuid().ToString()), String.Concat("<script language='javascript'>", String.Format(JScript.WinAlert, Message), "</script>"));
        }

		/// <summary>
        /// 显示提示框
		/// </summary>
		/// <param name="Message">显示的信息</param>
		/// <param name="strType">对话框类型。话框类型共有四种，分别为：错误、警告、提问、提示</param>
		public void MessageBox(string Message,string strType)
		{
			Message=Message.Replace("\r\n","");
			string script=string.Format(@"
					<script LANGUAGE=""VBScript"">
					Function msg(str,btn)
						if btn=""错误"" then
							MsgBox str,16,""系统提示:""
						elseif btn=""警告"" then
							MsgBox str,48,""系统提示:""
						elseif btn=""提问"" then
							dim MyVar
							MyVar=MsgBox(str,4+256,""系统提示:"")
							if MyVar=6 then
								document.all(""Form1"").submit
							end if
						elseif btn=""提示"" then              		
							MsgBox str,64,""系统提示:""
						else
							MsgBox str,64,""系统提示:""
						end if
					End Function
					msg ""{0}"",""{1}""
					</script>",Message,strType);
            base.ClientScript.RegisterStartupScript(this.GetType(),String.Concat("message", Guid.NewGuid().ToString()), script);
		}

		/// <summary>
		/// 注册控件显示提示框脚本。控件为WebControl类型
		/// </summary>
		/// <param name="Message">显示的信息</param>
		/// <param name="control">注册的控件</param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//为控件注册客户端提示框事件。
		///			//BUTTON1在客户端点击时将提示：确定删除？按“是”将执行BUTTON1服务器事件，按“否”不执行BUTTON1服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				MessageBox("确定删除？",BUTTON1);
		///			}
		///	</code>
		/// </example>		
		public void MessageBox(string Message, WebControl control)
		{
			Message=Message.Replace("\r\n","");
			control.Attributes.Add("onclick", String.Concat(String.Format(JScript.MessageDailog, Message), ";"));
		}

		/// <summary>
		/// 函数重载：注册控件显示提示框脚本。控件为HtmlButton类型
		/// </summary>
		/// <param name="Message">显示的信息</param>
		/// <param name="control">注册的控件</param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//为控件注册客户端提示框事件。
		///			//BUTTON1在客户端点击时将提示：确定删除？按“是”将执行BUTTON1服务器事件，按“否”不执行BUTTON1服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				MessageBox("确定删除？",BUTTON1);
		///			}
		///	</code>
		/// </example>			
		public void MessageBox(string Message, System.Web.UI.HtmlControls.HtmlButton control)
		{
			Message=Message.Replace("\r\n","");
			control.Attributes.Add("onclick", String.Format(JScript.MessageDailog_HtmlButton, Message));
		}
		#endregion

		#region SetFocusControl：控件的聚焦 
		/// <summary>
		/// 控件的聚焦
		/// </summary>
		/// <param name="control">控件</param>
		public void SetFocusControl(System.Web.UI.Control control) 
		{
			string strScript=string.Format(@"
				<script language=javascript>
					var control=document.getElementById('{0}');
					if(control!=null) 
						control.focus();
				</script>",control.ClientID);
			Page.ClientScript.RegisterStartupScript(this.GetType(),"Focus",strScript);
		}
		#endregion

		#region ControlMoveState：设置在控件上移动发生的颜色改变		
		/// <summary>
		/// 设置在控件上移动发生的颜色改变
		/// </summary>
		/// <param name="MoveColor">移动的颜色</param>
		/// <param name="MoveOutColor">移开的颜色</param>
		/// <param name="control">设置的控件</param>
		public void ControlMoveState(string MoveColor, string MoveOutColor, WebControl control)
		{
			control.Attributes.Add("style", "CURSOR: hand");
			control.Attributes.Add("onmouseover", String.Format(JScript.OnMoveStyle, MoveColor));
			control.Attributes.Add("onmouseout", String.Format(JScript.OnMoveOutStyle, MoveOutColor));
		}

		/// <summary>
		/// 设置在控件上移动发生的颜色改变
		/// </summary>
		/// <param name="MoveColor">移动的颜色</param>
		/// <param name="MoveOutColor">移开的颜色</param>
		/// <param name="control">设置的控件</param>
		public void ControlMoveState(string MoveColor, string MoveOutColor, params WebControl[] control)
		{
			WebControl[] webControls = control;
			for (int i = 0; i < (int)webControls.Length; i++)
			{
				WebControl webControl = webControls[i];
				ControlMoveState(MoveColor, MoveOutColor, webControl);
			}
		}
		#endregion

		#region SetRetrunValue：设置窗体的返回值
		/// <summary>
		/// 设置窗体的返回值
		/// </summary>
		/// <param name="valueText">返回值的内容</param>
		/// <example>
		/// <code>
		///		private void BUTTON1_ServerClick(object sender, System.EventArgs e)
		///		{
		///			SetRetrunValue("111");
		///		}
		///	</code>
		///	</example>
		public void SetRetrunValue(string valueText)
		{
            base.ClientScript.RegisterStartupScript(this.GetType(), "returnvalue", String.Concat("<script>", String.Format("window.returnValue=\"{0}\";window.close();", valueText), "</script>"));
		}
		#endregion

		#region OpenDialog：打开一个新窗体对话框（showModalDialog）
		/// <summary>
		/// 注册控件的客户端事件：打开一个新窗体。
		/// </summary>
		/// <param name="button">要注册的控件</param>
		/// <param name="PageName">新窗体页面路径</param>
		/// <param name="width">新窗体宽度</param>
		/// <param name="height">新窗体高度</param>
		/// <param name="resizable"></param>
		/// <param name="textbox">获取返回值的文本框</param>
		/// <param name="IsPost">是否回调该控件的服务器事件</param>
		/// <param name="HaveQuot">页面路径里面是否有引号</param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///示例1：			
		///			//为BUTTON1注册客户端事件。
		///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体宽600高400，新窗体执行完毕后不再处理BUTTON1的服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,false);
		///			}
		///示例2：			
		///			//为BUTTON1注册客户端事件。
		///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体宽600高400，新窗体执行完毕后能继续处理BUTTON1的服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,true);
		///			}
		///
		///示例3：			
		///			//为BUTTON1注册客户端事件。
		///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体宽600高400，新窗体执行的返回值赋给TextBox1，BUTTON1不在处理服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,TextBox1,false);
		///			}
		///示例4：			
		///			//为BUTTON1注册客户端事件。
		///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体宽600高400，新窗体执行的返回值赋给TextBox1，新窗体执行完毕后能继续执行BUTTON1的服务器事件。
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,TextBox1,true);
		///			}
		///示例5：			
		///			//为BUTTON1注册客户端事件。
		///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体宽600高400，新窗体执行的返回值赋给TextBox1，新窗体执行完毕后能继续执行BUTTON1的服务器事件。
		///			//注意：示例5与示例4的区别在于“新窗体页面路径”参数里面是否有引号
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,TextBox1,true,1);
		///			}
		///	</code>
		/// </example>			
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height,bool resizable,Control textbox,bool IsPost,int HaveQuot)
		{
			string str2;
			if(HaveQuot<=0)
			{
				if(textbox==null)
				{
					str2 = JScript.OpenDialog(PageName, width, height,resizable);
				}
				else
				{
					str2 = JScript.OpenDialog(PageName, width, height,resizable,textbox.ClientID);
				}
			}
			else
			{
				str2 = JScript.OpenDialog(PageName,width, height,resizable,textbox.ClientID,HaveQuot);
			}

			if (IsPost)
			{
				if(button is WebControl)
					((WebControl)button).Attributes.Add("onclick", str2);
				else if(button is System.Web.UI.HtmlControls.HtmlButton)
					((System.Web.UI.HtmlControls.HtmlButton)button).Attributes.Add("onclick", str2);
			}
			else
			{
				if(button is WebControl)
					((WebControl)button).Attributes.Add("onclick", String.Concat(str2, ";return false;"));
				else if(button is System.Web.UI.HtmlControls.HtmlButton)
					((System.Web.UI.HtmlControls.HtmlButton)button).Attributes.Add("onclick", String.Concat(str2, ";return false;"));
			}
		}

		/// <summary>
		/// 重载函数：打开一个指定宽度和高度的新窗体。新窗体执行完毕的返回值赋给指定的控件.
		/// </summary>
		/// <param name="button"></param>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="textbox"></param>
		/// <param name="IsPost"></param>
		/// <param name="HaveQuot"></param>
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height,Control textbox,bool IsPost,int HaveQuot)
		{
			OpenDialog(button,PageName,width,height,false,textbox,IsPost,HaveQuot);
		}

		/// <summary>
		/// 重载函数：
		/// 注册控件的客户端事件：打开一个指定宽度和高度的新窗体。新窗体执行完毕无返回值
		/// </summary>
		/// <param name="button">要注册的控件</param>
		/// <param name="PageName">新窗体页面路径</param>
		/// <param name="width">新窗体宽度</param>
		/// <param name="height">新窗体高度</param>
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height)
		{
			OpenDialog(button,PageName,width,height,true,null,false,0);
		}

		/// <summary>
		/// 重载函数：
		/// 注册控件的客户端事件：打开一个指定宽度和高度的新窗体。可以把新窗体执行完毕的返回值，赋给textbox控件。
		/// </summary>
		/// <param name="button">要注册的控件</param>
		/// <param name="PageName">新窗体页面路径</param>
		/// <param name="width">新窗体宽度</param>
		/// <param name="height">新窗体高度</param>
		/// <param name="textbox">获取返回值的文本框</param>
		/// <param name="IsPost">是否回调该控件的服务器事件</param>
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height, Control textbox, bool IsPost)
		{
			OpenDialog(button,PageName,width, height,true,textbox,IsPost,0);
		}

		/// <summary>
		/// 重载函数：
		/// 允许 ASP.NET 服务器控件在 System.Web.UI.Page 中发出客户端脚本块。
		/// 打开一个指定宽度和高度的新窗体。可以把新窗体执行完毕的返回值，赋给textbox控件。 
		/// </summary>
		/// <param name="PageName">新窗体页面路径</param>
		/// <param name="width">新窗体宽度</param>
		/// <param name="height">新窗体高度</param>
		/// <param name="resizable"></param>
		/// <param name="textbox">获取返回值的文本框</param>
		/// <param name="HaveQuot">标识页面路径里面是否有引号</param>
		/// <param name="IsPostBack">是否需要刷新该页面</param>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///示例1：			
		///			//在按钮BUTTON1的服务器单击事件中，发出客户端脚本块。
		///			//单击BUTTON1将将弹出一个指定高度和宽度的新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体执行的返回值赋给TextBox1。
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...其它服务器代码
		///				this.OpenDialog("../test/Test.aspx",600,400,TextBox1);
		///			}
		///示例2：			
		///			//在按钮BUTTON1的服务器单击事件中，发出客户端脚本块。
		///			//单击BUTTON1将将弹出一个指定高度和宽度的新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体执行的返回值赋给TextBox1。
		///			//注意：
		///			//示例2与示例1的区别是：OpenDialog函数的第一个参数值“新窗体页面路径”中有单引号。
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...其它服务器代码
		///				this.OpenDialog("../test/Test.aspx?sql=select name from book where id='001'",600,400,TextBox1,true);
		///			}
		///示例3：			
		///			//在按钮BUTTON1的服务器单击事件中，发出客户端脚本块。
		///			//单击BUTTON1将将弹出一个指定高度和宽度的新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体执行完毕无返回值
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...其它服务器代码
		///				this.OpenDialog("../test/Test.aspx",600,400);
		///			}
		///示例4：			
		///			//在按钮BUTTON1的服务器单击事件中，发出客户端脚本块。
		///			//单击BUTTON1将打开一个默认600宽度和500高度的新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体执行完毕无返回值
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...其它服务器代码
		///				this.OpenDialog("../test/Test.aspx");
		///			}
		///示例5：			
		///			//在按钮BUTTON1的服务器单击事件中，发出客户端脚本块。
		///			//单击BUTTON1将打开一个默认600宽度和500高度的新窗体，新窗体的页面地址是“../test/Test.aspx”，
		///			//新窗体执行的返回值赋给TextBox1。
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...其它服务器代码
		///				this.OpenDialog("../test/Test.aspx",TextBox1);
		///			}
		///	</code>
		public void OpenDialog(string PageName, int width, int height,bool resizable,Control textbox,int HaveQuot,bool IsPostBack)
		{
			string str;
			if(HaveQuot<=0)
			{
				if(textbox==null)
				{
					str = JScript.OpenDialog(PageName, width, height,resizable);
				}
				else
				{
					str = JScript.OpenDialog(PageName, width, height,resizable,textbox.ClientID);
				}
			}
			else
			{
				str = JScript.OpenDialog(PageName,width, height,resizable,textbox.ClientID,HaveQuot);
			}

			if(IsPostBack)
				str +="__doPostBack('','')";
			//base.GetPostBackClientHyperlink(this,"");

            base.ClientScript.RegisterStartupScript(this.GetType(),String.Concat("OpenDialog", Guid.NewGuid().ToString()), String.Concat("<script>", str, "</script>"));
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="textbox"></param>
		/// <param name="HaveQuot"></param>
		/// <param name="IsPostBack"></param>
		public void OpenDialog(string PageName, int width, int height,Control textbox,int HaveQuot,bool IsPostBack)
		{
			OpenDialog(PageName, width, height,true,textbox,HaveQuot,IsPostBack);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="textbox"></param>
		/// <param name="HaveQuot"></param>
		public void OpenDialog(string PageName, int width, int height,Control textbox,int HaveQuot)
		{
			OpenDialog(PageName, width, height,true,textbox,HaveQuot,false);
		}

		/// <summary>
		/// 重载函数：
		/// 允许 ASP.NET 服务器控件在 System.Web.UI.Page 中发出客户端脚本块。
		/// 打开一个指定宽度和高度的新窗体。可以把新窗体执行完毕的返回值，赋给textbox控件。 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="textbox"></param>
		public void OpenDialog(string PageName, int width, int height,Control textbox)
		{
			OpenDialog(PageName, width, height,true,textbox,0,false);
		}


		/// <summary>
		/// 重载函数：
		/// 允许 ASP.NET 服务器控件在 System.Web.UI.Page 中发出客户端脚本块。
		/// 打开一个指定宽度和高度的新窗体。 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="resizable"></param>
		public void OpenDialog(string PageName, int width, int height,bool resizable)
		{
			OpenDialog(PageName, width, height,resizable,null,0,false);
		}		
		
		/// <summary>
		/// 重载函数：
		/// 允许 ASP.NET 服务器控件在 System.Web.UI.Page 中发出客户端脚本块。
		/// 打开一个指定宽度和高度的新窗体。 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void OpenDialog(string PageName, int width, int height)
		{
			OpenDialog(PageName, width, height,true,null,0,false);
		}
		#endregion

		#region OpenWin：打开一个新窗体（window.open）
		/// <summary>
		/// 打开一个新窗体（window.open）
		/// </summary>
		/// <param name="Url">新窗体页面路径</param>
        /// <param name="WinName">窗体名称</param>
		/// <param name="width">新窗体宽度</param>
		/// <param name="height">新窗体高度</param>
        /// <param name="top">新窗体位置坐标位于顶部的尺寸</param>
        /// <param name="left">新窗体位置坐标位于左部的尺寸</param>		
        /// <param name="resizable">是否可以放缩</param>
        /// <param name="scrollbars">是否可以有滚动条</param>
        /// <param name="status">是否可以有状态条</param>
		/// <example>
		/// <code>
		///			//首先WebForm1必须从CommonPage类继承
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///示例1：			
		///			//为BUTTON1注册客户端事件。
        ///			//在客户端点击BUTTON1时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
		/// 		private void Button_OnClick(object sender, System.EventArgs e)
		///			{
		///				this.OpenWin("../test/Test.aspx",600,400);
		///			}
        /// 
        ///示例2：			
        ///			//为BUTTON1注册客户端事件。
        ///			//BUTTON1在客户端点击时将将弹出新窗体，新窗体的页面地址是“../test/Test.aspx”，
        ///			//新窗体宽600高400
        /// 		private void Button_OnClick(object sender, System.EventArgs e)
        ///			{
        ///				this.OpenWin("../test/Test.aspx","MyWin",600,400,200,100,true,true,true);
        ///			}		
        ///	</code>
		/// </example>			
        public void OpenWin(string Url,string WinName, int width, int height,int top,int left,bool resizable,bool scrollbars,bool status)
        {
            string strResizable = resizable ? "yes" : "no";
            string strScrollbars = scrollbars ? "yes" : "no";
            string strStatus = status ? "yes" : "no";

            string strScript = string.Format("<script language=javascript>window.open(\"{0}\",\"{1}\",\"width={2},height={3},top={4},left={5},resizable={6},scrollbars={7},status={8}\")</script>",
                Url,WinName, width, height,top,left,strResizable,strScrollbars,status);
            base.ClientScript.RegisterStartupScript(this.GetType(), String.Concat("OpenDialog", Guid.NewGuid().ToString()), strScript);
        }

        /// <summary>
        /// 打开一个新窗体（window.open）
        /// </summary>
        /// <param name="Url">新窗体页面路径</param>
        /// <param name="width">新窗体宽度</param>
        /// <param name="height">新窗体高度</param>
        public void OpenWin(string Url, int width, int height)
        {
            OpenWin(Url, "", width, height, 100, 100, false, true, false);
        }
        #endregion


        #region PrintView：打印预览数据集
        /// <summary>
		/// WEB打印预览。
		/// </summary>
		/// <param name="ds">DataSet数据集</param>
		/// <param name="txtReportData">HtmlInputHidden隐藏控件。用来存放XML字符串</param>
		/// <param name="ReportTemplateFile">报表模板文件</param>
		/// <param name="ReportName">打印类别名称，比如：报表</param>
		/// <param name="iView">是否预览。1：预览；0：直接打印</param>
		/// <param name="iSetPrint">是否套打。1：套打；0：不套打</param>
		/// <remarks>
		/// WEB打印预览，此方法不会在服务器端生成临时XML文件，而是在页面生成XML字符串，然后赋值给页面上的隐藏控件。
		/// </remarks>
		public void PrintView(DataSet ds,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string ReportTemplateFile,string ReportName,int iView, int iSetPrint)
		{
			//操作员
			string Operator = Page.User.Identity.Name;
			
			//由数据集生成XML字符串，然后传给隐藏对象
			txtReportData.Value=PublicClass.DatasetToXML(ds,"");
		
			string strScript=string.Format(@"
				<script language=javascript>
				document.all.TrainPrintReport.ReportDataStr=document.all('{0}').value;
				document.all.TrainPrintReport.ReportTemplateFile='{1}';
				document.all.TrainPrintReport.Operator='{2}';
				document.all.TrainPrintReport.PrintView('{3}',{4},{5}); 
				</script>",txtReportData.ClientID,ReportTemplateFile,Operator,ReportName,iView,iSetPrint);
			Page.ClientScript.RegisterStartupScript(this.GetType(),"btnPrintView_Click",strScript);		
		}

		/// <summary>
		/// 重载方法。此方法不在服务器端生成临时XML文件，而是生成XML字符串，然后传给WEB页面上的隐藏控件
		/// </summary>
		/// <param name="ds">DataSet数据集</param>
		/// <param name="txtReportData">HtmlInputHidden隐藏控件。用来存放XML字符串</param>
		/// <param name="ReportName">打印类别名称，比如：学员证</param>
		/// <param name="iView">是否预览。1：预览；0：直接打印</param>
		/// <param name="iSetPrint">是否套打。1：套打；0：不套打</param>
		public void PrintView(DataSet ds,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string ReportName,int iView, int iSetPrint)
		{
			string ReportTemplateFile=REPORT_PATH+ReportName+".txt";
			ReportTemplateFile="http://" + Request.ServerVariables["HTTP_HOST"] + Page.ResolveUrl(ReportTemplateFile);
			PrintView(ds,txtReportData,ReportTemplateFile,ReportName,iView,iSetPrint);
		}		
		#endregion

		#region DataGrid通用打印
		/// <summary>
		/// DataGrid通用打印
		/// </summary>
		/// <param name="dg">DataGrid对象</param>
		/// <param name="txtReportData">HtmlInputHidden对象。用来存放XML字符串</param>
		/// <param name="dgTitle">由列名组成的字符串，每个列名之间用逗号分割。如果DataGrid数据列是动态创建的，才需要此参数</param>
		/// <param name="ReportTitle">报表标题</param>
		/// <param name="QueryCondition">报表条件</param>
		/// <param name="iView">是否预览</param>
		/// <example>
		/// <code>
		/// 示例1：打印DataGrid1中的数据，DataGrid1中的数据列都是静态生成的。
		/// //如果页面上没有HtmlInputHidden控件，可以从ActionNavigator上找一个。
		/// HtmlInputHidden txtData=(HtmlInputHidden)ActionNavigator1.FindControl("txtTrainPrintReportData");
		///	PrintViewDataGrid(DataGrid1,txtData,"","邮件发送记录表","",1);
		///	
		/// 示例2：打印DataGrid1中的数据，DataGrid1中的数据列都是静态生成的。
		/// protected System.Web.UI.HtmlControls.HtmlInputHidden txtHidden;
		/// ...
		///	PrintViewDataGrid(DataGrid1,txtHidden,"","邮件发送记录表","",1);
		///	
		/// 示例3：打印DataGrid1中的数据，DataGrid1中有动态态生成的数据列。
		/// HtmlInputHidden txtData=(HtmlInputHidden)ActionNavigator1.FindControl("txtTrainPrintReportData");
		/// string strTitleNames=",流水号,发送时间,发送状态,单位个人,收件人"
		///	PrintViewDataGrid(DataGrid1,txtHidden,"","邮件发送记录表","",1);
		/// </code>
		/// </example>
		/// <remarks>
		/// <code>
		/// 备注：
		/// 1.隐藏列，包括列名为空的列都被忽略。
		/// 2.AutoGenerateColumns为true的时候，无法直接获取列名，所以需要传入dgTitle参数（列名字符串）。
		/// 3.如果dgTitle参数不为空，那么dgTitle一定要跟DataGrid中的列保持一致。
		/// </code>
		/// </remarks>
		public void PrintViewDataGrid(DataGrid dg,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string dgTitle,string ReportTitle,string QueryCondition,int iView)
		{
			//操作员
			string Operator = Page.User.Identity.Name;
			string IsAlignRightColumns;
			//报表数据
			txtReportData.Value=PublicClass.ReturnDataGridXML(dg,dgTitle,out IsAlignRightColumns);
			//报表模板
			string ReportTemplateFile=REPORT_PATH+"报表.txt";
			ReportTemplateFile="http://" + Request.ServerVariables["HTTP_HOST"] + Page.ResolveUrl(ReportTemplateFile);
			
			string strScript=string.Format(@"
				<script language=javascript>
				document.all.TrainPrintReport.ReportDataStr=document.all('{0}').value;
				document.all.TrainPrintReport.ReportTemplateFile='{1}';
				document.all.TrainPrintReport.Operator='{2}';
				document.all.TrainPrintReport.ReportTitle='{3}';
				document.all.TrainPrintReport.QueryCondition='{4}';
				document.all.TrainPrintReport.IsAlignRightColumns='{5}';
				document.all.TrainPrintReport.PrintView('报表',{6},0); 
				</script>",txtReportData.ClientID,ReportTemplateFile,Operator,ReportTitle,QueryCondition,IsAlignRightColumns,iView);
			Page.ClientScript.RegisterStartupScript(this.GetType(),"btnPrintView_Click",strScript);			
		}
		#endregion

        #region ExportToExcel：导出excel
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="FileName">输出文件名</param>
        /// <param name="dt">需要输出的Datatable</param>
        public void ExportToExcel(string FileName, DataTable dt)
        {
            Response.Clear();
            Response.Buffer = false;
            //以下语句导出出现乱码情况(注释：鲁伟兴)  
            if (dt.Rows.Count > 1)
            {
                Response.Charset = "utf-8";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentEncoding = System.Text.Encoding.UTF8;//System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
            }
            else
            {
                Response.Charset = "GB2312";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
            }

            //Response.Charset = "big5";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;//System.Text.Encoding.GetEncoding("GB2312");
            //Response.ContentType = "application/ms-excel";

            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            string newLine = "";
            newLine = "<table cellspacing=\"1\" border=\"1\">";
            oHtmlTextWriter.WriteLine(newLine);
            newLine = "<tr><td colspan=\"" + dt.Columns.Count + "\" align=\"center\">" + dt.TableName + "</td></tr>";
            oHtmlTextWriter.WriteLine(newLine);
            newLine = "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                newLine += "<td>" + dt.Columns[i].Caption + "</td>";
            }
            newLine += "</tr>";
            oHtmlTextWriter.WriteLine(newLine);
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                newLine = "<tr>";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    newLine += "<td>" + dt.Rows[j][i].ToString() + "</td>";
                }
                newLine += "</tr>";
                oHtmlTextWriter.WriteLine(newLine);
            }
            oHtmlTextWriter.WriteLine("</table>");
            Response.Write(oStringWriter.ToString());
            Response.End();
        }


        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="FileName">输出文件名</param>
        /// <param name="ControlID">要输出控件id</param>
        public void ExportToExcel(string FileName,string ControlID)
        {
            Response.Clear();
            Response.Buffer = false;
            Response.Charset = "GB2312";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.FindControl(ControlID).RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        /// <summary>
        /// 利用提供的模板,导出excel
        /// </summary>
        /// <param name="FileName">输出文件名</param>
        /// <param name="TFileName">模板文件名</param>
        /// <param name="ds">DataTable集合：第一个结果集为表头；第二个结果集为表体；第三个结果集为表尾</param>
        public void ExportToExcelWithT(string FileName, string TFileName, DataSet ds)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //得到模板文件的路径
            string strVirPath = System.Configuration.ConfigurationManager.AppSettings["TempletPath"] + TFileName;
            string filePath = Server.MapPath(strVirPath);
            //读取模板文件,存储到arraylist中
            System.IO.StreamReader objReader = new System.IO.StreamReader(filePath);
            string sLine = "";
            System.Collections.ArrayList arrText = new System.Collections.ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    if (sLine.IndexOf("</table>") != -1)
                    {
                        sb.Append(sLine);
                        arrText.Add(sb.ToString());
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sb.Append(sLine);
                    }
                }
            }
            objReader.Close();

            //生成表,用来存储Excel标题
            DataTable tmpDT = new DataTable();
            DataRow dr = tmpDT.NewRow();
            for (int colcnt = 0; colcnt < ds.Tables[1].Columns.Count; colcnt++)
            {
                tmpDT.Columns.Add(new DataColumn());
                dr[colcnt] = ds.Tables[1].Columns[colcnt].Caption;
            }
            tmpDT.Rows.Add(dr);

            ds.Tables.Add(tmpDT);

            //判断提供数据源是否为空
            if (ds.Tables[1].Rows.Count < 1)
            {
                this.MessageBox("请选择数据源，然后导出Excel.");
                return;
            }

            //配置Repons的一些参数,将HTTP 响应数据发送到客户端
            Response.Clear();
            Response.Buffer = false;

            Response.Charset = "utf-8";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";

            //逐行遍历配置文件，拼接字符串
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            string newLine = "";
            string newRow = "";
            oHtmlTextWriter.WriteLine(newLine);


            for (int i = 0; i < arrText.Count; i++)
            {
                int flag = -1; //标示是否是循环的table:-1:不循环
                flag = arrText[i].ToString().IndexOf("circle");

                int idx, x, tmp = 0;
                string y, str1 = "", str2 = "", str3 = "";
                newLine = arrText[i].ToString();
                idx = newLine.LastIndexOf("parm");
                if (idx == -1) //没有参数需要替换
                {
                    oHtmlTextWriter.WriteLine(newLine);
                    continue;
                }

                x = Convert.ToInt32(newLine.Substring(idx + 4, 1));    //表示应该绑定那个结果集
                y = Convert.ToString(newLine.Substring(idx + 5, 1));    //表示结果集的第几个参数

                if (flag != -1)
                {
                    str1 = newLine.Substring(0, newLine.IndexOf("<tr>") + 4);
                    str2 = newLine.Substring(newLine.IndexOf("<tr>") + 4, newLine.LastIndexOf("</tr>") - str1.Length);
                    str3 = newLine.Substring(newLine.LastIndexOf("</tr>"), newLine.Length - newLine.LastIndexOf("</tr>"));

                    for (int z = 0; z < ds.Tables[x].Rows.Count; z++)
                    {
                        if (z > 0)
                            newRow = newRow + "<tr>";

                        for (int j = 0; j < ds.Tables[x].Columns.Count; j++)
                        {
                            if (y == "x")
                            {
                                newRow = newRow + str2.Replace("parm" + x + y, ds.Tables[x].Rows[z][j].ToString());
                            }
                            else
                            {
                                newRow = str2.Replace("parm" + x + tmp, ds.Tables[x].Rows[z][j].ToString());
                                str2 = str2.Replace("parm" + x + tmp, ds.Tables[x].Rows[z][j].ToString());
                                tmp++;
                            }
                        }

                        if (z < ds.Tables[x].Rows.Count - 1)
                            newRow = newRow + "</tr>";
                    }
                    newRow = str1 + newRow + str3;
                }
                else
                {
                    if (y == "x")
                    {
                        str1 = newLine.Substring(0, newLine.IndexOf("<tr>") + 4);
                        str2 = newLine.Substring(newLine.IndexOf("<tr>") + 4, newLine.LastIndexOf("</tr>") - str1.Length);
                        str3 = newLine.Substring(newLine.LastIndexOf("</tr>"), newLine.Length - newLine.LastIndexOf("</tr>"));
                    }

                    for (int m = 0; m < ds.Tables[x].Rows.Count; m++)
                    {
                        for (int n = 0; n < ds.Tables[x].Columns.Count; n++)
                        {
                            if (y == "x")
                            {
                                newRow = newRow + str2.Replace("parm" + x + y, ds.Tables[x].Rows[m][n].ToString());
                            }
                            else
                            {
                                newRow = newLine = newLine.Replace("parm" + x + tmp, ds.Tables[x].Rows[m][n].ToString());
                                tmp++;
                            }
                        }
                    }
                    if (y == "x")
                        newRow = str1 + newRow + str3;
                }

                oHtmlTextWriter.WriteLine(newRow);
                newLine = "";
                newRow = "";
            }


            Response.Write(oStringWriter.ToString());
            Response.End();
        }


        /// <summary>
        /// 数据报送导出excel
        /// </summary>
        /// <param name="FileName">输出文件名</param>
        /// <param name="dt">需要输出的Datatable</param>
        public void ExportToSJBSExcel(string FileName, DataTable dt)
        {
            Response.Clear();
            Response.Buffer = false;
            //以下语句导出出现乱码情况(注释：鲁伟兴)  
            if (dt.Rows.Count > 1)
            {
                Response.Charset = "utf-8";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentEncoding = System.Text.Encoding.UTF8;//System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
            }
            else
            {
                Response.Charset = "GB2312";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
            }

            //Response.Charset = "big5";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;//System.Text.Encoding.GetEncoding("GB2312");
            //Response.ContentType = "application/ms-excel";

            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            string newLine = "";
            newLine = "<table cellspacing=\"1\" border=\"1\">";
            oHtmlTextWriter.WriteLine(newLine);
            //newLine = "<tr><td colspan=\"" + dt.Columns.Count + "\" align=\"center\">" + dt.TableName + "</td></tr>";
            //oHtmlTextWriter.WriteLine(newLine);
            newLine = "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                newLine += "<td>" + dt.Columns[i].Caption + "</td>";
            }
            newLine += "</tr>";
            oHtmlTextWriter.WriteLine(newLine);
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                newLine = "<tr>";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        newLine += "<td  style =\"vnd.ms-excel.numberformat:@\">" + dt.Rows[j][i].ToString() + "</td>";
                    }
                    else
                    {
                        newLine += "<td>" + dt.Rows[j][i].ToString() + "</td>";
                    }
                }
                newLine += "</tr>";
                oHtmlTextWriter.WriteLine(newLine);
            }
            oHtmlTextWriter.WriteLine("</table>");
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        //public override void VerifyRenderingInServerForm(Control control)
        //{ }
        #endregion

        #region SetButtonPower：设置页面上面的按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="ArrayPowers">权限列表</param>
        /// <param name="btn">按钮</param>
        /// <returns></returns>
        private bool GetButtonPower(string[] ArrayPowers, System.Web.UI.Control btn)
        {
            bool bResult = false;

            string strBtnCode = "";
            if (btn is System.Web.UI.HtmlControls.HtmlControl)
                strBtnCode = (btn as System.Web.UI.HtmlControls.HtmlControl).Attributes["value"];
            else if (btn is System.Web.UI.WebControls.WebControl)
                strBtnCode = (btn as System.Web.UI.WebControls.WebControl).Attributes["value"];

            foreach (string sysid in ArrayPowers)
            {
                if (strBtnCode == sysid) //value里面填写
                {
                    bResult = true; 
                    break;
                }
            }

            return bResult;
        }


        /// <summary>
        /// 设置页面按钮权限
        /// </summary>
        /// <param name="btns">按钮数组。可以是HtmlButton、HtmlImage对象</param>
        /// <returns></returns>
        public string SetButtonPower(params System.Web.UI.Control[] btns)
        {
            return SetButtonPower(true, btns);
        }

        /// <summary>
        /// 设置页面按钮权限
        /// </summary>
        /// <param name="IsHidden">当没有权限时是否隐藏该对象。</param>
        /// <param name="btns">按钮数组。可以是HtmlButton、HtmlImage对象</param>
        /// <returns></returns>
        public string SetButtonPower(bool IsHidden, params System.Web.UI.Control[] btns)
        {
            string[] ArrayPowers = this.ModelPowers.Split(',');
            string strBtnID = "";
            foreach (System.Web.UI.Control btn in btns)
            {
                bool bActive = GetButtonPower(ArrayPowers, btn);

                if (btn is System.Web.UI.HtmlControls.HtmlControl)
                    (btn as System.Web.UI.HtmlControls.HtmlControl).Disabled = !bActive;
                else if (btn is System.Web.UI.WebControls.WebControl)
                    (btn as System.Web.UI.WebControls.WebControl).Enabled = bActive;

                if (!bActive && IsHidden) //没有权限
                    btn.Visible = false;

                if (bActive == true) //该按钮有权限使用
                {
                    if (strBtnID == "")  //返回第一个有权限使用的功能按钮，以便加载页面时自动执行
                        strBtnID = btn.ID;

                    //最末尾为“c.jpg”为灰色图片，应该变成活动图片
                    if (btn is System.Web.UI.HtmlControls.HtmlButton)
                    {
                        if( (btn as System.Web.UI.HtmlControls.HtmlButton).InnerHtml.IndexOf(".jpg",StringComparison.OrdinalIgnoreCase)!=-1)
                            (btn as System.Web.UI.HtmlControls.HtmlButton).InnerHtml = (btn as System.Web.UI.HtmlControls.HtmlButton).InnerHtml.Replace("c.jpg", ".jpg"); 
                        else
                            (btn as System.Web.UI.HtmlControls.HtmlButton).InnerHtml = (btn as System.Web.UI.HtmlControls.HtmlButton).InnerHtml.Replace("c.gif", ".gif"); 
                    }
                    else if (btn is System.Web.UI.HtmlControls.HtmlImage)
                    {
                        if ((btn as System.Web.UI.HtmlControls.HtmlImage).Src.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) != -1)
                            (btn as System.Web.UI.HtmlControls.HtmlImage).Src = (btn as System.Web.UI.HtmlControls.HtmlImage).Src.Replace("c.jpg", ".jpg");
                        else
                            (btn as System.Web.UI.HtmlControls.HtmlImage).Src = (btn as System.Web.UI.HtmlControls.HtmlImage).Src.Replace("c.gif", ".gif");
                    }
                    else if (btn is System.Web.UI.WebControls.ImageButton || btn is System.Web.UI.WebControls.Image)
                    {
                        if ((btn as System.Web.UI.WebControls.Image).ImageUrl.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) != -1)
                            (btn as System.Web.UI.WebControls.Image).ImageUrl = (btn as System.Web.UI.WebControls.Image).ImageUrl.Replace("c.jpg", ".jpg");
                        else
                            (btn as System.Web.UI.WebControls.Image).ImageUrl = (btn as System.Web.UI.WebControls.Image).ImageUrl.Replace("c.gif", ".gif");
                    }
                }
            }

            return strBtnID;
        }
        #endregion

        #region OnInit：覆盖页面的初始化方法。
        /// <summary>
        /// 覆盖页面的初始化方法。加载页面时，生成进度条
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (Session["UserCode"] == null)
                Response.Redirect("/");
            int nFormOrder = -1;
            for (int i = 0; i < this.Page.Controls.Count; i++)
            {
                if (this.Page.Controls[i].GetType().FullName == "System.Web.UI.HtmlControls.HtmlForm")
                {
                    nFormOrder = i;
                    break;
                }
            }

            if (nFormOrder == -1)
            {
                base.OnInit(e);
                return;
            }

            System.Web.UI.HtmlControls.HtmlForm Form1 = (System.Web.UI.HtmlControls.HtmlForm)this.Page.Controls[nFormOrder];

            this.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script>window.onload = function () {document.all.LCMS_inSubmit.style.visibility = 'hidden';document.all.imb_Background.style.visibility = 'hidden';};function LCMS_Export_Ex_Fun(){document.getElementById('hd_LCMS_Export_ex').value = '9';} function LCMS_DoSubmit(form){if(document.getElementById('hd_LCMS_Export_ex').value == '9'){document.getElementById('hd_LCMS_Export_ex').value = ''; return true;} document.all.imb_Background.style.visibility = 'visible';document.all.LCMS_inSubmit.style.visibility = 'visible';return true;}</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script>function DoSubmit(form){document.all.LCMS_inSubmit.style.visibility = 'visible';return true;}</script>");
            if (Form1.Attributes["onsubmit"] != null && Form1.Attributes["onsubmit"].ToString() != string.Empty && Form1.Attributes["onsubmit"].IndexOf("LCMS_DoSubmit") == -1)
            {
                Form1.Attributes["onsubmit"] = Form1.Attributes["onsubmit"].ToString() + ";return LCMS_DoSubmit(this);";
            }
            else
            {
                Form1.Attributes.Add("onsubmit", "return LCMS_DoSubmit(this);");
            }
            string sWaiting = "<DIV id=\"LCMS_inSubmit\" style=\"BORDER-RIGHT: #82AAD2; BORDER-TOP: #82AAD2 ; Z-INDEX: 10001; ; LEFT:";
            sWaiting += "expression((document.body.clientWidth - 200)/2); VISIBILITY: visible; BORDER-LEFT: #82AAD2 ; WIDTH: 200px; BORDER-BOTTOM: #82AAD2;";
            sWaiting += "POSITION: absolute; ; TOP: expression((document.body.clientHeight - 100)/2); HEIGHT: 50px; BACKGROUND-COLOR: #ffffff \">";
            sWaiting += "<table width=\"100%\" height=\"100%\" align=\"center\" class=\"tsxxk\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr>";
            sWaiting += "<td align=\"center\" class=\"tsnr\">系统正在处理，请稍候......";
            sWaiting += "<marquee style=\"border:1px solid #82AAD2\" direction=\"right\" width=\"200\" scrollamount=\"3\" scrolldelay=\"10\" bgcolor=\"#ffffff\" >";
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
            sWaiting += "</table></div><input type=hidden id=hd_LCMS_Export_ex>"; ;
            sWaiting += "<div id=\"imb_Background\"style=\" FILTER:alpha(opacity=100);position:absolute;left:0;top:0;WIDTH:expression(document.body.clientWidth);HEIGHT:expression(document.body.clientHeight);Z-INDEX: 10000; visibility:hidden;\"><TABLE style=\"WIDTH:expression(document.body.clientWidth);HEIGHT:expression(document.body.clientHeight)\" BORDER=0 CELLSPACING=0 CELLPADDING=0><TR><TD align=center><br></td></tr></table></div>";
            //this.ClientScript.RegisterStartupScript(this.GetType(), "Div", sWaiting);
            //this.ClientScript.RegisterStartupScript(this.GetType(), "DivEnd", "</div>");
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Div", sWaiting);
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "DivEnd", "</div>");
            base.OnInit(e);
        }
        #endregion
    }
}
