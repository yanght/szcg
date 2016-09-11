/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：绿地部件数据访问
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
using Teamax.Common;
using System.Data;
using System.Data.SqlClient;
using bacgDL.Pub;


namespace bacgDL.greenland
{
    /// <summary>
    /// GreenLendQuery查询条件
    /// </summary>
    public class GreenLendQuery
    {
        private string _objName = "";
        private string _deptCode = "";
        private string _deptName = "";
        private string _ownerCode = "";
        private string _ownerName = "";
        private string _objPos = "";
        public string startDate = "";
        public string endDate = "";
        public string objType = "";
        public int pageIndex = 0;
        public int pageSize =15;

        public string _bgCode = "false";
        public string bgCode
        {
            get { return _bgCode; }
            set
            {
                string tempBgcode=DataProvider.BGCodeFormat(value);
                if(tempBgcode!="")
                   _bgCode = tempBgcode;
            }
        }

        public string deptCode
        {
            get { return _deptCode; }
            set
            {
                Public pub = new Public();
                _deptCode = value.Replace("'", "''");
               
            }    
        }
        public string deptName
        {
            get { return _deptName; }
            set
            {
                Public pub = new Public();
                _deptName = value.Replace("'", "''");

            }
        }
        public string ownerName
        {
            get { return _ownerName; }
            set
            {
                Public pub = new Public();
                _ownerName = value.Replace("'", "''");
            }
        }
        public string ownerCode
        {
            get { return _ownerCode; }
            set
            {
                Public pub = new Public();
                _ownerCode = value.Replace("'", "''");
            }
        }
        public string objPos
        {
            get { return _objPos; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 80)
                    {
                        _objPos = value.Replace("'", "''").Substring(0, 80);
                    }
                    else
                    {
                        _objPos = value.Replace("'", "''");
                    }
                }
            }
        }

        public string objName
        {
            get { return _objName;}
            set
            {
                if (value != null)
                {
                    if (value.Length > 30)
                    {
                        _objName = value.Replace("'", "''").Substring(0, 30);
                    }
                    else
                    {
                        _objName = value.Replace("'", "''");
                    }
                }
            }
        }
    }

    /// <summary>
    /// FamouTreeQuery查询条件类
    /// </summary>
    public class FamouTreeQuery
    {
        private string _treeName = "";
        public string objCode = "";
        private string _deptCode = "";
        private string _manager = "";
        public int pageIndex = 0;
        public int pageSize = 15;

        public string _bgCode = "";
        public string bgCode
        {
            get { return _bgCode; }
            set
            {
                string tempBgcode = DataProvider.BGCodeFormat(value);
                if (tempBgcode != "")
                    _bgCode = tempBgcode;
            }
        }

        public string deptCode
        {
            get { return _deptCode; }
            set
            {
                if (value != null)
                {
                    Public pub = new Public();
                    _deptCode = pub.GetSubDepartCode(value);
                }
            }
        }

        public string treeName
        {
            get { return _treeName; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 100)
                    {
                        _treeName = value.Replace("'", "''").Substring(0, 100);
                    }
                    else
                    {
                        _treeName = value.Replace("'", "''");
                    }
                }
            }
        }

        public string manager
        {
            get { return _manager; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 50)
                    {
                        _manager = value.Replace("'", "''").Substring(0, 50);
                    }
                    else
                    {
                        _manager = value.Replace("'", "''");
                    }
                }
            }
        }
        
    }

    /// <summary>
    /// 评价属性类
    /// </summary>
    public class Appraise
    {
        public int id = 0;
        public int  level=0;
        private string _username="";
        public int objcode=0;
        private string _info = "";
        private string _memo = "";
        public string datetime;

        public string username
        {
            get { return _username; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 50)
                    {
                        _username = value.Replace("'", "''").Substring(0, 50);
                    }
                    else
                    {
                        _username = value.Replace("'", "''");
                    }
                }
            }
        }

        public string info
        {
            get { return _info; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 3000)
                    {
                        _info = value.Replace("'", "''").Substring(0, 3000);
                    }
                    else
                    {
                        _info = value.Replace("'", "''");
                    }
                }
            }
        }

        public string memo
        {
            get { return _memo; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 3000)
                    {
                        _memo = value.Replace("'", "''").Substring(0, 3000);
                    }
                    else
                    {
                        _memo = value.Replace("'", "''");
                    }
                }
            }
        }
    }

    public class DataProvider 
    {
        public const string strKey = "SdeConnString";
        /// <summary>
        /// 获取绿地列表
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public QueryUtil GetGreenLendList(GreenLendQuery sq, string select, string layer)
        {
            string where="1=1";

            where = where + GetObjTypeSql(sq.objType);
            if (sq.startDate != "")
                where += " and ORDATE >=convert(datetime,'" + sq.startDate + "')";
            if (sq.endDate != "")
                where += " and ORDATE <=convert(datetime,'" + sq.endDate + "')";
            string[] bgcodes = sq.bgCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if(bgcodes.Length>0)
            {
                 for(int i=0;i<bgcodes.Length;i++)
                 {
                     if(i==0)
                         where += " and (charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                     else
                         where += " or charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                 }
                where +=")";
            }
            if (sq.ownerCode != "")
            {
                if (sq.deptCode != "")
                {
                    where += " and (charindex('" + sq.ownerCode + "',OWNERNAME)>0 or charindex('" + sq.deptCode + "',DEPTNAME)>0)";
                }
                else
                {
                    where += " and charindex('" + sq.ownerCode + "',OWNERNAME)>0 ";
                }
            }
            else
            {
                if (sq.deptCode != "")
                    where += " and charindex('" + sq.deptCode + "',DEPTNAME)>0 ";
            }
            if (sq.deptName != "")
                where += " and DEPTNAME like '%" + sq.deptName + "%'";
            if (sq.ownerName != "")
                where += " and OWNERNAME like '%" + sq.ownerName + "%'";
            if (sq.objName != "")
                where += " and charindex('"+  sq.objName +"',OBJNAME)>0 ";
            if (sq.objPos != "")
                where += " and charindex('"+ sq.objPos +"',OBJPOS)>0 ";
           
            QueryUtil qu = new QueryUtil(select, layer, where);
            qu.Key = "OBJECTID";
            qu.SortBy = "OBJECTID";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize = sq.pageSize;
            if (sq.pageSize == 0)
            {
                qu.ExecuteDataset(strKey);
            }
            else
            {
                qu.ExecuteDataset(sq.pageIndex, strKey);
            }
            return qu;
        }

        private static string GetObjTypeSql(string objType)
        {
            string where = "";
            if (objType != "")
            {

                if (objType == "其他绿地")//特殊处理 为空的归为其他绿地
                {
                    where += " and (OBJTYPE = '" + objType + "' or OBJTYPE is null) ";
                }
                else
                {
                    where += " and OBJTYPE = '" + objType + "'";
                }
            }
            return where;
        }

        /// <summary>
        /// 获取绿设施列表（包括sde.部件_宝安_绿地护栏", "sde.部件_宝安_绿地维护设施", "sde.部件_宝安_护树设施", "sde.部件_宝安_花架花钵）
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public QueryUtil GetGuardrailList(GreenLendQuery sq, string select, string layer)
        {
            string where = "1=1";

            where = where + GetObjTypeSql(sq.objType);
            if (sq.startDate != "")
                where += " and ORDATE >=convert(datetime,'" + sq.startDate + "')";
            if (sq.endDate != "")
                where += " and ORDATE <=convert(datetime,'" + sq.endDate + "')";
            if (sq.deptName != "")
                where += " and DEPTNAME like '%" + sq.deptName + "%'";
            if (sq.ownerName != "")
                where += " and OWNERNAME like '%" + sq.ownerName + "%'";
            if (sq.bgCode != "" && sq.bgCode!="false")
                where += " and charindex('" + sq.bgCode + "',BGCODE)>0";
            if (sq.objName != "")
                where += " and charindex('" + sq.objName + "',OBJNAME)>0 ";
            if (sq.objPos != "")
                where += " and charindex('" + sq.objPos + "',OBJPOS)>0 ";

            QueryUtil qu = new QueryUtil(select, layer, where);
            qu.Key = "OBJECTID";
            qu.SortBy = "OBJECTID";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize = sq.pageSize;
            if (sq.pageSize == 0)
            {
                qu.ExecuteDataset(strKey);
            }
            else
            {
                qu.ExecuteDataset(sq.pageIndex, strKey);
            }
             
            return qu;
        }
        
        /// <summary>
        /// 获取绿地列表
        /// </summary>
        /// <param name="sq"></param>
        /// <returns></returns>
        public DataTable GetGreenLendList(GreenLendQuery sq)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("\n declare @objType varchar(50) ,");
		    sb.Append("\n @BGCODE  varchar(14)");
		    sb.Append("\n set @objType =@objType1;");
            sb.Append("\n set @BGCODE =@BGCODE1;");
            sb.Append("\n CREATE TABLE #AreaInfo");
            sb.Append("\n (");
            sb.Append("\n    AREARNAME varchar(18) null,");
	        sb.Append("\n    BGCODE varchar(18) null,");
	        sb.Append("\n    ADDAREA  numeric(38, 8)  NULL,");
	        sb.Append("\n    ROADTREEAREA  numeric(38, 8)  NULL,");
	        sb.Append("\n    COVERAREA  numeric(38, 8)  NULL,");
	        sb.Append("\n )");
	        sb.Append("\n if(@objType<>'')");
	        sb.Append("\n begin");
		    sb.Append("\n     if(len(@BGCODE)<=6)");
		    sb.Append("\n     begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n         SELECT '' as AREARNAME,");
            sb.Append("\n 	        substring(BGCODE,1,6),sum(isnull(ADDAREA,0)) as ADDAREA,sum(isnull(ROADTREEAREA,0)) as ROADTREEAREA,sum(isnull(COVERAREA,0)) as COVERAREA");
			sb.Append("\n         from sde.部件_宝安_绿地");
            sb.Append("\n         where charindex(@BGCODE,BGCODE)>0 "+GetObjTypeSql(sq.objType));
			sb.Append("\n         group by substring(BGCODE,1,6)");
		    sb.Append("\n     end");
		    sb.Append("\n     if(len(@BGCODE)<=9)");
		    sb.Append("\n    begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n        SELECT '' as AREARNAME,");
            sb.Append("\n 	        substring(BGCODE,1,9),sum(isnull(ADDAREA,0)) as ADDAREA,sum(isnull(ROADTREEAREA,0)) as ROADTREEAREA,sum(isnull(COVERAREA,0)) as COVERAREA");
            sb.Append("\n         from sde.部件_宝安_绿地");
            sb.Append("\n         where charindex(@BGCODE,BGCODE)>0 and len(BGCODE)>=9 " + GetObjTypeSql(sq.objType));
			sb.Append("\n         group by substring(BGCODE,1,9)");
		    sb.Append("\n     end");
		    sb.Append("\n     if(len(@BGCODE)<=12)");
		    sb.Append("\n     begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n         SELECT '' as AREARNAME ,");
            sb.Append("\n 	        substring(BGCODE,1,12),sum(isnull(ADDAREA,0)) as ADDAREA,sum(isnull(ROADTREEAREA,0)) as ROADTREEAREA,sum(isnull(COVERAREA,0)) as COVERAREA");
            sb.Append("\n        from sde.部件_宝安_绿地");
            sb.Append("\n       where charindex(@BGCODE,BGCODE)>0 and len(BGCODE)>=12 " + GetObjTypeSql(sq.objType));
			sb.Append("\n         group by substring(BGCODE,1,12)");
		    sb.Append("\n     end");
	        sb.Append("\n end");
	        sb.Append("\n else");
	        sb.Append("\n begin");
		    sb.Append("\n     if(len(@BGCODE)<=6)");
		    sb.Append("\n    begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n         SELECT '' as AREARNAME ,");
			sb.Append("\n 	        substring(BGCODE,1,6),sum(ADDAREA) as ADDAREA,sum(ROADTREEAREA) as ROADTREEAREA,sum(COVERAREA) as COVERAREA");
            sb.Append("\n         from sde.部件_宝安_绿地");
			sb.Append("\n         where charindex(@BGCODE,BGCODE)>0 ");
			sb.Append("\n         group by substring(BGCODE,1,6)");
		    sb.Append("\n     end");
		    sb.Append("\n     if(len(@BGCODE)<=9)");
		    sb.Append("\n    begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n         SELECT '' as AREARNAME ,");
			sb.Append("\n 	        substring(BGCODE,1,9),sum(ADDAREA) as ADDAREA,sum(ROADTREEAREA) as ROADTREEAREA,sum(COVERAREA) as COVERAREA");
            sb.Append("\n         from sde.部件_宝安_绿地");
			sb.Append("\n         where charindex(@BGCODE,BGCODE)>0 and len(BGCODE)>=9 ");
			sb.Append("\n         group by substring(BGCODE,1,9)");
		    sb.Append("\n     end");
		    sb.Append("\n     if(len(@BGCODE)<=12)");
		    sb.Append("\n     begin");
			sb.Append("\n         INSERT INTO #AreaInfo");
            sb.Append("\n         SELECT '' as AREARNAME ,");
			sb.Append("\n 	        substring(BGCODE,1,12),sum(ADDAREA) as ADDAREA,sum(ROADTREEAREA) as ROADTREEAREA,sum(COVERAREA) as COVERAREA");
            sb.Append("\n         from sde.部件_宝安_绿地");
			sb.Append("\n         where charindex(@BGCODE,BGCODE)>0 and len(BGCODE)>=12");
			sb.Append("\n         group by substring(BGCODE,1,12)");
		    sb.Append("\n     end");
	        sb.Append("\n end");
	        sb.Append("\n select * from #AreaInfo");
            SqlParameter[] spInputs = new SqlParameter[]{
                new SqlParameter("@objType1", sq.objType),
                new SqlParameter("@BGCODE1", sq.bgCode)
            };
            using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString(strKey)))
            {
                DataSet ds = cdb.ExecuteDataset(sb.ToString(), CommandType.Text, spInputs);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 古树名木列表获取
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="select"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public QueryUtil GetFamouTreeList(FamouTreeQuery sq, string select, string layer)
        {
            string where = "1=1";
            string[] bgcodes = sq.bgCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (bgcodes.Length > 0)
            {
                for (int i = 0; i < bgcodes.Length; i++)
                {
                    if (i == 0)
                        where += " and (charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                    else
                        where += " or charindex('" + bgcodes[i] + "',BGCODE)>0 ";
                }
                where += ")";
            }
            if (sq.deptCode != "")
                where += " and charindex('" + sq.deptCode + "',DEPTNAME)>0 ";
            if (sq.objCode != "")
                where += " and charindex('" + sq.objCode + "',OBJCODE)>0 ";
            if (sq.manager != "")
                where += " and charindex('" + sq.manager + "',MANAGER)>0 ";
            if (sq.treeName != "")
                where += " and charindex('" + sq.treeName + "',TREENAME)>0 ";

            QueryUtil qu = new QueryUtil(select, layer, where);
            qu.Key = "OBJECTID";
            qu.SortBy = "OBJECTID";
            qu.SortOrder = Teamax.Common.SortOrder.Descending;
            qu.PageSize = sq.pageSize;
            qu.ExecuteDataset(sq.pageIndex, strKey);
            return qu;
        }

        /// <summary>
        /// 修改部件
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int SetOBJInfo(string sql)
        {
            int tag = 0;
            using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString(strKey)))
            {
               tag= cdb.ExecuteNonQuery(sql);
            }
            return tag;
        }

        /// <summary>
        /// 取当前表最大排序列值并自增1
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="layer"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int selectMaxIndex(string tablename, string keyvalue, string columname, string datavalue)
        {
            string sql = "select isnull(Max(" + keyvalue + "),0) as " + keyvalue + " from " + tablename + " where 1=1 ";

            if (columname != null && columname != "" && datavalue != null && datavalue != "")
                sql = sql + " and " + columname + "='" + datavalue + "'";

            using (CommonDatabase DAO = new CommonDatabase(CommonDatabase.GetConnectionString(strKey)))
            {
                IDataReader rs = DAO.ExecuteReader(sql);
                int result = 0;
                if (rs != null)
                {
                    while (rs.Read())
                    {
                        result = Convert.ToInt32(rs[keyvalue].ToString()) + 1;
                    }
                }
                rs.Close();
                return result;
            }
        }

        /// <summary>
        /// 评价管理
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public DataTable AppraiseManage(Appraise appraise)
        {
            string strSql = "";
            if (appraise.level != 0 && appraise.info != null && appraise.username != null && appraise.objcode != 0)
            {
                strSql=" INSERT INTO a_greenland_appraise ([objCode],[username],[_level],[info],[memo])";
                strSql+=" VALUES(@objCode,@username,@_level,@info,@memo) select * from a_greenland_appraise where objCode =@objCode";
                SqlParameter[] paras = new SqlParameter[]{new SqlParameter("@objCode", appraise.objcode),
                                                          new SqlParameter("@username", appraise.username),
                                                          new SqlParameter("@_level", appraise.level),
                                                          new SqlParameter("@info", appraise.info),
                                                          new SqlParameter("@memo", appraise.memo)
                };

                using (CommonDatabase cd = new CommonDatabase())
                {
                  return  cd.ExecuteDataset(strSql, paras).Tables[0];
                }

            }
            if (appraise.objcode != 0)
            {
                strSql = " select * from a_greenland_appraise where objCode =@objCode";
                SqlParameter para = new SqlParameter("@objCode", appraise.objcode);

                using (CommonDatabase cd = new CommonDatabase())
                {
                    return cd.ExecuteDataset(strSql, para).Tables[0];
                }
                
            }
            return null;
        }

        /// <summary>
        ///  绿地统计查询
        /// </summary>
        /// <param name="bgCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetGreenLandStatInfo(string bgCode, string startDate, string endDate)
        {
            StringBuilder sb = new StringBuilder();  
            sb.Append("\n Declare @GTotal numeric(38, 8),");
            sb.Append("\n @CGPTotal numeric(38, 8),");
            sb.Append("\n @PNumber int,");
            sb.Append("\n @PTotal numeric(38, 8),");
            sb.Append("\n @CPTotal numeric(38, 8),");
            sb.Append("\n @PlazaNumber int,");
	    	sb.Append("\n @PlazaTotal numeric(38, 8),");
            sb.Append("\n @CPlazaTotal numeric(38, 8),");
            sb.Append("\n @STotal numeric(38, 8),");
            sb.Append("\n @CSTotal numeric(38, 8),");
            sb.Append("\n @ParkTotal numeric(38, 8),");
            sb.Append("\n @CParkTotal numeric(38, 8),");
            sb.Append("\n @AttTotal numeric(38, 8),");
            sb.Append("\n @CAttTotal numeric(38, 8),");
            sb.Append("\n @GuardTotal numeric(38, 8),");
            sb.Append("\n @CGuardTotal numeric(38, 8),");
            sb.Append("\n @ProduceTotal numeric(38, 8),");
            sb.Append("\n @CProduceTotal numeric(38, 8),");
            sb.Append("\n @OtherTotal numeric(38, 8),");
            sb.Append("\n @COtherTotal numeric(38, 8),");
            sb.Append("\n @CGTotal numeric(38, 8),");
            sb.Append("\n @CTreeTotal numeric(38, 8)");

            sb.Append("\n declare @varTab table(");
            sb.Append("\n [OBJTYPE] varchar(60)  NULL,");
		    sb.Append("\n [Count] int  NULL,");
		    sb.Append("\n [ADDAREA] numeric(38, 8)  NULL,");
		    sb.Append("\n [COVERAREA] numeric(38, 8)  NULL,");
		    sb.Append("\n [ROADTREEAREA] numeric(38, 8)  NULL");
	        sb.Append("\n )");
	        sb.Append("\n 	INSERT INTO @varTab");
            sb.Append("\n	select OBJTYPE,count(OBJTYPE)as [Count] ,SUM(isnull(ADDAREA,0)) as ADDAREA ,SUM(isnull(COVERAREA,0))as COVERAREA,SUM(isnull(ROADTREEAREA,0)) as ROADTREEAREA");
            sb.Append("\n from sde.部件_宝安_绿地 where 1=1 ");
            if (startDate!="")
                sb.Append("\n and ORDATE >=convert(datetime,'" + startDate + "') ");
            if (endDate != "")
                sb.Append("\n and ORDATE <=convert(datetime, '" + endDate + "') ");
            if (bgCode != "")
            sb.Append("\n and charindex('" + bgCode + "',BGCODE)>0");
            sb.Append("\n group by OBJTYPE ");
            sb.Append("\n select @CTreeTotal=SUM(ROADTREEAREA) from @varTab");
            sb.Append("\n select @GTotal= SUM(ADDAREA),@CGPTotal= SUM(COVERAREA) from @varTab");
		    sb.Append("\n select @PNumber=[Count],@PTotal=ADDAREA,@CPTotal=COVERAREA from @varTab where OBJTYPE='公园绿地'");
		    sb.Append("\n select @PlazaNumber=[Count],@PlazaTotal=ADDAREA,@CPlazaTotal=COVERAREA from @varTab where OBJTYPE='广场绿地'");
            sb.Append("\n select @STotal=SUM(ADDAREA),@CSTotal=SUM(COVERAREA) from @varTab where OBJTYPE='街道绿地' or OBJTYPE= '道路旁' or OBJTYPE= '道路旁绿地'");
            sb.Append("\n select @ParkTotal=isnull(@PTotal,0)+isnull(@PlazaTotal,0)+isnull(@STotal,0),@CParkTotal=isnull(@CPTotal,0)+isnull(@CPlazaTotal,0)+isnull(@CSTotal,0)");
            sb.Append("\n select @AttTotal=SUM(ADDAREA),@CAttTotal=SUM(COVERAREA) from @varTab where OBJTYPE='厂前绿地' or OBJTYPE='住宅绿地'");
		    sb.Append("\n select @GuardTotal=ADDAREA,@CGuardTotal=COVERAREA from @varTab where OBJTYPE='分车带'");
		    sb.Append("\n select @ProduceTotal=ADDAREA,@CProduceTotal=COVERAREA from @varTab where OBJTYPE='生产绿地'");
            sb.Append("\n select @OtherTotal=SUM(ADDAREA),@COtherTotal=SUM(COVERAREA) from @varTab where OBJTYPE ='社会公共类' or OBJTYPE ='非经营性'or OBJTYPE ='绿地' ");
            sb.Append("\n Declare @tempAREA numeric(38, 8), @tempCOVERAREA numeric(38, 8)");
            sb.Append("\n select @tempAREA=SUM(ADDAREA),@tempCOVERAREA=SUM(COVERAREA) from @varTab where OBJTYPE is null or OBJTYPE = ''");
            sb.Append("\n select @OtherTotal=isnull(@OtherTotal,0)+isnull(@tempAREA,0),@COtherTotal=isnull(@COtherTotal,0)+isnull(@tempCOVERAREA,0)");
            sb.Append("\n select @CGTotal = isnull(@CGPTotal,0) + isnull(@CTreeTotal,0)");
		
		    sb.Append("\n select @GTotal as GTotal,@CGPTotal as CGPTotal, @PNumber as PNumber,@PTotal as PTotal,@CPTotal as CPTotal,@PlazaNumber as PlazaNumber,@PlazaTotal as PlazaTotal,@CPlazaTotal as CPlazaTotal,");
		    sb.Append("\n 	   @STotal as STotal,@CSTotal as CSTotal,@ParkTotal as ParkTotal,@CParkTotal as CParkTotal,@AttTotal as AttTotal,@CAttTotal as CAttTotal,@GuardTotal as GuardTotal,@CGuardTotal as CGuardTotal,");
            sb.Append("\n 	   @ProduceTotal as ProduceTotal,@CProduceTotal as CProduceTotal,@OtherTotal as OtherTotal,@COtherTotal as COtherTotal,@CGTotal as CGTotal,@CTreeTotal as CTreeTotal");
    
            using (CommonDatabase cdb = new CommonDatabase(CommonDatabase.GetConnectionString(strKey)))
            {
                return cdb.ExecuteDataset(sb.ToString()).Tables[0];
            }
        }

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
            using (CommonDatabase cdb = new CommonDatabase())
            {
                return cdb.ExecuteDataset("pr_p_GetAreaForDepart", CommandType.StoredProcedure, spInputs).Tables[0];
            }

        }

        /// <summary>
        /// 过滤bgcode
        /// </summary>
        /// <param name="bgCode">bgcode字符串</param>
        /// <returns></returns>
        public static string BGCodeFormat(string bgCode)
        {
            List<string> alArea = new List<string>();
            List<string> alComm = new List<string>();
            List<string> alStreet = new List<string>();

            string[] arrStr = bgCode.Split(',');
            for (int i = 0; i < arrStr.Length; i++)
            {
                if (arrStr[i].Length == 6)
                {
                    alArea.Add(arrStr[i]);
                }
                else if (arrStr[i].Length == 9)
                {
                    alStreet.Add(arrStr[i]);
                }
                else if (arrStr[i].Length == 12)
                {
                    alComm.Add(arrStr[i]);
                }
            }
            for (int i = 0; i < alArea.Count; i++)
            {
                for (int j = 0; j < alStreet.Count; j++)
                {
                    for (int m = 0; m < alComm.Count; m++)
                    {
                        if (alComm[m].StartsWith(alStreet[j]))
                            alComm.RemoveAt(m);
                    }
                    if (alStreet[j].StartsWith(alArea[i]))
                        alStreet.RemoveAt(j);
                }
                for (int j = 0; j < alComm.Count; j++)
                {
                    if (alComm[j].StartsWith(alArea[i]))
                        alComm.RemoveAt(j);
                }
            }
            bgCode = "";
            for (int i = 0; i < alArea.Count; i++)
            {
                if (bgCode == "")
                    bgCode = alArea[i];
                else
                    bgCode += "," + alArea[i];
            }
            for (int i = 0; i < alStreet.Count; i++)
            {
                if (bgCode == "")
                    bgCode = alStreet[i];
                else
                    bgCode += "," + alStreet[i];
            }
            for (int i = 0; i < alComm.Count; i++)
            {
                if (bgCode == "")
                    bgCode = alComm[i];
                else
                    bgCode += "," + alComm[i];
            }

            return bgCode;
        }


    }
}
