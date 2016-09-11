/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：数据库访问类。对ADO.NET进行简单封装，方便使用。
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-05-21
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Teamax.Common.Loger;

namespace Teamax.Common
{
	/// <summary>
	/// 数据库访问类。对ADO.NET进行简单封装，方便使用。
	///	</summary>
	///	<remarks>
	/// 准备工作：ConnectionString提前写在Web.Config文件中，如下所示：
	///<code>
	///		<appSettings>
    ///			<add key="ConnectionString" value="server=192.168.0.250\testdb; user id=sa; password=123; database=gps;" />
	///		</appSettings>
	///	</code>
	///	</remarks>
	///	<example>
	///	<code>
	///	//1.一个数据访问层类示例程序如下。
	///	using System;
	///	using System.Data;
	///	using System.Data.SqlClient;
	///	using Teamax.Common;  //引用命名空间
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
    ///			#region GetProj：获取案件信息
    ///			public DataSet GetProj(string projcode)
    ///			{
    ///				string strSQL = string.Format(@"　SELECT * FROM b_project WHERE projcode='{0}'　",projcode);
    ///				return ExecuteDataset(strSQL,"b_project");
    ///			}
    ///			#endregion
    /// 
    ///			#region GetProjAndTrace：获取案件信息和案件流程
    ///			public DataSet GetProjAndTrace(string projcode)
	///			{
    ///				string strSQL = string.Format(@"　SELECT * FROM b_project WHERE projcode='{0}'　",projcode);
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
		#region 属性与字段变量
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
		/// 数据库连接字符串
		/// </summary>
        private static string _ConnectionString = "";

		/// <summary>
		/// 连接字符串：直接从配置文件中读取
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

        #region 一些内部使用的方法
        #region AttachParameters：将SqlParameter[]数组添加到SqlCommand
        /// <summary>
        /// 将SqlParameter[]数组添加到SqlCommand
        /// </summary>
        /// <param name="command">ADO.net中的SqlCommand</param>
        /// <param name="commandParameters">SqlParameter[]数组</param>
        private void AttachParameters(IDbCommand command, IDataParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command参数为空");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        //判断参数值是否为空
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

        #region PrepareCommand：生成SqlCommand
        /// <summary>
        /// 生成SqlCommand
        /// </summary>
        /// <param name="commandText">SELECT语句：SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数</param>
        private void PrepareCommand(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (IsDisposed)
                throw new ObjectDisposedException("CommonDatabase", "对象已经摧毁，无法再使用");

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
                    throw new ObjectDisposedException("_Trans","事务对象为空，请提供一个打开的SqlTransaction");
                _Command.Transaction = _Trans;
            }
        }
        #endregion
        #endregion

		#region 构造函数
		/// <summary>
		/// 构造函数。
		/// 数据库连接字符串默认从配置文件获取
		/// </summary>
		public CommonDatabase():this("")
        {
		}

		/// <summary>
		/// 构造函数。
		/// 传入数据库连接字符串。
		/// </summary>
        /// <param name="strConnStr">数据库连接字符串</param>
		public CommonDatabase(string strConnStr)
		{
            if (strConnStr == "")
                strConnStr = ConnectionString;

            _Conn = new SqlConnection(strConnStr); 
			_Conn.Open();
		}

		#endregion

