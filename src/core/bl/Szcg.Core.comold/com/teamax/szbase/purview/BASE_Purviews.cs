using System;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using szcg.com.teamax.util;
using System.Data;
using szcg.com.teamax.szbase.systemsetting.logmanage;
using szcg.web.szbase.systemsetting.logmanage;

namespace szcg.com.teamax.szbase.purview
{
	/// <summary>
	/// BASE_Purview ����Ҫ�����Ȩ������ģ����еĴ����ࡣ
	/// </summary>
	public class BASE_Purviews
	{
		//�õ����ݿ��еĽ����
		protected SqlDataReader dr;
		//�������
		protected StringBuilder sb;
		//�����������
		protected ArrayList list;

		private const string NAMESPACE_PATH = "szcg.com.teamax.szbase.purview.BASE_Purviews.";
		
		//���캯������ʼ�����ݿ������
		public BASE_Purviews()
		{
			
		}

		//ȡ�����ݿ����ݣ�����TreeView��
		public String[] getPurviewTreeData(int userId,string areacode)
		{
			string sql = "select rolecode,rolename from role";
			if(!areacode.Equals("4403"))
			{
				sql="select rolecode,rolename from role where areacode='"+areacode+"'";
			}
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
			
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToDecimal(dr["rolecode"])+",");
					sb.Append(Convert.ToString(dr["rolename"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
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
					NAMESPACE_PATH + "getPurviewTreeData");
				throw;
			}
		}

		/// <summary>
		/// �����û�����ȡ���û����еĽ�ɫ
		/// </summary>
		/// <param name="strUserCode">�û�����</param>
		/// <returns></returns>
		public String[] getPurviewTreeData(string strUserCode,int userId)
		{
			string sql = "select a.rolecode,a.rolename from role a,user_role b where a.rolecode=b.rolecode and b.usercode=" + strUserCode;
			dr = DataAccess.ExecuteReader(sql,null);
			try
			{
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToDecimal(dr["rolecode"])+",");
					sb.Append(Convert.ToString(dr["rolename"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
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
					NAMESPACE_PATH + "getPurviewTreeData");
				throw;
			}
		}

		//�����û�ID��ģ��IDȥ��ѯ���Ƿ��Ѿ�������Ȩ��
		public string getPurviewIsvisible(string rolecode,string modelcode,int userId)
		{
			string sql = "select isvisible from role_model where rolecode = '"+rolecode+"' and system_model_fid = '"+modelcode+"'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				string str="";
				while(dr.Read())
				{
					str = Convert.ToString(dr["isvisible"]);
				}
				dr.Close();
				return str;
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
					NAMESPACE_PATH + "getPurviewIsvisible");
				throw;
			}
		}

