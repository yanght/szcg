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
	/// ���ϵͳ��ģ������
	/// </summary>
	/// <remarks>������</remarks>
	public class BASE_ModerId
	{
		//ϵͳId
		const string SYSTEM_ZCPT_10 = "10";		///���ֳǹ�֧��ƽ̨ϵ
		const string SYSTEM_ZHYW_11 = "11";		//�ۺ�ҵ������ϵͳ
		const string SYSTEM_SPGX_12 = "12";		//��Ƶ����ϵͳ
		const string SYSTEM_ZNBG_13 = "13";		//���ܰ칫ϵͳ
		const string SYSTEM_SJGX_14 = "14";		//���ݹ����뽻��ϵͳ
		const string SYSTEM_WLFB_15 = "15";		//���缰WAP����ϵͳ
		const string SYSTEM_CLDW_16 = "16";		//GPS������λϵͳ
		const string SYSTEM_YDCJ_17 = "17";		//�ƶ��ɼ����칫ϵͳ
		const string SYSTEM_YYKF_18 = "18";		//GISӦ�ÿ���ϵͳ
		const string SYSTEM_GFYG_19 = "19";		//�߷ֱ���ң��Ӱ��
		//ģ��Id
		const string MODEL_10100 = "10100";		//�Ż�����
		const string MODEL_10101 = "10101";		//��֯��������
		const string MODEL_10102 = "10102";		//��ɫȨ�޹���
		const string MODEL_10103 = "10103";		//����������
		const string MODEL_10104 = "10104";		//ģ�嶨��
		const string MODEL_10105 = "10105";		//ϵͳ���ܹ���
		const string MODEL_10106 = "10106";		//ϵͳ����
		const string MODEL_10107 = "10107";		//���˰칫
		const string MODEL_101001000 = "101001000";		//��վ���ƹ���
		const string MODEL_101001001 = "101001001";		//��Ϣ�빫�淢��
		const string MODEL_101001002 = "101001002";		//����ҳ�涨��
		const string MODEL_101011000 = "101011000";		//�û�����
		const string MODEL_101011001 = "101011001";		//Ⱥ�����
		const string MODEL_101031000 = "101031000";		//���̽ڵ㶨��
		const string MODEL_101031001 = "101031001";		//����Ȩ������
		const string MODEL_101031002 = "101031002";		//����ά��
		const string MODEL_101031003 = "101031003";		//����������
		const string MODEL_101041000 = "101041000";		//��ѯģ�嶨��
		const string MODEL_101041001 = "101041001";		//����ģ�Ͷ���
		const string MODEL_101051000 = "101051000";		//���ܼ��
		const string MODEL_101051001 = "101051001";		//��־����
		const string MODEL_101061000 = "101061000";		//�ֵ������
		const string MODEL_101061001 = "101061001";		//���ݹ����뽻��
		const string MODEL_101061002 = "101061002";		//���ݱ����뻹ԭ
		const string MODEL_101071000 = "101071000";		//�����շ�����
		const string MODEL_101071001 = "101071001";		//����ͨѶ����
		const string MODEL_101071002 = "101071002";		//����ͨ������
		const string MODEL_11100 = "11100";		//��������
		const string MODEL_11101 = "11101";		//�ҵ���Ϣ
		const string MODEL_11102 = "11102";		//�ලԱ����
		const string MODEL_11103 = "11103";		//ʹ�õ�ͼ
		const string MODEL_11104 = "11104";		//֪ʶ���빫����
		const string MODEL_11105 = "11105";		//��������뷴��
		const string MODEL_11106 = "11106";		//ҵ��Ⱥ�����
		const string MODEL_11107 = "11107";		//����վ
		const string MODEL_11108 = "11108";		//������Ϣ
		const string MODEL_11109 = "11109";		//�ۺ�����
		const string MODEL_111001000 = "111001000";		//�Ǽ���
		const string MODEL_111001001 = "111001001";		//���ھٱ���
		const string MODEL_111001002 = "111001002";		//�˲���
		const string MODEL_111001003 = "111001003";		//���ļ�
		const string MODEL_111001004 = "111001004";		//�浵����
		const string MODEL_111001005 = "111001005";		//��ѯ��
		const string MODEL_111001006 = "111001006";		//ͳ����
		const string MODEL_111001007 = "111001007";		//������
		const string MODEL_111001008 = "111001008";		//������
		const string MODEL_111001009 = "111001009";		//ͨ�ò�ѯ
		const string MODEL_111011000 = "111011000";		//ҵ����Ϣ
		const string MODEL_111011001 = "111011001";		//PDA��Ϣ
		const string MODEL_111021000 = "111021000";		//��Ա�ල
		const string MODEL_111021001 = "111021001";		//��ѯ��λ
		const string MODEL_111041000 = "111041000";		//֪ʶ��
		const string MODEL_111041001 = "111041001";		//������
		const string MODEL_111051000 = "111051000";		//�������
		const string MODEL_111051001 = "111051001";		//�������
		const string MODEL_111091000 = "111091000";		//��������
		const string MODEL_111091001 = "111091001";		//��λ����
		const string MODEL_111091002 = "111091002";		//��������
		const string MODEL_111011002 = "111011002";		//������Ϣ
		//�ֵ��
		const int DICTIONARY_1 = 1;			//��־������
		const int DICTIONARY_2 = 2;			//������
		const int DICTIONARY_3 = 3;			//�칫����

		public BASE_ModerId()
		{

		}

//		/// <summary>
//		/// ��ȡϵͳ���ơ�ģ������
//		/// </summary>
//		/// <param name="argCode">ϵͳId��ģ��Id</param>
//		/// <returns>ϵͳ���ơ�ģ������</returns>
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
		/// ��ȡ���ֳǹ�֧��ƽ̨ϵ����
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_ZCPT()
		{
			return SYSTEM_ZCPT_10;
		}

		/// <summary>
		/// ��ȡ�ۺ�ҵ������ϵͳ����
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_ZHYW()
		{
			return SYSTEM_ZHYW_11;
		}

		/// <summary>
		/// ��ȡ��Ƶ����ϵͳ����
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_SPGX()
		{
			return SYSTEM_SPGX_12;
		}

		/// <summary>
		/// ��ȡ���ܰ칫ϵͳ����
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_ZNBG()
		{
			return SYSTEM_ZNBG_13;
		}

		/// <summary>
		/// ��ȡ���ݹ����뽻��ϵͳ����
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_SJGX()
		{
			return SYSTEM_SJGX_14;
		}

		/// <summary>
		/// ���缰WAP����ϵͳ
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_WLFB()
		{
			return SYSTEM_WLFB_15;
		}

		/// <summary>
		/// GPS������λϵͳ
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_CLDW()
		{
			return SYSTEM_CLDW_16;
		}

		/// <summary>
		/// �ƶ��ɼ����칫ϵͳ
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_YDCJ()
		{
			return SYSTEM_YDCJ_17;
		}

		/// <summary>
		/// GISӦ�ÿ���ϵͳ
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_YYKF()
		{
			return SYSTEM_YYKF_18;
		}

		/// <summary>
		/// �߷ֱ���ң��Ӱ��
		/// </summary>
		/// <returns>ϵͳ����</returns>
		public static string getSystem_GFYG()
		{
			return SYSTEM_GFYG_19;
		}

		/// <summary>
		/// �Ż�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10100()
		{
			return MODEL_10100;
		}

		/// <summary>
		/// ��֯��������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10101()
		{
			return MODEL_10101;
		}

		/// <summary>
		/// ��ɫȨ�޹���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10102()
		{
			return MODEL_10102;
		}

		/// <summary>
		/// ����������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10103()
		{
			return MODEL_10103;
		}

		/// <summary>
		/// ģ�嶨��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10104()
		{
			return MODEL_10104;
		}

		/// <summary>
		/// ϵͳ���ܹ���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10105()
		{
			return MODEL_10105;
		}

		/// <summary>
		/// ϵͳ����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10106()
		{
			return MODEL_10106;
		}

		/// <summary>
		/// ���˰칫
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_10107()
		{
			return MODEL_10107;
		}

		/// <summary>
		/// ��վ���ƹ���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101001000()
		{
			return MODEL_101001000;
		}

		/// <summary>
		/// ��Ϣ�빫�淢��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101001001()
		{
			return MODEL_101001001;
		}

		/// <summary>
		/// ����ҳ�涨��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101001002()
		{
			return MODEL_101001002;
		}

		/// <summary>
		/// �û�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101011000()
		{
			return MODEL_101011000;
		}

		/// <summary>
		/// Ⱥ�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101011001()
		{
			return MODEL_101011001;
		}

		/// <summary>
		/// ���̽ڵ㶨��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101031000()
		{
			return MODEL_101031000;
		}

		/// <summary>
		/// ����Ȩ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101031001()
		{
			return MODEL_101031001;
		}

		/// <summary>
		/// ����ά��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101031002()
		{
			return MODEL_101031002;
		}

		/// <summary>
		/// ����������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101031003()
		{
			return MODEL_101031003;
		}

		/// <summary>
		/// ��ѯģ�嶨��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101041000()
		{
			return MODEL_101041000;
		}

		/// <summary>
		/// ����ģ�Ͷ���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101041001()
		{
			return MODEL_101041001;
		}

		/// <summary>
		/// ���ܼ��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101051000()
		{
			return MODEL_101051000;
		}

		/// <summary>
		/// ��־����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101051001()
		{
			return MODEL_101051001;
		}

		/// <summary>
		/// �ֵ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101061000()
		{
			return MODEL_101061000;
		}

		/// <summary>
		/// ���ݹ����뽻��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101061001()
		{
			return MODEL_101061001;
		}

		/// <summary>
		/// ���ݱ����뻹ԭ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101061002()
		{
			return MODEL_101061002;
		}

		/// <summary>
		/// �����շ�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101071000()
		{
			return MODEL_101071000;
		}

		/// <summary>
		/// ����ͨѶ����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101071001()
		{
			return MODEL_101071001;
		}

		/// <summary>
		/// ����ͨ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_101071002()
		{
			return MODEL_101071002;
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11100()
		{
			return MODEL_11100;
		}

		/// <summary>
		/// �ҵ���Ϣ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11101()
		{
			return MODEL_11101;
		}

		/// <summary>
		/// �ලԱ����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11102()
		{
			return MODEL_11102;
		}

		/// <summary>
		/// ʹ�õ�ͼ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11103()
		{
			return MODEL_11103;
		}

		/// <summary>
		/// ֪ʶ���빫����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11104()
		{
			return MODEL_11104;
		}

		/// <summary>
		/// ��������뷴��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11105()
		{
			return MODEL_11105;
		}

		/// <summary>
		/// ҵ��Ⱥ�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11106()
		{
			return MODEL_11106;
		}

		/// <summary>
		/// ����վ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11107()
		{
			return MODEL_11107;
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11108()
		{
			return MODEL_11108;
		}

		/// <summary>
		/// �ۺ�����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_11109()
		{
			return MODEL_11109;
		}

		/// <summary>
		/// �Ǽ���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001000()
		{
			return MODEL_111001000;
		}

		/// <summary>
		/// ���ھٱ���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001001()
		{
			return MODEL_111001001;
		}

		/// <summary>
		/// �˲���
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001002()
		{
			return MODEL_111001002;
		}

		/// <summary>
		/// ���ļ�
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001003()
		{
			return MODEL_111001003;
		}

		/// <summary>
		/// �浵����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001004()
		{
			return MODEL_111001004;
		}

		/// <summary>
		/// ��ѯ��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001005()
		{
			return MODEL_111001005;
		}

		/// <summary>
		/// ͳ����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001006()
		{
			return MODEL_111001006;
		}

		/// <summary>
		/// ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001007()
		{
			return MODEL_111001007;
		}

		/// <summary>
		/// ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001008()
		{
			return MODEL_111001008;
		}

		/// <summary>
		/// ͨ�ò�ѯ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111001009()
		{
			return MODEL_111001009;
		}

		/// <summary>
		/// ҵ����Ϣ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111011000()
		{
			return MODEL_111011000;
		}

		/// <summary>
		/// PDA��Ϣ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111011001()
		{
			return MODEL_111011001;
		}

		/// <summary>
		/// ��Ա�ල
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111021000()
		{
			return MODEL_111021000;
		}

		/// <summary>
		/// ��ѯ��λ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111021001()
		{
			return MODEL_111021001;
		}

		/// <summary>
		/// ֪ʶ��
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111041000()
		{
			return MODEL_111041000;
		}

		/// <summary>
		/// ������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111041001()
		{
			return MODEL_111041001;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111051000()
		{					
			return MODEL_111051000;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111051001()
		{
			return MODEL_111051001;
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111091000()
		{
			return MODEL_111091000;
		}

		/// <summary>
		/// ��λ����
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111091001()
		{
			return MODEL_111091001;
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111091002()
		{
			return MODEL_111091002;
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <returns>ģ������</returns>
		public static string getModel_111011002()
		{
			return MODEL_111011002;
		}

		/// <summary>
		/// ��־������
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_1()
		{
			return DICTIONARY_1;
		}

		/// <summary>
		/// ������
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_2()
		{
			return DICTIONARY_2;
		}

		/// <summary>
		/// �칫����
		/// </summary>
		/// <returns></returns>
		public static int getDICTIONARY_3()
		{
			return DICTIONARY_3;
		}
	}
}
