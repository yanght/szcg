using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;

namespace szcg.web.szbase.systemsetting.logmanage
{
	/// <summary>
	/// 获得系统或模块名称
	/// </summary>
	/// <remarks>邹治武</remarks>
	public class BASE_ModerId
	{
		//系统Id
		const string SYSTEM_ZCPT_10 = "10";		///数字城管支撑平台系
		const string SYSTEM_ZHYW_11 = "11";		//综合业务受理系统
		const string SYSTEM_SPGX_12 = "12";		//视频共享系统
		const string SYSTEM_ZNBG_13 = "13";		//智能办公系统
		const string SYSTEM_SJGX_14 = "14";		//数据共享与交换系统
		const string SYSTEM_WLFB_15 = "15";		//网络及WAP发布系统
		const string SYSTEM_CLDW_16 = "16";		//GPS车辆定位系统
		const string SYSTEM_YDCJ_17 = "17";		//移动采集及办公系统
		const string SYSTEM_YYKF_18 = "18";		//GIS应用开发系统
		const string SYSTEM_GFYG_19 = "19";		//高分辨率遥感影像
		//模块Id
		const string MODEL_10100 = "10100";		//门户管理
		const string MODEL_10101 = "10101";		//组织机构管理
		const string MODEL_10102 = "10102";		//角色权限管理
		const string MODEL_10103 = "10103";		//工作流管理
		const string MODEL_10104 = "10104";		//模板定制
		const string MODEL_10105 = "10105";		//系统性能管理
		const string MODEL_10106 = "10106";		//系统设置
		const string MODEL_10107 = "10107";		//个人办公
		const string MODEL_101001000 = "101001000";		//网站定制工具
		const string MODEL_101001001 = "101001001";		//信息与公告发布
		const string MODEL_101001002 = "101001002";		//个人页面定制
		const string MODEL_101011000 = "101011000";		//用户管理
		const string MODEL_101011001 = "101011001";		//群组管理
		const string MODEL_101031000 = "101031000";		//流程节点定制
		const string MODEL_101031001 = "101031001";		//流程权限设置
		const string MODEL_101031002 = "101031002";		//流程维护
		const string MODEL_101031003 = "101031003";		//工作流调度
		const string MODEL_101041000 = "101041000";		//查询模板定制
		const string MODEL_101041001 = "101041001";		//评价模型定制
		const string MODEL_101051000 = "101051000";		//性能监控
		const string MODEL_101051001 = "101051001";		//日志管理
		const string MODEL_101061000 = "101061000";		//字典库设置
		const string MODEL_101061001 = "101061001";		//数据共享与交换
		const string MODEL_101061002 = "101061002";		//数据备份与还原
		const string MODEL_101071000 = "101071000";		//短信收发设置
		const string MODEL_101071001 = "101071001";		//个人通讯设置
		const string MODEL_101071002 = "101071002";		//其他通用设置
		const string MODEL_11100 = "11100";		//案卷受理
		const string MODEL_11101 = "11101";		//我的消息
		const string MODEL_11102 = "11102";		//监督员管理
		const string MODEL_11103 = "11103";		//使用地图
		const string MODEL_11104 = "11104";		//知识库与公文栏
		const string MODEL_11105 = "11105";		//意见征集与反馈
		const string MODEL_11106 = "11106";		//业务群组管理
		const string MODEL_11107 = "11107";		//回收站
		const string MODEL_11108 = "11108";		//个人信息
		const string MODEL_11109 = "11109";		//综合评价
		const string MODEL_111001000 = "111001000";		//登记栏
		const string MODEL_111001001 = "111001001";		//公众举报栏
		const string MODEL_111001002 = "111001002";		//核查栏
		const string MODEL_111001003 = "111001003";		//移文件
		const string MODEL_111001004 = "111001004";		//存档案卷
		const string MODEL_111001005 = "111001005";		//查询箱
		const string MODEL_111001006 = "111001006";		//统计箱
		const string MODEL_111001007 = "111001007";		//办理栏
		const string MODEL_111001008 = "111001008";		//督办栏
		const string MODEL_111001009 = "111001009";		//通用查询
		const string MODEL_111011000 = "111011000";		//业务消息
		const string MODEL_111011001 = "111011001";		//PDA消息
		const string MODEL_111021000 = "111021000";		//人员监督
		const string MODEL_111021001 = "111021001";		//查询定位
		const string MODEL_111041000 = "111041000";		//知识库
		const string MODEL_111041001 = "111041001";		//公文栏
		const string MODEL_111051000 = "111051000";		//征集意见
		const string MODEL_111051001 = "111051001";		//意见反馈
		const string MODEL_111091000 = "111091000";		//部门评价
		const string MODEL_111091001 = "111091001";		//岗位评价
		const string MODEL_111091002 = "111091002";		//区域评价
		const string MODEL_111011002 = "111011002";		//其他消息
		//字典库
		const int DICTIONARY_1 = 1;			//日志常用语
		const int DICTIONARY_2 = 2;			//惯用语
		const int DICTIONARY_3 = 3;			//办公用语

