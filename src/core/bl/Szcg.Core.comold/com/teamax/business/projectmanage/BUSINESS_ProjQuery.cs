using System;
using szcg.com.teamax.util;
using szcg.com.teamax.business.entity;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Data.SqlClient;
using System.Text;


namespace szcg.com.teamax.business.projectmanage
{
    /// <summary>
    /// BUSINESS_ProjQuery 的摘要说明。
    /// zhanghuagen2006-5-19
    /// </summary>
    public class BUSINESS_ProjQuery
    {

        protected SqlDataReader rs = null;
        protected StringBuilder sb = null;
        protected ArrayList list;
        protected com.teamax.business.entity.ProjInfo pinfo;
        protected com.teamax.business.entity.CollecterInfo cinfo;
        private static string QueryClass = "select bigclass_part.id as bid, bigclass_part.name as bigclassname ,smallclass_part.rolecode, smallclass_part.id as sid, smallclass_part.name as smallname, smallclass_part.t_time from bigclass_part, smallclass_part where bigclass_part.id = smallclass_part.fid and  smallclass_part.id= '?' ";
        private static string QueryEventClass = "select bigclass_event.id as bid,bigclass_event.name as bigclassname,smallclass_event.id as sid,smallclass_event.name as smallname,smallclass_event.t_time from bigclass_event,smallclass_event where bigclass_event.id=smallclass_event.fid and smallclass_event.id='?'";

