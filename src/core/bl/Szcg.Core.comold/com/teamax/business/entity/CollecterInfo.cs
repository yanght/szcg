using System;

namespace szcg.com.teamax.business.entity
{
	/// <summary>
	/// CollecterInfo 的摘要说明。
	/// </summary>
	public class CollecterInfo
	{
		public  int cu_pags = 0;
		public  int cu_rows = 0;
		private string version;
		private int areacode;
		/*监督员工号*/
		private string collcode;
		private string collname;
		/*监督员电话*/
		private string numbercode;
		private string sex;
		private string age;
		private string url;
		private string memo;
		private string gridcode;
		private string projcode;
		/*区域名称*/
		private string areaname;
		/*街道*/
		private string  street;
		/*社区*/
		private string  square;
		/*FID*/
		//private string  fid;
		/*是否在岗*/
		private string  isguard;
         /*电话*/
		private string mobile;
		//监督员X坐标
		private string collecterX;
		//监督员Y坐标
		private string collecterY;

		private string hometel;

		private string homeaddress;

		public CollecterInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			reset();
		}
		public CollecterInfo(string[] data)
		{
		    reset();

			this.collname=data[0];
			this.age=data[1];
			this.sex=data[2];
			this.memo=data[3];
		    this.areacode=Convert.ToInt32(data[4]);
			this.url=data[5];
			this.gridcode=data[6];
		}
		public string getCollecterX()
		{
			return this.collecterX;
		}

		public string getHometel()
		{
			return this.hometel;
		}

		public void setHometel(string hometel)
		{
			this.hometel = hometel;
		}

		public string getHomeaddress()
		{
			return this.homeaddress;
		}

		public void setHomeaddress(string homeaddress)
		{
			this.homeaddress = homeaddress;
		}

		public void setVersion(string version)
		{
			this.version = version;
		}

		public string getVersion()
		{
			return this.version;
		}

		public void setCollecterX(string collecterX)
		{
			this.collecterX=collecterX;
		}

		public string getCollecterY()
		{
			return this.collecterY;
		}

		public void setCollecterY(string collecterY)
		{
			this.collecterY=collecterY;
		}

		public string getCollcode()
		{
			return this.collcode;
		}
		public void setCollcode(string collcode)
		{ 
			this.collcode=collcode;  
		}

		public int getAreaCode()
		{
			return this.areacode;
		}
		public void setAreaCode(int areacode)
		{ 
			this.areacode=areacode;  
		}
		public string  getCollName()
		{
			return this.collname;
		}
		public void setCollName(string collname)
		{
			this.collname=collname;
		}

		public string  getNumbercode()
		{
			return this.numbercode;
		}
		public void setNumbercode(string numbercode)
		{
			this.numbercode=numbercode;
		}

		public string getSex()
		{
			return this.sex;
		}
		public void setSex(string sex)
		{
			this.sex=sex;
		}
		public string getAge()
		{
			return this.age;
		}
		public void setAge(string age)
		{
			this.age=age;
		}
		public string getUrl()
		{
			return this.url;
		}
		public void seturl(string url)
		{
			this.url=url;
		}
		public string getMemo()
		{
			return this.memo;
		}
		public void setMemo(string memo)
		{
			this.memo=memo;
		}
		public string getGridCode()
		{
			return this.gridcode;
		}
		public void setGridCode(string gridcode)
		{
			this.gridcode=gridcode;
		}
		public string getMobile()
		{
			return this.mobile;
		}

		public void setMobile(string mobile)
		{
			this.mobile = mobile;
		}
//		public int getAreaCode()
//		{
//		  return this.areacode;
//		}
//		public void setAreaCode(int areacode)
//	    { 
//		  this.areacode=areacode;  
//		}
//		public string  getCollName()
//		{
//		  return this.collname;
//		}
//		public void setCollName(string collname)
//		{
//		  this.collname=collname;
//		}
//		public string getSex()
//		{
//		 return this.sex;
//		}
//		public void setSex(string sex)
//		{
//		  this.sex=sex;
//		}
//		public string getAge()
//		{
//		 return this.age;
//		}
//		public void setAge(string age)
//		{
//		  this.age=age;
//		}
//		public string getUrl()
//		{
//		  return this.url;
//		}
//		public void seturl(string url)
//		{
//		  this.url=url;
//		}
//		public string getMemo()
//		{
//		  return this.memo;
//		}
//		public void setMemo(string memo)
//		{
//		  this.memo=memo;
//		}
//		public string getGridCode()
//		{
//		  return this.gridcode;
//		}
//		public void setGridCode(string gridcode)
//		{
//		 this.gridcode=gridcode;
//		}

		/*zhanghuagen*/

		public string getProjcode()
		{
			return this.projcode;
		}
		public void setProjcode(string projcode)
		{
			this.projcode=projcode;
		}

		public string getAreaname()
		{
			return this.areaname;
		}
		public void setAreaname(string areaname)
		{
			this.areaname=areaname;
		}

		public string getStreet()
		{
			return this.street;
		}
		public void setStreet(string street)
		{
			this.street=street;
		}

		public string getSquare()
		{
			return this.square;
		}
		public void setSquare(string square)
		{
			this.square=square;
		}

		public string getIsguard()
		{
			return this.isguard;
		}
		public void setIsguard(string isguard)
		{
			this.isguard=isguard;
		}

		public void reset()
		{
		 areacode=0;
		 collname="";
		 sex="";
		 age="";
		 url="";
		 memo="";
	     gridcode="";
		}
	}
}






























