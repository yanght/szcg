using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace bacgBL.Pub
{
    public class TxMsgClass : Teamax.Common.CommonDatabase
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>       
        public static int ShortMessage(string Phone, string Content)
        {
            int strResult = 1;
            try
            {
                using (Teamax.Common.CommonDatabase comm = new Teamax.Common.CommonDatabase())
                {
                    SqlParameter[] parameters = {
					new SqlParameter("@DestAddr", Phone),
					new SqlParameter("@SM_Content",Content)};
                    int row = comm.ExecuteNonQuery("msg_SendMessage", CommandType.StoredProcedure, parameters);

                    if (row > 0)
                        strResult = 0;
                    else
                        strResult = 1;
                }
            }
            catch (Exception ex)
            {
                strResult = 1;
            }
            return strResult;
        }
    }
}
