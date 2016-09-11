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
        public string TreatTime { get; set; }
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
        public int TargetDepartCode { get; set; }
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

    }

}
