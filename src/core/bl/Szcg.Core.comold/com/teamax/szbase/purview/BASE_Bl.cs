using System;

namespace szcg.com.teamax.szbase.purview
{
	/// <summary>
	/// BASE_Bl ��ժҪ˵����
	/// </summary>
	public class BASE_Bl
	{
		public static BASE_Bl instanse = null;

		public BASE_Bl()
		{
			
		}
		//��̬�ķ�����������ʼ���캯��
		public static BASE_Bl getInstanse()
		{
			if(instanse==null)
			{
				instanse=new BASE_Bl();
			}
			return instanse;
		}

		//���ݴ����string�����ɲ�����','������
		public string[] getModelId(string ids)
		{
			if(ids!=null && ids.Length>0 && ids.IndexOf(',')!=-1)
			{
				ids=ids.Substring(0,ids.Length-1);
			}
			
			string[] str = null;
			str = ids.Split(',');
			return str;
		}
	}
}
