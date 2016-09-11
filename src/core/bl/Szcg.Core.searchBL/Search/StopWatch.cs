using System;
using System.Runtime.InteropServices;

namespace bacgBL.Search
{
   
    public sealed class StopWatch
    {
        //apiImport
        [DllImport("kernel32.dll")]
        extern static int QueryPerformanceCounter(ref long x);

        //apiImport
        [DllImport("kernel32.dll")]
        extern static int QueryPerformanceFrequency(ref long x);
       
        //≥ı ºªØ
        public StopWatch()
        {
            Frequency = GetFrequency();
            Reset();
        }
       
        public void Reset()
        {
            StartTime = GetValue();
        }
       
        public long Peek()
        {
            return (long)(((GetValue() - StartTime) / (double)Frequency) * 10000);
        }
       
        private long GetValue()
        {
            long ret = 0;
            if (QueryPerformanceCounter(ref ret) == 0)
                throw new NotSupportedException("Error while querying the high-resolution performance counter.");
            return ret;
        }
        
        private long GetFrequency()
        {
            long ret = 0;
            if (QueryPerformanceFrequency(ref ret) == 0)
                throw new NotSupportedException("Error while querying the performance counter frequency.");
            return ret;
        }
        
        private long StartTime
        {
            get
            {
                return m_StartTime;
            }
            set
            {
                m_StartTime = value;
            }
        }
     
        private long Frequency
        {
            get
            {
                return m_Frequency;
            }
            set
            {
                m_Frequency = value;
            }
        }
       
        private long m_StartTime;
       
        private long m_Frequency;
    }
}