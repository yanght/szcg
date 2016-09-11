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
	/// 司机数据操作类。
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
		/// 查询(不包含照片内容)。
		/// </summary>
		/// <param name="intDriverId">司机标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strName">司机姓名，若不想加入该条件请设 null 。</param>
		/// <param name="intSex">司机性别，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dttMinBirthday">最小出生日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxBirthday">最大出生日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="strPhotoType">照片类型，若不想加入该条件请设 null 。</param>
		/// <param name="intMinPhotoLength">最小照片长度，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intMaxPhotoLength">最大照片长度，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strPhone">司机电话，若不想加入该条件请设 null 。</param>
		/// <param name="strDescription">司机描述，若不想加入该条件请设 null 。</param>
		/// <param name="dttMinRegisterTime">最小注册时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxRegisterTime">最大注册时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <returns>司机信息表</returns>
		public SystemDs.DriverInfoDataTable Select1(int intDriverId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
		{
			string strSQL = SQL_SELECT1;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();
			DateTime initialDateTime = new DateTime();

			#region 为执行准备查询语句及参数

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
        /// 根据部门departcode查询司机信息(不包含照片内容)。
        /// </summary>
        public SystemDs.DriverInfoDataTable Select2(int intId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
        {
            string strSQL = SQL_SELECT2;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();
            DateTime initialDateTime = new DateTime();

            #region 为执行准备查询语句及参数

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


        #region 环卫司机查询
        public SystemDs.DriverInfoDataTable Select(int intDriverId, string strName, int intSex, DateTime dttMinBirthday, DateTime dttMaxBirthday, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strPhone, string strDescription, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
        {
            string strSQL = SQL_SELECT;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();
            DateTime initialDateTime = new DateTime();

            #region 为执行准备查询语句及参数

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
		/// 获取所有。
		/// </summary>
		/// <returns>司机信息表</returns>
		public SystemDs.DriverInfoDataTable SelectAll()
		{
			return this.Select(-1, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// 查询(依据司机标识)。
		/// </summary>
		/// <param name="intDriverId">司机标识</param>
		/// <returns>司机信息表</returns>
		public SystemDs.DriverInfoDataTable SelectByDriverId(int intDriverId)
		{
			return this.Select(intDriverId, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
		}

		/// <summary>
		/// 查询(依据司机名称)。
		/// </summary>
		/// <param name="strName">司机名称</param>
		/// <returns>司机信息</returns>
		public SystemDs.DriverInfoDataTable SelectByDriverName(string strName)
		{
			return this.Select(-1, strName, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #region 依据司机ID查询
        public SystemDs.DriverInfoDataTable SelectByDriverName1(string strName,int driverid)
        {
            return this.Select1(driverid, strName, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #endregion

        /// <summary>
        /// 依据部门departid查询司机
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="driverid"></param>
        /// <returns></returns>
        #region 依据司机ID查询
        public SystemDs.DriverInfoDataTable SelectBydepartid(string strName, int Intid)
        {
            return this.Select2(Intid, null, -1, new DateTime(), new DateTime(), null, -1, -1, null, null, new DateTime(), new DateTime());
        }
        #endregion


        /// <summary>
        /// 分页查询。
        /// </summary>
        /// <param name="intSexId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
        /// <param name="strName">群组名称，若不想加入该条件请设 null 。</param>
        /// <param name="strDescription">群组描述，若不想加入该条件请设 null 。</param>
        /// <returns>车辆群组信息表</returns>
        public QueryUtil Select_Of_Page(int Intid, string strName, int pageindex, int pagesize)
        {
            //调试
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
        /// 分页查询。
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
		/// 判断司机名称已存在
		/// </summary>
		/// <param name="strName">司机名称</param>
		/// <returns>是否已存在</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(SQL_SELECT_NAME_EXIST, new SqlParameter(PARM_NAME, strName));

			if (intCount > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 查询司机照片(依据司机标识)
		/// </summary>
		/// <param name="intDriverId">司机标识</param>
		/// <returns>司机信息表</returns>
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
		/// 插入。
		/// </summary>
		/// <param name="strName">司机姓名，若不想插入值请设 null 。</param>
		/// <param name="intSex">司机性别，若不想插入值请设负值，如 -1 。</param>
		/// <param name="dttBirthday">司机生日，若不想插入值请设 new DateTime() 。</param>
		/// <param name="bytPhoto">照片内容，若该数组的长度为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="strPhotoType">照片类型，若该值为 null 则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="intPhotoLength">照片长度，若该值为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="strPhone">司机电话，若不想插入值请设 null 。</param>
		/// <param name="strDescription">司机描述，若不想插入值请设 null 。</param>
		/// <param name="dttRegisterTime">注册时间，若不想插入值请设 new DateTime() 。</param>
		/// <returns>执行是否成功</returns>
		public bool Insert(string strName, int intSex, DateTime dttBirthday, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strPhone, string strDescription, DateTime dttRegisterTime)
		{
			#region 为执行准备参数

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
        /// 插入。
        /// </summary>
        /// <param name="strName">司机姓名，若不想插入值请设 null 。</param>
        /// <param name="intSex">司机性别，若不想插入值请设负值，如 -1 。</param>
        /// <param name="dttBirthday">司机生日，若不想插入值请设 new DateTime() 。</param>
        /// <param name="bytPhoto">照片内容，若该数组的长度为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="strPhotoType">照片类型，若该值为 null 则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="intPhotoLength">照片长度，若该值为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="strPhone">司机电话，若不想插入值请设 null 。</param>
        /// <param name="strDescription">司机描述，若不想插入值请设 null 。</param>
        /// <param name="dttRegisterTime">注册时间，若不想插入值请设 new DateTime() 。</param>
        /// 
        /// <returns>执行是否成功</returns>
        public bool Insert1(string strName, int intSex, DateTime dttBirthday, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strPhone, string strDescription, DateTime dttRegisterTime,int Intdepartcode)
        {
            #region 为执行准备参数

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
		/// 更新(依据司机标识)(不更新照片)。
		/// </summary>
		/// <param name="intDriverId">司机标识</param>
		/// <param name="strName">司机姓名，若想清空请设 null 。</param>
		/// <param name="intSex">司机性别，若想清空请设负值，如 -1 。</param>
		/// <param name="dttBirthday">司机生日，若想清空请设 new DateTime() 。</param>
		/// <param name="strPhone">司机电话，若想清空请设 null 。</param>
		/// <param name="strDescription">司机描述，若想清空请设 null 。</param>
		/// <returns>执行是否成功</returns>
		public bool Update(int intDriverId, string strName, int intSex, DateTime dttBirthday, string strPhone, string strDescription)
		{
			#region 为执行准备参数

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
		/// 更新司机照片(依据司机标识)。
		/// </summary>
		/// <param name="intDriverId">司机标识</param>
		/// <param name="bytPhoto">照片内容，若想清空照片相关数据(照片内容、照片类型、照片长度)请将该数组的长度设为 0 。</param>
		/// <param name="strPhotoType">照片类型，若想清空照片相关数据(照片内容、照片类型、照片长度)请设 null 。</param>
		/// <param name="intPhotoLength">照片长度，若想清空照片相关数据(照片内容、照片类型、照片长度)请设负值，如 -1 。</param>
		/// <returns>执行是否成功</returns>
		public bool UpdatePhoto(int intDriverId, byte[] bytPhoto, string strPhotoType, int intPhotoLength)
		{
			#region 为执行准备参数

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
		/// 删除(依据司机标识)。
		/// </summary>
		/// <param name="intDriverId">司机标识</param>
		/// <returns>执行是否成功</returns>
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
