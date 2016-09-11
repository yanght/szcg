using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Teamax.Common.ScheduledTasks
{
    public class JobsConfiguration 
    {
        bool disabled = false;
        bool singleThread = true;
        int minutes = 15;
        private Dictionary<string, Job> jobList = new Dictionary<string, Job>();


        /// <summary>
        /// 是否禁用
        /// true 表示禁用
        /// </summary>
        public bool Disabled
        {
            get { return disabled; }
        }

        /// <summary>
        /// 单线程
        /// true 表示单进程
        /// </summary>
        public bool IsSingleThread
        {
            get { return singleThread; }
        }

        /// <summary>
        /// 执行时间间隔
        /// </summary>
        public int Interval
        {
            get { return minutes * 60000;}
        }

        /// <summary>
        /// 计划任务列表
        /// </summary>
        public Dictionary<string, Job> JobList
        {
            get { return jobList; }
        }

        public static JobsConfiguration GetConfig()
        {
            return (JobsConfiguration)ConfigurationManager.GetSection("bacg/jobs");
        }

        public int GetIntAttribute(XmlAttributeCollection attributes, string key, int defaultValue)
        {
            int val = defaultValue;

            if (attributes[key] != null
                && !string.IsNullOrEmpty(attributes[key].Value))
            {
                int.TryParse(attributes[key].Value, out val);
            }
            return val;
        }

        public bool GetBoolAttribute(XmlAttributeCollection attributes, string key, bool defaultValue)
        {
            bool val = defaultValue;

            if (attributes[key] != null
                && !string.IsNullOrEmpty(attributes[key].Value))
            {
                bool.TryParse(attributes[key].Value, out val);
            }
            return val;
        }

        public void LoadValuesFromConfigurationXml(XmlNode node)
        {
            disabled = GetBoolAttribute(node.Attributes, "disabled", false);
            singleThread = GetBoolAttribute(node.Attributes, "singleThread", true);
            minutes = GetIntAttribute(node.Attributes, "minutes", 15);


            foreach (XmlNode jnode in node.ChildNodes)
            {
                if (jnode.NodeType != XmlNodeType.Comment)
                {
                    XmlAttribute typeAttribute = jnode.Attributes["type"];
                    XmlAttribute nameAttribute = jnode.Attributes["name"];

                    Type type = Type.GetType(typeAttribute.Value);
                    if (type != null)
                    {
                        if (!jobList.ContainsKey(nameAttribute.Value))
                        {
                            Job j = new Job(type, jnode);
                            jobList[nameAttribute.Value] = j;

                            if (!IsSingleThread || !j.SingleThreaded)
                                j.InitializeTimer();
                        }
                    }
                }
            }
        }
    }
}
