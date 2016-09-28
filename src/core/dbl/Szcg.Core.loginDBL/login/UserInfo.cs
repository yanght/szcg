/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���û���Ϣʵ����
 * �ṹ��ɣ�
 * ��    �ߣ�yannis
 * �������ڣ�2007-05-21
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;

namespace szcg.com.teamax.business.entity
{
    /// <summary>
    /// �û���Ϣʵ����
    /// </summary>
    public class UserInfo
    {
        //�Ƿ񵯳�ʽ����
        private bool _IsOpenWindow;

        /// <summary>
        /// �Ƿ񵯳�ʽ����
        /// </summary>
        public bool IsOpenWindow
        {
            get
            {
                return _IsOpenWindow;
            }
            set
            {
                this._IsOpenWindow = value;
            }
        }

        // �û���ǰʹ�õĽ�ɫ����
        public int _intCurrentRole;

        /// <summary>
        /// �û���ǰʹ�õĽ�ɫ����
        /// </summary>
        public int CurrentRole
        {
            get { return this._intCurrentRole; }
            set { this._intCurrentRole = value; }
        }

        // ��ǰ�ڵ�
        public string _CurrentNodeID;

        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public string CurrentNodeID
        {
            get { return this._CurrentNodeID; }
            set { this._CurrentNodeID = value; }
        }

        //�û���ǰ��½��ϵͳ;
        public string _CurrentSystemId;

        /// <summary>
        /// �û���ǰ��½��ϵͳ
        /// </summary>
        public string CurrentSystemId
        {
            get
            {
                return _CurrentSystemId;
            }
            set
            {
                this._CurrentSystemId = value;
            }
        }

        //�û���Ȩ�޲�����ģ���б�
        public string _ModelPowers;

        /// <summary>
        /// �û���Ȩ�޲�����ģ���б�ģ��֮���ö��ŷָ�
        /// </summary>
        public string ModelPowers
        {
            get
            {
                return _ModelPowers;
            }
            set
            {
                this._ModelPowers = value;
            }
        }


        //��ǰ�û���Ӧ�Ķ��ֽ�ɫ;
        public string[] _role;

        //��ǰ�û�����ϵͳ����;
        public string[] _systemid;

        public int _usercode;
        /*��½����*/
        public int _logincount;

        public string _loginname;
        public string _username;

        public string _password;
        public int _departcode;
        /*�����½ʱ��*/
        public string _regdate;
        /*����½ʱ��*/
        public string _lastoutdate;

        /*�Ƿ�����*/
        public string _isonline;
        /*�绰*/
        public string _tel;

        public string _mobile;

        public string _email;
        public string _address;
        public string _sex;
        public string _areacode;
        public string _birthday;

        public string _ca;
        public bool _is_ca;

        /*��������*/
        public string _departname;
        public string _departDefinedcode;
        /*���Ÿ����*/
        public int _parentcode;

        public int step;

        public string _videolevel;
        public string _hcpower;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="logincount"></param>
        /// <param name="loginname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="departcode"></param>
        /// <param name="regdate"></param>
        /// <param name="lastoutdate"></param>
        /// <param name="isonline"></param>
        /// <param name="tel"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="sex"></param>
        /// <param name="areacode"></param>
        /// <param name="birthday"></param>
        /// <param name="ca"></param>
        /// <param name="departname"></param>
        /// <param name="parentcode"></param>
        /// <param name="videolevel"></param>
        public UserInfo(int usercode, int logincount, string loginname, string username,
            string password, int departcode, string regdate, string lastoutdate, string isonline,
            string tel, string mobile, string email, string address, string sex, string areacode, string birthday,
            string ca, string departname, int parentcode, string videolevel, string hcpower)
        {
            this._usercode = usercode;
            this._logincount = logincount;
            this._loginname = loginname;
            this._username = username;
            this._password = password;
            this._departcode = departcode;
            this._regdate = regdate;
            this._lastoutdate = lastoutdate;
            this._isonline = isonline;
            this._tel = tel;
            this._mobile = mobile;
            this._email = email;
            this._address = address;
            this._sex = sex;
            this._areacode = areacode;
            this._birthday = birthday;
            this._ca = ca;
            this._is_ca = false;
            this._departname = departname;
            this._parentcode = parentcode;
            this._videolevel = videolevel;
            this._hcpower = hcpower;
        }

