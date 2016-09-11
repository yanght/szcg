using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Teamax.Common;

namespace bacgDL.greenland.GreenLandStat
{
    public class GreenLandStatDao : Teamax.Common.CommonDatabase, IDisposable
    {
        //���ô洢���̻�ȡ�������۽��
        public DataTable GetYhsbData(string departId, string catalog, string strdate, string enddate)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@departId", departId), 
                                new SqlParameter("@catalog", catalog),
                                new SqlParameter("@strdate", strdate),
                                new SqlParameter("@enddate", enddate)};

            DataSet ds = ExecuteDataset("pr_g_TJGreenLandYhsbInfo", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            dt.Columns["departname"].Caption = "������";
            dt.Columns["catalog"].Caption = "�豸����";
            dt.Columns["spec"].Caption = "�豸���";
            dt.Columns["type"].Caption = "�豸�ͺ�";
            dt.Columns["addressnum"].Caption = "�豸����";

            return dt;
        }

        //���ô洢���̻�ȡ�������۽��
        public DataTable GetZzyhData(string streetcode,
                                            string commcode,
                                            string type,
                                            string strdate,
                                            string enddate,
                                            string departId)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@streetcode", streetcode), 
                                new SqlParameter("@commcode", commcode),
                                new SqlParameter("@type",type),
                                new SqlParameter("@strdate",strdate),
                                new SqlParameter("@enddate",enddate),
                                new SqlParameter("@departId",departId)};

            DataSet ds = ExecuteDataset("pr_g_TJGreenLandZzyhInfo", CommandType.StoredProcedure, arrSP);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            dt.Columns["acreage"].Caption = "��ֲ���";
            dt.Columns["amount"].Caption = "��ֲ����";

            return dt;
        }

        #region getPersonnelInfo�����ô�����̼����̻���Աͳ�Ƶ���Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="streetcode"></param>
        /// <param name="population"></param>
        /// <param name="area"></param>
        /// <param name="type"></param>
        /// <param name="Avgpopulation"></param>
        /// <param name="Avgarea"></param>
        /// <param name="PersonnelCount"></param>
        public void GetPersonnelStatInfo(string streetcode, string population, string area, string type,
                    ref object Avgpopulation, ref object Avgarea, ref object PersonnelCount)
        {
            //����Ҫִ�еĴ洢���̵Ĳ�����ֵ


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

            //ִ�д洢����
            this.ExecuteNonQuery("pr_e_TJPersonnelInfoLH", CommandType.StoredProcedure,
                    sqlp1, sqlp2, sqlp3, sqlp5, sqlp6, sqlp7);
            Avgpopulation = sqlp5.Value;
            Avgarea = sqlp6.Value;
            PersonnelCount = sqlp7.Value;
        }
        #endregion
    }
}
