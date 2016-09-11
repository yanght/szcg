using System;
using System.Collections;
using System.Data.SqlClient;
using bacgDL.environment.entitys;
using Teamax.Common;
using bacgDL.environment.sysset;
using System.Data;


namespace bacgBL.environment.sysset
{
    public class Base_Codedicthelper
    {
        CodedictDao Dao1 = new CodedictDao();
        public Base_Codedicthelper()
        {
            //
        }

        //得到代码类别列表或者某一类别的代码值

        public PageManage GetAllCodeList(codedictcls cls1, int pageIndex, int pageSize)
        {
            try
            {
                PageManage page = Dao1.GetAllcodedictList(cls1, pageIndex, pageSize);
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增一条代码类别或者某一类别的代码值

        public int insertIntocodedict(codedictcls cls1)
        {
            try
            {
                int i = Dao1.insertIntocodedict(cls1);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //更新一条代码类别或者某一类别的代码值

        public int updateIntocodedict(codedictcls cls1)
        {
            try
            {
                int i = Dao1.updateIntocodedict(cls1);
                return i;
            }
            catch
            {
                throw;
            }
        }

        //删除一条代码类别或者某一类别的代码值

        public int deleteFromcodedict(codedictcls cls1)
        {
            try
            {
                int i = Dao1.deleteFromcodedict(cls1);
                return i;
            }
            catch
            {
                throw;
            }

        }
 

        //根据主键patrolid得到该巡查信息

        public codedictcls getPatrolInfoByID(codedictcls cls1)
        {
            try
            {
                SqlDataReader rs = (SqlDataReader)Dao1.GetCodeInfoByID(cls1);
                codedictcls per = new codedictcls();
                while (rs.Read())
                {
                    per.codetype = cls1.codetype;
                    per.codeid = cls1.codeid;
                    per.codename = rs["codename"].ToString();
                    per.inputcode = rs["inputcode"].ToString();
                    per.standardcode = rs["standardcode"].ToString();
                    per.status = rs["status"].ToString();
                    per.systemid =Convert.ToInt32(rs["systemid"]);
                   
                }
                return per;
            }
            catch
            {
                throw;
            }
        }

        public DataTable getMaxCodeid(int codetype)
        {
            try
            {
                DataTable dt = Dao1.getMaxCodeid(codetype);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable getMaxCodeType()
        {
            try
            {
                DataTable dt = Dao1.getMaxCodeType();
                return dt;
            }
            catch
            {
                throw;
            }
        }
    }
}
