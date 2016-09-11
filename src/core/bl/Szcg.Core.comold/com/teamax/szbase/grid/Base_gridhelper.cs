using System;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Data;
using System.Text;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.grid
{
	/// <summary>
	/// ����������Ԫ,������,�ֵ�,����,����ȶ���Ĺ���
	///  add by yxw(2006-05-22)
	/// </summary>
	public class BASE_gridhelper
	{
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.grid.BASE_gridhelper.";


		#region ��������ط���
		/**
		 * ȡ����������¼
		 **/
		public static ArrayList GetAreas(int userId,string areacode)
		{
			ArrayList areas = new ArrayList(); 
			string sql = "select id,areaname,areacode from area";
			if(!areacode.Equals("4403"))
			{
				sql="select id,areaname,areacode from area where areacode='"+areacode+"'";
			}
			try
			{
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] area = new string[3];
					area[0] = rs["id"].ToString();
					area[1] = rs["areaname"].ToString();
					area[2] = rs["areacode"].ToString();
					areas.Add(area);
				}

				rs.Close();
				return areas;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetAreas");
				throw;
			}
		}

        //TODO:��Ҫ�޸ĸ�ģ��
		public static ArrayList GetAreas()
		{
			ArrayList areas = new ArrayList();
            string sql = "select areacode as id,areaname,areacode from s_area  where areaname not like  '%�縣%' and areaname not like '����' ";
			
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] area = new string[3];
					area[0] = rs["id"].ToString();
					area[1] = rs["areaname"].ToString();
					area[2] = rs["areacode"].ToString();
					areas.Add(area);
				}

				rs.Close();
				return areas;
			
		}

		/**
		 *������ID��ѯ����ϸ��Ϣ
		 *id ��ID
		**/
		public static string[] GetAreaInfoById(int id,int userId)
		{
			string sql = "getAreaInfoById";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0] = new SqlParameter("@id",SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[4];
				output[0] = new SqlParameter("@areacode", SqlDbType.Int);
				output[1] = new SqlParameter("@areaname", SqlDbType.VarChar, 18);
				output[2] = new SqlParameter("@area", SqlDbType.VarChar, 18);
				output[3] = new SqlParameter("@memo", SqlDbType.VarChar, 512);

				output[0].Direction = output[1].Direction = output[2].Direction 
					= output[3].Direction = ParameterDirection.Output;

				return DataAccess.ExecuteStoreProcedure1(sql,input,output).Split(';');
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetAreaInfoById");
				throw;
			}
		}

		/**
		 *�������¼
		**/
		public static int InsertToArea(string[] values,int userId)
		{
			string sql = "insertToArea";
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@areacode", SqlDbType.Int);
				input[0].Value = Convert.ToInt32(values[0]);
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@areaname", SqlDbType.VarChar, 18);
				input[1].Value = values[1];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@area", SqlDbType.VarChar, 18);
				input[2].Value = values[2];
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[3].Value = values[3];
				input[3].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
            
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.InsertToArea");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InsertToArea");
				throw;
			}
		}

		/**
		 *��������¼
		**/
		public static int UpdateToArea(int id, string[] values,int userId)
		{
			string sql = "updateToArea";
			try
			{
				SqlParameter[] input = new SqlParameter[5];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@areacode", SqlDbType.Int);
				input[1].Value = Convert.ToInt32(values[0]);
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@areaname", SqlDbType.VarChar, 18);
				input[2].Value = values[1];
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@area", SqlDbType.VarChar, 18);
				input[3].Value = values[2];
				input[3].Direction = ParameterDirection.Input;
				input[4] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[4].Value = values[3];
				input[4].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.UpdateToArea");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "UpdateToArea");
				throw;
			}
		}

		/**
		 *����IDɾ������¼
		**/
		public static int DeleteFromArea(int id,int userId)
		{
			string sql = "deleteFromArea";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				SqlParameter[] output = new SqlParameter[1];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.DeleteFromArea");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "DeleteFromArea");
				throw;
			}
		}

		/**
		 *����Ƿ������ͬ��������
		**/
		public static bool CheckAreaName(string name,int userId)
		{
			string sql="select count(*) from area where areaname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckAreaName");
				throw;
			}
		}
		#endregion

		#region �ֵ�������ط���
		/**
		 * ��������Ų�ѯ�ֵ�
		 **/
		public static ArrayList GetStreetByAreaId(int areaId,int userId)
		{
			ArrayList streets = new ArrayList(); 
			string sql = "select id,streetname,streetcode from street where fid=" + areaId;
			try
			{
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] street = new string[3];
					street[0] = rs["id"].ToString();
					street[1] = rs["streetname"].ToString();
					street[2] = rs["streetcode"].ToString();
					streets.Add(street);
				}

				rs.Close();
				return streets;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetStreetByAreaId");
				throw;
			}
		}
        
		public static ArrayList GetStreetByAreaId(int areaId)
		{
			ArrayList streets = new ArrayList();
            string sql = "select streetcode ,streetname from s_street where areacode=" + areaId;
            sql = sql + "order by id";
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] street = new string[2];
					street[0] = rs["streetcode"].ToString();
					street[1] = rs["streetname"].ToString();
					streets.Add(street);
				}

				rs.Close();
				return streets;
			
		}

		/**
		 *���ݽֵ�ID��ѯ�ֵ���ϸ��Ϣ
		 *id �ֵ�ID
		**/
		public static string[] GetStreetInfoById(int id,int userId)
		{
			string sql = "getStreetInfoByID";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0] = new SqlParameter("@id",SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[5];
				output[0] = new SqlParameter("@streetcode", SqlDbType.VarChar, 32);
				output[1] = new SqlParameter("@streetname", SqlDbType.VarChar, 255);
				output[2] = new SqlParameter("@fid", SqlDbType.Int);
				output[3] = new SqlParameter("@areaname", SqlDbType.VarChar, 18);
				output[4] = new SqlParameter("@memo", SqlDbType.VarChar, 512);

				output[0].Direction = output[1].Direction = output[2].Direction 
					= output[3].Direction = output[4].Direction = ParameterDirection.Output;
	
				return DataAccess.ExecuteStoreProcedure1(sql,input,output).Split(';');
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetStreetInfoById");
				throw;
			}
		}

		/**
		 *��ӽֵ���¼
		**/
		public static int InsertToStreet(string[] values,int userId)
		{
			string sql = "insertToStreet";
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@streetcode", SqlDbType.VarChar, 32);
				input[0].Value = values[0];
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@streetname", SqlDbType.VarChar, 255);
				input[1].Value = values[1];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@fid", SqlDbType.Int);
				input[2].Value = Convert.ToInt32(values[2]);
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[3].Value = values[3];
				input[3].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;
            
				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.InsertToArea");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InsertToStreet");
				throw;
			}
		}

		/**
		 *��������¼
		**/
		public static int UpdateToStreet(int id, string[] values,int userId)
		{
			string sql = "updateToStreet";
			try
			{
				SqlParameter[] input = new SqlParameter[5];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@streetcode", SqlDbType.VarChar, 32);
				input[1].Value = values[0];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@streetname", SqlDbType.VarChar, 255);
				input[2].Value = values[1];
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@fid", SqlDbType.Int);
				input[3].Value = Convert.ToInt32(values[2]);
				input[3].Direction = ParameterDirection.Input;
				input[4] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[4].Value = values[3];
				input[4].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.UpdateToStreet");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "UpdateToStreet");
				throw;
			}
		}

		/**
		 *����IDɾ������¼
		**/
		public static int DeleteFromStreet(int id,int userId)
		{
			string sql = "deleteFromStreet";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				SqlParameter[] output = new SqlParameter[1];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.DeleteFromStreet");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "DeleteFromStreet");
				throw;
			}
		}
		#endregion

		#region ����������ط���
		/**
		 * ���ݽֵ���Ų�ѯ����
		 **/
		public static ArrayList GetCommunityByStreetId(int streetId,int userId)
		{
			ArrayList communitys = new ArrayList(); 
			string sql = "select id,commname,commcode from community where fid=" + streetId;
			try
			{
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] community = new string[3];
					community[0] = rs["id"].ToString();
					community[1] = rs["commname"].ToString();
					community[2] = rs["commcode"].ToString();
					communitys.Add(community);
				}

				rs.Close();
				return communitys;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetCommunityByStreetId");
				throw;
			}
		}

		public static ArrayList GetCommunityByStreetId(int streetId)
		{
			ArrayList communitys = new ArrayList();
            string sql = "select commcode as id,commname from s_community where streetcode=" + streetId;

				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] community = new string[2];
					community[0] = rs["id"].ToString();
					community[1] = rs["commname"].ToString();
					communitys.Add(community);
				}

				rs.Close();
				return communitys;
			
		}


		/**
		 *��������ID��ѯ������ϸ��Ϣ
		 *id ����ID
		**/
		public static string[] GetCommunityInfoById(int id,int userId)
		{
			string sql = "getCommunityInfoByID";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0] = new SqlParameter("@id",SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[5];
				output[0] = new SqlParameter("@commcode", SqlDbType.VarChar, 32);
				output[1] = new SqlParameter("@commname", SqlDbType.VarChar, 255);
				output[2] = new SqlParameter("@fid", SqlDbType.Int);
				output[3] = new SqlParameter("@streetname", SqlDbType.VarChar, 255);
				output[4] = new SqlParameter("@memo", SqlDbType.VarChar, 512);

				output[0].Direction = output[1].Direction = output[2].Direction 
					= output[3].Direction = output[4].Direction = ParameterDirection.Output;

				return DataAccess.ExecuteStoreProcedure1(sql,input,output).Split(';');
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetCommunityInfoById");
				throw;
			}
		}

		/**
		 *���������¼
		**/
		public static int InsertToCommunity(string[] values,int userId)
		{
			string sql = "insertToCommunity";
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@Commcode", SqlDbType.VarChar, 32);
				input[0].Value = values[0];
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@Commname", SqlDbType.VarChar, 255);
				input[1].Value = values[1];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@fid", SqlDbType.Int);
				input[2].Value = Convert.ToInt32(values[2]);
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[3].Value = values[3];
				input[3].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.InsertToCommunity");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InsertToCommunity");
				throw;
			}
		}

		/**
		 *����������¼
		**/
		public static int UpdateToCommunity(int id, string[] values,int userId)
		{
			string sql = "updateToCommunity";
			try
			{
				SqlParameter[] input = new SqlParameter[5];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@Commcode", SqlDbType.VarChar, 32);
				input[1].Value = values[0];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@Commname", SqlDbType.VarChar, 255);
				input[2].Value = values[1];
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@fid", SqlDbType.Int);
				input[3].Value = Convert.ToInt32(values[2]);
				input[3].Direction = ParameterDirection.Input;
				input[4] = new SqlParameter("@memo", SqlDbType.VarChar, 512);
				input[4].Value = values[3];
				input[4].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.UpdateToCommunity");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "UpdateToCommunity");
				throw;
			}
		}

		/**
		 *����IDɾ��������¼
		**/
		public static int DeleteFromCommunity(int id,int userId)
		{
			string sql = "deleteFromCommunity";
			try
			{	
				SqlParameter[] input = new SqlParameter[1];
				SqlParameter[] output = new SqlParameter[1];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.DeleteFromCommunity");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "DeleteFromCommunity");
				throw;
			}
		}
		#endregion

		#region ���������ط���
		/**
		 * ����������Ų�ѯ����
		 **/
		public static ArrayList GetGridByCommunityId(int communityId,int userId)
		{
			ArrayList grids = new ArrayList(); 
			string sql = "select id,gridcode from grid where commfid=" + communityId;
			try
			{
				SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
			
				while(rs.Read())
				{
					string[] grid = new string[2];
					grid[0] = rs["id"].ToString();
					grid[1] = rs["gridcode"].ToString();
					grids.Add(grid);
				}

				rs.Close();
				return grids;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetGridByCommunityId");
				throw;
			}
		}

		/**
		 *��������ID��ѯ������ϸ��Ϣ
		 *id ����ID
		**/
		public static string[] GetGridInfoById(int id,int userId)
		{
			string sql = "getGridInfoByID";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				input[0] = new SqlParameter("@id",SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[4];
				output[0] = new SqlParameter("@gridcode", SqlDbType.VarChar, 18);
				output[1] = new SqlParameter("@commfid", SqlDbType.Int);
				output[2] = new SqlParameter("@commname", SqlDbType.VarChar, 255);
				output[3] = new SqlParameter("@_desc", SqlDbType.VarChar, 255);

				output[0].Direction = output[1].Direction = output[2].Direction 
					= output[3].Direction = ParameterDirection.Output;

				return DataAccess.ExecuteStoreProcedure1(sql,input,output).Split(';');
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "GetGridInfoById");
				throw;
			}
		}

		/**
		 *�������
		**/
		public static int InsertToGrid(string[] values,int userId)
		{
			string sql = "insertToGrid";
			try
			{
				SqlParameter[] input = new SqlParameter[3];
				input[0] = new SqlParameter("@gridcode", SqlDbType.VarChar, 18);
				input[0].Value = values[0];
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@commfid", SqlDbType.Int);
				input[1].Value = Convert.ToInt32(values[1]);
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@_desc", SqlDbType.VarChar, 255);
				input[2].Value = values[2];
				input[2].Direction = ParameterDirection.Input;

				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.InsertToGrid");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "InsertToGrid");
				throw;
			}
		}

		/**
		 *��������
		**/
		public static int UpdateToGrid(int id, string[] values,int userId)
		{
			string sql = "updateToGrid";
			try
			{
				SqlParameter[] input = new SqlParameter[4];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				input[1] = new SqlParameter("@gridcode", SqlDbType.VarChar, 18);
				input[1].Value = values[0];
				input[1].Direction = ParameterDirection.Input;
				input[2] = new SqlParameter("@commfid", SqlDbType.Int);
				input[2].Value = Convert.ToInt32(values[1]);
				input[2].Direction = ParameterDirection.Input;
				input[3] = new SqlParameter("@_desc", SqlDbType.VarChar, 255);
				input[3].Value = values[2];
				input[3].Direction = ParameterDirection.Input;
				SqlParameter[] output = new SqlParameter[1];
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.UpdateToGrid");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "UpdateToGrid");
				throw;
			}
		}

		/**
		 *����IDɾ�������¼
		**/
		public static int DeleteFromGrid(int id,int userId)
		{
			string sql = "deleteFromGrid";
			try
			{
				SqlParameter[] input = new SqlParameter[1];
				SqlParameter[] output = new SqlParameter[1];
				input[0] = new SqlParameter("@id", SqlDbType.Int);
				input[0].Value = id;
				input[0].Direction = ParameterDirection.Input;
				output[0] = new SqlParameter("@result", SqlDbType.Int);
				output[0].Direction = ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql, input, output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10107(),sql,"szcg.com.teamax.szbase.grid.BASE_gridhelper.DeleteFromGrid");

				return i;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:"+sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "DeleteFromGrid");
				throw;
			}
		}
		#endregion

		public static bool CheckAreaCode(string code,int userId)
		{
			string sql="select count(*) from area where areacode='"+code+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckAreaCode");
				throw;
			}
		}

		public static bool CheckStreetCode(string code,int userId)
		{
			string sql="select count(*) from street where streetcode='"+code+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckStreetCode");
				throw;
			}
		}

		public static bool CheckStreetName(string name,int userId)
		{
			string sql="select count(*) from street where streetname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckStreetName");
				throw;
			}
		}

		public static bool CheckCommName(string name,int userId)
		{
			string sql="select count(*) from community where commname='"+name+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckCommName");
				throw;
			}
		}

		public static bool CheckCommCode(string code,int userId)
		{
			string sql="select count(*) from community where commcode='"+code+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckCommCode");
				throw;
			}
		}

		public static bool CheckGridCode(string code,int userId)
		{
			string sql="select count(*) from grid where gridcode='"+code+"'";
			try
			{
				if(Convert.ToInt32(DataAccess.ExecuteScalar(sql,null))>0)
				{
					return true;
				}
				return false;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "CheckGridCode");
				throw;
			}
		}
	}
}
