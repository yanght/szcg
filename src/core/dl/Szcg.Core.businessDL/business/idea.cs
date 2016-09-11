/* ****************************************************************************************
 * ��Ȩ���У����˿�˼����Ƽ����޹�˾ 
 * ��    ;���ҵ���Ϣ-���ݲ�����ࡣ
 * �ṹ��ɣ�
 * ��    �ߣ�Ԭ����
 * �������ڣ�2007-07-17
 * ��ʷ��¼��
 * ****************************************************************************************
 * �޸���Ա��               
 * �޸����ڣ� 
 * �޸�˵����   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


using Teamax.Common;

namespace bacgDL.business
{
    public class idea : Teamax.Common.CommonDatabase
    {
        public int insertIdea(string title, string content, string usercode, string memo)
        {
            string strSQL = string.Format(@"insert into b_opinion_collect(title,cudate,content,usercode,memo)  
                                values('{0}',getdate(),'{1}','{2}','{3}')  select @@Identity", title, content, usercode, memo);
            object obj = ExecuteScalar(strSQL);
            return int.Parse(obj.ToString());
        }

        public int UpdateFileIdeaAtt(int opinionid, string attname, string physicalname, int size)
        {
            string strSQL = string.Format(@"insert into b_opinion_collect_att(opinionid,attname,physicalname,filesize)
                                            values('{0}','{1}','{2}','{3}')", opinionid, attname, physicalname, size);
            return this.ExecuteNonQuery(strSQL);
        }

        #region GetContent:��ȡ��˼�����뷴��ϵͳ����б�
        /// <summary>
        /// ��ȡ��˼�����뷴��ϵͳ����б�
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageSize">ÿҳ��ʾ������</param>
        /// <param name="ErrorString">���ش�����Ϣ</param>
        /// <returns></returns>
        public DataSet GetContent(int CurrentPage, int PageSize, string Order, string Field,string name,string begintime,string endtime, ref string ErrorString)
        {
            //try
            //{
            //    string select = "a.*,b.username";
            //    string from ="b_opinion_collect a left join p_user b on a.usercode=b.usercode ";
            //    string where = " (a.projcode = '' or a.projcode is null) ";
            //    QueryUtil qu = new QueryUtil(select, from, where);
            //    qu.Key = "id";
            //    qu.SortBy = "id";
            //    qu.SortOrder = SortOrder.Descending;
            //    qu.PageSize = size;
            //    qu.ExecuteDataset(page);
            //    string ids="";
            //    for (int i = 0; i < qu.ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (ids == "")
            //            ids = qu.ds.Tables[0].Rows[i]["id"].ToString();
            //        else
            //            ids +=","+ qu.ds.Tables[0].Rows[i]["id"].ToString();
            //    }
            //    DataTable dt= this.ExecuteDataset("select fid as id,count(*) as count from b_opinion_feedback where fid in("+ids+") group by fid").Tables[0];
            //    DataTable dt1 = qu.ds.Tables[0];
            //    dt1.Columns.Add("count");
            //    for (int i = 0; i < dt1.Rows.Count; i++)
            //    {
            //        bool tag =true;
            //        for (int j = 0; j < dt.Rows.Count; j++)
            //        {
            //            if (dt1.Rows[i]["id"].ToString() == dt.Rows[j]["id"].ToString())
            //            {
            //                dt1.Rows[i]["count"] = "�ѷ���"+dt.Rows[j]["count"].ToString()+"��";
            //                tag =false;
            //                break;
            //            }
            //        }
            //        if (tag)
            //            dt1.Rows[i]["count"] = "û�з�����Ϣ";
                    
            //    }
            //    DataTable dt2 = this.ExecuteDataset("select  opinionid as id,attname, physicalName from b_opinion_collect_att where opinionid in(" + ids + ")").Tables[0].Copy();
            //    qu.ds.Tables.Add(dt2);

            //    return qu;
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.WriteLine(e.Message);
            //    return null;
            //}
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@Order",Order),
                                new SqlParameter("@Field",Field),
                                new SqlParameter("@CurrentPage",CurrentPage),
                                new SqlParameter("@PageSize",PageSize),
                                new SqlParameter("@Name",name),
                                new SqlParameter("@Begintime",begintime),
                                new SqlParameter("@Endtime",endtime)};
            try
            {
                DataSet dt = this.ExecuteDataset("pr_b_GetContent", CommandType.StoredProcedure, arrSP);
                return dt;
            }
            catch (Exception ex)
            {
                ErrorString = ex.ToString();
                return null;
            }
        }
        #endregion

        #region getinfoByid:����ID����ѯ��ϸ��Ϣ
        /// <summary>
        /// ����ID����ѯ��ϸ��Ϣ
        /// </summary>
        /// <param name="id">Ҫ��ѯ��ID</param>
        /// <param name="strErr">���صĴ�����Ϣ</param>
        /// <returns></returns>
        public QueryUtil getinfoByid(string id, ref string strErr)
        {
            try
            {
                string select = "a.*,b.username";
                string from = "b_opinion_collect a left join p_user b on a.usercode=b.usercode ";
                string where = " (a.projcode = '' or a.projcode is null) and a.id="+id;
                QueryUtil qu = new QueryUtil(select, from, where);
                qu.Key = "id";
                qu.SortBy = "id";
                qu.SortOrder = Teamax.Common.SortOrder.Descending;
                qu.PageSize = 1;
                qu.ExecuteDataset(1);
                string ids = "";
                for (int i = 0; i < qu.ds.Tables[0].Rows.Count; i++)
                {
                    if (ids == "")
                        ids = qu.ds.Tables[0].Rows[i]["id"].ToString();
                    else
                        ids += "," + qu.ds.Tables[0].Rows[i]["id"].ToString();
                }
                DataTable dt = this.ExecuteDataset("select fid as id,count(*) as count from b_opinion_feedback where fid in(" + ids + ") group by fid").Tables[0];
                DataTable dt1 = qu.ds.Tables[0];
                dt1.Columns.Add("count");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    bool tag = true;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt1.Rows[i]["id"].ToString() == dt.Rows[j]["id"].ToString())
                        {
                            dt1.Rows[i]["count"] = "�ѷ���" + dt.Rows[j]["count"].ToString() + "��";
                            tag = false;
                            break;
                        }
                    }
                    if (tag)
                        dt1.Rows[i]["count"] = "û�з�����Ϣ";

                }
                DataTable dt2 = this.ExecuteDataset("select  opinionid as id,attname, physicalName from b_opinion_collect_att where opinionid in(" + ids + ")").Tables[0].Copy();
                qu.ds.Tables.Add(dt2);

                return qu;
            }
            catch (Exception e)
            {
                strErr = e.ToString();
                return null;
            }
        }
        #endregion
    }
}
