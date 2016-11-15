using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    /// <summary>
    /// 监督员
    /// </summary>
    public class Collecter
    {
        /// <summary>
        /// 监督员编号
        /// </summary>
        public int CollCode { get; set; }
        /// <summary>
        /// 监督员姓名
        /// </summary>
        public string CollName { get; set; }
        /// <summary>
        /// 所属社区编码
        /// </summary>
        public string CommCode { get; set; }
        /// <summary>
        /// 所属网格编码
        /// </summary>
        public string GridCode { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 是否是领导
        /// </summary>
        public int IsGuard { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 城管通号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 社区名称
        /// </summary>
        public string CommName { get; set; }
        /// <summary>
        /// 街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 城管通版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HireDate { get; set; }
        public string FireDate { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 头像图片地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomeTel { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string HomeAddress { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public string Cu_Date { get; set; }
        /// <summary>
        /// 左边经度
        /// </summary>
        public string Cu_X { get; set; }
        /// <summary>
        /// 坐标纬度
        /// </summary>
        public string Cu_Y { get; set; }
        /// <summary>
        /// 轨迹上报时间间隔（分钟）
        /// </summary>
        public string  TimeOut { get; set; }
        /// <summary>
        /// 其他备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IMSI { get; set; }
        /// <summary>
        /// 手机串号
        /// </summary>
        public string IMEI { get; set; }
        public string PowerFlag { get; set; }
        /// <summary>
        /// 是否在岗
        /// </summary>
        public string IsGuardName { get; set; }
        /// <summary>
        /// GPS状态是否在线
        /// </summary>
        public string IsGPS { get; set; }
    }
}
