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
using bacgDL.environment.entitys;
using Teamax.Common;

namespace bacgDL.environment.personnels
{
    public class PersonnelDAO : CommonDatabase
    {

        /// <summary>
        /// 获取部门人员的列表(环卫绿化)
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public Teamax.Common.PageManage GetDepartPersonList(string departcode, string Type, int pageIndex, int pageSize)
        {
            if (pageIndex == 0) pageIndex = 1;
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@departcode", Convert.ToInt32(departcode)),
                new SqlParameter("@Type", Type),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@RowCount",SqlDbType.Int),
                new SqlParameter("@PageCount",SqlDbType.Int),
            };
            spInputs[4].Direction = ParameterDirection.Output;
            spInputs[5].Direction = ParameterDirection.Output;

            DataSet ds = ExecuteDataset("pr_p_GetDepartPersonList", CommandType.StoredProcedure, spInputs);
            int _RowCount = spInputs[4].Value.ToString() == "" ? 0 : int.Parse(spInputs[4].Value.ToString());
            int _PageCount = spInputs[5].Value.ToString() == "" ? 0 : int.Parse(spInputs[5].Value.ToString());
            Teamax.Common.PageManage page = new Teamax.Common.PageManage();
            page.ds = ds;

            if (_RowCount % pageSize != 0)
                _PageCount++;
            if (_PageCount < pageIndex)
                pageIndex = _PageCount;
            page.rowCount = _RowCount;
            page.pageCount = _PageCount;
            page.pageSize = pageSize;
            return page; ;
        }


