using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Teamax.Common.ScheduledTasks
{
    public interface IJob
    {
        void Execute(XmlNode node);
    }
}
