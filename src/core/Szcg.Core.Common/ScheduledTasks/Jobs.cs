using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;
using System.Threading;

namespace Teamax.Common.ScheduledTasks
{
    public class Jobs
    {
      
        private static readonly Jobs _jobs = null;


        private Dictionary<string, Job> jobList = new Dictionary<string,Job>();
        private int Interval = 15 * 60000;
        private Timer singleTimer = null;
        private DateTime _created;
        private DateTime _started;
        private DateTime _completed;
        private bool _isRunning;

  
        static Jobs()
        {
            _jobs = new Jobs();
        }
  
        public static Jobs Instance()
        {
            return _jobs;
        }

        private Jobs()
        {
            _created = DateTime.Now;
        }

        public Dictionary<string, Job> CurrentJobs
        {
            get { return jobList; }
        }


        public override string ToString()
        {
            return string.Format("Created: {0}, LastStart: {1}, LastStop: {2}, IsRunning: {3}, Minutes: {4}", _created, _started, _completed, _isRunning, Interval / 60000);
        }

        public System.Collections.Specialized.ListDictionary CurrentStats
        {
            get
            {
                ListDictionary stats = new ListDictionary();
                stats.Add("Created", _created);
                stats.Add("LastStart", _started);
                stats.Add("LastStop", _completed);
                stats.Add("IsRunning", _isRunning);
                stats.Add("Minutes", Interval / 60000);
                return stats;
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (jobList.Count != 0)
                return;

            JobsConfiguration config = JobsConfiguration.GetConfig();

            if (!config.Disabled)
            {
                jobList = config.JobList;

                foreach (Job job in jobList.Values)
                {
                    if (!config.IsSingleThread || !job.SingleThreaded)
                        job.InitializeTimer();
                }

                if (config.IsSingleThread)
                {
                    //创建新的计时器
                    singleTimer = new Timer(new TimerCallback(call_back), null, Interval, Interval);
                }
            }
        }

        /// <summary>
        /// 返回函数
        /// </summary>
        /// <param name="state"></param>
        private void call_back(object state)
        {
            _isRunning = true;
            _started = DateTime.Now;
            singleTimer.Change(Timeout.Infinite, Timeout.Infinite);

            foreach (Job job in jobList.Values)
                if (job.Enabled && job.SingleThreaded)
                    job.ExecuteJob();

            singleTimer.Change(Interval, Interval);
            _isRunning = false;
            _completed = DateTime.Now;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            if (jobList != null)
            {
                foreach (Job job in jobList.Values)
                    job.Dispose();

                jobList.Clear();

                if (singleTimer != null)
                {
                    singleTimer.Dispose();
                    singleTimer = null;
                }
            }
        }

        public bool IsJobEnabled(string jobName)
        {
            if (!jobList.ContainsKey(jobName))
                return false;
            return ((Job)jobList[jobName]).Enabled;
        }
    }

}
