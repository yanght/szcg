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
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using bacgDL;
using bacgDL.environment.establishments;
using System.Data;
using Teamax.Common;

namespace bacgBL.environment.establishments
{
	/// <summary>
	/// collecter 的摘要说明。
	/// </summary>
    public class BASE_establishmenthelper 
{
        EstablishmentDAO establishmentDAO = new EstablishmentDAO();
        public BASE_establishmenthelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        public Teamax.Common.PageManage GetEstablishmentListByType(string table, Establishment per, string select, int pageIndex, int pageSize)
        {       
            try
            {
                Teamax.Common.PageManage page = establishmentDAO.GetEstablishmentListByType(table, per, select, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //得到环卫设施的最大ID
        public int getMaxOBJID(string tablename)
        {
            try
            {
                DataTable rs = establishmentDAO.getMaxOBJID(tablename);
                int s = Convert.ToInt32(rs.Rows[0]["OBJECTID"]);
                return Convert.ToInt32(rs.Rows[0]["OBJECTID"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //得到环卫设施详细信息
        public Establishment getOBJInfo(string select,string objcode, string table)
        {
            try
            {
                string from = table;
                string where = "OBJECTID='" + objcode + "'";
                SqlDataReader rs = (SqlDataReader)establishmentDAO.getOBJInfo(select, from, where);
                Establishment per = new Establishment();
                while (rs.Read())
                {
                    per.object_1 = rs["OBJECTID"].ToString();
                    per.compid = rs["compid"].ToString();
                    per.objcode = rs["objcode"].ToString();
                    per.objname = rs["objname"].ToString();
                    per.deptname = rs["deptname"].ToString();
                    per.ownername = rs["ownername"].ToString();
                    per.bgcode = rs["bgcode"].ToString();
                    per.objpos = rs["objpos"].ToString();
                    per.objstate = rs["objstate"].ToString();
                    per.objusestat = rs["objusestat"].ToString();
                    per.ordate = rs["ordate"].ToString();
                    per.chdate = rs["chdate"].ToString();
                    per.mapscale = rs["mapscale"].ToString();
                    per.photo = rs["photo"].ToString();
                    per.note_ = rs["note_"].ToString();
                    if ("sde.部件_宝安_公厕指示牌" == from)
                    {
                        per.spectype = rs["spectype"].ToString();
                        per.material = rs["material"].ToString();

                    }
                    else if ("sde.部件_宝安_化粪池" == from)
                    {
                        per.objcertnum = rs["objcertnum"].ToString();
                        per.objtype = rs["objtype"].ToString();
                        per.capacity = rs["capacity"].ToString();
                        per.addarea = rs["addarea"].ToString();
                        per.structure = rs["structure"].ToString();
                        per.addower = rs["addower"].ToString();
                        per.material = rs["material"].ToString();
                    }


                }
                return per;
            }
            catch 
            {
                throw;
            }
        }
        //得到环卫设施详细信息
        public Hashtable getOBJInfoHash(string select, string OBJECTID, string from)
        {
            try
            {
                //string from = table;
                string where = "OBJECTID='" + OBJECTID + "'";
                SqlDataReader rs = (SqlDataReader)establishmentDAO.getOBJInfo(select, from, where);
                Hashtable table = new Hashtable();
                while (rs.Read())
                {
                    table.Add("objectid", rs["objectid"].ToString());
                    table.Add("compid", rs["compid"].ToString());
                    table.Add("objcode", rs["objcode"].ToString());
                    table.Add("bgcode", rs["bgcode"].ToString());
                    table.Add("objname", rs["objname"].ToString());
                    table.Add("deptname", rs["deptname"].ToString());
                    table.Add("ownername", rs["ownername"].ToString());
                    table.Add("objpos", rs["objpos"].ToString());
                    table.Add("objstate", rs["objstate"].ToString());
                    table.Add("objusestat", rs["objusestat"].ToString());
                    table.Add("ordate", rs["ordate"].ToString() == "1900-1-1 0:00:00" ? "" : rs["ordate"].ToString());
                    table.Add("chdate", rs["chdate"].ToString() == "1900-1-1 0:00:00" ? "" : rs["chdate"].ToString());
                    table.Add("mapscale", rs["mapscale"].ToString());
                    table.Add("photo", rs["photo"].ToString());
                    table.Add("note_", rs["note_"].ToString());

                    if ("sde.部件_宝安_公厕指示牌" == from)
                    {
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("material", rs["material"].ToString());

                    }
                    else if ("sde.部件_宝安_化粪池" == from)
                    {
                        table.Add("objcertnum", rs["objcertnum"].ToString());
                        table.Add("objtype", rs["objtype"].ToString());
                        table.Add("capacity", rs["capacity"].ToString());
                        table.Add("addarea", rs["addarea"].ToString());
                        table.Add("structure", rs["structure"].ToString());
                        table.Add("addowner", rs["addowner"].ToString());
                        table.Add("material", rs["material"].ToString());
                    }
                    else if ("sde.部件_宝安_公共厕所" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("objcertnum", rs["objcertnum"].ToString());
                        table.Add("objtype", rs["objtype"].ToString());
                        table.Add("addarea", rs["addarea"].ToString());
                        table.Add("toolarea", rs["toolarea"].ToString());
                        table.Add("mannum", rs["mannum"].ToString());
                        table.Add("womannum", rs["womannum"].ToString());
                        table.Add("peenum", rs["peenum"].ToString());
                        table.Add("starttime", rs["starttime"].ToString());
                        table.Add("addowner", rs["addowner"].ToString());
                    }
                    else if ("sde.部件_宝安_垃圾箱" == from)
                    {
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("material", rs["material"].ToString());
                        table.Add("capacity", rs["capacity"].ToString());
                        table.Add("addowner", rs["addowner"].ToString());
                        table.Add("userdept", rs["userdept"].ToString());
                    }
                    else if ("sde.部件_宝安_中转站" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("addarea", rs["addarea"].ToString());
                        table.Add("dealwith", rs["dealwith"].ToString());
                        table.Add("starttime", rs["starttime"].ToString());
                    }
                    else if ("sde.部件_宝安_垃圾间楼" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("addarea", rs["addarea"].ToString());
                        table.Add("toolarea", rs["toolarea"].ToString());
                        table.Add("objtype", rs["objtype"].ToString());
                        table.Add("dealwith", rs["dealwith"].ToString());
                        table.Add("starttime", rs["starttime"].ToString());
                        table.Add("addowner", rs["addowner"].ToString());
                    }
                    else if ("sde.部件_宝安_环卫工具房" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("material", rs["material"].ToString());
                        table.Add("addowner", rs["addowner"].ToString());
                    }
                    else if ("sde.部件_宝安_主干道" == from)
                    {

                        table.Add("addname", rs["addname"].ToString());
                        table.Add("direction", rs["direction"].ToString());
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("structure", rs["structure"].ToString());
              
                    }
                    else if ("sde.部件_宝安_次干道" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("direction", rs["direction"].ToString());
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("structure", rs["structure"].ToString());
                    }
                    else if ("sde.部件_宝安_支路" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("direction", rs["direction"].ToString());
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("structure", rs["structure"].ToString());
                    }
                    else if ("sde.部件_宝安_街坊路" == from)
                    {
                        table.Add("addname", rs["addname"].ToString());
                        table.Add("direction", rs["direction"].ToString());
                        table.Add("spectype", rs["spectype"].ToString());
                        table.Add("structure", rs["structure"].ToString());
                    }

                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //新增一条设施信息
        public int insertIntoEstablishment(Hashtable obj, string tablel, string select)
        {
            try
            {
                int i = establishmentDAO.insertIntoEstablishment(obj, tablel, select);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //更新一条设施信息
        public int updateEstablishment(Hashtable obj, string tablel, string select)
        {
            try
            {
                int i = establishmentDAO.updateEstablishment(obj, tablel, select);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //删除一条设施信息
        public int deleEstablishment(string table, string OBJECTIDs)
        {
            try
            {
                int i = establishmentDAO.deleEstablishment(table, OBJECTIDs);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
	}
}
