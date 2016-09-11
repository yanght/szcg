using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Index;
using Lucene.Net.Store;


using Teamax.Common.ScheduledTasks;


namespace bacgBL.Search
{
	/// <summary>
	/// 查询索引
	/// </summary>
	public class QueryIndex : IDisposable
	{
		IndexReader reader = null;
		Searcher searcher = null;
        public static readonly int pageSize = 40;

		/// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="filterSearchText">过滤条件</param>
		/// <param name="filterField">过滤字段</param>
		/// <param name="searchText">查询条件</param>
		/// <param name="pageIndex">查询页</param>
		/// <param name="fragmentSize">显示的数据长度</param>
		/// <returns>查询结果集</returns>
        public static ResultSet SafeSearch(string type,string filterSearchText, string filterField, string searchText, int pageIndex, int fragmentSize)
		{
			SearchLock.AquireReader(30,type);
			QueryIndex qi = null;
			try
			{
                qi = new QueryIndex(Jobs.Instance().CurrentJobs[type].PhysicalPath);
				qi.FragementSize = fragmentSize;
				return qi.Search(filterSearchText,filterField,searchText,pageIndex);
			}
			finally
			{
				if(qi != null)
				{
					qi.Close();
				}
				SearchLock.ReleaseReader(type);
			}
		}

		/// <summary>
        /// 查询数据
		/// </summary>
        /// <param name="text">查询条件</param>
        /// <param name="pageIndex">查询页</param>
        /// <param name="fragmentSize">显示的数据长度</param>
        /// <returns>查询结果集</returns>
        public static ResultSet SafeSearch(string type, string text, int pageIndex, int fragmentSize)
		{
			SearchLock.AquireReader(30,type);
			QueryIndex qi = null;
			try
			{
                qi = new QueryIndex(Jobs.Instance().CurrentJobs[type].PhysicalPath);
				qi.FragementSize = fragmentSize;
				return qi.Search(text,pageIndex);
			}
			catch(Exception e)
			{
				System.Web.HttpContext.Current.Response.Write("正在建立索引,请稍候再试!");
				return null;
			}
			finally
			{
				if(qi != null)
				{
					qi.Close();
				}
				SearchLock.ReleaseReader(type);
			}
		}


        public QueryIndex(string physicalPath)
		{
			reader = IndexReader.Open(physicalPath);
			searcher = new IndexSearcher(reader);
		}

		private int _fragementSize=0;
		
		/// <summary>
        ///显示的数据长度
		/// </summary>
		public int FragementSize
		{
			get {return this._fragementSize;}
			set {this._fragementSize = value;}
		}

		
		public ResultSet Search(string filterSearchText, string filterField, string searchText, int pageIndex)
		{
            string[] fields = { "datainfo", "title", "tag", "key", "dateadded", "gridcode" };
            Query multiquery = MultiFieldQueryParser.Parse(FormatSearch(searchText), fields, ConfigAnalyzer.GetAnalyzer());

			QueryFilter qf = new QueryFilter(QueryParser.Parse(filterSearchText,filterField,ConfigAnalyzer.GetAnalyzer()));
			
			StopWatch sw = new StopWatch();
            Hits hits = searcher.Search(multiquery, qf);
			long executionTime = sw.Peek();

            ResultSet results = GetResults(hits, pageIndex, multiquery);
			results.ExecutionTime = executionTime;

			
			return results;
		}	

		
		public ResultSet Search(string text, int pageIndex)
		{			
			StopWatch sw = new StopWatch();
            string[] fields = { "datainfo", "title", "tag", "key", "dateadded", "gridcode" };
            Query multiquery = MultiFieldQueryParser.Parse(FormatSearch(text), fields, ConfigAnalyzer.GetAnalyzer());
          
            Hits hits = searcher.Search(multiquery);
            multiquery = multiquery.Rewrite(this.reader);
			long executionTime = sw.Peek();

            ResultSet results = GetResults(hits, pageIndex, multiquery);
			results.ExecutionTime = executionTime;
			
			return results;
		}

		
		private string FormatSearch(string searchText)
		{
            
			searchText = Regex.Replace(searchText,@"\sand\s"," && ",RegexOptions.IgnoreCase);
			searchText = Regex.Replace(searchText,@"\s\+\s"," + ",RegexOptions.IgnoreCase);
            searchText= searchText.Replace("-","");
            searchText= searchText.Replace("!","\\!");
            searchText= searchText.Replace("(","\\(");
            searchText= searchText.Replace(")","\\)");
            searchText= searchText.Replace("[","\\]");
            searchText= searchText.Replace("^","\\^");
            searchText= searchText.Replace("\"","\\\"");
            searchText= searchText.Replace(":","\\:");
            searchText= searchText.Replace("~","\\~");
            searchText= searchText.Replace("*","\\*");
            searchText= searchText.Replace("?","\\?");

			return searchText;
		}

