/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;�����ݿ�����ࡣ��ADO.NET���м򵥷�װ������ʹ�á�
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
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Teamax.Common
{
	/// <summary>
	/// �����ࡣ
	/// PublicClass�ṩһϵ�г��õľ�̬������
	/// </summary>
	/// <example>
	/// 
	/// </example>
	/// <remarks>
	/// </remarks>
	public class PublicClass
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PublicClass()
		{
			// 
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// ö�����ͣ����ܰ�ť��Ȩ������
		/// </summary>
		public enum ButtonPowerType
		{
			/// <summary>
			/// ���Ȩ��
			/// </summary>
			BrowsePower,
			/// <summary>
			/// ����Ȩ��
			/// </summary>
			OperatorPower,
			/// <summary>
			/// ��ӡȨ��
			/// </summary>
			PrintPower			
		}

		#region GetMenuCode����ȡ��ǰҳ��Ĳ˵�����
		/// <summary>
		/// ��ȡ��ǰҳ��Ĳ˵�����
		/// </summary>
		/// <param name="URL">ҳ���ַ</param>
		/// <returns>����ҳ������Ӧ�Ĳ˵�����</returns>
		/// <example>
		/// <code>
		/// 	private void BUTTON1_ServerClick(object sender, System.EventArgs e) 
		///		{
		///			string URL=Request.ServerVariables["SCRIPT_NAME"].ToString();
		///			TextBox1.Text=PublicClass.GetMenuCode(strConn,URL);
		///		}
		/// </code>
		/// </example> 
		public static string GetMenuCode(string URL)
		{
			string strResult="";

			using (CommonDatabase MyDatabase = new CommonDatabase())
			{
				try 
				{
					string strSQL="SELECT MenuCode FROM system_MenuInfo WHERE URL LIKE '%"+URL+"%'";
					object oResult=MyDatabase.ExecuteScalar(strSQL);
					if(oResult!=null)
						strResult=oResult.ToString();
					return strResult;
				}
				catch 
				{
					return strResult;
				}
			}			
		}
		#endregion

		#region GetOperateName�����ص�ǰ����Ա����������
		/// <summary>
		/// ���ص�ǰ����Ա����������
		/// </summary>
		/// <param name="sender">��ǰҳ��Page</param>
		/// 		/// <returns>���ص�ǰ����Ա����������</returns>
		/// <example>
		///		ʾ������ȡ��ǰҳ��Ĳ���Ա����������
		///	<code>
		///		TextBox1.Text=PublicClass.GetOperateName(Page);
		///	</code>
		/// </example>
		public static string GetOperateName(object sender)
		{
			string strResult="";
			string BranchCode="";
			string conn="";
			string ManID;

			const string SQL_HaveBranchCode="SELECT ManName FROM System_ManInfo WHERE ManID='{0}' AND BranchCode='{1}'";
			const string SQL_NoHaveBranchCode="SELECT ManName FROM System_ManInfo WHERE ManID='{0}'";
			if(sender is System.Web.UI.Page)
			{
				if(((System.Web.UI.Page)sender).Session["BranchCode"]!=null)
					BranchCode=((System.Web.UI.Page)sender).Session["BranchCode"].ToString(); 
				if(((System.Web.UI.Page)sender).Application["Connection"]!=null)
					conn=((System.Web.UI.Page)sender).Application["Connection"].ToString();
				ManID=((System.Web.UI.Page)sender).User.Identity.Name; 
			}
			else
				return "";
			
			SqlCommand myCommand = new SqlCommand();
			using(myCommand)
			{
				myCommand.Connection = new SqlConnection(conn) ;
				try
				{
					if(myCommand.Connection.State==ConnectionState.Closed) 
						myCommand.Connection.Open();
					if(BranchCode!="")
						myCommand.CommandText = string.Format(SQL_HaveBranchCode,ManID,BranchCode);
					else
						myCommand.CommandText = string.Format(SQL_NoHaveBranchCode,ManID);
					object oResult=myCommand.ExecuteScalar();
					if(oResult!=null)
						strResult=oResult.ToString();
				}
				finally 
				{
					myCommand.Connection.Close();
				} //try ... finally				
			} //using(myCommand)
			return strResult;
		}
		#endregion

		#region GetButtonPower���õ���ť��Ȩ��
		/// <summary>
		/// �õ���ť��Ȩ��
		/// </summary>
		/// <param name="conn">���ݿ������ַ���</param>
		/// <param name="BranchCode">��֧��������</param>
		/// <param name="OperateID">����Ա����</param>
		/// <param name="MenuCode">�˵�����</param>
		/// <param name="btnPowerType">��ť������Ȩ������
		/// <seealso cref="ButtonPowerType"/>
		/// </param>
		/// <returns>���أ��桢��ֵ</returns>
		public static bool GetButtonPower(string conn,string BranchCode,string OperateID,string MenuCode,ButtonPowerType btnPowerType)
		{
			bool bBtnEnabled=false;
			string strSQL="";

			const string BrowsePowerSQL_HaveBranchCode = "SELECT BrowsePower FROM System_MenuPower WHERE BranchCode='{0}' AND OperatorID='{1}' AND Menucode='{2}'";
			const string OperatorPowerSQL_HaveBranchCode = "SELECT OperatorPower FROM System_MenuPower WHERE BranchCode='{0}' AND OperatorID='{1}' AND Menucode='{2}'";
			const string PrintPower_HaveBranchCode = "SELECT PrintPower FROM System_MenuPower WHERE BranchCode='{0}' AND OperatorID='{1}' AND Menucode='{2}'";

			const string BrowsePowerSQL_NoHaveBranchCode="SELECT BrowsePower FROM System_MenuPower WHERE OperatorID='{0}' AND Menucode='{1}'";
			const string OperatorPowerSQL_NoHaveBranchCode = "SELECT OperatorPower FROM System_MenuPower WHERE OperatorID='{0}' AND Menucode='{1}'";
			const string PrintPower_NoHaveBranchCode = "SELECT PrintPower FROM System_MenuPower WHERE OperatorID='{0}' AND Menucode='{1}'";
			if(BranchCode!="")
				switch(btnPowerType)
				{
					case ButtonPowerType.BrowsePower:
						strSQL=string.Format(BrowsePowerSQL_HaveBranchCode,BranchCode,OperateID,MenuCode);
						break;
					case ButtonPowerType.OperatorPower:
						strSQL=string.Format(OperatorPowerSQL_HaveBranchCode,BranchCode,OperateID,MenuCode);
						break;
					case ButtonPowerType.PrintPower:
						strSQL=string.Format(PrintPower_HaveBranchCode,BranchCode,OperateID,MenuCode);
						break;
				}
			else
				switch(btnPowerType)
				{
					case ButtonPowerType.BrowsePower:
						strSQL=string.Format(BrowsePowerSQL_NoHaveBranchCode,OperateID,MenuCode);
						break;
					case ButtonPowerType.OperatorPower:
						strSQL=string.Format(OperatorPowerSQL_NoHaveBranchCode,OperateID,MenuCode);
						break;
					case ButtonPowerType.PrintPower:
						strSQL=string.Format(PrintPower_NoHaveBranchCode,OperateID,MenuCode);
						break;
				}
			
			SqlCommand myCommand = new SqlCommand();
			using(myCommand)
			{
				myCommand.Connection = new SqlConnection(conn) ;
				try
				{
					if(myCommand.Connection.State==ConnectionState.Closed) 
						myCommand.Connection.Open();
					myCommand.CommandText = strSQL;
					object oResult=myCommand.ExecuteScalar();
					if(oResult!=null)
						if(oResult.ToString()=="1")
							bBtnEnabled=true;
				}
				finally 
				{
					myCommand.Connection.Close();
				} //try ... finally	
			}

			return bBtnEnabled;
		}

		/// <summary>
		/// ���ط������õ���ť��Ȩ��
		/// </summary>
		/// <param name="page"></param>
		/// <param name="btnPowerType"></param>
		/// <returns>���أ��桢��ֵ</returns>
		/// <example>
		/// <code>
		/// ʾ�����ж��Ƿ��в���Ȩ��
		///		private void Page_Load(object sender, System.EventArgs e)
		///		{
		///			bool IsHaveOperatorPower=Teamax.Common.PublicClass.GetButtonPower(Page,ButtonPowerType.OperatorPower); 
		///		}
		/// </code>
		/// </example>
		public static bool GetButtonPower(object page,ButtonPowerType btnPowerType)
		{
			string conn="";
			string OperateID="";
			string BranchCode="";
			string MenuCode="";

			if(!(page is System.Web.UI.Page))
				return false;
			
			if(((System.Web.UI.Page)page).Session["BranchCode"]!=null)
				BranchCode=((System.Web.UI.Page)page).Session["BranchCode"].ToString(); 
			if(((System.Web.UI.Page)page).Application["Connection"]!=null)
				conn=((System.Web.UI.Page)page).Application["Connection"].ToString();
			OperateID=((System.Web.UI.Page)page).User.Identity.Name; 
			if(((System.Web.UI.Page)page).Request["MenuCode"]!=null)
				MenuCode=((System.Web.UI.Page)page).Request["MenuCode"].ToString();
			else
			{
				string URL=((System.Web.UI.Page)page).Request.ServerVariables["SCRIPT_NAME"].ToString();
				MenuCode=GetMenuCode(URL);
			}

			return GetButtonPower(conn,BranchCode,OperateID,MenuCode,btnPowerType);
		}
		#endregion

		#region SetButtonPower�����ݵ�ǰ����Ա��Ȩ�����趨���ܰ�ť�Ƿ����ɲ���
		/// <summary>
		/// ���ݵ�ǰ����Ա��Ȩ�����趨���ܰ�ť�Ƿ����ɲ���
		/// </summary>
		/// <param name="conn">���ݿ������ַ���</param>
		/// <param name="BranchCode">��֧��������</param>
		/// <param name="OperateID">����Ա����</param>
		/// <param name="MenuCode">�˵�����</param>
		/// <param name="sender">��ť</param>
		/// <param name="btnPowerType">��ť������Ȩ������
		/// <seealso cref="ButtonPowerType"/>
		/// </param>
		/// <example>
		/// <code>
		///		private void Page_Load(object sender, System.EventArgs e)
		///		{
		///			string conn=Application["Connection"].ToString();
		///			string OperateID=User.Identity.Name;
		///			string BranchCode=Session["BranchCode"].ToString();
		///			string MenuCode="";
		///
		///			if(Request["MenuCode"]!=null)
		///				MenuCode=Request["MenuCode"].ToString();
		///			else
		///			{
		///				string URL=Request.ServerVariables["SCRIPT_NAME"].ToString();
		///				MenuCode=GetMenuCode(conn,URL);
		///			}
		///
		///			SetButtonPower(conn,BranchCode,OperateID,MenuCode,button,ButtonPowerType.OperatorPower);
		///		}
		///	</code>
		/// </example>
		public static void SetButtonPower(string conn,string BranchCode,string OperateID,string MenuCode,object sender,ButtonPowerType btnPowerType)
		{
			bool bBtnEnabled=GetButtonPower(conn,BranchCode,OperateID,MenuCode,btnPowerType);

			if(sender is System.Web.UI.WebControls.WebControl)
				((System.Web.UI.WebControls.WebControl)sender).Enabled=bBtnEnabled;
			else
				((System.Web.UI.HtmlControls.HtmlControl)sender).Disabled=!bBtnEnabled;
		}
		
		
		/// <summary>
		/// ���غ��������ݵ�ǰ����Ա��Ȩ�����趨���ܰ�ť�Ƿ����ɲ���
		/// </summary>
		/// <param name="page">��ǰҳ��</param>
		/// <param name="button">ҳ���ϵ�ĳ����ť</param>
		/// <param name="btnPowerType">��ť������Ȩ������</param>
		/// <example>
		///		ʾ��������ĳ��ҳ���ϵĴ�ӡ���ܰ�ťButton1�Ե�ǰ����Ա�Ƿ�ɲ���
		///	<code>
		///		private void Page_Load(object sender, System.EventArgs e)
		///		{
		///			Teamax.Common.PublicClass.SetButtonPower(Page,Button1,ButtonPowerType.PrintPower); 
		///		}
		///	</code>
		/// </example>
		public static void SetButtonPower(object page,object button,ButtonPowerType btnPowerType)
		{
			string conn="";
			string OperateID="";
			string BranchCode="";
			string MenuCode="";

			if(!(page is System.Web.UI.Page))
				return;
			
			if(((System.Web.UI.Page)page).Session["BranchCode"]!=null)
				BranchCode=((System.Web.UI.Page)page).Session["BranchCode"].ToString(); 
			if(((System.Web.UI.Page)page).Application["Connection"]!=null)
				conn=((System.Web.UI.Page)page).Application["Connection"].ToString();
			OperateID=((System.Web.UI.Page)page).User.Identity.Name; 
			if(((System.Web.UI.Page)page).Request["MenuCode"]!=null)
				MenuCode=((System.Web.UI.Page)page).Request["MenuCode"].ToString();
			else
			{
				string URL=((System.Web.UI.Page)page).Request.ServerVariables["SCRIPT_NAME"].ToString();
				MenuCode=GetMenuCode(URL);
			}

			SetButtonPower(conn,BranchCode,OperateID,MenuCode,button,btnPowerType);
		}

		/// <summary>
		/// ���غ��������ݵ�ǰ����Ա��Ȩ�����趨���ܰ�ť�Ƿ����ɲ������ڴ����غ�����
		///		���ܰ�ť��Ȩ������Ĭ���ǲ������ͣ�ButtonPowerType.OperatorPower��
		/// </summary>
		/// <param name="page">��ǰҳ��</param>
		/// <param name="button">ҳ���ϵ�ĳ����ť</param>
		/// <example>
		///		ʾ��������ĳ��ҳ���ϵĲ������ܰ�ťButton1�Ե�ǰ����Ա�Ƿ�ɲ���
		/// <code>
		///		private void Page_Load(object sender, System.EventArgs e)
		///		{
		///			Teamax.Common.PublicClass.SetButtonPower(Page,Button1); 
		///		}
		///	</code>
		/// </example>
		public static void SetButtonPower(object page,object button)
		{
				SetButtonPower(page,button,ButtonPowerType.OperatorPower);
		}

		/// <summary>
		/// ���غ��������ݵ�ǰ����Ա��Ȩ�����趨���ܰ�ť�Ƿ����ɲ�����Ĭ����Щ���ܰ�ť�ǲ������͡�
		/// </summary>
		/// <param name="page">��ǰҳ��</param>
		/// <param name="button">ҳ���ϵ�һϵ�а�ť��������1��������ť</param>
		/// <example>
		///		ʾ��������ĳ��ҳ���ϵ�Button1,Button2,Button3�����������ܰ�ť�Ե�ǰ����Ա�Ƿ�ɲ���
		///	<code>
		///		private void Page_Load(object sender, System.EventArgs e)
		///		{
		///			Teamax.Common.PublicClass.SetButtonPower(Page,Button1,Button2,Button3); 
		///		}
		///	</code>
		/// </example>
		public static void SetButtonPower(object page,params object[] button)
		{
			object[] buttons = button;
			for (int i = 0; i < (int)buttons.Length; i++)
			{
				object btn = buttons[i];
				SetButtonPower(page,btn,ButtonPowerType.OperatorPower);
			}						
		}
		#endregion

		#region GetSystemParameter����ȡϵͳ����ֵ
		/// <summary>
		/// ��ȡϵͳ����ֵ
		/// </summary>
		/// <param name="conn">���ݿ������ַ���</param>
		/// <param name="BranchCode">��֧��������</param>
		/// <param name="ParameterEnName">��������</param>
		/// <returns>���ز���ֵ</returns>
		/// <example>
		///		ʾ������ȡϵͳ����������λ"DW"��ֵ
		///	<code>
		///		string strConn = Application["Connection"].ToString();
		///		string BranchCode=Session["BranchCode"].ToString();			
		///		TextBox3.Text=PublicClass.GetSystemParameter(strConn,BranchCode,"DW"); 
		///	</code>
		/// </example>
		public static string GetSystemParameter(string conn,string BranchCode,string ParameterEnName)
		{
			string strResult="";
			string EntryCode="";
			string ParameterValue="";
			const string SQL_HaveBranchCode="SELECT EntryCode,ParameterValue FROM System_ParameterSet WHERE BranchCode='{0}' AND ParameterEnName='{1}'";
			const string SQL_NoHaveBranchCode="SELECT EntryCode,ParameterValue FROM System_ParameterSet WHERE ParameterEnName='{0}'";

			SqlCommand myCommand = new SqlCommand();
			using(myCommand)
			{
				myCommand.Connection = new SqlConnection(conn) ;
				try
				{
					if(myCommand.Connection.State==ConnectionState.Closed) 
						myCommand.Connection.Open();
					if(BranchCode!="") 
						myCommand.CommandText = string.Format(SQL_HaveBranchCode,BranchCode,ParameterEnName);
					else
						myCommand.CommandText = string.Format(SQL_NoHaveBranchCode,ParameterEnName);
					SqlDataReader rdr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
					if(rdr.Read())
					{
						if(!(rdr[0] is DBNull))
							EntryCode=rdr.GetString(0);
						if(!(rdr[1] is DBNull))
							ParameterValue=rdr.GetString(1);

						if(EntryCode!=""&&ParameterValue!="")
						{
							rdr.Close();
							
							if(myCommand.Connection.State==ConnectionState.Closed) 
								myCommand.Connection.Open();
							myCommand.CommandText = string.Format("select ContentName from System_EntryContent where EntryCode='{0}' and ContentCode='{1}'",EntryCode,ParameterValue);
							object oResult=myCommand.ExecuteScalar();
							if(oResult!=null)
								strResult=oResult.ToString();
						}
						else
							strResult=ParameterValue;
					}
					if(!rdr.IsClosed)
						rdr.Close();
				}
				catch(Exception e)
				{
					throw new Exception("��ȡϵͳ����ֵ����!<br>" + e.Message); 
				} //try ... catch				
			} //using(myCommand)
			return strResult;
		}

		/// <summary>
		/// ���غ�������ȡϵͳ����ֵ���޷�֧��������
		/// </summary>
		/// <param name="conn">���ݿ������ַ���</param>
		/// <param name="ParameterEnName">��������</param>
		/// <returns>���ز���ֵ</returns>
		/// <example>
		///		ʾ������ȡϵͳ����������λ"DW"��ֵ
		///	<code>
		///		string strConn = Application["Connection"].ToString();
		///		TextBox3.Text=PublicClass.GetSystemParameter(strConn,"DW"); 
		///	</code>
		/// </example> 
		public static string GetSystemParameter(string conn,string ParameterEnName)
		{
			return GetSystemParameter(conn,"",ParameterEnName);
		}	
		#endregion

		#region LookupObjectPropertyϵ�з���������Key��ѯ�ض���ֵ
		/// <summary>
		/// ����Key��ѯ�ض���ֵ
		/// </summary>
		/// <param name="key">����ֵ</param>
		/// <param name="tableName">����</param>
		/// <param name="keyFieldName">�����ֶ���</param>
		/// <param name="lookupFieldName">��ѯ�ֶ���</param>
		/// <param name="filter">��������</param>
		/// <returns>���ز�ѯ���</returns>
		public static object LookupObjectProperty(string key,string tableName,string keyFieldName,string lookupFieldName,string filter)
		{
			using(CommonDatabase MyDatabase = new CommonDatabase())
			{
				string SelectSql = "SELECT " + lookupFieldName + " FROM " + tableName 
					+ " WHERE " +  keyFieldName + "='" + key + "'";

				if (filter != "")
					SelectSql += " " + filter;

				return MyDatabase.ExecuteScalar(SelectSql);
			}
		}

		/// <summary>
		/// ����Key��ѯ�ض���ֵ(���ŷָ��б�)
		/// </summary>
		/// <param name="keys">����ֵ</param>
		/// <param name="tableName">����</param>
		/// <param name="keyFieldName">�����ֶ���</param>
		/// <param name="lookupFieldName">��ѯ�ֶ���</param>
		/// <param name="filter">��������</param>
		/// <returns>���ز�ѯ���</returns>
		public static string LookupObjectPropertys(string keys,string tableName,string keyFieldName,string lookupFieldName,string filter)
		{
			if (keys == "")
			{
				return "";
			}

			string MyKeys = "";
			string[] TmpKeys = keys.Split(',');
			for (int i = 0 ; i <= TmpKeys.Length - 1;i++)
				MyKeys = MyKeys + "'" + TmpKeys[i] + "',";

			string Lookups="";
			if (MyKeys.Length > 0)
			{
				MyKeys = MyKeys.Substring(0,MyKeys.Length-1);

				string SelectSql =  "SELECT " + lookupFieldName + " FROM " + tableName 
					+ " WHERE " +  keyFieldName + " IN (" + MyKeys + ")";

				if (filter != "")
					SelectSql += " " + filter;

				using (CommonDatabase MyDatabase = new CommonDatabase())
				{
					DataSet MyDataSet = MyDatabase.ExecuteDataset(SelectSql); 

					foreach (DataRow MyRow in (MyDataSet.Tables[0].Rows))
						Lookups = Lookups + (string)MyRow[lookupFieldName] + ",";
				}

				if (Lookups.Length > 0)
					Lookups = Lookups.Substring(0,Lookups.Length-1);
			}

			return Lookups;
		}

		/// <summary>
		/// ���Ҷ�������
		/// </summary>
		/// <param name="key">��</param>
		/// <param name="tableName">����</param>
		/// <param name="keyFieldName">���ֶ���</param>
		/// <param name="filter">��������</param>
		/// <returns>DataSet</returns>
		public static DataSet LookupObjectPropertys(string key,string tableName,string keyFieldName,string filter)
		{
			using(CommonDatabase MyDatabase = new CommonDatabase())
			{
				string SelectSql = "SELECT * FROM " + tableName 
					+ " WHERE " +  keyFieldName + "='" + key + "'";

				if (filter != "")
					SelectSql += " " + filter;

				return MyDatabase.ExecuteDataset(SelectSql);
			}
		}
	
		/// <summary>
		/// �������Ʋ�ѯ����
		/// </summary>
		/// <param name="objectName">����</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ĵ���</returns>
		public static string GetObjectCode(string objectName,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectName,tableName,nameFieldName,codeFieldName,"");
		}

		/// <summary>
		/// ���ݴ����ѯ����
		/// </summary>
		/// <param name="objectCode">����</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ������</returns>
		public static string GetObjectName(string objectCode,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectCode,tableName,codeFieldName,nameFieldName,"");
		}

		/// <summary>
		/// �������Ʋ�ѯ����(���ŷָ�������б�)
		/// </summary>
		/// <param name="objectNames">���ŷָ�������б�</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ķ��ŷָ�Ĵ����б�</returns>
		public static string GetObjectCodes(string objectNames,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectNames,tableName,nameFieldName,codeFieldName,"");
		}

		/// <summary>
		/// ���ݴ����ѯ����(���ŷָ�Ĵ����б�)
		/// </summary>
		/// <param name="objectCodes">���ŷָ�Ĵ����б�</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ķ��ŷָ�������б�</returns>
		public static string GetObjectNames(string objectCodes,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectCodes,tableName,codeFieldName,nameFieldName,"");
		}

		/// <summary>
		/// �������Ʋ�ѯ����
		/// </summary>
		/// <param name="branchCode">�ֲ�����</param>
		/// <param name="objectName">����</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ĵ���</returns>
		public static string GetObjectCode(string branchCode,string objectName,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectName,tableName,nameFieldName,codeFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// ���ݴ����ѯ����
		/// </summary>
		/// <param name="branchCode">�ֲ�����</param>
		/// <param name="objectCode">����</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ������</returns>
		public static string GetObjectName(string branchCode,string objectCode,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectCode,tableName,codeFieldName,nameFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// �������Ʋ�ѯ����(���ŷָ�������б�)
		/// </summary>
		/// <param name="branchCode">�ֲ�����</param>
		/// <param name="objectNames">���ŷָ�������б�</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ķ��ŷָ�Ĵ����б�</returns>
		public static string GetObjectCodes(string branchCode,string objectNames,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectNames,tableName,nameFieldName,codeFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// ���ݴ����ѯ����(���ŷָ�Ĵ����б�)
		/// </summary>
		/// <param name="branchCode">�ֲ�����</param>
		/// <param name="objectCodes">���ŷָ�Ĵ����б�</param>
		/// <param name="tableName">����</param>
		/// <param name="codeFieldName">�����ֶ���</param>
		/// <param name="nameFieldName">�����ֶ���</param>
		/// <returns>��Ӧ�Ķ��ŷָ�������б�</returns>
		public static string GetObjectNames(string branchCode,string objectCodes,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectCodes,tableName,codeFieldName,nameFieldName,"AND BranchCode='" + branchCode + "'");
		}
		#endregion

		#region DBObjectToString�����ݶ���ת�����ַ���
		/// <summary>
		/// �γ��ַ���
		/// </summary>
		/// <param name="dbObject"></param>
		/// <returns></returns>
		public static string DBObjectToString(Object dbObject)
		{
			if (dbObject == System.DBNull.Value)
				return "null";
			else if ((dbObject is string ) || (dbObject is String))
				return "'" + dbObject.ToString().Replace("'","��") + "'";
			else if (dbObject is DateTime)
				return "'" + ((DateTime)dbObject).ToString("yyyyMMdd") + "'";
			else if ((dbObject is bool) || (dbObject is System.Boolean))
				return ((bool)dbObject)?"1":"0";
			else
				return dbObject.ToString();
		}
		#endregion

		#region GetExecProcSql����ò���SQL���
		/// <summary>
		/// ��ò���SQL��䡣�������磺exec pr_test @param1='aaa',@param2='bbb' ��SQL���
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="ProcName"></param>
		/// <returns></returns>
		public static string GetExecProcSql(DataRow aRow,string ProcName)
		{
			string strColumnName;
			string SelectSql = "exec " + ProcName + " ";
			for (int i = 0 ; i < aRow.Table.Columns.Count ; i++)
			{
				strColumnName=aRow.Table.Columns[i].ColumnName;
				if (i != aRow.Table.Columns.Count - 1)
					SelectSql += "@"+strColumnName+"="+DBObjectToString(aRow[i])+",";
				else
					SelectSql += "@"+strColumnName+"="+DBObjectToString(aRow[i]);
			}
			return SelectSql;
		}
		#endregion

		#region GetInsertSql����ò���SQL���
		/// <summary>
		/// ��ò���SQL���
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public static string GetInsertSql(DataRow aRow,string tableName)
		{
			string SelectSql = "INSERT INTO " + tableName + "( ";
			for (int i = 0 ; i < aRow.Table.Columns.Count ; i++)
			{
				if (i != aRow.Table.Columns.Count - 1)
					SelectSql += aRow.Table.Columns[i].ColumnName + ",";
				else
					SelectSql += aRow.Table.Columns[i].ColumnName + ")";
			}
			SelectSql += " VALUES(";
			for (int i = 0 ; i < aRow.ItemArray.Length ; i++)
			{
				if (i != aRow.ItemArray.Length - 1)
					SelectSql += DBObjectToString(aRow[i]) + ",";
				else
					SelectSql += DBObjectToString(aRow[i]) + ")";
			}

			return SelectSql;
		}

		/// <summary>
		/// ���غ�������ò���SQL���
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="tableName"></param>
		/// <param name="NonColumns">�����������SQL����е���</param>
		/// <returns></returns>
		public static string GetInsertSql(DataRow aRow,string tableName,params string[] NonColumns)
		{
			bool bNonColumn;

			string SelectSql = "INSERT INTO " + tableName + "( ";
			string AllColumns = "";
			string ColumnValues="";

			for (int i = 0 ; i < aRow.Table.Columns.Count ; i++)
			{
				bNonColumn=false;
				foreach(string myColumn in NonColumns)
				{
					if(myColumn==aRow.Table.Columns[i].ColumnName)
					{
						bNonColumn=true;
						break;
					}
				}
				if(bNonColumn)
					continue;

				AllColumns += aRow.Table.Columns[i].ColumnName + ",";
			}
			if(AllColumns=="")
				throw new System.Exception("����GetInsertSql��������NonColumns̫�࣬�޷�����SQL��");
			AllColumns=AllColumns.Substring(0,AllColumns.Length-1) + ")";

			for (int i = 0 ; i < aRow.ItemArray.Length ; i++)
			{
				bNonColumn=false;
				foreach(string myColumn in NonColumns)
				{
					if(myColumn==aRow.Table.Columns[i].ColumnName)
					{
						bNonColumn=true;
						break;
					}
				}
				if(bNonColumn)
					continue;

				ColumnValues += DBObjectToString(aRow[i]) + ",";
			}
			ColumnValues=ColumnValues.Substring(0,ColumnValues.Length-1) + ")";

			SelectSql = SelectSql + AllColumns + " VALUES(" + ColumnValues;
			return SelectSql;
		}
		#endregion

		#region GetUpdateSql����ø���SQL���
		/// <summary>
		/// ��ø���SQL���
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="tableName"></param>
		/// <param name="keyFields"></param>
		/// <returns></returns>
		public static string GetUpdateSql(DataRow aRow,string tableName,params string[] keyFields)
		{
			string UpdateSql = "UPDATE " + tableName + " SET ";
			for (int i = 0 ; i < aRow.Table.Columns.Count ; i++)
			{
				if (i != aRow.Table.Columns.Count - 1)
					UpdateSql += aRow.Table.Columns[i].ColumnName + "=" + DBObjectToString(aRow[i]) + ",";
				else
					UpdateSql += aRow.Table.Columns[i].ColumnName  + "=" + DBObjectToString(aRow[i]) + " ";
			}
			UpdateSql += " WHERE ";
			for (int i = 0 ; i < keyFields.Length ; i++)
			{
				if (i == 0)
					UpdateSql += keyFields[i] + "=" + DBObjectToString(aRow[keyFields[i]]) + " ";
				else
					UpdateSql += "AND " + keyFields[i] + "=" + DBObjectToString(aRow[keyFields[i],DataRowVersion.Original]) + " ";
			}

			return UpdateSql;
		}
		#endregion

		#region GetDeleteSql�����ɾ��Sql
		/// <summary>
		/// ���ɾ��Sql
		/// </summary>
		/// <param name="aRowView"></param>
		/// <param name="tableName"></param>
		/// <param name="keyFields"></param>
		/// <returns></returns>
		public static string GetDeleteSql(DataRowView aRowView,string tableName,params string[] keyFields)
		{
			string DeleteSql = "DELETE FROM " + tableName + " WHERE ";
			for (int i = 0 ; i < keyFields.Length ; i++)
			{
				if (i == 0)
					DeleteSql += keyFields[i] + "=" + DBObjectToString(aRowView[keyFields[i]]) + " ";
				else
					DeleteSql += "AND " + keyFields[i] + "=" + DBObjectToString(aRowView[keyFields[i]]) + " ";
			}

			return DeleteSql;
		}

		/// <summary>
		/// ���ɾ��SQL
		/// </summary>
		/// <param name="aRow">ɾ����</param>
		/// <param name="tableName">����</param>
		/// <param name="keyFields">�ؼ�����</param>
		/// <returns>����ֵ</returns>
		public static string GetDeleteSql(DataRow aRow,string tableName,params string[] keyFields)
		{
			string DeleteSql = "DELETE FROM " + tableName + " WHERE ";
			for (int i = 0 ; i < keyFields.Length ; i++)
			{
				if (i == 0)
					DeleteSql += keyFields[i] + "=" + DBObjectToString(aRow[keyFields[i]]) + " ";
				else
					DeleteSql += "AND " + keyFields[i] + "=" + DBObjectToString(aRow[keyFields[i]]) + " ";
			}

			return DeleteSql;
		}

		#endregion

		#region GetNewObjectCode�����ź���
		/// <summary>
		/// ���ź���
		/// </summary>
		/// <param name="tableName">����</param>
		/// <param name="fieldName">�ֶ���</param>
		/// <returns>����</returns>
		public static string GetNewObjectCode(string tableName,string fieldName)
		{
			string TableName = tableName;
			string FieldName = fieldName;
			string SelectSql = "SELECT * FROM System_FeedNo "
				+ " WHERE TableName='" + TableName  + "'"
				+ " AND FieldName='" + FieldName + "'";

			using (CommonDatabase MyDatabase = new CommonDatabase())
			{
				DataTable MyDataTable = MyDatabase.ExecuteDataset(SelectSql).Tables[0];
				if (MyDataTable.Rows.Count == 0)
					throw new Exception(TableName + "��" + FieldName + "�ֶ�û�ж�Ӧ�ĸ��ſ��¼");
				else if (MyDataTable.Rows.Count > 1)
					throw new Exception(TableName + "��" + FieldName + "�ֶζ�Ӧ�������ſ��¼");
				else
				{
					string Prefix = "";
					if (MyDataTable.Rows[0]["Prefix"] != System.DBNull.Value)
						Prefix = (string)MyDataTable.Rows[0]["Prefix"];

					int Len = 0;
					if (MyDataTable.Rows[0]["Len"] != System.DBNull.Value)
						Len = (int)MyDataTable.Rows[0]["Len"];

					int CurrentValue = 0;
					if (MyDataTable.Rows[0]["CurrentValue"] != System.DBNull.Value)
						CurrentValue = (int)MyDataTable.Rows[0]["CurrentValue"];

					string StrValue = CurrentValue.ToString();
					for (int i = 0 ; i <= Len -CurrentValue.ToString().Length - 1 ; i++)
					{
						StrValue = "0" + StrValue ;
					}

					string UpdateSql = "UPDATE System_FeedNo SET CurrentValue=CurrentValue + 1 "
						+ " WHERE TableName='" + TableName  + "'"
						+ " AND FieldName='" + FieldName + "'";
			    
					MyDatabase.ExecuteNonQuery(UpdateSql);
					
					return (Prefix + StrValue); //DateTime.Now.ToString("yyyyMMdd")
				}
			}
		}
		#endregion
		
		#region CurrencyToUpper������ҽ���Сдת������
		/// <summary>
		/// Сд���ת��Ϊ��д���������������С��һ���ڣ������λС��
		/// </summary>
		/// <param name="d">Դ��d �� 1000000000000.00(һ����)���������λС�� </param>
		/// <returns>�������д���</returns>
		public static string CurrencyToUpper(decimal d)
		{
			if (d == 0)
				return "��Ԫ��";
			
			string je = "";
			if (d>0)
				je = d.ToString("####.00");
			else
				je = System.Math.Abs(d).ToString("####.00");
			if (je.Length > 15)
				return "";
			je = new String('0',15 - je.Length) + je;						//��С��15λ����ǰ�油0

			string stry = je.Substring(0,4);								//ȡ��'��'��Ԫ
			string strw = je.Substring(4,4);								//ȡ��'��'��Ԫ
			string strg = je.Substring(8,4);								//ȡ��'Ԫ'��Ԫ
			string strf = je.Substring(13,2);								//ȡ��С������
		
			string str1 = "",str2 = "",str3 = "";

			str1 = getupper(stry,"��");								//�ڵ�Ԫ�Ĵ�д
			str2 = getupper(strw,"��");								//��Ԫ�Ĵ�д
			str3 = getupper(strg,"Ԫ");								//Ԫ��Ԫ�Ĵ�д


			string str_y = "", str_w = "";									
			if (je[3] == '0' || je[4] == '0')								//�ں���֮���Ƿ���0
				str_y = "��";
			if (je[7] == '0' || je[8] == '0')								//���Ԫ֮���Ƿ���0
				str_w = "��";



			string ret = str1 + str_y + str2 + str_w + str3;				//�ڣ���Ԫ��������д�ϲ�

			for (int i = 0 ;i < ret.Length;i++)								//ȥ��ǰ���"��"			
			{
				if (ret[i] != '��')
				{
					ret = ret.Substring(i);
					break;
				}

			}
			for (int i = ret.Length - 1;i > -1 ;i--)						//ȥ������"��"	
			{
				if (ret[i] != '��')
				{
					ret = ret.Substring(0,i+1);
					break;
				}
			}
			
			if (ret[ret.Length  - 1] != 'Ԫ')								//�����λ����'Ԫ'�����һ��'Ԫ'��
				ret = ret + "Ԫ";

			if (ret == "����Ԫ")											//��Ϊ��Ԫ����ȥ��"Ԫ��"�����ֻҪС������
				ret = "";
			
			if (strf == "00")												//������С�����ֵ�ת��
			{
				ret = ret + "��";
			}
			else
			{
				string tmp = "";
				tmp = getint(strf[0]);
				if (tmp == "��")
					ret = ret + tmp;
				else
					ret = ret + tmp + "��";

				tmp = getint(strf[1]);
				if (tmp == "��")
					ret = ret + "��";
				else
					ret = ret + tmp + "��";
			}

			if (ret[0] == '��')
			{
				ret = ret.Substring(1);										//��ֹ0.03תΪ"������"����ֱ��תΪ"����"
			}

			if (d>0 )
				return  ret;													//��ɣ�����								
			else
				return "�� "+ret;
		}
		/// <summary>
		/// ��һ����ԪתΪ��д�����ڵ�Ԫ����Ԫ������Ԫ
		/// </summary>
		/// <param name="str">�����Ԫ��Сд���֣�4λ���������㣬��ǰ�油�㣩</param>
		/// <param name="strDW">�ڣ���Ԫ</param>
		/// <returns>ת�����</returns>
		private static string getupper(string str,string strDW)
		{
			if (str == "0000")
				return "";

			string ret = "";
			string tmp1 = getint(str[0]) ;
			string tmp2 = getint(str[1]) ;
			string tmp3 = getint(str[2]) ;
			string tmp4 = getint(str[3]) ;
			if (tmp1 != "��")
			{
				ret = ret + tmp1 + "Ǫ";
			}
			else
			{
				ret = ret + tmp1;
			}

			if (tmp2 != "��")
			{
				ret = ret + tmp2 + "��";
			}
			else
			{
				if (tmp1 != "��")											//��֤����������'00'�����ֻ��һ���㣬��ͬ
					ret = ret + tmp2;
			}

			if (tmp3 != "��")
			{
				ret = ret + tmp3 + "ʰ";
			}
			else
			{
				if (tmp2 != "��")
					ret = ret + tmp3;
			}

			if (tmp4 != "��")
			{
				ret = ret + tmp4 ;
			}
			
			if (ret[0] == '��')												//����һ���ַ���'��'����ȥ��
				ret = ret.Substring(1);
			if (ret[ret.Length - 1] == '��')								//�����һ���ַ���'��'����ȥ��
				ret = ret.Substring(0,ret.Length - 1);

			return ret + strDW;												//���ϱ���Ԫ�ĵ�λ
			
		}
		
		/// <summary>
		/// ��������תΪ��д
		/// </summary>
		/// <param name="c">Сд���������� 0---9</param>
		/// <returns>��д����</returns>
		private static string getint(char c)
		{
			string str = "";
			switch ( c )
			{
				case '0':
					str = "��";
					break;
				case '1':
					str = "Ҽ";
					break;
				case '2':
					str = "��";
					break;
				case '3':
					str = "��";
					break;
				case '4':
					str = "��";
					break;
				case '5':
					str = "��";
					break;
				case '6':
					str = "½";
					break;
				case '7':
					str = "��";
					break;
				case '8':
					str = "��";
					break;
				case '9':
					str = "��";
					break;
			}
			return str;
		}
		#endregion

		#region IsValidEmail���ж��ʼ���ʽ�Ƿ���ȷ
		/// <summary>
		/// �ж��ʼ���ʽ�Ƿ���ȷ
		/// </summary>
		/// <param name="email">�����ַ</param>
		/// <returns>�����ַ�����ȷ����ô�����棬���򷵻ؼ�</returns>
		/// <example>
		/// <code>
		/// string MyEMail="aaa@bbb.com"
		/// if( IsValidEmail(MyEMail) )
		///		Response.Write("����һ����ȷ�������ַ");
		///	else
		///		Response.Write("����ȷ�������ַ��");
		/// </code>
		/// </example>
		public static bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
		}
		#endregion

		#region IsValidInt���ж�һ���ַ����Ƿ�������
		/// <summary>
		/// �ж�һ���ַ����Ƿ�������
		/// </summary>
		/// <param name="strInt">Ҫ�жϵ�ֵ</param>
		/// <returns>�����ȷ����ô�����棬���򷵻ؼ�</returns>
		/// <example>
		/// <code>
		/// string strValue="111"
		/// if( IsValidInt(strValue) )
		///		Response.Write("����һ����ȷ������");
		///	else
		///		Response.Write("����ȷ��������");
		/// </code>
		/// </example>
		public static bool IsValidInt(string strInt)
		{
			bool bReturn=true;
			try
			{
                Int64.Parse(strInt);
			}
			catch
			{
				bReturn=false;
			}
			return bReturn;
		}
		#endregion

		#region IsValidFloat���ж�һ���ַ����Ƿ��Ǹ�������
		/// <summary>
		/// �ж�һ���ַ����Ƿ��Ǹ�������
		/// </summary>
		/// <param name="strValue">Ҫ�жϵ�ֵ</param>
		/// <returns>�����ȷ����ô�����棬���򷵻ؼ�</returns>
		/// <example>
		/// <code>
		/// string strValue="111"
		/// if( IsValidFloat(strValue) )
		///		Response.Write("����һ����ȷ�ĸ�����");
		///	else
		///		Response.Write("����ȷ�ĸ�������");
		/// </code>
		/// </example>
		public static bool IsValidFloat(string strValue)
		{
			bool bReturn=true;
			try
			{
				float.Parse(strValue);
			}
			catch
			{
				bReturn=false;
			}
			return bReturn;
		}
		#endregion

		#region DatasetToXML�������ݼ�����XML�ļ�
		/// <summary>
		/// �����ݼ�����XML�ļ�
		/// </summary>
		/// <param name="ds">���ݼ�</param>
		/// <param name="FullFileName">XML�ļ���</param>
		public static string DatasetToXML(DataSet ds,string FullFileName)
		{
			string strXML,strSchema,strRsData,strTmp;
			strXML=@"<xml encoding='utf-8' xmlns:s='uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882'
						xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882'
						xmlns:rs='urn:schemas-microsoft-com:rowset'
						xmlns:z='#RowsetSchema'>";

			//��ȡSchema
			strSchema=@"
					<s:Schema id='RowsetSchema'>
						<s:ElementType name='row'>";
			strTmp="";
			foreach(DataColumn dc in ds.Tables[0].Columns)
			{
				if(dc.DataType == System.Type.GetType("System.String"))
					strTmp +=string.Format(@"
							<s:AttributeType name='{0}' dt:maxLength='{1}' />",dc.ColumnName,dc.MaxLength);
				else
					strTmp +=string.Format(@"
							<s:AttributeType name='{0}' dt:maxLength='30' />",dc.ColumnName);
			}
			strSchema +=strTmp;
			strSchema +=@"
						</s:ElementType>
					</s:Schema>";

			//��ȡrs:data
			strRsData=@"
					<rs:data>";
			strTmp="";
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				strTmp +=@"
					<z:row ";
				foreach(DataColumn dc in ds.Tables[0].Columns)
				{
					string strValue = dr[dc].ToString().Replace("'","��").Replace("/","��").Replace("(","��").Replace(")","��").Replace("<","��").Replace("&","�}");
					strTmp += string.Format(@"{0}='{1}' ",dc.ColumnName,strValue);
				}
				strTmp +=@"/>";
			}
			strRsData +=strTmp;
			strRsData +=@"
					</rs:data>";

			strXML+=strSchema;
			strXML+=strRsData;
			strXML+=@"
					</xml>";

			if(FullFileName!="")
			{
				System.IO.FileStream xmlFile = new System.IO.FileStream(FullFileName,FileMode.Create, FileAccess.ReadWrite);
				StreamWriter sw = new StreamWriter(xmlFile);
				sw.BaseStream.Seek(0, SeekOrigin.End);
				sw.Write(strXML);
				sw.Flush();
				sw.Close();
				xmlFile.Close();
			}
			return strXML;
		}
		#endregion

		#region  ReturnDataGridXML����DataGrid�е���������XML�ַ���
		/// <summary>
		/// ��DataGrid�е���������XML�ַ���
		/// </summary>
		/// <param name="dg">DataGrid����</param>
		/// <param name="dgTitle">��������ɵ��ַ�����ÿ������֮���ö��ŷָ�</param>
		/// <param name="IsAlignRightColumns">��Щ��Ҫ�Ҷ���</param>
		/// <remarks>
		/// <code>
		/// ��ע��
		/// 1.�����У���������Ϊ�յ��ж������ԡ�
		/// 2.AutoGenerateColumnsΪtrue��ʱ���޷�ֱ�ӻ�ȡ������������Ҫ����dgTitle�����������ַ�������
		/// 3.���dgTitle������Ϊ�գ���ôdgTitleһ��Ҫ��DataGrid�е��б���һ�¡�
		/// </code>
		/// </remarks>
		public static string ReturnDataGridXML(System.Web.UI.WebControls.DataGrid dg,string dgTitle,out string IsAlignRightColumns)
		{
			string[] ColumnName;

			//------------------��ʼ������------------------
			if(dgTitle=="")
			{
				ColumnName = new string[dg.Columns.Count];
				for(int i=0;i<dg.Columns.Count;i++)
				{	
					//���������У�������һ���Ǿ�̬�У����Կ���ͨ��Columns���������ʡ�
					if(!dg.Columns[i].Visible)
					{
						ColumnName[i]="";
						continue;
					}
			
					//��������е�XML�﷨��������ַ�
					ColumnName[i]=dg.Columns[i].HeaderText.Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
				}
			}
			else
			{
				ColumnName=dgTitle.Split(',');
				for(int i=0;i<ColumnName.Length;i++)
				{	
					//���������У�������һ���Ǿ�̬�У����Կ���ͨ��Columns���������ʡ�
					if(dg.Columns.Count>i && !dg.Columns[i].Visible)
					{
						ColumnName[i]="";
						continue;
					}
			
					//��������е�XML�﷨��������ַ�
					ColumnName[i]=ColumnName[i].Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
				}
			}

			//------------------��ʼ���п�------------------
			int[] ColumnWidth=new int[ColumnName.Length];
			for(int i=0;i<ColumnName.Length;i++)
			{	
				ColumnWidth[i]=ColumnName[i].Length;
			}
			
			//------------------����XMLͷ------------------
			string strXML,strSchema,strRsData,strTmp,strFooterText;
			string strValue;
			strXML=@"<xml encoding='utf-8' xmlns:s='uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882'
						xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882'
						xmlns:rs='urn:schemas-microsoft-com:rowset'
						xmlns:z='#RowsetSchema'>";

			//------------------��ȡrs:data------------------
			//����¼
			strRsData=@"
					<rs:data>";
			strTmp="";
			foreach(System.Web.UI.WebControls.DataGridItem dgItem in dg.Items)
			{
				strTmp +=@"
					<z:row ";
				for(int i=0;i<dgItem.Cells.Count;i++)
				{	
					//��������Ϊ�յ��У����������С�
					if(ColumnName[i]=="")
						continue;
                    
					//��ȡ��ǰ��ֵ��ͬʱҪ�����ֵ��XML��������ַ������磺�����š�������ŵ�
					if(dgItem.Cells[i].Text!="&nbsp;" && dgItem.Cells[i].Text!="")
					{
						strValue=dgItem.Cells[i].Text.Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
						strTmp += string.Format(@"{0}='{1}' ",ColumnName[i],strValue);
					
						//��ȡ�п�������80�����ڡ��������80�����������г��Ⱦ���Ϊ-1����Memo�ֶΣ�
						if(ColumnWidth[i]!=-1)
						{
							if(strValue.Length>ColumnWidth[i] && strValue.Length<=80) 
								ColumnWidth[i]=strValue.Length;
							else if(strValue.Length>80)
								ColumnWidth[i]=-1;
						}
					}
				}
				strTmp +=@"/>";
			}

			//���ҳ��
			IsAlignRightColumns="";
			strFooterText="";
			for(int i=0;i<dg.Columns.Count;i++)
			{	
				//���������У�������һ���Ǿ�̬�У����Կ���ͨ��Columns���������ʡ�
				if(ColumnName[i]=="")
					continue;

				if(dg.Columns[i].FooterText!="&nbsp;" && dg.Columns[i].FooterText!="")
				{
					strValue=dg.Columns[i].FooterText.Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
					strFooterText += string.Format(@"{0}='{1}' ",ColumnName[i],strValue);

					if(ColumnWidth[i]!=-1)
					{
						if(strValue.Length>ColumnWidth[i] && strValue.Length<=80) 
							ColumnWidth[i]=strValue.Length;
						else if(strValue.Length>80)
							ColumnWidth[i]=-1;
					}
				}

				if(dg.Columns[i].ItemStyle.HorizontalAlign==System.Web.UI.WebControls.HorizontalAlign.Right)
					IsAlignRightColumns +=i.ToString()+",";
			}
			if(strFooterText!="")
			{
				strFooterText =@"
					<z:row "+strFooterText+@"/>";
			}

			strRsData +=strTmp;
			strRsData +=strFooterText;
			strRsData +=@"
					</rs:data>";

			//------------------��ȡSchema------------------
			strSchema=@"
					<s:Schema id='RowsetSchema'>
						<s:ElementType name='row'>";
			strTmp="";
			for(int i=0;i<ColumnName.Length;i++)
			{	
				//��������Ϊ�յ��У����������С�
				if(ColumnName[i]=="")
					continue;

				strTmp +=string.Format(@"
						<s:AttributeType name='{0}' dt:maxLength='{1}' />",ColumnName[i],ColumnWidth[i]);
			}
			strSchema +=strTmp;
			strSchema +=@"
						</s:ElementType>
					</s:Schema>";

			//------------------���������XML�ַ���------------------
			strXML+=strSchema;
			strXML+=strRsData;
			strXML+=@"
					</xml>";

			return strXML;
		}
		
		
		/// <summary>
		/// ���غ�������DataGrid�е���������XML�ַ�����DataGrid�е��ж�Ϊ��̬�С�
		/// </summary>
		/// <param name="dg"></param>
		/// <param name="IsAlignRightColumns">��Щ��Ҫ�Ҷ���</param>
		/// <returns></returns>
		public static string ReturnDataGridXML(System.Web.UI.WebControls.DataGrid dg,out string IsAlignRightColumns)
		{
			return ReturnDataGridXML(dg,"",out IsAlignRightColumns);
		}
		#endregion

		#region �����ֶδ�У��ϵ�з���...
		/// <summary>
		///����ַ������� 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetStrLength(string str)
		{
			byte[] sarr = System.Text.Encoding.Default.GetBytes(str);
			return sarr.Length;
		}

		/// <summary>
		///����ָ�������ַ����� 
		/// </summary>
		/// <param name="str">�ַ���</param>
		/// <param name="len">����</param>
		/// <returns>ָ�������ַ���</returns>
		/// <remarks>
		/// ���ı��༭��TextBox�ؼ����У�һ�����ĺ��ֺ�һ��Ӣ����ĸһ������ռһλ�ֽڣ�
		/// ������SQL Server���ݿ��У��洢һ�����ĺ�����ռ����λ�ֽڣ������Ϳ��ܻᵼ��
		/// �ڱ����ı��༭�������ʱ�ֽڱ��ضϣ�����������Ծ���Ҫ����ָ�����ȵĵ�ǰ ANSI ����ҳ�ı����ַ���
		/// <code>
		/// string strTmp = txtMemo.Text;
		/// strTmp=Teamax.Common.PublicClass.GetStrEncoding(strTmp,txtMemo.MaxLength);
		/// txtMemo.Text=strTmp;
		/// </code>
		/// </remarks>
		public static string GetStrEncoding(string str,int len)
		{
			byte[] sarr = System.Text.Encoding.Default.GetBytes(str);
			if(sarr.Length>len && len>0)
				return System.Text.Encoding.Default.GetString(sarr,0,len);
			else
				return str;
		}
		
		/// <summary>
		/// //�ж����֤
		/// </summary>
		/// <param name="Cert"></param>
		/// <returns></returns>
		public static bool IsValidCert(string Cert)
		{
			return Regex.IsMatch(Cert, @"^(\d{15}|\d{17}[\dxX])$"); 
		}


		/// <summary>
		///�ж��û���
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public static bool IsValidUserName(string UserName)
		{ 
			return Regex.IsMatch(UserName, @"^[a-zA-Z0-9\-]+$"); 
		}

		
		/// <summary>
		///�ж�Ӣ���� 
		/// </summary>
		/// <param name="EnPersonName"></param>
		/// <returns></returns>
		public static bool IsValidEnPersonName(string EnPersonName)
		{
			return Regex.IsMatch(EnPersonName, @"^[a-zA-Z]{1,30}$"); 
		}

		/// <summary>
		/// �ж�����
		/// </summary>
		/// <param name="PassWord"></param>
		/// <returns></returns>
		public static bool IsValidPassWord(string PassWord)
		{
			return Regex.IsMatch(PassWord, @"^(\w){6,20}$"); 
		}

		/// <summary>
		/// �жϵ绰/����
		/// </summary>
		/// <param name="Tel"></param>
		/// <returns></returns>
		public static bool IsValidTel(string Tel)
		{
			return Regex.IsMatch(Tel, @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$"); 
		}

		/// <summary>
		/// �ж��ֻ�
		/// </summary>
		/// <param name="Mobil"></param>
		/// <returns></returns>
		public static bool IsValidMobil(string Mobil)
		{
            return Regex.IsMatch(Mobil, @"^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$"); 
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="Zip"></param>
		/// <returns></returns>
		public static bool IsValidZip(string Zip)
		{
			return Regex.IsMatch(Zip, @"^[a-zA-Z0-9]{3,12}$"); 
		}

		/// <summary>
		/// �ж�����
		/// </summary>
		/// <param name="Date"></param>
		/// <returns></returns>
		public static bool IsValidDate(string Date)
		{
			bool bValid=Regex.IsMatch(Date, @"^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"); 
			return (bValid && Date.CompareTo("1753-01-01")>=0);

		}
		
		/// <summary>
		/// �ж�ֻ������ĸ
		/// </summary>
		/// <param name="EnName"></param>
		/// <returns></returns>
		public static bool IsValidEnName(string EnName)
		{
			return Regex.IsMatch(EnName, @"[a-zA-Z]"); 
		}
		#endregion

		#region DateCompare����������������ؼ��е�ֵ���д���
		/// <summary>
		/// ��������������ؼ��е�ֵ���д�������ǺϷ������ڸ�ʽ����ô������С��ֵ���ڵ�һ��������У����ڴ��ֵ���ڵڶ���������С�
		/// </summary>
        /// <param name="txtDateFrom">HTML����ؼ�������1</param>
        /// <param name="txtDateTo">HTML����ؼ�������2</param>
		/// <returns>����ǺϷ������ڸ�ʽ����ô�����棻���򷵻ؼ�</returns>
		/// <remarks>��������������ؼ�����HTML����ؼ�</remarks>
		/// <example>
		/// <code>
		/// ʾ��1����ĳ��ҳ��Ĳ�ѯ�����д��ڡ����ڴ�...��...��
		/// string BeginDateFrom,BeginDateTo;
		///	if(Teamax.Common.PublicClass.DateCompare(txtBeginDateFrom,txtBeginDateTo))
		///	{
		///		BeginDateFrom=txtBeginDateFrom.Value;	
		///		BeginDateTo=txtBeginDateTo.Value;
		///	}
		///	else
		///	{
		///		((Teamax.Common.CommonPage)Page).MessageBox("��ѯ�����еġ����ڡ�����ȷ�����������룡");
		///		return ;				
		///	}
		///	</code>
		/// </example>
		public static bool DateCompare(System.Web.UI.HtmlControls.HtmlInputControl txtDateFrom,System.Web.UI.HtmlControls.HtmlInputControl txtDateTo)
		{
            return DateCompare(false, txtDateFrom, txtDateTo);
		}

        /// <summary>
        /// ��������������ؼ��е�ֵ���д�������ǺϷ������ڸ�ʽ����ô������С��ֵ���ڵ�һ��������У����ڴ��ֵ���ڵڶ���������С�
        /// </summary>
        /// <param name="IsWithTime">�Ƿ��ʱ�䲿�� Сʱ���֣���</param>
        /// <param name="txtDateFrom">HTML����ؼ�������1</param>
        /// <param name="txtDateTo">HTML����ؼ�������2</param>
        /// <returns>����ǺϷ������ڸ�ʽ����ô�����棻���򷵻ؼ�</returns>
        /// <remarks>��������������ؼ�����HTML����ؼ�</remarks>
        /// <example>
        /// <code>
        /// ʾ��1����ĳ��ҳ��Ĳ�ѯ�����д��ڡ����ڴ�...��...��
        /// string BeginDateFrom,BeginDateTo;
        ///	if(Teamax.Common.PublicClass.DateCompare(true,txtBeginDateFrom,txtBeginDateTo))
        ///	{
        ///		BeginDateFrom=txtBeginDateFrom.Value;	
        ///		BeginDateTo=txtBeginDateTo.Value;
        ///	}
        ///	else
        ///	{
        ///		((Teamax.Common.CommonPage)Page).MessageBox("��ѯ�����еġ����ڡ�����ȷ�����������룡");
        ///		return ;				
        ///	}
        ///	</code>
        /// </example>
        public static bool DateCompare(bool IsWithTime, System.Web.UI.HtmlControls.HtmlInputControl txtDateFrom, System.Web.UI.HtmlControls.HtmlInputControl txtDateTo)
        {
            string strFormat = IsWithTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd";
            try
            {
                CultureInfo culture = new CultureInfo("zh-CN");
                string strSendDateFrom = txtDateFrom.Value.Trim();
                string strSendDateTo = txtDateTo.Value.Trim();
                string strTmp;
                if (strSendDateFrom != "")
                {
                    txtDateFrom.Value = Convert.ToDateTime(strSendDateFrom, culture).ToString(strFormat);
                    //txtDateFrom.Value = System.DateTime.Parse(strSendDateFrom).ToString(strFormat);
                    if (txtDateFrom.Value.CompareTo("1753-01-01") < 0)
                        txtDateFrom.Value = "2" + txtDateFrom.Value.Substring(1);
                }

                if (strSendDateTo != "")
                {
                    txtDateTo.Value = Convert.ToDateTime(strSendDateTo, culture).ToString(strFormat);
                    //txtDateTo.Value = System.DateTime.Parse(strSendDateTo).ToString(strFormat);
                    if (txtDateTo.Value.CompareTo("1753-01-01") < 0)
                        txtDateTo.Value = "2" + txtDateTo.Value.Substring(1);
                }

                if (strSendDateFrom != "" && strSendDateTo != "")
                {
                    if (txtDateFrom.Value.CompareTo(txtDateTo.Value) > 0)
                    {
                        strTmp = txtDateFrom.Value;
                        txtDateFrom.Value = txtDateTo.Value;
                        txtDateTo.Value = strTmp;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

		/// <summary>
		/// ���ط�����������TextBox�ı���ؼ���ֵ���д�������ǺϷ������ڸ�ʽ����ô������С��ֵ���ڵ�һ��������У����ڴ��ֵ���ڵڶ���������С�
		/// </summary>
        /// <param name="txtDateFrom">TextBox����ؼ�������1</param>
        /// <param name="txtDateTo">TextBox����ؼ�������2</param>
		/// <returns>����ǺϷ������ڸ�ʽ����ô�����棻���򷵻ؼ�</returns>
		/// <remarks>��������������ؼ�����TextBox�ı���ؼ�</remarks>
		/// <example>
		/// <code>
		/// ʾ��1����ĳ��ҳ��Ĳ�ѯ�����д��ڡ����ڴ�...��...��
		/// string BeginDateFrom,BeginDateTo;
		///	if(Teamax.Common.PublicClass.DateCompare(txtBeginDateFrom,txtBeginDateTo))
		///	{
		///		BeginDateFrom=txtBeginDateFrom.Value;	
		///		BeginDateTo=txtBeginDateTo.Value;
		///	}
		///	else
		///	{
		///		((Teamax.Common.CommonPage)Page).MessageBox("��ѯ�����еġ����ڡ�����ȷ�����������룡");
		///		return ;				
		///	}
		///	</code>
		/// </example>
		public static bool DateCompare(System.Web.UI.WebControls.TextBox txtDateFrom,System.Web.UI.WebControls.TextBox txtDateTo)
		{
            return DateCompare(false, txtDateFrom, txtDateTo);
		}

        /// <summary>
        /// ���ط�����������TextBox�ı���ؼ���ֵ���д�������ǺϷ������ڸ�ʽ����ô������С��ֵ���ڵ�һ��������У����ڴ��ֵ���ڵڶ���������С�
        /// </summary>
        /// <param name="IsWithTime">�Ƿ��ʱ�䲿�� Сʱ���֣���</param>
        /// <param name="txtDateFrom">TextBox����ؼ�������1</param>
        /// <param name="txtDateTo">TextBox����ؼ�������2</param>
        /// <returns>����ǺϷ������ڸ�ʽ����ô�����棻���򷵻ؼ�</returns>
        /// <remarks>��������������ؼ�����TextBox�ı���ؼ�</remarks>
        /// <example>
        /// <code>
        /// ʾ��1����ĳ��ҳ��Ĳ�ѯ�����д��ڡ����ڴ�...��...��
        /// string BeginDateFrom,BeginDateTo;
        ///	if(Teamax.Common.PublicClass.DateCompare(true,txtBeginDateFrom,txtBeginDateTo))
        ///	{
        ///		BeginDateFrom=txtBeginDateFrom.Value;	
        ///		BeginDateTo=txtBeginDateTo.Value;
        ///	}
        ///	else
        ///	{
        ///		((Teamax.Common.CommonPage)Page).MessageBox("��ѯ�����еġ����ڡ�����ȷ�����������룡");
        ///		return ;				
        ///	}
        ///	</code>
        /// </example>
        public static bool DateCompare(bool IsWithTime, System.Web.UI.WebControls.TextBox txtDateFrom, System.Web.UI.WebControls.TextBox txtDateTo)
        {
            string strFormat = IsWithTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd";
            try
            {
                CultureInfo culture = new CultureInfo("zh-CN");
                string strSendDateFrom = txtDateFrom.Text.Trim();
                string strSendDateTo = txtDateTo.Text.Trim();
                string strTmp;
                if (strSendDateFrom != "")
                {
                    txtDateFrom.Text = Convert.ToDateTime(strSendDateFrom, culture).ToString(strFormat);
                    //txtDateFrom.Text = System.DateTime.Parse(strSendDateFrom).ToString(strFormat);
                    if (txtDateFrom.Text.CompareTo("1753-01-01") < 0)
                        txtDateFrom.Text = "2" + txtDateFrom.Text.Substring(1);
                }

                if (strSendDateTo != "")
                {
                    txtDateTo.Text = Convert.ToDateTime(strSendDateTo, culture).ToString(strFormat);
                    //txtDateTo.Text = System.DateTime.Parse(strSendDateTo).ToString(strFormat);
                    if (txtDateTo.Text.CompareTo("1753-01-01") < 0)
                        txtDateTo.Text = "2" + txtDateTo.Text.Substring(1);
                }

                if (strSendDateFrom != "" && strSendDateTo != "")
                {
                    if (txtDateFrom.Text.CompareTo(txtDateTo.Text) > 0)
                    {
                        strTmp = txtDateFrom.Text;
                        txtDateFrom.Text = txtDateTo.Text;
                        txtDateTo.Text = strTmp;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
		#endregion		
	}
}
