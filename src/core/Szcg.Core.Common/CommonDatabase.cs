/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;�����ݿ�����ࡣ��ADO.NET���м򵥷�װ������ʹ�á�
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Teamax.Common.Loger;

namespace Teamax.Common
{
	/// <summary>
	/// ���ݿ�����ࡣ��ADO.NET���м򵥷�װ������ʹ�á�
	///	</summary>
	///	<remarks>
	/// ׼��������ConnectionString��ǰд��Web.Config�ļ��У�������ʾ��
	///<code>
	///		<appSettings>
    ///			<add key="ConnectionString" value="server=192.168.0.250\testdb; user id=sa; password=123; database=gps;" />
	///		</appSettings>
	///	</code>
	///	</remarks>
	///	<example>
	///	<code>
	///	//1.һ�����ݷ��ʲ���ʾ���������¡�
	///	using System;
	///	using System.Data;
	///	using System.Data.SqlClient;
	///	using Teamax.Common;  //���������ռ�
	///
	///	namespace BacgDAL.Proj
	///	{
    ///		public class ProjDA:Teamax.Common.CommonDatabase
	///		{
    ///			public ProjDA()
	///			{
    /// 
	///			}
	///
    ///			#region GetProj����ȡ������Ϣ
    ///			public DataSet GetProj(string projcode)
    ///			{
    ///				string strSQL = string.Format(@"��SELECT * FROM b_project WHERE projcode='{0}'��",projcode);
    ///				return ExecuteDataset(strSQL,"b_project");
    ///			}
    ///			#endregion
    /// 
    ///			#region GetProjAndTrace����ȡ������Ϣ�Ͱ�������
    ///			public DataSet GetProjAndTrace(string projcode)
	///			{
    ///				string strSQL = string.Format(@"��SELECT * FROM b_project WHERE projcode='{0}'��",projcode);
    ///				DataSet dsProject = ExecuteDataset(strSQL,"b_project");
	///
	///				strSQL = string.Format(@"	SELECT 	*
    ///									        FROM b_project_trace
    ///									        WHERE projcode='{0}'",projcode);
    ///				LoadDataSet(dsProject,strSQL,"b_project_trace");
    /// 
    ///				return dsProject;
	///			}
	///			#endregion
	///
	///		}
	///	 }
	///   
	/// </code>
	/// </example> 
	public class CommonDatabase:IDisposable
	{
		#region �������ֶα���
		private IDbTransaction _Trans;
		private IDbConnection _Conn;
		private IDbCommand _Command;
		private IDbDataAdapter _Adapter;
		private DataSet _Dataset;
		private IDataReader _Reader;
		private bool IsDisposed = false;
        private bool IsCanCloseConn = true;
        public int _CommandTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"]);
        

		/// <summary>
		/// ���ݿ������ַ���
		/// </summary>
        private static string _ConnectionString = "";

		/// <summary>
		/// �����ַ�����ֱ�Ӵ������ļ��ж�ȡ
		/// </summary>
		public static string ConnectionString
		{
			get 
			{
                return _ConnectionString == "" ? ConfigurationManager.AppSettings["ConnString"] : _ConnectionString;
			}
			set
			{
				_ConnectionString = value;
			}
		}

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
		#endregion

