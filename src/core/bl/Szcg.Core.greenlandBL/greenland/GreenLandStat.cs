/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;��԰��ͳ���߼���
 * �ṹ��ɣ�
 * ��    �ߣ�ycg
 * �������ڣ�2007-05-29
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using bacgDL.Pub;
using bacgDL.greenland;
using Teamax.Common;

namespace bacgBL.greenland
{
    public class StatData
    {
        /// <summary>
        /// ���������
        /// </summary>
        public decimal Total=0;
        /// <summary>
        /// �̵������
        /// </summary>
        public decimal GTotal = 0;
        /// <summary>
        /// ԰���̵���
        /// </summary>
        public decimal PGRate = 0;
        /// <summary>
        /// ԰���̵ظ��������
        /// </summary>
        public decimal CGPTotal = 0;
        /// <summary>
        /// ��԰����
        /// </summary>
        public decimal PNumber = 0;
        /// <summary>
        ///��԰�����
        /// </summary>
        public decimal PTotal = 0;
        /// <summary>
        /// ��԰�ܸ������
        /// </summary>
        public decimal CPTotal = 0;
        /// <summary>
        /// �㳡����
        /// </summary>
        public decimal PlazaNumber = 0;
        /// <summary>
        /// �㳡�����
        /// </summary>
        public decimal PlazaTotal = 0;
        /// <summary>
        /// �㳡���������
        /// </summary>
        public decimal CPlazaTotal = 0;
        /// <summary>
        /// �ֵ������
        /// </summary>
        public decimal STotal = 0;
        /// <summary>
        /// �ֵ����������
        /// </summary>
        public decimal CSTotal = 0;
        /// <summary>
        /// ��԰�̵������
        /// </summary>
        public decimal ParkTotal = 0;
        /// <summary>
        /// ��԰�̵ظ��������
        /// </summary>
        public decimal CParkTotal = 0;
        /// <summary>
        /// �����̵������
        /// </summary>
        public decimal AttTotal = 0;
        /// <summary>
        /// �����̵ظ��������
        /// </summary>
        public decimal CAttTotal = 0;
        /// <summary>
        /// �����̵������
        /// </summary>
        public decimal GuardTotal = 0;
        /// <summary>
        /// �����̵ظ��������
        /// </summary>
        public decimal CGuardTotal = 0;
        /// <summary>
        /// �����̵������
        /// </summary>
        public decimal ProduceTotal = 0;
        /// <summary>
        /// �����̵ظ������
        /// </summary>
        public decimal CProduceTotal = 0;
        /// <summary>
        /// �����̵������
        /// </summary>
        public decimal OtherTotal = 0;
        /// <summary>
        /// �����̵ظ������
        /// </summary>
        public decimal COtherTotal = 0;
        /// <summary>
        /// �̵ظ��������
        /// </summary>
        public decimal CGTotal = 0;
        /// <summary>
        /// �е����̻����������
        /// </summary>
        public decimal CTreeTotal = 0;
        /// <summary>
        /// �̻�������
        /// </summary>
        public decimal GRate = 0;

    }



    public class GreenLandStat
    {

