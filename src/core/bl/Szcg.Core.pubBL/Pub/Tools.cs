/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：公用数据访问工具类-逻辑层
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-06-10 
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：王超群                
 * 修改日期：2007-6-16 
 * 修改说明：整理函数 
 * ****************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace bacgBL.Pub
{
	/// <summary>
	/// 工具类
	/// </summary>
	public class Tools
	{
		private static string char1="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`~!@#$%^&*()_+-=\\|{}[]:;'<>?,./";
		private static string IP = System.Configuration.ConfigurationManager.AppSettings.Get("IP");

        #region  StrConv：字符串编码
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="strIn">字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string StrConv(string strIn, string encoding)
        {
            return System.Web.HttpUtility.UrlEncode(strIn, System.Text.Encoding.GetEncoding(encoding));
        }
        #endregion

        //字符串到数组;
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
         * 将数组source 根据seprator 合并成字符串
         * @param source 源串
         * @param seprator 合并字符
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

        //验证是否有中文字符存在;
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

        #region replaceUrl:将Url地址转为正常的地址
        public static  string replaceUrl(string oldurl)
		{
			
			int index = oldurl.IndexOf("bacgWeb");
			string newurl = oldurl.Substring(index, oldurl.Length - index);
			newurl = newurl.Replace("\\\\","/");
			
			return  "http://" + IP   + "/" +  newurl;
        }
        #endregion


        #region changeNull:把"null"转换为""
        public static string changeNull(string str)
		{
			if(str.ToLower().Equals("null"))
				str="";
			return str;
        }
        #endregion

        //验证输入是否是整形数字
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
