/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：环卫系统人员管理
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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL.environment.statistics;
using Teamax.Common;

namespace bacgBL.environment.statistics
{
	/// <summary>
	/// collecter 的摘要说明。
	/// </summary>
    public class BASE_statistichelper 
{
        StatisticDAO statisticDAO = new StatisticDAO();
        SqlConnection bacgsql;
        SqlConnection GPSsql;
        #region BASE_statistichelper : 在此处添加构造函数逻辑
        public BASE_statistichelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
            string Gpssqlconnecting = ConfigurationManager.AppSettings["CONN_GPS_STRING"].ToString();
            string bacgsqlconnecting = ConfigurationManager.AppSettings["ConnString"].ToString();
            bacgsql = new SqlConnection(bacgsqlconnecting);
            GPSsql = new SqlConnection(Gpssqlconnecting);
        }
        #endregion

        #region getRjhwryAndChdhwmj : 根据区域得到人均环卫人员数、单个环卫人员承担环卫面积数
        public ArrayList getRjhwryAndChdhwmj(string streetcode, string organID)
        {
            ArrayList arr = new ArrayList();
            List<Area> streets = Public.GetAreaInfo(streetcode);
            for (int i = 0; i < streets.Count;i++)
            {
                Hashtable t1 = new Hashtable();
                StatisticType statisticType = this.GetPersonnelStatInfo(streets[i].areaCode, streets[i].areaPopulation, streets[i].area, organID);
                t1.Add("streetcode", statisticType.streetcode);
                t1.Add("streetname", streets[i].areaName);
                t1.Add("population", streets[i].areaPopulation);
                t1.Add("area", streets[i].area);
                t1.Add("PersonnelCount", statisticType.personnelCount);
                t1.Add("Avgpopulation", statisticType.Avgpopulation);
                t1.Add("Avgarea", statisticType.Avgarea);
                arr.Add(t1);
            }
                return arr;
            }
        #endregion

        #region getRjhwryAndDgxcmj : 根据区域得到巡查人员数、单个巡查人员承担环卫面积数
        //public ArrayList getRjhwryAndDgxcmj(string streetcode, string organID)
        //{
            //ArrayList arr = new ArrayList();
            //List<Area> streets = Public.GetAreaInfo(streetcode);
            //for (int i = 0; i < streets.Count; i++)
            //{
            //    Hashtable t1 = new Hashtable();
        //StatisticType statisticType = this.GetXunchaInfo(streets[i].areaCode, streets[i].areaPopulation, streets[i].area, organID);
            //    t1.Add("streetcode", statisticType.streetcode);
            //    t1.Add("streetname", streets[i].areaName);
            //    t1.Add("PersonnelCount", statisticType.personnelCount);
            //    t1.Add("Avgpopulation", statisticType.Avgpopulation);
            //    t1.Add("Avgarea", statisticType.Avgarea);
            //    arr.Add(t1);
            //}
            //return arr;
        //}
        public ArrayList getRjhwryAndDgxcmj(string streetcode, string startdate, string enddate)
        {
            ArrayList arr = new ArrayList();
            List<Area> streets = Public.GetAreaInfo(streetcode);

            for (int i = 0; i < streets.Count; i++)
            {
                Hashtable t1 = new Hashtable();

                StatisticDAO dao = new StatisticDAO();

                if (streets[i].areaCode.Length == 6)
                {
                    DataTable dt = dao.getXunchaInfo(streets[i].areaCode, startdate, enddate);

                    t1.Add("streetcode", streets[i].areaCode);
                    t1.Add("streetname", streets[i].areaName);
                    t1.Add("plannum", Convert.ToDecimal(dt.Rows[0]["plannum"].ToString()));
                    t1.Add("actualnum", Convert.ToDecimal(dt.Rows[0]["actualnum"].ToString()));
                    //t1.Add("actualnum", dt.Columns["actualnum"]);
                    arr.Add(t1);
                }

                if (streets[i].areaCode.Length == 9)
                {
                    DataTable dt = dao.getXunchaInfo(streets[i].areaCode, startdate, enddate);

                    t1.Add("streetcode", streets[i].areaCode);
                    t1.Add("streetname", streets[i].areaName);
                    t1.Add("plannum", Convert.ToDecimal(dt.Rows[0]["plannum"].ToString()));
                    t1.Add("actualnum", Convert.ToDecimal(dt.Rows[0]["actualnum"].ToString()));
                    //t1.Add("actualnum", dt.Columns["actualnum"]);
                    arr.Add(t1);
                    for (int j = 0; j < streets.Count; j++)
                    {
                        if (streets[j].areaCode.Length == 12 && streets[j].areaCode.Substring(0, 9) == streets[i].areaCode)
                        {
                            Hashtable t2 = new Hashtable();
                            DataTable dt2 = dao.getXunchaInfo(streets[j].areaCode, startdate, enddate);

                            t2.Add("streetcode", streets[j].areaCode);
                            t2.Add("streetname", streets[j].areaName);
                            t2.Add("plannum", Convert.ToDecimal(dt2.Rows[0]["plannum"].ToString()));
                            t2.Add("actualnum", Convert.ToDecimal(dt2.Rows[0]["actualnum"].ToString()));
                            //t1.Add("actualnum", dt.Columns["actualnum"]);
                            arr.Add(t2);
                        }
                    }
                }
            }
            
            return arr;
        }
        #endregion