        /// <summary>
        /// ��ȡͳ����ϸ��Ϣ
        /// </summary>
        /// <param name="bgCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public StatData GetStatDataInfo(string bgCode, string startDate, string endDate)
        {
            string strCacheKey=bgCode + startDate + endDate;
            StatData sd = (StatData)MyCache.Get(strCacheKey);//��ȡ������Ϣ
            if (sd != null)
                return sd;

            DataProvider dp = new DataProvider();
            sd = new StatData();
            List<Area> list = Public.GetAreaInfo(bgCode);
            sd.Total = Convert.ToDecimal(list[0].area) * 100;//��λ���� 
            
            
            DataTable dt = dp.GetGreenLandStatInfo(bgCode, startDate, endDate);

            sd.GTotal = Convert.ToDecimal(dt.Rows[0]["GTotal"].ToString() == "" ? 0 : dt.Rows[0]["GTotal"])/10000;
            sd.STotal = Convert.ToDecimal(dt.Rows[0]["STotal"].ToString() == "" ? 0 : dt.Rows[0]["STotal"]) / 10000;
            sd.PTotal = Convert.ToDecimal(dt.Rows[0]["PTotal"].ToString() == "" ? 0 : dt.Rows[0]["PTotal"]) / 10000;
            sd.ProduceTotal = Convert.ToDecimal(dt.Rows[0]["ProduceTotal"].ToString() == "" ? 0 : dt.Rows[0]["ProduceTotal"]) / 10000;
            sd.PNumber = Convert.ToDecimal(dt.Rows[0]["PNumber"].ToString() == "" ? 0 : dt.Rows[0]["PNumber"]);
            sd.PlazaTotal = Convert.ToDecimal(dt.Rows[0]["PlazaTotal"].ToString() == "" ? 0 : dt.Rows[0]["PlazaTotal"]) / 10000;
            sd.PlazaNumber = Convert.ToDecimal(dt.Rows[0]["PlazaNumber"].ToString() == "" ? 0 : dt.Rows[0]["PlazaNumber"]);
            sd.ParkTotal = Convert.ToDecimal(dt.Rows[0]["ParkTotal"].ToString() == "" ? 0 : dt.Rows[0]["ParkTotal"]) / 10000;
            sd.OtherTotal = Convert.ToDecimal(dt.Rows[0]["OtherTotal"].ToString() == "" ? 0 : dt.Rows[0]["OtherTotal"]) / 10000;
            sd.GuardTotal = Convert.ToDecimal(dt.Rows[0]["GuardTotal"].ToString() == "" ? 0 : dt.Rows[0]["GuardTotal"]) / 10000;
            sd.CTreeTotal = Convert.ToDecimal(dt.Rows[0]["CTreeTotal"].ToString() == "" ? 0 : dt.Rows[0]["CTreeTotal"]) / 10000;
            sd.CSTotal = Convert.ToDecimal(dt.Rows[0]["CSTotal"].ToString() == "" ? 0 : dt.Rows[0]["CSTotal"]) / 10000;
            sd.CPTotal = Convert.ToDecimal(dt.Rows[0]["CPTotal"].ToString() == "" ? 0 : dt.Rows[0]["CPTotal"]) / 10000;
            sd.CProduceTotal = Convert.ToDecimal(dt.Rows[0]["CProduceTotal"].ToString() == "" ? 0 : dt.Rows[0]["CProduceTotal"]) / 10000;
            sd.CPlazaTotal = Convert.ToDecimal(dt.Rows[0]["CPlazaTotal"].ToString() == "" ? 0 : dt.Rows[0]["CPlazaTotal"]) / 10000;
            sd.CParkTotal = Convert.ToDecimal(dt.Rows[0]["CParkTotal"].ToString() == "" ? 0 : dt.Rows[0]["CParkTotal"]) / 10000;
            sd.COtherTotal = Convert.ToDecimal(dt.Rows[0]["COtherTotal"].ToString() == "" ? 0 : dt.Rows[0]["COtherTotal"]) / 10000;
            sd.CGuardTotal = Convert.ToDecimal(dt.Rows[0]["CGuardTotal"].ToString() == "" ? 0 : dt.Rows[0]["CGuardTotal"]) / 10000;
            sd.CGTotal = Convert.ToDecimal(dt.Rows[0]["CGTotal"].ToString() == "" ? 0 : dt.Rows[0]["CGTotal"]) / 10000;
            sd.CAttTotal = Convert.ToDecimal(dt.Rows[0]["CAttTotal"].ToString() == "" ? 0 : dt.Rows[0]["CAttTotal"]) / 10000;
            sd.CGPTotal = Convert.ToDecimal(dt.Rows[0]["CGPTotal"].ToString() == "" ? 0 : dt.Rows[0]["CGPTotal"]) / 10000;
            sd.AttTotal = Convert.ToDecimal(dt.Rows[0]["AttTotal"].ToString() == "" ? 0 : dt.Rows[0]["AttTotal"]) / 10000;
            if (sd.Total == 0)
                sd.Total = 1;
            if (sd.GTotal == 0)
                sd.GTotal = 1;
            sd.PGRate = (sd.GTotal / sd.Total) * 100;
            sd.GRate = (sd.CGTotal / sd.Total) * 100;
            MyCache.Insert(strCacheKey, sd, 1200);
            return sd;
        }

