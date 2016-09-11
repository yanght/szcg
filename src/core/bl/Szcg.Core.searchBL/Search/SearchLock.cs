using System;
using System.Threading;

namespace bacgBL.Search
{
	public class SearchLock
	{
		
		private SearchLock(){}

		static SearchLock()
		{
            rwl_project = new ReaderWriterLock();
            rwl_object = new ReaderWriterLock();
		}

		private static ReaderWriterLock rwl_project = null;
        private static ReaderWriterLock rwl_object = null;
		
		public static void AquireReader(int timeout,string type)
		{
            try
            {
                if (type == "object_index")
                    rwl_object.AcquireReaderLock(timeout * 1000);
                else if (type == "project_index")
                    rwl_project.AcquireReaderLock(timeout * 1000);
                else
                    throw new Exception("�������");
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("���ڽ�������,���Ժ�����!");
               
            }
		}

        public static void ReleaseReader(string type)
		{
            try
            {
                if (type == "object_index")
                    rwl_object.ReleaseReaderLock();
                else if (type == "project_index")
                    rwl_project.ReleaseReaderLock();
                else
                    throw new Exception("�������");
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("���ڽ�������,���Ժ�����!");
               
            }
		}

        public static void ReleaseWriter(string type)
		{
            try
            {
                if (type == "object_index")
                    rwl_object.ReleaseWriterLock();
                else if (type == "project_index")
                    rwl_project.ReleaseWriterLock();
                else
                    throw new Exception("�������");
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("���ڽ�������,���Ժ�����!");

            }
		}

		public static void AquireWriter(int timeout,string type)
        {
            try
            {
                if (type == "object_index")
                    rwl_object.AcquireWriterLock(timeout * 1000);
                else if (type == "project_index")
                    rwl_project.AcquireWriterLock(timeout * 1000);
                else
                    throw new Exception("�������");
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("���ڽ�������,���Ժ�����!");

            }
		}

	}
}
