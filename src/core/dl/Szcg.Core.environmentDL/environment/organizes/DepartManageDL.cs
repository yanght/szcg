/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：部门结构树管理。
 * 结构组成：
 * 作    者：王超群
 * 创建日期：2007-05-25
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using Teamax.Common;

namespace bacgDL.environment.organizes
{
    public class DepartInfo
    {
        public string DepartCode;
        public string UserDefinedCode;
        public string DepartName;
        public string ParentCode;
        public string ParentName;
        public string Area;
        public string Principal;
        public string Mobile;
        public string Tel;
        public string StartDate;
        public string EndDate;
        public string Duty_Addarea;
        public string P_number;
        public string Expense;
        public string AreaCode;
        public string AreaDetail;
        public string DepartAdress;
        public string Memo;
        public bool IsOuter;
        public string GuideCode;
        public string GuideName;
        public string type;
    }
    public class DepartManageDL : CommonDatabase
    {

        #region GetDepartList：获取部门列表
        /// <summary>
        /// 获取部门的列表
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public DataTable GetDepartList(string departcode,string Type)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@departcode", departcode),
                new SqlParameter("@Type", Type)
            };
            DataSet ds = ExecuteDataset("pr_p_GetDepartList", CommandType.StoredProcedure, spInputs);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取部门的列表
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public DataTable GetDepartListDetail(string departcode, string Type)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@departcode", departcode),
                new SqlParameter("@Type", Type)
            };
            DataSet ds = ExecuteDataset("pr_p_GetDepartListDetail", CommandType.StoredProcedure, spInputs);
            return ds.Tables[0];
        }

      

        /// <summary>
        /// 删除部门
        /// </summary>
        public int DelDepartInfo(string departcode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@departcode", departcode)
            };
            int tag =(int) ExecuteScalar("pr_p_DelDepartInfo", CommandType.StoredProcedure, spInputs);
            return tag;
        }

        /// <summary>
        /// 获取部门的详细信息
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public DepartInfo GetDepartOuterInfo(string departcode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@departcode", departcode)
            };
            DataSet ds = ExecuteDataset("pr_p_GetDepartOuterInfo", CommandType.StoredProcedure, spInputs);
            DepartInfo di = new DepartInfo();
            if (ds.Tables[0].Rows.Count == 1)
            {
                di.DepartCode = ds.Tables[0].Rows[0]["departcode"].ToString();
                di.UserDefinedCode = ds.Tables[0].Rows[0]["UserDefinedCode"].ToString();
                di.DepartName = ds.Tables[0].Rows[0]["departname"].ToString();
                di.Area = ds.Tables[0].Rows[0]["area"].ToString();
                di.ParentCode = ds.Tables[0].Rows[0]["parentcode"].ToString();
                di.Principal = ds.Tables[0].Rows[0]["principal"].ToString();
                di.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                di.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                di.DepartAdress = ds.Tables[0].Rows[0]["departadress"].ToString();
                di.Memo = ds.Tables[0].Rows[0]["memo"].ToString();
                di.StartDate = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["startdate"].ToString()).ToShortDateString();
                di.EndDate = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["enddate"].ToString()).ToShortDateString();
                di.Duty_Addarea = ds.Tables[0].Rows[0]["duty_addarea"].ToString();
                di.Expense = ds.Tables[0].Rows[0]["expense"].ToString();
                di.P_number = ds.Tables[0].Rows[0]["p_number"].ToString();
                di.AreaCode = ds.Tables[0].Rows[0]["areacode"].ToString();
                di.AreaDetail = ds.Tables[0].Rows[0]["areadetail"].ToString();
                di.ParentName = ds.Tables[0].Rows[0]["parentdepartname"].ToString();
                di.IsOuter = true;
            }
            return di;   
        }
        #endregion

        #region 修改部门
        /// <summary>
        ///修改部门 
        /// </summary>
        public string  SetDepartInfo(DepartInfo di)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                 new SqlParameter("@departcode", di.DepartCode==null?-1:int.Parse(di.DepartCode)),
                 new SqlParameter("@UserDefinedCode", di.UserDefinedCode),
                 new SqlParameter("@departname", di.DepartName),
                 new SqlParameter("@parentcode", int.Parse(di.ParentCode)),
                 new SqlParameter("@area", di.Area),
                 new SqlParameter("@principal", di.Principal),
                 new SqlParameter("@Mobile", di.Mobile),
                 new SqlParameter("@Tel", di.Tel),
                 new SqlParameter("@startdate", di.StartDate),
                 new SqlParameter("@enddate", di.EndDate),
                 new SqlParameter("@duty_addarea", di.Duty_Addarea==""?0:Convert.ToDecimal(di.Duty_Addarea)),
                 new SqlParameter("@p_number", di.P_number==""?0:int.Parse(di.P_number)),
                 new SqlParameter("@expense", di.Expense==""?0:Convert.ToDecimal(di.Expense)),
                 new SqlParameter("@areacode", di.AreaCode),
                 new SqlParameter("@areadetail", di.AreaDetail),
                 new SqlParameter("@departadress", di.DepartAdress),
                 new SqlParameter("@memo", di.Memo),
                 new SqlParameter("@IsOuter", di.IsOuter==true?1:0),
                 new SqlParameter("@Type", di.type)
            };
            spInputs[0].Direction = ParameterDirection.InputOutput;
            ExecuteNonQuery("pr_p_SetDepartInfo", CommandType.StoredProcedure, spInputs);

            return spInputs[0].Value.ToString();
        }

        #endregion

        #region 设置指导单位
        /// <summary>
        ///设置指导单位 
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public int SetGuideDepart(string departcode,string guidecode,string guidename)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                 new SqlParameter("@departcode", departcode),
                 new SqlParameter("@guidecode", guidecode),
                 new SqlParameter("@guidename", guidename)
            };
            int tag = ExecuteNonQuery("pr_p_SetGuideDepart", CommandType.StoredProcedure, spInputs);
            return tag;
        }
        #endregion

        /// <summary>
        /// 设置责任区域
        /// </summary>
        public int SetDutyAreaInfo(string departcode, string areacode, string areaname, string areanumber)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                 new SqlParameter("@departcode", departcode),
                 new SqlParameter("@areacode", areacode),
                 new SqlParameter("@areaname", areaname),
                 new SqlParameter("@areanumber", areanumber)
            };
            int tag = ExecuteNonQuery("pr_p_SetDutyAreaInfo", CommandType.StoredProcedure, spInputs);
            return tag;
        }

        #region 获取指导单位
        /// <summary>
        ///获取指导单位 
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public string[] GetGuideDepart(string departcode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                 new SqlParameter("@departcode", departcode)
            };
            DataSet ds = ExecuteDataset("select guidecode,guidename from dbo.[p_depart_guide] where departcode=" + departcode);
            
            string[] guideinfo = new string[2];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    guideinfo[0] = ds.Tables[0].Rows[i]["guidecode"].ToString();
                    guideinfo[1] = ds.Tables[0].Rows[i]["guidename"].ToString();
                }
                else
                {
                    guideinfo[0] = guideinfo[0] + "," + ds.Tables[0].Rows[i]["guidecode"].ToString();
                    guideinfo[1] = guideinfo[1] + "," + ds.Tables[0].Rows[i]["guidename"].ToString();
                }
            }
            return guideinfo;
        }

        #endregion

        /// <summary>
        ///获取责任区域
        /// </summary>
        /// <param name="areacode">部门编码</param>
        /// <returns></returns>
        public DataTable GetDutyArea(string departcode)
        {
            SqlParameter[] spInputs = new SqlParameter[]{
                 new SqlParameter("@departcode", departcode)
            };
            DataSet ds = ExecuteDataset("select areacode,areaname,areanumber from dbo.[p_depart_dutyarea] where departcode=" + departcode);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取部门标识
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public SqlDataReader getDepartType(int departcode)
        {
            string sql = "select a.* from p_depart a where a.departcode =" + departcode;
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

        public bool HasDeptName(string name)
        {
            string sql = "select departname from (select departname,departcode,isDel from p_depart union select departname,departcode,isDel from s_depart_outer) a where departname='"+name+"' and isnull(isDel,0) = 0";
            
            try
            {
                using (CommonDatabase commondatabase = new CommonDatabase())
                {
                    SqlDataReader rs = (SqlDataReader)commondatabase.ExecuteReader(sql);
                    if (rs.HasRows)
                    {
                        rs.Close();
                        return true;
                    }
                    else
                    {
                        rs.Close();
                        return false;
                    }                    
                }
            }
            catch
            {
                return false;
            }
        }

      
        public int SetDepartType(DepartInfo mat)
        {
            string sql = "update p_depart set type = '" + mat.type + "' where departcode=" + mat.DepartCode;

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


        public DataSet GetDepartSignList()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 14 and codeid > 0 and codeid <> 20";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }

        public DataSet GetDepartGnSignList()
        {
            string strSQL = "select codeid as codeid,codename as codename from s_codedict where codetype = 14 and (codeid >= 20 or codeid = 9) ";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }

        public DataSet GetTargetDepartList()
        {
            string strSQL = "select userdefinedcode,departname,departcode,parentcode from p_depart where isduty = 1 ";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }

        public DataSet GetTargetDepart(string value)
        {
            string strSQL = "select userdefinedcode,departname,departcode,parentcode from p_depart where isduty = 1 and userdefinedcode like '" + value + "%' and userdefinedcode <> '" + value + "' ";
            DataSet ds = this.ExecuteDataset(strSQL);
            return ds;
        }
    }
}