		//ɾ������Աȡ����Ȩ��
		public void deletePurview(string rolecode,string modelcode,int userId)
		{
			string sql = "delete from role_model where rolecode = '"+rolecode+"' and system_model_fid = '"+modelcode+"'";
			try
			{
				DataAccess.ExecuteNonQuery(sql,null);
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
					NAMESPACE_PATH + "deletePurview");
				throw;
			}
			
		}

		//���ݽ�ɫIDȡ��ģ��ID�������ã���Ӧ��Ȩ��CheckBox��CheckBox.Checked=true
		public ArrayList getPruviewModleCode(string id,int userId)
		{
			string sql = "select system_model_fid from role_model where rolecode = '"+id+"' and isvisible = '1'";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				list = new ArrayList();
				string modelcode = "";
				while(dr.Read())
				{
					modelcode = Convert.ToString(dr["system_model_fid"]);
					list.Add(modelcode);
				}
				dr.Close();
				return list;
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
					NAMESPACE_PATH + "getPruviewModleCode");
				throw;
			}
		}
		
		
		//ȡ�����е�ģ�����ƣ����뵽�����棬��������Ȩ����
		public String[] getModelName(int userId)
		{
			string sql = "select * from system_model";
			try
			{
				dr = DataAccess.ExecuteReader(sql,null);
				list = new ArrayList();
				while(dr.Read())
				{
					sb = new StringBuilder();
					sb.Append(Convert.ToDecimal(dr["parentcode"])+",");
					sb.Append(Convert.ToDecimal(dr["id"])+",");
					sb.Append(Convert.ToString(dr["modelname"]));
					list.Add(sb.ToString());
				}
				dr.Close();
				return (String[])(list.ToArray(System.Type.GetType("System.String")));
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
					NAMESPACE_PATH + "getModelName");
				throw;
			}
		}

		//����Ȩ��
		public string savePurview(string usercode,string str,int userId)
		{
			try
			{
				System.Diagnostics.Debug.WriteLine(System.DateTime.Now);
				SqlParameter[] input = new SqlParameter[2];
				SqlParameter[] output= new SqlParameter[1];
				input[0] = new SqlParameter("@RoleID",SqlDbType.Int);
				input[0].Value=Convert.ToInt32(usercode);
				input[0].Direction=ParameterDirection.Input;
				input[1] = new SqlParameter("@RoleStr",SqlDbType.NVarChar,800);
				input[1].Value=str;
				input[1].Direction=ParameterDirection.Input;
				output[0] = new SqlParameter("@result",SqlDbType.Char,1);
				output[0].Direction=ParameterDirection.Output;
				System.Diagnostics.Debug.WriteLine(System.DateTime.Now);
				string c =Convert.ToString(DataAccess.ExecuteStoreProcedure1("EditRole",input,output));
			
				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10102(),"EditRole","szcg.com.teamax.szbase.purview.BASE_Purviews.savePurview");

				return c;
			}
			catch(Exception ex)
			{
				BASE_logmanageservice.writeSystemLog(
					userId,
					"Procedure:EditRole",
					System.DateTime.Now,
					System.DateTime.Now, 
					BASE_ModerId.getSystem_ZCPT(),
					ex.ToString(),
					NAMESPACE_PATH + "savePurview");
				throw;
			}
		}

		//�����úõ�Ȩ��д�����ݿ�
		public void insertSystemModel(string rolecode,string modelcode,string islogin)
		{

			string sql = "insert into role_model(rolecode,system_model_fid,isvisible) values('"+rolecode+"','"+modelcode+"','"+islogin+"')";
			DataAccess.ExecuteNonQuery(sql,null);
		}

        //�����û�IDȡ�������������н�ɫ��Ϣ��
		public ArrayList getRoleInfoByUser(int userCode,int userId)
		{
			string sql="select a.rolecode rolecode,a.rolename rolename from role a , user_role b where a.rolecode = b.rolecode and b.usercode="+userCode;
			try
			{
				ArrayList array=new ArrayList();
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					string[] role=new string[2];
					role[0]=rs["rolecode"].ToString();
					role[1]=rs["rolename"].ToString();
					array.Add(role);
				}
				rs.Close();
				return array;
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
					NAMESPACE_PATH + "getRoleInfoByUser");
				throw;
			}

		}

		public string getAccreditedRoleID(string userCode,int userId)
		{
			string sql="select modecode from accredited where user_to='"+userCode+"'";
			try
			{
				StringBuilder ids=new StringBuilder();
				SqlDataReader rs=DataAccess.ExecuteReader(sql,null);
				while(rs.Read())
				{
					ids.Append(rs["modecode"].ToString()+",");
				}
				rs.Close();
				if(ids.Length>1)
				{
					return ids.ToString().Substring(0,ids.Length-1);

				}
				return "";
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
					NAMESPACE_PATH + "getAccreditedRoleID");
				throw;
			}
		}

		public int insertToAccredited(string[] values,int userId)
		{
			string sql="insertToAccredited";
			try
			{
				SqlParameter[] input=new SqlParameter[6];
				input[0]=new SqlParameter("@fid",SqlDbType.Int);
				input[0].Value=Convert.ToInt32(values[0]);
				input[0].Direction=ParameterDirection.Input;
				input[1]=new SqlParameter("@user_go",SqlDbType.Int);
				input[1].Value=Convert.ToInt32(values[1]);
				input[1].Direction=ParameterDirection.Input;
				input[2]=new SqlParameter("@user_to",SqlDbType.Int);
				input[2].Value=Convert.ToInt32(values[2]);
				input[2].Direction=ParameterDirection.Input;
				input[3]=new SqlParameter("@cu_date",SqlDbType.DateTime);
				input[3].Value=DateTime.Now;
				input[3].Direction=ParameterDirection.Input;
				input[4]=new SqlParameter("@memo",SqlDbType.VarChar,255);
				input[4].Value="";
				input[4].Direction=ParameterDirection.Input;
				input[5]=new SqlParameter("@ids",SqlDbType.VarChar,500);
				input[5].Value=values[3];
				input[5].Direction=ParameterDirection.Input;

				SqlParameter[] output=new SqlParameter[1];
				output[0]=new SqlParameter("@result",SqlDbType.Int);
				output[0].Direction=ParameterDirection.Output;

				int i=Convert.ToInt32(DataAccess.ExecuteStoreProcedure1(sql,input,output));

				BASE_logmanageservice.writeUserLog(userId,DateTime.Now,DateTime.Now,BASE_ModerId.getModel_10102(),sql,"szcg.com.teamax.szbase.purview.BASE_Purviews.insertToAccredited");

				return i;
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
					NAMESPACE_PATH + "insertToAccredited");
				throw;
			}
		
			
		}


	}
}
