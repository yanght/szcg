using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.zhpj
{
    public class AppraiseSettingTFile
    {
        private String id = "";
        private String viewname = "";
        private String tempname = "";

        public String getId()
        {
            return id;
        }
        public void setId(String id)
        {
            this.id = id;
        }
        public String getViewName()
        {
            return viewname;
        }
        public void setViewName(String viewname)
        {
            this.viewname = viewname;
        }
        public String getTempName()
        {
            return tempname;
        }
        public void setTempName(String tempname)
        {
            this.tempname = tempname;
        }
    }
}
