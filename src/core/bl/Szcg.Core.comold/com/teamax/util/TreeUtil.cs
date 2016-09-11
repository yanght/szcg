using System;
using MicrosoftWebControls = Microsoft.Web.UI.WebControls;
using System.Data;

namespace szcg.com.teamax.util
{
	public class TreeUtil
	{
        private MicrosoftWebControls.TreeView TreeView1 = null;
		public TreeUtil()
		{
			
		}
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="TreeView1"></param>
        public TreeUtil(MicrosoftWebControls.TreeView TreeView1)
		{
			this.TreeView1=TreeView1;
		 
		}
         /// <summary>
         /// �õ�����ѡ�ֽڵ�
         /// </summary>
         /// <returns></returns>
		public  MicrosoftWebControls.TreeNode getSelectedTreeNode()
		{
             return this.TreeView1.GetNodeFromIndex(TreeView1.SelectedNodeIndex);   
		}
		/// <summary>
		/// ��ʼ����
		/// </summary>
		/// <param name="dataSet"></param>
		/// <param name="id">ID�ֶ�</param>
		/// <param name="parentid">��ID�ֶ�</param>
		/// <param name="text">����</param>
		/// <param name="nodeValue">ֵ</param>
		public void initTreeView(DataSet dataSet,string id,string parentid,string text,string nodeValue)
		{
		
			DataTable dataTable=dataSet.Tables[0];
			recursion(null,dataTable,id,parentid,text,nodeValue);
			this.TreeView1.ExpandLevel=3;	    
		}

		/// <summary>
		///  �����ɵĵݹ麯��
		/// </summary>
		/// <param name="parentNode">���ڵ�</param>
		/// <param name="dataTable">����</param>
		/// <param name="id">ID�ֶ�</param>
		/// <param name="parentid">��ID�ֶ�</param>
		/// <param name="text">�ڵ���ʾֵ</param>
		/// <param name="nodeValue">�ڵ�ʵ��ֵ</param>
		public void recursion(MicrosoftWebControls.TreeNode parentNode,DataTable dataTable,string id,string parentid,string text,string nodeValue)
		{
			if(this.TreeView1.Nodes.Count==0 || parentNode==null)
			{
				DataRow dataRow=dataTable.Rows[0];
				MicrosoftWebControls.TreeNode root=new MicrosoftWebControls.TreeNode();

				root.ID=Convert.ToString(dataRow[id]);
				root.Text=Convert.ToString(dataRow[text]);
				root.NodeData=Convert.ToString(dataRow[nodeValue]);
				

				this.TreeView1.Nodes.Add(root);
				recursion(root,dataTable,id,parentid,text,nodeValue);
			}
			else
			{

				DataRow[] rows=dataTable.Select(parentid+"="+parentNode.ID);
				for(int i=0;i<rows.Length;i++)
				{
					MicrosoftWebControls.TreeNode node=new MicrosoftWebControls.TreeNode();
					DataRow row=(DataRow)rows.GetValue(i);
					
					node.ID=Convert.ToString(row[id]);
					node.Text=Convert.ToString(row[text]);
					node.NodeData=Convert.ToString(row[nodeValue]);
					parentNode.Nodes.Add(node);
					recursion(node,dataTable,id,parentid,text,nodeValue);
				}
			}
		}
	}
}
