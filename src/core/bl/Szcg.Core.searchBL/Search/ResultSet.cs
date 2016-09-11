using System;

namespace bacgBL.Search
{
	/// <summary>
	/// 结果集合
	/// </summary>
	public class ResultSet
	{
		public ResultSet()
		{

		}

		private int _count;
		
		
		public int Count
		{
			get {return this._count;}
			set {this._count = value;}
		}

		private long _executionTime;
		
		public long ExecutionTime
		{
			get {return this._executionTime;}
			set {this._executionTime = value;}
		}

		private Result[] _results;
		
		public Result[] Results
		{
			get {return this._results;}
			set {this._results = value;}
		}

		private int _pageIndex;
		
		public int PageIndex
		{
			get {return this._pageIndex;}
			set {this._pageIndex = value;}
		}
	}
}
