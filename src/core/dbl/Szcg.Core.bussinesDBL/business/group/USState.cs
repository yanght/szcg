using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.business.group
{
    public class USState
    {
        private string myShortName ;
		private string myLongName ;
    
		public USState(string strLongName, string strShortName)
		{

			this.myShortName = strShortName;
			this.myLongName = strLongName;
		}

		public string ShortName
		{
			get
			{
				return myShortName;
			}
		}

		public string LongName
		{
        
			get
			{
				return myLongName ;
			}
		}

		public override string ToString()
		{
			return this.ShortName + " - " + this.LongName;
		}
    }

    public class GroupPerson
    {
        public int usercode;
        public string username;
    }

    public struct GroupTreeSuruct
    {
        public int id;
        public int ParentGroupID;
        public int usercode;
        public string groupname;
        public bool GroupType;
    }

    public struct TreeSuruct
    {
        public string pcode;
        public string code;
        public string text;
        public string tag;
    }
}
