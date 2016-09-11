using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using System.Threading;


namespace bacgBL.Search
{
	
	public class Indexer : IDisposable
	{
		protected Directory dir = null;
		protected IndexWriter writer = null;
        public IndexReader reader=null;
        protected bool _create;
        private int intwait = 0;

        public Indexer(System.Xml.XmlNode node, bool create)
		{
            string physicalPath = node.Attributes["physicalPath"].Value;
            if (!System.IO.Directory.Exists(physicalPath))
			{
				create=true;
			}
            if (node.Attributes["name"].Value == "object_index")
                create = true;
            _create = create;
			dir = FSDirectory.GetDirectory(physicalPath,create);
            IndexReader.Unlock(dir); 
			writer = new IndexWriter(dir,ConfigAnalyzer.GetAnalyzer(),create);
            reader = IndexReader.Open(dir);
			writer.mergeFactor = 50;
            writer.minMergeDocs = 2000;
		}


		public void AddDocument(Document doc)
		{
            if (!_create)
            {
                reader.Delete(new Term("key", doc.GetField("key").StringValue()));
            }
			writer.AddDocument(doc);
            if (intwait >= 10)
            {
                Thread.Sleep(1);
                intwait = 0;
            }
            intwait++;
		}

		public void Close()
		{
			if(!disposed)
			{
                reader.Close();
				dir.Close();
				writer.Optimize();
				writer.Close();
				disposed = true;
			}
		}

		private bool disposed = false;

	
		public void Dispose()
		{
			Close();
		}

	}
}