		private ResultSet GetResults(Hits hits, int pageIndex, Query query)
		{
			int startPosition = (pageIndex - 1) * pageSize;
			int endPosition = startPosition + pageSize;

			if(hits.Length() < endPosition)
			{
				endPosition = hits.Length();
			}
			
			return GetResults(hits,startPosition,endPosition,query);

		}


		private ResultSet GetResults(Hits hits, Query query)
		{
			return GetResults(hits,0,hits.Length(),query);
		}

		private ResultSet GetResults(Hits hits, int startPosition, int endPosition, Query query)
		{
			ResultSet results = new ResultSet();
			try
			{
				results.Count = hits.Length();

				QueryHighlightExtractor highlighter = null;
				if(this.FragementSize > 0)
				{
					highlighter = new QueryHighlightExtractor(query,ConfigAnalyzer.GetAnalyzer(),"<font color=\"red\">","</font>");
				}
				ArrayList al = new ArrayList();
				Result result = null;
				Document doc = null;
				for(int i = startPosition; i<endPosition; i++)
				{
					result = new Result();
					doc = hits.Doc(i);

                   

                    result.PermaLink = doc.GetField("permaLink").StringValue();
				
					
			
					if(FragementSize > 0)
					{
                        string temp = doc.GetField("datainfo").StringValue();
                        result.DataInfo = highlighter.GetBestFragment(temp, FragementSize);
                        if (result.DataInfo == null)
                            result.DataInfo = temp;
                        temp = doc.GetField("dateadded").StringValue().Insert(4, "-").Insert(7, "-");
                        result.DateAdded = highlighter.GetBestFragment(temp, FragementSize);
                        if (result.DateAdded == null)
                            result.DateAdded = temp;
                        temp = doc.GetField("title").StringValue();
                        result.Title = highlighter.GetBestFragment(temp, FragementSize);
                        if (result.Title == null)
                            result.Title = temp;
                        temp = doc.GetField("tag").StringValue();
                        result.Tag = highlighter.GetBestFragment(temp, FragementSize);
                        if (result.Tag == null)
                            result.Tag = temp;
                        temp = doc.GetField("gridcode").StringValue();
                        result.Gridcode = highlighter.GetBestFragment(temp, FragementSize);
                        if (result.Gridcode == null)
                            result.Gridcode = temp;
					}
					else
					{
                        result.Title = doc.GetField("title").StringValue();
                        result.Tag = doc.GetField("tag").StringValue();
                        result.DateAdded = doc.GetField("dateadded").StringValue();
                        result.DataInfo = doc.GetField("datainfo").StringValue();
                        result.Gridcode = doc.GetField("gridcode").StringValue();
					}
					result.Score = hits.Score(i);
					
					al.Add(result);
				}
				results.Results = (Result[])al.ToArray(typeof(Result));
			}
			catch(Exception e)
			{
			    //
			}
			return results;

		}

		public void Dispose()
		{
			Close();
		}

		private bool isDisposed = false;

		public void Close()
		{
			if(!isDisposed)
			{
				searcher.Close();
				reader.Close();
				isDisposed = true;
			}
		}


	}
}
