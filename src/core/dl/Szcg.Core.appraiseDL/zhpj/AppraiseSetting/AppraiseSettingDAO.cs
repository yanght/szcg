using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using bacgDL.business;
using DBbase.zhpj;


namespace bacgDL.zhpj
{
    public class AppraiseSettingDAO : Teamax.Common.CommonDatabase
    {
        private int resutlCount = 0;

        public int getResutlCount()
        {
            return resutlCount;
        }

        public void setResutlCount(int resutlCount)
        {
            this.resutlCount = resutlCount;
        }

         /*
	     * ����һ���Զ����ֶ�
	     */
        public int insertAppraiseSetting(AppraiseSetting appraiseSetting, ref string strErr)
        {
            String isDisplay = appraiseSetting.getIsDisplay();
            String isBase = "1";
            String insertSql = "";
            // ��������sql���
            InsertSqlMaker insertSqlMaker = new InsertSqlMaker("a_appraise_field_detail");
            //insertSqlMaker.addData("ID", "'"+ appraiseSetting.getId()+ "'");
            insertSqlMaker.addData("FIELDNAME", "'" + appraiseSetting.getFieldName() + "'");
            insertSqlMaker.addData("VIEWNAME", "'" + appraiseSetting.getViewName() + "'");
            insertSqlMaker.addData("CODENAME", "'" + appraiseSetting.getCodeName() + "'");
            insertSqlMaker.addData("EXPRESS", "'" + appraiseSetting.getExpress() + "'");
            insertSqlMaker.addData("EXPRESS_", "'" + appraiseSetting.getExpress_() + "'");
            insertSqlMaker.addData("ISBASE", "'" + isBase + "'");
            insertSqlMaker.addData("ISDISPLAY", "'" + isDisplay + "'");
            insertSqlMaker.addData("MODEL", "'" + appraiseSetting.getModel() + "'");
            insertSqlMaker.addData("model_defined", "'" + appraiseSetting.getModelDefined() + "'");
            insertSqlMaker.addData("REMARK", "'" + appraiseSetting.getRemark() + "'");
            insertSqlMaker.addData("ROLEID", "'" + appraiseSetting.getRoleid() + "'");
            int order_ = this.getFieldOrderNum(appraiseSetting.getModel()) + 1;
            insertSqlMaker.addData("ORDER_", "" + order_ + "");
            //insertSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSetting.getFormTemplate() + "'");

            insertSqlMaker.addData("TABLENAME", "'" + appraiseSetting.getTablename() + "'");
            insertSqlMaker.addData("PROCEDURENAME", "'" + appraiseSetting.getProcedurename() + "'");

            insertSql = insertSqlMaker.getSql();
            // ִ�в���
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(insertSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
	     * ɾ��һ���Զ����ֶΣ�ֻ����ɾ���û����ݣ�
	     */
        public int deleteField(String id, ref string strErr)
        {
		    String sql = " delete from a_appraise_field_detail where id='"+id+"' and isbase='1'";
            // ִ�в���
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(sql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
	    }

        /**
         * ����ֶε�˳���
         */
        public int getFieldOrderNum(string model)
        {
            int res = 0;
            String sql = " select isnull(max(order_),0) as ORDER_ from a_appraise_field_detail where model='" + model + "' ";
            // ִ�в���
            SqlDataReader dr = (SqlDataReader)this.ExecuteReader(sql);
            while (dr.Read())
            {
                res = Convert.ToInt32(dr["ORDER_"].ToString());
            }
            dr.Close();
            return res;
        }

        /*
         * ���ֶν�������
         */
        //public void orderField(string model, string fieldID, string status, ref string strErr)
        //{
        //    //���ȵõ��ֶε�λ��
        //    int order_num = 0;
        //    int max_num = 0;
        //    int min_num = 0;
        //    String sql = " select order_ from a_appraise_field_detail where id='" + fieldID + "'";
        //    String sql2 = " select isnull(max(order_),0) as MAX_ORDER,isnull(min(order_),0) as MIN_ORDER from a_appraise_field_detail where model='" + model + "'";
        //    try
        //    {
        //        SqlDataReader vResult = (SqlDataReader)this.ExecuteReader(sql);
        //        Hashtable hTemp = null;
        //        while (vResult.Read())
        //        {
        //            if ("".Equals(vResult["ORDER_"].ToString()))
        //            {
        //                order_num = 0;
        //            }
        //            else
        //            {
        //                order_num = Convert.ToInt32(vResult["ORDER_"].ToString());
        //            }
        //        }
        //        vResult.Close();
        //        SqlDataReader vResult2 = (SqlDataReader)this.ExecuteReader(sql2);
        //        while (vResult2.Read())
        //        {
        //            if (vResult2["MAX_ORDER"].ToString() == null)
        //            {
        //                max_num = 0;
        //            }
        //            else
        //            {
        //                max_num = Convert.ToInt32(vResult2["MAX_ORDER"].ToString());
        //            }
        //            if (vResult2["MIN_ORDER"].ToString() == null)
        //            {
        //                min_num = 0;
        //            }
        //            else
        //            {
        //                min_num = Convert.ToInt32(vResult2["MIN_ORDER"].ToString());
        //            }
        //        }
        //        vResult2.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //    try
        //    {
        //        sql2 = "";
        //        if ("UP".Equals(status))
        //        {//�����ƶ�
        //            if (min_num != order_num)
        //            {//��ǰλ�ò��ڵ�һλ
        //                sql = " update a_appraise_field_detail set order_=order_+1 where model='" + model + "'and order_=" + (order_num - 1) + "";
        //                sql2 = " update a_appraise_field_detail set order_=order_-1 where id = '" + fieldID + "'";
        //                this.ExecuteReader(sql);
        //                this.ExecuteReader(sql2);
        //            }
        //        }
        //        else
        //        {//�����ƶ�
        //            if (max_num != order_num)
        //            {//��ǰλ�ò������һλ
        //                sql = " update a_appraise_field_detail set order_=order_-1 where model='" + model + "'and order_=" + (order_num + 1) + "";
        //                sql2 = " update a_appraise_field_detail set order_=order_+1 where id = '" + fieldID + "'";
        //                this.ExecuteReader(sql);
        //                this.ExecuteReader(sql2);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //}


        /**
         * ����һ����¼
         * @param appraiseSetting
         * ��װҪ���µ���Ϣ
         */
        public int updateAppraiseSetting(AppraiseSetting appraiseSetting, ref string strErr)
        {
            if (appraiseSetting.getId() == null
                    || "".Equals(appraiseSetting.getId()))
            {
                strErr = "AppraiseSettingDAO.updateAppraiseSetting()|appraiseSetting.ID����Ϊ��";
                return 0;
            }
            String updateSql = "";
            String isDisplay = appraiseSetting.getIsDisplay();
            // ������µ�sql���
            UpdateSqlMaker updateSqlMaker = new UpdateSqlMaker("a_appraise_field_detail");
            updateSqlMaker.addData("codeName", "'" + appraiseSetting.getCodeName() + "'");
            updateSqlMaker.addData("express", "'" + appraiseSetting.getExpress() + "'");
            updateSqlMaker.addData("express_", "'" + appraiseSetting.getExpress_() + "'");
            updateSqlMaker.addData("viewName", "'" + appraiseSetting.getViewName() + "'");
            updateSqlMaker.addData("FIELDNAME", "'" + appraiseSetting.getFieldName() + "'");

            updateSqlMaker.addData("isDisplay", "'" + isDisplay + "'");
            updateSqlMaker.addData("model", "'" + appraiseSetting.getModel() + "'");
            updateSqlMaker.addData("model_defined", "'" + appraiseSetting.getModelDefined() + "'");
            updateSqlMaker.addData("remark", "'" + appraiseSetting.getRemark() + "'");
            updateSqlMaker.addData("roleid", "'" + appraiseSetting.getRoleid() + "'");

            updateSqlMaker.addData("TABLENAME", "'" + appraiseSetting.getTablename() + "'");
            updateSqlMaker.addData("PROCEDURENAME", "'" + appraiseSetting.getProcedurename() + "'");

            updateSqlMaker.setWhere("id = '" + appraiseSetting.getId() + "'");
            updateSql = updateSqlMaker.getSql();
            // ִ�в���
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(updateSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
        * ����һ���Զ���ģ��
        */
        public int insertAppraiseSettingModel(AppraiseSettingModel appraiseSettingModel, ref string strErr)
        {
            String insertSql = "";
            // ��������sql���
            InsertSqlMaker insertSqlMaker = new InsertSqlMaker("a_appraise_model");

            insertSqlMaker.addData("ID", "@id");
            insertSqlMaker.addData("MODELNAME", "'" + appraiseSettingModel.getModelName() + "'");
            insertSqlMaker.addData("PARENTMODEL", "'" + appraiseSettingModel.getParentModel() + "'");
            insertSqlMaker.addData("ISBASE", "'" + appraiseSettingModel.getIsbase() + "'");
            insertSqlMaker.addData("REMARK", "'" + appraiseSettingModel.getRemark() + "'");
            insertSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSettingModel.getFormTemplate() + "'");

            insertSql = insertSqlMaker.getSql();
            // ִ�в���
            try
            {
                int result = 0;
                insertSql = "declare @id int select @id=max(id)+1 from dbo.a_appraise_model " + insertSql;

                result = this.ExecuteNonQuery(insertSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
	     * ɾ��һ���Զ���ģ��
	     */
        public int deleteAppraiseSettingModel(String id, ref string strErr)
        {
            String sql = " update a_appraise_model set IsDel = 1 where id='" + id + "' and isbase='1'";
            // ִ�в���
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(sql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
         * �༭һ���Զ���ģ��
         */
        public int updateAppraiseSettingModel(AppraiseSettingModel appraiseSettingModel, ref string strErr)
        {
            if (appraiseSettingModel.getId() == null
                    || "".Equals(appraiseSettingModel.getId()))
            {
                strErr = "AppraiseSettingDAO.updateAppraiseSetting()|appraiseSetting.ID����Ϊ��";
                return 0;
            }
            String updateSql = "";
            // ������µ�sql���
            UpdateSqlMaker updateSqlMaker = new UpdateSqlMaker("a_appraise_model");

            updateSqlMaker.addData("MODELNAME", "'" + appraiseSettingModel.getModelName() + "'");
            updateSqlMaker.addData("PARENTMODEL", "'" + appraiseSettingModel.getParentModel() + "'");
            updateSqlMaker.addData("ISBASE", "'" + appraiseSettingModel.getIsbase() + "'");
            updateSqlMaker.addData("REMARK", "'" + appraiseSettingModel.getRemark() + "'");
            updateSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSettingModel.getFormTemplate() + "'");

            updateSqlMaker.setWhere("id = '" + appraiseSettingModel.getId() + "'");
            updateSql = updateSqlMaker.getSql();
            // ִ�в���
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(updateSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
         * У��ͬ��ģ��
         */
        public int getSameNameNum(string name,string id)
        {
            int res = 0;
            String sql = " select isnull(count(modelname),0) as sum from a_appraise_model where isnull(isdel,0)<>1 and parentmodel = '" + id + "' and modelname='" + name + "' ";
            // ִ�в���
            SqlDataReader dr = (SqlDataReader)this.ExecuteReader(sql);
            while (dr.Read())
            {
                res = Convert.ToInt32(dr["sum"].ToString());
            }
            dr.Close();
            return res;
        }

        /// <summary>
        /// �õ�ģ�����������б�
        /// </summary>
        /// <returns></returns>
        public DataSet getModelinfolist(AppraiseSettingModel prj, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@ID",prj.getParentModel()),
                                new SqlParameter("@MODELNAME",prj.getModelName()),
                                new SqlParameter("@CurrentPage",page.CurrentPage),    
                                new SqlParameter("@RowCount",SqlDbType.Int),          //���
                                new SqlParameter("@PageCount",SqlDbType.Int),         //���
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[3].Direction = ParameterDirection.Output;
            arrSP[4].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetModelinfolist", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[3].Value.ToString();
            page.PageCount = arrSP[4].Value.ToString();

            return ds;
        }

        /// <summary>
        /// �õ��Զ����ֶ���Ϣ�б�
        /// </summary>
        /// <returns></returns>
        public DataSet getFieldList(AppraiseSetting prj, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@viewName",prj.getViewName()),
                                new SqlParameter("@codeName",prj.getCodeName()),
                                new SqlParameter("@model",prj.getModel()),  
                                new SqlParameter("@modeldefined",prj.getModelDefined()),          
                                new SqlParameter("@CurrentPage",page.CurrentPage),    
                                new SqlParameter("@RowCount",SqlDbType.Int),          //���
                                new SqlParameter("@PageCount",SqlDbType.Int),         //���
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetAppraiseSetting", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }

        /// <summary>
        /// Ϊ�Զ����ֶν�������
        /// </summary>
        /// <returns></returns>
        public void orderField(string model, string id, string status)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@model",model),
                                new SqlParameter("@id",id),
                                new SqlParameter("@status",status)};
            this.ExecuteNonQuery("pr_a_orderField", CommandType.StoredProcedure, arrSP);
        }

        #region ExecReaderSql��ִ��sql���,����datareader���͵����ݼ�
        /// <summary>
        /// ִ��sql���,����datareader���͵����ݼ�
        /// </summary>
        /// <param name="strSQL">Ҫ��ѯ��sql���</param>
        /// <returns></returns>
        public SqlDataReader ExecReaderSql(string strSQL)
        {
            return (SqlDataReader)this.ExecuteReader(strSQL);
        }
        #endregion

        /*
         * �õ��Զ����ֶ���Ϣ�б�
         */
        //public ArrayList getFieldList(AppraiseSetting appraiseSetting)
        //{
        //    ArrayList list = new ArrayList(); //��ŷ��ؽ���Ķ���
        //    String querySql = "select ID from a_appraise_field_detail where 1=1 ";
        //    if (!"".Equals(appraiseSetting.getViewName()))
        //    {
        //        querySql += " and viewName like '" + appraiseSetting.getViewName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getCodeName()))
        //    {
        //        querySql += " and codeName like '" + appraiseSetting.getCodeName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getModel()))
        //    {
        //        querySql += " and model = '" + appraiseSetting.getModel() + "'";
        //    }
        //    querySql += " order by ORDER_ asc ";
        //    // ִ�в���
        //    DbOperator dbOperator = new DbOperator();
        //    Vector vResult = null;
        //    try
        //    {
        //        dbOperator.getConnection();
        //        vResult = dbOperator.openPageSql(querySql, startIndex, pageSize);
        //        this.resutlCount = dbOperator.getResultCount();
        //        Hashtable hTemp = null;
        //        for (int i = 0; i < vResult.size(); i++)
        //        {
        //            hTemp = (Hashtable)vResult.get(i);
        //            String id = (String)hTemp.get("ID");
        //            list.add(this.getFieldDetail(id));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.printStackTrace();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dbOperator.freeConnection();
        //    }
        //    return list;
        //}

        /*
         * �ж�ָ������Ƿ����
         */
        public Boolean isCodeNameExist(String codeName, ref string strErr)
        {
            Boolean res = false;
            String querySql = "select * from a_appraise_field_detail where codeName='" + codeName.Trim() + "'";

            // ִ�в���
            try
            {

                SqlDataReader dr = (SqlDataReader)this.ExecuteReader(querySql);
                if (dr.Read())
                {
                    res = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return res;
        }

        /*
         * �ж�fieldname�Ƿ����
         */
        public Boolean isFieldNameExist(String fieldName, ref string strErr)
        {
            Boolean res = false;
            String querySql = "select * from a_appraise_field_detail where fieldName='" + fieldName.Trim() + "'";

            // ִ�в���
            try
            {
                SqlDataReader dr = (SqlDataReader)this.ExecuteReader(querySql);
                if (dr.Read())
                {
                    res = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return res;
        }

        /*
         * �õ��Զ����ֶ�
         */
        //public ArrayList getFieldList(AppraiseSetting appraiseSetting, ref string strErr)
        //{
        //    ArrayList list = new ArrayList(); //��ŷ��ؽ���Ķ���
        //    String querySql = "select ID from a_appraise_field_detail where 1=1 ";
        //    if (!"".Equals(appraiseSetting.getViewName()))
        //    {
        //        querySql += " and viewName like '" + appraiseSetting.getViewName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getCodeName()))
        //    {
        //        querySql += " and codeName like '" + appraiseSetting.getCodeName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getModel()))
        //    {
        //        querySql += " and model = '" + appraiseSetting.getModel() + "'";
        //    }
        //    querySql += " order by ID desc ";


        //     ִ�в���
        //    try
        //    {
        //        DataSet ds = this.ExecuteDataset(querySql);
        //        this.resutlCount = ds.Tables[0].Rows.Count;
        //        for (int i = 0; i < resutlCount; i++)
        //        {
        //            String id = ds.Tables[0].Rows[0].ToString();
        //            list.Add(id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //    return list;
        //}

    }

    public class InsertSqlMaker
    {

        public InsertSqlMaker(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("���ݿ��������Ϊ�գ�");
            }
            this.tableName = tableName;
        }

        /** ����*/
        private String tableName;

        /** ���ڱ�����������*/
        ArrayList arrayList_Data = new ArrayList();

        /** ����һ�����ݵ�bean*/
        OptionBean optionBean = null;

        /**
         * ���Ҫ���������
         * @param fieldName �ֶ���
         * @param data �ֶ����ݣ����������ַ�������''��
         */
        public void addData(String fieldName, String data)
        {
            if (!"".Equals(data))
            {
                optionBean = new OptionBean(fieldName, data);
                arrayList_Data.Add(optionBean);
            }
        }

        /** �õ������sql*/
        public String getSql()
        {
            if (this.arrayList_Data.Count == 0)
            {
                //throw new Exception("û�в������ݣ�");
            }

            String mySql = "";//sql���
            String mySql_field = "";//sql��field����
            String mySql_data = "";//sql��data����

            OptionBean optionBean;
            //����field, data
            for (int i = 0; i < this.arrayList_Data.Count; i++)
            {
                optionBean = (OptionBean)this.arrayList_Data[i];
                mySql_field += optionBean.getLabel() + ",";
                mySql_data += optionBean.getValue() + ",";
            }
            //ȥ�����һ��","
            mySql_field = mySql_field.Substring(0, mySql_field.Length - 1);
            mySql_data = mySql_data.Substring(0, mySql_data.Length - 1);

            mySql = "INSERT INTO " + this.tableName;
            mySql += " (" + mySql_field + ") ";
            mySql += "VALUES(" + mySql_data + ")";
            return mySql;
        }
        public String getTableName()
        {
            return tableName;
        }
        public void setTableName(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("���ݿ��������Ϊ�գ�");
            }

            this.tableName = tableName;
        }
    }

    public class UpdateSqlMaker
    {

        public UpdateSqlMaker(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("���ݿ��������Ϊ�գ�");
            }
            this.tableName = tableName;
        }

        /** ����*/
        private String tableName = "";

        /**���µ�����*/
        private String where = "";

        /** ���ڱ�����������*/
        ArrayList arrayList_Data = new ArrayList();

        /** ����һ�����ݵ�bean*/
        OptionBean optionBean = null;

        /**
         * ���Ҫ���������
         * @param fieldName �ֶ���
         * @param data �ֶ����ݣ����������ַ�������''��
         */
        public void addData(String fieldName, String data)
        {
            if (!"".Equals(data))
            {
                optionBean = new OptionBean(fieldName, data);
                arrayList_Data.Add(optionBean);
            }
        }

        /** �õ������sql*/
        public String getSql()
        {
            if (this.arrayList_Data.Count == 0)
            {
            }
            if (where.Length <= 0)
            {
            }

            String mySql = "";//sql���
            mySql = "UPDATE " + this.tableName + " SET ";

            OptionBean optionBean;
            //����field, data
            for (int i = 0; i < this.arrayList_Data.Count; i++)
            {
                optionBean = (OptionBean)this.arrayList_Data[i];
                mySql += optionBean.getLabel() + "=" + optionBean.getValue() + ",";
            }
            //ȥ�����һ��","
            mySql = mySql.Substring(0, mySql.Length - 1);

            mySql += " WHERE " + this.where;
            return mySql;
        }
        public String getTableName()
        {
            return tableName;
        }
        public void setTableName(String tableName)
        {
            if (tableName.Length == 0)
            {
            }

            this.tableName = tableName;
        }
        public void setWhere(String where)
        {
            this.where = where;
        }
    }

    public class OptionBean
    {


        // ----------------------------------------------------------- Constructors


        /**
         * Construct an instance with the supplied property values.
         */
        public OptionBean(String label, String value)
        {
            this.label = label;
            this.value = value;
        }


        // ------------------------------------------------------------- Properties


        /**
         * The property which supplies the option label visible to the end user.
         */
        private String label;

        public String getLabel()
        {
            return this.label;
        }

        public void setLabel(String label)
        {
            this.label = label;
        }


        /**
         * The property which supplies the value returned to the server.
         */
        private String value;

        public String getValue()
        {
            return this.value;
        }

        public void setValue(String value)
        {
            this.value = value;
        }


    }
}
