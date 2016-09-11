using System;
using Lucene.Net;

namespace bacgBL.Search
{
	/// <summary>
	/// ConfigAnalyzer 的摘要说明。
	/// </summary>
	public class ConfigAnalyzer
	{
		public static Lucene.Net.Analysis.Analyzer GetAnalyzer()
		{
			return new Lucene.Net.Analysis.China.ChineseAnalyzer();
		}
	}
}
