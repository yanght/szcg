using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;



namespace szcg.com.teamax.util
{
	/// <summary>
	///   ����EXCEL���� author:shenglianjun
	/// </summary>
	public class ExcelReport
	{
		

		/// <summary>
		/// һά����ת��Ϊ��ά����
		/// </summary>
		/// <param name="str">һά����</param>
		/// <returns>��ά����</returns>
		public static  String[][] convertArray(String[] str)
		{
			String[][] array=new String[str.Length][];
			for(int i=0;i<str.Length;i++)
			{
				String[] temp=str[i].Split(',');
				array[i]=new String[temp.Length];
				for(int j=0;j<temp.Length;j++)
				{
					int index=temp[j].IndexOf("=");
                    String myValue=temp[j].Substring(index+1);
					array[i][j]=myValue;
				}
			}
			return array;
		}
		
		/// <summary>
		/// �ö�ά���鴴��EXCEL��ͨ�ã�
		/// </summary>
		/// <param name="str">Ϊ��������</param>
		/// <param name="head">head�Ǳ���</param>
		public static void createExcelByPlanarArray(String[][] str,String head)
		{
            //Excel.ApplicationClass app=new Excel.ApplicationClass();
            //app.UserControl=false;
            //Excel.WorkbookClass wb=(Excel.WorkbookClass)app.Workbooks.Add(System.Reflection.Missing.Value);
            //app.Cells[1,4]="����";
            //if(head!=null && (!head.Equals("")))
            //{
            //    String[] headArray=head.Split(',');
            //    for(int i=0;i<headArray.Length;i++)
            //    {
            //        app.Cells[2,i+1]=headArray[i];
            //    }
            //}
            //for(int i=0;i<str.Length;i++){
            //   for(int j=0;j<str[i].Length;j++){
            //      app.Cells[i+3,j+1]=str[i][j];
            //   }
            //}
            //wb.Saved=true;
			
            //app.ActiveWorkbook.SaveCopyAs("C:/Inetpub/wwwroot/citygrid/zhpj/report/ttt.xls");
            //wb.Close(null,null,null);
            //app.Workbooks.Close();
            //app.Application.Quit();
            //app.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            //wb=null;
            //app=null;
            //System.GC.Collect();
            //Process[] excelProcess=Process.GetProcessesByName("EXCEL");
            //if(excelProcess!=null && excelProcess.Length>0)
            //{
            //    excelProcess[excelProcess.Length-1].Kill();
            //}
				
			
		}
	}
}
