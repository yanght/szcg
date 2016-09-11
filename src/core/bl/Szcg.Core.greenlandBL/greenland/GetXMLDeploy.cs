/* ****************************************************************************************
 * ��Ȩ���У��������ĿƼ��������޹�˾
 * ��    ;����ȡxml������Ϣ
 * �ṹ��ɣ�
 * ��    �ߣ�ycg
 * �������ڣ�2007-05-29
 * �������ڣ�2008-01-19
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
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
        /// ��ȡ�ṹ��
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>���ؽṹ��</returns>
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
        /// ��ȡ�ṹ��
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>���ؽṹ��</returns>
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
        ///  ð������
        /// </summary>
        /// <param name="array">��������</param>
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
