using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.szbase
{
    public class FlowStateModel:BaseModel
    {
        public FlowStateModel()
        { }

        private string _type;
        /// <summary>
        /// 类别
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _id;
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string  _code;
        /// <summary>
        /// 代码 
        /// </summary>
        public string  Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _flowistype;
        /// <summary>
        /// 
        /// </summary>
        public string FlowIsType
        {
            get { return _flowistype; }
            set { _flowistype = value; }
        }

        private string _delnode;
        /// <summary>
        /// 
        /// </summary>
        public string DelNode
        {
            get { return _delnode; }
            set { _delnode = value; }
        }

        private string _buttontype;
        /// <summary>
        /// 
        /// </summary>
        public string ButtonType
        {
            get { return _buttontype; }
            set { _buttontype = value; }
        }

    }
}
