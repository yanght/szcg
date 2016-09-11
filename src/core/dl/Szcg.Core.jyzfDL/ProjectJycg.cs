using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Teamax.Common;
using System.Data.SqlClient;
using System.IO;


namespace dl.zhifa
{
    public class ProjectJycg : Teamax.Common.CommonDatabase, IDisposable
    {

        //数字城管执法登记列表
        public DataSet GetProjectJycgList(string AcceptID,string starttime, string endtime, string area, string ApplyUnit, string IssuranceUnit, int RowCount,
                                                             int PageCount, int CurrentPage, int PageSize, string Order, string Field)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@AcceptID",AcceptID),
                        new SqlParameter("@startdate",starttime),
                        new SqlParameter("@enddate",endtime),
                        new SqlParameter("@area",area),
                        new SqlParameter("@ApplyUnit",ApplyUnit),
                        new SqlParameter("@IssuranceUnit",IssuranceUnit),
                        new SqlParameter("@RowCount",RowCount),
                        new SqlParameter("@PageCount",PageCount),
                        new SqlParameter("@CurrentPage",CurrentPage),
                        new SqlParameter("@PageSize",PageSize),
                        new SqlParameter("@Order",Order),
                        new SqlParameter("@Field",Field)
                    };
            return this.ExecuteDataset("pr_z_projList", CommandType.StoredProcedure, arrSP);
        }

        //加载类别列表
        public DataSet GetCategoryList()
        {
            string sql = "select * from e_CategoryName_jyzf";
            return this.ExecuteDataset(sql);
        }

        //加载单条登记信息
        public DataSet GetProject(int id)
        {
            string sql = "select * from b_project_jyzf where id=" + id;
            return this.ExecuteDataset(sql);
        }


        //案卷登记
        public void AddCategory(int Category,  string AcceptID,  string Area,  string ApplyUnit, string ContactPerson,  string ContactWay,
	                                        string IssuranceUnit, string IssurancePerson,  string IssuranceWay,  string Dimension, string ArrangeAdd,
	                                        string RegisterTime, string BeginTime,   string EndTime,  string AccessoriesName)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@Category",Category),
                        new SqlParameter("@AcceptID",AcceptID),
                        new SqlParameter("@area",Area),
                        new SqlParameter("@ApplyUnit",ApplyUnit),
                        new SqlParameter("@contactPerson",ContactPerson),
                        new SqlParameter("@contactWay",ContactWay),
                        new SqlParameter("@IssuranceUnit",IssuranceUnit),
                        new SqlParameter("@IssurancePerson",IssurancePerson),
                        new SqlParameter("@IssuranceWay",IssuranceWay),
                        new SqlParameter("@Dimension",Dimension),
                        new SqlParameter("@ArrangeAdd",ArrangeAdd),
                        new SqlParameter("@RegisterTime",RegisterTime),
                        new SqlParameter("@BeginTime",BeginTime),
                        new SqlParameter("@EndTime",EndTime),
                        new SqlParameter("@AccessoriesName",AccessoriesName)
                        
                    };
            this.ExecuteDataset("pr_z_insertproject", CommandType.StoredProcedure, arrSP);
        }

        //案卷修改
        public void UpdateCategory(string id, int Category, string AcceptID, string Area, string ApplyUnit, string ContactPerson, string ContactWay,
                                            string IssuranceUnit, string IssurancePerson, string IssuranceWay, string Dimension, string ArrangeAdd,
                                            string RegisterTime, string BeginTime, string EndTime, string AccessoriesName)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@id",id),
                        new SqlParameter("@Category",Category),
                        new SqlParameter("@AcceptID",AcceptID),
                        new SqlParameter("@area",Area),
                        new SqlParameter("@ApplyUnit",ApplyUnit),
                        new SqlParameter("@contactPerson",ContactPerson),
                        new SqlParameter("@contactWay",ContactWay),
                        new SqlParameter("@IssuranceUnit",IssuranceUnit),
                        new SqlParameter("@IssurancePerson",IssurancePerson),
                        new SqlParameter("@IssuranceWay",IssuranceWay),
                        new SqlParameter("@Dimension",Dimension),
                        new SqlParameter("@ArrangeAdd",ArrangeAdd),
                        new SqlParameter("@RegisterTime",RegisterTime),
                        new SqlParameter("@BeginTime",BeginTime),
                        new SqlParameter("@EndTime",EndTime),
                        new SqlParameter("@AccessoriesName",AccessoriesName)
                        
                    };
            this.ExecuteDataset("pr_z_updateproject", CommandType.StoredProcedure, arrSP);
        }

        //数字城管执法处罚登记列表
        public DataSet GetProjectCFList(string PunishCode, string PunishOfficerID, string starttime, string endtime, int RowCount,
                                                             int PageCount, int CurrentPage, int PageSize, string Order, string Field)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@RowCount",SqlDbType.Int),
                        new SqlParameter("@PageCount",SqlDbType.Int),
                        new SqlParameter("@PunishCode",PunishCode),
                        new SqlParameter("@PunishOfficerID",PunishOfficerID),
                        new SqlParameter("@startdate",starttime),
                        new SqlParameter("@enddate",endtime),
                        new SqlParameter("@CurrentPage",CurrentPage),
                        new SqlParameter("@PageSize",PageSize),
                        new SqlParameter("@Order",Order),
                        new SqlParameter("@Field",Field)
                    };
                arrSP[0].Direction = ParameterDirection.Output;
                arrSP[1].Direction = ParameterDirection.Output;
                ds =  this.ExecuteDataset("pr_z_projcfList", CommandType.StoredProcedure, arrSP);

                if (ds.Tables.Count != 0)
                {
                    RowCount = Convert.ToInt32(arrSP[0].Value);
                    PageCount = Convert.ToInt32(arrSP[1].Value);
                }
                else
                {
                    ds = null;
                }
                return ds;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //数字城管执法处罚登记详细信息
        public DataSet GetZfProjDetail(string PunishCode)
        {
            try
            {
                SqlParameter[] arrSP = new SqlParameter[]{
                        new SqlParameter("@PunishCode",PunishCode),                     
                    };
                return this.ExecuteDataset("pr_z_getZfProjDetail", CommandType.StoredProcedure, arrSP);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
