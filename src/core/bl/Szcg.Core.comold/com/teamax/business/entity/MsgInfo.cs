using System;

namespace szcg.com.teamax.business.entity
{
	/// <summary>
	/// MsgInfo  zhg 2006-5-11的摘要说明。
	/// </summary>


	public struct  MsgInfo
	{

		private int _id;
        /*系统编号 */
		private int _Sysmsgid;
		/*发送用户编码 */
		private int _go_user;

　　　　/*接收用户编码 */
		private int _to_user;
　　　　/*主题 */
		private string _msgtitle;
		/*内容 */
		private string _msgcontent;
　　　　/*发送时间 */　
		private string _cu_date;
		/*是否被浏览过 */
		private string _isread;
		/*部门名称*/
		private string _departname; 
		/*发送人姓名*/
		private string _username;
		/*案卷编号*/
		private string _projcode;
		/*案卷名称*/
		private string _projname;  
		/*监督员工号*/
		private string _collcode; 
		/*监督员姓名*/
		private string _collname; 
		private string _codes;


		public MsgInfo(int id, int Sysmsgid, int go_user, int to_user, 
			string msgtitle,string _codes, string msgcontent, string cu_date,string isread,string departname,string username,string projcode, string projname, string collcode,string collname)
		{

			//reset();
			this._codes = _codes;
			this._id = id;
			this._Sysmsgid = Sysmsgid;
			this._go_user =go_user;
			this._to_user = to_user;
			this._msgtitle = msgtitle;
			this._msgcontent=msgcontent;
			this._cu_date =cu_date;
			this._isread =isread;
			this._departname=departname;
			this._username=username;
			this._projcode=projcode;
			this._projname=projname;
			this._collcode=collcode;
			this._collname=collname;



			
		}
		/// <summary>
		/// zhaghuagen
		/// </summary>
		/// <returns></returns>

		public String getProjname()
		{
			return _projname;
		}

		public string getCodes()
		{
			return _codes;
		}

		public void setCodes(string codes)
		{
			this._codes = codes;
		}

		public void setProjname(String projname)
		{
			this._projname = projname;
		}

		public int getId()
		{
			return _id;
		}
		public void setId(int id) 
		{
			this._id = id;
		}
 

		public int getSysmsgid()
		{
			return  _Sysmsgid;
		}
		public void setSysmsgid(int Sysmsgid) 
		{
			this._Sysmsgid = Sysmsgid;
		}
		
		public int getGo_user()
		{
			return  _go_user;
		}

		public void setGo_user(int go_user) 
		{
			this._go_user = go_user;
		}

		public int getTo_user()
		{
			return _to_user;
		}

		public void setTo_user(int to_user) 
		{
			this._to_user = to_user;
		}

		public string getMsgtitle()
		{
			return _msgtitle;
		}
		public void setMsgtitle(String msgtitle)
		{
			this._msgtitle = msgtitle;
		}

		public string getMsgcontent()
		{
			return _msgcontent;
		}
		public void setMsgcontent(String msgcontent)
		{
			this._msgcontent = msgcontent;
		}



		public string getCu_date()
		{
			return _cu_date;
		}
		public void setCu_date(String cu_date)
		{
			this._cu_date = cu_date;
		}

		public string getIsread()
		{
			return _isread;
		}
		public void setIsread(String isread)
		{
			this._isread = isread;
		}

		public string getDepartname() 
		{
			return _departname;
		}

		public void setDepartname(String departname)
		{
			this._departname = departname;
		}

		public string getUsername() 
		{
			return _username;
		}

		public void setUsername(String username)
		{
			this._username = username;
		} 
       /*zhanghuagen05-26PDA消息*/
	    public string getProjcode() 
		{
			return _projcode;
		}

		public void setProjcode(String projcode)
		{
			this._projcode = projcode;
		}
		public string getCollcode() 
		{
			return _collcode;
		}

		public void setCollcode(String collcode)
		{
			this._collcode = collcode;
		}
		public string getCollname() 
		{
			return _collname;
		}

		public void setCollname(String collname)
		{
			this._collname = collname;
		}

		public void reset()
		{
			_id = 0;
			_Sysmsgid = 0;
			_go_user = 0;
			_to_user = 0;

			_msgtitle = "";
			_msgcontent = "";
	

		}
	
	}
}
