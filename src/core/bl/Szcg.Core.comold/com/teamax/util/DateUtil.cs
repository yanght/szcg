using System;

namespace szcg.com.teamax.util
{
	/// <summary>
	///日期使用类 author:shenglianjun 
	/// </summary>
	public class DateUtil
	{
		/// <summary>
		/// 得到当前日期
		/// </summary>
		/// <returns></returns>
		public static string getCurrentDate()
		{
           return System.DateTime.Now.Year+"年"+System.DateTime.Now.Month+"月"+System.DateTime.Now.Date+"日";
		}
		/// <summary>
		/// 日期转化函数
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static string datetimeToString(DateTime dateTime)
		{
		    //return Convert.ToString(dateTime);
			string result=dateTime.Year+"年"+dateTime.Month+"月"+dateTime.Date+"日";
			return result;
		}
		/// <summary>
		/// 得到当前时间
		/// </summary>
		/// <returns></returns>
		public static string getCurrentTime()
		{
			string datetime=System.DateTime.Now.Year+"年"+System.DateTime.Now.Month+"月"+System.DateTime.Now.Date+"日"+
				System.DateTime.Now.Hour+"时"+System.DateTime.Now.Minute+"分"+System.DateTime.Now.Second+"秒";
			return datetime;
			
		}
    	/// <summary>
		/// 返回星期几  基姆拉尔森计算公式 W= (d+2*m+3*(m+1)/5+y+y/4-y/100+y/400) mod 7. 把一月和二月看成是上一年的十三月和十四月
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		public static string getWeekDay(DateTime datetime)
		{
			int year=datetime.Year;
			int month=datetime.Month;
			int day=datetime.Day;
			
			if(month==1) month=13;
			if(month==2) month=14;
			int week=(day+2*month+3*(month+1)/5+year+year/4-year/100+year/400)%7; 
			string weekday="";
			switch(week){
				case 1: weekday="星期一"; break;
				case 2: weekday="星期二"; break;
				case 3: weekday="星期三"; break;
				case 4: weekday="星期四"; break;
				case 5: weekday="星期五"; break;
				case 6: weekday="星期六"; break;
				case 7: weekday="星期日"; break;
			}
           return weekday; 
	    }
		/// <summary>
		/// 得到2个日期相差的时间天数
		/// </summary>
		/// <param name="starttime">开始时间</param>
		/// <param name="endtime">结束时间</param>
		/// <returns></returns>
		public int getDateDistanse(DateTime starttime,DateTime endtime)
		{
			TimeSpan timeSpan=endtime-starttime;
			return timeSpan.Days;
		}
		
		//得到两个时间相差的小时数 
		public static int getHourDistanse(DateTime starttime,DateTime endtime)
		{
			TimeSpan timeSpan = endtime-starttime;
			return timeSpan.Days*24 + timeSpan.Hours;
		}
	}
}
