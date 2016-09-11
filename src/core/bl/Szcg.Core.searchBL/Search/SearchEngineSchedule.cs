using System;

using bacgDL.Search; 

namespace bacgBL.Search
{
    public class SearchEngineSchedule : Teamax.Common.ScheduledTasks.IJob
    {
        public void Execute(System.Xml.XmlNode node)
        {
            if (node.Attributes["name"].Value == "object_index")
            {
                SearchProvider searchProvider = new SearchProvider();
                DateTime LastCompleted = searchProvider.GetLastExecuteScheduledEventDateTime(node.Attributes["name"].Value, Environment.MachineName);
                if (DateTime.Now.Hour == 3 && LastCompleted < DateTime.Now && (int)DateTime.Now.DayOfWeek==1)
                {
                    IndexManager.RebuildSafeIndex(30, node);
                }
            }
            else if (node.Attributes["name"].Value == "project_index")
            {
                IndexManager.RebuildSafeIndex(30, node);
            }
        }
    }      
}
