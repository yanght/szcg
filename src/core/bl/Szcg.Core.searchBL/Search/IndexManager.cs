using System;
using System.Collections;
using System.IO;

using Lucene.Net.Documents;


namespace bacgBL.Search
{
	
	public class IndexManager
	{
		private IndexManager()
		{

		}

		public static void RebuildSafeIndex(int lockSeconds,System.Xml.XmlNode node)
		{
			try
			{
                Build(node, lockSeconds);
			}
			catch(Exception e)
			{
				//
			}
		}

        private static void Build(System.Xml.XmlNode node, int lockSeconds)
		{
            EntryData data = new EntryData(node); 
            SearchLock.AquireWriter(lockSeconds, node.Attributes["name"].Value);
            Indexer index = new Indexer(node, false);
			try
			{
				data.BuildItems(index);
			}
			catch(Exception e)
			{
				//
			}
			finally
			{
				index.Close();
                SearchLock.ReleaseWriter(node.Attributes["name"].Value);
			}
		}

	}
}
