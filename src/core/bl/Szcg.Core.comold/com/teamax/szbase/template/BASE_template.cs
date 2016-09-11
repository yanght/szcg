using System;
using System.Data;
using System.Data.SqlClient;
using szcg.com.teamax.util;
using System.Collections;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;


namespace szcg.com.teamax.szbase.template
{
	/// <summary>
	/// BASE_template 的摘要说明。
	/// </summary>
	public class BASE_template
	{
		protected static string[] str;
		protected static string sql;
		protected static SqlDataReader dr;
		protected static string[] colses1;
		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.template.BASE_template.";
		public BASE_template()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
           
		}
		/// <summary>
		/// 获取DataSet数据集,使用前请判断是否为空
		/// </summary>
		/// <returns></returns>
		public static DataSet getStatTreeData(int userId)
		{
			string sql="select * from searchtable";
			try
			{
				return DataAccess.ExecuteDataSet(sql,null);	
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getStatTreeData");
				throw;
			}

		}
		/// <summary>
		/// 更新modelcsstable表中的记录
		/// </summary>
		/// <param name="cellxy">设置DataGrid单元个的宽和高</param>
		/// <param name="headxy">设置DataGrid header的宽和高</param>
		/// <param name="forecolor">设置DataGrid前景色</param>
		/// <param name="backcolor">设置DataGrid背景色</param>
		/// <param name="fonsize">设置DataGrid中的字体的大小</param>
		/// <param name="fonname">设置Datagrid中的字体的名字</param>
		/// <param name="align">设置DataGrid中的排列方式</param>
		/// <param name="cols">设置DataGrid中的字段</param>
		/// <param name="id">查询模板类型标识</param>
		public  static void insertData(string colwidth,string rowheight,string cellrgb,string headrgb,string forecolor,string backcolor,string fonsize,string fonname,string align,int pagelines,int id,string cols,string tablename,int userId)
		{

			sql="select * from modelcsstable where fid='"+id+"' and cols='"+cols+"'";
			SqlDataReader sd=DataAccess.ExecuteReader(sql,null);
			if(sd.Read())
			{
			  excmodelcsstable(colwidth,rowheight,cellrgb,headrgb,forecolor,backcolor,fonsize,fonname,align,pagelines,id,cols,1,tablename,userId);
			}
			else
			{
			   excmodelcsstable(colwidth,rowheight,cellrgb,headrgb,forecolor,backcolor,fonsize,fonname,align,pagelines,id,cols,0,tablename,userId);
				//string sql="INSERT INTO modelcsstable(fid,cols,tables,colwidth,rowheight,cellrgb,headrgb,forecolor,backcolor,fonsize,fonname,alige,pagelines) values('"+id+"','"+cols+"','"+tablename+"','"+colwidth+"','"+rowheight+"','"+cellrgb+"','"+headrgb+"','"+forecolor+"','"+backcolor+"','"+fonsize+"','"+fonname+"','"+align+"','"+pagelines+"')";
                    
				//DataAccess.ExecuteNonQuery(sql,null);
			}
			sd.Close();
		}
		/// <summary>
		/// 执行更新modelcsstable表的函数
		/// </summary>
		/// <param name="cellxy"></param>
		/// <param name="headxy"></param>
		/// <param name="forecolor"></param>
		/// <param name="backcolor"></param>
		/// <param name="fonsize"></param>
		/// <param name="fonname"></param>
		/// <param name="align"></param>
		/// <param name="id"></param>
		/// <param name="cols"></param>
		/// <param name="i"></param>
		private static void excmodelcsstable(string colwidth,string rowheight,string cellrgb,string headrgb,string forecolor,string backcolor,string fonsize,string fonname,string align,int pagelines,int id,string cols,int i,string tables,int userId)
		{
			try
			{
				SqlParameter[] param=new SqlParameter[13];
				param[0]=new SqlParameter("@cols",SqlDbType.VarChar,256);
				param[0].Value=cols;
				param[1]=new SqlParameter("@colwidth",SqlDbType.VarChar,18);
				param[1].Value=colwidth;
				param[2]=new SqlParameter("@rowheight",SqlDbType.VarChar,18);
				param[2].Value=rowheight;
				param[3]=new SqlParameter("@cellrgb",SqlDbType.VarChar,18);
				param[3].Value=cellrgb;
				param[4]=new SqlParameter("@headrgb",SqlDbType.VarChar,18);
				param[4].Value=headrgb;
				param[5]=new SqlParameter("@forecolor",SqlDbType.VarChar,18);
				param[5].Value=forecolor;
				param[6]=new SqlParameter("@backcolor",SqlDbType.VarChar,18);
				param[6].Value=backcolor;
				param[7]=new SqlParameter("@fonsize",SqlDbType.VarChar,18);
				param[7].Value=fonsize;
				param[8]=new SqlParameter("@fonname",SqlDbType.VarChar,18);
				param[8].Value=fonname;
				param[9]=new SqlParameter("@alige",SqlDbType.VarChar,18);
				param[9].Value=align;
				param[10]=new SqlParameter("@pagelines",SqlDbType.Int,4);
				param[10].Value=pagelines;
				param[11]=new SqlParameter("@fid",SqlDbType.Int,4);
				param[11].Value=id;
				param[12]=new SqlParameter("@tables",SqlDbType.VarChar,128);
				param[12].Value=tables;
				if(i==1)
				{
					DataAccess.ExecuteStoredProcedure("template_update",param);

					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10104(),"template_update",NAMESPACE_PATH + "excmodelcsstable");
				}
				else
				{
					DataAccess.ExecuteStoredProcedure("template_insert",param);
					
					BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10104(),"template_insert",NAMESPACE_PATH + "excmodelcsstable");
				}
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
				    "Procedure:template_update",
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "excmodelcsstable");
				throw;
			}

		}
		/// <summary>
		/// 获取表名的数组
		/// </summary>
		/// <param name="str">要分解的字符串</param>
		/// <returns></returns>
		private static string[] slit(string str)
		{
			string[] str1=new string[1];
			if(str!=null && str.IndexOf(',')!=-1)
			{
				return str.Split(',');
			}
			else
			{
				if(str!=null)
				{
					str1[0]=str;
					return str1;
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// 把模板定义中所有的字段,以Hashtable表的形式返回
		/// </summary>
		/// <param name="id">用来区别选择的是那种模板定制</param>
		/// <returns></returns>
		public static Hashtable getProjectFieldData(string tablename,int userId)
		{
			try
			{
				Hashtable table=new Hashtable();
				str=slit(tablename);
				if(!str.Equals(""))
				{
					for(int i=0;i<str.Length;i++)
					{
						sql="select fields,fields_text from interpret_text_detail,interpret_text where interpret_text_detail.fid=interpret_text.id and interpret_text.tablename='"+str[i]+"'";
						dr=DataAccess.ExecuteReader(sql,null);
						while(dr!=null&&dr.Read())
						{
							string field=Convert.ToString(str[i]+"."+dr["fields"]);
							string text=Convert.ToString(dr["fields_text"]);
							table.Add(text,field);
						}
						dr.Close();
					}
				}
				else
				{
					return null;
				}
				return table;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getProjectFieldData");
				throw;
			}
		}

		/// <summary>
		/// 获取表中字段COLS列放入数组
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string[] getColsValus(int id,int userId)
		{
			try
			{
				sql= "select cols from modelcsstable where fid='"+id+"'";
				dr=DataAccess.ExecuteReader(sql,null);
				int i=0;
				int j=0;
				if(dr!=null)
				{
					while(dr.Read())
					{
						i=i+1;
					}
					dr.Close();
				
				}
				string[] colses=new string[i];
				if(i>0)
				{
					sql= "select cols from modelcsstable where fid='"+id+"'";
					dr=DataAccess.ExecuteReader(sql,null);
					while(dr.Read())
					{
						colses[j]=Convert.ToString(dr["cols"]);
						j=j+1;
					}
					dr.Close();
				}
				else
				{
					return null;
				}
				return colses;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getColsValus");
				throw;
			}

		}
		/// <summary>
		/// 获取上次设置的值
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Hashtable getSetValus(int id,int userId)
		{
			try
			{
				Hashtable table=new Hashtable();
				sql="select Top 1 * from modelcsstable where fid='"+id+"'";
				dr=DataAccess.ExecuteReader(sql,null);
				if(dr!=null)
				{
					while(dr.Read())
					{
						string cols=Convert.ToString(dr["cols"]);
						table.Add("cols",cols);
						string colwidth=Convert.ToString(dr["colwidth"]);
						table.Add("colwidth",colwidth);
						string rowheight=Convert.ToString(dr["rowheight"]);
						table.Add("rowheight",rowheight);
						string cellrgb=Convert.ToString(dr["cellrgb"]);
						table.Add("cellrgb",cellrgb);
						string headrgb=Convert.ToString(dr["headrgb"]);
						table.Add("headrgb",headrgb);
						string forecolor=Convert.ToString(dr["forecolor"]);
						table.Add("forecolor",forecolor);
						string backcolor=Convert.ToString(dr["backcolor"]);
						table.Add("backcolor",backcolor);
						string fonsize=Convert.ToString(dr["fonsize"]);
						table.Add("fonsize",fonsize);
						string fonname=Convert.ToString(dr["fonname"]);
						table.Add("fonname",fonname);
						string alige=Convert.ToString(dr["alige"]);
						table.Add("alige",alige);
						string pagelines=Convert.ToString(dr["pagelines"]);
						table.Add("pagelines",pagelines);
					}
						
				}

				else
				{
					return null;
				}
				return table;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getSetValus");
				throw;
			}


		}
		public static Hashtable gettable(int userId,int fid)
		{
			try
			{
				Hashtable table=new Hashtable();
				sql="select fields,fields_text from interpret_text_detail where fid="+fid;
				dr=DataAccess.ExecuteReader(sql,null);
				while(dr!=null&&dr.Read())
				{
					string fields=Convert.ToString(dr["fields"]);
					string fields_text=Convert.ToString(dr["fields_text"]);
					table.Add(fields,fields_text);
				}
				dr.Close();
				return table;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "gettable");
				throw;
			}

		}

		/// <summary>
		/// 获取表字段中文
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string getfields(string list2text,int userId)
		{
			try
			{
				string fields2="";
				sql="select fields from interpret_text_detail where fields_text='"+list2text+"'";
				dr=DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					fields2=Convert.ToString(dr["fields"]);
				}
				dr.Close();
				return fields2;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getfields");
				throw;
			}
		}

		/// <summary>
		/// 获取表中的行宽带
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string getrowheight(string colscon,int userId)
		{
			try
			{
				string rowheight2="";
				sql="SELECT rowheight FROM modelcsstable where cols='"+colscon+"'";
				dr=DataAccess.ExecuteReader(sql,null);
				while(dr.Read())
				{
					rowheight2=Convert.ToString(dr["rowheight"]);
				}
				dr.Close();
				return rowheight2;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "getfields");
				throw;
			}
		}

		/// <summary>
		/// 修改表中的行宽数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static void Updaterow(string colscon,string newrowheight,int userId)
		{
			try
			{
				sql="UPDATE modelcsstable set rowheight='"+newrowheight+"' where cols='"+colscon+"'";
				DataAccess.ExecuteNonQuery(sql,null);
				
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10104(),sql,NAMESPACE_PATH + "deleteData");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "Updaterow");
				throw;
			}

		}

		/// <summary>
		/// 删除未在选择字段中的数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static void deleteData(string cols1,int userId)
		{
			try
			{
				sql= "delete modelcsstable where cols='"+cols1+"'";
				DataAccess.ExecuteNonQuery(sql,null);
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10104(),sql,NAMESPACE_PATH + "deleteData");
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					sql,
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "deleteData");
				throw;
			}

		}
	}
}
