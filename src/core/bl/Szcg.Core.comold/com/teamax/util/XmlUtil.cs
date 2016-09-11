using System;
using System.Xml;


namespace szcg.com.teamax.util
{
	/// <summary>
	/// Xml使用类 author:shenglianjun
	/// </summary>
	public class XmlUtil
	{
	
		/// <summary>
		/// XML初始化方法
		/// </summary>
		/// <param name="xmlFilePath"></param>
		/// <param name="document"></param>
		public static void init(string xmlFilePath,XmlDocument document)
		{
			document=new XmlDocument();
			document.Load(xmlFilePath);
		}
			
		/// <summary>
		/// 得到元素（节点）列表
		/// </summary>
		/// <param name="document">XmlDocument对象</param>
		/// <param name="elementName">元素名</param>
		/// <returns>元素节点列表</returns>
		public static XmlNodeList getElementList(XmlDocument document,string elementName)
		{
			XmlNodeList nodeList=document.GetElementsByTagName(elementName);
			return nodeList;
		}
	
		/// <summary>
		/// 得到元素（节点）值
		/// </summary>
		/// <param name="elementName">节点名称</param>
		/// <returns></returns>
		public static string getElementValue(XmlNode elementName)
		{
			return elementName.FirstChild.Value;
		}
		
		/// <summary>
		/// 得到元素（节点）属性值
		/// </summary>
		/// <param name="elementName">元素名称</param>
		/// <param name="attributeName">属性名称</param>
		/// <returns></returns>
		public static string getAttribute(XmlNode elementName,string attributeName)
		{
			XmlAttributeCollection attributes=elementName.Attributes;
			XmlAttribute attribute=attributes[attributeName];
			return attribute.InnerText; 

		}
		
		/// <summary>
		/// 得到元素（节点）属性数组
		/// </summary>
		/// <param name="elementName">元素名称</param>
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

