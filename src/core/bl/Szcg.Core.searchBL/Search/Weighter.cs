using System;

namespace bacgBL.Search
{
	public class Weighter
	{
		private DateTime currentDateTime;
		public Weighter()
		{
			currentDateTime = DateTime.Now;
		}

		public int Calculate(DateTime CreatedDate)
		{
			int totalScore = 0;
			totalScore += ScoreDate(CreatedDate); 
			return totalScore;
		}

		protected int ScoreDate(DateTime CreatedDate)
		{
			TimeSpan ts = currentDateTime - CreatedDate;
			int days = ts.Days;
			int w = 0;
			if(days <= 360)
			{
                w = (days - 360)*100 * -1;
			}
			return w;
		}
	}
}
