using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.zhpj
{
    public class StructQuery
    {
        public string bgCode = "";
        public int projectType = 0;
        public string bigClassCode = "0";
        public string smallClassCode = "0";
        public string deptCode = "";
        public int intWeeks = 0;
        public int intMonths = 0;
        public int intYears = 0;
        public int intQuarter = 0;
        public string typecode = "";
        public DateTime startDate = new DateTime(1999, 1, 1);
        public DateTime endDate = DateTime.Now;
    }
}
