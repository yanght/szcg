using System;
using AjaxPro;
using System.Data.SqlClient;
using System.Data;

namespace SZCG.GPS.DAL.GPS
{
	/// <summary>
	/// MsgManager 的摘要说明。
	/// </summary>
	public class MsgManager
	{
		public MsgManager()
		{
			
		}

		
		[AjaxMethod]
		public string getCarXY()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Data.SqlClient.SqlDataReader dr  = SZCG.GPS.DAL.GPS.DataAccess.ExecuteStoredProcedure2("getCarXY", null);
			while(dr.Read())
			{
				string temp = System.Convert.ToString(dr[0])  + "," + System.Convert.ToString(dr[1]) + "," + System.Convert.ToString(dr[2]);
				sb.Append(temp).Append("$");	
			}
			dr.Close();
			string ret = sb.ToString();
			return  (ret != null && ret.Length > 0) ? ret.Substring(0, ret.Length-1) : "";

		}
		
		[AjaxMethod]
		public string getCar(string vcode)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			System.Data.SqlClient.SqlParameter [] input = new SqlParameter[1];
			input[0] = new SqlParameter("@code", System.Data.SqlDbType.VarChar,200);
			input[0].Direction = ParameterDirection.Input;
			input[0].Value = vcode;

			System.Data.SqlClient.SqlDataReader dr  = SZCG.GPS.DAL.GPS.DataAccess.ExecuteStoredProcedure2("getCar", input);

			//0;区,1;手机;2名称;
			if(dr.Read())
			{
				sb.Append( DataAccess.StrConv(System.Convert.ToString(dr[0]),"GB2312") );
				
			}
			dr.Close();
			string ret = sb.ToString();
			return ret;
			
			
		}



		[AjaxMethod]
		public string getCarRowsInfo(string vcode)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			System.Data.SqlClient.SqlParameter [] input = new SqlParameter[1];
			input[0] = new SqlParameter("@carcode", System.Data.SqlDbType.VarChar,200);
			input[0].Direction = ParameterDirection.Input;
			input[0].Value = vcode;

			
			System.Data.SqlClient.SqlDataReader dr  = SZCG.GPS.DAL.GPS.DataAccess.ExecuteStoredProcedure2("getCarInfo", input);
			
			while(dr.Read())
			{
                sb.Append(System.Convert.ToString(dr[1]).Trim() + "," + System.Convert.ToString(dr[0]).Trim()).Append(",");
			}
			dr.Close();
			string ret = sb.ToString().TrimEnd(',');
			return  (ret != null && ret.Length > 0) ? ret.Substring(0, ret.Length-1) : "";

		}	
		
	}
}