        #region һЩ�ڲ�ʹ�õķ���
        #region AttachParameters����SqlParameter[]������ӵ�SqlCommand
        /// <summary>
        /// ��SqlParameter[]������ӵ�SqlCommand
        /// </summary>
        /// <param name="command">ADO.net�е�SqlCommand</param>
        /// <param name="commandParameters">SqlParameter[]����</param>
        private void AttachParameters(IDbCommand command, IDataParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command����Ϊ��");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        //�жϲ���ֵ�Ƿ�Ϊ��
                        if ( (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) 
                            && (p.Value == null) )
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion

        #region PrepareCommand������SqlCommand
        /// <summary>
        /// ����SqlCommand
        /// </summary>
        /// <param name="commandText">SELECT��䣺SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="commandParameters">����</param>
        private void PrepareCommand(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (IsDisposed)
                throw new ObjectDisposedException("CommonDatabase", "�����Ѿ��ݻ٣��޷���ʹ��");

            if (_Conn.State != ConnectionState.Open)
                _Conn.Open();

            _Command = new SqlCommand(commandText, (SqlConnection)_Conn);
            _Command.CommandType = commandType;
            _Command.CommandTimeout = _CommandTimeOut;

            if (commandParameters != null)
                AttachParameters(_Command, commandParameters);

            if (this._Trans != null)
            {
                if (_Trans.Connection == null)
                    throw new ObjectDisposedException("_Trans","�������Ϊ�գ����ṩһ���򿪵�SqlTransaction");
                _Command.Transaction = _Trans;
            }
        }
        #endregion
        #endregion

		#region ���캯��
		/// <summary>
		/// ���캯����
		/// ���ݿ������ַ���Ĭ�ϴ������ļ���ȡ
		/// </summary>
		public CommonDatabase():this("")
        {
		}

		/// <summary>
		/// ���캯����
		/// �������ݿ������ַ�����
		/// </summary>
        /// <param name="strConnStr">���ݿ������ַ���</param>
		public CommonDatabase(string strConnStr)
		{
            if (strConnStr == "")
                strConnStr = ConnectionString;

            _Conn = new SqlConnection(strConnStr); 
			_Conn.Open();
		}

		#endregion

		#region ��������
		/// <summary>
		/// ����ʹ����.NET�Ƽ���Disposeģʽ���ͷ�Connection��Դ����ص�Command,DataAdapter����
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// �ͷ�Connection��Դ����ص�Command,DataAdapter����
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if(!this.IsDisposed)
			{
				if(disposing)
				{
					// �ͷ��й���Դ
					//_Conn.Close();
					_Conn.Dispose();
					if (_Trans!=null)
						_Trans.Dispose();
					if (_Command!=null)
						_Command.Dispose();
					//��ΪDataAdapter��û��Dispose�������Բ��ͷ�
					//_Adapter
					//��Ϊ����ĵ����߻���Ҫʹ��Dataset�����Բ��ͷ�
					//_Dataset;
					//��Ϊ����ĵ��û���Ҫʹ��DataReader,���Բ��ͷ�
					//_Reader;
				}
				// �ͷŷ��й���Դ
				// only the following code is executed.
				//CloseHandle(handle);
				//handle = IntPtr.Zero;
			}
			IsDisposed = true;
		}

		/// <summary>
		/// ��������
		/// </summary>
		~CommonDatabase()      
		{
			Dispose(false);
		}
		#endregion

        #region ExecuteDataset��ִ��SQL��䣨SQL �ı�������ߴ洢���̣���SQL����п��԰�������������DataSet��
        /// <summary>
        /// ִ��SQL �ı��������DataSet��
		/// </summary>
        /// <param name="commandText">SQL �ı������Ĭ�ϣ�</param>
        /// <returns>���ظ���SQL������ɵ�DataSet������ֻ����һ��DataTable����</returns>
		/// <remarks>
		/// <code>
		///   using (CommonDatabase MyDatabase = new CommonDatabase())
		///   {
        ///	        DataSet ds = MyDatabase.ExecuteDataset("SELECT * FROM b_project")		
		///   }
		/// </code>
        /// <seealso cref="ExecuteDataset(string, IDataParameter[] )"/>
		/// </remarks>
		public DataSet ExecuteDataset(string commandText)
		{
            return ExecuteDataset(commandText, CommandType.Text);
		}
        
