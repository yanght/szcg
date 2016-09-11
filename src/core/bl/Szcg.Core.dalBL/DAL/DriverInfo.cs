using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL;
using Teamax.Common;
using bacgDL.Pub;
using bacgDL;

namespace SZCG.GPS.DAL 
{
	/// <summary>
	/// ˾�����ݲ����ࡣ
	/// </summary>
	public class DriverInfo : CommonPage
	{
        private const string SQL_SELECT = "SELECT DriverId, [Name], Sex, isnull(Convert(varchar(10),Birthday,120),'') as Birthday, PhotoType, PhotoLength, Phone, [Description], RegisterTime FROM DriverInfo WHERE Area LIKE @Area + '%'";
        private const string SQL_SELECT1 = "SELECT DriverId, [Name], Sex, isnull(Convert(varchar(10),Birthday,120),'') as Birthday, PhotoType, PhotoLength, Phone, [Description], RegisterTime FROM DriverInfo WHERE Area LIKE @Area + '%' AND DriverId=@DriverId";
        private const string SQL_SELECT2 = "SELECT DriverId, [Name], Sex, isnull(Convert(varchar(10),Birthday,120),'') as Birthday, PhotoType, PhotoLength, Phone, [Description], RegisterTime FROM DriverInfo WHERE departcode=@departcode";
		private const string SQL_SELECT_PHOTO = "SELECT Photo, PhotoType, PhotoLength FROM DriverInfo WHERE DriverId = @DriverId";
		private const string SQL_SELECT_NAME_EXIST = "SELECT COUNT(*) FROM DriverInfo WHERE [Name] = @Name";
		private const string SQL_INSERT = "INSERT INTO DriverInfo(Area, [Name], Sex, Birthday, Photo, PhotoType, PhotoLength, Phone, [Description], RegisterTime) VALUES(@Area, @Name, @Sex, @Birthday, @Photo, @PhotoType, @PhotoLength, @Phone, @Description, @RegisterTime)";
        private const string SQL_INSERT1 = "INSERT INTO DriverInfo(Area, [Name], Sex, Birthday, Photo, PhotoType, PhotoLength, Phone, [Description], RegisterTime,departcode) VALUES(@Area, @Name, @Sex, @Birthday, @Photo, @PhotoType, @PhotoLength, @Phone, @Description, @RegisterTime,@departcode)";
		private const string SQL_UPDATE = "UPDATE DriverInfo SET [Name] = @Name, Sex = @Sex, Birthday = @Birthday, Phone = @Phone, [Description] = @Description WHERE DriverId = @DriverId";
		private const string SQL_UPDATE_PHOTO = "UPDATE DriverInfo SET Photo = @Photo, PhotoType = @PhotoType, PhotoLength = @PhotoLength WHERE DriverId = @DriverId";
		private const string SQL_DELETE = "DELETE DriverInfo WHERE DriverId = @DriverId";

		private const string PART_DRIVER_ID = "DriverId = @DriverId";
		private const string PART_NAME = "[Name] LIKE '%' + @Name + '%'";
		private const string PART_SEX = "Sex = @Sex";
		private const string PART_BIRTHDAY = "Birthday >= @MinBirthday AND Birthday <= @MaxBirthday";
		private const string PART_PHOTO_TYPE = "PhotoType LIKE '%' + @PhotoType + '%'";
		private const string PART_PHOTO_LENGTH = "PhotoLength >= @MinPhotoLength AND PhotoLength <= @MaxPhotoLength";
		private const string PART_PHONE = "Phone LIKE '%' + @Phone + '%'";
		private const string PART_DESCRIPTION = "[Description] LIKE '%' + @Description + '%'";
		private const string PART_REGISTER_TIME = "RegisterTime >= @MinRegisterTime AND RegisterTime <= @MaxRegisterTime";