        #region getRjhwryAndDgljqy : 根据面积得到人均清理垃圾数，单个人员承担的环卫面积


        public ArrayList getRjhwryAndDgljqy(string streetcode, string startdate, string enddate)
        {
            ArrayList arr = new ArrayList();
            List<Area> streets = Public.GetAreaInfo(streetcode);

            for (int i = 0; i < streets.Count; i++)
            {
                Hashtable t1 = new Hashtable();

                StatisticDAO dao = new StatisticDAO();

                DataTable dt = dao.getLjqyInfo(streets[i].areaCode, startdate, enddate);
                t1.Add("streetname", streets[i].areaName);
                t1.Add("plannum",Convert.ToDecimal(dt.Rows[0]["plannum"].ToString()));
                //t1.Add("actualnum", dt.Columns["actualnum"]);
                arr.Add(t1);
            }
            return arr;
            
        }
        //public ArrayList getRjhwryAndDgljqy(string streetcode, string organID)
        //{
        //    ArrayList arr = new ArrayList();
        //    List<Area> streets = Public.GetAreaInfo(streetcode);
        //    for (int i = 0; i < streets.Count; i++)
        //    {
        //        Hashtable t1 = new Hashtable();
        //        StatisticType statisticType = this.GetLjqyInfo(streets[i].areaCode, streets[i].areaPopulation, streets[i].area, organID);
        //        t1.Add("streetcode", statisticType.streetcode);
        //        t1.Add("streetname", streets[i].areaName);
        //        t1.Add("PersonnelCount", statisticType.personnelCount);
        //        t1.Add("Avgpopulation", statisticType.Avgpopulation);
        //        t1.Add("Avgarea", statisticType.Avgarea);
        //        t1.Add("actualclearnum", statisticType.actualclearnum);
        //        arr.Add(t1);
        //    }
        //    return arr;
        //}
        #endregion

        #region getRjShouna : 受纳场
        public ArrayList getRjShouna(string streetcode, string tjdate1, string tjdate2)
        {
            ArrayList arr = new ArrayList();
            string[] arrStr = streetcode.Split(',');
            for (int i = 0; i < arrStr.Length; i++)
            {
                Hashtable t1 = new Hashtable();
                StatisticType statisticType = this.GetShounaInfo(arrStr[i], tjdate1, tjdate2);
                t1.Add("streetcode", statisticType.streetcode);
                if (arrStr[i].Trim() == "")
                    t1.Add("streetname", "");
                else
                    t1.Add("streetname", GetStreetNameByStreetCode(arrStr[i]));
                t1.Add("grefivenum", statisticType.grefivenum);
                t1.Add("lesfivenum", statisticType.lesfivenum);
                t1.Add("acceptdirtnum", statisticType.acceptdirtnum);
                t1.Add("flatsoliddirtnum", statisticType.flatsoliddirtnum);
                t1.Add("germicidal", statisticType.germicidal);
                t1.Add("remark", statisticType.remark);

                arr.Add(t1);
            }

            return arr;
        }
        #endregion

