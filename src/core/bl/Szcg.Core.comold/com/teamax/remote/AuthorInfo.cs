using System;

namespace DemoRemoteObj
{
	/// <summary>
	/// Summary description for AuthorInfo.
	/// </summary>
	[Serializable()]
	public class RemoteAuthorInfo
	{
		public RemoteAuthorInfo(string name,string address)
		{
			//
			// TODO: Add constructor logic here
			this.name = name;
			this.by = "by hello!";
			this.address = address;
			//
		}
		public string by ;
		public string name;
		public string address;

	 
	}
}