        //根据搜索条件得到环卫人员列表(新)
        public Teamax.Common.PageManage GetAllPersonnels(person per, int pageIndex, int pageSize)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@departcode",per.organ),
                                new SqlParameter("@Type", per.type),
                                new SqlParameter("@name", per.name),
                                new SqlParameter("@personid", per.personID),
                                new SqlParameter("@phone",per.phone),
                                new SqlParameter("@street",per.street),  
                                new SqlParameter("@PageIndex",pageIndex),
                                new SqlParameter("@RowCount",SqlDbType.Int),
                                new SqlParameter("@PageCount",SqlDbType.Int),
                                new SqlParameter("@PageSize",pageSize),
                                new SqlParameter("@Order","asc"),
                                new SqlParameter("@Field","id")};

                arrSP[7].Direction = ParameterDirection.Output;
                arrSP[8].Direction = ParameterDirection.Output;


                DataSet ds = this.ExecuteDataset("pr_p_GetPersonList", CommandType.StoredProcedure, arrSP);
                Teamax.Common.PageManage page = new Teamax.Common.PageManage();
                page.ds = ds;
                page.rowCount = Convert.ToInt32(arrSP[7].Value);
                page.pageCount = Convert.ToInt32(arrSP[8].Value);
                page.pageSize = pageSize;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据搜索条件得到环卫人员列表
        public PageManage GetAllPersonnel(person per, int pageIndex, int pageSize)
        {
            string select = "id,personID,name,phone,area,street,organ,sex,date,address";
            string where = "1=1 ";

            if (per.name != null && !"".Equals(per.name))
            {
                where += " and name like '%" + per.name + "%'";
            }
            if (per.phone != null && !"".Equals(per.phone))
            {
                where += " and phone like '%" + per.phone + "%'";
            }
            if (per.area != null && !"".Equals(per.area))
            {
                where += " and area like '" + per.area + "'";
            }
            if (per.street != null && !"".Equals(per.street))
            {
                where += " and street like '" + per.street + "'";
            }
            if (per.organ != null && !"".Equals(per.organ))
            {
                where += " and organ = '" + per.organ + "'";
            }
            string from = "s_personnel ";

            try
            {
                QueryUtil qu = new QueryUtil(select, from, where);
                qu.PageSize = pageSize;
                qu.SortBy = "id";
                qu.SortOrder = Teamax.Common.SortOrder.Descending;
                DataSet ds = qu.ExecuteDataset(pageIndex);
                PageManage page = new PageManage();
                page.ds = ds;
                page.rowCount = qu.RowCount;
                page.pageCount = qu.PageCount;
                page.pageSize = qu.PageSize;

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int isDelSuscessbyDept(string departcode)
        {
            string sql3 = "select id from s_personnel where organ= '" + departcode + "'";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql3);
                if (rs.HasRows)
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string sql2 = " select departcode from s_depart_outer where isnull(isdel,0)=0 and  parentcode= '" + departcode + "'";
            
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql2);
                if (rs.HasRows)
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }

        //判断用户输入的员工编号是否已经存在

        public bool hasPersonID(string personID)
        {
            string sql = "select count(*) as count from s_personnel  where personID='" + personID + "'";
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
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region 添加员工信息;
        /// <summary>
        /// 新增员工信息  
        /// </summary>
        /// <param name="person">员工实体</param>
        /// <returns></returns>
        public int insertIntoPersonnel(person person)
        {
            string sql = "insert into s_personnel(personID,name,sex,date,area,street,community,phone,telephone,organ,degree,technology,type,character,address) "
                       + " values('" + person.personID + "','" + person.name + "'," + person.sex + ",'" + person.date + "','" + person.area + "','" + person.street
                       + "','" + person.community + "','" + person.phone + "','" + person.telephone + "','" + person.organ + "','" + person.degree + "','"
                       + person.technology + "','" + person.type + "','" + person.character+ "','" + person.address + "')";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion;

        #region 修改员工信息;
        /// <summary>
        /// 修改员工信息;
        /// </summary>
        /// <param name="person">员工实体</param>
        /// <returns></returns>
        public int updateIntoPersonnel(person person)
        {
            string sql = "update s_personnel set personID='" + person.personID + "',name='" + person.name + "',sex='" + person.sex + "',date='" + person.date 
                       + "',area='" + person.area + "',street='" + person.street + "',community='" + person.community + "',phone='" + person.phone + "',telephone='" 
                       + person.telephone + "',organ='" + person.organ + "',degree='" + person.degree + "',technology='" + person.technology + "',type='"
                       + person.type + "',character='" + person.character + "',address='" + person.address + "' where id=" + person.id;

            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion;

        #region 删除环卫员工;
        /// <summary>
        /// 根据员工ID，删除员工信息;
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <returns></returns>
        public int deleteFromPersonnel(int id)
        {
            string sql = "delete from s_personnel where id =" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                int i = commondatabase.ExecuteNonQuery(sql);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion;

        #region 根据员工编号得到该员工信息;
        /// <summary>
        /// 根据员工编号得到该员工信息
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <returns></returns>
        public SqlDataReader getPersonnelInfoByID(int id)
        {
            string sql = "select personID,name,sex,date,area,street,community,phone,telephone,organ,degree,technology,type,character,address from s_personnel where id=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);             
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion;

        public SqlDataReader getDeptNameByID(string id)
        {
            string sql = "select departcode,UserDefinedCode,departname,parentcode,area from p_depart where departcode=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader getDeptNameByIDOutDept(string id)
        {
            string sql = "select departcode,UserDefinedCode,departname,parentcode,area from s_depart_outer where departcode=" + id;
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader getAreaNameByID(string id)
        {
            string sql = "select areaname from s_area where areacode=" + id + " ";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader getStreetNameByID(string id)
        {
            string sql = "select streetname from s_street where streetcode=" + id + " ";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader getCommunityNameByID(string id)
        {
            string sql = "select commname from s_community where commcode=" + id + " ";
            try
            {
                CommonDatabase commondatabase = new CommonDatabase();
                SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region GetCharActerList : 获取人员性质
        public DataSet GetCharActerList()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 6 and isnull(status,0) = 0  and codeid > 0";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion

        #region GetPeopleType : 获取人员类型
        public DataSet GetPeopleType()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 5 and isnull(status,0) = 0  and codeid > 0";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion

        #region GetDegreeType : 获取学历类型
        public DataSet GetDegreeType()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 11 and isnull(status,0) = 0  and codeid > 0";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion

        #region GetTechnologyType : 获取技术职称
        public DataSet GetTechnologyType()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 13 and isnull(status,0) = 0  and codeid > 0";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
        #endregion
    }
}
