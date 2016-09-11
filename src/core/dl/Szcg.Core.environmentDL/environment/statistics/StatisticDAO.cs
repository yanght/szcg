/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统人员数据库操作
 * 结构组成：
 * 作    者：吴林波
 * 创建日期：2007-05-26
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明： 
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Teamax.Common;

namespace bacgDL.environment.statistics
{
    public class StatisticDAO : Teamax.Common.CommonDatabase
    {
        public const string strKey = "SdeConnString";

        #region getInfoCount : 得到相应表，相关条件下的条数
        public int getInfoCount(string table,string key,string value)
        {
            string sql = "select count(*) as count from "+table+"  where "+key+"='" + value + "'";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                int count = 0;
                while (rs.Read())
                {
                    count = Convert.ToInt32(rs["count"]);
                }
                rs.Close();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getCarInfo : 车辆类型
        public DataTable getCarInfo()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string sql = "select type from CarType";
            try
            {
                using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString("CONN_GPS_STRING")))
                {
                    ds = cdb.ExecuteDataset(sql);
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion
        
        #region getEstablishmentInfo : 调用储存过程计算环卫设施统计的信息
        /// <summary>
        /// 计算环卫设施统计的信息
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="streetcode">街道ID</param>
        /// <param name="population">人口总数</param>
        /// <param name="area">区域</param>
        /// <param name="type">类型</param>
        public void getEstablishmentInfo(ref DataSet ds, string streetcode, string population, string area, string type)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n DECLARE @CodeChar varchar(40)");
                sb.Append("\n DECLARE @Avgpopulation decimal(18, 4)");
                sb.Append("\n DECLARE @Avgarea decimal(18, 4)");
                sb.Append("\n DECLARE @typeCount int");
                sb.Append("\n set @Avgpopulation=0");
                sb.Append("\n set @Avgarea =0");
                sb.Append("\n set @typeCount =0");
                sb.Append("\n SET @CodeChar=LTRIM(RTRIM(@streetcode))+'%'");
                sb.Append("\n IF @type=1 BEGIN");
                sb.Append("\n SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_公厕指示牌");
                sb.Append("\n WHERE BGCode LIKE @CodeChar");
                sb.Append("\n IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n SET @Avgarea=@typeCount/@area");
                sb.Append("\n END");
                sb.Append("\n END");
                sb.Append("\n ELSE IF @type=2 BEGIN");
                sb.Append("\n SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_化粪池");
                sb.Append("\n WHERE BGCode LIKE @CodeChar");
                sb.Append("\n IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n SET @Avgarea=@typeCount/@area");
                sb.Append("\n END");
                sb.Append("\n END");
                sb.Append("\n  ELSE IF @type=3 BEGIN");
                sb.Append("\n   SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_公共厕所");
                sb.Append("\n    WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n 	  ELSE IF @type=4 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_垃圾箱");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=5 BEGIN");
                sb.Append("\n 	  SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_垃圾间楼");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=6 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_中转站");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n 	  SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=7 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_环卫工具房");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n 	  SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=8 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_主干道");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n 	  END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=9 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_次干道");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n 	  END");
                sb.Append("\n     ELSE IF @type=10 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_支路");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     ELSE IF @type=11 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.部件_宝安_街坊路");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     SELECT @Avgpopulation as Avgpopulation,@Avgarea as Avgarea,@typeCount as typeCount");
                //给需要执行的存储过程的参数赋值



                SqlParameter[] spInputs = new SqlParameter[]
                {
                    new SqlParameter("@streetcode", streetcode),
                    new SqlParameter("@population", Convert.ToDecimal(population)),
                    new SqlParameter("@area", Convert.ToDecimal(area)),
                    new SqlParameter("@type", Convert.ToInt32(type)),
                };
                using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString(strKey)))
                {
                    ds = cdb.ExecuteDataset(sb.ToString(), CommandType.Text, spInputs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region getPersonnelInfo：调用储存过程计算环卫人员统计的信息
        /// <summary>
        /// 调用储存过程计算环卫人员统计的信息
        /// </summary>
        /// <param name="streetcode">街道ID</param>
        /// <param name="population">人口总数</param>
        /// <param name="area">区域面积数</param>
        /// <param name="type">类型：环卫还是绿化</param>
        /// <param name="Avgpopulation">人均环卫人员数(个/万人)</param>
        /// <param name="Avgarea">单个环卫人员承担面积(平方公里/人)</param>
        /// <param name="PersonnelCount"></param>
        public void GetPersonnelStatInfo(string streetcode, string population, string area, string type,
                    ref object Avgpopulation, ref object Avgarea, ref object PersonnelCount)
        {
            //给需要执行的存储过程的参数赋值
            SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 14);
            sqlp1.Value = streetcode;
            SqlParameter sqlp2 = new SqlParameter("@population", SqlDbType.Decimal);
            sqlp2.Value = Convert.ToDecimal(population);
            SqlParameter sqlp3 = new SqlParameter("@area", SqlDbType.Decimal);
            sqlp3.Value = Convert.ToDecimal(area);

            //SqlParameter sqlp4 = new SqlParameter("@organ", SqlDbType.Int);
            //sqlp4.Value = Convert.ToInt32(type);

            SqlParameter sqlp5 = new SqlParameter("@Avgpopulation", SqlDbType.Decimal);
            sqlp5.Scale = 4;
            sqlp5.Direction = ParameterDirection.Output;
            SqlParameter sqlp6 = new SqlParameter("@Avgarea", SqlDbType.Decimal);
            sqlp6.Scale = 4;
            sqlp6.Direction = ParameterDirection.Output;
            SqlParameter sqlp7 = new SqlParameter("@PersonnelCount", SqlDbType.Int);
            sqlp7.Direction = ParameterDirection.Output;

            //执行存储过程
            this.ExecuteNonQuery("pr_e_TJPersonnelInfo", CommandType.StoredProcedure,
                    sqlp1, sqlp2, sqlp3, sqlp5, sqlp6, sqlp7);
            Avgpopulation = sqlp5.Value;
            Avgarea = sqlp6.Value;
            PersonnelCount = sqlp7.Value;
        }
        #endregion

        #region getEquipmentInfo:调用储存过程计算环卫设备统计的信息


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlcommand"></param>
        /// <param name="streetcode"></param>
        /// <param name="population"></param>
        /// <param name="area"></param>
        /// <param name="equipmenttype"></param>
        public void getEquipmentInfo(ref SqlCommand sqlcommand, string streetcode, string population, string area, string equipmenttype)
        {
            try
            {
                //给需要执行的存储过程的参数赋值


                SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 14);
                sqlp1.Value = streetcode;
                SqlParameter sqlp2 = new SqlParameter("@population", SqlDbType.Decimal);
                sqlp2.Value = Convert.ToDecimal(population);
                SqlParameter sqlp3 = new SqlParameter("@area", SqlDbType.Decimal);
                sqlp3.Value = Convert.ToDecimal(area);
                SqlParameter sqlp4 = new SqlParameter("@cartype", SqlDbType.VarChar,256);
                sqlp4.Value = equipmenttype;
                SqlParameter sqlp5 = new SqlParameter("@Avgpopulation", SqlDbType.Decimal);
                sqlp5.Scale = 4;
                sqlp5.Direction = ParameterDirection.Output;
                SqlParameter sqlp6 = new SqlParameter("@Avgarea", SqlDbType.Decimal);
                sqlp6.Scale = 4;
                sqlp6.Direction = ParameterDirection.Output;
                SqlParameter sqlp7 = new SqlParameter("@equipmentCount", SqlDbType.Int);
                sqlp7.Direction = ParameterDirection.Output;

                //添加参数到存储过程


                sqlcommand.Parameters.Add(sqlp1);
                sqlcommand.Parameters.Add(sqlp2);
                sqlcommand.Parameters.Add(sqlp3);
                sqlcommand.Parameters.Add(sqlp5);
                sqlcommand.Parameters.Add(sqlp6);
                sqlcommand.Parameters.Add(sqlp7);
                sqlcommand.Parameters.Add(sqlp4);

                sqlcommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getXunchaInfo : 调用储存过程计算环卫巡查统计的信息
        /// <summary>
        /// 调用储存过程计算环卫巡查统计的信息
        /// </summary>
        /// <param name="streetcode">街道ID</param>
        /// <param name="strdate">开始日期</param>
        /// <param name="enddate">结束日期</param>
        /// <returns>实际清扫人数(个)</returns>
        /// <returns>计划清扫人数(个)</returns>
        public DataTable getXunchaInfo(string streetcode, string strdate, string enddate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@streetcode", streetcode),                                 
                                new SqlParameter("@strdate",strdate),
                                new SqlParameter("@enddate",enddate)};

            DataSet ds = ExecuteDataset("pr_e_TJXuncha", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            //dt.Columns["streetname"].Caption = "区/街道";
            //dt.Columns["actualnum"].Caption = "清扫人员总数";
            //dt.Columns["plannum"].Caption = "计划清扫人员总数";

            return dt;
        }
        //public void getXunchaInfo(string streetcode, string population, string area, string type, ref object Avgpopulation, ref object Avgarea, ref object PersonnelCount)
        //{ 
        //        //给需要执行的存储过程的参数赋值
        //        SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 14);
        //        sqlp1.Value = streetcode;
        //        SqlParameter sqlp2 = new SqlParameter("@population", SqlDbType.Decimal);
        //        sqlp2.Value = Convert.ToDecimal(population);
        //        SqlParameter sqlp3 = new SqlParameter("@area", SqlDbType.Decimal);
        //        sqlp3.Value = Convert.ToDecimal(area);
        //        SqlParameter sqlp4 = new SqlParameter("@organ", SqlDbType.Int);
        //        sqlp4.Value = Convert.ToInt32(type);
        //        SqlParameter sqlp5 = new SqlParameter("@Avgpopulation", SqlDbType.Decimal);
        //        sqlp5.Scale = 4;
        //        sqlp5.Direction = ParameterDirection.Output;
        //        SqlParameter sqlp6 = new SqlParameter("@Avgarea", SqlDbType.Decimal);
        //        sqlp6.Scale = 4;
        //        sqlp6.Direction = ParameterDirection.Output;
        //        SqlParameter sqlp7 = new SqlParameter("@PersonnelCount", SqlDbType.Int);
        //        sqlp7.Direction = ParameterDirection.Output;   

        //        //添加参数到存储过程

        //        //执行存储过程
        //        this.ExecuteNonQuery("pr_e_TJXuncha", CommandType.StoredProcedure,
        //                sqlp1, sqlp2, sqlp3, sqlp4, sqlp5, sqlp6, sqlp7);
        //        Avgpopulation = sqlp5.Value;
        //        Avgarea = sqlp6.Value;
        //        PersonnelCount = sqlp7.Value;
        //    }
        //调用存储过程获取区的评价结果
        
        #endregion

        #region getRjhwryAndDgljqy 调用存储过程计算垃圾清运统计的信息
        /// <summary>
        /// 调用存储过程计算垃圾清运统计的信息
        /// </summary>
        /// <param name="streetcode">街道ID</param>
        /// <param name="strdate">开始日期</param>
        /// <param name="enddate">结束日期</param>
        /// <returns>垃圾清运数量(吨)</returns>
        public DataTable getLjqyInfo(string streetcode, string strdate, string enddate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@streetcode", streetcode),                                 
                                new SqlParameter("@strdate",strdate),
                                new SqlParameter("@enddate",enddate)};

            DataSet ds = ExecuteDataset("pr_e_TJLjqy", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //dt.Columns["streetname"].Caption = "区/街道";
            //dt.Columns["plannum"].Caption = "垃圾清运数量(吨)";

            return dt;
        }
        //public void getRjhwryAndDgljqy(string streetcode, string population, string area, string type, ref object Avgpopulation, ref object Avgarea, ref object PersonnelCount,ref object actualclearnum)
        //{
        //    //给需要执行的存储过程的参数赋值
        //    SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 14);
        //    sqlp1.Value = streetcode;
        //    SqlParameter sqlp2 = new SqlParameter("@population", SqlDbType.Decimal);
        //    sqlp2.Value = Convert.ToDecimal(population);
        //    SqlParameter sqlp3 = new SqlParameter("@area", SqlDbType.Decimal);
        //    sqlp3.Value = Convert.ToDecimal(area);
        //    SqlParameter sqlp4 = new SqlParameter("@organ", SqlDbType.Int);
        //    sqlp4.Value = Convert.ToInt32(type);
        //    SqlParameter sqlp5 = new SqlParameter("@Avgpopulation", SqlDbType.Decimal);
        //    sqlp5.Scale = 4;
        //    sqlp5.Direction = ParameterDirection.Output;
        //    SqlParameter sqlp6 = new SqlParameter("@Avgarea", SqlDbType.Decimal);
        //    sqlp6.Scale = 4;
        //    sqlp6.Direction = ParameterDirection.Output;
        //    SqlParameter sqlp7 = new SqlParameter("@PersonnelCount", SqlDbType.Int);
        //    sqlp7.Direction = ParameterDirection.Output;
        //    SqlParameter sqlp8 = new SqlParameter("@actualclearnum", SqlDbType.Decimal);
        //    sqlp8.Scale = 4;
        //    sqlp8.Direction = ParameterDirection.Output;

        //    //添加参数到存储过程
        //    //执行存储过程
        //    this.ExecuteNonQuery("pr_e_TJLjqy", CommandType.StoredProcedure,
        //            sqlp1, sqlp2, sqlp3, sqlp4, sqlp5, sqlp6, sqlp7,sqlp8);
        //    Avgpopulation = sqlp5.Value;
        //    Avgarea = sqlp6.Value;
        //    PersonnelCount = sqlp7.Value;
        //    if (sqlp8.Value == DBNull.Value)
        //    {
        //        actualclearnum = 0.0000;
        //    }
        //    else
        //    {
        //        actualclearnum = sqlp8.Value;
        //    }
        //}
        #endregion 

        #region getRjShouna : 调用存储过程得到受纳场的信息
        public void getRjShouna(string streetcode, string tjdate1, string tjdate2, ref object grefivenum, ref object lesfivenum, ref object acceptdirtnum, ref object flatsoliddirtnum, ref object germicidal)
        {
            //给需要执行的存储过程的参数赋值
            ////街道ID
            SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 24);
            sqlp1.Value = streetcode;
            //开始时间
            SqlParameter sqlp2 = new SqlParameter("@tjdate1", SqlDbType.DateTime);
            sqlp2.Value = tjdate1;
            //结束时间
            SqlParameter sqlp3 = new SqlParameter("@tjdate2", SqlDbType.DateTime);
            sqlp3.Value = tjdate2;
            //进场车辆5吨以上（车次）
            SqlParameter sqlp4 = new SqlParameter("@grefivenum", SqlDbType.Decimal);
            sqlp4.Scale = 0;
            sqlp4.Direction = ParameterDirection.Output;
            //进场车辆5吨以下（车次）
            SqlParameter sqlp5 = new SqlParameter("@lesfivenum", SqlDbType.Decimal);
            sqlp5.Scale = 0;
            sqlp5.Direction = ParameterDirection.Output;
            //受纳渣土量（吨）
            SqlParameter sqlp6 = new SqlParameter("@acceptdirtnum", SqlDbType.Decimal);
            sqlp6.Scale = 2;
            sqlp6.Direction = ParameterDirection.Output;
            //推平压实渣土量（平方米）
            SqlParameter sqlp7 = new SqlParameter("@flatsoliddirtnum", SqlDbType.Decimal);
            sqlp7.Scale = 2;
            sqlp7.Direction = ParameterDirection.Output;
            //
            SqlParameter sqlp8 = new SqlParameter("@germicidal", SqlDbType.Decimal);
            sqlp8.Scale = 0;
            sqlp8.Direction = ParameterDirection.Output;
            //添加参数到存储过程


            //执行存储过程
            this.ExecuteNonQuery("pr_e_TJShouna", CommandType.StoredProcedure,
                    sqlp1, sqlp2, sqlp3, sqlp4, sqlp5, sqlp6, sqlp7, sqlp8);
            //   dealdate = Convert.ToString(sqlp4.Value);
            if (sqlp4.Value == DBNull.Value)
            {
                grefivenum = 0;
            }
            else
            {
                grefivenum = sqlp4.Value;
            }
            if (sqlp5.Value == DBNull.Value)
            {
                lesfivenum = 0;
            }
            else
            {
                lesfivenum = sqlp5.Value;
            }
            if (sqlp6.Value == DBNull.Value)
            {
                acceptdirtnum = 0.00;
            }
            else
            {
                acceptdirtnum = sqlp6.Value;
            }
            if (sqlp7.Value == DBNull.Value)
            {
                flatsoliddirtnum = 0.00;
            }
            else
            {
                flatsoliddirtnum = sqlp7.Value;
            }
            if (sqlp8.Value == DBNull.Value)
            {
                germicidal = 0;
            }
            else
            {
                germicidal = sqlp8.Value;
            }
        }
        #endregion

        #region getYnztInfo : 调用存储过程计算淤泥渣土统计的信息
        public void getYnztInfo(string streetcode, int @dealtype, string tjdate1, string tjdate2, ref object cleardirtnum,
                                       ref object washroadnum, ref object sprinklecarnum, ref object newbuilnum, ref object repairbuilnum)
        {
            //给需要执行的存储过程的参数赋值
            //街道ID
            SqlParameter sqlp1 = new SqlParameter("@streetcode", SqlDbType.VarChar, 24);
            sqlp1.Value = streetcode;
            //处理类型
            SqlParameter sqlp2 = new SqlParameter("@dealtype", SqlDbType.Int);
            sqlp2.Value = @dealtype;
            //开始时间
            SqlParameter sqlp3 = new SqlParameter("@tjdate1", SqlDbType.DateTime);
            sqlp3.Value = tjdate1;
            //结束时间
            SqlParameter sqlp4 = new SqlParameter("@tjdate2", SqlDbType.DateTime);
            sqlp4.Value = tjdate2;
            //清理渣土量(吨)
            SqlParameter sqlp5 = new SqlParameter("@cleardirtnum", SqlDbType.Decimal);
            sqlp5.Scale = 2;
            sqlp5.Direction = ParameterDirection.Output;
            //冲洗道路面积(平方米)
            SqlParameter sqlp6 = new SqlParameter("@washroadnum", SqlDbType.Decimal);
            sqlp6.Scale = 2;
            sqlp6.Direction = ParameterDirection.Output;
            //出动洒水车(车次)
            SqlParameter sqlp7 = new SqlParameter("@sprinklecarnum", SqlDbType.Decimal);
            sqlp7.Scale = 0;
            sqlp7.Direction = ParameterDirection.Output;
            //新增工地数量(个)
            SqlParameter sqlp8 = new SqlParameter("@newbuilnum", SqlDbType.Decimal);
            sqlp8.Scale = 0;
            sqlp8.Direction = ParameterDirection.Output;
            //整治工地数量(个)
            SqlParameter sqlp9 = new SqlParameter("@repairbuilnum", SqlDbType.Decimal);
            sqlp9.Scale = 0;
            sqlp9.Direction = ParameterDirection.Output;
            //添加参数到存储过程

            //执行存储过程
            this.ExecuteNonQuery("pr_e_TJYuniZatu", CommandType.StoredProcedure,
                    sqlp1, sqlp2, sqlp3, sqlp4, sqlp5, sqlp6, sqlp7, sqlp8, sqlp9);
            if (sqlp5.Value == DBNull.Value)
            {
                cleardirtnum = 0.00;
            }
            else
            {
                cleardirtnum = sqlp5.Value;
            }
            if (sqlp6.Value == DBNull.Value)
            {
                washroadnum = 0.00;
            }
            else
            {
                washroadnum = sqlp6.Value;
            }
            if (sqlp7.Value == DBNull.Value)
            {
                sprinklecarnum = 0;
            }
            else
            {
                sprinklecarnum = sqlp7.Value;
            }
            if (sqlp8.Value == DBNull.Value)
            {
                newbuilnum = 0;
            }
            else
            {
                newbuilnum = sqlp8.Value;
            }
            if (sqlp9.Value == DBNull.Value)
            {
                repairbuilnum = 0;
            }
            else
            {
                repairbuilnum = sqlp9.Value;
            }
        }
        #endregion
    }
}
