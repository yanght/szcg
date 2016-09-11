using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace bacgDL.szbase.grid
{
     public class GridManageDAL: Teamax.Common.CommonDatabase
    {
        public struct TreeSuruct
        {
            public string pcode;
            public string code;
            public string text;
            public string tag;
        }

        public struct TreeSuructZYBM
        {
            public string pcode;
            public string code;
            public string text;
            public string tag;
            public string isduty;
        }
        #region GetAreaList��ȡ����������¼
        /// <summary>
        /// ȡ����������¼
        /// </summary>
        /// <param name="areacode">�������</param>
        /// <returns></returns>
         public ArrayList GetAreaList(string areacode, ref string strErr)
         {
             string area = "";
             if (areacode.Length > 6)
             {
                 area = areacode.Substring(0, 6);
             }
             else
             {
                 area = areacode;
             }
             ArrayList areas = new ArrayList();
             string strSQL = string.Format(@"select areacode,areaname 
                                            from s_area
                                            where char(areacode) = '{0}'", area);
             try
             {
                 IDataReader dr = (IDataReader)this.ExecuteReader(strSQL);
                 while (dr.Read())
                 {
                     string[] areaInfo = new string[2];
                     areaInfo[0] = dr["areacode"].ToString();
                     areaInfo[1] = dr["areaname"].ToString();
                     areas.Add(areaInfo);
                 }

                 dr.Close();
                 return areas;

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
        #endregion

         #region GetAreaInfoByAreacode��������������ȡ������Ϣ
         /// <summary>
         /// ������������ȡ������Ϣ
         /// </summary>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
         public DataSet GetAreaInfoByAreacode(int areacode, ref string strErr)
         {

             string strSQL = string.Format(@"select * from s_area
                                            where char(areacode) = '{0}'", areacode);
             try
             {
                 return this.ExecuteDataset(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion


         #region GetStreetByAreaId��������������ȡ�ֵ��б�
         /// <summary>
         /// ������������ȡ�ֵ��б�
         /// </summary>
         /// <param name="currentAreaCode">��ǰ��������</param>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
         public ArrayList GetStreetByAreaId(string areacode, string currentAreaCode, ref string strErr)
         {
             string strSQL = "";
             ArrayList streets = new ArrayList();
             if (currentAreaCode.Length > 8)
             {
                 strSQL = string.Format(@"select id,streetname 
                                            from s_street
                                            where char(streetcode) = '{0}' order by id", currentAreaCode.Substring(0, 9));
             }
             else
             {
                 strSQL = string.Format(@"select id,streetname 
                                            from s_street
                                            where char(areacode) = '{0}' order by id", areacode);
             }

             try
             {
                 IDataReader dr = (IDataReader)this.ExecuteReader(strSQL);

                 while (dr.Read())
                 {
                     string[] street = new string[2];
                     street[0] = dr["id"].ToString();
                     street[1] = dr["streetname"].ToString();
                     streets.Add(street);
                 }

                 dr.Close();
                 return streets;

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region GetCommunityByStreetId�����ݽֵ������ȡ�����б�
         /// <summary>
         /// ���ݽֵ������ȡ�����б�
         /// </summary>
         /// <param name="streetid">�ֵ�id</param>
         /// <returns></returns>
         public ArrayList GetCommunityByStreetId(string streetid, string areacode, ref string strErr)
         {
             string strSQL = "";
             ArrayList communitys = new ArrayList();
             if (areacode.Length > 10)
             {
                 strSQL = string.Format(@"select a.id,a.commname 
                                            from s_community
                                            where char(commcode) = '{0}'", areacode);
             }
             else
             {
                 strSQL = string.Format(@"select a.id,a.commname 
                                            from s_community a left join s_street b
                                            on a.streetcode=b.streetcode
                                            where char(b.id) = '{0}'", streetid);
             }
             try
             {
                 IDataReader dr = (IDataReader)this.ExecuteReader(strSQL);
                 while (dr.Read())
                 {
                     string[] community = new string[2];
                     community[0] = dr["id"].ToString();
                     community[1] = dr["commname"].ToString();
                     communitys.Add(community);
                 }

                 dr.Close();
                 return communitys;
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region GetGridByCommunityId���������������ȡ�����б�
         /// <summary>
         /// �������������ȡ�����б�
         /// </summary>
         /// <param name="commcode">����id</param>
         /// <returns></returns>
         public ArrayList GetGridByCommunityId(string commid, ref string strErr)
         {
             ArrayList grids = new ArrayList();
             string strSQL = string.Format(@"select a.id,a.gridcode 
                                            from s_grid a left join s_community b
                                            on a.commcode=b.commcode
                                            where char(b.id)='{0}'", commid);
             try
             {
                 IDataReader dr = (IDataReader)this.ExecuteReader(strSQL);
                 while (dr.Read())
                 {
                     string[] grid = new string[2];
                     grid[0] = dr["id"].ToString();
                     grid[1] = dr["gridcode"].ToString();
                     grids.Add(grid);
                 }
                 dr.Close();
                 return grids;

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion
         #region GetStreetInfoByStreetCode�����ݽֵ������ȡ�ֵ���Ϣ
         /// <summary>
         /// ���ݽֵ������ȡ�ֵ���Ϣ
         /// </summary>
         /// <param name="streetid">�ֵ�id</param>
         /// <returns></returns>
         public DataSet GetStreetInfoByStreetCode(int streetid, ref string strErr)
         {

             string strSQL = string.Format(@"select a.*,b.areaname from s_street a left join s_area b
                                            on a.areacode=b.areacode
                                            where char(a.id) = '{0}'", streetid);
             try
             {
                return this.ExecuteDataset(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region GetCommunityInfoByCommCode���������������ȡ������Ϣ
         /// <summary>
         /// �������������ȡ������Ϣ
         /// </summary>
         /// <param name="commId">����id</param>
         /// <returns></returns>
         public DataSet GetCommunityInfoByCommCode(int commId, ref string strErr)
         {

             string strSQL = string.Format(@"select a.*,b.streetname from s_community a left join s_street b
                                            on a.streetcode=b.streetcode
                                            where char(a.id) = '{0}'", commId);
             try
             {
                 return this.ExecuteDataset(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region GetGridInfoByGridCode����������ID��ȡ������Ϣ
         /// <summary>
         /// ��������ID��ȡ������Ϣ
         /// </summary>
         /// <param name="gridId">����Id</param>
         /// <param name="strErr">���ش�����Ϣ</param>
         /// <returns></returns>
         public DataSet GetGridInfoByGridCode(string gridId, ref string strErr)
         {
             try
             {
                 SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("@GridId", gridId) };
                 return this.ExecuteDataset("pr_s_GridInfo", CommandType.StoredProcedure, arrSP);

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region DeleteFromArea��ɾ��������Ϣ
         /// <summary>
         /// ɾ��������Ϣ
         /// </summary>
         /// <param name="areaCode">�������</param>
         /// <param name="flag">״̬��Ϣ</param>0��ɾ�������쳣1����ʶɾ���ɹ�2��������ڽֵ���ɾ��ʧ��
         /// <param name="strErr">����Ĵ�����Ϣ</param> 
         /// <returns></returns>
         public void DeleteFromArea(int areaCode, out int flag, ref string strErr)
         {
             
             SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("id",areaCode), 
                                new SqlParameter("result",SqlDbType.Int) };
             arrSP[1].Direction = ParameterDirection.Output;
             try
             {
                 this.ExecuteDataset("pr_p_DeleteFromArea", CommandType.StoredProcedure, arrSP);
                 flag = Convert.ToInt32(arrSP[1].Value);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 flag = 0;
             }
         }
         #endregion

         #region DeleteFromStreet��ɾ���ֵ���Ϣ
         /// <summary>
         /// ɾ���ֵ���Ϣ
         /// </summary>
         /// <param name="streetId">�ֵ�id</param>
         /// <param name="flag">״̬��Ϣ</param>0��ɾ�������쳣1����ʶɾ���ɹ�2��������ڽֵ���ɾ��ʧ��
         /// <param name="strErr">����Ĵ�����Ϣ</param> 
         /// <returns></returns>
         public void DeleteFromStreet(int streetId, out int flag, ref string strErr)
         {
             SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("id",streetId), 
                                new SqlParameter("result",SqlDbType.Int) };
             arrSP[1].Direction = ParameterDirection.Output;
             try
             {
                 this.ExecuteDataset("pr_p_DeleteFromStreet", CommandType.StoredProcedure, arrSP);
                 flag = Convert.ToInt32(arrSP[1].Value);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 flag = 0;
             }
         }
         #endregion

         #region DeleteFromCommunity��ɾ��������Ϣ
         /// <summary>
         /// ɾ��������Ϣ
         /// </summary>
         /// <param name="commId">����id</param>
         /// <param name="flag">״̬��Ϣ</param>0��ɾ�������쳣1����ʶɾ���ɹ�2��������ڽֵ���ɾ��ʧ��
         /// <param name="strErr">����Ĵ�����Ϣ</param> 
         /// <returns></returns>
         public void DeleteFromCommunity(int commId, out int flag, ref string strErr)
         {
             SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("id",commId), 
                                new SqlParameter("result",SqlDbType.Int) };
             arrSP[1].Direction = ParameterDirection.Output;
             try
             {
                 this.ExecuteDataset("pr_p_DeleteFromCommunity", CommandType.StoredProcedure, arrSP);
                 flag = Convert.ToInt32(arrSP[1].Value);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 flag = 0;
             }
         }
         #endregion

         #region DeleteFromGrid��ɾ��������Ϣ
         /// <summary>
         /// ɾ��������Ϣ
         /// </summary>
         /// <param name="gridId">����id</param>
         /// <param name="flag">״̬��Ϣ</param>0��ɾ�������쳣1����ʶɾ���ɹ�2��������ڽֵ���ɾ��ʧ��
         /// <param name="strErr">����Ĵ�����Ϣ</param> 
         /// <returns></returns>
         public void DeleteFromGrid(int gridId, out int flag, ref string strErr)
         {
             SqlParameter[] arrSP = new SqlParameter[] {
                                new SqlParameter("id",gridId), 
                                new SqlParameter("result",SqlDbType.Int) };
             arrSP[1].Direction = ParameterDirection.Output;
             try
             {
                 this.ExecuteDataset("pr_p_DeleteFromGrid", CommandType.StoredProcedure, arrSP);
                 flag = Convert.ToInt32(arrSP[1].Value);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 flag = 0;
             }
         }
         #endregion

         #region CheckAreaCode���ж������Ƿ��Ѿ�����
         /// <summary>
         /// �ж������Ƿ��Ѿ�����
         /// </summary>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
         public bool CheckAreaCode(string areacode, ref string strErr)
         {
             string strSQL = string.Format(@"select count(*) from s_area
                                            where char(areacode) ='{0}'", areacode);
             try
             {
                 return Convert.ToInt32(this.ExecuteScalar(strSQL)) > 0;
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return false;
             }
         }
         #endregion

         #region InsertToArea������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="id">�������</param>
         /// <param name="name">��������</param>
         /// <param name="area">��������</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int InsertToArea(int id, string name, string area,
                                     string square, string population,
                                     string memo, ref string strErr)
         {
             string strSQL = string.Format(@"insert into s_area(areacode,areaname,population,area,memo)
                                            values({0},'{1}',{2},{3},'{4}')", id, name, population, square, memo);
             try
             {
                 return this.ExecuteNonQuery(strSQL);
                 
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region CheckStreetCode���жϽֵ��Ƿ��Ѿ�����
         /// <summary>
         /// �жϽֵ��Ƿ��Ѿ�����
         /// </summary>
         /// <param name="streetCode">�ֵ�����</param>
         /// <returns></returns>
         public int CheckStreetCode(string streetCode, ref string strErr)
         {
             string strSQL = string.Format(@"select count(*) from s_street
                                            where char(streetcode) ='{0}'", streetCode);
             try
             {

                 return Convert.ToInt32(this.ExecuteScalar(strSQL));
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region InsertToStreet������ֵ���Ϣ
         /// <summary>
         /// ����ֵ���Ϣ
         /// </summary>
         /// <param name="streetcode">�ֵ�����</param>
         /// <param name="areacode">�����������</param>
         /// <param name="streetname">�ֵ�����</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int InsertToStreet(string streetcode, string areacode, string streetname,
                                     string square, string population, string memo,
                                     ref string strErr)
         {
             string strSQL = string.Format(@"insert into s_street(streetcode,areacode,streetname,population,area,memo)
                                            values({0},{1},'{2}',{3},{4},'{5}')", 
                                            streetcode, areacode, streetname, population, square, memo);
             try
             {
                     return this.ExecuteNonQuery(strSQL);
                 
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region UpateArea������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="oldId">�޸�ǰ����</param>
         /// <param name="id">�������</param>
         /// <param name="name">��������</param>
         /// <param name="area">��������</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int UpateArea(int oldId, int id, string name,
                                 string area, string square, string population,
                                 string memo, ref string strErr)
         {
             string strSQL = string.Format(@"update s_area set areacode = {0},areaname = '{1}',population = {2},
					                                            area = {3},memo = '{4}'
                                            where char(areacode) ='{5}'", id, name, population, square, memo, oldId);
             try
             {
                 return this.ExecuteNonQuery(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region UpateStreet�����½ֵ���Ϣ
         /// <summary>
         /// ���½ֵ���Ϣ
         /// </summary>
         /// <param name="id">�ֵ�id</param>
         /// <param name="streetCode">�ֵ����</param>
         /// <param name="streetName">�ֵ�����</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int UpateStreet(string id, string streetCode, string streetName,
                                     string square, string population, string memo,
                                     ref string strErr)
         {
             string strSQL = string.Format(@"update s_street set streetcode={0},streetname='{1}',population={2},
						                           area = {3},memo='{4}'
                                            where char(id)='{5}'", streetCode, streetName, population, square, memo, id);
             try
             {
                 return this.ExecuteNonQuery(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region CheckCommCode���ж������Ƿ��Ѿ�����
         /// <summary>
         /// �ж������Ƿ��Ѿ�����
         /// </summary>
         /// <param name="commCode">��������</param>
         /// <returns></returns>
         public int CheckCommCode(string commCode, ref string strErr)
         {
             string strSQL = string.Format(@"select count(*) from s_community
                                            where char(commcode) = '{0}'", commCode);
             try
             {
                return this.ExecuteNonQuery(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region InsertToComm������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="streetId">�ֵ�id</param>
         /// <param name="commCode">��������</param>
         /// <param name="commName">��������</param>
         /// <param name="area">��������</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int InsertToComm(string streetId, string commCode, string commName,
                                     string square, string population, string memo,
                                     ref string strErr)
         {
             string streetSql = string.Format(@"select streetcode 
                                            from s_street
                                            where char(id)='{0}'", streetId);
             try
             {

                 string streetCode = Convert.ToString(this.ExecuteScalar(streetSql));
                 if (streetCode.Length > 0)
                 {
                     string strSQL = string.Format(@"insert into s_community(commcode,streetcode,commname,population,area,memo)
                                            values('{0}',{1},'{2}',{3},{4},'{5}')", commCode, streetCode, commName, population, square, memo);
                     return this.ExecuteNonQuery(strSQL);
                 }
                 return 0;
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region UpateComm������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="commId">����id</param>
         /// <param name="commName">��������</param>
         /// <param name="commCode">��������</param>
         /// <param name="population">�˿�</param>
         /// <param name="square">���</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int UpateComm(string commId, string commName, string commCode,
                                 string population, string square, string memo,
                                 ref string strErr)
         {
             string strSQL = string.Format(@"update s_community set commcode='{0}',commname='{1}',population={2},
						                            area={3},memo='{4}'
                                            where id={5}", commCode, commName, population, square, memo, commId);
             try
             {
                return this.ExecuteNonQuery(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region CheckGridCode���ж������Ƿ��Ѿ�����
         /// <summary>
         /// �ж������Ƿ��Ѿ�����
         /// </summary>
         /// <param name="gridCode">�������</param>
         /// <returns></returns>
         public int CheckGridCode(string gridCode, ref string strErr)
         {
             string strSQL = string.Format(@"select count(*) from s_grid
                                            where char(gridcode) = '{0}'", gridCode);
             try
             {
                 return Convert.ToInt32(this.ExecuteNonQuery(strSQL));
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region InsertToGrid������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="id">����id</param>
         /// <param name="name">��������</param>
         /// <param name="area">��������</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int InsertToGrid(string id, string gridCode, string memo, ref string strErr)
         {
             try
             {
                 string commSQL = string.Format(@"select commcode 
                                                    from s_community
                                                    where id={0}", id);
                 string commCode = Convert.ToString(this.ExecuteScalar(commSQL));
                 if (commCode.Length > 0)
                 {
                     string strSQL = string.Format(@"insert into s_grid(gridcode,commcode,memo)
                                            values('{0}','{1}','{2}')", gridCode, commCode, memo);
                     return this.ExecuteNonQuery(strSQL);
                 }
                 return 0;

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region UpateGrid������������Ϣ
         /// <summary>
         /// ����������Ϣ
         /// </summary>
         /// <param name="oldId">ǰ����</param>
         /// <param name="id">�ֵ�����</param>
         /// <param name="name">�ֵ�����</param>
         /// <param name="area">��������</param>
         /// <param name="square">���</param>
         /// <param name="population">�˿�</param>
         /// <param name="memo">��ע</param>
         /// <param name="strErr">���������Ϣ</param>
         /// <returns></returns>
         public int UpateGrid(string id, string gridCode, string memo, string IsLockLogin, ref string strErr)
         {
             string strSQL = string.Format(@"update s_grid 
                                            set gridcode = '{0}',memo='{1}',IsLockLogin={3}
                                            where id = {2}", gridCode, memo, id, IsLockLogin);
             try
             {
                 return this.ExecuteNonQuery(strSQL);
             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return 0;
             }
         }
         #endregion

         #region GetGridTreeList��ȡ����������Ϣ�������ֵ�������������
         /// <summary>
         /// ȡ����������Ϣ�������ֵ�������������
         /// </summary>
         /// <param name="areacode">�������</param>
         /// <returns></returns>
         public ArrayList GetGridTreeList(string areacode, ref string strErr)
         {
             ArrayList treeStructList = new ArrayList();
             try
             {
                 SqlParameter[] arrSP = new SqlParameter[] { new SqlParameter("areacode", areacode) };
                 DataSet ds = this.ExecuteDataset("pr_p_GetDGridList", CommandType.StoredProcedure, arrSP);

                 if (ds.Tables.Count > 2)
                 {
                     DataTable dtStreet = ds.Tables[0];
                     foreach (DataRow dr in dtStreet.Rows)
                     {
                         TreeSuruct ts;
                         ts.pcode = dr["parentcode"].ToString();
                         ts.code = dr["nodecode"].ToString();
                         ts.text = dr["nodename"].ToString();
                         ts.tag = dr["memo"].ToString();
                         treeStructList.Add(ts);
                     }

                     DataTable dtComm = ds.Tables[1];
                     foreach (DataRow dr in dtComm.Rows)
                     {
                         TreeSuruct ts;
                         ts.pcode = dr["parentcode"].ToString();
                         ts.code = dr["nodecode"].ToString();
                         ts.text = dr["nodename"].ToString();
                         ts.tag = dr["memo"].ToString();
                         treeStructList.Add(ts);
                     }

                     DataTable dtGrid = ds.Tables[2];
                     foreach (DataRow dr in dtGrid.Rows)
                     {
                         TreeSuruct ts;
                         ts.pcode = dr["parentcode"].ToString();
                         ts.code = dr["nodecode"].ToString();
                         ts.text = dr["nodename"].ToString();
                         ts.tag = dr["memo"].ToString();
                         treeStructList.Add(ts);
                     }
                     return treeStructList;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch (Exception ex)
             {
                 strErr = ex.Message;
                 return null;
             }
         }
         #endregion

         #region ��ȡ�������� list
         public ArrayList GetStreetListByParent(string parendtCode)
         {
             ArrayList list = new ArrayList();
             string strSQL = "";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL = " select streetcode,areacode,streetname,id from s_street where char(areacode)='" + parendtCode + "'";
             else
                 strSQL = " select streetcode,areacode,streetname,id from s_street where char(areacode) like '" + parendtCode + "%'";

             IDataReader rs = (IDataReader)this.ExecuteReader(strSQL);

             while (rs.Read())
             {
                 string[] values = new string[4];
                 values[0] = rs["streetcode"].ToString();
                 values[1] = rs["areacode"].ToString();
                 values[2] = rs["streetname"].ToString();
                 values[3] = rs["id"].ToString();
                 list.Add(values);
             }

             rs.Close();
             return list;
         }
         public ArrayList GetCommunityListByParent(string parendtCode)
         {
             ArrayList list = new ArrayList();
             string strSQL = "";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL = "select * from s_community where char(streetcode)='" + parendtCode + "'";
             else
                 strSQL = "select * from s_community where char(streetcode) like '" + parendtCode + "%'";

             IDataReader rs = (IDataReader)this.ExecuteReader(strSQL);

             while (rs.Read())
             {
                 string[] values = new string[4];
                 values[0] = rs["commcode"].ToString();
                 values[1] = rs["streetcode"].ToString();
                 values[2] = rs["commname"].ToString();
                 values[3] = rs["id"].ToString();
                 list.Add(values);
             }

             rs.Close();
             return list;
         }

         public ArrayList GetGridListByParent(string parendtCode)
         {
             ArrayList list = new ArrayList();
             string strSQL = "";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL = "select * from s_grid where char(commcode)='" + parendtCode + "'";
             else
                 strSQL = "select * from s_grid where char(commcode)='" + parendtCode + "%'";

             IDataReader rs = (IDataReader)this.ExecuteReader(strSQL);

             while (rs.Read())
             {
                 string[] values = new string[3];
                 values[0] = rs["gridcode"].ToString();
                 values[1] = rs["commcode"].ToString();
                 values[2] = rs["id"].ToString();
                 list.Add(values);
             }

             rs.Close();
             return list;
         }
         #endregion

         #region
         public DataTable GetStreetTable(string parendtCode, string streetCode)
         {
             string strSQL = " select * from s_street where 1=1 ";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL += " and char(areacode)='" + parendtCode + "'";
             if (!string.IsNullOrEmpty(streetCode))
                 strSQL += " and char(streetcode) = '" + streetCode + "'";

             DataSet ds = this.ExecuteDataset(strSQL);

             if (ds != null && ds.Tables.Count > 0)
                 return ds.Tables[0];
             else
                 return null;

         }
         public DataTable GetCommunityTable(string parendtCode, string communityCode)
         {
             string strSQL = "select * from s_community where 1=1 ";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL += " and char(streetcode)='" + parendtCode + "'";
             if (!string.IsNullOrEmpty(communityCode))
                 strSQL += "and char(commcode) = '" + communityCode + "'";

             DataSet ds = this.ExecuteDataset(strSQL);
             if (ds != null && ds.Tables.Count > 0)
                 return ds.Tables[0];
             else
                 return null;
         }

         public DataTable GetGridTable(string parendtCode, string gridCode)
         {
             string strSQL = "select * from s_grid where 1=1 ";
             if (!string.IsNullOrEmpty(parendtCode))
                 strSQL += " and char(commcode)='" + parendtCode + "'";
             if (!string.IsNullOrEmpty(gridCode))
                 strSQL += " and char(gridcode)='" + gridCode + "'";

             DataSet ds = this.ExecuteDataset(strSQL);

             if (ds != null && ds.Tables.Count > 0)
                 return ds.Tables[0];
             else
                 return null;
         }
         #endregion

    }
}
