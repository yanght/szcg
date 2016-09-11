using System;
using System.Collections.Generic;
using System.Text;

namespace bacgDL.business
{
    /// <summary>
    /// ������Ϣ�ṹ��
    /// </summary>
    public class ProjectInfo
    {
        public string NodeId = ""; //�ڵ�
        public string TargetDepartCode = ""; //Ӧ������
        public string DoDepartCode = ""; //�ṹ������רҵ����
        public string stepid = ""; //�׶�
        public string _cityId = "";//�У���
        public string areacode = ""; //����
        public string role = ""; //��ɫ
        public string projcode = ""; //�������
        public string projcodeName = ""; //�������
        public string IsManual = "0"; //�Ƿ��ֹ��Ǽǵİ���
        public string probsource = ""; //
        public string Telephonist = ""; //����Ա
        public string TelephonistCode = ""; //����Ա����
        public string Fid = ""; //
        public string typecode = ""; //
        public string bigClass = ""; //
        public string smallclass = ""; //
        public string detailedclass = "";//
        public string partID = "";
        public string IsNeedFeedBack = "0"; //��Ҫ�ظ�
        public string isFeadback = "0";    //�Ƿ��ѻظ�
        public string IsDel = "";
        public string startdate = "";  //����ʱ��
        public string ispress = "";    //�Ƿ񶽰�
        public string _area = ""; //
        public string _street = ""; //
        public string _square = ""; //
        public string gridcode = ""; //��������
        public string WorkGrid = ""; //��������
        public string ProcessType = ""; //��������
        public string address = ""; //
        public string isthough = "0"; //
        public string isgreat = "0"; //
        public string groupid = "5301"; //
        public string probdesc = ""; //
        public string ButtonId = "";//
        public string strButtonId = "";
        public string departcode = "";
        public string strUserCode = "";
        public string strCityName = "";//��������

        public string PdaIoFlag = ""; //PDA �˲�״̬
        public string Pdamsg = "";    //�˲鷴����Ϣ

        public string departTime = "0";   //ְ�ܲ��Ŵ���ʱ��
        public string departTimeType = "0"; //ְ�ܲ��Ŵ���ʱ������

        public string withDept = "";//���Ŵ���ʽ

        //�Ǽ��ϴ�ͼƬ���  2009-4-15  ���
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

        #region   �ϴ���ͼƬ��������Ϣ  ���ļ���Ϣ���ļ���׺
        private string _upLoadType;
        /// <summary>
        /// �ϴ������� 0 �˲顢1 ��ʵ
        /// </summary>
        public string UpLoadType
        {
            get { return _upLoadType; }
            set { _upLoadType = value; }
        }

        private string _imgFtpPath;
        /// <summary>
        ///  ftpͼƬ��ַ
        /// </summary>
        public string ImgFtpPath
        {
            get { return _imgFtpPath; }
            set { _imgFtpPath = value; }
        }
        private string _soundFtpPath;
        /// <summary>
        /// ftp������ַ
        /// </summary>
        public string SoundFtpPath
        {
            get { return _soundFtpPath; }
            set { _soundFtpPath = value; }
        }

        private string _imgStream;
        /// <summary>
        /// ����ͼƬ����Ϣ
        /// </summary>
        public string ImgStream
        {
            get { return _imgStream; }
            set { _imgStream = value; }
        }
        private string _imgSuffix;
        /// <summary>
        /// ͼƬ��׺��
        /// </summary>
        public string ImgSuffic
        {
            get { return _imgSuffix; }
            set { _imgSuffix = value; }
        }

        private string _soundStream;
        /// <summary>
        /// ����ͼƬ������Ϣ
        /// </summary>
        public string SoundStream
        {
            get { return _soundStream; }
            set { _soundStream = value; }
        }


        private string _soundSuffix;
        /// <summary>
        /// ������׺��
        /// </summary>
        public string SoundSuffix
        {
            get { return _soundSuffix; }
            set { _soundSuffix = value; }
        }

        #endregion
    }

}
