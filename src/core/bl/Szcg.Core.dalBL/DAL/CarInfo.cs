using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL.GPS;
using Teamax.Common;
using bacgDL.Pub;
using bacgDL;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// 车辆数据操作类。
	/// </summary>
    public class CarInfo : CommonPage
    {
        #region private ：　私有的常量
        private const string SQL_SELECT = "SELECT CarId, SIM, License, [Name], DepartmentId, isnull(CarStateId,-1) as CarStateId, PhotoType, PhotoLength, [Description], Type, Color, isnull(Prices,-1) as Prices, isnull(LoadNo,-1) as LoadNo, HaveInsurance, InsuranceType, InsuranceCompany,isnull(Convert(varchar(10),ProductionDate,120),'') as ProductionDate, isnull(Convert(varchar(10),PurchaseDate,120),'') as PurchaseDate, isnull(Convert(varchar(10),PutIntoUseDate,120),'') as PutIntoUseDate, RegisterTime FROM CarInfo WHERE Area LIKE @Area + '%'";
		private const string SQL_SELECT_PHOTO = "SELECT Photo, PhotoType, PhotoLength FROM CarInfo WHERE CarId = @CarId";
		private const string SQL_SELECT_NAME_EXIST = "SELECT COUNT(*) FROM CarInfo WHERE [Name] = @Name AND Area = @Area";
		private const string SQL_SELECT_SIM_EXIST = "SELECT COUNT(*) FROM CarInfo WHERE SIM = @SIM";
		private const string SQL_INSERT = "INSERT INTO CarInfo(Area, SIM, VirtualIP, License, [Name], DepartmentId, CarStateId, Photo, PhotoType, PhotoLength, [Description], Type, Color, Prices, LoadNo, HaveInsurance, InsuranceType, InsuranceCompany, ProductionDate, PurchaseDate, PutIntoUseDate, RegisterTime) VALUES(@Area, @SIM, @VirtualIP, @License, @Name, @DepartmentId, @CarStateId, @Photo, @PhotoType, @PhotoLength, @Description, @Type, @Color, @Prices, @LoadNo, @HaveInsurance, @InsuranceType, @InsuranceCompany, @ProductionDate, @PurchaseDate, @PutIntoUseDate, @RegisterTime)";
        private const string SQL_INSERTS = "INSERT INTO CarInfo(Area, SIM, VirtualIP, License, [Name], DepartmentId, CarStateId, Photo, PhotoType, PhotoLength, [Description], Type, Color, Prices, LoadNo, HaveInsurance, InsuranceType, InsuranceCompany, ProductionDate, PurchaseDate, PutIntoUseDate, RegisterTime) VALUES(@Area, @SIM, @VirtualIP, @License, @Name, @DepartmentId, @CarStateId, @Photo, @PhotoType, @PhotoLength, @Description, @Type, @Color, @Prices, @LoadNo, @HaveInsurance, @InsuranceType, @InsuranceCompany, @ProductionDate, @PurchaseDate, @PutIntoUseDate, @RegisterTime) where DepartmentId = @depart";
		private const string SQL_UPDATE = "UPDATE CarInfo SET SIM = @SIM, VirtualIP = @VirtualIP, License = @License, [Name] = @Name, DepartmentId = @DepartmentId, CarStateId = @CarStateId, [Description] = @Description, Type = @Type, Color = @Color, Prices = @Prices, LoadNo = @LoadNo, HaveInsurance = @HaveInsurance, InsuranceType = @InsuranceType, InsuranceCompany = @InsuranceCompany, ProductionDate = @ProductionDate, PurchaseDate = @PurchaseDate, PutIntoUseDate = @PutIntoUseDate WHERE CarId = @CarId";
        private const string SQL_UPDATE1 = "UPDATE CarInfo SET SIM = @SIM, VirtualIP = @VirtualIP, License = @License, [Name] = @Name, CarStateId = @CarStateId, [Description] = @Description, Type = @Type, Color = @Color, Prices = @Prices, LoadNo = @LoadNo, HaveInsurance = @HaveInsurance, InsuranceType = @InsuranceType, InsuranceCompany = @InsuranceCompany, ProductionDate = @ProductionDate, PurchaseDate = @PurchaseDate, PutIntoUseDate = @PutIntoUseDate WHERE CarId = @CarId";
        private const string SQL_UPDATE2 = "UPDATE CarInfo SET SIM = @SIM, VirtualIP = @VirtualIP, License = @License, [Name] = @Name, DepartmentId = @DepartmentId, CarStateId = @CarStateId, [Description] = @Description, Type = @Type, Color = @Color, Prices = @Prices, LoadNo = @LoadNo, HaveInsurance = @HaveInsurance, InsuranceType = @InsuranceType, InsuranceCompany = @InsuranceCompany, ProductionDate = @ProductionDate, PurchaseDate = @PurchaseDate, PutIntoUseDate = @PutIntoUseDate ,area = @area WHERE CarId = @CarId";
        private const string SQL_UPDATE_PHOTO = "UPDATE CarInfo SET Photo = @Photo, PhotoType = @PhotoType, PhotoLength = @PhotoLength WHERE CarId = @CarId";
		private const string SQL_UPDATE_CLEAR_CAR_STATE_ID = "UPDATE CarInfo SET CarStateId = null WHERE CarStateId = @CarStateId";
		private const string SQL_UPDATE_CLEAR_DEPARTMENT_ID = "UPDATE CarInfo SET DepartmentId = null WHERE DepartmentId = @DepartmentId";
		private const string SQL_DELETE = "DELETE FROM CarInfo WHERE CarId = @CarId";

		private const string PART_CAR_ID = "CarId = @CarId";
		private const string PART_AREA = "Area = @Area";
		private const string PART_SIM = "SIM LIKE '%' + @SIM + '%'";
		private const string PART_LICENSE = "License LIKE '%' + @License + '%'";
		private const string PART_NAME = "[Name] LIKE '%' + @Name + '%' ";
		private const string PART_DEPARTMENT_ID = "DepartmentId = @DepartmentId";
		private const string PART_CAR_STATE_ID = "CarStateId = @CarStateId";
		private const string PART_PHOTO_TYPE = "PhotoType LIKE '%' + @PhotoType + '%'";
		private const string PART_PHOTO_LENGTH = "PhotoLength >= @MinPhotoLength AND PhotoLength <= @MaxPhotoLength";
		private const string PART_DESCRIPTION = "[Description] LIKE '%' + @Description + '%'";
		private const string PART_TYPE = "Type LIKE '%' + @Type + '%'";
		private const string PART_COLOR = "Color LIKE '%' + @Color + '%'";
		private const string PART_PRICES = "Prices >= @MinPrices AND Prices <= @MaxPrices";
		private const string PART_LOAD_NO = "LoadNo >= @MinLoadNo AND LoadNo <= @MaxLoadNo";
		private const string PART_HAVE_INSURANCE = "HaveInsurance = @HaveInsurance";
		private const string PART_INSURANCE_TYPE = "InsuranceType LIKE '%' + @InsuranceType + '%'";
		private const string PART_INSURANCE_COMPANY = "InsuranceCompany LIKE '%' + @InsuranceCompany + '%'";
		private const string PART_PRODUCTION_DATE = "ProductionDate >= @MinProductionDate AND ProductionDate <= @MaxProductionDate";
		private const string PART_PURCHASE_DATE = "PurchaseDate >= @MinPurchaseDate AND PurchaseDate <= @MaxPurchaseDate";
		private const string PART_PUT_INTO_USE_DATE = "PutIntoUseDate >= @MinPutIntoUseDate AND PutIntoUseDate <= @MaxPutIntoUseDate";
		private const string PART_REGISTER_TIME = "RegisterTime >= @MinRegisterTime AND RegisterTime <= @MaxRegisterTime";
		private const string PART_CAR_GROUP_ID = "CarId IN (SELECT CarId FROM CarGroupRelation WHERE CarGroupId = @CarGroupId)";

		private const string PARM_CAR_ID = "@CarId";
		private const string PARM_AREA = "@Area";
		private const string PARM_SIM = "@SIM";
		private const string PARM_VIRTUAL_IP = "@VirtualIP";
		private const string PARM_LICENSE = "@License";
		private const string PARM_NAME = "@Name";
		private const string PARM_DEPARTMENT_ID = "@DepartmentId";
		private const string PARM_CAR_STATE_ID = "@CarStateId";
		private const string PARM_PHOTO = "@Photo";
		private const string PARM_PHOTO_TYPE = "@PhotoType";
		private const string PARM_PHOTO_LENGTH = "@PhotoLength";
		private const string PARM_MIN_PHOTO_LENGTH = "@MinPhotoLength";
		private const string PARM_MAX_PHOTO_LENGTH = "@MaxPhotoLength";
		private const string PARM_DESCRIPTION = "@Description";
		private const string PARM_TYPE = "@Type";
		private const string PARM_COLOR = "@Color";
		private const string PARM_PRICES = "@Prices";
		private const string PARM_MIN_PRICES = "@MinPrices";
		private const string PARM_MAX_PRICES = "@MaxPrices";
		private const string PARM_LOAD_NO = "@LoadNo";
		private const string PARM_MIN_LOAD_NO = "@MinLoadNo";
		private const string PARM_MAX_LOAD_NO = "@MaxLoadNo";
		private const string PARM_HAVE_INSURANCE = "@HaveInsurance";
		private const string PARM_INSURANCE_TYPE = "@InsuranceType";
		private const string PARM_INSURANCE_COMPANY = "@InsuranceCompany";
		private const string PARM_PRODUCTION_DATE = "@ProductionDate";
		private const string PARM_MIN_PRODUCTION_DATE = "@MinProductionDate";
		private const string PARM_MAX_PRODUCTION_DATE = "@MaxProductionDate";
		private const string PARM_PURCHASE_DATE = "@PurchaseDate";
		private const string PARM_MIN_PURCHASE_DATE = "@MinPurchaseDate";
		private const string PARM_MAX_PURCHASE_DATE = "@MaxPurchaseDate";
		private const string PARM_PUT_INTO_USE_DATE = "@PutIntoUseDate";
		private const string PARM_MIN_PUT_INTO_USE_DATE = "@MinPutIntoUseDate";
		private const string PARM_MAX_PUT_INTO_USE_DATE = "@MaxPutIntoUseDate";
		private const string PARM_REGISTER_TIME = "@RegisterTime";
		private const string PARM_MIN_REGISTER_TIME = "@MinRegisterTime";
		private const string PARM_MAX_REGISTER_TIME = "@MaxRegisterTime";
		private const string PARM_CAR_GROUP_ID = "@CarGroupId";
        private const string PARM_ENVIR_DEPART = "@depart";

		private string strArea;

        #endregion

        #region CarInfo ： 得到区域代码
        public CarInfo()
		{
			 strArea = this.AreaCode;
        }
        #endregion

        #region Select ： 车俩信息表
        /// <summary>
		/// 查询。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarGroupId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strSIM">SIM 卡号，若不想加入该条件请设 null 。</param>
		/// <param name="strLicense">车牌号码，若不想加入该条件请设 null 。</param>
		/// <param name="strName">车辆名称，若不想加入该条件请设 null 。</param>
		/// <param name="intDepartmentId">部门标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarStateId">状态标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strPhotoType">照片类型，若不想加入该条件请设 null 。</param>
		/// <param name="intMinPhotoLength">最小照片长度，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intMaxPhotoLength">最大照片长度，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strDescription">车辆描述，若不想加入该条件请设 null 。</param>
		/// <param name="strType">车辆型号，若不想加入该条件请设 null 。</param>
		/// <param name="strColor">车辆颜色，若不想加入该条件请设 null 。</param>
		/// <param name="dblMinPrices">最小价格，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMaxPrices">最大价格，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMinLoadNo">最小载重，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="dblMaxLoadNo">最大载重，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intHaveInsurance">是否保险，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strInsuranceType">保险类型，若不想加入该条件请设 null 。</param>
		/// <param name="strInsuranceCompany">保险公司，若不想加入该条件请设 null 。</param>
		/// <param name="dttMinProductionDate">最小生产日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxProductionDate">最大生产日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMinPurchaseDate">最小购买日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxPurchaseDate">最大购买日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMinPutIntoUseDate">最小投入使用日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxPutIntoUseDate">最大投入使用日期，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMinRegisterTime">最小注册时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <param name="dttMaxRegisterTime">最大注册时间，若不想加入该条件请设 new DateTime() 。</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable Select(int intCarId, int intCarGroupId, string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, string strPhotoType, int intMinPhotoLength, int intMaxPhotoLength, string strDescription, string strType, string strColor, double dblMinPrices, double dblMaxPrices, double dblMinLoadNo, double dblMaxLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dttMinProductionDate, DateTime dttMaxProductionDate, DateTime dttMinPurchaseDate, DateTime dttMaxPurchaseDate, DateTime dttMinPutIntoUseDate, DateTime dttMaxPutIntoUseDate, DateTime dttMinRegisterTime, DateTime dttMaxRegisterTime)
		{
			string strSelect = SQL_SELECT;
			ArrayList arrParms = new ArrayList();

			SQLUtility sqlUtility = new SQLUtility();

			#region 为执行准备查询语句及参数

			arrParms.Add(new SqlParameter(PARM_AREA, strArea));
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
			if (strSIM != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_SIM);
				arrParms.Add(new SqlParameter(PARM_SIM, strSIM));
			}
			if (strLicense != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_LICENSE);
				arrParms.Add(new SqlParameter(PARM_LICENSE, strLicense));
			}
			if (strName != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_NAME);
				arrParms.Add(new SqlParameter(PARM_NAME, strName));
			}
			if (intDepartmentId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_DEPARTMENT_ID);
				arrParms.Add(new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId));
			}
			if (intCarStateId >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_STATE_ID);
				arrParms.Add(new SqlParameter(PARM_CAR_STATE_ID, intCarStateId));
			}
			if (strPhotoType != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_PHOTO_TYPE);
				arrParms.Add(new SqlParameter(PARM_PHOTO_TYPE, strPhotoType));
			}
			if (intMinPhotoLength >= 0 || intMaxPhotoLength >= 0)
			{
				intMinPhotoLength = intMinPhotoLength < 0 ? int.MinValue : intMinPhotoLength;
				intMaxPhotoLength = intMaxPhotoLength < 0 ? int.MaxValue : intMaxPhotoLength;

				strSelect = sqlUtility.AddQualification(strSelect, PART_PHOTO_LENGTH);
				arrParms.Add(new SqlParameter(PARM_MIN_PHOTO_LENGTH, intMinPhotoLength));
				arrParms.Add(new SqlParameter(PARM_MAX_PHOTO_LENGTH, intMaxPhotoLength));
			}
			if (strDescription != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_DESCRIPTION);
				arrParms.Add(new SqlParameter(PARM_DESCRIPTION, strDescription));
			}
			if (strType != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_TYPE);
				arrParms.Add(new SqlParameter(PARM_TYPE, strType));
			}
			if (strColor != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_COLOR);
				arrParms.Add(new SqlParameter(PARM_COLOR, strColor));
			}
			if (dblMinPrices >= 0 || dblMaxPrices >= 0)
			{
				dblMinPrices = dblMinPrices < 0 ? double.MinValue : dblMinPrices;
				dblMaxPrices = dblMaxPrices < 0 ? double.MaxValue : dblMaxPrices;

				strSelect = sqlUtility.AddQualification(strSelect, PART_PRICES);
				arrParms.Add(new SqlParameter(PARM_MIN_PRICES, dblMinPrices));
				arrParms.Add(new SqlParameter(PARM_MAX_PRICES, dblMaxPrices));
			}
			if (dblMinLoadNo >= 0 || dblMaxLoadNo >= 0)
			{
				dblMinLoadNo = dblMinLoadNo < 0 ? double.MinValue : dblMinLoadNo;
				dblMaxLoadNo = dblMaxLoadNo < 0 ? double.MaxValue : dblMaxLoadNo;

				strSelect = sqlUtility.AddQualification(strSelect, PART_LOAD_NO);
				arrParms.Add(new SqlParameter(PARM_MIN_LOAD_NO, dblMinLoadNo));
				arrParms.Add(new SqlParameter(PARM_MAX_LOAD_NO, dblMaxLoadNo));
			}
			if (intHaveInsurance >= 0)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_HAVE_INSURANCE);
				arrParms.Add(new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance));
			}
			if (strInsuranceType != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_INSURANCE_TYPE);
				arrParms.Add(new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType));
			}
			if (strInsuranceCompany != null)
			{
				strSelect = sqlUtility.AddQualification(strSelect, PART_INSURANCE_COMPANY);
				arrParms.Add(new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany));
			}
			if (dttMinProductionDate != new DateTime() || dttMaxProductionDate != new DateTime())
			{
				dttMinProductionDate = dttMinProductionDate == new DateTime() ? DateTime.MinValue : dttMinProductionDate;
				dttMaxProductionDate = dttMaxProductionDate == new DateTime() ? DateTime.MaxValue : dttMaxProductionDate;

				strSelect = sqlUtility.AddQualification(strSelect, PART_PRODUCTION_DATE);
				arrParms.Add(new SqlParameter(PARM_MIN_PRODUCTION_DATE, dttMinProductionDate));
				arrParms.Add(new SqlParameter(PARM_MAX_PRODUCTION_DATE, dttMaxProductionDate));
			}
			if (dttMinPurchaseDate != new DateTime() || dttMaxPurchaseDate != new DateTime())
			{
				dttMinPurchaseDate = dttMinPurchaseDate == new DateTime() ? DateTime.MinValue : dttMinPurchaseDate;
				dttMaxPurchaseDate = dttMaxPurchaseDate == new DateTime() ? DateTime.MaxValue : dttMaxPurchaseDate;

				strSelect = sqlUtility.AddQualification(strSelect, PART_PURCHASE_DATE);
				arrParms.Add(new SqlParameter(PARM_MIN_PURCHASE_DATE, dttMinPurchaseDate));
				arrParms.Add(new SqlParameter(PARM_MAX_PURCHASE_DATE, dttMaxPurchaseDate));
			}
			if (dttMinPutIntoUseDate != new DateTime() || dttMaxPutIntoUseDate != new DateTime())
			{
				dttMinPutIntoUseDate = dttMinPutIntoUseDate == new DateTime() ? DateTime.MinValue : dttMinPutIntoUseDate;
				dttMaxPutIntoUseDate = dttMaxPutIntoUseDate == new DateTime() ? DateTime.MaxValue : dttMaxPutIntoUseDate;

				strSelect = sqlUtility.AddQualification(strSelect, PART_PUT_INTO_USE_DATE);
				arrParms.Add(new SqlParameter(PARM_MIN_PUT_INTO_USE_DATE, dttMinPutIntoUseDate));
				arrParms.Add(new SqlParameter(PARM_MAX_PUT_INTO_USE_DATE, dttMaxPutIntoUseDate));
			}
			if (dttMinRegisterTime != new DateTime() || dttMaxRegisterTime != new DateTime())
			{
				dttMinRegisterTime = dttMinRegisterTime == new DateTime() ? DateTime.MinValue : dttMinRegisterTime;
				dttMaxRegisterTime = dttMaxRegisterTime == new DateTime() ? DateTime.MaxValue : dttMaxRegisterTime;

				strSelect = sqlUtility.AddQualification(strSelect, PART_REGISTER_TIME);
				arrParms.Add(new SqlParameter(PARM_MIN_REGISTER_TIME, dttMinRegisterTime));
				arrParms.Add(new SqlParameter(PARM_MAX_REGISTER_TIME, dttMaxRegisterTime));
			}

			#endregion

			SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

			SystemDs.CarInfoDataTable dataTable = new SZCG.GPS.DAL.SystemDs.CarInfoDataTable();

			(new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);
            //string dd = Convert.ToString( dataTable.Rows[0]["Prices"]);

			return dataTable;
        }
        #endregion

        #region SelectAll ： 获取所有车辆信息表
        /// <summary>
		/// 获取所有。
		/// </summary>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectAll()
		{
			return this.Select(-1, -1, null, null, null, -1, -1, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        /// <summary>
        /// 分页查询。
        /// </summary>
        /// <param name="intSexId">车辆群组标识，若不想加入该条件请设负值，如 -1 。</param>
        /// <param name="strName">群组名称，若不想加入该条件请设 null 。</param>
        /// <param name="strDescription">群组描述，若不想加入该条件请设 null 。</param>
        /// <returns>车辆群组信息表</returns>
        public QueryUtil Select_Of_Page(int intGroupId, string strSIM, string strLicense, string strName, int intDepartmentId, int intStateId, int pageindex, int pagesize)
        {
            //调试
            string strselect = " CarId, SIM, License, [Name], DepartmentId, isnull(CarStateId,-1) as CarStateId, PhotoType, PhotoLength, [Description], Type, Color, isnull(Prices,-1) as Prices, isnull(LoadNo,-1) as LoadNo, HaveInsurance, InsuranceType, InsuranceCompany,isnull(Convert(varchar(10),ProductionDate,120),'') as ProductionDate, isnull(Convert(varchar(10),PurchaseDate,120),'') as PurchaseDate, isnull(Convert(varchar(10),PutIntoUseDate,120),'') as PutIntoUseDate, RegisterTime";
            string strfrom = " CarInfo ";
            string strwhere = " 1=1 and  Area LIKE '" + this.strArea + "%'";

            if (intGroupId >= 0)
            {
                strwhere = strwhere + " and CarId IN (SELECT CarId FROM CarGroupRelation WHERE CarGroupId =" + Convert.ToString(intGroupId) + ")";
            }
            if (strSIM != null)
            {
                strwhere = strwhere + " and SIM LIKE '%" + strSIM + "%'";  
            }
            if (strLicense != null)
            {
                strwhere = strwhere + " and License LIKE '%" + strLicense + "%'";
            }
            if (strName != null)
            {
                strwhere = strwhere + " and [Name] LIKE '%" + strName + "%'";
            }
            if (intDepartmentId >= 0)
            {
                strwhere = strwhere + " and DepartmentId =" + intDepartmentId;
            }
            if (intStateId >= 0)
            {
                strwhere = strwhere + " and CarStateId =" + intStateId;
            }

            QueryUtil qu = new QueryUtil(strselect, strfrom, strwhere);
            qu.Key = "CarId";
            qu.SortBy = "CarId";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize = pagesize;
            qu.ExecuteDataset(pageindex, "CONN_GPS_STRING");
            return qu;
        }

        /// <summary>
        /// 分页查询。
        /// </summary>
        public bacgDL.PageManage Select_V2(int intGroupId, string strSIM, string strLicense, string strName, int intDepartmentId, int intStateId, int pageindex, int pagesize)
        {
            bacgDL.PageManage pm = new bacgDL.PageManage();
            QueryUtil qu = Select_Of_Page(intGroupId, strSIM,strLicense,strName,intDepartmentId,intStateId, pageindex, pagesize);
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }

        #region SelectByCarId ： 查询(依据车辆标识)。车辆信息表
        /// <summary>
		/// 查询(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectByCarId(int intCarId)
		{
			return this.Select(intCarId, -1, null, null, null, -1, -1, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        #region SelectBySIM ： 查询(依据 SIM 卡号)，车辆信息表
        /// <summary>
		/// 查询(依据 SIM 卡号)。
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectBySIM(string strSIM)
		{
			return this.Select(-1, -1, strSIM, null, null, -1, -1, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        #region SelectByName ： 查询(依据车辆名称)，车辆信息表
        /// <summary>
		/// 查询(依据车辆名称)。
		/// </summary>
		/// <param name="strName">车辆名称</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectByName(string strName)
		{
			return this.Select(-1, -1, null, null, strName, -1, -1, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        #region IsNameExist ： 判断车辆名称是否已存在
        /// <summary>
		/// 判断车辆名称是否已存在
		/// </summary>
		/// <param name="strName">车辆名称</param>
		/// <returns>是否已存在</returns>
		public bool IsNameExist(string strName)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(
				SQL_SELECT_NAME_EXIST, 
				new SqlParameter(PARM_NAME, strName), 
				new SqlParameter(PARM_AREA, this.strArea)
				);

			if (intCount > 0)
				return true;
			else
				return false;
        }
        #endregion

        #region IsSIMExist ： 判断 SIM 卡号是否已存在
        /// <summary>
		/// 判断 SIM 卡号是否已存在
		/// </summary>
		/// <param name="strSIM">SIM 卡号</param>
		/// <returns>是否已存在</returns>
		public bool IsSIMExist(string strSIM)
		{
			int intCount = (int)(new SQLHelper()).ExecuteScalar(
				SQL_SELECT_SIM_EXIST, 
				new SqlParameter(PARM_SIM, strSIM)
				);

			if (intCount > 0)
				return true;
			else
				return false;
        }
        #endregion

        #region SelectByDepartmentId ：  查询(依据部门标识)，车辆信息表
        /// <summary>
		/// 查询(依据部门标识)。
		/// </summary>
		/// <param name="intDepartmentId">部门标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectByDepartmentId(int intDepartmentId)
		{
			return this.Select(-1, -1, null, null, null, intDepartmentId, -1, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        #region SelectForQuery ： 查询(依据群组标识、SIM 卡号、车牌号码、车辆名称、部门标识、车辆状态)，车辆信息表
        /// <summary>
		/// 查询(依据群组标识、SIM 卡号、车牌号码、车辆名称、部门标识、车辆状态)。
		/// </summary>
		/// <param name="intCarGroupId">群组标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="strSIM">SIM 卡号，若不想加入该条件请设 null 。</param>
		/// <param name="strLicense">车牌号码，若不想加入该条件请设 null 。</param>
		/// <param name="strName">车辆名称，若不想加入该条件请设 null 。</param>
		/// <param name="intDepartmentId">部门标识，若不想加入该条件请设负值，如 -1 。</param>
		/// <param name="intCarStateId">车辆状态，若不想加入该条件请设负值，如 -1 。</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectForQuery(int intCarGroupId, string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId)
		{
			return this.Select(-1, intCarGroupId, strSIM, strLicense, strName, intDepartmentId, intCarStateId, null, -1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
        }
        #endregion

        #region SelectPhoto ： 查询车辆照片(依据车辆标识)，车辆信息表
        /// <summary>
		/// 查询车辆照片(依据车辆标识)
		/// </summary>
		/// <param name="intCarId">车辆标识</param>
		/// <returns>车辆信息表</returns>
		public SystemDs.CarInfoDataTable SelectPhoto(int intCarId)
		{
			SystemDs.CarInfoDataTable dataTable = new DAL.SystemDs.CarInfoDataTable();

			(new SQLHelper()).ExecuteDataTable(
				dataTable, 
				SQL_SELECT_PHOTO, 
				new SqlParameter(PARM_CAR_ID, intCarId)
				);

			return dataTable;
        }
        #endregion

        #region Insert ： gps车辆添加
        /// <summary>
		/// 插入。
		/// </summary>
		/// <param name="strSIM">SIM 卡号，若不想插入值请设 null 。</param>
		/// <param name="strLicense">车牌号码，若不想插入值请设 null 。</param>
		/// <param name="strName">车辆名称，若不想插入值请设 null 。</param>
		/// <param name="intDepartmentId">使用单位，若不想插入值请设负值，如 -1 。</param>
		/// <param name="intCarStateId">状态标识，若不想插入值请设负值，如 -1 。</param>
		/// <param name="bytPhoto">照片内容，若该数组的长度为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="strPhotoType">照片类型，若该值为 null 则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="intPhotoLength">照片长度，若该值为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
		/// <param name="strDescription">车辆描述，若不想插入值请设 null 。</param>
		/// <param name="strType">车辆型号，若不想插入值请设 null 。</param>
		/// <param name="strColor">车辆颜色，若不想插入值请设 null 。</param>
		/// <param name="dblPrices">车辆价格，若不想插入值请设负值，如 -1 。</param>
		/// <param name="dblLoadNo">车辆载重，若不想插入值请设负值，如 -1 。</param>
		/// <param name="intHaveInsurance">是否保险，若不想插入值请设负值，如 -1 。</param>
		/// <param name="strInsuranceType">保险类型，若不想插入值请设 null 。</param>
		/// <param name="strInsuranceCompany">保险公司，若不想插入值请设 null 。</param>
		/// <param name="dttProductionDate">生产日期，若不想插入值请设 new DateTime() 。</param>
		/// <param name="dttPurchaseDate">购买日期，若不想插入值请设 new DateTime() 。</param>
		/// <param name="dttPutIntoUseDate">投入使用日期，若不想插入值请设 new DateTime() 。</param>
		/// <returns>执行是否成功</returns>
		public bool Insert(string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strDescription, string strType, string strColor, double dblPrices, double dblLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dttProductionDate, DateTime dttPurchaseDate, DateTime dttPutIntoUseDate)
		{
			#region 为执行准备参数

			SqlParameter[] parms = new SqlParameter[22];

			if (strArea == null)
				parms[0] = new SqlParameter(PARM_AREA, DBNull.Value);
			else
				parms[0] = new SqlParameter(PARM_AREA, strArea);

			if (strSIM == null)
			{
				parms[1] = new SqlParameter(PARM_SIM, DBNull.Value);
				parms[2] = new SqlParameter(PARM_VIRTUAL_IP, DBNull.Value);
			}
			else
			{
				parms[1] = new SqlParameter(PARM_SIM, strSIM);
				parms[2] = new SqlParameter(PARM_VIRTUAL_IP, (new VirtualIP()).Encode(strSIM));
			}

			if (strLicense == null)
				parms[3] = new SqlParameter(PARM_LICENSE, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_LICENSE, strLicense);

			if (strName == null)
				parms[4] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[4] = new SqlParameter(PARM_NAME, strName);

			if (intDepartmentId < 0)
				parms[5] = new SqlParameter(PARM_DEPARTMENT_ID, DBNull.Value);
			else
				parms[5] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

			if (intCarStateId < 0)
				parms[6] = new SqlParameter(PARM_CAR_STATE_ID, DBNull.Value);
			else
				parms[6] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

			if (bytPhoto.Length <= 0 || strPhotoType == null || intPhotoLength <= 0)
			{
				parms[7] = new SqlParameter(PARM_PHOTO, new byte[0]);
				parms[8] = new SqlParameter(PARM_PHOTO_TYPE, DBNull.Value);
				parms[9] = new SqlParameter(PARM_PHOTO_LENGTH, DBNull.Value);
			}
			else
			{
				parms[7] = new SqlParameter(PARM_PHOTO, bytPhoto);
				parms[8] = new SqlParameter(PARM_PHOTO_TYPE, strPhotoType);
				parms[9] = new SqlParameter(PARM_PHOTO_LENGTH, intPhotoLength);
			}

			if (strDescription == null)
				parms[10] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[10] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			if (strType == null)
				parms[11] = new SqlParameter(PARM_TYPE, DBNull.Value);
			else
				parms[11] = new SqlParameter(PARM_TYPE, strType);

			if (strColor == null)
				parms[12] = new SqlParameter(PARM_COLOR, DBNull.Value);
			else
				parms[12] = new SqlParameter(PARM_COLOR, strColor);

			if (dblPrices < 0)
				parms[13] = new SqlParameter(PARM_PRICES, DBNull.Value);
			else
				parms[13] = new SqlParameter(PARM_PRICES, dblPrices);

			if (dblLoadNo < 0)
				parms[14] = new SqlParameter(PARM_LOAD_NO, DBNull.Value);
			else
				parms[14] = new SqlParameter(PARM_LOAD_NO, dblLoadNo);

			if (intHaveInsurance < 0)
				parms[15] = new SqlParameter(PARM_HAVE_INSURANCE, DBNull.Value);
			else
				parms[15] = new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance);

			if (strInsuranceType == null)
				parms[16] = new SqlParameter(PARM_INSURANCE_TYPE, DBNull.Value);
			else
				parms[16] = new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType);

			if (strInsuranceCompany == null)
				parms[17] = new SqlParameter(PARM_INSURANCE_COMPANY, DBNull.Value);
			else
				parms[17] = new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany);

			if (dttProductionDate == new DateTime())
				parms[18] = new SqlParameter(PARM_PRODUCTION_DATE, DBNull.Value);
			else
				parms[18] = new SqlParameter(PARM_PRODUCTION_DATE, dttProductionDate);

			if (dttPurchaseDate == new DateTime())
				parms[19] = new SqlParameter(PARM_PURCHASE_DATE, DBNull.Value);
			else
				parms[19] = new SqlParameter(PARM_PURCHASE_DATE, dttPurchaseDate);

			if (dttPutIntoUseDate == new DateTime())
				parms[20] = new SqlParameter(PARM_PUT_INTO_USE_DATE, DBNull.Value);
			else
				parms[20] = new SqlParameter(PARM_PUT_INTO_USE_DATE, dttPutIntoUseDate);

			parms[21] = new SqlParameter(PARM_REGISTER_TIME, DateTime.Now);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

			return intRetVal > 0 ? true : false;
        }
        #endregion

        #region Inserts ： 环卫系统中的插入车辆
        /// <summary>
        /// 插入。
        /// </summary>
        /// <param name="strSIM">SIM 卡号，若不想插入值请设 null 。</param>
        /// <param name="strLicense">车牌号码，若不想插入值请设 null 。</param>
        /// <param name="strName">车辆名称，若不想插入值请设 null 。</param>
        /// <param name="intDepartmentId">使用单位，若不想插入值请设负值，如 -1 。</param>
        /// <param name="intCarStateId">状态标识，若不想插入值请设负值，如 -1 。</param>
        /// <param name="bytPhoto">照片内容，若该数组的长度为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="strPhotoType">照片类型，若该值为 null 则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="intPhotoLength">照片长度，若该值为不大于 0 的数则不会插入照片相关数据(照片内容、照片类型、照片长度)。</param>
        /// <param name="strDescription">车辆描述，若不想插入值请设 null 。</param>
        /// <param name="strType">车辆型号，若不想插入值请设 null 。</param>
        /// <param name="strColor">车辆颜色，若不想插入值请设 null 。</param>
        /// <param name="dblPrices">车辆价格，若不想插入值请设负值，如 -1 。</param>
        /// <param name="dblLoadNo">车辆载重，若不想插入值请设负值，如 -1 。</param>
        /// <param name="intHaveInsurance">是否保险，若不想插入值请设负值，如 -1 。</param>
        /// <param name="strInsuranceType">保险类型，若不想插入值请设 null 。</param>
        /// <param name="strInsuranceCompany">保险公司，若不想插入值请设 null 。</param>
        /// <param name="dttProductionDate">生产日期，若不想插入值请设 new DateTime() 。</param>
        /// <param name="dttPurchaseDate">购买日期，若不想插入值请设 new DateTime() 。</param>
        /// <param name="dttPutIntoUseDate">投入使用日期，若不想插入值请设 new DateTime() 。</param>
        /// <returns>执行是否成功</returns>
        public bool Inserts(string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, byte[] bytPhoto, string strPhotoType, int intPhotoLength, string strDescription, string strType, string strColor, double dblPrices, double dblLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dttProductionDate, DateTime dttPurchaseDate, DateTime dttPutIntoUseDate, int DepartmentId,string streetcode)
        {
            #region 为执行准备参数

            SqlParameter[] parms = new SqlParameter[23];

            if (streetcode == null)
                parms[0] = new SqlParameter(PARM_AREA, DBNull.Value);
            else
                parms[0] = new SqlParameter(PARM_AREA, streetcode);

            if (strSIM == null)
            {
                parms[1] = new SqlParameter(PARM_SIM, DBNull.Value);
                parms[2] = new SqlParameter(PARM_VIRTUAL_IP, DBNull.Value);
            }
            else
            {
                parms[1] = new SqlParameter(PARM_SIM, strSIM);
                parms[2] = new SqlParameter(PARM_VIRTUAL_IP, (new VirtualIP()).Encode(strSIM));
            }

            if (strLicense == null)
                parms[3] = new SqlParameter(PARM_LICENSE, DBNull.Value);
            else
                parms[3] = new SqlParameter(PARM_LICENSE, strLicense);

            if (strName == null)
                parms[4] = new SqlParameter(PARM_NAME, DBNull.Value);
            else
                parms[4] = new SqlParameter(PARM_NAME, strName);

            if (intDepartmentId < 0)
                parms[5] = new SqlParameter(PARM_DEPARTMENT_ID, DBNull.Value);
            else
                parms[5] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

            if (intCarStateId < 0)
                parms[6] = new SqlParameter(PARM_CAR_STATE_ID, DBNull.Value);
            else
                parms[6] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

            if (bytPhoto.Length <= 0 || strPhotoType == null || intPhotoLength <= 0)
            {
                parms[7] = new SqlParameter(PARM_PHOTO, new byte[0]);
                parms[8] = new SqlParameter(PARM_PHOTO_TYPE, DBNull.Value);
                parms[9] = new SqlParameter(PARM_PHOTO_LENGTH, DBNull.Value);
            }
            else
            {
                parms[7] = new SqlParameter(PARM_PHOTO, bytPhoto);
                parms[8] = new SqlParameter(PARM_PHOTO_TYPE, strPhotoType);
                parms[9] = new SqlParameter(PARM_PHOTO_LENGTH, intPhotoLength);
            }

            if (strDescription == null)
                parms[10] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
            else
                parms[10] = new SqlParameter(PARM_DESCRIPTION, strDescription);

            if (strType == null)
                parms[11] = new SqlParameter(PARM_TYPE, DBNull.Value);
            else
                parms[11] = new SqlParameter(PARM_TYPE, strType);

            if (strColor == null)
                parms[12] = new SqlParameter(PARM_COLOR, DBNull.Value);
            else
                parms[12] = new SqlParameter(PARM_COLOR, strColor);

            if (dblPrices < 0)
                parms[13] = new SqlParameter(PARM_PRICES, DBNull.Value);
            else
                parms[13] = new SqlParameter(PARM_PRICES, dblPrices);

            if (dblLoadNo < 0)
                parms[14] = new SqlParameter(PARM_LOAD_NO, DBNull.Value);
            else
                parms[14] = new SqlParameter(PARM_LOAD_NO, dblLoadNo);

            if (intHaveInsurance < 0)
                parms[15] = new SqlParameter(PARM_HAVE_INSURANCE, DBNull.Value);
            else
                parms[15] = new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance);

            if (strInsuranceType == null)
                parms[16] = new SqlParameter(PARM_INSURANCE_TYPE, DBNull.Value);
            else
                parms[16] = new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType);

            if (strInsuranceCompany == null)
                parms[17] = new SqlParameter(PARM_INSURANCE_COMPANY, DBNull.Value);
            else
                parms[17] = new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany);

            if (dttProductionDate == new DateTime())
                parms[18] = new SqlParameter(PARM_PRODUCTION_DATE, DBNull.Value);
            else
                parms[18] = new SqlParameter(PARM_PRODUCTION_DATE, dttProductionDate);

            if (dttPurchaseDate == new DateTime())
                parms[19] = new SqlParameter(PARM_PURCHASE_DATE, DBNull.Value);
            else
                parms[19] = new SqlParameter(PARM_PURCHASE_DATE, dttPurchaseDate);

            if (dttPutIntoUseDate == new DateTime())
                parms[20] = new SqlParameter(PARM_PUT_INTO_USE_DATE, DBNull.Value);
            else
                parms[20] = new SqlParameter(PARM_PUT_INTO_USE_DATE, dttPutIntoUseDate);

            parms[21] = new SqlParameter(PARM_REGISTER_TIME, DateTime.Now);
            if (DepartmentId < 0)
                parms[22] = new SqlParameter(PARM_ENVIR_DEPART, DBNull.Value);
            else
                parms[22] = new SqlParameter(PARM_ENVIR_DEPART, DepartmentId);
            #endregion

            int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_INSERT, parms);

            return intRetVal > 0 ? true : false;
        }
        #endregion

        #region  Update ： 更新(依据车辆标识)(不更新照片)
        /// <summary>
		/// 更新(依据车辆标识)(不更新照片)。
		/// </summary>
		/// <param name="intCarId">车辆标识</param>
		/// <param name="strSIM">SIM 卡号，若想清空请设 null 。</param>
		/// <param name="strLicense">车牌号码，若想清空请设 null 。</param>
		/// <param name="strName">车辆名称，若想清空请设 null 。</param>
		/// <param name="intDepartmentId">使用单位，若想清空请设负值，如 -1 。</param>
		/// <param name="intCarStateId">状态标识，若想清空请设负值，如 -1 。</param>
		/// <param name="strDescription">车辆描述，若想清空请设 null 。</param>
		/// <param name="strType">车辆型号，若想清空请设 null 。</param>
		/// <param name="strColor">车辆颜色，若想清空请设 null 。</param>
		/// <param name="dblPrices">车辆价格，若想清空请设负值，如 -1 。</param>
		/// <param name="dblLoadNo">车辆载重，若想清空请设负值，如 -1 。</param>
		/// <param name="intHaveInsurance">是否保险，若想清空请设负值，如 -1 。</param>
		/// <param name="strInsuranceType">保险类型，若想清空请设 null 。</param>
		/// <param name="strInsuranceCompany">保险公司，若想清空请设 null 。</param>
		/// <param name="dttProductionDate">生产日期，若想清空请设 new DateTime() 。</param>
		/// <param name="dttPurchaseDate">购买日期，若想清空请设 new DateTime() 。</param>
		/// <param name="dttPutIntoUseDate">投入使用日期，若想清空请设 new DateTime() 。</param>
		/// <returns>执行是否成功</returns>
		public bool Update(int intCarId, string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, string strDescription, string strType, string strColor, double dblPrices, double dblLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dtmProductionDate, DateTime dtmPurchaseDate, DateTime dtmPutIntoUseDate)
		{
			#region 为执行准备参数

			SqlParameter[] parms = new SqlParameter[18];

			if (strSIM == null)
			{
				parms[0] = new SqlParameter(PARM_SIM, DBNull.Value);
				parms[1] = new SqlParameter(PARM_VIRTUAL_IP, DBNull.Value);
			}
			else
			{
				parms[0] = new SqlParameter(PARM_SIM, strSIM);
				parms[1] = new SqlParameter(PARM_VIRTUAL_IP, (new VirtualIP()).Encode(strSIM));
			}

			if (strLicense == null)
				parms[2] = new SqlParameter(PARM_LICENSE, DBNull.Value);
			else
				parms[2] = new SqlParameter(PARM_LICENSE, strLicense);

			if (strName == null)
				parms[3] = new SqlParameter(PARM_NAME, DBNull.Value);
			else
				parms[3] = new SqlParameter(PARM_NAME, strName);

			if (intDepartmentId < 0)
				parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, DBNull.Value);
			else
				parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

			if (intCarStateId < 0)
				parms[5] = new SqlParameter(PARM_CAR_STATE_ID, DBNull.Value);
			else
				parms[5] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

			if (strDescription == null)
				parms[6] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
			else
				parms[6] = new SqlParameter(PARM_DESCRIPTION, strDescription);

			if (strType == null)
				parms[7] = new SqlParameter(PARM_TYPE, DBNull.Value);
			else
				parms[7] = new SqlParameter(PARM_TYPE, strType);

			if (strColor == null)
				parms[8] = new SqlParameter(PARM_COLOR, DBNull.Value);
			else
				parms[8] = new SqlParameter(PARM_COLOR, strColor);

			if (dblPrices < 0)
				parms[9] = new SqlParameter(PARM_PRICES, DBNull.Value);
			else
				parms[9] = new SqlParameter(PARM_PRICES, dblPrices);

			if (dblLoadNo < 0)
				parms[10] = new SqlParameter(PARM_LOAD_NO, DBNull.Value);
			else
				parms[10] = new SqlParameter(PARM_LOAD_NO, dblLoadNo);

			if (intHaveInsurance < 0)
				parms[11] = new SqlParameter(PARM_HAVE_INSURANCE, DBNull.Value);
			else
				parms[11] = new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance);

			if (strInsuranceType == null)
				parms[12] = new SqlParameter(PARM_INSURANCE_TYPE, DBNull.Value);
			else
				parms[12] = new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType);

			if (strInsuranceCompany == null)
				parms[13] = new SqlParameter(PARM_INSURANCE_COMPANY, DBNull.Value);
			else
				parms[13] = new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany);

			if (dtmProductionDate == new DateTime())
				parms[14] = new SqlParameter(PARM_PRODUCTION_DATE, DBNull.Value);
			else
				parms[14] = new SqlParameter(PARM_PRODUCTION_DATE, dtmProductionDate);

			if (dtmPurchaseDate == new DateTime())
				parms[15] = new SqlParameter(PARM_PURCHASE_DATE, DBNull.Value);
			else
				parms[15] = new SqlParameter(PARM_PURCHASE_DATE, dtmPurchaseDate);

			if (dtmPutIntoUseDate == new DateTime())
				parms[16] = new SqlParameter(PARM_PUT_INTO_USE_DATE, DBNull.Value);
			else
				parms[16] = new SqlParameter(PARM_PUT_INTO_USE_DATE, dtmPutIntoUseDate);

			parms[17] = new SqlParameter(PARM_CAR_ID, intCarId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE, parms);

			return intRetVal > 0 ? true : false;
        }
        #endregion

        #region  Update1 ： 更新(依据车辆标识)(不更新照片)
        /// <summary>
        /// 更新(依据车辆标识)(不更新照片)。
        /// </summary>
        /// <param name="intCarId">车辆标识</param>
        /// <param name="strSIM">SIM 卡号，若想清空请设 null 。</param>
        /// <param name="strLicense">车牌号码，若想清空请设 null 。</param>
        /// <param name="strName">车辆名称，若想清空请设 null 。</param>
        /// <param name="intDepartmentId">使用单位，若想清空请设负值，如 -1 。</param>
        /// <param name="intCarStateId">状态标识，若想清空请设负值，如 -1 。</param>
        /// <param name="strDescription">车辆描述，若想清空请设 null 。</param>
        /// <param name="strType">车辆型号，若想清空请设 null 。</param>
        /// <param name="strColor">车辆颜色，若想清空请设 null 。</param>
        /// <param name="dblPrices">车辆价格，若想清空请设负值，如 -1 。</param>
        /// <param name="dblLoadNo">车辆载重，若想清空请设负值，如 -1 。</param>
        /// <param name="intHaveInsurance">是否保险，若想清空请设负值，如 -1 。</param>
        /// <param name="strInsuranceType">保险类型，若想清空请设 null 。</param>
        /// <param name="strInsuranceCompany">保险公司，若想清空请设 null 。</param>
        /// <param name="dttProductionDate">生产日期，若想清空请设 new DateTime() 。</param>
        /// <param name="dttPurchaseDate">购买日期，若想清空请设 new DateTime() 。</param>
        /// <param name="dttPutIntoUseDate">投入使用日期，若想清空请设 new DateTime() 。</param>
        /// <returns>执行是否成功</returns>
        public bool Update1(int intCarId, string strSIM, string strLicense, string strName, int intCarStateId, string strDescription, string strType, string strColor, double dblPrices, double dblLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dtmProductionDate, DateTime dtmPurchaseDate, DateTime dtmPutIntoUseDate)
        {
            #region 为执行准备参数

            SqlParameter[] parms = new SqlParameter[17];

            if (strSIM == null)
            {
                parms[0] = new SqlParameter(PARM_SIM, DBNull.Value);
                parms[1] = new SqlParameter(PARM_VIRTUAL_IP, DBNull.Value);
            }
            else
            {
                parms[0] = new SqlParameter(PARM_SIM, strSIM);
                parms[1] = new SqlParameter(PARM_VIRTUAL_IP, (new VirtualIP()).Encode(strSIM));
            }

            if (strLicense == null)
                parms[2] = new SqlParameter(PARM_LICENSE, DBNull.Value);
            else
                parms[2] = new SqlParameter(PARM_LICENSE, strLicense);

            if (strName == null)
                parms[3] = new SqlParameter(PARM_NAME, DBNull.Value);
            else
                parms[3] = new SqlParameter(PARM_NAME, strName);

            //if (intDepartmentId < 0)
            //    parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, DBNull.Value);
            //else
            //    parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

            if (intCarStateId < 0)
                parms[4] = new SqlParameter(PARM_CAR_STATE_ID, DBNull.Value);
            else
                parms[4] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

            if (strDescription == null)
                parms[5] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
            else
                parms[5] = new SqlParameter(PARM_DESCRIPTION, strDescription);

            if (strType == null)
                parms[6] = new SqlParameter(PARM_TYPE, DBNull.Value);
            else
                parms[6] = new SqlParameter(PARM_TYPE, strType);

            if (strColor == null)
                parms[7] = new SqlParameter(PARM_COLOR, DBNull.Value);
            else
                parms[7] = new SqlParameter(PARM_COLOR, strColor);

            if (dblPrices < 0)
                parms[8] = new SqlParameter(PARM_PRICES, DBNull.Value);
            else
                parms[8] = new SqlParameter(PARM_PRICES, dblPrices);

            if (dblLoadNo < 0)
                parms[9] = new SqlParameter(PARM_LOAD_NO, DBNull.Value);
            else
                parms[9] = new SqlParameter(PARM_LOAD_NO, dblLoadNo);

            if (intHaveInsurance < 0)
                parms[10] = new SqlParameter(PARM_HAVE_INSURANCE, DBNull.Value);
            else
                parms[10] = new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance);

            if (strInsuranceType == null)
                parms[11] = new SqlParameter(PARM_INSURANCE_TYPE, DBNull.Value);
            else
                parms[11] = new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType);

            if (strInsuranceCompany == null)
                parms[12] = new SqlParameter(PARM_INSURANCE_COMPANY, DBNull.Value);
            else
                parms[12] = new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany);

            if (dtmProductionDate == new DateTime())
                parms[13] = new SqlParameter(PARM_PRODUCTION_DATE, DBNull.Value);
            else
                parms[13] = new SqlParameter(PARM_PRODUCTION_DATE, dtmProductionDate);

            if (dtmPurchaseDate == new DateTime())
                parms[14] = new SqlParameter(PARM_PURCHASE_DATE, DBNull.Value);
            else
                parms[14] = new SqlParameter(PARM_PURCHASE_DATE, dtmPurchaseDate);

            if (dtmPutIntoUseDate == new DateTime())
                parms[15] = new SqlParameter(PARM_PUT_INTO_USE_DATE, DBNull.Value);
            else
                parms[15] = new SqlParameter(PARM_PUT_INTO_USE_DATE, dtmPutIntoUseDate);

            parms[16] = new SqlParameter(PARM_CAR_ID, intCarId);

            #endregion

            int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE1, parms);

            return intRetVal > 0 ? true : false;
        }
        #endregion

        #region  Update2 ： 更新(依据车辆标识)(更新所属街道)
        /// <summary>
        /// 更新(依据车辆标识)(不更新照片)。
        /// </summary>
        /// <param name="intCarId">车辆标识</param>
        /// <param name="strSIM">SIM 卡号，若想清空请设 null 。</param>
        /// <param name="strLicense">车牌号码，若想清空请设 null 。</param>
        /// <param name="strName">车辆名称，若想清空请设 null 。</param>
        /// <param name="intDepartmentId">使用单位，若想清空请设负值，如 -1 。</param>
        /// <param name="intCarStateId">状态标识，若想清空请设负值，如 -1 。</param>
        /// <param name="strDescription">车辆描述，若想清空请设 null 。</param>
        /// <param name="strType">车辆型号，若想清空请设 null 。</param>
        /// <param name="strColor">车辆颜色，若想清空请设 null 。</param>
        /// <param name="dblPrices">车辆价格，若想清空请设负值，如 -1 。</param>
        /// <param name="dblLoadNo">车辆载重，若想清空请设负值，如 -1 。</param>
        /// <param name="intHaveInsurance">是否保险，若想清空请设负值，如 -1 。</param>
        /// <param name="strInsuranceType">保险类型，若想清空请设 null 。</param>
        /// <param name="strInsuranceCompany">保险公司，若想清空请设 null 。</param>
        /// <param name="dttProductionDate">生产日期，若想清空请设 new DateTime() 。</param>
        /// <param name="dttPurchaseDate">购买日期，若想清空请设 new DateTime() 。</param>
        /// <param name="dttPutIntoUseDate">投入使用日期，若想清空请设 new DateTime() 。</param>
        /// <returns>执行是否成功</returns>
        public bool Update2(int intCarId, string str_area , string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, string strDescription, string strType, string strColor, double dblPrices, double dblLoadNo, int intHaveInsurance, string strInsuranceType, string strInsuranceCompany, DateTime dtmProductionDate, DateTime dtmPurchaseDate, DateTime dtmPutIntoUseDate)
        {
            #region 为执行准备参数

            SqlParameter[] parms = new SqlParameter[19];

            if (strSIM == null)
            {
                parms[0] = new SqlParameter(PARM_SIM, DBNull.Value);
                parms[1] = new SqlParameter(PARM_VIRTUAL_IP, DBNull.Value);
            }
            else
            {
                parms[0] = new SqlParameter(PARM_SIM, strSIM);
                parms[1] = new SqlParameter(PARM_VIRTUAL_IP, (new VirtualIP()).Encode(strSIM));
            }

            if (strLicense == null)
                parms[2] = new SqlParameter(PARM_LICENSE, DBNull.Value);
            else
                parms[2] = new SqlParameter(PARM_LICENSE, strLicense);

            if (strName == null)
                parms[3] = new SqlParameter(PARM_NAME, DBNull.Value);
            else
                parms[3] = new SqlParameter(PARM_NAME, strName);

            if (intDepartmentId < 0)
                parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, DBNull.Value);
            else
                parms[4] = new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId);

            if (intCarStateId < 0)
                parms[5] = new SqlParameter(PARM_CAR_STATE_ID, DBNull.Value);
            else
                parms[5] = new SqlParameter(PARM_CAR_STATE_ID, intCarStateId);

            if (strDescription == null)
                parms[6] = new SqlParameter(PARM_DESCRIPTION, DBNull.Value);
            else
                parms[6] = new SqlParameter(PARM_DESCRIPTION, strDescription);

            if (strType == null)
                parms[7] = new SqlParameter(PARM_TYPE, DBNull.Value);
            else
                parms[7] = new SqlParameter(PARM_TYPE, strType);

            if (strColor == null)
                parms[8] = new SqlParameter(PARM_COLOR, DBNull.Value);
            else
                parms[8] = new SqlParameter(PARM_COLOR, strColor);

            if (dblPrices < 0)
                parms[9] = new SqlParameter(PARM_PRICES, DBNull.Value);
            else
                parms[9] = new SqlParameter(PARM_PRICES, dblPrices);

            if (dblLoadNo < 0)
                parms[10] = new SqlParameter(PARM_LOAD_NO, DBNull.Value);
            else
                parms[10] = new SqlParameter(PARM_LOAD_NO, dblLoadNo);

            if (intHaveInsurance < 0)
                parms[11] = new SqlParameter(PARM_HAVE_INSURANCE, DBNull.Value);
            else
                parms[11] = new SqlParameter(PARM_HAVE_INSURANCE, intHaveInsurance);

            if (strInsuranceType == null)
                parms[12] = new SqlParameter(PARM_INSURANCE_TYPE, DBNull.Value);
            else
                parms[12] = new SqlParameter(PARM_INSURANCE_TYPE, strInsuranceType);

            if (strInsuranceCompany == null)
                parms[13] = new SqlParameter(PARM_INSURANCE_COMPANY, DBNull.Value);
            else
                parms[13] = new SqlParameter(PARM_INSURANCE_COMPANY, strInsuranceCompany);

            if (dtmProductionDate == new DateTime())
                parms[14] = new SqlParameter(PARM_PRODUCTION_DATE, DBNull.Value);
            else
                parms[14] = new SqlParameter(PARM_PRODUCTION_DATE, dtmProductionDate);

            if (dtmPurchaseDate == new DateTime())
                parms[15] = new SqlParameter(PARM_PURCHASE_DATE, DBNull.Value);
            else
                parms[15] = new SqlParameter(PARM_PURCHASE_DATE, dtmPurchaseDate);

            if (dtmPutIntoUseDate == new DateTime())
                parms[16] = new SqlParameter(PARM_PUT_INTO_USE_DATE, DBNull.Value);
            else
                parms[16] = new SqlParameter(PARM_PUT_INTO_USE_DATE, dtmPutIntoUseDate);

            if (str_area == null)
                parms[17] = new SqlParameter(PARM_AREA, DBNull.Value);
            else
                parms[17] = new SqlParameter(PARM_AREA, str_area);

            parms[18] = new SqlParameter(PARM_CAR_ID, intCarId);

            #endregion

            int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE2, parms);

            return intRetVal > 0 ? true : false;
        }
        #endregion

        #region UpdatePhoto ： 更新车辆照片(依据车辆标识)
        /// <summary>
		/// 更新车辆照片(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识</param>
		/// <param name="bytPhoto">照片内容，若想清空照片相关数据(照片内容、照片类型、照片长度)请将该数组的长度设为 0 。</param>
		/// <param name="strPhotoType">照片类型，若想清空照片相关数据(照片内容、照片类型、照片长度)请设 null 。</param>
		/// <param name="intPhotoLength">照片长度，若想清空照片相关数据(照片内容、照片类型、照片长度)请设负值，如 -1 。</param>
		/// <returns>执行是否成功</returns>
		public bool UpdatePhoto(int intCarId, byte[] bytPhoto, string strPhotoType, int intPhotoLength)
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

			parms[3] = new SqlParameter(PARM_CAR_ID, intCarId);

			#endregion

			int intRetVal = (new SQLHelper()).ExecuteNonQuery(SQL_UPDATE_PHOTO, parms);

			return intRetVal > 0 ? true : false;
        }
        #endregion

        #region UpdateClearCarStateId ： 清空状态标识(依据状态标识)
        /// <summary>
		/// 清空状态标识(依据状态标识)
		/// </summary>
		/// <param name="intCarStateId">状态标识</param>
		/// <param name="trans">现有事务</param>
		/// <returns>执行是否成功</returns>
		public bool UpdateClearCarStateId(int intCarStateId, SqlTransaction trans)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_UPDATE_CLEAR_CAR_STATE_ID, new SqlParameter(PARM_CAR_STATE_ID, intCarStateId)
				);
			return intRetVal > 0 ? true : false;
        }
        #endregion

        #region UpdateClearDepartmentId ： 清空部门标识(依据部门标识)
        /// <summary>
		/// 清空部门标识(依据部门标识)
		/// </summary>
		/// <param name="intDepartmentId">部门标识</param>
		/// <param name="trans">现有事务</param>
		/// <returns>执行是否成功</returns>
		public bool UpdateClearDepartmentId(int intDepartmentId, SqlTransaction trans)
		{
			int intRetVal = (new SQLHelper()).ExecuteNonQuery(
				trans, SQL_UPDATE_CLEAR_DEPARTMENT_ID, new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId)
				);
			return intRetVal > 0 ? true : false;
        }
        #endregion

        #region Delete ： 删除(依据车辆标识)
        /// <summary>
		/// 删除(依据车辆标识)。
		/// </summary>
		/// <param name="intCarId">车辆标识</param>
		/// <returns>执行是否成功</returns>
		public bool Delete(int intCarId)
		{
			using (SqlConnection conn = new SqlConnection((new SQLHelper()).CONN_GPS_STRING))
			{
				conn.Open();
				using (SqlTransaction trans = conn.BeginTransaction())
				{
					try
					{
						// 删除司机关联
						(new CarDriverRelation()).DeleteByCarId(trans, intCarId);
						// 删除群组关联
						(new CarGroupRelation()).DeleteByCarId(trans, intCarId);
						// 删除油耗日志
						(new OilLog()).DeleteByCarId(trans, intCarId);
						// 删除维护日志
						(new MaintenanceLog()).DeleteByCarId(trans, intCarId);
						// 删除车辆
						int intRetVal = (new SQLHelper()).ExecuteNonQuery(
							trans, SQL_DELETE, new SqlParameter(PARM_CAR_ID, intCarId)
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
        #endregion
    }
}
