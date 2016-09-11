/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：公用数据访问-数据访问层

 * 结构组成：

 * 作    者：wcq
 * 创建日期：2007-05-29
 * 历史记录：

 * ****************************************************************************************
 * 修改人员：zmn               
 * 修改日期：2007-06-10 
 * 修改说明：添加事、部件访问方法   
 * ****************************************************************************************
 * ****************************************************************************************
 * 修改人员：wcq               
 * 修改日期：2007-06-10 
 * 修改说明：添加根据部门编码获取本部门和本部门的子部门的编码. 
 * ****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.Pub
{

    /// <summary>
    /// 
    /// </summary>
    public class Area
    {
        public string areaCode;
        public string areaName;
        public string areaPopulation;
        public string area;
    }

    public class Public : Teamax.Common.CommonDatabase
    {
        #region GetAreaInfo：根据传入的网格编码，获取改网格的区域信息

        /// <summary>
        /// 根据传入的网格编码，获取改网格的区域信息
        /// </summary>
        /// <param name="str">网格编码</param>
        /// <returns></returns>
        public static List<Area> GetAreaInfo(string str)
        {
            string[] arrStr = str.Split(',');
            StringBuilder sbArea = new StringBuilder();
            StringBuilder sbComm = new StringBuilder();
            StringBuilder sbStreet = new StringBuilder();
            for (int i = 0; i < arrStr.Length; i++)
            {
                if (arrStr[i].Length == 6)
                {
                    sbArea.Append(arrStr[i] + ",");
                }
                else if (arrStr[i].Length == 9)
                {
                    sbStreet.Append(arrStr[i] + ",");
                }
                else if (arrStr[i].Length == 12)
                {
                    sbComm.Append(arrStr[i] + ",");
                }
            }
            string strArea = sbArea.ToString();
            string strComm = sbComm.ToString();
            string strStreet = sbStreet.ToString();

            List<Area> listArea = new List<Area>();

            Teamax.Common.CommonDatabase commData = new Teamax.Common.CommonDatabase();
            //获取区的信息
            if (strArea.Length > 1)
            {
                strArea = strArea.Substring(0, strArea.Length - 1);
                string strAreaSQL = string.Format(@"select areacode,areaname,isnull(population,0) as population,
                                                        isnull(area,0) as area
                                                from s_area
                                                where areacode in({0})", strArea);
                DataSet dsArea = commData.ExecuteDataset(strAreaSQL);
                if (dsArea.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsArea.Tables[0].Rows)
                    {
                        Area area = new Area();
                        area.areaCode = dr["areacode"].ToString();
                        area.areaName = dr["areaname"].ToString();
                        area.area = dr["area"].ToString();
                        area.areaPopulation = dr["population"].ToString();
                        listArea.Add(area);
                    }
                }
            }
            //获取街道信息    
            if (strStreet.Length > 1)
            {
                strStreet = strStreet.Substring(0, strStreet.Length - 1);
                string strStreetSQL = string.Format(@"select streetcode,streetname,isnull(population,0) as population,
                                                        isnull(area,0) as area
                                                from s_street
                                                where streetcode in({0})", strStreet);
                DataSet dsStreet = commData.ExecuteDataset(strStreetSQL);
                if (dsStreet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsStreet.Tables[0].Rows)
                    {
                        Area area = new Area();
                        area.areaCode = dr["streetcode"].ToString();
                        area.areaName = dr["streetname"].ToString();
                        area.area = dr["area"].ToString();
                        area.areaPopulation = dr["population"].ToString();
                        listArea.Add(area);
                    }
                }
            }
            //获取社区信息
            if (strComm.Length > 1)
            {
                strComm = strComm.Substring(0, strComm.Length - 1);
                string strCommSQL = string.Format(@"select commcode,commname,isnull(population,0) as population,
                                                        isnull(area,0) as area
                                                  from s_community
                                                  where commcode in({0})", strComm);
                DataSet dsComm = commData.ExecuteDataset(strCommSQL);
                if (dsComm.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsComm.Tables[0].Rows)
                    {
                        Area area = new Area();
                        area.areaCode = dr["commcode"].ToString();
                        area.areaName = dr["commname"].ToString();
                        area.area = dr["area"].ToString();
                        area.areaPopulation = dr["population"].ToString();
                        listArea.Add(area);
                    }
                }
            }
            return listArea;
        }
        #endregion

        #region GetEventpartBigclass：获取事部件大类
        /// <summary>
        /// 获取事部件大类

        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>只进结果集流对象</returns>
        public IDataReader GetEventpartBigclass(string probclass)
        {
            string sql = "";
            if (probclass.Equals("0") || probclass.Equals("")) //部件大类表
            {
                sql = "select bigclassCode as code,name from s_bigclass_part ";
            }
            else if (probclass.Equals("1")) //事件大类表
            {
                sql = "select bigclassCode as code,name from s_bigclass_event ";
            }

            return this.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取事部件大类

        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>数据集对象</returns>
        public DataTable GetEventpartBigclassDT(string probclass)
        {
            string sql = "";
            if (probclass.Equals("0") || probclass.Equals("")) //部件大类表
            {
                sql = "select bigclassCode as code,name from s_bigclass_part ";
            }
            else if (probclass.Equals("1")) //事件大类表
            {
                sql = "select bigclassCode as code,name from s_bigclass_event ";
            }

            return this.ExecuteDataset(sql).Tables[0];
        }
        #endregion

        #region GetEventpartSmallclass：获取事部件小类
        /// <summary>
        /// 获取事部件小类

        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>只进结果集流对象</returns>
        public IDataReader GetEventpartSmallclass(string probclass)
        {
            string sql = "";
            if (probclass.Equals("0")) //部件小类表
            {
                sql = "select smallcallCode as code,name from s_smallclass_part ";
            }
            else if (probclass.Equals("1")) //事件小类表
            {
                sql = "select smallcallCode as code,name from s_smallclass_event ";
            }
            else if (probclass.Equals("")) //事、部件小类
            {
                sql = "select smallcallCode as code,name from s_smallclass_part union all select smallcallCode,name from s_smallclass_event ";
            }

            return this.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取事部件小类

        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>只进结果集流对象</returns>
        public DataTable GetEventpartSmallclassDT(string probclass)
        {
            string sql = "";
            if (probclass.Equals("0")) //部件小类表
            {
                sql = "select smallcallCode,name from s_smallclass_part";
            }
            else if (probclass.Equals("1")) //事件小类表
            {
                sql = "select smallcallCode,name from s_smallclass_event ";
            }
            else if (probclass.Equals("")) //事、部件小类
            {
                sql = "select smallcallCode,name from s_smallclass_part union all select smallcallCode,name from s_smallclass_event ";
            }

            return this.ExecuteDataset(sql).Tables[0];
        }
        #endregion


        /// <summary>
        /// 获取事部件小类

        /// </summary>
        /// <param name="probclass">类型</param>
        /// <returns>只进结果集流对象</returns>
        public DataTable hasGetEventpartSmallclassDT(string probclass)
        {
            string sql = "select * from (select smallcallCode,name,bigclassCode from s_smallclass_part union all select smallcallCode,name,bigclassCode from s_smallclass_event) a where a.bigclassCode='" + probclass + "'";
            return this.ExecuteDataset(sql).Tables[0];
        }


        #region GetSubDepartCode:根据部门编号获取部门和子部门的编号

        public string GetSubDepartCode(string departcode)
        {
            if (departcode == "0" || departcode == "null")
                return "";
            string strSQL = "";
            string retDepartCode = "";
            if (departcode.IndexOf(',') == -1)
            {
                strSQL = string.Format(@"select '{0}' as departcode
                                            union
                                         select departcode
                                         from p_depart
                                         where parentcode='{1}'", departcode, departcode);
                DataSet ds = this.ExecuteDataset(strSQL);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        retDepartCode += dr["departcode"].ToString() + ",";
                    }
                    if (retDepartCode.Length > 1)
                    {
                        retDepartCode = retDepartCode.Substring(0, retDepartCode.Length - 1);
                    }
                }
            }

            return retDepartCode;

        }
        #endregion
    }

    public class CameraService : Teamax.Common.CommonDatabase
    {
        public int AddCamera(string cameraName, string cameraNumber, string cameraLocation, string disabled)
        {
            string sqlstr = "insert into web_camera values(@cameraName,@cameraNumber,@cameraLocation,@disabled,getdate())";

            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@cameraName", cameraName),
                    new SqlParameter("@cameraNumber",cameraNumber),
                    new SqlParameter("@cameraLocation",cameraLocation),
                    new SqlParameter("@disabled",disabled)
                };

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);
        }
        public int EditCamera(string id, string cameraName, string cameraNumber, string cameraLocation, string disabled)
        {
            //string sqlstr = "insert into web_camera values(@cameraName,@cameraNumber,@cameraLocation,@disabled,getdate())";

            string sqlstr = "update  web_camera set cameraName=@cameraName,cameraNumber=@cameraNumber,cameraLocation=@cameraLocation,disabled=@disabled,Edittime=getdate() where id=@id";

            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@id", id),
                    new SqlParameter("@cameraName", cameraName),
                    new SqlParameter("@cameraNumber",cameraNumber),
                    new SqlParameter("@cameraLocation",cameraLocation),
                    new SqlParameter("@disabled",disabled)
                };

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);
        }
        public DataTable GetCameraList()
        {
            string sqlstr = "select * from web_camera order by edittime desc";
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetCamera(string id)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@id", id)
                };
            string sqlstr = "select * from web_camera where id=@id";
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, spInputs);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public int DeleteCamera(string id)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@id", id)
                };
            string sqlstr = "delete  from web_camera where id=@id";
            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);

        }

        public int AddLed(string ledName,string ledIp,string ledStatus,string parkName,string location,string userName,string message)
        {
            string sqlstr = "insert into web_led values(@LedName,@LedIp,@LedStatus,@ParkName,@Location,getdate(),@ModifyUser,@Message)";

            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@LedName", ledName),
                    new SqlParameter("@LedIp",ledIp),
                    new SqlParameter("@LedStatus",ledStatus),
                    new SqlParameter("@ParkName",parkName),
                    new SqlParameter("@Location",location),
                    new SqlParameter("@ModifyUser",userName),
                    new SqlParameter("@Message",message),
                };

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);
        }
        
        
        public DataTable GetLedList()
        {

            string sqlstr = "select * from web_led order by id desc";
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

         public DataTable GetLed(string id)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@id", id)
                };
            string sqlstr = "select * from web_led where id=@id";
            DataSet ds = this.ExecuteDataset(sqlstr, CommandType.Text, spInputs);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public int DeleteLed(string id)
        {
            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                    new SqlParameter("@id", id)
                };
            string sqlstr = "delete  from web_led where id=@id";
            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);

        }

        public int EditLed(string id, string ledName, string ledIp, string ledStatus, string parkName, string location, string userName,string message)
        {
            //string sqlstr = "insert into web_camera values(@cameraName,@cameraNumber,@cameraLocation,@disabled,getdate())";

            string sqlstr = "update  web_led set LedName=@LedName,LedIp=@LedIp,LedStatus=@LedStatus,ParkName=@ParkName,Location=@Location,ModifyUser=@ModifyUser,ModifyTime=getdate() ,Message=@Message where id=@id";

            SqlParameter[] spInputs = new SqlParameter[]
                {                    
                     new SqlParameter("@Id", id),
                    new SqlParameter("@LedName", ledName),
                    new SqlParameter("@LedIp",ledIp),
                    new SqlParameter("@LedStatus",ledStatus),
                    new SqlParameter("@ParkName",parkName),
                    new SqlParameter("@Location",location),
                    new SqlParameter("@ModifyUser",userName),
                    new SqlParameter("@Message",message),
                };

            return this.ExecuteNonQuery(sqlstr, CommandType.Text, spInputs);
        }
    }


}
