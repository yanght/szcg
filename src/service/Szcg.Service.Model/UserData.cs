using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Model
{
    public class UserData
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string departName { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string departcode { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string loginName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        public string parentID { get; set; }
        /// <summary>
        /// 个人主页
        /// </summary>
        public string priWeb { get; set; }
        /// <summary>
        /// 公共主页
        /// </summary>
        public string pubWeb { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string hometel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string zipCode { get; set; }
        /// <summary>
        /// 个人手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 部门手机
        /// </summary>
        public string mobile1 { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string user_areacode { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 头像图片地址
        /// </summary>
        public string strVirPath { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 呼叫中心用户编号
        /// </summary>
        public string callCenterUserID { get; set; }
        public string gradeList { get; set; }
        /// <summary>
        /// 核查栏核查权限
        /// </summary>
        public string Hcpower { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 视频流权限级别
        /// </summary>
        public string videolevel { get; set; }
        public string centerusercode { get; set; }
        /// <summary>
        /// 所属街道名称
        /// </summary>
        public string streetname { get; set; }
        /// <summary>
        /// 所属角色编码
        /// </summary>
        public string roleids { get; set; }
        /// <summary>
        /// 所属角色名称
        /// </summary>
        public string rolenames { get; set; }

    }
}
