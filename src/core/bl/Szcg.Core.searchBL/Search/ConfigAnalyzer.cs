using System;
using Lucene.Net;

namespace bacgBL.Search
{
	/// <summary>
	/// ConfigAnalyzer ��ժҪ˵����
	/// </summary>
	public class ConfigAnalyzer
	{
		public static Lucene.Net.Analysis.Analyzer GetAnalyzer()
		{
			return new Lucene.Net.Analysis.China.ChineseAnalyzer();
		}
	}
}
