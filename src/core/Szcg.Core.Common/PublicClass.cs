/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：数据库访问类。对ADO.NET进行简单封装，方便使用。
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
	/// 公共类。
	/// PublicClass提供一系列常用的静态方法。
	/// </summary>
	/// <example>
	/// 
	/// </example>
	/// <remarks>
	/// </remarks>
	public class PublicClass
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public PublicClass()
		{
			// 
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		/// <summary>
		/// 枚举类型：功能按钮的权限类型
		/// </summary>
		public enum ButtonPowerType
		{
			/// <summary>
			/// 浏览权限
			/// </summary>
			BrowsePower,
			/// <summary>
			/// 操作权限
			/// </summary>
			OperatorPower,
			/// <summary>
			/// 打印权限
			/// </summary>
			PrintPower			
		}

		#region GetMenuCode：获取当前页面的菜单代码
		/// <summary>
		/// 获取当前页面的菜单代码
		/// </summary>
		/// <param name="URL">页面地址</param>
		/// <returns>返回页面所对应的菜单代码</returns>
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

		#region GetOperateName：返回当前操作员的中文名称
		/// <summary>
		/// 返回当前操作员的中文名称
		/// </summary>
		/// <param name="sender">当前页面Page</param>
		/// 		/// <returns>返回当前操作员的中文名称</returns>
		/// <example>
		///		示例：获取当前页面的操作员的中文名称
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

		#region GetButtonPower：得到按钮的权限
		/// <summary>
		/// 得到按钮的权限
		/// </summary>
		/// <param name="conn">数据库连接字符串</param>
		/// <param name="BranchCode">分支机构代码</param>
		/// <param name="OperateID">操作员代码</param>
		/// <param name="MenuCode">菜单代码</param>
		/// <param name="btnPowerType">按钮所属的权限类型
		/// <seealso cref="ButtonPowerType"/>
		/// </param>
		/// <returns>返回：真、假值</returns>
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
		/// 重载方法：得到按钮的权限
		/// </summary>
		/// <param name="page"></param>
		/// <param name="btnPowerType"></param>
		/// <returns>返回：真、假值</returns>
		/// <example>
		/// <code>
		/// 示例：判断是否有操作权限
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

		#region SetButtonPower：根据当前操作员的权限来设定功能按钮是否对其可操作
		/// <summary>
		/// 根据当前操作员的权限来设定功能按钮是否对其可操作
		/// </summary>
		/// <param name="conn">数据库连接字符串</param>
		/// <param name="BranchCode">分支机构代码</param>
		/// <param name="OperateID">操作员代码</param>
		/// <param name="MenuCode">菜单代码</param>
		/// <param name="sender">按钮</param>
		/// <param name="btnPowerType">按钮所属的权限类型
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
		/// 重载函数：根据当前操作员的权限来设定功能按钮是否对其可操作
		/// </summary>
		/// <param name="page">当前页面</param>
		/// <param name="button">页面上的某个按钮</param>
		/// <param name="btnPowerType">按钮所属的权限类型</param>
		/// <example>
		///		示例：设置某个页面上的打印功能按钮Button1对当前操作员是否可操作
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
		/// 重载函数：根据当前操作员的权限来设定功能按钮是否对其可操作，在此重载函数中
		///		功能按钮的权限类型默认是操作类型（ButtonPowerType.OperatorPower）
		/// </summary>
		/// <param name="page">当前页面</param>
		/// <param name="button">页面上的某个按钮</param>
		/// <example>
		///		示例：设置某个页面上的操作功能按钮Button1对当前操作员是否可操作
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
		/// 重载函数：根据当前操作员的权限来设定功能按钮是否对其可操作。默认这些功能按钮是操作类型。
		/// </summary>
		/// <param name="page">当前页面</param>
		/// <param name="button">页面上的一系列按钮，可以是1个或多个按钮</param>
		/// <example>
		///		示例：设置某个页面上的Button1,Button2,Button3三个操作功能按钮对当前操作员是否可操作
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

		#region GetSystemParameter：获取系统参数值
		/// <summary>
		/// 获取系统参数值
		/// </summary>
		/// <param name="conn">数据库连接字符串</param>
		/// <param name="BranchCode">分支机构代码</param>
		/// <param name="ParameterEnName">参数代码</param>
		/// <returns>返回参数值</returns>
		/// <example>
		///		示例：获取系统参数计量单位"DW"的值
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
					throw new Exception("获取系统参数值出错!<br>" + e.Message); 
				} //try ... catch				
			} //using(myCommand)
			return strResult;
		}

		/// <summary>
		/// 重载函数：获取系统参数值。无分支机构代码
		/// </summary>
		/// <param name="conn">数据库连接字符串</param>
		/// <param name="ParameterEnName">参数代码</param>
		/// <returns>返回参数值</returns>
		/// <example>
		///		示例：获取系统参数计量单位"DW"的值
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

		#region LookupObjectProperty系列方法：根据Key查询特定的值
		/// <summary>
		/// 根据Key查询特定的值
		/// </summary>
		/// <param name="key">主键值</param>
		/// <param name="tableName">表名</param>
		/// <param name="keyFieldName">主键字段名</param>
		/// <param name="lookupFieldName">查询字段名</param>
		/// <param name="filter">过滤条件</param>
		/// <returns>返回查询结果</returns>
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
		/// 根据Key查询特定的值(逗号分割列表)
		/// </summary>
		/// <param name="keys">主键值</param>
		/// <param name="tableName">表名</param>
		/// <param name="keyFieldName">主键字段名</param>
		/// <param name="lookupFieldName">查询字段名</param>
		/// <param name="filter">过滤条件</param>
		/// <returns>返回查询结果</returns>
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
		/// 查找对象属性
		/// </summary>
		/// <param name="key">健</param>
		/// <param name="tableName">表名</param>
		/// <param name="keyFieldName">健字段名</param>
		/// <param name="filter">过滤条件</param>
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
		/// 根据名称查询代码
		/// </summary>
		/// <param name="objectName">名称</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的代码</returns>
		public static string GetObjectCode(string objectName,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectName,tableName,nameFieldName,codeFieldName,"");
		}

		/// <summary>
		/// 根据代码查询名称
		/// </summary>
		/// <param name="objectCode">代码</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的名称</returns>
		public static string GetObjectName(string objectCode,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectCode,tableName,codeFieldName,nameFieldName,"");
		}

		/// <summary>
		/// 根据名称查询代码(逗号分割的名称列表)
		/// </summary>
		/// <param name="objectNames">逗号分割的名称列表</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的逗号分割的代码列表</returns>
		public static string GetObjectCodes(string objectNames,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectNames,tableName,nameFieldName,codeFieldName,"");
		}

		/// <summary>
		/// 根据代码查询名称(逗号分割的代码列表)
		/// </summary>
		/// <param name="objectCodes">逗号分割的代码列表</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的逗号分割的名称列表</returns>
		public static string GetObjectNames(string objectCodes,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectCodes,tableName,codeFieldName,nameFieldName,"");
		}

		/// <summary>
		/// 根据名称查询代码
		/// </summary>
		/// <param name="branchCode">分部代码</param>
		/// <param name="objectName">名称</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的代码</returns>
		public static string GetObjectCode(string branchCode,string objectName,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectName,tableName,nameFieldName,codeFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// 根据代码查询名称
		/// </summary>
		/// <param name="branchCode">分部代码</param>
		/// <param name="objectCode">代码</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的名称</returns>
		public static string GetObjectName(string branchCode,string objectCode,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectProperty(objectCode,tableName,codeFieldName,nameFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// 根据名称查询代码(逗号分割的名称列表)
		/// </summary>
		/// <param name="branchCode">分部代码</param>
		/// <param name="objectNames">逗号分割的名称列表</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的逗号分割的代码列表</returns>
		public static string GetObjectCodes(string branchCode,string objectNames,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectNames,tableName,nameFieldName,codeFieldName,"AND BranchCode='" + branchCode + "'");
		}

		/// <summary>
		/// 根据代码查询名称(逗号分割的代码列表)
		/// </summary>
		/// <param name="branchCode">分部代码</param>
		/// <param name="objectCodes">逗号分割的代码列表</param>
		/// <param name="tableName">表名</param>
		/// <param name="codeFieldName">代码字段名</param>
		/// <param name="nameFieldName">名称字段名</param>
		/// <returns>对应的逗号分割的名称列表</returns>
		public static string GetObjectNames(string branchCode,string objectCodes,string tableName,string codeFieldName,string nameFieldName)
		{
			return (string)LookupObjectPropertys(objectCodes,tableName,codeFieldName,nameFieldName,"AND BranchCode='" + branchCode + "'");
		}
		#endregion

		#region DBObjectToString：数据对象转换成字符串
		/// <summary>
		/// 形成字符串
		/// </summary>
		/// <param name="dbObject"></param>
		/// <returns></returns>
		public static string DBObjectToString(Object dbObject)
		{
			if (dbObject == System.DBNull.Value)
				return "null";
			else if ((dbObject is string ) || (dbObject is String))
				return "'" + dbObject.ToString().Replace("'","’") + "'";
			else if (dbObject is DateTime)
				return "'" + ((DateTime)dbObject).ToString("yyyyMMdd") + "'";
			else if ((dbObject is bool) || (dbObject is System.Boolean))
				return ((bool)dbObject)?"1":"0";
			else
				return dbObject.ToString();
		}
		#endregion

		#region GetExecProcSql：获得插入SQL语句
		/// <summary>
		/// 获得插入SQL语句。生成形如：exec pr_test @param1='aaa',@param2='bbb' 的SQL语句
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

		#region GetInsertSql：获得插入SQL语句
		/// <summary>
		/// 获得插入SQL语句
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
		/// 重载函数。获得插入SQL语句
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="tableName"></param>
		/// <param name="NonColumns">不允许包含在SQL语句中的列</param>
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
				throw new System.Exception("调用GetInsertSql方法出错，NonColumns太多，无法生成SQL！");
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

		#region GetUpdateSql：获得更新SQL语句
		/// <summary>
		/// 获得更新SQL语句
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

		#region GetDeleteSql：获得删除Sql
		/// <summary>
		/// 获得删除Sql
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
		/// 获得删除SQL
		/// </summary>
		/// <param name="aRow">删除行</param>
		/// <param name="tableName">表名</param>
		/// <param name="keyFields">关键字名</param>
		/// <returns>返回值</returns>
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

		#region GetNewObjectCode：给号函数
		/// <summary>
		/// 给号函数
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="fieldName">字段名</param>
		/// <returns>号码</returns>
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
					throw new Exception(TableName + "表" + FieldName + "字段没有对应的给号库记录");
				else if (MyDataTable.Rows.Count > 1)
					throw new Exception(TableName + "表" + FieldName + "字段对应对条给号库记录");
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
		
		#region CurrencyToUpper：人民币金额大小写转化函数
		/// <summary>
		/// 小写金额转换为大写金额，其他条件：金额小于一万亿，最多两位小数
		/// </summary>
		/// <param name="d">源金额，d 《 1000000000000.00(一万亿)，且最多两位小数 </param>
		/// <returns>结果，大写金额</returns>
		public static string CurrencyToUpper(decimal d)
		{
			if (d == 0)
				return "零元整";
			
			string je = "";
			if (d>0)
				je = d.ToString("####.00");
			else
				je = System.Math.Abs(d).ToString("####.00");
			if (je.Length > 15)
				return "";
			je = new String('0',15 - je.Length) + je;						//若小于15位长，前面补0

			string stry = je.Substring(0,4);								//取得'亿'单元
			string strw = je.Substring(4,4);								//取得'万'单元
			string strg = je.Substring(8,4);								//取得'元'单元
			string strf = je.Substring(13,2);								//取得小数部分
		
			string str1 = "",str2 = "",str3 = "";

			str1 = getupper(stry,"亿");								//亿单元的大写
			str2 = getupper(strw,"万");								//万单元的大写
			str3 = getupper(strg,"元");								//元单元的大写


			string str_y = "", str_w = "";									
			if (je[3] == '0' || je[4] == '0')								//亿和万之间是否有0
				str_y = "零";
			if (je[7] == '0' || je[8] == '0')								//万和元之间是否有0
				str_w = "零";



			string ret = str1 + str_y + str2 + str_w + str3;				//亿，万，元的三个大写合并

			for (int i = 0 ;i < ret.Length;i++)								//去掉前面的"零"			
			{
				if (ret[i] != '零')
				{
					ret = ret.Substring(i);
					break;
				}

			}
			for (int i = ret.Length - 1;i > -1 ;i--)						//去掉最后的"零"	
			{
				if (ret[i] != '零')
				{
					ret = ret.Substring(0,i+1);
					break;
				}
			}
			
			if (ret[ret.Length  - 1] != '元')								//若最后不位不是'元'，则加一个'元'字
				ret = ret + "元";

			if (ret == "零零元")											//若为零元，则去掉"元数"，结果只要小数部分
				ret = "";
			
			if (strf == "00")												//下面是小数部分的转换
			{
				ret = ret + "整";
			}
			else
			{
				string tmp = "";
				tmp = getint(strf[0]);
				if (tmp == "零")
					ret = ret + tmp;
				else
					ret = ret + tmp + "角";

				tmp = getint(strf[1]);
				if (tmp == "零")
					ret = ret + "整";
				else
					ret = ret + tmp + "分";
			}

			if (ret[0] == '零')
			{
				ret = ret.Substring(1);										//防止0.03转为"零叁分"，而直接转为"叁分"
			}

			if (d>0 )
				return  ret;													//完成，返回								
			else
				return "负 "+ret;
		}
		/// <summary>
		/// 把一个单元转为大写，如亿单元，万单元，个单元
		/// </summary>
		/// <param name="str">这个单元的小写数字（4位长，若不足，则前面补零）</param>
		/// <param name="strDW">亿，万，元</param>
		/// <returns>转换结果</returns>
		private static string getupper(string str,string strDW)
		{
			if (str == "0000")
				return "";

			string ret = "";
			string tmp1 = getint(str[0]) ;
			string tmp2 = getint(str[1]) ;
			string tmp3 = getint(str[2]) ;
			string tmp4 = getint(str[3]) ;
			if (tmp1 != "零")
			{
				ret = ret + tmp1 + "仟";
			}
			else
			{
				ret = ret + tmp1;
			}

			if (tmp2 != "零")
			{
				ret = ret + tmp2 + "佰";
			}
			else
			{
				if (tmp1 != "零")											//保证若有两个零'00'，结果只有一个零，下同
					ret = ret + tmp2;
			}

			if (tmp3 != "零")
			{
				ret = ret + tmp3 + "拾";
			}
			else
			{
				if (tmp2 != "零")
					ret = ret + tmp3;
			}

			if (tmp4 != "零")
			{
				ret = ret + tmp4 ;
			}
			
			if (ret[0] == '零')												//若第一个字符是'零'，则去掉
				ret = ret.Substring(1);
			if (ret[ret.Length - 1] == '零')								//若最后一个字符是'零'，则去掉
				ret = ret.Substring(0,ret.Length - 1);

			return ret + strDW;												//加上本单元的单位
			
		}
		
		/// <summary>
		/// 单个数字转为大写
		/// </summary>
		/// <param name="c">小写阿拉伯数字 0---9</param>
		/// <returns>大写数字</returns>
		private static string getint(char c)
		{
			string str = "";
			switch ( c )
			{
				case '0':
					str = "零";
					break;
				case '1':
					str = "壹";
					break;
				case '2':
					str = "贰";
					break;
				case '3':
					str = "叁";
					break;
				case '4':
					str = "肆";
					break;
				case '5':
					str = "伍";
					break;
				case '6':
					str = "陆";
					break;
				case '7':
					str = "柒";
					break;
				case '8':
					str = "捌";
					break;
				case '9':
					str = "玖";
					break;
			}
			return str;
		}
		#endregion

		#region IsValidEmail：判断邮件格式是否正确
		/// <summary>
		/// 判断邮件格式是否正确
		/// </summary>
		/// <param name="email">邮箱地址</param>
		/// <returns>邮箱地址如果正确，那么返回真，否则返回假</returns>
		/// <example>
		/// <code>
		/// string MyEMail="aaa@bbb.com"
		/// if( IsValidEmail(MyEMail) )
		///		Response.Write("这是一个正确的邮箱地址");
		///	else
		///		Response.Write("不正确的邮箱地址！");
		/// </code>
		/// </example>
		public static bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
		}
		#endregion

		#region IsValidInt：判断一个字符串是否是整数
		/// <summary>
		/// 判断一个字符串是否是整数
		/// </summary>
		/// <param name="strInt">要判断的值</param>
		/// <returns>如果正确，那么返回真，否则返回假</returns>
		/// <example>
		/// <code>
		/// string strValue="111"
		/// if( IsValidInt(strValue) )
		///		Response.Write("这是一个正确的整数");
		///	else
		///		Response.Write("不正确的整数！");
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

		#region IsValidFloat：判断一个字符串是否是浮点数字
		/// <summary>
		/// 判断一个字符串是否是浮点数字
		/// </summary>
		/// <param name="strValue">要判断的值</param>
		/// <returns>如果正确，那么返回真，否则返回假</returns>
		/// <example>
		/// <code>
		/// string strValue="111"
		/// if( IsValidFloat(strValue) )
		///		Response.Write("这是一个正确的浮点数");
		///	else
		///		Response.Write("不正确的浮点数！");
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

		#region DatasetToXML：把数据集生成XML文件
		/// <summary>
		/// 把数据集生成XML文件
		/// </summary>
		/// <param name="ds">数据集</param>
		/// <param name="FullFileName">XML文件名</param>
		public static string DatasetToXML(DataSet ds,string FullFileName)
		{
			string strXML,strSchema,strRsData,strTmp;
			strXML=@"<xml encoding='utf-8' xmlns:s='uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882'
						xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882'
						xmlns:rs='urn:schemas-microsoft-com:rowset'
						xmlns:z='#RowsetSchema'>";

			//获取Schema
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

			//获取rs:data
			strRsData=@"
					<rs:data>";
			strTmp="";
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				strTmp +=@"
					<z:row ";
				foreach(DataColumn dc in ds.Tables[0].Columns)
				{
					string strValue = dr[dc].ToString().Replace("'","’").Replace("/","／").Replace("(","（").Replace(")","）").Replace("<","〈").Replace("&","﹠");
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

		#region  ReturnDataGridXML：由DataGrid中的数据生成XML字符串
		/// <summary>
		/// 由DataGrid中的数据生成XML字符串
		/// </summary>
		/// <param name="dg">DataGrid对象</param>
		/// <param name="dgTitle">由列名组成的字符串，每个列名之间用逗号分割</param>
		/// <param name="IsAlignRightColumns">那些列要右对齐</param>
		/// <remarks>
		/// <code>
		/// 备注：
		/// 1.隐藏列，包括列名为空的列都被忽略。
		/// 2.AutoGenerateColumns为true的时候，无法直接获取列名，所以需要传入dgTitle参数（列名字符串）。
		/// 3.如果dgTitle参数不为空，那么dgTitle一定要跟DataGrid中的列保持一致。
		/// </code>
		/// </remarks>
		public static string ReturnDataGridXML(System.Web.UI.WebControls.DataGrid dg,string dgTitle,out string IsAlignRightColumns)
		{
			string[] ColumnName;

			//------------------初始化列名------------------
			if(dgTitle=="")
			{
				ColumnName = new string[dg.Columns.Count];
				for(int i=0;i<dg.Columns.Count;i++)
				{	
					//忽略隐藏列，隐藏列一般是静态列，所以可以通过Columns属性来访问。
					if(!dg.Columns[i].Visible)
					{
						ColumnName[i]="";
						continue;
					}
			
					//清除列名中的XML语法不允许的字符
					ColumnName[i]=dg.Columns[i].HeaderText.Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
				}
			}
			else
			{
				ColumnName=dgTitle.Split(',');
				for(int i=0;i<ColumnName.Length;i++)
				{	
					//忽略隐藏列，隐藏列一般是静态列，所以可以通过Columns属性来访问。
					if(dg.Columns.Count>i && !dg.Columns[i].Visible)
					{
						ColumnName[i]="";
						continue;
					}
			
					//清除列名中的XML语法不允许的字符
					ColumnName[i]=ColumnName[i].Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
				}
			}

			//------------------初始化列宽------------------
			int[] ColumnWidth=new int[ColumnName.Length];
			for(int i=0;i<ColumnName.Length;i++)
			{	
				ColumnWidth[i]=ColumnName[i].Length;
			}
			
			//------------------生成XML头------------------
			string strXML,strSchema,strRsData,strTmp,strFooterText;
			string strValue;
			strXML=@"<xml encoding='utf-8' xmlns:s='uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882'
						xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882'
						xmlns:rs='urn:schemas-microsoft-com:rowset'
						xmlns:z='#RowsetSchema'>";

			//------------------获取rs:data------------------
			//表格记录
			strRsData=@"
					<rs:data>";
			strTmp="";
			foreach(System.Web.UI.WebControls.DataGridItem dgItem in dg.Items)
			{
				strTmp +=@"
					<z:row ";
				for(int i=0;i<dgItem.Cells.Count;i++)
				{	
					//忽略列名为空的列，包括隐藏列。
					if(ColumnName[i]=="")
						continue;
                    
					//读取当前列值，同时要清除列值中XML不允许的字符，比如：单引号、左尖括号等
					if(dgItem.Cells[i].Text!="&nbsp;" && dgItem.Cells[i].Text!="")
					{
						strValue=dgItem.Cells[i].Text.Replace("'","").Replace("/","").Replace("(","").Replace(")","").Replace("<","").Replace("&","");
						strTmp += string.Format(@"{0}='{1}' ",ColumnName[i],strValue);
					
						//获取列宽，限制于80长度内。如果大于80，就用数据列长度就设为-1（即Memo字段）
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

			//表格页脚
			IsAlignRightColumns="";
			strFooterText="";
			for(int i=0;i<dg.Columns.Count;i++)
			{	
				//忽略隐藏列，隐藏列一般是静态列，所以可以通过Columns属性来访问。
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

			//------------------获取Schema------------------
			strSchema=@"
					<s:Schema id='RowsetSchema'>
						<s:ElementType name='row'>";
			strTmp="";
			for(int i=0;i<ColumnName.Length;i++)
			{	
				//忽略列名为空的列，包括隐藏列。
				if(ColumnName[i]=="")
					continue;

				strTmp +=string.Format(@"
						<s:AttributeType name='{0}' dt:maxLength='{1}' />",ColumnName[i],ColumnWidth[i]);
			}
			strSchema +=strTmp;
			strSchema +=@"
						</s:ElementType>
					</s:Schema>";

			//------------------组成完整的XML字符串------------------
			strXML+=strSchema;
			strXML+=strRsData;
			strXML+=@"
					</xml>";

			return strXML;
		}
		
		
		/// <summary>
		/// 重载函数。由DataGrid中的数据生成XML字符串，DataGrid中的列都为静态列。
		/// </summary>
		/// <param name="dg"></param>
		/// <param name="IsAlignRightColumns">那些列要右对齐</param>
		/// <returns></returns>
		public static string ReturnDataGridXML(System.Web.UI.WebControls.DataGrid dg,out string IsAlignRightColumns)
		{
			return ReturnDataGridXML(dg,"",out IsAlignRightColumns);
		}
		#endregion

		#region 输入字段串校验系列方法...
		/// <summary>
		///获得字符串长度 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetStrLength(string str)
		{
			byte[] sarr = System.Text.Encoding.Default.GetBytes(str);
			return sarr.Length;
		}

		/// <summary>
		///返回指定长度字符串。 
		/// </summary>
		/// <param name="str">字符串</param>
		/// <param name="len">长度</param>
		/// <returns>指定长度字符串</returns>
		/// <remarks>
		/// 在文本编辑框（TextBox控件）中，一个中文汉字和一个英文字母一样都是占一位字节，
		/// 但是在SQL Server数据库中，存储一个中文汉字是占用两位字节，这样就可能会导致
		/// 在保存文本编辑框的内容时字节被截断，保存出错。所以就需要返回指定长度的当前 ANSI 代码页的编码字符。
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
		/// //判断身份证
		/// </summary>
		/// <param name="Cert"></param>
		/// <returns></returns>
		public static bool IsValidCert(string Cert)
		{
			return Regex.IsMatch(Cert, @"^(\d{15}|\d{17}[\dxX])$"); 
		}


		/// <summary>
		///判断用户名
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public static bool IsValidUserName(string UserName)
		{ 
			return Regex.IsMatch(UserName, @"^[a-zA-Z0-9\-]+$"); 
		}

		
		/// <summary>
		///判断英文名 
		/// </summary>
		/// <param name="EnPersonName"></param>
		/// <returns></returns>
		public static bool IsValidEnPersonName(string EnPersonName)
		{
			return Regex.IsMatch(EnPersonName, @"^[a-zA-Z]{1,30}$"); 
		}

		/// <summary>
		/// 判断密码
		/// </summary>
		/// <param name="PassWord"></param>
		/// <returns></returns>
		public static bool IsValidPassWord(string PassWord)
		{
			return Regex.IsMatch(PassWord, @"^(\w){6,20}$"); 
		}

		/// <summary>
		/// 判断电话/传真
		/// </summary>
		/// <param name="Tel"></param>
		/// <returns></returns>
		public static bool IsValidTel(string Tel)
		{
			return Regex.IsMatch(Tel, @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$"); 
		}

		/// <summary>
		/// 判断手机
		/// </summary>
		/// <param name="Mobil"></param>
		/// <returns></returns>
		public static bool IsValidMobil(string Mobil)
		{
            return Regex.IsMatch(Mobil, @"^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$"); 
		}

		/// <summary>
		/// 邮政编码
		/// </summary>
		/// <param name="Zip"></param>
		/// <returns></returns>
		public static bool IsValidZip(string Zip)
		{
			return Regex.IsMatch(Zip, @"^[a-zA-Z0-9]{3,12}$"); 
		}

		/// <summary>
		/// 判断日期
		/// </summary>
		/// <param name="Date"></param>
		/// <returns></returns>
		public static bool IsValidDate(string Date)
		{
			bool bValid=Regex.IsMatch(Date, @"^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"); 
			return (bValid && Date.CompareTo("1753-01-01")>=0);

		}
		
		/// <summary>
		/// 判断只能输字母
		/// </summary>
		/// <param name="EnName"></param>
		/// <returns></returns>
		public static bool IsValidEnName(string EnName)
		{
			return Regex.IsMatch(EnName, @"[a-zA-Z]"); 
		}
		#endregion

		#region DateCompare：对两个日期输入控件中的值进行处理。
		/// <summary>
		/// 对两个日期输入控件中的值进行处理。如果是合法的日期格式，那么把日期小的值放在第一个输入框中，日期大的值放在第二个输入框中。
		/// </summary>
        /// <param name="txtDateFrom">HTML输入控件。日期1</param>
        /// <param name="txtDateTo">HTML输入控件。日期2</param>
		/// <returns>如果是合法的日期格式，那么返回真；否则返回假</returns>
		/// <remarks>这两个日期输入控件都是HTML输入控件</remarks>
		/// <example>
		/// <code>
		/// 示列1：在某个页面的查询条件中存在“日期从...至...”
		/// string BeginDateFrom,BeginDateTo;
		///	if(Teamax.Common.PublicClass.DateCompare(txtBeginDateFrom,txtBeginDateTo))
		///	{
		///		BeginDateFrom=txtBeginDateFrom.Value;	
		///		BeginDateTo=txtBeginDateTo.Value;
		///	}
		///	else
		///	{
		///		((Teamax.Common.CommonPage)Page).MessageBox("查询条件中的“日期”不正确，请重新输入！");
		///		return ;				
		///	}
		///	</code>
		/// </example>
		public static bool DateCompare(System.Web.UI.HtmlControls.HtmlInputControl txtDateFrom,System.Web.UI.HtmlControls.HtmlInputControl txtDateTo)
		{
            return DateCompare(false, txtDateFrom, txtDateTo);
		}

        /// <summary>
        /// 对两个日期输入控件中的值进行处理。如果是合法的日期格式，那么把日期小的值放在第一个输入框中，日期大的值放在第二个输入框中。
        /// </summary>
        /// <param name="IsWithTime">是否带时间部分 小时：分：秒</param>
        /// <param name="txtDateFrom">HTML输入控件。日期1</param>
        /// <param name="txtDateTo">HTML输入控件。日期2</param>
        /// <returns>如果是合法的日期格式，那么返回真；否则返回假</returns>
        /// <remarks>这两个日期输入控件都是HTML输入控件</remarks>
        /// <example>
        /// <code>
        /// 示列1：在某个页面的查询条件中存在“日期从...至...”
        /// string BeginDateFrom,BeginDateTo;
        ///	if(Teamax.Common.PublicClass.DateCompare(true,txtBeginDateFrom,txtBeginDateTo))
        ///	{
        ///		BeginDateFrom=txtBeginDateFrom.Value;	
        ///		BeginDateTo=txtBeginDateTo.Value;
        ///	}
        ///	else
        ///	{
        ///		((Teamax.Common.CommonPage)Page).MessageBox("查询条件中的“日期”不正确，请重新输入！");
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
		/// 重载方法。对两个TextBox文本框控件的值进行处理。如果是合法的日期格式，那么把日期小的值放在第一个输入框中，日期大的值放在第二个输入框中。
		/// </summary>
        /// <param name="txtDateFrom">TextBox输入控件。日期1</param>
        /// <param name="txtDateTo">TextBox输入控件。日期2</param>
		/// <returns>如果是合法的日期格式，那么返回真；否则返回假</returns>
		/// <remarks>这两个日期输入控件都是TextBox文本框控件</remarks>
		/// <example>
		/// <code>
		/// 示列1：在某个页面的查询条件中存在“日期从...至...”
		/// string BeginDateFrom,BeginDateTo;
		///	if(Teamax.Common.PublicClass.DateCompare(txtBeginDateFrom,txtBeginDateTo))
		///	{
		///		BeginDateFrom=txtBeginDateFrom.Value;	
		///		BeginDateTo=txtBeginDateTo.Value;
		///	}
		///	else
		///	{
		///		((Teamax.Common.CommonPage)Page).MessageBox("查询条件中的“日期”不正确，请重新输入！");
		///		return ;				
		///	}
		///	</code>
		/// </example>
		public static bool DateCompare(System.Web.UI.WebControls.TextBox txtDateFrom,System.Web.UI.WebControls.TextBox txtDateTo)
		{
            return DateCompare(false, txtDateFrom, txtDateTo);
		}

        /// <summary>
        /// 重载方法。对两个TextBox文本框控件的值进行处理。如果是合法的日期格式，那么把日期小的值放在第一个输入框中，日期大的值放在第二个输入框中。
        /// </summary>
        /// <param name="IsWithTime">是否带时间部分 小时：分：秒</param>
        /// <param name="txtDateFrom">TextBox输入控件。日期1</param>
        /// <param name="txtDateTo">TextBox输入控件。日期2</param>
        /// <returns>如果是合法的日期格式，那么返回真；否则返回假</returns>
        /// <remarks>这两个日期输入控件都是TextBox文本框控件</remarks>
        /// <example>
        /// <code>
        /// 示列1：在某个页面的查询条件中存在“日期从...至...”
        /// string BeginDateFrom,BeginDateTo;
        ///	if(Teamax.Common.PublicClass.DateCompare(true,txtBeginDateFrom,txtBeginDateTo))
        ///	{
        ///		BeginDateFrom=txtBeginDateFrom.Value;	
        ///		BeginDateTo=txtBeginDateTo.Value;
        ///	}
        ///	else
        ///	{
        ///		((Teamax.Common.CommonPage)Page).MessageBox("查询条件中的“日期”不正确，请重新输入！");
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
