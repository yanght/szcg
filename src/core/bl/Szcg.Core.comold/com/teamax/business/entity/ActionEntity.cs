using System;

namespace szcg.com.teamax.business.entity
{
	/// <summary>
	/// ActionEntity 的摘要说明。
	/// </summary>
	public class ActionEntity
	{
		
		
		private int  id;
		private int stepid;
		private string prvstate;
		private string nextstate;
		private string actiontext;
		private string actioncode;
		private string actionname;


		public ActionEntity(System.Int32 _id,  System.Int32 _stepid, string _prvstate, string _nextstate, string actioncode, string actiontext, string actionname )
		{
			this.id = _id;
			this.stepid = _stepid;
			this.prvstate = _prvstate;
			this.nextstate = _nextstate;
			this.actioncode = actioncode;
			this.actiontext = actiontext;
			this.actionname = actionname;

		}

		public int getid(){
			return id;
		}

		public void setid(int _id){
			this.id = _id;
		}

		public int getstepid()
		{
			return stepid;
		}

		public void setstepid(int stepid)
		{
			this.stepid = stepid;
		}

		public string getprvstate()
		{
			return prvstate;
		}

		public void setprvstate(string prvstate)
		{
			this.prvstate = prvstate;
		}

		public string getnextstate()
		{
			return nextstate;
		}

		public void setnextstate(string nextstate)
		{
			this.nextstate = nextstate;
		}


		public string getactiontext()
		{
			return actiontext;
		}

		public void setactiontext(string actiontext)
		{
			this.actiontext = actiontext;
		}

		public string getactioncode()
		{
			return actioncode;
		}

		public void setactioncode(string actioncode)
		{
			this.actioncode = actioncode;
		}

		public string getactionname()
		{
			return actionname;
		}

		public void setactionname(string actionname)
		{
			this.actionname = actionname;
		}

		
	}
}
