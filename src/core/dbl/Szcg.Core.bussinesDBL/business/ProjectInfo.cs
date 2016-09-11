using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business
{
    /// <summary>
    /// 案件信息结构体
    /// </summary>
    public class ProjectInfo
    {
        public string NodeId = ""; //节点
        public string TargetDepartCode = ""; //应处理部门
        public string DoDepartCode = ""; //结构反馈的专业部门
        public string stepid = ""; //阶段
        public string _cityId = "";//市，区
        public string areacode = ""; //区域
        public string role = ""; //角色
        public string projcode = ""; //案件编号
        public string projcodeName = ""; //案件编号
        public string IsManual = "0"; //是否手工登记的案件
        public string probsource = ""; //
        public string Telephonist = ""; //接线员
        public string TelephonistCode = ""; //接线员编码
        public string Fid = ""; //
        public string typecode = ""; //
        public string bigClass = ""; //
        public string smallclass = ""; //
        public string detailedclass = "";//
        public string partID = "";
        public string IsNeedFeedBack = "0"; //需要回复
        public string isFeadback = "0";    //是否已回复
        public string IsDel = "";
        public string startdate = "";  //受理时间
        public string ispress = "";    //是否督办
        public string _area = ""; //
        public string _street = ""; //
        public string _square = ""; //
        public string gridcode = ""; //基础网格
        public string WorkGrid = ""; //工作网格
        public string ProcessType = ""; //处理类型
        public string address = ""; //
        public string isthough = "0"; //
        public string isgreat = "0"; //
        public string groupid = "5301"; //
        public string probdesc = ""; //
        public string ButtonId = "";//
        public string strButtonId = "";
        public string departcode = "";
        public string strUserCode = "";
        public string strCityName = "";//城市名称

        public string PdaIoFlag = ""; //PDA 核查状态
        public string Pdamsg = "";    //核查反馈信息

        public string departTime = "0";   //职能部门处置时限
        public string departTimeType = "0"; //职能部门处置时限类型

        public string withDept = "";//部门处理方式

        //登记上传图片相关  2009-4-15  添加
        private byte[][] _bytesFiles;
        private string[] _images ={ "","",""};
        private string[] _filepath;

        public byte[][] BytesFiles
        {
            get { return _bytesFiles; }
            set { _bytesFiles = value; }
        }

        public string[] Images
        {
            get { return _images; }
            set { _images = value; }
        }

        public string[] Filepath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }


        public string cityId
        {
            get { return _cityId; }
            set { _cityId = value.Trim(); }
        }

        public string area
        {
            get { return _area; }
            set { _area = value.Trim(); }
        }

        public string street
        {
            get { return _street; }
            set { _street = value.Trim(); }
        }

        public string square
        {
            get { return _square; }
            set { _square = value.Trim(); }
        }

        #region   上传的图片和声音信息  流文件信息及文件后缀
        private string _upLoadType;
        /// <summary>
        /// 上传的类型 0 核查、1 核实
        /// </summary>
        public string UpLoadType
        {
            get { return _upLoadType; }
            set { _upLoadType = value; }
        }

        private string _imgFtpPath;
        /// <summary>
        ///  ftp图片地址
        /// </summary>
        public string ImgFtpPath
        {
            get { return _imgFtpPath; }
            set { _imgFtpPath = value; }
        }
        private string _soundFtpPath;
        /// <summary>
        /// ftp声音地址
        /// </summary>
        public string SoundFtpPath
        {
            get { return _soundFtpPath; }
            set { _soundFtpPath = value; }
        }

        private string _imgStream;
        /// <summary>
        /// 案卷图片流信息
        /// </summary>
        public string ImgStream
        {
            get { return _imgStream; }
            set { _imgStream = value; }
        }
        private string _imgSuffix;
        /// <summary>
        /// 图片后缀名
        /// </summary>
        public string ImgSuffic
        {
            get { return _imgSuffix; }
            set { _imgSuffix = value; }
        }

        private string _soundStream;
        /// <summary>
        /// 案卷图片声音信息
        /// </summary>
        public string SoundStream
        {
            get { return _soundStream; }
            set { _soundStream = value; }
        }


        private string _soundSuffix;
        /// <summary>
        /// 声音后缀名
        /// </summary>
        public string SoundSuffix
        {
            get { return _soundSuffix; }
            set { _soundSuffix = value; }
        }

        #endregion
    }

}
