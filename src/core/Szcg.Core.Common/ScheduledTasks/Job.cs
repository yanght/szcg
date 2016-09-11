using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using System.Xml;
using System.Threading;

namespace Teamax.Common.ScheduledTasks
{
    [Serializable]
    [XmlRoot("job")]
    public class Job : IDisposable
    {
        public event EventHandler PreJob;
        public event EventHandler PostJob;

        private void OnPreJob()
        {
            try
            {
                if (PreJob != null)
                    PreJob(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void OnPostJob()
        {
            try
            {
                if (PostJob != null)
                    PostJob(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                //
            }
        }

        public Job(Type ijob, XmlNode node)
        {
            _node = node;

            _jobType = ijob;

            XmlAttribute att = node.Attributes["enabled"];
            if (att != null)
                this._enabled = bool.Parse(att.Value);

            att = node.Attributes["enableShutDown"];
            if (att != null)
                this._enableShutDown = bool.Parse(att.Value);

            att = node.Attributes["name"];
            if (att != null)
                this._name = att.Value;

            att = node.Attributes["seconds"];
            if (att != null)
            {
                _seconds = Int32.Parse(att.Value);
            }

            att = node.Attributes["firstRun"];
            if (att != null)
            {
                _firstRun = Int32.Parse(att.Value);
            }

            att = node.Attributes["physicalPath"];
            if (att != null)
            {
                _physicalPath = att.Value;
            }
            
            att = node.Attributes["minutes"];
            if (att != null)
            {
                try
                {
                    this._minutes = Int32.Parse(att.Value);
                }
                catch
                {
                    this._minutes = 15;
                }
            }

            att = node.Attributes["singleThread"];
            if (att != null && !string.IsNullOrEmpty(att.Value) && string.Compare(att.Value, "false", false) == 0)
                _singleThread = false;
        }



        #region Private Members

        private IJob _ijob;
        private bool _enabled = true;
        private Type _jobType;
        private string _name;
        private string _physicalPath;
        private bool _enableShutDown = false;
        private int _minutes = 15;
        private Timer _timer = null;
        private bool disposed = false;
        private XmlNode _node = null;
        private bool _singleThread = true;
        private DateTime _lastStart;
        private DateTime _lastSucess;
        private DateTime _lastEnd;
        private bool _isRunning;
        private int _seconds = -1;
        private int _firstRun = -1;

        protected int Interval
        {
            get
            {
                if (_firstRun > 0)
                    return _firstRun * 1000;

                if (_seconds > 0)
                    return _seconds * 1000;

                return Minutes * 60000;
            }
        }

        #endregion


        /// <summary>
        /// 创建一个 定时器
        /// </summary>
        public void InitializeTimer()
        {
            if (_timer == null && Enabled)
            {
                _timer = new Timer(new TimerCallback(timer_Callback), null, Interval, Interval);
            }
        }

        /// <summary>
        /// 相应事件
        /// </summary>
        /// <param name="state"></param>
        private void timer_Callback(object state)
        {

            if (!Enabled)
                return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            _firstRun = -1;



            ExecuteJob();



            if (Enabled)
                _timer.Change(Interval, Interval);
            else
                this.Dispose();

        }

        public void ExecuteJob()
        {
            OnPreJob();

            _isRunning = true;
            IJob ijob = this.CreateJobInstance();
            if (ijob != null)
            {
                _lastStart = DateTime.Now;
                try
                {
                    ijob.Execute(this._node);
                    _lastEnd = _lastSucess = DateTime.Now;

                }
                catch (Exception)
                {
                    this._enabled = !this.EnableShutDown;
                    _lastEnd = DateTime.Now;
                }
            }
            _isRunning = false;

            OnPostJob();
        }

        #region Public Properities

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public DateTime LastEnd
        {
            get { return _lastEnd; }
        }

        public DateTime LastSuccess
        {
            get { return _lastSucess; }
        }

        public bool SingleThreaded
        {
            get { return _singleThread; }
        }

        /// <summary>
        /// job类型
        /// </summary>
        public Type JobType
        {
            get { return this._jobType; }

        }

        /// <summary>
        /// 最后一次开始事件
        /// </summary>
        public DateTime LastStart
        {
            get { return this._lastStart; }

        }

        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }


        /// <summary>
        /// 异常是否停止
        /// </summary>
        public bool EnableShutDown
        {
            get { return this._enableShutDown; }
        }


        /// <summary>
        /// job名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enabled
        {
            get { return this._enabled; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string PhysicalPath
        {
            get { return this._physicalPath; }
        }



     
        public IJob CreateJobInstance()
        {
            if (Enabled)
            {
                if (_ijob == null)
                {

                    if (_jobType != null)
                    {
                        _ijob = Activator.CreateInstance(_jobType) as IJob;
                    }
                    _enabled = (_ijob != null);

                    if (!_enabled)
                        this.Dispose();
                }
            }
            return _ijob;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_timer != null && !disposed)
            {
                lock (this)
                {
                    _timer.Dispose();
                    _timer = null;
                    disposed = true;
                }
            }
        }



        #endregion
    }

}
