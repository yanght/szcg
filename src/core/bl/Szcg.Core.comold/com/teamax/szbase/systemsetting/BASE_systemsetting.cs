using System;
using System.Data;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Text;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.systemsetting
{
	/// <summary>
	/// BASE_systemsetting 的摘要说明。
	/// </summary>
	public class BASE_systemsetting
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.systemsetting.";
		private string sql;	
		private StringBuilder strSQL;
		
		public BASE_systemsetting()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public DataSet getStatTreeData(string sql)
		{
			return DataAccess.ExecuteDataSet(sql, null);
		}
		public void insertdate(int userId, int fid,string short_sentence,string text_sentence,string stepid)
		{
			try
			{
				sql="insert into diction_sentence(fid,text_sentence,short_sentence,stepid) values(@fid, @text, @short, @stepid)";
				SqlParameter[] param = new SqlParameter[4];
				param[0] = new SqlParameter("@fid", SqlDbType.Int, 4);
				param[0].Direction = ParameterDirection.Input;
				param[0].Value = fid;
				param[1] = new SqlParameter("@text", SqlDbType.VarChar, 512);
				param[1].Direction = ParameterDirection.Input;
				param[1].Value = text_sentence;
				param[2] = new SqlParameter("@short", SqlDbType.VarChar, 50);
				param[2].Direction = ParameterDirection.Input;
				param[2].Value = short_sentence;
				param[3] = new SqlParameter("@stepid", SqlDbType.VarChar, 1);
				param[3].Direction = ParameterDirection.Input;
				param[3].Value = stepid;
				DataAccess.ExecuteNonQuery(sql, param);
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZHYW(),
					err.ToString(),
					NAMESPACE_PATH + "insertdate");
				throw;
			}
		}

		public void deleteDiction(int userId, int id)
		{
			try
			{
				strSQL = new StringBuilder();
				strSQL.Append("	DELETE diction_sentence WHERE id = @id");
				SqlParameter[] param = new SqlParameter[1];
				param[0] = new SqlParameter("@id", SqlDbType.Int, 4);
				param[0].Direction = ParameterDirection.Input;
				param[0].Value = id;
				DataAccess.ExecuteNonQuery(strSQL.ToString(), param);

				BASE_logmanageservice.writeUserLog(
					userId,
					System.DateTime.Today,
					System.DateTime.Today,
					BASE_ModerId.getSystem_ZCPT(),
					strSQL.ToString(),
					NAMESPACE_PATH + "deleteDiction");
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					strSQL.ToString(),
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZHYW(),
					err.ToString(),
					NAMESPACE_PATH + "deleteDiction");
				throw;
			}
		}

		/// <summary>
		/// 取得字典库数据 
		/// </summary>
		/// <param name="argUserCode">登陆用户Id</param>
		/// <returns>群组类型</returns>
		public DataSet getZidiankuInfo(int userId)
		{

				//StringBuilder strSQL;
				DataSet dataset;
				strSQL = new StringBuilder();
				strSQL.Append("	SELECT id AS code, ");
				strSQL.Append("		name AS name ");
				strSQL.Append("	FROM dictionarylib ");
				try
				{
					dataset = DataAccess.ExecuteDataSet(strSQL.ToString(), null);
					return dataset;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						strSQL.ToString(),
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZHYW(),
						err.ToString(),
						NAMESPACE_PATH + "getZidiankuInfo");
					throw;
				}
		}

		/// <summary>
		/// 取得字典库数据 
		/// </summary>
		/// <param name="argUserCode">登陆用户Id</param>
		/// <returns>群组类型</returns>
		public DataSet getJiDuanInfo(int userId)
		{
			DataSet dataset;
			strSQL = new StringBuilder();
			strSQL.Append("	SELECT id AS code, ");
			strSQL.Append("		stepname AS name ");
			strSQL.Append("	FROM step ");
			try
			{
				dataset = DataAccess.ExecuteDataSet(strSQL.ToString(), null);
				return dataset;
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					strSQL.ToString(),
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZHYW(),
					err.ToString(),
					NAMESPACE_PATH + "getJiDuanInfo");
				throw;
			}
		}

		/// <summary>
		/// 获得字典库数据条数
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="argfid">日志常用语id, 惯用语Id, 办公用语Id</param>
		/// <returns></returns>
		public int getDictionRecodeCount(int userId, int argfid)
		{
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection)) 
			{
				try
				{
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("getDictionsCount", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@fid", SqlDbType.Int).Value         = argfid;
					SqlDataReader reader = myCommand.ExecuteReader();
					reader.Read();
					int recodeCount = (int)reader["CNT"];
					return recodeCount;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						Convert.ToInt16(userId),
						"StoredProcedure:getDictionsCount",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZHYW(),
						err.ToString(),
						NAMESPACE_PATH + "getDictionRecodeCount");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// 获得字典库数据
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="argfid">日志常用语id, 惯用语Id, 办公用语Id</param>
		/// <param name="argPageIndex">页序号</param>
		/// <param name="argPageSize">页的行数</param>
		/// <returns></returns>
		public DataSet getInfoD(int userId, int argfid, int argPageIndex, int argPageSize)
		{
			SqlDataAdapter dad;
			DataSet dtst;
			using( SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection)) 
			{
				try 
				{
					dtst = new DataSet();
					dad = new SqlDataAdapter();
					myConnection.Open();
					SqlCommand myCommand = new SqlCommand("getDictions", myConnection);
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.Add("@fid", SqlDbType.Int).Value         = argfid;
					myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value   = argPageIndex;
					myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value    = argPageSize;
					dad.SelectCommand = myCommand;
					dad.Fill(dtst, "tblDictions"); 
					myConnection.Close();
					return dtst;
				}
				catch (Exception err)
				{
					BASE_logmanageservice.writeSystemLog(
						userId,
						"StoredProcedure:getDictions",
						System.DateTime.Today,
						System.DateTime.Today, 
						BASE_ModerId.getSystem_ZCPT(),
						err.ToString(),
						NAMESPACE_PATH + "getInfoD");
					throw;
				}
				finally
				{
					myConnection.Dispose();
				}
			}
		}

		public string[] getDPInfo(int userId, int id, int fid)
		{
			string[] array=new string[4];
			try
			{
				strSQL = new StringBuilder();
				strSQL.Append(" SELECT A.short_sentence, ");
				strSQL.Append("		A.text_sentence, ");
				strSQL.Append("		A.stepid, ");
				strSQL.Append("		B.name ");
				strSQL.Append(" FROM diction_sentence AS A, ");
				strSQL.Append("			dictionarylib AS B ");
				strSQL.Append(" WHERE A.fid = B.[id] ");
				strSQL.Append("		AND A.id = @id ");
				strSQL.Append("		AND A.fid = @fid ");
				SqlParameter[] param = new SqlParameter[2];
				param[0] = new SqlParameter("@id", SqlDbType.Int, 4);
				param[0].Direction = ParameterDirection.Input;
				param[0].Value = id;
				param[1] = new SqlParameter("@fid", SqlDbType.Int, 4);
				param[1].Direction = ParameterDirection.Input;
				param[1].Value = fid;
				SqlDataReader dr=DataAccess.ExecuteReader(strSQL.ToString(), param);
				while(dr.Read())
				{
					array[0]=dr["short_sentence"].ToString();
					array[1]=dr["text_sentence"].ToString();
					array[2]=dr["name"].ToString();
					array[3]=dr["stepid"].ToString();
				}
				dr.Close();
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "getDPInfo");
				throw;
			}
			
			return array;
		}
		public void updateinfo(int userId, int id, int fid, string short_sentence,string text_sentence, string stepid)
		{
			try
			{
				sql="update diction_sentence set fid=@fid, text_sentence=@text_sentence, short_sentence=@short_sentence, stepid=@stepid where id=@id";
				SqlParameter[] param = new SqlParameter[5];
				param[0] = new SqlParameter("@id", SqlDbType.Int, 4);
				param[0].Direction = ParameterDirection.Input;
				param[0].Value = id;
				param[1] = new SqlParameter("@fid", SqlDbType.Int, 4);
				param[1].Direction = ParameterDirection.Input;
				param[1].Value = fid;
				param[2] = new SqlParameter("@text_sentence", SqlDbType.VarChar, 512);
				param[2].Direction = ParameterDirection.Input;
				param[2].Value = text_sentence;
				param[3] = new SqlParameter("@short_sentence", SqlDbType.VarChar, 50);
				param[3].Direction = ParameterDirection.Input;
				param[3].Value = short_sentence;
				param[4] = new SqlParameter("@stepid", SqlDbType.VarChar, 1);
				param[4].Direction = ParameterDirection.Input;
				param[4].Value = stepid;
				DataAccess.ExecuteNonQuery(sql, param);

				BASE_logmanageservice.writeUserLog(
					userId,
					System.DateTime.Today,
					System.DateTime.Today,
					BASE_ModerId.getSystem_ZCPT(),
					sql,
					NAMESPACE_PATH + "updateinfo");
			}
			catch (Exception err)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Today,
					System.DateTime.Today, 
					BASE_ModerId.getSystem_ZCPT(),
					err.ToString(),
					NAMESPACE_PATH + "updateinfo");
				throw;
			}
		}
	}
}
