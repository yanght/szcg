using System;

namespace szcg.com.teamax.business.entity
{
	/// <summary>
	/// ProjInfo zhg-05-14 ��ժҪ˵����
	/// </summary>
	public struct ProjInfo
	{    
		/// <summary>
		/// �Ƿ񶽰�
		/// </summary>
		private string  _ispress;

		/*רҵ�����Ƿ��Ѵ���*/
		private string  _isProcess;
		/*�������*/
		private int  _projcode;
		/*��������*/
		private string _projname;  
		/*������Դ*/
		private string _probsource;  
		/*�������*/
		private string _probclass;  
        /*С����*/
		private string _bigclass; 
       /*������*/
		private string _smallclass; 
	   /*��������*/
	    private string _area;
		private string _street;
        private string _commname;
		private string _depart;
		private string _usercode;
		private string _square; 
		private int _allrow;
		private string _address; 
		private string _probdesc; 
		/*������*/
		private string _receiver;
		/*��������*/
		private string _bigclassname;
         /*С������*/
		private string _smallclassname;

		private string _filestate;
       /*�ļ�״̬*/
		private string _soundstate;
		/*����״̬*/
		private string _rolecode;

		/*������*/
		private string _stepid;            
		/*�����״̬*/
		private string _stateid;
		/*��ɫ*/
		private int _role;
		/*�������¼����ͣ�*/
		private string _typecode;
		/*��ʼ��תʱ��*/

		private string _startdate;

        private string _stepdate;
		/*�����תʱ��*/
		private string _enddate;
		/*�����*/
		private string _gridcode;
		/*Ψһ��ʶ*/
		private string _fid;
        private string _streetcode;
        private string _commcode;
		/*�Ƿ����ⷢ��*/
		private string _release;
		/*����վ*/
		private string _isdel;

		/*����*/
		private string _num;

		/*ͼƬ*/
		private string  _soundpath;

		/*����*/
		private string _filepath;
		/*ͼƬʱ��*/
		private string _cudate;	
		/*�ٱ���*/
		private string _name; 	
		/*��ϵ��ʽ*/ 
		private string _tel;						  
		/*�ظ���ʽ*/
		private string _type;
		/*�ظ�����*/
		private string _retobject;
		private string _option;
		private string _redback;
		private string _isthrough;
		private string _isgreat;

		public ProjInfo(int projcode,string stepdate, string projname, string probsource, string probclass, 
			string bigclass, string smallclass,string area, string  street, string  square, string  address,string filestate,string soundstate,
			string probdesc, string stepid, string stateid, int role, string typecode, string startdate, string enddate, string gridcode,
			string fid, string streetcode,string commcode,string commname,string release,int allrow,string redback,string isgreat,string isthrough, string option,string usercode,string depart,string receiver,string smallclassname,string bigclassname,string rolecode, string isdel,string num,string soundpath, string filepath, string cudate,string name,string tel,string type,string retobject,string isProcess,string ispress)
		{
            this._stepdate = stepdate;
			this._isProcess = isProcess;
			this._redback = redback;
			this._option = option;
			this._usercode = usercode;
			this._depart = depart;
			this._projcode = projcode;
			this._projname = projname;
			this._probsource = probsource;
			this._probclass = probclass;
			this._bigclass = bigclass;
			this._rolecode = rolecode;
			this._receiver=receiver;
			this._smallclassname = smallclassname;
			this._bigclassname = bigclassname;
			this._allrow = allrow;
			this._smallclass = smallclass;
			this._filestate=filestate;
			this._soundstate=soundstate;
			this._area=area;
			this._isthrough=isthrough;
			this._street = street;
			this._isgreat = isgreat;
			this._square = square;
            this._commname = commname;

			this._address = address;
			this._probdesc  = probdesc;

			this._stepid = stepid;
			this._stateid = stateid;
			this._role  = role;
			this._typecode = typecode;
			this._startdate= startdate;
			this._enddate = enddate;

			this._gridcode= gridcode;
			this._fid = fid;
            this._streetcode = streetcode;
            this._commcode = commcode;
			this._release  = release;
			this._isdel = isdel;
			this._num =num;
			this._soundpath=soundpath;
			this._filepath=filepath;
			this._cudate=cudate;
			this._name=name;
			this._tel=tel;
			this._type=type;
			this._retobject=retobject;
			this._ispress = ispress;
		}

		/// <summary>
		/// �Ƿ񶽰츳ֵ
		/// </summary>
		/// <param name="ispress"></param>
		public void setIspress(string ispress)
		{
			this._ispress = ispress;
		}

		/// <summary>
		/// �Ƿ񶽰�ȡֵ
		/// </summary>
		/// <returns></returns>
		public string getIspress()
		{
			return this._ispress;
		}

		public void setIsProcess(string isProcess)
		{
			this._isProcess = isProcess;
		}

		public string getIsProcess()
		{
			return this._isProcess;
		}

		public void setDepart(string depart)
		{
			this._depart = depart;
		}

		public void setAllrow(int allrow)
		{
			this._allrow = allrow;
		}

		public void setIsthrough(string isthrough)
		{
			this._isthrough = isthrough;
		}

		public string getIsthrough()
		{
			return this._isthrough;
		}
        public void setCommname(string commname)
        {
            this._commname = commname;
        }
        public string getCommname()
        {
            return this._commname;
        }

		public void setIsgreat(string isgreat)
		{
			this._isgreat = isgreat;
		}

		public string getIsgreat()
		{
			return this._isgreat;
		}


		public void setRedback(string redback)
		{
			this._redback = redback;
		}

		public string getRedback()
		{
			return this._redback;
		}

		public int getAllrow()
		{
			return this._allrow;
		}

