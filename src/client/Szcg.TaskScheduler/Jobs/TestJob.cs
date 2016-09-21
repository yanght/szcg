using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;

namespace Szcg.TaskScheduler.Jobs
{
    public class TestJob : IJob
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TestJob));
        public void Execute(IJobExecutionContext context)
        {
            _logger.InfoFormat("TestJob...");
        }
    }
}
