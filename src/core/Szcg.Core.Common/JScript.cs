/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：JScript类，用于生成客户端JavaScript脚本的字符串，以便响应客户端事件
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

namespace Teamax.Common
{
	/// <summary>
	/// JScript类，用于生成客户端JavaScript脚本的字符串，以便响应客户端事件。
	/// </summary>
	/// <seealso cref="Teamax.Common.CommonPage"/>
	public class JScript
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public JScript()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		/// <summary>
		/// 设置窗体状态栏信息
		///window.status='{0}'
		/// </summary>
		public const string WinStatusMessage = "window.status='{0}'";
		/// <summary>
		/// 关闭窗体
		/// window.parent.close()
		/// </summary>
		public const string ParentWinClose="window.parent.close()";
        /// <summary>
        /// 关闭窗体
        /// window.parent.close()
        /// </summary>
        public const string WinClose = "window.close()";
		/// <summary>
		/// 返回false
		/// ;return false
		/// </summary>
		public const string ReturnFalse =";return false";
		/// <summary>
		/// 鼠标在控件移动背景色
		/// this.style.backgroundColor='{0}'
		/// </summary>
		public const string OnMoveStyle ="this.style.backgroundColor='{0}'";
		/// <summary>
		/// 鼠标移出控件背景色
		/// this.style.backgroundColor ='{0}'
		/// </summary>
		public const string OnMoveOutStyle ="this.style.backgroundColor ='{0}'";
		/// <summary>
		/// 设置窗体的标题
		/// window.document.title='{0}'
		/// </summary>
		public const string SetWindowTitle="window.document.title='{0}'";
		/// <summary>
		/// 设置控件的值
		/// window.document.all('{0}').value='{1}';
		/// </summary>
		public const string SetControlValue="window.document.all('{0}').value='{1}';";
		
		/// <summary>
		/// 弹出提示信息
		/// window.alert('{0}')
		/// </summary>
		public const string WinAlert="window.alert(\"{0}\")";
		/// <summary>
		/// 显示提问框
		/// return window.confirm('{0}')
		/// </summary>
		public const string MessageDailog ="return window.confirm('{0}')";
		/// <summary>
		/// 显示提问框,专为HtmlButton服务
		/// </summary>
		public const string MessageDailog_HtmlButton ="if(window.confirm('{0}'))";
		//public const string MessageDailog_delete="window.document.all('{0}').value=window.confirm('{1}');";

		/// <summary>
		/// 设置窗体返回值
		/// window.returnValue=\"{0}\"
		/// </summary>
		public const string SetReturnValue = "window.returnValue=\"{0}\"";
		/// <summary>
		/// 设置控件不可视
		/// </summary>
		public const string HiddenControl = "document.all(\"{0}\").style.visibility=\"hidden\"";
		
		
		/// <summary>
		/// 返回指定高,宽度的模式窗体脚本
		/// </summary>
		/// <param name="PageName">页面文件</param>
		/// <param name="width">窗体宽度</param>
		/// <param name="height">窗体高度</param>
		/// <returns>string</returns>
		public static string OpenDialog(string PageName,int width,int height)
		{
			return OpenDialog(PageName,width,height,true);
		}

		/// <summary>
		/// 返回指定高,宽度的模式窗体脚本
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="resizable">是否可以拉动边框</param>
		/// <returns></returns>
		public static string OpenDialog(string PageName,int width,int height,bool resizable)
		{
			string strOpen = "window.showModalDialog('{0}','','dialogHeight: {1}px; dialogWidth: {2}px; edge: Raised; center: Yes; help: No; resizable: {3}; status: No;');";
			string strResizable = resizable?"Yes":"No";
			return string.Format(strOpen,PageName,height,width,strResizable);
		}



		/// <summary>
		/// 返回指定高,宽度的模式窗体脚本，
		/// 并把窗体返回指定到相应的控件
		/// </summary>
		/// <param name="PageName">页面文件</param>
		/// <param name="width">窗体宽度</param>
		/// <param name="height">窗体高度</param>
		/// <param name="resizable"></param>
		/// <param name="ControlClientID">接收值控件</param>
		/// <returns>string</returns>
		public static string OpenDialog(string PageName,int width,int height,bool resizable,string ControlClientID)
		{
			return OpenDialog(PageName,width,height,resizable,ControlClientID,0);
		}

		/// <summary>
		/// 返回指定高,宽度的模式窗体脚本，
		/// 并把窗体返回指定到相应的控件
		/// </summary>
		/// <param name="PageName">页面文件</param>
		/// <param name="width">窗体宽度</param>
		/// <param name="height">窗体高度</param>
		/// <param name="resizable"></param>
		/// <param name="ControlClientID">接收值控件</param>
		/// <param name="HaveQuot">用来标识参数PageName里面有引号</param>
		/// <returns>string</returns>
		public static string OpenDialog(string PageName,int width,int height,bool resizable,string ControlClientID,int HaveQuot)
		{
			string strOpen ="";
			if(HaveQuot==2)
				strOpen = "var getvalue=window.showModalDialog(&quot;{0}&quot;,'','dialogHeight: {1}px; dialogWidth: {2}px; edge: Raised; center: Yes; help: No; resizable: {3}; status: No;')";
			else if(HaveQuot==1)
				strOpen = "var getvalue=window.showModalDialog(\"{0}\",'','dialogHeight: {1}px; dialogWidth: {2}px; edge: Raised; center: Yes; help: No; resizable: {3}; status: No;')";				
			else
				strOpen = "var getvalue=window.showModalDialog('{0}','','dialogHeight: {1}px; dialogWidth: {2}px; edge: Raised; center: Yes; help: No; resizable: {3}; status: No;')";
			strOpen +=";if(getvalue !=null) window.document.all('{4}').value=getvalue;";

			string strResizable = resizable?"Yes":"No";

			return string.Format(strOpen,PageName,height,width,strResizable,ControlClientID);
		}
	}
}
