using System;
using System.IO;
using System.Text;
using System.Resources;
using System.Collections;
using System.Drawing;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// 文件使用类 author:shenglianjun
	/// </summary>
	public class FileUpload
	{
		/// <summary>
		/// 文件上传方法
		/// </summary>
		/// <param name="b">文件流数组</param>
		/// <param name="fileName">文件名</param>
		/// <param name="filePath">文件路径</param>
		/// <returns>返回值为是否上传成功</returns>
		public static bool uploadFile(byte[] b,string fileName,string filePath)
		{
			try
			{
				if(filePath==null)
				{
                    filePath = System.Configuration.ConfigurationManager.AppSettings["UloadFilePath"];
				}
		        filePath=filePath+fileName;
				
				MemoryStream memoryStream=new MemoryStream(b);
				FileStream fileStream=new FileStream(filePath,FileMode.Create);
				memoryStream.WriteTo(fileStream);
				memoryStream.Close();
				fileStream.Close();
				memoryStream=null;
				fileStream=null;
				return true;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;  
			}
			
		}
		/// <summary>
		/// 读文件
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>文件内容</returns>
		public static string readFileToString(string filePath)
		{
			try
			{
				StreamReader reader=new StreamReader(filePath,System.Text.Encoding.Default);
				StringBuilder sb=new StringBuilder();
				String str;
				while((str=reader.ReadLine())!=null)
					sb.Append(str+"\r\n");
				reader.Close();
				return sb.ToString();
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return null;
			}
			
		}
		/// <summary>
		/// 写文件
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="content">写入内容</param>
		/// <returns>是否写入成功</returns>
		public static bool writeStringToFile(string filePath,string content)
		{
			try
			{
				StreamWriter writer=new StreamWriter(filePath,false,System.Text.Encoding.Default);
				//writer.WriteLine("士大夫大师傅");
				writer.WriteLine(content);
				writer.Flush();
				writer.Close();
				return true;
			}
			catch(Exception e)
			{
                System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}
			
		}
		/// <summary>
		/// 写资源文件
		/// </summary>
		/// <param name="resourcefile">资源文件名</param>
		/// <param name="values">string资源</param>
		/// <param name="images">images资源</param>
		/// <returns>是否创建成功</returns>
		public static bool createResource(string resourcefile,Hashtable values,Hashtable images)
		{
			ResourceWriter writer=null;
			try
			{
				
				writer=new ResourceWriter(resourcefile);
				if(values!=null)
				{
			     System.Collections.IEnumerator colls=values.Keys.GetEnumerator();
					while(colls.MoveNext())
					{
						string key=(string)colls.Current;
						string val=(string)values[key];
                        writer.AddResource(key,val);
					}
				}
				if(images!=null)
				{
					System.Collections.IEnumerator c=images.Keys.GetEnumerator();
					while(c.MoveNext())
					{
						string key=(string)c.Current;
						Image image=(Image)images[key];
						writer.AddResource(key,image);
					}                 
					
				}
		           
				writer.Generate();
				writer.Close();
                return true;
			}
			catch(Exception e)
			{
                writer.Close();
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}
			
		}
	}
}
