using System;

namespace bacgBL.Search
{
	
	public class Result
	{
		private string _permaLink;
		
		public string PermaLink
		{
			get {return this._permaLink;}
			set {this._permaLink = value;}
		}

		private string _datainfo;


        public string DataInfo
		{
            get { return this._datainfo; }
            set { this._datainfo = value; }
		}

        private string _title;

        public string Title
		{
            get { return this._title; }
            set { this._title = value; }
		}

        private string _dateadded;

        public string DateAdded
		{
            get { return this._dateadded; }
            set { this._dateadded = value; }
		}

        private string _tag;

        public string Tag
        {
            get { return this._tag; }
            set { this._tag = value; }
        }   

        private float _score;

        public float Score
        {
            get { return this._score; }
            set { this._score = value; }
        }

        private string _gridcode;

        public string Gridcode
        {
            get { return this._gridcode; }
            set { this._gridcode = value; }
        }   
	}
}
