using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// SqlFilter 的摘要说明。
	/// </summary>
	public class SqlFilter
	{
		
		//private string[] keywords={"select","update","insert","delete","distinct","and","or"};
		
		private static Regex FilterInjectionRegex;

		// SQL Injection过滤 add by yxw(2006-05-23)
		public static string FilterInjection(string s)
		{
			s = s.Replace("'", "''");
			if (FilterInjectionRegex == null)
				FilterInjectionRegex = new Regex(@"%3D|=|%27|%2D|--|%3B|;", RegexOptions.IgnoreCase);
			if (FilterInjectionRegex.IsMatch(s))
			{
				return "";
			}
			else
				return s;
		}

		public static string filter(string inputString, int maxLength) 
		{
			StringBuilder retVal = new StringBuilder();
         
			if ((inputString != null) && (inputString != String.Empty)) 
			{
				inputString = inputString.Trim();
				if (inputString.Length > maxLength)
				{
					inputString = inputString.Substring(0, maxLength);
				}
			
				for (int i = 0; i < inputString.Length; i++) 
				{
					switch (inputString[i]) 
					{
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}

				retVal.Replace("'", " ");
			}
			return retVal.ToString();
		}
	}
}
