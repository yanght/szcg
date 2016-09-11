using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;

namespace Teamax.Common
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 升序
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// 降序
        /// </summary>
        Descending = 1,
    }

    /// <summary>
    /// 查询分页通用类
    /// </summary>
    public class QueryUtil
    {
        #region 字段变量与属性
        /// <summary>
        ///查询结果
        /// </summary>
        public string  Select = "*";

        /// <summary>
        ///数据集
        /// </summary>
        private DataSet _ds ;

        /// <summary>
        /// 查询集
        /// </summary>
        public string  From = "";

        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where = "1=1 ";

        /// <summary>
        /// 顺序
        /// </summary>
        public SortOrder SortOrder = SortOrder.Descending;

        /// <summary>
        /// 排序规则
        /// </summary>
        public string SortBy = "";

        /// <summary>
        /// 排序规则
        /// </summary>
        public string GroupBy = "";

        /// <summary>
        /// key
        /// </summary>
        public string Key = "";

        /// <summary>
        /// 分页大小
        /// </summary>
        private int _PageSize;

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set 
            { 
                _PageSize = value;
                if (_PageSize == 0)
                {
                    _PageSize = 15;
                }
            }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        private int _PageIndex = 0;

        /// <summary>
        /// 总行数
        /// </summary>
        private int _RowCount = 0;

        /// <summary>
        /// 总页码数
        /// </summary>
        private int _PageCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount
        {
            get {
                return _RowCount;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _PageCount;
            }
        }

        /// <summary>
        /// 数据集
        /// </summary>
        public DataSet ds
        {
            get
            {
                if (_ds == null)
                    _ds = new DataSet();
                return _ds;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="select">查询条件</param>
        /// <param name="from">查询集</param>
        /// <param name="where">查询条件</param>
        public QueryUtil(string select,string from,string where)
        {
            From = from;
            Select = select;
            if (where.Trim()!="")
                Where = where;
        }

        #region ExecuteDataset：返回指定页码的数据集

        /// <summary>
        /// 返回指定页码的数据集
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset(int pageIndex)
        {
            return ExecuteDataset(pageIndex,"");
        }

        /// <summary>
        /// 返回指定页码的数据集
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset(string key)
        {
            return ExecuteDataset(-1, key);
        }


        /// <summary>
        /// 返回指定页码的数据集
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset()
        {
            return ExecuteDataset("");
        }

        /// <summary>
        /// 返回指定页码的数据集
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset(int pageIndex,string key)
        {

            _PageIndex = pageIndex == 0 ? 1 : pageIndex;
            DataSet ds;
            if (_PageIndex != -1)
            {
                if (key != "")
                {
                    using (CommonDatabase myDatabase = new CommonDatabase(ConfigurationManager.AppSettings[key]))
                    {
                        ds = myDatabase.ExecuteDataset(GeneratePageSql());
                    }
                }
                else
                {
                    using (CommonDatabase myDatabase = new CommonDatabase())
                    {
                        ds = myDatabase.ExecuteDataset(GeneratePageSql());
                    }
                }

                _RowCount = System.Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                _PageCount = _RowCount / _PageSize;
                if (_RowCount % _PageSize != 0)
                    _PageCount++;
                if (_PageCount < _PageIndex)
                    _PageIndex = _PageCount;
                ds.Tables.RemoveAt(0);
            }
            else
            {
                using (CommonDatabase myDatabase = new CommonDatabase())
                {
                    ds = myDatabase.ExecuteDataset(GenerateSql());
                }
            }
            _ds = ds;
            return ds;
        }
        #endregion

        /// <summary>
        /// 组织完整的SQL语句
        /// </summary>
        /// <returns>组织好的SQL语句</returns>
        private string GenerateSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" Select ");
            sql.Append(Select);
            sql.Append(" From ");
            sql.Append(From);
            sql.Append(" Where ");
            sql.Append(Where);
            if (GroupBy != "")
            {
                sql.Append(" group by " + GroupBy);
            }
            if (SortBy != "")
            {
                sql.Append(" order by ");
                sql.Append(SortBy);
                sql.Append(SortOrder == SortOrder.Ascending ? " asc " : " desc ");
            }
            return sql.ToString();
        }

        /// <summary>
        /// 生成页码SQL
        /// </summary>
        /// <returns></returns>
        private string GeneratePageSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("    declare @TempSize int, @RowCount int  ");
            sql.Append(" Select @RowCount=count(1) From ");
            sql.Append(From);
            sql.Append(" Where ");
            sql.Append(Where);
            sql.Append(" Select @RowCount  if(@RowCount<" + System.Convert.ToString(PageSize * (_PageIndex)) + ") set @TempSize=@RowCount-"+System.Convert.ToString(PageSize * (_PageIndex-1)));
            sql.Append(" else  set @TempSize =" + PageSize);
            sql.Append(" if(@TempSize<=0) set @TempSize=@RowCount ");
            if (Key == "")
            {
                PageSqlForNoKey(sql);
            }
            else
            {
                PageSqlForKey(sql);
            }
           
            return sql.ToString();
        }

        /// <summary>
        /// 取出分页的SQL
        /// </summary>
        /// <param name="sql"></param>
        private void PageSqlForKey(StringBuilder sql)
        {
            sql.Append(" Select ");
            sql.Append(Select);
            sql.Append(" From ");
            sql.Append(From);
            sql.Append(" Where ");
            sql.Append(Key);
            sql.Append(" in (select top(@TempSize) ");
            sql.Append(Key);
            sql.Append(" from ( select top " + System.Convert.ToString(PageSize * _PageIndex));
            sql.Append(" " + Key );
            sql.Append(" From ");
            sql.Append(From);
            sql.Append(" Where ");
            sql.Append(Where);
            
            if (SortBy == "")
            {
                sql.Append(" order by 1) as t1 order by 1 desc ) order by 1 ");
            }
            else
            {
                sql.Append(" order by ");
                sql.Append(SortBy);
                string order = "", unorder = "";
                if (SortOrder == SortOrder.Ascending)
                {
                    order = " asc ";
                    unorder = " desc ";
                }
                else if (SortOrder == SortOrder.Descending)
                {
                    order = " desc ";
                    unorder = " asc ";
                }
                sql.Append(order);
                sql.Append(" ) as t1  order by ");
                sql.Append(SortBy);
                sql.Append(unorder);
                sql.Append(")  order by ");
                sql.Append(SortBy);
                sql.Append(order);
            }
        }

        /// <summary>
        /// 取出分页的SQL
        /// </summary>
        /// <param name="sql"></param>
        private void PageSqlForNoKey(StringBuilder sql)
        {
            sql.Append("  Select * from (select top(@TempSize) ");
            sql.Append(" * from ( select top " + System.Convert.ToString(PageSize * _PageIndex));
            sql.Append(" "+Select);
            sql.Append(" From ");
            sql.Append(From);
            sql.Append(" Where ");
            sql.Append(Where);

            if (GroupBy != "")
            {
                sql.Append(" group by " + GroupBy + " ");
            } 

            if(SortBy=="")
            {
                sql.Append(" order by 1) as t1 order by 1 desc ) as t2  order by 1 ");
            }
            else
            {
                sql.Append(" order by ");
                sql.Append(SortBy);
                string order = "", unorder = "";
                if (SortOrder == SortOrder.Ascending)
                {
                    order = " asc ";
                    unorder=" desc ";
                }
                else if(SortOrder == SortOrder.Descending)
                {
                    order = " desc ";
                    unorder=" asc ";
                }
                sql.Append(order);
                sql.Append(" ) as t1  order by ");
                sql.Append(SortBy);
                sql.Append(unorder);
                sql.Append(") as t2  order by ");
                sql.Append(SortBy);
                sql.Append(order);
            }
        }
    }
}
