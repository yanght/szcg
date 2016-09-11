using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Teamax.Common;

namespace bacgBL.zhpj
{
    public class Appraise_patrol
    {
        public DataTable getPatrolList(String classid)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase();
            String sql = "SELECT * FROM a_appraise_patrol where classid={0}";
            sql = String.Format(sql, classid);
            DataSet ds = cdb.ExecuteDataset(sql);
            return ds.Tables[0];
        }

        public DataTable getPatrolDetailList(String classid)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase();
            String sql = "SELECT * FROM a_appraise_patrol_detail where classid={0}";
            sql = String.Format(sql, classid);
            DataSet ds = cdb.ExecuteDataset(sql);
            return ds.Tables[0];
        }

        public DataTable getMarkDetail(string id)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase();
            String sql = "SELECT * FROM a_appraise_patrol_mark where id={0}";
            sql = String.Format(sql, id);
            DataSet ds = cdb.ExecuteDataset(sql);
            return ds.Tables[0];
        }

        public DataTable getCodeList(string id)
        {
            Teamax.Common.CommonDatabase cdb = new Teamax.Common.CommonDatabase();
            String sql = "SELECT * FROM a_appraise_patrol_code where classid={0}";
            sql = String.Format(sql, id);
            DataSet ds = cdb.ExecuteDataset(sql);
            return ds.Tables[0];
        }
    }
}