		public BASE_ModerId()
		{

		}

//		/// <summary>
//		/// 获取系统名称、模块名称
//		/// </summary>
//		/// <param name="argCode">系统Id、模块Id</param>
//		/// <returns>系统名称、模块名称</returns>
//		private string getId(string argCode)
//		{
//			try
//			{
//				SqlConnection myConnection = new SqlConnection(ConfigSettings.Connection;
//				SqlCommand myCommand = new SqlCommand("getModelName", myConnection);
//				myCommand.CommandType = CommandType.StoredProcedure;
//				myCommand.Parameters.Add("@modelcode", SqlDbType.VarChar).Value	= argCode;
//				SqlDataReader reader = myCommand.ExecuteReader();
//				reader.Read();
//				return reader["code"].ToString();
//			}
//			catch
//			{
//				throw;
//			}
//		}

		/// <summary>
		/// 获取数字城管支撑平台系名称
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_ZCPT()
		{
			return SYSTEM_ZCPT_10;
		}

		/// <summary>
		/// 获取综合业务受理系统名称
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_ZHYW()
		{
			return SYSTEM_ZHYW_11;
		}

		/// <summary>
		/// 获取视频共享系统名称
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_SPGX()
		{
			return SYSTEM_SPGX_12;
		}

		/// <summary>
		/// 获取智能办公系统名称
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_ZNBG()
		{
			return SYSTEM_ZNBG_13;
		}

		/// <summary>
		/// 获取数据共享与交换系统名称
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_SJGX()
		{
			return SYSTEM_SJGX_14;
		}

		/// <summary>
		/// 网络及WAP发布系统
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_WLFB()
		{
			return SYSTEM_WLFB_15;
		}

		/// <summary>
		/// GPS车辆定位系统
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_CLDW()
		{
			return SYSTEM_CLDW_16;
		}

		/// <summary>
		/// 移动采集及办公系统
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_YDCJ()
		{
			return SYSTEM_YDCJ_17;
		}

		/// <summary>
		/// GIS应用开发系统
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_YYKF()
		{
			return SYSTEM_YYKF_18;
		}

		/// <summary>
		/// 高分辨率遥感影像
		/// </summary>
		/// <returns>系统名称</returns>
		public static string getSystem_GFYG()
		{
			return SYSTEM_GFYG_19;
		}

		/// <summary>
		/// 门户管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10100()
		{
			return MODEL_10100;
		}

		/// <summary>
		/// 组织机构管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10101()
		{
			return MODEL_10101;
		}

		/// <summary>
		/// 角色权限管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10102()
		{
			return MODEL_10102;
		}

		/// <summary>
		/// 工作流管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10103()
		{
			return MODEL_10103;
		}

		/// <summary>
		/// 模板定制
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10104()
		{
			return MODEL_10104;
		}

		/// <summary>
		/// 系统性能管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10105()
		{
			return MODEL_10105;
		}

		/// <summary>
		/// 系统设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10106()
		{
			return MODEL_10106;
		}

		/// <summary>
		/// 个人办公
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_10107()
		{
			return MODEL_10107;
		}

		/// <summary>
		/// 网站定制工具
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101001000()
		{
			return MODEL_101001000;
		}

		/// <summary>
		/// 信息与公告发布
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101001001()
		{
			return MODEL_101001001;
		}

		/// <summary>
		/// 个人页面定制
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101001002()
		{
			return MODEL_101001002;
		}

		/// <summary>
		/// 用户管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101011000()
		{
			return MODEL_101011000;
		}

		/// <summary>
		/// 群组管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101011001()
		{
			return MODEL_101011001;
		}

		/// <summary>
		/// 流程节点定制
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101031000()
		{
			return MODEL_101031000;
		}

		/// <summary>
		/// 流程权限设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101031001()
		{
			return MODEL_101031001;
		}

		/// <summary>
		/// 流程维护
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101031002()
		{
			return MODEL_101031002;
		}

		/// <summary>
		/// 工作流调度
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101031003()
		{
			return MODEL_101031003;
		}

		/// <summary>
		/// 查询模板定制
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101041000()
		{
			return MODEL_101041000;
		}

