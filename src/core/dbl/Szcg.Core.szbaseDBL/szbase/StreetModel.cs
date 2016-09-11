using System;

namespace DBbase.szbase
{
    /// <summary>
    /// StreetModel 的摘要说明
    /// </summary>
    public class StreetModel:BaseModel
    {
        public StreetModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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
        private string _code;
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
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
        private string _parentCode;
        /// <summary>
        /// 父节点代码
        /// </summary>
        public string ParentCode
        {
            get { return _parentCode; }
            set { _parentCode = value; }
        }
        private string _population;
        /// <summary>
        /// 人口
        /// </summary>
        public string Population
        {
            get { return _population; }
            set { _population = value; }
        }
        private string _area;

        /// <summary>
        /// 面积    
        /// </summary>
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
        private string _memo;
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
