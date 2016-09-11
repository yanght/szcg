/* ****************************************************************************************
 * 版权所有：杭州天夏科技集团有限公司
 * 用    途：获取xml配置信息
 * 结构组成：
 * 作    者：ycg
 * 创建日期：2007-05-29
 * 更新日期：2008-01-19
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace bacgBL.greenland
{
    public class ObjProperty
    {
        public string key;
        public string name;
        public string type;
        public bool enable;
        public string length;
        public string value;
        public int sort;
    }

    public class GetXMLDeploy
    {
        /// <summary>
        /// 获取结构体
        /// </summary>
        /// <param name="name">部件名</param>
        /// <returns>返回结构串</returns>
        public static Hashtable GetObjXML(string name, string path)
        {
            XmlDocument deployDocument = new XmlDocument();
            deployDocument.Load(path);
            Hashtable ht = new Hashtable();
            XmlNode node = deployDocument.DocumentElement.SelectSingleNode("objmain");
            if (node != null)
            {
                XmlNodeList nodes = node.SelectNodes("Property");
                SeTDataXML(ht, nodes);
            }

            XmlNodeList nodeList = deployDocument.DocumentElement.SelectNodes("objSub");
            if (nodeList != null)
            {
                bool tag = false;
                foreach (XmlNode n in nodeList)
                {
                    if (n.Attributes["name"].Value == name)
                    {
                        XmlNodeList nodes = n.SelectNodes("Property");
                        SeTDataXML(ht, nodes);
                        tag = true;
                        break;
                    }
                }
                if (!tag)
                {
                    return null;
                }
            }
          
            return ht;
        }

        /// <summary>
        /// 获取结构体
        /// </summary>
        /// <param name="name">部件名</param>
        /// <returns>返回结构串</returns>
        public static ArrayList GetObjXMLSortList(string name, string path)
        {
            XmlDocument deployDocument = new XmlDocument();
            deployDocument.Load(path);
            ArrayList al = new ArrayList();
            XmlNode node = deployDocument.DocumentElement.SelectSingleNode("objmain");
            if (node != null)
            {
                XmlNodeList nodes = node.SelectNodes("Property");
                SeTDataXML(al, nodes);
            }

            XmlNodeList nodeList = deployDocument.DocumentElement.SelectNodes("objSub");
            if (nodeList != null)
            {
                bool tag = false;
                foreach (XmlNode n in nodeList)
                {
                    if (n.Attributes["name"].Value == name)
                    {
                        XmlNodeList nodes = n.SelectNodes("Property");
                        SeTDataXML(al, nodes);
                        tag = true;
                        break;
                    }
                }
                if (!tag)
                {
                    return null;
                }
            }
            Sort(al);
            return al;
        }

        /// <summary>
        ///  冒泡排序
        /// </summary>
        /// <param name="array">整型数组</param>
        public static void Sort(ArrayList al)
        {
            for (int i = 0; i < al.Count; i++)
            {
                for (int j = i; j < al.Count; j++)
                {
                    if (((ObjProperty)al[i]).sort >((ObjProperty)al[j]).sort)
                    {
                        object temp = al[i];
                        al[i] = al[j];
                        al[j] = temp;
                    }
                }
            }
        }

        private static void SeTDataXML(ArrayList al, XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                ObjProperty op = new ObjProperty();
                op.key = nodes[i].Attributes["key"].Value;
                op.sort =int.Parse(nodes[i].Attributes["sort"].Value);
                al.Add(op);
            }
        }

        private static void SeTDataXML(Hashtable ht, XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                ObjProperty op = new ObjProperty();
                op.key = nodes[i].Attributes["key"].Value;
                op.name = nodes[i].Attributes["name"].Value;
                op.type = nodes[i].Attributes["type"].Value;
                op.length = nodes[i].Attributes["length"].Value;
                op.enable = nodes[i].Attributes["enable"].Value == "true" ? true : false;
                if (!ht.Contains(op.key))
                    ht.Add(op.key,op);
            }
        }

    }
}
