/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：绿地部件管理
 * 结构组成：
 * 作    者：ycg
 * 创建日期：2007-05-29
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using bacgDL.greenland;
using bacgDL;
using Teamax.Common;

namespace bacgBL.greenland
{

    public class SEDDataManage
    {
        /// <summary>
        /// 获取绿地列表
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Teamax.Common.PageManage GetGreenLandList(GreenLendQuery sq, string select, string layer)
        {
            DataProvider dp = new DataProvider();
            QueryUtil qu = dp.GetGreenLendList(sq, select, layer);
            Teamax.Common.PageManage pm = new Teamax.Common.PageManage();
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }

        public string GetDutyArea(string departcode)
        {
            string areas = "";
            if (departcode != "")
            {
                DataProvider dp = new DataProvider();
                DataTable dt = dp.GetDutyArea(departcode);
                List<string> arealist = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!arealist.Contains(dt.Rows[i][0].ToString()))
                        arealist.Add(dt.Rows[i][0].ToString());
                }
                for (int i = 0; i < arealist.Count; i++)
                {
                    if (i == 0)
                        areas = arealist[i];
                    else
                    {

                        areas += "," + arealist[i];
                    }
                }
            }
            return areas;
        }


        /// <summary>
        /// 获取古树名木列表
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Teamax.Common.PageManage GetFamouTreeList(FamouTreeQuery sq, string select, string layer)
        {
            DataProvider dp = new DataProvider();
            QueryUtil qu = dp.GetFamouTreeList(sq, select, layer);
            Teamax.Common.PageManage pm = new Teamax.Common.PageManage();
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }

        /// <summary>
        /// 获取绿地护栏列表
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Teamax.Common.PageManage GetGuardrailList(GreenLendQuery sq, string select, string layer)
        {
            DataProvider dp = new DataProvider();
            QueryUtil qu = dp.GetGuardrailList(sq, select, layer);
            Teamax.Common.PageManage pm = new Teamax.Common.PageManage();
            pm.ds = qu.ds;
            pm.pageCount = qu.PageCount;
            pm.pageSize = qu.PageSize;
            pm.rowCount = qu.RowCount;
            return pm;
        }

        /// <summary>
        /// 获取部件详细数据
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="layer"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SetOBJInfo(Hashtable ht, string layer, string type)
        {
            string OBJECTID = ((ObjProperty)(ht["OBJECTID"])).value;
            ((ObjProperty)(ht["OBJNAME"])).value = layer.Substring(layer.LastIndexOf('_') + 1);
            string insertStr = "", strUpdate = "";
            string insertStr1 = "";
            string insertStr2 = "";
            int pk = 0;

            DataProvider dp = new DataProvider();

            if (type == "delete")
            {
                string strDel = "delete from" + layer + " where OBJECTID =" + OBJECTID;
                if (dp.SetOBJInfo(strDel) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            foreach (ObjProperty op in ht.Values)
            {
                if (type == "update")
                {
                    GetOBJUpdateStr(op, ref strUpdate);
                }
                else if (type == "add")
                {
                    //pk = dp.selectMaxIndex(layer, "OBJECTID", "", "");
                    GetOBJInsertStr(op, ref insertStr1, ref insertStr2);
                }
            }
            if (type == "update")
            {
                if (strUpdate == "")
                {
                    return false;
                }
                strUpdate = "UPDATE " + layer + " set " + strUpdate + " where OBJECTID =" + OBJECTID;
                if (dp.SetOBJInfo(strUpdate) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == "add")
            {
                if (insertStr1 == "")
                {
                    return false;
                }
                //insertStr1 = "OBJECTID," + insertStr1;
                //insertStr2 = pk + "," + insertStr2;
                insertStr = "INSERT INTO " + layer + " (" + insertStr1 + ") VALUES(" + insertStr2 + ")";
                if (dp.SetOBJInfo(insertStr) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 构造插入sql
        /// </summary>
        /// <param name="op"></param>
        /// <param name="insertStr1"></param>
        /// <param name="insertStr2"></param>
        private static void GetOBJInsertStr(ObjProperty op, ref string insertStr1, ref string insertStr2)
        {
            if (op.value != "" && op.value != null && op.key != "OBJECTID")
            {
                if (insertStr1 == "")
                {
                    insertStr1 = op.key;
                    switch (op.type)
                    {
                        case "number":
                            insertStr2 = op.value;
                            break;
                        case "date":
                            insertStr2 = "cast('" + op.value + "' as datetime)";
                            break;
                        default:
                            insertStr2 = "'" + op.value + "'";
                            break;
                    }
                }
                else
                {
                    insertStr1 += "," + op.key;
                    switch (op.type)
                    {
                        case "number":
                            insertStr2 += "," + op.value;
                            break;
                        case "date":
                            insertStr2 += "," + "cast('" + op.value + "' as datetime)";
                            break;
                        default:
                            insertStr2 += "," + "'" + op.value + "'";
                            break;
                    }
                }
            }
            //else if (op.key == "OBJECTID")
            //{
            //    if (insertStr1 == "")
            //    {
            //        insertStr1 = op.key;
            //        insertStr2 = Convert.ToString(pk);
            //    }
            //    else
            //    {
            //        insertStr1 += "," + op.key;
            //        insertStr2 += "," + pk;
            //    }
            //}
        }

        /// <summary>
        /// 构造更新sql
        /// </summary>
        /// <param name="op"></param>
        /// <param name="strUpdate"></param>
        /// <returns></returns>
        private static string GetOBJUpdateStr(ObjProperty op, ref string strUpdate)
        {
            if (op.value != null && op.key != "OBJECTID" && op.key != "OBJCODE")
            {
                if (strUpdate == "")
                {
                    switch (op.type)
                    {
                        case "number":
                            if (op.value != "")
                                strUpdate = op.key + "=" + op.value;
                            else
                                strUpdate = op.key + "=" + 0;
                            break;
                        case "date":
                            strUpdate = op.key + "=cast('" + op.value + "' as datetime)";
                            break;
                        default:
                            strUpdate = op.key + "='" + op.value + "'";
                            break;
                    }
                }
                else
                {
                    switch (op.type)
                    {
                        case "number":
                            if (op.value != "")
                                strUpdate += "," + op.key + "=" + op.value;
                            else
                                strUpdate += "," + op.key + "=" + 0;
                            break;
                        case "date":
                            strUpdate += "," + op.key + "=cast('" + op.value + "' as datetime)";
                            break;
                        default:
                            strUpdate += "," + op.key + "='" + op.value + "'";
                            break;
                    }

                }
            }
            return strUpdate;
        }

        /// <summary>
        /// 对评价系统的数据操作
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public List<Appraise> GetAppraiseManage(Appraise appraise)
        {
            DataProvider dp = new DataProvider();
            DataTable dt = dp.AppraiseManage(appraise);
            List<Appraise> list = new List<Appraise>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Appraise app = new Appraise();
                app.objcode = Convert.ToInt32(dt.Rows[i]["objCode"]);
                app.username = dt.Rows[i]["username"].ToString(); ;
                app.level = Convert.ToInt32(dt.Rows[i]["_level"]);
                app.info = dt.Rows[i]["info"].ToString();
                app.memo = dt.Rows[i]["memo"].ToString();
                app.datetime = dt.Rows[i]["udate"].ToString();
                list.Add(app);
            }
            return list;
        }

        /// <summary>
        /// 获取绿地分类
        /// </summary>
        /// <returns></returns>
        public static string[] GetGreenLandType()
        {
            //string[] names = new string[] { "社会公共类", "道路旁", "公园绿地","广场绿地", "厂前绿地", "道路旁绿地", "住宅绿地", "街道绿地", "分车带", "绿地", "非经营性" };
            
            DataSet ds = null;
            string sql = "select codename from dbo.s_codedict where codetype = 7 and codeid >0 and isnull(status,0) = 0 ";
            using (CommonDatabase cmd = new CommonDatabase(CommonDatabase.GetConnectionString("ConnString")))
            {
               ds = cmd.ExecuteDataset(sql);
            }
            string[] names = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = String.Format("{0}", ds.Tables[0].Rows[i]["codename"]);
            }
            return names;
        }
    }
}
