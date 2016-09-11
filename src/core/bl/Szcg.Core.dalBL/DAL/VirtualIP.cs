using System;

namespace SZCG.GPS.DAL
{
	/// <summary>
	/// VirtualIP ��ժҪ˵����
	/// </summary>
	public class VirtualIP
	{
		public VirtualIP() {}

		public string Encode(string strSIM)
		{
			if (!this.IsNumber(strSIM))
				throw new Exception("SIM ���ű���Ϊ���֣��磺13512345678��");
			if (strSIM.Length != 11)
				throw new Exception("SIM ���ų��ȱ���Ϊ 11 λ��");
            //int intHead = int.Parse(strSIM.Substring(0, 3));
            //if (intHead < 130 || intHead > 145)
            //    throw new Exception("SIM ����ǰ 3 λ������ڵ��� 130 С�ڵ��� 145��");
            int intHead = int.Parse(strSIM.Substring(0, 2));
            if (intHead != 13 && intHead != 15)
            {
                throw new Exception("SIM ����ǰ 2 λ������ 13 �� 15 !");
            }
            int intHead1 = Convert.ToInt32(strSIM.Substring(0, 3));
            
            int intMend = 0;
            if (intHead == 13)
            {
                intMend = intHead1 - 130;
            }
            else 
            if(intHead == 15)
            {
                intMend = intHead1 - 150;
            }
              //int intMend = intHead - 130;

			int intMend_1st = intMend >> 3;
			int intMend_2nd = (intMend & 4) >> 2;
			int intMend_3rd = (intMend & 2) >> 1;
			int intMend_4th = (intMend & 1);

			int intSect_1st = int.Parse(strSIM.Substring(3, 2)) + (intMend_1st << 7);
			int intSect_2nd = int.Parse(strSIM.Substring(5, 2)) + (intMend_2nd << 7);
			int intSect_3rd = int.Parse(strSIM.Substring(7, 2)) + (intMend_3rd << 7);
			int intSect_4th = int.Parse(strSIM.Substring(9, 2)) + (intMend_4th << 7);

			return string.Format(
				"{0}{1}{2}{3}", 
				string.Format("{0:X}", intSect_1st).PadLeft(2, '0'), 
				string.Format("{0:X}", intSect_2nd).PadLeft(2, '0'), 
				string.Format("{0:X}", intSect_3rd).PadLeft(2, '0'), 
				string.Format("{0:X}", intSect_4th).PadLeft(2, '0')
				);
		}

		public string Decode(string strVIP)
		{
			if (strVIP.Length != 8)
				throw new Exception("α IP ���ȱ���Ϊ 8 λ��");
			if (!this.IsHex(strVIP))
				throw new Exception("α IP ����Ϊ 16 ���ơ�");

			int intSect_1st = Convert.ToInt32(strVIP.Substring(0, 2), 16);
			int intSect_2nd = Convert.ToInt32(strVIP.Substring(2, 2), 16);
			int intSect_3rd = Convert.ToInt32(strVIP.Substring(4, 2), 16);
			int intSect_4th = Convert.ToInt32(strVIP.Substring(6, 2), 16);

			int intMend_1st = (intSect_1st & 128) >> 7;
			int intMend_2nd = (intSect_2nd & 128) >> 7;
			int intMend_3rd = (intSect_3rd & 128) >> 7;
			int intMend_4th = (intSect_4th & 128) >> 7;

			int intMend = (intMend_1st << 3) + (intMend_2nd << 2) + (intMend_3rd << 1) + intMend_4th;

			int intHead = intMend + 130;

			intSect_1st &= 127;
			intSect_2nd &= 127;
			intSect_3rd &= 127;
			intSect_4th &= 127;

			return string.Format(
				"{0}{1}{2}{3}{4}", 
				intHead, 
				intSect_1st.ToString().PadLeft(2, '0'), 
				intSect_2nd.ToString().PadLeft(2, '0'), 
				intSect_3rd.ToString().PadLeft(2, '0'), 
				intSect_4th.ToString().PadLeft(2, '0')
				);
		}

		private bool IsNumber(string strSIM)
		{
			bool isNumber = false;
			try
			{
				long.Parse(strSIM);
				isNumber = true;
			}
			catch {}
			return isNumber;
		}

		private bool IsHex(string strVIP)
		{
			bool isHex = false;
			try
			{
				Convert.ToInt32(strVIP, 16);
				isHex = true;
			}
			catch {}
			return isHex;
		}
	}
}
