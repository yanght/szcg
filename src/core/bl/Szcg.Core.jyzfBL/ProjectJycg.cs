using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using bl.zhifa;
using dbl.zhifa;
using System.Collections;

namespace bl.zhifa
{
    public class ProjectJycg
    {
        public ProjectJycg()
        {
        }

        //数字城管执法登记列表
        public DataSet GetProjectJycgList(projectjyzf proj, int PageSize, int PageIndex, string Order, string Field)
        {
            try
            { 
                dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                int RowCount=1;
                int PageCount=2;
                return dl.GetProjectJycgList(proj.AcceptID, proj.Time1, proj.Time2, proj.Area, proj.ApplyUnit, proj.IssuranceUnit,RowCount, PageCount, PageIndex, PageSize, Order, Field);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //加载类别信息
        public DataSet GetCategoryList()
        {
            try
            {
                dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                return dl.GetCategoryList();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //加载单条登记信息
        public DataSet GetProject(int id)
        {
            try
            {
                dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                return dl.GetProject(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        //城管执法登记
        public void AddCategory(projectjyzf proj, ref string strErr)
        {
            try
            {
                   dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                   dl.AddCategory(proj.Category,
	                                        proj.AcceptID,
	                                        proj.Area,
                                            proj.ApplyUnit,
	                                        proj.ContactPerson,
	                                        proj.ContactWay,
	                                        proj.IssuranceUnit,
	                                        proj.IssurancePerson,
	                                        proj.IssuranceWay,
	                                        proj.Dimension,
	                                        proj.ArrangeAdd,
	                                        proj.RegisterTime,
	                                        proj.BeginTime,
                                            proj.EndTime,
                                            proj.AccessoriesName);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        //信息修改
        public void UpdateCategory(projectjyzf proj, ref string strErr)
        {
            try
            {
                   dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                   dl.UpdateCategory(proj.Id, 
                                            proj.Category,
	                                        proj.AcceptID,
	                                        proj.Area,
                                            proj.ApplyUnit,
	                                        proj.ContactPerson,
	                                        proj.ContactWay,
	                                        proj.IssuranceUnit,
	                                        proj.IssurancePerson,
	                                        proj.IssuranceWay,
	                                        proj.Dimension,
	                                        proj.ArrangeAdd,
	                                        proj.RegisterTime,
	                                        proj.BeginTime,
                                            proj.EndTime,
                                            proj.AccessoriesName);
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }


        //数字城管执法处罚登记列表
        public DataSet GetProjectZFCFList(projectzfcf proj,int RowCount, int PageCount,int PageSize, int PageIndex, string Order, string Field)
        {
            try
            {
                dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
                return dl.GetProjectCFList(proj.PunishCode,proj.PunishOfficerID, proj.Time1, proj.Time2, RowCount, PageCount, PageIndex, PageSize, Order, Field);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
  
        //数字城管执法处罚登记详细信息
        public projectzfcf getZfProjDetail(string PunishCode)
        {
            projectzfcf proj = new projectzfcf();
            dl.zhifa.ProjectJycg dl = new dl.zhifa.ProjectJycg();
            DataSet ds = null;
            try
            {
                ds = dl.GetZfProjDetail(PunishCode);
                if (ds.Tables.Count != 0)
                {
                    proj.Name = ds.Tables[0].Rows[0]["ArtiName"].ToString(); //ArtiName: 被罚款人姓名
                    if (ds.Tables[0].Rows[0]["ArtiSex"].ToString() == "1")//ArtiSex: 性别
                    {
                        proj.Sex= "男";
                    }
                    else
                    {
                        proj.Sex = "女";
                    }
                    proj.Age = ds.Tables[0].Rows[0]["ArtiAge"].ToString();//ArtiAge: 年龄
                    proj.Address = ds.Tables[0].Rows[0]["ArtiAddress"].ToString();//ArtiAddress: 住址
                    proj.BusinessDate= ds.Tables[0].Rows[0]["BusinessDate"].ToString();//BusinessDate: 经营活动发生日期
                    proj.BusinessAddress = ds.Tables[0].Rows[0]["BusinessAddress"].ToString();//BusinessAddress: 经营地点
                    proj.BusinessContent= ds.Tables[0].Rows[0]["BusinessContent"].ToString();//BusinessContent: 经营内容
                    proj.PunishDate = ds.Tables[0].Rows[0]["PunishDate"].ToString();//PunishDate: 罚款日期
                    proj.PunishAddress = ds.Tables[0].Rows[0]["PunishAddress"].ToString();//PunishAddress: 罚款地点(保留)
                    proj.PunishContent = ds.Tables[0].Rows[0]["PunishContent"].ToString();//PunishContent: 罚款内容
                    proj .PunishMoney = ds.Tables[0].Rows[0]["PunishMoney"].ToString();//PunishMoney: 罚款人民币
                    proj.ConficateMoney  = ds.Tables[0].Rows[0]["ConficateMoney"].ToString();//ConficateMoney: 没收非法所得人民币
                    proj .PunishOfficerID= ds.Tables[0].Rows[0]["PunishOfficerID"].ToString();//PunishOfficerID: 执法员ID (跟m_Collecter表的collcode对应)
                    proj.PunishPhotos  = ds.Tables[0].Rows[0]["PunishPhotos"].ToString();//PunishPhotos: 现场照片
                }
                
                return proj;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
