using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 案卷详细
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 案卷年份
        /// </summary>
        public string StartYear { get; set; }
        /// <summary>
        /// 是否已结案
        /// </summary>
        public bool IsEnd { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string BigClassName { get; set; }
        /// <summary>
        /// 小类名称
        /// </summary>
        public string SmallClassName { get; set; }
        /// <summary>
        /// 事部件类型（事件，部件）
        /// </summary>
        public string ProbClassName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FileNum { get; set; }
        /// <summary>
        /// 超时状态
        /// </summary>
        public int TimeState { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string ReturnedFlag { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string SeriaNumber { get; set; }
        /// <summary>
        /// 案卷编号
        /// </summary>
        public string Projcode { get; set; }
        /// <summary>
        /// 案卷名称
        /// </summary>
        public string ProjName { get; set; }
        /// <summary>
        /// 流程节点Id
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 案卷受理时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 案卷结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SteptDate { get; set; }
        /// <summary>
        /// 案卷来源
        /// </summary>
        public string ProbSource { get; set; }
        /// <summary>
        /// 案卷来源名称
        /// </summary>
        public string ProbSourceName
        {
            get
            {
                string rtn = string.Empty;
                #region
                switch (ProbSource)
                {
                    case "0":
                        rtn = "公众举报";
                        break;
                    case "1":
                        rtn = "监督员上报";
                        break;
                    case "2":
                        rtn = "领导交办";
                        break;
                    case "3":
                        rtn = "网站举报";
                        break;
                    case "4":
                        rtn = "传真举报";
                        break;
                    case "5":
                        rtn = "短信举报";
                        break;
                    case "6":
                        rtn = "信访举报";
                        break;
                    case "7":
                        rtn = "媒体举报";
                        break;
                    case "8":
                        rtn = "邮件举报";
                        break;
                    case "9":
                        rtn = "其他举报";
                        break;
                    case "10":
                        rtn = "监督员快速上报";
                        break;
                    case "-1":
                        rtn = "坐席拒接";
                        break;
                    case "11":
                        rtn = "电话举报";
                        break;
                    case "15":
                        rtn = "微信举报";
                        break;
                    case "16":
                        rtn = "监控抓拍";
                        break;
                    default:
                        rtn = "未知";
                        break;

                }
                #endregion
                return rtn;
            }
        }
        /// <summary>
        /// 接线员名称（当前操作用户）
        /// </summary>
        public string Telephonist { get; set; }
        /// <summary>
        /// 接线员编号（当前操作用户）
        /// </summary>
        public string TelephonistCode { get; set; }
        /// <summary>
        /// 事部件类型（1：事件 2：部件）
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 大类编码
        /// </summary>
        public string BigClass { get; set; }
        /// <summary>
        /// 小类编码
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 案卷所属区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 案卷所属街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 街道编号
        /// </summary>
        public string StreetId { get; set; }
        /// <summary>
        /// 案卷所属社区
        /// </summary>
        public string Square { get; set; }
        /// <summary>
        /// 社区编号
        /// </summary>
        public string SquareId { get; set; }
        /// <summary>
        /// 基础网格（单元网格）
        /// </summary>
        public string GrideCode { get; set; }
        /// <summary>
        /// 工作网格
        /// </summary>
        public string WorkGrid { get; set; }
        /// <summary>
        /// 案卷坐标经纬度
        /// </summary>
        public string Fid { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude
        {
            get
            {
                if (!string.IsNullOrEmpty(Fid))
                {
                    return Fid.Split(',')[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude
        {
            get
            {
                if (!string.IsNullOrEmpty(Fid))
                {
                    return Fid.Split(',')[1];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 案卷详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 案卷详细描述
        /// </summary>
        public string ProbDesc { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public string IsLock { get; set; }
        /// <summary>
        /// 是否回退
        /// </summary>
        public bool IsReturned { get; set; }
        /// <summary>
        /// 是否督办
        /// </summary>
        public bool IsPress { get; set; }
        /// <summary>
        /// 是否是重大案卷
        /// </summary>
        public string IsGreat { get; set; }
        public bool IsTransaction { get; set; }
        /// <summary>
        /// 是否需要向公众反馈
        /// </summary>
        public bool IsNeedFeedBack { get; set; }
        /// <summary>
        /// 是否手工录入的案卷
        /// </summary>
        public string IsManual { get; set; }
        /// <summary>
        /// /职能部门处置时限
        /// </summary>
        public int DepartTime { get; set; }
        /// <summary>
        /// 职能部门处置时限类型
        /// </summary>
        public int DepartTimeType { get; set; }
        public DateTime TraceTime { get; set; }

        /// <summary>
        /// 案卷流程时间
        /// </summary>
        public string TreatTime { get; set; }
        /// <summary>
        /// 派发时间
        /// </summary>
        public string RacTime { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public string LimitTime { get; set; }
        /// <summary>
        /// 案卷终止时间
        /// </summary>
        public string EndTime { get; set; }


        public string ImpeachName { get; set; }
        public string ImpeachTel { get; set; }
        /// <summary>
        /// 核查反馈信息
        /// </summary>
        public string PdaMsgContent { get; set; }
        /// <summary>
        /// PDA 核查状态
        /// </summary>
        public int IoFlag { get; set; }
        public int ProcessTypeId { get; set; }
        /// <summary>
        /// 处理类型
        /// </summary>
        public int ProcessType { get; set; }
        /// <summary>
        /// 处理部门编码
        /// </summary>
        public int TargetDepartCode { get; set; }
        /// <summary>
        /// 处理部门名称
        /// </summary>
        public string TargetDepartName { get; set; }
        /// <summary>
        /// 结果反馈的专业部门
        /// </summary>
        public int DoDepartCode { get; set; }
        public int PdaIoFlag { get; set; }
        public int DeptProjectState { get; set; }
        public int DepartType { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public int StepId { get; set; }
        public int StateId { get; set; }
        public string ButtonCode { get; set; }

        /// <summary>
        /// 举报人姓名
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 举报人电话
        /// </summary>
        public string ReportTel { get; set; }


        #region

        /// <summary>
        /// 案卷图片
        /// </summary>
        public List<ProjectFile> ProjectFiles { get; set; }
        /// <summary>
        /// 案卷声音
        /// </summary>
        public List<ProjectSound> ProjectSounds { get; set; }
        /// <summary>
        /// 监督员
        /// </summary>
        public List<Collecter> Collecters { get; set; }

        /// <summary>
        /// 监督员核查信息
        /// </summary>
        public List<ProjectCheckMessage> CollectorMessages { get; set; }

        #endregion    
        

    }

    /// <summary>
    /// 案卷图片
    /// </summary>
    public class ProjectFile
    {
        public int FileState { get; set; }
        public string FilePath { get; set; }
        public DateTime CuDate { get; set; }
    }

    /// <summary>
    ///案卷声音 
    /// </summary>
    public class ProjectSound
    {
        public int SoundState { get; set; }
        public string SoundPath { get; set; }
        public DateTime CuDate { get; set; }
    }

    /// <summary>
    /// 事部件大类
    /// </summary>
    public class ProjectBigClass
    {
        public string Id { get; set; }
        /// <summary>
        /// 大类编码
        /// </summary>
        public string BigClassCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string Memo { get; set; }
        public string Link_BigClassCode { get; set; }
    }

    /// <summary>
    /// 事部件小类
    /// </summary>
    public class ProjectSmallClass
    {
        public string Id { get; set; }
        /// <summary>
        /// 大类编码
        /// </summary>
        public string BigClassCode { get; set; }
        /// <summary>
        /// 小类编码
        /// </summary>
        public string SmallCallCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 责任单位
        /// </summary>
        public string Dutyunit { get; set; }

        public int T_Time_Kc { get; set; }

        public int T_Time { get; set; }
        public int T_Time_Ts { get; set; }
        public int T_Time_Gc { get; set; }
        public int GisType { get; set; }
        public string PartSign { get; set; }

        public int RoleCode { get; set; }
        public string Memo { get; set; }
        public string Link_SmallCode { get; set; }
        public string Max_Nemuric { get; set; }
        public string Min_Nemuric { get; set; }

        #region  time1

        public int T1Time { get; set; }
        public int T1Time_2 { get; set; }
        public int T1Time_3 { get; set; }
        public int T1Time_4 { get; set; }

        public int T1Time_LA { get; set; }
        public int T1Time_PQ { get; set; }
        public int T1Time_DB { get; set; }
        public int T1Time_HC { get; set; }
        public string T1Name { get; set; }

        public int T1Type { get; set; }
        public int T1Type_2 { get; set; }
        public int T1Type_3 { get; set; }
        public int T1Type_4 { get; set; }

        public int T1Type_LA { get; set; }
        public int T1Type_PQ { get; set; }
        public int T1Type_DB { get; set; }
        public int T1Type_HC { get; set; }

        #endregion

        #region  time2

        public int T2Time { get; set; }
        public int T2Time_2 { get; set; }
        public int T2Time_3 { get; set; }
        public int T2Time_4 { get; set; }

        public int T2Time_LA { get; set; }
        public int T2Time_PQ { get; set; }
        public int T2Time_DB { get; set; }
        public int T2Time_HC { get; set; }
        public string T2Name { get; set; }

        public int T2Type { get; set; }
        public int T2Type_2 { get; set; }
        public int T2Type_3 { get; set; }
        public int T2Type_4 { get; set; }

        public int T2Type_LA { get; set; }
        public int T2Type_PQ { get; set; }
        public int T2Type_DB { get; set; }
        public int T2Type_HC { get; set; }

        #endregion

        #region  time3

        public int T3Time { get; set; }
        public int T3Time_2 { get; set; }
        public int T3Time_3 { get; set; }
        public int T3Time_4 { get; set; }

        public int T3Time_LA { get; set; }
        public int T3Time_PQ { get; set; }
        public int T3Time_DB { get; set; }
        public int T3Time_HC { get; set; }
        public string T3Name { get; set; }

        public int T3Type { get; set; }
        public int T3Type_2 { get; set; }
        public int T3Type_3 { get; set; }
        public int T3Type_4 { get; set; }

        public int T3Type_LA { get; set; }
        public int T3Type_PQ { get; set; }
        public int T3Type_DB { get; set; }
        public int T3Type_HC { get; set; }

        #endregion

        #region  time4

        public int T4Time { get; set; }
        public int T4Time_2 { get; set; }
        public int T4Time_3 { get; set; }
        public int T4Time_4 { get; set; }

        public int T4Time_LA { get; set; }
        public int T4Time_PQ { get; set; }
        public int T4Time_DB { get; set; }
        public int T4Time_HC { get; set; }
        public string T4Name { get; set; }

        public int T4Type { get; set; }
        public int T4Type_2 { get; set; }
        public int T4Type_3 { get; set; }
        public int T4Type_4 { get; set; }

        public int T4Type_LA { get; set; }
        public int T4Type_PQ { get; set; }
        public int T4Type_DB { get; set; }
        public int T4Type_HC { get; set; }

        #endregion

        #region  time5

        public int T5Time { get; set; }
        public int T5Time_2 { get; set; }
        public int T5Time_3 { get; set; }
        public int T5Time_4 { get; set; }

        public int T5Time_LA { get; set; }
        public int T5Time_PQ { get; set; }
        public int T5Time_DB { get; set; }
        public int T5Time_HC { get; set; }
        public string T5Name { get; set; }

        public int T5Type { get; set; }
        public int T5Type_2 { get; set; }
        public int T5Type_3 { get; set; }
        public int T5Type_4 { get; set; }

        public int T5Type_LA { get; set; }
        public int T5Type_PQ { get; set; }
        public int T5Type_DB { get; set; }
        public int T5Type_HC { get; set; }

        #endregion

        #region  time6

        public int T6Time { get; set; }
        public int T6Time_2 { get; set; }
        public int T6Time_3 { get; set; }
        public int T6Time_4 { get; set; }

        public int T6Time_LA { get; set; }
        public int T6Time_PQ { get; set; }
        public int T6Time_DB { get; set; }
        public int T6Time_HC { get; set; }
        public string T6Name { get; set; }

        public int T6Type { get; set; }
        public int T6Type_2 { get; set; }
        public int T6Type_3 { get; set; }
        public int T6Type_4 { get; set; }

        public int T6Type_LA { get; set; }
        public int T6Type_PQ { get; set; }
        public int T6Type_DB { get; set; }
        public int T6Type_HC { get; set; }

        #endregion

        #region  time7

        public int T7Time { get; set; }
        public int T7Time_2 { get; set; }
        public int T7Time_3 { get; set; }
        public int T7Time_4 { get; set; }

        public int T7Time_LA { get; set; }
        public int T7Time_PQ { get; set; }
        public int T7Time_DB { get; set; }
        public int T7Time_HC { get; set; }
        public string T7Name { get; set; }

        public int T7Type { get; set; }
        public int T7Type_2 { get; set; }
        public int T7Type_3 { get; set; }
        public int T7Type_4 { get; set; }

        public int T7Type_LA { get; set; }
        public int T7Type_PQ { get; set; }
        public int T7Type_DB { get; set; }
        public int T7Type_HC { get; set; }


        #endregion

        #region  time8

        public int T8Time { get; set; }
        public int T8Time_2 { get; set; }
        public int T8Time_3 { get; set; }
        public int T8Time_4 { get; set; }

        public int T8Time_LA { get; set; }
        public int T8Time_PQ { get; set; }
        public int T8Time_DB { get; set; }
        public int T8Time_HC { get; set; }
        public string T8Name { get; set; }

        public int T8Type { get; set; }
        public int T8Type_2 { get; set; }
        public int T8Type_3 { get; set; }
        public int T8Type_4 { get; set; }

        public int T8Type_LA { get; set; }
        public int T8Type_PQ { get; set; }
        public int T8Type_DB { get; set; }
        public int T8Type_HC { get; set; }

        #endregion

        #region  time9

        public int T9Time { get; set; }
        public int T9Time_2 { get; set; }
        public int T9Time_3 { get; set; }
        public int T9Time_4 { get; set; }

        public int T9Time_LA { get; set; }
        public int T9Time_PQ { get; set; }
        public int T9Time_DB { get; set; }
        public int T9Time_HC { get; set; }
        public string T9Name { get; set; }

        public int T9Type { get; set; }
        public int T9Type_2 { get; set; }
        public int T9Type_3 { get; set; }
        public int T9Type_4 { get; set; }

        public int T9Type_LA { get; set; }
        public int T9Type_PQ { get; set; }
        public int T9Type_DB { get; set; }
        public int T9Type_HC { get; set; }

        #endregion

        #region  time10

        public int T10Time { get; set; }
        public int T10Time_2 { get; set; }
        public int T10Time_3 { get; set; }
        public int T10Time_4 { get; set; }

        public int T10Time_LA { get; set; }
        public int T10Time_PQ { get; set; }
        public int T10Time_DB { get; set; }
        public int T10Time_HC { get; set; }
        public string T10Name { get; set; }

        public int T10Type { get; set; }
        public int T10Type_2 { get; set; }
        public int T10Type_3 { get; set; }
        public int T10Type_4 { get; set; }

        public int T10Type_LA { get; set; }
        public int T10Type_PQ { get; set; }
        public int T10Type_DB { get; set; }
        public int T10Type_HC { get; set; }

        #endregion


    }

}
