using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// ����˾����ϵ���ݲ����ࡣ
	/// </summary>
	public class CarDriverRelation
	{
		private const string SQL_SELECT = "SELECT * FROM CarDriverRelation";
		private const string SQL_INSERT = "INSERT INTO CarDriverRelation(CarId, DriverId) VALUES(@CarId, @DriverId)";
		private const string SQL_DELETE = "DELETE FROM CarDriverRelation";

		private const string PART_RELATION_ID = "RelationId = @RelationId";
		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_DRIVER_ID = "DriverId = @DriverId";

		private const string PARM_RELATION_ID = "@RelationId";
		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_DRIVER_ID = "@DriverId";

		public CarDriverRelation() {}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <param name="intRelationId">��ϵ��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intDriverId">˾����ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����˾����ϵ��</returns>
		public SystemDs.CarDriverRelationDataTable Select(int intRelationId, int intCarId, int intDriverId)
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
			if (intDriverId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarDriverRelationDataTable dataTable = new SystemDs.CarDriverRelationDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

			return dataTable;
		}

		/// <summary>
		/// ��ȡ���С�
		/// </summary>
		/// <returns>����˾����ϵ��</returns>
		public SystemDs.CarDriverRelationDataTable SelectAll()
		{
			return this.Select(-1, -1, -1);
		}

		/// <summary>
		/// ��ѯ(���ݳ�����ʶ)��
		/// </summary>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����˾����ϵ��</returns>
		public SystemDs.CarDriverRelationDataTable SelectByCarId(int intCarId)
		{
			return this.Select(-1, intCarId, -1);
		}

		/// <summary>
		/// ��ѯ(����˾����ʶ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ�������������������踺ֵ���� -1 ��</param>
		/// <returns>����˾����ϵ��</returns>
		public SystemDs.CarDriverRelationDataTable SelectByDriverId(int intDriverId)
		{
			return this.Select(-1, -1, intDriverId);
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="intCarId">������ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="intCarGroupId">˾����ʶ�����������ֵ���踺ֵ���� -1 ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(int intCarId, int intDriverId)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				SQL_INSERT, 
				new SqlParameter(PARM_CAR_ID, intCarId), 
				new SqlParameter(PARM_DRIVER_ID, intDriverId)
				);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ����
		/// </summary>
		/// <param name="intRelationId">��ϵ��ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intCarId">������ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="intDriverId">˾����ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="trans">��������������ʹ���������� null ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		private bool Delete(SqlTransaction trans, int intRelationId, int intCarId, int intDriverId)
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
			if (intDriverId >= 0)
			{
				strDelete = sqlUtility.AddQualification(strDelete, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
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
		/// ɾ��(����˾����ʶ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ(ֵ����Ϊ����)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByDriverId(int intDriverId)
		{
			if (intDriverId < 0)
				throw new Exception("˾����ʶ��ֵ����Ϊ������");
			else
				return this.Delete(null, -1, -1, intDriverId);
		}

		/// <summary>
		/// ɾ��(����˾����ʶ)��
		/// </summary>
		/// <param name="trans">��������</param>
		/// <param name="intDriverId">˾����ʶ(ֵ����Ϊ����)</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool DeleteByDriverId(SqlTransaction trans, int intDriverId)
		{
			if (intDriverId < 0)
				throw new Exception("˾����ʶ��ֵ����Ϊ������");
			else
				return this.Delete(trans, -1, -1, intDriverId);
		}
	}
}
