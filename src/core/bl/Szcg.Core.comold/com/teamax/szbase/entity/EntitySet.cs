using System.Collections;

namespace szcg.com.teamax.szbase.entity
{
	/// <summary>
	/// 实体集合
	/// </summary>
	public class EntitySet
	{
		private ArrayList objects = new ArrayList();
		private int totalRecords = 0;

		public int TotalRecords
		{
			get { return totalRecords; }
			set { totalRecords = value; }
		}

		public ArrayList Objects
		{
			get { return objects; }
		}

		public bool HasResults
		{
			get
			{
				if (objects.Count > 0)
					return true;
				return false;
			}
		}
	}
}