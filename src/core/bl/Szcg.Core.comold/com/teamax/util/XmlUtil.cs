using System;
using System.Xml;


namespace szcg.com.teamax.util
{
	/// <summary>
	/// Xmlʹ���� author:shenglianjun
	/// </summary>
	public class XmlUtil
	{
	
		/// <summary>
		/// XML��ʼ������
		/// </summary>
		/// <param name="xmlFilePath"></param>
		/// <param name="document"></param>
		public static void init(string xmlFilePath,XmlDocument document)
		{
			document=new XmlDocument();
			document.Load(xmlFilePath);
		}
			
		/// <summary>
		/// �õ�Ԫ�أ��ڵ㣩�б�
		/// </summary>
		/// <param name="document">XmlDocument����</param>
		/// <param name="elementName">Ԫ����</param>
		/// <returns>Ԫ�ؽڵ��б�</returns>
		public static XmlNodeList getElementList(XmlDocument document,string elementName)
		{
			XmlNodeList nodeList=document.GetElementsByTagName(elementName);
			return nodeList;
		}
	
		/// <summary>
		/// �õ�Ԫ�أ��ڵ㣩ֵ
		/// </summary>
		/// <param name="elementName">�ڵ�����</param>
		/// <returns></returns>
		public static string getElementValue(XmlNode elementName)
		{
			return elementName.FirstChild.Value;
		}
		
		/// <summary>
		/// �õ�Ԫ�أ��ڵ㣩����ֵ
		/// </summary>
		/// <param name="elementName">Ԫ������</param>
		/// <param name="attributeName">��������</param>
		/// <returns></returns>
		public static string getAttribute(XmlNode elementName,string attributeName)
		{
			XmlAttributeCollection attributes=elementName.Attributes;
			XmlAttribute attribute=attributes[attributeName];
			return attribute.InnerText; 

		}
		
		/// <summary>
		/// �õ�Ԫ�أ��ڵ㣩��������
		/// </summary>
		/// <param name="elementName">Ԫ������</param>
		/// <returns></returns>
		public static string[] getAtrributeList(XmlNode elementName)
		{
            
			XmlAttributeCollection attributes=elementName.Attributes;
			if(attributes==null || attributes.Count==0)
			{
				return null;
			}
			string[] array=new string[attributes.Count];
			for(int i=0;i<attributes.Count;i++)
			{
				XmlAttribute attribute=attributes[i];
				array[i]=attribute.Name;
			}
			return array;
		}
	}
}

