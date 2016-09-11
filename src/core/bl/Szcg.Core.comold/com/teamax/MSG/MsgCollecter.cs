using System;
using AjaxPro;
using System.Data.SqlClient;
using System.Data;

namespace szcg.com.teamax.msg
{
	/// <summary>
	/// MsgCollecter 的摘要说明。
	/// </summary>
	public class MsgCollecter
	{
		public MsgCollecter()
		{
			
		}

		[AjaxMethod]
		public string getProjNumber(string areacode)
		{

			System.Data.SqlClient.SqlParameter [] input = new SqlParameter[1];
			input[0] = new SqlParameter("@areacode", System.Data.SqlDbType.NVarChar);
			input[0].Direction = ParameterDirection.Input;
			input[0].Value = areacode;

			SqlParameter [] output = new SqlParameter[4];
			output[0] = new SqlParameter("@collnum", System.Data.SqlDbType.Int);
			output[0].Direction = ParameterDirection.Output;
	
			output[1] = new SqlParameter("@endcase", System.Data.SqlDbType.Int);
			output[1].Direction = ParameterDirection.Output;

			output[2] = new SqlParameter("@uncase", System.Data.SqlDbType.Int);
			output[2].Direction = ParameterDirection.Output;

			output[3] = new SqlParameter("@approve", System.Data.SqlDbType.Int);
			output[3].Direction = ParameterDirection.Output;


			string result  = com.teamax.util.DataAccess.ExecuteStoreProcedure1("getProjNumber", input,output);
			
			input = null;
			output = null;

			return result;
		}

		

	}
}