		/// <summary>
		/// 评价模型定制
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101041001()
		{
			return MODEL_101041001;
		}

		/// <summary>
		/// 性能监控
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101051000()
		{
			return MODEL_101051000;
		}

		/// <summary>
		/// 日志管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101051001()
		{
			return MODEL_101051001;
		}

		/// <summary>
		/// 字典库设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101061000()
		{
			return MODEL_101061000;
		}

		/// <summary>
		/// 数据共享与交换
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101061001()
		{
			return MODEL_101061001;
		}

		/// <summary>
		/// 数据备份与还原
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101061002()
		{
			return MODEL_101061002;
		}

		/// <summary>
		/// 短信收发设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101071000()
		{
			return MODEL_101071000;
		}

		/// <summary>
		/// 个人通讯设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101071001()
		{
			return MODEL_101071001;
		}

		/// <summary>
		/// 其他通用设置
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_101071002()
		{
			return MODEL_101071002;
		}

		/// <summary>
		/// 案卷受理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11100()
		{
			return MODEL_11100;
		}

		/// <summary>
		/// 我的消息
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11101()
		{
			return MODEL_11101;
		}

		/// <summary>
		/// 监督员管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11102()
		{
			return MODEL_11102;
		}

		/// <summary>
		/// 使用地图
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11103()
		{
			return MODEL_11103;
		}

		/// <summary>
		/// 知识库与公文栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11104()
		{
			return MODEL_11104;
		}

		/// <summary>
		/// 意见征集与反馈
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11105()
		{
			return MODEL_11105;
		}

		/// <summary>
		/// 业务群组管理
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11106()
		{
			return MODEL_11106;
		}

		/// <summary>
		/// 回收站
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11107()
		{
			return MODEL_11107;
		}

		/// <summary>
		/// 个人信息
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11108()
		{
			return MODEL_11108;
		}

		/// <summary>
		/// 综合评价
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_11109()
		{
			return MODEL_11109;
		}

		/// <summary>
		/// 登记栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001000()
		{
			return MODEL_111001000;
		}

		/// <summary>
		/// 公众举报栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001001()
		{
			return MODEL_111001001;
		}

		/// <summary>
		/// 核查栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001002()
		{
			return MODEL_111001002;
		}

		/// <summary>
		/// 移文件
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001003()
		{
			return MODEL_111001003;
		}

		/// <summary>
		/// 存档案卷
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001004()
		{
			return MODEL_111001004;
		}

		/// <summary>
		/// 查询箱
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001005()
		{
			return MODEL_111001005;
		}

		/// <summary>
		/// 统计箱
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001006()
		{
			return MODEL_111001006;
		}

		/// <summary>
		/// 办理栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001007()
		{
			return MODEL_111001007;
		}

		/// <summary>
		/// 督办栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001008()
		{
			return MODEL_111001008;
		}

		/// <summary>
		/// 通用查询
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111001009()
		{
			return MODEL_111001009;
		}

		/// <summary>
		/// 业务消息
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111011000()
		{
			return MODEL_111011000;
		}

		/// <summary>
		/// PDA消息
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111011001()
		{
			return MODEL_111011001;
		}

		/// <summary>
		/// 人员监督
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111021000()
		{
			return MODEL_111021000;
		}

		/// <summary>
		/// 查询定位
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111021001()
		{
			return MODEL_111021001;
		}

		/// <summary>
		/// 知识库
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111041000()
		{
			return MODEL_111041000;
		}

		/// <summary>
		/// 公文栏
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111041001()
		{
			return MODEL_111041001;
		}

		/// <summary>
		/// 征集意见
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111051000()
		{					
			return MODEL_111051000;
		}

		/// <summary>
		/// 意见反馈
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111051001()
		{
			return MODEL_111051001;
		}

		/// <summary>
		/// 部门评价
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111091000()
		{
			return MODEL_111091000;
		}

		/// <summary>
		/// 岗位评价
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111091001()
		{
			return MODEL_111091001;
		}

		/// <summary>
		/// 区域评价
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111091002()
		{
			return MODEL_111091002;
		}

		/// <summary>
		/// 其他消息
		/// </summary>
		/// <returns>模块名称</returns>
		public static string getModel_111011002()
		{
			return MODEL_111011002;
		}

		/// <summary>
		/// 日志常用语
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_1()
		{
			return DICTIONARY_1;
		}

		/// <summary>
		/// 惯用语
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_2()
		{
			return DICTIONARY_2;
		}

		/// <summary>
		/// 办公用语
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_3()
		{
			return DICTIONARY_3;
		}
	}
}
