using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Teamax.Common;
namespace bacgBL.environment.CarManager
{
    public class BASE_carmanager : CommonDatabase
    {
        public ArrayList getGroup(string area)
        {
            ArrayList groups = new ArrayList();
            string sql = "select CarGroupId,Name from CarGroupInfo where Area like '"+area+"%'";
            string connectionString = CommonDatabase.GetConnectionString("CONN_GPS_STRING");
            CommonDatabase commondatabase = new CommonDatabase(connectionString);
            SqlDataReader ds = (SqlDataReader)commondatabase.ExecuteReader(sql);
            try
            {
                while (ds.Read())
                {
                    string[] group = new string[2];
                    group[0] = ds["CarGroupId"].ToString();
                    group[1] = ds["Name"].ToString();
                    groups.Add(group);
                }
                ds.Close();
                return groups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ArrayList getDriverId(string departid)
        {
            ArrayList array = new ArrayList();
            string sql = "select driverid from DriverInfo where   departcode =" + Convert.ToInt32(departid) ;
            try
            {
                string connection = CommonDatabase.GetConnectionString("CONN_GPS_STRING");
                CommonDatabase commondatabase = new CommonDatabase(connection);
                SqlDataReader ds = (SqlDataReader)commondatabase.ExecuteReader(sql);
                while (ds.Read())
                {
                    string[] carid = new string[1];
                    carid[0] = ds["driverid"].ToString();
                    array.Add(carid);
                }
                ds.Close();
                return array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