        /// <summary>
        /// ִ��SQL��䣨SQL �ı�������ߴ洢���̣����������������DataSet��
        /// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>���ظ���SQL������ɵ�DataSet������ֻ����һ��DataTable����</returns>
        public DataSet ExecuteDataset(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteDataset(commandText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// ִ��SQL��䣨SQL �ı�������ߴ洢���̣����������������DataSet��
        /// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>���ظ���SQL������ɵ�DataSet������ֻ����һ��DataTable����</returns>
        public DataSet ExecuteDataset(string commandText, CommandType commandType, params IDataParameter[] commandParameters)
        {
            return ExecuteDataset(commandText, commandType, 0, commandParameters);
        }

        /// <summary>
        ///  ִ��SQL��䷵��DataSet������DataSet�еı��������SQL����п��԰���������
        /// </summary>
        /// <param name="commandText">SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="commandParameters">��������ѡ��</param>
        /// <returns>DataSet������ֻ����һ��DataTable����</returns>
        /// <remarks>
        /// ע�⣺
        ///     �������commandTextΪ�洢���̣�commandTypeӦ��ΪCommandType.StoredProcedure
        /// </remarks>
        public DataSet ExecuteDataset(string commandText,CommandType commandType,int commandTimeOut,params IDataParameter[] commandParameters)
        {
            try
            {
                this.PrepareCommand(commandType, commandText, commandParameters);
                if (commandTimeOut != 0)
                    _Command.CommandTimeout = commandTimeOut;
                _Adapter = new SqlDataAdapter((SqlCommand)_Command);
                _Dataset = new DataSet();
                _Adapter.Fill(_Dataset);
                if(IsCanCloseConn)
                    _Command.Connection.Close();
                LogerForDB.DEBUG(new MyLog(commandText));
                return _Dataset;
            }
            catch (Exception e)
            {
                LogerForDB.ERROR(new MyLog(commandText, e.Message));
                throw e;
            }
        }		
		#endregion

        #region ExecuteReader��ִ��SQL��䣨SQL �ı�������ߴ洢���̣���SQL����п��԰�������������DataReader����
        /// <summary>
        /// ִ��SQL �ı��������DataReader
		/// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <returns>����DataReader����</returns>
        /// <remarks>
        /// <code>
        /// public IDataReader GetProject(string strCode)
        /// {
        ///		CommonDatabase MyDatabase = new CommonDatabase()
        ///		string strSQL=string.Format(@"	SELECT * FROM b_Project
        ///										WHERE projcode='{0}'",strCode);
        ///		return MyDatabase.ExecuteReader(strSQL);
        /// }  
        /// </code>
        /// </remarks>
		public IDataReader ExecuteReader(string commandText)
		{
            return ExecuteReader(commandText, CommandType.Text, true);
		}

        /// <summary>
        /// ִ��SQL��䣨SQL �ı�������ߴ洢���̣�������DataReader����
        /// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>����DataReader����</returns>
        public IDataReader ExecuteReader(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteReader(commandText, CommandType.Text, true, commandParameters);
        }

        /// <summary>
        /// ִ��SQL��䣨SQL �ı�������ߴ洢���̣���SQL����п��԰�������������DataReader����
        /// </summary>
        /// <param name="commandText">SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="bCanClose">�Ƿ��ܹ��Զ��ر�Connection �����棺����رչ����� DataReader ����������� Connection ����Ҳ���ر�</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>����DataReader����</returns>
        /// <remarks>
        /// ע�⣺
        ///     �������commandTextΪ�洢���̣�commandTypeӦ��ΪCommandType.StoredProcedure
        /// </remarks>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, bool bCanClose, params IDataParameter[] commandParameters)
        {
            try
            {
                this.PrepareCommand(commandType, commandText, commandParameters);
                if(bCanClose)
                    _Reader = _Command.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    _Reader= _Command.ExecuteReader();

                LogerForDB.DEBUG(new MyLog(commandText));
                return _Reader;
            }
            catch (Exception e)
            {
                LogerForDB.ERROR(new MyLog(commandText, e.Message));
                throw e;
            }
        }		
		#endregion

        #region ExecuteNonQuery��������ִ�� Transact-SQL ��䣨SQL �ı�������ߴ洢���̣���������Ӱ���������
        /// <summary>
        /// ִ��SQL �ı����������Ӱ�������
		/// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <returns>������Ӱ�������</returns>
		/// <example>
		/// <code>
        /// ʾ��1��
		///   using (CommonDatabase MyDatabase = new CommonDatabase())
		///   {
        ///			MyDatabase.ExecuteNonQuery("DELETE FROM b_project WHERE b_project='001'");
		///   }
        /// 
        /// ʾ��2��
        /// using (Teamax.Common.CommonDatabase myDatabase = new Teamax.Common.CommonDatabase())
        /// {                       
        ///     string sql = "insert into tab1(a,b)  values(@aa,@bb)";
        ///	    SqlParameter spAA = new SqlParameter("@aa","���»�");
        ///	    SqlParameter spBB = new SqlParameter("@bb","�ݳ�");
        ///	    myDatabase.ExecuteNonQuery(sql,CommandType.Text,spAA,spBB)
        /// }
        /// </code>
		/// </example>
		public int ExecuteNonQuery(string commandText)
		{
            return ExecuteNonQuery(commandText, CommandType.Text);
		}

        /// <summary>
        ///  ִ��SQL �ı����������Ӱ���������֧��SQL����еĲ�����
        /// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>������Ӱ�������</returns>
        public int ExecuteNonQuery(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, true, commandParameters);
        }

		/// <summary>
        /// ������ִ�� Transact-SQL ��䣨SQL �ı�������ߴ洢���̣���������Ӱ���������֧��SQL����еĲ���
		/// </summary>
        /// <param name="commandText">SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>������Ӱ�������</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params IDataParameter[] commandParameters)
		{
            return ExecuteNonQuery(commandText, commandType, true, commandParameters);
		}

        /// <summary>
        /// ������ִ�� Transact-SQL ��䣨SQL �ı�������ߴ洢���̣���������Ӱ���������֧��SQL����еĲ���
        /// </summary>
        /// <param name="commandText">SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="IsWriteLog">�Ƿ�д��־������ǿ�Ʋ�д��־�����磺ִ��дϵͳ��־�ķ���ʱ�����Բ�д��־����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>������Ӱ�������</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, bool IsWriteLog, params IDataParameter[] commandParameters)
        {
            try
            {
                this.PrepareCommand(commandType, commandText, commandParameters);
                _Command.Transaction = _Trans;
                int i = _Command.ExecuteNonQuery();
                if (IsCanCloseConn)
                    _Command.Connection.Close();
                if (IsWriteLog)
                    LogerForDB.INFO(new MyLog(commandText));

                return i;
            }
            catch (Exception e)
            {
                if (IsWriteLog)
                    LogerForDB.ERROR(new MyLog(commandText, e.Message));
                throw e;
            }
        }
		#endregion

        #region ExecuteScalar��ִ��Transact-SQL ��䣨SQL �ı�������ߴ洢���̣����ص�һ�е�һ�е�ֵ
        /// <summary>
        /// ִ��SQL �ı�������ص�һ�е�һ�е�ֵ
		/// </summary>
        /// <param name="commandText">SQL �ı�����</param>
		/// <returns>��һ�е�һ�е�ֵ</returns>
		/// <remarks>
		/// <code>
        /// public object GetProject(string strCode)
		/// {
		///		using (CommonDatabase MyDatabase = new CommonDatabase())
		///		{
        ///			string strSQL=string.Format(@"	SELECT stepid FROM project
        ///											WHERE projcode='{0}'",strCode);
		///			return MyDatabase.ExecuteScalar(strSQL);
		///		}
		/// }
		/// </code>
		/// </remarks>
		public object ExecuteScalar(string commandText)
		{
            return ExecuteScalar(commandText, CommandType.Text);
		}

        /// <summary>
        /// ִ��SQL �ı�������ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="commandText">SQL �ı�����</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>������Ӱ�������</returns>
        public object ExecuteScalar(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteScalar(commandText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// ִ��SQL������䡣֧��SQL����еĲ���
        /// </summary>
        /// <param name="commandText">SQL �ı�������ߴ洢���̵�����</param>
        /// <param name="commandType">��������</param>
        /// <param name="commandParameters">�������󼯺ϣ���ѡ��</param>
        /// <returns>������Ӱ�������</returns>
        public object ExecuteScalar(string commandText, CommandType commandType, params IDataParameter[] commandParameters)
        {
            try
            {
                this.PrepareCommand(commandType, commandText, commandParameters);                
                object oReturn = _Command.ExecuteScalar();
                if (IsCanCloseConn)
                    _Command.Connection.Close();
                LogerForDB.DEBUG(new MyLog(commandText));

                return oReturn;
            }
            catch (Exception e)
            {
                LogerForDB.ERROR(new MyLog(commandText, e.Message));
                throw e;
            }
        }
		#endregion

		#region Rollback���ع�����
		/// <summary>
		/// �ع�����
		/// </summary>
		public void Rollback()
		{
			_Trans.Rollback();
            IsCanCloseConn = true;
            _Command.Connection.Close();
		}
		#endregion

		#region Commit���ݽ�����
		/// <summary>
		/// �ݽ�����
		/// </summary>
		public void Commit()
		{
			_Trans.Commit();
            IsCanCloseConn = true;
            _Command.Connection.Close();
		}
		#endregion

		#region BeginTrans����ʼ����
		/// <summary>
		/// ��ʼ����
		/// </summary>
		/// <remarks>
		/// <code>
		/// </code>
		/// </remarks>
		/// <example>
		/// <code>
		///   //������
		///   using (CommonDatabase MyDatabase = new CommonDatabase())
		///   {
		///		try
		///		{
		///			MyDatabase.BeginTrans();
		///			MyDatabase.ExecuteNonQuery("DELETE FROM Table1 WHERE Code='001'");
		///			MyDatabase.ExecuteNonQuery("DELETE FROM Table2 WHERE Code='001'");
		///			......
		///			MyDatabase.Commit();
		///		}
		///		catch
		///		{
		///			MyDatabase.Rollback();
		///			......
		///		}
		///   }	
		/// </code>
		/// </example>
		public void BeginTrans()
		{
			_Trans = _Conn.BeginTransaction();
            IsCanCloseConn = false;
		}
		#endregion	
	}
}