        public UserInfo()
        {

        }

        public string[] getRole()
        {
            return this._role;
        }

        public string getRole(int index)
        {
            if (this._role == null)
                return null;
            if (index >= this._role.Length)
                return null;
            return this._role[index];
        }

        public void setRole(System.Collections.ArrayList role)
        {
            if (role == null)
                return;

            this._role = new String[role.Count];
            for (int i = 0; i < role.Count; i++)
            {
                this._role[i] = System.Convert.ToString(role[i]);
            }
        }

        public string[] getSystemid()
        {
            return this._systemid;
        }

        public void setSystemid(string[] systemid)
        {
            this._systemid = systemid;
        }

        public void setSystemid(System.Collections.ArrayList al)
        {
            if (al == null)
                return;

            this._systemid = new String[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                this._systemid[i] = System.Convert.ToString(al[i]);
            }
        }

        public int getUsercode()
        {
            return _usercode;
        }

        public void setUsercode(int usercode)
        {
            this._usercode = usercode;
        }

        public String getLoginname()
        {
            return _loginname;
        }
        public void setLoginname(String loginname)
        {
            this._loginname = loginname;
        }

        public String getVideoLevel()
        {
            return _videolevel;
        }

        public void setVideoLevel(String videolevel)
        {
            this._videolevel = videolevel;
        }
        public String getHcpower()
        {
            return _hcpower;
        }
        public void setHcpower(String hcpower)
        {
            this._hcpower = hcpower;
        }
        public String getUsername()
        {
            return _username;
        }

        public void setUsername(String username)
        {
            this._username = username;
        }

        public String getPassword()
        {
            return _password;
        }
        public void setPassword(String password)
        {
            this._password = password;
        }


        public int getDepartcode()
        {
            return _departcode;
        }

        public void setDepartcode(int departcode)
        {
            this._departcode = departcode;
        }

        public string getDepartDefinedcode()
        {
            return _departDefinedcode;
        }

        public void setDepartDefinedcode(string departDefinedcode)
        {
            this._departDefinedcode = departDefinedcode;
        }

        public String getRegdate()
        {
            return _regdate;
        }

        public void setRegdate(String regdate)
        {
            this._regdate = regdate;
        }

        public String getLastoutdate()
        {
            return _lastoutdate;
        }

        public void setLastoutdate(String lastoutdate)
        {
            this._lastoutdate = lastoutdate;
        }

        public int getLogincount()
        {
            return _logincount;
        }

        public void setLogincount(int logincount)
        {
            this._logincount = logincount;
        }

        public String getIsonline()
        {
            return _isonline;
        }

        public void setIsonline(String isonline)
        {
            this._isonline = isonline;
        }

        public String getTel()
        {
            return _tel;
        }
        public void setTel(String tel)
        {
            this._tel = tel;
        }

        public String getMobile()
        {
            return _mobile;
        }

        public void setMobile(String mobile)
        {
            this._mobile = mobile;
        }

        public String getEmail()
        {
            return _email;
        }
        public void setEmail(String email)
        {
            this._email = email;
        }

        public String getAddress()
        {
            return _address;
        }

        public void setAddress(String address)
        {
            this._address = address;
        }

        public String getSex()
        {
            return _sex;
        }
        public void setSex(String sex)
        {
            this._sex = sex;
        }
        public String getAreacode()
        {
            return _areacode;
        }

        public void setAreacode(String _areacode)
        {
            this._areacode = _areacode;
        }

        public String getBirthday()
        {
            return _birthday;
        }

        public void setBirthday(String birthday)
        {
            this._birthday = birthday;
        }
        public String getCa()
        {
            return _ca;
        }
        public void setCa(String ca)
        {
            this._ca = ca;
        }

        public bool getIs_ca()
        {
            return this._is_ca;
        }
        public void setIs_ca(bool is_ca)
        {
            this._is_ca = is_ca;
        }


        public String getDepartname()
        {
            return _departname;
        }

        public void setDepartname(String departname)
        {
            this._departname = departname;
        }


        public int getParentcode()
        {
            return _parentcode;
        }

        public void setParentcode(int parentcode)
        {
            this._parentcode = parentcode;
        }

        public int getStep()
        {
            return this.step;
        }

        public void setStep(int step)
        {
            this.step = step;
        }

    }
}
