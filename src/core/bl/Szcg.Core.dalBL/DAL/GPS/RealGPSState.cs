using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SZCG.GPS.DAL;

namespace SZCG.GPS.DAL.GPS
{
    /// <summary>
    /// 实时车载 GPS 终端状态数据操作类。
    /// </summary>
    public class RealGPSState:Teamax.Common.CommonPage
    {
        private const string SQL_SELECT = "SELECT car.SIM AS SIM, car.Name AS CarName,car.License AS CarLicense, real.LON AS Longitude, real.LAT AS Latitude, real.SPEED AS Speed, real.HEADING AS Angle, real.StarCount, real.RECTIME AS ReceiveTime,real.VCode FROM P_REAL_GPS real, CarInfo car WHERE real.VCode IN (SELECT VirtualIP FROM CarInfo WHERE Area LIKE @Area + '%') AND real.VCode = car.VirtualIP";

        private const string PART_VIRTUAL_IP = "real.VCode = @VirtualIP";
        private const string PART_SIM = "car.SIM = @SIM";
        private const string PART_LICENSE = "car.License LIKE '%' + @License + '%'";
        private const string PART_NAME = "car.Name LIKE '%' + @Name + '%' ";
        //private const string PART_DEPARTMENT_ID = "car.DepartmentId = @DepartmentId";
        private const string PART_Area_ID = "car.Area = @AreaId";
        private const string PART_CAR_STATE_ID = "car.CarStateId = @CarStateId";
        private const string PART_CAR_GROUP_ID = "car.CarId IN (SELECT CarId FROM CarGroupRelation WHERE CarGroupId = @CarGroupId)";
        private const string PART_OUT_TIME = "RECTIME >= @OutTime";

        private const string PARM_AREA = "@Area";
        private const string PARM_VIRTUAL_IP = "@VirtualIP";
        private const string PARM_SIM = "@SIM";
        private const string PARM_LICENSE = "@License";
        private const string PARM_NAME = "@Name";
        //private const string PARM_DEPARTMENT_ID = "@DepartmentId";
        private const string PARM_Area_ID = "@AreaId";
        private const string PARM_CAR_STATE_ID = "@CarStateId";
        private const string PARM_CAR_GROUP_ID = "@CarGroupId";
        private const string PARM_OUT_TIME = "@OutTime";

        private string strArea;

        public RealGPSState()
        {
            //strArea = (string)HttpContext.Current.Session["CurrentRoleArea"];
            strArea = AreaCode;
        }

        //        /// <summary>
        //        /// 查询实时终端状态。
        //        /// </summary>
        //        /// <param name="strSIM">SIM 卡号，若不想加入该条件请设 null 。</param>
        //        /// <returns>实时终端状态表</returns>
        //        public CarGPSDs.RealGPSStateDataTable Select(string strSIM, string strLicense, string strName, int intDepartmentId, int intCarStateId, int intCarGroupId)
        //        {
        //            string strSelect = SQL_SELECT;
        //            ArrayList arrParms = new ArrayList();

        //            SQLUtility sqlUtility = new SQLUtility();

        //            #region 为执行准备查询语句及参数

        //            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
        //            if (strSIM != null)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_SIM);
        //                arrParms.Add(new SqlParameter(PARM_SIM, strSIM));
        //            }
        //            if (strLicense != null)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_LICENSE);
        //                arrParms.Add(new SqlParameter(PARM_LICENSE, strLicense));
        //            }
        //            if (strName != null)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_NAME);
        //                arrParms.Add(new SqlParameter(PARM_NAME, strName));
        //            }
        //            if (intDepartmentId >= 0)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_DEPARTMENT_ID);
        //                arrParms.Add(new SqlParameter(PARM_DEPARTMENT_ID, intDepartmentId));
        //            }
        //            if (intCarStateId >= 0)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_STATE_ID);
        //                arrParms.Add(new SqlParameter(PARM_CAR_STATE_ID, intCarStateId));
        //            }
        //            if (intCarGroupId >= 0)
        //            {
        //                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_GROUP_ID);
        //                arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
        //            }

        //            #endregion

        ////			int intSeconds = Math.Abs(Convert.ToInt32(ConfigurationManager.AppSettings["CarActiveTimeOut"]));
        ////			DateTime dttOutTime = intSeconds == 0 ? new DateTime() : DateTime.Now.AddSeconds(0 - intSeconds);
        ////
        ////			strSelect = sqlUtility.AddQualification(strSelect, PART_OUT_TIME);
        ////			arrParms.Add(new SqlParameter(PARM_OUT_TIME, dttOutTime));

        //            strSelect += " ORDER BY real.RECTIME DESC";

        //            SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

        //            CarGPSDs.RealGPSStateDataTable dataTable = new CarGPSDs.RealGPSStateDataTable();

        //            (new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

        //            return dataTable;
        //        }


