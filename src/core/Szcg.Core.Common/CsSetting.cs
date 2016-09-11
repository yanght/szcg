using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Teamax.Common
{
   public class CsSetting
    {
       //构造方法   
    static public  string getBackCs(string cs)
    {
        using (CommonDatabase MyDatabase = new CommonDatabase())
        {
            string sql = "select * from sys_configparmset where parmname='" + cs + "'";
           DataSet ds=MyDatabase.ExecuteDataset(sql);
           if (ds.Tables[0].Rows.Count > 0)
           {
               return ds.Tables[0].Rows[0]["parmvalue"].ToString();
           }
           else
           {
               return "";
           }
        }
        
    }
    }


}
