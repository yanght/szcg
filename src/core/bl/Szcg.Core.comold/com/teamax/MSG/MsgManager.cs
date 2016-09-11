using System;
using AjaxPro;
using System.Data.SqlClient;
using System.Data;

namespace szcg.com.teamax.msg
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
		public string getMessage(string usercode)
		{
			
			SqlParameter [] input = new SqlParameter[1];

			input[0] = new SqlParameter("@usercode", System.Data.SqlDbType.Int);
			input[0].Direction = ParameterDirection.Input;
			input[0].Value = usercode;
			
			SqlParameter [] output = new SqlParameter[1];
			output[0] = new SqlParameter("@flag", System.Data.SqlDbType.Int);
			output[0].Direction = ParameterDirection.Output;

			string result = com.teamax.util.DataAccess.ExecuteStoreProcedure1("getmsg",input, output);
			
			return result;

		}
		
		[AjaxMethod]
		public string getCollecterXY()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Data.SqlClient.SqlDataReader dr  = com.teamax.util.DataAccess.ExecuteStoredProcedure2("getCXY", null);
			while(dr.Read()){
				string temp = System.Convert.ToInt32(dr[0])  + "," + System.Convert.ToString(dr[1]) + "," + System.Convert.ToString(dr[2]);
				sb.Append(temp).Append("$");	
			}
			if(dr != null)
				dr.Close();

			string ret = sb.ToString();
			return  (ret != null && ret.Length > 0) ? ret.Substring(0, ret.Length-1) : "";
		}


		[AjaxMethod]
		public string getCollecterInfo(string collcode)
		{
			System.Data.SqlClient.SqlParameter [] input = new SqlParameter[1];
			input[0] = new SqlParameter("@collcode", System.Data.SqlDbType.NVarChar);
			input[0].Direction = ParameterDirection.Input;
			input[0].Value = collcode;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Data.SqlClient.SqlDataReader dr  = com.teamax.util.DataAccess.ExecuteStoredProcedure2("getCollecterInfo",input);
			if(dr.Read())
			{
				sb.Append(System.Convert.ToInt32(dr[0])).Append(",").
                    Append(bacgBL.Pub.Tools.StrConv(System.Convert.ToString(dr[1]), "GB2312")).Append(",")
					.Append( System.Convert.ToString(dr[2]))
					.Append(",").Append(System.Convert.ToString(dr[3])).Append(",").Append(System.Convert.ToString(dr[4]))
					.Append(",").Append(System.Convert.ToString(dr[5])).Append(",").Append(System.Convert.ToString(dr[6]));
				
			}
			if(dr != null)
				dr.Close();

			string ret = sb.ToString();
			return ret;
		}
		


		[AjaxMethod]
		public string DrawCollectTrack(string collectercode, string numb)
		{
			System.Data.SqlClient.SqlDataReader dr = null;
			try
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();

				//string sql = "select top " +numb+" cu_x, cu_y from ( SELECT id, cu_x, cu_y FROM collecter_xy WHERE collcode = "+ collectercode+ " ) as t order by t.id desc";
				string sql = "select top " +numb+" cu_x, cu_y from ( SELECT id, cu_x, cu_y FROM collecter_xy WHERE collcode = "+ collectercode+ " and DATEDIFF(DAY,cu_date,GETDATE())=0 ) as t order by t.id desc";
	
				
				dr = com.teamax.util.DataAccess.getDataReader(sql);
				while(dr != null && dr.Read()){
					sb.Append(System.Convert.ToString(dr[0]) + "," + System.Convert.ToString(dr[1])).Append("#");
				}
				if(dr != null)
					dr.Close();

				string ret = sb.ToString();
				return (ret != null && ret.Length>0) ? ret.Substring(0,ret.Length -1) : "";

				
			}
			catch(Exception e)
			{
				if(dr != null) dr.Close();
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "";
			}
		}


		[AjaxMethod]
		public string deleteMsg(int id)
		{
			try
			{
                string sql = "delete s_message where  id = '" + id + "'";
				com.teamax.util.DataAccess.ExecuteNonQuery(sql,null);
				return "1";
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "异常";
			}
		}

		[AjaxMethod]
		public string searchcolltrack(string collcode, string begint, string endt)
		{
			try
			{
				System.Data.SqlClient.SqlParameter [] input = new SqlParameter[3];
				input[0] = new SqlParameter("@collcode", System.Data.SqlDbType.NVarChar);
				input[0].Direction = ParameterDirection.Input;
				input[0].Value = collcode;

				input[1] = new SqlParameter("@begint", System.Data.SqlDbType.NVarChar);
				input[1].Direction = ParameterDirection.Input;
				input[1].Value = begint;

				input[2] = new SqlParameter("@endt", System.Data.SqlDbType.NVarChar);
				input[2].Direction = ParameterDirection.Input;
				input[2].Value = endt;

				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				System.Data.SqlClient.SqlDataReader dr  = com.teamax.util.DataAccess.ExecuteStoredProcedure2("searchcolltrack",input);
				while(dr.Read())
				{
                    sb.Append(dr[0]).Append(",").Append(bacgBL.Pub.Tools.StrConv(System.Convert.ToString(dr[1]), "GB2312"))
						.Append(",").Append(dr[2]).Append(",").Append(dr[3]).Append(",").Append(dr[4]).Append(",").Append(dr[5]);
					sb.Append("#");
				}
				string ret = sb.ToString();
				return (ret != null && ret.Length  > 0) ? ret.Substring(0,ret.Length) : "";
			
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return "异常";
			}
		}



	}
}
