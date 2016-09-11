using System;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// 排序类  author:shenglianjun
	/// </summary>
	public class SortUtil
	{
		/// <summary>
		///  冒泡排序
		/// </summary>
		/// <param name="array">整型数组</param>
		public static void bubbleSort(int[] array)
		{
			for(int i=0;i<array.Length;i++)
			{
				
				for(int j=i;j<array.Length;j++)
				{
					if(array[i]>array[j])
					{
						int temp=array[i];
						array[i]=array[j];
						array[j]=temp;
						
					}
		
				}

				
			}
		}
		/// <summary>
		/// 选择排序
		/// </summary>
		/// <param name="array">整型数组</param>
		public static void selectionSort(int[] array)
		{
			for(int i=0;i<array.Length;i++)
			{
				int min=i;
				for(int j=i;j<array.Length;j++)
				{
					if(array[min]>array[j])
					{
                        min=j;
					}
				}
                int temp=array[i];
				array[i]=array[min];
				array[min]=temp;
			}
		}
		/// <summary>
		/// 插入排序
		/// </summary>
		/// <param name="array">整型数组</param>
		public static void insertionSort(int[] array)
		{
			for(int i=0;i<array.Length;i++)
			{
				int temp=array[i];
				int j=i;
				for( j=i;j>0;j--)
				{
					if(array[j-1]>temp)
					{
						array[j]=array[j-1];
					}
					else
					{
						break;
					}
				}
				array[j]=temp;
			}
		}
		
		/// <summary>
		/// 快速排序
		/// </summary>
		/// <param name="array"></param>
		/// <param name="i"></param>
		/// <param name="j"></param>
		public static void quicksort(int[] array,int i,int j)
		{
			int middle=array.Length/2;
			while(i<j)
			{
				while(i<=middle && array[i]<=array[middle]) i++;
				while(j>=middle && array[j]>=array[middle]) j--;
				if(i<j)
				{
					int temp=array[i];
					array[i]=array[j];
					array[j]=temp;
				}
			}
			quicksort(array,0,i);
			quicksort(array,i,array.Length-1);
 
		}
		public static void shellSort(int[]  array)
		{
           quicksort(array,0,array.Length-1);
           
		}
		
	}
}




 