		private const string PARM_AREA = "@Area";
		private const string PARM_DRIVER_ID = "@DriverId";
		private const string PARM_NAME = "@Name";
		private const string PARM_SEX = "@Sex";
		private const string PARM_BIRTHDAY = "@Birthday";
		private const string PARM_MIN_BIRTHDAY = "@MinBirthday";
		private const string PARM_MAX_BIRTHDAY = "@MaxBirthday";
		private const string PARM_PHOTO = "@Photo";
		private const string PARM_PHOTO_TYPE = "@PhotoType";
		private const string PARM_PHOTO_LENGTH = "@PhotoLength";
		private const string PARM_MIN_PHOTO_LENGTH = "@MinPhotoLength";
		private const string PARM_MAX_PHOTO_LENGTH = "@MaxPhotoLength";
		private const string PARM_PHONE = "@Phone";
		private const string PARM_DESCRIPTION = "@Description";
		private const string PARM_REGISTER_TIME = "@RegisterTime";
		private const string PARM_MIN_REGISTER_TIME = "@MinRegisterTime";
		private const string PARM_MAX_REGISTER_TIME = "@MaxRegisterTime";

		private string strArea;

		public DriverInfo()	
		{
			strArea = this.AreaCode;
		}

		/// <summary>
		/// ��ѯ(��������Ƭ����)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ�������������������踺ֵ���� -1 ��</param>
		/// <param name="strName">˾�������������������������� null ��</param>
		/// <param name="intSex">˾���Ա������������������踺ֵ���� -1 ��</param>
		/// <param name="dttMinBirthday">��С�������ڣ������������������� new DateTime() ��</param>
		/// <param name="dttMaxBirthday">���������ڣ������������������� new DateTime() ��</param>
		/// <param name="strPhotoType">��Ƭ���ͣ������������������� null ��</param>
		/// <param name="intMinPhotoLength">��С��Ƭ���ȣ������������������踺ֵ���� -1 ��</param>
		/// <param name="intMaxPhotoLength">�����Ƭ���ȣ������������������踺ֵ���� -1 ��</param>
		/// <param name="strPhone">˾���绰�������������������� null ��</param>
		/// <param name="strDescription">˾�������������������������� null ��</param>
		/// <param name="dttMinRegisterTime">��Сע��ʱ�䣬������������������ new DateTime() ��</param>
		/// <param name="dttMaxRegisterTime">���ע��ʱ�䣬������������������ new DateTime() ��</param>
		/// <returns>˾����Ϣ��</returns>
		public SystemDs.DriverInfoDataTable Select1(int intDriverId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
		{
			string strSQL = SQL_SELECT1;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime initialDateTime = new DateTime();

			#region Ϊִ��׼����ѯ��估����

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
			if (intDriverId >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DRIVER_ID);
				arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
			}
			if (strName != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_NAME);
				arrParms.Add(new SqlParameter(PARM_NAME, strName));
			}
			if (intSex >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_SEX);
				arrParms.Add(new SqlParameter(PARM_SEX, intSex));
			}

