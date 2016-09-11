/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���ԳƼӽ����ࡣ
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Teamax.Common
{
	/// <summary>
	/// �ԳƼ��ܽ���ͨ����⺯���������ڶ��û�������������ݼ򵥼ӽ��ܡ�
	/// </summary>
	/// <remarks>
	/// ���Ҫ�������ݿ������ַ����������Teamax.Common.EncryptDecrypt.EncryptDecrypt���е�
	/// ������̬������EncryptString��DecryptString��ʵ�֣�
	/// </remarks>
	/// <example>
	/// <code>
	/// ʾ��1��
	/// //����
	/// private string EnPassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES();
	///		des.CryptKey = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptIV  = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};
	///		des.CryptText = PWD;
	///		return des.Encrypt();
	///	}
	/// //����
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
	/// ʾ��2��
	/// //����
	/// private string EnPassword(string PWD)
	///	{
	///		Teamax.Common.DES des = new Teamax.Common.DES(true);
	///		des.CryptText = PWD;
	///		return des.Encrypt();
	///	}
	/// //����
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
		private string _CryptText;  //�����ܺͽ��ܵ��ַ����б���
		private byte[] _CryptKey;   //���ܽ���˽Կ����
		private byte[] _CryptIV;    //���ܽ��ܳ�ʼ������IV����

		/// <summary>
		/// �����ܻ���ܵ��ַ�����
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
		/// ����˽Կ
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
		/// ���ܵĳ�ʼ������IV
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
		/// ���캯��
		/// </summary>
		public DES()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���캯��
		/// ��ʼ�����ܵ�˽Կ�ͳ�ʼ������IV
		/// </summary>
		/// <remarks>
		/// RijndaelManaged��ʵ����BlockSize���ܲ������СĬ��Ϊ128�� ��֮ƥ���IV����Ӧ��Ϊ16λ��
		/// </remarks>
		/// <param name="IsInit">�Ƿ�Ҫ��ʼ��˽Կ��IV</param>
		public DES(bool IsInit)
		{
			if(IsInit)
			{
				_CryptKey = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};	//32λ
				_CryptIV  = new byte[] {8,1,7,5,1,2,1,6,8,1,7,5,1,2,1,6};	//16λ
			}
		}

		/// <summary>
		/// ���ܺ���,
		/// ���ڶ��ַ������м��ܡ���Ҫ�ṩ��Ӧ����Կ��IV��
		/// </summary>
		/// <returns></returns>
		public string Encrypt()
		{
			byte[] inputByteArray=System.Text.Encoding.UTF8.GetBytes(CryptText);

			//�˴�Ҳ���Դ��������Ľ�����ʵ������ע�ⲻͬ(����)�ļ�����Ҫ��ͬ����ԿKey�ͳ�ʼ������IV
			RijndaelManaged RMCrypto = new RijndaelManaged();			
			MemoryStream ms=new MemoryStream();
			CryptoStream cs=new CryptoStream(ms,RMCrypto.CreateEncryptor(CryptKey,CryptIV),CryptoStreamMode.Write);
			cs.Write(inputByteArray,0,inputByteArray.Length);
			cs.FlushFinalBlock();

			return Convert.ToBase64String(ms.ToArray());
		}

		/// <summary>
		/// ���ܺ�����
		/// ���ڶԾ������ܵ��ַ����н��н��ܡ���Ҫ�ṩ��Ӧ����Կ��IV��
		/// </summary>
		/// <returns></returns>
		public string Decrypt()
		{
			byte[] inputByteArray=Convert.FromBase64String(CryptText);

			//�˴�Ҳ���Դ��������Ľ�����ʵ������ע�ⲻͬ�ļ�����Ҫ��ͬ(����)����ԿKey�ͳ�ʼ������IV
			RijndaelManaged RMCrypto = new RijndaelManaged();
			MemoryStream ms=new MemoryStream();
			CryptoStream cs=new CryptoStream(ms,RMCrypto.CreateDecryptor(CryptKey,CryptIV),CryptoStreamMode.Write);
			cs.Write(inputByteArray,0,inputByteArray.Length);
			cs.FlushFinalBlock();

			return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        #region md5��md5����
        /// <summary>
        /// md5����
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            if (code == 16) //16λMD5���ܣ�ȡ32λ���ܵ�9~25�ַ��� 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32) //32λ���� 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return null;
        }
        #endregion

        #region base64��base64����
        /// <summary>
        /// base64����
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

        #region base64��base64����
        /// <summary>
        /// base64����
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

