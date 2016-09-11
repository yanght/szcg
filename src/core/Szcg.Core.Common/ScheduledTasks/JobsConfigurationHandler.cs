using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Teamax.Common.ScheduledTasks
{
    internal class JobsConfigurationHandler : IConfigurationSectionHandler
    {
        public virtual object Create(Object parent, Object context, XmlNode node)
        {
            JobsConfiguration config = new JobsConfiguration();
            config.LoadValuesFromConfigurationXml(node);
            return config;
        }
    }
}
