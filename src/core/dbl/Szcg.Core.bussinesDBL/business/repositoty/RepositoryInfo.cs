using System;
using System.Collections.Generic;
using System.Text;

namespace szcg.web.business.repository
{
    /// <summary>
    /// ÖªÊ¶¿âRepositoryInfo(mgh)
    /// </summary>
    public class RepositoryInfo
    {
        private String name = "";
        private String code = "";
        private String pcode = "";
        private String memo = "";
        private string title = "";
        private string date = "";

        public RepositoryInfo()
        {

        }
        public RepositoryInfo(string name, string title, string date, string memo)
        {
            this.name = name;
            this.title = title;
            this.date = date;
            this.memo = memo;
        }


        public RepositoryInfo(String[] info)
        {
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i].IndexOf('=') == -1)
                {
                    continue;
                }

                String propery = info[i].Substring(0, info[i].IndexOf('=', 0)).Trim();
                String values = info[i].Substring(info[i].IndexOf('=') + 1).Trim();

                if (propery.ToLower().Equals("name"))
                {
                    this.name = values;
                }
                else if (propery.ToLower().Equals("code"))
                {
                    this.code = values;
                }
                else if (propery.ToLower().Equals("pcode"))
                {
                    this.pcode = values;
                }
                else if (propery.ToLower().Equals("descs"))
                {
                    this.memo = values;
                }
            }
        }



        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getCode()
        {
            return this.code;
        }

        public void setCode(String code)
        {
            this.code = code;
        }

        public void setPcode(String pcode)
        {
            this.pcode = pcode;
        }

        public String getPcode()
        {
            return pcode;
        }

        public void setMemo(String memo)
        {
            this.memo = memo;
        }

        public String getMemo()
        {
            return memo;
        }
        public void setDate(string date)
        {
            this.date = date;
        }
        public string getDate()
        {
            return this.date;
        }
        public void setTitle(string title)
        {
            this.title = title;
        }
        public string getTitle()
        {
            return this.title;
        }
    }
}
