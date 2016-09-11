/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;��ҳ����ࡣ��System.Web.UI.Page���м򵥷�װ������ʹ�á�
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
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
	/// ҳ���ࡣ
	/// �Զ���Ļ���ҳ�棬��ӶԿͻ���JavaScript�ű������ܣ��Ա�����Ӧ�ͻ����¼�
	/// <seealso cref="Teamax.Common.JScript"/>
	/// </summary>
	public class CommonPage:System.Web.UI.Page
	{
		const char CommonSeparator='$';

		/// <summary>
		/// �洢��ʱ�ļ���Ŀ¼
		/// </summary>
		public string TMP_PATH="../TmpFiles/";
		/// <summary>
		/// ����ģ��洢·��
		/// </summary>
		public string REPORT_PATH="../res/";

        /// <summary>
        /// ���ݱ��ķ�ҳ��С
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
                    return "15"; //Ĭ��15��
                }
            }
        }

		/// <summary>
		/// ���캯��
		/// </summary>
		public CommonPage()
		{
		
		}

		/// <summary>
		/// ��ϵͳID��
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
		/// �û�����
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
        /// �û����� 
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
		/// ���ű��� 
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
        /// �����Զ������ 
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
		/// ��ɫ����
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
        /// �������
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
        /// ��������
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
        /// �û���Ȩ�޲�����ģ���б�ģ��֮���ö��ŷָ�
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
		/// �û�����
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

		#region	CloseWindow���رմ���
		/// <summary>
		/// �ر�IE����
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
		/// ���ط��������ؼ�ע��رմ���ͻ����¼�
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//ΪBUTTON1��ťע��ͻ����¼�����BUTTON1��ť�����ʱ�رմ��塣
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

		#region	SetTitle�����ô������		
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="pTitle"></param>
		/// <example>
		/// <code>
		///		private void BUTTON1_ServerClick(object sender, System.EventArgs e)
		///		{
		///			SetTitle("�ҵĴ��ڱ���");
		///		}
		///	</code>
		///	</example>
		public void SetTitle(string pTitle)
		{
            base.ClientScript.RegisterStartupScript(this.GetType(), "setTitle", "<script>" + string.Format(JScript.SetWindowTitle, pTitle) + "</script>");
		}
		#endregion

		#region	HiddenControl�����ؿؼ�
		/// <summary>
		/// ���ؿؼ�
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//����TextBox1�ؼ�
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
		/// ���ط��������ؿؼ���
		/// </summary>
		/// <param name="control"></param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//����TextBox1,TextBox2,TextBox3�ؼ�
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

		#region MessageBox����ʾ��ʾ��		
		/// <summary>
		/// ��ʾ��ʾ��
		/// </summary>
		/// <param name="Message">��ʾ����Ϣ</param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
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
        /// ��ʾ��ʾ��	
        /// </summary>
        /// <param name="sender">ҳ����</param>
        /// <param name="Message">��ʾ����</param>
        public static void MessageBox(System.Web.UI.Page sender, string Message)
        {
            if (sender == null) throw new ArgumentNullException("sender����Ϊ��!");
            Message = Message.Replace("\""," ").Replace("\r\n", "");
            sender.ClientScript.RegisterStartupScript(sender.GetType(), String.Concat("message", Guid.NewGuid().ToString()), String.Concat("<script language='javascript'>", String.Format(JScript.WinAlert, Message), "</script>"));
        }

		/// <summary>
        /// ��ʾ��ʾ��
		/// </summary>
		/// <param name="Message">��ʾ����Ϣ</param>
		/// <param name="strType">�Ի������͡��������͹������֣��ֱ�Ϊ�����󡢾��桢���ʡ���ʾ</param>
		public void MessageBox(string Message,string strType)
		{
			Message=Message.Replace("\r\n","");
			string script=string.Format(@"
					<script LANGUAGE=""VBScript"">
					Function msg(str,btn)
						if btn=""����"" then
							MsgBox str,16,""ϵͳ��ʾ:""
						elseif btn=""����"" then
							MsgBox str,48,""ϵͳ��ʾ:""
						elseif btn=""����"" then
							dim MyVar
							MyVar=MsgBox(str,4+256,""ϵͳ��ʾ:"")
							if MyVar=6 then
								document.all(""Form1"").submit
							end if
						elseif btn=""��ʾ"" then              		
							MsgBox str,64,""ϵͳ��ʾ:""
						else
							MsgBox str,64,""ϵͳ��ʾ:""
						end if
					End Function
					msg ""{0}"",""{1}""
					</script>",Message,strType);
            base.ClientScript.RegisterStartupScript(this.GetType(),String.Concat("message", Guid.NewGuid().ToString()), script);
		}

		/// <summary>
		/// ע��ؼ���ʾ��ʾ��ű����ؼ�ΪWebControl����
		/// </summary>
		/// <param name="Message">��ʾ����Ϣ</param>
		/// <param name="control">ע��Ŀؼ�</param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//Ϊ�ؼ�ע��ͻ�����ʾ���¼���
		///			//BUTTON1�ڿͻ��˵��ʱ����ʾ��ȷ��ɾ���������ǡ���ִ��BUTTON1�������¼��������񡱲�ִ��BUTTON1�������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				MessageBox("ȷ��ɾ����",BUTTON1);
		///			}
		///	</code>
		/// </example>		
		public void MessageBox(string Message, WebControl control)
		{
			Message=Message.Replace("\r\n","");
			control.Attributes.Add("onclick", String.Concat(String.Format(JScript.MessageDailog, Message), ";"));
		}

		/// <summary>
		/// �������أ�ע��ؼ���ʾ��ʾ��ű����ؼ�ΪHtmlButton����
		/// </summary>
		/// <param name="Message">��ʾ����Ϣ</param>
		/// <param name="control">ע��Ŀؼ�</param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///			
		///			//Ϊ�ؼ�ע��ͻ�����ʾ���¼���
		///			//BUTTON1�ڿͻ��˵��ʱ����ʾ��ȷ��ɾ���������ǡ���ִ��BUTTON1�������¼��������񡱲�ִ��BUTTON1�������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				MessageBox("ȷ��ɾ����",BUTTON1);
		///			}
		///	</code>
		/// </example>			
		public void MessageBox(string Message, System.Web.UI.HtmlControls.HtmlButton control)
		{
			Message=Message.Replace("\r\n","");
			control.Attributes.Add("onclick", String.Format(JScript.MessageDailog_HtmlButton, Message));
		}
		#endregion

		#region SetFocusControl���ؼ��ľ۽� 
		/// <summary>
		/// �ؼ��ľ۽�
		/// </summary>
		/// <param name="control">�ؼ�</param>
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

		#region ControlMoveState�������ڿؼ����ƶ���������ɫ�ı�		
		/// <summary>
		/// �����ڿؼ����ƶ���������ɫ�ı�
		/// </summary>
		/// <param name="MoveColor">�ƶ�����ɫ</param>
		/// <param name="MoveOutColor">�ƿ�����ɫ</param>
		/// <param name="control">���õĿؼ�</param>
		public void ControlMoveState(string MoveColor, string MoveOutColor, WebControl control)
		{
			control.Attributes.Add("style", "CURSOR: hand");
			control.Attributes.Add("onmouseover", String.Format(JScript.OnMoveStyle, MoveColor));
			control.Attributes.Add("onmouseout", String.Format(JScript.OnMoveOutStyle, MoveOutColor));
		}

		/// <summary>
		/// �����ڿؼ����ƶ���������ɫ�ı�
		/// </summary>
		/// <param name="MoveColor">�ƶ�����ɫ</param>
		/// <param name="MoveOutColor">�ƿ�����ɫ</param>
		/// <param name="control">���õĿؼ�</param>
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

		#region SetRetrunValue�����ô���ķ���ֵ
		/// <summary>
		/// ���ô���ķ���ֵ
		/// </summary>
		/// <param name="valueText">����ֵ������</param>
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

		#region OpenDialog����һ���´���Ի���showModalDialog��
		/// <summary>
		/// ע��ؼ��Ŀͻ����¼�����һ���´��塣
		/// </summary>
		/// <param name="button">Ҫע��Ŀؼ�</param>
		/// <param name="PageName">�´���ҳ��·��</param>
		/// <param name="width">�´�����</param>
		/// <param name="height">�´���߶�</param>
		/// <param name="resizable"></param>
		/// <param name="textbox">��ȡ����ֵ���ı���</param>
		/// <param name="IsPost">�Ƿ�ص��ÿؼ��ķ������¼�</param>
		/// <param name="HaveQuot">ҳ��·�������Ƿ�������</param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///ʾ��1��			
		///			//ΪBUTTON1ע��ͻ����¼���
		///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´����600��400���´���ִ����Ϻ��ٴ���BUTTON1�ķ������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,false);
		///			}
		///ʾ��2��			
		///			//ΪBUTTON1ע��ͻ����¼���
		///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´����600��400���´���ִ����Ϻ��ܼ�������BUTTON1�ķ������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,true);
		///			}
		///
		///ʾ��3��			
		///			//ΪBUTTON1ע��ͻ����¼���
		///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´����600��400���´���ִ�еķ���ֵ����TextBox1��BUTTON1���ڴ���������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,TextBox1,false);
		///			}
		///ʾ��4��			
		///			//ΪBUTTON1ע��ͻ����¼���
		///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´����600��400���´���ִ�еķ���ֵ����TextBox1���´���ִ����Ϻ��ܼ���ִ��BUTTON1�ķ������¼���
		/// 		private void Page_Load(object sender, System.EventArgs e)
		///			{
		///				this.OpenDialog(Button1,"../test/Test.aspx",600,400,TextBox1,true);
		///			}
		///ʾ��5��			
		///			//ΪBUTTON1ע��ͻ����¼���
		///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´����600��400���´���ִ�еķ���ֵ����TextBox1���´���ִ����Ϻ��ܼ���ִ��BUTTON1�ķ������¼���
		///			//ע�⣺ʾ��5��ʾ��4���������ڡ��´���ҳ��·�������������Ƿ�������
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
		/// ���غ�������һ��ָ����Ⱥ͸߶ȵ��´��塣�´���ִ����ϵķ���ֵ����ָ���Ŀؼ�.
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
		/// ���غ�����
		/// ע��ؼ��Ŀͻ����¼�����һ��ָ����Ⱥ͸߶ȵ��´��塣�´���ִ������޷���ֵ
		/// </summary>
		/// <param name="button">Ҫע��Ŀؼ�</param>
		/// <param name="PageName">�´���ҳ��·��</param>
		/// <param name="width">�´�����</param>
		/// <param name="height">�´���߶�</param>
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height)
		{
			OpenDialog(button,PageName,width,height,true,null,false,0);
		}

		/// <summary>
		/// ���غ�����
		/// ע��ؼ��Ŀͻ����¼�����һ��ָ����Ⱥ͸߶ȵ��´��塣���԰��´���ִ����ϵķ���ֵ������textbox�ؼ���
		/// </summary>
		/// <param name="button">Ҫע��Ŀؼ�</param>
		/// <param name="PageName">�´���ҳ��·��</param>
		/// <param name="width">�´�����</param>
		/// <param name="height">�´���߶�</param>
		/// <param name="textbox">��ȡ����ֵ���ı���</param>
		/// <param name="IsPost">�Ƿ�ص��ÿؼ��ķ������¼�</param>
		public void OpenDialog(System.Web.UI.Control button, string PageName, int width, int height, Control textbox, bool IsPost)
		{
			OpenDialog(button,PageName,width, height,true,textbox,IsPost,0);
		}

		/// <summary>
		/// ���غ�����
		/// ���� ASP.NET �������ؼ��� System.Web.UI.Page �з����ͻ��˽ű��顣
		/// ��һ��ָ����Ⱥ͸߶ȵ��´��塣���԰��´���ִ����ϵķ���ֵ������textbox�ؼ��� 
		/// </summary>
		/// <param name="PageName">�´���ҳ��·��</param>
		/// <param name="width">�´�����</param>
		/// <param name="height">�´���߶�</param>
		/// <param name="resizable"></param>
		/// <param name="textbox">��ȡ����ֵ���ı���</param>
		/// <param name="HaveQuot">��ʶҳ��·�������Ƿ�������</param>
		/// <param name="IsPostBack">�Ƿ���Ҫˢ�¸�ҳ��</param>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///ʾ��1��			
		///			//�ڰ�ťBUTTON1�ķ����������¼��У������ͻ��˽ű��顣
		///			//����BUTTON1��������һ��ָ���߶ȺͿ�ȵ��´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´���ִ�еķ���ֵ����TextBox1��
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...��������������
		///				this.OpenDialog("../test/Test.aspx",600,400,TextBox1);
		///			}
		///ʾ��2��			
		///			//�ڰ�ťBUTTON1�ķ����������¼��У������ͻ��˽ű��顣
		///			//����BUTTON1��������һ��ָ���߶ȺͿ�ȵ��´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´���ִ�еķ���ֵ����TextBox1��
		///			//ע�⣺
		///			//ʾ��2��ʾ��1�������ǣ�OpenDialog�����ĵ�һ������ֵ���´���ҳ��·�������е����š�
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...��������������
		///				this.OpenDialog("../test/Test.aspx?sql=select name from book where id='001'",600,400,TextBox1,true);
		///			}
		///ʾ��3��			
		///			//�ڰ�ťBUTTON1�ķ����������¼��У������ͻ��˽ű��顣
		///			//����BUTTON1��������һ��ָ���߶ȺͿ�ȵ��´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´���ִ������޷���ֵ
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...��������������
		///				this.OpenDialog("../test/Test.aspx",600,400);
		///			}
		///ʾ��4��			
		///			//�ڰ�ťBUTTON1�ķ����������¼��У������ͻ��˽ű��顣
		///			//����BUTTON1����һ��Ĭ��600��Ⱥ�500�߶ȵ��´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´���ִ������޷���ֵ
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...��������������
		///				this.OpenDialog("../test/Test.aspx");
		///			}
		///ʾ��5��			
		///			//�ڰ�ťBUTTON1�ķ����������¼��У������ͻ��˽ű��顣
		///			//����BUTTON1����һ��Ĭ��600��Ⱥ�500�߶ȵ��´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		///			//�´���ִ�еķ���ֵ����TextBox1��
		/// 		private void BUTTON1_Click(object sender, System.EventArgs e)
		///			{
		///			    //...��������������
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
		/// ���غ�����
		/// ���� ASP.NET �������ؼ��� System.Web.UI.Page �з����ͻ��˽ű��顣
		/// ��һ��ָ����Ⱥ͸߶ȵ��´��塣���԰��´���ִ����ϵķ���ֵ������textbox�ؼ��� 
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
		/// ���غ�����
		/// ���� ASP.NET �������ؼ��� System.Web.UI.Page �з����ͻ��˽ű��顣
		/// ��һ��ָ����Ⱥ͸߶ȵ��´��塣 
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
		/// ���غ�����
		/// ���� ASP.NET �������ؼ��� System.Web.UI.Page �з����ͻ��˽ű��顣
		/// ��һ��ָ����Ⱥ͸߶ȵ��´��塣 
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void OpenDialog(string PageName, int width, int height)
		{
			OpenDialog(PageName, width, height,true,null,0,false);
		}
		#endregion

		#region OpenWin����һ���´��壨window.open��
		/// <summary>
		/// ��һ���´��壨window.open��
		/// </summary>
		/// <param name="Url">�´���ҳ��·��</param>
        /// <param name="WinName">��������</param>
		/// <param name="width">�´�����</param>
		/// <param name="height">�´���߶�</param>
        /// <param name="top">�´���λ������λ�ڶ����ĳߴ�</param>
        /// <param name="left">�´���λ������λ���󲿵ĳߴ�</param>		
        /// <param name="resizable">�Ƿ���Է���</param>
        /// <param name="scrollbars">�Ƿ�����й�����</param>
        /// <param name="status">�Ƿ������״̬��</param>
		/// <example>
		/// <code>
		///			//����WebForm1�����CommonPage��̳�
		///			public class WebForm1 : Teamax.Common.CommonPage
		///
		///ʾ��1��			
		///			//ΪBUTTON1ע��ͻ����¼���
        ///			//�ڿͻ��˵��BUTTON1ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
		/// 		private void Button_OnClick(object sender, System.EventArgs e)
		///			{
		///				this.OpenWin("../test/Test.aspx",600,400);
		///			}
        /// 
        ///ʾ��2��			
        ///			//ΪBUTTON1ע��ͻ����¼���
        ///			//BUTTON1�ڿͻ��˵��ʱ���������´��壬�´����ҳ���ַ�ǡ�../test/Test.aspx����
        ///			//�´����600��400
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
        /// ��һ���´��壨window.open��
        /// </summary>
        /// <param name="Url">�´���ҳ��·��</param>
        /// <param name="width">�´�����</param>
        /// <param name="height">�´���߶�</param>
        public void OpenWin(string Url, int width, int height)
        {
            OpenWin(Url, "", width, height, 100, 100, false, true, false);
        }
        #endregion


        #region PrintView����ӡԤ�����ݼ�
        /// <summary>
		/// WEB��ӡԤ����
		/// </summary>
		/// <param name="ds">DataSet���ݼ�</param>
		/// <param name="txtReportData">HtmlInputHidden���ؿؼ����������XML�ַ���</param>
		/// <param name="ReportTemplateFile">����ģ���ļ�</param>
		/// <param name="ReportName">��ӡ������ƣ����磺����</param>
		/// <param name="iView">�Ƿ�Ԥ����1��Ԥ����0��ֱ�Ӵ�ӡ</param>
		/// <param name="iSetPrint">�Ƿ��״�1���״�0�����״�</param>
		/// <remarks>
		/// WEB��ӡԤ�����˷��������ڷ�������������ʱXML�ļ���������ҳ������XML�ַ�����Ȼ��ֵ��ҳ���ϵ����ؿؼ���
		/// </remarks>
		public void PrintView(DataSet ds,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string ReportTemplateFile,string ReportName,int iView, int iSetPrint)
		{
			//����Ա
			string Operator = Page.User.Identity.Name;
			
			//�����ݼ�����XML�ַ�����Ȼ�󴫸����ض���
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
		/// ���ط������˷������ڷ�������������ʱXML�ļ�����������XML�ַ�����Ȼ�󴫸�WEBҳ���ϵ����ؿؼ�
		/// </summary>
		/// <param name="ds">DataSet���ݼ�</param>
		/// <param name="txtReportData">HtmlInputHidden���ؿؼ����������XML�ַ���</param>
		/// <param name="ReportName">��ӡ������ƣ����磺ѧԱ֤</param>
		/// <param name="iView">�Ƿ�Ԥ����1��Ԥ����0��ֱ�Ӵ�ӡ</param>
		/// <param name="iSetPrint">�Ƿ��״�1���״�0�����״�</param>
		public void PrintView(DataSet ds,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string ReportName,int iView, int iSetPrint)
		{
			string ReportTemplateFile=REPORT_PATH+ReportName+".txt";
			ReportTemplateFile="http://" + Request.ServerVariables["HTTP_HOST"] + Page.ResolveUrl(ReportTemplateFile);
			PrintView(ds,txtReportData,ReportTemplateFile,ReportName,iView,iSetPrint);
		}		
		#endregion

		#region DataGridͨ�ô�ӡ
		/// <summary>
		/// DataGridͨ�ô�ӡ
		/// </summary>
		/// <param name="dg">DataGrid����</param>
		/// <param name="txtReportData">HtmlInputHidden�����������XML�ַ���</param>
		/// <param name="dgTitle">��������ɵ��ַ�����ÿ������֮���ö��ŷָ���DataGrid�������Ƕ�̬�����ģ�����Ҫ�˲���</param>
		/// <param name="ReportTitle">�������</param>
		/// <param name="QueryCondition">��������</param>
		/// <param name="iView">�Ƿ�Ԥ��</param>
		/// <example>
		/// <code>
		/// ʾ��1����ӡDataGrid1�е����ݣ�DataGrid1�е������ж��Ǿ�̬���ɵġ�
		/// //���ҳ����û��HtmlInputHidden�ؼ������Դ�ActionNavigator����һ����
		/// HtmlInputHidden txtData=(HtmlInputHidden)ActionNavigator1.FindControl("txtTrainPrintReportData");
		///	PrintViewDataGrid(DataGrid1,txtData,"","�ʼ����ͼ�¼��","",1);
		///	
		/// ʾ��2����ӡDataGrid1�е����ݣ�DataGrid1�е������ж��Ǿ�̬���ɵġ�
		/// protected System.Web.UI.HtmlControls.HtmlInputHidden txtHidden;
		/// ...
		///	PrintViewDataGrid(DataGrid1,txtHidden,"","�ʼ����ͼ�¼��","",1);
		///	
		/// ʾ��3����ӡDataGrid1�е����ݣ�DataGrid1���ж�̬̬���ɵ������С�
		/// HtmlInputHidden txtData=(HtmlInputHidden)ActionNavigator1.FindControl("txtTrainPrintReportData");
		/// string strTitleNames=",��ˮ��,����ʱ��,����״̬,��λ����,�ռ���"
		///	PrintViewDataGrid(DataGrid1,txtHidden,"","�ʼ����ͼ�¼��","",1);
		/// </code>
		/// </example>
		/// <remarks>
		/// <code>
		/// ��ע��
		/// 1.�����У���������Ϊ�յ��ж������ԡ�
		/// 2.AutoGenerateColumnsΪtrue��ʱ���޷�ֱ�ӻ�ȡ������������Ҫ����dgTitle�����������ַ�������
		/// 3.���dgTitle������Ϊ�գ���ôdgTitleһ��Ҫ��DataGrid�е��б���һ�¡�
		/// </code>
		/// </remarks>
		public void PrintViewDataGrid(DataGrid dg,System.Web.UI.HtmlControls.HtmlInputHidden txtReportData,string dgTitle,string ReportTitle,string QueryCondition,int iView)
		{
			//����Ա
			string Operator = Page.User.Identity.Name;
			string IsAlignRightColumns;
			//��������
			txtReportData.Value=PublicClass.ReturnDataGridXML(dg,dgTitle,out IsAlignRightColumns);
			//����ģ��
			string ReportTemplateFile=REPORT_PATH+"����.txt";
			ReportTemplateFile="http://" + Request.ServerVariables["HTTP_HOST"] + Page.ResolveUrl(ReportTemplateFile);
			
			string strScript=string.Format(@"
				<script language=javascript>
				document.all.TrainPrintReport.ReportDataStr=document.all('{0}').value;
				document.all.TrainPrintReport.ReportTemplateFile='{1}';
				document.all.TrainPrintReport.Operator='{2}';
				document.all.TrainPrintReport.ReportTitle='{3}';
				document.all.TrainPrintReport.QueryCondition='{4}';
				document.all.TrainPrintReport.IsAlignRightColumns='{5}';
				document.all.TrainPrintReport.PrintView('����',{6},0); 
				</script>",txtReportData.ClientID,ReportTemplateFile,Operator,ReportTitle,QueryCondition,IsAlignRightColumns,iView);
			Page.ClientScript.RegisterStartupScript(this.GetType(),"btnPrintView_Click",strScript);			
		}
		#endregion

        #region ExportToExcel������excel
        /// <summary>
        /// ����excel
        /// </summary>
        /// <param name="FileName">����ļ���</param>
        /// <param name="dt">��Ҫ�����Datatable</param>
        public void ExportToExcel(string FileName, DataTable dt)
        {
            Response.Clear();
            Response.Buffer = false;
            //������䵼�������������(ע�ͣ�³ΰ��)  
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
        /// ����excel
        /// </summary>
        /// <param name="FileName">����ļ���</param>
        /// <param name="ControlID">Ҫ����ؼ�id</param>
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
        /// �����ṩ��ģ��,����excel
        /// </summary>
        /// <param name="FileName">����ļ���</param>
        /// <param name="TFileName">ģ���ļ���</param>
        /// <param name="ds">DataTable���ϣ���һ�������Ϊ��ͷ���ڶ��������Ϊ���壻�����������Ϊ��β</param>
        public void ExportToExcelWithT(string FileName, string TFileName, DataSet ds)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //�õ�ģ���ļ���·��
            string strVirPath = System.Configuration.ConfigurationManager.AppSettings["TempletPath"] + TFileName;
            string filePath = Server.MapPath(strVirPath);
            //��ȡģ���ļ�,�洢��arraylist��
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

            //���ɱ�,�����洢Excel����
            DataTable tmpDT = new DataTable();
            DataRow dr = tmpDT.NewRow();
            for (int colcnt = 0; colcnt < ds.Tables[1].Columns.Count; colcnt++)
            {
                tmpDT.Columns.Add(new DataColumn());
                dr[colcnt] = ds.Tables[1].Columns[colcnt].Caption;
            }
            tmpDT.Rows.Add(dr);

            ds.Tables.Add(tmpDT);

            //�ж��ṩ����Դ�Ƿ�Ϊ��
            if (ds.Tables[1].Rows.Count < 1)
            {
                this.MessageBox("��ѡ������Դ��Ȼ�󵼳�Excel.");
                return;
            }

            //����Repons��һЩ����,��HTTP ��Ӧ���ݷ��͵��ͻ���
            Response.Clear();
            Response.Buffer = false;

            Response.Charset = "utf-8";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";

            //���б��������ļ���ƴ���ַ���
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            string newLine = "";
            string newRow = "";
            oHtmlTextWriter.WriteLine(newLine);


            for (int i = 0; i < arrText.Count; i++)
            {
                int flag = -1; //��ʾ�Ƿ���ѭ����table:-1:��ѭ��
                flag = arrText[i].ToString().IndexOf("circle");

                int idx, x, tmp = 0;
                string y, str1 = "", str2 = "", str3 = "";
                newLine = arrText[i].ToString();
                idx = newLine.LastIndexOf("parm");
                if (idx == -1) //û�в�����Ҫ�滻
                {
                    oHtmlTextWriter.WriteLine(newLine);
                    continue;
                }

                x = Convert.ToInt32(newLine.Substring(idx + 4, 1));    //��ʾӦ�ð��Ǹ������
                y = Convert.ToString(newLine.Substring(idx + 5, 1));    //��ʾ������ĵڼ�������

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
        /// ���ݱ��͵���excel
        /// </summary>
        /// <param name="FileName">����ļ���</param>
        /// <param name="dt">��Ҫ�����Datatable</param>
        public void ExportToSJBSExcel(string FileName, DataTable dt)
        {
            Response.Clear();
            Response.Buffer = false;
            //������䵼�������������(ע�ͣ�³ΰ��)  
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

        #region SetButtonPower������ҳ������İ�ťȨ��
        /// <summary>
        /// ��ȡ��ťȨ��
        /// </summary>
        /// <param name="ArrayPowers">Ȩ���б�</param>
        /// <param name="btn">��ť</param>
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
                if (strBtnCode == sysid) //value������д
                {
                    bResult = true; 
                    break;
                }
            }

            return bResult;
        }


        /// <summary>
        /// ����ҳ�水ťȨ��
        /// </summary>
        /// <param name="btns">��ť���顣������HtmlButton��HtmlImage����</param>
        /// <returns></returns>
        public string SetButtonPower(params System.Web.UI.Control[] btns)
        {
            return SetButtonPower(true, btns);
        }

        /// <summary>
        /// ����ҳ�水ťȨ��
        /// </summary>
        /// <param name="IsHidden">��û��Ȩ��ʱ�Ƿ����ظö���</param>
        /// <param name="btns">��ť���顣������HtmlButton��HtmlImage����</param>
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

                if (!bActive && IsHidden) //û��Ȩ��
                    btn.Visible = false;

                if (bActive == true) //�ð�ť��Ȩ��ʹ��
                {
                    if (strBtnID == "")  //���ص�һ����Ȩ��ʹ�õĹ��ܰ�ť���Ա����ҳ��ʱ�Զ�ִ��
                        strBtnID = btn.ID;

                    //��ĩβΪ��c.jpg��Ϊ��ɫͼƬ��Ӧ�ñ�ɻͼƬ
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

        #region OnInit������ҳ��ĳ�ʼ��������
        /// <summary>
        /// ����ҳ��ĳ�ʼ������������ҳ��ʱ�����ɽ�����
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
            sWaiting += "<td align=\"center\" class=\"tsnr\">ϵͳ���ڴ������Ժ�......";
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
