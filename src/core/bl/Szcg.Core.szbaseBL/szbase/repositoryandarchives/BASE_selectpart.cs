//* ****************************************************************************************
// * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
// * ��    ;����֪ʶ�ⲿ�Ž�����ʾ��������á�
// * �ṹ��ɣ� ��szcg.web.szbase.repositoryandarchives.BASE_selectpartҳ��ҵ����з�װ
// * ��    �ߣ���ΰ
// * �������ڣ�2007-06-11
// * ��ʷ��¼��
// * ****************************************************************************************
// * �޸���Ա��               
// * �޸����ڣ� 
// * �޸�˵����   
// * **************************************************************************************using System;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using AjaxPro;

namespace bacgBL.web.szbase.repositoryandarchives
{
    public class BASE_selectpart
    {
        public BASE_selectpart()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        private const int pageSize = 15;
        private const string NAMESPACE_PATH = "bacgBL.web.szbase.repositoryandarchives";


        /// <summary>
        /// �ӹ��ı���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">��</param>
        /// <returns>�б���أ�����ID�����ı���</returns>
        //public ArrayList getAllArchives()
        //{
        //     try
        //    {
        //        using (bacgDL.szbase.repositoryandarchives.BASE_archivesmanage dl = new bacgDL.szbase.repositoryandarchives.BASE_archivesmanage())
        //        {
        //            return dl.getAllArchives ();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// �Ӳ��ű���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">userId��area</param>
        /// <returns>���������Ϣ</returns>
        public static ArrayList getDepts(int userId, string areacode)
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getDepts(userId, areacode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



        /// <summary>
        /// �Ӳ��ű���ȡ���� 
        /// </summary>
        /// <param name="argUserCode">userId��area</param>
        /// <returns>���������Ϣ</returns>
        public  ArrayList getDepts()
        {
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getDepts();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
          

        }



        /// <summary>
        /// �Ӳ��ű���ȡ��С�ĸ��ڵ�������� 
        /// </summary>
        /// <param name="argUserCode">��</param>
        /// <returns>�б���أ����������Ϣ</returns>

        public static int getMinParentDepartID(int userId)
        {
            //string sql="select min(parentcode) from depart";
            //string sql = "SELECT MIN(PARENTCODE) FROM P_DEPART";
            try
            {
                using (bacgDL.szbase.repositoryandarchives.BASE_selectpart dl = new bacgDL.szbase.repositoryandarchives.BASE_selectpart())
                {
                    return dl.getMinParentDepartID( userId );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }


    }
}
