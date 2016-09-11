using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgBL.web.szbase.entity;
using bacgDL.szbase.grid;
namespace bacgBL.web.szbase.grid
{
    public class GridManage
    {
        #region GetAreaList：取得所有区记录
        /// <summary>
        /// 取得所有区记录
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public ArrayList GetAreaList(string areacode, ref string strErr)
        {
            string area = "";
            if (areacode.Length >= 6)
            {
                area = areacode.Substring(0, 6);
            }
            else
            {
                area = areacode;
            }
            ArrayList areas = new ArrayList();
            /*     string strSQL = string.Format(@"select areacode,areaname 
                                                 from s_area
                                                 where areacode = '{0}' or len(areacode)=4",area);桐乡二期注释*/
            string strSQL = string.Format(@"select areacode,areaname 
                                                 from s_area
                                                 where areacode like '{0}%' or len(areacode)=4", area);//桐乡二期修改
            if (areacode.Length == 4)
                strSQL = "select areacode,areaname from s_area";
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] areaInfo = new string[2];
                        areaInfo[0] = dr["areacode"].ToString();
                        areaInfo[1] = dr["areaname"].ToString();
                        areas.Add(areaInfo);
                    }

                    dr.Close();
                    return areas;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetStreetByAreaId：根据区域编码获取街道列表
        /// <summary>
        /// 根据区域编码获取街道列表
        /// </summary>
        /// <param name="currentAreaCode">当前所在区域</param>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public ArrayList GetStreetByAreaId(string areacode,string currentAreaCode,ref string strErr)
        {
            string strSQL = "";
            ArrayList streets = new ArrayList();
            if (currentAreaCode.Length > 8)
            {
                strSQL = string.Format(@"select id,streetname 
                                            from s_street
                                            where streetcode = '{0}' order by id", currentAreaCode.Substring(0, 9));
            }
            else
            {
                strSQL = string.Format(@"select id,streetname 
                                            from s_street
                                            where areacode = '{0}' order by id", areacode);
            }

            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] street = new string[2];
                        street[0] = dr["id"].ToString();
                        street[1] = dr["streetname"].ToString();
                        streets.Add(street);
                    }

                    dr.Close();
                    return streets;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetCommunityByStreetId：根据街道编码获取社区列表
        /// <summary>
        /// 根据街道编码获取社区列表
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <returns></returns>
        public ArrayList GetCommunityByStreetId(string streetid,string areacode,ref string strErr)
        {
            string strSQL = "";
            ArrayList communitys = new ArrayList();
            if (areacode.Length > 10)
            {
                strSQL = string.Format(@"select a.id,a.commname 
                                            from s_community
                                            where commcode = '{0}'",areacode);
            }
            else
            {
                strSQL = string.Format(@"select a.id,a.commname 
                                            from s_community a left join s_street b
                                            on a.streetcode=b.streetcode
                                            where b.id = '{0}'", streetid);
            }
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] community = new string[2];
                        community[0] = dr["id"].ToString();
                        community[1] = dr["commname"].ToString();
                        communitys.Add(community);
                    }

                    dr.Close();
                    return communitys;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetGridByCommunityId：根据社区编码获取网格列表
        /// <summary>
        /// 根据社区编码获取网格列表
        /// </summary>
        /// <param name="commcode">社区id</param>
        /// <returns></returns>
        public ArrayList GetGridByCommunityId(string commid, ref string strErr)
        {
            ArrayList grids = new ArrayList();
            string strSQL = string.Format(@"select a.id,a.gridcode 
                                            from s_grid a left join s_community b
                                            on a.commcode=b.commcode
                                            where b.id='{0}'",commid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        string[] grid = new string[2];
                        grid[0] = dr["id"].ToString();
                        grid[1] = dr["gridcode"].ToString();
                        grids.Add(grid);
                    }
                    dr.Close();
                    return grids;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetAreaInfoByAreacode：根据区域编码获取区域信息
        /// <summary>
        /// 根据区域编码获取区域信息
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public DataSet GetAreaInfoByAreacode(int areacode, ref string strErr)
        {

            string strSQL = string.Format(@"select * from s_area
                                            where areacode = '{0}'", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetStreetInfoByStreetCode：根据街道编码获取街道信息
        /// <summary>
        /// 根据街道编码获取街道信息
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <returns></returns>
        public DataSet GetStreetInfoByStreetCode(int streetid, ref string strErr)
        {

            string strSQL = string.Format(@"select a.*,b.areaname from s_street a left join s_area b
                                            on a.areacode=b.areacode
                                            where a.id = '{0}'", streetid);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetCommunityInfoByCommCode：根据社区编码获取社区信息
        /// <summary>
        /// 根据社区编码获取社区信息
        /// </summary>
        /// <param name="commId">社区id</param>
        /// <returns></returns>
        public DataSet GetCommunityInfoByCommCode(int commId, ref string strErr)
        {

            string strSQL = string.Format(@"select a.*,b.streetname from s_community a left join s_street b
                                            on a.streetcode=b.streetcode
                                            where a.id = '{0}'", commId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecDatasetSql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region GetGridInfoByGridCode：根据网格ID获取网格信息
        /// <summary>
        /// 根据网格ID获取网格信息
        /// </summary>
        /// <param name="gridId">网格Id</param>
        /// <param name="strErr">返回错误信息</param>
        /// <returns></returns>
        public DataSet GetGridInfoByGridCode(string gridId, ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.grid.GridManage dl = new bacgDL.szbase.grid.GridManage())
                {
                    SqlParameter[] arrSP = new SqlParameter[] {
                        new SqlParameter("@GridId", gridId)
                    };

                    return dl.ExecuteDataset("pr_s_GridInfo", CommandType.StoredProcedure, arrSP);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region DeleteFromArea：删除区域信息
        /// <summary>
        /// 删除区域信息
        /// </summary>
        /// <param name="areaCode">区域编码</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：区域存在街道，删除失败
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteFromArea(int areaCode, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",areaCode), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromArea", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
            }
        }
        #endregion

        #region DeleteFromStreet：删除街道信息
        /// <summary>
        /// 删除街道信息
        /// </summary>
        /// <param name="streetId">街道id</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：区域存在街道，删除失败
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteFromStreet(int streetId, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",streetId), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromStreet", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
            }
        }
        #endregion

        #region DeleteFromCommunity：删除社区信息
        /// <summary>
        /// 删除社区信息
        /// </summary>
        /// <param name="commId">社区id</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：区域存在街道，删除失败
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteFromCommunity(int commId, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",commId), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromCommunity", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
            }
        }
        #endregion

        #region DeleteFromGrid：删除网格信息
        /// <summary>
        /// 删除网格信息
        /// </summary>
        /// <param name="gridId">网格id</param>
        /// <param name="flag">状态信息</param>0：删除发生异常1：标识删除成功2：区域存在街道，删除失败
        /// <param name="strErr">输出的错误信息</param> 
        /// <returns></returns>
        public void DeleteFromGrid(int gridId, out int flag, ref string strErr)
        {
            SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("@id",gridId), 
                                new SqlParameter("@result",SqlDbType.Int) };
            arrSP[1].Direction = ParameterDirection.Output;
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    dl.ExecProc("pr_p_DeleteFromGrid", ref arrSP);
                    flag = Convert.ToInt32(arrSP[1].Value);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                flag = 0;
            }
        }
        #endregion

        #region CheckAreaCode：判断区域是否已经存在
        /// <summary>
        /// 判断区域是否已经存在
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public bool CheckAreaCode(string areacode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from s_area
                                            where areacode ='{0}'", areacode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    if (Convert.ToInt32(dl.ExecuteScalar(strSQL)) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }
        #endregion

        #region InsertToArea：插入区域信息
        /// <summary>
        /// 插入区域信息
        /// </summary>
        /// <param name="id">区域编码</param>
        /// <param name="name">区域名字</param>
        /// <param name="area">所属区域</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int InsertToArea(int id,string name,string area,
                                    string square,string population,
                                    string memo, ref string strErr)
        {
            string strSQL = string.Format(@"insert into s_area(areacode,areaname,population,area,memo)
                                            values('{0}','{1}','{2}','{3}','{4}')", id, name,population,square,memo);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckStreetCode：判断街道是否已经存在
        /// <summary>
        /// 判断街道是否已经存在
        /// </summary>
        /// <param name="streetCode">街道编码</param>
        /// <returns></returns>
        public int CheckStreetCode(string streetCode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from s_street
                                            where streetcode ='{0}'", streetCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecScalarSql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertToStreet：插入街道信息
        /// <summary>
        /// 插入街道信息
        /// </summary>
        /// <param name="streetcode">街道编码</param>
        /// <param name="areacode">所属区域编码</param>
        /// <param name="streetname">街道名字</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int InsertToStreet(string streetcode, string areacode,string streetname,
                                    string square, string population,string memo,
                                    ref string strErr)
        {
            string strSQL = string.Format(@"insert into s_street(streetcode,areacode,streetname,population,area,memo)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}')", streetcode, areacode, streetname, population, square, memo);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateArea：更新区域信息
        /// <summary>
        /// 更新区域信息
        /// </summary>
        /// <param name="oldId">修改前编码</param>
        /// <param name="id">区域编码</param>
        /// <param name="name">区域名字</param>
        /// <param name="area">所属区域</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateArea(int oldId,int id, string name,
                                string area,string square, string population,
                                string memo, ref string strErr)
        {
            string strSQL = string.Format(@"update s_area set areacode = '{0}',areaname = '{1}',population = '{2}',
					                                            area = '{3}',memo = '{4}'
                                            where areacode ='{5}'", id, name, population, square, memo,oldId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateStreet：更新街道信息
        /// <summary>
        /// 更新街道信息
        /// </summary>
        /// <param name="id">街道id</param>
        /// <param name="streetCode">街道编号</param>
        /// <param name="streetName">街道名字</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateStreet(string id, string streetCode,string streetName,
                                    string square, string population,string memo, 
                                    ref string strErr)
        {
            string strSQL = string.Format(@"update s_street set streetcode='{0}',streetname='{1}',population='{2}',
						                           area = '{3}',memo='{4}'
                                            where id='{5}'", streetCode,streetName, population, square, memo,id);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckCommCode：判断社区是否已经存在
        /// <summary>
        /// 判断社区是否已经存在
        /// </summary>
        /// <param name="commCode">社区编码</param>
        /// <returns></returns>
        public int CheckCommCode(string commCode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from s_community
                                            where commcode = '{0}'",commCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecScalarSql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertToComm：插入社区信息
        /// <summary>
        /// 插入社区信息
        /// </summary>
        /// <param name="streetId">街道id</param>
        /// <param name="commCode">社区编码</param>
        /// <param name="commName">社区名字</param>
        /// <param name="area">所属区域</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int InsertToComm(string streetId,string commCode,string commName,
                                    string square, string population, string memo,
                                    ref string strErr)
        {
            string streetSql = string.Format(@"select streetcode 
                                            from s_street
                                            where id='{0}'", streetId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {

                    string streetCode = Convert.ToString(dl.ExecScalarSql(streetSql));
                    if (streetCode.Length > 0)
                    {
                        string strSQL = string.Format(@"insert into s_community(commcode,streetcode,commname,population,area,memo)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}')", commCode, streetCode, commName, population, square, memo);
                        return dl.ExecNonQuerySql(strSQL);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateComm：更新社区信息
        /// <summary>
        /// 更新社区信息
        /// </summary>
        /// <param name="commId">社区id</param>
        /// <param name="commName">社区名字</param>
        /// <param name="commCode">社区编码</param>
        /// <param name="population">人口</param>
        /// <param name="square">面积</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateComm(string commId,string commName,string commCode,
                                string population, string square, string memo,
                                ref string strErr)
        {
            string strSQL = string.Format(@"update s_community set commcode='{0}',commname='{1}',population='{2}',
						                            area='{3}',memo='{4}'
                                            where id='{5}'", commCode, commName, population, square, memo, commId);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region CheckGridCode：判断网格是否已经存在
        /// <summary>
        /// 判断网格是否已经存在
        /// </summary>
        /// <param name="gridCode">网格编码</param>
        /// <returns></returns>
        public int CheckGridCode(string gridCode, ref string strErr)
        {
            string strSQL = string.Format(@"select count(*) from s_grid
                                            where gridcode = '{0}'",gridCode);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return Convert.ToInt32(dl.ExecNonQuerySql(strSQL));
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region InsertToGrid：插入网格信息
        /// <summary>
        /// 插入网格信息
        /// </summary>
        /// <param name="id">社区id</param>
        /// <param name="name">社区名字</param>
        /// <param name="area">所属区域</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int InsertToGrid(string id,string gridCode,string memo,ref string strErr)
        {
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    string commSQL = string.Format(@"select commcode 
                                                    from s_community
                                                    where id='{0}'",id);
                    string commCode = Convert.ToString(dl.ExecScalarSql(commSQL));
                    if (commCode.Length > 0)
                    {
                        string strSQL = string.Format(@"insert into s_grid(gridcode,commcode,memo)
                                            values('{0}','{1}','{2}')", gridCode, commCode, memo);
                        return dl.ExecNonQuerySql(strSQL);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region UpateGrid：更新网格信息
        /// <summary>
        /// 更新网格信息
        /// </summary>
        /// <param name="oldId">前编码</param>
        /// <param name="id">街道编码</param>
        /// <param name="name">街道名字</param>
        /// <param name="area">所属区域</param>
        /// <param name="square">面积</param>
        /// <param name="population">人口</param>
        /// <param name="memo">备注</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public int UpateGrid(string id, string gridCode, string memo, string IsLockLogin, ref string strErr)
        {
            string strSQL = string.Format(@"update s_grid 
                                            set gridcode = '{0}',memo='{1}',IsLockLogin={3}
                                            where id = '{2}'", gridCode, memo, id, IsLockLogin);
            try
            {
                using (bacgDL.szbase.organize.DepartManage dl = new bacgDL.szbase.organize.DepartManage())
                {
                    return dl.ExecNonQuerySql(strSQL);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }
        #endregion

        #region GetGridTreeList：取得网格树信息（区，街道，社区，网格）
        /// <summary>
        /// 取得网格树信息（区，街道，社区，网格）
        /// </summary>
        /// <param name="areacode">区域编码</param>
        /// <returns></returns>
        public ArrayList GetGridTreeList(string areacode, ref string strErr)
        {
            ArrayList treeStructList = new ArrayList();
            try
            {
                using (bacgDL.szbase.grid.GridManage dl = new bacgDL.szbase.grid.GridManage())
                {
                    DataSet ds = dl.GetGridTreeList(areacode);
                    if (ds.Tables.Count > 2)
                    {
                        DataTable dtStreet = ds.Tables[0];
                        foreach (DataRow dr in dtStreet.Rows)
                        {
                            TreeSuruct ts;
                            ts.pcode = dr["parentcode"].ToString();
                            ts.code = dr["nodecode"].ToString();
                            ts.text = dr["nodename"].ToString();
                            ts.tag = dr["memo"].ToString();
                            treeStructList.Add(ts);
                        }

                        DataTable dtComm = ds.Tables[1];
                        foreach (DataRow dr in dtComm.Rows)
                        {
                            TreeSuruct ts;
                            ts.pcode = dr["parentcode"].ToString();
                            ts.code = dr["nodecode"].ToString();
                            ts.text = dr["nodename"].ToString();
                            ts.tag = dr["memo"].ToString();
                            treeStructList.Add(ts);
                        }

                        DataTable dtGrid = ds.Tables[2];
                        foreach (DataRow dr in dtGrid.Rows)
                        {
                            TreeSuruct ts;
                            ts.pcode = dr["parentcode"].ToString();
                            ts.code = dr["nodecode"].ToString();
                            ts.text = dr["nodename"].ToString();
                            ts.tag = dr["memo"].ToString();
                            treeStructList.Add(ts);
                        }
                        return treeStructList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion

        #region 获取网格数据
        public DataTable GetStreetTable(string parentCode, string streetCode)
        {
            GridManageDAL dal = new GridManageDAL();
            return dal.GetStreetTable(parentCode, streetCode);
        }
        public DataTable GetCommunityTable(string parentCode, string communityCode)
        {
            GridManageDAL dal = new GridManageDAL();
            return dal.GetCommunityTable(parentCode, communityCode);
        }
        public DataTable GetGridTable(string parentCode, string gridCode)
        {
            GridManageDAL dal = new GridManageDAL();
            return dal.GetGridTable(parentCode, gridCode);
        }
        #endregion
    }
}