			if (dttMinBirthday != initialDateTime || dttMaxBirthday != initialDateTime)
			{
				if (dttMinBirthday == initialDateTime)
					dttMinBirthday = DateTime.MinValue;
				if (dttMaxBirthday == initialDateTime)
					dttMaxBirthday = DateTime.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_BIRTHDAY);
				arrParms.Add(new SqlParameter(PARM_MIN_BIRTHDAY, dttMinBirthday));
				arrParms.Add(new SqlParameter(PARM_MAX_BIRTHDAY, dttMaxBirthday));
			}

			if (strPhotoType != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_TYPE);
				arrParms.Add(new SqlParameter(PARM_PHOTO_TYPE, strPhotoType));
			}

			if (intMinPhotoLength >= 0 || intMaxPhotoLength >= 0)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_LENGTH);
				arrParms.Add(new SqlParameter(PARM_MIN_PHOTO_LENGTH, intMinPhotoLength));
				arrParms.Add(new SqlParameter(PARM_MAX_PHOTO_LENGTH, intMaxPhotoLength));
			}

			if (strPhone != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_PHONE);
				arrParms.Add(new SqlParameter(PARM_PHONE, strPhone));
			}

			if (strDescription != null)
			{
				strSQL = sqlUtility.AddQualification(strSQL, PART_DESCRIPTION);
				arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
			}

			if (dttMinRegisterTime != initialDateTime || dttMaxRegisterTime != initialDateTime)
			{
				if (dttMinRegisterTime == initialDateTime)
					dttMinRegisterTime = DateTime.MinValue;
				if (dttMaxRegisterTime == initialDateTime)
					dttMaxRegisterTime = DateTime.MaxValue;
				strSQL = sqlUtility.AddQualification(strSQL, PART_REGISTER_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_REGISTER_TIME, dttMinRegisterTime));
				arrParms.Add(new SqlParameter(PARM_MAX_REGISTER_TIME, dttMaxRegisterTime));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.DriverInfoDataTable dataTable = new SystemDs.DriverInfoDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

			return dataTable;
        }

        /// <summary>
        /// ���ݲ���departcode��ѯ˾����Ϣ(��������Ƭ����)��
        /// </summary>
        public SystemDs.DriverInfoDataTable Select2(int intId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
        {
            string strSQL = SQL_SELECT2;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();
            DateTime initialDateTime = new DateTime();

            #region Ϊִ��׼����ѯ��估����

            //arrParms.Add(new SqlParameter(PARM_AREA, strArea));
            if (intId >= 0)
            {
               // strSQL = sqlUtility.AddQualification(strSQL, "departcode = @departcode");
                arrParms.Add(new SqlParameter("@departcode", intId));
            }
            if (strName != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_NAME);
                arrParms.Add(new SqlParameter(PARM_NAME, strName));
            }
            if (intSex >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_SEX);
                arrParms.Add(new SqlParameter(PARM_SEX, intSex));
            }

            if (dttMinBirthday != initialDateTime || dttMaxBirthday != initialDateTime)
            {
                if (dttMinBirthday == initialDateTime)
                    dttMinBirthday = DateTime.MinValue;
                if (dttMaxBirthday == initialDateTime)
                    dttMaxBirthday = DateTime.MaxValue;
                strSQL = sqlUtility.AddQualification(strSQL, PART_BIRTHDAY);
                arrParms.Add(new SqlParameter(PARM_MIN_BIRTHDAY, dttMinBirthday));
                arrParms.Add(new SqlParameter(PARM_MAX_BIRTHDAY, dttMaxBirthday));
            }

            if (strPhotoType != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_TYPE);
                arrParms.Add(new SqlParameter(PARM_PHOTO_TYPE, strPhotoType));
            }

            if (intMinPhotoLength >= 0 || intMaxPhotoLength >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_LENGTH);
                arrParms.Add(new SqlParameter(PARM_MIN_PHOTO_LENGTH, intMinPhotoLength));
                arrParms.Add(new SqlParameter(PARM_MAX_PHOTO_LENGTH, intMaxPhotoLength));
            }

            if (strPhone != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHONE);
                arrParms.Add(new SqlParameter(PARM_PHONE, strPhone));
            }

            if (strDescription != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_DESCRIPTION);
                arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
            }

            if (dttMinRegisterTime != initialDateTime || dttMaxRegisterTime != initialDateTime)
            {
                if (dttMinRegisterTime == initialDateTime)
                    dttMinRegisterTime = DateTime.MinValue;
                if (dttMaxRegisterTime == initialDateTime)
                    dttMaxRegisterTime = DateTime.MaxValue;
                strSQL = sqlUtility.AddQualification(strSQL, PART_REGISTER_TIME);
                arrParms.Add(new SqlParameter(PARM_MIN_REGISTER_TIME, dttMinRegisterTime));
                arrParms.Add(new SqlParameter(PARM_MAX_REGISTER_TIME, dttMaxRegisterTime));
            }

            #endregion

            SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

            SystemDs.DriverInfoDataTable dataTable = new SystemDs.DriverInfoDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

            return dataTable;
        }


        #region ����˾����ѯ
        public SystemDs.DriverInfoDataTable Select(int intDriverId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
        {
            string strSQL = SQL_SELECT;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();
            DateTime initialDateTime = new DateTime();

            #region Ϊִ��׼����ѯ��估����

            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
            if (intDriverId >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_DRIVER_ID);
                arrParms.Add(new SqlParameter(PARM_DRIVER_ID, intDriverId));
            }
            if (strName != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_NAME);
                arrParms.Add(new SqlParameter(PARM_NAME, strName));
            }
            if (intSex >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_SEX);
                arrParms.Add(new SqlParameter(PARM_SEX, intSex));
            }

            if (dttMinBirthday != initialDateTime || dttMaxBirthday != initialDateTime)
            {
                if (dttMinBirthday == initialDateTime)
                    dttMinBirthday = DateTime.MinValue;
                if (dttMaxBirthday == initialDateTime)
                    dttMaxBirthday = DateTime.MaxValue;
                strSQL = sqlUtility.AddQualification(strSQL, PART_BIRTHDAY);
                arrParms.Add(new SqlParameter(PARM_MIN_BIRTHDAY, dttMinBirthday));
                arrParms.Add(new SqlParameter(PARM_MAX_BIRTHDAY, dttMaxBirthday));
            }

            if (strPhotoType != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_TYPE);
                arrParms.Add(new SqlParameter(PARM_PHOTO_TYPE, strPhotoType));
            }

            if (intMinPhotoLength >= 0 || intMaxPhotoLength >= 0)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHOTO_LENGTH);
                arrParms.Add(new SqlParameter(PARM_MIN_PHOTO_LENGTH, intMinPhotoLength));
                arrParms.Add(new SqlParameter(PARM_MAX_PHOTO_LENGTH, intMaxPhotoLength));
            }

            if (strPhone != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_PHONE);
                arrParms.Add(new SqlParameter(PARM_PHONE, strPhone));
            }

            if (strDescription != null)
            {
                strSQL = sqlUtility.AddQualification(strSQL, PART_DESCRIPTION);
                arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
            }

            if (dttMinRegisterTime != initialDateTime || dttMaxRegisterTime != initialDateTime)
            {
                if (dttMinRegisterTime == initialDateTime)
                    dttMinRegisterTime = DateTime.MinValue;
                if (dttMaxRegisterTime == initialDateTime)
                    dttMaxRegisterTime = DateTime.MaxValue;
                strSQL = sqlUtility.AddQualification(strSQL, PART_REGISTER_TIME);
                arrParms.Add(new SqlParameter(PARM_MIN_REGISTER_TIME, dttMinRegisterTime));
                arrParms.Add(new SqlParameter(PARM_MAX_REGISTER_TIME, dttMaxRegisterTime));
            }

            #endregion

            SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

            SystemDs.DriverInfoDataTable dataTable = new SystemDs.DriverInfoDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSQL, parms);

            return dataTable;
        }
        #endregion
        /// <summary>
		/// ��ȡ���С�
		/// </summary>
		/// <returns>˾����Ϣ��</returns>
		public SystemDs.DriverInfoDataTable SelectAll()
		{
			return this.Select(-1, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// ��ѯ(����˾����ʶ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <returns>˾����Ϣ��</returns>
		public SystemDs.DriverInfoDataTable SelectByDriverId(int intDriverId)
		{
			return this.Select(intDriverId, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// ��ѯ(����˾������)��
		/// </summary>
		/// <param name="strName">˾������</param>
		/// <returns>˾����Ϣ</returns>
		public SystemDs.DriverInfoDataTable SelectByDriverName(string strName)
		{
			return this.Select(-1, strName, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #region ����˾��ID��ѯ
        public SystemDs.DriverInfoDataTable SelectByDriverName1(string strName,int driverid)
        {
            return this.Select1(driverid, strName, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #endregion

        /// <summary>
        /// ���ݲ���departid��ѯ˾��
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="driverid"></param>
        /// <returns></returns>
        #region ����˾��ID��ѯ
        public SystemDs.DriverInfoDataTable SelectBydepartid(string strName, int Intid)
        {
            return this.Select2(Intid, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #endregion


        /// <summary>
        /// ��ҳ��ѯ��
        /// </summary>
        /// <param name="intSexId">����Ⱥ���ʶ�������������������踺ֵ���� -1 ��</param>
        /// <param name="strName">Ⱥ�����ƣ������������������� null ��</param>
        /// <param name="strDescription">Ⱥ�������������������������� null ��</param>
        /// <returns>����Ⱥ����Ϣ��</returns>
        public QueryUtil Select_Of_Page(int Intid, string strName, int pageindex, int pagesize)
        {
            //����
            string strselect = " DriverId, [Name], Sex, isnull(Convert(varchar(10),Birthday,120),'') as Birthday, PhotoType, PhotoLength, Phone, [Description], RegisterTime";
            string strfrom = " DriverInfo ";
            string strwhere = " 1=1 and  Area LIKE '" + this.AreaCode + "%'";

            if (Intid >= 0)
            {
                strwhere = strwhere + " and departcode =" + Convert.ToString(Intid);
            }
            if (strName != null)
            {
                strwhere = strwhere + " and [Name] LIKE '%" + strName + "%'";
            }


            QueryUtil qu = new QueryUtil(strselect, strfrom, strwhere);
            qu.Key = "DriverId";
            qu.SortBy = "DriverId";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize = pagesize;
            qu.ExecuteDataset(pageindex, "CONN_GPS_STRING");
            return qu;
        }

        /// <summary>
        /// ��ҳ��ѯ��
        /// </summary>
        public bacgDL.PageManage Select_V2(int Intid, string strName, int pageindex, int pagesize)
        {
            bacgDL.PageManage pm = new bacgDL.PageManage();
            QueryUtil qu = Select_Of_Page(Intid , strName , pageindex , pagesize ) ;
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }


        /// <summary>
		/// �ж�˾�������Ѵ���
		/// </summary>
		/// <param name="strName">˾������</param>
		/// <returns>�Ƿ��Ѵ���</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// ��ѯ˾����Ƭ(����˾����ʶ)
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <returns>˾����Ϣ��</returns>
		public SystemDs.DriverInfoDataTable SelectPhoto(int intDriverId)
		{
			SystemDs.DriverInfoDataTable dataTable = new SystemDs.DriverInfoDataTable();

			(new SQLHelper()).ExecuteDataTable(
				dataTable, 
				SQL_SELECT_PHOTO, 
				new SqlParameter(PARM_DRIVER_ID, intDriverId));

			return dataTable;
		}

		/// <summary>
		/// ���롣
		/// </summary>
		/// <param name="strName">˾�����������������ֵ���� null ��</param>
		/// <param name="intSex">˾���Ա����������ֵ���踺ֵ���� -1 ��</param>
		/// <param name="dttBirthday">˾�����գ����������ֵ���� new DateTime() ��</param>
		/// <param name="bytPhoto">��Ƭ���ݣ���������ĳ���Ϊ������ 0 �����򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
		/// <param name="strPhotoType">��Ƭ���ͣ�����ֵΪ null �򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
		/// <param name="intPhotoLength">��Ƭ���ȣ�����ֵΪ������ 0 �����򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
		/// <param name="strPhone">˾���绰�����������ֵ���� null ��</param>
		/// <param name="strDescription">˾�����������������ֵ���� null ��</param>
		/// <param name="dttRegisterTime">ע��ʱ�䣬���������ֵ���� new DateTime() ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Insert(string strName, int intSex, DateTime dttBirthday, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strPhone, string strDescription, DateTime dttRegisterTime)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[10];

			if (strArea == null)
				parms[0] = new SqlParameter(PARM_AREA, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_AREA, strArea);

			if (strName == null)
				parms[1] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[1] = new SqlParameter(PARM_NAME, strName);

			if (intSex < 0)
				parms[2] = new SqlParameter(PARM_SEX, DBNull.Value);
			else
				parms[2] = new SqlParameter(PARM_SEX, intSex);

			if (dttBirthday == (new DateTime()))
				parms[3] = new SqlParameter(PARM_BIRTHDAY, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_BIRTHDAY, dttBirthday);

			if (bytPhoto.Length <= 0 || strPhotoType == null || intPhotoLength <= 0)
			{
				parms[4] = new SqlParameter(PARM_PHOTO, new byte[0]);
				parms[5] = new SqlParameter(PARM_PHOTO_TYPE, DBNull.Value);
				parms[6] = new SqlParameter(PARM_PHOTO_LENGTH, DBNull.Value);
			}
			else
			{
				parms[4] = new SqlParameter(PARM_PHOTO, bytPhoto);
				parms[5] = new SqlParameter(PARM_PHOTO_TYPE, strPhotoType);
				parms[6] = new SqlParameter(PARM_PHOTO_LENGTH, intPhotoLength);
			}

			if (strPhone == null)
				parms[7] = new SqlParameter(PARM_PHONE, DBNull.Value);
			else
				parms[7] = new SqlParameter(PARM_PHONE, strPhone);

			if (strDescription == null)
				parms[8] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[8] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			if (dttRegisterTime == (new DateTime()))
				parms[9] = new SqlParameter(PARM_REGISTER_TIME, DBNull.Value);
			else
				parms[9] = new SqlParameter(PARM_REGISTER_TIME, dttRegisterTime);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
		}

        /// <summary>
        /// ���롣
        /// </summary>
        /// <param name="strName">˾�����������������ֵ���� null ��</param>
        /// <param name="intSex">˾���Ա����������ֵ���踺ֵ���� -1 ��</param>
        /// <param name="dttBirthday">˾�����գ����������ֵ���� new DateTime() ��</param>
        /// <param name="bytPhoto">��Ƭ���ݣ���������ĳ���Ϊ������ 0 �����򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
        /// <param name="strPhotoType">��Ƭ���ͣ�����ֵΪ null �򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
        /// <param name="intPhotoLength">��Ƭ���ȣ�����ֵΪ������ 0 �����򲻻������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)��</param>
        /// <param name="strPhone">˾���绰�����������ֵ���� null ��</param>
        /// <param name="strDescription">˾�����������������ֵ���� null ��</param>
        /// <param name="dttRegisterTime">ע��ʱ�䣬���������ֵ���� new DateTime() ��</param>
        /// 
        /// <returns>ִ���Ƿ�ɹ�</returns>
        public bool Insert1(string strName, int intSex, DateTime dttBirthday, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strPhone, string strDescription, DateTime dttRegisterTime,int Intdepartcode)
        {
            #region Ϊִ��׼������

            SqlParameter[] parms = new SqlParameter[11];

            if (strArea == null)
                parms[0] = new SqlParameter(PARM_AREA, DBNull.Value);
            else
                parms[0] = new SqlParameter(PARM_AREA, strArea);

            if (strName == null)
                parms[1] = new SqlParameter(PARM_NAME, DBNull.Value);
            else
                parms[1] = new SqlParameter(PARM_NAME, strName);

            if (intSex < 0)
                parms[2] = new SqlParameter(PARM_SEX, DBNull.Value);
            else
                parms[2] = new SqlParameter(PARM_SEX, intSex);

            if (dttBirthday == (new DateTime()))
                parms[3] = new SqlParameter(PARM_BIRTHDAY, DBNull.Value);
            else
                parms[3] = new SqlParameter(PARM_BIRTHDAY, dttBirthday);

            if (bytPhoto.Length <= 0 || strPhotoType == null || intPhotoLength <= 0)
            {
                parms[4] = new SqlParameter(PARM_PHOTO, new byte[0]);
                parms[5] = new SqlParameter(PARM_PHOTO_TYPE, DBNull.Value);
                parms[6] = new SqlParameter(PARM_PHOTO_LENGTH, DBNull.Value);
            }
            else
            {
                parms[4] = new SqlParameter(PARM_PHOTO, bytPhoto);
                parms[5] = new SqlParameter(PARM_PHOTO_TYPE, strPhotoType);
                parms[6] = new SqlParameter(PARM_PHOTO_LENGTH, intPhotoLength);
            }

            if (strPhone == null)
                parms[7] = new SqlParameter(PARM_PHONE, DBNull.Value);
            else
                parms[7] = new SqlParameter(PARM_PHONE, strPhone);

            if (strDescription == null)
                parms[8] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
            else
                parms[8] = new SqlParameter(PARM_DESCRIPTION, strDescription);

            if (dttRegisterTime == (new DateTime()))
                parms[9] = new SqlParameter(PARM_REGISTER_TIME, DBNull.Value);
            else
                parms[9] = new SqlParameter(PARM_REGISTER_TIME, dttRegisterTime);

            if (Intdepartcode == null)
                parms[10] = new SqlParameter("@departcode", DBNull.Value);
            else
                parms[10] = new SqlParameter("@departcode", Intdepartcode);

            #endregion

            int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT1, parms);

            return intRetVal > 0 ? true : false;
        }

		/// <summary>
		/// ����(����˾����ʶ)(��������Ƭ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <param name="strName">˾������������������� null ��</param>
		/// <param name="intSex">˾���Ա�����������踺ֵ���� -1 ��</param>
		/// <param name="dttBirthday">˾�����գ������������ new DateTime() ��</param>
		/// <param name="strPhone">˾���绰������������� null ��</param>
		/// <param name="strDescription">˾������������������� null ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Update(int intDriverId, string strName, int intSex, DateTime dttBirthday, string strPhone, string strDescription)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[6];

			if (strName == null)
				parms[0] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_NAME, strName);

			if (intSex < 0)
				parms[1] = new SqlParameter(PARM_SEX, DBNull.Value);
			else
				parms[1] = new SqlParameter(PARM_SEX, intSex);

			if (dttBirthday == (new DateTime()))
				parms[2] = new SqlParameter(PARM_BIRTHDAY, DBNull.Value);
			else
				parms[2] = new SqlParameter(PARM_BIRTHDAY, dttBirthday);

			if (strPhone == null)
				parms[3] = new SqlParameter(PARM_PHONE, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_PHONE, strPhone);

			if (strDescription == null)
				parms[4] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[4] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			parms[5] = new SqlParameter(PARM_DRIVER_ID, intDriverId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ����˾����Ƭ(����˾����ʶ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <param name="bytPhoto">��Ƭ���ݣ����������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)�뽫������ĳ�����Ϊ 0 ��</param>
		/// <param name="strPhotoType">��Ƭ���ͣ����������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)���� null ��</param>
		/// <param name="intPhotoLength">��Ƭ���ȣ����������Ƭ�������(��Ƭ���ݡ���Ƭ���͡���Ƭ����)���踺ֵ���� -1 ��</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool UpdatePhoto(int intDriverId, byte[] bytPhoto, string strPhotoType, int intPhotoLength)
		{
			#region Ϊִ��׼������

			SqlParameter[] parms = new SqlParameter[4];

			if (bytPhoto.Length <= 0 || strPhotoType == null || intPhotoLength <= 0)
			{
				parms[0] = new SqlParameter(PARM_PHOTO, new byte[0]);
				parms[1] = new SqlParameter(PARM_PHOTO_TYPE, DBNull.Value);
				parms[2] = new SqlParameter(PARM_PHOTO_LENGTH, DBNull.Value);
			}
			else
			{
				parms[0] = new SqlParameter(PARM_PHOTO, bytPhoto);
				parms[1] = new SqlParameter(PARM_PHOTO_TYPE, strPhotoType);
				parms[2] = new SqlParameter(PARM_PHOTO_LENGTH, intPhotoLength);
			}

			parms[3] = new SqlParameter(PARM_DRIVER_ID, intDriverId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE_PHOTO, parms);

			return intRetVal > 0 ? true : false;
		}

		/// <summary>
		/// ɾ��(����˾����ʶ)��
		/// </summary>
		/// <param name="intDriverId">˾����ʶ</param>
		/// <returns>ִ���Ƿ�ɹ�</returns>
		public bool Delete(int intDriverId)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						(new OilLog()).DeleteByDriverId(trans, intDriverId);
						(new MaintenanceLog()).DeleteByDriverId(trans, intDriverId);
						(new CarDriverRelation()).DeleteByDriverId(trans, intDriverId);
						int intRetVal = (new SQLHelper()).ExecuteNonQuery(
							trans, SQL_DELETE, new SqlParameter(PARM_DRIVER_ID, intDriverId)
							);
						trans.Commit();
						return intRetVal > 0 ? true : false;
					}
					catch
					{
						trans.Rollback();
						throw;
					}
				}
			}
		}
	}
}