		public string getDepart()
		{
			return this._depart;
		}

		public void setOpinion(string option)
		{
			this._option = option;
		}

		public string getOpinion()
		{
			return this._option;
		}

		public void setUsercode(string usercode)
		{
			this._usercode = usercode;
		}

		public string getUsercode()
		{
			return this._usercode;
		}
		public string getReceiver()
		{
		   return this._receiver;
		}
		public void setReceiver(string receiver)
		{
		 this._receiver=receiver;
		}
		public void setSmallclassname(string smallclassname)
		{
			this._smallclassname = smallclassname;
		}

		public string getSmallclassname()
		{
			return this._smallclassname;
		}

		public void setBigclassname(string bigclassname)
		{
			this._bigclassname = bigclassname;
		}

		public string getBigclassname()
		{
			return this._bigclassname;
		}

		public void setFilestate(string filestate)
		{
			this._filestate = filestate;
		}

		public string getFilestate()
		{
			return this._filestate;
		}

		public void setSoundstate(string soundstate)
		{
			this._soundstate = soundstate;
		}

		public string getSoundstate()
		{
			return this._soundstate;
		}
		/*zhouli05-25*/
		public string getRolecode()
		{
			return this._rolecode;
		}

		public void setRolecode(string rolecode)
		{
			this._rolecode=rolecode;
		}

		public int getProjcode()
		{
			return _projcode;
		}
		public void setProjcode(int projcode) 
		{
			this._projcode = projcode;
		}
 

		public String getProjname()
		{
			return _projname;
		}
		public void setProjname(String projname)
		{
			this._projname = projname;
		}

		public String getProbsource()
		{
			return _probsource;
		}
		public void setProbsource(String probsource)
		{
			this._probsource = probsource;
		}

		public String getProbclass()
		{
			return _probclass;
		}
		public void setProbclass(String probclass)
		{
			this._probclass= probclass;
		}

		public String getBigclass()
		{
			return _bigclass;
		}
		public void setBigclass (String bigclass)
		{
			this._bigclass = bigclass;
		}

		public String getSmallclass()
		{
			return _smallclass;
		}
		public void setSmallclass(String smallclass)
		{
			this._smallclass = smallclass;
		}

        /*zhanghuagen0529*/
		public String getArea()
		{
			return _area;
		}
		public void setArea(String area)
		{
			this._area = area;
		}

		public String getStreet()
		{
			return _street;
		}
		public void setStreet(String street)
		{
			this._street = street;
		}

		public String getSquare()
		{
			return _square;
		}
		public void setSquare(String square)
		{
			this._square = square;
		}
		public String getAddress()
		{
			return _address;
		}
		public void setAddress(String address)
		{
			this._address = address;
		}
		public String getProbdesc()
		{
			return _probdesc;
		}
		public void setProbdesc(String probdesc)
		{
			this._probdesc = probdesc;
		}

		public String getStepid()
		{
			return _stepid;
		}
		public void setStepid(String stepid)
		{
			this._stepid = stepid;
		}
		public String getStateid()
		{
			return _stateid;
		}
		public void setStateid(String stateid)
		{
			this._stateid = stateid;
		}

		
		public int getRole()
		{
			return _role;
		}
		public void setRole(int role) 
		{
			this._role = role;
		}
 
		public String getTypecode()
		{
			return _typecode;
		}
		public void setTypecode(String typecode)
		{
			this._typecode = typecode;
		}
		public String getStartdate()
		{
			return _startdate;
		}
		public void setStartdate(String startdate)
		{
			this._startdate = startdate;
		}

        public String getStepdate()
        {
            return _stepdate;
        }
        public void setStepdate(String stepdate)
        {
            this._stepdate = stepdate;
        }

		public String getEnddate()
		{
			return _enddate;
		}
		public void setEnddate(String enddate)
		{
			this._enddate = enddate;
		}

		public String getGridcode()
		{
			return _gridcode;
		}
		public void setGridcode(String gridcode)
		{
			this._gridcode = gridcode;
		}
        public String getCommcode()
        {
            return _commcode;
        }
        public void setCommcode(String commcode)
        {
            this._commcode = commcode;
        }
		public String getFid()
		{
			return _fid;
		}
		public void setFid(String fid)
		{
			this._fid = fid;
		}
        public String getStreetcode()
        {
            return _streetcode;
        }
        public void setStreetcode(String streetcode)
        {
            this._streetcode = streetcode;
        }
		public String getRelease()
		{
			return _release;
		}
		public void setRelease(String release)
		{
			this._release = release;
		}

		public String getIsdel()
		{
			return _isdel;
		}
		public void setIsdel(String isdel)
		{
			this._isdel = isdel;
		}

		public String getNum()
		{
			return _num;
		}
		public void setNum(String  num)
		{
			this._num = num;
		}

		public String getSoundpath()
		{
			return _soundpath;
		}
		public void setSoundpath(String  soundpath)
		{
			this._soundpath = soundpath;
		}
		public String  getFilepath()
		{
			return _filepath;
		}
		public void setFilepath(String filepath)
		{
			this._filepath = filepath;
		}
		public String getCudate()
		{
			return _cudate;
		}
		public void setCudate(String  cudate)
		{
			this._cudate = cudate;
		}
		public String getName()
		{
			return _name;
		}
		public void setName(String  name)
		{
			this._name = name;
		}
		public String getTel()
		{
			return _tel;
		}
		public void setTel(String  tel)
		{
			this._tel = tel;
		}
		public String getType()
		{
			return _type;
		}
		public void setType(String  type)
		{
			this._type = type;
		}
		public String getRetobject()
		{
			return _retobject;
		}
		public void setRetobject(String  retobject)
		{
			this._retobject = retobject;
		}

	}
}
