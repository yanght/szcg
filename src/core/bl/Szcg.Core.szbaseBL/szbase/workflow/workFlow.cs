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
        #region 得到工作流名称和类别
        /// <summary>
        /// 得到工作流名称和类别
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


        #region 得到工作流名称和类别
        /// <summary>
        /// 得到工作流名称和类别
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


        #region 得到各类工作流的所有工作流名称
        /// <summary>
        /// 得到各类工作流的所有工作流名称
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


        #region 增加新的工作流，或节点或操作的ID
        /// <summary>
        /// 增加新的工作流，或节点或操作的ID
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
        public void InsertNodeId(int actionType)
        {
            using (bacgDL.szbase.workflow.workFlow wf = new bacgDL.szbase.workflow.workFlow())
            {
                wf.InsertNodeId(actionType);
            }
        }
        #endregion

        #region 保存工作流，或节点或操作
        /// <summary>
        /// 删除工作流，或节点或操作
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

        #region 删除工作流，或节点或操作
        /// <summary>
        /// 删除工作流，或节点或操作
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
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

        #region 获取工作流，或节点或操作最大的ID
        /// <summary>
        ///获取工作流，或节点或操作最大的ID
        /// </summary>
        /// <param name="actionType">“0”代表工作流ID，“1”代表节点ID，“2”代表操作的ID</param>
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

        #region 初始化节点或事件状态位名称和类别
        /// <summary>
        /// 初始化节点或事件状态位名称和类别
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

        #region 得到图片地址和名称
        /// <summary>
        /// 得到图片地址和名称
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

        #region 得到工作流名称和类别
        /// <summary>
        /// 得到工作流名称和类别
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

        #region 得到当前页面所在工作流的类别
        /// <summary>
        /// 得到当前页面所在工作流的类别
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

        #region 得到当前页面所在工作流的类别
        /// <summary>
        /// 如果改变了流程，获得对应流程开始节点的ButtonId
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