        //protected szcg.com.teamax.business.entity.CollecterInfo collInfo;
        public BUSINESS_ProjQuery()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        //0625zhg
        private ArrayList getDateByList(String sql, int page)
        {
            try
            {
                string sql1 = "select sum(num) from (" + sql + ") as tt";
                SqlDataReader dr = com.teamax.util.DataAccess.ExecuteReader(sql1, null);
                string counts = "0";
                if (dr.Read())
                {
                    counts = Convert.ToString(dr[0]);
                }
                dr.Close();
                int RowCount;
                int PageCount;
                ArrayList arr = new ArrayList();
                com.teamax.util.DataAccess.cutPage(sql, 12, out RowCount, out PageCount);
                SqlDataReader rdr = com.teamax.util.DataAccess.getPageData(sql, 12, page, "num", "desc", RowCount, PageCount);
                //System.Collections.ArrayList al = new System.Collections.ArrayList();

                arr.Add(Convert.ToString(PageCount) + ";" + counts);
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        ProjInfo pinfo = new ProjInfo();

                        pinfo.setArea(System.Convert.ToString(rdr["area"]));
                        pinfo.setStreet(System.Convert.ToString(rdr["street"]));
                        pinfo.setSquare(System.Convert.ToString(rdr["square"]));
                        pinfo.setSmallclass(System.Convert.ToString(rdr["smallclass"]));
                        pinfo.setBigclass(System.Convert.ToString(rdr["bigclass"]));
                        pinfo.setAllrow(RowCount);
                        pinfo.setProbclass(System.Convert.ToString(rdr["probclass"]));
                        string tempsql = "";
                        if (Convert.ToString(rdr["probclass"]).Equals("0"))
                        {
                            tempsql = QueryClass.Replace("?", System.Convert.ToString(rdr["smallclass"]));
                        }
                        else
                        {
                            tempsql = QueryEventClass.Replace("?", System.Convert.ToString(rdr["smallclass"]));
                        }
                        SqlDataReader temp_dr = com.teamax.util.DataAccess.ExecuteReader(tempsql, null);
                        if (temp_dr.Read())
                        {
                            pinfo.setBigclassname(System.Convert.ToString(temp_dr["bigclassname"]));
                            pinfo.setSmallclassname(System.Convert.ToString(temp_dr["smallname"]));
                        }
                        temp_dr.Close();
                        pinfo.setNum(System.Convert.ToString(rdr["num"]));

                        arr.Add(pinfo);
                    }

                }
                rdr.Close();
                return arr;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
                //throw;
            }
        }
        /// <summary>
        /// 案卷统计 获取区zhanghuagen 05-29
        /// </summary>
        //		public ArrayList getDateByAreas(string strStartDate, string strEndDate, string[] strAreas, bool b1,bool b5,bool b6,bool b7, int page)
        //		{
        //			
        ////			select area , street, square, probclass, bigclass, smallclass, count(*) as _sum from 
        ////			(
        ////			select a.projcode, area, street, square, probclass,bigclass,smallclass
        ////			from b_project a, b_project_detail b 
        ////			where exists (
        ////			select distinct projcode from projtrace where projtrace.projcode = a.projcode and 
        ////			a.projcode = b.projcode 
        ////			) 
        ////			
        ////			and 
        ////			( area like '%罗湖区%' or street like '%罗湖区%' or square like '%罗湖区%') 
        ////			and 
        ////			(   ((a.Stepid = 2) and (a.isdel='0'))  or (a.isdel='1')  ) 
        ////			
        ////			union  
        ////			
        ////			select a.projcode , area, street, square, probclass,bigclass,smallclass
        ////			 from szcg_bak.dbo.b_project a, szcg_bak.dbo.b_project_detail b 
        ////			
        ////			where 
        ////			a.projcode = b.projcode and  ((a.Stepid ='5'  )  and (a.isdel='0'))  
        ////			
        ////			) as t
        ////			
        ////			group by area ,street ,square ,probclass,bigclass, smallclass 
        //
        //
        //			string strSQL = "select area, street, square, probclass,bigclass,smallclass,count(*) as num from (" +
        //				" select a.projcode, area, street, square, probclass,bigclass,smallclass from szcg.dbo.b_project a, szcg.dbo.b_project_detail b where exists ( " +
        //				" select distinct projcode from projtrace where projtrace.projcode = a.projcode and  a.projcode = b.projcode )   " ;
        //		
        //
        //
        //			if(b1)
        //			{
        //				strSQL += " and a.startdate >= '" + strStartDate + "' and a.startdate <= '" + strEndDate + "'";
        //			}
        //
        //			strSQL += " and (";
        //				for( int i = 0; i < strAreas.Length; i++ )
        //				{
        //					if( i != 0 )
        //						strSQL += " or";
        //					strSQL += " area like '%" + strAreas[i] + "%'";
        //					strSQL += " or street like '%" + strAreas[i] + "%'";
        //					strSQL += " or square like '%" + strAreas[i] + "%'";
        //				}
        //				strSQL += ")";
        //		
        //			if(b5 || b6 ||b7)
        //			{
        //				if(b5 || b6 )
        //				{
        //					strSQL=strSQL+" and ( ";
        //				}
        //				else
        //				{
        //					strSQL=strSQL+" and ( 2=3)";
        //				}
        //
        //
        //				string strTag="No";
        //				if (b5)
        //				{
        //					strTag="YES";
        //					strSQL=strSQL+"  ((a.Stepid =2) and (a.isdel='0')) "; //立案
        //				}
        //				if (b6)
        //				{
        //					if (strTag=="No")
        //					{
        //						strTag="YES";
        //						strSQL=strSQL+" (a.isdel='1') ";
        //					}
        //					else
        //					{
        //						strSQL=strSQL+" or (a.isdel='1') ";
        //					}
        //				}			
        //			
        //				if(b5 || b6 )
        //				{
        //					strSQL=strSQL+" ) ";
        //				}
        //
        //				if (b7)
        //				{
        //	
        //					strSQL=strSQL+" union   select a.projcode , area, street, square, probclass,bigclass,smallclass  from szcg_bak.dbo.b_project a, szcg_bak.dbo.b_project_detail b  where   a.projcode = b.projcode " ;
        //					strSQL += " and (";
        //					for( int i = 0; i < strAreas.Length; i++ )
        //					{
        //						if( i != 0 )
        //							strSQL += " or";
        //						strSQL += " area like '%" + strAreas[i] + "%'";
        //						strSQL += " or street like '%" + strAreas[i] + "%'";
        //						strSQL += " or square like '%" + strAreas[i] + "%'";
        //					}
        //					strSQL += ")";
        //
        //					strSQL += " and  ((a.Stepid ='5'  )  and (a.isdel='0'))  ";
        //		
        //				
        //				}
        //			}
        //			else
        //			{
        //			strSQL=strSQL+" and ( 2=3)";
        //			}
        //
        //
        //			strSQL += "  ) as t  Group BY  area,street,square,probclass,bigclass,smallclass";
        //
        //			ArrayList arrs = getDateByList(strSQL,page);
        //			return arrs;
        //
        //		}


        public bool checkArea(string _area)
        {
            string[] area = _area.Split(',');
            for (int i = area.Length - 1; i > 0; i--)
            {
                if (area[i].Length != area[0].Length)
                {
                    if (area[i].IndexOf(area[0]) == -1 && area[0].IndexOf(area[i]) == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        /// <summary>
        /// 案卷统计 zhanghuagen 06-26最后修改
        /// </summary>
        /// <param name="condition"></param>
        ///
        public ArrayList getProbstat(string strStartDate, string strEndDate, string parts, string events, string areacode, string area, string type, int page)
        {
            //			string strSQL = " select area , street, square, probclass, bigclass, smallclass, count(*) as num from  (" +
            //				" select area, street, square, probclass,bigclass,smallclass from b_project, " +
            //				" b_project_detail,bigclass_part,smallclass_part,bigclass_event," +
            //				" smallclass_event  where b_project.projcode=b_project_detail.projcode and (bigclass_part.id=b_project_detail.bigclass " +
            //				" and smallclass_part.id=b_project_detail.smallclass ) and (bigclass_event.id=b_project_detail.bigclass and smallclass_event.id=b_project_detail.smallclass ) " ;
            System.Text.StringBuilder sb = new StringBuilder();

            if (type == "1")
            {
                sb.Append("select ttt.area,ttt.street,ttt.square,ttt.probclass,ttt.bigclass,ttt.smallclass,sum(num) as num from ( ");
                sb.Append("select b_project_detail.area, b_project_detail.street, b_project_detail.square,")
                    .Append("probclass,bigclass, smallclass , count(b_project.projcode) as num ")
                    .Append("from b_project , b_project_detail ")
                    .Append("where exists ( ")
                    .Append("select  grid.gridcode ")
                    .Append("from area , street, community ,grid ")
                    .Append("where area.id = street.fid and street.id = community.fid and community.id = grid.commfid ")
                    .Append("and  b_project.gridcode = grid.gridcode ")
                    .Append("and b_project.projcode = b_project_detail.projcode and stepid <> 0 ");
                sb.Append(" and (b_project.stepid > 2 or (b_project.stepid = '1' and b_project.stateid = '3') or (b_project.stepid = '2' and b_project.stateid='4'))");
            }
            else if (type == "2")
            {
                sb.Append("select b_project_detail.area, b_project_detail.street, b_project_detail.square,")
                    .Append("probclass,bigclass, smallclass , count(b_project.projcode) as num ")
                    .Append("from b_project , b_project_detail ")
                    .Append("where exists ( ")
                    .Append("select  grid.gridcode ")
                    .Append("from area , street, community ,grid ")
                    .Append("where area.id = street.fid and street.id = community.fid and community.id = grid.commfid ")
                    .Append("and  b_project.gridcode = grid.gridcode ")
                    .Append("and b_project.projcode = b_project_detail.projcode and stepid <> 0 ");
                sb.Append(" and b_project.isdel = '1' ");
            }
            else if (type == "3")
            {
                sb.Append("select b_project_detail.area, b_project_detail.street, b_project_detail.square,")
                    .Append("probclass,bigclass, smallclass , count(b_project.projcode) as num ")
                    .Append("from szcg_bak.dbo.b_project , szcg_bak.dbo.b_project_detail ")
                    .Append("where exists ( ")
                    .Append("select  grid.gridcode ")
                    .Append("from area , street, community ,grid ")
                    .Append("where area.id = street.fid and street.id = community.fid and community.id = grid.commfid ")
                    .Append("and  b_project.gridcode = grid.gridcode ")
                    .Append("and b_project.projcode = b_project_detail.projcode ");
            }

            if (area == "")
            {
                sb.Append(" and b_project.gridcode like '" + areacode + "%' ");
            }
            else
            {
                string[] areas = area.Split(',');
                sb.Append(" and ( ");
                for (int h = 0; h < areas.Length; h++)
                {
                    if (areas[h] != null && areas[h].Length == 7)
                    {
                        sb.Append(" area.areacode  like '" + areas[h] + "' or ");
                    }
                    else if (areas[h] != null && areas[h].Length == 9)
                    {
                        sb.Append(" street.streetcode like '" + areas[h] + "' or ");
                    }
                    else if (areas[h] != null && areas[h].Length == 12)
                    {
                        sb.Append(" community.commcode like '" + areas[h] + "' or ");
                    }
                }
                sb.Remove(sb.ToString().Length - 3, 3);
                sb.Append(" ) ");
            }

            if (strStartDate != "")
            {
                sb.Append(" and b_project.startdate > '" + strStartDate + " 00:00:00" + "' ");
            }
            if (strEndDate != "")
            {
                sb.Append(" and b_project.startdate < '" + strEndDate + " 23:59:59" + "' ");
            }

            if (parts != "" && events != "")
            {
                sb.Append(" and ( b_project_detail.probclass = '0' and (");
                string[] strparts = parts.Split(',');
                for (int i = 0; i < strparts.Length; i++)
                {
                    sb.Append(" b_project_detail.bigclass =  '" + strparts[i] + "' or b_project_detail.smallclass = '" + strparts[i] + "' or ");
                }
                sb.Remove(sb.ToString().Length - 3, 3);
                sb.Append(" ) ");
                sb.Append(" or ( b_project_detail.probclass = '1' and (");
                string[] strevents = events.Split(',');
                for (int j = 0; j < strevents.Length; j++)
                {
                    sb.Append(" b_project_detail.bigclass =  '" + strevents[j] + "' or b_project_detail.smallclass = '" + strevents[j] + "' or ");
                }
                sb.Remove(sb.ToString().Length - 3, 3);
                sb.Append(" ) )");
            }
            else if (parts != "")
            {
                sb.Append(" and ( b_project_detail.probclass = '0' and (");
                string[] strparts1 = parts.Split(',');
                for (int k = 0; k < strparts1.Length; k++)
                {
                    sb.Append(" b_project_detail.bigclass =  '" + strparts1[k] + "' or b_project_detail.smallclass = '" + strparts1[k] + "' or ");
                }
                sb.Remove(sb.ToString().Length - 3, 3);
                sb.Append(" ) )");
            }
            else if (events != "")
            {
                sb.Append(" and ( b_project_detail.probclass = '1' and (");
                string[] strevents1 = events.Split(',');
                for (int l = 0; l < strevents1.Length; l++)
                {
                    sb.Append(" b_project_detail.bigclass =  '" + strevents1[l] + "' or b_project_detail.smallclass = '" + strevents1[l] + "' or ");
                }
                sb.Remove(sb.ToString().Length - 3, 3);
                sb.Append(" ) )");
            }
            sb.Append(") group by b_project_detail.area, b_project_detail.street, b_project_detail.square,probclass,bigclass, smallclass ");

            if (type == "1")
            {
                sb.Append(" union all select b_project_detail.area, b_project_detail.street, b_project_detail.square,")
                    .Append("probclass,bigclass, smallclass , count(b_project.projcode) as num ")
                    .Append("from szcg_bak.dbo.b_project , szcg_bak.dbo.b_project_detail ")
                    .Append("where exists ( ")
                    .Append("select  grid.gridcode ")
                    .Append("from area , street, community ,grid ")
                    .Append("where area.id = street.fid and street.id = community.fid and community.id = grid.commfid ")
                    .Append("and  b_project.gridcode = grid.gridcode ")
                    .Append("and b_project.projcode = b_project_detail.projcode ");
                if (area == "")
                {
                    sb.Append(" and b_project.gridcode like '" + areacode + "%' ");
                }
                else
                {
                    string[] areas = area.Split(',');
                    sb.Append(" and ( ");
                    for (int h = 0; h < areas.Length; h++)
                    {
                        if (areas[h] != null && areas[h].Length == 7)
                        {
                            sb.Append(" area.areacode  like '" + areas[h] + "' or ");
                        }
                        else if (areas[h] != null && areas[h].Length == 9)
                        {
                            sb.Append(" street.streetcode like '" + areas[h] + "' or ");
                        }
                        else if (areas[h] != null && areas[h].Length == 12)
                        {
                            sb.Append(" community.commcode like '" + areas[h] + "' or ");
                        }
                    }
                    sb.Remove(sb.ToString().Length - 3, 3);
                    sb.Append(" ) ");
                }

                if (strStartDate != "")
                {
                    sb.Append(" and b_project.startdate > '" + strStartDate + " 00:00:00" + "' ");
                }
                if (strEndDate != "")
                {
                    sb.Append(" and b_project.startdate < '" + strEndDate + " 23:59:59" + "' ");
                }

                if (parts != "" && events != "")
                {
                    sb.Append(" and ( (b_project_detail.probclass = '0' and (");
                    string[] strparts = parts.Split(',');
                    for (int i = 0; i < strparts.Length; i++)
                    {
                        sb.Append(" b_project_detail.bigclass =  '" + strparts[i] + "' or b_project_detail.smallclass = '" + strparts[i] + "' or ");
                    }
                    sb.Remove(sb.ToString().Length - 3, 3);
                    sb.Append(" ) )");
                    sb.Append(" or ( b_project_detail.probclass = '1' and (");
                    string[] strevents = events.Split(',');
                    for (int j = 0; j < strevents.Length; j++)
                    {
                        sb.Append(" b_project_detail.bigclass =  '" + strevents[j] + "' or b_project_detail.smallclass = '" + strevents[j] + "' or ");
                    }
                    sb.Remove(sb.ToString().Length - 3, 3);
                    sb.Append(" ) ))");
                }
                else if (parts != "")
                {
                    sb.Append(" and ( b_project_detail.probclass = '0' and (");
                    string[] strparts1 = parts.Split(',');
                    for (int k = 0; k < strparts1.Length; k++)
                    {
                        sb.Append(" b_project_detail.bigclass =  '" + strparts1[k] + "' or b_project_detail.smallclass = '" + strparts1[k] + "' or ");
                    }
                    sb.Remove(sb.ToString().Length - 3, 3);
                    sb.Append(" ) )");
                }
                else if (events != "")
                {
                    sb.Append(" and ( b_project_detail.probclass = '1' and (");
                    string[] strevents1 = events.Split(',');
                    for (int l = 0; l < strevents1.Length; l++)
                    {
                        sb.Append(" b_project_detail.bigclass =  '" + strevents1[l] + "' or b_project_detail.smallclass = '" + strevents1[l] + "' or ");
                    }
                    sb.Remove(sb.ToString().Length - 3, 3);
                    sb.Append(" ) )");
                }
                sb.Append(") group by b_project_detail.area, b_project_detail.street, b_project_detail.square,probclass,bigclass, smallclass ");
                sb.Append(" ) as ttt group by ttt.area,ttt.street,ttt.square,ttt.probclass,ttt.bigclass, ttt.smallclass");
            }
            System.Diagnostics.Debug.WriteLine(sb.ToString());

            ArrayList arrs = getDateByList(sb.ToString(), page);
            return arrs;
        }

        /// <summary>
        /// 案卷查询 zhanghuagen 06-1 
        /// </summary>
        /// <summary>
        /// 获取监督员zhanghuagen 07-09
        /// </summary>
        /// <param name="cinfo"></param> getcollname
        public ArrayList getCollecterInfo(string areacode)
        {
            ArrayList collresult = new ArrayList();
            string sql = "";
            if (!areacode.Equals(""))
            {
                sql = "select collcode,collname  from collecter  where substring(gridcode,0,8) = '" + areacode + "'";
            }
            else
            {

                sql = " select collcode,collname  from collecter ";
            }
            collresult = getCollresult(sql);
            return collresult;
        }

        private ArrayList getCollresult(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    cinfo = new CollecterInfo();
                    cinfo.setCollcode(System.Convert.ToString(rs["collcode"]));
                    cinfo.setCollName(System.Convert.ToString(rs["collname"]));
                    list.Add(cinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }


        /// <summary>
        /// 获取街道zhanghuagen 06-1 
        /// </summary>
        /// <param name="cinfo"></param> getAreaInfo

        public ArrayList getStreetInfo(string areacode)
        {
            ArrayList streetresult = new ArrayList();
            string sql = "";
            if (!areacode.Equals(""))
            {
                sql = "select a.streetcode,a.streetname from s_street as a,s_area as b where  a.areacode =b.areacode and b.areacode = '" + areacode + "'";
            }
            else
            {

                sql = "  select streetcode,streetname from s_street ";
            }
            streetresult = getStreetList(sql);
            return streetresult;
        }


        //区域综合评价模块获取街道
        public ArrayList getStreetInfo2(string areacode,int departid)
        {
            ArrayList streetresult = new ArrayList();
            string sql = "";
            string sql2 = "select area from p_depart where departcode='" + departid + "'";
            DataSet ds = DataAccess.ExecuteDataSet(sql2, null);
            string area_num = ds.Tables[0].Rows[0]["area"].ToString();
            if (area_num.Length == 6)
            {
                //if (!areacode.Equals(""))
                //{
                //    sql = "select a.streetcode,a.streetname from s_street as a,s_area as b where  a.areacode =b.areacode and b.areacode = '" + areacode + "'";
                //}
                //else
                //{

                sql = "  select streetcode,streetname from s_street order by id";
                //}
                streetresult = getStreetList(sql);
                return streetresult;
            }
            else
            {
                sql = "  select streetcode,streetname from s_street where streetcode='" + area_num + "' order by id";
                streetresult = getStreetList(sql);
                return streetresult;
            }

        }

        
        private ArrayList getStreetList(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    pinfo = new ProjInfo();
                    pinfo.setStreetcode(System.Convert.ToString(rs["streetcode"]));
                    pinfo.setStreet(System.Convert.ToString(rs["streetname"]));
                    list.Add(pinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }



        /// <summary>
        /// 根据街道获取社区06-10
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList getSquareInfoid(string streetid, string areacode)
        {
            ArrayList squareresult = new ArrayList();
            string sql = "";
            if (!areacode.Equals(""))
            {
                if (!streetid.Equals(""))
                {
                    //string sql1 = "select streetcode from s_street where streetname='" + streetid+"'";

                    //sql = "select commcode,commname from s_community where streetcode='" + sql1 + "'";
                    sql = "select a.commcode,a.commname from s_community as a,s_street as b where a.streetcode = b.streetcode and b.streetname='" + streetid + "'";
                }
                else
                {

                    sql = "select a.commcode,a.commname from  s_community as a,s_street as b,s_area as c  where  a.streetcode =b.streetcode and b.areacode =c.areacode  and c.areacode = '" + areacode + "' ";
                }
            }
            else
            {
                sql = "select streetcode,commname from s_community ";

            }

            squareresult = getSquareList(sql);
            return squareresult;
        }

        private ArrayList getSquareList(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    pinfo = new ProjInfo();
                    //pinfo.setFid(System.Convert.ToString(rs["id"]));
                    //pinfo.setSquare(System.Convert.ToString(rs["commname"]));
                    pinfo.setFid(System.Convert.ToString(rs["commcode"]));
                    pinfo.setSquare(System.Convert.ToString(rs["commname"]));
                    list.Add(pinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }


        /// <summary>
        /// 社区获取网格
        /// </summary>
        /// <returns></returns>
        public ArrayList getGridInfoid(string commid, string areacode)
        {
            ArrayList gridresult = new ArrayList();
            string sql = "";
            if (!areacode.Equals(""))
            {
                if (!commid.Equals(""))
                {
                    sql = "select gridcode from grid  where  commfid='" + commid + "'";
                }
                else
                {
                    sql = "select a.gridcode  from  grid as a,community as b,street as c,area as d  where a.commfid =b.id and b.fid =c.id and c.fid =d.id and d.areacode='" + areacode + "' ";
                }
            }
            else
            {
                sql = "select gridcode from grid ";

            }

            gridresult = getGridList(sql);
            return gridresult;
        }
        /// <summary>
        ///	 根据areacode获取专业部门列表
        /// </summary>
        /// <returns></returns>
        public SqlDataReader getDepartList(string areacode)
        {
                string sql = string.Format(@"select A.departname,A.departcode from p_depart A join p_role B on B.departcode=A.departcode
                                       where B.stepid=4 and B.areacode like '{0}%'", areacode);
                return DataAccess.ExecuteReader(sql, null);
  
        }


        //责任单位综合评价模块取部门
        public SqlDataReader getDepartList2(string areacode, int departid)
        {
            string sql2 = "select area from p_depart where departcode='" + departid + "'";
            DataSet ds = DataAccess.ExecuteDataSet(sql2, null);
            string area_num = ds.Tables[0].Rows[0]["area"].ToString();
            if (area_num.Length == 6)
            {
//                string sql = string.Format(@"select A.departname,A.departcode from p_depart A join p_role B on B.departcode=A.departcode
//                                       where B.stepid=4 and B.areacode like '{0}%'", areacode);
                string sql = string.Format(@"select departcode,departname from p_depart where IsDuty=1 and area like '{0}%'", areacode);
                return DataAccess.ExecuteReader(sql, null);
            }
            else
            {
//                string sql = string.Format(@"select A.departname,A.departcode from p_depart A join p_role B on B.departcode=A.departcode
//                                       where  A.area = '{0}'", area_num);
                string sql = string.Format(@"select departcode,departname from p_depart where IsDuty=1 and area = '{0}%'", areacode);
                return DataAccess.ExecuteReader(sql, null);
            }
        }


        public ArrayList getCollecter(string streetid, string commid, string areacode)
        {
            ArrayList coll = new ArrayList();
            string sql = "select collcode,collname from collecter ";
            if (!areacode.Equals(""))
            {

                if (!commid.Equals(""))
                {
                    string sql2 = "select commcode from community where id='" + commid + "'";
                    string commcode = "";
                    SqlDataReader rs = DataAccess.ExecuteReader(sql2, null);
                    if (rs.Read())
                    {
                        commcode = Convert.ToString(rs["commcode"]);
                    }
                    rs.Close();
                    sql += " where gridcode like '" + commcode + "%'";
                }
                else if (!streetid.Equals(""))
                {
                    string sql1 = "select streetcode from street where id='" + streetid + "'";
                    string streetcode = "";
                    SqlDataReader rs = DataAccess.ExecuteReader(sql1, null);
                    if (rs.Read())
                    {
                        streetcode = Convert.ToString(rs["streetcode"]);
                    }
                    rs.Close();
                    sql += " where gridcode like '" + streetcode + "%'";
                }
                else
                {
                    sql += " where gridcode like '" + areacode + "%'";
                }
            }

            coll = getCollList(sql);
            return coll;
        }

        private ArrayList getCollList(string sql)
        {
            list = new ArrayList();
            try
            {
                rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                }
                while (rs.Read())
                {
                    CollecterInfo coll = new CollecterInfo();
                    coll.setCollcode(Convert.ToString(rs["collcode"]));
                    coll.setCollName(Convert.ToString(rs["collname"]));
                    list.Add(coll);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                rs.Close();
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }

        private ArrayList getGridList(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    pinfo = new ProjInfo();
                    pinfo.setGridcode(System.Convert.ToString(rs["gridcode"]));
                    //					string sql1 = "select collcode,collname from collecter where gridcode = '"+Convert.ToString(rs["gridcode"])+"'";
                    //					SqlDataReader drs = DataAccess.ExecuteReader(sql1,null);
                    //					while(drs.Read())
                    //					{
                    //						pinfo.setNum(Convert.ToString(drs["collcode"]));
                    //						pinfo.setName(Convert.ToString(drs["collname"]));
                    //					}
                    //					drs.Close();
                    list.Add(pinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 获取部件大类 zhanghuagen 05-31
        /// </summary>

        public ArrayList getBigpartInfoid(string probclass)
        {
            ArrayList bigpartresult = new ArrayList();
            string sql = "";
            if (probclass.Equals("0"))
            {
                sql = "select id,name from bigclass_part ";
            }
            else if (probclass.Equals("1"))
            {
                sql = "select id,name from bigclass_event ";
            }
            else if (probclass.Equals(""))
            {
                sql = "select id,name from bigclass_part ";
            }
            //			else if(probclass.Equals(""))
            //			{
            //				sql="select id, name from bigclass_part union select id, name from bigclass_event";
            //			}

            bigpartresult = getBigpartList(sql);
            return bigpartresult;
        }

        private ArrayList getBigpartList(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    pinfo = new ProjInfo();
                    pinfo.setBigclass(System.Convert.ToString(rs["id"]));//大类编号
                    pinfo.setBigclassname(System.Convert.ToString(rs["name"]));
                    list.Add(pinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }


        /// <summary>
        /// 由大类获取部件小类0604
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList getSmallpartInfoid(string bigclassid, string probclass)
        {
            ArrayList smallpartresultid = new ArrayList();
            //string sql="select id,name from smallclass_part  where fid='"+bigclassid+"'";
            string sql = "";
            if (probclass.Equals("0"))
            {
                sql = "select id,name from smallclass_part  where fid='" + bigclassid + "'";
            }
            else if (probclass.Equals("1"))
            {
                sql = "select id,name from smallclass_event  where fid='" + bigclassid + "' ";
            }

            else if (probclass.Equals(""))
            {
                sql = "select id, name from smallclass_part union select id, name from smallclass_event";
            }

            smallpartresultid = getSmallpartList(sql);
            return smallpartresultid;
        }

        private ArrayList getSmallpartList(String sql)
        {

            list = new ArrayList();
            try
            {

                SqlDataReader rs = DataAccess.ExecuteReader(sql, null);
                if (rs == null)
                {
                    rs.Close();
                    return null;
                }
                while (rs.Read())
                {
                    pinfo = new ProjInfo();
                    pinfo.setSmallclass(System.Convert.ToString(rs["id"]));//小类编号
                    pinfo.setSmallclassname(System.Convert.ToString(rs["name"]));
                    list.Add(pinfo);
                }
                rs.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 获取区名称06-26 zhanghuagen0602
        /// </summary>
        public static string getAreaname(int areacode)
        {
            string areaname = "";
            string sql = "select areaname from area where areacode='" + areacode + "'";
            using (SqlDataReader dr = szcg.com.teamax.util.DataAccess.ExecuteReader(sql, null))
            {
                while (dr != null && dr.Read())
                {
                    areaname = System.Convert.ToString(dr["areaname"]);
                }
            }
            return areaname;
        }


        /// <summary>
        /// 查询案卷06-2 zhanghuagen0602
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>

        //取得trace表的阶段名称 author:zhg
        public string getTraceStepName(string stepid, string actionname)
        {

            if (stepid.Equals("1") && actionname.Equals("批转"))
            {
                return "接线员批转";
            }
            else if (stepid.Equals("1") && actionname.Equals("核查"))
            {
                return "接线员核查处理";
            }
            else if (stepid.Equals("1") && actionname.Equals("核查反馈"))
            {
                return "发值班长结案";
            }
            else if (stepid.Equals("1") && actionname.Equals("重派遣"))
            {
                return "发指挥中心重派遣";
            }
            else if (stepid.Equals("2") && actionname.Equals("立案"))
            {
                return "值班长立案处理";
            }
            else if (stepid.Equals("2") && actionname.Equals("值班长回退"))
            {
                return "值班长回退";
            }
            else if (stepid.Equals("2") && actionname.Equals("消案"))
            {
                return "值班长消案处理";
            }
            else if (stepid.Equals("2") && actionname.Equals("结案"))
            {
                return "值班长结案处理";
            }
            else if (stepid.Equals("3") && actionname.Equals("派遣"))
            {
                return "指挥中心任务派遣";
            }
            else if (stepid.Equals("3") && actionname.Equals("指挥中心回退"))
            {
                return "指挥中心回退";
            }
            else if (stepid.Equals("3") && actionname.Equals("操作员核查"))
            {
                return "指挥中心核查";
            }
            else if (stepid.Equals("4") && actionname.Equals("结果反馈"))
            {
                return "专业部门结果反馈";
            }
            else if (stepid.Equals("4") && actionname.Equals("回退"))
            {
                return "专业部门回退";
            }
            else if (actionname.Equals(""))
            {
                return "案卷批转";
            }

            return "";

        }

        /// <summary>
        /// 移交件06-2 zhanghuagen0603
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        public DataSet SelectHandover(string stepId, int usercode, string areacode, int PageSize, int CurrentPage, out int totalpages, out int totalrows)
        {
            ArrayList list = new ArrayList();
            StringBuilder sb = new StringBuilder();

            string sql = "SELECT  a.startdate,a.stepid,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where exists ( " +
                    " select distinct  projcode from projtrace where usercode = '" + usercode + "' and a.projcode = projtrace.projcode and (a.isdel = 0 or isdel is null))";

            int rowCount = 0, pageCount = 0;
            //取得父表当前页的projcode,作为子表的条件
            DataAccess.cutPage(sql, PageSize, out rowCount, out pageCount);
            totalpages = pageCount;
            totalrows = rowCount;

            int currentRows = (CurrentPage >= pageCount) ? (rowCount - (pageCount - 1) * PageSize) : PageSize;//当前页的行数
            int currentTotalRows = CurrentPage * PageSize;//从开始到本页的行数

            //sql = "select * from (select top "+currentRows+" * from (select top "+currentTotalRows+" * from "+
            //	" ("+sql+") as t0 order by projcode desc) as t1 order by projcode ) as t2 order by projcode desc";


            //string sqls= sql + ";" + sql2;
            //start
            rs = DataAccess.getPageData(sql, PageSize, CurrentPage, "projcode", "desc", totalrows, totalpages);
            DataTable table = new DataTable("Customers");
            DataRow row;
            table.Columns.Add("projcode");
            table.Columns.Add("probclass");
            table.Columns.Add("probsource");
            table.Columns.Add("probdesc");
            table.Columns.Add("probaddress");
            table.Columns.Add("area");
            table.Columns.Add("street");
            table.Columns.Add("square");
            while (rs.Read())
            {
                row = table.NewRow();
                sb.Append(Convert.ToString(rs["projcode"]) + ",");
                row[0] = Convert.ToString(rs["projname"]);
                string prob = Convert.ToString(rs["probclass"]);
                if (prob == "0")
                {
                    row[1] = "部件";
                }
                else
                {
                    row[1] = "事件";
                }
                string probs = Convert.ToString(rs["probsource"]);
                if (probs == "0")
                {
                    row[2] = "公众举报";
                }
                else if (probs == "1")
                {
                    row[2] = "监督员上报";
                }
                else if (probs == "2")
                {
                    row[2] = "执法人员举报";
                }
                else if (probs == "3")
                {
                    row[2] = "网站举报";
                }
                else if (probs == "4")
                {
                    row[2] = "传真举报";
                }
                else if (probs == "5")
                {
                    row[2] = "短信举报";
                }
                else if (probs == "6")
                {
                    row[2] = "信访举报";
                }
                else if (probs == "7")
                {
                    row[2] = "媒体举报";
                }
                else if (probs == "8")
                {
                    row[2] = "邮件举报";
                }
                else if (probs == "9")
                {
                    row[2] = "其他举报";
                }
                //row[1]=Convert.ToString(rs["probclass"]);
                //row[2]=Convert.ToString(rs["probsource"]);
                row[3] = Convert.ToString(rs["probdesc"]);
                row[4] = Convert.ToString(rs["address"]);
                row[5] = Convert.ToString(rs["area"]);
                row[6] = Convert.ToString(rs["street"]);
                row[7] = Convert.ToString(rs["square"]);
                table.Rows.Add(row);
            }
            rs.Close();
            string sql2 = "SELECT c.username as userid,a.projcode AS projcode1,a.stepid,a.actionname,a.cu_date,a._opinion,b.projname   FROM  projtrace  as a,b_project_detail as b,loginuser as c " +
                "  where a.projcode=b.projcode and a.usercode=c.usercode ";
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
                sql2 = sql2 + " and b.projcode in(" + sb.ToString() + ")";
            }
            else
            {
                return null;
            }
            rs = DataAccess.getDataReader(sql2);
            DataTable table2 = new DataTable("Orders");
            table2.Columns.Add("案卷号");
            table2.Columns.Add("经办人");
            table2.Columns.Add("阶段名称");
            table2.Columns.Add("意见");
            table2.Columns.Add("受理时间");

            while (rs.Read())
            {
                row = table2.NewRow();

                string _stepid = Convert.ToString(rs["stepid"]);
                string _actionname = Convert.ToString(rs["actionname"]);

                row[0] = Convert.ToString(rs["projname"]);
                row[1] = Convert.ToString(rs["userid"]);
                row[2] = getTraceStepName(_stepid, _actionname);
                row[3] = Convert.ToString(rs["_opinion"]);
                row[4] = Convert.ToString(rs["cu_date"]);

                table2.Rows.Add(row);
            }
            rs.Close();

            //end

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table);
            dataSet.Tables.Add(table2);
            //dataSet=dataAccess.getDataSet(sqls);
            //dataSet.Tables[0].TableName = "Customers";
            //dataSet.Tables[1].TableName = "Orders";

            string relName = "Customer2Orders";
            if (dataSet == null)
            {
                totalpages = 0;
                totalrows = 0;
                return null;
            }
            DataRelation rel = new DataRelation(relName,
                dataSet.Tables["Customers"].Columns["projcode"],
                dataSet.Tables["Orders"].Columns["案卷号"]);

            dataSet.Relations.Add(rel);


            return dataSet;

        }
        /// <summary>
        /// 督办件06-2 zhanghuagen0604
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        public DataSet Superintend(string areacode, int PageSize, int CurrentPage, out int totalpages, out int totalrows)
        {
            ArrayList list = new ArrayList();
            string sql = "";
            if (areacode == "4403")
            {
                sql = "SELECT distinct a.startdate,a.stepid,a.ispress,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where ispress='1' and isgreat = '1'";
            }
            else
            {
                sql = "SELECT distinct a.startdate,a.stepid,a.ispress,b.* FROM b_project a INNER JOIN b_project_detail b ON a.PROJCODE = b.PROJCODE  where ispress='1' and a.isdel='0' and (isgreat = '0' or isgreat is null) and substring(a.gridcode,0,8)='" + areacode + "'";
            }

            string sql2 = "SELECT c.username as userid,a.projcode AS projcode1,a.content,a.title,a.cudate,b.projname   FROM  inspect  as a,b_project_detail as b,loginuser as c " +
                "  where a.projcode=b.projcode and a.usercode=c.usercode";

            int rowCount = 0, pageCount = 0;
            //取得父表当前页的projcode,作为子表的条件
            DataAccess.cutPage(sql, PageSize, out rowCount, out pageCount);
            totalpages = pageCount;
            totalrows = rowCount;
            SqlDataReader rs = DataAccess.getPageData(sql, PageSize, CurrentPage, "projcode", "desc", rowCount, pageCount);
            StringBuilder sb = new StringBuilder();
            while (rs.Read())
            {
                sb.Append("'" + Convert.ToString(rs["projcode"]) + "',");
            }
            rs.Close();
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
                sql2 = sql2 + " and b.projcode in(" + sb.ToString() + ")";
            }
            else
            {
                return null;
            }


            int currentRows = (CurrentPage >= pageCount) ? (rowCount - (pageCount - 1) * PageSize) : PageSize;//当前页的行数
            int currentTotalRows = CurrentPage * PageSize;//从开始到本页的行数
            sql = "select * from (select top " + currentRows + " * from (select top " + currentTotalRows + " * from " +
                " (" + sql + ") as t0 order by projcode desc) as t1 order by projcode ) as t2 order by projcode desc";
            //string sqls=sql+";"+sql2;
            //start
            rs = DataAccess.getDataReader(sql);
            DataTable table = new DataTable("Customers");
            DataRow row;
            table.Columns.Add("projcode");
            table.Columns.Add("probclass");
            table.Columns.Add("probsource");
            table.Columns.Add("probdesc");
            table.Columns.Add("probaddress");
            table.Columns.Add("street");
            table.Columns.Add("square");
            while (rs.Read())
            {
                row = table.NewRow();
                row[0] = Convert.ToString(rs["projname"]);
                string prob = Convert.ToString(rs["probclass"]);
                if (prob == "0")
                {
                    row[1] = "部件";
                }
                else
                {
                    row[1] = "事件";
                }
                string probs = Convert.ToString(rs["probsource"]);
                if (probs == "0")
                {
                    row[2] = "公众举报";
                }
                else if (probs == "1")
                {
                    row[2] = "监督员上报";
                }
                else if (probs == "2")
                {
                    row[2] = "执法人员举报";
                }
                else if (probs == "3")
                {
                    row[2] = "网站举报";
                }
                else if (probs == "4")
                {
                    row[2] = "传真举报";
                }
                else if (probs == "5")
                {
                    row[2] = "短信举报";
                }
                else if (probs == "6")
                {
                    row[2] = "信访举报";
                }
                else if (probs == "7")
                {
                    row[2] = "媒体举报";
                }
                else if (probs == "8")
                {
                    row[2] = "邮件举报";
                }
                else if (probs == "9")
                {
                    row[2] = "其他举报";
                }
                //row[1]=Convert.ToString(rs["probclass"]);
                //row[2]=Convert.ToString(rs["probsource"]);
                row[3] = Convert.ToString(rs["probdesc"]);
                row[4] = Convert.ToString(rs["address"]);
                row[5] = Convert.ToString(rs["street"]);
                row[6] = Convert.ToString(rs["square"]);
                table.Rows.Add(row);
            }
            rs.Close();

            rs = DataAccess.getDataReader(sql2);
            DataTable table2 = new DataTable("Orders");
            table2.Columns.Add("案卷号");
            table2.Columns.Add("督办人");
            table2.Columns.Add("督办主题");
            table2.Columns.Add("督办内容");
            table2.Columns.Add("督办时间");

            while (rs.Read())
            {
                row = table2.NewRow();
                //string _stepid=Convert.ToString(rs["stepid"]);
                //string _actionname=Convert.ToString(rs["actionname"]);

                row[0] = Convert.ToString(rs["projname"]);
                row[1] = Convert.ToString(rs["userid"]);
                row[2] = Convert.ToString(rs["title"]);
                row[3] = Convert.ToString(rs["content"]);
                row[4] = Convert.ToString(rs["cudate"]);

                table2.Rows.Add(row);
            }
            rs.Close();

            //end

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table);
            dataSet.Tables.Add(table2);
            //dataSet=dataAccess.getDataSet(sqls);
            //dataSet.Tables[0].TableName = "Customers";
            //dataSet.Tables[1].TableName = "Orders";

            string relName = "Customer2Orders";
            DataRelation rel = new DataRelation(
                relName,
                dataSet.Tables["Customers"].Columns["projcode"],
                dataSet.Tables["Orders"].Columns["案卷号"]
                );
            dataSet.Relations.Add(rel);
            return dataSet;


        }



    }
}
