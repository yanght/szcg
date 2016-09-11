using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using bacgDL.greenland;
using bacgDL;
using Teamax.Common;
using System.Configuration;

namespace bacgBL.greenland
{
    public class GreendLandORG
    {
        /// <summary>
        /// 获取绿地基层组织机构
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public ArrayList GetGreendLandORG(string area)
        {
            string sql = "select distinct(DEPTNAME) as DEPTNAME , isnull(substring(OBJCODE,1,9),440306) as ARECODE  from sde.部件_宝安_绿地 where DEPTNAME <> ''";
            if (area != null && area != "")
                sql = sql + "and  charindex('" + area + "',isnull(substring(OBJCODE,1,12),440306))>0"; 
            sql = sql + " order by ARECODE ";
            try
            {
                ArrayList list = new ArrayList();
                string strKey = "SdeConnString";
                //CommonDatabase DAO = null;
                //using (DAO = new CommonDatabase(ConfigurationManager.AppSettings[strKey]));
                CommonDatabase DAO = new CommonDatabase(ConfigurationManager.AppSettings[strKey]);
                {
                    IDataReader rs = DAO.ExecuteReader(sql, null);
                    if (rs != null)
                    {
                        while (rs.Read())
                        {
                            Hashtable table = new Hashtable();
                            string DEPTNAME = rs["DEPTNAME"].ToString();
                            table.Add("DEPTNAME", DEPTNAME);
                            string ARECODE = rs["ARECODE"].ToString();
                            table.Add("ARECODE", ARECODE);
                            list.Add(table);
                        }
                    }
                    rs.Close();
                }
                return list;
            }
            catch
            {
                throw;
            }
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
        /// 获取部件详细数据
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="layer"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool  SetOBJInfo(Hashtable ht,string layer,string type)
        {
            string OBJECTID=((ObjProperty)(ht["OBJECTID"])).value;
            string insertStr ="", strUpdate = "";
            string insertStr1 = "";
            string insertStr2 = "";


            DataProvider dp = new DataProvider();

            if (type == "delete")
            {
                string strDel = "delete " + layer + " where OBJECTID =" + OBJECTID;
                if (dp.SetOBJInfo(strDel) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            foreach(ObjProperty op in ht.Values)
            {
                if (type == "update")
                {
                    GetOBJUpdateStr(op,ref strUpdate);
                }
                else if (type == "add")
                {
                    
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
        }

        /// <summary>
        /// 构造更新sql
        /// </summary>
        /// <param name="op"></param>
        /// <param name="strUpdate"></param>
        /// <returns></returns>
        private static string GetOBJUpdateStr(ObjProperty op,ref string strUpdate)
        {
            if (op.value != "" && op.value != null && op.key != "OBJECTID")
            {
                if (strUpdate == "")
                {
                    switch (op.type)
                    {
                        case "number":
                            strUpdate = op.key + "=" + op.value;
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
                            strUpdate += "," + op.key + "=" + op.value;
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
            DataTable dt=dp.AppraiseManage(appraise);
            List<Appraise> list = new List<Appraise>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Appraise app=new Appraise();
                app.objcode = Convert.ToInt32(dt.Rows[i]["objCode"]);
                app.username = dt.Rows[i]["username"].ToString(); ;
                app.level = Convert.ToInt32(dt.Rows[i]["_level"]);
                app.info = dt.Rows[i]["info"].ToString();
                app.memo = dt.Rows[i]["memo"].ToString();
                app.datetime = dt.Rows[i]["udate"].ToString() ;
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
            string[] names = new string[] { "公园绿地", "街道绿地", "广场绿地", "附属绿地", "防护绿地", "生产绿地", "其他绿地" };
            return names;
        }
    }
}
