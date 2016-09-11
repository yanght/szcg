using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using bacgDL.business.workflow;

namespace bacgBL.com.teamax.szbase.workflow
{
    //private string 
    public class WorkFlowDeal
    {
        public string insertFlowInfo(Flowinfo flowinfo)
        {
            string tablename = "s_flowinfo";
            string flowinfoid = getPKkey(tablename);
            flowinfo.Flowinfoid = flowinfoid;
            workflowDAO DAO = new workflowDAO();
            DAO.insertFlowinfo(flowinfo);
            return flowinfoid;
        }

        public string insertFlowNode(FlowNode flowNode)
        {
            string tablename = "s_flownodeinfo";
            string flownodeid = getPKkey(tablename);
            string keyvalue = "Sequence";
            int maxindex = getMaxIndex(tablename, keyvalue, "flowinfoid", flowNode.Flowinfoid);
            flowNode.Flownodeid = flownodeid;
            flowNode.Sequence = maxindex;
            workflowDAO DAO = new workflowDAO();
            DAO.insertFlownodeinfo(flowNode);
            return flownodeid;
        }

        public bool insertFlowBusis(FlowBusi flowBusi)
        {
            string tablename = "s_flowbusistatus";
            string flowbusistatusid = getPKkey(tablename);
            string busistatus = getMaxStatusidkey(flowBusi.Flowinfoid, flowBusi.Flownodeid);
            flowBusi.Flowbusistatusid = flowbusistatusid;
            flowBusi.Busistatus = busistatus;
            workflowDAO DAO = new workflowDAO();
            DAO.insertFlowbusistatus(flowBusi);
            //return flownodeid;
            return true;
        }

        public bool insertFlowNodeRelation(Flowrelainfo flowrelainfo)
        {
            string tablename = "s_flownoderelainfo";
            string relainfoid = getPKkey(tablename);
            flowrelainfo.Relainfoid = relainfoid;
            workflowDAO DAO = new workflowDAO();
            DAO.insertFlowrelainfo(flowrelainfo);
            return true;
        }

        public bool updateFlowNodeRelation(Flowrelainfo flowrelainfo)
        {
            workflowDAO DAO = new workflowDAO();
            DAO.updateFlowrelainfo(flowrelainfo);
            return true;
        }

        public string getMaxStatusidkey(string flowinfoid, string flownodeid)
        {
            workflowDAO DAO = new workflowDAO();
            string result = "";
            result = DAO.getMaxStatusidkey(flowinfoid, flownodeid);
            return result;
        }

        public string getPKkey(string tableName)
        {
            workflowDAO DAO = new workflowDAO();
            string result = "";
            result = DAO.getKeyValue(tableName);
            return result;
        }

        public int getMaxIndex(string tablename, string keyvalue, string columname, string datavalue)
        {
            workflowDAO DAO = new workflowDAO();
            int result = 0;
            result = DAO.selectMaxIndex(tablename, keyvalue, columname, datavalue);
            return result;
        }

        public Hashtable getFlowRelation(string relainfoid)
        {
            Hashtable table = new Hashtable();
            workflowDAO DAO = new workflowDAO();
            IDataReader rs = DAO.GetFlowRelationForDetail(relainfoid);
            if (rs != null)
            {
                while (rs.Read())
                {
                    table.Add("relainfoid", rs["Relainfoid"].ToString());
                    table.Add("corenodeid", rs["corenodeid"].ToString());
                    table.Add("corenodename", rs["corenodename"].ToString());
                    table.Add("nextnodeid", rs["nextnodeid"].ToString());
                    table.Add("nextnodename", rs["nextnodename"].ToString());
                    table.Add("busistatus", rs["Busistatus"].ToString());
                    table.Add("flowinfoid", rs["Flowinfoid"].ToString());
                    table.Add("status", rs["status"].ToString());
                    table.Add("remark", rs["remark"].ToString());
                }
            }
            rs.Close();
            return table;
        }

        public ArrayList getFlowBusistatus(string flownodeid)
        {
            workflowDAO DAO = new workflowDAO();
            IDataReader rs  = DAO.GetFlownodeBusis("",flownodeid);
            ArrayList list = new ArrayList();
            if (rs != null)
            {
                while (rs.Read())
                {
                    FlowBusi bean = new FlowBusi();
                    bean.Flowbusistatusid = rs["flowbusistatusid"].ToString();
                    bean.Flowinfoid = rs["flowinfoid"].ToString();
                    bean.Flownodeid = rs["flownodeid"].ToString();
                    bean.Busistatus = rs["busistatus"].ToString();
                    bean.Businame = rs["businame"].ToString();
                    bean.Status = rs["status"].ToString();
                    bean.Remark = rs["remark"].ToString();
                    list.Add(bean);
                }
            }
            rs.Close();
            return list;

        }

        public ArrayList getFlownodeinfo(string flowinfoid, string flownodeid)
        {
            workflowDAO DAO = new workflowDAO();
            IDataReader rs = DAO.GetFlownodeinfo(flowinfoid);
            ArrayList list = new ArrayList();
            if (rs != null)
            {
                while (rs.Read())
                {
                    FlowNode bean = new FlowNode();
                    if (rs["flownodeid"].ToString() != flownodeid)
                    {
                        bean.Flowinfoid = rs["flowinfoid"].ToString();
                        bean.Flownodeid = rs["flownodeid"].ToString();
                        bean.Flownodename = rs["flownodename"].ToString();
                        bean.Busideal = rs["busideal"].ToString();
                        bean.Property = rs["property"].ToString();
                        bean.Status = rs["status"].ToString();
                        bean.Remark = rs["remark"].ToString();
                        list.Add(bean);
                    }
                }
            }
            rs.Close();
            return list;

        }
    }
}
