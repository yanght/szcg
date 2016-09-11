using System;
using System.Web;

namespace SZCG.GPS.Web
{
	/// <summary>
	/// WebUtility 的摘要说明。
	/// </summary>
	public abstract class WebUtility
	{
		public static string FormatHtmlString(string inputString, int length)
		{
			if(inputString==null)
				return "";

			inputString = inputString.Trim();

			if (inputString == "&nbsp;")
				return "";

			if (length == 0)
				return HttpUtility.HtmlEncode(inputString);

			string cutHTMLText = "<font color='#666666'>&nbsp;[截断]</font>";

			if (inputString.Length > length)
				inputString = HttpUtility.HtmlEncode(inputString.Substring(0, length > 4 ? length - 4 : length)) + cutHTMLText;
			else
				inputString = HttpUtility.HtmlEncode(inputString);

			return inputString;
		}

		public static void ShowJsAlert(string strMessage)
		{
			HttpContext.Current.Response.Write("<script>alert('" + strMessage + "')</script>");
		}

		public static bool CheckStringLength(string inputString, int intLength)
		{
			if (inputString.Length > intLength)
				return false;
			else
				return true;
		}

		public static bool IsDateTime(string inputString)
		{
			try
			{
				Convert.ToDateTime(inputString);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsInt32(string inputString)
		{
			try
			{
				Convert.ToInt32(inputString);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsInt64(string inputString)
		{
			try
			{
				Convert.ToInt64(inputString);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsDouble(string inputString)
		{
			try
			{
				Convert.ToDouble(inputString);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
