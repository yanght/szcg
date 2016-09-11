/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：对称加解密类。
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-05-21
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Teamax.Common
{
	/// <summary>
	/// 对称加密解密通用类库函数，适用于对用户密码等敏感数据简单加解密。
	/// </summary>
	/// <remarks>
	/// 如果要加密数据库连接字符串，请调用Teamax.Common.EncryptDecrypt.EncryptDecrypt类中的
	/// 两个静态方法：EncryptString、DecryptString来实现！
	/// </remarks>
	/// <example>
	/// <code>
	/// 示例1：
	/// //加密
	/// private string EnPassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES();
	///		des.CryptKey = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptIV  = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptText = PWD;
	///		return des.Encrypt();
	///	}
	/// //解密
	/// private string DePassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES();
	///		des.CryptKey = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptIV  = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptText = PWD;
	///		return des.Decrypt();
	///	}	
	///	
	///	
	/// 示例2：
	/// //加密
	/// private string EnPassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES(true);
	///		des.CryptText = PWD;
	///		return des.Encrypt();
	///	}
	/// //解密
	/// private string DePassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES();
	///		des.CryptText = PWD;
	///		return des.Decrypt();
	///	}	
	/// </code>
	/// </example>
	public class DES
	{
		private string _CryptText;  //待加密和解密的字符序列变量
		private byte[] _CryptKey;   //加密解密私钥变量
		private byte[] _CryptIV;    //加密解密初始化向量IV变量

		/// <summary>
		/// 待加密或解密的字符序列
		/// </summary>
		public string CryptText
		{
			set
			{
				_CryptText=value;
			}
			get
			{
				return _CryptText; 
			}
		}

		/// <summary>
		/// 加密私钥
		/// </summary>
		public byte[] CryptKey
		{
			set
			{
				_CryptKey=value;
			}
			get
			{
				return _CryptKey;
			}
		}

		/// <summary>
		/// 加密的初始化向量IV
		/// </summary>
		public byte[] CryptIV
		{
			set
			{
				_CryptIV=value;
			}
			get
			{
				return _CryptIV;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public DES()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 构造函数
		/// 初始化加密的私钥和初始化向量IV
		/// </summary>
		/// <remarks>
		/// RijndaelManaged类实例的BlockSize加密操作块大小默认为128， 与之匹配的IV长度应该为16位。
		/// </remarks>
		/// <param name="IsInit">是否要初始化私钥和IV</param>
		public DES(bool IsInit)
		{
			if(IsInit)
			{
				_CryptKey = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};	//32位
				_CryptIV  = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};	//16位
			}
		}

		/// <summary>
		/// 加密函数,
		/// 用于对字符串进行加密。需要提供相应的密钥和IV。
		/// </summary>
		/// <returns></returns>
		public string Encrypt()
		{
			byte[] inputByteArray=System.Text.Encoding.UTF8.GetBytes(CryptText);

			//此处也可以创建其他的解密类实例，但注意不同(长度)的加密类要求不同的密钥Key和初始化向量IV
			RijndaelManaged RMCrypto = new RijndaelManaged();			
			MemoryStream ms=new MemoryStream();
			CryptoStream cs=new CryptoStream(ms,RMCrypto.CreateEncryptor(CryptKey,CryptIV),CryptoStreamMode.Write);
			cs.Write(inputByteArray,0,inputByteArray.Length);
			cs.FlushFinalBlock();

			return Convert.ToBase64String(ms.ToArray());
		}

		/// <summary>
		/// 解密函数，
		/// 用于对经过加密的字符序列进行解密。需要提供相应的密钥和IV。
		/// </summary>
		/// <returns></returns>
		public string Decrypt()
		{
			byte[] inputByteArray=Convert.FromBase64String(CryptText);

			//此处也可以创建其他的解密类实例，但注意不同的加密类要求不同(长度)的密钥Key和初始化向量IV
			RijndaelManaged RMCrypto = new RijndaelManaged();
			MemoryStream ms=new MemoryStream();
			CryptoStream cs=new CryptoStream(ms,RMCrypto.CreateDecryptor(CryptKey,CryptIV),CryptoStreamMode.Write);
			cs.Write(inputByteArray,0,inputByteArray.Length);
			cs.FlushFinalBlock();

			return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        #region md5：md5加密
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32) //32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return null;
        }
        #endregion

        #region base64：base64加密
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string base64(string str)
        {
            string strResult = "";
            if (str != null && str.Length > 0)
            {
                strResult = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes(str));
            }
            return strResult;
        }
        #endregion

        #region base64：base64解密
        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string base641(string str)
        {
            string strResult = "";
            if (str != null && str.Length > 0)
            {
                strResult = System.Text.ASCIIEncoding.Default.GetString(Convert.FromBase64String(str));
            }
            return strResult;
        }
        #endregion
	}
}