		#region 析构函数
		/// <summary>
		/// 这里使用了.NET推荐的Dispose模式以释放Connection资源和相关的Command,DataAdapter对象。
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 释放Connection资源和相关的Command,DataAdapter对象
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if(!this.IsDisposed)
			{
				if(disposing)
				{
					// 释放托管资源
					//_Conn.Close();
					_Conn.Dispose();
					if (_Trans!=null)
						_Trans.Dispose();
					if (_Command!=null)
						_Command.Dispose();
					//因为DataAdapter类没有Dispose对象，所以不释放
					//_Adapter
					//因为程序的调用者还需要使用Dataset，所以不释放
					//_Dataset;
					//因为程序的调用还需要使用DataReader,所以不释放
					//_Reader;
				}
				// 释放非托管资源
				// only the following code is executed.
				//CloseHandle(handle);
				//handle = IntPtr.Zero;
			}
			IsDisposed = true;
		}

		/// <summary>
		/// 析构函数
		/// </summary>
		~CommonDatabase()      
		{
			Dispose(false);
		}
		#endregion

        #region ExecuteDataset：执行SQL语句（SQL 文本命令或者存储过程），SQL语句中可以包含参数，返回DataSet。
        /// <summary>
        /// 执行SQL 文本命令，返回DataSet。
		/// </summary>
        /// <param name="commandText">SQL 文本命令。（默认）</param>
        /// <returns>返回根据SQL语句生成的DataSet，其中只包含一个DataTable对象</returns>
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
        /// 执行SQL语句（SQL 文本命令或者存储过程），传入参数，返回DataSet。
        /// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回根据SQL语句生成的DataSet，其中只包含一个DataTable对象</returns>
        public DataSet ExecuteDataset(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteDataset(commandText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行SQL语句（SQL 文本命令或者存储过程），传入参数，返回DataSet。
        /// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回根据SQL语句生成的DataSet，其中只包含一个DataTable对象</returns>
        public DataSet ExecuteDataset(string commandText, CommandType commandType, params IDataParameter[] commandParameters)
        {
            return ExecuteDataset(commandText, commandType, 0, commandParameters);
        }

        /// <summary>
        ///  执行SQL语句返回DataSet，并给DataSet中的表赋予表名，SQL语句中可以包含参数。
        /// </summary>
        /// <param name="commandText">SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数（可选）</param>
        /// <returns>DataSet，其中只包含一个DataTable对象</returns>
        /// <remarks>
        /// 注意：
        ///     如果参数commandText为存储过程，commandType应该为CommandType.StoredProcedure
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

        #region ExecuteReader：执行SQL语句（SQL 文本命令或者存储过程），SQL语句中可以包含参数，返回DataReader对象。
        /// <summary>
        /// 执行SQL 文本命令，返回DataReader
		/// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <returns>返回DataReader对象</returns>
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
        /// 执行SQL语句（SQL 文本命令或者存储过程），返回DataReader对象。
        /// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回DataReader对象</returns>
        public IDataReader ExecuteReader(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteReader(commandText, CommandType.Text, true, commandParameters);
        }

        /// <summary>
        /// 执行SQL语句（SQL 文本命令或者存储过程），SQL语句中可以包含参数，返回DataReader对象。
        /// </summary>
        /// <param name="commandText">SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="bCanClose">是否能够自动关闭Connection 对象。真：如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回DataReader对象</returns>
        /// <remarks>
        /// 注意：
        ///     如果参数commandText为存储过程，commandType应该为CommandType.StoredProcedure
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

        #region ExecuteNonQuery：对连接执行 Transact-SQL 语句（SQL 文本命令或者存储过程）并返回受影响的行数。
        /// <summary>
        /// 执行SQL 文本命令并返回受影响的行数
		/// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <returns>返回受影响的行数</returns>
		/// <example>
		/// <code>
        /// 示例1：
		///   using (CommonDatabase MyDatabase = new CommonDatabase())
		///   {
        ///			MyDatabase.ExecuteNonQuery("DELETE FROM b_project WHERE b_project='001'");
		///   }
        /// 
        /// 示例2：
        /// using (Teamax.Common.CommonDatabase myDatabase = new Teamax.Common.CommonDatabase())
        /// {                       
        ///     string sql = "insert into tab1(a,b)  values(@aa,@bb)";
        ///	    SqlParameter spAA = new SqlParameter("@aa","刘德华");
        ///	    SqlParameter spBB = new SqlParameter("@bb","演出");
        ///	    myDatabase.ExecuteNonQuery(sql,CommandType.Text,spAA,spBB)
        /// }
        /// </code>
		/// </example>
		public int ExecuteNonQuery(string commandText)
		{
            return ExecuteNonQuery(commandText, CommandType.Text);
		}

        /// <summary>
        ///  执行SQL 文本命令并返回受影响的行数，支持SQL语句中的参数。
        /// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, true, commandParameters);
        }

		/// <summary>
        /// 对连接执行 Transact-SQL 语句（SQL 文本命令或者存储过程）并返回受影响的行数。支持SQL语句中的参数
		/// </summary>
        /// <param name="commandText">SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params IDataParameter[] commandParameters)
		{
            return ExecuteNonQuery(commandText, commandType, true, commandParameters);
		}

        /// <summary>
        /// 对连接执行 Transact-SQL 语句（SQL 文本命令或者存储过程）并返回受影响的行数。支持SQL语句中的参数
        /// </summary>
        /// <param name="commandText">SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="IsWriteLog">是否写日志。可以强制不写日志（比如：执行写系统日志的方法时，可以不写日志。）</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回受影响的行数</returns>
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

        #region ExecuteScalar：执行Transact-SQL 语句（SQL 文本命令或者存储过程，返回第一行第一列的值
        /// <summary>
        /// 执行SQL 文本命令，返回第一行第一列的值
		/// </summary>
        /// <param name="commandText">SQL 文本命令</param>
		/// <returns>第一行第一列的值</returns>
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
        /// 执行SQL 文本命令，返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">SQL 文本命令</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回受影响的行数</returns>
        public object ExecuteScalar(string commandText, params IDataParameter[] commandParameters)
        {
            return ExecuteScalar(commandText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// 执行SQL操作语句。支持SQL语句中的参数
        /// </summary>
        /// <param name="commandText">SQL 文本命令或者存储过程的名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数对象集合（可选）</param>
        /// <returns>返回受影响的行数</returns>
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

		#region Rollback：回滚事务
		/// <summary>
		/// 回滚事务
		/// </summary>
		public void Rollback()
		{
			_Trans.Rollback();
            IsCanCloseConn = true;
            _Command.Connection.Close();
		}
		#endregion

		#region Commit：递交事务
		/// <summary>
		/// 递交事务
		/// </summary>
		public void Commit()
		{
			_Trans.Commit();
            IsCanCloseConn = true;
            _Command.Connection.Close();
		}
		#endregion

		#region BeginTrans：开始事务
		/// <summary>
		/// 开始事务
		/// </summary>
		/// <remarks>
		/// <code>
		/// </code>
		/// </remarks>
		/// <example>
		/// <code>
		///   //事务处理
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