        /// <summary>
        /// ��ȡͳ����Ϣ�б�
        /// </summary>
        /// <param name="bgCode"></param>
        /// <param name="type"></param>
        /// <param name="pageindex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetStatDataList(string bgCode, string type)
        {
            string strCacheKey=bgCode + "_" + type + "_greenland";
            DataTable dt = (DataTable)MyCache.Get(strCacheKey);
            if (dt != null)
                return dt;

            DataProvider dp = new DataProvider();
            
            GreenLendQuery sq = new GreenLendQuery();
            sq.bgCode = bgCode;
            sq.objType = type;

            dt = dp.GetGreenLendList(sq);
            Hashtable htPcode = new Hashtable();//���ڵ�
            bgCode = UpdateDataForBgcode(bgCode, dt,htPcode);
            List<Area> list = Public.GetAreaInfo(bgCode);
            Hashtable ht=new Hashtable();//װ��������ϸ��Ϣ
            
            for(int i=0;i<list.Count;i++)
            {
                if(!ht.ContainsKey(list[i].areaCode))
                    ht.Add(list[i].areaCode,list[i]);
            }

            
            dt.Columns.Add("�̻���(%)");
            dt.Columns.Add("Ͻ���˿�");
            dt.Columns.Add("�˾��̻����");
            dt.Columns.Add("PBGCODE");
            dt.Columns["AREARNAME"].Caption = "Ͻ������";
            dt.Columns["BGCODE"].Caption="BGCODE";
            dt.Columns["ADDAREA"].Caption="�̻����(ƽ����)";
            dt.Columns["COVERAREA"].Caption = "�̻��������(ƽ����)";
            dt.Columns["ROADTREEAREA"].Caption = "�е����������(ƽ����)";
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string bgcode = dt.Rows[i]["BGCODE"].ToString();
                    if (bgcode.Length==14)
                    bgcode = bgcode.Substring(0, bgcode.Length - 2);
                    Area area;
                    area = (Area)ht[bgcode];
                    decimal temp= (Convert.ToDecimal(dt.Rows[i]["ADDAREA"].ToString() == "" ? 0 : dt.Rows[i]["ADDAREA"]) / Convert.ToDecimal(area.area));
                    dt.Rows[i]["�̻���(%)"] = temp.ToString("F2");

                    dt.Rows[i]["Ͻ���˿�"] = (Convert.ToDecimal(area.areaPopulation)*10000).ToString("F2");
                    temp = Convert.ToDecimal(dt.Rows[i]["ADDAREA"].ToString() == "" ? 0 : dt.Rows[i]["ADDAREA"]) / Convert.ToDecimal(area.areaPopulation);
                    dt.Rows[i]["�˾��̻����"] = temp.ToString("F2");
                    dt.Rows[i]["AREARNAME"] = area.areaName;
                    dt.Rows[i]["PBGCODE"] =(string)htPcode[bgcode];

                    dt.Rows[i]["ADDAREA"] = Convert.ToDecimal(dt.Rows[i]["ADDAREA"].ToString() == "" ? 0 : dt.Rows[i]["ADDAREA"]).ToString("F2");
                    dt.Rows[i]["COVERAREA"] = Convert.ToDecimal(dt.Rows[i]["COVERAREA"].ToString() == "" ? 0 : dt.Rows[i]["COVERAREA"]).ToString("F2");
                    dt.Rows[i]["ROADTREEAREA"] = Convert.ToDecimal(dt.Rows[i]["ROADTREEAREA"].ToString() == "" ? 0 : dt.Rows[i]["ROADTREEAREA"]).ToString("F2");
                    
                }
                catch { }
            }
            MyCache.Insert(strCacheKey, dt, 1200);
            return dt;
        }

        /// <summary>
        /// ȥ��bgCode��������
        /// </summary>
        /// <param name="bgCode"></param>
        /// <param name="dt"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        private static string UpdateDataForBgcode(string bgCode, DataTable dt, Hashtable ht)
        {
            List<string> bgs = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string bgcode = dt.Rows[i]["BGCODE"].ToString();
                if (!bgs.Contains(bgcode))
                    bgs.Add(bgcode);
            }
            bgCode = "";
            for (int i = 0; i < bgs.Count; i++)
            {
                if (i == 0)
                    bgCode = bgs[i];
                else
                    bgCode += "," + bgs[i];
            }
            for (int i = 0; i < bgs.Count; i++)
            {
                if (bgs[i].Length == 6)
                {
                    if (!ht.ContainsKey(bgs[i]))
                        ht.Add(bgs[i], "");
                }
                else if (bgs[i].Length == 9)
                {
                    if (!bgs.Contains(bgs[i].Substring(0, 6)))
                    {
                        if (!ht.ContainsKey(bgs[i]))
                            ht.Add(bgs[i], "");
                    }
                    else
                    {
                        if (!ht.ContainsKey(bgs[i]))
                            ht.Add(bgs[i], bgs[i].Substring(0, 6));
                    }
                }
                else if (bgs[i].Length == 12)
                {
                    if (bgs.Contains(bgs[i].Substring(0, 9)))
                    {
                        if (!ht.ContainsKey(bgs[i]))
                            ht.Add(bgs[i], bgs[i].Substring(0, 9));
                    }
                    else if (bgs.Contains(bgs[i].Substring(0, 6)))
                    {
                        if (!ht.ContainsKey(bgs[i]))
                            ht.Add(bgs[i], bgs[i].Substring(0, 6));
                    }
                    else
                    {
                        if (!ht.ContainsKey(bgs[i]))
                            ht.Add(bgs[i], "");
                    }
                }
            }
            return bgCode;
        }

    }
}
