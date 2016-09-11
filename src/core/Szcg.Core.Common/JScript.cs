/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;��JScript�࣬�������ɿͻ���JavaScript�ű����ַ������Ա���Ӧ�ͻ����¼�
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

namespace Teamax.Common
{
	/// <summary>
	/// JScript�࣬�������ɿͻ���JavaScript�ű����ַ������Ա���Ӧ�ͻ����¼���
	/// </summary>
	/// <seealso cref="Teamax.Common.CommonPage"/>
	public class JScript
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public JScript()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// ���ô���״̬����Ϣ
		///window.status='{0}'
		/// </summary>
		public const string WinStatusMessage = "window.status='{0}'";
		/// <summary>
		/// �رմ���
		/// window.parent.close()
		/// </summary>
		public const string ParentWinClose="window.parent.close()";
        /// <summary>
        /// �رմ���
        /// window.parent.close()
        /// </summary>
        public const string WinClose = "window.close()";
		/// <summary>
		/// ����false
		/// ;return false
		/// </summary>
		public const string ReturnFalse =";return false";
		/// <summary>
		/// ����ڿؼ��ƶ�����ɫ
		/// this.style.backgroundColor='{0}'
		/// </summary>
		public const string OnMoveStyle ="this.style.backgroundColor='{0}'";
		/// <summary>
		/// ����Ƴ��ؼ�����ɫ
		/// this.style.backgroundColor ='{0}'
		/// </summary>
		public const string OnMoveOutStyle ="this.style.backgroundColor ='{0}'";
		/// <summary>
		/// ���ô���ı���
		/// window.document.title='{0}'
		/// </summary>
		public const string SetWindowTitle="window.document.title='{0}'";
		/// <summary>
		/// ���ÿؼ���ֵ
		/// window.document.all('{0}').value='{1}';
		/// </summary>
		public const string SetControlValue="window.document.all('{0}').value='{1}';";
		
		/// <summary>
		/// ������ʾ��Ϣ
		/// window.alert('{0}')
		/// </summary>
		public const string WinAlert="window.alert(\"{0}\")";
		/// <summary>
		/// ��ʾ���ʿ�
		/// return window.confirm('{0}')
		/// </summary>
		public const string MessageDailog ="return window.confirm('{0}')";
		/// <summary>
		/// ��ʾ���ʿ�,רΪHtmlButton����
		/// </summary>
		public const string MessageDailog_HtmlButton ="if(window.confirm('{0}'))";
		//public const string MessageDailog_delete="window.document.all('{0}').value=window.confirm('{1}');";

		/// <summary>
		/// ���ô��巵��ֵ
		/// window.returnValue=\"{0}\"
		/// </summary>
		public const string SetReturnValue = "window.returnValue=\"{0}\"";
		/// <summary>
		/// ���ÿؼ�������
		/// </summary>
		public const string HiddenControl = "document.all(\"{0}\").style.visibility=\"hidden\"";
		
		
		/// <summary>
		/// ����ָ����,��ȵ�ģʽ����ű�
		/// </summary>
		/// <param name="PageName">ҳ���ļ�</param>
		/// <param name="width">������</param>
		/// <param name="height">����߶�</param>
		/// <returns>string</returns>
		public static string OpenDialog(string PageName,int width,int height)
		{
			return OpenDialog(PageName,width,height,true);
		}

		/// <summary>
		/// ����ָ����,��ȵ�ģʽ����ű�
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="resizable">�Ƿ���������߿�</param>
		/// <returns></returns>
		public static string OpenDialog(string PageName,int width,int height,bool resizable)
		{
			string strOpen = "window.showModalDialog('{0}','','dialogHeight: {1}px; dialogWidth: {2}px; edge: Raised; center: Yes; help: No; resizable: {3}; status: No;');";
			string strResizable = resizable?"Yes":"No";
			return string.Format(strOpen,PageName,height,width,strResizable);
		}



		/// <summary>
		/// ����ָ����,��ȵ�ģʽ����ű���
		/// ���Ѵ��巵��ָ������Ӧ�Ŀؼ�
		/// </summary>
		/// <param name="PageName">ҳ���ļ�</param>
		/// <param name="width">������</param>
		/// <param name="height">����߶�</param>
		/// <param name="resizable"></param>
		/// <param name="ControlClientID">����ֵ�ؼ�</param>
		/// <returns>string</returns>
		public static string OpenDialog(string PageName,int width,int height,bool resizable,string ControlClientID)
		{
			return OpenDialog(PageName,width,height,resizable,ControlClientID,0);
		}

		/// <summary>
		/// ����ָ����,��ȵ�ģʽ����ű���
		/// ���Ѵ��巵��ָ������Ӧ�Ŀؼ�
		/// </summary>
		/// <param name="PageName">ҳ���ļ�</param>
		/// <param name="width">������</param>
		/// <param name="height">����߶�</param>
		/// <param name="resizable"></param>
		/// <param name="ControlClientID">����ֵ�ؼ�</param>
		/// <param name="HaveQuot">������ʶ����PageName����������</param>
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
