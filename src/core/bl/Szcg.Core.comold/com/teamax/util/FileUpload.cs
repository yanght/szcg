using System;
using System.IO;
using System.Text;
using System.Resources;
using System.Collections;
using System.Drawing;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// �ļ�ʹ���� author:shenglianjun
	/// </summary>
	public class FileUpload
	{
		/// <summary>
		/// �ļ��ϴ�����
		/// </summary>
		/// <param name="b">�ļ�������</param>
		/// <param name="fileName">�ļ���</param>
		/// <param name="filePath">�ļ�·��</param>
		/// <returns>����ֵΪ�Ƿ��ϴ��ɹ�</returns>
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
		/// ���ļ�
		/// </summary>
		/// <param name="filePath">�ļ�·��</param>
		/// <returns>�ļ�����</returns>
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
		/// д�ļ�
		/// </summary>
		/// <param name="filePath">�ļ�·��</param>
		/// <param name="content">д������</param>
		/// <returns>�Ƿ�д��ɹ�</returns>
		public static bool writeStringToFile(string filePath,string content)
		{
			try
			{
				StreamWriter writer=new StreamWriter(filePath,false,System.Text.Encoding.Default);
				//writer.WriteLine("ʿ����ʦ��");
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
		/// д��Դ�ļ�
		/// </summary>
		/// <param name="resourcefile">��Դ�ļ���</param>
		/// <param name="values">string��Դ</param>
		/// <param name="images">images��Դ</param>
		/// <returns>�Ƿ񴴽��ɹ�</returns>
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