        /// <summary>
        /// 查询实时终端状态。
        /// </summary>
        /// <param name="strSIM">SIM 卡号，若不想加入该条件请设 null 。</param>
        /// <returns>实时终端状态表</returns>
        public CarGPSDs.RealGPSStateDataTable Select(string strSIM, string strLicense, string strName, string strAreaId, int intCarStateId, int intCarGroupId)
        {
            string strSelect = SQL_SELECT;
            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();

            #region 为执行准备查询语句及参数

            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
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
            if (strAreaId != null)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_Area_ID);
                arrParms.Add(new SqlParameter(PARM_Area_ID, strAreaId));
            }
            if (intCarStateId >= 0)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_STATE_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_STATE_ID, intCarStateId));
            }
            if (intCarGroupId >= 0)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_GROUP_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
            }

            #endregion

            //			int intSeconds = Math.Abs(Convert.ToInt32(ConfigurationManager.AppSettings["CarActiveTimeOut"]));
            //			DateTime dttOutTime = intSeconds == 0 ? new DateTime() : DateTime.Now.AddSeconds(0 - intSeconds);
            //
            //			strSelect = sqlUtility.AddQualification(strSelect, PART_OUT_TIME);
            //			arrParms.Add(new SqlParameter(PARM_OUT_TIME, dttOutTime));

            strSelect += " ORDER BY real.RECTIME DESC";

            SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

            CarGPSDs.RealGPSStateDataTable dataTable = new CarGPSDs.RealGPSStateDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

            return dataTable;
        }


        /// <summary>
        /// 查询实时终端状态。
        /// </summary>
        /// <param name="strSIM">SIM 卡号，若不想加入该条件请设 null 。</param>
        /// <returns>实时终端状态表</returns>
        public CarGPSDs.RealGPSStateDataTable SelectJZZT(string strSIM, string strLicense, string strName, string strAreaId, int intCarStateId, int intCarGroupId)
        {

            string strSelect = "SELECT car.carid,car.SIM AS SIM, car.Name AS CarName,car.License AS CarLicense, real.LON AS Longitude, real.LAT AS Latitude, real.SPEED AS Speed, real.HEADING AS Angle, real.StarCount, ";
            strSelect = strSelect + " real.RECTIME AS ReceiveTime,real.VCode,case when ";
            strSelect = strSelect + " ((real.RECTIME not between convert(varchar(10),getdate(),120)+' 08:00:00' and convert(varchar(10),getdate(),120)+' 18:00:00')";
            strSelect = strSelect + " and real.RECTIME > convert(varchar(10),dateadd(dd,-1,getdate()),120) +' 18:00:00' )";
            strSelect = strSelect + " then 1 else 0 end as istimeout ";
            strSelect = strSelect + " FROM P_REAL_GPS real,CarInfo car WHERE car.Name like '%渣土%' and real.VCode = car.VirtualIP";
            strSelect = strSelect + " and real.VCode IN (SELECT VirtualIP FROM CarInfo WHERE Area LIKE @Area + '%')";

            ArrayList arrParms = new ArrayList();

            SQLUtility sqlUtility = new SQLUtility();

            #region 为执行准备查询语句及参数

            arrParms.Add(new SqlParameter(PARM_AREA, strArea));
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
            if (strAreaId != null)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_Area_ID);
                arrParms.Add(new SqlParameter(PARM_Area_ID, strAreaId));
            }
            if (intCarStateId >= 0)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_STATE_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_STATE_ID, intCarStateId));
            }
            if (intCarGroupId >= 0)
            {
                strSelect = sqlUtility.AddQualification(strSelect, PART_CAR_GROUP_ID);
                arrParms.Add(new SqlParameter(PARM_CAR_GROUP_ID, intCarGroupId));
            }
            #endregion

            strSelect += " ORDER BY real.RECTIME DESC";

            SqlParameter[] parms = (SqlParameter[])arrParms.ToArray(typeof(SqlParameter));

            CarGPSDs.RealGPSStateDataTable dataTable = new CarGPSDs.RealGPSStateDataTable();

            (new SQLHelper()).ExecuteDataTable(dataTable, strSelect, parms);

            return dataTable;
        }


        /// <summary>
        /// 查询实时终端状态(依据 SIM 卡号)。
        /// </summary>
        /// <param name="strSIM">SIM 卡号</param>
        /// <returns>实时终端状态表</returns>
        public CarGPSDs.RealGPSStateDataTable SelectBySIM(string strSIM)
        {
            return this.Select(strSIM, null, null, null, -1, -1);
        }

        /// <summary>
        /// 获取所有实时终端状态
        /// </summary>
        /// <returns>实时终端状态表</returns>
        public CarGPSDs.RealGPSStateDataTable SelectAll()
        {
            return this.Select(null, null, null, null, -1, -1);
        }
    }
}
