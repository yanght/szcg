using System;
using System.Collections.Generic;
using System.Text;
using bacgDL;
using bacgDL.Pub;
using bacgDL.greenland;
using bacgDL.greenland.GreenLandStat;
using bacgDL.environment.entitys;
using Teamax.Common;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace bacgBL.greenland
{
    public class GreenLandStatBl
    {
        public DataTable getYhsbStatData(string departId, string catalog, string strdate, string enddate)
        {
            GreenLandStatDao dl = new GreenLandStatDao();
            return dl.GetYhsbData(departId, catalog, strdate, enddate);
        }

        public DataTable getZzyhStatData(string streetcode,
                                            string commcode,
                                            string type,
                                            string strdate,
                                            string enddate,
                                            string departId)
        {
            GreenLandStatDao dl = new GreenLandStatDao();
            return dl.GetZzyhData(streetcode, commcode, type, strdate, enddate, departId);
        }

        #region getRjhwryAndChdhwmj : ��������õ��˾��̻���Ա���������̻���Ա�е����������
        public ArrayList getRjhwryAndChdhwmj(string streetcode, string organID)
        {
            ArrayList arr = new ArrayList();
            List<Area> streets = Public.GetAreaInfo(streetcode);
            for (int i = 0; i < streets.Count; i++)
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

        #region getRjhwssAndChdhwmj : ���ݽֵ�/������ʩ����ͳ����������Ӧ��ʩ��������Ͻֵ�/��������˿�������������õ��˾��̻���ʩ����������ʩ�е��̻�����������ͳ������
        /// <summary>
        /// ���ݽֵ�/������ʩ����ͳ����������Ӧ��ʩ����
        /// </summary>
        /// <param name="streetcodes">�ֵ����루����ֵ����á������ָ</param>
        /// <param name="type">��ʩ����(���ʵʩ���á������ָ�)</param>
        /// <param name="typename">��ʩ����</param>
        /// <returns></returns>
        public ArrayList getRjhwssAndChdhwmj(string streetcode, string type, string typename)
        {
            ArrayList arr = new ArrayList();
            List<Area> street = Public.GetAreaInfo(streetcode);
            string[] arrgreenland = type.Split(',');
            string[] arrgreenlandname = typename.Split(',');
            for (int i = 0; i < street.Count; i++)
            {
                for (int j = 0; j < arrgreenland.Length; j++)
                {
                    Hashtable t1 = new Hashtable();
                    StatisticType statisticType = this.GetGreenLnadStatInfo(street[i].areaCode, street[i].areaPopulation, street[i].area, arrgreenland[j]);
                    t1.Add("streetcode", street[i].areaCode);
                    t1.Add("streetname", street[i].areaName);
                    t1.Add("typename", arrgreenlandname[j]);
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

        #region
        public StatisticType GetGreenLnadStatInfo(string streetcode, string population, string area, string type)
	    {
            StatisticType stats = new StatisticType();
            DataSet ds=new DataSet();

            //����ִ�д洢���̡�
            this.getGreenLandInfo(ref ds, streetcode, population, area, type);
            
            //ִ�й��󷵻صĲ���ֵ
            stats.Avgarea = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avgarea"]);
            stats.Avgpopulation = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avgpopulation"]);
            stats.typeCount = Convert.ToInt32(ds.Tables[0].Rows[0]["typeCount"]);
            return stats;
        }
        #endregion

        #region
        public void getGreenLandInfo(ref DataSet ds, string streetcode, string population, string area, string type)
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
                sb.Append("\n SELECT @typeCount=COUNT(*) FROM sde.����_����_�̵ػ���");
                sb.Append("\n WHERE BGCode LIKE @CodeChar");
                sb.Append("\n IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n SET @Avgarea=@typeCount/@area");
                sb.Append("\n END");
                sb.Append("\n END");
                sb.Append("\n ELSE IF @type=2 BEGIN");
                sb.Append("\n SELECT @typeCount=COUNT(*) FROM sde.����_����_�̵�ά����ʩ");
                sb.Append("\n WHERE BGCode LIKE @CodeChar");
                sb.Append("\n IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n SET @Avgarea=@typeCount/@area");
                sb.Append("\n END");
                sb.Append("\n END");
                sb.Append("\n  ELSE IF @type=3 BEGIN");
                sb.Append("\n   SELECT @typeCount=COUNT(*) FROM sde.����_����_������ʩ");
                sb.Append("\n    WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n 	  ELSE IF @type=4 BEGIN");
                sb.Append("\n     SELECT @typeCount=COUNT(*) FROM sde.����_����_���ܻ���");
                sb.Append("\n     WHERE BGCode LIKE @CodeChar");
                sb.Append("\n     IF(@population<>0 AND @area<>0) BEGIN");
                sb.Append("\n     SET @Avgpopulation=@typeCount/@population");
                sb.Append("\n     SET @Avgarea=@typeCount/@area");
                sb.Append("\n     END");
                sb.Append("\n     END");
                sb.Append("\n     SELECT @Avgpopulation as Avgpopulation,@Avgarea as Avgarea,@typeCount as typeCount");
                //����Ҫִ�еĴ洢���̵Ĳ�����ֵ



                SqlParameter[] spInputs = new SqlParameter[]
                {
                    new SqlParameter("@streetcode", streetcode),
                    new SqlParameter("@population", Convert.ToDecimal(population)),
                    new SqlParameter("@area", Convert.ToDecimal(area)),
                    new SqlParameter("@type", Convert.ToInt32(type)),
                };

               using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString("SdeConnString")))
               {
                   ds = cdb.ExecuteDataset(sb.ToString(), CommandType.Text, spInputs);
               }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetPersonnelStatInfo�����ô�����̼����̻���Աͳ�Ƶ���Ϣ
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
            using (bacgDL.greenland.GreenLandStat.GreenLandStatDao dao = new bacgDL.greenland.GreenLandStat.GreenLandStatDao())
            {

                dao.GetPersonnelStatInfo(streetcode, population, area, organID,
                    ref Avgpopulation, ref Avgarea, ref personnelCount);

            }

            //ִ�й��󷵻صĲ���ֵ
            stats.Avgarea = Convert.ToDecimal(Avgarea);
            stats.Avgpopulation = Convert.ToDecimal(Avgpopulation);
            stats.personnelCount = Convert.ToInt32(personnelCount);

            return stats;
        }
        #endregion
    }    
}
