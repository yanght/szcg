using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;

namespace Teamax.Common
{
    /// <summary>
    /// ����ʽ
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// ����
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// ����
        /// </summary>
        Descending = 1,
    }

    /// <summary>
    /// ��ѯ��ҳͨ����
    /// </summary>
    public class QueryUtil
    {
        #region �ֶα���������
        /// <summary>
        ///��ѯ���
        /// </summary>
        public string  Select = "*";

        /// <summary>
        ///���ݼ�
        /// </summary>
        private DataSet _ds ;

        /// <summary>
        /// ��ѯ��
        /// </summary>
        public string  From = "";

        /// <summary>
        /// ��ѯ����
        /// </summary>
        public string Where = "1=1 ";

        /// <summary>
        /// ˳��
        /// </summary>
        public SortOrder SortOrder = SortOrder.Descending;

        /// <summary>
        /// �������
        /// </summary>
        public string SortBy = "";

        /// <summary>
        /// �������
        /// </summary>
        public string GroupBy = "";

        /// <summary>
        /// key
        /// </summary>
        public string Key = "";

        /// <summary>
        /// ��ҳ��С
        /// </summary>
        private int _PageSize;

        /// <summary>
        /// ��ҳ��С
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
        /// ��ǰҳ��
        /// </summary>
        private int _PageIndex = 0;

        /// <summary>
        /// ������
        /// </summary>
        private int _RowCount = 0;

        /// <summary>
        /// ��ҳ����
        /// </summary>
        private int _PageCount = 0;

        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int RowCount
        {
            get {
                return _RowCount;
            }
        }

        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        public int PageCount
        {
            get
            {
                return _PageCount;
            }
        }

        /// <summary>
        /// ���ݼ�
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
        /// ���캯��
        /// </summary>
        /// <param name="select">��ѯ����</param>
        /// <param name="from">��ѯ��</param>
        /// <param name="where">��ѯ����</param>
        public QueryUtil(string select,string from,string where)
        {
            From = from;
            Select = select;
            if (where.Trim()!="")
                Where = where;
        }

        #region ExecuteDataset������ָ��ҳ������ݼ�

        /// <summary>
        /// ����ָ��ҳ������ݼ�
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset(int pageIndex)
        {
            return ExecuteDataset(pageIndex,"");
        }

        /// <summary>
        /// ����ָ��ҳ������ݼ�
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset(string key)
        {
            return ExecuteDataset(-1, key);
        }


        /// <summary>
        /// ����ָ��ҳ������ݼ�
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataset()
        {
            return ExecuteDataset("");
        }

        /// <summary>
        /// ����ָ��ҳ������ݼ�
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
        /// ��֯������SQL���
        /// </summary>
        /// <returns>��֯�õ�SQL���</returns>
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
        /// ����ҳ��SQL
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
        /// ȡ����ҳ��SQL
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
        /// ȡ����ҳ��SQL
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