        //初始化受纳场树结构
        public static string GetStreetNameByStreetCode(string StreetCode)
        {
            string StreetName;
            StreetName = "";
            string sql = "select objcode,addname from sde.部件_宝安_受纳场 where objcode='" + StreetCode+"'";
            try
            {
                string connectionString = CommonDatabase.GetConnectionString("SdeConnString");
                CommonDatabase commondatabase = new CommonDatabase(connectionString);
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);


                while (rs.Read())
                {
                    StreetName = rs["addname"].ToString();
                }

                rs.Close();
                return StreetName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region GetShounaInfo : 调用存储过程计算收纳场
        public StatisticType GetShounaInfo(string streetcode, string tjdate1, string tjdate2)
        {
            StatisticType stats = new StatisticType();
            object grefivenum = null, lesfivenum = null, acceptdirtnum = null, flatsoliddirtnum = null, germicidal = null;//, remark = null;
            using (bacgDL.environment.statistics.StatisticDAO dao = new bacgDL.environment.statistics.StatisticDAO())
            {
                dao.getRjShouna(streetcode,
                     tjdate1, tjdate2, ref grefivenum, ref lesfivenum, ref acceptdirtnum, ref flatsoliddirtnum, ref germicidal);
            }
            //执行过后返回的参数值
            stats.streetcode = streetcode;
            stats.dealdate = Convert.ToString(tjdate1);
            stats.grefivenum = Convert.ToDecimal(grefivenum);
            stats.lesfivenum = Convert.ToDecimal(lesfivenum);
            stats.acceptdirtnum = Convert.ToDecimal(acceptdirtnum);
            stats.flatsoliddirtnum = Convert.ToDecimal(flatsoliddirtnum);
            stats.germicidal = Convert.ToDecimal(germicidal);
            return stats;
        }
        #endregion

        #region getRjhwryAndDgynzt : 根据时间、类型统计余泥渣土信息

        public ArrayList getRjhwryAndDgynzt(string streetcode, int dealtype, string tjdate1, string tjdate2)
        {
            ArrayList arr = new ArrayList();
            List<Area> streets = Public.GetAreaInfo(streetcode);
            for (int i = 0; i < streets.Count; i++)
            {
                Hashtable t1 = new Hashtable();
                StatisticType statisticType = this.GetYnztInfo(streets[i].areaCode, dealtype, tjdate1, tjdate2);
                t1.Add("streetcode", streets[i].areaCode);
                t1.Add("streetname", streets[i].areaName);

                t1.Add("cleardirtnum", statisticType.cleardirtnum);
                t1.Add("washroadnum", statisticType.washroadnum);
                t1.Add("sprinklecarnum", statisticType.sprinklecarnum);
                t1.Add("newbuilnum", statisticType.newbuilnum);
                t1.Add("repairbuilnum", statisticType.repairbuilnum);

                arr.Add(t1);
            }
            return arr;
        }
        #endregion
        
        #region getbajdAndAvgSum : 根据区域得到区域内环卫设施数量、人口和土地面积

        public ArrayList getbajdAndAvgSum(string streetcode, string type, string typename)
	    {
            ArrayList arr = new ArrayList();
            List<Area> street = Public.GetAreaInfo(streetcode);
            string[] arrtype = type.Split(',');
            string[] arrtypename = typename.Split(',');
            for (int i = 0; i < street.Count;i++)
            {
                for (int j = 0; j < arrtype.Length; j++)
                {
                    Hashtable t1 = new Hashtable();
                    StatisticType statisticType = this.GetEstablishmentStatInfo(street[i].areaCode, street[i].areaPopulation, street[i].area, arrtype[j]);
                    t1.Add("streetcode", street[i].areaCode);
                    t1.Add("streetname", street[i].areaName);
                    t1.Add("typename", arrtypename[j]);
                    t1.Add("population", street[i].areaPopulation);
                    t1.Add("area", street[i].area);
                    t1.Add("typeCount", statisticType.typeCount);
                    t1.Add("Avgpopulation", statisticType.Avgpopulation);
                    t1.Add("Avgarea", statisticType.Avgarea);
                    arr.Add(t1);
                }
            }
            return arr;
        }
        #endregion
        
        #region getequipmentAndAvgSum : 根据区域得到区域内环卫设备数量、人口和土地面积

        public ArrayList getequipmentAndAvgSum(string streetcode, string equipmenttype)
        {
            ArrayList arr = new ArrayList();
            List<Area> street = Public.GetAreaInfo(streetcode);
            string[] arrequipment = equipmenttype.Split(',');
            for (int i = 0; i < street.Count; i++)
            {
                for (int j = 0; j < arrequipment.Length; j++)
                {
                    Hashtable t1 = new Hashtable();
                    StatisticType statisticType = this.GetEquipmentInfo(street[i].areaCode, street[i].areaPopulation, street[i].area, arrequipment[j]);
                    t1.Add("streetname", street[i].areaName);
                    t1.Add("equipmentname", arrequipment[j]);
                    t1.Add("equipmentCount", statisticType.equipmentCount);
                    t1.Add("Avgpopulation", statisticType.Avgpopulation);
                    t1.Add("Avgarea", statisticType.Avgarea);
                    arr.Add(t1);
                }
            }
            return arr;
        }
        #endregion

        #region GetEstablishmentStatInfo : 调用储存过程计算环卫设施统计的信息

        public StatisticType GetEstablishmentStatInfo(string streetcode, string population, string area, string type)
	    {
            StatisticType stats = new StatisticType();
            DataSet ds=new DataSet();

            //调用执行存储过程　
            statisticDAO.getEstablishmentInfo(ref ds, streetcode, population, area, type);
            
            //执行过后返回的参数值
            stats.Avgarea = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avgarea"]);
            stats.Avgpopulation = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avgpopulation"]);
            stats.typeCount = Convert.ToInt32(ds.Tables[0].Rows[0]["typeCount"]);
            return stats;
        }
        #endregion

        #region GetPersonnelStatInfo：调用储存过程计算环卫人员统计的信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="streetcode"></param>
        /// <param name="population"></param>
        /// <param name="area"></param>
        /// <param name="organID"></param>
        /// <returns></returns>
        private StatisticType GetPersonnelStatInfo(string streetcode, string population, string area, string organID)
        {
            StatisticType stats = new StatisticType();
            object Avgpopulation = null, Avgarea = null, personnelCount = null;
            using (bacgDL.environment.statistics.StatisticDAO dao = new bacgDL.environment.statistics.StatisticDAO())
            {

                dao.GetPersonnelStatInfo(streetcode, population, area, organID,
                    ref Avgpopulation,ref Avgarea,ref personnelCount);
                
            }

            //执行过后返回的参数值
            stats.Avgarea = Convert.ToDecimal(Avgarea);
            stats.Avgpopulation = Convert.ToDecimal(Avgpopulation);
            stats.personnelCount = Convert.ToInt32( personnelCount);

            return stats;
        }
        #endregion

        #region GetXunchaInfo : 调用储存过程计算巡查人员统计的信息

        //public StatisticType GetXunchaInfo(string streetcode, string population, string area, string organID)
        //{
        //    StatisticType stats = new StatisticType();
        //    object Avgpopulation = null, Avgarea = null, personnelCount = null;
        //    using (bacgDL.environment.statistics.StatisticDAO dao = new bacgDL.environment.statistics.StatisticDAO())
        //    {
        //        dao.getXunchaInfo(streetcode, population, area, organID,
        //            ref Avgpopulation, ref Avgarea, ref personnelCount);
        //    }
        //    //执行过后返回的参数值
        //    stats.Avgarea = Convert.ToDecimal(Avgarea);
        //    stats.Avgpopulation = Convert.ToDecimal(Avgpopulation);
        //    stats.personnelCount = Convert.ToInt32(personnelCount);
        //    return stats;
        //}
        #endregion

        #region : GetLjqyInfo 调用存储过程计算垃圾清运统计的信息

        //public StatisticType GetLjqyInfo(string streetcode, string startdate, string enddate)
        //{
        //    StatisticType stats = new StatisticType();
        //    object Avgpopulation = null, Avgarea = null, personnelCount = null, actualclearnum = null;
        //    using (bacgDL.environment.statistics.StatisticDAO dao = new bacgDL.environment.statistics.StatisticDAO())

        //        dao.getRjhwryAndDgljqy(streetcode, startdate, enddate);

        //    //执行过后返回的参数值
        //    stats.Avgarea = Convert.ToDecimal(Avgarea);
        //    stats.Avgpopulation = Convert.ToDecimal(Avgpopulation);
        //    stats.personnelCount = Convert.ToInt32(personnelCount);
        //    stats.actualclearnum = Convert.ToDecimal(actualclearnum);
        //    return stats;
        //}
        #endregion
        
        #region GetYnztInfo : 调用储存过程计算处理淤泥渣土人员统计的信息

        public StatisticType GetYnztInfo(string streetcode,int @dealtype ,string tjdate1,string tjdate2)
        {
            StatisticType stats = new StatisticType();
            object cleardirtnum = null, washroadnum = null, sprinklecarnum = null, newbuilnum = null, repairbuilnum = null;

            using (bacgDL.environment.statistics.StatisticDAO dao = new bacgDL.environment.statistics.StatisticDAO())
            {
                dao.getYnztInfo(streetcode, @dealtype, tjdate1,tjdate2,
                    ref cleardirtnum, ref washroadnum, ref sprinklecarnum, ref newbuilnum, ref repairbuilnum);

            }

            //执行过后返回的参数值
            stats.cleardirtnum = Convert.ToDecimal(cleardirtnum);
            stats.washroadnum = Convert.ToDecimal(washroadnum);
            stats.sprinklecarnum = Convert.ToDecimal(sprinklecarnum);
            stats.newbuilnum = Convert.ToDecimal(newbuilnum);
            stats.repairbuilnum = Convert.ToDecimal(repairbuilnum); 

            return stats;

        }
        #endregion
        
        #region GetEquipmentInfo : 调用储存过程计算环卫设备统计的信息

        public StatisticType GetEquipmentInfo(string streetcode, string population, string area, string equipmenttype)
        {
            StatisticType stats = new StatisticType();
            if (GPSsql.State == ConnectionState.Closed)
            {
                GPSsql.Open();
            }
            //创建执行存储过程
            SqlCommand sqlcommand = new SqlCommand("TJ_CarInfo", GPSsql);
            sqlcommand.CommandType = CommandType.StoredProcedure;

            //调用执行存储过程　
            statisticDAO.getEquipmentInfo(ref sqlcommand, streetcode, population, area, equipmenttype);

            //执行过后返回的参数值
            stats.Avgarea = Convert.ToDecimal(sqlcommand.Parameters["@Avgarea"].Value);
            stats.Avgpopulation = Convert.ToDecimal(sqlcommand.Parameters["@Avgpopulation"].Value);
            stats.equipmentCount = Convert.ToInt32(sqlcommand.Parameters["@equipmentCount"].Value);
            sqlcommand.Dispose();
            GPSsql.Close();
            return stats;
        }
        #endregion

        #region getInfoByArea : 根据区域得到区域内环卫员工数量、人口和土地面积

        public string[] getInfoByArea(string area)
        {
            string[] num = new string[3];
            //环卫员工数量
            int hwygNum = statisticDAO.getInfoCount("s_personnel","area",area);
            //人口
            int rkNum = 100;
            //面积
            float areaSize = 200;
            num.SetValue(Convert.ToString(hwygNum), 0);
            num.SetValue(Convert.ToString(rkNum), 1);
            num.SetValue(Convert.ToString(areaSize), 2);
            return num;
        }
        #endregion

        #region getInfoByStreet : 根据街道得到区域内环卫员工数量、人口和土地面积

        public string[] getInfoByStreet(string street)
        {
            string[] num = new string[3];
            int hwygNum = statisticDAO.getInfoCount("s_personnel","street",street);
            int rkNum = 10;
            float streetSize = 20;
            num.SetValue(Convert.ToString(hwygNum), 0);
            num.SetValue(Convert.ToString(rkNum), 1);
            num.SetValue(Convert.ToString(streetSize), 2);
            return num;
        }
        #endregion

        #region getInfoByCommunity : 根据社区得到区域内环卫员工数量、人口和土地面积

        public string[] getInfoByCommunity(string community)
        {
            string[] num = new string[3];
            int hwygNum =  statisticDAO.getInfoCount("s_personnel","community",community);;
            int rkNum = 0;
            float communitySize = 0;
            num.SetValue(hwygNum, 0);
            num.SetValue(rkNum, 1);
            num.SetValue(communitySize, 2);
            return num;
        }
        #endregion

        #region getCarTypeInfo : 加载车辆类型
        public DataTable getCarTypeInfo()
        {
            DataTable dt = new DataTable();
            dt = statisticDAO.getCarInfo();
            return dt;
        }
        #endregion
    }
}
