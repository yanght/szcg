/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���������ݷ��ʹ�����-�߼���
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-06-10 
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա������Ⱥ                
 * �޸����ڣ�2007-6-16 
 * �޸�˵���������� 
 * ****************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace bacgBL.Pub
{
	/// <summary>
	/// ������
	/// </summary>
	public class Tools
	{
		private static string char1="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`~!@#$%^&*()_+-=\\|{}[]:;'<>?,./";
		private static string IP = System.Configuration.ConfigurationManager.AppSettings.Get("IP");

        #region  StrConv���ַ�������
        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="strIn">�ַ���</param>
        /// <param name="encoding">��������</param>
        /// <returns></returns>
        public static string StrConv(string strIn, string encoding)
        {
            return System.Web.HttpUtility.UrlEncode(strIn, System.Text.Encoding.GetEncoding(encoding));
        }
        #endregion

        //�ַ���������;
        public static String[] strToArray(String str, String tag)
        {
            int n = 1;
            for (int ii = 0; ii < str.Length; ii++)
                if (str.ToCharArray()[ii] == tag.ToCharArray()[0])
                    n++;

            String[] list = new String[n];
            int i;
            for (i = 0; str.IndexOf(tag) != -1; i++)
            {
                int pos = str.IndexOf(tag);
                String pam = str.Substring(0, pos);
                list[i] = pam;
                str = str.Substring(pos + tag.Length);
            }

            list[i] = str;
            return list;
        }

        /**
         * ������source ����seprator �ϲ����ַ���
         * @param source Դ��
         * @param seprator �ϲ��ַ�
         * @return
         */
        public static String arrayToString(String[] source, String seprator)
        {
            String ret = "";
            if (source != null && source.Length > 0)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    ret = ret + source[i] + seprator;
                }
                ret = ret.Substring(0, ret.Length - 1);
            }
            else
                ret = "";
            return ret;
        }

        //��֤�Ƿ��������ַ�����;
        public static bool CheckIsGBK(string Tokstr)
        {
            if (Tokstr != null && Tokstr.Length > 0)
            {

                char[] carray = Tokstr.ToCharArray();
                for (int i = 0; i < carray.Length; i++)
                {

                    if (Tools.char1.IndexOf(carray[i]) == -1)
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        #region replaceUrl:��Url��ַתΪ�����ĵ�ַ
        public static  string replaceUrl(string oldurl)
		{
			
			int index = oldurl.IndexOf("bacgWeb");
			string newurl = oldurl.Substring(index, oldurl.Length - index);
			newurl = newurl.Replace("\\\\","/");
			
			return  "http://" + IP   + "/" +  newurl;
        }
        #endregion


        #region changeNull:��"null"ת��Ϊ""
        public static string changeNull(string str)
		{
			if(str.ToLower().Equals("null"))
				str="";
			return str;
        }
        #endregion

        //��֤�����Ƿ�����������
        public static bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }
	}
}
