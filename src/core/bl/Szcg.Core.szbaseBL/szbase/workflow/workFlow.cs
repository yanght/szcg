using System;
using System.Collections.Generic;
using System.Text;
using bacgDL.szbase.workflow;
using System.Data;
using System.Collections;
using Teamax.Common;
using System.Data.SqlClient;


namespace bacgBL.szbase.workflow
{
    public class workFlow
    {
        #region �õ����������ƺ����
        /// <summary>
        /// �õ����������ƺ����
        /// </summary>
        /// <returns></returns>
        public ArrayList GetWorkFlowName()
        {
            ArrayList list = new ArrayList();
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                DataSet ds = new DataSet();
                ds = wf.GetWorkFlowName();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string[] flowInfo = new string[2];
                        flowInfo[0] = ds.Tables[0].Rows[i]["Typename"].ToString();
                        flowInfo[1] = ds.Tables[0].Rows[i]["flowtype"].ToString();
                        list.Add(flowInfo);
                    }
                }
            }
            return list;
        }
        #endregion


        #region �õ����������ƺ����
        /// <summary>
        /// �õ����������ƺ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetFlowName()
        {
            try
            {
                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = new DataSet();
                    ds = wf.GetWorkFlowName();
                    return ds;
                }
            }
            catch
            {
                return null;
            }

        }
        #endregion


        #region �õ����๤���������й���������
        /// <summary>
        /// �õ����๤���������й���������
        /// </summary>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public ArrayList GetSubFlowName(int parentType)
        {
            ArrayList list = new ArrayList();
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                DataSet ds = new DataSet();
                ds = wf.GetSubFlowName(parentType);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string[] flowInfo = new string[2];
                        flowInfo[0] = ds.Tables[0].Rows[i]["FlowName"].ToString();
                        flowInfo[1] = ds.Tables[0].Rows[i]["Flowid"].ToString();
                        list.Add(flowInfo);
                    }
                }
            }
            return list;

        }

        #endregion


        #region �����µĹ���������ڵ�������ID
        /// <summary>
        /// �����µĹ���������ڵ�������ID
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
        public void InsertNodeId(int actionType)
        {
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                wf.InsertNodeId(actionType);
            }
        }
        #endregion

        #region ���湤��������ڵ�����
        /// <summary>
        /// ɾ������������ڵ�����
        /// </summary>
        [AjaxPro.AjaxMethod]
        public string SaveFlowInfo(string tag, string flowid, string xml, string filePath)
        {
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                string flowName = wf.SaveFlowInfo(tag, int.Parse(flowid.TrimStart('f')), xml);
                System.Xml.XmlDocument deployDocument = new System.Xml.XmlDocument();
                deployDocument.LoadXml(xml);
                System.Xml.XmlNode nodeRoot = deployDocument.DocumentElement.SelectSingleNode("FlowConfig").SelectSingleNode("BaseProperties");
                nodeRoot.Attributes["flowText"].Value = flowName;

                deployDocument.Save(filePath);
                return flowName;
            }
        }
        #endregion

        #region ɾ������������ڵ�����
        /// <summary>
        /// ɾ������������ڵ�����
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
        [AjaxPro.AjaxMethod]
        public string DeleteFlowInfo(int actionType, string sid)
        {
            string id = sid;
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                if (actionType == 0)
                    id = id.TrimStart('f');
                else if (actionType == 1)
                    id = id.TrimStart('s');
                else if (actionType == 2)
                    id = id.TrimStart('a');
                int tag = wf.DeleteFlowInfo(actionType, int.Parse(id));

                return sid;
            }
        }
        #endregion

        #region ��ȡ����������ڵ���������ID
        /// <summary>
        ///��ȡ����������ڵ���������ID
        /// </summary>
        /// <param name="actionType">��0����������ID����1������ڵ�ID����2�����������ID</param>
        /// <returns></returns>
        public string GetNodeId(int actionType)
        {
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                string ID = wf.GetNodeId(actionType);
                if (actionType == 0)
                    ID = "f" + ID;
                else if (actionType == 1)
                    ID = "s" + ID;
                else if (actionType == 2)
                    ID = "a" + ID;
                return ID;
            }
        }
        #endregion

        #region ��ʼ���ڵ���¼�״̬λ���ƺ����
        /// <summary>
        /// ��ʼ���ڵ���¼�״̬λ���ƺ����
        /// </summary>
        /// <returns></returns>
        public DataSet InitNodeEventState(string strType)
        {

            try
            {

                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = wf.InitNodeEventState(strType);
                    return ds;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }
        #endregion

        #region �õ�ͼƬ��ַ������
        /// <summary>
        /// �õ�ͼƬ��ַ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetImageAddAndName(string strName)
        {

            try
            {
                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = wf.GetImageAddAndName(strName);
                    return ds;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }
        #endregion

        #region �õ����������ƺ����
        /// <summary>
        /// �õ����������ƺ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetFlowInfo()
        {
            try
            {
                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = new DataSet();
                    ds = wf.GetWorkFlowInfo();
                    return ds;
                }
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region �õ���ǰҳ�����ڹ����������
        /// <summary>
        /// �õ���ǰҳ�����ڹ����������
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageFlowInfo(string strButId)
        {
            try
            {
                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = new DataSet();
                    ds = wf.GetPageFlowType(strButId);
                    return ds;
                }
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region �õ���ǰҳ�����ڹ����������
        /// <summary>
        /// ����ı������̣���ö�Ӧ���̿�ʼ�ڵ��ButtonId
        /// </summary>
        /// <returns></returns>
        public DataSet GetChangeFlowButtonId(string strFlowId)
        {
            try
            {
                using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
                {
                    DataSet ds = new DataSet();
                    ds = wf.GetChangeFlowButtonId(strFlowId);
                    return ds;
                }
            }
            catch
            {
                return null;
            }

        }
        #endregion

        public DataTable GetFlowState(string type)
        {
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                return wf.GetFlowState(type);
            }

        }
    }
}
