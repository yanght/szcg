using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.Search
{
    public class ObjProperty
    {
        public string key;
        public string name;
        public string type;
        public string value;
    }

    public class SearchProvider 
    {
        public const string strKey = "SdeConnString";

        public DateTime GetLastExecuteScheduledEventDateTime(string key, string serverName)
        {
            using (Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase())
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@Key", key);
                p[1] = new SqlParameter("@ServerName", serverName);
                p[2] = new SqlParameter("@LastExecuted", SqlDbType.DateTime, 8);
                p[2].Direction = ParameterDirection.Output;
                cdb.ExecuteNonQuery("GetLastExecuteScheduledEventDateTime", CommandType.StoredProcedure, p);
                return (DateTime)p[2].Value;
            } 
        }

        public void SetLastExecuteScheduledEventDateTime(string key, string serverName, DateTime dt)
        {
            using (Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase())
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@Key", key);
                p[1] = new SqlParameter("@ServerName", serverName);
                p[2] = new SqlParameter("@LastExecuted", dt);
                cdb.ExecuteNonQuery("SetLastExecuteScheduledEventDateTime", CommandType.StoredProcedure, p);
            }
        }

        #region GetObjectById：根据图层、部件编号来获得部件信息
        public DataTable GetObjectById(string id, string layer)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase(Teamax.Common.CommonDatabase.GetConnectionString(strKey));
            DataTable dt = cdb.ExecuteDataset("select * from sde." + layer + " where OBJCODE='" + id+"'").Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据图层、部件编号来获得部件信息
        /// </summary>
        /// <param name="IsHaveCode1">是否有ObjCode1字段</param>
        /// <param name="id">部件编号</param>
        /// <param name="layer">图层名称</param>
        /// <returns></returns>
        /// <remarks>2007-12-07,zmn重载该方法</remarks>
        public DataTable GetObjectById(bool IsHaveCode1,string id, string layer)
        {
            if (!IsHaveCode1)
                return GetObjectById(id, layer);
 
            string strConn = Teamax.Common.CommonDatabase.GetConnectionString(strKey);
            string strSQL = string.Format(@"select *
                                            from sde.{0}
                                            where ObjCode='{1}'
                                             or ObjCode1='{1}'",layer,id);

            using (Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase(strConn))
            {
                return cdb.ExecuteDataset(strSQL).Tables[0];
            }
        }
        #endregion

        public IDataReader GetIndexObject(string key)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase(Teamax.Common.CommonDatabase.GetConnectionString(strKey));
            IDataReader dr = cdb.ExecuteReader(getObjectDataSql());
            SetLastExecuteScheduledEventDateTime(key, Environment.MachineName, DateTime.Now.AddHours(1));
            return dr;
        }

        public DataTable GetIndexProject(string key)
        {
            DataTable table = null;
            DateTime LastCompleted = GetLastExecuteScheduledEventDateTime(key, Environment.MachineName);
            using (Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase())
            {
                SqlParameter[] p = { new SqlParameter("@Stepdate", LastCompleted) };
                table = cdb.ExecuteDataset("GetProjectForStepdate", CommandType.StoredProcedure, p).Tables[0];
                LastCompleted = DateTime.Now;
                SetLastExecuteScheduledEventDateTime(key, Environment.MachineName, LastCompleted);
                return table;
            }
        }


        private string getObjectDataSql()
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("\n select name as tablename into #tmpTableName from dbo.sysobjects where xtype='U' and name like '部件_宝安_%'");
            sb.Append("\n CREATE TABLE #SmallPart");
            sb.Append("\n (");
	        sb.Append("\n [key] varchar(24) null,");
            sb.Append("\n gridcode varchar(24) null,");
	        sb.Append("\n dateadded datetime null,");
	        sb.Append("\n tag varchar(100) null,");
	        sb.Append("\n datainfo varchar(500) null,");
	        sb.Append("\n title varchar(100)null");
            sb.Append("\n )");
            sb.Append("\n DECLARE @sql varchar(400)");
            sb.Append("\n DECLARE tables_cursor CURSOR FOR");
            sb.Append("\n select tablename from #tmpTableName where tablename like '部件_宝安_%'");
            sb.Append("\n OPEN tables_cursor");
            sb.Append("\n DECLARE @tablename varchar(40)");
            sb.Append("\n FETCH NEXT FROM tables_cursor INTO @tablename");
            sb.Append("\n WHILE (@@FETCH_STATUS <> -1)");
            sb.Append("\n BEGIN");
            sb.Append("\n set @sql='");
            sb.Append("\n insert into #SmallPart ");
            sb.Append("\n select OBJCODE as [key],isnull(BGCODE,'''') as gridcode,getdate() as dateadded ,OBJPOS as tag,");
            sb.Append("\n ''主管单位：''+isnull(OWNERNAME,'''') + ''      权属单位：'' + isnull(DEPTNAME,'''') as datainfo,'''+@tablename+''' as title");
            sb.Append("\n from sde.'+@tablename	");
            sb.Append("\n 	EXEC (@sql)");
            sb.Append("\n 	FETCH NEXT FROM tables_cursor INTO @tablename");
            sb.Append("\n END");
            sb.Append("\n CLOSE tables_cursor");
            sb.Append("\n DEALLOCATE tables_cursor");
            sb.Append("\n select * from #SmallPart");
            return sb.ToString();
        }

    }
}
