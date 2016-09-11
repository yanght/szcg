using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// ����Ⱥ���ϵ���ݲ����ࡣ
	/// </summary>
	public class CarGroupRelation
	{
		private const string SQL_SELECT = "SELECT * FROM CarGroupRelation";
		private const string SQL_INSERT = "INSERT INTO CarGroupRelation(CarId, CarGroupId) VALUES(@CarId, @CarGroupId)";
		private const string SQL_DELETE = "DELETE FROM CarGroupRelation";

		private const string PART_RELATION_ID = "RelationId = @RelationId";
		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_CAR_GROUP_ID = "CarGroupId = @CarGroupId";

		private const string PARM_RELATION_ID = "@RelationId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_CAR_GROUP_ID = "@CarGroupId";

		public CarGroupRelation() {}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <param name="intRelationId">��ϵ��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarGroupId">����Ⱥ���ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����Ⱥ���ϵ��</returns>
		public SystemDs.CarGroupRelationDataTable Select(int intRelationId, int intCarId, int intCarGroupId)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region Ϊִ��׼����ѯ��估����

			if (intRelationId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_RELATION_ID);
				arrParms.Add(new SqlParameter(PARM_RELATION_ID, intRelationId));
			}
			if (intCarId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_ID, intCarId));
			}
			if (intCarGroupId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_GROUP_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarGroupRelationDataTable dataTable = new SystemDs.CarGroupRelationDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ȡ���С�
		/// </summary>
		/// <returns>����Ⱥ���ϵ��</returns>
		public SystemDs.CarGroupRelationDataTable SelectAll()
		{
			return this.Select(-1, -1, -1);
		}

		/// <summary>
		/// ��ѯ(���ݳ�����ʶ)��
		/// </summary>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����Ⱥ���ϵ��</returns>
		public SystemDs.CarGroupRelationDataTable SelectByCarId(int intCarId)
		{
			return this.Select(-1, intCarId, -1);
		}

		/// <summary>
		/// ��ѯ(����Ⱥ���ʶ)��
		/// </summary>
		/// <param name="intDriverId">Ⱥ���ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����Ⱥ���ϵ��</returns>
		public SystemDs.CarGroupRelationDataTable SelectByCarGroupId(int intGroupId)
		{
			return this.Select(-1, -1, intGroupId);
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="intCarId">������ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="intCarGroupId">Ⱥ���ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(int intCarId, int intCarGroupId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_INSERT, 
				new SqlParameter(PARM_CAR_ID, intCarId), 
				new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ����
		/// </summary>
		/// <param name="trans">��������������ʹ���������� null ��</param>
		/// <param name="intRelationId">��ϵ��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarGroupId">Ⱥ���ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		private bool Delete(SqlTransaction trans, int intRelationId, int intCarId, int intCarGroupId)
		{
			string strDelete = SQL_DELETE;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region Ϊִ��׼����ѯ��估����

			if (intRelationId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_RELATION_ID);
				arrParms.Add(new SqlParameter(PARM_RELATION_ID, intRelationId));
			}
			if (intCarId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_CAR_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_ID, intCarId));
			}
			if (intCarGroupId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_CAR_GROUP_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			int intRetVal;
			if (trans == null)
				intRetVal = (new SQLHelper()).ExecuteNonQuery(strDelete, parms);
			else
				intRetVal = (new SQLHelper()).ExecuteNonQuery(trans, strDelete, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��(���ݳ�����ʶ)��
		/// </summary>
		/// <param name="intCarId">������ʶ(ֵ����Ϊ����)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByCarId(int intCarId)
		{
			if (intCarId < 0)
				throw new Exception("������ʶ��ֵ����Ϊ������");
			else
				return this.Delete(null, -1, intCarId, -1);
		}

		/// <summary>
		/// ɾ��(���ݳ�����ʶ)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="intCarId">������ʶ(ֵ����Ϊ����)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByCarId(SqlTransaction trans, int intCarId)
		{
			if (intCarId < 0)
				throw new Exception("������ʶ��ֵ����Ϊ������");
			else
				return this.Delete(trans, -1, intCarId, -1);
		}

		/// <summary>
		/// ɾ��(����Ⱥ���ʶ)��
		/// </summary>
		/// <param name="intCarGroupId">Ⱥ���ʶ(ֵ����Ϊ����)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByCarGroupId(int intCarGroupId)
		{
			if (intCarGroupId < 0)
				throw new Exception("����Ⱥ���ʶ��ֵ����Ϊ������");
			else
				return this.Delete(null, -1, -1, intCarGroupId);
		}

		/// <summary>
		/// ɾ��(����Ⱥ���ʶ)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="intCarGroupId">Ⱥ���ʶ(ֵ����Ϊ��)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByCarGroupId(SqlTransaction trans, int intCarGroupId)
		{
			if (intCarGroupId < 0)
				throw new Exception("����Ⱥ���ʶ��ֵ����Ϊ������");
			else
				return this.Delete(trans, -1, -1, intCarGroupId);
		}
	}
}
