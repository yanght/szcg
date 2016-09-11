using System;

namespace szcg.com.teamax.util
{
	/// <summary>
	///����ʹ���� author:shenglianjun 
	/// </summary>
	public class DateUtil
	{
		/// <summary>
		/// �õ���ǰ����
		/// </summary>
		/// <returns></returns>
		public static string getCurrentDate()
		{
           return System.DateTime.Now.Year+"��"+System.DateTime.Now.Month+"��"+System.DateTime.Now.Date+"��";
		}
		/// <summary>
		/// ����ת������
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static string datetimeToString(DateTime dateTime)
		{
		    //return Convert.ToString(dateTime);
			string result=dateTime.Year+"��"+dateTime.Month+"��"+dateTime.Date+"��";
			return result;
		}
		/// <summary>
		/// �õ���ǰʱ��
		/// </summary>
		/// <returns></returns>
		public static string getCurrentTime()
		{
			string datetime=System.DateTime.Now.Year+"��"+System.DateTime.Now.Month+"��"+System.DateTime.Now.Date+"��"+
				System.DateTime.Now.Hour+"ʱ"+System.DateTime.Now.Minute+"��"+System.DateTime.Now.Second+"��";
			return datetime;
			
		}
    	/// <summary>
		/// �������ڼ�  ��ķ����ɭ���㹫ʽ W= (d+2*m+3*(m+1)/5+y+y/4-y/100+y/400) mod 7. ��һ�ºͶ��¿�������һ���ʮ���º�ʮ����
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
				case 1: weekday="����һ"; break;
				case 2: weekday="���ڶ�"; break;
				case 3: weekday="������"; break;
				case 4: weekday="������"; break;
				case 5: weekday="������"; break;
				case 6: weekday="������"; break;
				case 7: weekday="������"; break;
			}
           return weekday; 
	    }
		/// <summary>
		/// �õ�2����������ʱ������
		/// </summary>
		/// <param name="starttime">��ʼʱ��</param>
		/// <param name="endtime">����ʱ��</param>
		/// <returns></returns>
		public int getDateDistanse(DateTime starttime,DateTime endtime)
		{
			TimeSpan timeSpan=endtime-starttime;
			return timeSpan.Days;
		}
		
		//�õ�����ʱ������Сʱ�� 
		public static int getHourDistanse(DateTime starttime,DateTime endtime)
		{
			TimeSpan timeSpan = endtime-starttime;
			return timeSpan.Days*24 + timeSpan.Hours;
		}
	}
}
