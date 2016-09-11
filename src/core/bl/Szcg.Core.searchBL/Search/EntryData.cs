using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using Lucene.Net.Documents;

using bacgDL.Search;

namespace bacgBL.Search
{
	public class EntryData
	{
		private Weighter weighter = null;
        private System.Xml.XmlNode _node = null;
        private string jobname = "";
        private DataTable table = null;
        private IDataReader dr = null;

        public EntryData(System.Xml.XmlNode node)
        {
			weighter = new Weighter();
            _node = node;
            jobname = _node.Attributes["name"].Value;
            SearchProvider searchProvider = new SearchProvider();
            if (jobname == "project_index")
            {
                table = searchProvider.GetIndexProject(jobname);
            }
            else if (jobname == "object_index")
            {
                dr = searchProvider.GetIndexObject(jobname);
            }
		}

		public void BuildItems(Indexer index)
		{
            try
			{
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        index.AddDocument(CreateDoc(table.Rows[i]));
                    }
                }
                else if (dr != null)
                {
                    int indexaa =0;
                    while (dr.Read())
                    {
                        try
                        {
                            index.AddDocument(CreateDoc(dr));
                        }
                        catch
                        {
                            indexaa++;
                        }
                    }
                    
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }
			}
			catch(Exception e)
			{
				
			}
			finally
			{
                if (table != null)
                {
                    table.Clear();
                    table.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
			}
		}

        private Document CreateDoc(IDataReader reader)
        {
            Document doc = new Document();
            try
            {
                doc.Add(Field.Keyword("key", (string)reader["key"]));
                doc.Add(Field.Keyword("gridcode", (string)reader["gridcode"]));
                doc.Add(Field.Text("tag", (string)reader["tag"]));
                doc.Add(Field.Text("title", (string)reader["title"]));
                doc.Add(Field.Text("datainfo", (string)reader["datainfo"]));
                DateTime dateCreated = (DateTime)reader["dateadded"];
                doc.Add(Field.Keyword("dateadded", dateCreated.ToString("yyyyMMdd")));
                string permaLink = "";
                if (jobname == "project_index")
                {
                    permaLink = _node.Attributes["urlFormat"].Value + "?key=" + (string)reader["key"] + "&date=" + dateCreated.ToShortDateString();
                }
                else if (jobname == "object_index")
                {
                    permaLink = _node.Attributes["urlFormat"].Value + "?key=" + (string)reader["key"] + "&layer=" + (string)reader["title"];
                }
               

                int boost = weighter.Calculate(dateCreated);
                doc.SetBoost(boost);
                doc.Add(Field.UnIndexed("permaLink", permaLink));
            }
            catch (Exception e)
            {
                //日志
            }
            return doc;
        }
		
		private Document CreateDoc(DataRow reader)
		{
			
			Document doc = new Document();
			try
			{
                doc.Add(Field.Keyword("key", (string)reader["key"]));
                doc.Add(Field.Text("gridcode", (string)reader["gridcode"]));
                doc.Add(Field.Text("tag", (string)reader["tag"]));
				doc.Add(Field.Text("title",(string)reader["title"]));
				doc.Add(Field.Text("datainfo",(string)reader["datainfo"]));
				DateTime dateCreated = (DateTime)reader["dateadded"];
                doc.Add(Field.Text("dateadded", dateCreated.ToString("yyyyMMdd")));

                string permaLink = _node.Attributes["urlFormat"].Value + "?key=" + (string)reader["key"] + "&date=" + dateCreated.ToShortDateString();

				int boost = weighter.Calculate(dateCreated);
				doc.SetBoost(boost);
                doc.Add(Field.UnIndexed("permaLink", permaLink));
			}
			catch(Exception e)
			{
                //日志
			}
			return doc;
		}

	}
}
